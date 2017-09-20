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
    public partial class frmEigenValueAnalysis : Form
    {
        IApplication iApp;
        public frmEigenValueAnalysis(IApplication app)
        {
            InitializeComponent();
            iApp = app;
        }
    }
}
