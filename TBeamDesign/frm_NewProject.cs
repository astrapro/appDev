using System;
using System.IO;
using System.Windows.Forms;

namespace BridgeAnalysisDesign
{
    public partial class frm_NewProject : Form
    {
        public string Project_Name
        {
            get
            {
                return txt_working_folder.Text;
            }
            set
            {
                txt_working_folder.Text = value;
            }
        }
        public string Working_Folder { get; set; }
        public frm_NewProject(string Working_Folder)
        {
            InitializeComponent();
            this.Working_Folder = Working_Folder;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_OK.Name)
            {

                string fld = Path.Combine(Working_Folder, Project_Name);

                if (Directory.Exists(fld))
                {
                    if (MessageBox.Show("'" + Project_Name + "' is already exist. Do you want to overwrite ?", "ASTRA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) return;
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else if (btn.Name == btn_Cancel.Name)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
            this.Close();
        }
    }
}
