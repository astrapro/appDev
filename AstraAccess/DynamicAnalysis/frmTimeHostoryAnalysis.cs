using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.Interface;

namespace AstraAccess.DynamicAnalysis
{
    public partial class frmTimeHostoryAnalysis : Form
    {
        IApplication iApp;
        public frmTimeHostoryAnalysis(IApplication app)
        {
            InitializeComponent();
            iApp = app;
        }
    }
}
