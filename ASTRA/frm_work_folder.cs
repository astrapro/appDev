using System;
using System.IO;
using System.Windows.Forms;
using AstraInterface.Interface;

namespace AstraFunctionOne
{
    public partial class frm_work_folder : Form
    {
        IApplication iApp;
        public frm_work_folder(IApplication app)
        {
            InitializeComponent();
            this.iApp = app;


        }
        public string last_work_fol { get { return Path.Combine(Application.StartupPath, "lastwf.dll"); } }
        public void SetWorkingFolder()
        {
            string tst = "";

            //Tables
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                //Chiranjit [2012 09 26]
                if (!Directory.Exists(txt_path.Text))
                {
                    if (File.Exists(last_work_fol))
                        fbd.SelectedPath = File.ReadAllText(last_work_fol);
                }
                else
                    fbd.SelectedPath = txt_path.Text;

                fbd.Description = "Select your Working Folder";
                if (fbd.ShowDialog() != DialogResult.Cancel)
                {
                    txt_path.Text = fbd.SelectedPath;
                    File.WriteAllText(last_work_fol, fbd.SelectedPath);
                }
            }
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            SetWorkingFolder();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            iApp.LastDesignWorkingFolder = txt_path.Text;
            File.WriteAllText(last_work_fol, txt_path.Text);
            DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frm_work_folder_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox1, "Click Here to open Working Folder...");

            //if (Directory.Exists(iApp.LastDesignWorkingFolder))
            txt_path.Text = iApp.LastDesignWorkingFolder;

            if (!Directory.Exists(txt_path.Text))
            {
                if (File.Exists(last_work_fol))
                    txt_path.Text = File.ReadAllText(last_work_fol);
            }
            if (!Directory.Exists(txt_path.Text)) txt_path.Text = "";
        }

        private void txt_path_TextChanged(object sender, EventArgs e)
        {
            
            btn_OK.Enabled = Directory.Exists(txt_path.Text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txt_path.Text))
                System.Diagnostics.Process.Start(txt_path.Text);

            //MessageBox.Show(frmDemoVersion.Get_UniqueId());
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Create_Directory();
        }

        private void Create_Directory()
        {

            try
            {
                if (!Directory.Exists(txt_path.Text))
                {
                    Directory.CreateDirectory(txt_path.Text);
                }
                btn_OK.Enabled = Directory.Exists(txt_path.Text);
            }
            catch (Exception ex) { }
        }

        private void txt_path_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyData == Keys.C)
            //{
            //     if(e.Modifiers == Keys.Alt)
            //    Create_Directory();
            //}
        }

        private void txt_path_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if (e.KeyChar == 'C')
            //    Create_Directory();

        }

        private void txt_path_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.C)
            {
                if (e.Modifiers == Keys.Alt)
                    Create_Directory();
            }
        }
    }
}
