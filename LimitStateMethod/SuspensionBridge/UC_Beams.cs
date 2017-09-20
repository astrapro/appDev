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
    public partial class UC_Beams : UserControl
    {
        public PropertyGrid Pro_Grid { get; set; }

        public UC_Beams()
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
                if (app != null) Load_Beam_Sections();
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
                return cmb_sec_name_1.Text;
            }
            set
            {
                cmb_sec_name_1.SelectedItem = value;
            }
        }

        public string Section_Size
        {
            get
            {
                return cmb_sec_size_1.Text;
            }
            set
            {
                cmb_sec_size_1.SelectedItem = value;
            }
        }

        public string D
        {
            get
            {
                return txt_sec_D_1.Text;
            }
            set
            {
                txt_sec_D_1.Text = value;
            }
        }
        public string Zxx
        {
            get
            {
                return txt_sec_Zxx_1.Text;
            }
            set
            {
                txt_sec_Zxx_1.Text = value;
            }
        }

        public string tw
        {
            get
            {
                return txt_sec_tw_1.Text;
            }
            set
            {
                txt_sec_tw_1.Text = value;
            }
        }

        public string Area
        {
            get
            {
                return txt_sec_Area_1.Text;
            }
            set
            {
                txt_sec_Area_1.Text = value;
            }
        }


        public string Ixx
        {
            get
            {
                return txt_sec_Ixx_1.Text;
            }
            set
            {
                txt_sec_Ixx_1.Text = value;
            }
        }


        #endregion Properties


        private void cmb_sec_name_1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.Name == cmb_sec_name_1.Name)
            {
                Load_Beam_Sections_code(cmb.Text, cmb_sec_size_1);
            }
        }
        TableRolledSteelBeams tbl_rolledSteelBeams
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return iApp.Tables.BS_SteelBeams;
                else if (iApp.DesignStandard == eDesignStandard.LRFDStandard)
                    return iApp.Tables.AISC_SteelBeams;

                return iApp.Tables.IS_SteelBeams;
            }
        }

        private void Load_Beam_Sections()
        {
            ComboBox cmb_name = cmb_sec_name_1;
            cmb_name.Items.Clear();
            if (tbl_rolledSteelBeams == null) return;
            foreach (var item in tbl_rolledSteelBeams.List_Table)
            {
                if (!cmb_name.Items.Contains(item.SectionName))
                    cmb_name.Items.Add(item.SectionName);
            }
            if (cmb_name.Items.Count > 0) cmb_name.SelectedIndex = 0;
        }

        private void cmb_sec_size_1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.Name == cmb_sec_size_1.Name)
            {
                Load_Beam_Sections_details(cmb_sec_name_1.Text, cmb.Text, 1);
            }
        }
        private void Load_Beam_Sections_code(string name, ComboBox cmb_code)
        {
            cmb_code.Items.Clear();

            foreach (var item in tbl_rolledSteelBeams.List_Table)
            {
                if ((item.SectionName == name))
                {
                    cmb_code.Items.Add(item.SectionCode);
                }
            }
            if (cmb_code.Items.Count > 0) cmb_code.SelectedIndex = 0;

        }

        private void Load_Beam_Sections_details(string name, string code, int opt)
        {

            double D = 0.0, Zxx = 0.0, tw = 0.0, Area = 0.0, Ixx = 0.0;
            foreach (var item in tbl_rolledSteelBeams.List_Table)
            {
                if ((item.SectionName == name) && item.SectionCode == code)
                {
                    if (Pro_Grid != null) Pro_Grid.SelectedObject = item;
                    D = item.Depth;
                    tw = item.WebThickness;
                    Ixx = item.Ixx;
                    Area = item.Area;

                    break;
                }
            }
            //int opt = 1;

            D = D / 10.0;
            Zxx = Ixx / (D / 2.0);

            txt_sec_D_1.Text = D.ToString();
            txt_sec_Zxx_1.Text = Zxx.ToString("f3");
            txt_sec_tw_1.Text = tw.ToString();
            txt_sec_Area_1.Text = Area.ToString();
            txt_sec_Ixx_1.Text = Ixx.ToString();
        }
        public void Convert_IS_to_BS(string name, string size)
        {
            SectionData sd = new SectionData();
            sd.SectionName = name;
            sd.SectionCode = size;


            iApp.Tables.Steel_Convert.Convert_IS_to_BS(ref sd);

            Section_Name = sd.SectionName;
            Section_Size = sd.SectionCode;
        }

    }
}
