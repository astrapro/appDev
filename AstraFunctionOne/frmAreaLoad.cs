using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;
namespace AstraFunctionOne
{
    internal partial class frmAreaLoad : Form
    {
        IApplication iApp;
        public frmAreaLoad(IApplication iApp)
        {
            InitializeComponent();
            this.iApp = iApp;
        }

        private void frmAreaLoad_Load(object sender, EventArgs e)
        {

        }
    }
}