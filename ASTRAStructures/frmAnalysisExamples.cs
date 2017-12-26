using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ASTRAStructures
{
    public partial class frmAnalysisExamples : Form
    {
        int Example_No { get; set; }
        public frmAnalysisExamples(int example_no)
        {
            InitializeComponent();
            Example_No = example_no;
        }

        private void frmAnalysisExamples_Load(object sender, EventArgs e)
        {
            if (Example_No == 2)
            {
                lsb_examples.Items.Add("Supports with Conditions");
                lsb_examples.Items.Add("Supports with no condition");
                lsb_examples.Items.Add("Supports with Spring & conditions");
                lsb_examples.Items.Add("Supports with Spring no condition");
            }
            else if (Example_No == 12)
            {
                lsb_examples.Items.Add("With Nodal Constraint  DOF");
                lsb_examples.Items.Add("Without Nodal Constraint DOF");
            }
            lsb_examples.SelectedIndex = 0;



        }
        public int Selected_Index
        {
            get
            {
                return lsb_examples.SelectedIndex;
            }
        }
        public string Selected_Text
        {
            get
            {
                return lsb_examples.SelectedItem.ToString();
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
    }
}
