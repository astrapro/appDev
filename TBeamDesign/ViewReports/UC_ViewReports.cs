using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AstraInterface.Interface;

namespace BridgeAnalysisDesign.ViewReports
{
    public partial class UC_ViewReports : UserControl
    {
        public IApplication iApp { get; set; }
        public UC_ViewReports()
        {
            InitializeComponent();
        }

        public void Load_Analysis_Reports()
        {
            ListDirectory(trv_reports, iApp.user_path);
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);

            string ext = "";
            foreach (var directory in directoryInfo.GetDirectories())
            {
                if (Directory.GetFiles(directory.FullName).Length > 0 ||
                    Directory.GetDirectories(directory.FullName).Length > 0)
                    directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                ext = Path.GetExtension(file.Name.ToLower());
                if (ext == ".txt" || ext.StartsWith(".xls"))
                    directoryNode.Nodes.Add(new TreeNode(file.Name));
            }
            return directoryNode;
        }

        private void trv_reports_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string file_name = Path.Combine(Path.GetDirectoryName(iApp.user_path), trv_reports.SelectedNode.FullPath);

            if (File.Exists(file_name))
            {
                if (Path.GetExtension(file_name.ToLower()) == ".txt")
                {
                    //lbl_rep_file.Text = Path.GetFileName(file_name);
                    lbl_rep_file.Text = trv_reports.SelectedNode.FullPath;
                    rtb_rep_file.Lines = File.ReadAllLines(file_name);
                }
                else
                {
                    rtb_rep_file.Text = Path.GetFileName(file_name);
                }
            }
            btn_open_file.Enabled = File.Exists(file_name);
        }

        private void btn_open_file_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string file_name = Path.Combine(Path.GetDirectoryName(iApp.user_path), trv_reports.SelectedNode.FullPath);


            if (btn == btn_open_file)
            {
                if (Path.GetExtension(file_name.ToLower()).StartsWith(".xls"))
                {
                    iApp.OpenExcelFile(file_name, "2011ap");
                }
                else
                {
                    System.Diagnostics.Process.Start(file_name);
                }
            }
            else if (btn == btn_open_folder)
            {
                if (File.Exists(file_name))
                {
                    System.Diagnostics.Process.Start(Path.GetDirectoryName(file_name));
                }
                else
                {
                    System.Diagnostics.Process.Start(file_name);
                }
            }

        }
    }
}
