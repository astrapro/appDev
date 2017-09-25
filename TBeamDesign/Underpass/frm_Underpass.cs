using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using AstraFunctionOne.BridgeDesign;
using AstraFunctionOne.BridgeDesign.SteelTruss;
//using AstraFunctionOne.BridgeDesign.SteelTrussTables;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraInterface.TrussBridge;

namespace BridgeAnalysisDesign.Underpass
{
    public partial class frm_Underpass : Form
    {
        //const string Title = "ANALYSIS OF UNDERPASS";
        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF UNDERPASS [BS]";
                return "DESIGN OF UNDERPASS [IRC]";
            }
        }

        IApplication iApp = null;
        RccAbutment Abut = null;
        TopRCCSlab top_slab = null;
        RccBoxStructure box = null;

        public frm_Underpass(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            this.Text = Title + " : " + MyList.Get_Modified_Path(iApp.LastDesignWorkingFolder);

            iApp.LastDesignWorkingFolder = System.IO.Path.Combine(iApp.LastDesignWorkingFolder, Title);

            if (!System.IO.Directory.Exists(iApp.LastDesignWorkingFolder))
                System.IO.Directory.CreateDirectory(iApp.LastDesignWorkingFolder);

            Abut = new RccAbutment(iApp);
            top_slab = new TopRCCSlab(iApp);
            box = new RccBoxStructure(iApp);
        }
        public string Worksheet_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(iApp.LastDesignWorkingFolder, "Worksheet_Design")) == false)
                    Directory.CreateDirectory(Path.Combine(iApp.LastDesignWorkingFolder, "Worksheet_Design"));
                return Path.Combine(iApp.LastDesignWorkingFolder, "Worksheet_Design");
            }
        }
        public string Drawing_Folder
        {
            get
            {
                if (Directory.Exists(Path.Combine(iApp.LastDesignWorkingFolder, "DRAWINGS")) == false)
                    Directory.CreateDirectory(Path.Combine(iApp.LastDesignWorkingFolder, "DRAWINGS"));
                return Path.Combine(iApp.LastDesignWorkingFolder, "DRAWINGS");
            }
        }
        #region RCC Abutment Methods
        public void Abut_Read_User_Input()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(box.user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";

            #endregion
            int indx = -1;
            try
            {
                for (int i = 0; i < lst_content.Count; i++)
                {
                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    #region SWITCH
                    switch (VarName)
                    {
                        case "d1":
                            Abut.d1 = mList.GetDouble(1);
                            txt_abut_d1.Text = Abut.d1.ToString();
                            break;
                        case "t":
                            Abut.t = mList.GetDouble(1);
                            txt_abut_t.Text = Abut.t.ToString();
                            break;
                        case "H":
                            Abut.H = mList.GetDouble(1);
                            txt_abut_H.Text = Abut.H.ToString();
                            break;
                        case "a":
                            Abut.a = mList.GetDouble(1);
                            txt_abut_a.Text = Abut.a.ToString();
                            break;
                        case "gamma_b":
                            Abut.gamma_b = mList.GetDouble(1);
                            txt_abut_gamma_b.Text = Abut.gamma_b.ToString();
                            break;
                        case "gamma_c":
                            Abut.gamma_c = mList.GetDouble(1);
                            txt_abut_gamma_c.Text = Abut.gamma_c.ToString();
                            break;
                        case "phi":
                            Abut.phi = mList.GetDouble(1);
                            txt_abut_phi.Text = Abut.phi.ToString();
                            break;
                        case "p":
                            Abut.p = mList.GetDouble(1);
                            txt_abut_p_bearing_capacity.Text = Abut.p.ToString();
                            break;
                        case "f_ck":
                            Abut.f_ck = mList.GetDouble(1);
                            txt_abut_concrete_grade.Text = Abut.f_ck.ToString();
                            break;

                        case "f_y":
                            Abut.f_y = mList.GetDouble(1);
                            txt_abut_steel_grade.Text = Abut.f_y.ToString();
                            break;

                        case "w6":
                            Abut.w6 = mList.GetDouble(1);
                            txt_abut_w6.Text = Abut.w6.ToString();
                            break;

                        case "w5":
                            Abut.w5 = mList.GetDouble(1);
                            txt_abut_w5.Text = Abut.w5.ToString();
                            break;

                        case "F":
                            Abut.F = mList.GetDouble(1);
                            txt_abut_F.Text = Abut.F.ToString();
                            break;

                        case "d2":
                            Abut.d2 = mList.GetDouble(1);
                            txt_abut_d2.Text = Abut.d2.ToString();
                            break;

                        case "d3":
                            Abut.d3 = mList.GetDouble(1);
                            txt_abut_d3.Text = Abut.d3.ToString();
                            break;

                        case "B":
                            Abut.B = mList.GetDouble(1);
                            txt_abut_B.Text = Abut.B.ToString();
                            break;


                        case "theta":
                            Abut.theta = mList.GetDouble(1);
                            txt_abut_theta.Text = Abut.theta.ToString();
                            break;

                        case "delta":
                            Abut.delta = mList.GetDouble(1);
                            txt_abut_delta.Text = Abut.delta.ToString();
                            break;

                        case "z":
                            Abut.z = mList.GetDouble(1);
                            txt_abut_z.Text = Abut.z.ToString();
                            break;

                        case "mu":
                            Abut.mu = mList.GetDouble(1);
                            txt_abut_mu.Text = Abut.mu.ToString();
                            break;

                        case "L1":
                            Abut.L1 = mList.GetDouble(1);
                            txt_abut_L1.Text = Abut.L1.ToString();
                            break;

                        case "L2":
                            Abut.L2 = mList.GetDouble(1);
                            txt_abut_L2.Text = Abut.L2.ToString();
                            break;

                        case "L3":
                            Abut.L3 = mList.GetDouble(1);
                            txt_abut_L3.Text = Abut.L3.ToString();
                            break;

                        case "L4":
                            Abut.L4 = mList.GetDouble(1);
                            txt_abut_L4.Text = Abut.L4.ToString();
                            break;

                        case "h1":
                            Abut.h1 = mList.GetDouble(1);
                            txt_abut_h1.Text = Abut.h1.ToString();
                            break;

                        case "L":
                            Abut.L = mList.GetDouble(1);
                            txt_abut_L.Text = Abut.L.ToString();
                            break;
                        case "d4":
                            Abut.d4 = mList.GetDouble(1);
                            txt_abut_d4.Text = Abut.d4.ToString();
                            break;

                        case "cover":
                            Abut.cover = mList.GetDouble(1);
                            txt_abut_cover.Text = Abut.cover.ToString();
                            break;

                        case "factor":
                            Abut.factor = mList.GetDouble(1);
                            txt_abut_fact.Text = Abut.factor.ToString();
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
        }
        private void Abut_InitializeData()
        {
            #region Variables Initialize with default data

            Abut.d1 = MyList.StringToDouble(txt_abut_d1.Text, 0.0);
            Abut.t = MyList.StringToDouble(txt_abut_t.Text, 0.0);
            Abut.H = MyList.StringToDouble(txt_abut_H.Text, 0.0);
            Abut.a = MyList.StringToDouble(txt_abut_a.Text, 0.0);
            Abut.gamma_b = MyList.StringToDouble(txt_abut_gamma_b.Text, 0.0);
            Abut.gamma_c = MyList.StringToDouble(txt_abut_gamma_c.Text, 0.0);
            Abut.phi = MyList.StringToDouble(txt_abut_phi.Text, 0.0);
            Abut.p = MyList.StringToDouble(txt_abut_p_bearing_capacity.Text, 0.0);
            Abut.f_ck = MyList.StringToDouble(txt_abut_concrete_grade.Text, 0.0);
            Abut.f_y = MyList.StringToDouble(txt_abut_steel_grade.Text, 0.0);
            Abut.w6 = MyList.StringToDouble(txt_abut_w6.Text, 0.0);
            Abut.w5 = MyList.StringToDouble(txt_abut_w5.Text, 0.0);
            Abut.F = MyList.StringToDouble(txt_abut_F.Text, 0.0);
            Abut.d2 = MyList.StringToDouble(txt_abut_d2.Text, 0.0);
            Abut.d3 = MyList.StringToDouble(txt_abut_d3.Text, 0.0);
            Abut.B = MyList.StringToDouble(txt_abut_B.Text, 0.0);
            Abut.theta = MyList.StringToDouble(txt_abut_theta.Text, 0.0);
            Abut.delta = MyList.StringToDouble(txt_abut_delta.Text, 0.0);
            Abut.z = MyList.StringToDouble(txt_abut_z.Text, 0.0);
            Abut.mu = MyList.StringToDouble(txt_abut_mu.Text, 0.0);
            Abut.L1 = MyList.StringToDouble(txt_abut_L1.Text, 0.0);
            Abut.L2 = MyList.StringToDouble(txt_abut_L2.Text, 0.0);
            Abut.L3 = MyList.StringToDouble(txt_abut_L3.Text, 0.0);
            Abut.L4 = MyList.StringToDouble(txt_abut_L4.Text, 0.0);
            Abut.h1 = MyList.StringToDouble(txt_abut_h1.Text, 0.0);
            Abut.L = MyList.StringToDouble(txt_abut_L.Text, 0.0);
            Abut.d4 = MyList.StringToDouble(txt_abut_d4.Text, 0.0);
            Abut.cover = MyList.StringToDouble(txt_abut_cover.Text, 0.0);
            Abut.factor = MyList.StringToDouble(txt_abut_fact.Text, 0.0);

            #endregion
        }
        #endregion RCC Abutment Methods

        #region RCC Abutment Form Events
        private void btn_Abut_Report_Click(object sender, EventArgs e)
        {
            iApp.View_Result(Abut.rep_file_name);
        }
        private void btn_Abut_Process_Click(object sender, EventArgs e)
        {
            Abut_InitializeData();
            Abut.FilePath = iApp.LastDesignWorkingFolder;
            Abut.Write_User_input();
            Abut.Calculate_Program(Abut.rep_file_name);
            Abut.Write_Drawing_File();
            MessageBox.Show(this, "Report file written in " + Abut.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Abut.is_process = true;
            Button_Enable_Disable();
        }
        #endregion RCC Abutment Form Events

        #region Top Slab Methods
        public void Top_Slab_Initialize_InputData()
        {
            top_slab.D = MyList.StringToDouble(txt_top_slab_D.Text, 0.0);
            top_slab.CW = MyList.StringToDouble(txt_top_slab_CW.Text, 0.0);
            top_slab.FP = MyList.StringToDouble(txt_top_slab_FP.Text, 0.0);
            top_slab.L = MyList.StringToDouble(txt_top_slab_L.Text, 0.0);
            top_slab.WC = MyList.StringToDouble(txt_top_slab_WC.Text, 0.0);
            top_slab.support_width = MyList.StringToDouble(txt_top_slab_width_support.Text, 0.0);
            top_slab.conc_grade = MyList.StringToDouble(txt_top_slab_concrete_grade.Text, 0.0);
            //top_slab.conc_grade = (CONCRETE_GRADE)(int)conc_grade;
            top_slab.st_grade = MyList.StringToDouble(txt_top_slab_steel_grade.Text, 0.0);
            top_slab.sigma_cb = MyList.StringToDouble(txt_top_slab_sigma_cb.Text, 0.0);
            top_slab.sigma_st = MyList.StringToDouble(txt_top_slab_sigma_st.Text, 0.0);
            top_slab.m = MyList.StringToDouble(txt_top_slab_m.Text, 0.0);
            top_slab.j = MyList.StringToDouble(txt_top_slab_j.Text, 0.0);
            top_slab.Q = MyList.StringToDouble(txt_top_slab_Q.Text, 0.0);
            top_slab.a1 = MyList.StringToDouble(txt_top_slab_a1.Text, 0.0);
            top_slab.b1 = MyList.StringToDouble(txt_top_slab_b1.Text, 0.0);
            top_slab.b2 = MyList.StringToDouble(txt_top_slab_b2.Text, 0.0);
            top_slab.W1 = MyList.StringToDouble(txt_top_slab_W1.Text, 0.0);
            top_slab.cover = MyList.StringToDouble(txt_top_slab_cover.Text, 0.0);
            top_slab.delta_c = MyList.StringToDouble(txt_top_slab_delta_c.Text, 0.0);
            top_slab.delta_wc = MyList.StringToDouble(txt_top_slab_delta_wc.Text, 0.0);
        }
        public void Top_Slab_Read_User_Input()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(box.user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";

            #endregion
            int indx = -1;
            try
            {
                for (int i = 0; i < lst_content.Count; i++)
                {
                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);

                    #region SWITCH
                    switch (VarName)
                    {
                        case "D":
                            top_slab.D = mList.GetDouble(1);
                            txt_top_slab_D.Text = top_slab.D.ToString();
                            break;
                        case "CW":
                            top_slab.CW = mList.GetDouble(1);
                            txt_top_slab_CW.Text = top_slab.CW.ToString();
                            break;
                        case "FP":
                            top_slab.FP = mList.GetDouble(1);
                            txt_top_slab_FP.Text = top_slab.FP.ToString();
                            break;
                        case "L":
                            top_slab.L = mList.GetDouble(1);
                            txt_top_slab_L.Text = top_slab.L.ToString();
                            break;
                        case "support_width":
                            top_slab.support_width = mList.GetDouble(1);
                            txt_top_slab_width_support.Text = top_slab.support_width.ToString();
                            break;
                        case "W":
                            top_slab.W1 = mList.GetDouble(1);
                            txt_top_slab_W1.Text = top_slab.W1.ToString();
                            break;
                        case "conc_grade":
                            top_slab.conc_grade = mList.GetDouble(1);
                            txt_top_slab_concrete_grade.Text = top_slab.conc_grade.ToString();
                            break;
                        case "st_grade":
                            top_slab.st_grade = mList.GetDouble(1);
                            txt_top_slab_steel_grade.Text = top_slab.st_grade.ToString();
                            break;
                        case "sigma_cb":
                            top_slab.sigma_cb = mList.GetDouble(1);
                            txt_top_slab_sigma_cb.Text = top_slab.sigma_cb.ToString();
                            break;
                        case "sigma_st":
                            top_slab.sigma_st = mList.GetDouble(1);
                            txt_top_slab_sigma_st.Text = top_slab.sigma_st.ToString();
                            break;

                        case "m":
                            top_slab.m = mList.GetDouble(1);
                            txt_top_slab_m.Text = top_slab.m.ToString();
                            break;
                        case "j":
                            top_slab.j = mList.GetDouble(1);
                            txt_top_slab_j.Text = top_slab.j.ToString();
                            break;
                        case "Q":
                            top_slab.Q = mList.GetDouble(1);
                            txt_top_slab_Q.Text = top_slab.Q.ToString();
                            break;
                        case "a1":
                            top_slab.a1 = mList.GetDouble(1);
                            txt_top_slab_a1.Text = top_slab.a1.ToString();
                            break;
                        case "b1":
                            top_slab.b1 = mList.GetDouble(1);
                            txt_top_slab_b1.Text = top_slab.b1.ToString();
                            break;
                        case "W1":
                            top_slab.W1 = mList.GetDouble(1);
                            txt_top_slab_W1.Text = top_slab.W1.ToString();
                            break;
                        case "cover":
                            top_slab.cover = mList.GetDouble(1);
                            txt_top_slab_cover.Text = top_slab.cover.ToString();
                            break;
                        case "delta_c":
                            top_slab.delta_c = mList.GetDouble(1);
                            txt_top_slab_delta_c.Text = top_slab.delta_c.ToString();
                            break;
                        case "delta_wc":
                            top_slab.delta_wc = mList.GetDouble(1);
                            txt_top_slab_delta_wc.Text = top_slab.delta_wc.ToString();
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
        }
        #endregion Top Slab Methods

        #region Top Slab Form Events
        private void btn_Top_Slab_Process_Click(object sender, EventArgs e)
        {
            Top_Slab_Initialize_InputData();
            top_slab.FilePath = iApp.LastDesignWorkingFolder;
            top_slab.WriteUserInput();
            top_slab.Calculate_Program(top_slab.rep_file_name);
            top_slab.Write_Drawing_File();
            MessageBox.Show(this, "Report file written in " + top_slab.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            top_slab.is_process = true;
            Button_Enable_Disable();
        }
        private void btn_Top_Slab_Report_Click(object sender, EventArgs e)
        {
            iApp.View_Result(top_slab.rep_file_name);
        }
        #endregion Top Slab Form Events


        #region Box Structure Methods
        public void Box_Read_From_File(string fileName)
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(fileName));
            string kStr = "";
            MyList mList = null;
            string VarName = "";

            #endregion
            int indx = -1;
            try
            {
                for (int i = 0; i < lst_content.Count; i++)
                {
                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    #region SWITCH
                    switch (VarName)
                    {
                        case "H":
                            box.H = mList.GetDouble(1);
                            txt_H.Text = box.H.ToString();
                            break;
                        case "b":
                            box.b = mList.GetDouble(1);
                            txt_b.Text = box.b.ToString();
                            break;
                        case "d":
                            box.d = mList.GetDouble(1);
                            txt_d.Text = box.d.ToString();
                            break;
                        case "d1":
                            box.d1 = mList.GetDouble(1);
                            txt_d1.Text = box.d1.ToString();
                            break;
                        case "d2":
                            box.d2 = mList.GetDouble(1);
                            txt_d2.Text = box.d2.ToString();
                            break;
                        case "d3":
                            box.d3 = mList.GetDouble(1);
                            txt_d3.Text = box.d3.ToString();
                            break;
                        case "gamma_b":
                            box.gamma_b = mList.GetDouble(1);
                            txt_gamma_b.Text = box.gamma_b.ToString();
                            break;
                        case "gamma_c":
                            box.gamma_c = mList.GetDouble(1);
                            txt_gamma_c.Text = box.gamma_c.ToString();
                            break;
                        case "R":
                            box.R = mList.GetDouble(1);
                            txt_R.Text = box.R.ToString();
                            break;
                        case "t":
                            box.t = mList.GetDouble(1);
                            txt_t.Text = box.t.ToString();
                            break;
                        case "j":
                            box.j = mList.GetDouble(1);
                            txt_j.Text = box.j.ToString();
                            break;
                        case "cover":
                            box.cover = mList.GetDouble(1);
                            txt_cover.Text = box.cover.ToString();
                            break;
                        case "sigma_c":
                            box.sigma_c = mList.GetDouble(1);
                            txt_sigma_c.Text = box.sigma_c.ToString();
                            break;

                        case "sigma_st":
                            box.sigma_st = mList.GetDouble(1);
                            txt_sigma_st.Text = box.sigma_st.ToString();
                            break;
                        case "b1":
                            box.b1 = mList.GetDouble(1);
                            txt_b1.Text = box.b1.ToString();
                            break;
                        case "b2":
                            box.b2 = mList.GetDouble(1);
                            txt_b2.Text = box.b2.ToString();
                            break;
                        case "a1":
                            box.a1 = mList.GetDouble(1);
                            txt_a1.Text = box.a1.ToString();
                            break;
                        case "w1":
                            box.w1 = mList.GetDouble(1);
                            txt_w1.Text = box.w1.ToString();
                            break;
                        case "w2":
                            box.w2 = mList.GetDouble(1);
                            txt_w2.Text = box.w2.ToString();
                            break;
                        case "b3":
                            box.b3 = mList.GetDouble(1);
                            txt_b3.Text = box.b3.ToString();
                            break;
                        case "F":
                            box.F = mList.GetDouble(1);
                            txt_F.Text = box.F.ToString();
                            break;
                        case "S":
                            box.S = mList.GetDouble(1);
                            txt_S.Text = box.S.ToString();
                            break;
                        case "sbc":
                            box.sbc = mList.GetDouble(1);
                            txt_sbc.Text = box.sbc.ToString();
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
        }
        public void Box_Initialize_InputData()
        {
            #region Variable Initialization

            box.H = MyList.StringToDouble(txt_H.Text, 0.0);
            box.b = MyList.StringToDouble(txt_b.Text, 0.0);
            box.d = MyList.StringToDouble(txt_d.Text, 0.0);
            box.d1 = MyList.StringToDouble(txt_d1.Text, 0.0);
            box.d2 = MyList.StringToDouble(txt_d2.Text, 0.0);
            box.d3 = MyList.StringToDouble(txt_d3.Text, 0.0);
            box.gamma_b = MyList.StringToDouble(txt_gamma_b.Text, 0.0);
            box.gamma_c = MyList.StringToDouble(txt_gamma_c.Text, 0.0);
            box.R = MyList.StringToDouble(txt_R.Text, 0.0);
            box.t = MyList.StringToDouble(txt_t.Text, 0.0);
            box.j = MyList.StringToDouble(txt_j.Text, 0.0);
            box.sigma_st = MyList.StringToDouble(txt_sigma_st.Text, 0.0);
            box.sigma_c = MyList.StringToDouble(txt_sigma_c.Text, 0.0);
            box.cover = MyList.StringToDouble(txt_cover.Text, 0.0);
            box.b1 = MyList.StringToDouble(txt_b1.Text, 0.0);
            box.b2 = MyList.StringToDouble(txt_b2.Text, 0.0);
            box.a1 = MyList.StringToDouble(txt_a1.Text, 0.0);
            box.w1 = MyList.StringToDouble(txt_w1.Text, 0.0);
            box.w2 = MyList.StringToDouble(txt_w2.Text, 0.0);
            box.b3 = MyList.StringToDouble(txt_b3.Text, 0.0);
            box.F = MyList.StringToDouble(txt_F.Text, 0.0);
            box.S = MyList.StringToDouble(txt_S.Text, 0.0);
            box.sbc = MyList.StringToDouble(txt_sbc.Text, 0.0);
            int grade = (int)box.sigma_c;

            box.Con_Grade = (CONCRETE_GRADE)grade;

            //switch (grade)
            //{
            //    case 15 :
            //        St_Grade = TAU_C.STEEL_GRADE.M15;
            //            break;
            //    case 20 :
            //        St_Grade = TAU_C.STEEL_GRADE.M20;
            //            break;
            //    case 25 :
            //        St_Grade = TAU_C.STEEL_GRADE.M25;
            //            break;
            //    case 30 :
            //        St_Grade = TAU_C.STEEL_GRADE.M30;
            //            break;
            //    case 35 :
            //        St_Grade = TAU_C.STEEL_GRADE.M35;
            //            break;
            //    case 40 :
            //        St_Grade = TAU_C.STEEL_GRADE.M40;
            //            break;
            //}

            #endregion

        }
        #endregion Box Structure Methods

        #region Box Structure Form Events
        private void btn_Box_Report_Click(object sender, EventArgs e)
        {
            iApp.View_Result(box.rep_file_name);
        }
        private void btn_Box_Process_Click(object sender, EventArgs e)
        {
            Box_Initialize_InputData();
            box.FilePath = iApp.LastDesignWorkingFolder;
            box.WriteUserInput();
            box.Calculate_Program(box.rep_file_name);
            btn_box_Report.Enabled = File.Exists(box.rep_file_name);
            MessageBox.Show(this, "Report file written in " + box.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(box.rep_file_name);
            box.is_process = true;
            Button_Enable_Disable();
        }
        #endregion Box Structure Form Events

        private void frm_Underpass_Load(object sender, EventArgs e)
        {
            pic_pedestrian.BackgroundImage = AstraFunctionOne.ImageCollection.Box_Culvert;
            pic_veh_top_slab.BackgroundImage = AstraFunctionOne.ImageCollection.SLAB_Culvert;
            pic_veh_rcc_abut.BackgroundImage = AstraFunctionOne.ImageCollection.Abutment;

            Button_Enable_Disable();
        }
         
        private void btn_dwg_slab_Click(object sender, EventArgs e)
        {

            Button b = sender as Button;

            if (b.Name == btn_dwg_slab.Name)
            {

                iApp.SetDrawingFile_Path(top_slab.drawing_path, "Slab_Culvert_Interactive", "");
            }
            else if (b.Name == btn_dwg_pedestrian.Name)
            {

                if (listBox1.SelectedIndex == -1)
                {
                    MessageBox.Show(this, "Select a Drawing item from the list.", "ASTRA", MessageBoxButtons.OK);
                    return;
                }

                string drawing_command, User_Drawing_Folder;
                drawing_command = User_Drawing_Folder = "";

                switch (listBox1.SelectedIndex)
                {
                    case 0:
                        drawing_command = "Box_Culvert_Single_Cell";
                        User_Drawing_Folder = "Box_Culvert_Single_Cell";
                        break;

                    case 1:
                        drawing_command = "Box_Culvert_Double_Cell";
                        User_Drawing_Folder = "Box_Culvert_Double_Cell";
                        break;

                    case 2:
                        drawing_command = "Box_Culvert_Tripple_Cell";
                        User_Drawing_Folder = "Box_Culvert_Tripple_Cell";
                        break;
                }
                User_Drawing_Folder = Path.Combine(Drawing_Folder, User_Drawing_Folder);
                iApp.RunViewer(User_Drawing_Folder, drawing_command);

                //iApp.SetDrawingFile_Path(box.drawing_path, "Box_Culvert", "");
            }
            else if (b.Name == btn_dwg_abut.Name)
            {
                iApp.SetDrawingFile_Path(Abut.drawing_path, "Abutment_Reinforcements", "");
                //iApp.OpenDefaultDrawings(Drawing_Folder, "Abutment_Reinforcements", "");
            }
        }

        private void Button_Enable_Disable()
        {
            btn_top_slab_report.Enabled = File.Exists(top_slab.rep_file_name);
            btn_dwg_slab.Enabled = File.Exists(top_slab.drawing_path);

            btnReport.Enabled = File.Exists(Abut.rep_file_name);
            btn_dwg_abut.Enabled = File.Exists(Abut.rep_file_name);

            btn_box_Report.Enabled = File.Exists(box.rep_file_name);
            btn_dwg_pedestrian.Enabled = File.Exists(box.drawing_path);
        }

    }
    public class RccAbutment
    {
        #region Variable Declaration
        public string rep_file_name = "";
        public string file_path = "";
        public string user_path = "";
        public string drawing_path = "";
        public string system_path = "";
        public string user_input_file = "";
        public bool is_process = false;



        public double d1, t, H, a, gamma_b, gamma_c, phi, p, f_ck, f_y, w6, w5, F, d2, d3, d4, B;
        public double theta, delta, z, mu, L1, L2, L3, L4, h1, L, cover, factor;



        #endregion

        #region Drawing Variable

        double _bd1, _bd2, _bd3, _bd4, _bd5, _bd6, _bd7;
        double _sp1, _sp2, _sp3, _sp4, _sp5, _sp6, _sp7;
        #endregion

        IApplication iApp = null;
        public RccAbutment(IApplication app)
        {
            this.iApp = app;
            _bd1 = 0.0;
            _bd2 = 0.0;
            _bd3 = 0.0;
            _bd4 = 0.0;
            _bd5 = 0.0;
            _bd6 = 0.0;
            _bd7 = 0.0;

            _sp1 = 0.0;
            _sp2 = 0.0;
            _sp3 = 0.0;
            _sp4 = 0.0;
            _sp5 = 0.0;
            _sp6 = 0.0;
            _sp7 = 0.0;
        }
        public void Calculate_Program(string file_name)
        {

            string ref_string = "";

            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            #region TechSOFT Banner
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t*            ASTRA Pro Release 21             *");
            sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t*           DESIGN OF RCC ABUTMENT            *");
            sw.WriteLine("\t\t*          FOR  VEHICULAR UNDERPASS           *");
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t----------------------------------------------");
            sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            sw.WriteLine("\t\t----------------------------------------------");

            #endregion

            try
            {
                //sw.WriteLine("DESIGN OF RCC ABUTMENT");
                //sw.WriteLine("----------------------");
                sw.WriteLine();
                sw.WriteLine();

                #region USER Data
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Depth of Girder Seat = {0} m. = d1", d1.ToString("0.00"));
                sw.WriteLine("Thickness of wall = {0} m = t", t);
                sw.WriteLine("Height of Retained Earth = {0} m = H", H);
                sw.WriteLine("Width of wall = {0} m = B", B);
                sw.WriteLine("Equivalent height of Earth for Live Load Surcharge = {0} m = d2", d2);
                sw.WriteLine("Thickness of Approach Slab = {0} m = d3", d3);
                sw.WriteLine("Length of base in front of wall = {0} m = L1", L1);
                sw.WriteLine("Length of base in wall location = {0} m = L2", L2);
                sw.WriteLine("Length of base at back of wall  = {0} m = L3", L3);
                sw.WriteLine("Thickness of wall at the Top = {0} m = L4", L4);
                sw.WriteLine("Thickness of Base = {0:f2} m = d4", d4);
                sw.WriteLine("Angle between wall and Horizontal base on Earth side = {0}° = θ", theta);
                sw.WriteLine("Inclination of Earth fill side with the Horizontal = {0}° = δ", delta);
                sw.WriteLine("Angle of friction between Earth and Wall = {0}° = z", z);
                sw.WriteLine("Coefficient of friction between Earth and wall = {0} = µ", mu);
                sw.WriteLine("Unit weight of Back fill Earth = {0} kN/cu.m = γ_b", gamma_b);
                sw.WriteLine("Unit weight of Concrete = {0} kN/cu.m = γ_c", gamma_c);
                sw.WriteLine("Angle of Internal friction of backfill = {0}° = φ", phi);
                sw.WriteLine("Bearing Capacity = {0} kN/sq.m = p", p);
                sw.WriteLine("Concrete Grade = M {0:f0} = f_ck = {0:f0}", f_ck);
                sw.WriteLine("Steel Grade = Fe {0:f0} = f_y = {0:f0}", f_y);
                sw.WriteLine("Live Load from vehicles = {0:f2} kN/m = w6", w6);
                sw.WriteLine("Permanent Load from Super Structure = {0:f2} kN/m = w5", w5);
                sw.WriteLine("Braking Force = {0:f2} kN = F", F);
                sw.WriteLine("Bending Moment and Shear Force Factor = {0:f2} = Fact", factor);
                sw.WriteLine("Clear Cover = {0:f2} mm = cover", cover);
                #endregion

                double rad = Math.PI / 180;

                phi = phi * rad;
                delta = delta * rad;
                z *= rad;
                theta *= rad;



                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                #region STEP 1
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Approximate Sizing (dimensions)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Using Rankine's formula for the depth of foundation base");
                sw.WriteLine();

                double hd = (p / gamma_b) * Math.Pow(((1 - Math.Sin(phi)) / (1 + Math.Sin(phi))), 2.0);
                sw.WriteLine("hd = (p/γ_b) * ((1 - Sin φ)/(1 + Sin φ))^2");
                sw.WriteLine("   = ({0}/{1}) * ((1 - Sin {2})/(1 + Sin {2}))^2",
                    p,
                    gamma_b,
                    phi / rad);

                if (hd < a)
                    sw.WriteLine("   = {0} m < a = {1} m , OK", hd.ToString("0.000"), a.ToString("0.000"));
                else
                    sw.WriteLine("   = {0} m > a = {1} m, NOT OK", hd.ToString("0.000"), a.ToString("0.000"));

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Assuming Thickness of base to be 10% of total height {0} m", H);

                double d = d4;
                sw.WriteLine();
                sw.WriteLine("Let us Provide thickness of base = d = {0} cm = {1} m",
                    d * 100,
                    d);

                sw.WriteLine();
                sw.WriteLine("Length of base = l = H √((Ca * Cos δ)/((1 - m) * (1 + 3 * m)))"); //Ca = ?
                //TO DO

                sw.WriteLine();
                sw.WriteLine("Angle of Back fill is horizontal, δ = {0}, m = 1 - (4/(9*q))", delta);
                sw.WriteLine();

                double q = (gamma_b / p) * (H - d);
                sw.WriteLine("where q = γ*h/p = {0}/{1} * (H - d) = {0}/{1} * ({2} - {3}) = {4}",
                    gamma_b,
                    p,
                    H,
                    d,
                    q.ToString("0.000"));
                double Ca = 0.0;
                Ca = (1 - Math.Sin(phi)) / (1 + Math.Sin(phi));
                sw.WriteLine("     Ca = (1 - Sin φ)/(1 + Sin φ)) ");
                sw.WriteLine("        = (1 - Sin {0:f0})/(1 + Sin {0:f0})", phi / rad);
                sw.WriteLine("        = {0:f3} ", Ca);
                sw.WriteLine();

                double m = 1 - (4 / (9 * q));
                sw.WriteLine("      m = 1 - (4/(9*{0:f3}))", q);
                sw.WriteLine("        = {0} ", m.ToString("0.00"));


                double l = 0.0;
                l = H * (Math.Sqrt(((Ca * Math.Cos(delta)) / ((1 - m) * (1 + 3 * m)))));
                //sw.WriteLine("Length of base = l = H √((Ca * Cos δ)/(1 - m) * (1 + 3 * m))");
                sw.WriteLine("      l = {0} √(({1} * Cos {2})/(1 - {3}) * (1 + 3 * {3}))",
                    H.ToString("0.000"),
                    Ca.ToString("0.000"),
                    delta / rad,
                    m.ToString("0.000"));
                sw.WriteLine("        = {0:f2} ", l);
                double provided_l = L1 + L2 + L3;
                sw.WriteLine();
                sw.WriteLine("Provided l = L1 + L2 + L3 = {0} + {1} + {2} = {3} m",
                    L1, L2, L3, provided_l);

                double ml = m * provided_l;
                sw.WriteLine();
                sw.WriteLine("        ml = {0:f2} * {1:f2} = {2:f2}",
                    m,
                    provided_l,
                    ml);
                double adopting_average_thickness = t; //m 100 cm
                sw.WriteLine();
                sw.WriteLine("Adopting average thickness of wall = {0:f2} cm = {1:f2} m",
                    t * 100,
                    t);

                sw.WriteLine();
                sw.WriteLine();

                #endregion

                #region STEP 2

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Stability Check");
                sw.WriteLine("         Weight of wall");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double w1 = (H - d1 - d4) * (L2 - L4) * gamma_c;
                sw.WriteLine("   w1 = (H - d1 - d4) * (L2 - L4) * γ_c");
                sw.WriteLine("      = ({0} - {1} - {2}) * ({3} - {4}) * {5}",
                    H,
                    d1,
                    d4,
                    L2,
                    L4,
                    gamma_c);
                sw.WriteLine("      = {0:f3} kN", w1);

                sw.WriteLine();
                double D1 = ((L2 - L4) / 2) + L3;
                sw.WriteLine("    D1 = (L2-L4)/2 + L3");
                sw.WriteLine("       = ({0}-{1})/2 + {2} = {3:f3} m",
                    L2,
                    L4,
                    L3,
                    D1);

                sw.WriteLine();
                double w2 = (H - d4) * L4 * gamma_c;
                sw.WriteLine("    w2 = (H - d4) * L4 * γ_c");
                sw.WriteLine("       = ({0} - {1}) * {2} * {3}",
                    H,
                    d4,
                    L4,
                    gamma_c);
                sw.WriteLine("       = {0} kN", w2);

                double D2 = L4 / 2 + (L2 - L4) + L3;
                sw.WriteLine();
                sw.WriteLine("    D2 = L4 / 2 + (L2 - L4) + L3");
                sw.WriteLine("       = {0} / 2 + ({1} - {0}) + {2}",
                    L4,
                    L2,
                    L3);
                sw.WriteLine("       = {0} m", D2);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Weight of Base");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double w3 = (L1 + L2 + L3) * d4 * gamma_c;
                sw.WriteLine("    w3 = (L1 + L2 + L3) * d3 * γ_c");
                sw.WriteLine("       = ({0} + {1} + {2}) * {3} * {4}",
                    L1,
                    L2,
                    L3,
                    d4,
                    gamma_c);
                sw.WriteLine("       = {0:f2} kN", w3);
                double D3 = provided_l / 2;
                sw.WriteLine();
                sw.WriteLine("    D3 = {0} m", D3);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Weight of Earth on Heel Slab");
                sw.WriteLine("------------------------------------------------------------");

                double w4 = (H - d4) * L1 * gamma_b;
                sw.WriteLine();
                sw.WriteLine("   w4 = (H - d4) * L1 * γ_b");
                sw.WriteLine("      = ({0} - {1}) * {2} * {3}",
                    H,
                    d4,
                    L1,
                    gamma_b);
                sw.WriteLine();
                sw.WriteLine("      = {0:f2} kN", w4);

                double D4 = (L1 / 2) + L2 + L3;
                sw.WriteLine();
                sw.WriteLine("   D4 = (L1 / 2) + L2 + L3");
                sw.WriteLine("      = ({0} / 2) + {1} + {2}",
                    L1,
                    L2,
                    L3);
                sw.WriteLine("      = {0:f2} m", D4);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Permanent Load from Super Structure = w5 = {0} kN", w5);
                sw.WriteLine();
                double D5 = L3 + ((L2 - L4) / 2);
                sw.WriteLine("                                  D5 = L3 + ((L2 - L4) / 2)");
                sw.WriteLine("                                     = {0} + (({1} - {2}) / 2)",
                    L3,
                    L2,
                    L4);
                sw.WriteLine("                                     = {0:f2} m", D5);
                sw.WriteLine();
                sw.WriteLine("Vertical Load from Super Structure = w6 = {0} kN", w6);
                double D6 = L3 + (L2 - L4) / 2;
                sw.WriteLine();
                sw.WriteLine("                                     D6 = L3 + (L2 - L4) / 2");
                sw.WriteLine("                                        = {0} + ({1} - {2}) / 2",
                    L3,
                    L2,
                    L4);
                sw.WriteLine("                                        = {0:f2} m", D6);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Force due to  braking");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Braking Force = F = {0} kN", F);

                double one_abut_force = F / 2;
                sw.WriteLine();
                sw.WriteLine("Force on one Abutment wall = {0}/2 = {1} kN",
                    F,
                    one_abut_force.ToString("0.0"));
                sw.WriteLine();
                sw.WriteLine("Width of Abutment wall = B = {0} m", B);

                sw.WriteLine();
                double P2 = one_abut_force / B;
                sw.WriteLine("Horizontal Force per m. of wall = {0:f2}/{1:f2} = {2:f3} kN/m = P2",
                    one_abut_force,
                    B,
                    P2);

                double d7 = H - d1;
                sw.WriteLine();
                sw.WriteLine("d7 = H - d1 = {0} - {1} = {2:f3} m",
                    H,
                    d1,
                    d7);
                //double h1 = 1.2;
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Bracking is applied at height = h1 = {0} m", h1);// ? to be check
                sw.WriteLine("Span of Longitudinal Girder = {0} m = L", L);// ? to be check

                sw.WriteLine();
                sw.WriteLine("Vetical bracking force reaction on one abutment");
                double w7 = (F * (h1 + d1 + d3)) / (L * B);
                sw.WriteLine("      W7 = (F * (h1 + d1 + d3)) / (L * B)");
                sw.WriteLine("         = ({0} * ({1} + {2} + {3})) / ({4} * {5})",
                    F,
                    h1,
                    d1,
                    d3,
                    L,
                    B);
                sw.WriteLine("         = {0:f2} kN/m", w7);

                double D8 = L3 + (L2 - L4) / 2;
                sw.WriteLine();
                sw.WriteLine("D8 = L3 + (L2 - L4) / 2");
                sw.WriteLine("   = {0} + ({1} - {2}) / 2",
                    L3,
                    L2,
                    L4);
                sw.WriteLine("   = {0:f2} m", D8);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Active Earth Pressure");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("P1 = 0.5 * H * H * γ_b * ka");

                //double radian = Math.PI / 180;
                double radian = 1;

                double Ka = (Math.Sin(theta * radian - phi * radian) / Math.Sin(theta * radian)) * (1 / (Math.Sqrt(Math.Sin(theta * radian + z * radian)) +
                    (Math.Sqrt((Math.Sin(phi * radian + z * radian)) * (Math.Sin(phi * radian - delta * radian)) / (Math.Sin(theta * radian - delta * radian))))));

                //sw.WriteLine("P1 = 0.5 * H * H * gamma_b * ka");
                //sw.WriteLine("γσµφδπρ√τ≈αβθ = 0.5 * H * H * gamma_b * ka");
                sw.WriteLine();
                sw.WriteLine("θ = {0}° , φ = {1}°, z = {2}°, δ = {3}",
                    (theta / rad),
                    phi / rad,
                    z / rad,
                    delta / rad);
                sw.WriteLine();
                sw.WriteLine("ka = (Sin(θ - φ) / Sin(θ)) / ((√(Sin(θ + z)) + (√(Sin(φ + z) * Sin(φ - δ)/Sin(θ - δ)))");
                sw.WriteLine("   = {0:f3}", Ka);
                sw.WriteLine();

                double P1 = 0.5 * H * H * gamma_b * Ka;

                sw.WriteLine("P1 = 0.5 * {0} * {0} * {1} * {2:f3} = {3:f2} kN/m",
                    H, gamma_b, Ka, P1);

                sw.WriteLine();
                double D9 = 0.42 * H;
                sw.WriteLine("D9 = 0.42 * H = 0.42 * {0} = {1} m", H, D9.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Load from Vehicle and Approach Slab");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double h2 = d2;

                sw.WriteLine("Equivalent height of earth for Vehicle Load Surcharge = d2 = {0:f2} m", d2);

                double hor_force_vehicle = d2 * gamma_b * Ka * H;
                sw.WriteLine();
                sw.WriteLine("Horizontal force for Vehicle Load Surcharge = d2 * γ_b * Ka * H");
                sw.WriteLine("                                            = {0:f2} * {1} * {2:f4} * {3:f2}",
                    h2,
                    gamma_b,
                    Ka,
                    H);
                sw.WriteLine("                                            = {0:f2} ", hor_force_vehicle);

                double hor_force_approach_slab = d3 * gamma_c * Ka * H;
                sw.WriteLine();
                sw.WriteLine("Horizontal force for Approach slab = d3 * γ_c * ka * H");
                sw.WriteLine("                                   = {0} * {1} * {2:f3} * {3}",
                    d3, gamma_c, Ka, H);
                sw.WriteLine("                                   = {0:f2} ", hor_force_approach_slab);

                double P3_total_hor_force = hor_force_approach_slab + hor_force_vehicle;
                sw.WriteLine();
                sw.WriteLine("Total Horizontal Force = {0:f2} + {1:f2} = {2:f2} kN/m.",
                    hor_force_approach_slab,
                    hor_force_vehicle,
                    P3_total_hor_force);
                double D10 = H / 2.0;
                sw.WriteLine();

                sw.WriteLine("D10 = H / 2.0 = {0} / 2.0 = {1} m",
                    H,
                    D10);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Verticle Force due to Vehicle Load Surcharge");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                //d3
                double w8 = (h2 * gamma_b + d3 * gamma_c) * L1;
                sw.WriteLine("w8 = (h2 * γ_b + d3 * γ_c) * L1;");
                sw.WriteLine("   = ({0:f2} * {1} + {2:f2} * {3}) * {4:f2}",
                    h2, gamma_b, d3, gamma_c, L1);
                sw.WriteLine("   = {0:f2} kN/m", w8);

                double D11 = (L1 / 2) + L2 + L3;
                sw.WriteLine();
                sw.WriteLine("D11 = (L1 / 2) + L2 + L3");
                sw.WriteLine("    = ({0:f2} / 2) + {1:f2} + {2:f2}",
                    L1,
                    L2,
                    L3);
                sw.WriteLine("    = {0:f2} m.",
                    D11);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("________________________________________________________________________________");
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                    "", "", "V", "H", "DISTANCE", "Mv", "Mh");
                sw.WriteLine("________________________________________________________________________________");

                double Mv1 = (w1 * D1);
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                  "1.",
                                  "Self Weight (w1)",
                                  w1.ToString("0.000"),
                                  "",
                                  "D1=" + D1.ToString("0.00"),
                                  Mv1.ToString("0.000"),
                                  "");

                double Mv2 = (w2 * D2);
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                     "2.",
                                     "Self Weight (w2)",
                                     w2.ToString("0.000"),
                                     "",
                                     "D2=" + D2.ToString("0.00"),
                                     Mv2.ToString("0.000"),
                                     "");

                double Mv3 = (w3 * D3);
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                  "3.",
                                  "Self Weight (w3)",
                                  w3.ToString("0.000"),
                                  "",
                                  "D3=" + D3.ToString("0.00"),
                                  Mv3.ToString("0.000"),
                                  "");

                double Mv4 = (w4 * D4);
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                  "4.",
                                  "Weight of ",
                                  "",
                                  "",
                                  "",
                                  "",
                                  "");
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                  "",
                                                  "Earth on Heel Slab (w4)",
                                                  w4.ToString("0.000"),
                                                  "",
                                                  "D4=" + D4.ToString("0.00"),
                                                  Mv4.ToString("0.000"),
                                                  "");

                double Mv5 = (w5 * D5);
                sw.WriteLine("{0,5}{1,-27}{2,15}{3,13}{4,13}{5,13}{6,13}",
                                   "5.",
                                   "Permanent Load from ",
                                   "",
                                   "",
                                   "",
                                   "",
                                   "");
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                 "",
                                                 "Super Structure (w5)",
                                                 w5.ToString("0.000"),
                                                 "",
                                                 "D5=" + D5.ToString("0.00"),
                                                 Mv5.ToString("0.000"),
                                                 "");

                double Mh1 = (P1 * D9);
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                  "6.",
                                  "Active Earth Pressure (P1)",
                                  "",
                                  P1.ToString("0.000"),
                                  "D9=" + D9.ToString("0.00"),
                                  "",
                                  Mh1.ToString("0.000"));

                double Mv7 = (w8 * D11);
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                  "7.",
                                  "Vertical Load for ",
                                  "",
                                  "",
                                  "",
                                  "",
                                  "");
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                  "",
                                                  "Vehicle Load Surcharge (w8)",
                                                  w8.ToString("0.000"),
                                                  "",
                                                  "D11=" + D11.ToString("0.00"),
                                                  Mv7.ToString("0.000"),
                                                  "");

                double Mh2 = (P3_total_hor_force * D10);
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                  "8.",
                                  "Horizontal Force for ",
                                  "",
                                  "",
                                  "",
                                  "",
                                  "");

                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                  "",
                                                  "Vehicle Load Surcharge (P3)",
                                                  "",
                                                  P3_total_hor_force.ToString("0.000"),
                                                  "D10=" + D10.ToString("0.00"),
                                                  "",
                                                  Mh2.ToString("0.000"));

                sw.WriteLine("________________________________________________________________________________");
                double V1 = w1 + w2 + w3 + w4 + w5 + w8;
                double H1 = P1 + P3_total_hor_force;
                double MV1_SUM = Mv1 + Mv2 + Mv3 + Mv4 + Mv5 + Mv7;
                double MH1_SUM = Mh1 + Mh2;
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                  "Sum",
                                                  " of Items in",
                                                   "V2=",
                                                  "H2=",
                                                  "",
                                                  "MV2=",
                                                  "MH2=");
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                   "",
                                                  "Span Unloaded Condition",
                                                  "" + V1.ToString("0.00"),
                                                  "" + H1.ToString("0.00"),
                                                  "",
                                                  "" + MV1_SUM.ToString("0.00"),
                                                  "" + MH1_SUM.ToString("0.00"));
                sw.WriteLine("________________________________________________________________________________");
                double Mh3 = (P2 * d7);
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                  "9.",
                                                  "Horizontal ",
                                                  "",
                                                  "",
                                                  "",
                                                  "",
                                                  "");

                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                  "",
                                                  "Bracking Force (P2)",
                                                  "",
                                                  P2.ToString("0.000"),
                                                  "D7=" + d7.ToString("0.00"),
                                                  "",
                                                  Mh3.ToString("0.000"));

                double Mv8 = (w7 * D8);
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                  "10.",
                                                  "Verticle",
                                                  "",
                                                  "",
                                                  "",
                                                  "",
                                                  "");

                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                  "",
                                                  "Bracking Force (w7)",
                                                  w7.ToString("0.000"),
                                                  "",
                                                  "D8=" + D8.ToString("0.00"),
                                                  Mv8.ToString("0.000"),
                                                  "");

                double Mv9 = (w6 * D6);
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                  "11.",
                                                  "Vehicle Load from ",
                                                  "",
                                                  "",
                                                  "",
                                                  "",
                                                  "");

                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                                 "",
                                                                 "Super Structure (w6)",
                                                                 w6.ToString("0.000"),
                                                                 "",
                                                                 "D6=" + D6.ToString("0.00"),
                                                                 Mv9.ToString("0.000"),
                                                                 "");

                sw.WriteLine("________________________________________________________________________________");
                double V2 = V1 + w7 + w6;
                double H2 = H1 + P2;
                double MV2_SUM = MV1_SUM + Mv8 + Mv9;
                double MH2_SUM = MH1_SUM + Mh3;
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                 "Sum",
                                                 " of Items in",
                                                 "V2=",
                                                 "H2=",
                                                 "",
                                                 "MV2=",
                                                 "MH2=");
                sw.WriteLine("{0,3}{1,-27}{2,11}{3,10}{4,10}{5,10}{6,9}",
                                                  "",
                                                  "Span Loaded Condition",
                                                  "" + V2.ToString("0.00"),
                                                  "" + H2.ToString("0.00"),
                                                  "",
                                                  "" + MV2_SUM.ToString("0.00"),
                                                  "" + MH2_SUM.ToString("0.00"));

                sw.WriteLine("________________________________________________________________________________");
                sw.WriteLine("________________________________________________________________________________");

                #endregion

                #region STEP 3
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Check for Stability against Overturning");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("CASE I : Span Loaded Condition");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Overturning Moment about toe = MH2 = {0} kN-m.", MH2_SUM.ToString("0.000"));
                sw.WriteLine("Restoring Moment about toe = MV2 = {0} kN-m.", MV2_SUM.ToString("0.000"));
                sw.WriteLine();

                double safety_factor = MV2_SUM / MH2_SUM;
                sw.WriteLine("Factor of Safety against overturning = MV2/MH2");

                if (safety_factor > 2.0)
                {
                    sw.WriteLine("                                     = {0}/{1} = {2} > 2.0 , OK",
                        MV2_SUM.ToString("0.000"),
                        MH2_SUM.ToString("0.000"),
                        safety_factor.ToString("0.000"));
                }
                else
                {

                    sw.WriteLine("                                     = {0}/{1} = {2} < 2.0, NOT OK",
                        MV2_SUM.ToString("0.000"),
                        MH2_SUM.ToString("0.000"),
                        safety_factor.ToString("0.000"));
                    sw.WriteLine("Increase the Length of base wall on Earth Retaining Side,");
                    sw.WriteLine("more than present length of {0} m (L1)", L1);
                }

                sw.WriteLine();
                sw.WriteLine("Location of Resultant from toe");
                double Xo = (MV2_SUM - MH2_SUM) / V2;
                sw.WriteLine("Xo = (MV2 - MH2)/V2 = ({0} - {1})/{2} = {3} m",
                    MV2_SUM.ToString("0.000"),
                    MH2_SUM.ToString("0.000"),
                    V2.ToString("0.000"),
                    Xo.ToString("0.000"));

                double emax = (L1 + L2 + L3) / 6.0;
                sw.WriteLine();
                sw.WriteLine("Maximum permissible Eccentricity  = (L1 + L2 + L3)/6.0");
                sw.WriteLine("                                  = ({0} + {1} + {2})/6.0",
                    L1,
                    L2,
                    L3);
                sw.WriteLine("                                  = {0} = emax", emax.ToString("0.000"));

                double e1 = (L1 + L2 + L3) / 2 - Xo;

                sw.WriteLine();
                sw.WriteLine("Eccentricity of Resultant = (L1 + L2 + L3)/2 - Xo");
                sw.WriteLine("                          = ({0} + {1} + {2})/2 - {3:f2}",
                    L1,
                    L2,
                    L3,
                    Xo);
                if (e1 < emax)
                    sw.WriteLine("                      e1  = {0} < {1}(emax) , OK", e1.ToString("0.00"), emax.ToString("0.000"));
                else
                {
                    sw.WriteLine("                      e1  = {0} > {1}(emax), NOT OK", e1.ToString("0.00"), emax.ToString("0.000"));
                    sw.WriteLine("Increase the length of base of wall on Earth Retaining Side,");
                    sw.WriteLine("more than present length of {0} m (L1)", L1);
                }
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("CASE II : Span Unloaded Condition");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Overturning Moment about toe = {0}", MH1_SUM.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("Restoring Moment about toe   = {0}", MV1_SUM.ToString("0.00"));

                safety_factor = MV1_SUM / MH1_SUM;
                sw.WriteLine();
                if (safety_factor >= 2.0)
                {
                    sw.WriteLine("Factor of Safety against overturning = {0} / {1} = {2} > 2.0 , OK",
                        MV1_SUM.ToString("0.00"),
                        MH1_SUM.ToString("0.00"),
                        safety_factor.ToString("0.00"));
                }
                else
                {
                    sw.WriteLine("Factor of Safety against overturning = {0} / {1} = {2} < 2.0, NOT OK",
                        MV1_SUM.ToString("0.00"),
                        MH1_SUM.ToString("0.00"),
                        safety_factor.ToString("0.00"));
                }

                sw.WriteLine();
                sw.WriteLine("Location of Resultant for toe");
                Xo = (MV1_SUM - MH1_SUM) / V1;
                sw.WriteLine("Xo = (MV1 - MH1)/V1 = ({0} - {1})/{2} = {3}",
                    MV1_SUM.ToString("0.000"),
                    MH1_SUM.ToString("0.000"),
                    V1.ToString("0.000"),
                    Xo.ToString("0.000"));

                emax = (L1 + L2 + L3) / 6.0;
                sw.WriteLine();
                sw.WriteLine("Maximum permissible Eccentricity  = (L1 + L2 + L3)/6.0");
                sw.WriteLine("                                  = ({0} + {1} + {2})/6.0",
                    L1,
                    L2,
                    L3);
                sw.WriteLine("                                  = {0} = emax", emax.ToString("0.000"));

                double e2 = (L1 + L2 + L3) / 2 - Xo;

                sw.WriteLine();
                sw.WriteLine("Eccentricity of Resultant = (L1 + L2 + L3)/2 - Xo");
                sw.WriteLine("                          = ({0} + {1} + {2})/2 - {3:f2}",
                    L1,
                    L2,
                    L3,
                    Xo);
                if (e2 < emax)
                    sw.WriteLine("                      e2  = {0} < {1}(emax) , OK", e2.ToString("0.000"), emax.ToString("0.000"));
                else
                {
                    sw.WriteLine("                      e2  = {0} > {1}(emax), NOT OK", e2.ToString("0.000"), emax.ToString("0.000"));
                    sw.WriteLine("Increase the length of base of wall on Earth Retaining Side,");
                    sw.WriteLine("more than present length of {0} m (L1)", L1);
                }



                #endregion


                #region STEP 4

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 :Check for Stresses at Base ");
                sw.WriteLine("        For Span Loaded Condition");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Total downward forces = V2 = {0:f3} kN", V2);
                sw.WriteLine("Extreme Stresses at Base = (V2 / ((L1+L2+L3)*1.0)) *( 1.0 ± ((6 * e1)/(L1+L2+L3)))");
                sw.WriteLine("                         = ({0} / {1}*1.0) *( 1.0 ± ((6 * {2})/{1}))",
                    V2.ToString("0.000"),
                    provided_l,
                    e1.ToString("0.00"));

                double val1, val2, Pr1, Pr5;
                val1 = V2 / provided_l;
                val2 = (6 * e1) / provided_l;

                //p2
                Pr1 = val1 * (1 + val2);
                Pr5 = val1 * (1 - val2);


                sw.WriteLine("                         = {0} * (1 ± {1})", val1.ToString("0.000"), val2.ToString("0.000"));
                sw.WriteLine("                         = {0} = p1 and {1} = p2",
                    Pr1.ToString("0.000"),
                    Pr5.ToString("0.000"));

                if (Pr1 < 350)
                {
                    sw.WriteLine("p1 < 350 kN = Bearing Capacity, OK");
                }
                else
                {
                    sw.WriteLine("p1 > 350 kN = Bearing Capacity, NOT OK");
                }
                if (Pr5 >= 0)
                {
                    sw.WriteLine("p2 > 0 = No Tension, OK");
                }
                else
                    sw.WriteLine("p2 < 0 = No Tension, NOT OK");



                #endregion


                #region STEP 5
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5");
                sw.WriteLine("Check for Sliding");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Sliding Force = H2 = {0} kN", H2.ToString("0.000"));
                double FF = mu * V2;
                sw.WriteLine();
                sw.WriteLine("Force resisting Sliding = µ * V2 = {0} * {1} = {2} = FF",
                    mu.ToString("0.00"),
                    V2.ToString("0.00"),
                    FF.ToString("0.000"));
                safety_factor = FF / H2;
                sw.WriteLine();
                if (safety_factor > 1.5)
                {
                    sw.WriteLine("Factor of Safety against Sliding = FF/H2 = {0}/{1} = {2} > 1.5 , OK",
                        FF.ToString("0.000"),
                        H2.ToString("0.000"),
                        safety_factor.ToString("0.00"));
                }
                else
                {
                    sw.WriteLine("Factor of Safety against Sliding = FF/H2 = {0}/{1} = {2} < 1.5 , NOT OK",
                        FF.ToString("0.000"),
                        H2.ToString("0.000"),
                        safety_factor.ToString("0.00"));
                    sw.WriteLine("Shear key will be required.");
                }
                #endregion


                #region STEP 6
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : Reinforcement Steel Bars.");
                sw.WriteLine("Design of Base Slab at Front Toe for Steel requirement.");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("Thickness of Base Slab = d4 = {0:f3} m", d4);

                double deff = d4 - (cover / 1000);
                sw.WriteLine("Deff = d4 - cover = {0:f3} - {1:f3} = {2:f3} m",
                    d4,
                    (cover / 1000),
                    deff);

                double Pr2 = ((Pr1 - Pr5) / provided_l) * (provided_l - (L3 - deff));
                Pr2 += Pr5;
                double Pr3 = ((Pr1 - Pr5) / provided_l) * (L1 + L2);
                Pr3 += Pr5;
                double Pr4 = ((Pr1 - Pr5) / provided_l) * (L1);
                Pr4 += Pr5;

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("ON BASE :");
                sw.WriteLine();
                sw.WriteLine("Pr1 = Upward pressure at Toe = {0} kN/sq.m.", Pr1.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("Pr2 = Upward Pressure at a distance of effective depth from Front of wall");
                sw.WriteLine("    = {0:F3} kN/sq.m.",
                    Pr2);
                sw.WriteLine();
                sw.WriteLine("Pr3 = Upward Pressure at The Front Face of wall = {0:f3} kN/sq.m.",
                                    Pr3);

                sw.WriteLine();
                sw.WriteLine("Pr4 = Upward Pressure at The Backfill Face of wall = {0:f3} kN/sq.m.",
                                    Pr4);
                sw.WriteLine();
                sw.WriteLine("Pr5 = Upward Pressure at Heel = {0:f3} kN/sq.m.",
                                                    Pr5);
                double Dpr = d4 * gamma_c;
                sw.WriteLine();
                sw.WriteLine("Dpr = downward Pressure by Self weight of Base = {0:f2} * {1} = {2:f3} kN/sq.m.",
                    d4,
                    gamma_c,
                    Dpr);

                double Vu = factor * (((Pr1 + Pr2) / 2) - Dpr) * (L3 - deff);
                sw.WriteLine();
                sw.WriteLine("Design Shear Force ");
                sw.WriteLine("   = Vu = Shear Force Factor * [((Pr1 * Pr2) / 2) - Dpr) * (L3 - deff)");
                sw.WriteLine("   = {0:f2} * [(({1:f3} * {2:f3}) / 2) - {3:f3}) * ({4:f3} - {5:f2})",
                    factor,
                    Pr1,
                    Pr2,
                    Dpr,
                    L3,
                    deff);
                sw.WriteLine("                        = {0:f3}", Vu);
                double Mu = factor * (((L3 - deff) * Pr1 * L3 * L3 * 0.67 +
                    (L3 - deff) * Pr2 * L3 * L3 * (L3 - deff) - Dpr * L3 * L3 * (L3 - deff)));

                sw.WriteLine();
                sw.WriteLine("Design Bending Moment");
                sw.WriteLine("  = Mu = Bending Moment Factor * [(L3-deff) * Pr1 * L3 * L3 * 0.67 + ");
                sw.WriteLine("        (L3-deff) * Pr2 * L3 * L3 * (L3-deff) - Dpr * L3 * L3 * (L3 - deff)]");

                sw.WriteLine("       = {0:f2} * [({1:f2}-{2:f2}) * {3:f3} * {1:f3} * {1:f3} * 0.67 + ",
                    factor,
                    L3,
                    deff,
                    Pr1);

                sw.WriteLine("        ({0:f2}-{1:f2}) * {2:f2} * {0:f2} * {0:f2} * ({0:f2}-{1:f2}) - {3:f2} * {0:f2} * {0:f2} * ({0:f2} - {1:f2})]",

                    L3,
                    deff,
                    Pr2,
                    Dpr);


                sw.WriteLine();
                sw.WriteLine("      = {0:f3} kN-m.", Mu);

                double b = 1000;

                double eff_depth = Math.Sqrt((Mu * 10E5) / (0.138 * f_ck * b));
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Effective Depth of Base Slab");
                sw.WriteLine("         = √((Mu * 10E5)/(0.138*σ_c*b))");
                sw.WriteLine("         = √(({0:f2} * 10E5)/(0.138*{1}*{2}))",
                    Mu,
                    f_ck,
                    b);
                if (eff_depth < deff * 1000)
                {
                    sw.WriteLine("                = {0:f3} < 700 , Provided Deff , OK.",
                        eff_depth);
                }
                else
                {
                    sw.WriteLine("                = {0:f3} > 700 , Provided Deff, NOT OK.",
                        eff_depth);
                }

                sw.WriteLine();
                sw.WriteLine("Provide Base Thick {0:f2} mm", (d4 * 1000));
                sw.WriteLine();
                sw.WriteLine("Area of Steel required at bottom Base slab at Toe");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Mu = 0.87 * f_y * Ast * [d - (f_y * Ast)/(f_ck * b)]");
                sw.WriteLine("{0:f2} * 10E5 = 0.87 * {1:f3} * Ast * [{2:f2} - ({1:f2} * Ast)/({3:f2} * {4:F2})]",
                    Mu,
                    f_y,
                    deff * 1000,
                    f_ck,
                    b);
                double _a, _b, _c, _d, _Mu;

                _Mu = Mu;

                _a = 0.87 * f_y;
                _b = (f_y / (f_ck * b));

                _c = _a * deff * 1000;
                _d = _a * _b;

                _b = _c / _d;

                _c = _Mu / _d;

                sw.WriteLine();
                sw.WriteLine("Ast * Ast - {0:f2} * Ast + {1:f2} * 10E5 = 0",
                    _b,
                    _c);

                double Ast1, Ast2;
                sw.WriteLine();
                sw.WriteLine("Ast = ({0:f2} ± √({0:f2}*{0:f2} - 4*{1:f3}))/2",
                    _b,
                    _c);

                _d = Math.Sqrt((_b * _b - 4 * _c * 10E5));
                sw.WriteLine("    =  ({0:f2} ± {1:f2})/2",
                    _b,
                    _d);

                Ast1 = (_b + _d) / 2;
                Ast2 = (_b - _d) / 2;

                sw.WriteLine("    = {0:f2}, {1:f2}", Ast1, Ast2);

                double Ast_provided = Math.PI * 20 * 20 / 4;
                int no_bar = 1000 / 200;

                double bar_dia = 15;
                do
                {
                    bar_dia += 5;
                    if (bar_dia == 30) bar_dia += 2;
                    Ast_provided = Math.PI * bar_dia * bar_dia / 4;
                    no_bar = 1000 / 200;
                    Ast_provided = Ast_provided * no_bar;
                }
                while (Ast_provided < Ast2);
                sw.WriteLine();
                sw.WriteLine("Provided T{0:f0} bars @ 200 mm c/c at bottom of Base Slab at Toe", bar_dia);

                _bd4 = bar_dia;
                _sp4 = 200;

                sw.WriteLine();
                sw.WriteLine("Provided Provided Ast = {0:f2} sq.mm.", Ast_provided);

                double Pst = Ast_provided * 200 / (b * deff * 1000);
                sw.WriteLine();
                sw.WriteLine("Percentage of Tension Steel = Pst = {0:f2}%",
                    Pst);

                double tau_c = iApp.Tables.Permissible_Shear_Stress(Pst, (CONCRETE_GRADE)(int)f_ck, ref ref_string);
                sw.WriteLine();
                //sw.WriteLine("Allowable Shear Stress = τ_c = {0:f2} IS 456 : 2000", tau_c);
                sw.WriteLine("Allowable Shear Stress = τ_c = {0:f2} {1}", tau_c, ref_string);

                sw.WriteLine();
                double tau_v = Vu * 10E2 / (b * deff * 1000);
                sw.WriteLine();
                if (tau_v < tau_c)
                {
                    sw.WriteLine("Applied Shear Stress τ_v = Vu/b*d = {0:f3} * 10E2/({1:f2}*{2:f2}) = {3:f2} < {4:f2} , OK",
                    Vu,
                    b,
                    deff * 1000,
                    tau_v,
                    tau_c);

                }
                else
                {
                    sw.WriteLine("Applied Shear Stress τ_v = Vu/b*d = {0:f3} * 10E2/({1:f2}*{2:f2}) = {3:f2} > {4:f2}, NOT OK",
                        Vu,
                        b,
                        deff * 1000,
                        tau_v,
                        tau_c);
                }
                double dist_steel = 0.12 / 100 * b * deff * 1000;
                sw.WriteLine();
                sw.WriteLine("Distribution Steel = 0.12/100 * {0} * {1} = {2:f2} sq.mm.",
                    b,
                    deff,
                    dist_steel);

                sw.WriteLine();
                sw.WriteLine("Provide T10 @ 90 mm c/c");
                _bd3 = 10;
                _sp3 = 90;

                _bd5 = 10;
                _sp5 = 90;

                Ast_provided = Math.PI * 10 * 10 / 4;
                no_bar = (int)(1000.0 / 90.0);
                Ast_provided = Ast_provided * no_bar;
                sw.WriteLine("Ast Provided = {0:f2} sq.mm", Ast_provided);

                #endregion


                #region STEP 7
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 7 : Design of Base Slab at Backfill Heel");
                sw.WriteLine("         Heel Side for Steel Reinforcements");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Upward Pressure varies from Pr3 = {0:f2} to Pr5 = {1:f2}",
                    Pr3,
                    Pr5);
                sw.WriteLine("downward Pressure is Earth Load + Surcharge + Self Weight");

                double Pr6 = (H - d4) * gamma_b + h2 * gamma_b + d3 * gamma_c + d4 * gamma_c;
                sw.WriteLine("             Pr6 = (H-d4)*γ_b + h2 * γ_b  + d3*γ_c + d4*γ_c");
                sw.WriteLine("                 = ({0:f3}-{1:f2})*{2:f2} + {3:f2} * {2:f2}  + {4:f2}*{5:f2} + {1:f3}*{5:f3}",
                    H,
                    d4,
                    gamma_b,
                    h2,
                    d3,
                    gamma_c);
                sw.WriteLine("                 = {0:f2}", Pr6);

                sw.WriteLine();
                sw.WriteLine("Here downward pressure Pr6 = {0:f2} is more than Pr4 = {1:f2} and Pr5 = {2:f2}",
                    Pr6, Pr4, Pr5);//? Pr3 = 157 and Pr6 = 135 , How
                sw.WriteLine();
                sw.WriteLine("So, tension reinforcement steel will be required at the top");

                Vu = factor * (Pr6 * L1 - 0.5 * Pr4 * L1 - 0.5 * Pr5 * L1);


                sw.WriteLine();
                sw.WriteLine("Design Shear Force");
                sw.WriteLine("           Vu = Shear Force Factor * (Pr6 * L1 - 0.5 * Pr4 * L1 - 0.5 * Pr5 * L1)");
                sw.WriteLine("              = {0:f2} * ({1:f2} * {2:f2} - 0.5 * {3:f3} * {2:f3} - 0.5 * {4:f3} * {2:f3})",
                    factor,
                    Pr6,
                    L1,
                    Pr4,
                    Pr5);

                sw.WriteLine("              = {0:f2} kN", Vu);

                Mu = factor * (Pr6 * L1 * L1 * 0.5 - 0.5 * Pr4 * L1 * L1 * 0.33 - 0.5 * Pr5 * L1 * L1 * 0.67);

                sw.WriteLine();
                sw.WriteLine("Design Bending Moment ");
                sw.WriteLine("      Mu = Bending Moment Factor * (Pr6 * L1 * L1 * 0.5 - 0.5 * Pr4 * L1 * L1 * 0.33 - 0.5 * Pr5 * L1 * L1 * 0.67)");
                sw.WriteLine("         = {0:f2} * ({1:f2} * {2:f3} * {2:f3} * 0.5 - 0.5 * {3:f3} * {1:f3} * {1:f3} * 0.33 - 0.5 * {4:f3} * {2:f3} * {2:f3} * 0.67)",
                    factor,
                    Pr6,
                    L1,
                    Pr4,
                    Pr5);
                sw.WriteLine();
                sw.WriteLine("         = {0:f2} kN-m", Mu);


                eff_depth = Math.Sqrt((Mu * 10E5) / (0.138 * f_ck * b));
                sw.WriteLine();
                sw.WriteLine("Effective Depth of Base Slab at Heel = √((Mu * 10E5)/(0.138*σ_c*b))");
                sw.WriteLine("                                     = √(({0:f2} * 10E5)/(0.138*{1:f2}*{2:f2}))",
                    Mu,
                    f_ck, b);

                if (eff_depth < (deff * 1000))
                    sw.WriteLine("                                     = {0:f2} mm < {1:f2}", eff_depth,
                        (deff * 1000));

                sw.WriteLine();
                sw.WriteLine("Area of Steel required at top of base slab at Heel");
                //σ_st

                sw.WriteLine();
                sw.WriteLine("  Mu = 0.87 * σ_st * Ast * (d-((σ_st*Ast)/(σ_c*b))");
                sw.WriteLine();
                sw.WriteLine("  {0:f2}*10E5 = 0.87 * {1:f2} * Ast * ({2:f2}-(({1:f2}*Ast)/({3}*{4}))",
                    Mu,
                    f_y,
                    deff * 1000,
                    f_ck,
                    b);

                _c = 0.87 * f_y * deff * 1000;
                _d = (0.87 * f_y * f_y) / (f_ck * b);


                _b = _c / _d;

                sw.WriteLine();
                sw.WriteLine("Ast*Ast - {0:f2}*Ast + {1:f2} * 10E6 = 0",
                    _b, _c);

                _c = (Mu / _d) * 10E5;
                _d = Math.Sqrt((_b * _b - 4 * _c));

                Ast1 = (_b + _d) / 2;
                Ast2 = (_b - _d) / 2;

                sw.WriteLine();
                sw.WriteLine("Ast = {0:f2} and {1:f2} sq.mm.", Ast1, Ast2);
                bar_dia = 15;
                do
                {
                    bar_dia += 5;
                    if (bar_dia == 30) bar_dia += 2;
                    Ast_provided = (Math.PI * bar_dia * bar_dia / 4) * 10;
                }
                while (Ast_provided < Ast2);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Provide T{0} bars @ 100 mm c/c at Top of bar slab at Heel.", bar_dia);

                _bd6 = bar_dia;
                _sp6 = 100;


                sw.WriteLine();
                sw.WriteLine("Provide Ast = {0:f2} sq.mm", Ast_provided);

                double percent = Ast_provided * 100 / (1000 * deff * 1000);

                tau_c = iApp.Tables.Permissible_Shear_Stress(percent, (CONCRETE_GRADE)(int)f_ck, ref ref_string);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Allowable Shear Stress = τ_c = {0:f2} {1}", tau_c, ref_string);

                tau_v = Vu * 1000 / (b * deff * 1000);

                sw.WriteLine();
                if (tau_v < tau_c)
                {
                    sw.WriteLine("Applied Shear Stress τ_v = ({0:f2} * 1000)/(1000*{1:f1}) = {2:f2} N/sq.mm. < τ_c , OK",
                        Vu,
                        deff * 1000,
                        tau_v);
                }
                else
                {
                    sw.WriteLine("Applied Shear Stress τ_v = ({0:f2} * 1000)/(1000*{1:f1}) = {2:f2} N/sq.mm. > τ_c ,NOT OK",
                        Vu,
                        deff * 1000,
                        tau_v);
                }
                sw.WriteLine();
                sw.WriteLine("Distribution Steel = 0.12/100 * {0} * {1} = {2:f2} sq.mm.",
                    b,
                    deff,
                    dist_steel);

                sw.WriteLine();
                sw.WriteLine("Provide T10 @ 90 mm c/c");

                _bd7 = 10;
                _sp7 = 90;

                Ast_provided = Math.PI * 10 * 10 / 4;
                no_bar = (int)(1000.0 / 90.0);
                Ast_provided = Ast_provided * no_bar;
                sw.WriteLine();
                sw.WriteLine("Ast Provided = {0:f2} sq.mm", Ast_provided);

                #endregion


                #region STEP 8
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 8 : Design of Wall Reinforcement");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("At the bottom of the front face of the wall");
                sw.WriteLine("   Design Bending Moment ");
                sw.WriteLine("     = (1/6) * Ca * γ_b * H * H * H + (1/2) * Ca * γ_b * h1 * H * H");
                sw.WriteLine("     = (1/6) * {0:f2} * {1:f0} * {2:f2} * {2:f2} * {2:f2} + (1/2) * {0:f2} * {1:f0} * {3:f2} * {2:f2} * {2:f2}",
                    Ca,
                    gamma_b,
                    H,
                    h1);

                double deg_bend_mom = (1.0 / 6.0) * Ca * gamma_b * H * H * H + (1.0 / 2.0) * Ca * gamma_b * h2 * H * H;
                sw.WriteLine();
                sw.WriteLine("     = {0:f2} kN-m", deg_bend_mom);


                double deg_shear = Ca * gamma_b * h1 * H + 0.5 * Ca * gamma_b * H * H;
                sw.WriteLine();
                sw.WriteLine("   Design Shear ");
                sw.WriteLine("      = Ca * γ_b * h1 * H + 0.5 * Ca * γ_b * H * H");
                sw.WriteLine("      = {0:f2} * {1:f0} * {2:f2} * {3:f2} + 0.5 * {0:f2} * {1:f0} * {3:f2} * {3:f2}",
                    Ca,
                    gamma_b,
                    h1,
                    H);
                sw.WriteLine("                = {0:f2} kN", deg_shear);

                sw.WriteLine();
                sw.WriteLine();
                double fact_bend_mom = factor * deg_bend_mom;
                sw.WriteLine("   Mu = Factored Bending Moment = {0:f2} * {1:f2} = {2:f2} kN-m",
                    factor,
                    deg_bend_mom,
                    fact_bend_mom);

                double fact_shear_force = factor * deg_shear;
                sw.WriteLine("   Vu = Factored Shear Force = {0:f2} * {1:f2} = {2:f2} kN",
                                    factor,
                                    deg_shear,
                                    fact_shear_force);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Effective Thickness of wall at the base");
                _d = Math.Sqrt((Mu * 10E5) / (0.138 * f_ck * b));
                sw.WriteLine(" d = √((Mu * 10E5)/(0.138*σ_c*b))");
                sw.WriteLine("   = √(({0:f2} * 10E5)/(0.138*{1:f2}*{2}))",
                    Mu, f_ck, b);
                if (_d < 1000)
                {
                    sw.WriteLine("   = {0:f2} mm < 1000 mm , OK",
                        _d);
                }
                else
                {
                    sw.WriteLine("   = {0:f2} mm > 1000 mm, NOT OK",
                    _d);
                }
                //sw.WriteLine("Area of steel required = Ast = (0.36 * σ_c * b * 0.48 * (L2-cover))/(0.87 * σ_st)");
                sw.WriteLine();
                sw.WriteLine("Area of steel required = Ast = (0.36 * σ_c * b * 0.48 * d)/(0.87 * σ_st)");
                double _ast = (0.36 * f_ck * b * 0.48 * _d) / (0.87 * f_y);

                sw.WriteLine("                             = (0.36 * {0} * {1} * 0.48 * d)/(0.87 * {3})",
                    f_ck,
                    b,
                    _d,
                    f_y);
                sw.WriteLine("                             = {0:f2} sq.mm.", _ast);

                bar_dia = 15;


                do
                {
                    bar_dia += 5;
                    Ast_provided = (Math.PI * bar_dia * bar_dia / 4) * (1000 / 120);
                }
                while (Ast_provided < _ast);

                sw.WriteLine();
                sw.WriteLine("Provide T{0} bars @120 mm c/c", bar_dia);

                _bd1 = bar_dia;
                _sp1 = 120;


                sw.WriteLine("Provided Ast = {0:f2} sq.mm",
                    Ast_provided);

                percent = (Ast_provided * 100) / (b * (t * 1000 - cover));
                //percent = (Ast_provided * 100) / (b * _d);
                sw.WriteLine();
                sw.WriteLine("Percentage of Steel provided = p = {0:f2}*100/({1}*{2}) = {3:f2}%",
                    Ast_provided,
                    b,
                    (t * 1000 - cover),
                    percent);
                tau_c = iApp.Tables.Permissible_Shear_Stress(percent, (CONCRETE_GRADE)(int)f_ck, ref ref_string);
                sw.WriteLine();
                sw.WriteLine("{0}", ref_string);
                sw.WriteLine();
                sw.WriteLine(" Allowable Shear Stress of Concrete = τ_c = {0:f2} N/sq.mm",
                    tau_c);
                tau_v = Vu * 1000 / (b * (t * 1000 - cover));

                sw.WriteLine();
                if (tau_v < tau_c)
                {
                    sw.WriteLine("Applied Shear Stress = τ_v = Vu/b*d = {0:f2} * 1000/({1} * {2}) = {3:f2} < τ_c , OK",
                        Vu,
                        b,
                        (t * 1000 - cover),
                        tau_v);
                }
                else
                {
                    sw.WriteLine("Applied Shear Stress = τ_v = Vu/b*d = {0:f2} * 1000/({1} * {2}) = {3:f2} N/sqmm > τ_c NOT , OK",
                    Vu,
                    b,
                    _d,
                    tau_v);
                }
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Distribution Steel for Temperature Reinforcements:");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Total Sectional Area of the wall");

                double total_sectional_area = (H - d4) * d3 + (H - d4 - d1) * deff;
                sw.WriteLine("    = (H - d4) * d3 + (H - d4 - h1) * deff");
                sw.WriteLine("    = ({0:f2} - {1:f2}) * {2:f2} + ({0:f2} - {1:f2} - {3:f2}) * {4:F2}",
                    H,
                    d4,
                    d3,
                    h1,
                    deff);
                sw.WriteLine("    = {0:f2} sq.m",
                    total_sectional_area);
                sw.WriteLine("    = {0:f2} sq.mm",
                    total_sectional_area * 1000000);

                _a = (0.12 / 100) * total_sectional_area * 100 * 10e3;

                sw.WriteLine();
                sw.WriteLine("Area of Temperature Steel  = 0.12% = {0:f2} sq.mm", _a);

                _ast = Math.PI * 100 / 4;
                no_bar = (int)(_a / _ast);
                sw.WriteLine();
                sw.WriteLine("Use 10 mm bars, Number of bars = {0:f2}/{1:f2} = {2} nos",
                    _a,
                    _ast,
                    no_bar);




                int front_bar = (int)(no_bar * (2.0 / 3.0));
                int back_fill_bar = (int)(no_bar * (1.0 / 3.0));
                sw.WriteLine();
                sw.WriteLine("Provide {0} bars horizontally on the Front face", front_bar);

                _c = (H - d1 - d4) * 1000 / front_bar;
                sw.WriteLine("    = (H - d1 - d4) * 1000 / {0}", front_bar);
                sw.WriteLine("    = ({0:f2} - {1:f2} - {2:f2}) * 1000 / {3}",
                    H,
                    h1,
                    d4,
                    front_bar);
                sw.WriteLine("    = {0:f2} mm", _c);

                _c = (int)(_c / 10.0);
                _c += 1.0;
                _c *= 10;
                sw.WriteLine("    ≈ {0:f2} mm", _c);




                sw.WriteLine();
                sw.WriteLine("Provide {0} bars horizontally on the Backfill side face", back_fill_bar);

                _c = (H - d4) * 1000 / back_fill_bar;
                sw.WriteLine("    = (H - d4) * 1000 / {0}", back_fill_bar);
                sw.WriteLine("    = ({0:f2} - {1:f2}) * 1000 / {2:f2}",
                    H,
                    d4,
                    back_fill_bar);
                sw.WriteLine("    = {0:f2} mm", _c);

                _c = (int)(_c / 10.0);
                _c += 1.0;
                _c *= 10;
                sw.WriteLine("    ≈ {0:f2} mm c/c", _c);

                _bd2 = 10;
                _sp2 = _c;
                #endregion

                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------            Thank you for using ASTRA Pro          ---------------");
                sw.WriteLine("---------------------------------------------------------------------------");
  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                sw.Flush();
                sw.Close();
                iApp.View_Result(file_name);
            }
        }
        public string FilePath
        {
            set
            {
                //this.Text = "DESIGN OF ABUTMENT : " + value;
                user_path = value;


                //file_path = GetAstraDirectoryPath(user_path);
                file_path = Path.Combine(user_path, "Design of RCC Abutment Underpass");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Underpass_Veh_Abutment.TXT");
                user_input_file = Path.Combine(system_path, "RCC_ABUTMENT_UNDERPASS.FIL");
            }
        }
        public void Write_Drawing_File()
        {
             drawing_path = Path.Combine(system_path, "RCC_ABUTMENT_UNDERPASS_DRAWING.FIL");

            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
            try
            {
                sw.WriteLine("_L1={0}", L1 * 1000);
                sw.WriteLine("_L2={0}", L2 * 1000);
                sw.WriteLine("_L3={0}", L3 * 1000);
                sw.WriteLine("_D={0}", ((L1 + L2 + L3) * 1000));
                sw.WriteLine("_d4={0}", d4 * 1000);
                sw.WriteLine("_H={0}", H * 1000);
                sw.WriteLine("_d1={0}", d1 * 1000);
                sw.WriteLine("_d3={0}", d3 * 1000);
                sw.WriteLine("_L4={0}", L4 * 1000);

                sw.WriteLine("_bd1={0}", _bd1);
                sw.WriteLine("_bd2={0}", _bd2);
                sw.WriteLine("_bd3={0}", _bd3);
                sw.WriteLine("_bd4={0}", _bd4);
                sw.WriteLine("_bd5={0}", _bd5);
                sw.WriteLine("_bd6={0}", _bd6);
                sw.WriteLine("_bd7={0}", _bd7);

                sw.WriteLine("_sp1={0}", _sp1);
                sw.WriteLine("_sp2={0}", _sp2);
                sw.WriteLine("_sp3={0}", _sp3);
                sw.WriteLine("_sp4={0}", _sp4);
                sw.WriteLine("_sp5={0}", _sp5);
                sw.WriteLine("_sp6={0}", _sp6);
                sw.WriteLine("_sp7={0}", _sp7);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void Write_User_input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region USER Data

                sw.WriteLine("d1 = {0} ", d1);
                sw.WriteLine("t = {0}", t);
                sw.WriteLine("H = {0}", H);
                sw.WriteLine("B = {0}", B);
                sw.WriteLine("d2 = {0}", d2);
                sw.WriteLine("d3 = {0}", d3);
                sw.WriteLine("L1 = {0}", L1);
                sw.WriteLine("L2 = {0}", L2);
                sw.WriteLine("L3 = {0}", L3);
                sw.WriteLine("L4 = {0}", L4);
                sw.WriteLine("L = {0}", L);
                sw.WriteLine("d4 = {0:f2}", d4);
                sw.WriteLine("theta = {0}", theta);
                sw.WriteLine("delta = {0}", delta);
                sw.WriteLine("z = {0}", z);
                sw.WriteLine("mu = {0}", mu);
                sw.WriteLine("gamma_b = {0}", gamma_b);
                sw.WriteLine("gamma_c = {0}", gamma_c);
                sw.WriteLine("phi = {0}", phi);
                sw.WriteLine("p = {0}", p);
                sw.WriteLine("f_ck = {0:f0}", f_ck);
                sw.WriteLine("f_y = {0:f0}", f_y);
                sw.WriteLine("w6 = {0:f2}", w6);
                sw.WriteLine("w5 = {0:f2}", w5);
                sw.WriteLine("F = {0:f2}", F);
                sw.WriteLine("factor = {0:f2}", factor);
                sw.WriteLine("cover = {0:f2}", cover);
                #endregion
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }

        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
    public class TopRCCSlab
    {
        #region Variable Declaration

        public string file_path = "";
        public string rep_file_name = "";
        public string user_input_file = "";
        public string user_path = "";
        public string system_path = "";
        public string drawing_path = "";
        public bool is_process = false;



        public double D, CW, FP, L, WC, support_width, conc_grade, st_grade, sigma_cb, sigma_st;
        public double m, j, Q, a1, b1, b2, W1, cover, delta_c, delta_wc;
        CONCRETE_GRADE CON_GRADE;

        bool isProcess = false;

        IApplication iApp = null;

        #region  Drawing Variable
        double _bd1, _bd2, _bd3, _bd4, _bd5, _bd6, _bd7;
        double _sp1, _sp2, _sp3, _sp4, _sp7;

        #endregion

        #endregion

        public TopRCCSlab(IApplication app)
        {
            iApp = app;
            _bd1 = 0.0;
            _bd2 = 0.0;
            _bd3 = 0.0;
            _bd4 = 0.0;
            _bd5 = 0.0;
            _bd6 = 0.0;
            _bd7 = 0.0;
            _sp1 = 0.0;
            _sp2 = 0.0;
            _sp3 = 0.0;
            _sp4 = 0.0;
            _sp7 = 0.0;
        }

        public string FilePath
        {
            set
            {
                user_path = value;

                file_path = Path.Combine(user_path, "Design of Top RCC Slab Underpass");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Underpass_Veh_Slab.TXT");

                user_input_file = Path.Combine(system_path, "TOP_RCC_SLAB_UNDERPASS.FIL");



            }
        }
        public void Calculate_Program(string fileName)
        {

            StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Create));

            try
            {
                #region TechSOFT Banner
                //sw.WriteLine("----------------------------------------------------------------------------------------------");
                //sw.WriteLine("----------------------------------------------------------------------------------------------");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 21             *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*            DESIGN OF TOP RCC SLAB           *");
                sw.WriteLine("\t\t*           FOR VEHICULAR  UNDERPASS          *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                #endregion

                #region User's Data
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Thickness of Slab [D] = {0:f2} mm", D);
                sw.WriteLine("Carriageway width [CW] = {0:f2} m", CW);
                sw.WriteLine("Footpath width [FP] = {0:f2} m", FP);
                sw.WriteLine("Clear Span [L] = {0:f2} m", L);
                sw.WriteLine("Thickness of Wearing Course [WC] = {0:f2} mm", WC);
                sw.WriteLine("Width of End Support / Bearing = {0:f2} m", support_width);
                sw.WriteLine("Concrete Grade = M {0:f0} ", conc_grade);
                sw.WriteLine("Steel Grade = Fe {0:f0} ", st_grade);
                sw.WriteLine("Permissible Stress [σ_cb] = {0:f2} N/sq. mm", sigma_cb);
                sw.WriteLine("Permissible Stress [σ_st] = {0:f2} ", sigma_st);
                sw.WriteLine("Modular Ratio [m] = {0:f2} ", m);
                sw.WriteLine("j = {0:f2} ", j);
                sw.WriteLine("Q = {0:f2} ", Q);
                sw.WriteLine("Live Load Dimension [a1] = {0:f2} m", a1);
                sw.WriteLine("Live Load Dimension [b1] = {0:f2} m", b1);
                sw.WriteLine("Live Load Dimension [b2] = {0:f2} m", b2);
                sw.WriteLine("Total Live Load [W1] = {0:f2} kN", W1);
                sw.WriteLine("Clear cover to Reinforcement Bars [cover] = {0:f2} mm", cover);
                sw.WriteLine("Unit weight of Concrete [γ_c] = {0:f2} N/cu.m", delta_c);
                sw.WriteLine("Unit weight of Wearing course [γ_wc] = {0:f2} N/cu.m", delta_wc);
                sw.WriteLine();
                sw.WriteLine();



                #endregion


                #region Page2
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Calculating Effective Span ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Overall thickness of Slab = {0:f2} mm = D", D);

                double deff = D - (cover + 10);
                sw.WriteLine("Effective thickness of Slab = {0:f2} - ({1:f2} + 10) = {2:f2} mn = deff",
                    D,
                    cover,
                    deff);
                sw.WriteLine();

                sw.WriteLine("Effective span is lesser of");
                double total_span = L + (deff / 1000);
                sw.WriteLine("  i)   Clear Span + Effective Depth = {0:f2} + {1:f2} = {2:f2} m",
                    L, (deff / 1000), total_span);

                double l = L + support_width;
                sw.WriteLine("  ii)  Centre to Centre distance of End Supports / Bearings = {0:f2} + {1:f2} = {2:f2} m",
                    L, support_width, l);


                sw.WriteLine();
                sw.WriteLine("  So, Effective Span = {0:f2} m = l", l);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Bending Moment by Permanent Loads ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double slab_weight = (D / 1000.0) * delta_c;

                sw.WriteLine("Weight of Slab = (D /1000) * γc = {0:f3} * {1:f2} = {2:f2} kN/sq.m.",
                    (D / 1000.0),
                    delta_c,
                    slab_weight);

                double wt_wear_cour = (WC / 1000) * delta_wc;

                sw.WriteLine("Weight of Wearing Course = (WC /1000) * γwc = {0:f3} * {1} = {2:f3} kN/sq.m.",
                    (WC / 1000.0), delta_wc, wt_wear_cour);


                double W2 = (int)(slab_weight + wt_wear_cour);
                W2 = W2 + 1;

                sw.WriteLine();
                sw.WriteLine("Total Load = {0:f3} kN/sq.m = {1} kN/sq.m. = W2",
                    (slab_weight + wt_wear_cour), W2);

                double M1 = (W2 * l * l) / 8;

                sw.WriteLine();
                sw.WriteLine("Bending Moment for Permanent Loads = M1 = ({0:f2} * l*l) / 8", W2);
                sw.WriteLine("                                    = ({0:f2} * {1:f2}*{1:f2}) / 8 ",
                    W2, l);
                sw.WriteLine("                                    = {0:f2} kN-m", M1);

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Bending Moment by Vehicle Load / Live Load ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("For 5m Span Impact Factor 25%");
                sw.WriteLine("For 9m Span Impact Factor 10%");

                sw.WriteLine();
                sw.WriteLine("So, for {0:f2} m Span Impact factor ", l);
                //double im_fact = 25 - ((25 - 10) / (9 - 5)) * (6.4 - 5);


                double fact = 25.0 - ((25.0 - 10.0) / (9.0 - 5.0)) * (6.4 - 5);

                //double fact = 25 - ((25 - 10) / (9 - 5)) * (l - 5);
                sw.WriteLine("  = 25 - ((25 - 10) / (9 - 5)) * ({0:f2} - 5) = {1:f2}% = fact", l, fact);

                sw.WriteLine();
                sw.WriteLine("Length of Load = a1 = {0:f2} m.", a1);

                double ld = a1 + 2 * ((D + WC) / 1000);

                sw.WriteLine("Length of Load including 45° dispersal = a1 + 2 * ((D + WC) / 1000)");
                sw.WriteLine("                                       = {0:f2} + 2 * (({1:f2} + {2:f2}) / 1000)",
                    a1, D, WC);
                sw.WriteLine("                                       = {0:f2} m. = ld", ld);

                sw.WriteLine();
                sw.WriteLine("Effective Width of Slab perpendicular to Span = be = K * x * (1 - (x / L)) + bw");

                sw.WriteLine();
                sw.WriteLine("Placing the Load symmetrically on the Span");

                double x = l / 2;
                sw.WriteLine("x = Distance from centre of end support to centre of Load = l/2 = {0:f2}/2={1:f2} m.", l, x);

                double B = CW + (2 * FP);
                sw.WriteLine("B = Width of Slab = CW + (2 * FP) =  {0:f2} + (2 * {1:f2}) = {2:f2} m",
                    CW, FP, B);


                double b_by_l = B / l;
                sw.WriteLine();
                sw.WriteLine("B / l = {0:f2} / {1:f2} = {2:f2}", B, l, b_by_l);
                double bw = b1 + (2 * (WC / 1000));

                sw.WriteLine("bw = b1 + (2 * (WC / 1000)) = {0:f3} + (2 * {1:f3}) = {2:f3} m.",
                    b1, (WC / 1000.0), bw);

                sw.WriteLine();
                sw.WriteLine("From Table of IRC 21:2000 ");
                sw.WriteLine();

                double K = KValue.Get_K_Value(b_by_l);
                //double K = 2.84;

                sw.WriteLine();
                sw.WriteLine("For B / l = {0:f3}, for simply Supported Slab,  K = {1:f3}", b_by_l, K);
                double be = K * x * (1 - (x / l)) + bw;

                //sw.WriteLine("So, Effective Width of Load = be = 2.84 * 3.2 * {1 - (3.2 / 6.4)} + 1.01 = 5.56 m.");
                sw.WriteLine();
                sw.WriteLine("So, Effective Width of Load = be ");
                sw.WriteLine("                            = {0:f2} * {1:f2} * (1 - ({1:f2} / {2:f2})) + {3:f2}",
                    K, x, l, bw);
                sw.WriteLine("                            = {0:f2} m.", be);

                double Wd = (2 * (be / 2)) + (2 * (b1 / 2)) + b2;

                sw.WriteLine();
                sw.WriteLine("Width of Load with 45 dispersal = Wd");
                sw.WriteLine("                                =(2 * (be / 2)) + (2 * (b1 / 2)) + b2");
                sw.WriteLine("                                =(2 * ({0:f2}) / 2)) + (2 * ({1:f2} / 2)) + {2:f3}",
                    be, b1, b2);
                sw.WriteLine("                                = {0:f3} m", Wd);

                #endregion

                #region Page 3

                double TLL = W1 * ((fact / 100.0) + 1.0);
                sw.WriteLine();
                sw.WriteLine("Total Live Load including Impact = TLL");
                sw.WriteLine("                                 = W1 * ((fact/ 100.0) + 1.0) kN");
                sw.WriteLine("                                 = {0:f2} * ({1:f2}) ", W1, (fact * L / 100));
                sw.WriteLine("                                 = {0:f0} kN", TLL);

                double LLUA = TLL / (ld * Wd); // live load unit area
                sw.WriteLine();
                sw.WriteLine("Live Load per Unit Area = LLUA");
                sw.WriteLine("                        = TLL / (ld * wd)");
                sw.WriteLine("                        = {0:f0} / ({1:f2} * {2:f2})", TLL, ld, Wd);

                sw.WriteLine("                        = {0:f2} kN/sq.m.", LLUA);

                double M2 = ((LLUA * ld) / 2) * (l / 2) - ((LLUA * ld) / 2) * (ld / 4);

                sw.WriteLine();
                sw.WriteLine("Bending Moment for Live Load = M2");
                sw.WriteLine("                             = ((LLUA * ld) / 2) * (l / 2) - ((LLUA * ld) / 2) * (ld / 4)");
                sw.WriteLine("                             = (({0:f2}*{1:f2})/2) * ({2:f2}/2) - (({0:f2} * {1:f2})/2) * ({1:f2}/4)",
                    LLUA, ld, l);
                sw.WriteLine("                             = {0:f2} kN-m", M2);


                double M = M1 + M2;

                sw.WriteLine();
                sw.WriteLine("Design Bending Moment = M = M1 + M2 ");
                sw.WriteLine("                          = {0:f2} + {1:f2}", M1, M2);
                sw.WriteLine("                          = {0:f2} kN-m", M);

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : Shear Force by Live Load ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Effective Span = l = {0:f2} m", l);
                sw.WriteLine("Length of Load including 45° dispersal = ld = {0:f2} m", ld);
                sw.WriteLine();
                sw.WriteLine("To get maximum Shear Force at support let us place the Load ");
                sw.WriteLine("coinciding the start point of the above lengths");

                x = ld / 2;

                sw.WriteLine();
                sw.WriteLine("x = ld / 2 = {0:f2} / 2 = {1:f2} m.", ld, x);
                b_by_l = B / l;

                sw.WriteLine("B / l = {0:f2} / {1:f2} = {2:f2}", B, l, b_by_l);
                K = KValue.Get_K_Value(b_by_l);
                sw.WriteLine();
                sw.WriteLine("From IRC 21:2000, K = {0:f3}", K);
                sw.WriteLine("bw = {0:f2} m", bw);

                be = K * x * (1 - (x / l)) + bw;

                sw.WriteLine();
                sw.WriteLine("Effective width of Load = be");
                sw.WriteLine("                        = K * x * (1 - (x / l)) + bw");
                sw.WriteLine("                        = {0:f2} * {1:f2} * (1 - ({1:f2}/{2:f2})) + {3:f3}",
                    K, x, l, bw);
                sw.WriteLine("                        = {0:f2} m", be);

                sw.WriteLine();
                sw.WriteLine("Width of Load with 45° dispersal = Wd");
                sw.WriteLine("                                 =((2 * be) / 2) + ((2 * b1) / 2) + b2");
                sw.WriteLine("                                 =((2 * {0:f2}) / 2) + ((2 * {1:f2}) / 2) + {2:f3}",
                    be, b1, b2);
                sw.WriteLine("                                 = {0:f3} m", Wd);

                LLUA = TLL / (ld * Wd); // live load unit area
                sw.WriteLine();
                sw.WriteLine("Live Load per Unit area = TLL / (ld * wd)");
                sw.WriteLine("                        = {0:f2} / ({1:f2} * {2:f2})", TLL, ld, Wd);
                sw.WriteLine("                        = {0:f2} kN/sq.m.", LLUA);

                double V1 = (LLUA * ld * 2 * (b1 + 2 * (WC / 1000) + 2 * (D / 1000)) / l);

                sw.WriteLine();
                sw.WriteLine("Shear Force by Live Load = V1");
                sw.WriteLine("                         = [LLUA * ld * 2 * (b1 + 2 * (wc / 1000) + 2 * (D / 1000)]/l");
                sw.WriteLine("                         = [{0:f2} * {1:f2} * 2 * ({2:f2} + 2 * {3:f3} + 2 * {4:f3}] / {5:f3}",
                    LLUA, ld, b1, (WC / 1000.0), (D / 1000), l);
                sw.WriteLine("                         = {0:f3} kN", V1);


                double V2 = W2 * l / 2;
                sw.WriteLine();
                sw.WriteLine("Dead Load Shear = V2 = W2 * l / 2 ");
                sw.WriteLine("                     = ({0:f2} * {1:f2}) / 2 ", W2, l);
                sw.WriteLine("                     = {0:f2} kN", V2);

                double V = V1 + V2;

                sw.WriteLine();
                sw.WriteLine("Total Design Shear = V = V1 + V2 kN");
                sw.WriteLine("                       = {0:f2} + {1:f2} kN", V1, V2);
                sw.WriteLine("                       = {0:f2} kN", V);

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : Structural Design of Slab ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double d = Math.Sqrt((M * 10E5) / (Q * 1000));
                sw.WriteLine("Required Effective Depth = d = √((M * 10E5) / (Q * b))");
                sw.WriteLine("                             = √(({0:f2} * 10E5) / ({1:f2} * 1000))",
                    M, Q);
                sw.WriteLine("                             = {0:f2} m", d);
                sw.WriteLine();


                #endregion

                #region Page 4

                sw.WriteLine();
                if (deff > d)
                {
                    sw.WriteLine("Effective depth provided = deff = {0:f2} > d = {1:f2} OK", deff, d);
                }
                else
                {
                    sw.WriteLine("Effective depth provided = deff = {0:f2} < d = {1:f2} NOT OK", deff, d);
                }

                double Ast = (M * 10E5) / (sigma_st * j * d);
                sw.WriteLine();
                sw.WriteLine("Required Steel reinforcement = Ast = ( M * 10E5) / (σ_st * j * d)");
                sw.WriteLine("                                   = ({0:f2} * 10E5) / ({1:f2} * {2:f3} * {3:f2})",
                    M,
                    sigma_st, j, d);
                sw.WriteLine("                                   = {0:f3} sq.mm", Ast);


                double spacing = (1000 * Math.PI * 20 * 20 / 4.0) / Ast;

                sw.WriteLine();
                sw.WriteLine("Using 20 mm dia. Bars, spacing = [1000 * ((π * 20 * 20) / 4)] / {0:f2}", Ast);
                sw.WriteLine("                               = {0:f2} mm", spacing);

                if (spacing > 50 && spacing < 200)
                {
                    spacing = (int)(spacing / 10.0); ;
                    spacing = (spacing * 10.0); ;
                }
                else if (spacing < 50)
                {
                    sw.WriteLine("Spacing {0:f0} mm c/c < 50 mm c/c , NOT OK, Redesign", spacing);
                }
                else
                {
                    spacing = 200;
                }
                sw.WriteLine();
                sw.WriteLine("Provide T20 @ {0:f0} mm c/c", spacing);

                _bd1 = 20;
                _sp1 = spacing;

                double BMDS = 0.2 * M1 + 0.3 * M2;

                sw.WriteLine();
                sw.WriteLine("Bending Moment for Distribution Steel = 0.2 * M1 + 0.3 * M2");
                sw.WriteLine("                                      = 0.2 * {0:f2} + 0.3 * {1:f2}", M1, M2);
                sw.WriteLine("                                      = {0:f3} kN-m", BMDS);
                BMDS = (int)BMDS;
                BMDS += 1.0;

                sw.WriteLine("                                      = {0:f2} kN-m", BMDS);

                double e_dep = deff - 10.0 - 10.0 / 2.0;
                sw.WriteLine();
                sw.WriteLine("Using 12 mm. dia Bars, Effective Depth = deff - (10 + 6) = {0:f2} mm", e_dep);

                double req_steel = BMDS * 10E5 / (sigma_st * j * e_dep);

                sw.WriteLine();
                sw.WriteLine("Required Steel = ({0:f2} * 10E5) / ( {1:f3} * {2:f3} * {3:f2}) = {4:f3} sq.mm",
                    BMDS, sigma_st, j, e_dep, req_steel); ;

                spacing = (1000 * Math.PI * 12 * 12) / (req_steel * 4);
                sw.WriteLine();
                sw.WriteLine("Spacing of bars = (1000 * π * 12 * 12) / ({0:f2} * 4)", req_steel);
                sw.WriteLine("                = {0:f3} mm", spacing);

                if (spacing > 150)
                    spacing = 150;
                else
                {
                    spacing = (int)(spacing / 10.0);
                    spacing = (spacing * 10.0);

                }

                sw.WriteLine();
                sw.WriteLine("Provide T12 @ {0:f0} mm c/c as distribution Steel.", spacing);

                _bd2 = 12;
                _sp2 = spacing;
                #endregion

                #region Page 5
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : Shear Reinforcements ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double tau = V * 10E2 / (1000 * deff);

                sw.WriteLine("Design Shear = τ = (V * 10E2) / (b * deff) ");
                sw.WriteLine("                 = ({0:f2} * 10E2) / (1000 * {1:f2}) N/sq.mm", V, deff);
                sw.WriteLine("                 = {0:f3} N/sq.mm", tau);
                sw.WriteLine();
                double K1 = 1.14 - 0.7 * (deff / 1000); // 1.14 = ?, 0.7 = ?
                if (K1 >= 0.5)
                {
                    sw.WriteLine("K1 = 1.14 - 0.7 * (deff / 1000) = 1.14 - 0.7 * {0:f3} = {1:f3} >= 0.5, O.K.",
                        (deff / 1000.0),
                        K1);
                }
                else
                {
                    sw.WriteLine("K1 = 1.14 - 0.7 * (deff / 1000) = 1.14 - 0.7 * {0:f3} = {1:f3} < 0.5, NOT OK",
                        (deff / 1000.0),
                        K1);
                }

                sw.WriteLine();
                sw.WriteLine("Bending up alternate bars, provide T20 bars @ 280 mm c/c");



                double ast_prov = (Math.PI * 20 * 20 / 4.0) * (1000.0 / 280.0);
                sw.WriteLine();
                sw.WriteLine("Ast provided = {0:f0} sq.mm", ast_prov);

                double p = (ast_prov * 100) / (1000 * deff);

                sw.WriteLine();
                sw.WriteLine("Percentage p = ({0:f0} * 100) / (1000 * {1:f2}) = {2:f3}%",
                    ast_prov, deff, p);

                double K2 = 0.5 + 0.25 * p;
                sw.WriteLine();
                if (K2 < 1.0)
                {
                    sw.WriteLine("K2 = 0.5 + 0.25 * p = 0.5 + 0.25 * {0:f3} = {1:f3} < 1.0,  NOT OK.", p, K2);
                    K2 = 1.0;
                    sw.WriteLine("So, K2 = 1.0");

                }
                else
                {
                    sw.WriteLine("K2 = 0.5 + 0.25 * p = 0.5 + 0.25 * {0:f3} = {1:f3} >= 1.0, OK.", p, K2);
                }

                sw.WriteLine();
                sw.WriteLine("----------------------------------------------------------");
                sw.WriteLine("Concrete Grade (M)  15     20     25     30     35     40 ");
                sw.WriteLine("τ_co               0.28   0.34   0.40   0.45   0.50   0.50");
                sw.WriteLine("----------------------------------------------------------");
                sw.WriteLine();
                double tau_co = 0.0;
                CON_GRADE = (CONCRETE_GRADE)(int)(conc_grade);

                switch (CON_GRADE)
                {
                    case CONCRETE_GRADE.M15:
                        tau_co = 0.28;
                        break;
                    case CONCRETE_GRADE.M20:
                        tau_co = 0.34;
                        break;
                    case CONCRETE_GRADE.M25:
                        tau_co = 0.40;
                        break;
                    case CONCRETE_GRADE.M30:
                        tau_co = 0.45;
                        break;
                    case CONCRETE_GRADE.M35:
                        tau_co = 0.50;
                        break;
                    case CONCRETE_GRADE.M40:
                        tau_co = 0.50;
                        break;
                }

                sw.WriteLine("Therefore, Permissible Shear Stress");
                double tau_c = K1 * K2 * tau_co;
                sw.WriteLine("τ_c = K1 * K2 * τ_co");
                sw.WriteLine("    = {0:f3} * {1:f3} * {2:f3}", K1, K2, tau_co);
                if (tau_c > tau)
                    sw.WriteLine("    = {0:f3} > τ = {1:f3} N/sq.mm, OK", tau_c, tau);
                else
                    sw.WriteLine("    = {0:f3} < τ = {1:f3} N/sq.mm, NOT OK, more Steel will required.", tau_c, tau);


                //sw.WriteLine("      = 0.80 * 1.0 * 0.40 = 0.328 N / Sq.mm. > τ = 0.254 N / Sq.mm.
                //sw.WriteLine("So, O.K.
                //sw.WriteLine("Else not O.K.. more Steel will be required.

                #endregion
                #region End of Report
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------            Thank you for using ASTRA Pro          ---------------");
                sw.WriteLine("---------------------------------------------------------------------------");
  
                #endregion
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
            iApp.View_Result(fileName);


            #region Page 1 USER DATA
            //Thickness of Slab (Assumed) = 500 mm
            //Carriageway width   = 7.5 m = CW
            //Footpath width         = 1.0 m = FP
            //Clear Span = 6.0 m

            //Thickness of Wearing Course = 80 mm = WC
            //Width of End Support / Bearing = 0.4 m
            //Concrete Grade = M25
            //Steel Grade = Fe 415
            //Permissible Stress σcb = 8.3 N/sq. mm.
            //Permissible Stress σst = 200 N/sq. mm

            //Modular Ratio m = 10

            //j = 0.9
            //Q = 1.1

            //Live Load Dimension a1 = 3.6 m
            //Live Load Dimension b1 = 0.85 m
            //Live Load Dimension b2 = 1.20 m

            //Total Live Load W1 = 700 KN

            //Clear cover to Reinforcement Bars = 30 mm’
            //Unit weight of Concrete = δc = 24 KN/cu.m.
            //Unit weight of Wearing course = δwc = 22 KN / cu.m.
            #endregion

            #region Page2
            //            Overall depth of Slab = 500 mm = D
            //Effective depth of Slab = 500 - (cover + 10) = 460 mn = d eff.

            //Effective span is lesser of

            //i)                Clear Span + Effective Depth = 6 + 0.46 = 6.46 m
            //ii)                Centre to Centre distance of End Supports / Bearings = 6 + 0.4 = 6.4 m

            //So, Effective Span = 6.4 m = l

            //STEP 2:

            //Bending Moment by Permanent Loads
            //Weight of Slab = (D /1000) * δc = 0.5 * 24 = 12 KN/sq.m.
            //Weight of Wearing Course = (WC /1000) * δwc = 0.080 * 22 = 1.76 KN/sq.m.
            //Total Load = 13.76 NK/sq.m. - 14 KN/sq.m. = W2
            //Bending Moment for Permanent Loads = M1 = (14 * l2) / 8 = (14 * 6.42) / 8 = 72 KN m.

            //STEP 3:

            //Bending Moment by Vehicle Load / Live Load

            //For 5m Span Impact Factor 25%
            //For 9m Span Impact Factor 10%

            //So, for 6.4m Span Impact factor 
            //= 25 - {(25 - 10) / (9 - 5)} * (6.4 - 5) = 25 - (15 / 4) * 1.4 = 19.7% = If
            //Length of Load = a1 = 3.6 m.
            //Length of Load including 45° dispersal = 3.6 + 2 * {(D + WC) / 1000}
            //                        = 3.6 + 2 * (0.58) = 4.76 m. = ld

            //Effective Width of Slab perpendicular to Span = be = Kx * {1 - (x / L)} + bw

            //Placing the Load symmetrically on the Span

            //x = Distance from centre of end support to centre of Load = l / 2 = 6.4 / 2 = 3.2 m.
            //B = Width of Slab = CW + (2 * FP) =  7.5 + 2 = 9.5 m
            //B / l = 9.5 / 6.4 = 1.48
            //bw = b1 + {2 * (WC / 1000)} = 0.85 + (2 * 0.08) = 1.01 m.

            //From Table of IRC 21:2000 (given at the end of this design)

            //For B / l = 1.48, for simply Supported Slab,  K = 2.84
            //So, Effective Width of Load = be = 2.84 * 3.2 * {1 - (3.2 / 6.4)} + 1.01 = 5.56 m.

            //Width of Load with 45 dispersal = 2 * (5.56 / 2) + 2 * (0.85 / 2) + 1.2
            //                     = {(2 * be) / 2} + {(2 * b1) / 2} + b2
            //                     = 7.61 m = Wd


            #endregion


            #region Page 3
            //            Total Live Load including Impact = W1 * (IF / 100) = 700 * 1.197 = 838 KN
            //Live Load per Unit area = 838 / (ld * wd) = 838 / (4.76 * 7.61) = 23.134 KN / sq.m.

            //Bending Moment for Live Load = M2 = {(23.14 * ld) / 2} * (l / 2) - {(23.14 * ld) / 2) * (ld / 4)
            //                      = {(23.14 * 4.76) / 2} * 3.2 - {(23.14 * 4.76) / 2) * 1.19
            //                      = 110.7 KN m.

            //Design Bending Moment = M = M1 + M2 = 72 + 110.7 = 182.7 = 185 KN m.

            //STEP 4:

            //Shear Force by Live Load
            //Effective Span = l = 6.4m.
            //Length of Load including 45° dispersal = ld = 4.76 m.
            //To get maximum Shear Force at support let us place the Load coinciding the start point of the above lengths

            //x = ld / 2 = 4.76 / 2 = 2.38 m.
            //B / l = 9.5 / 6.4 = 1.48

            //From IRC 21:2000, K = 2.84
            //bw = 1.01m.

            //Effective width of Load = be = Kx * {1 - (x / L)} + bw
            //                = 2.84 * 2.38 * {1 - (2.38 / 6.4)}+ 1.01
            //                = 5.256 m.

            //Width of Load with 45° dispersal = 2 * (5.256 / 2) + 2 * (0.85 / 2) + 1.2 = 7.3 M. = wd

            //Live Load per unit area = 838 / (ld * wd) = 838 / (4.76 * 7.3) = 24.1 KN / sq.m.

            //Shear Force by Live Load = [24.1 * 4.76 * 2 * (b1 + 2 * (wc / 1000) + 2 * (D / 1000)] / l
            //                   = [24.1 * 4.76 * 2 * {0.85 + (2 * 0.08) + 1}] / 6.4
            //                   = 72 KN = V1

            //Dead Load Shear = (W2 * l) / 2 = (14 * 6.4) / 2 = 45 KN = V2

            //Total Design Shear = V = V1 + V2 = 72 + 45 = 117 KN

            //STEP 5:

            //Structural Design of Slab.
            //                      ________________
            //Required Effective Depth = d = √ ( M * 106) / (Q * b)
            //                      _____________________
            //                = √ ( 185 * 106) / (1.1 * 1000) = 410 mm

            #endregion

            #region Page 4
            //            Effective depth provided = d eff = 460 > d = 410, O.K.

            //Required Steel reinforcement = Ast = ( M * 106) / (σst * j * d)
            //                          = (185 * 106) / ( 200 * 0.9 * 460)
            //                          = 2234 Sq. mm.

            //Using 20 mm dia. Bars, spacing = [1000 * {(π * 20 * 20) / 4}] / 2234
            //                    = 140 mm

            //Provide T20 @ 140 mm c/c,

            //Bending Moment for Distribution Steel = 0.2 * M1 + 0.3 * M2
            //                        = 0.2 * 72 + 0.3 * 110
            //                        = 47.4
            //                        = 48 KN m.

            //Using 12 mm. diaBars, Effective Depth = deff - (10 + 6) = 444 mm

            //Required Steel = (48 * 106) / ( 200 * 0.9 * 444) = 600.6 Sq. mm.

            //Spacing of bars = (1000 * π * 12 * 12) / (600.6 * 4) = 188.3 mm

            //Provide T12 @ 150 mm c/c as distribution Steel.

            #endregion

            #region Page 5
            //STEP 6:    Shear Reinforcements
            //Design Shear = τ = V / bd eff = (117 * 103) / (1000 * 460) = 0.254 N / Sq. mm.
            //K1 = 1.14 - 0.7 * (d eff / 1000) = 1.14 - 0.7 * 0.460 = 0.82 >= 0.5, O.K.
            //Bending up alternate bars, provide T20 bars @ 280 mm c/c
            //Ast provided = 1122 Sq. mm.
            //Percentage p = (1122 * 100) / (1000 * 460) = 0.243%
            //K2 = 0.5 + 0.25 * p = 0.5 + 0.25 * 0.243 = 0.560 < 1.0, Not O.K.
            //So, K2 = 1.0
            //Concrete Grade (M)                15                20                25                30                35                40
            // τ co                                                0.28                0.34                0.40                0.45                0.50                0.50
            //Therefore, Permissible Shear Stress
            //τ c = K1 * K2 * τ co
            //      = 0.80 * 1.0 * 0.40 = 0.328 N / Sq.mm. > τ = 0.254 N / Sq.mm.
            //So, O.K.
            //Else not O.K.. more Steel will be required.

            #endregion

        }
        public void Write_Drawing_File()
        {
            drawing_path = Path.Combine(system_path, "TOP_RCC_SLAB_UNDERPASS_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));

            #region Variable
            //string _box1 = "1/3 x 3/5";
            //string _box2 = "[1/3 x 3/5]";

            double _A, _B, _C, _D, _E;
            //double _bd1, _bd2, _bd3, _bd4, _bd5, _bd6, _bd7;
            //double _sp1, _sp2, _sp3, _sp4, _sp7;

            _A = L * 1000;
            _B = D;
            _C = WC;
            _D = CW * 1000;
            _E = support_width * 1000;

            //_bd1 = 0.0;
            //_bd2 = 0.0;
            _bd3 = 10;
            _bd4 = 10;
            _bd5 = 20;
            _bd6 = 20;
            _bd7 = 10;
            //_sp1 = 0.0;
            //_sp2 = 0.0;
            _sp3 = 300;
            _sp4 = 300;
            _sp7 = 300;
            #endregion

            try
            {

                sw.WriteLine("_A={0}", _A);
                sw.WriteLine("_B={0}", _B);
                sw.WriteLine("_C={0}", _C);
                sw.WriteLine("_D={0}", _D);
                sw.WriteLine("_E={0}", _E);

                sw.WriteLine("_bd1=T{0:f0}", _bd1);
                sw.WriteLine("_bd2=T{0:f0}", _bd2);
                sw.WriteLine("_bd3=T{0:f0}", _bd3);
                sw.WriteLine("_bd4=T{0:f0}", _bd4);
                sw.WriteLine("_bd5=T{0:f0}", _bd5);
                sw.WriteLine("_bd6=T{0:f0}", _bd6);
                sw.WriteLine("_bd7=T{0:f0}", _bd7);

                sw.WriteLine("_sp1={0:f0} c/c", _sp1);
                sw.WriteLine("_sp2={0:f0} c/c", _sp2);
                sw.WriteLine("_sp3={0:f0} c/c", _sp3);
                sw.WriteLine("_sp4={0:f0} c/c", _sp4);
                sw.WriteLine("_sp7={0:f0} c/c", _sp7);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void WriteUserInput()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {
                sw.WriteLine("D = {0:f2}", D);
                sw.WriteLine("CW = {0:f2}", CW);
                sw.WriteLine("FP = {0:f2}", FP);
                sw.WriteLine("L = {0:f2}", L);
                sw.WriteLine("WC = {0:f2}", WC);
                sw.WriteLine("support_width = {0:f2}", support_width);
                sw.WriteLine("conc_grade = {0:f2}", conc_grade);
                sw.WriteLine("st_grade = {0:f2}", st_grade);
                sw.WriteLine("sigma_cb = {0:f2}", sigma_cb);

                sw.WriteLine("sigma_st = {0:f2}", sigma_st);
                sw.WriteLine("m = {0:f2}", m);
                sw.WriteLine("j = {0:f2}", j);
                sw.WriteLine("Q = {0:f2}", Q);
                sw.WriteLine("a1 = {0:f2}", a1);
                sw.WriteLine("b1 = {0:f2}", b1);
                sw.WriteLine("b2 = {0:f2}", b2);
                sw.WriteLine("W1 = {0:f2}", W1);
                sw.WriteLine("cover = {0:f2}", cover);
                sw.WriteLine("delta_c = {0:f2}", delta_c);
                sw.WriteLine("delta_wc = {0:f2}", delta_wc);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

    }
    public class RccBoxStructure
    {

        public string rep_file_name = "";
        public string file_path = "";
        public string user_input_file = "";
        public string system_path = "";
        public string drawing_path = "";
        public string user_path = "";



        public bool is_process = false;

        #region Variable Initialization

        public double H, b, d, d1, d2, d3, gamma_b, gamma_c, R, t, j, cover;
        public double b1, b2, a1, w1, w2, b3, F, S, sbc, sigma_st, sigma_c;
        public CONCRETE_GRADE Con_Grade;

        public List<double> lst_Bar_Dia = null;
        public List<double> lst_Bar_Space = null;

        #endregion

        #region Drawing Variables

        public double bd1, bd2, bd3, bd4, bd5, bd6, bd7, bd8, bd9, bd10, bd11, bd12, bd13, bd14, bd15;
        public double sp1, sp2, sp3, sp4, sp5, sp6, sp7, sp8, sp9, sp10, sp11, sp12, sp13, sp14, sp15;
        public double _pressure = 0.0;

        #endregion

        IApplication iApp = null;

        public RccBoxStructure(IApplication app)
        {
            this.iApp = app;
            //St_Grade = TAU_C.STEEL_GRADE.M25;
            lst_Bar_Dia = new List<double>();
            lst_Bar_Space = new List<double>();
            lst_Bar_Dia.Add(0);
            lst_Bar_Space.Add(0);
        }

        public string FilePath
        {
            set
            {
                user_path = value;

                file_path = Path.Combine(user_path, "Design of RCC Box Structure");
                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);


                rep_file_name = Path.Combine(file_path, "Underpass_Ped_Box.TXT");
                user_input_file = Path.Combine(system_path, "RCC_BOX_STRUCTURE.FIL");
            }
        }

        public void Calculate_Program(string file_name)
        {
            string ref_string = "";
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 21             *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*         DESIGN OF RCC BOX STRUCTURE         *");
                sw.WriteLine("\t\t*          FOR PEDESTRIAN UNDERPASS           *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine();

                #endregion

                #region USER INPUT DATA
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}    Marked as (H) in the Drawing",
                    "Height of Earth Cushion = H",
                    H,
                    "m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}    Marked as (b) in the Drawing",
                    "Inside Clear Width = b",
                    b,
                    "m");

                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}    Marked as (d) in the Drawing",
                    "Inside Clear Depth = d",
                    d,
                    "m");


                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}   Marked as (d1) in the Drawing",
                    "Thickness of Top Slab = d1",
                    d1,
                    "m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}   Marked as (d2) in the Drawing",
                    "Thickness of Bottom Slab = d2",
                    d2,
                    "m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}   Marked as (d3) in the Drawing",
                    "Thickness of Side Walls = d3",
                    d3,
                    "m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Unit weight of Earth = γ_b",
                    gamma_b,
                    "kN/cu.m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Unit weight of Concrete = γ_c",
                    gamma_c,
                    "kN/cu.m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "R",
                    R,
                    "");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "t",
                    t,
                    "");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Lever Arm Factor = j",
                    j,
                    "");

                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Clear Cover = cover",
                    cover,
                    "mm");

                sw.WriteLine("{0,50} = {1,12} {2,-5}",
                    "Concrete Grade",
                    "M" + sigma_c.ToString("0"),
                    "");
                sw.WriteLine("{0,50} = {1,12} {2,-5}",
                    "Steel Grade",
                    "Fe " + sigma_st.ToString("0"),
                    "");


                // For single Track Loading in 2-Lane

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("For single Track Loading in 2-Lane");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                  "Total Load = w1",
                  w1,
                  "kN");

                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Separating Distance of two loads = b1",
                    b1,
                    "m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Width of each loaded Area",
                    b2,
                    "m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Length of each Loaded Area = b2",
                    a1,
                    "m");

                // FOR Double Track Loading in 4-Lane
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("For Double Track Loading in 4-Lane");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Total Load = w2",
                    w2,
                    "kN");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Separating Distance between two tracks = b3",
                    b3,
                    "m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Two track Load dispersion factor = F",
                    F,
                    "");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Equivalent Earth height for Live Load Surchage = S",
                    S,
                    "m");
                sw.WriteLine("{0,50} = {1,12:f3} {2,-5}",
                    "Safe bearing capacity of Ground = sbc",
                    sbc,
                    "kN/sq.m.");

                sw.WriteLine();
                sw.WriteLine();
                #endregion

                #region STEP 1
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.0 : LOAD CALCULATION");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                #region STEP 1.1

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("1.1 LOAD ON TOP SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Permanent Loads");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double earth_cusion = H * gamma_b;
                sw.WriteLine("    Earth Cushion = H * γ_b = {0:f2} * {1:f3} = {2:f2} kN/sq.m",
                    H,
                    gamma_b,
                    earth_cusion);

                double self_weight_top_slab = d1 * gamma_c;

                sw.WriteLine("    Self weight of Top Slab = d1 * γ_c ");
                sw.WriteLine("                            = {0:f3} * {1:f3}", d1, gamma_c);
                sw.WriteLine("                            = {0:f2} kN/sq.m", self_weight_top_slab);

                double p1 = earth_cusion + self_weight_top_slab;

                sw.WriteLine();
                sw.WriteLine("    Total Permanent Load per unit area for one track = p1");
                sw.WriteLine("                                                 = {0:f3} + {1:f3}",
                                    earth_cusion, self_weight_top_slab);
                sw.WriteLine("                                                 = {0:f3} kN/sq.m", p1);

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Live Load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("For One Track Load Covering 2-Lane");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double width_loaded_area = b1 + b2 + b2 + H + H;
                //double a = b1 + b2 + b2 + H + H;
                sw.WriteLine("    Width of Loaded Area including 45° dispersion");
                sw.WriteLine("      = a = b1 + b2 + b2 + H + H");
                sw.WriteLine("      = {0:f3} + {1:f3} + {1:f3} + {2:f3} + {2:f3}",
                    b1, b2, H);
                sw.WriteLine("      = {0:f3} m", width_loaded_area);

                double length_loaded_area = a1 + H + H;
                sw.WriteLine();
                sw.WriteLine("    Length of Loaded Area including 45° dispersion");
                sw.WriteLine("       = b = a1 + H + H");
                sw.WriteLine("       = {0:f3} + {1:f3} + {1:f3}",
                    a1, H);
                sw.WriteLine("       = {0:f3} m", length_loaded_area);

                double loaded_area_dispersion = width_loaded_area * length_loaded_area;

                sw.WriteLine();
                sw.WriteLine("    Loaded Area including dispersion");
                sw.WriteLine("        = {0:f3} * {1:f3} = {2:f3} sq.m",
                    width_loaded_area,
                    length_loaded_area, loaded_area_dispersion);

                sw.WriteLine();
                sw.WriteLine("    Load for One Track = w1 = {0:f2} kN ", w1);

                double p2 = w1 / loaded_area_dispersion;
                sw.WriteLine();
                sw.WriteLine("    Load per unit Area for one Track ");
                sw.WriteLine("       = p2 = {0:f2}/{1:f2}", w1, loaded_area_dispersion);
                sw.WriteLine("       = {0:f3} kN/sq.m", p2);

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("For Two Track Load Covering 4-Lane");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double width_of_loaded_area_2 = (b1 + b2 + b2) * 2 + b3 + (H + H);
                sw.WriteLine("    Width of Loaded Area = (b1 + b2 + b2) * 2 + b3 + (H + H)");
                sw.WriteLine("                         = ({0:f2} + {1:f2} + {1:f2}) * 2 + {2:f3} + ({3:f3} + {3:f3})",
                    b1, b2, b3, H);
                sw.WriteLine("                         =  {0:f3} m", width_of_loaded_area_2);

                double length_loaded_area_2 = a1 + H + H;
                sw.WriteLine();
                sw.WriteLine("    Length of Loaded Area = (a1 + H + H)");
                sw.WriteLine("                          = ({0:f2} + {1:f2} + {1:f2})", a1, H);
                sw.WriteLine("                          = {0:f2} m", length_loaded_area_2);

                double loaded_area_dispersion_2 = width_of_loaded_area_2 * length_loaded_area_2;

                sw.WriteLine();
                sw.WriteLine("    Loaded Area including dispersion = {0:f2} * {1:f2}",
                    width_of_loaded_area_2, length_loaded_area_2);
                sw.WriteLine("                                     = {0:f2} sq.m", loaded_area_dispersion_2);

                double load_for_two_tracks = 2 * w1;
                sw.WriteLine();
                sw.WriteLine("    Load for Two Tracks = 2 * {0:f2} = {1:f2} kN",
                                    w1,
                                    load_for_two_tracks);

                double p3 = (load_for_two_tracks * F) / (loaded_area_dispersion_2);

                sw.WriteLine();
                sw.WriteLine("    Load per Unit Area for Two Tracks = p3 ");
                sw.WriteLine("                                      = {0:f2} * F / {1:f2}",
                                                    load_for_two_tracks,
                                                    loaded_area_dispersion_2);
                sw.WriteLine("                                      = {0:f2} kN", p3);

                double p4 = p1 + p3;

                sw.WriteLine();
                sw.WriteLine(" Considering Two-track load Covering 4-Lane");
                sw.WriteLine();
                sw.WriteLine(" Total Load per unit area = p4 = p1 + p3");
                sw.WriteLine("                               = {0:f2} + {1:f3} ", p1, p3);
                sw.WriteLine("                               = {0:f3} kN/sq.m.", p4);

                #endregion

                #region STEP 1.2  LOAD ON BOTTOM SLAB
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.2 : LOAD ON BOTTOM SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Permanent Load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Load from Top Slab = p1 = {0:f3} kN/sq.m", p1);
                double loads_walls = (d * d3 * 2 * gamma_c) / (1 * b + d3 + d3);
                sw.WriteLine();
                sw.WriteLine("  Load of Walls = (d * d3 * 2 * γ_c)/(1 * b + d3 + d3)");
                sw.WriteLine("                = ({0:f2} * {1:f2} * 2 * {2:f2})/(1 * ({3:f2} + {1:f2} + {1:f2}))",
                    d,
                    d3,
                    gamma_c,
                    b);
                sw.WriteLine("                = {0:f2} kN", loads_walls);

                double p5 = p1 + loads_walls;
                sw.WriteLine();
                sw.WriteLine("  Total Load = p5 ");
                sw.WriteLine("             = {0:f3} + {1:f3}", p1, loads_walls);
                sw.WriteLine("             = {0:f2} kN", p5);

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Live Load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Load from Top Slab = p3 = {0:f3} kN/sq.m", p3);
                double p6 = p5 + p3;
                sw.WriteLine();
                sw.WriteLine("  Total Load Per Unit Area = p6");
                sw.WriteLine("                           = {0:f3} + {1:f3}", p5, p3);
                sw.WriteLine("                           = {0:f3} kN/sq.m", p6);

                sw.WriteLine();

                #endregion

                #region STEP 1.3  LOAD ON SIDE WALLS

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.3 : LOAD ON SIDE WALLS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                #region Case 1
                sw.WriteLine("      Case 1 : Box Empty + Live Load Surcharge");
                sw.WriteLine();
                double p7 = S * gamma_b * 0.5;
                sw.WriteLine();
                sw.WriteLine("               Pressure by Live Load Surchage = p7");
                sw.WriteLine("                                              = S * γ_b * 0.5");
                sw.WriteLine("                                              = {0:f3} * {1:f3} * 0.5",
                    S,
                    gamma_b);
                sw.WriteLine("                                              = {0:f3} kN/sq.m", p7);

                double p8 = H * gamma_b * 0.5;
                sw.WriteLine();
                sw.WriteLine("               Pressure by Earth Surchage = p8");
                sw.WriteLine("                                          = H * γ_b * 0.5");
                sw.WriteLine("                                          = {0:f3} * {1:f3} * 0.5",
                    H,
                    gamma_b);
                sw.WriteLine("                                          = {0:f3} kN/sq.m", p8);

                double p9 = (d + d1) * gamma_b * 0.5;
                sw.WriteLine();
                sw.WriteLine("               Pressure by Earth Back fill = p9");
                sw.WriteLine("                                           = (d + d1) * γ_b * 0.5");
                sw.WriteLine("                                           = ({0:f3} + {1:f3}) * {2:f3} * 0.5",
                    d,
                    d1,
                    gamma_b);
                sw.WriteLine("                                           = {0:f3} kN/sq.m", p9);

                #endregion
                #region Case 2
                sw.WriteLine();
                sw.WriteLine("      Case 2 : Box Full with Water + Live Load Surcharge");

                //double p7 = S * gamma_b * 0.5;
                sw.WriteLine();
                sw.WriteLine("               Pressure by Live Load Surchage = p7 ");
                sw.WriteLine("                                              = {0:f3} kN/sq.m", p7);
                sw.WriteLine();
                sw.WriteLine("               Pressure by Earth Surchage     = p8");
                sw.WriteLine("                                              = {0:f3} kN/sq.m", p8);

                double p10 = 0.5 * (gamma_b - 10) * (d + d1);
                sw.WriteLine();
                sw.WriteLine("               Pressure by Submerged Earth Back fill");
                sw.WriteLine("                                     = p10 = 0.5 * (γ_b - 10) * (d + d1)");
                sw.WriteLine("                                     = 0.5 * ({0:f2} - 10) * ({1:f2} + {2:f2})",
                    gamma_b, d, d1);
                sw.WriteLine("                                     = {0:f3} kN/sq.m", p10);
                sw.WriteLine();
                #endregion
                #region Case 3
                sw.WriteLine("      Case 3 : Box Full with Water + No Live Load Surcharge");

                //double p7 = S * gamma_b * 0.5;
                sw.WriteLine();
                sw.WriteLine("               Pressure by Surchage Earth Backfill = p10");
                sw.WriteLine("                                                   = {0:f3} kN/sq.m", p10);
                sw.WriteLine();
                sw.WriteLine("               Pressure by Earth Surchage = p8 ");
                sw.WriteLine("                                          = {0:f3} kN/sq.m", p8);
                sw.WriteLine();
                #endregion

                #endregion

                #region STEP 1.4  BASE PRESSURE

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1.4 : BASE PRESSURE");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Permanent Load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("      Load from Top Slab and Walls including Cushion = p5");
                sw.WriteLine("                                                     = {0:f3} kN/sq.m", p5);
                sw.WriteLine();
                double self_weight_bottom_slab = d2 * gamma_c;

                sw.WriteLine("      Self weight at Bottom slab = d2 * γ_c");
                sw.WriteLine("                                 = {0:f2} * {1:f2}", d2, gamma_c);
                sw.WriteLine("                                 = {0:f3} kN/sq.m", self_weight_bottom_slab);
                double total_load = p5 + self_weight_bottom_slab;
                sw.WriteLine();
                sw.WriteLine("      Total Load = {0:f2} + {1:f2} ", p5, self_weight_bottom_slab);
                sw.WriteLine("                 = {0:f3} kN/sq.m", total_load);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Live Load");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("      Load from Top Slab = p3 = {0:f3} kN/sq.m", p3);
                sw.WriteLine();
                double base_pressure = total_load + p3;

                if (base_pressure < sbc)
                {
                    sw.WriteLine("      Base Pressure = {0:f3} + {1:f3} = {2:f3} kN/sq.m  < {3:f3} (sbc) OK.",
                        total_load, p3, base_pressure, sbc);
                }
                else
                {
                    sw.WriteLine("      Base Pressure = {0:f3} + {1:f3} = {2:f3} kN/sq.m  > {3:f3} (sbc) NOT OK.",
                        total_load, p3, base_pressure, sbc);
                }
                _pressure = base_pressure;
                #endregion
                #endregion

                #region STEP 2 BENDING MOMENT CALCULATION
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2.0 : BENDING MOMENT CALCULATION");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                #region 2.1 TOP SLAB
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2.1 : TOP SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double m1 = p1 * (d + d1) * (d + d1) / 12.0;
                sw.WriteLine("      Fixed End Moment for Permanent Load");
                sw.WriteLine("           = m1");
                sw.WriteLine("           = p1 * (d + d1) * (d + d1) / 12 ");
                sw.WriteLine("           = {0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/12 ",
                    p1, d, d1);
                sw.WriteLine("           = {0:f3} kN-m", m1);

                double m2 = p3 * (d + d1) * (d + d1) / 12.0;
                sw.WriteLine();
                sw.WriteLine("      Fixed End Moment for Live Load");
                sw.WriteLine("           = m2");
                sw.WriteLine("           = p3 * (d + d1) * (d + d1)/12 ");
                sw.WriteLine("           = {0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/12",
                    p3, d, d1);
                sw.WriteLine("           = {0:f3} kN-m", m2);

                double m3 = m1 + m2;
                sw.WriteLine();
                sw.WriteLine("      Total Fixed End Moment = m3 ");
                sw.WriteLine("                             = {0:f3} + {1:f3} ", m1, m2);
                sw.WriteLine("                             = {0:f3} kN-m", m3);

                double m4 = p1 * (d + d1) * (d + d1) / 8.0;
                sw.WriteLine();
                sw.WriteLine("      Mid span Moment for Permanent Load ");
                sw.WriteLine("           = m4");
                sw.WriteLine("           = p1 * (d + d1) * (d + d1) / 8");
                sw.WriteLine("           = {0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/8",
                    p1, d, d1);
                sw.WriteLine("           = {0:f3} kN-m", m4);

                double m5 = p3 * (d + d1) * (d + d1) / 8.0;
                sw.WriteLine();
                sw.WriteLine("      Mid span Moment for Live Load");
                sw.WriteLine("           = m5");
                sw.WriteLine("           = p3 * (d + d1) * (d + d1)/8");
                sw.WriteLine("           = {0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 8 ",
                    p3, d, d1);
                sw.WriteLine("           = {0:f3} kN-m", m5);

                double m6 = m4 + m5;
                sw.WriteLine();
                sw.WriteLine("      Total Mid span Moment = m6");
                sw.WriteLine("                            = m4 + m5");
                sw.WriteLine("                            = {0:f2} + {1:f2}", m4, m5);
                sw.WriteLine("                            = {0:f2} kN-m", m6);


                #endregion


                #region 2.2 BOTTOM SLAB
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2.2 : BOTTOM SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                double m7 = p5 * (d + d1) * (d + d1) / 12.0;
                sw.WriteLine("      Fixed End Moment for Permanent Load");
                sw.WriteLine("          = m7 ");
                sw.WriteLine("          = p5 * (d + d1) * (d + d1) / 12 ");
                sw.WriteLine("          = {0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 12 ",
                    p5, d, d1);
                sw.WriteLine("          = {0:f3} kN-m", m7);

                //double m2 = p3 * (d + d1) * (d + d1) / 12.0;
                sw.WriteLine();
                sw.WriteLine("      Fixed End Moment for Live Load");
                sw.WriteLine("          = m2 ");
                sw.WriteLine("          ={0:f3} kN-m", m2);

                double m8 = m7 + m2;
                sw.WriteLine();
                sw.WriteLine("      Total Fixed End Moment = m8 ");
                sw.WriteLine("                             = m7 + m2 ");
                sw.WriteLine("                             = {0:f3} + {1:f3}", m7, m2);
                sw.WriteLine("                             = {0:f3} kN-m", m8);
                sw.WriteLine();
                double m9 = p5 * (d + d1) * (d + d1) / 8.0;
                sw.WriteLine();
                sw.WriteLine("      Mid span Moment for Permanent Load");
                sw.WriteLine("          = m9");
                sw.WriteLine("          = p5 * (d + d1) * (d + d1) / 8");
                sw.WriteLine("          = {0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/8",
                    p5, d, d1);
                sw.WriteLine("          = {0:f3} kN-m", m9);

                //double m5 = p3 * (d + d1) * (d + d1) / 8.0;
                sw.WriteLine();
                sw.WriteLine("      Mid span Moment for Live Load = m5 ");
                sw.WriteLine("                                    = {0:f3} kN-m", m5);

                double m10 = m9 + m5;
                sw.WriteLine();
                sw.WriteLine("      Total Mid span Moment = m10");
                sw.WriteLine("                            = m9 + m5");
                sw.WriteLine("                            = {0:f2} + {1:f2}", m9, m5);
                sw.WriteLine("                            = {0:f2} kN-m", m10);
                sw.WriteLine();


                #endregion

                #region Side Wall
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("SIDE WALL");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                #region  Case 1 : BOX Empty + Live Load Surcharge
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine(" Case 1 : BOX Empty + Live Load Surcharge");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Top for Permanent Load");
                double m11 = ((p8 * (d + d1) * (d + d1) / 12) + ((p9 * (d + d1) * (d + d1) / 30)));
                sw.WriteLine();
                sw.WriteLine("   = m11 = (p8 * (d + d1) * (d + d1) / 12) + ");
                sw.WriteLine("           ((p9 * (d + d1) * (d + d1) / 30))");
                sw.WriteLine();
                sw.WriteLine("   = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/12) + ",
                    p8, d, d1);
                sw.WriteLine("     (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/30))",
                    p9, d, d1);
                sw.WriteLine();
                sw.WriteLine("   = {0:f3} kN-m", m11);

                sw.WriteLine();
                double m12 = (p7 * (d + d1) * (d + d1)) / 12.0;
                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Top for Live Load");
                sw.WriteLine();
                sw.WriteLine("   = m12 = (p7 * (d + d1) * (d + d1)) / 12.0");
                sw.WriteLine("   = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}))/12.0",
                    p7, d, d1);
                sw.WriteLine();
                sw.WriteLine("   = {0:f2} kN-m", m12);
                double m13 = m11 + m12;
                sw.WriteLine();
                sw.WriteLine(" Total Fixed End Moment at Top = m13 ");
                sw.WriteLine("                               = m11 + m12");
                sw.WriteLine("                               = {0:f2} + {1:f2}", m11, m12);
                sw.WriteLine("                               = {0:f3} kN-m", m13);




                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Base for Permanent Load ");

                double m14 = ((p8 * (d + d1) * (d + d1) / 12) + ((p9 * (d + d1) * (d + d1) / 20)));
                sw.WriteLine();
                sw.WriteLine("  = m14 = (p8 * (d + d1) * (d + d1) / 12) +");
                sw.WriteLine("          ((p9 * (d + d1) * (d + d1) / 20))");
                sw.WriteLine();
                sw.WriteLine("  = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 12) + ",
                    p8, d, d1, p9);
                sw.WriteLine("    (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/20))",
                    p9, d, d1);
                sw.WriteLine();
                sw.WriteLine("  = {0:f3} kN-m", m14);

                //double m12 = (p7 * (d + d1) * (d + d1)) / 12.0;
                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Base for Live Load ");
                sw.WriteLine();
                sw.WriteLine("  = m12 = (p7 * (d + d1) * (d + d1)) / 12.0");
                sw.WriteLine("  = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}))/12.0",
                    p7, d, d1);
                sw.WriteLine();
                sw.WriteLine("  =  {0:f2} kN-m", m12);

                double m15 = m14 + m12;
                sw.WriteLine();
                sw.WriteLine("Total Fixed End Moment at Base  = m15 ");
                sw.WriteLine("                                = m14 + m12");
                sw.WriteLine("                                = {0:f2} + {1:f2}", m14, m12);
                sw.WriteLine("                                = {0:f3} kN-m", m15);



                sw.WriteLine();
                sw.WriteLine("Mid Span Moment for Dead Load ");
                double m16 = ((p8 * (d + d1) * (d + d1) / 8) + ((p9 * (d + d1) * (d + d1) / 16)));

                sw.WriteLine("   = m16 = (p8 * (d + d1) * (d + d1) / 8) + ");
                sw.WriteLine("           ((p9 * (d + d1) * (d + d1) / 16))");
                sw.WriteLine();
                sw.WriteLine("   = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 8) + ",
                    p8, d, d1);
                sw.WriteLine("     (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/16))",
                    p9, d, d1);
                sw.WriteLine();
                sw.WriteLine("   = {0:f3} kN-m", m16);

                double m17 = (p7 * (d + d1) * (d + d1)) / 8.0;
                sw.WriteLine();
                sw.WriteLine("Mid Span Moment for Live Load ");
                sw.WriteLine();
                sw.WriteLine("   = m17 = (p7 * (d + d1) * (d + d1)) / 8.0");
                sw.WriteLine("   = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}))/8.0",
                    p7, d, d1);
                sw.WriteLine();
                sw.WriteLine("   = {0:f3} kN-m", m17);

                double m18 = m16 + m17;
                sw.WriteLine();
                sw.WriteLine(" Total Fixed End Moment at Base = m18");
                sw.WriteLine("                                = m16 + m17");
                sw.WriteLine("                                = {0:f2} + {1:f2}", m16, m17);
                sw.WriteLine("                                = {0:f3} kN-m", m18);

                #endregion

                #region  Case 2 : BOX Full with Water + Live Load Surcharge
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine(" Case 2 : BOX Full with Water + Live Load Surcharge");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Top for Permanent Load ");
                sw.WriteLine();
                double m19 = ((p8 * (d + d1) * (d + d1) / 12) + ((p10 * (d + d1) * (d + d1) / 30)));
                sw.WriteLine("  = m19 = (p8 * (d + d1) * (d + d1) / 12) + ");
                sw.WriteLine("          ((p10 * (d + d1) * (d + d1) / 30))");
                sw.WriteLine();
                sw.WriteLine("  = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 12) + ",
                    p8, d, d1);
                sw.WriteLine("    (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/30))",
                    p10, d, d1);
                sw.WriteLine();
                sw.WriteLine("  = {0:f3} kN-m", m19);


                //double m12 = (p7 * (d + d1) * (d + d1)) / 12.0;
                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Top for Live Load ");
                sw.WriteLine();
                sw.WriteLine("  = m12 = (p7 * (d + d1) * (d + d1)) / 12.0");
                sw.WriteLine("  = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}))/12.0",
                    p7, d, d1);
                sw.WriteLine();
                sw.WriteLine("  = {0:f3} kN-m", m12);

                double m20 = m19 + m12;
                sw.WriteLine();
                sw.WriteLine("Total Fixed End Moment at Top = m20");
                sw.WriteLine("                              = m19 + m12");
                sw.WriteLine("                              = {0:f2} + {1:f2}",
                    m19, m12);
                sw.WriteLine("                              = {0:f3} kN-m", m20);




                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Base for Permanent Load ");
                sw.WriteLine();
                double m21 = ((p8 * (d + d1) * (d + d1) / 12.0) + ((p10 * (d + d1) * (d + d1) / 20.0)));
                sw.WriteLine("   = m21 = (p8 * (d + d1) * (d + d1) / 12) + ");
                sw.WriteLine("           ((p10 * (d + d1) * (d + d1) / 20))");
                sw.WriteLine();
                sw.WriteLine("   = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 12) + ",
                    p8, d, d1);
                sw.WriteLine("     (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/20))",
                     p10, d, d1);
                sw.WriteLine();
                sw.WriteLine("   = {0:f3} kN-m", m21);

                //double m12 = (p7 * (d + d1) * (d + d1)) / 12.0;
                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Base for Live Load ");
                sw.WriteLine();
                sw.WriteLine("   = m12 = (p7 * (d + d1) * (d + d1)) / 12.0");
                sw.WriteLine("   = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})) / 12.0",
                    p7, d, d1);
                sw.WriteLine();
                sw.WriteLine("   = {0:f2} kN-m", m12);

                double m22 = m21 + m12;
                sw.WriteLine();
                sw.WriteLine("Total Fixed End Moment at Base = m22");
                sw.WriteLine("                               = m21 + m12");
                sw.WriteLine("                               = {0:f2} + {1:f2} kN-m", m21, m12);
                sw.WriteLine("                               = {0:f3} kN-m", m22);
                sw.WriteLine();

                sw.WriteLine("Mid Span Moment for Dead Load ");
                sw.WriteLine();
                double m23 = ((p8 * (d + d1) * (d + d1) / 8) + ((p10 * (d + d1) * (d + d1) / 16)));
                sw.WriteLine("  = m23 = (p8 * (d + d1) * (d + d1) / 8) + ");
                sw.WriteLine("          ((p10 * (d + d1) * (d + d1) / 16))");
                sw.WriteLine();
                sw.WriteLine("  = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 8) + ",
                    p8, d, d1);
                sw.WriteLine("    (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 16))",
                    p10, d, d1);
                sw.WriteLine();
                sw.WriteLine("  = {0:f3} kN-m", m23);

                //double m17 = (p7 * (d + d1) * (d + d1)) / 8.0;
                sw.WriteLine();
                sw.WriteLine("Mid Span Moment for Live Load ");
                sw.WriteLine("     = m17 = (p7 * (d + d1) * (d + d1)) / 8.0");
                sw.WriteLine("     = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})) / 8.0",
                    p7, d, d1);
                sw.WriteLine();
                sw.WriteLine("     = {0:f3} kN-m", m17);

                double m24 = m23 + m17;
                sw.WriteLine();
                sw.WriteLine("Total Fixed End Moment at Base = m24 ");
                sw.WriteLine("                               = m23 + m17");
                sw.WriteLine("                               = {0:f2} + {1:f2}", m23, m17);
                sw.WriteLine("                               = {0:f3} kN-m", m24);

                #endregion

                #region  Case 3 : BOX Full with Water + No Live Load Surcharge
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Case 3 :  BOX Full with Water + No Live Load Surcharge");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Top for Permanent Load ");
                double m25 = ((p8 * (d + d1) * (d + d1) / 12) + ((p10 * (d + d1) * (d + d1) / 30)));
                sw.WriteLine("  = m25 = (p8 * (d + d1) * (d + d1) / 12) + ");
                sw.WriteLine("          ((p10 * (d + d1) * (d + d1) / 30))");
                sw.WriteLine();
                sw.WriteLine("  = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 12) + ", p8, d, d1);
                sw.WriteLine("    (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2})/30))", p10, d, d1);
                sw.WriteLine();
                sw.WriteLine("  = {0:f3} kN-m", m25);


                //double m12 = (p7 * (d + d1) * (d + d1)) / 12.0;
                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Top for Live Load ");
                double m26 = 0;
                sw.WriteLine("  = m26 = {0:f2} kN-m", m26);

                //double m20 = m19 + m12;
                sw.WriteLine();
                sw.WriteLine("Total Fixed End Moment at Top = m25 + m26 ");
                sw.WriteLine("                              = {0:f2} + {1:f2} m", m25, m26);
                sw.WriteLine("                              = {0:f3} kN-m", m25);
                sw.WriteLine();




                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Base for Permanent Load ");

                double m27 = ((p8 * (d + d1) * (d + d1) / 12) + ((p10 * (d + d1) * (d + d1) / 20)));

                sw.WriteLine("  = m27 = (p8 * (d + d1) * (d + d1) / 12) + ");
                sw.WriteLine("          ((p10 * (d + d1) * (d + d1) / 20))");
                sw.WriteLine();
                sw.WriteLine("  = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 12) +", p8, d, d1);
                sw.WriteLine("    (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 20))", p10, d, d1);
                sw.WriteLine();
                sw.WriteLine("  = {0:f3} kN-m", m27);

                //double m12 = (p7 * (d + d1) * (d + d1)) / 12.0;
                double m28 = 0;
                sw.WriteLine();
                sw.WriteLine("Fixed End Moment at Base for Live Load = m28 = 0 kN-m");

                //double m22 = m21 + m12;
                sw.WriteLine();
                sw.WriteLine("Total Fixed End Moment at Base = m27 + m28 ");
                sw.WriteLine("                               = {0:f2} + {1:f2}", m27, m28);
                sw.WriteLine("                               = {0:f3} kN-m", m27);



                sw.WriteLine();
                sw.WriteLine("Mid Span Moment for Permanent Load ");
                double m29 = ((p8 * (d + d1) * (d + d1) / 8) + ((p10 * (d + d1) * (d + d1) / 16)));

                sw.WriteLine("  = m29 = (p8 * (d + d1) * (d + d1) / 8) + ");
                sw.WriteLine("          ((p10 * (d + d1) * (d + d1) / 16))");
                sw.WriteLine();
                sw.WriteLine("  = ({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 8) + ",
                    p8, d, d1);
                sw.WriteLine("    (({0:f2} * ({1:f2} + {2:f2}) * ({1:f2} + {2:f2}) / 16))",
                    p10, d, d1);
                sw.WriteLine();
                sw.WriteLine("  = {0:f3} kN-m", m29);

                //double m17 = (p7 * (d + d1) * (d + d1)) / 8.0;
                double m30 = 0;
                sw.WriteLine();
                sw.WriteLine("Mid Span Moment for Live Load = m30 = 0 kN-m");

                //double m24 = m23 + m17;
                sw.WriteLine();
                sw.WriteLine("Total Fixed End Moment at Base = m29 + m30 ");
                sw.WriteLine("                               = {0:f2} + {1:f2}", m29, m30);
                sw.WriteLine("                               = {0:f3} kN-m", m29);
                sw.WriteLine();

                #endregion


                #endregion

                #endregion

                #region STEP 3 DISTRIBUTION FACTORS

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : DISTRIBUTION FACTORS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Let us denote the Four corner of Box Culvert as A,B,C and D clockwise,");
                sw.WriteLine("Starting from Left Top Corner, then next Right Top Corner, ");
                sw.WriteLine("next Right Bottom Corner and finally Left Bottom Corner");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("{0,-15}{1,-11}{2,-15}{3,-15}",
                    "Corner/Joint",
                    "Associated",
                    "  4EI/L",
                    "  DF");
                sw.WriteLine("{0,-15}{1,-11}{2,-15}{3,-15}",
                                   "",
                                   "  Sides",
                                   "",
                                   "");

                sw.WriteLine("------------------------------------------------------------");


                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                    "A",
                    "AB",
                    "K_ab",
                    "DF_ab = 0.5");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                   "",
                                   "",
                                   "(Cal 1)",
                                   "(Cal 2)");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                   "",
                                   "AD",
                                   "K_ad",
                                   "DF_ad = 0.5");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                   "",
                                   "",
                                   "(Cal 3)",
                                   "(Cal 4)");


                sw.WriteLine();
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "B",
                                    "BA",
                                    "K_ba",
                                    "DF_ba = 0.5");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                "",
                                "",
                                "(Cal 5)",
                                "(Cal 6)");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "",
                                    "BC",
                                    "K_bc",
                                    "DF_bc = 0.5");

                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "",
                                    "",
                                    "(Cal 7)",
                                    "(Cal 8)");
                sw.WriteLine();
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "C",
                                    "CD",
                                    "K_cd",
                                    "DF_cd = 0.5");

                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                   "",
                                   "",
                                   "(Cal 9)",
                                   "(Cal 10)");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "",
                                    "CB",
                                    "K_cb",
                                    "DF_cb = 0.5");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "",
                                    "",
                                    "(Cal 11)",
                                    "(Cal 12)");

                sw.WriteLine();
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "D",
                                    "DA",
                                    "K_da",
                                    "DF_da = 0.5");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                      "",
                                      "",
                                      "(Cal 13)",
                                      "(Cal 14)");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "",
                                    "DC",
                                    "K_dc",
                                    "DF_dc = 0.5");
                sw.WriteLine("{0,-18}{1,-11}{2,-12}{3,-15}",
                                    "",
                                    "",
                                    "(Cal 15)",
                                    "(Cal 16)");

                #region Calculation Details
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Calculation Details");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Cal 1   : K_ab = (k * d1 * d1 * d1) / (b + d3)");
                sw.WriteLine("Cal 2   : DF_ab = K_ab/(K_ab+K_ad)");
                sw.WriteLine("Cal 3   : K_ad = (k*d3*d3*d3)/(d+(d1+d3)/2))");
                sw.WriteLine("Cal 4   : DF_ad = K_ad/(K_ab+K_ad) = 0.5");
                sw.WriteLine("Cal 5   : K_ba = K_ab");
                sw.WriteLine("Cal 6   : DF_ba = K_bA/(K_ba+K_bc) = 0.5");
                sw.WriteLine("Cal 7   : K_bc = (k*d3*d3*d3)/(d+(d1+d3)/2))");
                sw.WriteLine("Cal 8   : DF_bc = K_bc/(K_ba+K_bc) = 0.5");
                sw.WriteLine("Cal 9   : K_cd = K*d2*d2*d2/(b+d3)");
                sw.WriteLine("Cal 10  : DF_cd = K_cd/(K_cd+K_cb) = 0.5");
                sw.WriteLine("Cal 11  : K_cb= (K*d3*d3*d3)/(d+(d3+d2)/2)");
                sw.WriteLine("Cal 12  : DF_cb = K_cb/(K_cd+K_cb) = 0.5");
                sw.WriteLine("Cal 13  : K_da = (K*d3*d3*d3)/(d+(d3+d2)/2)");
                sw.WriteLine("Cal 14  : DF_da = K_da/(K_da+K_dc) = 0.5");
                sw.WriteLine("Cal 15  : K_dc = K_cd");
                sw.WriteLine("Cal 16  : DF_dc = K_dc/(K_da+K_dc) = 0.5");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                #endregion
                #endregion

                #region STEP 4 : MOMENT DISTRIBUTION
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : MOMENT DISTRIBUTION");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                #region Case 1 : FIXED END MOMENT for Permanent Load
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Permanent Load ( Case 1)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Mab = m1 = {0:f3}", m1);
                sw.WriteLine("  Mba = m1 = {0:f3}", m1);
                sw.WriteLine("  Mdc = m7 = {0:f3}", m7);
                sw.WriteLine("  Mcd = m7 = {0:f3}", m7);
                sw.WriteLine("  Mad = m11 = {0:f3}", m11);
                sw.WriteLine("  Mbc = m11 = {0:f3}", m11);
                sw.WriteLine("  Mda = m14 = {0:f3}", m14);
                sw.WriteLine("  Mcb = m14 = {0:f3}", m14);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Live Load ( Case 1)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Mab = m2 = {0:f3}", m2);
                sw.WriteLine("  Mba = m2 = {0:f3}", m2);
                sw.WriteLine("  Mdc = m2 = {0:f3}", m2);
                sw.WriteLine("  Mcd = m2 = {0:f3}", m2);
                sw.WriteLine("  Mad = m12 = {0:f3}", m12);
                sw.WriteLine("  Mbc = m12 = {0:f3}", m12);
                sw.WriteLine("  Mda = m12 = {0:f3}", m12);
                sw.WriteLine("  Mcb = m12 = {0:f3}", m12);
                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Total Load ( Case 1)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Mab1 = m3 = -{0:f3} (-)", m3);
                sw.WriteLine("  Mba1 = m3 = {0:f3} (+)", m3);
                sw.WriteLine("  Mdc1 = m8 = {0:f3} (+)", m8);
                sw.WriteLine("  Mcd1 = m8 = -{0:f3} (-)", m8);
                sw.WriteLine("  Mad1 = m13 = {0:f3} (+)", m13);
                sw.WriteLine("  Mbc1 = m13 = -{0:f3} (-)", m13);
                sw.WriteLine("  Mda1 = m15 = -{0:f3} (-)", m15);
                sw.WriteLine("  Mcb1 = m15 = {0:f3} (+)", m15);
                sw.WriteLine();

                sw.WriteLine("----------------------------------------");

                sw.WriteLine("    AB         BC        CD       DA");
                sw.WriteLine("  AB  BA     BC  CB    CD  DC   DA  AD");
                sw.WriteLine("  -   +      -   +     -   +    -   + ");
                sw.WriteLine("----------------------------------------");

                #endregion


                #region Case 2 : FIXED END MOMENT for Permanent Load
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Permanent Load ( Case 2)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Mab = m1 = {0:f3}", m1);
                sw.WriteLine("  Mba = m1 = {0:f3}", m1);
                sw.WriteLine("  Mdc = m7 = {0:f3}", m7);
                sw.WriteLine("  Mcd = m7 = {0:f3}", m7);
                sw.WriteLine("  Mad = m19 = {0:f3}", m19);
                sw.WriteLine("  Mbc = m19 = {0:f3}", m19);
                sw.WriteLine("  Mda = m21 = {0:f3}", m21);
                sw.WriteLine("  Mcb = m21 = {0:f3}", m21);
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Live Load ( Case 2)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("  Mab = m2 = {0:f3}", m2);
                sw.WriteLine("  Mba = m2 = {0:f3}", m2);
                sw.WriteLine("  Mdc = m2 = {0:f3}", m2);
                sw.WriteLine("  Mcd = m2 = {0:f3}", m2);
                sw.WriteLine("  Mad = m12 = {0:f3}", m12);
                sw.WriteLine("  Mbc = m12 = {0:f3}", m12);
                sw.WriteLine("  Mda = m12 = {0:f3}", m12);
                sw.WriteLine("  Mcb = m12 = {0:f3}", m12);
                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Total Load ( Case 2)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Mab2 = m3 = -{0:f3} (-)", m3);
                sw.WriteLine("  Mba2 = m3 = {0:f3} (+)", m3);
                sw.WriteLine("  Mdc2 = m8 = {0:f3} (+)", m8);
                sw.WriteLine("  Mcd2 = m8 = -{0:f3} (-)", m8);
                sw.WriteLine("  Mad2 = m20 = {0:f3} (+)", m20);
                sw.WriteLine("  Mbc2 = m20 = -{0:f3} (-)", m20);
                sw.WriteLine("  Mda2 = m22 = -{0:f3} (-)", m22);
                sw.WriteLine("  Mcb2 = m22 = {0:f3} (+)", m22);
                sw.WriteLine();


                #endregion

                #region Case 3 : FIXED END MOMENT for Permanent Load
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Permanent Load ( Case 3)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Mab = m1 = {0:f3}", m1);
                sw.WriteLine("  Mba = m1 = {0:f3}", m1);
                sw.WriteLine("  Mdc = m7 = {0:f3}", m7);
                sw.WriteLine("  Mcd = m7 = {0:f3}", m7);
                sw.WriteLine("  Mad = m19 = {0:f3}", m19);
                sw.WriteLine("  Mbc = m19 = {0:f3}", m19);
                sw.WriteLine("  Mda = m21 = {0:f3}", m21);
                sw.WriteLine("  Mcb = m21 = {0:f3}", m21);
                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Live Load ( Case 3)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Mab = m2 = {0:f3}", m2);
                sw.WriteLine("  Mba = m2 = {0:f3}", m2);
                sw.WriteLine("  Mdc = m2 = {0:f3}", m2);
                sw.WriteLine("  Mcd = m2 = {0:f3}", m2);
                sw.WriteLine("  Mad = m26 = {0:f3}", m26);
                sw.WriteLine("  Mbc = m26 = {0:f3}", m26);
                sw.WriteLine("  Mda = m28 = {0:f3}", m28);
                sw.WriteLine("  Mcb = m28 = {0:f3}", m28);
                sw.WriteLine();

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("  FIXED END MOMENT for Total Load ( Case 3)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Mab3 = m3 = -{0:f3} (-)", m3);
                sw.WriteLine("  Mba3 = m3 = {0:f3} (+)", m3);
                sw.WriteLine("  Mdc3 = m8 = {0:f3} (+)", m8);
                sw.WriteLine("  Mcd3 = m8 = -{0:f3} (-)", m8);
                sw.WriteLine("  Mad3 = m25 = {0:f3} (+)", m25);
                sw.WriteLine("  Mbc3 = m25 = -{0:f3} (-)", m25);
                sw.WriteLine("  Mda3 = m27 = -{0:f3} (-)", m27);
                sw.WriteLine("  Mcb3 = m27 = {0:f3} (+)", m27);
                sw.WriteLine();


                #endregion

                #endregion

                #region Table-1
                sw.WriteLine(" Table-1 MOMENT DISTRIBUTION for TOTAL LOAD ON TOP, BOTTOM & SIDES for CASE I ");
                sw.WriteLine(" -----------------------------------------------------------------------------");
                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                    "DISTRIB",
                    "DF_ab=",
                    "DF_ad=",
                    "DF_ba=",
                    "DF_bc=",
                    "DF_cb=",
                    "DF_cd=",
                    "DF_dc=",
                    "DF_da=");

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                    "FACTORS",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5");
                sw.WriteLine("---------------------------------------------------------------------------------");

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                    "FEM",
                    "Mab1 =",
                    "Mad1 =",
                    "Mba1 =",
                    "Mbc1 =",
                    "Mcb1 =",
                    "Mcd1 =",
                    "Mdc1 =",
                    "Mda1 =");
                double Mab, Mad, Mba, Mbc, Mcb, Mcd, Mdc, Mda;
                Mab = -m3;
                Mad = m13;
                Mba = m3;
                Mbc = -m13;
                Mcb = m15;
                Mcd = -m8;
                Mdc = m8;
                Mda = -m15;

                double SMab1, SMad1, SMba1, SMbc1, SMcb1, SMcd1, SMdc1, SMda1;
                double SMab2, SMad2, SMba2, SMbc2, SMcb2, SMcd2, SMdc2, SMda2;
                double SMab3, SMad3, SMba3, SMbc3, SMcb3, SMcd3, SMdc3, SMda3;

                SMab1 = Mab;

                SMab1 = Mab;
                SMad1 = Mad;
                SMba1 = Mba;
                SMbc1 = Mbc;
                SMcb1 = Mcb;
                SMcd1 = Mcd;
                SMdc1 = Mdc;
                SMda1 = Mda;



                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                    "",
                                    "" + Mab.ToString("0.00"),
                                    "" + Mad.ToString("0.00"),
                                    "" + Mba.ToString("0.00"),
                                    "" + Mbc.ToString("0.00"),
                                    "" + Mcb.ToString("0.00"),
                                    "" + Mcd.ToString("0.00"),
                                    "" + Mdc.ToString("0.00"),
                                    "" + Mda.ToString("0.00"));

                double D1, D2, D3, D4, D5, D6, D7, D8;
                D1 = 0 - (Mab + Mad) * 0.5;
                D2 = 0 - (Mab + Mad) * 0.5;
                D3 = 0 - (Mba + Mbc) * 0.5;
                D4 = 0 - (Mba + Mbc) * 0.5;
                D5 = 0 - (Mcb + Mcd) * 0.5;
                D6 = 0 - (Mcb + Mcd) * 0.5;
                D7 = 0 - (Mdc + Mda) * 0.5;
                D8 = 0 - (Mdc + Mda) * 0.5;


                //SMab1 += D1;
                //SMad1 += D2;
                //SMba1 += D3;
                //SMbc1 += D4;
                //SMcb1 += D5;
                //SMcd1 += D6;
                //SMdc1 += D7;
                //SMda1 += D8;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D1 =",
                                                    "D2 =",
                                                    "D3 =",
                                                    "D4 =",
                                                    "D5 =",
                                                    "D6 =",
                                                    "D7 =",
                                                    "D8 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                   "",
                                                   "(Cal 1)",
                                                   "(Cal 2)",
                                                   "(Cal 3)",
                                                   "(Cal 4)",
                                                   "(Cal 5)",
                                                   "(Cal 6)",
                                                   "(Cal 7)",
                                                   "(Cal 8)");


                double C1, C2, C3, C4, C5, C6, C7, C8;

                C1 = D3 * 0.5;
                C2 = D8 * 0.5;
                C3 = D1 * 0.5;
                C4 = D5 * 0.5;
                C5 = D4 * 0.5;
                C6 = D7 * 0.5;
                C7 = D6 * 0.5;
                C8 = D2 * 0.5;

                SMab1 += D1;
                SMad1 += D2;
                SMba1 += D3;
                SMbc1 += D4;
                SMcb1 += D5;
                SMcd1 += D6;
                SMdc1 += D7;
                SMda1 += D8;

                SMab1 += C1;
                SMad1 += C2;
                SMba1 += C3;
                SMbc1 += C4;
                SMcb1 += C5;
                SMcd1 += C6;
                SMdc1 += C7;
                SMda1 += C8;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C1 =",
                                                    "C2 =",
                                                    "C3 =",
                                                    "C4 =",
                                                    "C5 =",
                                                    "C6 =",
                                                    "C7 =",
                                                    "C8 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                  "",
                                                  "(Cal 9)",
                                                  "(Cal 10)",
                                                  "(Cal 11)",
                                                  "(Cal 12)",
                                                  "(Cal 13)",
                                                  "(Cal 14)",
                                                  "(Cal 15)",
                                                  "(Cal 16)");


                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

                SMab1 += D1;
                SMad1 += D2;
                SMba1 += D3;
                SMbc1 += D4;
                SMcb1 += D5;
                SMcd1 += D6;
                SMdc1 += D7;
                SMda1 += D8;

                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D9 =",
                                                    "D10 =",
                                                    "D11 =",
                                                    "D12 =",
                                                    "D13 =",
                                                    "D14 =",
                                                    "D15 =",
                                                    "D16 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                               "",
                                               "(Cal 17)",
                                               "(Cal 18)",
                                               "(Cal 19)",
                                               "(Cal 20)",
                                               "(Cal 21)",
                                               "(Cal 22)",
                                               "(Cal 23)",
                                               "(Cal 24)");


                #region DEMO 1
                C1 = D3 * 0.5;
                C2 = D8 * 0.5;
                C3 = D1 * 0.5;
                C4 = D5 * 0.5;
                C5 = D4 * 0.5;
                C6 = D7 * 0.5;
                C7 = D6 * 0.5;
                C8 = D2 * 0.5;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C9 =",
                                                    "C10 =",
                                                    "C11 =",
                                                    "C12 =",
                                                    "C13 =",
                                                    "C14 =",
                                                    "C15 =",
                                                    "C16 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                             "",
                                             "(Cal 25)",
                                             "(Cal 26)",
                                             "(Cal 27)",
                                             "(Cal 28)",
                                             "(Cal 29)",
                                             "(Cal 30)",
                                             "(Cal 31)",
                                             "(Cal 32)");

                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

                SMab1 += D1;
                SMad1 += D2;
                SMba1 += D3;
                SMbc1 += D4;
                SMcb1 += D5;
                SMcd1 += D6;
                SMdc1 += D7;
                SMda1 += D8;

                SMab1 += C1;
                SMad1 += C2;
                SMba1 += C3;
                SMbc1 += C4;
                SMcb1 += C5;
                SMcd1 += C6;
                SMdc1 += C7;
                SMda1 += C8;

                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D17 =",
                                                    "D18 =",
                                                    "D19 =",
                                                    "D20 =",
                                                    "D21 =",
                                                    "D22 =",
                                                    "D23 =",
                                                    "D24 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                            "",
                                            "(Cal 33)",
                                            "(Cal 34)",
                                            "(Cal 35)",
                                            "(Cal 36)",
                                            "(Cal 37)",
                                            "(Cal 38)",
                                            "(Cal 39)",
                                            "(Cal 40)");



                #endregion

                #region DEMO 2
                C1 = D3 * 0.5;
                C2 = D8 * 0.5;
                C3 = D1 * 0.5;
                C4 = D5 * 0.5;
                C5 = D4 * 0.5;
                C6 = D7 * 0.5;
                C7 = D6 * 0.5;
                C8 = D2 * 0.5;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C17 =",
                                                    "C18 =",
                                                    "C19 =",
                                                    "C20 =",
                                                    "C21 =",
                                                    "C22 =",
                                                    "C23 =",
                                                    "C24 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                            "",
                                            "(Cal 41)",
                                            "(Cal 42)",
                                            "(Cal 43)",
                                            "(Cal 44)",
                                            "(Cal 45)",
                                            "(Cal 46)",
                                            "(Cal 47)",
                                            "(Cal 48)");


                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

                SMab1 += D1;
                SMad1 += D2;
                SMba1 += D3;
                SMbc1 += D4;
                SMcb1 += D5;
                SMcd1 += D6;
                SMdc1 += D7;
                SMda1 += D8;

                SMab1 += C1;
                SMad1 += C2;
                SMba1 += C3;
                SMbc1 += C4;
                SMcb1 += C5;
                SMcd1 += C6;
                SMdc1 += C7;
                SMda1 += C8;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D25 =",
                                                    "D26 =",
                                                    "D27 =",
                                                    "D28 =",
                                                    "D29 =",
                                                    "D30 =",
                                                    "D31 =",
                                                    "D32 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                           "",
                                           "(Cal 49)",
                                           "(Cal 50)",
                                           "(Cal 51)",
                                           "(Cal 52)",
                                           "(Cal 53)",
                                           "(Cal 54)",
                                           "(Cal 55)",
                                           "(Cal 56)");

                #endregion

                #region DEMO 3
                C1 = D3 * 0.5;
                C2 = D8 * 0.5;
                C3 = D1 * 0.5;
                C4 = D5 * 0.5;
                C5 = D4 * 0.5;
                C6 = D7 * 0.5;
                C7 = D6 * 0.5;
                C8 = D2 * 0.5;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C25 =",
                                                    "C26 =",
                                                    "C27 =",
                                                    "C28 =",
                                                    "C29 =",
                                                    "C30 =",
                                                    "C31 =",
                                                    "C32 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                           "",
                                           "(Cal 57)",
                                           "(Cal 58)",
                                           "(Cal 59)",
                                           "(Cal 60)",
                                           "(Cal 61)",
                                           "(Cal 62)",
                                           "(Cal 63)",
                                           "(Cal 64)");

                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

                SMab1 += D1;
                SMad1 += D2;
                SMba1 += D3;
                SMbc1 += D4;
                SMcb1 += D5;
                SMcd1 += D6;
                SMdc1 += D7;
                SMda1 += D8;

                SMab1 += C1;
                SMad1 += C2;
                SMba1 += C3;
                SMbc1 += C4;
                SMcb1 += C5;
                SMcd1 += C6;
                SMdc1 += C7;
                SMda1 += C8;


                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D33 =",
                                                    "D34 =",
                                                    "D35 =",
                                                    "D36 =",
                                                    "D37 =",
                                                    "D38 =",
                                                    "D39 =",
                                                    "D40 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                          "",
                                          "(Cal 65)",
                                          "(Cal 66)",
                                          "(Cal 67)",
                                          "(Cal 68)",
                                          "(Cal 69)",
                                          "(Cal 70)",
                                          "(Cal 71)",
                                          "(Cal 72)");


                #endregion

                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "SUM ",
                                                    "" + SMab1.ToString("0.00"),
                                                    "" + SMad1.ToString("0.00"),
                                                    "" + SMba1.ToString("0.00"),
                                                    "" + SMbc1.ToString("0.00"),
                                                    "" + SMcb1.ToString("0.00"),
                                                    "" + SMcd1.ToString("0.00"),
                                                    "" + SMdc1.ToString("0.00"),
                                                    "" + SMda1.ToString("0.00"));

                sw.WriteLine("---------------------------------------------------------------------------------");


                sw.WriteLine();
                sw.WriteLine();
                #region Table Calculation

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Calculation Details");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                #region D  Cal(1-8)
                sw.WriteLine("Cal 1 : D1 = 0-(Mab1 + Mad1) * DF_ab");
                //sw.WriteLine();
                sw.WriteLine("Cal 2 : D2 = 0-(Mab1 + Mad1) * DF_ad");
                //sw.WriteLine();
                sw.WriteLine("Cal 3 : D3 = 0-(Mba1 + Mbc1) * DF_ba");
                //sw.WriteLine();
                sw.WriteLine("Cal 4 : D4 = 0-(Mba1 + Mbc1) * DF_bc");
                //sw.WriteLine();
                sw.WriteLine("Cal 5 : D5 = 0-(Mcb1 + Mcd1) * DF_cb");
                //sw.WriteLine();
                sw.WriteLine("Cal 6 : D6 = 0-(Mcb1 + Mcd1) * DF_cd");
                //sw.WriteLine();
                sw.WriteLine("Cal 7 : D7 = 0-(Mdc1 + Mda1) * DF_dc");
                //sw.WriteLine();
                sw.WriteLine("Cal 8 : D8 = 0-(Mdc1 + Mda1) * DF_da");

                #endregion

                #region C  Cal(9 - 16)
                //sw.WriteLine();
                sw.WriteLine("Cal 9  : C1 = D3 * 0.5");
                //sw.WriteLine();
                sw.WriteLine("Cal 10 : C2 = D8 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 11 : C3 = D1 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 12 : C4 = D5 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 13 : C5 = D4 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 14 : C6 = D7 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 15 : C7 = D6 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 16 : C8 = D2 * 0.5");

                #endregion

                #region D  Cal(17-24)
                //sw.WriteLine();
                sw.WriteLine("Cal 17 : D9 = 0 - (C1 + C2) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 18 : D10 = 0 - (C1 + C2) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 19 : D11 = 0 - (C3 + C4) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 20 : D12 = 0 - (C3 + C4) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 21 : D13 = 0 - (C5 + C6) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 22 : D14 = 0 - (C5 + C6) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 23 : D15 = 0 - (C7 + C8) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 24 : D16 = 0 - (C7 + C8) * 0.5");

                #endregion

                #region C  Cal(25 - 32)
                //sw.WriteLine();
                sw.WriteLine("Cal 25 : C9 = D11 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 26 : C10 = D16 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 27 : C11 = D9 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 28 : C12 = D13 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 29 : C13 = D12 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 30 : C14 = D15 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 31 : C15 = D14 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 32 : C16 = D10 * 0.5");

                #endregion

                #region D  Cal(32-40)
                //sw.WriteLine();
                sw.WriteLine("Cal 33 : D17 = 0 - (C9 + C10) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 34 : D18 = 0 - (C9 + C10) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 35 : D19 = 0 - (C11 + C12) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 36 : D20 = 0 - (C11 + C12) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 37 : D21 = 0 - (C13 + C14) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 38 : D22 = 0 - (C13 + C14) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 39 : D23 = 0 - (C15 + C16) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 40 : D24 = 0 - (C15 + C16) * 0.5");

                #endregion

                #region C  Cal(41 - 48)
                //sw.WriteLine();
                sw.WriteLine("Cal 41 : C17 = D19 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 42 : C18 = D24 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 43 : C19 = D17 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 44 : C20 = D21 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 45 : C21 = D20 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 46 : C22 = D23 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 47 : C23 = D22 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 48 : C24 = D18 * 0.5");

                #endregion

                #region D  Cal(49-56)
                //sw.WriteLine();
                sw.WriteLine("Cal 49 : D25 = 0 - (C17 + C18) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 50 : D26 = 0 - (C17 + C18) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 51 : D27 = 0 - (C19 + C20) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 52 : D28 = 0 - (C19 + C20) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 53 : D29 = 0 - (C21 + C22) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 54 : D30 = 0 - (C21 + C22) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 55 : D31 = 0 - (C23 + C24) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 56 : D32 = 0 - (C23 + C24) * 0.5");

                #endregion

                #region C  Cal(57 - 64)
                //sw.WriteLine();
                sw.WriteLine("Cal 57 : C25 = D27 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 58 : C26 = D32 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 59 : C27 = D25 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 60 : C28 = D29 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 61 : C29 = D28 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 62 : C30 = D31 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 63 : C31 = D30 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 64 : C32 = D26 * 0.5");

                #endregion

                #region D  Cal(65-72)
                //sw.WriteLine();
                sw.WriteLine("Cal 65 : D33 = 0 - (C25 + C26) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 66 : D34 = 0 - (C25 + C26) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 67 : D35 = 0 - (C27 + C28) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 68 : D36 = 0 - (C27 + C28) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 69 : D37 = 0 - (C29 + C30) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 70 : D38 = 0 - (C29 + C30) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 71 : D39 = 0 - (C31 + C32) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 72 : D40 = 0 - (C31 + C32) * 0.5");

                #endregion
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("------------------------------------------------------------");

                #endregion

                #endregion

                #region Table-2
                sw.WriteLine();
                sw.WriteLine(" Table-2 MOMENT DISTRIBUTION for TOTAL LOAD ON TOP, BOTTOM & SIDES for CASE II ");
                sw.WriteLine(" -----------------------------------------------------------------------------");
                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                    "DISTRIB",
                    "DF_ab=",
                    "DF_ad=",
                    "DF_ba=",
                    "DF_bc=",
                    "DF_cb=",
                    "DF_cd=",
                    "DF_dc=",
                    "DF_da=");

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                    "FACTORS",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5");
                sw.WriteLine("---------------------------------------------------------------------------------");

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                   "FEM",
                    "Mab2 =",
                    "Mad2 =",
                    "Mba2 =",
                    "Mbc2 =",
                    "Mcb2 =",
                    "Mcd2 =",
                    "Mdc2 =",
                    "Mda2 =");
                Mab = -m3;
                Mad = m20;
                Mba = m3;
                Mbc = -m20;
                Mcb = m22;
                Mcd = -m8;
                Mdc = m8;
                Mda = -m22;


                SMab2 = Mab;
                SMad2 = Mad;
                SMba2 = Mba;
                SMbc2 = Mbc;
                SMcb2 = Mcb;
                SMcd2 = Mcd;
                SMdc2 = Mdc;
                SMda2 = Mda;



                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                    "",
                                    "" + Mab.ToString("0.00"),
                                    "" + Mad.ToString("0.00"),
                                    "" + Mba.ToString("0.00"),
                                    "" + Mbc.ToString("0.00"),
                                    "" + Mcb.ToString("0.00"),
                                    "" + Mcd.ToString("0.00"),
                                    "" + Mdc.ToString("0.00"),
                                    "" + Mda.ToString("0.00"));

                D1 = 0 - (Mab + Mad) * 0.5;
                D2 = 0 - (Mab + Mad) * 0.5;
                D3 = 0 - (Mba + Mbc) * 0.5;
                D4 = 0 - (Mba + Mbc) * 0.5;
                D5 = 0 - (Mcb + Mcd) * 0.5;
                D6 = 0 - (Mcb + Mcd) * 0.5;
                D7 = 0 - (Mdc + Mda) * 0.5;
                D8 = 0 - (Mdc + Mda) * 0.5;


                //SMab1 += D1;
                //SMad1 += D2;
                //SMba1 += D3;
                //SMbc1 += D4;
                //SMcb1 += D5;
                //SMcd1 += D6;
                //SMdc1 += D7;
                //SMda1 += D8;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D1 =",
                                                    "D2 =",
                                                    "D3 =",
                                                    "D4 =",
                                                    "D5 =",
                                                    "D6 =",
                                                    "D7 =",
                                                    "D8 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                   "",
                                                   "(Cal 1)",
                                                   "(Cal 2)",
                                                   "(Cal 3)",
                                                   "(Cal 4)",
                                                   "(Cal 5)",
                                                   "(Cal 6)",
                                                   "(Cal 7)",
                                                   "(Cal 8)");



                C1 = D3 * 0.5;
                C2 = D8 * 0.5;
                C3 = D1 * 0.5;
                C4 = D5 * 0.5;
                C5 = D4 * 0.5;
                C6 = D7 * 0.5;
                C7 = D6 * 0.5;
                C8 = D2 * 0.5;

                SMab2 += D1;
                SMad2 += D2;
                SMba2 += D3;
                SMbc2 += D4;
                SMcb2 += D5;
                SMcd2 += D6;
                SMdc2 += D7;
                SMda2 += D8;

                SMab2 += C1;
                SMad2 += C2;
                SMba2 += C3;
                SMbc2 += C4;
                SMcb2 += C5;
                SMcd2 += C6;
                SMdc2 += C7;
                SMda2 += C8;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C1 =",
                                                    "C2 =",
                                                    "C3 =",
                                                    "C4 =",
                                                    "C5 =",
                                                    "C6 =",
                                                    "C7 =",
                                                    "C8 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                  "",
                                                  "(Cal 9)",
                                                  "(Cal 10)",
                                                  "(Cal 11)",
                                                  "(Cal 12)",
                                                  "(Cal 13)",
                                                  "(Cal 14)",
                                                  "(Cal 15)",
                                                  "(Cal 16)");


                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

                SMab2 += D1;
                SMad2 += D2;
                SMba2 += D3;
                SMbc2 += D4;
                SMcb2 += D5;
                SMcd2 += D6;
                SMdc2 += D7;
                SMda2 += D8;

                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D9 =",
                                                    "D10 =",
                                                    "D11 =",
                                                    "D12 =",
                                                    "D13 =",
                                                    "D14 =",
                                                    "D15 =",
                                                    "D16 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                               "",
                                               "(Cal 17)",
                                               "(Cal 18)",
                                               "(Cal 19)",
                                               "(Cal 20)",
                                               "(Cal 21)",
                                               "(Cal 22)",
                                               "(Cal 23)",
                                               "(Cal 24)");


                #region DEMO 1
                C1 = D3 * 0.5;
                C2 = D8 * 0.5;
                C3 = D1 * 0.5;
                C4 = D5 * 0.5;
                C5 = D4 * 0.5;
                C6 = D7 * 0.5;
                C7 = D6 * 0.5;
                C8 = D2 * 0.5;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C9 =",
                                                    "C10 =",
                                                    "C11 =",
                                                    "C12 =",
                                                    "C13 =",
                                                    "C14 =",
                                                    "C15 =",
                                                    "C16 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                             "",
                                             "(Cal 25)",
                                             "(Cal 26)",
                                             "(Cal 27)",
                                             "(Cal 28)",
                                             "(Cal 29)",
                                             "(Cal 30)",
                                             "(Cal 31)",
                                             "(Cal 32)");

                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

                SMab2 += D1;
                SMad2 += D2;
                SMba2 += D3;
                SMbc2 += D4;
                SMcb2 += D5;
                SMcd2 += D6;
                SMdc2 += D7;
                SMda2 += D8;

                SMab2 += C1;
                SMad2 += C2;
                SMba2 += C3;
                SMbc2 += C4;
                SMcb2 += C5;
                SMcd2 += C6;
                SMdc2 += C7;
                SMda2 += C8;

                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D17 =",
                                                    "D18 =",
                                                    "D19 =",
                                                    "D20 =",
                                                    "D21 =",
                                                    "D22 =",
                                                    "D23 =",
                                                    "D24 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                            "",
                                            "(Cal 33)",
                                            "(Cal 34)",
                                            "(Cal 35)",
                                            "(Cal 36)",
                                            "(Cal 37)",
                                            "(Cal 38)",
                                            "(Cal 39)",
                                            "(Cal 40)");



                #endregion

                #region DEMO 2
                C1 = D3 * 0.5;
                C2 = D8 * 0.5;
                C3 = D1 * 0.5;
                C4 = D5 * 0.5;
                C5 = D4 * 0.5;
                C6 = D7 * 0.5;
                C7 = D6 * 0.5;
                C8 = D2 * 0.5;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C17 =",
                                                    "C18 =",
                                                    "C19 =",
                                                    "C20 =",
                                                    "C21 =",
                                                    "C22 =",
                                                    "C23 =",
                                                    "C24 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                            "",
                                            "(Cal 41)",
                                            "(Cal 42)",
                                            "(Cal 43)",
                                            "(Cal 44)",
                                            "(Cal 45)",
                                            "(Cal 46)",
                                            "(Cal 47)",
                                            "(Cal 48)");


                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

                SMab2 += D1;
                SMad2 += D2;
                SMba2 += D3;
                SMbc2 += D4;
                SMcb2 += D5;
                SMcd2 += D6;
                SMdc2 += D7;
                SMda2 += D8;

                SMab2 += C1;
                SMad2 += C2;
                SMba2 += C3;
                SMbc2 += C4;
                SMcb2 += C5;
                SMcd2 += C6;
                SMdc2 += C7;
                SMda2 += C8;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D25 =",
                                                    "D26 =",
                                                    "D27 =",
                                                    "D28 =",
                                                    "D29 =",
                                                    "D30 =",
                                                    "D31 =",
                                                    "D32 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                           "",
                                           "(Cal 49)",
                                           "(Cal 50)",
                                           "(Cal 51)",
                                           "(Cal 52)",
                                           "(Cal 53)",
                                           "(Cal 54)",
                                           "(Cal 55)",
                                           "(Cal 56)");

                #endregion

                #region DEMO 3
                C1 = D3 * 0.5;
                C2 = D8 * 0.5;
                C3 = D1 * 0.5;
                C4 = D5 * 0.5;
                C5 = D4 * 0.5;
                C6 = D7 * 0.5;
                C7 = D6 * 0.5;
                C8 = D2 * 0.5;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C25 =",
                                                    "C26 =",
                                                    "C27 =",
                                                    "C28 =",
                                                    "C29 =",
                                                    "C30 =",
                                                    "C31 =",
                                                    "C32 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                           "",
                                           "(Cal 57)",
                                           "(Cal 58)",
                                           "(Cal 59)",
                                           "(Cal 60)",
                                           "(Cal 61)",
                                           "(Cal 62)",
                                           "(Cal 63)",
                                           "(Cal 64)");

                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

                SMab2 += D1;
                SMad2 += D2;
                SMba2 += D3;
                SMbc2 += D4;
                SMcb2 += D5;
                SMcd2 += D6;
                SMdc2 += D7;
                SMda2 += D8;

                SMab2 += C1;
                SMad2 += C2;
                SMba2 += C3;
                SMbc2 += C4;
                SMcb2 += C5;
                SMcd2 += C6;
                SMdc2 += C7;
                SMda2 += C8;


                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D33 =",
                                                    "D34 =",
                                                    "D35 =",
                                                    "D36 =",
                                                    "D37 =",
                                                    "D38 =",
                                                    "D39 =",
                                                    "D40 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                          "",
                                          "(Cal 65)",
                                          "(Cal 66)",
                                          "(Cal 67)",
                                          "(Cal 68)",
                                          "(Cal 69)",
                                          "(Cal 70)",
                                          "(Cal 71)",
                                          "(Cal 72)");


                #endregion

                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "SUM ",
                                                    "" + SMab2.ToString("0.00"),
                                                    "" + SMad2.ToString("0.00"),
                                                    "" + SMba2.ToString("0.00"),
                                                    "" + SMbc2.ToString("0.00"),
                                                    "" + SMcb2.ToString("0.00"),
                                                    "" + SMcd2.ToString("0.00"),
                                                    "" + SMdc2.ToString("0.00"),
                                                    "" + SMda2.ToString("0.00"));

                sw.WriteLine("---------------------------------------------------------------------------------");


                sw.WriteLine();
                sw.WriteLine();
                #region Table Calculation

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Calculation Details");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                #region D  Cal(1-8)
                sw.WriteLine("Cal 1  : D1 = 0-(Mab2 + Mad2) * DF_ab");
                //sw.WriteLine();
                sw.WriteLine("Cal 2  : D2 = 0-(Mab2 + Mad2) * DF_ad");
                //sw.WriteLine();
                sw.WriteLine("Cal 3  : D3 = 0-(Mba2 + Mbc2) * DF_ba");
                //sw.WriteLine();
                sw.WriteLine("Cal 4  : D4 = 0-(Mba2 + Mbc2) * DF_bc");
                //sw.WriteLine();
                sw.WriteLine("Cal 5  : D5 = 0-(Mcb2 + Mcd2) * DF_cb");
                //sw.WriteLine();
                sw.WriteLine("Cal 6  : D6 = 0-(Mcb2 + Mcd2) * DF_cd");
                //sw.WriteLine();
                sw.WriteLine("Cal 7  : D7 = 0-(Mdc2 + Mda2) * DF_dc");
                //sw.WriteLine();
                sw.WriteLine("Cal 8  : D8 = 0-(Mdc2 + Mda2) * DF_da");

                #endregion

                #region C  Cal(9 - 16)
                //sw.WriteLine();
                sw.WriteLine("Cal 9  : C1 = D3 * 0.5");
                //sw.WriteLine();
                sw.WriteLine("Cal 10 : C2 = D8 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 11 : C3 = D1 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 12 : C4 = D5 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 13 : C5 = D4 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 14 : C6 = D7 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 15 : C7 = D6 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 16 : C8 = D2 * 0.5");

                #endregion

                #region D  Cal(17-24)
                //sw.WriteLine();
                sw.WriteLine("Cal 17 : D9 = 0 - (C1 + C2) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 18 : D10 = 0 - (C1 + C2) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 19 : D11 = 0 - (C3 + C4) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 20 : D12 = 0 - (C3 + C4) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 21 : D13 = 0 - (C5 + C6) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 22 : D14 = 0 - (C5 + C6) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 23 : D15 = 0 - (C7 + C8) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 24 : D16 = 0 - (C7 + C8) * 0.5");

                #endregion

                #region C  Cal(25 - 32)
                //sw.WriteLine();
                sw.WriteLine("Cal 25 : C9 = D11 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 26 : C10 = D16 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 27 : C11 = D9 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 28 : C12 = D13 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 29 : C13 = D12 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 30 : C14 = D15 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 31 : C15 = D14 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 32 : C16 = D10 * 0.5");

                #endregion

                #region D  Cal(32-40)
                //sw.WriteLine();
                sw.WriteLine("Cal 33 : D17 = 0 - (C9 + C10) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 34 : D18 = 0 - (C9 + C10) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 35 : D19 = 0 - (C11 + C12) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 36 : D20 = 0 - (C11 + C12) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 37 : D21 = 0 - (C13 + C14) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 38 : D22 = 0 - (C13 + C14) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 39 : D23 = 0 - (C15 + C16) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 40 : D24 = 0 - (C15 + C16) * 0.5");

                #endregion

                #region C  Cal(41 - 48)
                //sw.WriteLine();
                sw.WriteLine("Cal 41 : C17 = D19 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 42 : C18 = D24 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 43 : C19 = D17 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 44 : C20 = D21 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 45 : C21 = D20 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 46 : C22 = D23 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 47 : C23 = D22 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 48 : C24 = D18 * 0.5");

                #endregion

                #region D  Cal(49-56)
                //sw.WriteLine();
                sw.WriteLine("Cal 49 : D25 = 0 - (C17 + C18) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 50 : D26 = 0 - (C17 + C18) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 51 : D27 = 0 - (C19 + C20) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 52 : D28 = 0 - (C19 + C20) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 53 : D29 = 0 - (C21 + C22) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 54 : D30 = 0 - (C21 + C22) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 55 : D31 = 0 - (C23 + C24) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 56 : D32 = 0 - (C23 + C24) * 0.5");

                #endregion

                #region C  Cal(57 - 64)
                //sw.WriteLine();
                sw.WriteLine("Cal 57 : C25 = D27 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 58 : C26 = D32 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 59 : C27 = D25 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 60 : C28 = D29 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 61 : C29 = D28 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 62 : C30 = D31 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 63 : C31 = D30 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 64 : C32 = D26 * 0.5");

                #endregion

                #region D  Cal(65-72)
                //sw.WriteLine();
                sw.WriteLine("Cal 65 : D33 = 0 - (C25 + C26) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 66 : D34 = 0 - (C25 + C26) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 67 : D35 = 0 - (C27 + C28) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 68 : D36 = 0 - (C27 + C28) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 69 : D37 = 0 - (C29 + C30) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 70 : D38 = 0 - (C29 + C30) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 71 : D39 = 0 - (C31 + C32) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 72 : D40 = 0 - (C31 + C32) * 0.5");

                #endregion
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("------------------------------------------------------------");

                #endregion

                #endregion

                #region Table-3
                sw.WriteLine();
                sw.WriteLine(" Table-3 MOMENT DISTRIBUTION for TOTAL LOAD ON TOP, BOTTOM & SIDES for CASE III ");
                sw.WriteLine(" -----------------------------------------------------------------------------");
                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                    "DISTRIB",
                    "DF_ab=",
                    "DF_ad=",
                    "DF_ba=",
                    "DF_bc=",
                    "DF_cb=",
                    "DF_cd=",
                    "DF_dc=",
                    "DF_da=");

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                    "FACTORS",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5",
                    "0.5");
                sw.WriteLine("---------------------------------------------------------------------------------");

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                   "FEM",
                   "Mab3 =",
                    "Mad3 =",
                    "Mba3 =",
                    "Mbc3 =",
                    "Mcb3 =",
                    "Mcd3 =",
                    "Mdc3 =",
                    "Mda3 =");
                Mab = -m3;
                Mad = m25;
                Mba = m3;
                Mbc = -m25;
                Mcb = m27;
                Mcd = -m8;
                Mdc = m8;
                Mda = -m27;

                SMab3 = Mab;
                SMad3 = Mad;
                SMba3 = Mba;
                SMbc3 = Mbc;
                SMcb3 = Mcb;
                SMcd3 = Mcd;
                SMdc3 = Mdc;
                SMda3 = Mda;



                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                    "",
                                    "" + Mab.ToString("0.00"),
                                    "" + Mad.ToString("0.00"),
                                    "" + Mba.ToString("0.00"),
                                    "" + Mbc.ToString("0.00"),
                                    "" + Mcb.ToString("0.00"),
                                    "" + Mcd.ToString("0.00"),
                                    "" + Mdc.ToString("0.00"),
                                    "" + Mda.ToString("0.00"));

                D1 = 0 - (Mab + Mad) * 0.5;
                D2 = 0 - (Mab + Mad) * 0.5;
                D3 = 0 - (Mba + Mbc) * 0.5;
                D4 = 0 - (Mba + Mbc) * 0.5;
                D5 = 0 - (Mcb + Mcd) * 0.5;
                D6 = 0 - (Mcb + Mcd) * 0.5;
                D7 = 0 - (Mdc + Mda) * 0.5;
                D8 = 0 - (Mdc + Mda) * 0.5;


                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D1 =",
                                                    "D2 =",
                                                    "D3 =",
                                                    "D4 =",
                                                    "D5 =",
                                                    "D6 =",
                                                    "D7 =",
                                                    "D8 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                   "",
                                                   "(Cal 1)",
                                                   "(Cal 2)",
                                                   "(Cal 3)",
                                                   "(Cal 4)",
                                                   "(Cal 5)",
                                                   "(Cal 6)",
                                                   "(Cal 7)",
                                                   "(Cal 8)");



                C1 = D3 * 0.5;
                C2 = D8 * 0.5;
                C3 = D1 * 0.5;
                C4 = D5 * 0.5;
                C5 = D4 * 0.5;
                C6 = D7 * 0.5;
                C7 = D6 * 0.5;
                C8 = D2 * 0.5;

                SMab3 += D1;
                SMad3 += D2;
                SMba3 += D3;
                SMbc3 += D4;
                SMcb3 += D5;
                SMcd3 += D6;
                SMdc3 += D7;
                SMda3 += D8;

                SMab3 += C1;
                SMad3 += C2;
                SMba3 += C3;
                SMbc3 += C4;
                SMcb3 += C5;
                SMcd3 += C6;
                SMdc3 += C7;
                SMda3 += C8;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C1 =",
                                                    "C2 =",
                                                    "C3 =",
                                                    "C4 =",
                                                    "C5 =",
                                                    "C6 =",
                                                    "C7 =",
                                                    "C8 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                  "",
                                                  "(Cal 9)",
                                                  "(Cal 10)",
                                                  "(Cal 11)",
                                                  "(Cal 12)",
                                                  "(Cal 13)",
                                                  "(Cal 14)",
                                                  "(Cal 15)",
                                                  "(Cal 16)");


                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

                SMab3 += D1;
                SMad3 += D2;
                SMba3 += D3;
                SMbc3 += D4;
                SMcb3 += D5;
                SMcd3 += D6;
                SMdc3 += D7;
                SMda3 += D8;

                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D9 =",
                                                    "D10 =",
                                                    "D11 =",
                                                    "D12 =",
                                                    "D13 =",
                                                    "D14 =",
                                                    "D15 =",
                                                    "D16 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                               "",
                                               "(Cal 17)",
                                               "(Cal 18)",
                                               "(Cal 19)",
                                               "(Cal 20)",
                                               "(Cal 21)",
                                               "(Cal 22)",
                                               "(Cal 23)",
                                               "(Cal 24)");


                #region DEMO 1
                C1 = D3 * 0.5;
                C2 = D8 * 0.5;
                C3 = D1 * 0.5;
                C4 = D5 * 0.5;
                C5 = D4 * 0.5;
                C6 = D7 * 0.5;
                C7 = D6 * 0.5;
                C8 = D2 * 0.5;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C9 =",
                                                    "C10 =",
                                                    "C11 =",
                                                    "C12 =",
                                                    "C13 =",
                                                    "C14 =",
                                                    "C15 =",
                                                    "C16 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                             "",
                                             "(Cal 25)",
                                             "(Cal 26)",
                                             "(Cal 27)",
                                             "(Cal 28)",
                                             "(Cal 29)",
                                             "(Cal 30)",
                                             "(Cal 31)",
                                             "(Cal 32)");

                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

                SMab3 += D1;
                SMad3 += D2;
                SMba3 += D3;
                SMbc3 += D4;
                SMcb3 += D5;
                SMcd3 += D6;
                SMdc3 += D7;
                SMda3 += D8;

                SMab3 += C1;
                SMad3 += C2;
                SMba3 += C3;
                SMbc3 += C4;
                SMcb3 += C5;
                SMcd3 += C6;
                SMdc3 += C7;
                SMda3 += C8;

                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D17 =",
                                                    "D18 =",
                                                    "D19 =",
                                                    "D20 =",
                                                    "D21 =",
                                                    "D22 =",
                                                    "D23 =",
                                                    "D24 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                            "",
                                            "(Cal 33)",
                                            "(Cal 34)",
                                            "(Cal 35)",
                                            "(Cal 36)",
                                            "(Cal 37)",
                                            "(Cal 38)",
                                            "(Cal 39)",
                                            "(Cal 40)");



                #endregion

                #region DEMO 2
                C1 = D3 * 0.5;
                C2 = D8 * 0.5;
                C3 = D1 * 0.5;
                C4 = D5 * 0.5;
                C5 = D4 * 0.5;
                C6 = D7 * 0.5;
                C7 = D6 * 0.5;
                C8 = D2 * 0.5;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C17 =",
                                                    "C18 =",
                                                    "C19 =",
                                                    "C20 =",
                                                    "C21 =",
                                                    "C22 =",
                                                    "C23 =",
                                                    "C24 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                            "",
                                            "(Cal 41)",
                                            "(Cal 42)",
                                            "(Cal 43)",
                                            "(Cal 44)",
                                            "(Cal 45)",
                                            "(Cal 46)",
                                            "(Cal 47)",
                                            "(Cal 48)");


                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

                SMab3 += D1;
                SMad3 += D2;
                SMba3 += D3;
                SMbc3 += D4;
                SMcb3 += D5;
                SMcd3 += D6;
                SMdc3 += D7;
                SMda3 += D8;

                SMab3 += C1;
                SMad3 += C2;
                SMba3 += C3;
                SMbc3 += C4;
                SMcb3 += C5;
                SMcd3 += C6;
                SMdc3 += C7;
                SMda3 += C8;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D25 =",
                                                    "D26 =",
                                                    "D27 =",
                                                    "D28 =",
                                                    "D29 =",
                                                    "D30 =",
                                                    "D31 =",
                                                    "D32 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                           "",
                                           "(Cal 49)",
                                           "(Cal 50)",
                                           "(Cal 51)",
                                           "(Cal 52)",
                                           "(Cal 53)",
                                           "(Cal 54)",
                                           "(Cal 55)",
                                           "(Cal 56)");

                #endregion

                #region DEMO 3
                C1 = D3 * 0.5;
                C2 = D8 * 0.5;
                C3 = D1 * 0.5;
                C4 = D5 * 0.5;
                C5 = D4 * 0.5;
                C6 = D7 * 0.5;
                C7 = D6 * 0.5;
                C8 = D2 * 0.5;

                sw.WriteLine();
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "CO",
                                                    "C25 =",
                                                    "C26 =",
                                                    "C27 =",
                                                    "C28 =",
                                                    "C29 =",
                                                    "C30 =",
                                                    "C31 =",
                                                    "C32 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + C1.ToString("0.00"),
                                                    "" + C2.ToString("0.00"),
                                                    "" + C3.ToString("0.00"),
                                                    "" + C4.ToString("0.00"),
                                                    "" + C5.ToString("0.00"),
                                                    "" + C6.ToString("0.00"),
                                                    "" + C7.ToString("0.00"),
                                                    "" + C8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                           "",
                                           "(Cal 57)",
                                           "(Cal 58)",
                                           "(Cal 59)",
                                           "(Cal 60)",
                                           "(Cal 61)",
                                           "(Cal 62)",
                                           "(Cal 63)",
                                           "(Cal 64)");

                D1 = 0 - (C1 + C2) * 0.5;
                D2 = 0 - (C1 + C2) * 0.5;
                D3 = 0 - (C3 + C4) * 0.5;
                D4 = 0 - (C3 + C4) * 0.5;
                D5 = 0 - (C5 + C6) * 0.5;
                D6 = 0 - (C5 + C6) * 0.5;
                D7 = 0 - (C7 + C8) * 0.5;
                D8 = 0 - (C7 + C8) * 0.5;

                SMab3 += D1;
                SMad3 += D2;
                SMba3 += D3;
                SMbc3 += D4;
                SMcb3 += D5;
                SMcd3 += D6;
                SMdc3 += D7;
                SMda3 += D8;

                SMab3 += C1;
                SMad3 += C2;
                SMba3 += C3;
                SMbc3 += C4;
                SMcb3 += C5;
                SMcd3 += C6;
                SMdc3 += C7;
                SMda3 += C8;


                sw.WriteLine();

                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "DIST",
                                                    "D33 =",
                                                    "D34 =",
                                                    "D35 =",
                                                    "D36 =",
                                                    "D37 =",
                                                    "D38 =",
                                                    "D39 =",
                                                    "D40 =");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "",
                                                    "" + D1.ToString("0.00"),
                                                    "" + D2.ToString("0.00"),
                                                    "" + D3.ToString("0.00"),
                                                    "" + D4.ToString("0.00"),
                                                    "" + D5.ToString("0.00"),
                                                    "" + D6.ToString("0.00"),
                                                    "" + D7.ToString("0.00"),
                                                    "" + D8.ToString("0.00"));
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                          "",
                                          "(Cal 65)",
                                          "(Cal 66)",
                                          "(Cal 67)",
                                          "(Cal 68)",
                                          "(Cal 69)",
                                          "(Cal 70)",
                                          "(Cal 71)",
                                          "(Cal 72)");


                #endregion

                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.WriteLine("{0,-9}{1,-9}{2,-9}{3,-9}{4,-9}{5,-9}{6,-9}{7,-9}{8,-9}",
                                                    "SUM ",
                                                    "" + SMab3.ToString("0.00"),
                                                    "" + SMad3.ToString("0.00"),
                                                    "" + SMba3.ToString("0.00"),
                                                    "" + SMbc3.ToString("0.00"),
                                                    "" + SMcb3.ToString("0.00"),
                                                    "" + SMcd3.ToString("0.00"),
                                                    "" + SMdc3.ToString("0.00"),
                                                    "" + SMda3.ToString("0.00"));

                sw.WriteLine("---------------------------------------------------------------------------------");


                sw.WriteLine();
                sw.WriteLine();
                #region Table Calculation

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Calculation Details");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();


                #region D  Cal(1-8)
                sw.WriteLine("Cal 1  : D1 = 0-(Mab2 + Mad2) * DF_ab");
                //sw.WriteLine();
                sw.WriteLine("Cal 2  : D2 = 0-(Mab2 + Mad2) * DF_ad");
                //sw.WriteLine();
                sw.WriteLine("Cal 3  : D3 = 0-(Mba2 + Mbc2) * DF_ba");
                //sw.WriteLine();
                sw.WriteLine("Cal 4  : D4 = 0-(Mba2 + Mbc2) * DF_bc");
                //sw.WriteLine();
                sw.WriteLine("Cal 5  : D5 = 0-(Mcb2 + Mcd2) * DF_cb");
                //sw.WriteLine();
                sw.WriteLine("Cal 6  : D6 = 0-(Mcb2 + Mcd2) * DF_cd");
                //sw.WriteLine();
                sw.WriteLine("Cal 7  : D7 = 0-(Mdc2 + Mda2) * DF_dc");
                //sw.WriteLine();
                sw.WriteLine("Cal 8  : D8 = 0-(Mdc2 + Mda2) * DF_da");

                #endregion

                #region C  Cal(9 - 16)
                //sw.WriteLine();
                sw.WriteLine("Cal 9  : C1 = D3 * 0.5");
                //sw.WriteLine();
                sw.WriteLine("Cal 10 : C2 = D8 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 11 : C3 = D1 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 12 : C4 = D5 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 13 : C5 = D4 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 14 : C6 = D7 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 15 : C7 = D6 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 16 : C8 = D2 * 0.5");

                #endregion

                #region D  Cal(17-24)
                //sw.WriteLine();
                sw.WriteLine("Cal 17 : D9 = 0 - (C1 + C2) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 18 : D10 = 0 - (C1 + C2) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 19 : D11 = 0 - (C3 + C4) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 20 : D12 = 0 - (C3 + C4) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 21 : D13 = 0 - (C5 + C6) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 22 : D14 = 0 - (C5 + C6) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 23 : D15 = 0 - (C7 + C8) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 24 : D16 = 0 - (C7 + C8) * 0.5");

                #endregion

                #region C  Cal(25 - 32)
                //sw.WriteLine();
                sw.WriteLine("Cal 25 : C9 = D11 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 26 : C10 = D16 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 27 : C11 = D9 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 28 : C12 = D13 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 29 : C13 = D12 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 30 : C14 = D15 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 31 : C15 = D14 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 32 : C16 = D10 * 0.5");

                #endregion

                #region D  Cal(32-40)
                //sw.WriteLine();
                sw.WriteLine("Cal 33 : D17 = 0 - (C9 + C10) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 34 : D18 = 0 - (C9 + C10) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 35 : D19 = 0 - (C11 + C12) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 36 : D20 = 0 - (C11 + C12) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 37 : D21 = 0 - (C13 + C14) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 38 : D22 = 0 - (C13 + C14) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 39 : D23 = 0 - (C15 + C16) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 40 : D24 = 0 - (C15 + C16) * 0.5");

                #endregion

                #region C  Cal(41 - 48)
                //sw.WriteLine();
                sw.WriteLine("Cal 41 : C17 = D19 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 42 : C18 = D24 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 43 : C19 = D17 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 44 : C20 = D21 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 45 : C21 = D20 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 46 : C22 = D23 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 47 : C23 = D22 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 48 : C24 = D18 * 0.5");

                #endregion

                #region D  Cal(49-56)
                //sw.WriteLine();
                sw.WriteLine("Cal 49 : D25 = 0 - (C17 + C18) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 50 : D26 = 0 - (C17 + C18) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 51 : D27 = 0 - (C19 + C20) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 52 : D28 = 0 - (C19 + C20) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 53 : D29 = 0 - (C21 + C22) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 54 : D30 = 0 - (C21 + C22) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 55 : D31 = 0 - (C23 + C24) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 56 : D32 = 0 - (C23 + C24) * 0.5");

                #endregion

                #region C  Cal(57 - 64)
                //sw.WriteLine();
                sw.WriteLine("Cal 57 : C25 = D27 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 58 : C26 = D32 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 59 : C27 = D25 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 60 : C28 = D29 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 61 : C29 = D28 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 62 : C30 = D31 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 63 : C31 = D30 * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 64 : C32 = D26 * 0.5");

                #endregion

                #region D  Cal(65-72)
                //sw.WriteLine();
                sw.WriteLine("Cal 65 : D33 = 0 - (C25 + C26) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 66 : D34 = 0 - (C25 + C26) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 67 : D35 = 0 - (C27 + C28) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 68 : D36 = 0 - (C27 + C28) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 69 : D37 = 0 - (C29 + C30) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 70 : D38 = 0 - (C29 + C30) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 71 : D39 = 0 - (C31 + C32) * 0.5");

                //sw.WriteLine();
                sw.WriteLine("Cal 72 : D40 = 0 - (C31 + C32) * 0.5");

                #endregion
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("------------------------------------------------------------");

                #endregion

                #endregion

                #region TABLE-4 SUMMARY OF SUPPORT MOMENTS FOR TOTAL LOADS

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("TABLE-4 SUMMARY OF SUPPORT MOMENTS FOR TOTAL LOADS");
                sw.WriteLine("--------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------------------------------");
                sw.WriteLine("{0,-10}{1,12}{2,18}{3,18}{4,18}",
                    "CASES",
                    "Mab",
                    "Mdc",
                    "Mad",
                    "Mda");
                sw.WriteLine("------------------------------------------------------------------------------------");
                sw.WriteLine("{0,-10}{1,18}{2,18}{3,18}{4,18}",
                                    "CASE 1",
                                    "SMab1 = " + Math.Abs(SMab1).ToString("0.000"),
                                    "SMdc1 = " + Math.Abs(SMdc1).ToString("0.000"),
                                    "SMad1 = " + Math.Abs(SMad1).ToString("0.000"),
                                    "SMda1 = " + Math.Abs(SMda1).ToString("0.000"));
                sw.WriteLine("{0,-10}{1,18}{2,18}{3,18}{4,18}",
                    "CASE 2",
                    "SMab2 = " + Math.Abs(SMab2).ToString("0.000"),
                    "SMdc2 = " + Math.Abs(SMdc2).ToString("0.000"),
                    "SMad2 = " + Math.Abs(SMad2).ToString("0.000"),
                    "SMda2 = " + Math.Abs(SMda2).ToString("0.000"));
                sw.WriteLine("{0,-10}{1,18}{2,18}{3,18}{4,18}",
                                    "CASE 3",
                                    "SMab3 = " + Math.Abs(SMab3).ToString("0.000"),
                                    "SMdc3 = " + Math.Abs(SMdc3).ToString("0.000"),
                                    "SMad3 = " + Math.Abs(SMad3).ToString("0.000"),
                                    "SMda3 = " + Math.Abs(SMda3).ToString("0.000"));
                sw.WriteLine("------------------------------------------------------------------------------------");

                double SMabx, SMdcx, SMadx, SMdax;
                SMabx = 0.0;
                SMdcx = 0.0;
                SMadx = 0.0;
                SMdax = 0.0;

                //SMabx = (SMab1 > SMab2) ? ((SMab1 > SMab3) ? SMab1 : SMab3) : ((SMab2 > SMab3) ? SMab2 : SMab3);
                //SMdcx = (SMdc1 > SMdc2) ? ((SMdc1 > SMdc3) ? SMdc1 : SMdc3) : ((SMdc2 > SMdc3) ? SMdc2 : SMdc3);
                //SMadx = (SMad1 > SMad2) ? ((SMad1 > SMad3) ? SMad1 : SMad3) : ((SMad2 > SMad3) ? SMad2 : SMad3);
                //SMdax = (SMda1 > SMda2) ? ((SMda1 > SMda3) ? SMda1 : SMda3) : ((SMda2 > SMda3) ? SMda2 : SMda3);

                SMabx = (Math.Abs(SMab1) > Math.Abs(SMab2)) ? ((Math.Abs(SMab1) > Math.Abs(SMab3)) ? Math.Abs(SMab1) : Math.Abs(SMab3)) : ((Math.Abs(SMab2) > Math.Abs(SMab3)) ? Math.Abs(SMab2) : Math.Abs(SMab3));
                SMdcx = (Math.Abs(SMdc1) > Math.Abs(SMdc2)) ? ((Math.Abs(SMdc1) > Math.Abs(SMdc3)) ? Math.Abs(SMdc1) : Math.Abs(SMdc3)) : ((Math.Abs(SMdc2) > Math.Abs(SMdc3)) ? Math.Abs(SMdc2) : Math.Abs(SMdc3));
                SMadx = (Math.Abs(SMad1) > Math.Abs(SMad2)) ? ((Math.Abs(SMad1) > Math.Abs(SMad3)) ? Math.Abs(SMad1) : Math.Abs(SMad3)) : ((Math.Abs(SMad2) > Math.Abs(SMad3)) ? Math.Abs(SMad2) : Math.Abs(SMad3));
                SMdax = (Math.Abs(SMda1) > Math.Abs(SMda2)) ? ((Math.Abs(SMda1) > Math.Abs(SMda3)) ? Math.Abs(SMda1) : Math.Abs(SMda3)) : ((Math.Abs(SMda2) > Math.Abs(SMda3)) ? Math.Abs(SMda2) : Math.Abs(SMda3));

                //SMdcx = (SMdc1 > SMdc2) ? ((SMdc1 > SMdc3) ? SMdc1 : SMdc3) : ((SMdc2 > SMdc3) ? SMdc2 : SMdc3);
                //SMadx = (SMad1 > SMad2) ? ((SMad1 > SMad3) ? SMad1 : SMad3) : ((SMad2 > SMad3) ? SMad2 : SMad3);
                //SMdax = (SMda1 > SMda2) ? ((SMda1 > SMda3) ? SMda1 : SMda3) : ((SMda2 > SMda3) ? SMda2 : SMda3);


                sw.WriteLine("{0,-10}{1,18}{2,18}{3,18}{4,18}",
                                      "MAXIMUM",
                                    "SMabx = " + SMabx.ToString("0.000"),
                                    "SMdcx = " + SMdcx.ToString("0.000"),
                                    "SMadx = " + SMadx.ToString("0.000"),
                                    "SMdax = " + SMdax.ToString("0.000"));

                sw.WriteLine("------------------------------------------------------------------------------------");

                #endregion

                #region TABLE 5 : MID SPAN MOMENT
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("TABLE-5 MID SPAN MOMENTS");
                sw.WriteLine("------------------------");
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------------------------------");
                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                    "",
                    "CASE 1",
                    "CASE 2",
                    "CASE 3");
                sw.WriteLine("------------------------------------------------------------------------------------");
                List<double> lst_Mab, lst_Mdc, lst_Mad;
                lst_Mab = new List<double>();

                lst_Mab.Add(m6 - Math.Abs(SMab1));
                lst_Mab.Add(m6 - Math.Abs(SMab2));
                lst_Mab.Add(m6 - Math.Abs(SMab3));
                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                                                   "Mab",
                    String.Format("  {0:f2} - {1:f2}", "m6", "SMab1"),
                    String.Format("  {0:f2} - {1:f2}", "m6", "SMab2"),
                    String.Format("  {0:f2} - {1:f2}", "m6", "SMab3"));
                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                    "",
                    String.Format("= {0:f2} - {1:f2}", m6, Math.Abs(SMab1)),
                    String.Format("= {0:f2} - {1:f2}", m6, Math.Abs(SMab2)),
                    String.Format("= {0:f2} - {1:f2}", m6, Math.Abs(SMab3)));


                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                                    "",
                                    String.Format("= {0:f2}", lst_Mab[0]),
                                    String.Format("= {0:f2}", lst_Mab[1]),
                                    String.Format("= {0:f2}", lst_Mab[2]));
                sw.WriteLine("------------------------------------------------------------------------------------");


                lst_Mdc = new List<double>();
                lst_Mdc.Add(m10 - Math.Abs(SMdc1));
                lst_Mdc.Add(m10 - Math.Abs(SMdc2));
                lst_Mdc.Add(m10 - Math.Abs(SMdc3));

                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                                                   "Mdc",
                    String.Format("  {0:f2} - {1:f2}", "m10", "SMdc1"),
                    String.Format("  {0:f2} - {1:f2}", "m10", "SMdc2"),
                    String.Format("  {0:f2} - {1:f2}", "m10", "SMdc3"));


                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                    "",
                    String.Format("= {0:f2} - {1:f2}", m10, Math.Abs(SMdc1)),
                    String.Format("= {0:f2} - {1:f2}", m10, Math.Abs(SMdc2)),
                    String.Format("= {0:f2} - {1:f2}", m10, Math.Abs(SMdc3)));


                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                                    "",
                                    String.Format("= {0:f2}", lst_Mdc[0]),
                                    String.Format("= {0:f2}", lst_Mdc[1]),
                                    String.Format("= {0:f2}", lst_Mdc[2]));





                sw.WriteLine("------------------------------------------------------------------------------------");

                lst_Mad = new List<double>();
                lst_Mad.Add(m18 - (Math.Abs(SMad1) + Math.Abs(SMda1)) / 2.0);
                lst_Mad.Add(m24 - (Math.Abs(SMad2) + Math.Abs(SMda2)) / 2.0);
                lst_Mad.Add(m29 - (Math.Abs(SMad3) + Math.Abs(SMda3)) / 2.0);



                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                                                   "Mad",
                    String.Format("  {0:f2}-{1:f2}", "m18", "(SMad1+SMda1)/2"),
                    String.Format("  {0:f2}-{1:f2}", "m24", "(SMad2+SMda2)/2"),
                    String.Format("  {0:f2}-{1:f2}", "m29", "(SMad3+SMda3)/2"));


                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                    "",
                    String.Format("= {0:f2}-({1:f2}+{2:F2})/2", m18, (Math.Abs(SMad1)), Math.Abs(SMda1)),
                    String.Format("= {0:f2}-({1:f2}+{2:F2})/2", m24, (Math.Abs(SMad2)), Math.Abs(SMda2)),
                    String.Format("= {0:f2}-({1:f2}+{2:F2})/2", m29, (Math.Abs(SMad3)), Math.Abs(SMda3)));


                sw.WriteLine("{0,-5}{1,-27}{2,-27}{3,-25}",
                                    "",
                                    String.Format("= {0:f2}", lst_Mad[0]),
                                    String.Format("= {0:f2}", lst_Mad[1]),
                                    String.Format("= {0:f2}", lst_Mad[2]));
                sw.WriteLine("------------------------------------------------------------------------------------");
                sw.WriteLine("------------------------------------------------------------------------------------");
                #endregion

                #region Taking Maiximum Values, Design Moments
                sw.WriteLine();
                sw.WriteLine("  Taking Maximum Values, Design Moments");
                sw.WriteLine();
                sw.WriteLine("      M_AB for Corners /Supports A & B        = {0:f3}", SMabx);
                sw.WriteLine("      M_DC for Corners /Supports C & D        = {0:f3}", SMdcx);
                lst_Mab.Sort();
                sw.WriteLine("      M_AB for Mid span of the Top Slab       = {0:f3}", lst_Mab[2]);

                lst_Mdc.Sort();
                sw.WriteLine("      M_DC for Mid span of the Bottom Slab    = {0:f3}", lst_Mdc[2]);

                lst_Mad.Sort();
                sw.WriteLine("      M_AD for Mid span of the Two side walls = {0:f3}", lst_Mad[2]);
                #endregion


                #region STEP 5

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : DESIGN OF TOP SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double M = SMabx;
                sw.WriteLine("  Maximum Bending Moment at Support / Midspan");
                sw.WriteLine("  M = {0:f2} kN-m", M);
                double depth = Math.Sqrt((M * 10E5) / (1000 * R));
                sw.WriteLine();
                sw.WriteLine("  Depth = d = √((M * 10E5) / (1000 * R)");
                sw.WriteLine("            = √(({0:f2} * 10E5) / (1000 * {1:f3}))", M, R);
                sw.WriteLine("            = {0:f3} mm", depth);
                sw.WriteLine();
                sw.WriteLine("  Provided Slab Thickness = {0:f2} mm", (d1 * 1000));
                double bar_dia = 16;
                sw.WriteLine();
                sw.WriteLine("  Considering Clear Cover = {0:f2} mm and Reinforcement bar dia = {1} mm",
                    cover, bar_dia);
                double deff = (d1 * 1000) - cover - (bar_dia / 2.0);
                sw.WriteLine();
                if (deff > depth)
                {
                    sw.WriteLine("  Effective Depth = deff = {0:f2} - {1:f2} - {2:f0}/2 = {3:f2} mm > {4:f2} mm OK",
                        (d1 * 1000), cover, bar_dia, deff, depth);
                }
                else
                {
                    sw.WriteLine("  Effective Depth = deff = {0:f2} - {1:f2} - {2:f2} = {3:f2} mm < {4:f2} mm NOT OK",
                        (d1 * 1000), cover, (bar_dia / 2.0), deff, depth);
                }

                double req_st_ast = (M * 10E5) / (t * j * deff);
                sw.WriteLine();
                sw.WriteLine("  Required Main Reinforcement Steel Ast = (M*10E5)/(t*j*d)");
                sw.WriteLine("                                        = ({0:f2}*10E5)/({1:f2}*{2:f3}*{3:f2})",
                    M,
                    t, j, deff);
                sw.WriteLine("                                        = {0:f3} sq.mm.", req_st_ast);
                double pro_st_ast = (Math.PI * bar_dia * bar_dia / 4);

                double spacing = req_st_ast / pro_st_ast;
                spacing = (int)((1000.0 / spacing) / 10.0);
                spacing = spacing * 10.0;



                pro_st_ast = pro_st_ast * (1000.0 / spacing);

                sw.WriteLine();
                sw.WriteLine("Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm", bar_dia, spacing, pro_st_ast);

                lst_Bar_Dia.Add(bar_dia);
                lst_Bar_Space.Add(spacing);
                bd6 = bar_dia;
                bd7 = bar_dia;
                bd8 = bar_dia;
                sp6 = sp7 = sp8 = spacing * 2;


                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN OF SHEAR REINFORCEMENT");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Total Load per unit Area = p4 = {0:f2} kN/sq.m", p4);
                sw.WriteLine();
                sw.WriteLine("  Shear Force at distance = deff ,from face of wall");

                double shear_force = p4 * (b - (deff + cover) * 2 / 1000) / 2;
                sw.WriteLine("  = p4*[b-(deff+cover)*2/1000] / 2");
                sw.WriteLine("  = {0:f3}*[{1:f2}-({2:f2}+{3:f2})*2/1000] / 2",
                    p4,
                    b,
                    deff,
                    cover);
                sw.WriteLine("  = {0:f2} kN", shear_force);
                double shear_stress = shear_force * 1000 / (1000 * deff);
                sw.WriteLine();
                sw.WriteLine("  Shear Stress = {0:f2}*1000/(1000*{1:f2})",
                    shear_force, deff);
                sw.WriteLine("               = {0:f2} N/sq.mm", shear_stress);

                sw.WriteLine();
                //sw.WriteLine("  Using Table 23, IS 456 : 2000 (Page 84)");
                //double percent = req_st_ast * 100 / (1000 * deff);
                //double tau_c = iApp.Tables.Permissible_Shear_Stress(percent, St_Grade);
                //sw.WriteLine("  Using Table 1, IS 456 : 2000 (Page 84)");
                double percent = req_st_ast * 100 / (1000 * deff);

                double tau_c = Get_Table_1_Value(percent, Con_Grade, ref ref_string );
                sw.WriteLine("  Using Table 1, Given at end of this report. : {0}" , ref_string);
                //double tau_c = iApp.Tables.Permissible_Shear_Stress(percent, St_Grade);

                sw.WriteLine();
                sw.WriteLine("  percent = Ast*100/(1000*deff)");
                sw.WriteLine("          = {0:f2}*100/(1000*{1:f2})",
                    req_st_ast, deff);
                sw.WriteLine("          = {0:f0}%", (percent * 100));

                sw.WriteLine();
                sw.WriteLine("  τ_c = {0:f3} N/sq.mm.", tau_c);
                double shear_capacity = tau_c * 1000 * deff;
                sw.WriteLine();
                sw.WriteLine("  Shear Capacity of section = τ_c*1000*deff");
                sw.WriteLine("                            = {0:f2}*1000*{1:f2} N",
                    tau_c, deff);
                sw.WriteLine("                            = {0:f2} N", shear_capacity);

                shear_capacity = shear_capacity / 1000.0;
                sw.WriteLine("                            = {0:f2} kN", shear_capacity);

                double balance_shear = shear_force - shear_capacity;
                sw.WriteLine();
                sw.WriteLine("  Balance Shear = {0:f2} - {1:f2} = {2:f2} kN = {3:f2} N",
                    shear_force, shear_capacity, balance_shear, (balance_shear * 1000));

                balance_shear = balance_shear * 1000;
                sw.WriteLine();

                //double ast_2 = Math.PI * 8 * 8 / 4 * (1000 / 250);

                double ast_2 = Math.PI * 10 * 10 / 4;



                sw.WriteLine("  Provide T10 @ 250 mm c/c, Ast = {0:f2} sq.mm", ast_2);
                bd9 = bd10 = 10;
                sp9 = sp10 = 250;


                #region Bar_Dia And Spacing for Drawing Table
                lst_Bar_Dia.Add(8);
                lst_Bar_Space.Add(250);
                #endregion


                double Asw = balance_shear * 250 / (200 * deff);
                sw.WriteLine();
                sw.WriteLine("  Asw = {0:f3} * 250 / ({1:f2} * {2:f2})",
                    balance_shear, ast_2, deff);

                sw.WriteLine("      = {0:f3} sq.mm.", Asw);


                double x = -((((shear_capacity * 2) / p4) - b) * (1000 / 2) + cover);
                x = x / 1000;
                sw.WriteLine();
                sw.WriteLine("  p4*[b-(x+cover)*(2/1000)]/2 = {0:f3}", shear_capacity);

                double _x = x * 10;
                //double _x = x / 100;
                _x = (int)(_x + 1);
                _x = _x * 100;


                sw.WriteLine("  x = {0:f2} m = {1:f2} mm (say)", x, _x);
                sw.WriteLine();
                sw.WriteLine("  Provided Shear reinforcements for a distance of {0:f2} mm from", _x);
                sw.WriteLine("  the face of wall on both sides.");


                #endregion

                #region STEP 6


                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : DESIGN OF BOTTOM SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Maximum Bending Moment at Support / Mid Span");


                M = SMdcx;
                sw.WriteLine("  M = {0:f2} kN-m", M);
                depth = Math.Sqrt((M * 10E5) / (1000 * R));

                sw.WriteLine();
                sw.WriteLine("  Depth Required = √((M * 10E5) / (1000 * R)");
                sw.WriteLine("            = √(({0:f2} * 10E5) / (1000 * {1:f3}))", M, R);
                sw.WriteLine("            = {0:f3} mm", depth);
                sw.WriteLine();
                sw.WriteLine("  Provided Effective Depth deff = {0:f2} mm", (d1 * 1000));

                bar_dia = 16;
                //sw.WriteLine("  Considering Clear Cover = {0:f2} mm and Reinforcement bar dia = {1} mm",
                //cover, bar_dia);
                deff = (d1 * 1000) - cover - (bar_dia / 2.0);
                sw.WriteLine();
                if (deff > depth)
                {
                    sw.WriteLine("  Effective Depth = deff = {0:f2} - {1:f2} - {2:f0}/2 = {3:f2} mm > {4:f2} mm OK",
                        (d1 * 1000), cover, bar_dia, deff, depth);
                }
                else
                {
                    sw.WriteLine("  Effective Depth = deff = {0:f2} - {1:f2} - {2:f0}/2 = {3:f2} mm < {4:f2} mm NOT OK",
                        (d1 * 1000), cover, bar_dia, deff, depth);
                }

                req_st_ast = (M * 10E5) / (t * j * deff);
                sw.WriteLine();
                sw.WriteLine("  Required Main Reinforcement Steel Ast = (M*10E5)/(t*j*d)");
                sw.WriteLine("                                        = ({0:f2}*10E5)/({1:f2}*{2:f3}*{3:f2})",
                    M,
                    t, j, deff);
                sw.WriteLine("                                        = {0:f3} sq.mm.", req_st_ast);


                pro_st_ast = (Math.PI * bar_dia * bar_dia / 4);
                spacing = req_st_ast / pro_st_ast;
                spacing = 1000.0 / spacing;
                spacing = (int)(spacing / 10.0);
                spacing = (spacing * 10.0);



                pro_st_ast = (Math.PI * bar_dia * bar_dia / 4) * (1000.0 / spacing);




                sw.WriteLine();
                sw.WriteLine("  Provided T{0} bars @ {1:f0} mm c/c", bar_dia, spacing);

                bd1 = bd3 = bar_dia;
                sp1 = spacing * 2;
                sp3 = spacing;


                #region Bar_Dia And Spacing for Drawing Table
                lst_Bar_Dia.Add(bar_dia);
                lst_Bar_Space.Add(150);
                #endregion



                sw.WriteLine();
                sw.WriteLine("  Provided Ast = {0:f3} sq.mm", pro_st_ast);

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN OF SHEAR REINFORCEMENT");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                //sw.WriteLine("  Total Load per unit Area = p4 = {0:f2} kN/sq.m", p4);
                //sw.WriteLine("  Shear Force at distance = deff ,from face of wall");

                shear_force = p6 * (b - (deff + cover) * 2 / 1000) / 2;
                sw.WriteLine("  = p6*[b-(deff+cover)*2/1000] / 2");
                sw.WriteLine("  = {0:f3}*[{1:f2}-({2:f2}+{3:f2})*2/1000] / 2",
                    p6,
                    b,
                    deff,
                    cover);
                sw.WriteLine("  = {0:f2} kN", shear_force);

                shear_stress = shear_force * 1000 / (1000 * deff);
                sw.WriteLine();
                sw.WriteLine("  Shear Stress = {0:f2}*1000/(1000*{1:f2})",
                    shear_force, deff);
                sw.WriteLine("               = {0:f2} N/sq.mm", shear_stress);

                sw.WriteLine();
                //sw.WriteLine("  Using Table 23, IS 456 : 2000 (Page 84)");

                //percent = pro_st_ast * 100 / (1000 * deff);
                ////percent = req_st_ast * 100 / (1000 * deff);
                //tau_c = iApp.Tables.Permissible_Shear_Stress(percent, Con_Grade);


                //sw.WriteLine("  Using Table 1, IS 456 : 2000 (Page 84)");
                percent = req_st_ast * 100 / (1000 * deff);

                tau_c = Get_Table_1_Value(percent, Con_Grade, ref ref_string);
                sw.WriteLine("  Using Table 1, Given at end of this report : {0}", ref_string);
                //double tau_c = iApp.Tables.Permissible_Shear_Stress(percent, St_Grade);

                sw.WriteLine("  percent = Ast*100/(1000*deff)");
                sw.WriteLine("          = {0:f2}*100/(1000*{1:f2})",
                    req_st_ast, deff);
                sw.WriteLine("          = {0:f0}%", (percent * 100));
                sw.WriteLine();
                sw.WriteLine("  τ_c = {0:f3} N/sq.mm.", tau_c);
                shear_capacity = tau_c * 1000 * deff;
                sw.WriteLine();
                sw.WriteLine("  Shear Capacity of section = τ_c*1000*deff");
                sw.WriteLine("                            = {0:f3}*1000*{1:f3} N",
                    tau_c, deff);
                sw.WriteLine("                            = {0:f2} N", shear_capacity);

                shear_capacity = shear_capacity / 1000.0;
                sw.WriteLine("                            = {0:f2} kN", shear_capacity);

                balance_shear = shear_force - shear_capacity;
                sw.WriteLine();
                sw.WriteLine("  Balance Shear = {0:f2} - {1:f2} = {2:f2} kN = {3:f2} N",
                    shear_force, shear_capacity, balance_shear, (balance_shear * 1000));

                balance_shear = balance_shear * 1000;
                sw.WriteLine();
                ast_2 = Math.PI * 10 * 10 / 4 * (1000 / 250);

                Asw = balance_shear * 250 / (200 * deff);
                sw.WriteLine("  Asw = {0:f3} * 250 / ({1:f2} * {2:f2})",
                    balance_shear, 200, deff);

                sw.WriteLine("      = {0:f3} sq.mm.", Asw);

                sw.WriteLine();
                sw.WriteLine("  Provide T10 @ 250 mm c/c, Ast = {0:f2} sq.mm", ast_2);

                bd13 = bd11 = bd14 = bd12 = 10;
                sp13 = sp11 = sp14 = sp12 = 250;

                #region Bar_Dia And Spacing for Drawing Table
                lst_Bar_Dia.Add(10);
                lst_Bar_Space.Add(250);
                #endregion

                x = -((((shear_capacity * 2) / p6) - b) * (1000 / 2) + cover);
                x = x / 1000;
                sw.WriteLine();
                sw.WriteLine("  p6*[b-(x+cover)*(2/1000)]/2 = {0:f3}", shear_capacity);

                _x = x * 10;
                //double _x = x / 100;
                _x = (int)(_x + 1);
                _x = _x * 100;


                sw.WriteLine();
                sw.WriteLine("  x = {0:f2} m = {1:f2} mm (say)", x, _x);
                sw.WriteLine();
                sw.WriteLine("  Provided Shear reinforcements for a distance of {0:f2} mm from", _x);
                sw.WriteLine("  the face of wall on both sides.");

                #endregion

                #region STEP 7

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 7 : DESIGN OF SIDE WALLS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Maximum moments at joints with top slab and bottom slab are save as");
                sw.WriteLine("  taken for Design of Slabs. So provide same Reinforments.");

                sw.WriteLine();
                sw.WriteLine("  Midspan moments are calculated as:");
                sw.WriteLine("      {0:f2}, {1:f2} and {2:f2}", lst_Mad[0], lst_Mad[1], lst_Mad[2]);

                lst_Mad.Sort();

                M = lst_Mad[2];

                double eff_thickness = Math.Sqrt((M * 10e5) / (1000 * R));

                sw.WriteLine();
                sw.WriteLine("  Effective Thickness of wall required = √((M * 10E5) / (1000 * R))");
                sw.WriteLine("                                       = √(({0:f2} * 10E5) / (1000 * {1:f3}))",
                    M, R);
                sw.WriteLine("                                       = {0:f2} mm", eff_thickness);
                if (deff > eff_thickness)
                {
                    sw.WriteLine(" Provided Effective thickness = {0:f2} > {1:f2} mm , OK", deff, eff_thickness);
                }
                else
                {
                    sw.WriteLine("      Provided Effective thickness = {0:f2} < {1:f2} mm , NOT OK", deff, eff_thickness);
                }

                req_st_ast = (M * 10E5) / (t * j * deff);

                sw.WriteLine();
                sw.WriteLine("  Required Steel = Ast = (M * 10E5) / (t * j * d)");
                sw.WriteLine("                       = ({0:f2} * 10E5) / ({1:f2} * {2:f3} * {3:f2})",
                    M, t, j, d);
                sw.WriteLine("                       = {0:f3} sq.mm", req_st_ast);

                sw.WriteLine();
                sw.WriteLine("  Provide T16 @300 mm c/c");
                pro_st_ast = (Math.PI * 16 * 16 / 4) * (1000.0 / 300.0);
                sw.WriteLine("  Provided Ast = {0:f0} sq.mm ", pro_st_ast);
                sw.WriteLine();

                bd2 = bd5 = 16;
                sp2 = sp5 = 300;


                #endregion

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("TABLE 1");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                Write_Table_1(sw);
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                Write_Drawing();

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------            Thank you for using ASTRA Pro          ---------------");
                sw.WriteLine("---------------------------------------------------------------------------");
  
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void WriteUserInput()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {

                #region WRITE USER INPUT DATA

                sw.WriteLine("CODE : BOX CULVERT");
                sw.WriteLine("USER INPUT DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("H = {0:f3}", H);
                sw.WriteLine("b = {0:f3}", b);
                sw.WriteLine("d = {0:f3}", d);
                sw.WriteLine("d1 = {0:f3}", d1);
                sw.WriteLine("d2 = {0:f3}", d2);
                sw.WriteLine("d3 = {0:f3}", d3);
                sw.WriteLine("gamma_b = {0:f3}", gamma_b);
                sw.WriteLine("gamma_c = {0:f3}", gamma_c);
                sw.WriteLine("R = {0:f3}", R);
                sw.WriteLine("t = {0:f3}", t);
                sw.WriteLine("j = {0:f3}", j);
                sw.WriteLine("cover = {0:f3}", cover);
                sw.WriteLine("sigma_st = {0:f3}", sigma_st);
                sw.WriteLine("sigma_c = {0:f3}", sigma_c);
                sw.WriteLine("b1 = {0:f3}", b1);
                sw.WriteLine("b2 = {0:f3}", b2);
                sw.WriteLine("a1 = {0:f3}", a1);
                sw.WriteLine("w1 = {0:f3}", w1);
                sw.WriteLine("w2 = {0:f3}", w2);
                sw.WriteLine("b3 = {0:f3}", b3);
                sw.WriteLine("F = {0:f3}", F);
                sw.WriteLine("S = {0:f3}", S);
                sw.WriteLine("sbc = {0:f3}", sbc);
                sw.WriteLine();
                sw.WriteLine("FINISH");

                #endregion
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }


        public double Get_Table_1_Value(double percent, CONCRETE_GRADE con_grade, ref string ref_string)
        {
            return iApp.Tables.Permissible_Shear_Stress(percent, con_grade, ref ref_string);
        }
        public void Write_Table_1(StreamWriter sw)
        {

            List<string> lst_content = iApp.Tables.Get_Tables_Permissible_Shear_Stress();

            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
            lst_content.Clear();
        }

        public void Write_Drawing()
        {
            drawing_path = Path.Combine(system_path, "RCC_BOX_STRUCTURE_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
            #region Variable
            //string _box1 = "1/3 x 3/5";
            //string _box2 = "[1/3 x 3/5]";
            string _box1 = String.Format("1/{0:f0} x {1:f0}/{2:f0}", b, d, H);
            string _box2 = "[" + _box1 + "]";
            double _a = b * 1000.0;
            double _b = d * 1000.0;
            double _c = 0;
            double _d = d1 * 1000.0;
            double _e = d2 * 1000.0;
            double _f = d3 * 1000.0;
            double _h = H * 1000.0;
            //double _pressure = 130.43;

            #endregion

            try
            {
                sw.WriteLine("_box1={0:f0}", _box1);
                sw.WriteLine("_box2={0:f0}", _box2);
                sw.WriteLine("_a={0:f0}", _a);
                sw.WriteLine("_b={0:f0}", _b);
                sw.WriteLine("_c={0:f0}", _c);
                sw.WriteLine("_d={0:f0}", _d);
                sw.WriteLine("_e={0:f0}", _e);
                sw.WriteLine("_f={0:f0}", _f);
                sw.WriteLine("_h={0:f0}", _h);
                sw.WriteLine("_pressure={0:f2}", _pressure);

                sw.WriteLine("_b1={0:f0}", lst_Bar_Dia[1]);
                sw.WriteLine("_s1={0:f0}", lst_Bar_Space[1]);
                sw.WriteLine("_b2={0:f0}", lst_Bar_Dia[2]);
                sw.WriteLine("_s2={0:f0}", lst_Bar_Space[2]);
                sw.WriteLine("_b3={0:f0}", lst_Bar_Dia[3]);
                sw.WriteLine("_s3={0:f0}", lst_Bar_Space[3]);
                sw.WriteLine("_b4={0:f0}", lst_Bar_Dia[4]);
                sw.WriteLine("_s4={0:f0}", lst_Bar_Space[4]);
                sw.WriteLine("_b5={0:f0}", lst_Bar_Dia[5]);
                sw.WriteLine("_s5={0:f0}", lst_Bar_Space[5]);
                sw.WriteLine("_b6={0:f0}", lst_Bar_Dia[6]);
                sw.WriteLine("_s6={0:f0}", lst_Bar_Space[6]);
                sw.WriteLine("_b7={0:f0}", lst_Bar_Dia[7]);
                sw.WriteLine("_s7={0:f0}", lst_Bar_Space[7]);
                sw.WriteLine("_b8={0:f0}", lst_Bar_Dia[8]);
                sw.WriteLine("_s8={0:f0}", lst_Bar_Space[8]);
                sw.WriteLine("_b9={0:f0}", lst_Bar_Dia[9]);
                sw.WriteLine("_s9={0:f0}", lst_Bar_Space[9]);
                sw.WriteLine("_b10={0:f0}", lst_Bar_Dia[10]);
                sw.WriteLine("_s10={0:f0}", lst_Bar_Space[10]);
                sw.WriteLine("_b11={0:f0}", lst_Bar_Dia[11]);
                sw.WriteLine("_s11={0:f0}", lst_Bar_Space[11]);
                sw.WriteLine("_b12={0:f0}", lst_Bar_Dia[12]);
                sw.WriteLine("_s12={0:f0}", lst_Bar_Space[12]);
                sw.WriteLine("_b13={0:f0}", lst_Bar_Dia[13]);
                sw.WriteLine("_s13={0:f0}", lst_Bar_Space[13]);
                sw.WriteLine("_b14={0:f0}", lst_Bar_Dia[14]);
                sw.WriteLine("_s14={0:f0}", lst_Bar_Space[14]);
                sw.WriteLine("_b15={0:f0}", lst_Bar_Dia[15]);
                sw.WriteLine("_s15={0:f0}", lst_Bar_Space[15]);
                sw.WriteLine("_b16={0:f0}", lst_Bar_Dia[16]);
                sw.WriteLine("_s16={0:f0}", lst_Bar_Space[16]);
                sw.WriteLine("_b17={0:f0}", lst_Bar_Dia[17]);
                sw.WriteLine("_s17={0:f0}", lst_Bar_Space[17]);
                sw.WriteLine("_b18={0:f0}", lst_Bar_Dia[18]);
                sw.WriteLine("_s18={0:f0}", lst_Bar_Space[18]);
                sw.WriteLine("_b19={0:f0}", lst_Bar_Dia[19]);
                sw.WriteLine("_s19={0:f0}", lst_Bar_Space[19]);
                sw.WriteLine("_b20={0:f0}", lst_Bar_Dia[20]);
                sw.WriteLine("_s20={0:f0}", lst_Bar_Space[20]);
                sw.WriteLine("_b21={0:f0}", lst_Bar_Dia[21]);
                sw.WriteLine("_s21={0:f0}", lst_Bar_Space[21]);



            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void Write_Drawing_Test()
        {
            drawing_path = Path.Combine(system_path, "RCC_BOX_STRUCTURE_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
            #region Variable
            //string _box1 = "1/3 x 3/5";
            //string _box2 = "[1/3 x 3/5]";
            //string _box1 = String.Format("1/{0:f0} x {1:f0}/{2:f0}", b, d, H);
            //string _box2 = "[" + _box1 + "]";
            //double _a = b * 1000.0;
            //double _b = d * 1000.0;
            //double _c = 0;
            //double _d = d1 * 1000.0;
            //double _e = d2 * 1000.0;
            //double _f = d3 * 1000.0;
            //double _h = H * 1000.0;
            //double _pressure = 130.43;
            //double _b1 = 16;
            //double _b2 = 16;
            //double _b3 = 16;
            //double _b4 = 0;
            //double _b5 = 10;
            //double _b6 = 16;
            //double _b7 = 16;
            //double _b8 = 16;
            //double _b9 = 10;
            //double _b10 = 10;
            //double _b11 = 10;
            //double _b12 = 10;
            //double _b13 = 10;
            //double _b14 = 10;
            //double _b15 = 16;
            //double _b16 = 10;
            //double _b17 = 10;
            //double _b18 = 10;
            //double _b19 = 10;
            //double _b20 = 10;
            //double _b21 = 12;
            //double _s1 = 210;
            //double _s2 = 300;
            //double _s3 = 130;
            //double _s4 = 0;
            //double _s5 = 250;
            //double _s6 = 300;
            //double _s7 = 300;
            //double _s8 = 300;
            //double _s9 = 300;
            //double _s10 = 300;
            //double _s11 = 250;
            //double _s12 = 250;
            //double _s13 = 250;
            //double _s14 = 250;
            //double _s15 = 250;
            //double _s16 = 0;
            //double _s17 = 0;
            //double _s18 = 150;
            //double _s19 = 0;
            //double _s20 = 150;
            //double _s21 = 0;
            #endregion

            #region Variable
            //string _box1 = "1/3 x 3/5";
            //string _box2 = "[1/3 x 3/5]";
            string _box1 = String.Format("1/{0:f0} x {1:f0}/{2:f0}", b, d, H);
            string _box2 = "[" + _box1 + "]";
            double _a = b * 1000.0;
            double _b = d * 1000.0;
            double _c = 0;
            double _d = d1 * 1000.0;
            double _e = d2 * 1000.0;
            double _f = d3 * 1000.0;
            double _h = H * 1000.0;
            double _pressure = 130.43;
            double _b1 = bd1;
            double _b2 = bd2;
            double _b3 = bd3;
            double _b4 = 0;
            double _b5 = bd5;
            double _b6 = bd6;
            double _b7 = bd7;
            double _b8 = bd8;
            double _b9 = bd9;
            double _b10 = bd10;
            double _b11 = bd11;
            double _b12 = bd12;
            double _b13 = bd13;
            double _b14 = bd14;
            double _b15 = 16;
            double _b16 = 10;
            double _b17 = 10;
            double _b18 = 10;
            double _b19 = 10;
            double _b20 = 10;
            double _b21 = 12;

            double _s1 = sp1;
            double _s2 = sp2;
            double _s3 = sp3;
            double _s4 = 0;
            double _s5 = sp5;
            double _s6 = sp6;
            double _s7 = sp7;
            double _s8 = sp8;
            double _s9 = sp9;
            double _s10 = sp10;
            double _s11 = sp11;
            double _s12 = sp12;
            double _s13 = sp13;
            double _s14 = sp14;
            double _s15 = sp15;
            double _s16 = 0;
            double _s17 = 0;
            double _s18 = 150;
            double _s19 = 0;
            double _s20 = 150;
            double _s21 = 0;
            #endregion

            try
            {
                sw.WriteLine("_box1={0:f0}", _box1);
                sw.WriteLine("_box2={0:f0}", _box2);
                sw.WriteLine("_a={0:f0}", _a);
                sw.WriteLine("_b={0:f0}", _b);
                sw.WriteLine("_c={0:f0}", _c);
                sw.WriteLine("_d={0:f0}", _d);
                sw.WriteLine("_e={0:f0}", _e);
                sw.WriteLine("_f={0:f0}", _f);
                sw.WriteLine("_h={0:f0}", _h);
                sw.WriteLine("_pressure={0:f2}", _pressure);
                sw.WriteLine("_b1={0:f0}", _b1);
                sw.WriteLine("_b2={0:f0}", _b2);
                sw.WriteLine("_b3={0:f0}", _b3);
                sw.WriteLine("_b4={0:f0}", _b4);
                sw.WriteLine("_b5={0:f0}", _b5);
                sw.WriteLine("_b6={0:f0}", _b6);
                sw.WriteLine("_b7={0:f0}", _b7);
                sw.WriteLine("_b8={0:f0}", _b8);
                sw.WriteLine("_b9={0:f0}", _b9);
                sw.WriteLine("_b10={0:f0}", _b10);
                sw.WriteLine("_b11={0:f0}", _b11);
                sw.WriteLine("_b12={0:f0}", _b12);
                sw.WriteLine("_b13={0:f0}", _b13);
                sw.WriteLine("_b14={0:f0}", _b14);
                sw.WriteLine("_b15={0:f0}", _b15);
                sw.WriteLine("_b16={0:f0}", _b16);
                sw.WriteLine("_b17={0:f0}", _b17);
                sw.WriteLine("_b18={0:f0}", _b18);
                sw.WriteLine("_b19={0:f0}", _b19);
                sw.WriteLine("_b20={0:f0}", _b20);
                sw.WriteLine("_b21={0:f0}", _b21);
                sw.WriteLine("_s1={0:f0}", _s1);
                sw.WriteLine("_s2={0:f0}", _s2);
                sw.WriteLine("_s3={0:f0}", _s3);
                sw.WriteLine("_s4={0:f0}", _s4);
                sw.WriteLine("_s5={0:f0}", _s5);
                sw.WriteLine("_s6={0:f0}", _s6);
                sw.WriteLine("_s7={0:f0}", _s7);
                sw.WriteLine("_s8={0:f0}", _s8);
                sw.WriteLine("_s9={0:f0}", _s9);
                sw.WriteLine("_s10={0:f0}", _s10);
                sw.WriteLine("_s11={0:f0}", _s11);
                sw.WriteLine("_s12={0:f0}", _s12);
                sw.WriteLine("_s13={0:f0}", _s13);
                sw.WriteLine("_s14={0:f0}", _s14);
                sw.WriteLine("_s15={0:f0}", _s15);
                sw.WriteLine("_s16={0:f0}", _s16);
                sw.WriteLine("_s17={0:f0}", _s17);
                sw.WriteLine("_s18={0:f0}", _s18);
                sw.WriteLine("_s19={0:f0}", _s19);
                sw.WriteLine("_s20={0:f0}", _s20);
                sw.WriteLine("_s21={0:f0}", _s21);

            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }

        }
    }

}
