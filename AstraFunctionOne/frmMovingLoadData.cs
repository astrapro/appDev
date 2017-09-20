using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.DataStructure;
using AstraInterface.Interface;

namespace AstraFunctionOne
{
    public partial class frmMovingLoadData : Form
    {
        IApplication iapp;
        LiveLoadCollections Loads { get; set; }

        public string Live_Load_File { get; set; }

        public frmMovingLoadData(IApplication app)
        {
            InitializeComponent();
            this.iapp = app;
            Live_Load_File = iapp.LL_TXT_Path;
            Read_Loads();
        }

        public frmMovingLoadData(IApplication app, string live_load_file)
        {
            
            InitializeComponent();
            this.iapp = app;
            Live_Load_File = live_load_file;
            Read_Loads(live_load_file);
        }

        void Read_Loads()
        {
            dgv.Rows.Clear();
            Loads = iapp.LiveLoads;
            foreach (var item in Loads)
            {
                dgv.Rows.Add(item.Data);
            }
        }

        void Read_Loads(string live_load_file)
        {
            dgv.Rows.Clear();
            Loads = new LiveLoadCollections(live_load_file);
            foreach (var item in Loads)
            {
                dgv.Rows.Add(item.Data);
            }
        }

        void Select_Loads(int indx)
        {
            try
            {
                txt_type.Text = Loads[indx].TypeNo.Replace("TYPE ","");
                txt_lm.Text = Loads[indx].Code;
                txt_imf.Text = Loads[indx].ImpactFactor.ToString("f3");
                txt_distances.Text = Loads[indx].Distances.ToString().Trim().Replace(" ",",");
                txt_load_width.Text = Loads[indx].LoadWidth.ToString("f3");
                txt_loads.Text = Loads[indx].Loads.ToString().Trim().Replace(" ", ",");

                for(int i = 0; i < Loads[indx].Loads.Count;i++)
                {
                    if (i == 0)
                    {
                        dgv_inputs.Rows.Clear();
                        dgv_inputs.Rows.Add((i + 1), Loads[indx].Loads[i], "");
                        dgv_inputs[2, 0].ReadOnly = true;
                        dgv_inputs[2, 0].Style.BackColor = Color.DarkKhaki;
                        
                    }
                    else
                    {
                        dgv_inputs.Rows.Add((i + 1), Loads[indx].Loads[i], Loads[indx].Distances[i - 1]);
                        //dgv_inputs[1, 0].ReadOnly = true;
                        //dgv_inputs[1, 0].Style.BackColor = Color.Violet;
                    }
                    txt_total_load.Text = Loads[indx].Loads.SUM.ToString("f3");
                    txt_total_dist.Text = Loads[indx].Distances.SUM.ToString("f3");
                    Set_Data_Grid();

                }

            }
            catch (Exception ex) { }
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            LoadData ld = new LoadData();


            //dgv.Rows.Add("AAAAAAAAAAAAAAA\n\rAAAAAAAAAAAAAAA\n\rAAAAAAAAAAAAAAA\n\rAAAAAAAAAAAAAAA\n\r");

            //dgv.Rows[0].Height = 100;

            ld.TypeNo = "TYPE " + txt_type.Text;

            //ld.TypeNo = "TYPE " + (dgv.RowCount + 1);
            ld.Code = txt_lm.Text;
            ld.Distances = new MyList(txt_distances.Text.Replace(",", " "), ' ');
            ld.Loads = new MyList(txt_loads.Text.Replace(",", " "), ' ');
            ld.LoadWidth = MyList.StringToDouble(txt_load_width.Text, 0.0);
            ld.ImpactFactor = MyList.StringToDouble(txt_imf.Text, 1.0);

            if (btn.Name == btn_save.Name)
            {
                try
                {
                    Loads.Save_LL_TXT(System.IO.Path.GetDirectoryName(Live_Load_File), true);
                    //Loads.Save_LL_TXT(System.IO.Path.GetDirectoryName(iapp.LL_TXT_Path), true);
                    //Loads.Save_LL_TXT(Application.StartupPath, true);
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex) { }
            }
            else if (btn.Name == btn_cancel.Name)
            {
                DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else if (btn.Name == btn_restore.Name)
            {
                try
                {
                    if (MessageBox.Show("Do you want to restore default Moving Load Data ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        iapp.Write_Default_LiveLoad_Data();
                        Read_Loads();
                    }
                    
                }
                catch (Exception ex) { }
            }
            else if (ld.Distances.Count != (ld.Loads.Count - 1))
            {
                string s = string.Format("Number of Distances = {0}    and     Number of Loads = {1}", ld.Distances.Count, ld.Loads.Count);
                MessageBox.Show(this, s + "\n\nNumber of Distances must be equal to " + (ld.Loads.Count - 1) + " (Number of Loads - 1).", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (btn.Name == btn_new.Name)
            {
                txt_type.Text = (dgv.RowCount + 1).ToString();
                ld.TypeNo = "TYPE " + txt_type.Text;
                Loads.Add(ld);
                dgv.Rows.Add(ld.Data);
            }
            else if (btn.Name == btn_update.Name)
            {
                try
                {
                    Loads[dgv.CurrentCell.RowIndex] = ld;
                    dgv[0, dgv.CurrentCell.RowIndex].Value = ld.Data;

                    Select_Loads(dgv.CurrentCell.RowIndex);
                }
                catch (Exception ex) { }
            }
            else if (btn.Name == btn_delete.Name)
            {
                try
                {
                    Loads.RemoveAt(dgv.CurrentCell.RowIndex);
                    dgv.Rows.RemoveAt(dgv.CurrentCell.RowIndex);

                    //dgv.Rows[0].Selected = true;
                }
                catch (Exception ex) { }
            }
        }

        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox TB = (TextBox)e.Control;
            TB.Multiline = true;      
        }

        private void txt_distances_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MyList ml = new MyList(MyList.RemoveAllSpaces(txt_distances.Text), ',');
                txt_total_dist.Text = ml.SUM.ToString("f3");
                txt_tot_dis.Text = ml.Count.ToString("f0");

                ml = new MyList(MyList.RemoveAllSpaces(txt_loads.Text), ',');
                //txt_total_dist.Text = ml.SUM.ToString("f3");
                txt_tot_ld.Text = ml.Count.ToString("f0");
                
            }
            catch (Exception ex) { }
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            Select_Loads(dgv.CurrentCell.RowIndex);
            //Set_Data_Grid();
        }

        private void dgv_inputs_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Set_Data_Grid();
        }

        private void btn_def_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;


            if (btn.Name == btn_def_add.Name)
            {
                dgv_inputs.Rows.Add("", txt_inp_load.Text, txt_inp_distance.Text);
            }
            else if (btn.Name == btn_def_insert.Name)
            {

                if (dgv_inputs.CurrentCell.RowIndex > -1)
                {
                    if (dgv_inputs.CurrentCell.RowIndex == 0)
                        dgv_inputs.Rows.Insert(1, txt_inp_load.Text, txt_inp_distance.Text);
                    else
                        dgv_inputs.Rows.Insert( dgv_inputs.CurrentCell.RowIndex, "", txt_inp_load.Text, txt_inp_distance.Text);
                }
            }
            else if (btn.Name == btn_def_del.Name)
            {
                try
                {
                    if (dgv_inputs.CurrentCell.RowIndex > -1)
                        dgv_inputs.Rows.RemoveAt(dgv_inputs.CurrentCell.RowIndex);
                }
                catch (Exception ex) { }
            }
            Set_Data_Grid();
        }
        public void Set_Data_Grid()
        {
            double tot_ld = 0.0;
            double tot_dst = 0.0;
            try
            {

                if (dgv_inputs.RowCount > 1)
                {
                    txt_loads.Text = "";
                    txt_distances.Text = "";

                    for (int i = 0; i < dgv_inputs.RowCount - 1; i++)
                    {
                        dgv_inputs[0, i].Value = (i + 1);
                        dgv_inputs[0, i].ReadOnly = true;

                        if (i == 0)
                        {
                            dgv_inputs[2, 0].Style.BackColor = Color.DarkKhaki;
                            dgv_inputs[2, 0].Value = "";

                            txt_loads.Text += dgv_inputs[1, 0].Value.ToString();

                            //txt_distances.Text += dgv_inputs[1, i+1].Value.ToString();

                        }
                        else
                        {
                            dgv_inputs[2, i].Style.BackColor = Color.White;
                            txt_loads.Text += "," + dgv_inputs[1, i].Value;
                            if (i == 1)
                            {
                                txt_distances.Text += dgv_inputs[2, i].Value.ToString();

                            }
                            else
                            {
                                if (dgv_inputs[2, i].Value != null)
                                    txt_distances.Text += "," + dgv_inputs[2, i].Value.ToString();

                            }
                        }

                        tot_dst += MyList.StringToDouble(dgv_inputs[2, i].Value.ToString(), 0.0);
                        tot_ld += MyList.StringToDouble(dgv_inputs[1, i].Value.ToString(), 0.0);
                    }
                    txt_total_dist.Text = tot_dst.ToString("f3");
                    txt_total_load.Text = tot_ld.ToString("f3");
                }
            }
            catch (Exception ex) { }
        }


    }
}
