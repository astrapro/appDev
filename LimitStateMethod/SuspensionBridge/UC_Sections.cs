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
    public partial class UC_Sections : UserControl
    {
        // An event that clients can use to be notified whenever the
        // elements of the list change.
        public event EventHandler Changed;

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }
        public PropertyGrid Pro_Grid { get; set; }

        public UC_Sections()
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
                if (app != null) Load_Sections();
            }
        }

        public string Section_Name
        {
            get
            {
                return cmb_name.Text;
            }
            set
            {
                cmb_name.SelectedItem = value;
            }
        }


        public string Section_Size
        {
            get
            {
                return cmb_size.Text;
            }
            set
            {
                cmb_size.SelectedItem = value;
            }
        }

        public double D { get; set; }
        public double Zxx { get; set; }

        public double tw { get; set; }

        public double Area { get; set; }


        public double Ixx { get; set; }
        public double Iyy { get; set; }


        public double Izz 
        { get
            {
                return Ixx + Iyy;
            }
        }

        #endregion Properties

        public void Convert_IS_to_BS(string name, string size)
        {
            SectionData sd = new SectionData();
            sd.SectionName = name;
            sd.SectionCode = size;


            iApp.Tables.Steel_Convert.Convert_IS_to_BS(ref sd);

            Section_Name = sd.SectionName;
            Section_Size = sd.SectionCode;
        }
        private void cmb_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.Name == cmb_name.Name)
            {
                Load_Sections_code(cmb.Text, cmb_size);
            }
        }
        private void cmb_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.Name == cmb_size.Name)
            {
                Load_Channel_Sections_details(cmb_name.Text, cmb.Text, 1);
            }
            OnChanged(e);
        }


        TableRolledSteelChannels tbl_Channels
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return iApp.Tables.BS_SteelChannels;
                return iApp.Tables.IS_SteelChannels;
            }
        }

        TableRolledSteelBeams tbl_Beams
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return iApp.Tables.BS_SteelBeams;
                return iApp.Tables.IS_SteelBeams;
            }
        }
        TableRolledSteelAngles tbl_Angles
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return iApp.Tables.BS_SteelAngles;
                return iApp.Tables.IS_SteelAngles;
            }
        }
        public void Load_Sections()
        {
            //ComboBox cmb_name = cmb_name;
            cmb_name.Items.Clear();
            if (tbl_Angles != null)
            {
                foreach (var item in tbl_Angles.List_Table)
                {
                    if (!cmb_name.Items.Contains(item.SectionName))
                        cmb_name.Items.Add(item.SectionName);
                }
            }
            if (tbl_Channels != null)
            {
                foreach (var item in tbl_Channels.List_Table)
                {
                    if (!cmb_name.Items.Contains(item.SectionName))
                        cmb_name.Items.Add(item.SectionName);
                }
            }
            if (tbl_Beams != null)
            {
                foreach (var item in tbl_Beams.List_Table)
                {
                    if (!cmb_name.Items.Contains(item.SectionName))
                        cmb_name.Items.Add(item.SectionName);
                }
            }


            if (cmb_name.Items.Count > 0) cmb_name.SelectedIndex = 0;
        }
        private void Load_Sections_code(string name, ComboBox cmb_code)
        {

            cmb_code.Items.Clear();
            bool flg = true;

            foreach (var item in tbl_Angles.List_Table)
            {
                //if ((item.SectionName == name))
                //{
                //    cmb_code.Items.Add(item.SectionCode);
                //    flg = false;
                //}


                if ((item.SectionName == name))
                {
                    cmb_code.Items.Add(item.SectionSize + "X" + item.Thickness);
                    flg = false;
                }
            }

            if (flg)
            {
                foreach (var item in tbl_Channels.List_Table)
                {
                    if ((item.SectionName == name))
                    {
                        cmb_code.Items.Add(item.SectionCode);
                        flg = false;
                    }
                }
            }
            if (flg)
            {
                foreach (var item in tbl_Beams.List_Table)
                {
                    if ((item.SectionName == name))
                    {
                        cmb_code.Items.Add(item.SectionCode);
                    }
                }
            }


            if (cmb_code.Items.Count > 0) cmb_code.SelectedIndex = 0;
        }
        private void Load_Channel_Sections_details(string name, string code, int opt)
        {

            MyList ml = new MyList(code, 'X');


            double ts = 0.0;

            if (name.EndsWith("A"))
            {
                ts = ml.GetDouble(2);
                code = ml[0] + "X" + ml[1];
                foreach (var item in tbl_Angles.List_Table)
                {
                    if ((item.SectionName == name) && item.SectionSize == code && item.Thickness == ts)
                    {
                        if (Pro_Grid != null)
                            Pro_Grid.SelectedObject = item;

                        Area = item.Area;
                        tw = item.Thickness;
                        Ixx = item.Ixx;
                        Iyy = item.Iyy;

                        break;
                    }
                }
            }



            foreach (var item in tbl_Channels.List_Table)
            {
                if ((item.SectionName == name) && item.SectionCode == code)
                {
                    if (Pro_Grid != null) Pro_Grid.SelectedObject = item;

                    D = item.Depth;
                    tw = item.WebThickness;
                    Ixx = item.Ixx;
                    Area = item.Area;
                    Iyy = item.Iyy;

                    break;
                }
            }
            foreach (var item in tbl_Beams.List_Table)
            {
                if ((item.SectionName == name) && item.SectionCode == code)
                {
                    if (Pro_Grid != null) Pro_Grid.SelectedObject = item;

                    D = item.Depth;
                    tw = item.WebThickness;
                    Ixx = item.Ixx;
                    Area = item.Area;
                    Iyy = item.Iyy;

                    break;
                }
            }
        }





    }
}
