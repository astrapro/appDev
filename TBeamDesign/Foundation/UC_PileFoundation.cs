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
    public partial class UC_PileFoundation : UserControl
    {
        PileFoundation pile = null;
        public IApplication iApp;

        public string user_path { get; set; }

        public UC_PileFoundation()
        {
            InitializeComponent();

            pile = new PileFoundation(iApp);

            Pile_Foundation_Load();

            //pic_pile.BackgroundImage = AstraFunctionOne.ImageCollection.Dialog_Box_Pile_Foundation_Image01;
            pic_pile.BackgroundImage = AstraFunctionOne.ImageCollection.Pile_Layout_Image01;

        }


        #region Pile Form Event

        private void txt_PCBL_TextChanged(object sender, EventArgs e)
        {
            double pbcl = MyList.StringToDouble(txt_PCBL.Text, 62.0);
            double pl = MyList.StringToDouble(txt_PL.Text, 30.0);
            txt_FL.Text = (pbcl - pl).ToString("f3");
            txt_SL.Text = (pbcl - 2).ToString("f3");
        }


        private void cmb_concrete_grade_steel_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ASTRAGrade astg = null;

            ComboBox cmb = sender as ComboBox;

            Control ctrl = sender as Control;

            if (ctrl.Name.ToLower().StartsWith("cmb_pile"))
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

        public event EventHandler OnButtonClick;

        private void btn_Pile_Process_Click(object sender, EventArgs e)
        {
            //return;


            user_path =  iApp.user_path;

            if (!Directory.Exists(user_path)) return;

            user_path = Path.Combine(iApp.user_path, "Pile Design");


            Directory.CreateDirectory(user_path);



            Pile_Checked_Grid();
            if (Pile_Initialize_InputData())
            {
                pile.Project_Name = "";
                pile.FilePath = user_path;
                pile.Write_User_Input();
                pile.iApp = iApp;
                pile.Calculate_Program();
                pile.Write_Drawing_File();
                if (File.Exists(pile.rep_file_name)) { MessageBox.Show(this, "Report file written in " + pile.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(pile.rep_file_name); }
                pile.is_process = true;
            }
            Button_Enable_Disable();
            //iApp.Save_Form_Record(this, pile.user_path);
        }
        void Button_Enable_Disable()
        {
            pile.FilePath = user_path;
            btnReport.Enabled = File.Exists(pile.rep_file_name);
        }

        private void btn_Pile_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(pile.rep_file_name);
        }
        private void btn_Pile_Drawing_Click(object sender, EventArgs e)
        {
            iApp.Form_Drawing_Editor(eBaseDrawings.RCC_FOUNDATION_PILE, Path.Combine(pile.file_path, "DRAWINGS"), pile.rep_file_name).ShowDialog();
        }
        private void dgv_Pile_SelectionChanged(object sender, EventArgs e)
        {
            Pile_Checked_Grid();
        }
        #endregion Pile Form Event



        #region Pile Methods
        public void Pile_Foundation_Load()
        {




            cmb_pcap_fck.SelectedIndex = 3;
            cmb_pcap_fy.SelectedIndex = 2;
            cmb_pile_fck.SelectedIndex = 3;
            cmb_pile_fy.SelectedIndex = 2;

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

                    if (k == 5 && d == 0.0)
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


        public double Spacing_Len = 7.0;


        public double Spacing_Width = 6.0;

        
        private void txt_D_TextChanged(object sender, EventArgs e)
        {
            Interactive();

        }

        public void Interactive()
        {

            double _D = MyList.StringToDouble(txt_D.Text, 0.0);


            double _Np = MyList.StringToDouble(txt_Np.Text, 0.0);
            double _N = MyList.StringToDouble(txt_N.Text, 0.0);
            double _L2 = MyList.StringToDouble(txt_L2.Text, 0.0);



            //txt_L1.Text = ((Spacing_Width / 2 + _D / 2) * 1000).ToString();
            txt_L1.Text = (_L2 + (_D / 2) * 1000).ToString();
            //txt_L2.Text = "1000";
            txt_L3.Text = ((Spacing_Width / 2 + _D / 2) * 1000).ToString();



            double _LPr = (((_N - 1) * Spacing_Len + _D) * 1000);
            double _LPC = (((_N - 1) * Spacing_Len) * 1000 + 2 * _L2);
            double _BPC = (Spacing_Len + _D) * 1000 + _L2;



            double _BPr = (Spacing_Width * 1000 );



            txt_LPr.Text = _LPr.ToString();
            txt_LPC.Text = _LPC.ToString();
            txt_BPC.Text = _BPC.ToString();
            txt_BPr.Text = _BPr.ToString();


        }

        
    }
}
