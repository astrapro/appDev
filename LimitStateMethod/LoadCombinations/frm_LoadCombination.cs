using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AstraFunctionOne.ProcessList;
using AstraFunctionOne.BridgeDesign;
using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraInterface.TrussBridge;
using BridgeAnalysisDesign;
using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign.RCC_T_Girder;
using BridgeAnalysisDesign.PSC_I_Girder;
using BridgeAnalysisDesign.Composite;
using LimitStateMethod.RCC_T_Girder;
//using LimitStateMethod.LS_Progress;



namespace LimitStateMethod.LoadCombinations
{
    public partial class frm_LoadCombination : Form
    {
        IApplication iApp;

        DataGridView dgv_liveloads;
        DataGridView dgv_loads;

        public frm_LoadCombination(IApplication app, DataGridView liveloads, DataGridView loads)
        {
            InitializeComponent();

            iApp = app;
            dgv_liveloads = liveloads;
            dgv_loads = loads;
        }

        private void frm_LoadCombination_Load(object sender, EventArgs e)
        {
            LOAD_COMB_TEXT = new List<string>();
            Restore_Default_Loads();

            LONG_GIRDER_LL_TXT();

            LLC.Clear();

            LLC.AddRange(LoadData.GetLiveLoads(long_ll));

            for(int i = 0; i <  LLC.Count;i++)
            {
                lst_design_loads.Items.Add(LLC[i].Code);
                if (lst_default_loads.Items.Contains(LLC[i].Code))
                    lst_default_loads.Items.Remove(LLC[i].Code);
            }

            //dgv_loads_comb = dgv_loads;
            List<string> ll = new List<string>();
            List<string> lst_spcs = new List<string>();

            for (int i = 0; i < dgv_loads.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            string kStr = "";


            for (int i = 0; i < dgv_loads.Rows.Count; i++)
            {
                kStr = "";
                for (int c = 0; c < dgv_loads.ColumnCount; c++)
                {
                    kStr += dgv_loads[c, i].Value.ToString() + ",";
                }

                ll.Add(kStr);

            }
            LOAD_COMB_TEXT = ll;
            Filled_Type_LoadData(dgv_loads_comb);

            Set_Combination();
        }

        private void Restore_Default_Loads()
        {

            lst_default_loads.Items.Clear();
            for (int i = 0; i < iApp.LiveLoads.Count; i++)
            {
                var item = iApp.LiveLoads[i];
                lst_default_loads.Items.Add(item.Code);
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;


            List<int> def_indx = new List<int>();
            List<int> des_indx = new List<int>();

            int i = 0;

            for (i = 0; i < lst_default_loads.SelectedIndices.Count; i++)
            {
                def_indx.Add(lst_default_loads.SelectedIndices[i]);
            }

            for (i = 0; i < lst_design_loads.SelectedIndices.Count; i++)
            {
                des_indx.Add(lst_design_loads.SelectedIndices[i]);
            }

            if (btn.Name == btn_add.Name)
            {
                #region Add Data

                if (def_indx.Count > 0)
                {
                    def_indx.Sort();

                    for (i = 0; i < def_indx.Count; i++)
                    {
                        var item = lst_default_loads.Items[def_indx[i]];
                        if (!lst_design_loads.Items.Contains(item))
                            lst_design_loads.Items.Add(item);
                        else
                        {
                            MessageBox.Show(item + " load is already exist.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    def_indx.Reverse();

                    for (i = 0; i < def_indx.Count; i++)
                    {
                        lst_default_loads.Items.RemoveAt(def_indx[i]);
                    
                    }
                }

                #endregion Add Data
            }
            else if (btn.Name == btn_add_all.Name)
            {
                #region Add All Data
                lst_design_loads.Items.AddRange(lst_default_loads.Items);
                lst_default_loads.Items.Clear();
                #endregion Add Data
            }
            else if (btn.Name == btn_remove.Name)
            {
                #region Back Data
                //lst_default_loads.Items.Add(lst_design_loads.SelectedItem);
                if (des_indx.Count > 0)
                {
                    des_indx.Sort();

                    for (i = 0; i < des_indx.Count; i++)
                    {
                        //lst_default_loads.Items.Add(lst_design_loads.Items[des_indx[i]]);


                        var item = lst_design_loads.Items[des_indx[i]];
                        if (!lst_default_loads.Items.Contains(item))
                            lst_default_loads.Items.Add(item);
                        else
                        {
                            MessageBox.Show(item + " load is already exist.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                    }

                    des_indx.Reverse();

                    for (i = 0; i < des_indx.Count; i++)
                    {
                        lst_design_loads.Items.RemoveAt(des_indx[i]);
                    }
                }
                #endregion Back Data
            }
            else if (btn.Name == btn_remove_all.Name)
            {
                #region Back All Data 
                lst_default_loads.Items.AddRange(lst_design_loads.Items);
                lst_design_loads.Items.Clear();
                #endregion Back All Data
            }
            Set_Combination();
        }

        public List<string> Design_Loads 
        { 
            get
            {
                List<string> list = new List<string>();
                string load = "";
                int i = 0;
                list.Clear();
                int typ_no = 1;
                for (i = 0; i < LLC.Count; i++)
                {
                    var item = LLC[i];
                    load = item.Code;

                    list.Add(string.Format("TYPE {0}, {1}", typ_no++, load));
                    list.Add(string.Format("AXLE LOAD IN TONS , {0}", item.Loads.GetString(0).Replace(" ", ",")));
                    list.Add(string.Format("AXLE SPACING IN METRES, {0}", item.Distances.GetString(0).Replace(" ", ",")));
                    list.Add(string.Format("AXLE WIDTH IN METRES, {0:f3}", item.LoadWidth));
                    list.Add(string.Format("IMPACT FACTOR, {0}", item.ImpactFactor));
                    list.Add(string.Format(""));

                }
                return list;
            }
        }
        

        LiveLoadCollections LLC = new LiveLoadCollections();

        private void Set_Combination()
        {

            LiveLoadCollections LLD = new LiveLoadCollections();

            LLD.AddRange(iApp.LiveLoads);


            LoadData ld = new LoadData();


            LLC.Clear();

            cmb_Ana_load_type.Items.Clear();
            for (int i = 0; i < lst_design_loads.Items.Count; i++)
            {
                var item = lst_design_loads.Items[i].ToString();
                cmb_Ana_load_type.Items.Add("TYPE " + (i + 1) + " : " + item.ToString());

                ld = LLD.Get_LoadData(item);

                if (ld != null)
                {
                    ld.TypeNo = "TYPE " + (i + 1);
                    LLC.Add(ld);
                }
            }

            //cmb_Ana_load_type.Items.Clear();
            //for (int i = 0; i < lst_design_loads.Items.Count; i++)
            //{
            //    cmb_Ana_load_type.Items.Add("TYPE " + (i + 1) + " : " + lst_design_loads.Items[i].ToString());
            //}
            //return i;


            if (lst_design_loads.Items.Count > 0) txt_load_data.Text = "";
        }

        private void btn_restore_def_loads_Click(object sender, EventArgs e)
        {
            Restore_Default_Loads();
        }
        private void btn_def_mov_load_Click(object sender, EventArgs e)
        {
            iApp.Show_LL_Dialog();
            //iApp.LiveLoads.Fill_Combo(ref cmb_Ana_load_type);
            Restore_Default_Loads();

        }

        private void cmb_Ana_load_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (iApp.LiveLoads.Count > 0)
            //{
            //    txt_Ana_X.Text = iApp.LiveLoads[cmb_Ana_load_type.SelectedIndex].Distance.ToString("f4"); // Chiranjit [2013 05 28] Kolkata
            //    txt_Load_Impact.Text = iApp.LiveLoads[cmb_Ana_load_type.SelectedIndex].ImpactFactor.ToString("f3");
            //}
            MyList ml = new MyList(cmb_Ana_load_type.Text, ':');
            //iApp.LiveLoads.Get_Distance()

            try
            {
                txt_Ana_X.Text = iApp.LiveLoads.Get_Distance(ml.StringList[0]).ToString();
            }
            catch (Exception exx) { }
        }

        private void btn_add_load_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_inp_load.Rows.Add(cmb_Ana_load_type.Text, txt_Ana_X.Text, txt_Y.Text, txt_Z.Text, txt_XINCR.Text);
            }
            catch (Exception ex) { }
        }

        private void btn_Ana_live_load_remove_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_inp_load.Rows.RemoveAt(dgv_inp_load.CurrentRow.Index);
                //chk_ana_active_LL.Checked = (dgv_Ana_live_load.Rows.Count != 0);
            }
            catch (Exception ex) { }
        }
        private void btn_Ana_live_load_remove_all_Click(object sender, EventArgs e)
        {
            dgv_inp_load.Rows.Clear();
            //chk_ana_active_LL.Checked = (dgv_Ana_live_load.Rows.Count != 0);

        }

        private void lst_design_loads_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_design_loads.SelectedIndex > -1)
            {
                try
                {
                    var item = LLC[lst_design_loads.SelectedIndex];
                    txt_load_data.Lines = item.DataArray.ToArray();
                    //txt_load_data.Lines.
                    cmb_Ana_load_type.SelectedIndex = lst_design_loads.SelectedIndex;
           
                }
                catch (Exception ex) { }
             }

        }


        public List<string> LOAD_COMB_TEXT { get; set; }
        private void btn_combination_Click(object sender, EventArgs e)
        {
            List<string> lst_spcs = new List<string>();
            dgv_loads_comb.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_loads_comb.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            List<string> list = new List<string>();


            List<int> lanes = new List<int>();


            #region Long Girder
            list.Clear();


            int nos_lane;


            //d = MyList.StringToDouble(txt_deck_width.Text, 0.0);
            //lane_width = MyList.StringToDouble(txt_lane_width.Text, 0.0);

            nos_lane = dgv_inp_load.RowCount;

            for (i = 1; i <= nos_lane; i++)
            {
                lanes.Add(i);
            }

            string load = "LOAD " + 1;
            string x = "X";
            string z = "Z";

            LiveLoadCollections llc = new LiveLoadCollections();

            //llc.D

            #region Load 1

            for (int ld = 1; ld <= 1; ld++)
            {
                load = "LOAD " + (LOAD_COMB_TEXT.Count / 4 + 1);
                x = "X";
                z = "Z";
                for (i = 0; i < lanes.Count; i++)
                {
                    load += ","  + dgv_inp_load[0, i].Value.ToString().Split(':')[0];
                    x += "," + dgv_inp_load[1,i].Value.ToString();
                    z += "," + dgv_inp_load[3, i].Value.ToString();
                }

                list.Add(load);
                list.Add(x);
                list.Add(z);
                list.Add(string.Format(""));
            }

            LOAD_COMB_TEXT.AddRange(list.ToArray());
            #endregion Load 1

            #endregion


            LiveLoadCollections lc = new LiveLoadCollections();
            //lc.S

            list.Clear();
            list.AddRange(LOAD_COMB_TEXT.ToArray());
            for (i = 0; i < list.Count; i++)
            {
                dgv_loads_comb.Rows.Add(lst_spcs.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                try
                {
                    if (mlist[0] == "")
                    {
                        dgv_loads_comb.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                    }
                    else
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            if (list.Count < dgv_loads_comb.Rows.Count)
                                dgv_loads_comb[j, dgv_loads_comb.RowCount - list.Count + i].Value = mlist[j];
                            else
                                dgv_loads_comb[j, i].Value = mlist[j];
                        }
                    }
                }
                catch (Exception ex) { }
            }

            Set_All_Loads(dgv_loads_comb);

        }

        private void btn_remove_comb_Click(object sender, EventArgs e)
        {
            if (all_loads.Count == 0) return;
            all_loads.RemoveAt(Current_Load_Index);

            List<List<string>> lb = new List<List<string>>();

            List<string> lh = new List<string>();

            string kStr = "";
            for(int i = 0; i < LOAD_COMB_TEXT.Count; i++)
            {
                kStr = LOAD_COMB_TEXT[i];
                if(kStr.StartsWith("LOAD"))
                {
                    lh = new List<string>();
                    lb.Add(lh);
                }
                lh.Add(kStr);
            }




            lb.RemoveAt(Current_Load_Index);


            LOAD_COMB_TEXT.Clear();
            for (int i = 0; i < lb.Count; i++)
            {
                foreach (var item in lb[i])
                {
                    LOAD_COMB_TEXT.Add(item);
                }
            }

            Filled_Type_LoadData(dgv_loads_comb);

        }

        public void Filled_LoadData(DataGridView dgv_load)
        {
            
            List<string> list = Design_Loads;
            List<string> lst_spc = new List<string>();
            dgv_load.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_load.ColumnCount; i++)
            {
                lst_spc.Add("");
            }
            for (i = 0; i < list.Count; i++)
            {
                dgv_load.Rows.Add(lst_spc.ToArray());
            }

            MyList mlist = null;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                try
                {
                    if (list[i] == "")
                    {
                        dgv_load.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                    }
                    else
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            dgv_load[j, i].Value = mlist[j];
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }
        public void Filled_Type_LoadData(DataGridView dgv_fill)
        {
            List<string> lst_spcs = new List<string>();
            dgv_fill.Rows.Clear();
            int i = 0;
            for (i = 0; i < dgv_fill.ColumnCount; i++)
            {
                lst_spcs.Add("");
            }
            List<string> list = LOAD_COMB_TEXT;


            for (i = 0; i < list.Count; i++)
            {
                dgv_fill.Rows.Add(lst_spcs.ToArray());
            }

            MyList mlist = null;
            int load_no = 1;
            for (i = 0; i < list.Count; i++)
            {
                mlist = new MyList(list[i], ',');
                try
                {
                    if (mlist[0] == "")
                    {
                        dgv_fill.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;

                    }
                    else
                    {
                        for (int j = 0; j < mlist.Count; j++)
                        {
                            if(mlist[j].StartsWith("LOAD"))
                                dgv_fill[j, i].Value = "LOAD " + load_no++;
                            else
                                dgv_fill[j, i].Value = mlist[j];
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_save.Name)
            {
                Filled_Type_LoadData(dgv_loads);
                Filled_LoadData(dgv_liveloads);
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            }
            else if (btn.Name == btn_cancel.Name)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.No;
            }
            this.Close();

        }

        List<string> long_ll = new List<string>();
        List<string> long_ll_types = new List<string>();
        List<List<string>> all_loads = new List<List<string>>();

        public int Current_Load_Index { get; set; }
        public void LONG_GIRDER_LL_TXT()
        {

            int i = 0;
            int c = 0;
            string kStr = "";
            string txt = "";
            long_ll.Clear();
            long_ll_types.Clear();
            all_loads.Clear();
            List<string> long_ll_impact = new List<string>();


            bool flag = false;
            for (i = 0; i < dgv_liveloads.RowCount; i++)
            {
                txt = "";

                for (c = 0; c < dgv_liveloads.ColumnCount; c++)
                {
                    kStr = dgv_liveloads[c, i].Value.ToString();
                    if (flag)
                    {
                        long_ll_impact.Add(kStr);
                        flag = false;
                        txt = "";
                        kStr = "";
                        continue;
                    }
                    if (kStr.ToUpper().StartsWith("IMPACT"))
                    {
                        flag = true;
                        continue;
                    }
                    else if (kStr != "" && !kStr.StartsWith("AXLE"))
                    {
                        txt += kStr + " ";
                    }
                }

                if (txt != "" && txt.StartsWith("TYPE"))
                {
                    long_ll_types.Add(txt);
                }
                long_ll.Add(txt);
            }
          
            i = 0;

            List<string> list = new List<string>();

            List<string> def_load = new List<string>();
            List<double> def_x = new List<double>();
            List<double> def_z = new List<double>();


            int fl = 0;
            double xinc = MyList.StringToDouble(txt_XINCR.Text, 0.0);
            double imp_fact = 1.179;
            int count = 0;
            Set_All_Loads(dgv_loads);
          
            fl = 3;
        }

        private void Set_All_Loads(DataGridView dgv)
        {



            int i = 0;
            int c = 0;
            string kStr = "";
            string txt = "";



            List<string> list = new List<string>();

            List<string> def_load = new List<string>();
            List<double> def_x = new List<double>();
            List<double> def_z = new List<double>();


            int fl = 0;
            double xinc = MyList.StringToDouble(txt_XINCR.Text, 0.0);
            double imp_fact = 1.179;
            int count = 0;
            //long_ll.Clear();
            //long_ll_types.Clear();
            all_loads.Clear();


            for (i = 0; i < dgv.RowCount; i++)
            {
                txt = "";
                fl = 0;
                kStr = dgv[0, i].Value.ToString();

                if (kStr == "")
                {
                    list = new List<string>();
                    count++;
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        for (int f = 0; f < long_ll_types.Count; f++)
                        {
                            if (long_ll_types[f].StartsWith(def_load[j]))
                            {
                                //txt = string.Format("{0} {1:f3}", long_ll_types[f], long_ll_impact[f]);
                                break;
                            }
                        }
                        if (list.Contains(txt) == false)
                            list.Add(txt);
                    }
                    list.Add("LOAD GENERATION " + txt_XINCR.Text);
                    for (int j = 0; j < def_load.Count; j++)
                    {
                        txt = string.Format("{0} {1:f3} 0.0 {2:f3} XINC {3}", def_load[j], def_x[j], def_z[j], xinc);
                        list.Add(txt);
                    }
                    def_load.Clear();
                    def_x.Clear();
                    def_z.Clear();

                    all_loads.Add(list);
                }

                if (kStr != "" && (kStr.StartsWith("LOAD") || kStr.StartsWith("TOTAL")))
                {
                    fl = 1; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("X"))
                {
                    fl = 2; //continue;
                }
                else if (kStr != "" && kStr.StartsWith("Z"))
                {
                    fl = 3; //continue;
                }
                else
                    continue;
                for (c = 1; c < dgv.ColumnCount; c++)
                {
                    kStr = dgv[c, i].Value.ToString();

                    if (kStr == "") continue;
                    if (fl == 1)
                        def_load.Add(kStr);
                    else if (fl == 2)
                        def_x.Add(MyList.StringToDouble(kStr, 0.0));
                    else if (fl == 3)
                        def_z.Add(MyList.StringToDouble(kStr, 0.0));
                }
            }
            fl = 3;
        }

        private void dgv_loads_comb_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (all_loads.Count == 0) return;

            int load_index = (e.RowIndex / 4);
            Current_Load_Index = load_index;

            List<string> ll = new List<string>();


            ll.AddRange(all_loads[load_index].ToArray());

            MyList mlist= null;
            bool flag = false;
            for(int i = 0; i < ll.Count; i++)
            {
                var ss = ll[i];
                if (ss.StartsWith("LOAD"))
                {
                    flag = true;
                    dgv_inp_load.Rows.Clear();
                    continue;
                }

                if (flag)
                {
                    mlist = new MyList(ll[i], ' ');
                    dgv_inp_load.Rows.Add(mlist[0] + " "+  mlist[1] +  " : " + LLC[mlist.GetInt(1) -1].Code, mlist[2], mlist[3], mlist[4], mlist[6]);
                }

            }
        }

        private void btn_upd_comb_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();


            List<int> lanes = new List<int>();

            int nos_lane;

            nos_lane = dgv_inp_load.RowCount;

            for (int i = 1; i <= nos_lane; i++)
            {
                lanes.Add(i);
            }

            string load = "", x = "", z = "";
            //int i = 0;
            for (int ld = 1; ld <= 1; ld++)
            {
                load = "LOAD " + (LOAD_COMB_TEXT.Count / 4 + 1);
                x = "X";
                z = "Z";
                for (int i = 0; i < lanes.Count; i++)
                {
                    load += "," + dgv_inp_load[0, i].Value.ToString().Split(':')[0];
                    x += "," + dgv_inp_load[1, i].Value.ToString();
                    z += "," + dgv_inp_load[3, i].Value.ToString();
                }

                list.Add(load);
                list.Add(x);
                list.Add(z);
                list.Add(string.Format(""));
            }

            //LOAD_COMB_TEXT.AddRange(list.ToArray());



            //all_loads[Current_Load_Index] = list;


            List<List<string>> lb = new List<List<string>>();
            List<string> lh = new List<string>();
            string kStr = "";
            for (int i = 0; i < LOAD_COMB_TEXT.Count; i++)
            {
                kStr = LOAD_COMB_TEXT[i];
                if (kStr.StartsWith("LOAD"))
                {
                    lh = new List<string>();
                    lb.Add(lh);
                }
                lh.Add(kStr);
            }

            int sl = Current_Load_Index;
            lb[Current_Load_Index] = list;


            LOAD_COMB_TEXT.Clear();
            for (int i = 0; i < lb.Count; i++)
            {
                foreach (var item in lb[i])
                {
                    LOAD_COMB_TEXT.Add(item);
                }
            }
            Filled_Type_LoadData(dgv_loads_comb);

            dgv_loads_comb.Rows[sl * 4].Selected = true;

            dgv_loads_comb.FirstDisplayedScrollingRowIndex = sl * 4;


            Set_All_Loads(dgv_loads_comb);

        }

    }
}
