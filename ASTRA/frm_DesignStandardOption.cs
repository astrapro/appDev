using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using   AstraInterface.DataStructure;
namespace AstraFunctionOne
{
    public partial class frm_DesignStandardOption : Form
    {

        public frm_DesignStandardOption()
        {
            InitializeComponent();
            IsAASTHO = !(TimeZone.CurrentTimeZone.StandardName.ToUpper().StartsWith("INDIA"));
            //IsAASTHO = false;
        }
        public eDesignStandard DesignStandard
        {
            get
            {
                if (rbtn_BS.Checked)
                    return eDesignStandard.BritishStandard;
                else if (rbtn_IS.Checked)
                    return eDesignStandard.IndianStandard;
                else if (rbtn_LRFD.Checked)
                    return eDesignStandard.LRFDStandard;
                return eDesignStandard.IndianStandard;
            }
            set
            {
                if (value == eDesignStandard.IndianStandard)
                {
                    rbtn_LRFD.Checked = false;
                    rbtn_BS.Checked = false;
                    rbtn_IS.Checked = true;
                }
                else if (value == eDesignStandard.LRFDStandard)
                {
                    rbtn_BS.Checked = false;
                    rbtn_IS.Checked = false;
                    rbtn_LRFD.Checked = true;
                }
                else 
                {
                    rbtn_BS.Checked = true;
                    rbtn_IS.Checked = false;
                    rbtn_LRFD.Checked = false;
                }
            }
        }
        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        //Chiranjit [2012 02 08]
        public bool IsAASTHO { get; set; }
        private void frm_DesignStandardOption_Load(object sender, EventArgs e)
        {
            if (rbtn_LRFD.Visible == false)
            {
                if (IsAASTHO)
                {
                    rbtn_BS.Location = new Point(16, 20);
                    rbtn_IS.Location = new Point(16, 46);
                    rbtn_LRFD.Location = new Point(16, 72);
                    //rbtn_IS.Checked = false;
                    //rbtn_BS.Checked = true;
                }
                else
                {
                    //rbtn_BS.Location = new Point(21, 53);
                    //rbtn_IS.Location = new Point(21, 27);
                    //rbtn_LRFD.Location = new Point(21, 61);
                    rbtn_IS.Location = new Point(16, 20);
                    rbtn_BS.Location = new Point(16, 46);
                    rbtn_LRFD.Location = new Point(16, 72);
                }
            }

            rbtn_IS.Checked = (DesignStandard == eDesignStandard.IndianStandard);
            rbtn_BS.Checked = (DesignStandard == eDesignStandard.BritishStandard);
            rbtn_LRFD.Checked = (DesignStandard == eDesignStandard.LRFDStandard);
        }

    }
}
