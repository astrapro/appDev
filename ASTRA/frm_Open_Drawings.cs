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
    public partial class frm_Open_Drawings : Form
    {
        public frm_Open_Drawings()
        {
            InitializeComponent();
        }
          //string user_path = "";
        IApplication iApp = null;
        string drwg_path = "";
        string drawing_command = "";
        eDrawingsType item_drawing = eDrawingsType.RCC_T_Girder;
        public frm_Open_Drawings(IApplication app, eDrawingsType item_drawing)
        {
            InitializeComponent();
            iApp = app;
            user_path = "";
            this.item_drawing = item_drawing;
        }
        public frm_Open_Drawings(IApplication app, eDrawingsType item_drawing, string working_folder)
        {
            InitializeComponent();
            iApp = app;
            user_path = working_folder;
            User_Drawing_Folder = user_path;
            this.item_drawing = item_drawing;
        }

        public string DrawingsPath 
        {
            get
            {
                if (item_drawing == eDrawingsType.RCC_BOX_Culvert)
                    return Path.Combine(Application.StartupPath, @"DRAWINGS\Culvert\RCC Box Culvert");
                else if (item_drawing == eDrawingsType.RCC_T_Girder)
                    return Path.Combine(Application.StartupPath, @"DRAWINGS\Bridges\T_Girder Bridge");
                return "";
            }
        }
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
        public string User_Drawing_Folder { get; set; }
        
        public string Title
        {
            set
            {
                this.Text = value;
            }
        }


        private void btn_working_folder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = iApp.LastDesignWorkingFolder;
                if (Directory.Exists(user_path))
                {

                    fbd.SelectedPath = user_path;
                    
                }
                if (fbd.ShowDialog() != DialogResult.Cancel)
                {
                    user_path = fbd.SelectedPath;
                    btn_drawing.Enabled = true;
                    cmb_drawing.Enabled = true;
                }
                iApp.LastDesignWorkingFolder = user_path;
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_drawing_Click(object sender, EventArgs e)
        {
            //System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", drwg_path);
            //iApp.RunViewer(user_path, drawing_command);

            if (item_drawing == eDrawingsType.RCC_T_Girder)
            {
                switch (cmb_drawing.Text)
                {
                    case "Drawings T_Girder Bridge Span 11m":
                        drawing_command = "T_Girder_Span_11m";
                        User_Drawing_Folder = "Span 11m";
                        break;
                    case "Drawings T_Girder Bridge Span 12m":
                        drawing_command = "T_Girder_Span_12m";
                        User_Drawing_Folder = "Span 12m";
                        break;
                    case "Drawings T_Girder Bridge Span 13m":
                        drawing_command = "T_Girder_Span_13m";
                        User_Drawing_Folder = "Span 13m";
                        break;
                    case "Drawings T_Girder Bridge Span 14_16m":
                        drawing_command = "T_Girder_Span_14_16m";
                        User_Drawing_Folder = "Span 14_16m";
                        break;
                    case "Drawings T_Girder Bridge Span 17m":
                        drawing_command = "T_Girder_Span_17m";
                        User_Drawing_Folder = "Span 17m";
                        break;
                    case "Drawings T_Girder Bridge Span 18_19m":
                        drawing_command = "T_Girder_Span_18_19m";
                        User_Drawing_Folder = "Span 18_19m";
                        break;
                    case "Drawings T_Girder Bridge Span 20_26m":
                        drawing_command = "T_Girder_Span_20_26m";
                        User_Drawing_Folder = "Span 20_26m";
                        break;
                    case "Drawings T_Girder Bridge Span 27m":
                        drawing_command = "T_Girder_Span_27m";
                        User_Drawing_Folder = "Span 27m";
                        break;
                    case "Drawings T_Girder Bridge Span 28m":
                        User_Drawing_Folder = "Span 28m";
                        drawing_command = "T_Girder_Span_28m";
                        break;
                }
            }
            else if (item_drawing == eDrawingsType.RCC_BOX_Culvert)
            {
                switch (cmb_drawing.Text)
                {
                    case "Single Cell Box Culvert":
                        drawing_command = "Box_Culvert_Single_Cell";
                        User_Drawing_Folder = "Box_Culvert_Single_Cell";
                        break;

                    case "Double Cell Box Culvert":
                        drawing_command = "Box_Culvert_Double_Cell";
                        User_Drawing_Folder = "Box_Culvert_Double_Cell";
                        break;

                    case "Tripple Cell Box Culvert":
                        drawing_command = "Box_Culvert_Tripple_Cell";
                        User_Drawing_Folder = "Box_Culvert_Tripple_Cell";
                        break;
                }
            }
            User_Drawing_Folder = Path.Combine(user_path, User_Drawing_Folder);
            if (!Directory.Exists(User_Drawing_Folder))
                Directory.CreateDirectory(User_Drawing_Folder);
            iApp.RunViewer(User_Drawing_Folder, drawing_command);
        }

        private void frm_Open_Drawings_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(user_path))
            {
                btn_drawing.Enabled = true;
                cmb_drawing.Enabled = true;
                btn_working_folder.Enabled = false;
            }
            cmb_drawing.Items.Clear();

            foreach (var item in Directory.GetDirectories(DrawingsPath))
            {
                cmb_drawing.Items.Add(Path.GetFileName(item));
            }
            if (cmb_drawing.Items.Count > 0)
                cmb_drawing.SelectedIndex = 0;
        }

    }
}
