using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using AstraInterface.Interface;
using AstraInterface.TrussBridge;
using AstraInterface.DataStructure;
//using AstraFunctionOne.BridgeDesign.SteelTrussTables;

namespace AstraFunctionOne.BridgeDesign.SteelTruss
{
    public partial class frmCompleteDesign : Form
    {
        public frmCompleteDesign()
        {
            InitializeComponent();
            //Total
        }
        public void MovingLoad_Calculation()
        {
            //Live
        }
        public string[] Get_MovingLoad_Data(List<LoadData> lst_load_data)
        {
            List<string> load_lst = new List<string>();
            //load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //load_lst.Add("TYPE 1 CLA 1.179");
            //load_lst.Add("TYPE 2 CLB 1.188");
            //load_lst.Add("TYPE 3 A70RT 1.10");
            //load_lst.Add("TYPE 4 CLAR 1.179");
            //load_lst.Add("TYPE 5 A70RR 1.188");
            //load_lst.Add("TYPE 6 IRC24RTRACK 1.188");
            //load_lst.Add("TYPE 7 AASHTO_LFRD_HL93_H20_TRUCK 1.25");
            //load_lst.Add("TYPE 8 AASHTO_LFRD_HL93_HS20_TRUCK 1.25");
            //load_lst.Add("TYPE 9 AASHTO_LFRD_HTL57_TRUCK 1.25");
            //load_lst.Add("TYPE 10 BG_RAIL_1 1.9");
            //load_lst.Add("TYPE 11 BG_RAIL_2 1.90");
            //load_lst.Add("TYPE 12 MG_RAIL_1 1.90");
            //load_lst.Add("TYPE 13 MG_RAIL_2 1.90");
            //iApp.LiveLoads.Impact_Factor(ref load_lst, iApp.DesignStandard);

            double lat_clrns = 0.5;
            int total_lanes = 1;
            double xincr = 0.5;
            double x, y, z;

            double vehicle_width = 0.0;
            double calc_width = 0;
            MyList mlist = new MyList(MyList.RemoveAllSpaces(cmb_custom_LL_type.Text.ToUpper()), ':');
            string load_type = mlist.StringList[0].Trim().TrimEnd();

            foreach (var item in lst_load_data)
            {
                if (item.TypeNo == load_type)
                {
                    vehicle_width = item.LoadWidth;
                    break;
                }
            }


            if (rbtn_custom_LL.Checked == false)
            {


                load_lst.Add("LOAD GENERATION " + txt_custom_LL_load_gen.Text);
                lat_clrns = MyList.StringToDouble(txt_custom_LL_lat_clrns.Text, 0.5);
                total_lanes = MyList.StringToInt(cmb_custom_LL_lanes.Text, 1);
                xincr = MyList.StringToDouble(txt_custom_LL_Xcrmt.Text, 0.5);
                z = lat_clrns;

                for (int i = 0; i < total_lanes; i++)
                {
                    x = -Truss_Analysis.Analysis.Length;
                    y = 0;
                    z = (i+1) * lat_clrns + i * vehicle_width;

                    //TYPE 6  -60.000 0 1.000 XINC 0.5
                    //load_lst.Add(string.Format("TYPE 6  -60.000 0 1.000 XINC 0.5"));
                    load_lst.Add(string.Format("{0}  {1} 0 {2} XINC {3}", load_type, x, z, xincr));
                }


                calc_width = lat_clrns * (total_lanes + 1) + vehicle_width * total_lanes;
            }
            else
            {


                load_lst.Add("DEFINE MOVING LOAD FILE LL.TXT");

                LoadReadFromGrid();

                foreach (LoadData ld in LoadList)
                {
                    //sw.WriteLine("TYPE 6 -60.0 0 1.00 XINC 0.5");
                    if (!load_lst.Contains(ld.ToString())) load_lst.Add(ld.ToString());
                }

                load_lst.Add("LOAD GENERATION " + txt_LL_load_gen.Text);
                //load_lst.Add("TYPE 7  -69.500 0 1.000 XINC 0.5");

                foreach (LoadData ld in LoadList)
                {
                    //sw.WriteLine("TYPE 6 -60.0 0 1.00 XINC 0.5");
                    load_lst.Add(string.Format("{0} {1:f3} {2} {3:f3} XINC {4}", ld.TypeNo, ld.X, ld.Y, ld.Z, ld.XINC));
                }

            }
            if (calc_width > Truss_Analysis.Analysis.Width)
            {
                string str = "In case Total Calculated Width " + calc_width + " > Width of Bridge " + Truss_Analysis.Analysis.Width;

                str = str +  "\nUser requested No. of Lanes of Vehicles can not be accomodated within the width of bridge.";
                MessageBox.Show(str, "ASTRA");
                return null;
            }

            return load_lst.ToArray();
        }
    }
    
    
}
