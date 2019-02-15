using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraFunctionOne.BridgeDesign;
using AstraInterface.DataStructure;
using AstraInterface.Interface;

namespace BridgeAnalysisDesign.Abutment
{
    public partial class UC_RCC_Abut : UserControl
    {


        public UC_RCC_Abut()
        {
            InitializeComponent();
            
        }
        public DataGridView DGV_Input_Open
        {
            get
            {
                return uC_Abut_Counterfort_LS1.DGV_Input;
            }
        }
        //public DataGridView DGV_Input_Pile
        //{
        //    get
        //    {
        //        return uC_Abut_.DGV_Input;
        //    }
        //}
        public TabControl TAB_MAIN
        {
            get
            {
                return tabControl1;
            }
        }

        public bool Is_Limit_State
        {
            get
            {
                return TAB_MAIN.TabPages.Contains(tab_abut_counterfort_LS);
            }
            set
            {
                if (value)
                {
                    if (TAB_MAIN.TabPages.Contains(tab_abut_counterfort_WS)) TAB_MAIN.TabPages.Remove(tab_abut_counterfort_WS);
                    if (TAB_MAIN.TabPages.Contains(tab_abut_canlilever_WS)) TAB_MAIN.TabPages.Remove(tab_abut_canlilever_WS);
                } 
                else
                {
                    //if (!TAB_MAIN.TabPages.Contains(tab_abut_counterfort_WS)) TAB_MAIN.TabPages.Add(tab_abut_counterfort_WS);
                    if (!TAB_MAIN.TabPages.Contains(tab_abut_canlilever_WS)) TAB_MAIN.TabPages.Add(tab_abut_canlilever_WS);
                }
            }
        }
        public IApplication iApp
        {
            get
            {
                return uC_Abut1.iApp;
            }
            set
            {
                if (value != null)
                {
                    uC_Abut1.iApp = value;

                    uC_Abut1.Load_Live_Loads();


                    uC_Abut_Cant1.iApp = value;
                    uC_Abut_Counterfort_LS1.iapp = value;

                    uC_Abut1.Load_Live_Loads();
                    uC_Abut_Cant1.Load_Live_Loads();
                }
            }
        }

        public double Length
        {
            get
            {
                return uC_Abut1.Length;
            }
            set
            {
                uC_Abut1.Length = value;
                uC_Abut_Counterfort_LS1.Length = value;
                uC_Abut_Cant1.Length = value.ToString("f2");
            }
        }
        public double Width
        {
            get
            {
                return uC_Abut1.Width;
            }
            set
            {
                uC_Abut1.Width = value;
            }
        }

        public string Deadload_Reaction
        {
            get
            {
                return uC_Abut_Cant1.Dead_Load_Reactions;
            }
            set
            {
                uC_Abut_Cant1.Dead_Load_Reactions = value;
            }
        }


        public double Overhang
        {
            get
            {
                return uC_Abut1.Overhang;
            }
            set
            {
                uC_Abut1.Overhang = value;
            }
        }

        public bool IsBoxType
        {
            get
            {
               return uC_Abut1.Is_Box_Type;
            }
            set
            {
                uC_Abut1.Is_Box_Type = value;
            }
        }


        public void Modified_Cells()
        {
            uC_Abut_Cant1.Modified_Cells();
            uC_Abut1.Modified_Cell();
            uC_Abut_Counterfort_LS1.Modified_Cell();

        }
        public void Load_Data()
        {
            uC_Abut_Cant1.Load_Live_Loads();
            uC_Abut1.Load_Live_Loads();
        }

        public event EventHandler Abut_Counterfort_LS1_dead_load_CheckedChanged;
        private void uC_Abut_Counterfort_LS1_dead_load_CheckedChanged(object sender, EventArgs e)
        {
            if (Abut_Counterfort_LS1_dead_load_CheckedChanged != null) Abut_Counterfort_LS1_dead_load_CheckedChanged(sender, e);

        }
        public bool Is_Individual
        {
            get
            {
                return uC_Abut_Counterfort_LS1.Is_Individual;
            }
            set
            {
                 uC_Abut_Counterfort_LS1.Is_Individual = value;
            }
        }

        private void uC_Abut_Counterfort_LS1_Load(object sender, EventArgs e)
        {

        }

        private void uC_Abut1_Load(object sender, EventArgs e)
        {

        }
    }
}
