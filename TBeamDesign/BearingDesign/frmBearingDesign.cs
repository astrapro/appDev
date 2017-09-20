using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraInterface.TrussBridge;

namespace BridgeAnalysisDesign.BearingDesign
{
    public partial class frmBearingDesign : Form
    {
        const string Title = "DESIGN OF BEARING";
        IApplication iApp = null;

        POT_PTFE_VERSO_BEARING_DESIGN VMABT;

        public frmBearingDesign(IApplication app)
        {
            InitializeComponent();

            iApp = app;
            user_path = iApp.LastDesignWorkingFolder;
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);


        }
        public string user_path { get; set; }

        public string Working_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(user_path, Title)) == false)
                    Directory.CreateDirectory(Path.Combine(user_path, Title));
                return Path.Combine(user_path, Title);
            }
        }
        public string Worksheet_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(user_path, "Worksheet_Design")) == false)
                    Directory.CreateDirectory(Path.Combine(user_path, "Worksheet_Design"));
                return Path.Combine(user_path, "Worksheet_Design");
            }
        }


        public string Drawing_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(user_path, "DRAWINGS")) == false)
                    Directory.CreateDirectory(Path.Combine(user_path, "DRAWINGS"));
                return Path.Combine(user_path, "DRAWINGS");
            }
        }

        private void btn_bearing_worksheet_Click(object sender, EventArgs e)
        {
            try
            {
                string excel_path = Path.Combine(Application.StartupPath, @"DESIGN\Bearing Design");
                string excel_file = "Bridge_Bearing_Design.xls";
                excel_file = Path.Combine(excel_path, excel_file);
                if (File.Exists(excel_file))
                {
                    iApp.OpenExcelFile(Worksheet_Folder, excel_file, "2011ap");
                }
            }
            catch (Exception ex) { }
        }
        #region MONO AXIAL BEARING TRANSVERSE
        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            VMABT.Working_Folder = Working_Folder;
            btn.BackColor = Color.LightBlue;
            if (btn.Name != btn_des_report.Name &&
               btn.Name != btn_save_design.Name &&
                btn.Name != btn_open_design.Name)
            {
                InVisible_Type1();
            }
            #region MONO AXIAL BEARING TRANSVERSE

            if (btn.Name == btn_1_deg_param.Name)
            {
                grb_1_deg_param.Visible = true;
            }
            else if (btn.Name == btn_2_1.Name)
            {
                grb_2_1.Visible = true;
            }
            else if (btn.Name == btn_2_2.Name)
            {
                grb_2_2.Visible = true;
            }
            else if (btn.Name == btn_2_3.Name)
            {
                grb_2_3.Visible = true;
            }
            else if (btn.Name == btn_2_4.Name)
            {
                grb_2_4.Visible = true;
            }
            else if (btn.Name == btn_2_5.Name)
            {
                grb_2_5.Visible = true;
            }
            else if (btn.Name == btn_2_6.Name)
            {
                grb_2_6.Visible = true;
            }
            else if (btn.Name == btn_2_7.Name)
            {
                grb_2_7.Visible = true;
            }
            else if (btn.Name == btn_2_8.Name)
            {
                grb_2_8.Visible = true;
            }
            else if (btn.Name == btn_3_1_bottom.Name)
            {
                grb_3_1_bottom.Visible = true;
            }
            else if (btn.Name == btn_3_1__top.Name)
            {
                grb_3_1_top.Visible = true;
            }
            else if (btn.Name == btn_3_2.Name)
            {
                grb_3_2.Visible = true;
            }
            else if (btn.Name == btn_3_3.Name)
            {
                grb_3_3.Visible = true;
            }
            else if (btn.Name == btn_3_4.Name)
            {
                grb_3_4.Visible = true;
            }
            else if (btn.Name == btn_3_5.Name)
            {
                grb_3_5.Visible = true;
            }
            else if (btn.Name == btn_3_6.Name)
            {
                grb_3_6.Visible = true;
            }
            else if (btn.Name == btn_3_7.Name)
            {
                grb_3_7.Visible = true;
            }
            else if (btn.Name == btn_3_8.Name)
            {
                grb_3_8.Visible = true;
            }
            else if (btn.Name == btn_des_report.Name)
            {
                Set_Type1_Input_Data();

                VMABT.Generate_Report_VERSO_MONO_AXIAL_BEARING_TRANSVERSE();

                iApp.View_Result(VMABT.Report_File);
            }
            #endregion MONO AXIAL BEARING TRANSVERSE

            else if (btn.Name == btn_save_design.Name)
            {

                Set_Type1_Input_Data();

                VMABT.Generate_Report_VERSO_MONO_AXIAL_BEARING_TRANSVERSE();

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "All Report Files(*.txt)|*.txt";
                    if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                    {
                        try
                        {
                            File.Copy(VMABT.Report_File, sfd.FileName, true);
                            iApp.View_Result(sfd.FileName);
                        }
                        catch (Exception ex) { }
                    }
                }
            }

            else if (btn.Name == btn_open_design.Name)
            {


                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "All Report Files(*.txt)|*.txt";
                    if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                    {
                        try
                        {
                            //To do
                        }
                        catch (Exception ex) { }
                    }
                }

            }
        }

        //VERSO_MONO_AXIAL_BEARING_TRANSVERSE
        public void InVisible_Type1()
        {
            grb_1_deg_param.Dock = DockStyle.Fill;


            grb_2_1.Dock = DockStyle.Fill;
            grb_2_2.Dock = DockStyle.Fill;
            grb_2_3.Dock = DockStyle.Fill;
            grb_2_4.Dock = DockStyle.Fill;
            grb_2_5.Dock = DockStyle.Fill;
            grb_2_6.Dock = DockStyle.Fill;
            grb_2_7.Dock = DockStyle.Fill;
            grb_2_8.Dock = DockStyle.Fill;

            grb_3_1_bottom.Dock = DockStyle.Fill;
            grb_3_1_top.Dock = DockStyle.Fill;
            grb_3_2.Dock = DockStyle.Fill;
            grb_3_3.Dock = DockStyle.Fill;
            grb_3_4.Dock = DockStyle.Fill;
            grb_3_5.Dock = DockStyle.Fill;
            grb_3_6.Dock = DockStyle.Fill;
            grb_3_7.Dock = DockStyle.Fill;
            grb_3_8.Dock = DockStyle.Fill;


            grb_1_deg_param.Visible = false;
            grb_2_1.Visible = false;
            grb_2_2.Visible = false;
            grb_2_3.Visible = false;
            grb_2_4.Visible = false;
            grb_2_5.Visible = false;
            grb_2_6.Visible = false;
            grb_2_7.Visible = false;
            grb_2_8.Visible = false;
            grb_3_1_bottom.Visible = false;
            grb_3_1_top.Visible = false;
            grb_3_2.Visible = false;
            grb_3_3.Visible = false;
            grb_3_4.Visible = false;
            grb_3_5.Visible = false;
            grb_3_6.Visible = false;
            grb_3_7.Visible = false;
            grb_3_8.Visible = false;
        }

        public void Set_Type1_Input_Data()
        {


            VMABT.Nmax = MyList.StringToDouble(txt_1_Nmax.Text, 0.0);
            VMABT.Hlatn = MyList.StringToDouble(txt_1_Hlatn.Text, 0.0);

            VMABT.Nnorm = MyList.StringToDouble(txt_1_Nnorm.Text, 0.0);
            VMABT.Hlts = MyList.StringToDouble(txt_1_Hlts.Text, 0.0);
            VMABT.Nmin = MyList.StringToDouble(txt_1_Nmin.Text, 0.0);
            VMABT.Hlng_n = MyList.StringToDouble(txt_1_Hingn.Text, 0.0);
            VMABT.theta_p = MyList.StringToDouble(txt_1_theta_p.Text, 0.0);
            VMABT.Hlng_s = MyList.StringToDouble(txt_1_Hings.Text, 0.0);
            VMABT.theta_v = MyList.StringToDouble(txt_1_theta_v.Text, 0.0);
            VMABT.H = MyList.StringToDouble(txt_1_H.Text, 0.0);
            VMABT.theta = MyList.StringToDouble(txt_1_theta.Text, 0.0);
            VMABT.elong = MyList.StringToDouble(txt_1_elong.Text, 0.0);
            VMABT.etrans = MyList.StringToDouble(txt_1_etrans.Text, 0.0);


            VMABT.steel_fy = MyList.StringToDouble(txt_2_fy.Text, 0.0);
            VMABT.di = MyList.StringToDouble(txt_2_di.Text, 0.0);
            VMABT.he = MyList.StringToDouble(txt_2_he.Text, 0.0);

            txt_2_Ae.Text = VMABT.Ae.ToString("f4");
            VMABT.IRHD = MyList.StringToDouble(txt_2_IRHD.Text, 0.0);
            VMABT.k1 = MyList.StringToDouble(txt_2_k1.Text, 0.0);
            VMABT.k2 = MyList.StringToDouble(txt_2_k2.Text, 0.0);

            txt_2_Med.Text = VMABT.Med.ToString("f4");
            txt_2_MRdn.Text = VMABT.MRdn.ToString("f4");
            txt_2_MRst.Text = VMABT.MRst.ToString("f4");
            txt_2_MRsl.Text = VMABT.MRsl.ToString("f4");


            VMABT.dp = MyList.StringToDouble(txt_2_dp.Text, 0.0);
            VMABT.w = MyList.StringToDouble(txt_2_w.Text, 0.0);
            VMABT.hm = MyList.StringToDouble(txt_2_hm.Text, 0.0);
            VMABT.An = MyList.StringToDouble(txt_2_An.Text, 0.0);
            VMABT.hts = MyList.StringToDouble(txt_2_hts.Text, 0.0);


            VMABT.Do_ = MyList.StringToDouble(txt_2_Do.Text, 0.0);
            VMABT.kb = MyList.StringToDouble(txt_2_kb.Text, 0.0);
            VMABT.do_ = MyList.StringToDouble(txt_2_do_.Text, 0.0);
            VMABT.hc = MyList.StringToDouble(txt_2_hc.Text, 0.0);

            txt_2_deb.Text = VMABT.deb.ToString("f4");
            txt_2_bp.Text = VMABT.bp.ToString("f4");
            VMABT.deb_avibl = MyList.StringToDouble(txt_2_deb.Text, 0.0);


            VMABT.a = MyList.StringToDouble(txt_2_a.Text, 0.0);
            VMABT.b = MyList.StringToDouble(txt_2_b.Text, 0.0);
            VMABT.ks = MyList.StringToDouble(txt_2_ks.Text, 0.0);
            VMABT.kt = MyList.StringToDouble(txt_2_kt.Text, 0.0);
            VMABT.as_ = MyList.StringToDouble(txt_2_as.Text, 0.0);
            VMABT.bs = MyList.StringToDouble(txt_2_bs.Text, 0.0);
            VMABT.ku = MyList.StringToDouble(txt_2_ku.Text, 0.0);
            VMABT.Lu = MyList.StringToDouble(txt_2_Lu.Text, 0.0);

            txt_2_det.Text = VMABT.det.ToString("f4");
            VMABT.det_avibl = MyList.StringToDouble(txt_2_det_avibl.Text, 0.0);



            VMABT.bottom_Mj = MyList.StringToDouble(txt_2_bottom_Mj.Text, 0.0);
            VMABT.bottom_Ab = MyList.StringToDouble(txt_2_bottom_Ab.Text, 0.0);
            VMABT.bottom_n = MyList.StringToDouble(txt_2_bottom_n.Text, 0.0);
            VMABT.bottom_bc = MyList.StringToDouble(txt_2_bottom_bc.Text, 0.0);
            VMABT.bottom_D = MyList.StringToDouble(txt_2_bottom_D.Text, 0.0);
            VMABT.bottom_L = MyList.StringToDouble(txt_2_bottom_L.Text, 0.0);


            VMABT.top_Mj = MyList.StringToDouble(txt_2_top_Mj.Text, 0.0);
            VMABT.top_Ab = MyList.StringToDouble(txt_2_top_Ab.Text, 0.0);
            VMABT.top_n = MyList.StringToDouble(txt_2_top_n.Text, 0.0);
            VMABT.top_bc = MyList.StringToDouble(txt_2_top_bc.Text, 0.0);

            txt_2_ha.Text = VMABT.ha.ToString("f4");

            VMABT.h = MyList.StringToDouble(txt_2_h.Text, 0.0);

            txt_3_conc_ads_norm.Text = VMABT.conc_ads_norm.ToString("f4");
            txt_3_conc_abs_norm.Text = VMABT.conc_abs_norm.ToString("f4");

            txt_3_conc_ads_seismic.Text = VMABT.conc_ads_seismic.ToString("f4");
            txt_3_conc_abs_seismic.Text = VMABT.conc_abs_seismic.ToString("f4");


            txt_3_sigma_cb_seismic.Text = VMABT.conc_sigma_cb_seismic.ToString("f4");
            txt_3_sigma_cb_normal.Text = VMABT.conc_sigma_cb_norm.ToString("f4");


            txt_3_sigma_cbc_normal.Text = VMABT.conc_sigma_cbc_norm.ToString("f4");
            txt_3_sigma_cbc_normal.Text = VMABT.conc_sigma_cbc_seismic.ToString("f4");


            txt_3_lamda_b_norm.Text = VMABT.lamda_b_norm.ToString("f4");
            txt_3_lamda_b_seimic.Text = VMABT.lamda_b_seismic.ToString("f4");


        }
        #endregion MONO AXIAL BEARING TRANSVERSE

        private void frmBearingDesign_Load(object sender, EventArgs e)
        {
            VMABT = new POT_PTFE_VERSO_BEARING_DESIGN(iApp, eDesignBearing.VERSO_MONO_AXIAL_BEARING_TRANSVERSE);
            InVisible_Type1();
            grb_1_deg_param.Visible = true;
        }

        private void txt_1_Nmax_TextChanged(object sender, EventArgs e)
        {
            //Set_Type1_Input_Data();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Set_Type1_Input_Data();
        }


    }



}
