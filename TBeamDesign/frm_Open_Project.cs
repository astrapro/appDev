using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.DataStructure;
using AstraInterface.Interface;


namespace BridgeAnalysisDesign
{
    public partial class frm_Open_Project : Form
    {
        public string Project_Type { get; set; }

        public string Wokring_Folder
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
        public frm_Open_Project(string ProjectType, string WorkingFolder)
        {
            InitializeComponent();
            Wokring_Folder = WorkingFolder;
            Project_Type = ProjectType;
        }

        private void frm_Project_Folder_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(Wokring_Folder))
            {
                string kStr = "";
                foreach (var item in Directory.GetDirectories(Wokring_Folder))
                {
                    //kStr = Path.Combine(item, Project_Type + ".apr");
                    kStr = Path.Combine(item, "Process\\Design.Sys");
                    if (File.Exists(kStr))
                        lst_proj_folders.Items.Add(Path.GetFileName(item));
                }
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            //iproj.Project_Folder = lst_proj_folders.SelectedItem.ToString();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            
        }

        public  string Example_Path
        {
            get
            {
                try
                {
                    return Path.Combine(Wokring_Folder, lst_proj_folders.SelectedItem.ToString());
                }
                catch (Exception exx) { }

                return "";
            }
        }

        public string SaveAs_Path
        {
            get
            {
                try
                {
                    if (lst_proj_folders.SelectedItem.ToString() == txt_save_as.Text) return "";

                    return Path.Combine(Wokring_Folder, txt_save_as.Text);
                }
                catch (Exception exx) { }

                return "";
            }
        }
        public void Load_Files()
        {

            string fld = Example_Path;
            //string fld = Wokring_Folder;

            //if (rbtn_TEXT.Checked)
            //    fld = TEXT_Example_Path;
            //else if (rbtn_SAP.Checked)
            //    fld = SAP_Example_Path;

            if (Directory.Exists(fld))
            {
                tv_files.Nodes.Clear();

                foreach (var item in Directory.GetDirectories(fld))
                {
                    //if (!item.ToUpper().Contains("TERRAIN") && !item.ToUpper().Contains("HYDRO"))
                    //{
                    tv_files.Nodes.Add(Path.GetFileName(item));

                    Load_Files(tv_files.Nodes[tv_files.Nodes.Count - 1], item, false);
                    //}
                }
                //if (tv_files.Nodes.Count == 0)
                //{

                    foreach (var item4 in Directory.GetFiles(fld))
                    {
                    //    if (Path.GetExtension(item4).ToLower() == ".dwg" ||
                    //        Path.GetExtension(item4).ToLower() == ".dxf" ||
                    //        Path.GetExtension(item4).ToLower() == ".vdml" ||
                    //        Path.GetExtension(item4).ToLower() == ".pdf")
                            tv_files.Nodes.Add(Path.GetFileName(item4));
                    }
                //}

            }
        }

        public void Load_Files(TreeNode tn, string fld, bool Show_Processed_Files)
        {
            //string fld = Path.Combine(Application.StartupPath, "ASTRA Pro Analysis Examples");

            if (Directory.Exists(fld))
            {
                foreach (var item in Directory.GetDirectories(fld))
                {

                    if (!Show_Processed_Files)
                        if (Path.GetFileName(item).ToLower().Contains("processed"))
                            continue;

                    tn.Nodes.Add(Path.GetFileName(item));

                    foreach (var item2 in Directory.GetDirectories(item))
                    {
                        if (!Show_Processed_Files)
                            if (Path.GetFileName(item2).ToLower().Contains("processed"))
                                continue;

                        tn.Nodes[tn.Nodes.Count - 1].Nodes.Add(Path.GetFileName(item2));

                        Load_Files(tn.Nodes[tn.Nodes.Count - 1].Nodes[tn.Nodes[tn.Nodes.Count - 1].Nodes.Count - 1], item2, Show_Processed_Files);

                    }
                    foreach (var item3 in Directory.GetFiles(item))
                    {
                        if (Path.GetExtension(item3).ToLower() == ".txt")
                        {
                            if (!Path.GetFileNameWithoutExtension(item3).ToUpper().StartsWith("README"))
                                tn.Nodes[tn.Nodes.Count - 1].Nodes.Add(Path.GetFileName(item3));
                        }
                        if (Path.GetExtension(item3).ToLower() == ".rep")
                        {
                            if (!Path.GetFileNameWithoutExtension(item3).ToUpper().StartsWith("README"))
                                tn.Nodes[tn.Nodes.Count - 1].Nodes.Add(Path.GetFileName(item3));
                        }
                        if (Path.GetExtension(item3).ToLower() == ".pdf")
                        {
                            //if (!Path.GetFileNameWithoutExtension(item3).ToUpper().StartsWith("README"))
                            tn.Nodes[tn.Nodes.Count - 1].Nodes.Add(Path.GetFileName(item3));
                        }
                        else
                        {
                            if (Path.GetExtension(item3).ToLower() == ".dwg" ||
                                Path.GetExtension(item3).ToLower() == ".dxf" ||
                                Path.GetExtension(item3).ToLower() == ".vdml")
                                tn.Nodes[tn.Nodes.Count - 1].Nodes.Add(Path.GetFileName(item3));
                        }
                    }
                }
                foreach (var item4 in Directory.GetFiles(fld))
                {
                    if (Path.GetExtension(item4).ToLower() == ".txt" ||
                        Path.GetExtension(item4).ToLower() == ".pdf")
                    {
                        if (!Path.GetFileNameWithoutExtension(item4).ToUpper().StartsWith("README"))
                            tn.Nodes.Add(Path.GetFileName(item4));
                    }
                    else
                    {
                        if (Path.GetExtension(item4).ToLower() == ".dwg" ||
                            Path.GetExtension(item4).ToLower() == ".dxf" ||
                            Path.GetExtension(item4).ToLower() == ".vdml")
                            tn.Nodes.Add(Path.GetFileName(item4));
                    }
                }
            }
        }

        private void lst_proj_folders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_proj_folders.SelectedItem != null)
            {
                txt_save_as.Text = lst_proj_folders.SelectedItem.ToString();
                Load_Files();
            }
        }

    }
}


