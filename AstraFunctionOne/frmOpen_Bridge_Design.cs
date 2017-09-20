using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using AstraInterface.Interface;
using AstraInterface.DataStructure;


namespace AstraFunctionOne
{
    public partial class frmOpen_Bridge_Design : Form
    {
        IApplication iApp = null;

        public string Title
        {
            get
            {
                return label2.Text;
            }
            set
            {
                label2.Text = value;
            }
        }
        public eDesignOption Design_Option
        {
            get
            {
                if(IsNew_Analysis)
                    return eDesignOption.New_Design;
                return eDesignOption.Open_Design;
            }
        }
        public bool IsNew_Analysis
        {
            get
            {
                return rbtn_new_analysis.Checked;
            }
            set
            {
                rbtn_new_analysis.Checked = value;
            }
        }
        public bool IsOpen_Analysis
        {
            get
            {
                return rbtn_open_analysis.Checked;
            }
            set
            {
                rbtn_open_analysis.Checked = value;
            }
        }

       public string user_path
        {
            get
            {

                if (iApp.LastDesignWorkingFolder == "" || Title.Contains("P DELTA"))
                {
                    if (File.Exists(iApp.WorkingFile))
                        return Path.Combine(Path.GetDirectoryName(iApp.WorkingFile), Title);
                }
                return Path.Combine(iApp.LastDesignWorkingFolder, Title);
            }
        }
       public string file_path
        {
            get
            {

               
                return Path.Combine(user_path, "ASTRA_Data_Input.TXT");

            }
        }

        public frmOpen_Bridge_Design(IApplication app, string title)
        {
            InitializeComponent();
            iApp = app;
            Title = title;

        }

        private void frmOpen_Bridge_Design_Load(object sender, EventArgs e)
        {
            if (Title.Contains("P DELTA"))
            {
                groupBox12.Text = "Select Stage Analysis Option";
                rbtn_new_analysis.Text = "New Stage Analysis";
                rbtn_open_analysis.Text = "Open Stage Analysis";
            }
            Design_Selection();
            if (File.Exists(file_path))
            {
                rbtn_open_analysis.Checked = true;
            }
            else
                this.Close();
        }

        private void rbtn_CheckedChanged(object sender, EventArgs e)
        {
            Design_Selection();
        }

        private void Design_Selection()
        {

            if (rbtn_open_analysis.Checked)
            {
                //file_path = "C:\\Users\\user\\Desktop\\ASTRA\\[2014 10 06] British code test 01\\ANALYSIS OF RAILWAY BRIDGE\\ANALYSIS OF RAILWAY BRIDGE\\ASTRA_Data_Input.TXT"
                if (File.Exists(file_path))
                {
                    lbl_option.Text = "Open Previous Analysis && Design which is exist in the current working folder.";

                    btn_process.Enabled = true;
                }
                else
                {
                    lbl_option.Text = "Previous Analysis && Design not found in the working folder.";
                    btn_process.Enabled = false;
                }
            }
            else
            {
                btn_process.Enabled = true;
                if (File.Exists(file_path))
                {
                    lbl_option.Text = "Previous Analysis && Design already exist in the working folder that will be deleted by the \"New Design\".";
                    //lbl_option.Visible = rbtn_new_analysis.Checked;
                }
                else
                    lbl_option.Text = "Create New Analysis && Design in the working folder by the \"New Design\".";
            }
        }

        void Delete_All_Files(string dir_path)
        {
            try
            {
                foreach (var item in Directory.GetFiles(dir_path))
                {
                    File.Delete(item);
                }
                foreach (var item in Directory.GetDirectories(dir_path))
                {
                    Delete_All_Files(item);
                    Directory.Delete(item);
                }
            }
            catch (Exception exx) { }
        }
        void Delete_All_Directories(string dir_path)
        {
            try
            {
                foreach (var item in Directory.GetFiles(dir_path))
                {
                    File.Delete(item);
                }
                foreach (var item in Directory.GetDirectories(dir_path))
                {
                    Delete_All_Files(item);
                    Directory.Delete(item);
                }
            }
            catch (Exception exx) { }
        }
        private void btn_process_Click(object sender, EventArgs e)
        {
            if(IsNew_Analysis)
            {
                if (File.Exists(file_path))
                {
                    if (MessageBox.Show("This Process will DELETE all the files and folders.\n\n Do you want to DELETE ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        Delete_All_Directories(user_path);
                    else
                        return;
                }
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
