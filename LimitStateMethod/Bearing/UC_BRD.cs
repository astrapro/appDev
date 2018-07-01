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
using BridgeAnalysisDesign.BearingDesign;

using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;



namespace LimitStateMethod.Bearing
{
    public partial class UC_BRD : UserControl
    {
        //const string Title = "DESIGN OF BEARING";

        public string Title
        {
            get
            {
                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    return "DESIGN OF BEARING [BS]";
                //return "DESIGN OF BEARING [IRC]";
                return "Design of Bearings";
            }
        }


        public event EventHandler OnButtonClick;

        public IApplication iApp = null;

        //VERSO MONO AXIAL BEARING Transverse
        POT_PTFE_VERSO_BEARING_DESIGN VMABT;
        //VERSO FIXED BEARING 
        POT_PTFE_VERSO_BEARING_DESIGN VFB;
        //VERSO BI AXIAL BEARING 
        POT_PTFE_VERSO_BEARING_DESIGN VBAB;

        //VERSO MONO AXIAL BEARING Longitudinal
        POT_PTFE_VERSO_BEARING_DESIGN VMABL;

        public UC_BRD(IApplication app)
        {
            InitializeComponent();

            iApp = app;
            //user_path = iApp.LastDesignWorkingFolder;
            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            this.Text = Title + " : " + MyList.Get_Modified_Path(user_path);

        }
        public UC_BRD()
        {
            InitializeComponent();
            //Load_Default_Data();
        }

        bool _frc = true;



        #region Elastomeric Bearing Properties
        public string Length
        {
            get
            {
                return txt_Introduction_G23.Text;
            }
            set
            {
                txt_Introduction_G23.Text = value;
            }
        }
        public string Girder_Length
        {
            get
            {
                return txt_Introduction_G24.Text;
            }
            set
            {
                txt_Introduction_G24.Text = value;
            }
        }
        public string Effective_Length
        {
            get
            {
                return txt_Introduction_G25.Text;
            }
            set
            {
                txt_Introduction_G25.Text = value;
            }
        }
        public string Overall_Span
        {
            get
            {
                return txt_b_L_case1_J9.Text;
            }
            set
            {
                txt_b_L_case1_J9.Text = value;
            }
        }
        public string Bearings_Span
        {
            get
            {
                return txt_b_L_case1_J10.Text;
            }
            set
            {
                txt_b_L_case1_J10.Text = value;
            }
        }
        public string Bearings_Trans
        {
            get
            {
                return txt_b_L_case1_J11.Text;
            }
            set
            {
                txt_b_L_case1_J11.Text = value;
            }
        }
        public string Bearings_Nos
        {
            get
            {
                return txt_b_L_case1_J12.Text;
            }
            set
            {
                txt_b_L_case1_J12.Text = value;
            }
        }

//        txt_Introduction_G23 = Length

//txt_Introduction_G24 = Girder Length

//txt_Introduction_G25 = Effective Length


//txt_b_L_case1_J9  = Overall span

//txt_b_L_case1_J10 = The c/c of Bearings for the span   


//txt_b_L_case1_J11  =  The c.c of the bearings (Transverse direction)


//txt_b_L_case1_J12 = No. of bearings per span 


        #endregion Elastomeric Bearing Properties
        public bool Show_Forces
        {
            set
            {
                _frc = value;

                if (_frc) tbc1.TabPages.Insert(0, tab_FRCS);
                else tbc1.TabPages.Remove(tab_FRCS);
                    
            }
            get
            {
                return _frc;
            }
        }
        public void Load_Default_Data()
        {
            #region Reactions

            dgv_reactions.Rows.Clear();


            List<string> list = new List<string>();
            //list.Add(string.Format("Tabulation of Reactions,,,,,,,,"));
            //list.Add(string.Format(""));
            list.Add(string.Format("MAXIMUM DL SIDL AND  LL VALUES,,"));
            list.Add(string.Format(""));
            list.Add(string.Format("Total DL + SIDL,100.0,kN"));
            list.Add(string.Format(",,"));
            list.Add(string.Format("Total LL Load (Max),446.9,kN"));
            list.Add(string.Format("Total LL Load (Min),254.0,kN"));
            list.Add(string.Format(",,"));
            list.Add(string.Format("Total Load (DL+ SIDL + Max LL) ,546.9,kN"));
            list.Add(string.Format("Total Load (DL+ SIDL + Min LL) ,354.0,kN"));
            list.Add(string.Format(",,"));
            list.Add(string.Format("MAXIMUM ROATION VALUES,,"));
            list.Add(string.Format(",,"));
            list.Add(string.Format("X-Rotation (DL+SIDL) (Max),-0.000091,rad"));
            list.Add(string.Format("Z-Rotation (DL+SIDL)(Max),-0.000265,rad"));
            list.Add(string.Format(",,"));
            list.Add(string.Format(",,"));
            list.Add(string.Format("X-Rotation (LL)(Max),-0.000091,rad"));
            list.Add(string.Format("Z-Rotation (LL)(Max),-0.000265,rad"));
            list.Add(string.Format(",,"));
            list.Add(string.Format(",,"));
            list.Add(string.Format("X-Rotation (Centrifugal Force)(Max),-0.000091,rad"));
            list.Add(string.Format("Z-Rotation (Centrifugal Force)(Max),-0.000265,rad"));
            list.Add(string.Format(",,"));
            list.Add(string.Format(",,"));
            list.Add(string.Format("MINIMUM ROATION VALUES,,"));
            list.Add(string.Format(",,"));
            list.Add(string.Format("X-Rotation (DL+SIDL) (Min),-0.000091,rad"));
            list.Add(string.Format("Z-Rotation (DL+SIDL)(Min),-0.000265,rad"));
            list.Add(string.Format(",,"));
            list.Add(string.Format(",,"));
            list.Add(string.Format("X-Rotation (LL)(Min),-0.000091,rad"));
            list.Add(string.Format("Z-Rotation (LL)(Min),-0.000265,rad"));
            list.Add(string.Format(",,"));
            list.Add(string.Format(",,"));
            list.Add(string.Format("X-Rotation (Centrifugal Force)(Min),-0.000091,rad"));
            list.Add(string.Format("Z-Rotation (Centrifugal Force)(Min),-0.000265,rad"));
            #endregion Reactions

            MyList mlist;
            for (int i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                dgv_reactions.Rows.Add(mlist.StringList.ToArray());
            }

            //for (int i = 0; i < dgv_reactions.Rows.Count; i++)
            //{
            //    for (int j = 0; j < dgv_reactions.Columns.Count; j++)
            //    {
            //        try
            //        {
            //            if (dgv_reactions[j, i].Value == null)
            //                dgv_reactions[j, i].Value = "";
            //        }
            //        catch (Exception ex) { }
            //    }
            //}

            #region Format Input Data
            DataGridView dgv = dgv_reactions;


            string s1 = "";
            string s2 = "";
            string s3 = "";
            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {
                    if (dgv[0, i].Value == "") dgv[0, i].Value = "";
                    if (dgv[1, i].Value == "") dgv[1, i].Value = "";
                    if (dgv[2, i].Value == "") dgv[2, i].Value = "";
                    //if (dgv_box_input_data[3, i].Value == "") dgv_box_input_data[3, i].Value = "";



                    s1 = dgv[0, i].Value.ToString();
                    s2 = dgv[1, i].Value.ToString();
                    s3 = dgv[2, i].Value.ToString();
                    //s4 = dgv_box_input_data[3, i].Value.ToString();
                    if (s1 != "" && s2 == "")
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        dgv.Rows[i].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.0f, FontStyle.Bold);
                    }
                    else
                    {
                        //if (s2 != "") dgv[0, i].Value = sl_no++;
                    }
                }
                catch (Exception exx) { }
            }

            #endregion Format Input Data

        }


        public string user_path { get; set; }

        public string Working_Folder
        {
            get
            {
                //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                if (iApp.user_path == "")
                    user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                else
                    user_path = Path.Combine(iApp.user_path, Title);

                if (Directory.Exists(user_path) == false)
                    Directory.CreateDirectory(user_path);

                return user_path;
            }
        }
        public string Worksheet_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(Working_Folder, "Elastomeric Bearing")) == false)
                    Directory.CreateDirectory(Path.Combine(Working_Folder, "Elastomeric Bearing"));
                return Path.Combine(Working_Folder, "Elastomeric Bearing");
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

            if (OnButtonClick != null) OnButtonClick(sender, e);


            POT_PTFE_VERSO_BEARING_DESIGN vtmp = null;

            Button btn = sender as Button;

            if (iApp.Check_Demo_Version())
            {
                //Save_FormRecord.Get_Demo_Data(this);
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
                //iApp.Save_Form_Record(this, user_path);
                MessageBox.Show(this, "Report file created in " + vtmp.Report_File, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                iApp.View_Result(vtmp.Report_File);
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


        string Get_Design_Report()
        {

            return Path.Combine(Worksheet_Folder, "Elastomeric_Bearing_Design.xls");

        }
        private void btn_bearing_worksheet_Click(object sender, System.EventArgs e)
        {
            string excel_path = Path.Combine(Application.StartupPath, @"DESIGN\Bearing Design");


            string excel_file = "Bridge_Bearing_Design.xls";


            if(iApp.DesignStandard == eDesignStandard.BritishStandard)
                excel_file = "Bridge_Bearing_Design_BS.xls";

                
                excel_file = Path.Combine(excel_path, excel_file);

            //try
            //{
            //    string excel_path = Path.Combine(Application.StartupPath, @"DESIGN\Bearing Design");
            //    string excel_file = "Bridge_Bearing_Design.xls";
            //    excel_file = Path.Combine(excel_path, excel_file);
            //    if (File.Exists(excel_file))
            //    {
            //        iApp.OpenExcelFile(Worksheet_Folder, excel_file, "2011ap");
            //    }
            //}
            //catch (Exception ex) { }

            string excel_file_name = excel_file;
            string copy_path = excel_file;

            if (!File.Exists(excel_file_name))
            {
                MessageBox.Show("Excel Program Module not found in Application folder.\n\n" + excel_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            try
            {

                //copy_path = Path.Combine(Worksheet_Folder, "Elastomeric_Bearing_Design.xls");
                copy_path = Get_Design_Report();

                File.Copy(excel_file_name, copy_path, true);
                Bearing_Excel_Update rcc_excel = new Bearing_Excel_Update();
                rcc_excel.Excel_File_Name = copy_path;


                #region User Inputs

                rcc_excel.Introduction_G23 = MyList.StringToDouble(txt_Introduction_G23.Text, 0.0);
                rcc_excel.Introduction_G24 = MyList.StringToDouble(txt_Introduction_G24.Text, 0.0);
                rcc_excel.Introduction_G25 = MyList.StringToDouble(txt_Introduction_G25.Text, 0.0);


                rcc_excel.spring_constant_G6 = MyList.StringToDouble(txt_spring_constant_G6.Text, 0.0);
                rcc_excel.spring_constant_I6 = MyList.StringToDouble(txt_spring_constant_I6.Text, 0.0);
                rcc_excel.spring_constant_G8 = MyList.StringToDouble(txt_spring_constant_G8.Text, 0.0);
                rcc_excel.spring_constant_G10 = MyList.StringToDouble(txt_spring_constant_G10.Text, 0.0);
                rcc_excel.spring_constant_G11 = MyList.StringToDouble(txt_spring_constant_G11.Text, 0.0);
                rcc_excel.spring_constant_G13 = MyList.StringToDouble(txt_spring_constant_G13.Text, 0.0);


                rcc_excel.DGV = dgv_reactions;


                rcc_excel.b_L_case1_J9 = MyList.StringToDouble(txt_b_L_case1_J9.Text, 0.0);
                rcc_excel.b_L_case1_J10 = MyList.StringToDouble(txt_b_L_case1_J10.Text, 0.0);
                rcc_excel.b_L_case1_J11 = MyList.StringToDouble(txt_b_L_case1_J11.Text, 0.0);
                rcc_excel.b_L_case1_J12 = MyList.StringToDouble(txt_b_L_case1_J12.Text, 0.0);



                #endregion User Inputs



                //rcc_excel.Report_File_Name = File_DeckSlab_Results;

                //Deck_Analysis.Live_Load_List = LoadData.GetLiveLoads(Deck_Analysis.LiveLoad_File);
                //rcc_excel.llc = Deck_Analysis.Live_Load_List;
                //rcc_excel.Deckslab_User_Inputs.Read_From_Grid(dgv_deck_user_input);
                //rcc_excel.Deckslab_Design_Inputs.Read_From_Grid(dgv_deck_design_input);
                //rcc_excel.Deckslab_User_Live_loads.Read_From_Grid(dgv_deck_user_live_loads);

                //rcc_excel.Deck_Loads = Deck_LL_Loads;
                iApp.Excel_Open_Message();

                rcc_excel.Read_Update_Data();

                iApp.Excel_Close_Message();

            }
            catch (Exception ex) { }
            //Button_Enable_Disable();
            return;


        }

        private void frm_BearingDesign_Load(object sender, System.EventArgs e)
        {
            //Chiranjit [2013 03 30]
            //Save_FormRecord.Set_Demo_Data(this);
            iApp.Check_Demo_Version();


            #region Chiranjit Design Option

            try
            {
                eDesignOption edp = iApp.Get_Design_Option(Title);
                if (edp == eDesignOption.None)
                {
                    //this.Close();
                }
                else if (edp == eDesignOption.Open_Design)
                {
                    user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                    //iApp.Read_Form_Record(this, user_path);


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
            Button btn = sender as Button;

            if (btn == btn_open_worksheet)
            {
                iApp.Open_ASTRA_Worksheet_Dialog();
            }
            else if (btn == btn_open_worksheet_report)
            {
                string excel_file = "Bridge_Bearing_Design.xls";



                //if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                //    excel_file = "Bridge_Bearing_Design_BS.xls";



                excel_file = Get_Design_Report();



                if (File.Exists(excel_file))
                {
                    iApp.OpenExcelFile(excel_file, "2011ap");
                }
                else
                {
                    MessageBox.Show(excel_file + " file not found.");
                    return;
                }
            }
        }



        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.maurer.co.uk/doc/MAURER_Elastomeric_Bearings.pdf");
        }
        public Reaction_Table Get_Reaction_Table()
        {
            Reaction_Table rt = new Reaction_Table();






            return rt;

        }

        public void Set_Forces(double vert_ld, double dl, double dl_rot, double ll_rot, double trans_ld, double long_ld)
        {


            //MONO AXIAL BRNG. (TRANS.)


            //MONO AXIAL BRNG. (LONG.)


            //BI AXIAL BRNG .


            //FIXED BRNG.


            double H = trans_ld > long_ld ? trans_ld : long_ld;

            #region MONO AXIAL BRNG. (TRANS.)
            txt_1_Nnorm.Text = vert_ld.ToString("f2");
            txt_1_Nmax.Text = (vert_ld * 0.25).ToString("f2");

            txt_1_Nmin.Text = dl.ToString("f2");

            txt_1_theta_p.Text = dl_rot.ToString();
            txt_1_theta_v.Text = ll_rot.ToString();
            txt_1_theta.Text = (dl_rot + ll_rot).ToString();

            txt_1_Hlatn.Text = (trans_ld).ToString("f2");

            txt_1_Hlts.Text = (trans_ld * 0.25).ToString("f2");

            txt_1_Hingn.Text = (long_ld).ToString("f2");

            txt_1_Hings.Text = (long_ld * 0.25).ToString("f2");





            txt_1_H.Text = (H).ToString("f2");



            #endregion MONO AXIAL BRNG. (TRANS.)



            #region MONO AXIAL BRNG. (LONG.)



            txt_VMABL_1_Nnorm.Text = vert_ld.ToString("f2");
            txt_VMABL_1_Nmax.Text = (vert_ld * 0.25).ToString("f2");


            txt_VMABL_1_Nmin.Text = dl.ToString("f2");

            txt_VMABL_1_theta_p.Text = dl_rot.ToString();
            txt_VMABL_1_theta_v.Text = ll_rot.ToString();
            txt_VMABL_1_theta.Text = (dl_rot + ll_rot).ToString();


            txt_VMABL_1_Hlatn.Text = (trans_ld).ToString("f2");
            txt_VMABL_1_Hlts.Text = (trans_ld * 0.25).ToString("f2");

            txt_VMABL_1_Hingn.Text = (long_ld).ToString("f2");

            txt_VMABL_1_Hings.Text = (long_ld * 0.25).ToString("f2");


            txt_VMABL_1_H.Text = (H).ToString("f2");

            #endregion MONO AXIAL BRNG. (LONG.)


            #region BI AXIAL BRNG

            txt_VBAB_1_Nnorm.Text = vert_ld.ToString("f2");
            txt_VBAB_1_Nmax.Text = (vert_ld * 0.25).ToString("f2");


            txt_VBAB_1_Nmin.Text = dl.ToString("f2");

            txt_VBAB_1_theta_p.Text = dl_rot.ToString();
            txt_VBAB_1_theta_v.Text = ll_rot.ToString();
            txt_VBAB_1_theta.Text = (dl_rot + ll_rot).ToString();

            txt_VBAB_1_Hlatn.Text = (trans_ld).ToString("f2");

            txt_VBAB_1_Hlts.Text = (trans_ld * 0.25).ToString("f2");


            txt_VBAB_1_Hingn.Text = (long_ld).ToString("f2");

            txt_VBAB_1_Hings.Text = (long_ld * 0.25).ToString("f2");

            txt_VBAB_1_H.Text = (H).ToString("f2");


            #endregion BI AXIAL BRNG


            #region FIXED BRNG.



            txt_VFB_1_Nnorm.Text = vert_ld.ToString("f2");
            txt_VFB_1_Nmax.Text = (vert_ld * 0.25).ToString("f2");


            txt_VFB_1_Nmin.Text = dl.ToString("f2");

            txt_VFB_1_theta_p.Text = dl_rot.ToString();
            txt_VFB_1_theta_v.Text = ll_rot.ToString();
            txt_VFB_1_theta.Text = (dl_rot + ll_rot).ToString();



            txt_VFB_1_Hlatn.Text = (trans_ld).ToString("f2");

            txt_VFB_1_Hlts.Text = (trans_ld * 0.25).ToString("f2");

            txt_VFB_1_Hingn.Text = (long_ld).ToString("f2");

            txt_VFB_1_Hings.Text = (long_ld * 0.25).ToString("f2");

            txt_VFB_1_H.Text = (H).ToString("f2");


            #endregion FIXED BRNG.


            return;

            #region Set Forces
            txt_1_Nmax.Text = "";
            txt_1_Nnorm.Text = "";



            txt_1_Nmin.Text = dl.ToString();

            txt_1_Hlatn.Text = "";

            txt_1_Hlts.Text = "";

            txt_1_Hingn.Text = "";

            txt_1_Hings.Text = "";

            #endregion Set Forces






            txt_VMABL_1_Nmax.Text = "";

            txt_VMABL_1_Nnorm.Text = "";


            txt_VMABL_1_Nmin.Text = "";

            txt_VMABL_1_Hlatn.Text = "";


            txt_VMABL_1_Hlts.Text = "";

            txt_VMABL_1_Hingn.Text = "";

            txt_VMABL_1_Hings.Text = "";



            txt_VBAB_1_Nmax.Text = "";

            txt_VBAB_1_Nnorm.Text = "";

            txt_VBAB_1_Nmin.Text = "";

            txt_VBAB_1_Hlatn.Text = "";

            txt_VBAB_1_Hlts.Text = "";


            txt_VBAB_1_Hingn.Text = "";

            txt_VBAB_1_Hings.Text = "";






            txt_VFB_1_Nmax.Text = "";

            txt_VFB_1_Nnorm.Text = "";

            txt_VFB_1_Nmin.Text = "";

            txt_VFB_1_Hlatn.Text = "";

            txt_VFB_1_Hlts.Text = "";

            txt_VFB_1_Hingn.Text = "";

            txt_VFB_1_Hings.Text = "";


        }

        public void Read_Reactions(string file_name)
        {
            List<string> list = new List<string>(File.ReadAllLines(file_name));
            MyList mlist;
            string kStr = "";

            int flag = 0;
            mlist = new MyList("",' ');
            dgv_rotation.Rows.Clear();
            dgv_forces.Rows.Clear();
            for(int i = 0; i < list.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(list[i].Trim());
                if (kStr.StartsWith("------")) continue;
                if (kStr == "") continue;
                if (kStr.Contains("TRANSLATION"))
                {
                    flag = 1; continue;
                }
                if (kStr.Contains("MAXIMUM FORCES"))
                {
                    flag = 2; continue;
                }
                mlist = new MyList(kStr, ' ');
                string dl_name = "";
                double tx,ty,tz,rx,ry,rz;
                if(flag == 1)
                {
                    try
                    {
                        dl_name = mlist.GetString(0, mlist.Count - 6 - 1);


                        rz = mlist.GetDouble(mlist.Count - 1);
                        ry = mlist.GetDouble(mlist.Count - 2);
                        rx = mlist.GetDouble(mlist.Count - 3);
                        tz = mlist.GetDouble(mlist.Count - 4);
                        ty = mlist.GetDouble(mlist.Count - 5);
                        tx = mlist.GetDouble(mlist.Count - 6);
                        dgv_rotation.Rows.Add(dl_name, tx.ToString("E5"), ty.ToString("E5"), tz.ToString("E5"), rx.ToString("F8"), ry.ToString("F8"), rz.ToString("F8"));
                    }
                    catch (Exception exx) { }
                } 
                else if (flag == 2)
                {
                    try
                    {
                        dl_name = mlist.GetString(0, mlist.Count - 5 - 1);

                        Reaction_Data rdt = new Reaction_Data(dl_name);


                        rdt.Z_Rotation = mlist.GetDouble(mlist.Count - 1);
                        rdt.X_Rotation = mlist.GetDouble(mlist.Count - 2);
                        rdt.Long_Force = mlist.GetDouble(mlist.Count - 3);
                        rdt.Transverse_Force = mlist.GetDouble(mlist.Count - 4);
                        rdt.Vertival_Load = mlist.GetDouble(mlist.Count - 5);






                        dgv_forces.Rows.Add(dl_name,
                            rdt.Vertival_Load,
                            rdt.Transverse_Force,
                            rdt.Long_Force,
                            rdt.X_Rotation,
                            rdt.Z_Rotation
                            
                            );
                    }
                    catch (Exception exx) { }
                }
            }

        }

        private void dgv_forces_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dgv_forces.RowCount; i++)
            {
                if (dgv_forces[0, i].Value != null)
                {
                    if (dgv_forces[0, i].Value.ToString().ToUpper().StartsWith("TOTAL"))
                    {
                        dgv_forces.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    if (dgv_forces[0, i].Value.ToString().ToUpper().StartsWith("MAX") ||
                        dgv_forces[0, i].Value.ToString().ToUpper().StartsWith("MIN"))
                    {
                        dgv_forces.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
            }

            //return;
            double sidl1 = MyList.StringToDouble(dgv_forces[2, 3].Value.ToString(), 0.0);
            double sidl2 = MyList.StringToDouble(dgv_forces[3, 3].Value.ToString(), 0.0);



            double vert_ld = MyList.StringToDouble(dgv_forces[1, dgv_forces.RowCount - 2].Value.ToString(), 0.0);
            double dl = MyList.StringToDouble(dgv_forces[1, 3].Value.ToString(), 0.0);
            double dl_rot = MyList.StringToDouble(dgv_forces[4, 3].Value.ToString(), 0.0);
            double ll_rot = MyList.StringToDouble(dgv_forces[4, dgv_forces.RowCount - 2].Value.ToString(), 0.0);

            double xrot = 0.0;
            double zrot = 0.0;

            double trans_ld = MyList.StringToDouble(dgv_forces[2, dgv_forces.RowCount - 2].Value.ToString(), 0.0);
            double long_ld = MyList.StringToDouble(dgv_forces[3, dgv_forces.RowCount - 2].Value.ToString(), 0.0);

            vert_ld = vert_ld * 10.0;
            dl = dl * 10.0;
            trans_ld = trans_ld * 10.0;
            long_ld = long_ld * 10.0;

            Set_Forces(vert_ld, dl, dl_rot, ll_rot, trans_ld, long_ld);




        }
    }
    public class Bearing_Excel_Update
    {
        public string Excel_File_Name { get; set; }
        public string Report_File_Name { get; set; }
        public Excel_User_Inputs Deckslab_User_Inputs { get; set; }
        public Excel_User_Inputs Deckslab_User_Live_loads { get; set; }
        public Excel_User_Inputs Deckslab_Design_Inputs { get; set; }

        public List<LoadData> llc = null;


        public double Introduction_G23 { get; set; }
        public double Introduction_G24 { get; set; }
        public double Introduction_G25 { get; set; }

        public double spring_constant_G6 { get; set; }
        public double spring_constant_I6 { get; set; }
        public double spring_constant_G8 { get; set; }
        public double spring_constant_G10 { get; set; }
        public double spring_constant_G11 { get; set; }
        public double spring_constant_G13 { get; set; }
        public double b_L_case1_J9 { get; set; }
        public double b_L_case1_J10 { get; set; }
        public double b_L_case1_J11 { get; set; }
        public double b_L_case1_J12 { get; set; }


        public DataGridView DGV { get; set; }
        public Bearing_Excel_Update()
        {
            Excel_File_Name = "";
            Report_File_Name = "";

            Deckslab_User_Inputs = new Excel_User_Inputs();
            Deckslab_User_Live_loads = new Excel_User_Inputs();
            Deckslab_Design_Inputs = new Excel_User_Inputs();



            Introduction_G23 = 15.52;
            Introduction_G24 = 15.50;
            Introduction_G25 = 14.70;

            spring_constant_G6 = 500.0;
            spring_constant_I6 = 320.0;
            spring_constant_G8 = 12.0;
            spring_constant_G10 = 4.0;
            spring_constant_G11 = 4.0;
            spring_constant_G13 = 6.0;
            b_L_case1_J9 = 11.50;
            b_L_case1_J10 = 11.0;
            b_L_case1_J11 = 3.0;
            b_L_case1_J12 = 8.0;



        }

        public void Read_Update_Data()
        {
            //if (!File.Exists(Report_File_Name)) return;

            List<string> list = new List<string>();



            //List<string> list = new List<string>(File.ReadAllLines(Report_File_Name));
            MyList mlist = null;



            #region Update_ExcelData

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            String fileName = Excel_File_Name; // set this to your file you want

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(fileName, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["Introduction"];



            String cellFormulaAsString = myExcelWorksheet.get_Range("G23", misValue).Formula.ToString(); // this puts the formula in Cell A2 or text depending whats in it in the string.

            //int cel_index = 30;
            //int cel_index = 32;
            int cel_index = 26;

            int ci = (int)('B');


            char c = (char)ci;


            int i = 0;
            #region User Inputs

            myExcelWorksheet.get_Range("G23", misValue).Formula = Introduction_G23;
            myExcelWorksheet.get_Range("G24", misValue).Formula = Introduction_G24;
            myExcelWorksheet.get_Range("G25", misValue).Formula = Introduction_G25;




            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["spring constant"];


            

            myExcelWorksheet.get_Range("G6", misValue).Formula = spring_constant_G6;
            myExcelWorksheet.get_Range("I6", misValue).Formula = spring_constant_I6;
            myExcelWorksheet.get_Range("G8", misValue).Formula = spring_constant_G8;
            myExcelWorksheet.get_Range("G10", misValue).Formula = spring_constant_G10;
            myExcelWorksheet.get_Range("G11", misValue).Formula = spring_constant_G11;
            myExcelWorksheet.get_Range("G13", misValue).Formula = spring_constant_G13;


            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["b_L_case1"];

            myExcelWorksheet.get_Range("J9", misValue).Formula = b_L_case1_J9;
            myExcelWorksheet.get_Range("J10", misValue).Formula = b_L_case1_J10;
            myExcelWorksheet.get_Range("J11", misValue).Formula = b_L_case1_J11;
            myExcelWorksheet.get_Range("J12", misValue).Formula = b_L_case1_J12;



            #region Reactions
            //list.Add(string.Format("Tabulation of Reactions,,,,,,,,"));
            list.Add(string.Format("Joint Nos.,66,89,138,161,210,233,282,305"));
            list.Add(string.Format(",,,,,,,,"));
            list.Add(string.Format("Dead Load,672.6,373.3,242.5,299.1,299.0,242.5,373.3,672.6"));
            list.Add(string.Format("Xrot,-0.000061,0.000760,-0.000363,0.000551,-0.000551,0.000363,-0.000760,0.000061"));
            list.Add(string.Format("Zrot,-0.000617,0.000670,-0.000356,0.000501,-0.000501,0.000356,-0.000670,0.000617"));
            list.Add(string.Format(",,,,,,,,"));
            list.Add(string.Format("Superimposed DL,40.8,17.7,15.2,16.3,16.3,15.2,17.7,40.8"));
            list.Add(string.Format("Xrot,-0.000005,0.000047,-0.000023,0.000033,-0.000033,0.000023,-0.000047,0.000005"));
            list.Add(string.Format("Zrot,-0.000038,0.000042,-0.000023,0.000032,-0.000032,0.000023,-0.000042,0.000038"));
            list.Add(string.Format(",,,,,,,,"));
            list.Add(string.Format("Centrifugal force,-22.9,-11.6,-31.9,-2.2,15.9,30.1,4.6,18.0"));
            list.Add(string.Format("Xrot,-0.000004,-0.000021,-0.000025,-0.000044,-0.000018,-0.000005,-0.000011,0.000002"));
            list.Add(string.Format("Zrot,0.000012,-0.000028,-0.000030,-0.000012,-0.000017,-0.000023,-0.000019,0.000013"));
            list.Add(string.Format(",,,,,,,,"));
            list.Add(string.Format("Total DL + SDL,690.6,379.3,225.8,313.2,331.3,287.8,395.6,731.4"));
            list.Add(string.Format(",,,,,,,,"));
            list.Add(string.Format("Live Load Without Impact,,,,,,,,"));
            list.Add(string.Format("3 lanes of Class A (centre of carriage way) - LC 73,136.9,1.8,124.8,32.9,72.5,87.6,840.0,203.2"));
            list.Add(string.Format("Xrot,-0.000068,0.000048,-0.000084,0.000057,-0.000147,0.000066,-0.000746,0.000031"));
            list.Add(string.Format("Zrot,-0.000083,0.000062,-0.000100,0.000079,-0.000160,0.000099,0.000238,0.000150"));
            list.Add(string.Format(",,,,,,,,"));
            list.Add(string.Format("Three lanes of Class A,106.2,-2.5,144.0,29.8,91.3,102.3,792.2,236.6"));
            list.Add(string.Format("Xrot,-0.000081,0.000029,-0.000106,0.000042,-0.000160,0.000054,-0.000768,0.000030"));
            list.Add(string.Format("Zrot,-0.000069,0.000044,-0.000092,0.000067,-0.000192,0.000103,0.000177,0.000172"));
            list.Add(string.Format(",,,,,,,,"));
            list.Add(string.Format("Three lanes of Class A,171.3,5.6,137.2,38.6,73.1,96.3,830.0,147.7"));
            list.Add(string.Format("Xrot,-0.000057,0.000068,-0.000071,0.000071,-0.000115,0.000071,-0.000707,0.000054"));
            list.Add(string.Format("Zrot,-0.000100,0.000082,-0.000094,0.000094,-0.000139,0.000100,0.000273,0.000122"));
            list.Add(string.Format(",,,,,,,,"));
            list.Add(string.Format(" 70R wheeled+ Class A,132.8,111.2,192.6,227.1,71.1,467.9,-3.4,354.6"));
            list.Add(string.Format("Xrot,-0.000120,0.000004,-0.000052,0.000018,-0.000038,-0.000006,-0.000018,0.000134"));
            list.Add(string.Format("Zrot,-0.000073,0.000061,-0.000108,0.000096,-0.000081,0.000083,-0.000053,-0.000021"));
            list.Add(string.Format(",,,,,,,,"));
            list.Add(string.Format(" 70R Tracked + Class A,446.9,65.9,206.3,243.0,195.0,188.7,55.3,98.9"));
            list.Add(string.Format("Xrot,-0.000091,0.000213,-0.000064,0.000226,-0.000058,0.000211,0.000016,0.000128"));
            list.Add(string.Format("Zrot,-0.000265,0.000309,-0.000158,0.000277,-0.000036,0.000152,-0.000095,0.000101"));
            #endregion Reactions


            myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["forces"];

            Excel_User_Inputs es = new Excel_User_Inputs();
            es.Read_From_Grid(DGV);

            cel_index = 3;
            i = 0;
            for (cel_index = 3; cel_index < 36; cel_index++)
            {

                if (cel_index == 4 || cel_index == 7
                     || (cel_index >= 10 && cel_index <= 12)
                     || (cel_index >= 15 && cel_index <= 16)
                     || (cel_index >= 19 && cel_index <= 20)
                     || (cel_index >= 19 && cel_index <= 20)
                     || (cel_index >= 23 && cel_index <= 26)
                     || (cel_index >= 29 && cel_index <= 30)
                     || (cel_index >= 33 && cel_index <= 34)

                    ) continue;

                myExcelWorksheet.get_Range("B" + cel_index, misValue).Formula = es[i++].Input_Value;
               
            }




            #endregion User Inputs





            try
            {
                myExcelWorkbook.Save();
                //myExcelWorkbook.Close(true, fileName, null);
                Marshal.ReleaseComObject(myExcelWorkbook);
            }
            catch (Exception ex) { }
            #endregion Update_ExcelData
        }
    }


    public class Reaction_Data
    {
        public string Title { get; set; }
        public double Vertival_Load { get; set; }
        public double Transverse_Force { get; set; }
        public double Long_Force { get; set; }
        public double X_Rotation { get; set; }
        public double Z_Rotation { get; set; }


        public Reaction_Data(string title)
        {
            Title = title;
            Vertival_Load = 0.0;
            Transverse_Force = 0.0;
            Long_Force = 0.0;
            X_Rotation = 0.0;
            Z_Rotation = 0.0;
        }
    }

    public class Reaction_Table: List<Reaction_Data>
    {
        public List<double> Joint_Nos { get; set; }
        public Reaction_Data DL { get; set; }
        public Reaction_Data SIDL { get; set; }
        public Reaction_Data CF { get; set; }

        public Reaction_Data LL_1 { get; set; }


        public Reaction_Table()
            : base()
        {
            DL = new Reaction_Data("Dead Load");
            SIDL = new Reaction_Data("Superimposed Dead Load");
            CF = new Reaction_Data("Centrifugal Force");


            LL_1 = new Reaction_Data("3 lanes of Class A (centre of carriage way) - LC 73");
            //LL_2 = new Reaction_Data("Three lanes of Class A");
            //LL_3 = new Reaction_Data("Three lanes of Class A");
            //LL_4 = new Reaction_Data("70R wheeled+ Class A");
            //LL_5 = new Reaction_Data("70R Tracked + Class A");

            Add(DL);
            Add(SIDL);
            Add(CF);
            Add(LL_1);
            //Add(LL_2);
            //Add(LL_2);
        }
    }

}
