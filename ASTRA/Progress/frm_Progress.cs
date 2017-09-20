using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;

namespace AstraFunctionOne.Progress
{
    public partial class frm_Progress : Form, IProgress
    {
        double _a, _b;
        bool _close_flag;

        public frm_Progress()
        {
            Close_Flag = true;
            InitializeComponent();
        }

        public int Progress_Value
        {
            get
            {
                return progressBar1.Value;
            }
            set
            {
                if (value > 100)
                    value = value - 100;
                lbl_percentage.Text = value.ToString("") + "%";
                progressBar1.Value = value;
            }
        }

        public double Progress_Value_A
        {
            get
            {
                return _a;
            }
            set
            {
                _a = value;
            }
        }

        public double Progress_Value_B
        {
            get
            {
                return _b;
            }
            set
            {
                _b = value;
            }
        }

        public bool Close_Flag
        {
            get
            {
                return _close_flag;
            }
            set
            {
                _close_flag = value;
            }
        }
        private void frm_Progress_Load(object sender, EventArgs e)
        {
            Close_Flag = false;
        }


    }
}
