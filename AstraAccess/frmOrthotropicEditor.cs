using AstraInterface.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AstraAccess
{
    public partial class frmOrthotropicEditor : Form
    {
        IApplication iApp;
        public frmOrthotropicEditor(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            uC_Orthotropic1.SetApplication(iApp);
        }

        private void frmOrthotropicEditor_Load(object sender, EventArgs e)
        {

        }
    }
}
