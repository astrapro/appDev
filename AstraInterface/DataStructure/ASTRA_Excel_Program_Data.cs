using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace AstraInterface.DataStructure
{
    public class Excel_User_Input_Data
    {
        public string Input_Text { get; set; }
        public string Input_Value { get; set; }
        public string Input_Unit { get; set; }
        public string Excel_Cell_Reference { get; set; }

        public Excel_User_Input_Data()
        {
            Input_Text = "";
            Input_Value = "";
            Input_Unit = "";
            Excel_Cell_Reference = "";
        }
        public override string ToString()
        {
            return string.Format("{0} = {1} {2}", Input_Text, Input_Value, Input_Unit);
        }
    }
    public class Excel_User_Inputs : List<Excel_User_Input_Data>
    {
        public Excel_User_Inputs()
            : base()
        {

        }
        public void Read_From_Grid(DataGridView dgv)
        {
            this.Clear();

            Excel_User_Input_Data di = new Excel_User_Input_Data();
            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {
                    di = new Excel_User_Input_Data();

                    di.Input_Text = dgv[0, i].Value.ToString();
                    di.Input_Value = dgv[1, i].Value.ToString().ToUpper().Replace("M", "").Replace("FE", "");
                    di.Input_Unit = dgv[2, i].Value.ToString();
                    if (di.Input_Value != "")
                        this.Add(di);
                }
                catch (Exception ex) { }
            }
        }
        public void Read_From_Grid(DataGridView dgv, int value_index, int UnitIndex)
        {
            this.Clear();

            Excel_User_Input_Data di = new Excel_User_Input_Data();
            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {
                    di = new Excel_User_Input_Data();

                    di.Input_Text = dgv[0, i].Value.ToString();
                    di.Input_Value = dgv[value_index, i].Value.ToString().ToUpper().Replace("M", "").Replace("FE", "");
                    di.Input_Unit = dgv[UnitIndex, i].Value.ToString();
                    if (di.Input_Value != "")
                        this.Add(di);
                }
                catch (Exception ex) { }
            }
        }

        public string Type_of_Cable { get; set; }
        public string Strand_Area { get; set; }
        public string UTS { get; set; }
        public string Permissible_Force { get; set; }
        public string Es { get; set; }
        public string Permissible_Slip { get; set; }
        public string Jacking_Distance { get; set; }

//Area of one strand			=	98.7
//UTS			=	355.8
//Max. Permissible Force 			=	266.9
//Es			=	195000.0
//Permissible Slip			=	6.0
//Distance of Jacking end from Brg.			=	0.4


    }
}
