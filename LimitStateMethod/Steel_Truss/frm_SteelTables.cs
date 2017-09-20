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

namespace LimitStateMethod.Steel_Truss
{
    public partial class frm_SteelTables : Form
    {
        IApplication iApp;
        public frm_SteelTables(IApplication app)
        {
            InitializeComponent();
            iApp = app;
        }

        private void frm_SteelTables_Load(object sender, EventArgs e)
        {
            if (iApp.DesignStandard == eDesignStandard.BritishStandard) rbtn_bs.Checked = true;
            if (iApp.DesignStandard == eDesignStandard.IndianStandard) rbtn_irc.Checked = true;
            LoadSecions();
        }

        private void LoadSecions()
        {

            uC_Angles1.iApp = iApp;
            uC_Beams1.iApp = iApp;
            uC_Channels1.iApp = iApp;



            uC_Angles1.Pro_Grid = propertyGrid1;
            uC_Beams1.Pro_Grid = pg_beam;
            uC_Channels1.Pro_Grid = pg_channels;
        }

        private void rbtn_irc_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_irc.Checked)
                iApp.DesignStandard = eDesignStandard.IndianStandard;
            else if (rbtn_bs.Checked)
                iApp.DesignStandard = eDesignStandard.BritishStandard;
            else
                iApp.DesignStandard = eDesignStandard.LRFDStandard;
            LoadSecions();
        }
    }
}
