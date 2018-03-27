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
    public partial class frm_Examples : Form
    {
        IApplication iApp;

        public bool IsProcess { get; set; }
        public frm_Examples(IApplication app, bool isProcess)
        {
            InitializeComponent();
            iApp = app;
            IsProcess = isProcess;
        }

        //public string OutputFile { get; set; }
        public string Example_Path
        {
            get
            {
                return Path.Combine(Application.StartupPath, "ASTRA Pro Analysis Examples");
            }
        }

        public string Active_Example_Path
        {
            get
            {
                if (rbtn_TEXT.Checked || rbtn_Stage.Checked)
                    return TEXT_Example_Path;

                else if (rbtn_SAP.Checked)
                    return SAP_Example_Path;

                else if (rbtn_dwg.Checked)
                    return DRAWING_Example_Path;


                return Example_Path;
            }
        }
        public string TEXT_Example_Path
        {
            get
            {
                return Path.Combine(Example_Path, "01 Analysis with Text Data File");
            }
        }
        public string SAP_Example_Path
        {
            get
            {
                return Path.Combine(Example_Path, "03 Analysis with SAP Data File");
            }
        }

        public string DRAWING_Example_Path
        {
            get
            {
                return Path.Combine(Example_Path, "02 Analysis with Drawing File");
            }
        }

        public string Working_Folder
        {
            get
            {
                try
                {
                    if (iApp.LastDesignWorkingFolder == "")
                    {
                        if (Directory.Exists(iApp.WorkingFolder))
                        {
                            iApp.LastDesignWorkingFolder = iApp.WorkingFolder;
                        }
                    }
                }
                catch (Exception ex)
                {
                    using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                    {
                        if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                        {
                            iApp.LastDesignWorkingFolder = fbd.SelectedPath;
                            return iApp.LastDesignWorkingFolder;
                        }
                    }
                }
                //MyList
                return iApp.LastDesignWorkingFolder;

                //return iApp.WorkingFolder;
            }
        }
        public void Load_Files()
        {

            //string fld = Example_Path;
            string fld = Active_Example_Path;

            //if (rbtn_TEXT.Checked)
            //    fld = TEXT_Example_Path;
            //else if (rbtn_SAP.Checked)
            //    fld = SAP_Example_Path;

            if(Directory.Exists(fld))
            {
                tv_files.Nodes.Clear();
                cmb_examples.Items.Clear();

                foreach (var item in Directory.GetDirectories(fld))
                {
                    if (!item.ToUpper().Contains("TERRAIN") && !item.ToUpper().Contains("HYDRO"))
                    {
                        tv_files.Nodes.Add(Path.GetFileName(item));
                        cmb_examples.Items.Add(Path.GetFileName(item));

                        Load_Files(tv_files.Nodes[tv_files.Nodes.Count - 1], item, chk_show_processed_files.Checked);
                    }
                }
                if (tv_files.Nodes.Count == 0)
                {

                    foreach (var item4 in Directory.GetFiles(fld))
                    {
                        //if (Path.GetExtension(item4).ToLower() == ".txt")
                        //{
                        //    if (!Path.GetFileNameWithoutExtension(item4).ToUpper().StartsWith("README"))
                        //        tv_files.Nodes.Add(Path.GetFileName(item4));
                        //}
                        //else
                        //{
                        if (Path.GetExtension(item4).ToLower() == ".dwg" ||
                            Path.GetExtension(item4).ToLower() == ".dxf" ||
                            Path.GetExtension(item4).ToLower() == ".vdml")
                            tv_files.Nodes.Add(Path.GetFileName(item4));
                        //}
                    }
                }
              
            }

            if (cmb_examples.Items.Count > 0)
                cmb_examples.SelectedIndex = 0;

            if (iApp.LastDesignWorkingFolder != "")
            this.Text = "ASTRA Pro Analysis Examples [" + MyList.Get_Modified_Path(iApp.LastDesignWorkingFolder) + "]";
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
                    if (Path.GetExtension(item4).ToLower() == ".txt")
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

        private void frm_Examples_Load(object sender, EventArgs e)
        {
            //chk_show_processed_files.Checked = true;
            btn_open_analysis.Visible = IsProcess;
            btn_open_input.Visible = IsProcess;
            Load_Files();
        }

        private void tv_files_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (e.Node.Parent == null)
            {
                if (cmb_examples.Items.Count > 0)
                {
                    cmb_examples.SelectedIndex = e.Node.Index;
                    return;
                }
            }
            //string fname = Path.Combine(Example_Path, tv_files.SelectedNode.FullPath);
            string fname = Path.Combine(Active_Example_Path, tv_files.SelectedNode.FullPath);

            if (File.Exists(fname))
            {
                string fn = Path.GetFileNameWithoutExtension(fname).ToUpper();
                //btn_copy.Enabled = ((fn != "ANALYSIS_REP") && (fn != "SAP_INPUT_DATA") && !fn.StartsWith("README"));
                btn_copy.Enabled = ((fn != "ANALYSIS_REP") && !fn.StartsWith("README"));
                if (Path.GetExtension(fname).ToLower() == ".txt")
                {
                    rtb_input_files.Lines = File.ReadAllLines(fname);
                    //lbl_fname.Text = tv_files.SelectedNode.FullPath;
                    lbl_fname.Text = Path.GetFileName(fname);
                }
                else
                {
                    rtb_input_files.Text = "This is a Drawing file.";
                    //lbl_fname.Text = tv_files.SelectedNode.FullPath;
                    lbl_fname.Text = Path.GetFileName(fname);
                }

            }
            else
                btn_copy.Enabled = false;

            btn_save.Enabled = btn_copy.Enabled;
            btn_open_analysis.Enabled = btn_copy.Enabled;
            btn_open_input.Enabled = btn_copy.Enabled;

            //for (int i = 0; i < tv_files.Nodes.Count; i++)
            //{
            //    tv_files.Nodes[i].BackColor = Color.White;

            //}
            //tv_files.SelectedNode.BackColor = Color.Cyan;
            //MyList.Get_LL_TXT_File
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;


            //if (iApp.LastDesignWorkingFolder == "")
            //{
            //    iApp.LastDesignWorkingFolder = Path.GetDirectoryName(iApp.WorkingFile);

            //}
            if (btn.Name == btn_copy.Name || btn.Name == btn_open_analysis.Name || btn.Name == btn_open_input.Name)
            {

                #region Copy Examples to Working Folder
                //string fname = Path.Combine(Example_Path, tv_files.SelectedNode.FullPath);
                string fname = Path.Combine(Active_Example_Path, tv_files.SelectedNode.FullPath);
                if (File.Exists(fname))
                {

                    string fld1 = Path.GetDirectoryName(tv_files.SelectedNode.FullPath);
                    string fld2 = "";
                    if(fld1 != "")
                     fld2 = Path.GetDirectoryName(fld1);


                    if (fld2 != "")
                        fld1 = Path.GetDirectoryName(fld2);
                    //string fld2 = tv_files.SelectedNode.
                    fld1 = Path.GetFileName(fld1);
                    fld2 = Path.GetFileName(fld2);

                    string ww = Working_Folder;
                    if (ww == "") return;

                    string fld3 = Path.Combine(Working_Folder, fld2);

                    fld3 = Path.Combine(fld3, fld1);


                    if (fld1 != "")
                    {
                        fld3 = Path.Combine(Working_Folder, fld1);
                        fld3 = Path.Combine(fld3, fld2);
                    }
                    else
                    {
                        fld3 = Path.Combine(Working_Folder, fld2);
                        //fld3 = Path.Combine(fld3, fld1);
                    }

                    if (!Directory.Exists(fld3))
                        Directory.CreateDirectory(fld3);

                    string fna1 = Path.Combine(fld3, Path.GetFileName(fname));

                    if (Path.GetExtension(fna1).ToLower() == ".txt")
                        File.WriteAllLines(fna1, rtb_input_files.Lines);

                    string ll_fname = MyList.Get_LL_TXT_File(fname);
                    if (File.Exists(ll_fname))
                    {
                        File.Copy(ll_fname, MyList.Get_LL_TXT_File(fna1), true);
                    }


                    foreach (var item in Directory.GetFiles(Path.GetDirectoryName(fname)))
                    {
                        //if (fname.ToUpper() != item.ToUpper())
                        try
                        {
                            File.Copy(item, Path.Combine(Path.GetDirectoryName(fna1), Path.GetFileName(item)), false);
                        }
                        catch (Exception ex1) { }
                    }

                    iApp.WorkingFile = fna1;
                    iApp.EXAMPLE_File = fna1;

                    if (Path.GetFileName(fna1).ToUpper().Contains("SAP"))
                        iApp.SAP_File = fna1;
                    else if (Path.GetExtension(fna1).ToUpper().Contains("DWG") ||
                        Path.GetExtension(fna1).ToUpper().Contains("VDML") ||
                        Path.GetExtension(fna1).ToUpper().Contains("DXF"))
                        iApp.Drawing_File = fna1;
                    else
                        iApp.TEXT_File = fna1;


                    if (btn.Name == btn_open_input.Name)
                    {
                        iApp.OpenWork(iApp.WorkingFile, "");

                    }
                    else if (btn.Name == btn_open_analysis.Name)
                    {
                        iApp.OpenWork(iApp.WorkingFile, false);
                    }
                    else
                    {
                        MessageBox.Show("Input Data File copied to Working Folder as [" + fna1 + "]", "ASTRA",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //iApp.OpenWork(fna1, false);
                }
                #endregion Copy Examples to Working Folder
            }
            else if (btn.Name == btn_save.Name)
            {
                #region Save Examples to Working Folder

                string fname = Path.Combine(Active_Example_Path, tv_files.SelectedNode.FullPath);
                string ll_fname = MyList.Get_LL_TXT_File(fname);

                if (File.Exists(fname))
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Filter = "Text Files (*.txt)|*.txt";
                        if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                        {
                            File.WriteAllLines(sfd.FileName, rtb_input_files.Lines);
                            if (File.Exists(ll_fname))
                            {
                                File.Copy(ll_fname, MyList.Get_LL_TXT_File(sfd.FileName), true);
                            }

                            iApp.WorkingFile = sfd.FileName;
                            iApp.EXAMPLE_File = sfd.FileName;
                            if (Path.GetFileNameWithoutExtension(sfd.FileName).ToUpper().Contains("SAP"))
                                iApp.SAP_File = sfd.FileName;
                            else if (Path.GetExtension(sfd.FileName).ToUpper().Contains("DWG"))
                                iApp.Drawing_File = sfd.FileName;
                            else
                                iApp.TEXT_File = sfd.FileName;


                        }
                    }
                }
                #endregion Save Examples to Working Folder
            }
            else if (btn.Name == btn_open_input.Name)
            {
                if (File.Exists(iApp.WorkingFile))
                    iApp.OpenWork(iApp.WorkingFile, "");
            }
            else if (btn.Name == btn_open_analysis.Name)
            {
                if (File.Exists(iApp.WorkingFile))
                    iApp.OpenWork(iApp.WorkingFile, false);
            }
            else if (btn.Name == btn_close.Name)
            {
                this.Close();
            }
            this.Text = "ASTRA Pro Analysis Examples [" + MyList.Get_Modified_Path(iApp.LastDesignWorkingFolder) + "]";
        }

        private void Copy_Data_to_Working_Folder()
        {

        }

        private void chk_show_processed_files_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (chk.Name == chk_show_processed_files.Name)
                Load_Files();
            else if (chk.Name == chk_expend.Name)
            {
                if (chk.Checked)
                    tv_files.ExpandAll();
                else
                    tv_files.CollapseAll();
            }

        }

        private void cmb_examples_SelectedIndexChanged(object sender, EventArgs e)
        {
            chk_expend.Checked = false;
            tv_files.CollapseAll();


            tv_files.SelectedNode = tv_files.Nodes[cmb_examples.SelectedIndex];
            tv_files.SelectedNode.ExpandAll();

            while(tv_files.SelectedNode.Nodes.Count > 0)
            {
                tv_files.SelectedNode = tv_files.SelectedNode.Nodes[0];
            }

            for (int i = 0; i < tv_files.Nodes.Count; i++)
            {
                tv_files.Nodes[i].BackColor = Color.White;

            }

            tv_files.Nodes[cmb_examples.SelectedIndex].BackColor = Color.Cyan;


            tv_files.Focus();


        }

        private void rbtn_TEXT_CheckedChanged(object sender, EventArgs e)
        {
            Load_Files();

        }
    }


}
