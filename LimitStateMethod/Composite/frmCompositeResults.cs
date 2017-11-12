using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LimitStateMethod.Composite
{

    public partial class frmCompositeResults : Form
    {
        public enum eCompositeResults
        {
            Read_Analysis_Results = 0,
            Open_Analysis_Reports = 1,
            Open_Design_Forces = 2,
        }
        public frmCompositeResults()
        {
            InitializeComponent();
            ResultOption = eCompositeResults.Open_Analysis_Reports;
        }


        public eCompositeResults ResultOption;
        private void btn_OK_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (rbtn_read.Checked) ResultOption = eCompositeResults.Read_Analysis_Results;
            else if (rbtn_openAnalysis.Checked) ResultOption = eCompositeResults.Open_Analysis_Reports;
            else if (rbtn_designForces.Checked) ResultOption = eCompositeResults.Open_Design_Forces;


            if (btn == btn_OK)
                DialogResult = System.Windows.Forms.DialogResult.OK;
            else if (btn == btn_Cancel)
                DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.Close();
        }
    }
}
