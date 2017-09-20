using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;

namespace AstraFunctionOne
{
    public partial class frm_OpenWorksheet_Design : Form
    {
         //string user_path = "";
        IApplication iApp = null;
        string drwg_path = "";
        string drawing_command = "";
        public frm_OpenWorksheet_Design(IApplication app, string excel_file, string _cmdString)
        {
            InitializeComponent();
            iApp = app;
            WorksheetFileName = excel_file;
            user_path = app.LastDesignWorkingFolder;
            drawing_command = _cmdString;
            MyList mlist = new MyList(_cmdString, ',');
            Drawing_DirectoryName = iApp.Des_Drawings.Get_Default_Drawing_Path(mlist.StringList[0]);
        }

        public string WorksheetFileName { get; set; }
        public string user_path { get; set; }
        public string Drawing_DirectoryName 
        {
            get
            {
                return drwg_path;
            }
            set
            {
                if (value != "" && value != null)
                {
                    drwg_path = Path.Combine(Application.StartupPath, "DRAWINGS");
                    drwg_path = Path.Combine(drwg_path, value);
                }
            }
        }
        public string Title
        {
            set
            {
                this.Text = value;
            }
        }


        private void frm_OpenWorksheet_Design_Load(object sender, EventArgs e)
        {
            //btn_drawing.Enabled = (Directory.Exists(Drawing_DirectoryName));

            //if (btn_drawing.Enabled == false)
            //{
            //    btn_close.Location = new Point(312, 12);
            //    this.Width -= 104;
            //    btn_drawing.Visible = false;
            //}

        }

        private void btn_design_Click(object sender, EventArgs e)
        {
            string source_path = Path.Combine(Application.StartupPath, "DESIGN");

            source_path = Path.Combine(source_path, WorksheetFileName);
            string copy_path = Path.Combine(user_path, Path.GetFileName(WorksheetFileName));
            if (File.Exists(source_path))
            {
                try
                {
                    //iApp.OpenExcelFile(source_path, "2011ap");

                    File.Copy(source_path, copy_path, true);
                    iApp.OpenExcelFile(copy_path, "2011ap");
                    btn_drawing.Enabled = (Directory.Exists(Drawing_DirectoryName));
                }
                catch (Exception ex) { }
            }
            else
            {
                MessageBox.Show(source_path, " file not found.", MessageBoxButtons.OK);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_drawing_Click(object sender, EventArgs e)
        {
            //System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", drwg_path);

            string p = Path.Combine(user_path, drawing_command);
            if (!Directory.Exists(p))
                Directory.CreateDirectory(p);
            //iApp.RunViewer(user_path, drawing_command);
            iApp.RunViewer(p, drawing_command);
        }

        private void btn_open_desg_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Worksheet Design File(*.xls)|*.xls;*.xlsx";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    user_path = Path.GetDirectoryName(ofd.FileName);
                    iApp.OpenExcelFile(ofd.FileName, "2011ap");
                    btn_drawing.Enabled = (Directory.Exists(Drawing_DirectoryName));
                }
            }
        }
    }
}
