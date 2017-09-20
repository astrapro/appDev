using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class frm_BearingDesign : Form
    {
        //const string Title = "DESIGN OF BEARING";

        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF BEARING [BS]";
                return "DESIGN OF BEARING [IRC]";
            }
        }



        IApplication iApp = null;

        //VERSO MONO AXIAL BEARING Transverse
        POT_PTFE_VERSO_BEARING_DESIGN VMABT;
        //VERSO FIXED BEARING 
        POT_PTFE_VERSO_BEARING_DESIGN VFB;
        //VERSO BI AXIAL BEARING 
        POT_PTFE_VERSO_BEARING_DESIGN VBAB;

        //VERSO MONO AXIAL BEARING Longitudinal
        POT_PTFE_VERSO_BEARING_DESIGN VMABL;

        public frm_BearingDesign(IApplication app)
        {
            InitializeComponent();

            iApp = app;
            //user_path = iApp.LastDesignWorkingFolder;
            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);
        }

        public string user_path { get; set; }

        public string Working_Folder
        {
            get
            {
                user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

                if (Directory.Exists(user_path) == false)
                    Directory.CreateDirectory(user_path);

                return user_path;
            }
        }
        public string Worksheet_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(Working_Folder, "Worksheet_Design")) == false)
                    Directory.CreateDirectory(Path.Combine(Working_Folder, "Worksheet_Design"));
                return Path.Combine(Working_Folder, "Worksheet_Design");
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


        private void tab_1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Set Input Values ,
        /// VERSO Mono Axial Bearing Transverse
        /// </summary>
        public void Set_VMABT_Input_Data()
        {
            if (VMABT == null)
            {
                VMABT = new POT_PTFE_VERSO_BEARING_DESIGN(iApp, eDesignBearing.VERSO_MONO_AXIAL_BEARING_TRANSVERSE);
            }
            VMABT.Working_Folder = Working_Folder;
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


            VMABT.fy = MyList.StringToDouble(txt_2_fy.Text, 0.0);
            VMABT.di = MyList.StringToDouble(txt_2_di.Text, 0.0);
            VMABT.he = MyList.StringToDouble(txt_2_he.Text, 0.0);

            VMABT.IRHD = MyList.StringToDouble(txt_2_IRHD.Text, 0.0);
            VMABT.k1 = MyList.StringToDouble(txt_2_k1.Text, 0.0);
            VMABT.k2 = MyList.StringToDouble(txt_2_k2.Text, 0.0);
 

            VMABT.dp = MyList.StringToDouble(txt_2_dp.Text, 0.0);
            VMABT.w = MyList.StringToDouble(txt_2_w.Text, 0.0);
            VMABT.hm = MyList.StringToDouble(txt_2_hm.Text, 0.0);
            VMABT.An = MyList.StringToDouble(txt_2_An.Text, 0.0);
            VMABT.hts = MyList.StringToDouble(txt_2_hts.Text, 0.0);


            VMABT.Do_ = MyList.StringToDouble(txt_2_Do.Text, 0.0);
            VMABT.kb = MyList.StringToDouble(txt_2_kb.Text, 0.0);
            VMABT.do_ = MyList.StringToDouble(txt_2_do_.Text, 0.0);
            VMABT.hc = MyList.StringToDouble(txt_2_hc.Text, 0.0);
            VMABT.deb_avibl = MyList.StringToDouble(txt_2_dev_avibl.Text, 0.0);

          

            VMABT.a = MyList.StringToDouble(txt_2_a.Text, 0.0);
            VMABT.b = MyList.StringToDouble(txt_2_b.Text, 0.0);
            VMABT.ks = MyList.StringToDouble(txt_2_ks.Text, 0.0);
            VMABT.kt = MyList.StringToDouble(txt_2_kt.Text, 0.0);
            VMABT.as_ = MyList.StringToDouble(txt_2_as.Text, 0.0);
            VMABT.bs = MyList.StringToDouble(txt_2_bs.Text, 0.0);
            VMABT.ku = MyList.StringToDouble(txt_2_ku.Text, 0.0);
            VMABT.Lu = MyList.StringToDouble(txt_2_Lu.Text, 0.0);
            VMABT.det_avibl = MyList.StringToDouble(txt_2_det_avibl.Text, 0.0);
            VMABT.h = MyList.StringToDouble(txt_2_h.Text, 0.0);
 


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


            VMABT.fck = MyList.StringToDouble(txt_3_fck.Text, 0.0);
            VMABT.steel_fy = MyList.StringToDouble(txt_3_fy.Text, 0.0);
            VMABT.bottom_mu = MyList.StringToDouble(txt_3_8_bot_mu.Text, 0.0);
            VMABT.bottom_sigma_bq_perm = MyList.StringToDouble(txt_3_8_bot_sigma_bq_perm.Text, 0.0);
            VMABT.top_mu = VMABT.bottom_mu;
            VMABT.top_sigma_bq_perm = VMABT.bottom_sigma_bq_perm;

        
        }

        public void Set_VFB_Input_Data()
        {
            if (VFB == null)
            {
                VFB = new POT_PTFE_VERSO_BEARING_DESIGN(iApp, eDesignBearing.VERSO_FIXED_BEARING);
            }
            VFB.Working_Folder = Working_Folder;
            VFB.Nmax = MyList.StringToDouble(txt_VFB_1_Nmax.Text, 0.0);
            VFB.Hlatn = MyList.StringToDouble(txt_VFB_1_Hlatn.Text, 0.0);

            VFB.Nnorm = MyList.StringToDouble(txt_VFB_1_Nnorm.Text, 0.0);
            VFB.Hlts = MyList.StringToDouble(txt_VFB_1_Hlts.Text, 0.0);
            VFB.Nmin = MyList.StringToDouble(txt_VFB_1_Nmin.Text, 0.0);
            VFB.Hlng_n = MyList.StringToDouble(txt_VFB_1_Hingn.Text, 0.0);
            VFB.theta_p = MyList.StringToDouble(txt_VFB_1_theta_p.Text, 0.0);
            VFB.Hlng_s = MyList.StringToDouble(txt_VFB_1_Hings.Text, 0.0);
            VFB.theta_v = MyList.StringToDouble(txt_VFB_1_theta_v.Text, 0.0);
            VFB.H = MyList.StringToDouble(txt_VFB_1_H.Text, 0.0);
            VFB.theta = MyList.StringToDouble(txt_VFB_1_theta.Text, 0.0);
            VFB.elong = MyList.StringToDouble(txt_VFB_1_elong.Text, 0.0);
            VFB.etrans = MyList.StringToDouble(txt_VFB_1_etrans.Text, 0.0);


            VFB.fy = MyList.StringToDouble(txt_VFB_2_fy.Text, 0.0);
            VFB.di = MyList.StringToDouble(txt_VFB_2_di.Text, 0.0);
            VFB.he = MyList.StringToDouble(txt_VFB_2_he.Text, 0.0);

            VFB.IRHD = MyList.StringToDouble(txt_VFB_2_IRHD.Text, 0.0);
            VFB.k1 = MyList.StringToDouble(txt_VFB_2_k1.Text, 0.0);
            VFB.k2 = MyList.StringToDouble(txt_VFB_2_k2.Text, 0.0);


            //VFB.dp = MyList.StringToDouble(txt_VFB_2_dp.Text, 0.0);
            VFB.w = MyList.StringToDouble(txt_VFB_2_w.Text, 0.0);
            VFB.hm = MyList.StringToDouble(txt_VFB_2_hm.Text, 0.0);
            VFB.An = MyList.StringToDouble(txt_VFB_2_An.Text, 0.0);
            VFB.hts = MyList.StringToDouble(txt_VFB_2_hts.Text, 0.0);


            VFB.Do_ = MyList.StringToDouble(txt_VFB_2_Do.Text, 0.0);
            VFB.kb = MyList.StringToDouble(txt_VFB_2_kb.Text, 0.0);
            VFB.do_ = MyList.StringToDouble(txt_VFB_2_do_.Text, 0.0);
            VFB.hc = MyList.StringToDouble(txt_VFB_2_hc.Text, 0.0);
            VFB.deb_avibl = MyList.StringToDouble(txt_VFB_2_dev_avibl.Text, 0.0);



            VFB.a = MyList.StringToDouble(txt_VFB_2_a.Text, 0.0);
            VFB.b = MyList.StringToDouble(txt_VFB_2_b.Text, 0.0);
            //VFB.ks = MyList.StringToDouble(txt_VFB_2_ks.Text, 0.0);
            VFB.kt = MyList.StringToDouble(txt_VFB_2_kt.Text, 0.0);
            //VFB.as_ = MyList.StringToDouble(txt_VFB_2_as.Text, 0.0);
            //VFB.bs = MyList.StringToDouble(txt_VFB_2_bs.Text, 0.0);
            //VFB.ku = MyList.StringToDouble(txt_VFB_2_ku.Text, 0.0);
            //VFB.Lu = MyList.StringToDouble(txt_VFB_2_Lu.Text, 0.0);
            VFB.det_avibl = MyList.StringToDouble(txt_VFB_2_det_avibl.Text, 0.0);
            VFB.h = MyList.StringToDouble(txt_VFB_2_h.Text, 0.0);



            VFB.bottom_Mj = MyList.StringToDouble(txt_VFB_2_bottom_Mj.Text, 0.0);
            VFB.bottom_Ab = MyList.StringToDouble(txt_VFB_2_bottom_Ab.Text, 0.0);
            VFB.bottom_n = MyList.StringToDouble(txt_VFB_2_bottom_n.Text, 0.0);
            VFB.bottom_bc = MyList.StringToDouble(txt_VFB_2_bottom_bc.Text, 0.0);
            VFB.bottom_D = MyList.StringToDouble(txt_VFB_2_bottom_D.Text, 0.0);
            VFB.bottom_L = MyList.StringToDouble(txt_VFB_2_bottom_L.Text, 0.0);


            VFB.top_Mj = MyList.StringToDouble(txt_VFB_2_top_Mj.Text, 0.0);
            VFB.top_Ab = MyList.StringToDouble(txt_VFB_2_top_Ab.Text, 0.0);
            VFB.top_n = MyList.StringToDouble(txt_VFB_2_top_n.Text, 0.0);
            VFB.top_bc = MyList.StringToDouble(txt_VFB_2_top_bc.Text, 0.0);


            VFB.fck = MyList.StringToDouble(txt_VFB_3_fck.Text, 0.0);
            VFB.steel_fy = MyList.StringToDouble(txt_VFB_3_fy.Text, 0.0);
            VFB.bottom_mu = MyList.StringToDouble(txt_VFB_3_8_bot_mu.Text, 0.0);
            VFB.bottom_sigma_bq_perm = MyList.StringToDouble(txt_VFB_3_8_bot_sigma_bq_perm.Text, 0.0);
            VFB.top_mu = VFB.bottom_mu;
            VFB.top_sigma_bq_perm = VFB.bottom_sigma_bq_perm;
        }

        public void Set_VBAB_Input_Data()
        {

            if (VBAB == null)
            {
                VBAB = new POT_PTFE_VERSO_BEARING_DESIGN(iApp, eDesignBearing.VERSO_BI_AXIAL_BEARING);
            }
            VBAB.Working_Folder = Working_Folder;
            VBAB.Nmax = MyList.StringToDouble(txt_VBAB_1_Nmax.Text, 0.0);
            VBAB.Hlatn = MyList.StringToDouble(txt_VBAB_1_Hlatn.Text, 0.0);

            VBAB.Nnorm = MyList.StringToDouble(txt_VBAB_1_Nnorm.Text, 0.0);
            VBAB.Hlts = MyList.StringToDouble(txt_VBAB_1_Hlts.Text, 0.0);
            VBAB.Nmin = MyList.StringToDouble(txt_VBAB_1_Nmin.Text, 0.0);
            VBAB.Hlng_n = MyList.StringToDouble(txt_VBAB_1_Hingn.Text, 0.0);
            VBAB.theta_p = MyList.StringToDouble(txt_VBAB_1_theta_p.Text, 0.0);
            VBAB.Hlng_s = MyList.StringToDouble(txt_VBAB_1_Hings.Text, 0.0);
            VBAB.theta_v = MyList.StringToDouble(txt_VBAB_1_theta_v.Text, 0.0);
            VBAB.H = MyList.StringToDouble(txt_VBAB_1_H.Text, 0.0);
            VBAB.theta = MyList.StringToDouble(txt_VBAB_1_theta.Text, 0.0);
            VBAB.elong = MyList.StringToDouble(txt_VBAB_1_elong.Text, 0.0);
            VBAB.etrans = MyList.StringToDouble(txt_VBAB_1_etrans.Text, 0.0);


            VBAB.fy = MyList.StringToDouble(txt_VBAB_2_fy.Text, 0.0);
            VBAB.di = MyList.StringToDouble(txt_VBAB_2_di.Text, 0.0);
            VBAB.he = MyList.StringToDouble(txt_VBAB_2_he.Text, 0.0);

            VBAB.IRHD = MyList.StringToDouble(txt_VBAB_2_IRHD.Text, 0.0);
            VBAB.k1 = MyList.StringToDouble(txt_VBAB_2_k1.Text, 0.0);
            VBAB.k2 = MyList.StringToDouble(txt_VBAB_2_k2.Text, 0.0);


            VBAB.dp = MyList.StringToDouble(txt_VBAB_2_dp.Text, 0.0);
            VBAB.w = MyList.StringToDouble(txt_VBAB_2_w.Text, 0.0);
            VBAB.hm = MyList.StringToDouble(txt_VBAB_2_hm.Text, 0.0);
            VBAB.An = MyList.StringToDouble(txt_VBAB_2_An.Text, 0.0);
            //VBAB.hts = MyList.StringToDouble(txt_VBAB_2_hts.Text, 0.0);


            VBAB.Do_ = MyList.StringToDouble(txt_VBAB_2_Do.Text, 0.0);
            VBAB.kb = MyList.StringToDouble(txt_VBAB_2_kb.Text, 0.0);
            VBAB.do_ = MyList.StringToDouble(txt_VBAB_2_do_.Text, 0.0);
            VBAB.hc = MyList.StringToDouble(txt_VBAB_2_hc.Text, 0.0);
            VBAB.deb_avibl = MyList.StringToDouble(txt_VBAB_2_dev_avibl.Text, 0.0);



            VBAB.a = MyList.StringToDouble(txt_VBAB_2_a.Text, 0.0);
            VBAB.b = MyList.StringToDouble(txt_VBAB_2_b.Text, 0.0);
            VBAB.ks = MyList.StringToDouble(txt_VBAB_2_ks.Text, 0.0);
            VBAB.kt = MyList.StringToDouble(txt_VBAB_2_kt.Text, 0.0);
            VBAB.as_ = MyList.StringToDouble(txt_VBAB_2_as.Text, 0.0);
            VBAB.bs = MyList.StringToDouble(txt_VBAB_2_bs.Text, 0.0);
            //VBAB.ku = MyList.StringToDouble(txt_VBAB_2_ku.Text, 0.0);
            //VBAB.Lu = MyList.StringToDouble(txt_VBAB_2_Lu.Text, 0.0);
            VBAB.det_avibl = MyList.StringToDouble(txt_VBAB_2_det_avibl.Text, 0.0);
            VBAB.h = MyList.StringToDouble(txt_VBAB_2_h.Text, 0.0);



            VBAB.bottom_Mj = MyList.StringToDouble(txt_VBAB_2_bottom_Mj.Text, 0.0);
            VBAB.bottom_Ab = MyList.StringToDouble(txt_VBAB_2_bottom_Ab.Text, 0.0);
            VBAB.bottom_n = MyList.StringToDouble(txt_VBAB_2_bottom_n.Text, 0.0);
            VBAB.bottom_bc = MyList.StringToDouble(txt_VBAB_2_bottom_bc.Text, 0.0);
            VBAB.bottom_D = MyList.StringToDouble(txt_VBAB_2_bottom_D.Text, 0.0);
            VBAB.bottom_L = MyList.StringToDouble(txt_VBAB_2_bottom_L.Text, 0.0);


            VBAB.top_Mj = MyList.StringToDouble(txt_VBAB_2_top_Mj.Text, 0.0);
            VBAB.top_Ab = MyList.StringToDouble(txt_VBAB_2_top_Ab.Text, 0.0);
            VBAB.top_n = MyList.StringToDouble(txt_VBAB_2_top_n.Text, 0.0);
            VBAB.top_bc = MyList.StringToDouble(txt_VBAB_2_top_bc.Text, 0.0);


            VBAB.fck = MyList.StringToDouble(txt_VBAB_3_fck.Text, 0.0);
            VBAB.steel_fy = MyList.StringToDouble(txt_VBAB_3_fy.Text, 0.0);
            VBAB.bottom_mu = MyList.StringToDouble(txt_VBAB_3_8_bot_mu.Text, 0.0);
            VBAB.bottom_sigma_bq_perm = MyList.StringToDouble(txt_VBAB_3_8_bot_sigma_bq_perm.Text, 0.0);
            VBAB.top_mu = VBAB.bottom_mu;
            VBAB.top_sigma_bq_perm = VBAB.bottom_sigma_bq_perm;


        }

        public void Set_VMABL_Input_Data()
        {
            if (VMABL == null)
            {
                VMABL = new POT_PTFE_VERSO_BEARING_DESIGN(iApp, eDesignBearing.VERSO_MONO_AXIAL_BEARING_LONGITUDINAL);
            }
            VMABL.Working_Folder = Working_Folder;
            VMABL.Nmax = MyList.StringToDouble(txt_VMABL_1_Nmax.Text, 0.0);
            VMABL.Hlatn = MyList.StringToDouble(txt_VMABL_1_Hlatn.Text, 0.0);

            VMABL.Nnorm = MyList.StringToDouble(txt_VMABL_1_Nnorm.Text, 0.0);
            VMABL.Hlts = MyList.StringToDouble(txt_VMABL_1_Hlts.Text, 0.0);
            VMABL.Nmin = MyList.StringToDouble(txt_VMABL_1_Nmin.Text, 0.0);
            VMABL.Hlng_n = MyList.StringToDouble(txt_VMABL_1_Hingn.Text, 0.0);
            VMABL.theta_p = MyList.StringToDouble(txt_VMABL_1_theta_p.Text, 0.0);
            VMABL.Hlng_s = MyList.StringToDouble(txt_VMABL_1_Hings.Text, 0.0);
            VMABL.theta_v = MyList.StringToDouble(txt_VMABL_1_theta_v.Text, 0.0);
            VMABL.H = MyList.StringToDouble(txt_VMABL_1_H.Text, 0.0);
            VMABL.theta = MyList.StringToDouble(txt_VMABL_1_theta.Text, 0.0);
            VMABL.elong = MyList.StringToDouble(txt_VMABL_1_elong.Text, 0.0);
            VMABL.etrans = MyList.StringToDouble(txt_VMABL_1_etrans.Text, 0.0);


            VMABL.fy = MyList.StringToDouble(txt_VMABL_2_fy.Text, 0.0);
            VMABL.di = MyList.StringToDouble(txt_VMABL_2_di.Text, 0.0);
            VMABL.he = MyList.StringToDouble(txt_VMABL_2_he.Text, 0.0);

            VMABL.IRHD = MyList.StringToDouble(txt_VMABL_2_IRHD.Text, 0.0);
            VMABL.k1 = MyList.StringToDouble(txt_VMABL_2_k1.Text, 0.0);
            VMABL.k2 = MyList.StringToDouble(txt_VMABL_2_k2.Text, 0.0);


            VMABL.dp = MyList.StringToDouble(txt_VMABL_2_dp.Text, 0.0);
            VMABL.w = MyList.StringToDouble(txt_VMABL_2_w.Text, 0.0);
            VMABL.hm = MyList.StringToDouble(txt_VMABL_2_hm.Text, 0.0);
            VMABL.An = MyList.StringToDouble(txt_VMABL_2_An.Text, 0.0);
            VMABL.hts = MyList.StringToDouble(txt_VMABL_2_hts.Text, 0.0);


            VMABL.Do_ = MyList.StringToDouble(txt_VMABL_2_Do.Text, 0.0);
            VMABL.kb = MyList.StringToDouble(txt_VMABL_2_kb.Text, 0.0);
            VMABL.do_ = MyList.StringToDouble(txt_VMABL_2_do_.Text, 0.0);
            VMABL.hc = MyList.StringToDouble(txt_VMABL_2_hc.Text, 0.0);
            VMABL.deb_avibl = MyList.StringToDouble(txt_VMABL_2_dev_avibl.Text, 0.0);



            VMABL.a = MyList.StringToDouble(txt_VMABL_2_a.Text, 0.0);
            VMABL.b = MyList.StringToDouble(txt_VMABL_2_b.Text, 0.0);
            VMABL.ks = MyList.StringToDouble(txt_VMABL_2_ks.Text, 0.0);
            VMABL.kt = MyList.StringToDouble(txt_VMABL_2_kt.Text, 0.0);
            VMABL.as_ = MyList.StringToDouble(txt_VMABL_2_as.Text, 0.0);
            VMABL.bs = MyList.StringToDouble(txt_VMABL_2_bs.Text, 0.0);
            VMABL.ku = MyList.StringToDouble(txt_VMABL_2_ku.Text, 0.0);
            VMABL.Lu = MyList.StringToDouble(txt_VMABL_2_Lu.Text, 0.0);
            VMABL.det_avibl = MyList.StringToDouble(txt_VMABL_2_det_avibl.Text, 0.0);
            VMABL.h = MyList.StringToDouble(txt_VMABL_2_h.Text, 0.0);



            VMABL.bottom_Mj = MyList.StringToDouble(txt_VMABL_2_bottom_Mj.Text, 0.0);
            VMABL.bottom_Ab = MyList.StringToDouble(txt_VMABL_2_bottom_Ab.Text, 0.0);
            VMABL.bottom_n = MyList.StringToDouble(txt_VMABL_2_bottom_n.Text, 0.0);
            VMABL.bottom_bc = MyList.StringToDouble(txt_VMABL_2_bottom_bc.Text, 0.0);
            VMABL.bottom_D = MyList.StringToDouble(txt_VMABL_2_bottom_D.Text, 0.0);
            VMABL.bottom_L = MyList.StringToDouble(txt_VMABL_2_bottom_L.Text, 0.0);


            VMABL.top_Mj = MyList.StringToDouble(txt_VMABL_2_top_Mj.Text, 0.0);
            VMABL.top_Ab = MyList.StringToDouble(txt_VMABL_2_top_Ab.Text, 0.0);
            VMABL.top_n = MyList.StringToDouble(txt_VMABL_2_top_n.Text, 0.0);
            VMABL.top_bc = MyList.StringToDouble(txt_VMABL_2_top_bc.Text, 0.0);


            VMABL.fck = MyList.StringToDouble(txt_VMABL_3_fck.Text, 0.0);
            VMABL.steel_fy = MyList.StringToDouble(txt_VMABL_3_fy.Text, 0.0);
            VMABL.bottom_mu = MyList.StringToDouble(txt_VMABL_3_8_bot_mu.Text, 0.0);
            VMABL.bottom_sigma_bq_perm = MyList.StringToDouble(txt_VMABL_3_8_bot_sigma_bq_perm.Text, 0.0);
            VMABL.top_mu = VMABL.bottom_mu;
            VMABL.top_sigma_bq_perm = VMABL.bottom_sigma_bq_perm;
        }

        private void btn_process_Click(object sender, System.EventArgs e)
        {
            POT_PTFE_VERSO_BEARING_DESIGN vtmp = null;

            Button btn = sender as Button;

            if (iApp.Check_Demo_Version())
            {
                Save_FormRecord.Get_Demo_Data(this);
            }


            if (btn.Name == btn_VMABT_process.Name)
            {
                Set_VMABT_Input_Data();
                vtmp = VMABT;
            }
            else if (btn.Name == btn_VMABL_process.Name)
            {
                Set_VMABL_Input_Data();
                vtmp = VMABL;
            }
            else if (btn.Name == btn_VBAB_process.Name)
            {
                Set_VBAB_Input_Data();
                vtmp = VBAB;
            }
            else if (btn.Name == btn_VFB_process.Name)
            {
                Set_VFB_Input_Data();
                vtmp = VFB;
            }


            if (vtmp != null)
            {
                vtmp.Generate_Report();
                iApp.Save_Form_Record(this, user_path);
                MessageBox.Show(this, "Report file created in " + vtmp.Report_File, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                iApp.View_Result(vtmp.Report_File);
            }
        }

        private void btn_open_save_design_Click(object sender, System.EventArgs e)
        {
            
            Button btn = sender as Button;
            if (iApp.Check_Demo_Version())
            {
                Save_FormRecord.Get_Demo_Data(this);
            }

            POT_PTFE_VERSO_BEARING_DESIGN vtmp = null;
            if (btn.Name == btn_save_VMABT_des.Name)
            {
                Set_VMABT_Input_Data();
                vtmp = VMABT;
            }
            else if (btn.Name == btn_save_VMABL_des.Name)
            {
                Set_VMABL_Input_Data();
                vtmp = VMABL;
            }
            else if (btn.Name == btn_save_VBAB_des.Name)
            {
                Set_VBAB_Input_Data();
                vtmp = VBAB;
            }
            else if (btn.Name == btn_save_VFB_des.Name)
            {
                Set_VFB_Input_Data();
                vtmp = VFB;
            }

            if (vtmp != null)
            {
                vtmp.Generate_Report();
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.FileName = vtmp.BearingDesign.ToString();
                    sfd.Filter = "All Report Files(*.txt)|*.txt";
                    if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                    {
                        try
                        {
                            File.Copy(vtmp.Report_File, sfd.FileName, true);
                            MessageBox.Show(this, "Report file created in " + sfd.FileName, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            iApp.Save_Form_Record(this, Path.GetDirectoryName(sfd.FileName));
                            iApp.View_Result(sfd.FileName);
                        }
                        catch (Exception ex) { }
                    }
                }
            }
            else if ((btn.Name == btn_open_VMABT_des.Name) ||
                (btn.Name == btn_open_VBAB_des.Name) ||
                (btn.Name == btn_open_VFB_des.Name) ||
                (btn.Name == btn_open_VMABL_des.Name))
            {

                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "All Report Files(*.txt)|*.txt";
                    if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                    {
                        try
                        {
                            iApp.Read_Form_Record(this, Path.GetDirectoryName(ofd.FileName));
                            MessageBox.Show(this, "Previous Design opened successfully. " + ofd.FileName, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //To do
                        }
                        catch (Exception ex) { }
                    }
                }

            }
        }

        private void btn_report_Click(object sender, System.EventArgs e)
        {

            Button btn = sender as Button;
            POT_PTFE_VERSO_BEARING_DESIGN vtmp = null;

            if (btn.Name == btn_VMABT_report.Name)
                vtmp = VMABT;
            else if (btn.Name == btn_VMABL_report.Name)
                vtmp = VMABL;
            else if (btn.Name == btn_VBAB_report.Name)
                vtmp = VBAB;
            else if (btn.Name == btn_VFB_report.Name)
                vtmp = VFB;


            try
            {
                if (File.Exists(vtmp.Report_File))
                    iApp.View_Result(vtmp.Report_File);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Report file is not created.", "ASTRA");
            }
        
        }

        private void btn_VBAB_report_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (File.Exists(VBAB.Report_File))
                    iApp.View_Result(VBAB.Report_File);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Report file is not created.", "ASTRA");
            }
        
        }

        private void btn_drawings_Click(object sender, System.EventArgs e)
        {
            Button btn = sender as Button;

            POT_PTFE_VERSO_BEARING_DESIGN ptvb = null;
            if (btn.Name == btn_VMABT_drawings.Name)
            {
                if (VMABT == null)
                    VMABT = new POT_PTFE_VERSO_BEARING_DESIGN(iApp, eDesignBearing.VERSO_MONO_AXIAL_BEARING_TRANSVERSE);
                ptvb = VMABT;
            }
            else if (btn.Name == btn_VMABL_drawings.Name)
            {
                if (VMABL == null)
                    VMABL = new POT_PTFE_VERSO_BEARING_DESIGN(iApp, eDesignBearing.VERSO_MONO_AXIAL_BEARING_LONGITUDINAL);
                ptvb = VMABL;
            }
            else if (btn.Name == btn_VBAB_drawings.Name)
            {
                if (VBAB == null)
                    VBAB = new POT_PTFE_VERSO_BEARING_DESIGN(iApp, eDesignBearing.VERSO_BI_AXIAL_BEARING);
             
                ptvb = VBAB;
            }
            else if (btn.Name == btn_VFB_drawings.Name)
            {
                if (VFB == null)
                    VFB = new POT_PTFE_VERSO_BEARING_DESIGN(iApp, eDesignBearing.VERSO_FIXED_BEARING);
                ptvb = VFB;
            }

            string draw_cmd = ptvb.BearingDesign.ToString();
            ptvb.Working_Folder = Working_Folder;

            iApp.RunViewer(Path.Combine(ptvb.Drawing_Folder, "DRAWINGS"), draw_cmd);
        }

        private void btn_bearing_worksheet_Click(object sender, System.EventArgs e)
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

        private void frm_BearingDesign_Load(object sender, System.EventArgs e)
        {
            //Chiranjit [2013 03 30]
            Save_FormRecord.Set_Demo_Data(this);
            iApp.Check_Demo_Version();


            #region Chiranjit Design Option

            try
            {
                eDesignOption edp = iApp.Get_Design_Option(Title);
                if (edp == eDesignOption.None)
                {
                    this.Close();
                }
                else if (edp == eDesignOption.Open_Design)
                {
                    user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                    iApp.Read_Form_Record(this, user_path);


                    if (iApp.IsDemo)
                        MessageBox.Show("ASTRA USB Dongle not found at any port....\nOpening with default data......", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Chiranjit Design Option



        }

        private void btn_open_worksheet_Click(object sender, System.EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
        }


    }
}
//Chiranjit [2013 03 30]
//Set Demo / Pofessional Version