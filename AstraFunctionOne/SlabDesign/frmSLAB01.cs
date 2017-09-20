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

namespace AstraFunctionOne.SlabDesign
{
    public partial class frmSLAB01 : Form
    {
        CSLAB01 slb01 = new CSLAB01();
        IApplication iApp = null;
        string filePathSet = "";
        string rep_filePath = "";
        string FilePath = "";
        public frmSLAB01(IApplication iApp)
        {
            InitializeComponent();
            this.iApp = iApp;
            FilePath = iApp.AppDocument.FilePath;
            //if (File.Exists(FilePath))
            //{
            //    FilePath = Path.GetDirectoryName(FilePath);
            //}
            //else
            //{
            //    if (Directory.Exists(FilePath))
            //        FilePath = FilePath;
            //    else
            //        throw new Exception("INVALID Path " + FilePath);
            //}
        }
        public bool SetPath()
        {
            if (Directory.Exists(filePathSet)) return true;

            if (!File.Exists(iApp.AppDocument.FilePath))
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    if (fbd.ShowDialog() != DialogResult.Cancel)
                    {
                        filePathSet = fbd.SelectedPath;
                    }
                    else
                        return false;
                }
            }

            return Directory.Exists(filePathSet);
        }


        public bool Set_SLAB01()
        {

            bool success = SetPath();
            if (!success) return success;
            try
            {
                slb01.D1 = double.Parse(txt_d1.Text);
                slb01.D2 = double.Parse(txt_d2.Text);
                slb01.H = double.Parse(txt_h.Text);
                slb01.Gamma_C = double.Parse(txt_gamma_c.Text);
                slb01.Gc = double.Parse(txt_Gc.Text);
                slb01.L = double.Parse(txt_L.Text);
                slb01.B1 = double.Parse(txt_b1.Text);
                slb01.B2 = double.Parse(txt_b2.Text);
                slb01.W1 = double.Parse(txt_w1.Text);
                slb01.W2 = double.Parse(txt_w2.Text);
                slb01.Dst = double.Parse(txt_Dst.Text);
                slb01.Sigma_St = double.Parse(txt_sigma_st.Text);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (Set_SLAB01())
            {
                FilePath = Path.Combine(filePathSet,"DESIGN_SLAB01");
                DirectoryInfo dInfo =  Directory.CreateDirectory(FilePath);
                rep_filePath = Path.Combine(FilePath, "SLAB01.txt");
                if (slb01.CalculateProgram(rep_filePath))
                {
                    MessageBox.Show(this, "Report written to file " + rep_filePath, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Enable_Button = true;
                }
            }
        }
        public bool Enable_Button
        {
            set
            {
                btnReport.Enabled = value;
                btnView.Enabled = value;
                btnDrawing.Enabled = value;
                btnBoQ.Enabled = value;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSLAB01_Load(object sender, EventArgs e)
        {
            Enable_Button = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Data saved in " + Path.Combine(filePathSet, "DESIGN_SLAB01\\SLAB01.txt"));

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            iApp.SetApp_Design_Slab(filePathSet, eSLAB.SLAB01, eSLAB_Part.VIEW);
        }
        public void ClearGlobalVars()
        {
            //System.Environment.SetEnvironmentVariable("ASTRA", null);

            //System.Environment.SetEnvironmentVariable("DESIGN_SLAB01_VIEW", null);
            //System.Environment.SetEnvironmentVariable("DESIGN_SLAB01_DRAWING", null);
            //System.Environment.SetEnvironmentVariable("DESIGN_SLAB02_VIEW", null);
            //System.Environment.SetEnvironmentVariable("DESIGN_SLAB02_DRAWING", null);
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            frmViewResult fvRes = new frmViewResult(rep_filePath);
            fvRes.ShowDialog();
        }

        private void btnDrawing_Click(object sender, EventArgs e)
        {
            iApp.SetApp_Design_Slab(filePathSet, eSLAB.SLAB01, eSLAB_Part.DRAWING);
        }

        private void btnBoQ_Click(object sender, EventArgs e)
        {
            iApp.SetApp_Design_Slab(filePathSet, eSLAB.SLAB01, eSLAB_Part.BoQ);
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
