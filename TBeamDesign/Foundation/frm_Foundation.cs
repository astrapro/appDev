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
using AstraFunctionOne.BridgeDesign.Foundation;
 

namespace BridgeAnalysisDesign.Foundation
{
    public partial class frm_Foundation : Form
    {
        //const string Title = "DESIGN OF FOUNDATION";

        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF FOUNDATION [BS]";
                return "DESIGN OF FOUNDATION [IRC]";
            }
        }



        AstraInterface.Interface.IApplication iApp = null;
        WellFoundation well = null;
        PileFoundation pile = null;
        public frm_Foundation(IApplication app)
        {
            InitializeComponent();
            iApp = app;
         
            this.Text = Title + " : " + MyList.Get_Modified_Path(iApp.LastDesignWorkingFolder);

            //iApp.LastDesignWorkingFolder = System.IO.Path.Combine(iApp.LastDesignWorkingFolder, Title);


            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            if (!System.IO.Directory.Exists(user_path))
                System.IO.Directory.CreateDirectory(user_path);
            cmb_well_K.SelectedIndex = 1;

            well = new WellFoundation(iApp);
            pile = new PileFoundation(iApp);
            
            Pile_Foundation_Load();
        }
        public frm_Foundation(IApplication app, bool OnlyPile)
        {
            InitializeComponent();
            iApp = app;

            this.Text = Title + " : " + MyList.Get_Modified_Path(iApp.LastDesignWorkingFolder);

            //iApp.LastDesignWorkingFolder = System.IO.Path.Combine(iApp.LastDesignWorkingFolder, Title);





            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            if (!System.IO.Directory.Exists(user_path))
                System.IO.Directory.CreateDirectory(user_path);
            cmb_well_K.SelectedIndex = 1;

            if(OnlyPile)
            {
                tc_main.TabPages.Remove(tab_rcc_well_fnd_LS);
                btn_worksheet_well.Visible = false;
            }



            well = new WellFoundation(iApp);
            pile = new PileFoundation(iApp);

            Pile_Foundation_Load();
        }
        public string user_path { get; set; }
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

        #region Well Foundation Form Events
        private void btn_well_Process_Click(object sender, EventArgs e)
        {
            well.FilePath = user_path;
            Well_Initialize_InputData();
            well.Write_User_Input();
            well.Calculate_Program();
            well.Write_Drawing_File();
            iApp.Save_Form_Record(this, well.user_path);
            if (File.Exists(well.rep_file_name)) { MessageBox.Show(this, "Report file written in " + well.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(well.rep_file_name); }
            well.is_process = true;
            Button_Enable_Disable();
        }
        private void btn_well_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(well.rep_file_name);
        }
        private void btn_well_Drawing_Click(object sender, EventArgs e)
        {
            iApp.SetDrawingFile_Path(well.drawing_file, "Well_Foundation", "Well_Foundation");
        }
        private void cmb_K_SelectedIndexChanged(object sender, EventArgs e)
        {
            double d = 0.0;
            switch (cmb_well_K.SelectedIndex)
            {
                case 0:
                    d = 0.030;
                    break;
                case 1:
                    d = 0.033;
                    break;
                case 2:
                    d = 0.039;
                    break;
                case 3:
                    d = 0.043;
                    break;
            }
            txt_well_K.Text = d.ToString("0.000");
        }
       
        private void btn_worksheet_well_Click(object sender, EventArgs e)
        {
            iApp.OpenExcelFile(Worksheet_Folder, Path.Combine(Application.StartupPath, @"DESIGN\Foundation\Well Foundation\WellFoundation_Design.xls"), "2011ap");
        }

        public void Well_Initialize_InputData()
        {
            #region USER DATA INPUT
            try
            {
                well.Di = MyList.StringToDouble(txt_well_di.Text, 0.0);
                well.L = MyList.StringToDouble(txt_well_L.Text, 0.0);
                well.fck = MyList.StringToDouble(cmb_well_fck.Text, 0.0);
                well.fy = MyList.StringToDouble(cmb_well_fy.Text, 0.0);
                well.K_Indx = cmb_well_K.SelectedIndex;
                well.K_Text = cmb_well_K.Text;
                well.K = MyList.StringToDouble(txt_well_K.Text, 0.0);
                well.D1 = MyList.StringToDouble(txt_well_D1.Text, 0.0);
                well.D2 = MyList.StringToDouble(txt_well_D2.Text, 0.0);
                well.Lc = MyList.StringToDouble(txt_well_Lc.Text, 0.0);
                well.Tc = MyList.StringToDouble(txt_well_Tc.Text, 0.0);



                well.D1_unit_Wt = MyList.StringToDouble(txt_well_D1_unit_wt.Text, 0.0);
                well.D2_unit_Wt = MyList.StringToDouble(txt_well_D2_unit_wt.Text, 0.0);
                well.D3_unit_Wt = MyList.StringToDouble(txt_well_D3_unit_wt.Text, 0.0);
                well.D3 = MyList.StringToDouble(txt_well_D3.Text, 0.0);

                well.min_reinf = MyList.StringToDouble(txt_well_mon_reinf.Text, 0.0);
                well.avg_dia = MyList.StringToDouble(txt_well_avg_dia.Text, 0.0);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }

            #endregion
        }
        public void Well_Read_User_Input()
        {
            #region USER DATA INPUT DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(well.user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";
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
                        case "di":
                            txt_well_di.Text = mList.StringList[1].Trim();
                            break;
                        case "L":
                            txt_well_L.Text = mList.StringList[1].Trim();
                            break;
                        case "fck":
                            //txt_well_fck.Text = mList.StringList[1].Trim();
                            break;
                        case "fy":
                            //txt_well_fy.Text = mList.StringList[1].Trim();
                            break;
                        case "K":
                            txt_well_K.Text = mList.StringList[1].Trim();
                            break;
                        case "D1":
                            txt_well_D1.Text = mList.StringList[1].Trim();
                            break;
                        case "D2":
                            txt_well_D2.Text = mList.StringList[1].Trim();
                            break;
                        case "Lc":
                            txt_well_Lc.Text = mList.StringList[1].Trim();
                            break;
                        case "Tc":
                            txt_well_Tc.Text = mList.StringList[1].Trim();
                            break;
                        case "K_Indx":
                            well.K_Indx = mList.GetInt(1);
                            cmb_well_K.SelectedIndex = well.K_Indx;
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
            #endregion
        }
        #endregion

        #region Pile Form Event
        private void btn_Pile_Process_Click(object sender, EventArgs e)
        {
            Pile_Checked_Grid();
            if (Pile_Initialize_InputData())
            {
                pile.Project_Name = txt_pile_project.Text;
                pile.FilePath = user_path;
                pile.Write_User_Input();
                pile.Calculate_Program();
                pile.Write_Drawing_File();
                iApp.Save_Form_Record(tab_rcc_pile_foundation, pile.file_path);
                if (File.Exists(pile.rep_file_name)) { MessageBox.Show(this, "Report file written in " + pile.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(pile.rep_file_name); }
                pile.is_process = true;

                
            }
            Button_Enable_Disable();
            //iApp.Save_Form_Record(this, pile.user_path);
        }
        
        private void btn_Pile_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(pile.rep_file_name);
        }
        private void btn_Pile_Drawing_Click(object sender, EventArgs e)
        {
            //iApp.SetDrawingFile_Path(pile.user_drawing_file, "Pile_Foundation", "");

            iApp.Form_Drawing_Editor(eBaseDrawings.RCC_FOUNDATION_PILE, Path.Combine(pile.file_path, "DRAWINGS"), pile.rep_file_name).ShowDialog();

        }
         private void dgv_Pile_SelectionChanged(object sender, EventArgs e)
        {
            Pile_Checked_Grid();
        }
        #endregion Pile Form Event



        #region Pile Methods
         public void Pile_Foundation_Load_2013_06_17()
         {

             pile.pft_list = new PileFoundationTableCollection(1);


             PileFoundationTable pft = null;




             pft = new PileFoundationTable();

             pft.Layers = 1;
             pft.Thickness = 10.00;
             pft.Phi = 12;
             pft.Alpha = 0.5;
             pft.Cohesion = 2.5;
             pft.GammaSub = 0.92;

             //pft_list.Add(pft);
             dgv.Rows.Add(1, pft.Layers, pft.Thickness, pft.Phi, pft.Alpha, pft.Cohesion, pft.GammaSub);


             pft = new PileFoundationTable();

             pft.Layers = 2;
             pft.Thickness = 3.618;
             pft.Phi = 23;
             pft.Alpha = 0.5;
             pft.Cohesion = 0;
             pft.GammaSub = 0.92;
             //pft_list.Add(pft);
             dgv.Rows.Add(2, pft.Layers, pft.Thickness, pft.Phi, pft.Alpha, pft.Cohesion, pft.GammaSub);



             pft = new PileFoundationTable();

             pft.Layers = 3;
             pft.Thickness = 5.618;
             pft.Phi = 20;
             pft.Alpha = 0.5;
             pft.Cohesion = 0.56;
             pft.GammaSub = 0.92;
             //pft_list.Add(pft);
             dgv.Rows.Add(2, pft.Layers, pft.Thickness, pft.Phi, pft.Alpha, pft.Cohesion, pft.GammaSub);




             pft = new PileFoundationTable();

             pft.Layers = 4;
             pft.Thickness = 8.618;
             pft.Phi = 22;
             pft.Alpha = 0.5;
             pft.Cohesion = 0.56;
             pft.GammaSub = 0.92;
             //pft_list.Add(pft);
             dgv.Rows.Add(2, pft.Layers, pft.Thickness, pft.Phi, pft.Alpha, pft.Cohesion, pft.GammaSub);




             pft = new PileFoundationTable();

             pft.Layers = 5;
             pft.Thickness = 2.146;
             pft.Phi = 22;
             pft.Alpha = 0.5;
             pft.Cohesion = 0.56;
             pft.GammaSub = 0.92;
             //pft_list.Add(pft);
             dgv.Rows.Add(2, pft.Layers, pft.Thickness, pft.Phi, pft.Alpha, pft.Cohesion, pft.GammaSub);

             Pile_Checked_Grid();
         }
         public void Pile_Foundation_Load()
         {

             pile.pft_list = new PileFoundationTableCollection(1);


             PileFoundationTable pft = null;




             pft = new PileFoundationTable();

             pft.SL_No = 1;
             pft.Layers = 1;
             pft.Cohesion = 0.15;
             pft.Phi = 24.0;
             pft.GammaSub = 1.81;
             pft.Alpha = 0.5;
             pft.Depth = 2.0;

             pile.pft_list.Add(pft);


             pft = new PileFoundationTable();

             pft.SL_No = 2;
             pft.Layers = 2;
             pft.Cohesion = 0.10;
             pft.Phi = 28.0;
             pft.GammaSub = 1.86;
             pft.Alpha = 0.5;
             pft.Depth = 5.0;

             pile.pft_list.Add(pft);


             pft = new PileFoundationTable();

             pft.SL_No = 3;
             pft.Layers = 3;
             pft.Cohesion = 0.15;
             pft.Phi = 24.0;
             pft.GammaSub = 1.91;
             pft.Alpha = 0.5;
             pft.Depth = 7.0;

             pile.pft_list.Add(pft);



             pft = new PileFoundationTable();

             pft.SL_No = 4;
             pft.Layers = 4;
             pft.Cohesion = 0.15;
             pft.Phi = 24.0;
             pft.GammaSub = 1.90;
             pft.Alpha = 0.5;
             pft.Depth = 10.0;

             pile.pft_list.Add(pft);








             pft = new PileFoundationTable();

             pft.SL_No = 5;
             pft.Layers = 5;
             pft.Cohesion = 0.35;
             pft.Phi = 24.0;
             pft.GammaSub = 1.91;
             pft.Alpha = 0.5;
             pft.Depth = 12.5;

             pile.pft_list.Add(pft);




             pft = new PileFoundationTable();

             pft.SL_No = 6;
             pft.Layers = 6;
             pft.Cohesion = 0.30;
             pft.Phi = 26.0;
             pft.GammaSub = 1.92;
             pft.Alpha = 0.5;
             pft.Depth = 14.5;

             pile.pft_list.Add(pft);




             pft = new PileFoundationTable();

             pft.SL_No = 7;
             pft.Layers = 7;
             pft.Cohesion = 0.10;
             pft.Phi = 30.0;
             pft.GammaSub = 1.92;
             pft.Alpha = 0.5;
             pft.Depth = 20.0;

             pile.pft_list.Add(pft);





             pft = new PileFoundationTable();

             pft.SL_No = 8;
             pft.Layers = 8;
             pft.Cohesion = 0.05;
             pft.Phi = 32.0;
             pft.GammaSub = 1.92;
             pft.Alpha = 0.5;
             pft.Depth = 25.0;

             pile.pft_list.Add(pft);



             pft = new PileFoundationTable();

             pft.SL_No = 9;
             pft.Layers = 9;
             pft.Cohesion = 0.05;
             pft.Phi = 33.0;
             pft.GammaSub = 1.92;
             pft.Alpha = 0.5;
             pft.Depth = 30.0;

             pile.pft_list.Add(pft);




             foreach (var item in pile.pft_list)
             {
                 dgv.Rows.Add(item.SL_No,
                     item.Layers,
                     item.Depth,
                     item.Thickness,
                     item.Phi,
                     item.Alpha,
                     item.Cohesion,
                     item.GammaSub);
             }



             Pile_Checked_Grid();
         }

        public bool Pile_Initialize_InputData()
        {
            #region USER DATA INPUT
            try
            {
                //pile.N_gamma = MyList.StringToDouble(txt_N_gamma.Text, 0.0);
                //pile.Nq = MyList.StringToDouble(txt_Nq.Text, 0.0);
                //pile.Nc = MyList.StringToDouble(txt_Nc.Text, 0.0);

                pile.D = MyList.StringToDouble(txt_D.Text, 0.0);
                pile.P = MyList.StringToDouble(txt_P.Text, 0.0);
                pile.AM = MyList.StringToDouble(txt_AM.Text, 0.0);
                pile.K = MyList.StringToDouble(txt_K.Text, 0.0);
              
                pile.FS = MyList.StringToDouble(txt_FS.Text, 0.0);
                pile.PCBL = MyList.StringToDouble(txt_PCBL.Text, 0.0);

                pile.PL = MyList.StringToDouble(txt_PL.Text, 0.0);
                pile.FL = MyList.StringToDouble(txt_FL.Text, 0.0);
                pile.SL = MyList.StringToDouble(txt_SL.Text, 0.0);

                pile.fck = MyList.StringToDouble(cmb_pile_fck.Text, 0.0);
                pile.pile_sigma_c = MyList.StringToDouble(txt_pile_sigma_c.Text, 0.0);
                pile.pile_sigma_st = MyList.StringToDouble(txt_pile_sigma_st.Text, 0.0);
                pile.gamma_c = MyList.StringToDouble(txt_gamma_c.Text, 0.0);


                pile.Np = MyList.StringToDouble(txt_Np.Text, 0.0);
                pile.N = MyList.StringToDouble(txt_N.Text, 0.0);
                pile.gamma_sub = MyList.StringToDouble(txt_gamma_sub.Text, 0.0);
             
                pile.m = MyList.StringToDouble(txt_m.Text, 0.0);
                pile.F = MyList.StringToDouble(txt_F.Text, 0.0);
                pile.d1 = MyList.StringToDouble(txt_d1.Text, 0.0);
                pile.d2 = MyList.StringToDouble(txt_d2.Text, 0.0);
                pile.d3 = MyList.StringToDouble(txt_d3.Text, 0.0);
                pile.LPC = MyList.StringToDouble(txt_LPC.Text, 0.0);
                pile.BPC = MyList.StringToDouble(txt_BPC.Text, 0.0);
                pile.LPr = MyList.StringToDouble(txt_LPr.Text, 0.0);
                pile.BPr = MyList.StringToDouble(txt_BPr.Text, 0.0);
                pile.DPC = MyList.StringToDouble(txt_DPC.Text, 0.0);
                pile.l1 = MyList.StringToDouble(txt_L1.Text, 0.0);
                pile.l2 = MyList.StringToDouble(txt_L2.Text, 0.0);
                pile.l3 = MyList.StringToDouble(txt_L3.Text, 0.0);

                pile.sigma_ck = MyList.StringToDouble(cmb_pile_fck.Text, 0.0);
                pile.fy = MyList.StringToDouble(cmb_pile_fy.Text, 0.0);
                pile.cap_sigma_ck = MyList.StringToDouble(cmb_pcap_fck.Text, 0.0);
                pile.cap_fy = MyList.StringToDouble(cmb_pcap_fy.Text, 0.0);
                pile.sigma_cbc = MyList.StringToDouble(txt_pcap_sigma_c.Text, 0.0);
                pile.sigma_st = MyList.StringToDouble(txt_pcap_sigma_st.Text, 0.0);


                pile.BoreholeNo = textBox1.Text;


                //Chiranjit [2016 06 16]
                pile.d_dash = MyList.StringToDouble(txt_d_dash.Text, 0.0);
                pile.main_dia = MyList.StringToDouble(txt_main_dia.Text, 0.0);
                pile.lateral_dia = MyList.StringToDouble(txt_lateral_dia.Text, 0.0);
                pile.max_spacing = MyList.StringToDouble(txt_max_spacing.Text, 0.0);


                pile.shear_dia = MyList.StringToDouble(txt_shear_dia.Text, 0.0);
                pile.clear_cover = MyList.StringToDouble(txt_clear_cover.Text, 0.0);
                pile.cap_spacing = MyList.StringToDouble(txt_cap_spacing.Text, 0.0); 



                return Pile_Read_Grid_Data();

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ERROR! Read from user input. \n" + ex.ToString());
            }
            return true;
            #endregion
        }
        public void Pile_Read_User_Input()
        {
            #region USER DATA INPUT DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(pile.user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";
            bool table_data_find = false;
            pile.pft_list.Clear();

            try
            {
                for (int i = 0; i < lst_content.Count; i++)
                {
                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    if (table_data_find)
                        pile.pft_list.Add(PileFoundationTable.Parse(lst_content[i]));
                    #region SWITCH
                    switch (VarName)
                    {
                        case "D":
                            txt_D.Text = mList.StringList[1].Trim();
                            break;
                        case "P":
                            txt_P.Text = mList.StringList[1].Trim();
                            break;
                        case "K":
                            txt_K.Text = mList.StringList[1].Trim();
                            break;
                        case "AM":
                            txt_AM.Text = mList.StringList[1].Trim();
                            break;
                        //case "N_gamma":
                        //    txt_N_gamma.Text = mList.StringList[1].Trim();
                        //    break;
                        //case "Nq":
                        //    txt_Nq.Text = mList.StringList[1].Trim();
                        //    break;
                        //case "Nc":
                        //    txt_Nc.Text = mList.StringList[1].Trim();
                        //    break;
                        case "FS":
                            txt_FS.Text = mList.StringList[1].Trim();
                            break;
                        case "PCBL":
                            txt_PCBL.Text = mList.StringList[1].Trim();
                            break;
                        case "SL":
                            txt_SL.Text = mList.StringList[1].Trim();
                            break;
                        case "FL":
                            txt_FL.Text = mList.StringList[1].Trim();
                            break;
                        case "sigma_ck":
                            //txt_sigma_ck.Text = mList.StringList[1].Trim();
                            break;
                        case "fy":
                            //txt_fy.Text = mList.StringList[1].Trim();
                            break;
                        case "gamma_c":
                            txt_gamma_c.Text = mList.StringList[1].Trim();
                            break;
                        case "Np":
                            txt_Np.Text = mList.StringList[1].Trim();
                            break;
                        case "N":
                            txt_N.Text = mList.StringList[1].Trim();
                            break;
                        //case "gamma_sub":
                        //    txt_gamma_sub.Text = mList.StringList[1].Trim();
                        //    break;
                        case "cap_sigma_ck":
                            //txt_cap_sigma_ck.Text = mList.StringList[1].Trim();
                            break;
                        case "cap_fy":
                            //txt_cap_fy.Text = mList.StringList[1].Trim();
                            break;
                        case "sigma_cbc":
                            //txt_sigma_cbc.Text = mList.StringList[1].Trim();
                            break;
                        case "sigma_st":
                            //txt_sigma_st.Text = mList.StringList[1].Trim();
                            break;
                        case "m":
                            txt_m.Text = mList.StringList[1].Trim();
                            break;
                        case "F":
                            txt_F.Text = mList.StringList[1].Trim();
                            break;
                        case "d1":
                            txt_d1.Text = mList.StringList[1].Trim();
                            break;
                        case "d2":
                            txt_d2.Text = mList.StringList[1].Trim();
                            break;
                        case "d3":
                            txt_d3.Text = mList.StringList[1].Trim();
                            break;
                        case "LPC":
                            txt_LPC.Text = mList.StringList[1].Trim();
                            break;
                        case "BPC":
                            txt_BPC.Text = mList.StringList[1].Trim();
                            break;
                        case "LPr":
                            txt_LPr.Text = mList.StringList[1].Trim();
                            break;
                        case "BPr":
                            txt_BPr.Text = mList.StringList[1].Trim();
                            break;
                        case "DPC":
                            txt_DPC.Text = mList.StringList[1].Trim();
                            break;
                        case "L1":
                            txt_L1.Text = mList.StringList[1].Trim();
                            break;
                        case "L2":
                            txt_L2.Text = mList.StringList[1].Trim();
                            break;
                        case "L3":
                            txt_L3.Text = mList.StringList[1].Trim();
                            break;
                        case "TABLE":
                            table_data_find = true;
                            break;
                    }
                    #endregion
                }

                dgv.Rows.Clear();
                for (int i = 0; i < pile.pft_list.Count; i++)
                {
                    dgv.Rows.Add(i + 1,
                        pile.pft_list[i].Layers,
                        pile.pft_list[i].Thickness,
                        pile.pft_list[i].Phi,
                        pile.pft_list[i].Alpha,
                        pile.pft_list[i].Cohesion,
                        pile.pft_list[i].GammaSub);
                }
                Pile_Checked_Grid();
            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
            #endregion
        }
        private void Pile_Checked_Grid()
        {
            double d = 0;

            for (int i = 0; i < dgv.RowCount - 1; i++)
            {
                dgv[0, i].Value = i + 1;
                dgv.Rows[i].DefaultCellStyle.BackColor = Color.White;
                for (int k = 2; k < dgv.ColumnCount; k++)
                {
                    if (dgv[k, i].Value != null)
                    {
                        if (!double.TryParse(dgv[k, i].Value.ToString(), out d))
                            d = 0.0;
                    }
                    else
                    {
                        d = 0.0;
                    }

                    if(k == 5 && d == 0.0)
                        dgv[k, i].Value = "0.500";
                    else
                        dgv[k, i].Value = d.ToString("0.000");


                    if (k == 3)
                    {
                        if (i > 0)
                            d = MyList.StringToDouble(dgv[2, i].Value.ToString(), 0.0) - MyList.StringToDouble(dgv[2, i - 1].Value.ToString(), 0.0);
                        else
                            d = MyList.StringToDouble(dgv[2, i].Value.ToString(), 0.0);

                        dgv[k, i].Value = d.ToString("0.000");

                    }
                }
            }
        }
        public bool Pile_Read_Grid_Data()
        {
                double PL = MyList.StringToDouble(txt_PL.Text, 0.0);

                double sum_thk = 0;
            try
            {

                pile.pft_list.Clear();
                PileFoundationTable pft = null;
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    pft = new PileFoundationTable();

                    pft.Layers = MyList.StringToInt(dgv[1, i].Value.ToString(), 0);
                    pft.Depth = MyList.StringToDouble(dgv[2, i].Value.ToString(), 0);
                    pft.Thickness = double.Parse(dgv[3, i].Value.ToString());

                    sum_thk += pft.Thickness;

                    if (sum_thk > PL)
                    {
                        pft.Thickness = pft.Thickness - (sum_thk - PL);
                        dgv[3, i].Value = pft.Thickness.ToString("f3");
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }


                    pft.Phi = double.Parse(dgv[4, i].Value.ToString());
                    pft.Alpha = double.Parse(dgv[5, i].Value.ToString());
                    pft.Cohesion = double.Parse(dgv[6, i].Value.ToString());
                    pft.GammaSub = double.Parse(dgv[7, i].Value.ToString());
                    pile.pft_list.Add(pft);
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;

                    if (sum_thk >= PL) break;


                }

            }
            catch (Exception ex) { }
                if (sum_thk < PL)
                {
                    MessageBox.Show("Total thickness ( " + sum_thk.ToString("f3") + " m ) of Layer in Sub Soil data is\n\n less than Length of Pile ( " + PL.ToString("f3") + " m ) .......\n\n" +
                        "Process Terminated......", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return false;
                }
            return true;
        }

        #endregion Pile Methods
        private void btn_worksheet_sheet_pile_Click(object sender, EventArgs e)
        {
            iApp.OpenExcelFile(Worksheet_Folder, Path.Combine(Application.StartupPath, @"DESIGN\Foundation\Sheet Pile\Design Sheet Pile.xls"), "2011ap");
        }

        private void btn_dwg_well_default_Click(object sender, EventArgs e)
        {
            //iApp.RunViewer(Path.Combine(Drawing_Folder, "Well Foundation Worksheet Drawing"), "Well_Foundation_Worksheet");

            string ss=  uC_Well_Foundation1.excel_file;

            iApp.Form_Drawing_Editor(eBaseDrawings.RCC_FOUNDATION_WELL, Path.Combine(Path.GetDirectoryName(ss), "DRAWINGS"), ss).ShowDialog();

        }

        private void frm_Foundation_Load(object sender, EventArgs e)
        {
            pic_well.BackgroundImage = AstraFunctionOne.ImageCollection.Rcc_Well_Foundation1;
            pic_pile.BackgroundImage = AstraFunctionOne.ImageCollection.Dialog_Box_Pile_Foundation_Image01;

            cmb_well_fck.SelectedIndex = 2;
            cmb_well_fy.SelectedIndex = 1;
            cmb_pile_fck.SelectedIndex = 4;
            cmb_pile_fy.SelectedIndex = 1;
            cmb_pcap_fck.SelectedIndex = 4;
            cmb_pcap_fy.SelectedIndex = 1;

            txt_PCBL.Text = "211.00";

            Button_Enable_Disable();

            uC_Well_Foundation1.iApp = iApp;
            uC_Well_Foundation1.Load_Data();



            tc_main.TabPages.Remove(tab_rcc_well_fnd);
            tc_main.TabPages.Remove(tab_worksheet_design);

            #region Chiranjit Design Option

            try
            {

                Set_Project_Name(eASTRADesignType.Foundation_Pile);
                Set_Project_Name(eASTRADesignType.Foundation_Well);

                //eDesignOption edp = iApp.Get_Design_Option(Title);
                //if (edp == eDesignOption.None)
                //{
                //    this.Close();
                //}
                //else if (edp == eDesignOption.Open_Design)
                //{
                //    user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                //    iApp.Read_Form_Record(this, user_path);


                //    if (iApp.IsDemo)
                //        MessageBox.Show("ASTRA USB Dongle not found at any port....\nOpening with default data......", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    else
                //        MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Chiranjit Design Option

            iApp.user_path = user_path;
            uC_Well_Foundation1.Modified_Cell();
        }
        void Button_Enable_Disable()
        {
            //well.FilePath = user_path;
            pile.FilePath = user_path;


            //btn_dwg_well_interactive.Enabled = File.Exists(well.drawing_file);


            //btn_well_Report.Enabled = File.Exists(well.rep_file_name);


            btn_dwg_pile_interactive.Enabled = File.Exists(pile.user_drawing_file);
            btnReport.Enabled = File.Exists(pile.rep_file_name);
        }
        private void btn_dwg_well_interactive_Click(object sender, EventArgs e)
        {
                iApp.SetDrawingFile_Path(well.drawing_file, "Well_Foundation", "");
        }

        private void btn_worksheet_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
        }
        private void cmb_concrete_grade_steel_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASTRAGrade astg = null;

            ComboBox cmb = sender as ComboBox;

            Control ctrl = sender as Control;

            if (ctrl.Name.ToLower().StartsWith("cmb_well"))
            {
                astg = new ASTRAGrade(cmb_well_fck.Text, cmb_well_fy.Text);
                txt_well_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString("f2");
                txt_well_sigma_st.Text = astg.sigma_sv_N_sq_mm.ToString("f2");
            }
            else if (ctrl.Name.ToLower().StartsWith("cmb_pile"))
            {
                astg = new ASTRAGrade(cmb_pile_fck.Text, cmb_pile_fy.Text);
                txt_pile_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString("f2");
                txt_pile_sigma_st.Text = astg.sigma_sv_N_sq_mm.ToString("f2");
            }
            else if (ctrl.Name.ToLower().StartsWith("cmb_pcap"))
            {
                astg = new ASTRAGrade(cmb_pcap_fck.Text, cmb_pcap_fy.Text);
                txt_pcap_sigma_c.Text = astg.sigma_c_N_sq_mm.ToString("f2");
                txt_pcap_sigma_st.Text = (astg.sigma_sv_N_sq_mm).ToString("f2");
            }
        }

        private void btn_well_open_des_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = user_path;
                ofd.Filter = "Text File |*.txt";
                if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    string input_file = ofd.FileName;
                    user_path = Path.GetDirectoryName(input_file);
                    iApp.Read_Form_Record(this, user_path);
                    iApp.LastDesignWorkingFolder = Path.GetDirectoryName(user_path);
                    MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Button_Enable_Disable();
                }
            }
        }

        private void txt_PCBL_TextChanged(object sender, EventArgs e)
        {
            double pbcl = MyList.StringToDouble(txt_PCBL.Text, 62.0);
            double pl = MyList.StringToDouble(txt_PL.Text, 30.0);
            txt_FL.Text = (pbcl - pl).ToString("f3");
            txt_SL.Text = (pbcl - 2).ToString("f3");
        }

        private void uC_Well_Foundation1_OnProcees(object sender, EventArgs e)
        {
            //iApp.Save_Form_Record(this, user_path);
            uC_Well_Foundation1.Project_Name = txt_well_project.Text;
            iApp.Save_Form_Record(tab_rcc_well_fnd_LS, uC_Well_Foundation1.Get_Project_Folder());

        }

        private void btn_pile_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_pile_browse.Name)
            {
                //frm_Open_Project frm = new frm_Open_Project(tab_rcc_pile_foundation.Text, user_path);
                //if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                //{
                //    iApp.Read_Form_Record(tab_rcc_pile_foundation, frm.Example_Path);
                //    txt_pile_project.Text = Path.GetFileName(frm.Example_Path);
                //    pile.Project_Name = txt_pile_project.Text;
                //    pile.FilePath = user_path;
                //    btn_pile_process.Enabled = true;


                //}


                string ss = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                ss = Path.Combine(ss, "PILE FOUNDATION");

                frm_Open_Project frm = new frm_Open_Project(tab_rcc_pile_foundation.Text, ss);


                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;
                    txt_pile_project.Text = Path.GetFileName(frm.Example_Path);
                    //Open_Project();


                    #region Save As
                    if (frm.SaveAs_Path != "")
                    {

                        string src_path = user_path;
                        txt_pile_project.Text = Path.GetFileName(frm.SaveAs_Path);
                        Create_Project(eASTRADesignType.Foundation_Pile);
                        string dest_path = user_path;
                        MyList.Folder_Copy(src_path, dest_path);
                    }
                    #endregion Save As

                    //Open_Project();
                    iApp.Read_Form_Record(this, user_path);

                    txt_pile_project.Text = Path.GetFileName(user_path);
                    pile.Project_Name = txt_pile_project.Text;
                    pile.FilePath = user_path;
                    btn_pile_process.Enabled = true;
                    //Write_All_Data();
                }
            }
            else if (btn.Name == btn_pile_new_design.Name)
            {
                //frm_NewProject frm = new frm_NewProject(user_path);
                ////frm.Project_Name = "Singlecell Box Culvert Design Project";
                //if (txt_pile_project.Text != "")
                //    frm.Project_Name = txt_pile_project.Text;
                //else
                //    frm.Project_Name = "Pile Foundation Design Project";
                //if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                //{
                //    txt_pile_project.Text = frm.Project_Name;
                //    btn_pile_process.Enabled = true;
                //}
                Create_Project(eASTRADesignType.Foundation_Pile);

            }
            Button_Enable_Disable();
        }


        #region Create Project / Open Project
        string Project_Name(eASTRADesignType projType)
        {
            if (projType == eASTRADesignType.Foundation_Pile) return txt_pile_project.Text;
            return txt_well_project.Text;
        }
        public void Create_Project(eASTRADesignType projType)
        {
            //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            //if (!Directory.Exists(user_path))
            //{
            //    Directory.CreateDirectory(user_path);
            //}

            //user_path = Path.Combine(user_path, Project_Name);
            //if (!Directory.Exists(user_path))
            //{
            //    Directory.CreateDirectory(user_path);
            //}

            //string fname = Path.Combine(user_path, Project_Name + ".apr");

            //int ty = (int)Project_Type;
            //File.WriteAllText(fname, ty.ToString());


           

            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }

            if(projType == eASTRADesignType.Foundation_Pile)
                user_path = Path.Combine(user_path, "PILE FOUNDATION");
            else
                user_path = Path.Combine(user_path, "WELL FOUNDATION");


            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }



            string fname = Path.Combine(user_path, Project_Name(projType) + ".apr");

            int ty = (int)Project_Type(projType);
            File.WriteAllText(fname, ty.ToString());
            user_path = Path.Combine(user_path, Project_Name(projType));

            if (Directory.Exists(user_path))
            {
                switch (MessageBox.Show(Project_Name(projType) + " is already exist. Do you want overwrite ?",
                    "ASTRA", MessageBoxButtons.YesNoCancel))
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        return;
                    case System.Windows.Forms.DialogResult.Yes:
                        //Delete Folders
                        Delete_Folder(user_path);
                        break;
                }
            }
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }

            //Write_All_Data();


            MessageBox.Show(Project_Name(projType) + " is Created.", "ASTRA", MessageBoxButtons.OK);
        }

        public void Set_Project_Name(eASTRADesignType projType)
        {
            string dir = Path.Combine(iApp.LastDesignWorkingFolder, Title);


            if(projType == eASTRADesignType.Foundation_Pile) dir = Path.Combine(dir, "PILE FOUNDATION");
            else  dir = Path.Combine(dir, "WELL FOUNDATION");


            string prj_name = "";
            string prj_dir = "";
            int c = 1;
            if (Directory.Exists(dir))
            {
                while (true)
                {
                    prj_name = "DESIGN JOB #" + c.ToString("00");
                    prj_dir = Path.Combine(dir, prj_name);

                    if (!Directory.Exists(prj_dir)) break;
                    c++;
                }
            }
            else
                prj_name = "DESIGN JOB #" + c.ToString("00");

            if(projType == eASTRADesignType.Foundation_Pile)
                txt_pile_project.Text = prj_name;
            else
                txt_well_project.Text = prj_name;

        }




        public void All_Button_Enable(bool flag)
        {
            //btn_create_data.Enabled = flag;
            //btn_process_analysis.Enabled = flag;
            //btn_create_data.Enabled = flag;
            //btn_process_analysis
        }

        eASTRADesignType Project_Type(eASTRADesignType es)
        {
            return es;
        }

        public void Delete_Folder(string folder)
        {
            MyList.Delete_Folder(folder);
        }

        #endregion Chiranjit [2016 09 07]

        private void btn_well_new_design_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            //if (btn.Name == btn_well_browse.Name)
            //{
            //    frm_Open_Project frm = new frm_Open_Project(tab_rcc_well_fnd_LS.Text, user_path);
            //    if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            //    {
            //        iApp.Read_Form_Record(tab_rcc_well_fnd_LS, frm.Example_Path);
            //        txt_well_project.Text = Path.GetFileName(frm.Example_Path);
            //        uC_Well_Foundation1.Project_Name = txt_well_project.Text;
            //        uC_Well_Foundation1.Modified_Cell();
            //    }
            //}
            //else if (btn.Name == btn_well_new_design.Name)
            //{
            //    frm_NewProject frm = new frm_NewProject(user_path);
            //    //frm.Project_Name = "Singlecell Box Culvert Design Project";
            //    if (txt_well_project.Text != "")
            //        frm.Project_Name = txt_well_project.Text;
            //    else
            //        frm.Project_Name = "Well Foundation Design Project";
            //    if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            //    {
            //        txt_well_project.Text = frm.Project_Name;
            //    }
            //}
            //Button_Enable_Disable();


            if (btn.Name == btn_well_browse.Name)
            {
                //frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));

                string ss = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                ss = Path.Combine(ss, "WELL FOUNDATION");

                frm_Open_Project frm = new frm_Open_Project(tab_rcc_well_fnd.Text, ss);


                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;
                    iApp.Read_Form_Record(this, frm.Example_Path);
                    txt_well_project.Text = Path.GetFileName(frm.Example_Path);
                    //Open_Project();



                    #region Save As
                    if (frm.SaveAs_Path != "")
                    {

                        string src_path = user_path;
                        txt_well_project.Text = Path.GetFileName(frm.SaveAs_Path);
                        Create_Project(eASTRADesignType.Foundation_Well);
                        string dest_path = user_path;
                        MyList.Folder_Copy(src_path, dest_path);
                    }
                    #endregion Save As

                    //Open_Project();

                    txt_well_project.Text = Path.GetFileName(user_path);


                    //Write_All_Data();
                }
            }
            else if (btn.Name == btn_well_new_design.Name)
            {
                //IsCreateData = true;
                Create_Project(eASTRADesignType.Foundation_Well);
            }

            uC_Well_Foundation1.user_path = user_path;

            Button_Enable_Disable();
        }

    }
    public class WellFoundation 
    {
        public string rep_file_name = "";
        public string user_input_file = "";
        public string drawing_file = "";
        public string user_path = "";
        public string file_path = "";
        public string system_path = "";
        public bool is_process = false;
        IApplication iApp = null;


        #region Drawing Variable
        string _inner_dia = "";
        string _outer_dia = "";
        string _bars_A = "";
        string _bars_B = "";
        string _C = "";
        string _D = "";
        string _E1 = "";
        string _E2 = "";
        #endregion



        public double Di, L, fck, fy, K, D1, D2, Lc, Tc;

        public double D1_unit_Wt, D2_unit_Wt, D3, D3_unit_Wt, min_reinf, avg_dia;



        public int K_Indx = -1;
        public string K_Text = "";
        public WellFoundation(IApplication app)
        {
            iApp = app;

        }


        #region Public Methods

        public void Calculate_Program()
        {
            AstraFunctionOne.BridgeDesign.frmCurve f_c = null;
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 20.0           *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*       DESIGN OF RCC WELL FOUNDATION         *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");

                #endregion

                #region USER DATA

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Internal Diameter of Well [Di] = {0} m           Marked as (Di) in the Drawing",  Di );
                _inner_dia = Di.ToString() + " m";

                sw.WriteLine("Depth of Well below Bed Level [L] = {0} m        Marked as (L) in the Drawing",  L );
                sw.WriteLine("Concrete Grade [fck] = {0} N/sq.mm",  fck );
                sw.WriteLine("Steel Grade [fy] = {0} N/sq.mm",  fy );
                sw.WriteLine("Diameter of Main Reinforcement Steel bars [D1] = {0} mm",  D1 );
                sw.WriteLine("Diameter of Main Hoop Steel bars [D2] = {0} mm",  D2 );
                sw.WriteLine("Depth of Curb [Lc] = {0} mm",  Lc );
                sw.WriteLine("Thickness of Curb at bottom [Tc] = {0} mm        Marked as (Tc) in the Drawing",  Tc );
                sw.WriteLine();
                sw.WriteLine("Minimum reinforcement = {0} kg/cu.m ", min_reinf);
                sw.WriteLine("Hoops average diameter  = {0} m ", avg_dia);
                sw.WriteLine();
                sw.WriteLine("{0}  K = {1:f3}",K_Text, K);
                //sw.WriteLine("K = {0}", K);
                #endregion

                #region DESIGN CALCULATIONS
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                #endregion

                #region STEP 1 : THICKNESS OF STEINING
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : THICKNESS OF STEINING");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Minimum thickness of steining is given by");
                sw.WriteLine();
                sw.WriteLine("    h = K * (Di + 2*h)*√L");
                sw.WriteLine("Or, h = (K * Di * √L) + (K * 2*h * √L)");
                sw.WriteLine();
                sw.WriteLine("Or, h * (1 - K * 2 * √L) = (K * Di * √L) ");

                double h = ((K * Di * Math.Sqrt(L)) / ((1 - (K * 2 * Math.Sqrt(L)))));
                h = double.Parse(h.ToString("0.000"));
                sw.WriteLine("Or, h = ((K * Di * √L)/(1 - K * 2 * √L))");
                sw.WriteLine("Or, h = (({0} * {1} * √{2})/(1 - {0} * 2 * √{2}))", K, Di, L);
                sw.WriteLine();
                sw.WriteLine("Or, h = {0} m = {1} mm", h, (h * 1000));

                double Ts = (int)(h * 10.0);
                Ts = Ts * 100;

                if (Ts < 500)
                {
                    sw.WriteLine();
                    sw.WriteLine(" h = {0} < 500", Ts);
                    Ts = 500;
                }

                sw.WriteLine();
                sw.WriteLine("Adopt a steining of {0} mm", Ts);

                #endregion

                #region STEP 2 : REINFORCEMENT IN STEINING
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : REINFORCEMENT IN STEINING");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                double De = (Di + (2 * (Ts / 1000.0)));
                De = double.Parse(De.ToString("0.000"));

                _outer_dia = string.Format("Di + 2 x {0} = {1} m", (Ts / 1000.0), De);

                sw.WriteLine("De = (Di + (2 * (Ts / 1000.0)))");
                sw.WriteLine("   = ({0} + (2 * ({1} / 1000.0)))", Di, Ts);
                sw.WriteLine("   = {0} m", De);
                sw.WriteLine();

                sw.WriteLine("For RCC wells, Minimum longitudinal reinforcement");
                sw.WriteLine();
                sw.WriteLine("Asc = 0.2% of gross cross sectional area");
                sw.WriteLine();


                double Asc = (0.2 / 100) * ((Math.PI / 4.0) * (((Di + (2 * (Ts / 1000.0))) * (Di + (2 * (Ts / 1000.0)))) - (Di * Di))) * 10E5;
                Asc = double.Parse(Asc.ToString("0"));
                sw.WriteLine();
                sw.WriteLine("   = (0.2/100)*[(π/4)*(De*De - Di*Di)]*10^6");
                sw.WriteLine("   = (0.2/100)*[(π/4)*({0}*{0} - {1}*{1})]*10^6", De, Di);
                sw.WriteLine();
                double Asc_by_2 = Asc / 2.0;
                Asc_by_2 = double.Parse(Asc_by_2.ToString("0"));
                sw.WriteLine("   = {0} sq.mm for both faces or {1} sq.mm for eachfaces.", Asc, Asc_by_2);
                sw.WriteLine();
                sw.WriteLine("Use {0:f0} mm diameter bars at 300 mm centres on both faces.      Marked as (A) in the Drawing", D1);

                // For drawing
                _bars_A = string.Format("Use {0:f0} mm diameter bars at 300 mm centres on both faces.", D1);

                sw.WriteLine();
                sw.WriteLine("Hoop reinforcement ≥ 0.04% of volume / unit length");

                double H_r = (0.04 / 100) * ((Math.PI / 4.0) * (De * De - Di * Di));
                H_r = double.Parse(H_r.ToString("0.0000000"));
                sw.WriteLine("                   ≥ (0.04/100)[(π/4)*(De*De - Di*Di)]");
                sw.WriteLine("                   ≥ (0.04/100)[(π/4)*({0}*{0} - {1}*{1})]", De, Di);
                sw.WriteLine("                   ≥ {0} cu.m/m", H_r);

                double H_r_1 = H_r * 7200.0;
                H_r_1 = double.Parse(H_r_1.ToString("0.000"));
                sw.WriteLine("                   ≥ {0} X 7200 kg/m", H_r);
                sw.WriteLine("                   ≥ {0} kg/m", H_r_1);

                sw.WriteLine();
                sw.WriteLine("Using {0} mm diameter bars", D2);
                sw.WriteLine();

                double avg_cir_hoop = Math.PI * 3.1;
                avg_cir_hoop = double.Parse(avg_cir_hoop.ToString("0.000"));


                double bar_unit_wt = D2_unit_Wt;


                sw.WriteLine("Using hoops of average diameter = {0} m", avg_dia);
                sw.WriteLine("Average circumference of the hoop = π * 3.1 = {0} m", avg_cir_hoop);
                //sw.WriteLine("                                  = {0} m", avg_cir_hoop);
                double wt_one_hoop = (bar_unit_wt * avg_cir_hoop);
                wt_one_hoop = double.Parse(wt_one_hoop.ToString("0"));
                sw.WriteLine();
                sw.WriteLine("Weight of one hoop = ({0} * {1}) = {2} kg.", bar_unit_wt, avg_cir_hoop, wt_one_hoop);
                sw.WriteLine();

                double no_hp_per_mtr = H_r_1 / wt_one_hoop;
                no_hp_per_mtr = double.Parse(no_hp_per_mtr.ToString("0.00"));
                sw.WriteLine("No of hoops per metre = ({0}/{1}) = {2}", H_r_1, wt_one_hoop, no_hp_per_mtr);
                sw.WriteLine();

                double spcng_hoop = (1000.0 / no_hp_per_mtr);
                spcng_hoop = double.Parse(spcng_hoop.ToString("0"));
                sw.WriteLine("So, Spacing of hoops = (1000/{0}) = {1} mm", no_hp_per_mtr, spcng_hoop);
                sw.WriteLine();
                sw.WriteLine("Use {0} diameter hoops at {1} mm centres on both faces.       Marked as (B) in the Drawing", D2, Ts);

                // For drawing
                _bars_B = string.Format("Use {0:f0} diameter hoops at {1} mm centres on both faces.", D2, Ts);

                sw.WriteLine();

                #endregion

                #region STEP 3 : WELL CURB
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : WELL CURB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double min_renf = min_reinf;


                sw.WriteLine("Minimum reinforcement = {0} kg/cu.m", min_renf);
                sw.WriteLine();
                sw.WriteLine("Proving a curb, {0} mm deep with a bottom width of {1} mm.", Lc, Tc);
                sw.WriteLine();

                double f12 = ((Ts / 1000) - 0.15);
                f12 = double.Parse(f12.ToString("0.000"));
                double f13 = (Di + 0.15);
                f13 = double.Parse(f13.ToString("0.000"));
                double vol_con_curb = ((Math.PI / 4.0) * (De * De - Di * Di))
                                        - (0.5 * f12 * 0.85 * Math.PI * f13);
                vol_con_curb = double.Parse(vol_con_curb.ToString("0.000"));

                sw.WriteLine("Volume of concrete in curb");
                sw.WriteLine("   = (π/4)*((De*De - Di*Di)) - (0.5 * ((Ts/1000) - 0.15)*0.85*(Di+0.15))");
                sw.WriteLine("   = (π/4)*(({0}*{0} - {1}*{1})) - (0.5 * (({2}/1000) - 0.15)*0.85*({1}+0.15))", De, Di, Ts);
                sw.WriteLine("   = {0} cu.m", vol_con_curb);
                sw.WriteLine();

                double tot_qnty_stl = min_renf * vol_con_curb;
                tot_qnty_stl = double.Parse(tot_qnty_stl.ToString("0"));


                // (C3/4)*(F9*F9-F10*F10)-(F7*F11*F12*C3*F13)
                sw.WriteLine("Total quantity of steel in Curb = {0} * {1} = {2} kg", min_renf, vol_con_curb, tot_qnty_stl);
                sw.WriteLine();
                sw.WriteLine("Using hoops of average diameter = {0} m", avg_dia);
                sw.WriteLine();
                sw.WriteLine("Weight of one hoop of 20 mm diameter = (π * 3.1 * 2.47) = 24 kg       Marked as (C) in the Drawing");

                double val1, val2;

                val1 = tot_qnty_stl / 2.0;

                val2 = val1 / 24.0;
                val2 = 1000.0 / val2;
                val2 = (int)((val2 / 10) + 1);
                val2 = (val2 * 10);

                //Weight of one hoop of 20 mm diameter  = (π * (Di+2x0.300) * 2.47) = (π * 3.1 * 2.47) = 24 kg, and Spacing = 160 mm c/c

                _C = string.Format("Weight of one hoop of 20 mm diameter = (π * (Di+2x0.300) * 2.47) = (π * 3.1 * 2.47) = 24 kg, and Spacing = {0:f0} mm c/c", val2);


                sw.WriteLine("Weight of one hoop of 16 mm diameter = (π * 3.1 * 1.58) = 15.38 kg   Marked as (D) in the Drawing");
                val2 = val1 / 15.38;
                val2 = 1000.0 / val2;
                val2 = (int)((val2 / 10) + 1);
                val2 = (val2 * 10);
                //Weight of one hoop of 16 mm diameter  = (π * (Di+2x0.300) * 1.58) = (π * 3.1 * 1.58) = 15.38 kg, and Spacing = 110 mm c/c
                _D = string.Format("Weight of one hoop of 16 mm diameter = (π * (Di+2x0.300) * 1.58) = (π * 3.1 * 1.58) = 15.38 kg, and Spacing={0:f0} mm c/c", val2);

                sw.WriteLine("Weight of one tie of 8 mm diameter 3 m long = (3 * 0.39) = 1.17 kg   Marked as (E) in the Drawing");
                //val2 = val1 / 15.38;
                _E1 = string.Format("Weight of one tie of 8 mm diameter 3 m long = (3 * 0.39) = 1.17 kg");

                sw.WriteLine();
                sw.WriteLine("Adopting a spacing of 300 mm for ties");
                _E2 = string.Format("Adopting a spacing of 300 mm for ties");

                sw.WriteLine();
                sw.WriteLine("Number of ties required = (π*3.1/0.3) = 33");
                sw.WriteLine();
                sw.WriteLine("Weight of ties = (33 * 1.17) = 39 kg");
                sw.WriteLine("Weight of 8 hoops of 20 mm diameter = (8 * 24) = 192 kg");
                sw.WriteLine("Weight of 6 hoops of 16 mm diameter = (6 * 15.38) = 92 kg");
                sw.WriteLine();
                if (tot_qnty_stl < 323)
                {
                    sw.WriteLine("Total quantity of steel provided in the Curb = (39 + 192 + 92)");
                    sw.WriteLine("                                             = 323 kg > {0} kg, OK", tot_qnty_stl);
                }
                else
                {
                    sw.WriteLine("Total quantity of steel provided in the Curb = (39 + 192 + 92)");
                    sw.WriteLine("                                             = 323 kg < {0} kg, NOT OK", tot_qnty_stl);
                }
                sw.WriteLine();

                #endregion


                #region END OF REPORT
                sw.WriteLine();
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
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region User Data

                sw.WriteLine("di = {0}", Di);
                sw.WriteLine("L = {0}", L);
                sw.WriteLine("fck = {0}", fck);
                sw.WriteLine("fy = {0}", fy);

                sw.WriteLine("K_Indx = {0}", K_Indx);
                sw.WriteLine("K = {0:f3}", K);

                sw.WriteLine("D1 = {0}", D1);
                sw.WriteLine("D2 = {0}", D2);
                sw.WriteLine("Lc = {0}", Lc);
                sw.WriteLine("Tc = {0}", Tc);

                #endregion
            }
            catch (Exception ex) { }

            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
       

        public string FilePath
        {
            set
            {
               
                user_path = value;
                file_path = Path.Combine(user_path, "Design of RCC Well Foundation");
                if (Directory.Exists(file_path) == false)
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (Directory.Exists(system_path) == false)
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_Found_Well_Found.TXT");
                user_input_file = Path.Combine(system_path, "RCC_WELL_FOUNDATION.FIL");
                drawing_file = Path.Combine(system_path, "RCC_WELL_FOUNDATION_DRAWING.FIL");
 

            }
        }
        public void Write_Drawing_File()
        {
            StreamWriter sw = new StreamWriter(new FileStream(drawing_file, FileMode.Create));
            try
            {
                sw.WriteLine("_inner_dia={0}", _inner_dia);
                sw.WriteLine("_outer_dia={0}", _outer_dia);
                sw.WriteLine("_bars_A={0}", _bars_A);
                sw.WriteLine("_bars_B={0}", _bars_B);
                sw.WriteLine("_C={0}", _C);
                sw.WriteLine("_D={0}", _D);
                sw.WriteLine("_E1={0}", _E1);
                sw.WriteLine("_E2={0}", _E2);
                sw.WriteLine("_L=L = {0} m", L);
                sw.WriteLine("_Tc=Tc = {0} m", (Tc / 1000.0));
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

        #endregion

        
    }
    public   class  PileFoundation
    {
        public string rep_file_name = "";
        public string user_input_file = "";
        public string user_drawing_file = "";
        public string user_path = "";
        public string file_path = "";
        public string system_path = "";
        public bool is_process = false;
        public IApplication iApp = null;

        public double D, P, K, PL, AM, N_gamma, Nq, Nc, FS, PCBL, SL, FL, sigma_ck, fy, gamma_c;
        public double Np, N, gamma_sub, cap_sigma_ck, cap_fy, sigma_cbc, sigma_st, m, F, d1, d2, d3;
        public double LPC, BPC, LPr, BPr, DPC, l1, l2, l3;

        public double pile_sigma_c = 0.0;
        public double pile_sigma_st = 0.0;

        

        //Chiranjit [2016 06 16]
        public double d_dash, main_dia, lateral_dia, max_spacing;

        public double shear_dia, clear_cover, cap_spacing;

        //chiranjit [2013 06 17]
        public string BoreholeNo { get; set; }

        string _1, _2, _3, _4;

        public PileFoundationTableCollection pft_list = null;
        public PileFoundation(IApplication app)
        {
            iApp = app;
            Project_Name = "Design of RCC Pile Foundation";
        }

     
        #region IReport Members

        public void Calculate_Program()
        {
            string ref_string = "";
            StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 20.0           *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*       DESIGN OF RCC PILE FOUNDATION         *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");

                #endregion

                #region USER DATA

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("FOR DESIGN OF PILE");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Pile Diameter [D] = {0} m",  D );
                sw.WriteLine("Applied Load on Pile Group [P] = {0} Ton", P);
                sw.WriteLine("Applied Moment on Pile Group [AM] = {0} Ton-m", AM);
                sw.WriteLine("Coefficient of Active Earth Pressure [K] = {0}", K);


                double phi = pft_list[pft_list.Count - 1].Phi;

                if(pft_list.Count > 0)
                    iApp.Tables.Terzaghi_Bearing_Capacity_Factors(phi, ref Nc, ref Nq, ref N_gamma, ref ref_string);   //Chiranjit [2013 06 17;


                //sw.WriteLine("Nγ = {0}",  N_gamma );
                //sw.WriteLine("Nq = {0}",  Nq );
                //sw.WriteLine("Nc = {0}",  Nc );
                sw.WriteLine("Factor of Safety [FS] = {0}",  FS );
                sw.WriteLine("Pile Cap Bottom Level [PCBL] = {0} m",  PCBL );
                sw.WriteLine("Length of Pile = [PL] = {0} m", PL);

                sw.WriteLine("Founding Level [FL] = {0}", FL);
                
                sw.WriteLine("Scour Level [SL) = {0} m",  SL );

                sw.WriteLine("Concrete Grade [σ_ck] = M{0:f0}", sigma_ck);

                sw.WriteLine("Allowable Flexural Stress in Concrete [σ_c] = {0} N/Sq.mm", pile_sigma_c);
                sw.WriteLine("Steel Grade [fy] = Fe{0:f0}", fy);

                sw.WriteLine("Permissible Stress in Steel [σ_st] = {0} N/Sq.mm", pile_sigma_st);

                sw.WriteLine("Unit Weight of Concrete [γ_c] = {0} Ton/cm",  gamma_c );
                sw.WriteLine("Total Piles [Np] = {0}",  Np );
                sw.WriteLine("Total Piles in front row [N] = {0}",  N );

                sw.WriteLine("γ_sub = {0} Ton/cu.m", gamma_sub);

                sw.WriteLine("");
                sw.WriteLine("Pile Reinforcement Cover [d'] = {0} mm.", d_dash);
                sw.WriteLine("Pile Main Reinforcement Dia. = {0} mm.", main_dia);
                sw.WriteLine("Pile Lateral Reinforcement / Binder Dia. = {0} mm.", lateral_dia);
                sw.WriteLine("Pile Maximum spacing of Rebars = {0} mm.", max_spacing);
                sw.WriteLine("");

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("FOR DESIGN OF PILE CAP");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Concrete Grade [σ_ck] = M {0:f0}", cap_sigma_ck);


                //double pcap_sigma_c = 0.0;
                //sw.WriteLine("Allowable Flexural Stress in Concrete [σ_c] = {0} N/sq.mm", pcap_sigma_c);
                
                
                sw.WriteLine("Steel Grade [fy] = Fe {0:f0}", cap_fy);

                //double pcap_sigma_st = 0.0;
                //sw.WriteLine("Allowable Flexural Stress in Concrete [σ_st] = {0} N/sq.mm", pcap_sigma_st);

          


                sw.WriteLine();
                sw.WriteLine("");
                sw.WriteLine("Allowable Stress in concretein bending compression [σ_cbc] = {0} N/sq.mm = {1} kg/sq.cm", sigma_cbc, (sigma_cbc *= 10));
                sw.WriteLine("Allowable stress in steel [σ_st] = {0} N/sq.mm = {1} kg/sq.cm", sigma_st, (sigma_st = sigma_st * 10));
                sw.WriteLine("Modular Ratio [m] = {0}", m);
                sw.WriteLine("Load Factor [F] = {0}", F);


                sw.WriteLine("Diameter of Main Steel Reinforcement bars [d1] = {0} mm",  d1 );
                sw.WriteLine("Bottom Reinforcement Bar Diameter [d2] = {0} mm",  d2 );
                sw.WriteLine("Top Reinforcement Bar Diameter [d3] = {0} mm",  d3 );

                sw.WriteLine("Shear Reinforcement Bar Diameter = {0} mm", shear_dia );
                sw.WriteLine("Reinforcement Clear Cover  = {0} mm", clear_cover);


                sw.WriteLine("Pile Cap Spacing of Rebars  = {0} mm", cap_spacing);
                
                sw.WriteLine("Pile Cap Length [LPC] = {0} mm                  Marked as (LPC) in the Drawing",  LPC );
                sw.WriteLine("Pile Cap Width [BPC] = {0} mm                   Marked as (BPC) in the Drawing",  BPC );
                sw.WriteLine("Depth of Pile Cap [DPC] = {0} mm                Marked as (DPC) in the Drawing", DPC);
                sw.WriteLine("Pier Length [LPr] = {0} mm                      Marked as (LPr) in the Drawing", LPr);
                sw.WriteLine("Pier Width [BPr] = {0} mm                       Marked as (BPr) in the Drawing",  BPr );
                sw.WriteLine("Distance   [L1] = {0} mm                        Marked as (L1) in the Drawing", l1);
                sw.WriteLine("Distance   [L2] = {0} mm                         Marked as (L2) in the Drawing", l2);
                sw.WriteLine("Distance   [L3] = {0} mm                        Marked as (L3) in the Drawing", l3);

                #endregion

                #region DESIGN CALCULATIONS
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                #endregion

                #region STEP 1 : CAPACITY FROM SOIL STRUCTURE INTERACTION
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : CAPACITY FROM SOIL STRUCTURE INTERACTION");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                double L1 = PCBL - FL;
                L1 = double.Parse(L1.ToString("0.000"));
                sw.WriteLine("Pile Length = PL = PCBL - FL");
                sw.WriteLine("                 = {0:f3} - {1:f3}", PCBL, FL);
                sw.WriteLine("                 = {0:f3} m", L1);

                double L2 = SL - FL;
                L2 = double.Parse(L2.ToString("0.000"));
                sw.WriteLine();
                sw.WriteLine("Embedded Length = EL = SL - FL");
                sw.WriteLine("                     = {0:f3} - {1:f3}", SL, FL);
                sw.WriteLine("                     = {0:f3} m", L2);
                sw.WriteLine();

                double Ap = Math.PI * D * D / 4.0;
                Ap = double.Parse(Ap.ToString("0.000"));
                sw.WriteLine("Cross Sectional Area of PIle = Ap = π*D*D/4");
                sw.WriteLine("                             = π*{0}*{0}/4", D);
                sw.WriteLine("                             = {0:f3} sq.m", Ap);
                sw.WriteLine();

                #region (A) FOR COHESIONLESS COMPONENT OF SOIL
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("(A) FOR COHESIONLESS COMPONENT OF SOIL :");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                #endregion

                #region SKIN FRICTION
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("SKIN FRICTION :");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                #endregion

                //sw.WriteLine("Layers ,Thickness, Depth below, Surface, φ (deg), δ (deg), γ_sub, P_D, P_Di");
                //sw.WriteLine("Layers ,of Sub soil Layer, scour level(H), Area (As), φ (deg), δ (deg), γ_sub, Pd, Pdi");
                sw.WriteLine();
                sw.WriteLine("Borehole No. : {0}", BoreholeNo);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("----------------------------------------------------------------------------------------------");

                sw.WriteLine("{0,-6} {1,10} {2,12} {3,12} {4,5} {5,7} {6,7} {7,7} {8,10} {9,7}",
                       "Layer",
                       "Depth upto",
                       "Thickness",
                       "Depth below",
                       "Surface",
                       "φ    ",
                       "δ    ",
                       "γ_sub",
                       "P_D   ",
                       "P_Di  ");
                sw.WriteLine("{0,-6} {1,10} {2,12} {3,12} {4,5} {5,7} {6,7} {7,7} {8,10} {9,7}",
                                       "Nos.",
                                       "bottom ",
                                       "of Sub ",
                                       "scour  ",
                                       "Area  ",
                                       "(deg)",
                                       "(deg)",
                                       "Ton /",
                                       "γ_sub*H",
                                       "Ton /");
                sw.WriteLine("{0,-6} {1,10} {2,12} {3,12} {4,5} {5,7} {6,7} {7,7} {8,10} {9,7}",
                                                      "",
                                                      "of Layer",
                                                      "soil Layer",
                                                      "level(H) ",
                                                      "(As)  ",
                                                      "",
                                                      "",
                                                      "cu.m",
                                                      "Ton /",
                                                      "sq.mm");
                sw.WriteLine("{0,-6} {1,10} {2,12} {3,12} {4,5} {5,7} {6,7} {7,7} {8,10} {9,7}",
                                                        "",
                                                        "(m)   ",
                                                        "(m)    ",
                                                        "(m)   ",
                                                        "",
                                                        "",
                                                        "",
                                                        "",
                                                        "sq.m",
                                                        "");
                sw.WriteLine("----------------------------------------------------------------------------------------------");

                for (int i = 0; i < pft_list.Count; i++)
                {
                    sw.WriteLine("{0,-6:f0} {1,10:f3} {2,10:f3} {3,12:f3} {4,8:f2} {5,6:f1}° {6,6:f1}° {7,7:f3} {8,10:f3} {9,7:f3}",
                        pft_list[i].Layers,
                        pft_list[i].Depth,
                        pft_list[i].Thickness,
                        pft_list[i].H_DepthBelowScourLevel,
                        pft_list[i].SurfaceArea,
                        pft_list[i].Phi,
                        pft_list[i].Delta,
                        pft_list[i].GammaSub,
                        pft_list[i].P_D,
                        pft_list[i].P_Di);
                }
                sw.WriteLine("----------------------------------------------------------------------------------------------");

                sw.WriteLine();
                for (int i = 0; i < pft_list.Count; i++)
                {
                    if (i == 0)
                    {
                        sw.WriteLine("P_Di[1] = (0 + {0})/2 = {1} Ton/sq.m", pft_list[0].P_D, pft_list[0].P_Di);
                    }
                    else
                    {
                        //sw.WriteLine("P_Di1 = (0 + {0})/2 = {1} Ton/sq.m", pft_list[0].P_D, pft_list[0].P_Di);
                        sw.WriteLine("P_Di[{0}] = ({1} + {2})/2 = {3} Ton/sq.m",
                            (i + 1),
                            pft_list[i - 1].P_D,
                            pft_list[i].P_D,
                            pft_list[i].P_Di);

                    }
                }
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Ultimate Resistance by Skin Friction :");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double sk_frc, Rfs;
                sk_frc = 0;
                Rfs = 0;

                List<double> list_dbl = new List<double>();
                for (int i = 0; i < pft_list.Count; i++)
                {
                    sk_frc = pft_list[i].SurfaceArea * K * pft_list[i].P_Di * Math.Tan((Math.PI / 180.0) * pft_list[i].Delta);
                    sk_frc = double.Parse(sk_frc.ToString("0.000"));
                    Rfs += sk_frc;

                    list_dbl.Add(sk_frc);
                    //sw.WriteLine("For Layer {0} : As{1}* K * P_Di{1} * tan δ", pft_list[i].Layers, (i + 1));

                    sw.WriteLine("For Layer {0} : As[{1}]* K * P_Di[{1}] * tan δ",
                        pft_list[i].Layers,
                        (i + 1));
                    sw.WriteLine("            = {0} * {1} * {2} * tan {3}",
                         pft_list[i].SurfaceArea,
                         K,
                         pft_list[i].P_Di,
                         pft_list[i].Delta);
                    sw.WriteLine("            = {0} Ton", sk_frc);
                    sw.WriteLine();
                }


                sw.WriteLine("Total Ultimate Resistance due to Skin Friction = Rfs = {0} Ton", Rfs);

                for (int i = 0; i < pft_list.Count; i++)
                {
                }

                sw.WriteLine();

                #endregion

                #region END BEARING
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("END BEARING");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Ultimate Resistance by End Bearing :");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Referring to Table No. 2, given at the end of this report,");
                sw.WriteLine();
                sw.WriteLine("For the value of φ = {0} deg, Nc = {1}, Nq = {2} and Nγ = Nr = {3}", phi, Nc, Nq, N_gamma);
                sw.WriteLine();
                sw.WriteLine();

                double R_us = Ap * ((1.0 / 2.0) * 0.92 * D * N_gamma);

                int cnt = pft_list.Count;

                if (cnt >= 1)
                {
                    R_us = Ap * ((1.0 / 2.0) * pft_list[cnt - 1].GammaSub * D * N_gamma + (pft_list[cnt - 1].P_D * Nq));

                    sw.WriteLine("R_us = Ap * ((1/2) * γ * D * Nγ + P_D[{0}] * Nq) ", cnt);
                    sw.WriteLine("     = {0:f3} * (0.5 * {1:f3} * {2} * {3:f3} + {4:f3} * {5:f3})) ",
                        Ap,
                        pft_list[cnt - 1].GammaSub,
                        D,
                        N_gamma,
                        pft_list[cnt - 1].P_D,
                        Nq);
                    sw.WriteLine();
                    sw.WriteLine("     = {0:f3} Ton", R_us);
                }
                sw.WriteLine();

                double total_resist = Rfs + R_us;
                sw.WriteLine("Total Ultimate Resistance of Pile = Rfs + R_us");
                sw.WriteLine("                                  = {0:f3} + {1:f3}", Rfs, R_us);
                sw.WriteLine("                                  = {0:f3} Ton", total_resist);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Factor of Safety = FS = {0}", FS);
                double Qus = total_resist / FS;

                sw.WriteLine();
                sw.WriteLine("Safe Load on Pile = {0:f3} / {1} = {2:f3} Ton", total_resist, FS, Qus);

                #endregion

                #region END BEARING
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("(B) FOR COHESIVE COMPONENT OF SOIL :");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("-----------------------------------------------------------------------------");
                sw.WriteLine("{0,-6} {1,10} {2,14} {3,14} {4,7} {5,7} {6,7}",
                    "Layers",
                    "Layer",
                    "Depth",
                    "Surface",
                    "",
                    "",
                    "Ultimate");
                sw.WriteLine("{0,-6} {1,10} {2,14} {3,14} {4,7} {5,7} {6,7}",
                    "",
                    "Thickness",
                    "below Scour",
                    "Area  ",
                    "α  ",
                    "c  ",
                    "resistance");
                sw.WriteLine("{0,-6} {1,10} {2,14} {3,14} {4,7} {5,7} {6,7}",
                    "",
                    "[D](m)",
                    "Level[H](m)",
                    "[As](sq.m)",
                    "",
                    "",
                    "[As*α*c](Ton)");
                sw.WriteLine("-----------------------------------------------------------------------------");

                double Rfc = 0.0;

                for (int i = 0; i < pft_list.Count; i++)
                {
                    sk_frc = (pft_list[i].SurfaceArea * pft_list[i].Alpha * pft_list[i].Cohesion);
                    Rfc += sk_frc;
                    sw.WriteLine("{0,-6} {1,10:f3} {2,14:f3} {3,14:f3} {4,7:f3} {5,7:f3} {6,7:f3}",
                    pft_list[i].Layers,
                    pft_list[i].Thickness,
                    pft_list[i].H_DepthBelowScourLevel,
                    pft_list[i].SurfaceArea,
                    pft_list[i].Alpha,
                    pft_list[i].Cohesion,
                    sk_frc);
                }
                sw.WriteLine("-----------------------------------------------------------------------------");
                sw.WriteLine("{0,63} {1,7:f3} Ton",
                    "Total Ultimate Resistance = ",
                    Rfc);
                sw.WriteLine();
                //sw.WriteLine("Total Ultimate Resistance = {0:f3} Ton", Rfc);
                //sw.WriteLine();

                //Chiranjit [2013 06 17]

                sw.WriteLine("Referring to Table No. 2, given at the end of this report,");
                sw.WriteLine();
                sw.WriteLine("For the value of φ = {0} deg, Nc = {1}, Nq = {2} and Nr = {3}", phi, Nc, Nq, N_gamma);
                sw.WriteLine();

                double end_brng = Ap * pft_list[cnt - 1].Cohesion * Nc;
                sw.WriteLine("End Bearing = Ap * C[{0}] * Nc", cnt);
                sw.WriteLine("            = {0} * {1} * {2}", Ap, pft_list[cnt - 1].Cohesion, Nc);
                sw.WriteLine("            = {0:f3} ", end_brng);
                sw.WriteLine();

                double Qu = Rfc + end_brng;
                sw.WriteLine("Total Ultimate Resistance of Pile = Qu");
                sw.WriteLine("    Qu = {0:f3} + {1:f3} = {2:f3} Ton", Rfc, end_brng, Qu);
                sw.WriteLine();
                sw.WriteLine("Factor of Safety = FS = {0}", FS);
                sw.WriteLine();

                double Quc = Qu / FS;
                sw.WriteLine("Safe Load on Pile = {0:f3}/{1} = {2:f3} Ton", Qu, FS, Quc);
                sw.WriteLine();

                double perm_load = Qus + Quc;
                sw.WriteLine("Permissible safe Load on Pile = Qus + Quc");
                sw.WriteLine("                              = {0:f3} + {1:f3}", Qus, Quc);
                sw.WriteLine("                              = {0:f3} Ton", perm_load);
                sw.WriteLine();
                sw.WriteLine("Applied Load on Pile = P = {0} Ton", P);
                sw.WriteLine();



                double load_cap = (LPC * BPC * DPC / 10E8) * gamma_c;
                //sw.WriteLine("Applied Load on Pile Group         = P = 216.000 Ton");
                sw.WriteLine("");
                sw.WriteLine("Load by Pile Cap on Pile Group = LPC x BPC x DPC x γ_c ");
                sw.WriteLine("                               = {0:f3} x {1:f3} x {2:f3} x {3:f3}", (LPC / 1000.0), (BPC / 1000.0), (DPC / 1000.0), gamma_c);
                sw.WriteLine("                               = {0:f3} Ton", load_cap);
                sw.WriteLine("");
                double self_wt = Ap * L1 * (gamma_c - 1.0);

                sw.WriteLine("Self weight of each Pile = Ap * L1 * (γ_c - 1)");
                sw.WriteLine("                    = {0:f3} * {1} * ({2} - 1)", Ap, L1, gamma_c);
                sw.WriteLine("                    = {0:f3} Ton", self_wt);
                sw.WriteLine();
                //sw.WriteLine("Self weight of each Pile                 = Ap * L1 * (γ_c - 1)");
                //sw.WriteLine("                                    = 0.785 * 25 * (2.5 - 1)");
                //sw.WriteLine("                                    = 29.438 Ton");
                sw.WriteLine("");

                double total_load = (P + load_cap) / Np + self_wt;
                sw.WriteLine("Total Load on Pile  = Pu");
                sw.WriteLine("                    = (P + Load by Pile Cap on Pile Group)/ Total Piles [Np] + Self weight of Pile");
                sw.WriteLine("                    = ({0:f3} + {1:f3})/{2:f0} + {3:f3} ", P, load_cap, Np, self_wt);
                sw.WriteLine("                    = {0:f3} + {1:f3}",((P + load_cap) / Np) ,self_wt);
                //sw.WriteLine("= 100.772 Ton < 449.824 Ton, Hence, Safe");
                sw.WriteLine("");

                double Pu = total_load;
                

                //double total_load = P + self_wt;
                if (total_load < perm_load)
                    sw.WriteLine("                    = {0:f3} Ton < {1:f3} Ton, Hence, Safe", total_load, perm_load);
                else
                    sw.WriteLine("                    = {0:f3} Ton > {1:f3} Ton, Hence, Unsafe, NOT OK", total_load, perm_load);


                #endregion

                #region STEP 2 : STRUCTURAL DESIGN OF PILE
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : STRUCTURAL DESIGN OF PILE");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();


                sw.WriteLine("Pile Dia = D = {0} m = {1} mm", D, (D * 1000));
                D = D * 1000;
                sw.WriteLine();
                double cover = 0.1 * D;
                frmPile_Graph fpg = new frmPile_Graph();

                //sw.WriteLine("Cover = d' = 0.1 * D = 0.1 * {0} = {1} mm", D, cover);

                cover = d_dash;
                sw.WriteLine("Pile Reinforcement Cover  = d' = {0} mm", cover);
                sw.WriteLine();
                sw.WriteLine("Cover / Pile Dia = d' / D = {0} / {1} = {2}", cover, D, (cover / D));
                fpg.txt_ddash.Text = (cover / D).ToString("0.00");
                sw.WriteLine();

                double val1 = Pu * 1000 * 10 / (sigma_ck * D * D);
                sw.WriteLine("Pu/(σ_ck*D*D) = {0:f3}*1000*10/({1}*{2}*{2})", Pu, sigma_ck, D);
                sw.WriteLine("              = {0:f4}", val1);
                fpg.txt_Pu.Text = val1.ToString("0.0000");
                sw.WriteLine();

                val1 = (AM * 1000 * 10 * 1000) / (sigma_ck * D * D * D);
                sw.WriteLine("Mu/(σ_ck*D**3) = {0}/({1}*{2}^3)", AM, sigma_ck, D);
                sw.WriteLine("               = {0:f4}", val1);
                fpg.txt_Mu.Text = val1.ToString("0.0000");
                sw.WriteLine();
                fpg.txt_sigma_y.Text = fy.ToString();
                fpg.txt_sigma_ck.Text = sigma_ck.ToString();
                fpg.txt_obtaned_value.Text = "0.0";

                fpg.ShowDialog();
                val1 = MyList.StringToDouble(fpg.txt_obtaned_value.Text, 0.0);
                sw.WriteLine("From Chart, we get p/σ_ck = {0:f4}", val1);
                sw.WriteLine();

                double _p = val1 * sigma_ck;
                sw.WriteLine("Therefore percentage of steel = p = {0:f4} x {1} = {2:f4}", val1, sigma_ck, _p);

                sw.WriteLine();
                sw.WriteLine("In piles, if  p <= 0.4% then provide p = 0.4%, here p = {0}%", _p);
                sw.WriteLine();

                if (_p < 0.4)
                {
                    _p = 0.4;
                }
                val1 = _p;

                sw.WriteLine("Provide {0}% Steel.", val1);
                sw.WriteLine();

                double area_mn_st = (val1 / 100) * (Math.PI / 4.0) * D * D;
                sw.WriteLine("Area of Main Steel Reinforcement = As");
                sw.WriteLine();
                sw.WriteLine("   As = ({0}/100) * (π / 4) * D * D", val1);
                sw.WriteLine("      = ({0}/100) * (π / 4) * {1} * {1}", val1, D);
                sw.WriteLine("      = {0:f2} sq.mm", area_mn_st);
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("Pile Main Reinforcement Bar Dia. = {0} mm", main_dia);
                sw.WriteLine();

                double ar_one_st_br = (Math.PI * main_dia * main_dia) / 4.0;


                sw.WriteLine("Area of one Steel reinforcement bar = π * {0} * {0} / 4", main_dia);
                sw.WriteLine("                                    = {0:f2} sq.mm", ar_one_st_br);
                sw.WriteLine();

                double total_bar = (int)((area_mn_st / ar_one_st_br) + 1);

                //total_bar = ((total_bar) + 1);

                sw.WriteLine("Total number of bars = {0:f2} / {1:f2} = {2:f3} = {3:f0} Nos", area_mn_st, ar_one_st_br, (area_mn_st / ar_one_st_br), total_bar);
                sw.WriteLine();

                sw.WriteLine("");
                sw.WriteLine("Maximum spacing of Rebars = {0} mm.", max_spacing);
                sw.WriteLine("");
                sw.WriteLine("Side cover in Piles = {0} mm.", clear_cover);
                sw.WriteLine("");
                //sw.WriteLine("Radius of Pile upto Rebars = (1200/2) - 75 = 600 - 75 = 525 mm.");

                double radius = (D / 2.0) - clear_cover;
                sw.WriteLine("Radius of Pile up to Rebars = ({0}/2) - {1} = {2} - {1} = {3} mm.", D, clear_cover, (D / 2), radius);

                double peri = 2 * Math.PI * radius;
                sw.WriteLine("");
                sw.WriteLine("Perimeter along Rebars = 2 x 3.1416 x {0:f0} = {1:f3} mm.", radius, peri);
                sw.WriteLine("");

                double req_spc = peri / (total_bar - 1);

                req_spc = (int) req_spc;
                if (req_spc > max_spacing)
                {
                    sw.WriteLine("Spacing of bars = {0:f3} / ({1} - 1) = {2:f0} mm. > {3:f0} mm. Maximum spacing of Rebars,", peri, total_bar, req_spc, max_spacing);
                    sw.WriteLine("");
                    sw.WriteLine("So, Provide Spacing = {0:f0} mm.", max_spacing);
                    sw.WriteLine("");

                    req_spc = max_spacing;
                }
                else
                {
                    sw.WriteLine("Spacing of bars = {0:f3} / ({1} - 1) = {2:f0} mm. < {3:f0} mm. Maximum spacing of Rebars,", peri, total_bar, req_spc, max_spacing);
                    sw.WriteLine("");
                    sw.WriteLine("So, Provide Spacing = {0:f0} mm.", req_spc);
                    sw.WriteLine("");
                }
                total_bar = (int)((peri / req_spc) + 1);

                total_bar = ((int)(total_bar));
                //sw.WriteLine("Nos. of Rebars = ({0:f3} / {1:f0}) + 1 = 17.49 = 18 Nos.", peri, max_spacing);
                //sw.WriteLine("Nos. of Rebars = ({0:f3} / {1:f0}) + 1 = {2:f3} = {3:f0} Nos.", peri, req_spc, ((peri / req_spc) + 1), total_bar);
                sw.WriteLine("");
                //sw.WriteLine("Provide 15 numbers T20 mm dia bars.");
                //sw.WriteLine("Provide 18 numbers T20 mm dia bars.");
                //sw.WriteLine("Provide {0} numbers T{1} mm dia bars.", total_bar, main_dia);
                sw.WriteLine("Provide {0:f0} numbers T{1:f0} mm dia bars at spacing {2:f0} mm c/c.", total_bar, main_dia, req_spc);
                sw.WriteLine("");
                sw.WriteLine("Pile Lateral Reinforcement / Binder Bar Dia. = {0} mm. ", lateral_dia);
                sw.WriteLine("");


                sw.WriteLine();
                sw.WriteLine("Use {0} mm diameter lateral MS bars as Ties / Binders", lateral_dia);
                sw.WriteLine("the pitch / spacing = r < 500 mm");
                sw.WriteLine("                        < 16*d1 = 16*{0} = {1} mm", main_dia, (16 * main_dia));

                val1 = (int)((16 * main_dia) / 100.0);
                val1 *= 100;

                double provide_spacing = val1;
                sw.WriteLine("                        < {0} mm", val1);
                sw.WriteLine();

                //sw.WriteLine("Provide T8 mm dia bars as lateral Ties/binders with spacing of 300 mm c/c     Marked as (4) in the Drawing", total_bar, d1, provide_spacing);
                sw.WriteLine("Provide T{0} mm dia bars as lateral Ties/binders with spacing of {1} mm c/c     Marked as (4) in the Drawing", lateral_dia, provide_spacing);
                //(4)  Main Bars 10 Nos. Dia. 20 MM. in Piles
                _4 = string.Format("Main Bars {0} Nos. Dia. {1} mm. in Piles", total_bar, d1, provide_spacing);


                #endregion

                #region STEP 3 : STRUCTURAL DESIGN OF PILE CAP
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : DESIGN OF PILE CAP :");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                double n = (m * sigma_cbc) / (sigma_st + (m * sigma_cbc));
                sw.WriteLine("Neutral Axis Factor = n = (m * σ_cbc) / (σ_st + (m * σ_cbc))");
                sw.WriteLine("         = ({0} * {1}) / ({2} + ({0} * {1}))", m, sigma_cbc, sigma_st);
                sw.WriteLine("         = {0:f3}", n);
                sw.WriteLine();

                double j = (1 - (n / 3));
                sw.WriteLine("Lever Arm Factor = j = 1 - (n/3) = 1 - ({0:f3}/3) = {1:f3}", n, j);
                sw.WriteLine();


                double Q = 0.5 * sigma_cbc * j * n;
                sw.WriteLine("Q = 0.5 * σ_cbc * j * n");
                sw.WriteLine("  = 0.5 * {0} * {1:f3} * {2:f3}", sigma_cbc, j, n);
                sw.WriteLine("  = {0:f3}", Q);
                sw.WriteLine();
                double P2 = N * Pu;
                sw.WriteLine("Sum of Forces on Piles in front row = P2 = N X Pu = {0} X {1:f3} = {2:f3} Ton", N, Pu, P2);
                //sw.WriteLine("  P2 = {0} + {1} = {2} Ton", N, P, P2);
                sw.WriteLine();
             
                double mom_pier = P2 * ((l1 / 1000.0) - (l2 / 1000.0));
                sw.WriteLine("Moment at the Face of Pier = P2 * ((L1 / 1000.0) - (L2 / 1000.0))");
                sw.WriteLine("                           = {0} * (({1} / 1000.0) - ({2} / 1000.0))", P2, l1, l2);
                sw.WriteLine("                           = {0:f2} Ton-m", mom_pier);
                sw.WriteLine();

                double P3 = (l1 / 1000.0) * (LPC / 1000.0) * (DPC / 1000.0) * gamma_c;
                sw.WriteLine("Relief due to self wt of Pile Cap = P3");
                sw.WriteLine(" = P3 = (L1 / 1000.0) * (LPC / 1000.0) * (DPC / 1000.0) * γ_c");
                sw.WriteLine(" = ({0} / 1000.0) * ({1} / 1000.0) * ({2} / 1000.0) * {3}", l1, LPC, DPC, gamma_c);
                sw.WriteLine(" = {0:f3} Ton", P3);
                sw.WriteLine();

                double mom_self_wt = P3 * (l1 / (1000.0 * 2));
                sw.WriteLine("Moment due to self wt of Pile Cap");
                sw.WriteLine("  = {0:f3} * (L1 / (1000.0 * 2))", P3);
                sw.WriteLine("  = {0:f3} * ({1} / (1000.0 * 2))", P3, l1);
                sw.WriteLine("  = {0:f3} Ton-m", mom_self_wt);
                sw.WriteLine();

                double total_mom = mom_pier - mom_self_wt;
                sw.WriteLine("Total Moment at the Face of Pier = {0:f3} - {1:f3} = {2:f3} Ton-m", mom_pier, mom_self_wt, total_mom);

                sw.WriteLine();


                double M = total_mom / (LPC / 1000.0);
                sw.WriteLine("Moment per Linear metre = {0:f3} / (LPC/1000)", total_mom);
                sw.WriteLine("                        = {0:f3} / ({1}/1000)", total_mom, LPC);
                sw.WriteLine("                        = {0:f2} Ton-m/m", M);
                sw.WriteLine();


                double req_dep = (M * 10E4) / (Q * 100);
                req_dep = Math.Sqrt(req_dep);

                sw.WriteLine("Depth required = √(({0:f2}*10^5)/(Q*100))", M);
                sw.WriteLine("               = √(({0:f2}*10^5)/({1:f2}*100))", M, Q);
                sw.WriteLine("               = {0:f2} cm = {1:f2} mm", req_dep, (req_dep * 10));

                req_dep = req_dep * 10;
                sw.WriteLine();
                sw.WriteLine("Overall Depth Provided = {0} mm", DPC);
                sw.WriteLine();
                //sw.WriteLine("Clear Cover = 175 mm");
                sw.WriteLine("Reinforcement Clear Cover in Pile Cap = {0} mm", clear_cover);
                sw.WriteLine();
                double half_bar_dia = d2 / 2.0;
                sw.WriteLine("Half Bar diameter of Steel Reinforcements = {0} m", half_bar_dia);
                sw.WriteLine();

                double eff_dep = DPC - clear_cover - half_bar_dia;
                sw.WriteLine("Effective Depth Provided = {0} - {1} - {2}", DPC,clear_cover, half_bar_dia);
                if (eff_dep > req_dep)
                {
                    sw.WriteLine("                         = {0:f3} mm > {1:f2} mm, Hence OK", eff_dep, req_dep);
                }
                else
                {
                    sw.WriteLine("                         = {0:f3} mm < {1:f3} mm, Hence NOT OK", eff_dep, req_dep);
                }
                double deff = eff_dep / 10;
                sw.WriteLine();
                sw.WriteLine("deff = {0} mm = {1} cm", eff_dep, deff);
                sw.WriteLine();

                double req_st_renf = (M * 10E4) / (j * sigma_st * deff * 1);
                sw.WriteLine("Required Steel Reinforcement = M * 10^5/(j*σ_st*deff*1)");
                sw.WriteLine("    = {0:f3} * 10^5/({1:f3}*{2}*{3}*1)", M, j, sigma_st, deff);
                sw.WriteLine("    = {0:f3} sq.cm/m", req_st_renf);
                sw.WriteLine();

                double req_min_tension = (0.2 / 100) * deff;
                sw.WriteLine("Required minimum Steel for tension = 0.2%");
                sw.WriteLine("                                   = (0.2/100) * {0}", deff);
                sw.WriteLine("                                   = {0:f3} sq.cm/m", req_min_tension);
                sw.WriteLine();

                sw.WriteLine("Pile Cap Main Reinforcement Bar Diameter = {0} mm.", d2);


                //double cap_spacing = 150;
                //cap_spacing = 150;

                sw.WriteLine("Provide Steel Reinforcements {0} Diameter bars @{1} mm c/c spacing.    Marked as (2) in the Drawing", d2, cap_spacing);
                //(2)  Main Bottom Bars Dia. 25 @ 150 c/c
                _2 = string.Format("Main Bottom Bars Dia. {0} @ {1} c/c", d2, cap_spacing);


                sw.WriteLine();
                double pro_area_st = (Math.PI * d2 * d2 / 4.0) * (1000.0 / 150.0);

                pro_area_st = pro_area_st / 100;

                if (pro_area_st > req_min_tension)
                {
                    sw.WriteLine("Area of Steel Provided at the bottom of the Pile Cap");
                    sw.WriteLine("in Longitudinal direction = {0:f3} sq.cm/m > {1:f3} sq.cm/m, Hence OK",
                        pro_area_st, req_min_tension);
                }
                else
                {
                    sw.WriteLine("Area of Steel Provided at the bottom of the Pile Cap");
                    sw.WriteLine("in Longitudinal direction = {0:f3} sq.cm/m < {1:f3} sq.cm/m, Hence NOT OK",
                        pro_area_st, req_min_tension);
                }
                sw.WriteLine();

                double nom_steel = (0.06 / 100) * deff;
                sw.WriteLine("Steel Provided in Longitudinal derection at the top of ");
                sw.WriteLine("Pile Cap = Nominal Steel = 0.06% of Area");
                sw.WriteLine("         = (0.06/100) * deff");
                sw.WriteLine("         = (0.06/100) * {0}", deff);
                sw.WriteLine("         = {0:f3} sq.cm/m", nom_steel);
                sw.WriteLine();
                sw.WriteLine("Pile Cap Distribution Reinforcement Bar Diameter = {0} mm", d3);
                sw.WriteLine();


                sw.WriteLine("Provided {0} mm dia bars at {1} mm c/c spacing.         Marked as (1) in the Drawing", d3, cap_spacing);
                //(1)  Main Top Bars Dia. 16 @ 150 c/c
                _1 = string.Format("Main Top Bars Dia. {0} @ {1} c/c", d3, cap_spacing);

                sw.WriteLine();

                double area_top = (Math.PI * d3 * d3 / 4.0) * (1000.0 / 150.0);
                sw.WriteLine("Area of Steel Provided = (π*{0}*{0}/4)*(1000/150)", d3);
                sw.WriteLine("                       = {0:f2} sq.mm/m", area_top);
                area_top = area_top / 100;
                sw.WriteLine("                       = {0:f2} sq.cm/m", area_top);

                sw.WriteLine();
                sw.WriteLine("Distribution Steel provided at top and bottom of Pile Cap");
                sw.WriteLine(" {0} mm dia bars at 150 mm c/c spacing", d3);
                sw.WriteLine();
                area_top = (Math.PI * d3 * d3 / 4.0) * (1000.0 / 150.0);
                sw.WriteLine("Area of Steel Provided = (π*{0}*{0}/4)*(1000/150)", d3);
                sw.WriteLine("                       = {0:f2} sq.mm/m", area_top);
                area_top = area_top / 100;
                sw.WriteLine("                       = {0:f2} sq.cm/m", area_top);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Shear Reinforcement :");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                deff = deff / 100;
                sw.WriteLine("Critical section at deff = {0} m from face of Pier", deff);
                sw.WriteLine();
                //sw.WriteLine("Factor for Distribution of Load = 0.1375");
                sw.WriteLine();

                //double P4 = P2 * 0.1375;
                double P4 = P2;

                //sw.WriteLine("Reaction on Piles in Front row  = P4 = {0} * 0.1375 = {1:f2} Ton", P2, P4);
                sw.WriteLine("Reaction on Piles in Front row  = P2 = {0:f2} Ton", P4);
                sw.WriteLine();

                //val1 = (((LPC - (l1 - deff)) / 1000.0) * DPC * gamma_c);
                deff = deff * 1000;
                val1 = (LPC / 1000.0) * ((l1 / 1000.0) - (deff / 1000.0)) * (DPC / 1000.0) * gamma_c;
                double tau_v = (P4 - val1) / (deff);

                //double _tau_v = P4 * 10 * (LPC / 1000.0) * ((l1 / 1000.0) - (deff / 1000.0));
                double _tau_v = P4 * 10 * LPC * (l1 - deff);

                tau_v = (_tau_v) / (LPC * deff * 1000);
                tau_v = double.Parse(tau_v.ToString("0.000"));

                sw.WriteLine("Nominnal Shear stress = τ_v");
                sw.WriteLine();
                //sw.WriteLine("τ_v = ((P4 - (LPC * ((LPC - (l1 - deff)) / 1000.0) * DPC * γ_c)) / (deff * 1000.0))");
                //sw.WriteLine("    = (({0:f2} - ({1} * (({1} - ({2} - {3})) / 1000.0) * {4} * {5})) / ({3} * 1000.0))",
                //    P4, LPC, l1, deff, DPC, gamma_c);


                sw.WriteLine("τ_v = (P2*10*LPC*(l1 - deff)) / (LPC * deff * 1000)");
                sw.WriteLine("    = ({0}*10*{1}*({2} - {3})) / ({1} * {3} * 1000)", P4, LPC, l1, deff);
                //sw.WriteLine();
                sw.WriteLine("    = {0:f4} N/sq.mm", tau_v);
                sw.WriteLine();

                deff = deff / 10;
                double percent = (100 * pro_area_st) / (100.0 * deff);
                percent = double.Parse(percent.ToString("0.000"));
                sw.WriteLine("Percent of bottom main reinforcement");
                sw.WriteLine(" p = (100 * {0:f3}) / (100.0 * {1})", pro_area_st, deff);
                sw.WriteLine("   = {0:f3}", percent);
                sw.WriteLine();

                double tau_c = Get_Table_1_Value(percent, (CONCRETE_GRADE)cap_sigma_ck, ref ref_string );
                sw.WriteLine("Permissible Shear Stress for p = {0} and for M{1:f0} Concrete", percent, cap_sigma_ck);
                sw.WriteLine("from Table 1 (given at the end of the report). {0}", ref_string);
                sw.WriteLine();
                if (tau_c > tau_v)
                {
                    sw.WriteLine("τ_c = {0:f2} N/sq.mm > τ_v", tau_c);

                    sw.WriteLine();

                    sw.WriteLine("So, no shear Reinforcement is required and provide");
                    sw.WriteLine("provide minimum shear reinforcement.");
                    sw.WriteLine();
                    double min_shr_renf = 0.0011 * 100 * 250;
                    sw.WriteLine("Minimum Shear Reinforcement = 0.0011 * b * S");
                    sw.WriteLine("                            = 0.0011 * 100 * 250");
                    sw.WriteLine("                            = 27.50 sq.cm/m");
                    sw.WriteLine();
                    sw.WriteLine("Provide 8 mm diameter 42 legged at 200 mm c/c spacing");
                    sw.WriteLine();

                    pro_area_st = (Math.PI * 8 * 8 / 4.0) * 42.0 * (1000.0 / 200.0);
                    sw.WriteLine();
                    sw.WriteLine("Area of Steel Provided = (π * 8 * 8 / 4.0) * 42.0 * (1000.0 / 200.0)");
                    sw.WriteLine("                       = {0:f2} sq.mm/m", pro_area_st);

                    pro_area_st = pro_area_st / 100.0;
                    sw.WriteLine("                       = {0:f2} sq.cm/m", pro_area_st);
                }
                else
                {
                    sw.WriteLine("τ_c = {0:f2} N/sq.mm < τ_v", tau_c);
                    sw.WriteLine();

                    double bal_sh_strs = tau_v - tau_c;
                    bal_sh_strs = double.Parse(bal_sh_strs.ToString("0.000"));
                    sw.WriteLine("Balance Shear Stress");
                    sw.WriteLine();
                    sw.WriteLine("τ_v - τ_c = {0} - {1} = {2} N/sq.mm", tau_v, tau_c, bal_sh_strs);
                    sw.WriteLine();

                    double bal_shr_frc = bal_sh_strs * l1 * deff / 1000.0;
                    bal_shr_frc = double.Parse(bal_shr_frc.ToString("0"));
                    sw.WriteLine("Balance Shear Force");
                    sw.WriteLine();
                    sw.WriteLine(" = ({0} * {1} * {2}) / 1000", bal_sh_strs, l1, deff);
                    sw.WriteLine(" = {0} kN", bal_shr_frc);
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("Pile Cap Shear Reinforcement Bar Diameter = {0} mm", shear_dia);
                    sw.WriteLine();
                    sw.WriteLine("Using {0} mm bars @{1} mm c/c            Marked as (3) in the Drawing", shear_dia, max_spacing);
                    //(3)  Shear Reinf. Bars Dia. 10, 42 Legged, Spacing 200 
                    _3 = string.Format("Shear Reinf. Bars Dia. 10, 42 Legged, Spacing 200");

                    sw.WriteLine();

                    double Ast = (bal_shr_frc * max_spacing * 1000.0) / (max_spacing * deff);
                    Ast = double.Parse(Ast.ToString("0"));
                    sw.WriteLine("Required Shear Reinforcement Steel");
                    sw.WriteLine("Ast = ({0} * {1} * 1000.0) / ({1} * {2})", bal_shr_frc, max_spacing, deff);
                    sw.WriteLine("    = {0} sq.mm", Ast);
                    sw.WriteLine("    = {0} sq.cm", (Ast / 100.0));
                    sw.WriteLine();
                    double min_shr_renf = 0.0011 * 100 * 250;
                    sw.WriteLine("Minimum Shear Reinforcement = 0.0011 * b * S");
                    sw.WriteLine("                            = 0.0011 * 100 * 250");
                    sw.WriteLine("                            = 27.50 sq.cm/m");
                    sw.WriteLine();
                    if (Ast < min_shr_renf)
                    {
                        sw.WriteLine();
                        sw.WriteLine("Provide 8 mm diameter 42 legged at 200 mm c/c spacing");
                        sw.WriteLine();

                        pro_area_st = (Math.PI * 8 * 8 / 4.0) * 42.0 * (1000.0 / 200.0);
                        sw.WriteLine();
                        sw.WriteLine("Area of Steel Provided = (π * 8 * 8 / 4.0) * 42.0 * (1000.0 / 200.0)");
                        sw.WriteLine("                       = {0:f2} sq.mm/m", pro_area_st);

                        pro_area_st = pro_area_st / 100.0;
                        sw.WriteLine("                       = {0:f2} sq.cm/m", pro_area_st);
                    }


                    //sw.WriteLine("τ_c = {0} N/sq.mm < τ_v", tau_c);
                    //sw.WriteLine();
                    //sw.WriteLine("Provide Shear reinforcement for balance of");
                    //sw.WriteLine("Shear Stress (τ_v - τ_c) N/sq.mm");
                    sw.WriteLine();
                }
                #endregion

                sw.WriteLine();
                Write_Table_1(sw);
                #region END OF REPORT
                sw.WriteLine();
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
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void Write_Drawing_File()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_drawing_file, FileMode.Create));
            try
            {
                //Length of Pile Cap = LPC = 8800 mm.
                //Width of Pile Cap = BPC = 4300 mm.
                //Thickness of Pile Cap = DPC = 1500 mm.
                //Length of Pier = LPr = 6100 mm.
                //Width of Pier = BPr = 1100 mm.
                //Diameter of Pile = D = 1000 mm.
                //Distance = l1 = 1600 mm.
                //Distance = l2 = 650 mm.
                //Distance = l3 = 1500 mm.

                //(1)  Main Top Bars Dia. 16 @ 150 c/c
                //(2)  Main Bottom Bars Dia. 25 @ 150 c/c
                //(3)  Shear Reinf. Bars Dia. 10, 42 Legged, Spacing 200 
                //(4)  Main Bars 10 Nos. Dia. 20 MM. in Piles


                sw.WriteLine("_LPC=Length of Pile Cap = LPC = {0} mm.", LPC);
                sw.WriteLine("_BPC=Width of Pile Cap = BPC = {0} mm.", BPC);
                sw.WriteLine("_DPC=Thickness of Pile Cap = DPC = {0} mm.", DPC);
                sw.WriteLine("_LPr=Length of Pier = LPr = {0} mm.", LPr);
                sw.WriteLine("_BPr=Width of Pier = BPr = {0} mm.", BPr);
                sw.WriteLine("_D=Diameter of Pile = D = {0} mm.", D * 1000);
                sw.WriteLine("_l1=Distance = l1 = {0} mm.", l1);
                sw.WriteLine("_l2=Distance = l2 = {0} mm.", l2);
                sw.WriteLine("_l3=Distance = l3 = {0} mm.", l3);
                sw.WriteLine("_1={0}", _1);
                sw.WriteLine("_2={0}", _2);
                sw.WriteLine("_3={0}", _3);
                sw.WriteLine("_4={0}", _4);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region User Data

                sw.WriteLine("D = {0}",  D );
                sw.WriteLine("P = {0}",  P );
                sw.WriteLine("K = {0}",  K );
                sw.WriteLine("AM = {0}",  AM );
                sw.WriteLine("N_gamma = {0}",  N_gamma );
                sw.WriteLine("Nq = {0}",  Nq );
                sw.WriteLine("Nc = {0}",  Nc );
                sw.WriteLine("FS = {0}",  FS );
                sw.WriteLine("PCBL = {0}",  PCBL );
                sw.WriteLine("SL = {0}",  SL );
                sw.WriteLine("FL = {0}",  FL );
                sw.WriteLine("sigma_ck = {0}",  sigma_ck );
                sw.WriteLine("fy = {0}",  fy );
                sw.WriteLine("gamma_c = {0}",  gamma_c );
                sw.WriteLine("Np = {0}",  Np );
                sw.WriteLine("N = {0}",  N );
                //sw.WriteLine("gamma_sub = {0}",  gamma_sub );
                sw.WriteLine("cap_sigma_ck = {0}",  cap_sigma_ck );
                sw.WriteLine("cap_fy = {0}",  cap_fy );
                sw.WriteLine("sigma_cbc = {0}",  sigma_cbc );
                sw.WriteLine("sigma_st = {0}",  sigma_st );
                sw.WriteLine("m = {0}",  m );
                sw.WriteLine("F = {0}",  F );
                sw.WriteLine("d1 = {0}",  d1 );
                sw.WriteLine("d2 = {0}",  d2 );
                sw.WriteLine("d3 = {0}",  d3 );
                sw.WriteLine("LPC = {0}",  LPC );
                sw.WriteLine("BPC = {0}",  BPC );
                sw.WriteLine("LPr = {0}",  LPr );
                sw.WriteLine("BPr = {0}",  BPr );
                sw.WriteLine("DPC = {0}",  DPC );
                sw.WriteLine("L1 = {0}",  l1 );
                sw.WriteLine("L2 = {0}",  l2 );
                sw.WriteLine("L3 = {0}",  l3 );

                sw.WriteLine("TABLE = SUB_SOIL");
                for (int i = 0; i < pft_list.Count; i++)
                {
                    sw.WriteLine(pft_list[i].ToString());
                }

                #endregion
            }
            catch (Exception ex) { }

            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public string Project_Name { get; set; }
        public string FilePath
        {
            set
            {
                user_path = value;
                file_path = Path.Combine(user_path, "");
                system_path = Path.Combine(file_path, "");
                rep_file_name = Path.Combine(file_path, "Bridge_Found_Pile_Found.TXT");
                user_input_file = Path.Combine(system_path, "RCC_PILE_FOUNDATION.FIL");
                user_drawing_file = Path.Combine(system_path, "RCC_PILE_FOUNDATION_DRAWING.FIL");
            }
        }
        #endregion

        public double Get_Table_1_Value(double percent, CONCRETE_GRADE con_grade, ref string ref_string)
        {
            return iApp.Tables.Permissible_Shear_Stress(percent, con_grade, ref  ref_string);
        }
        public void Write_Table_1(StreamWriter sw)
        {

            List<string> lst_content = iApp.Tables.Get_Tables_Permissible_Shear_Stress();

            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("TABLE 1 : PERMISSIBLE SHEAR STRESS");
            sw.WriteLine("------------------------------------------------------------");
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            lst_content.Clear(); 
            
            lst_content = iApp.Tables.Get_Tables_Terzaghi_Bearing_Capacity_Factors();
            sw.WriteLine("------------------------------------------------------------");
            sw.WriteLine("TABLE 2 : Terzaghi's Bearing Capacity");
            sw.WriteLine("------------------------------------------------------------");
            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
            lst_content.Clear();
        }


        public double fck { get; set; }
    }
    public class PileFoundationTable
    {
        int sl_n, layers;
        double thickness, phi, alpha, cohesion, H, thick_sub_soil_layer, surface_area, delta, gamma_sub, p_d, p_di;

        public double Depth { get; set; }//Chiranjit
        public PileFoundationTable()
        {
            sl_n = -1;
            thickness = 0.0d;
            phi = 0.0d;
            alpha = 0.0d;
            cohesion = 0.0d;
            H = 0.0d;
            thick_sub_soil_layer = 0.0d;
            surface_area = 0.0d;
            delta = 0.0d;
            gamma_sub = 0.0d;
            p_d = 0.0d;
            p_di = 0.0d;

            Depth = 0.0;
        }
        public int SL_No
        {
            get
            {
                return sl_n;
            }
            set
            {
                sl_n = value;
            }
        }
        public int Layers
        {
            get
            {
                return layers;
            }
            set
            {
                layers = value;
            }
        }
        public double Thickness
        {
            get
            {
                return thickness;
            }
            set
            {
                thickness = double.Parse(value.ToString("0.000"));
            }
        }
        public double Phi
        {
            get
            {
                return phi;
            }
            set
            {
                phi = double.Parse(value.ToString("0.000"));
            }
        }
        public double Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = double.Parse(value.ToString("0.000"));
            }
        }
        public double Cohesion
        {
            get
            {
                return cohesion;
            }
            set
            {
                cohesion = double.Parse(value.ToString("0.000"));
            }
        }
        public double H_DepthBelowScourLevel
        {
            get
            {
                return H;
            }
            set
            {
                H = double.Parse(value.ToString("0.000"));
            }
        }
        public double ThicknessSubSoil
        {
            get
            {
                return thick_sub_soil_layer;
            }
            set
            {
                thick_sub_soil_layer = double.Parse(value.ToString("0.000"));
            }
        }
        public double SurfaceArea
        {
            get
            {
                return surface_area;
            }
            set
            {
                surface_area = double.Parse(value.ToString("0.000"));
            }
        }
        public double Delta
        {
            get
            {
                return delta;
            }
            set
            {
                delta = double.Parse(value.ToString("0.000"));
            }
        }
        public double GammaSub
        {
            get
            {
                return gamma_sub;
            }
            set
            {
                gamma_sub = double.Parse(value.ToString("0.000"));
            }
        }
        public double P_D
        {
            get
            {
                return p_d;
            }
            set
            {
                p_d = double.Parse(value.ToString("0.000"));
            }
        }
        public double P_Di
        {
            get
            {
                return p_di;
            }
            set
            {
                p_di = double.Parse(value.ToString("0.000"));
            }
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", Layers, Thickness, Phi, Alpha, Cohesion, GammaSub);
        }
        public static PileFoundationTable Parse(string str)
        {
            string temp = str;

            temp = MyList.RemoveAllSpaces(temp);
            MyList mList = new MyList(temp, ' ');

            PileFoundationTable pft = new PileFoundationTable();
            if (mList.Count == 7)
            {
                pft.Layers = mList.GetInt(0);
                pft.Delta = mList.GetDouble(1);
                pft.Thickness = mList.GetDouble(2);
                pft.Phi = mList.GetDouble(3);
                pft.Alpha = mList.GetDouble(4);
                pft.Cohesion = mList.GetDouble(5);
                pft.GammaSub = mList.GetDouble(6);
            }
            else
                throw new Exception("Wrong Data!");
            return pft;
        }

    }
    public class PileFoundationTableCollection : IList<PileFoundationTable>
    {
        List<PileFoundationTable> list = null;
        double pile_dia, gama_sub;
        public PileFoundationTableCollection(double pile_diameter)
        {
            list = new List<PileFoundationTable>();
            pile_dia = pile_diameter;
        }

        #region IList<PileFoundationTable> Members

        public int IndexOf(PileFoundationTable item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i].Layers == item.Layers) && (item.Thickness == list[i].Thickness))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, PileFoundationTable item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public PileFoundationTable this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        #endregion

        #region ICollection<PileFoundationTable> Members

        public void Add(PileFoundationTable item)
        {
            double val = 0.0;

            int cnt = list.Count - 1;
            if (cnt >= 0)
            {
                item.SL_No = cnt + 2;
                item.H_DepthBelowScourLevel = item.Thickness + list[cnt].Thickness;
                item.SurfaceArea = Math.PI * pile_dia * item.Thickness;
                if (item.Cohesion == 0.0)
                {
                    item.Delta = item.Phi;
                }
                else
                {
                    item.Delta = (2.0 / 3.0) * item.Phi;
                }
                item.P_D = item.H_DepthBelowScourLevel * item.GammaSub;
                item.P_Di = (list[cnt].P_D + item.P_D) / 2.0;
            }
            else
            {
                item.SL_No = 1;
                item.H_DepthBelowScourLevel = item.Thickness;
                item.SurfaceArea = Math.PI * pile_dia * item.Thickness;
                if (item.Cohesion == 0.0)
                {
                    item.Delta = item.Phi;
                }
                else
                {
                    item.Delta = (2.0 / 3.0) * item.Phi;
                }
                item.P_D = item.H_DepthBelowScourLevel * item.GammaSub;
                item.P_Di = (0 + item.P_D) / 2.0;
            }
            //item.GammaSub = gama_sub;


            if (list.Count > 0)
                item.Thickness = item.Depth - list[list.Count - 1].Depth;
            else
                item.Thickness = item.Depth;

            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(PileFoundationTable item)
        {
            return ((IndexOf(item) != -1) ? true : false);
        }

        public void CopyTo(PileFoundationTable[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(PileFoundationTable item)
        {
            int i = IndexOf(item);
            if (i != -1)
            {
                RemoveAt(i);
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<PileFoundationTable> Members

        public IEnumerator<PileFoundationTable> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion
    }

}
