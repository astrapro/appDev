using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Management;

namespace AstraFunctionOne
{
    public partial class frmDemoVersion : Form
    {

        public frmDemoVersion()
        {
            InitializeComponent();
        }
        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string Autho_Code
        {
            get
            {
                return txt_Auth_code.Text;
            }
        }
        public static string Get_UniqueId()
        {
            string cpu_info = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");

            ManagementObjectCollection moc = mc.GetInstances();
            List<string> list = new List<string>();
            foreach (var item in moc)
            {
                cpu_info = item.Properties["processorId"].Value.ToString();
                foreach (var m in item.Properties)
                {
                   
                    list.Add(m.Name + " : " + m.Value);
                }
            }
            string fpth = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Templates), cpu_info);
            if (cpu_info != null)
                File.WriteAllLines(fpth, list.ToArray());
            return cpu_info;
        }
    }
}
