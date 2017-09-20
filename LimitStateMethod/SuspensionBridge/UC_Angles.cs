using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.DataStructure;
using AstraInterface.Interface;

using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace LimitStateMethod.SuspensionBridge
{
    public partial class UC_Angles : UserControl
    {
        public PropertyGrid Pro_Grid { get; set; }
        public UC_Angles()
        {
            InitializeComponent();
        }

        #region Properties

        IApplication app;
        public IApplication iApp
        {
            get
            {
                return app;
            }
            set
            {
                app = value;
                if (app != null) Load_Channel_Sections();
            }
        }
        public string Title
        {
            get
            {
                return grb_title.Text;
            }
            set
            {
                grb_title.Text = value;
            }
        }

        public string Section_Name
        {
            get
            {
                return cmb_sec_name_4.Text;
            }
            set
            {
                cmb_sec_name_4.SelectedItem = value;
            }
        }

        public string Section_Size
        {
            get
            {
                return cmb_sec_size_4.Text;
            }
            set
            {
                cmb_sec_size_4.SelectedItem = value;
            }
        }

        public string h
        {
            get
            {
                return txt_sec_h.Text;
            }
            set
            {
                txt_sec_h.Text = value;
            }
        }

        public string b
        {
            get
            {
                return txt_sec_b.Text;
            }
            set
            {
                txt_sec_b.Text = value;
            }
        }

        public string Cyy
        {
            get
            {
                return txt_sec_Cyy.Text;
            }
            set
            {
                txt_sec_Cyy.Text = value;
            }
        }

        public string tw
        {
            get
            {
                return txt_sec_tw.Text;
            }
            set
            {
                txt_sec_tw.Text = value;
            }
        }

        public string tf
        {
            get
            {
                return txt_sec_tf.Text;
            }
            set
            {
                txt_sec_tf.Text = value;
            }
        }


        public string Area
        {
            get
            {
                return txt_sec_Area.Text;
            }
            set
            {
                txt_sec_Area.Text = value;
            }
        }


        public string Ixx
        {
            get
            {
                return txt_sec_Ixx.Text;
            }
            set
            {
                txt_sec_Ixx.Text = value;
            }
        }

        public string Iyy
        {
            get
            {
                return txt_sec_Iyy.Text;
            }
            set
            {
                txt_sec_Iyy.Text = value;
            }
        }

        #endregion Properties


        private void cmb_sec_name_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            Load_Angle_Sections_code(cmb.Text, cmb_sec_size_4);
        }
        TableRolledSteelAngles tbl_rolledSteelAngles
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return iApp.Tables.BS_SteelAngles;
                else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                    return iApp.Tables.AISC_SteelAngles;


                return iApp.Tables.IS_SteelAngles;
            }
        }

        public void Load_Channel_Sections()
        {
            ComboBox cmb_name = cmb_sec_name_4;
            cmb_name.Items.Clear();
            if (tbl_rolledSteelAngles == null) return;
            foreach (var item in tbl_rolledSteelAngles.List_Table)
            {
                if (!cmb_name.Items.Contains(item.SectionName))
                    cmb_name.Items.Add(item.SectionName);
            }
            if (cmb_name.Items.Count > 0) cmb_name.SelectedIndex = 0;
        }
        private void cmb_sec_size_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            Load_Angle_Sections_details(cmb_sec_name_4.Text, cmb.Text);
        }

        private void Load_Angle_Sections_code(string name, ComboBox cmb_code)
        {
            cmb_code.Items.Clear();

            foreach (var item in tbl_rolledSteelAngles.List_Table)
            {
                if ((item.SectionName == name))
                {
                    cmb_code.Items.Add(item.SectionSize + "X" + item.Thickness);
                }
            }
            if (cmb_code.Items.Count > 0) cmb_code.SelectedIndex = 0;
        }

        private void Load_Angle_Sections_details(string name, string code)
        {
            List<string> sec_names = new List<string>();

            double Zxx = 0.0, Ixx = 0.0, Iyy = 0.0, D = 0.0, tw = 0.0;
            double h = 0.0;
            double b = 0.0;
            double Cyy = 0.0;
            double Area = 0.0;
            double tf = 0.0;

            MyList ml = new MyList(code, 'X');


            double ts = 0.0;

            if (name.EndsWith("A") || name.StartsWith("L"))
            {
                ts = ml.GetDouble(2);
                code = ml[0] + "X" + ml[1];
            }
            foreach (var item in tbl_rolledSteelAngles.List_Table)
            {
                if ((item.SectionName == name) && item.SectionSize == code && item.Thickness == ts)
                {
                    if(Pro_Grid != null)
                        Pro_Grid.SelectedObject = item;

                    h = item.Length_1;
                    b = item.Length_2;
                    Cyy = item.Cyy;
                    Area = item.Area;
                    tf = item.Thickness;
                    tw = item.Thickness;
                    Ixx = item.Ixx;
                    Iyy = item.Iyy;

                    break;
                }
            }


            
            TextBox txt_h = txt_sec_h;
            TextBox txt_b = txt_sec_b;
            TextBox txt_Cyy = txt_sec_Cyy;
            TextBox txt_Area = txt_sec_Area;
            TextBox txt_tf = txt_sec_tf;
            TextBox txt_tw = txt_sec_tw;
            TextBox txt_Ixx = txt_sec_Ixx;
            TextBox txt_Iyy = txt_sec_Iyy;


            ////txt_h.Text = h.ToString("f3");
            ////txt_b.Text = b.ToString("f3");
            ////txt_Cyy.Text = Cyy.ToString("f3");
            ////txt_Area.Text = Area.ToString("f3");
            ////txt_tf.Text = tf.ToString("f3");
            ////txt_tw.Text = tw.ToString("f3");
            ////txt_Ixx.Text = Ixx.ToString("f3");

            txt_h.Text = (h / 10).ToString();
            txt_b.Text = (b / 10).ToString();
            txt_Cyy.Text = Cyy.ToString();
            txt_Area.Text = Area.ToString();
            txt_tf.Text = (tf / 10).ToString();
            txt_tw.Text = (tw / 10).ToString();
            txt_Ixx.Text = Ixx.ToString();
            txt_Iyy.Text = Iyy.ToString();
        }
        public void Convert_IS_to_BS(string name, string size)
        {
            SectionData sd = new SectionData();
            sd.SectionName = name;
            sd.SectionCode = size;


            iApp.Tables.Steel_Convert.Convert_IS_to_BS(ref sd);

            Section_Name = sd.SectionName;
            Section_Size = sd.SectionCode + "X" + sd.AngleThickness;
        }


    }
}
