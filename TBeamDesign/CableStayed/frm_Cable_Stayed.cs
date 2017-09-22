using System;
using System.Collections.Generic;
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

using AstraAccess.SAP_Classes;

namespace BridgeAnalysisDesign.CableStayed
{

    public partial class frm_Cable_Stayed : Form
    {
        IApplication iApp = null;

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
                        break;
                    case eDesignStandard.BritishStandard:
                        return iApp.Tables.BS_SteelBeams;
                        break;
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
                    return "DESIGN OF CABLE STAYED BRIDGE [BS]";
                return "DESIGN OF CABLE STAYED BRIDGE [IRC]";
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
                string fname = Path.Combine(Path.GetDirectoryName(Input_Data_2D), "Nonlinear Analysis Input Data.TXT");
                return fname;
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
        public string user_path { get; set; }


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
        public frm_Cable_Stayed(IApplication app)
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
            iApp.Write_LiveLoad_LL_TXT(user_path);

            File.WriteAllLines(Input_Data, list_data.ToArray());
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
                txt_Ana_member_load.Lines = lis.ToArray();
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

            if (chk_Ana_active_SIDL.Checked)
            {
                load_lst.AddRange(txt_Ana_member_load.Lines);

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
                    load_lst.AddRange(Get_MovingLoad_Data(Live_Load_List));
            }

            load_lst.Remove("");
            inp_file_cont.InsertRange(indx, load_lst);
            //inp_file_cont.InsertRange(indx, );
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
            txt_Ana_member_load.Lines = list_member_load.ToArray();
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

            Bridge_Analysis = new BridgeMemberAnalysis(iApp, Analysis_Report);

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

        private void btn_Ana_create_data_Click(object sender, EventArgs e)
        {
            if (IsCreateData)
            {
                //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);


                if (Path.GetFileName(user_path) != Project_Name) Create_Project();
                Input_Data = Path.Combine(user_path, "INPUT_DATA.TXT");
            }
            if (!Directory.Exists(user_path))
                Directory.CreateDirectory(user_path);
            if (!File.Exists(CSB_DATA_File))
            {
                string sf = Path.Combine(Application.StartupPath, @"DESIGN\DefaultData\Cable Stayed Bridge\" + Path.GetFileName(CSB_DATA_File));

                if (File.Exists(sf))
                    File.Copy(sf, CSB_DATA_File);
            }
            ReadData();
            //CreateData();
            try
            {
                //CreateData_2D();
                //CreateData_2D_Left();
                //CreateData_2D_Right();
                //CreateData_Bridge_Deck();
                CreateData_Total_Structure();

                Bridge_Analysis = new BridgeMemberAnalysis(iApp, Input_Data);
                Calculate_Total_Weight();
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
            cmb_analysis_file.SelectedIndex = 0;


           MessageBox.Show(this, "Analysis Input data is created as \"" + Project_Name + "\\INPUT_DATA.TXT\" inside the working folder.",
              "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //if (File.Exists(CSB_DATA_File))
            //{
            //    ReadData();
            //    Calculate_Total_Weight();
            //}
        }



        #region Create Project / Open Project

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

            MessageBox.Show(Project_Name + " is Created.", "ASTRA", MessageBoxButtons.OK);
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

        private void btn_new_design_Click(object sender, EventArgs e)
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
                    //Open_Project();
                }
            }
            else if (btn.Name == btn_new_design.Name)
            {
                //IsCreate_Data = true;
                Create_Project();
                Write_All_Data();
            }
            Button_Enable_Disable();
        }

        private void Write_All_Data()
        {
            iApp.Save_Form_Record(this, user_path);
        }


        public void All_Button_Enable(bool flag)
        {
            //btn_create_data.Enabled = flag;
            //btn_process_analysis.Enabled = flag;
            //btn_create_data.Enabled = flag;
            //btn_process_analysis
        }
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
        eASTRADesignType Project_Type
        {
            get
            {
                return eASTRADesignType.Cable_Stayed_Bridge;
            }
        }

        public void Delete_Folder(string folder)
        {
            MyList.Delete_Folder(folder);
        }

        #endregion Chiranjit [2016 09 07]





        private void btn_Ana_view_structure_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            int opt = 0;

            if (btn.Name == btn_Ana_view_structure.Name)
            {
                opt = cmb_analysis_file.SelectedIndex;
            }
            else if (btn.Name == btn_view_structure.Name)
            {
                opt = cmb_structure_file.SelectedIndex;
            }

            if (opt == 0)
            {
                if (File.Exists(Input_Data)) iApp.OpenWork(Input_Data, false);
            }
            else if (opt == 1)
            {
                if (File.Exists(Input_Data_2D_Left))
                {
                    iApp.OpenWork(Input_Data_2D_Left, false);
                }
            }
            else if (opt == 2)
            {
                if (File.Exists(Input_Data_2D_Right))
                {
                    iApp.OpenWork(Input_Data_2D_Right, false);
                }
            }
            else if (opt == 3)
            {
                if (File.Exists(Input_Data_Bridge_Deck))
                {
                    iApp.OpenWork(Input_Data_Bridge_Deck, false);
                }
            }
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
                tc_analysis.TabPages.Remove(tab_Structure_Design);
                tc_analysis.TabPages.Remove(tab_drawing);

                tc_main.TabPages.Add(tab_cable_design);
                tc_main.TabPages.Add(tab_Structure_Design);
                tc_main.TabPages.Add(tab_drawing);


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

            //Open_Project();

        }

        private void Open_Project()
        {


            //Chiranjit [2014 10 08]
            #region Select Design Option

            try
            {
                //eDesignOption edp = iApp.Get_Design_Option(Title);
                //if (edp == eDesignOption.None)
                //{
                //    this.Close();
                //}
                //else if (edp == eDesignOption.Open_Design)
                //{
                    //IsCreate_Data = false;

                    //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                    string chk_file = Path.Combine(user_path, "INPUT_DATA.TXT");
                    //Chiranjit [2013 07 28]
                    iApp.Read_Form_Record(this, user_path);


                    Input_Data = chk_file;
                    user_path = Path.GetDirectoryName(Input_Data);
                    Open_AnalysisFile();
                    IsCreateData = false;
                    Show_ReadMemberLoad(Input_Data);
                    MessageBox.Show(this, "File opened succesfully.", "ASTRA", MessageBoxButtons.OK);

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

        private void btn_Ana_process_analysis_Click(object sender, EventArgs e)
        {
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
            if (cmb_analysis_file.SelectedIndex == 0)
            {
                if (File.Exists(Input_Data))
                {
                    iApp.View_Input_File(Input_Data);
                }
            }
            else if (cmb_analysis_file.SelectedIndex == 1)
            {
                if (File.Exists(Input_Data_2D_Left))
                {
                    iApp.View_Input_File(Input_Data_2D_Left);
                }
            }
            else if (cmb_analysis_file.SelectedIndex == 2)
            {
                if (File.Exists(Input_Data_2D_Right))
                {
                    iApp.View_Input_File(Input_Data_2D_Right);
                }
            }
            else if (cmb_analysis_file.SelectedIndex == 3)
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
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.Is_Full_Analysis_Report)
                    iApp.RunExe(Analysis_Report);
                else if (frm.Is_Analysis_Result)
                    iApp.RunExe(Analysis_Result_Report);
                else if (frm.Is_Cable_Analysis_Result)
                    iApp.RunExe(Cable_Design_Report);

            }


        }
        private void btn_Calculate_Click(object sender, EventArgs e)
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
                    //pb_sections.BackgroundImage = global::BridgeAnalysisDesign.Properties.Resources.Rectangle;
                }
            }
            if (((TextBox)sender).Name == txt_sec_D.Name)
            {
                if (dval != 0)
                {
                    txt_sec_dia.Text = "0.0";
                    //pb_sections.BackgroundImage = global::BridgeAnalysisDesign.Properties.Resources.Rectangle;
                }
            }
            if (((TextBox)sender).Name == txt_sec_dia.Name)
            {
                if (dval != 0)
                {
                    txt_sec_B.Text = "0.0";
                    txt_sec_D.Text = "0.0";
                    //pb_sections.BackgroundImage = global::BridgeAnalysisDesign.Properties.Resources.Circular;
                }
            }
            //if (((TextBox)sender).Name == txt_sec_thickness.Name)
            //{
            //    if (dval != 0)
            //    {
            //        txt_sec_B.Text = "0.0";
            //        txt_sec_D.Text = "0.0";
            //        pb_sections.BackgroundImage = global::BridgeAnalysisDesign.Properties.Resources.Pylon;
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
            Results.Add("\t\t*            ASTRA Pro Release 22            *");
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
                    pictureBox1.BackgroundImage = BridgeAnalysisDesign.Properties.Resources.Cable_Stayed_Bridge;
                    img_counter++;
                    break;
                case 1:
                    pictureBox1.BackgroundImage = BridgeAnalysisDesign.Properties.Resources.CSB1;
                    img_counter++;
                    break;
                case 2:
                    pictureBox1.BackgroundImage = BridgeAnalysisDesign.Properties.Resources.CSB2;
                    img_counter++;
                    break;
                case 3:
                    pictureBox1.BackgroundImage = BridgeAnalysisDesign.Properties.Resources.CSB3;
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
                    pb_sections.BackgroundImage = Properties.Resources.Angle_Section1;
                    Read_Angle_Sections();
                    break;
                case eAppliedSection.Angle_Section2:
                    pb_sections.BackgroundImage = Properties.Resources.Angle_Section2;
                    Read_Angle_Sections();
                    break;
                case eAppliedSection.Angle_Section3:
                    pb_sections.BackgroundImage = Properties.Resources.Bottom_Chord_Bracing;
                    Read_Angle_Sections();
                    break;
                case eAppliedSection.Beam_Section1:
                    pb_sections.BackgroundImage = Properties.Resources.Beam_Section1;
                    Read_Beam_Sections();
                    break;
                case eAppliedSection.Beam_Section2:
                    pb_sections.BackgroundImage = Properties.Resources.Beam_Section2;
                    Read_Beam_Sections();
                    break;
                case eAppliedSection.Channel_Section1:
                    pb_sections.BackgroundImage = Properties.Resources.Channel_Section1;
                    Read_Channel_Sections();
                    break;
                case eAppliedSection.Channel_Section2:
                    pb_sections.BackgroundImage = Properties.Resources.Channel_Section2;
                    Read_Channel_Sections();
                    break;
                case eAppliedSection.Reactangular_Section:
                    //pb_sections.BackgroundImage = Properties.Resources.Rectangle;
                    pb_sections.BackgroundImage = Properties.Resources.Pylon;
                    break;
                case eAppliedSection.Circular_Section:
                    pb_sections.BackgroundImage = Properties.Resources.Circular;
                    break;
                case eAppliedSection.Builtup_LongGirder:
                    pb_sections.BackgroundImage = Properties.Resources.Builtup_Long;
                    Read_Angle_Sections();
                    break;
                case eAppliedSection.Builtup_CrossGirder:
                    pb_sections.BackgroundImage = Properties.Resources.Builtup_Cross;
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
            if (File.Exists(Bridge_Analysis.Analysis_File))
                iApp.OpenWork(Bridge_Analysis.Analysis_File, true);
        }

        private void txt_L1_TextChanged(object sender, EventArgs e)
        {
            double len = L1 + L2 + L3;

            double xinc = MyList.StringToDouble(txt_Ana_XINCR.Text, 10.0);


            txt_Ana_LL_load_gen.Text = (len / xinc).ToString("f0");


            dgv_SIDL[1, 0].Value = L1;
            dgv_SIDL[1, 1].Value = L2;
            dgv_SIDL[1, 2].Value = L1;
            dgv_SIDL[1, 3].Value = L2;

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
            iApp.Form_Stage_Analysis(Input_Data);
        }

        private void btn_process_analysis_Click(object sender, EventArgs e)
        {

        }

        private void btn_open_analysis_report_Click(object sender, EventArgs e)
        {

        }

        private void btn_create_data_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }

    public class CableMember
    {
        public CableMember()
        {
            ASTRA_MemberNo = 0;
            User_MemberNo = 0;


            MemberDetails = new Member();
            AnalysisForce = new MaxForce();
            AnalysisStress = new MaxForce();
            Strain = 0.0;
            Elongation = 0.0;
            //Vertical_Deflection_at_Deck = 0.0;
            //Horizontal_Deflection_at_Pylon_Top = 0.0;

            DesignResult = new List<string>();
        }
        public int ASTRA_MemberNo { get; set; }
        public int User_MemberNo { get; set; }
        public Member MemberDetails { get; set; }
        public MaxForce AnalysisForce { get; set; }
        public MaxForce AnalysisStress { get; set; }
        public double CalculatedForce { get; set; }
        public double CalculatedStress { get; set; }
        public double Strain { get; set; }
        public double Elongation { get; set; }
        public double Vertical_Deflection_at_Deck { get { return Elongation / Math.Sin(MemberDetails.InclinationAngle_in_Radian); } }
        public double Horizontal_Deflection_at_Pylon_Top { get { return Elongation / Math.Cos(MemberDetails.InclinationAngle_in_Radian); } }

        public int StartJointNo { get { return MemberDetails.StartNode.NodeNo; } }
        public double StartJoint_X { get { return MemberDetails.StartNode.X; } }
        public double StartJoint_Y { get { return MemberDetails.StartNode.Y; } }
        public double StartJoint_Z { get { return MemberDetails.StartNode.Z; } }

        public int EndJointNo { get { return MemberDetails.EndNode.NodeNo; } }
        public double EndJoint_X { get { return MemberDetails.EndNode.X; } }
        public double EndJoint_Y { get { return MemberDetails.EndNode.Y; } }
        public double EndJoint_Z { get { return MemberDetails.EndNode.Z; } }
        public double InclinationAngle { get { return MemberDetails.InclinationAngle_in_Degree; } }
        public object[] GetData()
        {
            List<object> list = new List<object>();

            //list.Add(ASTRA_MemberNo);
            //list.Add(User_MemberNo);
            //list.Add(MemberDetails.StartNode.NodeNo);
            //list.Add(MemberDetails.StartNode.X);
            //list.Add(MemberDetails.StartNode.Y);
            //list.Add(MemberDetails.StartNode.Z);
            //list.Add(MemberDetails.EndNode.NodeNo);
            //list.Add(MemberDetails.EndNode.X);
            //list.Add(MemberDetails.EndNode.Y);
            //list.Add(MemberDetails.EndNode.Z);
            //list.Add(MemberDetails.Length);
            //list.Add(MemberDetails.InclinationAngle_in_Degree);
            //list.Add(MemberForce.Force);
            //list.Add(Stress);
            //list.Add(Strain);
            //list.Add(Elongation);
            //list.Add(Vertical_Deflection_at_Deck);
            //list.Add(Horizontal_Deflection_at_Pylon_Top);

            try
            {
                list.Add(ASTRA_MemberNo);
                list.Add(User_MemberNo);

                list.Add(MemberDetails.StartNode.NodeNo);
                list.Add(MemberDetails.StartNode.X.ToString("0.000"));
                list.Add(MemberDetails.StartNode.Y.ToString("0.000"));
                list.Add(MemberDetails.StartNode.Z.ToString("0.000"));
                list.Add(MemberDetails.EndNode.NodeNo);
                list.Add(MemberDetails.EndNode.X.ToString("0.000"));
                list.Add(MemberDetails.EndNode.Y.ToString("0.000"));
                list.Add(MemberDetails.EndNode.Z.ToString("0.000"));
                list.Add(MemberDetails.Length.ToString("0.000"));
                list.Add(MemberDetails.InclinationAngle_in_Degree.ToString("0°"));
                list.Add(AnalysisForce);
                list.Add(AnalysisStress);
                list.Add(Strain.ToString("E3"));
                list.Add(Elongation.ToString("E3"));
                list.Add(Vertical_Deflection_at_Deck.ToString("E3"));
                list.Add(Horizontal_Deflection_at_Pylon_Top.ToString("E3"));

            }
            catch (Exception ex) { }

            return list.ToArray();
        }

        public object[] GetData_Extradosed()
        {
            List<object> list = new List<object>();

            //list.Add(ASTRA_MemberNo);
            //list.Add(User_MemberNo);
            //list.Add(MemberDetails.StartNode.NodeNo);
            //list.Add(MemberDetails.StartNode.X);
            //list.Add(MemberDetails.StartNode.Y);
            //list.Add(MemberDetails.StartNode.Z);
            //list.Add(MemberDetails.EndNode.NodeNo);
            //list.Add(MemberDetails.EndNode.X);
            //list.Add(MemberDetails.EndNode.Y);
            //list.Add(MemberDetails.EndNode.Z);
            //list.Add(MemberDetails.Length);
            //list.Add(MemberDetails.InclinationAngle_in_Degree);
            //list.Add(MemberForce.Force);
            //list.Add(Stress);
            //list.Add(Strain);
            //list.Add(Elongation);
            //list.Add(Vertical_Deflection_at_Deck);
            //list.Add(Horizontal_Deflection_at_Pylon_Top);

            try
            {
                list.Add(ASTRA_MemberNo);
                list.Add(User_MemberNo);

                list.Add(MemberDetails.StartNode.NodeNo);
                list.Add(MemberDetails.StartNode.X.ToString("0.000"));
                list.Add(MemberDetails.StartNode.Y.ToString("0.000"));
                list.Add(MemberDetails.StartNode.Z.ToString("0.000"));
                list.Add(MemberDetails.EndNode.NodeNo);
                list.Add(MemberDetails.EndNode.X.ToString("0.000"));
                list.Add(MemberDetails.EndNode.Y.ToString("0.000"));
                list.Add(MemberDetails.EndNode.Z.ToString("0.000"));
                list.Add(MemberDetails.Length.ToString("0.000"));
                list.Add(MemberDetails.InclinationAngle_in_Degree.ToString("0°"));
                list.Add(AnalysisForce);
                list.Add(AnalysisStress);


                double dd = AnalysisStress.Force / 100;
                list.Add(dd.ToString("0.000"));
                list.Add("1770.000");

                list.Add(dd > 1770.0 ? "NOT OK" : "OK");



                list.Add(Strain.ToString("E3"));
                list.Add(Elongation.ToString("E3"));
                list.Add(Vertical_Deflection_at_Deck.ToString("E3"));
                list.Add(Horizontal_Deflection_at_Pylon_Top.ToString("E3"));

            }
            catch (Exception ex) { }

            return list.ToArray();
        }

        public object[] ToArray()
        {
            return GetData();
        }
        public object[] ToArray_Extradosed()
        {
            return GetData_Extradosed();
        }
        public List<string> DesignResult { get; set; }
    }

    public class CableMemberCollection : IList<CableMember>
    {
        List<CableMember> list = null;

        public CableMemberCollection()
        {
            list = new List<CableMember>();
        }

        #region IList<CableMember> Members

        public int IndexOf(int user_MemberNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].User_MemberNo == user_MemberNo)
                {
                    return i;
                }
            }
            return -1;
        }
        public int IndexOf(CableMember item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].User_MemberNo == item.User_MemberNo)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, CableMember item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public CableMember this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        #endregion

        #region ICollection<CableMember> Members

        public void Add(CableMember item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(CableMember item)
        {
            return (IndexOf(item) != -1);
        }

        public void CopyTo(CableMember[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(CableMember item)
        {
            int i = IndexOf(item);
            if (i != -1)
            {
                RemoveAt(i);
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<CableMember> Members

        public IEnumerator<CableMember> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion
    }

    public class CableStayedDeckSlab
    {
        public string rep_file_name = "";
        public string rep_file_name_inner = "";
        public string rep_file_name_outer = "";
        public string user_input_file = "";
        public string user_path = "";
        public string file_path = "";
        public string system_path = "";
        public string drawing_path = "";
        public bool is_process = false;
        IApplication iApp = null;

        public TableRolledSteelAngles tbl_rolledSteelAngles = null;

        public double S, B1, B2, B, fck, fy, m, YS, D, L, Dwc, gamma_c, gamma_wc;
        public double WL, v, u, IF, CF, Q, j, sigma_st, sigma_b, tau, sigma_tf, K, sigma_p;
        public double dw, tw, bf1, tf1, ang_thk, off;
        public double bf2, tf2;


        bool isPlateArrangement = false;

        public double LongGirder_nw, LongGirder_dw, LongGirder_tw, LongGirder_nf, LongGirder_bf1, LongGirder_tf1, LongGirder_bf2, LongGirder_tf2, LongGirder_ang_thk, LongGirder_nos_ang;
        public double CrossGirder_nw, CrossGirder_dw, CrossGirder_tw, CrossGirder_nf, CrossGirder_bf1, CrossGirder_tf1, CrossGirder_bf2, CrossGirder_tf2, CrossGirder_ang_thk, CrossGirder_nos_ang;
        public string LongGirder_ang_name, CrossGirder_ang_name;


        public double LongGirder_Moment, CrossGirder_Moment;
        public double LongGirder_Shear, CrossGirder_Shear;
        public double deff = 1.5;

        public double des_moment, des_shear;
        public int nw, nf, na;
        public string ang = "";
        public string LongGirder_ang = "";
        public string CrossGirder_ang = "";

        string _A, _B, _C, _G, _D, _E, _F, _bd1, _sp1, _bd2, _sp2;
        string _v, _u, _1, _2, _3, _4, _6, _7, _8, _10;

        public bool flg = false;

        public List<string> DesignSummery { get; set; }
        public List<string> DesignResult { get; set; }

        public bool IsPlateArrangement
        {
            get
            {
                return isPlateArrangement;
            }
            set
            {
                isPlateArrangement = value;
            }
        }
        public bool IsBoxArrangement
        {
            get
            {
                return !isPlateArrangement;
            }
            set
            {
                isPlateArrangement = !value;
            }
        }
        public CableStayedDeckSlab(IApplication app)
        {
            iApp = app;
            _A = "";
            _B = "";
            _C = "";
            _D = "";
            _E = "";
            _F = "";
            _bd1 = "";
            _sp1 = "";
            _bd2 = "";
            _sp2 = "";


            DesignResult = new List<string>();
            DesignSummery = new List<string>();
        }
        #region User Method

        public void Calculate_Program()
        {
            string ref_string = "";
            frmCurve f_c = null;

            string ang_name = "";
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                DesignResult.Clear();
                DesignSummery.Clear();
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 22            *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services       *");
                sw.WriteLine("\t\t*                                            *");
                sw.WriteLine("\t\t*        DESIGN OF CABLE STAYED BRIDGE       *");
                sw.WriteLine("\t\t*                                            *");
                sw.WriteLine("\t\t**********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");

                #endregion

                #region USER DATA

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Bridge Span [S] = {0} m", S);
                sw.WriteLine("Carriageway Width [B1] = {0} m    Marked as (A) in the Drawing", B1);
                _A = B1 + " m.";

                sw.WriteLine("Footpath Width [B2] = {0} m", B2);
                sw.WriteLine("Spacing on either side of Main Girders [B] = {0} m     Marked as (B) in the Drawing", B);
                _B = B + " m.";


                sw.WriteLine();
                sw.WriteLine("Concrete Grade [fck] = M {0:f0} = {0:f0} N/sq.mm", fck);
                sw.WriteLine("Reinforcement Steel Frade [fy] = Fe {0:f0} = {0:f0} N/sq.mm", fy);
                sw.WriteLine("Modular Ratio [m] = {0}", m);
                sw.WriteLine("Rolled Steel Section of Yield Stress [YS] = {0} N/sq.mm", YS);
                sw.WriteLine("SLAB Thickness [D] = {0} mm     Marked as (C) in the Drawing", D);
                _C = D / 1000.0 + " m.";



                sw.WriteLine("Panel Length [L] = {0} m        Marked as (D) in the Drawing", L);
                _D = L + " m.";


                sw.WriteLine("Thickness of wearing course [Dwc] = {0} mm     Marked as (G) in the Drawing", Dwc);
                _G = Dwc / 1000.0 + " m.";


                sw.WriteLine();
                sw.WriteLine("Unit Weight of Concrete [γ_c] = {0} kN/cu.m", gamma_c);
                sw.WriteLine("Unit Weight of wearing cource [γ_wc] = {0} kN/cu.m", gamma_wc);
                sw.WriteLine("Tracked Vehicle Load [WL] = {0} kN", WL);
                sw.WriteLine("Length of Loaded area [v] = {0} m     Marked as (E) in the Drawing", v);
                _E = v + " m.";


                sw.WriteLine("Width of Loaded area [u] = {0} m      Marked as (F) in the Drawing", u);
                _F = u + " m.";

                sw.WriteLine();
                sw.WriteLine("Impact Factor [IF] = {0}", IF);
                sw.WriteLine("Continuity Factor [CF] = {0}", CF);
                sw.WriteLine("Moment Factor [Q] = {0}", Q);
                sw.WriteLine("Lever Arm Factor [j] = {0}", j);
                sw.WriteLine("[σ_st] = {0} N/sq.mm", sigma_st);
                sw.WriteLine("Permissible Bending Stress in Steel [σ_b] = {0} N/sq.mm", sigma_b);
                sw.WriteLine("Permissible Shear Stress in Steel [τ] = {0} N/sq.mm", tau);
                sw.WriteLine("Permissible Shear Stress through fillet Weld [σ_tf] = {0} N/sq.mm", sigma_tf);
                sw.WriteLine("Constant ‘K’ = {0}", K);
                sw.WriteLine("Permissible Bearing Stress [σ_p] = {0} N/sq.mm", sigma_p);
                sw.WriteLine();
                sw.WriteLine();
                //==================================================

                //sw.WriteLine("Flange Plates : nf =4, Bf=1000 mm, tf=20 mm");
                //sw.WriteLine("Angles: na = 4, 100 x 100 x 10");
                //sw.WriteLine("==================================================");
                //sw.WriteLine("For Quarter Span Section (L/4), ");
                //sw.WriteLine("Starting from Deff  to L/4 ");
                //sw.WriteLine("And  Deff  L-L/4  to  L-Deff, ");
                //sw.WriteLine("User given Sections are as follows:");

                //sw.WriteLine("Web Plates : nw =1, Dw=1600 mm, tw=20 mm");
                //sw.WriteLine("Flange Plates : nf =2, Bf=1000 mm, tf=20 mm");
                //sw.WriteLine("Angles: na = 4, 100 x 100 x 10");
                //sw.WriteLine("==================================================");
                //sw.WriteLine("For Span Section (Deff) from either End up to distance equals to Effective Depth of Girder, ");
                //sw.WriteLine("Starting from Deff  to L/4 ");
                //sw.WriteLine("And  Deff  L-L/4  to  L-Deff, ");
                //sw.WriteLine("User given Sections are as follows:");

                //sw.WriteLine("Web Plates : nw =1, Dw=1600 mm, tw=20 mm");
                //sw.WriteLine("Flange Plates : nf =2, Bf=1000 mm, tf=20 mm");
                //sw.WriteLine("Angles: na = 4, 100 x 100 x 10");

                sw.WriteLine("");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                #endregion


                int step = 1;
                string step_text = "";
                double u_by_B = 0.0;

                #region Report

                do
                {
                    if (step == 1)
                    {
                        dw = LongGirder_dw;
                        nw = (int)LongGirder_nw;
                        tw = LongGirder_tw;
                        bf1 = LongGirder_bf1;
                        tf1 = LongGirder_tf1;
                        bf2 = LongGirder_bf2;
                        tf2 = LongGirder_tf2;
                        nf = (int)LongGirder_nf;



                        des_moment = LongGirder_Moment;
                        des_shear = LongGirder_Shear;

                        na = (int)LongGirder_nos_ang;
                        ang = LongGirder_ang;
                        ang_thk = LongGirder_ang_thk;

                        ang_name = LongGirder_ang_name;
                        step_text = "LONG GIRDER";
                    }
                    else if (step == 2)
                    {
                        dw = CrossGirder_dw;
                        nw = (int)CrossGirder_nw;
                        tw = CrossGirder_tw;
                        bf1 = CrossGirder_bf1;
                        nf = (int)CrossGirder_nf;
                        tf1 = CrossGirder_tf1;

                        bf2 = CrossGirder_bf2;
                        tf2 = CrossGirder_tf2;


                        des_moment = CrossGirder_Moment;
                        des_shear = CrossGirder_Shear;

                        na = (int)CrossGirder_nos_ang;
                        ang = CrossGirder_ang;
                        ang_thk = CrossGirder_ang_thk;
                        ang_name = CrossGirder_ang_name;

                        step_text = "CROSS GIRDER";
                    }


                    #region STEP 1 : DESIGN OF STEEL PALTE GIRDER
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0} : DESIGN OF STEEL PLATE GIRDER for {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.1 : Analysis forces and User given section Details at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    double M = des_moment;
                    sw.WriteLine("Design Bending Moment = M = {0} kN-m", M);
                    double deg_sh_frc = des_shear;
                    sw.WriteLine("Design shear force = V = {0} kN", des_shear);
                    sw.WriteLine();
                    sw.WriteLine();
                    //DesignSummery.Add("==============================================================================");
                    if (step == 1)
                    {
                        DesignSummery.Add("==========================      LONG GIRDER       ===============================");
                    }
                    else if (step == 2)
                    {
                        DesignSummery.Add("==========================     CROSS GIRDER       ===============================");
                    }

                    sw.WriteLine();
                    DesignSummery.Add("");
                    DesignSummery.Add(string.Format("Web Plates : Number of Plates [nw] = {0}, Depth [dw] = {1} mm, Thickness [tw] = {2} mm", nw, dw, tw));
                    DesignSummery.Add("");

                    sw.WriteLine("Web Plates : Number of Plates [nw] = {0}, Depth [dw] = {1} mm, Thickness [tw] = {2} mm", nw, dw, tw);
                    sw.WriteLine();
                    DesignSummery.Add(string.Format("Flange Plates : Number of Plates [nf] = {0}, Breadth [bf] = {1} mm, Thickness [tf] = {2} mm", nf, bf1, tf1));
                    DesignSummery.Add("");
                    //sw.WriteLine("Top Flange Plates : Number of Plates [nf] = {0}, Breadth [bf] = {1} mm, Thickness [tf] = {2} mm", nf, bf1, tf1);
                    sw.WriteLine("Top Flange Plates    :    Breadth [bf1] = {0} mm,   Thickness [tf1] = {1} mm", bf1, tf1);
                    sw.WriteLine("Bottom Flange Plates :    Breadth [bf2] = {0} mm,   Thickness [tf2] = {1} mm", bf2, tf2);


                    RolledSteelAnglesRow tab_data = null;

                    try
                    {
                        tab_data = iApp.Tables.Get_AngleData_FromTable(ang_name, ang, ang_thk);
                    }
                    catch (Exception ex) { }
                    //sw.WriteLine("Angles : Number of Angles = {0}, {1} x {2}", na, ang, ang_thk);
                    sw.WriteLine();
                    DesignSummery.Add("");
                    if (tab_data != null)
                    {
                        DesignSummery.Add(string.Format("Angles : Number of Angles [na] = {0}, Size: {1} {2}X{3}", na, tab_data.SectionName, tab_data.SectionSize, ang_thk));
                        sw.WriteLine("Angles : Number of Angles [na] = {0}, Size: {1} {2}X{3}", na, tab_data.SectionName, tab_data.SectionSize, ang_thk);
                        DesignSummery.Add("");
                    }
                    DesignSummery.Add("==============================================================================");
                    DesignSummery.Add("");
                    DesignSummery.Add("");
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.2 : Size of Web Plate and Flange Plate at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    //sw.WriteLine("Approximate depth of Girder = S /10 = {0} / 10 = {1} m", S, (S / 10.0));
                    double dval = (S / 2) + 2;
                    double flan_over_hang = 0;
                    sw.WriteLine("Approximate Web depth of Girder  = S /{0} = {1}/ {0} = {2} m", dval, S, (S / dval));
                    sw.WriteLine();

                    double eco_depth_girder = (M * 10E5) / sigma_b;

                    eco_depth_girder = Math.Pow(eco_depth_girder, (1.0 / 3.0));
                    eco_depth_girder = 5.0 * eco_depth_girder;


                    sw.WriteLine("Economical Web depth of Girder = 5 * (M / σb)^(1/3)");
                    sw.WriteLine("                               = 5 * ({0} * 10E5 / {1})^(1/3)", M, sigma_b);
                    eco_depth_girder = Double.Parse(eco_depth_girder.ToString("0"));
                    sw.WriteLine("                               = {0:f3} mm", eco_depth_girder);
                    sw.WriteLine();
                    sw.WriteLine("Thickness of web plate for shear considerations");
                    sw.WriteLine("Web depth by asuming thickness of plate = tw = {0} mm thick plate for shear considerations", tw);
                    //sw.WriteLine("asuming thickness of plate = tw");
                    //sw.WriteLine();
                    //sw.WriteLine("Web depth by {0} mm thick plate for shear considerations", tw);
                    sw.WriteLine();
                    double web_depth = (deg_sh_frc * 1000) / (tau * tw);
                    sw.WriteLine("    = V / (τ * {0})", tw);
                    sw.WriteLine("    = {0} * 1000 / ({1}*{2})", deg_sh_frc, tau, tw);
                    web_depth = Double.Parse(web_depth.ToString("0.00"));
                    sw.WriteLine("    = {0} mm", web_depth);
                    sw.WriteLine();
                    sw.WriteLine("Let us provide Web Depth = Dw = {0}mm  (user given value)", dw);
                    sw.WriteLine();
                    //
                    //double dw, tw;
                    //dw = 1000.0;
                    //tw = 10.0;
                    double Aw = nw * dw * tw;

                    dval = 16 * tw;
                    sw.WriteLine("Permissible overhang for flange =  16 * tw");
                    sw.WriteLine("                                =  16 * {0}", tw);
                    sw.WriteLine("                                =  {0} mm", dval);
                    sw.WriteLine();

                    flan_over_hang = (bf1 - tw) / 2.0;
                    sw.WriteLine("In present case the flange overhang  = (bf - tw) / 2.0");
                    sw.WriteLine("                                     = ({0} - {1}) / 2.0", bf1, tw);
                    //sw.WriteLine("                                     = {0} mm", flan_over_hang);
                    IsBoxArrangement = flan_over_hang > dval;
                    if (IsBoxArrangement)
                    {
                        //sw.WriteLine("In present case the flange overhang  = (bw - tf) / 2.0");
                        //sw.WriteLine("                                     = ({0} - {1}) / 2.0", bf, tw);
                        sw.WriteLine("                                     = {0:G3}mm > {1:G3}mm", flan_over_hang, dval);
                    }
                    else
                    {
                        //sw.WriteLine("In present case the flange overhang  = (bw - tf) / 2.0");
                        //sw.WriteLine("                                     = ({0} - {1}) / 2.0", bf, tw);
                        sw.WriteLine("                                     = {0:G3}mm < {1:G3}mm", flan_over_hang, dval);
                        //sw.WriteLine("                                     = {0} mm", flan_over_hang);


                        sw.WriteLine();
                        //sw.WriteLine("Number of Flange Please will be equaly divided on Upper and Lower Side of the Box Girder");
                        //sw.WriteLine("It is suggested to go for Steel Box Girder with 2 webs suitable plates");
                        //sw.WriteLine();
                    }

                    sw.WriteLine();
                    sw.WriteLine("");

                    int sides = IsBoxArrangement ? 2 : 1;
                    if (IsBoxArrangement)
                    {
                        sw.WriteLine("So, a Box Girder arrangement is considered, ");
                        sw.WriteLine("For Box Girder, sides =2, and , ");
                        sw.WriteLine("");
                    }
                    else
                    {
                        sw.WriteLine("So, a Plate Girder arrangement is considered, ");
                        sw.WriteLine("For Plate Girder, sides =1, and , ");
                        sw.WriteLine();
                    }
                    //sw.WriteLine("Let us provide the Web depth = {0}mm, thickness = {1}mm on either side,", dw, tw);
                    sw.WriteLine();
                    sw.WriteLine("Approximate Total Flange area required = Af = (M / (σ_b  x  dw)) - (dw  x  tw / 6)");

                    double Af = (M * 1000000 / (sigma_b * dw)) - (dw * tw / 6.0);


                    sw.WriteLine("                                            = ({0:E3} / ({1}  x  {2})) - ({2}  x  {3} / 6)",
                        (M * 1000000), sigma_b, dw, tw);
                    sw.WriteLine();
                    sw.WriteLine("                                            = {0:G3}  sq.mm", Af);
                    sw.WriteLine();

                    //So, a Box Girder arrangement is considered, 
                    //For Plate Girder, sides =1, and 
                    //For Box Girder, sides =2,     (Chiranjit, Hard Code this values)

                    //Let us provide the Web depth=1600 mm, thickness=20mm on either side, (User given values)

                    //Approximate Total Flange area required =
                    //Af = (M / (σ_b  x  dw)) - (dw  x  tw / 6)
                    //     = (30110  x  10^6) / (165  x  1500))  -  (1500  x  20) / 6
                    //     = 116656.6  sq.mm

                    //sw.WriteLine("Flange width = Bf = S /40 to S / 45");

                    double Bf = 0.0;
                    if ((S * 1000 / 40) > 500 && (S * 1000 / 45) <= 1500)
                    {
                        Bf = 1000.0;
                    }
                    else if ((S * 1000 / 40) > 0 && (S * 1000 / 45) <= 500)
                    {
                        Bf = 500;
                    }
                    else if ((S * 1000 / 40) > 1500 && (S * 1000 / 45) <= 2000)
                    {
                        Bf = 1500;
                    }

                    sw.WriteLine("Flange width = Bf = S /40 to S / 45");
                    //Flange width = Bf = S /40 to S / 45
                    sw.WriteLine("                  = ({0} * 1000) / 40  to  ({0} * 1000) / 45", S);
                    sw.WriteLine("                  = {0:f0} to {1:f0}", (S * 1000 / 40), (S * 1000 / 45));
                    sw.WriteLine("                  = {0:f0} mm (Say)", Bf);
                    sw.WriteLine();
                    //             = (46 * 1000) / 40  to  (46 * 1000) / 45
                    //             = 1150  to  1022 mm
                    //             = 1000 mm (Say)

                    //sw.WriteLine("Let us provide {0} Flange plates having Flange width={1} mm, thickness= {2} mm on the Bottom,", nf, Bf, tf);
                    //sw.WriteLine("Let us provide {0} Flange plates having Flange width={1} mm, thickness= {2} mm on the Bottom,", nf, bf1, tf1);
                    sw.WriteLine();
                    sw.WriteLine();
                    //sw.WriteLine("and {0} Flange plates having Flange width={1} mm, thickness={2}mm at the Bottom,", nf, bf, tf);
                    sw.WriteLine();
                    //Let us provide 4 Flange plates having Flange width=1000 mm, thickness=20mm on the top,
                    // And 4 Flange plates having Flange width=1000 mm, thickness=20mm at the Bottom, (User given values)

                    Aw = sides * nw * dw * tw;
                    sw.WriteLine("Finally Web Plates  :    sides x nw x Dw x tw, Area = Aw = {0} x {1} x {2} x {3} = {4:f3} Sq. mm", sides, nw, dw, tw, Aw);
                    //Finally Web Plates      sides x nw x Dw x tw, Area = Aw = 2 x 1 x 1600 x 20 = 64,000 Sq. mm
                    Af = bf1 * tf1;
                    double Af2 = bf2 * tf2;
                    sw.WriteLine();
                    sw.WriteLine("Top Flange Plates     :  bf1 x tf1, Area = Af1 = {0} x {1} = {2:f3} Sq. mm", bf1, tf1, Af);
                    sw.WriteLine();
                    sw.WriteLine("Bottom Flange Plates  :  bf2 x tf2, Area = Af2 = {0} x {1} = {2:f3} Sq. mm", bf2, tf2, Af2);
                    //            Flange Plates   sides x nf x Bf x tf, Area = Af = 2 x 4 x 1000 x 20 = 160,000 Sq. mm
                    sw.WriteLine();




                    //sw.WriteLine("Provided size of Web Plate is {0} * {1} * {2} = Aw       Marked as (3) in the Drawing", nw, dw, tw);
                    //sw.WriteLine("Let us try Web as {0} mm * {1} mm = dw * tw = Aw       Marked as (3) in the Drawing", dw, tw);

                    //(3) = Web depth x thickness = 1000 mm * 10 mm

                    _3 = string.Format("Size of Web Plate = {0} * {1} * {2} sq.mm", nw, dw, tw);


                    double i1 = sides * (nw * (tw * dw * dw * dw) / 12.0);


                    double i2 = (1 / 12.0) * (bf1 * Math.Pow((dw + (nf * tf1)), 3.0) - bf1 * dw * dw * dw);
                    double i4 = 0.0;
                    double i3 = 0.0;

                    if (tab_data != null)
                        i3 = ((tab_data.Ixx * 10000) + (tab_data.Area * 100) * (dw - (tab_data.Cxx * tab_data.Cxx * 100)));


                    double I = 0.0;





                    //sw.WriteLine();
                    //sw.WriteLine("Moment of Inertia  = I = (nw * (tw * dw^3) / 12.0) + ");
                    //sw.WriteLine("                         (1 / 12.0) * (bf * (dw + (nf * tf))^3) - bf * dw^3)");
                    //sw.WriteLine("                         (Ixx + a * (dw - (Cxx * Cxx)))");
                    //sw.WriteLine();
                    //sw.WriteLine("                       = ({0} * ({1} * {2}^3) / 12.0) + ", nw, tw, dw);
                    //sw.WriteLine("                         (1 / 12.0) * ({0} * ({1} + ({2} * {3}))^3) - {0} * {1}^3) + ",
                    //                                            bf, dw, nf, tf);
                    //sw.WriteLine("                         ({0} + {1} * ({2} - ({3} * {3})))", tab_data.Ixx * 10000, tab_data.Area * 100, dw, tab_data.Cxx * 10);
                    //sw.WriteLine();

                    sw.WriteLine();
                    sw.WriteLine("Moment of Inertia = I");
                    sw.WriteLine();
                    if (tab_data != null)
                        sw.WriteLine("I = MI of Web ({0}x{1}x{2}) + MI of both Flanges ({3}x{4}x{5}) + MI of Four Connecting Angles ({6}x{7}x{8})", nw, dw, tw, nf, bf1, tf1, na, ang, ang_thk);
                    else
                        sw.WriteLine("I = MI of Web ({0}x{1}x{2}) + MI of both Flanges ({3}x{4}x{5}) ", nw, dw, tw, nf, bf1, tf1, na, ang, ang_thk);

                    sw.WriteLine();

                    sw.WriteLine("  = sides * (nw  *  (tw  *  dw^3 )/ 12.0)  ");
                    sw.WriteLine("    +  (bf1 * tf1^3 / 12.0) + (bf1 * tf1 * (dw / 2.0 + tf1 / 2.0)^2.0))");
                    sw.WriteLine("    +  (bf2 * tf2^3 / 12.0) + (bf2 * tf2 * (dw / 2.0 + tf2 / 2.0)^2.0))");
                    if (tab_data != null)
                        sw.WriteLine("    + sides * ( na * (Ixx + a * (dw / 2.0 - Cxx)^2)))");


                    sw.WriteLine();
                    sw.WriteLine("  =  {0} * ({1} *  ({2} * {3}^3 )/ 12.0)", sides, nw, tw, dw);
                    sw.WriteLine("    +  ({0} * {1}^3 / 12.0) + ({0} * {1} * ({2} / 2.0 + {1} / 2.0)^2.0))", bf1, tf1, dw);
                    sw.WriteLine("    +  ({0} * {1}^3 / 12.0) + ({0} * {1} * ({2} / 2.0 + {1} / 2.0)^2.0))", bf2, tf2, dw);
                    if (tab_data != null)
                        sw.WriteLine("    +  {0} * ({1} * ({2}*10^4 + {3} * ({4} / 2.0 - {5})^2)))", sides, na, tab_data.Ixx, tab_data.Area * 100, dw, tab_data.Cxx * 10);
                    sw.WriteLine();
                    if (tab_data != null)
                        sw.WriteLine("Note : Angles are to be provided at Top and Bottom on either side of each Web Plate ");
                    sw.WriteLine();


                    i1 = sides * (nw * (tw * dw * dw * dw) / 12.0);
                    i2 = (bf1 * tf1 * tf1 * tf1 / 12.0) + (bf1 * tf1 * Math.Pow((dw / 2.0 + tf1 / 2.0), 2.0));
                    i3 = (bf1 * tf1 * tf1 * tf1 / 12.0) + (bf1 * tf1 * Math.Pow((dw / 2.0 + tf1 / 2.0), 2.0));

                    if (tab_data != null)
                        i4 = sides * (na * (tab_data.Ixx * 10000 + tab_data.Area * 100 * ((dw / 2.0 - tab_data.Cxx * 10) * (dw / 2.0 - tab_data.Cxx * 10))));



                    I = i1 + i2 + i3 + i4;
                    sw.WriteLine();


                    sw.WriteLine();
                    sw.WriteLine("  = {0:E3} sq.sq.mm", I);

                    double y = dw / 2.0 + tf1;

                    sw.WriteLine();
                    sw.WriteLine();

                    //sw.WriteLine("Approximate Flange area required");
                    // Af = ((M * 10E5) / (sigma_b * dw)) - (Aw / 6);
                    //Af = Double.Parse(Af.ToString("0"));

                    //sw.WriteLine();
                    //sw.WriteLine("Af = (M / (σ_b * dw)) - (Aw / 6)");
                    //sw.WriteLine("   = ({0} * 10E5) / ({1} * {2})) - (dw * tw) / 6", M, sigma_b, dw);
                    //sw.WriteLine("   = ({0} * 10E5) / ({1} * {2})) - ({2} * {3}) / 6", M, sigma_b, dw, tw);
                    //sw.WriteLine("   = {0} sq.mm", Af);
                    sw.WriteLine();

                    //sw.WriteLine("Flange width = Bf = S /40 to S / 45");
                    //sw.WriteLine("             = ({0} * 1000) / 40  to  ({0} * 1000) / 45", S);

                    //double Bf1 = S * 1000 / 40.0;
                    //double Bf2 = S * 1000 / 45.0;
                    // Bf = (Bf1 > Bf2) ? Bf1 : Bf2;

                    //Bf = (int)(Bf / 100.0);
                    //Bf += 1;
                    //Bf *= 100.0;

                    //sw.WriteLine("             = {0:f0}  to  {1:f0} mm", Bf1, Bf2);
                    //sw.WriteLine();
                    //sw.WriteLine();






                    //dval = 16 * tw * nw;
                    //sw.WriteLine("Permissible overhang for flange =  16 * tw * nw ");
                    //sw.WriteLine("                                =  16 * {0} * {1} ", tw, nw);
                    //sw.WriteLine("                                =  {0} mm", dval);
                    //sw.WriteLine();

                    //flan_over_hang = (bf - tw) / 2.0;
                    //sw.WriteLine("In present case the flange overhang = (bf - tw) / 2.0");
                    //sw.WriteLine("                                    = ({0} - {1}) / 2.0", bf, tw);
                    //sw.WriteLine("                                    = {0} mm", flan_over_hang);

                    //if (dval > flan_over_hang)
                    //{
                    //    sw.WriteLine();
                    //    sw.WriteLine("Number of Web Please will be equaly divided on Either Side of the Box Girder");
                    //    //sw.WriteLine("Number of Flange Please will be equaly divided on Upper and Lower Side the Box Girder");
                    //    sw.WriteLine("It is suggested to go for Steel Box Girder with 2 webs suitable plates");
                    //    sw.WriteLine();
                    //}
                    Bf = bf1;
                    //sw.WriteLine("So, Provided Size of Flange Plate = {0} x {1} x {2} x {3}     Marked as (4) in the Drawing ", sides, nf, Bf, tf1);
                    //(4) = Flange width x thickness = 500 mm * 30 mm

                    _4 = string.Format("Flange Size = {0} * {1} * {2} * {3} ", sides, nf, bf1, tf1);

                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.3 : Check for Maximum Stresses at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine();
                    //     and                 y = dw / 2 + (nf x tf)/2
                    //= 1000/2 + (2 x 20)/2
                    //       = 520 mm
                    y = (dw / 2.0 + (nf * tf2) / 2.0);
                    sw.WriteLine("  and   y = dw / 2 + tf2 / 2");
                    sw.WriteLine("          = {0} / 2 + {1} / 2", dw, tf2);
                    sw.WriteLine("          = {0:f3} mm", y);
                    sw.WriteLine();

                    double appl_stress = (M * 1000000 * y) / I;
                    appl_stress = Double.Parse(appl_stress.ToString("0"));
                    sw.WriteLine("Applied Stress = M * y  / I");
                    sw.WriteLine("               = {0:G3} * {1:G3}  / {2:G3}", (M * 1000000), y, I);
                    sw.WriteLine();
                    sw.WriteLine();
                    if (appl_stress < sigma_b)
                    {
                        sw.WriteLine("               = {0:G5} N/sq.mm < σ_b = {1:G5} N/sq.mm, OK", appl_stress, sigma_b);
                    }
                    else
                    {
                        DesignResult.Add("");
                        DesignResult.Add(string.Format("Design Failed At Step {0}.3 [{1}]", step, step_text));
                        DesignResult.Add(string.Format("Applied Stress  = {0:G5} N/sq.mm > σ_b = {1:G5} N/sq.mm, NOT OK, Need resizing.", appl_stress, sigma_b));
                        sw.WriteLine("               = {0:G5} N/sq.mm > σ_b = {1:G5} N/sq.mm, NOT OK, Need resizing.", appl_stress, sigma_b);
                    }
                    sw.WriteLine();

                    u_by_B = deg_sh_frc;
                    double V = u_by_B;
                    deg_sh_frc = des_shear;
                    //v = u_by_B;

                    double tau1 = V * 1000 / (dw * tw);
                    tau1 = double.Parse(tau1.ToString("0"));
                    sw.WriteLine("Average Shear Stress = τ1");
                    sw.WriteLine("                     = V * 1000 / (dw * tw)");
                    sw.WriteLine("                     = {0} * 1000 / ({1} * {2})", V, dw, tw);
                    sw.WriteLine("                     = {0} N/sq.mm", tau1);
                    sw.WriteLine();

                    double ratio = (dw / tw);
                    sw.WriteLine("Ratio dw / tw = {0} / {1} = {2}", dw, tw, ratio);
                    sw.WriteLine();
                    sw.WriteLine("Considering Stiffener Spacing = c = dw = {0} mm", dw);
                    sw.WriteLine();

                    // Calculate from Table 1
                    // **Problem How to calculate value from Table1 ?
                    double tau2 = Get_Table_1_Value(100, 1, ref ref_string);
                    //double tau2 = 87;
                    sw.WriteLine("From Table 1 (Given at the end of the Report) : {0} ", ref_string);
                    //sw.WriteLine("Allowable average Shear Stress = {0} N/Sq mm = t2", tau2);
                    sw.WriteLine();

                    if (tau2 > tau1)
                    {
                        sw.WriteLine("Allowable average Shear Stress = {0} N/Sq. mm > {1} N/Sq mm,    OK", tau2, tau1);
                        sw.WriteLine();
                        sw.WriteLine("So, Average shear stress is within Safe permissible Limits.", tau1, tau2);
                    }
                    else
                    {
                        DesignResult.Add("");
                        DesignResult.Add(string.Format("Design Failed At Step {0}.3 [{1}]", step, step_text));
                        DesignResult.Add(string.Format("Allowable average Shear Stress = {0} N/Sq. mm < {1} N/Sq mm,   NOT OK", tau2, tau1));
                        sw.WriteLine("Allowable average Shear Stress = {0} N/Sq. mm < {1} N/Sq mm,   NOT OK", tau2, tau1);
                        sw.WriteLine();
                        sw.WriteLine("So, Average shear stress is not within Safe permissible limits.", tau1, tau2);
                    }
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.4 : Connection Between Flange and Web at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    sw.WriteLine("Maximum Shear Force at the junction of Flange and Web is given by");
                    sw.WriteLine();

                    sw.WriteLine("    τ  = V * a * y  / I");
                    double a = bf2 * tf2;
                    Bf = bf2;
                    y = dw / 2.0 + (tf2 / 2.0);
                    //I = double.Parse(I.ToString("0"));

                    sw.WriteLine("    a = bf2 * tf2 = {0} * {1} = {2:f2} sq.mm", bf2, tf2, a);
                    sw.WriteLine();
                    sw.WriteLine("    y = dw/2 + tf2/2 = {0}/2 + {1}/2 = {2} mm", dw, tf2, y);
                    sw.WriteLine();
                    sw.WriteLine("    I = {0:G5} sq.sq.mm", I);
                    sw.WriteLine();
                    sw.WriteLine("    V = {0} * 1000 N", V);
                    sw.WriteLine();

                    //tau = (V * 1000 * a * y) / (I * 10E6);
                    tau = (V * 1000 * a * y) / (I);
                    //tau = double.Parse(tau.ToString("0"));
                    //sw.WriteLine("τ = 548 * 1000 * 15000 * 515 / (879 * 107) = 483 N/mm");
                    sw.WriteLine("    τ  = {0} * 1000 * {1} * {2}  / ({3:G5})", V, a, y, I);
                    sw.WriteLine("       = {0:G5} N/mm", tau);
                    sw.WriteLine();

                    sw.WriteLine("Adopting Continuous weld on either side, strength of weld of size ");
                    sw.WriteLine();
                    sw.WriteLine("  ‘S’ = 2 * k * S * σ_tf");

                    double _S = 2 * K * sigma_tf;
                    _S = double.Parse(_S.ToString("0"));
                    sw.WriteLine("      = 2 * {0} * S * {1}", K, sigma_tf);
                    sw.WriteLine("      = {0:G5} * S", _S);
                    sw.WriteLine();


                    sw.WriteLine("Equating, {0} * S = {1:G5},                S = {1:G5} / {0} = {2:G5} mm", _S, tau, (tau / _S));
                    sw.WriteLine();

                    _S = tau / +_S;

                    _S = (int)_S;
                    _S += 2;

                    sw.WriteLine("Use {0} mm Fillet Weld, continuous on either side.     Marked as (5) in the Drawing", _S);
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.5 : Intermediate Stiffeners at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    double val1, val2;

                    val1 = dw / tw;
                    if (val1 < 85)
                    {
                        sw.WriteLine("dw / tw = {0} / {1} = {2} < 85", dw, tw, val1);
                        sw.WriteLine("So, Vertical Stiffeners are required");
                        //sw.WriteLine("else, Vertical Stiffeners are not required.");
                    }
                    else
                    {
                        sw.WriteLine("dw / tw = {0} / {1} = {2} < 85", dw, tw, val1);
                        //sw.WriteLine("So, Vertical Stiffeners are required");
                        sw.WriteLine("So, Vertical Stiffeners are not required.");
                    }
                    sw.WriteLine();

                    double sp_stifn1 = 0.33 * dw;
                    double sp_stifn2 = 1.5 * dw;
                    sw.WriteLine("Spacing of Stiffeners = 0.33 * dw  to  1.5 * dw");
                    sw.WriteLine("            = 0.33 * {0}  to  1.5 * {0}", dw);
                    sw.WriteLine("            = {0} mm to {1} mm", sp_stifn1, sp_stifn2);
                    sw.WriteLine();

                    double c = 1000;

                    sw.WriteLine("Adopt Spacing = c = {0} mm", c);
                    sw.WriteLine();


                    sw.WriteLine("Required minimum Moment of Inertia of Stiffeners");
                    sw.WriteLine();


                    double _I = ((1.5 * dw * dw * dw * tw * tw * tw) / (c * c));
                    sw.WriteLine("I = 1.5 * dw**3 * tw**3 / c**2");
                    sw.WriteLine("  = 1.5 * {0}^3 * {1}^3 / {2}^2", dw, tw, c);
                    _I = _I / 10E4;
                    _I = double.Parse(_I.ToString("0"));
                    sw.WriteLine("  = {0} * 10E4 sq.sq.mm", _I);
                    sw.WriteLine();

                    double t = 10; // t is Constant?

                    sw.WriteLine("Use {0} mm thick plate, t = {0} mm", t);
                    sw.WriteLine();
                    sw.WriteLine("Maximum width of plate not to exceed = 12 * t = {0} mm", (12 * t));
                    sw.WriteLine();

                    // 80 ?
                    double h = 80;
                    sw.WriteLine("Use 80 mm size plate, h = 80 mm");
                    sw.WriteLine();
                    sw.WriteLine("Plate size is {0} mm * {1} mm      Marked as (6) in the Drawing", h, t);
                    _6 = string.Format("{0} mm x {1} mm", h, t);



                    sw.WriteLine();

                    double _I1 = (t * (h * h * h)) / 3.0;
                    //_I1 = _I1 / 10E4;
                    //_I1 = double.Parse(_I1.ToString("0"));

                    if (_I1 > _I)
                    {
                        sw.WriteLine("I = {0} * 80**3 / 3 = {1:e2} sq.sq.mm > {2:e2} sq.sq.mm,                OK", t, _I1, _I);
                    }
                    else
                    {
                        DesignResult.Add("");
                        DesignResult.Add(string.Format("Design Failed At Step {0}.5 [{1}]", step, step_text));
                        DesignResult.Add(string.Format("I = {0} * 80**3 / 3 = {1:e2} sq.sq.mm < {2:e2} sq.sq.mm,     NOT OK", t, _I1, _I));
                        DesignResult.Add("");
                        sw.WriteLine("I = {0} * 80**3 / 3 = {1:e2} sq.sq.mm < {2:e2} sq.sq.mm,     NOT OK", t, _I1, _I);
                    }
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.6 : Connections of Vertical Stiffener to Web at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    sw.WriteLine("Shear on weld connecting stiffener to Web");
                    sw.WriteLine();

                    // 125 = constant ?
                    double sh_wld_wb = 125 * tw * tw / h;
                    sw.WriteLine("    = 125 * tw*tw / h");
                    sw.WriteLine("    = 125 * {0}*{0} / {1}", tw, h);

                    sh_wld_wb = double.Parse(sh_wld_wb.ToString("0.00"));
                    sw.WriteLine("    = {0} kN/m", sh_wld_wb);
                    sw.WriteLine("    = {0} N/mm", sh_wld_wb);
                    sw.WriteLine();

                    double sz_wld = sh_wld_wb / (K * sigma_tf);
                    sz_wld = double.Parse(sz_wld.ToString("0.00"));
                    sw.WriteLine("Size of welds = 156.25 / (K * σ_tf)");
                    sw.WriteLine("              = {0} / ({1} * {2})", sh_wld_wb, K, sigma_tf);
                    sw.WriteLine("              = {0} mm", sz_wld);
                    sw.WriteLine();

                    //sw.WriteLine("Size of welds = 156.25 / (K * σtf) = 156.25 / (0.7 * 102.5) = 2.17 mm");
                    // How come 100 and 5?
                    sw.WriteLine("Use 100 mm Long 5 mm Fillet Welds alternately on either side.     Marked as (7) in the Drawing");

                    //(7)  5-100-100 (weld)
                    _7 = string.Format("5-100-100 (weld)");
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.7 : End Bearing Stiffeners at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    sw.WriteLine("Maximum Shear Force = V = {0} kN", V);
                    sw.WriteLine();

                    val1 = (h / t);
                    if (val1 < 12)
                    {
                        sw.WriteLine("The end bearing Stiffeners is designed as a column h / t < 12");
                    }
                    else
                    {
                        sw.WriteLine("The end bearing Stiffeners is designed as a column h / t > 12");
                    }
                    sw.WriteLine();
                    h = 180;
                    sw.WriteLine("Use ‘h’ = outstand of stiffeners = {0} mm", h);
                    sw.WriteLine();
                    t = h / 12;
                    sw.WriteLine("t = {0} / 12 = {1} mm", h, (h / 12.0));
                    sw.WriteLine();
                    sw.WriteLine("Use plate of size 180 mm * 15 mm     Marked as (8) in the Drawing");

                    //(8)  180 x15 mm

                    _8 = string.Format("180 x 15 mm");
                    sw.WriteLine();

                    double brng_ar_req = V * 1000 / sigma_p;
                    sw.WriteLine();
                    sw.WriteLine("Bearing area required = V * 1000 / σ_p");
                    sw.WriteLine("                      = {0} * 1000 / {1} sq.mm", V, sigma_p);
                    brng_ar_req = double.Parse(brng_ar_req.ToString("0"));
                    sw.WriteLine("                      = {0} sq.mm", brng_ar_req);
                    sw.WriteLine();

                    double tot_area = 2 * h * t;
                    sw.WriteLine("If two plates are used,");
                    sw.WriteLine("     Total area = 2 * {0} * {1}", h, t);
                    if (tot_area > brng_ar_req)
                    {
                        sw.WriteLine("                = {0} sq.mm > {1} sq.mm", tot_area, brng_ar_req);
                    }
                    else
                    {
                        sw.WriteLine("                = {0} sq.mm < {1} sq.mm", tot_area, brng_ar_req);
                    }
                    sw.WriteLine();

                    sw.WriteLine("The length of Web plate which acts along with Stiffener ");
                    sw.WriteLine("plates in bearing the reaction = lw = 20 * tw");
                    sw.WriteLine("                               = 20 * {0}", tw);
                    double brng_reaction = 20 * tw;
                    double lw = brng_reaction;
                    sw.WriteLine("                               = {0} mm", lw);
                    sw.WriteLine();
                    _I = ((t * (2 * h + 10) * (2 * h + 10) * (2 * h + 10)) / 12) + (2 * lw * tw * tw * tw / 12);

                    //**lw = ?
                    sw.WriteLine("    I = t * (2 * h + 10)^3 / 12 + 2 * lw * tw**3 / 12");
                    sw.WriteLine("      = {0} * (2 * {1} + 10)^3 / 12 + 2 * {2} * {3}^3 / 12", t, h, 200, tw);
                    //sw.WriteLine("      = 15 * 3703 / 12 + 2 * 200 * 103 / 12");
                    _I = (_I / 10E3);
                    _I = double.Parse(_I.ToString("0"));

                    sw.WriteLine("      = {0} * 10E3 Sq Sq mm", _I);
                    sw.WriteLine();

                    double A = 2 * h * t + 2 * lw * tw;
                    A = double.Parse(A.ToString("0"));
                    sw.WriteLine("    Area = A = 2 * h * t + 2 * lw * tw");
                    sw.WriteLine("         = 2 * {0} * {1} + 2 * {2} * {3}", h, t, lw, tw);
                    sw.WriteLine("         = {0} sq.mm", A);

                    sw.WriteLine();

                    double r = (_I * 10E3) / A;
                    r = Math.Sqrt(r);
                    r = double.Parse(r.ToString("0"));
                    sw.WriteLine("    r = √(I / A) = √({0} * 10E3 / {1}) = {2} mm", _I, A, r);
                    sw.WriteLine();


                    // ** 0.7 = ?
                    double _L = 0.7 * dw;
                    double lamda = (_L / r);
                    lamda = double.Parse(lamda.ToString("0.00"));
                    sw.WriteLine("    λ = Slenderness ratio = L / r");
                    sw.WriteLine();
                    sw.WriteLine("    L = Effective Length of stiffeners");
                    sw.WriteLine("      = 0.7 * dw");
                    sw.WriteLine("      = 0.7 * {0}", tw);
                    sw.WriteLine("      = {0} mm", _L);
                    sw.WriteLine();
                    sw.WriteLine("    λ = {0} / {1}", _L, r);
                    sw.WriteLine("      = {0}", lamda);

                    sw.WriteLine();

                    double sigma_ac = Get_Table_2_Value(lamda, 1, ref ref_string);
                    sw.WriteLine("    From Table 2 (given at the end of the Report) : {0}", ref_string);

                    sigma_ac = double.Parse(sigma_ac.ToString("0"));
                    sw.WriteLine("    Permissible Stress in axial compression σ_ac = {0} N/sq.mm", sigma_ac);
                    sw.WriteLine();

                    double area_req = V * 1000 / sigma_ac;
                    area_req = double.Parse(area_req.ToString("0"));
                    sw.WriteLine("    Area required = V * 1000 / σ_ac ");
                    sw.WriteLine("                  = {0} * 1000 / {1}", V, sigma_ac);
                    if (area_req < A)
                    {
                        sw.WriteLine("                  = {0} sq.mm < {1} sq.mm,  Ok", area_req, A);
                    }
                    else
                    {
                        sw.WriteLine("                  = {0} sq.mm > {1} sq.mm,  NOT OK", area_req, A);
                    }
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.8 : Connection between Bearing Stiffener and Web at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    //** 40 = ?
                    double len_alt = 2 * (dw - 40);
                    sw.WriteLine("Length available for alternate intermittent weld");
                    sw.WriteLine("   = 2 * (dw - 40)");
                    sw.WriteLine("   = 2 * ({0} - 40)", dw);
                    sw.WriteLine("   = {0} mm", len_alt);
                    sw.WriteLine();

                    double req_strnth_wld = (v * 1000 / len_alt);
                    req_strnth_wld = double.Parse(req_strnth_wld.ToString("0"));
                    sw.WriteLine("Required strength of weld = v * 1000 / 1920");
                    sw.WriteLine("                          = {0} * 1000 / {1}", v, len_alt);
                    sw.WriteLine("                          = {0} N/mm", req_strnth_wld);
                    sw.WriteLine();

                    sz_wld = req_strnth_wld / (K * sigma_tf);
                    sz_wld = double.Parse(sz_wld.ToString("0.00"));


                    //** σ_ac =  138 but 102.5 = ?
                    //sw.WriteLine("Size of weld = 286 / (K * σ_ac) = 286 / (0.7 * 102.5) = 3.98 mm");
                    sw.WriteLine("Size of weld = 286 / (K * σ_tf)");
                    sw.WriteLine("             = {0} / ({1} * {2})", req_strnth_wld, K, sigma_tf);
                    sw.WriteLine("             = {0} mm", sz_wld);
                    sw.WriteLine();

                    if (sz_wld < 5)
                        sz_wld = 5;
                    else
                    {
                        sz_wld = (int)sz_wld;
                        sz_wld += 1;
                    }
                    sw.WriteLine("Use {0} mm Fillet Weld", sz_wld);
                    sw.WriteLine();

                    double len_wld = 10 * tw;

                    sw.WriteLine("Length of Weld >= 10 * tw = 10 * {0} = {1} mm", tw, len_wld);
                    sw.WriteLine();

                    sw.WriteLine("Use {0} mm Long, {1} mm Weld Alternately.     Marked as (9) in the Drawing", len_wld, sz_wld);
                    sw.WriteLine();


                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP {0}.9 : Properties of Composite Section at {1}", step, step_text);
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();

                    double Ace = B * 1000 * D / m;
                    Ace = double.Parse(Ace.ToString("0"));

                    sw.WriteLine("Ace = B * 1000 * D/m");
                    sw.WriteLine("    = {0} * 1000 * {1}/{2}", B, D, m);
                    sw.WriteLine("    = {0} sq.mm", Ace);
                    sw.WriteLine();
                    sw.WriteLine("The centroid of Composite Section (Neutral Axis) is determined");
                    sw.WriteLine("by first moment of the areas about axis xx,");

                    Bf = bf1;

                    double Axy = Ace * (dw + 2 * tf1 + D / 2) + Bf * tf1 * (dw + tf1 + tf1 / 2) + dw * tw * (dw / 2 + tf1) + Bf * tf1 * tf1 / 2;

                    sw.WriteLine();
                    sw.WriteLine("Axy  = Ace * (dw + 2 * tf1 + D/2) + bf1 * tf1 * (dw + tf1 + tf1/2) ");
                    sw.WriteLine("       + dw * tw * (dw/2 +tf1) + Bf * tf1 * tf1/2");
                    sw.WriteLine();
                    sw.WriteLine("     = {0} * ({1} + 2 * {2} + {3}/2) + {4} * {2} * ({1} + {2} + {2}/2) ", Ace, dw, tf1, D, Bf);
                    sw.WriteLine("       + {0} * {1} * ({0}/2 + {2}) + {3} * {2} * {2}/2", dw, tw, tf1, Bf);
                    sw.WriteLine();

                    Axy = double.Parse(Axy.ToString("0"));
                    sw.WriteLine("     = {0}", Axy);
                    //sw.WriteLine("= 77046340");
                    double _A_d = Ace + (dw / 2) * tf1 + dw * tw + (dw / 2.0) * tf1;
                    _A_d = double.Parse(_A_d.ToString("0"));
                    // ** formula ?
                    sw.WriteLine();
                    sw.WriteLine("A = Ace + (dw / 2) * tf1 + dw * tw + (dw / 2.0) * tf1");
                    sw.WriteLine("  = {0} + ({1} / 2) * {2} + {0} * {3} + ({1} / 2.0) * {2}", Ace, dw, tf1, tw);
                    sw.WriteLine("  = {0} sq.mm", _A_d);
                    sw.WriteLine();

                    y = Axy / _A_d;
                    // ** sign y bar ?
                    sw.WriteLine("  y = Axy / A = {0} / {1}", Axy, _A_d);
                    //sw.WriteLine("    = {0:f0}", y);

                    y = double.Parse(y.ToString("0"));
                    sw.WriteLine("    = {0} mm", y);
                    sw.WriteLine();

                    double yc = dw + 2 * tf1 + D / 2 - y;

                    sw.WriteLine("  yc = dw + 2 * tf1 + D/2 -  y");
                    sw.WriteLine("     = {0} + 2 * {1} + {2}/2 -  {3}", dw, tf1, D, y);
                    sw.WriteLine("     = {0} mm", yc);
                    sw.WriteLine();


                    double Icomp = Ace * yc * yc +
                        (Bf * (dw + (2 * tf1)) * (dw + (2 * tf1)) * (dw + (2 * tf1))) / 12.0
                        - ((Bf - tw) * dw * dw * dw) / 12.0 +
                        (_A_d - Ace) * (y - (dw / 2.0) - tf1) * (y - (dw / 2.0) - tf1);


                    sw.WriteLine("Icomp = distance from centre of Deck Slab to Centroid of Composite Section");

                    sw.WriteLine("      = Ace * yc * yc ");
                    sw.WriteLine("        + (bf1 * (dw + (2 * tf1))^3 ) / 12.0");
                    sw.WriteLine("        - ((bf1 - tw) * dw**3) / 12.0 ");
                    sw.WriteLine("        + (A - Ace) * (y - (dw / 2.0) - tf1) * (y - (dw / 2.0) - tf1)");
                    sw.WriteLine();

                    sw.WriteLine("      = {0} * {1} * {1} ", Ace, yc);
                    sw.WriteLine("        + ({0} * ({1} + (2 * {2}))^3 ) / 12.0", Bf, dw, tf1);
                    sw.WriteLine("        - (({0} - {1}) * {2}^3) / 12.0 ", Bf, tw, dw);
                    sw.WriteLine("        + ({0} - {1}) * ({2} - ({3} / 2.0) - {4}) * ({2} - ({3} / 2.0) - {4})", _A_d, Ace, y, dw, tf1);
                    sw.WriteLine();




                    Icomp = Icomp / 10E9;
                    Icomp = double.Parse(Icomp.ToString("0.000"));
                    sw.WriteLine("      = {0} * 10E9 sq.sq.mm", Icomp);
                    sw.WriteLine();

                    sw.WriteLine("Maximum Shear force at junction of Slab and Girder is obtained by");

                    tau = (v * 1000 * Ace * yc) / (Icomp * 10E9);
                    sw.WriteLine("τ = v * 1000 * Ace *  yc / Icomp");
                    sw.WriteLine("  = {0} * 1000 * {1} * {2} / {3} * 10E9", v, Ace, yc, Icomp);
                    tau = double.Parse(tau.ToString("0"));
                    sw.WriteLine("  = {0} N/mm", tau);
                    sw.WriteLine();

                    double Q1 = tau * Bf;
                    Q1 = double.Parse(Q1.ToString("0"));
                    sw.WriteLine("Total Shear force at junction Q1 =  τ * Bf1 ");
                    sw.WriteLine("                                 =  {0} * {1}", tau, Bf);
                    sw.WriteLine("                                 =  {0} N", Q1);
                    sw.WriteLine();

                    double _do = 20.0;
                    sw.WriteLine("Using do = {0} mm diameter mild steel studs,     Marked as (10) in the Drawing", _do);
                    _10 = string.Format("{0} Ø Studs", _do);

                    sw.WriteLine("capacity of one shear connector is given by,");
                    sw.WriteLine();
                    // 196 = ?
                    double Q2 = 196 * _do * _do * Math.Sqrt(fck);
                    Q2 = double.Parse(Q2.ToString("0"));
                    sw.WriteLine("    Q2 = 196 * do*do *  √fck");
                    sw.WriteLine("       = 196 * {0}*{0} *  √{1}", _do, fck);
                    sw.WriteLine("       = {0} N", Q2);
                    sw.WriteLine();

                    // 5 = ?
                    double H = 5 * 20;
                    sw.WriteLine("Height of each stud = H");
                    sw.WriteLine("                    = 5 * do");
                    sw.WriteLine("                    = 5 * {0}", _do);
                    sw.WriteLine("                    = {0} mm", H);
                    sw.WriteLine();

                    double no_std_row = (Q1 / Q2);
                    no_std_row = double.Parse(no_std_row.ToString("0.00"));
                    sw.WriteLine("Number of studs required in a row");
                    sw.WriteLine();
                    if (no_std_row < 1.0)
                    {
                        sw.WriteLine("  = Q1 / Q2 = {0} / {1} = {2} < 1", Q1, Q2, no_std_row);
                    }
                    else
                    {
                        sw.WriteLine("  = Q1 / Q2 = {0} / {1} = {2} > 1", Q1, Q2, no_std_row);
                    }
                    sw.WriteLine("So, Provide a minimum of 2 mild Steel Studs in a row");
                    sw.WriteLine();

                    double N = 2;
                    double fs = 2.0;
                    double p = N * Q2 / (fs * tau);
                    p = double.Parse(p.ToString("0"));
                    sw.WriteLine("Pitch of Shear Connectors = p = N * Q2 / (fs * τ)");

                    sw.WriteLine("N = Number of Shear Connectors in a row = 2");
                    sw.WriteLine();
                    sw.WriteLine("Fs = Factor of Safety = 2.0");
                    sw.WriteLine();
                    sw.WriteLine("p = 2 * {0} / (2 * {1})", Q2, tau);
                    sw.WriteLine("  = {0} mm", p);
                    sw.WriteLine();

                    sw.WriteLine("Maximum permissible pitch is the lowest value of:");
                    sw.WriteLine();
                    sw.WriteLine("(i)     3 * Thickness of Slab = 3 * {0} = {1:f0} mm", D, (3 * D));
                    sw.WriteLine("(ii)    4 * Height of Stud = 4 * (5 * do) = 4 * {0:f0} = {1:f0} mm", (5 * _do), (4 * 5 * _do));
                    sw.WriteLine("(iii)   600 mm");
                    sw.WriteLine();
                    sw.WriteLine("Hence provide the pitch of 400 mm in the longitudinal direction.    Marked as (11) in the Drawing");

                    #endregion
                    step++;
                }
                while (step <= 3);



                #region STEP 2 : COMPUTATION OF Permanent Load
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP {0} : DESIGN OF RCC DECK SLAB", step);
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP {0}.1 : COMPUTATION OF PERMANENT LOAD", step);
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double self_weight_deck_slab = (D / 1000.0) * gamma_c;

                sw.WriteLine("Self weight of Deck Slab = (D/1000) * γ_c");
                sw.WriteLine("                         = ({0:f2}) * {1:f0}", (D / 1000), gamma_c);
                sw.WriteLine("                         = {0:f2} kN/sq.mm", self_weight_deck_slab);
                sw.WriteLine();

                double self_weight_wearing_course = (Dwc / 1000.0) * gamma_wc;
                sw.WriteLine("Self weight of wearing course = (Dwc/1000) * γ_wc");
                sw.WriteLine("                              = {0:f2} * {1}", (Dwc / 1000), gamma_wc);
                sw.WriteLine("                              = {0:f2} kN/sq.mm", self_weight_wearing_course);
                sw.WriteLine();
                double DL = self_weight_deck_slab + self_weight_wearing_course;

                sw.WriteLine("Total Load = DL ");
                sw.WriteLine("           = {0:f2} + {1:f2}", self_weight_deck_slab, self_weight_wearing_course);
                sw.WriteLine("           = {0:f2} kN/sq.mm", DL);
                DL = (int)DL;
                DL += 1.0;
                sw.WriteLine("           = {0:f2} kN/sq.mm", DL);
                #endregion

                #region STEP 2.2 : BENDING MOMENT BY MOVING LOAD
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP {0}.2 : BENDING MOMENT BY MOVING LOAD", step);
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("Load = WL = {0} kN", WL);
                sw.WriteLine("Panel Dimension L = {0:f2} m,                B = {1:f2} m", L, B);
                sw.WriteLine("Load Dimension v = {0:f2}, u = {1:f2} m", v, u);
                sw.WriteLine();
                sw.WriteLine("Considering 45° Load dispersion through wearing Course");
                sw.WriteLine();

                double _v = v + (2 * (Dwc / 1000.0));
                sw.WriteLine("    v = {0:f2} + (2*{1:f2}) = {2:f2} m.", v, (Dwc / 1000.0), _v);
                sw.WriteLine();
                //sw.WriteLine("      = {0:f2} m.", _v);
                double _u = u + (2 * (Dwc / 1000.0));
                sw.WriteLine("    u = {0:f2} + (2*{1:f2}) = {2:f2} m.", u, (Dwc / 1000.0), _u);
                sw.WriteLine();
                //sw.WriteLine("      = {0:f2} m.", _u);


                u_by_B = v;
                v = _v;
                _v = u_by_B;

                u_by_B = u;
                u = _u;
                _u = u_by_B;



                u_by_B = u / B;
                sw.WriteLine("    u / B = {0:f2} / {1:f2} = {2:f3}", u, B, u_by_B);
                sw.WriteLine();
                double v_by_L = v / L;

                sw.WriteLine("    v / L = {0:f2} / {1:f2} = {2:f3}", v, L, v_by_L);
                sw.WriteLine();

                double k = B / S;
                sw.WriteLine("    K = B / S = {0:f2} / {1:f2} = {2:f3}", B, L, k);
                sw.WriteLine();


                k = Double.Parse(k.ToString("0.0"));
                if (k < 0.4)
                    k = 0.4;
                if (k > 1.0) k = 1.0;
                f_c = new frmCurve(k, u_by_B, v_by_L, LoadType.PartialLoad);
                //if (k == 0.4)
                //{
                f_c.txt_m1.Text = "0.085";
                f_c.txt_m2.Text = "0.017";
                //}
                f_c.ShowDialog();
                double m1, m2;
                m1 = f_c.m1;
                m2 = f_c.m2;

                sw.WriteLine();
                sw.WriteLine("From Pigeaud’s Curves, for K = {0:f1}", k);
                sw.WriteLine("    m1 = {0}", m1);
                sw.WriteLine("    m2 = {0}", m2);

                double _MB = WL * (m1 + 0.15 * m2);
                _MB = double.Parse(_MB.ToString("0"));
                sw.WriteLine();
                sw.WriteLine("Short Span Bending Moment = MB’");
                sw.WriteLine("                          = WL * (m1 + 0.15 * m2)");
                sw.WriteLine("                          = {0} * ({1} + 0.15 * {2})", WL, m1, m2);
                sw.WriteLine("                          = {0} kN-m", _MB);
                sw.WriteLine();

                double MB1 = IF * CF * _MB;
                MB1 = double.Parse(MB1.ToString("0"));

                sw.WriteLine("Bending Moment including Impact and Continuity Factor");
                sw.WriteLine("  = MB1");
                sw.WriteLine("  = IF * CF * MB’ ");
                sw.WriteLine("  = {0} * {1:f2} * {0:f2} ", IF, CF, _MB);
                sw.WriteLine("  = {0} kN-m", MB1);
                sw.WriteLine();

                double _ML = WL * (m2 + 0.15 * m1);

                sw.WriteLine("Long Span Bending Moment = ML’ ");
                sw.WriteLine("                         = WL * (m2 + 0.15 * m1) ");
                sw.WriteLine("                         = {0} * ({1} + 0.15 * {2}) ", WL, m2, m1);
                sw.WriteLine("                         = {0:f2} kN-m", _ML);
                sw.WriteLine();

                double ML1 = IF * CF * _ML;
                sw.WriteLine("Bending Moment including Impact and Continuity Factor");
                sw.WriteLine("  = ML1");
                sw.WriteLine("  = IF * CF * ML’ ");
                sw.WriteLine("  = {0} * {1} * {2:f2} ", IF, CF, _ML);
                sw.WriteLine("  = {0:f2} kN-m", ML1);
                sw.WriteLine();
                #endregion

                #region STEP 2.3 : BENDING MOMENT BY Permanent Load
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP {0}.3 : BENDING MOMENT BY PERMANENT LOAD", step);
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("Permanent Load of Deck Slab = DL = {0} kN/sq.mm", DL);

                double WD = DL * B * L;
                sw.WriteLine("Permanent Load per Panel = WD");
                sw.WriteLine("                     = DL * B * L");
                sw.WriteLine("                     = {0} * {1} * {2}", DL, B, L);
                sw.WriteLine("                     = {0:f2} kN", WD);
                sw.WriteLine();
                sw.WriteLine("u / B = 1 and  v / L = 1");

                k = B / L;
                k = Double.Parse(k.ToString("0.000"));
                sw.WriteLine("k = B / L = {0:f2} / {1:f2} = {2:f1}", B, L, k);
                sw.WriteLine("1/k = 1 / {0} = {1:f2}", k, (1 / k));

                f_c = new frmCurve(k, 1.0, 1.0, LoadType.FullyLoad);

                k = Double.Parse(k.ToString("0.0"));
                //if (k == 0.4)
                //{
                f_c.txt_m1.Text = "0.047";
                f_c.txt_m2.Text = "0.006";
                //}
                f_c.ShowDialog();

                m1 = f_c.m1;
                m2 = f_c.m2;
                double MB, ML;

                sw.WriteLine();
                sw.WriteLine("Using Pigeaud’s Curves, m1 = {0} and m2 = {1}", m1, m2);
                sw.WriteLine();
                _MB = WD * (m1 + 0.15 * m2);
                sw.WriteLine("Short Span Bending Moment = MB’");
                sw.WriteLine("                          = WD * (m1 + 0.15 * m2)");
                sw.WriteLine("                          = {0:f2} * ({1} + 0.15 * {2})", WD, m1, m2);
                sw.WriteLine("                          = {0:f2} kN-m", _MB);
                sw.WriteLine();


                sw.WriteLine("Short Span Bending Moment including Continuity Factor");
                sw.WriteLine("  = MB2");

                double MB2 = CF * _MB;
                sw.WriteLine("  = CF * MB’");
                sw.WriteLine("  = {0:f2} * {1:f2}", CF, _MB);
                sw.WriteLine("  = {0:f2} kN-m", MB2);
                sw.WriteLine();

                _ML = WD * (m2 + 0.15 * m1);
                sw.WriteLine("Long Span Bending Moment = ML’");
                sw.WriteLine("                         = WD * (m2 + 0.15 * m1)");
                sw.WriteLine("                         = {0:f2} * ({1} + 0.15 * {2})", WD, m2, m1);
                sw.WriteLine("                         = {0:f2} kN-m", _ML);
                sw.WriteLine();

                double ML2 = CF * _ML;
                sw.WriteLine("Long Span Bending Moment including Continuity Factor");
                sw.WriteLine("  = ML2");
                sw.WriteLine("  = CF * ML’");
                sw.WriteLine("  = {0:f2} * {1:f2}", CF, _ML);
                sw.WriteLine("  = {0:f2} kN-m", ML2);
                sw.WriteLine();
                sw.WriteLine("Design Bending Moments are:");
                MB = MB1 + MB2;

                sw.WriteLine("Along Short Span = MB");
                sw.WriteLine("                 = MB1 + MB2");
                sw.WriteLine("                 = {0:f2} + {1:f2}", MB1, MB2);
                sw.WriteLine("                 = {0:f2} kN-m", MB);
                sw.WriteLine();


                ML = ML1 + ML2;
                sw.WriteLine("Along Long Span = ML");
                sw.WriteLine("                = ML1 + ML2");
                sw.WriteLine("                = {0:f2} + {1:f2}", ML1, ML2);
                sw.WriteLine("                = {0:f2} kN-m", ML);
                sw.WriteLine();

                #endregion

                #region STEP 2.4 : DESIGN OF SECTION FOR RCC DECK SLAB
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP {0}.4 : STRUCTURAL DETAILING FOR RCC DECK SLAB", step);
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();



                double d = (MB * 10E5) / (Q * 1000.0);

                d = Math.Sqrt(d);
                d = double.Parse(d.ToString("0"));
                sw.WriteLine("d = √((MB * 10E5) / (Q*b))");
                sw.WriteLine("  = √(({0:f2} * 10E5) / ({1:f3}*1000))", MB, Q);
                sw.WriteLine("  = {0} mm", d);
                sw.WriteLine();

                sw.WriteLine("The overall depth of RCC Deck Slab = {0} mm", D);

                double _d = d;
                d = D - 40.0;
                sw.WriteLine();
                sw.WriteLine("Effective Depth = {0} - 40 = {1} mm = d", D, d);


                double Ast = MB * 10E5 / (sigma_st * j * d);
                Ast = double.Parse(Ast.ToString("0"));
                sw.WriteLine();
                sw.WriteLine("Required steel along short span");
                sw.WriteLine("    = Ast");
                sw.WriteLine("    = (MB * 10E5) / (σ_st * j * d)");
                sw.WriteLine("    = ({0:f2} * 10E5) / ({1} * {2} * {3})", MB, sigma_st, j, d);
                sw.WriteLine("    = {0} sq.mm", Ast);

                List<double> lst_dia = new List<double>();

                lst_dia.Add(10);
                lst_dia.Add(12);
                lst_dia.Add(16);
                lst_dia.Add(20);
                lst_dia.Add(25);
                lst_dia.Add(32);


                int dia_indx = 0;
                double dia = lst_dia[0];
                double _ast = 0.0;
                double no_bar = 0.0;
                double spacing = 140;
                do
                {
                    dia = lst_dia[dia_indx];
                    no_bar = (1000.0 / spacing);
                    _ast = Math.PI * dia * dia / 4.0;
                    _ast = _ast * no_bar;
                    dia_indx++;
                }
                while (_ast < Ast);

                sw.WriteLine();
                sw.WriteLine("Provide T{0:f0} mm bars at {1:f0} mm c/c.     Marked as (1) in the Drawing", dia, spacing);
                //(1) = T12 mm bars at 140 mm c/c.
                _1 = string.Format("T{0:f0} mm bars at {1:f0} mm c/c.", dia, spacing);

                sw.WriteLine();

                sw.WriteLine("Effective depth for Long span using T10 mm bars");
                sw.WriteLine();

                double d1 = d - (dia / 2.0) - (10.0 / 2.0);
                sw.WriteLine("    d1 = d - ({0:f0}/2) - (10/2)", dia);
                sw.WriteLine("       = {0} - {1:f0} - 5", d, (dia / 2.0));
                sw.WriteLine("       = {0:f0} mm", d1);
                sw.WriteLine();

                Ast = (ML * 10E5) / (sigma_st * j * d1);
                Ast = double.Parse(Ast.ToString("0"));

                sw.WriteLine();
                sw.WriteLine("Required steel along long span");
                sw.WriteLine("  = Ast");
                sw.WriteLine("  = ML * 10E5 / (σ_st * j * d1)");
                sw.WriteLine("  = {0:f2} * 10E5 / ({1} * {2} * {3})", ML, sigma_st, j, d1);
                sw.WriteLine("  = {0} sq.mm", Ast);
                sw.WriteLine();

                spacing = 150;
                dia_indx = 0;
                do
                {
                    dia = lst_dia[dia_indx];
                    no_bar = (1000.0 / spacing);
                    _ast = Math.PI * dia * dia / 4.0;
                    _ast = _ast * no_bar;
                    dia_indx++;
                }
                while (_ast < Ast);

                sw.WriteLine("Provide T{0:f0} Bars at {1:f0} mm c/c.    Marked as (2) in the Drawing", dia, spacing);
                //(2) = T10 Bars at 150 mm c/c.
                _2 = string.Format("T{0:f0} mm bars at {1:f0} mm c/c.", dia, spacing);

                #endregion



                sw.WriteLine();
                sw.WriteLine();
                Write_Table_1(sw);
                Write_Table_2(sw);

                #endregion

                #region END OF REPORT
                sw.WriteLine();
                sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("Table 1: Allowable Average Shear Stress in Stiffened Webs");
                //sw.WriteLine("         of Steel Confirming to IS: 226 (IRC:24-1967)");
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("d                Shear Stress (N/mm2) for different distances between stiffeners");
                //sw.WriteLine("t     0.4d     0.6d     0.8d    1.0d     1.2d    1.4d    1.5d");
                //sw.WriteLine("-----------------------------------------------------------------------");

                //sw.WriteLine("110    87       87       87      87       87      87      87");
                //sw.WriteLine("130    87       87       87      87       87      84      82");
                //sw.WriteLine("150    87       87       87      85       80      77      75");
                //sw.WriteLine("170    87       87       83      80       76      72      70");
                //sw.WriteLine("190    87       87       79      75");
                //sw.WriteLine("200    87       85       77");
                //sw.WriteLine("220    87       80       73");
                //sw.WriteLine("240    87       77");
                //sw.WriteLine("-----------------------------------------------------------------------");


                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("Table 2: Allowable Working Stress σac in N/mm2 on Effective");
                //sw.WriteLine("        Cross Section for Axial Compression (IRC: 24 - 1967)");
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("            σy = Yield Stress of Steel (N/mm2) ");
                //sw.WriteLine("λ= (L/r)  ______________________________________");
                //sw.WriteLine("           236        299        331       362");
                //sw.WriteLine("---------------------------------------------------");
                //sw.WriteLine("0         140.0      171.2       191.5    210.0");
                //sw.WriteLine("20        136.0      167.0       186.0    204.0");
                //sw.WriteLine("40        130.0      157.0       174.0    190.0");
                //sw.WriteLine("60        118.0      139.0       151.6    162.0");
                //sw.WriteLine("80        101.0      113.5       120.3    125.5");
                //sw.WriteLine("100        80.5       87.0        90.2     92.7");
                //sw.WriteLine("120        63.0       66.2        68.0     69.0");
                //sw.WriteLine("140        49.4       51.2        52.0     52.6");
                //sw.WriteLine("160        39.0       40.1        40.7     41.1");
                //sw.WriteLine("---------------------------------------------------");





                if (DesignSummery.Count != 0)
                {
                    sw.WriteLine("---------------------------------------------------------------------------");
                    sw.WriteLine("---------------------       DESIGN SUMMARY       --------------------------");
                    sw.WriteLine("---------------------------------------------------------------------------");
                    sw.WriteLine();
                    //sw.WriteLine("BRIDGE SPAN = L = 46.000m. Depth of Girder = Deff = 1.5m.");
                    sw.WriteLine("BRIDGE SPAN = L = {0:f3}m. Depth of Girder = Deff = {1:f3}m.", S, deff);
                    if (IsBoxArrangement)
                    {
                        sw.WriteLine("A Steel Box Girder arrangement is selected,");
                        sw.WriteLine("Providing number of Web Plates as mentioned below on either side of the Box,");
                    }
                    else
                    {
                        sw.WriteLine("A Steel Plate Girder arrangement is selected,");
                        sw.WriteLine("Providing number of Web Plates as mentioned below at the Centre of the Girder, in between Flanges,");

                    }

                    sw.WriteLine();





                    foreach (string s in DesignSummery)
                    {
                        sw.WriteLine(s);
                    }
                    //sw.WriteLine();
                    //sw.WriteLine("---------------------------------------------------------------------------");
                    //sw.WriteLine("---------------------   END OF DESIGN SUMMARY    --------------------------");
                    //sw.WriteLine("---------------------------------------------------------------------------");
                    //sw.WriteLine();

                }
                //sw.WriteLine("---------------------------------------------------------------------------");
                //sw.WriteLine("---------------------        DESIGN RESULT       --------------------------");
                //sw.WriteLine("---------------------------------------------------------------------------");
                //sw.WriteLine();
                sw.WriteLine("DESIGN RESULT : ");

                if (DesignResult.Count != 0)
                {
                    foreach (string s in DesignResult)
                    {
                        sw.WriteLine(s);
                    }
                }
                else
                    sw.WriteLine("DESIGN IS FOUND OK");
                sw.WriteLine();
                //sw.WriteLine("---------------------------------------------------------------------------");
                //sw.WriteLine("---------------------    END OF DESIGN RESULT    --------------------------");
                //sw.WriteLine("---------------------------------------------------------------------------");
                //sw.WriteLine();

                #endregion
            }
            catch (Exception ex) { }
            finally
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------            Thank you for using ASTRA Pro          ---------------");
                sw.WriteLine("---------------------------------------------------------------------------");

                sw.Flush();
                sw.Close();
            }
        }

        #region IReport Members

        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region User Data

                sw.WriteLine("S = {0}", S);
                sw.WriteLine("B1 = {0}", B1);
                sw.WriteLine("B2 = {0}", B2);
                sw.WriteLine("B = {0}", B);
                sw.WriteLine("fck = {0}", fck);
                sw.WriteLine("fy = {0}", fy);
                sw.WriteLine("m = {0}", m);
                sw.WriteLine("YS = {0}", YS);
                sw.WriteLine("D = {0}", D);
                sw.WriteLine("L = {0}", L);
                sw.WriteLine("Dwc = {0}", Dwc);
                sw.WriteLine("gamma_c = {0}", gamma_c);
                sw.WriteLine("gamma_wc = {0}", gamma_wc);
                sw.WriteLine("WL = {0}", WL);
                sw.WriteLine("v = {0}", v);
                sw.WriteLine("u = {0}", u);
                sw.WriteLine("IF = {0}", IF);
                sw.WriteLine("CF = {0}", CF);
                sw.WriteLine("Q = {0}", Q);
                sw.WriteLine("j = {0}", j);
                sw.WriteLine("sigma_st = {0}", sigma_st);
                sw.WriteLine("sigma_b = {0}", sigma_b);
                sw.WriteLine("tau = {0}", tau);
                sw.WriteLine("sigma_tf = {0}", sigma_tf);
                sw.WriteLine("K = {0}", K);
                sw.WriteLine("sigma_p = {0}", sigma_p);

                //Chiranjit [2011 07 21]
                sw.WriteLine("dw = {0}", dw);
                sw.WriteLine("tw = {0}", tw);
                sw.WriteLine("nw = {0}", nw);

                sw.WriteLine("bf = {0}", bf1);
                sw.WriteLine("tf = {0}", tf1);
                sw.WriteLine("nf = {0}", nf);

                sw.WriteLine("ang = {0}", ang);
                sw.WriteLine("ang_thk = {0}", ang_thk);

                #endregion
            }
            catch (Exception ex) { }

            finally
            {
                sw.Flush();
                sw.Close();
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
            kPath = Path.Combine(kPath, "Cable Stayed Bridge");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            kPath = Path.Combine(kPath, "Design of Deck Slab + Steel Girder");

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
                //this.Text = "DESIGN OF COMPOSITE BRIDGE : " + value;
                user_path = value;

                //file_path = GetAstraDirectoryPath(user_path);
                file_path = Path.Combine(user_path, "Design of CSB Girders");
                if (Directory.Exists(file_path) == false)
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (Directory.Exists(system_path) == false)
                    Directory.CreateDirectory(system_path);

                rep_file_name_inner = Path.Combine(file_path, "CSB_Girder_Design.TXT");
                rep_file_name_outer = Path.Combine(file_path, "CSB_Girder_Design.TXT");
                rep_file_name = Path.Combine(file_path, "CSB_Girder_Design.TXT");

                user_input_file = Path.Combine(system_path, "COMPOSITE_BRIDGE.FIL");


            }
        }

        #endregion
        public string Title
        {
            get
            {
                return "CABLE_STAYED_BRIDGE";
            }
        }

        public double Get_Table_1_Value(double d_by_t, double d_point, ref string ref_string)
        {
            return iApp.Tables.Allowable_Average_Shear_Stress(d_by_t, d_by_t, ref ref_string);


            //int indx = -1;

            //if (d_point >= 0.4 && d_point < 0.6)
            //    indx = 1;
            //else if (d_point >= 0.6 && d_point < 0.8)
            //    indx = 2;
            //else if (d_point >= 0.8 && d_point < 1.0)
            //    indx = 3;
            //else if (d_point >= 1.0 && d_point < 1.2)
            //    indx = 4;
            //else if (d_point >= 1.2 && d_point < 1.4)
            //    indx = 5;
            //else if (d_point >= 1.4 && d_point < 1.5)
            //    indx = 6;
            //else if (d_point >= 1.5)
            //    indx = 7;


            //d_by_t = Double.Parse(d_by_t.ToString("0.0"));

            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_3.txt");

            //List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            //string kStr = "";
            //MyList mList = null;

            //bool find = false;

            //double a1, returned_value;
            ////double  b1, a2, b2, returned_value;

            ////a1 = 0.0;
            ////b1 = 0.0;
            ////a2 = 0.0;
            ////b2 = 0.0;
            //returned_value = 0.0;

            //List<MyList> lst_list = new List<MyList>();

            //for (int i = 0; i < lst_content.Count; i++)
            //{
            //    kStr = MyList.RemoveAllSpaces(lst_content[i]);
            //    kStr = kStr.Replace("<=", "");
            //    if (kStr.StartsWith("--------------"))
            //    {
            //        find = !find; continue;
            //    }
            //    if (find)
            //    {
            //        mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
            //        lst_list.Add(mList);
            //    }
            //}

            //for (int i = 0; i < lst_list.Count; i++)
            //{
            //    a1 = lst_list[i].GetDouble(0);

            //    if (d_by_t < a1)
            //    {
            //        returned_value = lst_list[i].GetDouble(indx);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //returned_value = Double.Parse(returned_value.ToString("0.000"));
            //return returned_value;
        }
        public double Get_Table_2_Value(double lamda, int indx, ref string ref_string)
        {

            return iApp.Tables.Allowable_Working_Stress_Cross_Section(lamda, indx, ref ref_string);
            //lamda = Double.Parse(lamda.ToString("0.000"));

            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_4.txt");

            //List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            //string kStr = "";
            //MyList mList = null;

            //bool find = false;

            //double a1, b1, a2, b2, returned_value;

            //a1 = 0.0;
            //b1 = 0.0;
            //a2 = 0.0;
            //b2 = 0.0;
            //returned_value = 0.0;

            //List<MyList> lst_list = new List<MyList>();

            //for (int i = 0; i < lst_content.Count; i++)
            //{
            //    kStr = MyList.RemoveAllSpaces(lst_content[i]);
            //    kStr = kStr.Replace("<=", "");
            //    if (kStr.StartsWith("--------------"))
            //    {
            //        find = !find; continue;
            //    }
            //    if (find)
            //    {
            //        mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
            //        lst_list.Add(mList);
            //    }
            //}

            //for (int i = 0; i < lst_list.Count; i++)
            //{
            //    a1 = lst_list[i].GetDouble(0);
            //    if (lamda < lst_list[0].GetDouble(0))
            //    {
            //        returned_value = lst_list[0].GetDouble(indx);
            //        break;
            //    }
            //    else if (lamda > (lst_list[lst_list.Count - 1].GetDouble(0)))
            //    {
            //        returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
            //        break;
            //    }

            //    if (a1 == lamda)
            //    {
            //        returned_value = lst_list[i].GetDouble(indx);
            //        break;
            //    }
            //    else if (a1 > lamda)
            //    {
            //        a2 = a1;
            //        b2 = lst_list[i].GetDouble(indx);

            //        a1 = lst_list[i - 1].GetDouble(0);
            //        b1 = lst_list[i - 1].GetDouble(indx);

            //        returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (lamda - a1);
            //        break;
            //    }
            //}

            //lst_list.Clear();
            //lst_content.Clear();


            //returned_value = Double.Parse(returned_value.ToString("0.000"));
            //return returned_value;
        }

        public void Write_Table_1(StreamWriter sw)
        {
            //sw.WriteLine("------------------------------------------------------------");
            //sw.WriteLine("Table 1: Allowable Average Shear Stress in Stiffened Webs");
            //sw.WriteLine("         of Steel Confirming to IS: 226 (IRC:24-1967)");
            //sw.WriteLine("------------------------------------------------------------");
            //sw.WriteLine();
            //sw.WriteLine();
            //sw.WriteLine("d                Shear Stress (N/mm2) for different distances between stiffeners");
            //sw.WriteLine("t     0.4d     0.6d     0.8d    1.0d     1.2d    1.4d    1.5d");
            //sw.WriteLine("-----------------------------------------------------------------------");


            string table_file = Path.Combine(Application.StartupPath, "TABLES");
            table_file = Path.Combine(table_file, "Steel_Plate_Tab_3.txt");

            List<string> lst_content = iApp.Tables.Get_Tables_Allowable_Average_Shear_Stress();


            sw.WriteLine();
            sw.WriteLine("TABLE 1 :");
            sw.WriteLine("---------");
            sw.WriteLine();
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
            sw.WriteLine();

            lst_content.Clear();
            sw.WriteLine("-----------------------------------------------------------------------");

        }
        public void Write_Table_2(StreamWriter sw)
        {
            sw.WriteLine();
            //sw.WriteLine("------------------------------------------------------------");
            //sw.WriteLine("Table 2: Allowable Working Stress σac in N/mm2 on Effective");
            //sw.WriteLine("        Cross Section for Axial Compression (IRC: 24 - 1967)");
            //sw.WriteLine("------------------------------------------------------------");
            //sw.WriteLine();
            //sw.WriteLine();
            //sw.WriteLine("            σy = Yield Stress of Steel (N/mm2) ");
            //sw.WriteLine("λ= (L/r)  ______________________________________");
            //sw.WriteLine("           236        299        331       362");
            //sw.WriteLine("---------------------------------------------------");


            string table_file = Path.Combine(Application.StartupPath, "TABLES");
            table_file = Path.Combine(table_file, "Steel_Plate_Tab_4.txt");
            List<string> lst_content = iApp.Tables.Get_Tables_Allowable_Working_Stress_Cross_Section();
            sw.WriteLine();
            sw.WriteLine("TABLE 2 :");
            sw.WriteLine("---------");
            sw.WriteLine();
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }

            lst_content.Clear();
            sw.WriteLine("---------------------------------------------------");

        }
        public void Write_Drawing_File()
        {
            //drawing_path = Path.Combine(drawing_path, "COMPOSITE_BRIDGE_DRAWING.FIL");
            drawing_path = Path.Combine(system_path, "COMPOSITE_BRIDGE_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
            try
            {
                //_A = spacing_main_girder - (width_long_girders / 1000.0);
                //_B = (width_long_girders / 1000.0);
                //_C = spacing_cross_girder;
                //_D = (width_cross_girders / 1000.0);
                //_E = Dwc / 1000.0;
                //_F = Ds / 1000.0;
                sw.WriteLine("_A={0}", _A);
                sw.WriteLine("_B={0}", _B);
                sw.WriteLine("_C={0}", _C);
                sw.WriteLine("_D={0}", _D);
                sw.WriteLine("_E={0}", _E);
                sw.WriteLine("_F={0}", _F);
                sw.WriteLine("_G={0}", _G);


                //(v) = (E) + 2 x (G) = 3.6 + 2 x 0.08 = 3.76 m. = 3760 mm.

                double val1, val2, val3;

                val1 = 0.0;
                val2 = 0.0;
                val3 = 0.0;

                if (double.TryParse(_E.Replace("m.", "").Trim().TrimEnd().TrimStart(), out val1))
                {
                    if (double.TryParse(_G.Replace("m.", "").Trim().TrimEnd().TrimStart(), out val2))
                    {
                        val3 = val1 + 2 * val2;
                        _v = string.Format("(E) + 2 x (G) = {0:f3} + 2 x {1:f3} = {2:f3} m. = {3:f2} mm.", val1, val2, val3, (val3 * 1000.0));
                    }

                }

                //(u) = (F) + 2 x (G) = 0.850 + 2 x 0.08 = 1.0 m. = 1000 mm.
                if (double.TryParse(_F.Replace("m.", "").Trim().TrimEnd().TrimStart(), out val1))
                {
                    if (double.TryParse(_G.Replace("m.", "").Trim().TrimEnd().TrimStart(), out val2))
                    {
                        val3 = val1 + 2 * val2;
                        _u = string.Format("(F) + 2 x (G) = {0:f3} + 2 x {1:f3} = {2:f3} m. = {3:f2} mm.", val1, val2, val3, (val3 * 1000.0));
                    }

                }

                sw.WriteLine("_v={0}", _v);
                sw.WriteLine("_u={0}", _u);
                sw.WriteLine("_1={0}", _1);
                sw.WriteLine("_2={0}", _2);
                sw.WriteLine("_3={0}", _3);
                sw.WriteLine("_4={0}", _4);
                sw.WriteLine("_6={0}", _6);
                sw.WriteLine("_7={0}", _7);
                sw.WriteLine("_8={0}", _8);
                sw.WriteLine("_10={0}", _10);

            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        #endregion
    }
}


//_LGIRDER1 PRI AX 2.52 IX 0.9261 IY 0.3024 IZ 1.2285
//_LGIRDER2 PRI AX 2.00 IX 0.67 IY 0.1670 IZ 0.837
//_XGIRDER1 PRI AX 2.52 IX 0.9261 IY 0.3024 IZ 1.2285
//_XGIRDER2 PRI AX 2.00 IX 0.67 IY 0.1670 IZ 0.837
//_LPYLON1 PRI AX 6.20 IX 16.517 IY 3.86 IZ 20.377

//_RPYLON1 PRI AX  IX  IY  IZ 
//_RPYLON2 PRI AX  IX  IY  IZ
//_RPYLON2 PRI AX  IX  IY  IZ

//CABLES PRI AX 0.0177 IX 0.0025 IY 0.0025 IZ 0.00005
//_TIEBEAM PRI AX 6.00 IX 4.5 IY 2.0 IZ 6.5
