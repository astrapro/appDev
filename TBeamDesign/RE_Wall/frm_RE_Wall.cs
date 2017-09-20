using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using AstraInterface.Interface;
using AstraInterface.DataStructure;



namespace BridgeAnalysisDesign.RE_Wall
{
    public partial class frm_RE_Wall : Form
    {
        RE_Wall_Design re_des = null;
        IApplication iApp = null;
        public frm_RE_Wall(IApplication app)
        {
            InitializeComponent();
            iApp = app;
            user_path = iApp.LastDesignWorkingFolder;
        }

        public string user_path { get; set; }

        //const string Title = "DESIGN OF RE (Reinforced Earth) WALL";
        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF RE (Reinforced Earth) WALL [BS]";
                return "DESIGN OF RE (Reinforced Earth) WALL [IRC]";
            }
        }



        public string Working_Folder
        {
            get
            {
                // if (Directory.Exists(Path.Combine(user_path, Title)) == false)
                //    Directory.CreateDirectory(Path.Combine(user_path, Title));
                //return Path.Combine(user_path, Title);

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
                if (Directory.Exists(Path.Combine(Working_Folder, "DRAWINGS")) == false)
                    Directory.CreateDirectory(Path.Combine(Working_Folder, "DRAWINGS"));
                return Path.Combine(Working_Folder, "DRAWINGS");
            }
        }

        public void Set_Input_Data()
        {

            if (re_des == null)
                re_des = new RE_Wall_Design(iApp);
            re_des.Working_Folder = Working_Folder;

            re_des.H1 = MyList.StringToDouble(txt_RE_H.Text, 0.0);
            re_des.Hf = MyList.StringToDouble(txt_RE_Hf.Text, 0.0);
            re_des.Hc = MyList.StringToDouble(txt_RE_Hc.Text, 0.0);
            re_des.Ht = MyList.StringToDouble(txt_RE_Ht.Text, 0.0);
            re_des.E = MyList.StringToDouble(txt_RE_E.Text, 0.0);
            re_des.H2 = MyList.StringToDouble(txt_RE_H2.Text, 0.0);


            txt_RE_Hm.Text = re_des.Hm.ToString();

            re_des.F = MyList.StringToDouble(txt_RE_F.Text, 0.0);
            re_des.B = MyList.StringToDouble(txt_RE_B.Text, 0.0);
            re_des.w = MyList.StringToDouble(txt_RE_w.Text, 0.0);
            re_des.Bs = MyList.StringToDouble(txt_RE_Bs.Text, 0.0);
            re_des.L = MyList.StringToDouble(txt_RE_L.Text, 0.0);
            re_des.fn = MyList.StringToDouble(txt_RE_fn.Text, 0.0);

            re_des.q = MyList.StringToDouble(txt_RE_q.Text, 0.0);
            re_des.gama1_max = MyList.StringToDouble(txt_RE_gama1_max.Text, 0.0);
            re_des.gama1_min = MyList.StringToDouble(txt_RE_gama1_min.Text, 0.0);
            re_des.phi1 = MyList.StringToDouble(txt_RE_phi1.Text, 0.0);
            re_des.Cu = MyList.StringToDouble(txt_RE_Cu.Text, 0.0);

            re_des.gama2 = MyList.StringToDouble(txt_RE_gama2.Text, 0.0);
            re_des.phi2 = MyList.StringToDouble(txt_RE_phi2.Text, 0.0);

            re_des.gama_f = MyList.StringToDouble(txt_RE_gama_f.Text, 0.0);
            re_des.phi_f = MyList.StringToDouble(txt_RE_phi_f.Text, 0.0);
            re_des.C = MyList.StringToDouble(txt_RE_C.Text, 0.0);
            re_des.Dp = MyList.StringToDouble(txt_RE_Dp.Text, 0.0);
            re_des.Df = MyList.StringToDouble(txt_RE_Df.Text, 0.0);


            re_des.Sl = MyList.StringToDouble(txt_RE_Sl.Text, 0.0);
            re_des.Sc = txt_RE_Sc.Text;
            re_des.Sp = txt_RE_Sp.Text;

            re_des.Zi = 0.385;
            re_des.wi = MyList.StringToDouble(txt_RE_wi.Text, 0.0);
            re_des.del_i = MyList.StringToDouble(txt_RE_del_i.Text, 0.0);
            re_des.tot_layers = 10;
            re_des.Start_H1 = MyList.StringToDouble(txt_RE_start_H1.Text, 0.0);

            re_des.cf = MyList.StringToDouble(txt_RE_cf.Text, 0.0);
            re_des.fo = MyList.StringToDouble(txt_RE_fo.Text, 0.0);



            re_des.Strip_Properties = new System.Collections.Hashtable();
            Polymetric_Strip strps = null;
            try
            {
                for (int i = 0; i < dgv_tech_strip.Rows.Count - 1; i++)
                {
                    strps = new Polymetric_Strip(MyList.StringToInt(dgv_tech_strip[0, i].Value.ToString(), -1));
                    strps.Ultimate_Tensile_Strength = MyList.StringToDouble(dgv_tech_strip[1, i].Value.ToString(), 0.0);
                    strps.Width = MyList.StringToDouble(dgv_tech_strip[2, i].Value.ToString(), 0.0);
                    strps.Long_Term_Design_Strength = MyList.StringToDouble(dgv_tech_strip[3, i].Value.ToString(), 0.0);
                    strps.Long_Term_Design_Strength_Consider = MyList.StringToDouble(dgv_tech_strip[4, i].Value.ToString(), 0.0);
                    re_des.Strip_Properties.Add(strps.Type, strps);
                }
            }
            catch (Exception ex) { }


        }
        private void btn_RE_process_Click(object sender, EventArgs e)
        {
            try
            {

                if (iApp.Check_Demo_Version())
                {
                    Save_FormRecord.Get_Demo_Data(this);
                }

                Save_FormRecord.Write_All_Data(this, Working_Folder);


                Set_Input_Data();


                for (int i = 0; i < re_des.Layout_Sections.Count; i++)
                {
                    try
                    {
                        re_des.Layout_Sections[i].Embedment_Depth = MyList.StringToDouble(dgv_RE_height_embedment[1, i].Value.ToString(), 0.43);
                    }
                    catch (Exception ex) { }
                }
            
                //re_des.Calculate_Program();
                re_des.Loop_Program();


                MessageBox.Show(this, "Report file created in " + re_des.Report_File + "\n\n and\n\n" + re_des.Report_Table_File, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frm_RE_wall_report f = new frm_RE_wall_report(iApp, false);

                f.Owner = this;
                f.ShowDialog();
                if (f.Is_Details)
                {
                    if (File.Exists(re_des.Report_File))
                        iApp.View_Result(re_des.Report_File);
                }
                else
                {
                    if (File.Exists(re_des.Report_Table_File))
                        iApp.View_Result(re_des.Report_Table_File);
                }
            }
            catch (Exception ex) { }

        }

        private void frm_RE_Wall_Load(object sender, EventArgs e)
        {

            dgv_tech_strip.Rows.Add(1, 20, 47, 11.08, 10.00);
            dgv_tech_strip.Rows.Add(2, 37.5, 49, 20.77, 18.88);
            dgv_tech_strip.Rows.Add(3, 50.0, 49, 27.69, 25.17);
            dgv_tech_strip.Rows.Add(4, 65.0, 49, 33.85, 30.77);
            txt_RE_L.Text = "3.1";
            txt_RE_H.Text = "13.21";



            Set_Project_Name();


            //Chiranjit [2013 04 04]
            Save_FormRecord.Set_Demo_Data(this);
            iApp.Check_Demo_Version();

        }

        private void Open_Project()
        {


            #region Chiranjit Design Option

            try
            {
                //eDesignOption edp = iApp.Get_Design_Option(Title);
                //if (edp == eDesignOption.None)
                //{
                //    this.Close();
                //}
                //else if (edp == eDesignOption.Open_Design)
                //{
                //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
                iApp.Read_Form_Record(this, user_path);

                string chk_file = Path.Combine(user_path, "ASTRA_Data_Input.txt");

                txt_analysis_file.Text = chk_file;
                Set_Input_Data();
                //Chiranjit [2013 04 22]
                re_des.Read_File(chk_file);

                string kStr = Path.GetDirectoryName(chk_file);
                kStr = Path.GetFileName(kStr);
                //if (kStr == Title)
                //    user_path = Path.GetDirectoryName(re_des.Working_Folder);
                //else
                //    user_path = re_des.Working_Folder;


                kStr = "";
                dgv_RE_height_embedment.Rows.Clear();
                for (int i = 0; i < re_des.Total_Sections.Count; i++)
                {
                    //if (i == re_des.Total_Sections.Count - 1)
                    //    kStr += re_des.Total_Sections[i].ToString("f3");
                    //else
                    //    kStr += re_des.Total_Sections[i].ToString("f3") + ", ";

                    dgv_RE_height_embedment.Rows.Add(re_des.Total_Sections[i], re_des.Layout_Sections[i].Embedment_Depth);
                }
                //txt_RE_Heights.Text = kStr;


                cmb_section_height.Items.Clear();
                foreach (var item in re_des.Total_Sections)
                {
                    cmb_section_height.Items.Add(item);
                }
                if (cmb_section_height.Items.Count > 0)
                    cmb_section_height.SelectedIndex = 0;


                if (iApp.IsDemo)
                    MessageBox.Show("ASTRA USB Dongle not found at any port....\nOpening with default data......", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Data Loaded successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Input data file Error..");
            }
            #endregion Chiranjit Design Option

        }

        private void btn_RE_save_des_Click(object sender, EventArgs e)
        {

            try
            {

                if (iApp.Check_Demo_Version())
                {
                    Save_FormRecord.Get_Demo_Data(this);
                }
                Set_Input_Data();
                re_des.Loop_Program();
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.FileName = re_des.Report_File;
                    sfd.Filter = "All Report Files(*.txt)|*.txt";
                    if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                    {
                        try
                        {
                            File.Copy(re_des.Report_File, sfd.FileName, true);
                            File.Copy(re_des.Report_Table_File, Path.Combine(Path.GetDirectoryName(sfd.FileName), Path.GetFileName(re_des.Report_Table_File)), true);
                            MessageBox.Show(this, "Report file created in " + sfd.FileName, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            iApp.Save_Form_Record(this, Path.GetDirectoryName(sfd.FileName));
                            iApp.View_Result(sfd.FileName);
                        }
                        catch (Exception ex) { }
                    }
                }

            }
            catch (Exception ex) { }
        }

        private void btn_RE_open_des_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "All Report Files(*.txt)|*.txt";
                ofd.InitialDirectory = re_des.Working_Folder;
                if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    try
                    {
                        iApp.Read_Form_Record(this, Path.GetDirectoryName(ofd.FileName));
                        txt_analysis_file.Text = ofd.FileName;
                        Set_Input_Data();
                        //Chiranjit [2013 04 22]
                        re_des.Read_File(ofd.FileName);

                        string kStr = Path.GetDirectoryName(ofd.FileName);
                        kStr = Path.GetFileName(kStr);
                        if(kStr == Title)
                            user_path = Path.GetDirectoryName(re_des.Working_Folder);
                        else
                            user_path = re_des.Working_Folder;


                        kStr = "";
                        dgv_RE_height_embedment.Rows.Clear();
                        for (int i = 0; i < re_des.Total_Sections.Count; i++)
                        {
                            //if (i == re_des.Total_Sections.Count - 1)
                            //    kStr += re_des.Total_Sections[i].ToString("f3");
                            //else
                            //    kStr += re_des.Total_Sections[i].ToString("f3") + ", ";

                            dgv_RE_height_embedment.Rows.Add(re_des.Total_Sections[i], re_des.Layout_Sections[i].Embedment_Depth);
                        }
                        //txt_RE_Heights.Text = kStr;


                        cmb_section_height.Items.Clear();
                        foreach (var item in re_des.Total_Sections)
                        {
                            cmb_section_height.Items.Add(item);
                        }
                        if (cmb_section_height.Items.Count > 0)
                            cmb_section_height.SelectedIndex = 0;
                        MessageBox.Show(this, "Previous Design opened successfully. " + ofd.FileName, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //To do
                    }
                    catch (Exception ex) { }
                }
            }
        }

        private void btn_RE_report_Click(object sender, EventArgs e)
        {
            try
            {
                frm_RE_wall_report f = new frm_RE_wall_report(iApp, true);

                f.Owner = this;
                f.ShowDialog();
                if (f.Is_Details)
                {
                    if (File.Exists(re_des.Report_File))
                        iApp.View_Result(re_des.Report_File);
                }
                else
                {
                    if (File.Exists(re_des.Report_Table_File))
                        iApp.View_Result(re_des.Report_Table_File);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Report file is not created.", "ASTRA");
            }
        }

        private void txt_RE_H_TextChanged(object sender, EventArgs e)
        {
            txt_RE_Hf.Text = txt_RE_H.Text;

            TextBox txt = sender as TextBox;

            Set_Input_Data();

            string kStr = "";

            if (txt.Name == txt_RE_L.Name)
            {
                re_des.Set_Design_Input_Data(true);

            }
            else
            {
                re_des.Set_Design_Input_Data(false);
                dgv_RE_height_embedment.Rows.Clear();
                for (int i = 0; i < re_des.Total_Sections.Count; i++)
                {
                    if (i == re_des.Total_Sections.Count - 1)
                    {
                        dgv_RE_height_embedment.Rows.Add(re_des.Total_Sections[i], re_des.Layout_Sections[i].Embedment_Depth);
                        kStr += re_des.Total_Sections[i].ToString("f3");
                    }
                    else
                    {
                        dgv_RE_height_embedment.Rows.Add(re_des.Total_Sections[i], re_des.Layout_Sections[i].Embedment_Depth);
                        kStr += re_des.Total_Sections[i].ToString("f3") + ", ";
                    }
                }
                txt_RE_Heights.Text = kStr;
            }

            cmb_section_height.Items.Clear();
            foreach (var item in re_des.Total_Sections)
            {
                cmb_section_height.Items.Add(item);
            }
            if (cmb_section_height.Items.Count > 0)
                cmb_section_height.SelectedIndex = 0;
        }

        private void cmb_section_height_SelectedIndexChanged(object sender, EventArgs e)
        {
            Reinforcement_Layout_Section sec = re_des.Layout_Sections[cmb_section_height.SelectedIndex];
            if (sec != null)
            {
                dgv_layout_sections.Rows.Clear();
                for (int i = 0; i < sec.Layers.Count; i++)
                {
                    dgv_layout_sections.Rows.Add(sec.Layers[i].ToArray());
                }
            }
        }

        private void btn_chng_layout_Click(object sender, EventArgs e)
        {

            Reinforcement_Layout_Section sec = new Reinforcement_Layout_Section();
            Reinforcement_Layout lay = null;

            for (int i = 0; i < dgv_layout_sections.Rows.Count; i++)
            {
                lay = new Reinforcement_Layout(i + 1);

                lay.Number_Strips = MyList.StringToInt(dgv_layout_sections[1, i].Value.ToString(), -1);
                lay.Strip_Type = MyList.StringToInt(dgv_layout_sections[2, i].Value.ToString(), -1);
                lay.Strip_Strength = MyList.StringToDouble(dgv_layout_sections[3, i].Value.ToString(), 0.0);
                lay.Strip_Length = MyList.StringToDouble(dgv_layout_sections[4, i].Value.ToString(), 0.0);
                lay.z = MyList.StringToDouble(dgv_layout_sections[5, i].Value.ToString(), 0.0);
                lay.delta_h = MyList.StringToDouble(dgv_layout_sections[6, i].Value.ToString(), 0.0);
                lay.fn = MyList.StringToDouble(dgv_layout_sections[7, i].Value.ToString(), 0.0);
                sec.Layers.Add(lay);
            }
            re_des.Layout_Sections[cmb_section_height.SelectedIndex] = sec;

            MessageBox.Show("Data modified.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgv_layout_sections_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            int _type = 0;
            Polymetric_Strip pmst = null;
            for (int i = 0; i < dgv_layout_sections.Rows.Count; i++)
            {
                _type = MyList.StringToInt(dgv_layout_sections[2,i].Value.ToString(), 1);
                pmst = re_des.Strip_Properties[_type] as Polymetric_Strip;
                if (pmst != null)
                    dgv_layout_sections[3, i].Value = pmst.Ultimate_Tensile_Strength.ToString("f3");
            }
        }

        private void txt_RE_Heights_TextChanged(object sender, EventArgs e)
        {
            Set_Input_Data();
            MyList mlist = new MyList(txt_RE_Heights.Text, ',');
            

            
            re_des.Total_Sections.Clear();
            for (int i = 0; i < mlist.Count; i++)
            {
                try
                {
                    re_des.Total_Sections.Add(mlist.GetDouble(i));
                }
                catch (Exception ex) { }
            }


            re_des.Set_Design_Input_Data(true);
            string kStr = "";
            for (int i = 0; i < re_des.Layout_Sections.Count; i++)
            {
                try
                {
                    re_des.Layout_Sections[i].Embedment_Depth = MyList.StringToDouble(dgv_RE_height_embedment[1, i].Value.ToString(), 0.43);
                }
                catch (Exception ex) { }
            }
            
            cmb_section_height.Items.Clear();
            foreach (var item in re_des.Total_Sections)
            {
                cmb_section_height.Items.Add(item);
            }
            if (cmb_section_height.Items.Count > 0)
                cmb_section_height.SelectedIndex = 0;
        }

        private void rbtn_ana_create_analysis_file_CheckedChanged(object sender, EventArgs e)
        {
            grb_select_analysis.Enabled = rbtn_ana_select_analysis_file.Checked;
        }

        private void dgv_RE_height_embedment_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string kStr = "";
            for (int i = 0; i < dgv_RE_height_embedment.Rows.Count-1; i++)
            {
                try
                {

                    if (i == dgv_RE_height_embedment.Rows.Count - 2)
                    {
                        kStr += dgv_RE_height_embedment[0,i].Value.ToString();
                        txt_RE_Heights.Text = kStr;
                    }
                    else
                    {
                        kStr += dgv_RE_height_embedment[0, i].Value.ToString() + ", ";
                    }

                }
                catch (Exception ex) { }
            }

        }

        private void pictureBox4_DoubleClick(object sender, EventArgs e)
        {
            txt_RE_Heights.Visible = !txt_RE_Heights.Visible;
        }

       

        private void btn_drawing_GAD_LHS_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;


            if (btn.Name == btn_drawing_GAD_LHS.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "General Arrangement Drawings [LHS]"), "RE_WALL_GAD_LHS");
            }
            else if (btn.Name == btn_drawing_GAD_RHS.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "General Arrangement Drawings [RHS]"), "RE_WALL_GAD_RHS");
            }
            else if (btn.Name == btn_drawing_GAD_1.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "General Arrangement Drawings 1"), "RE_WALL_GAD_1");
            }
            else if (btn.Name == btn_drawing_GAD_2.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "General Arrangement Drawings 2"), "RE_WALL_GAD_2");
            }
            else if (btn.Name == btn_drawing_GAD_3.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "General Arrangement Drawings 3"), "RE_WALL_GAD_3");
            }
            else if (btn.Name == btn_drawing_GAD_4.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "General Arrangement Drawings 4"), "RE_WALL_GAD_4");
            }
            else if (btn.Name == btn_drawing_Nomenclature.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "NOMENCLATURE"), "RE_WALL_NOMENCLATURE");
            }
            else if (btn.Name == btn_drawing_structural_details.Name)
            {
                iApp.RunViewer(Path.Combine(Drawing_Folder, "Structural Details"), "RE_WALL_STRUCTURAL");
            }
        }


        #region Create Project / Open Project

        public void Create_Project()
        {
            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            string fname = Path.Combine(user_path, Project_Name + ".apr");

            int ty = (int)Project_Type;
            File.WriteAllText(fname, ty.ToString());
            user_path = Path.Combine(user_path, Project_Name);

            if (Directory.Exists(user_path))
            {
                switch (MessageBox.Show(Project_Name + " is already exist. Do you want overwrite ?",
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

            MessageBox.Show(Project_Name + " is Created.", "ASTRA", MessageBoxButtons.OK);
        }

        public void Set_Project_Name()
        {
            string dir = Path.Combine(iApp.LastDesignWorkingFolder, Title);

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

            txt_project_name.Text = prj_name;

        }

        private void btn_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_browse_design.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;
                    iApp.Read_Form_Record(this, frm.Example_Path);
                    //txt_project_name.Text = Path.GetFileName(frm.Example_Path);
                    //Open_Project();



                    #region Save As
                    if (frm.SaveAs_Path != "")
                    {

                        string src_path = user_path;
                        txt_project_name.Text = Path.GetFileName(frm.SaveAs_Path);
                        Create_Project();
                        string dest_path = user_path;
                        MyList.Folder_Copy(src_path, dest_path);
                    }
                    #endregion Save As

                    Open_Project();

                    txt_project_name.Text = Path.GetFileName(user_path);

                    Write_All_Data();
                }
            }
            else if (btn.Name == btn_new_design.Name)
            {
                //IsCreate_Data = true;
                Create_Project();
                Write_All_Data();
            }
            //Button_Enable_Disable();
        }

        private void Write_All_Data()
        {
            iApp.Save_Form_Record(this, user_path);
        }


        public void All_Button_Enable(bool flag)
        {
            //btn_create_data.Enabled = flag;
            //btn_process_analysis.Enabled = flag;
            //btn_create_data.Enabled = flag;
            //btn_process_analysis
        }
        public string Project_Name
        {
            get
            {
                return txt_project_name.Text;
            }
            set
            {
                txt_project_name.Text = value;
            }
        }
        eASTRADesignType Project_Type
        {
            get
            {
                return eASTRADesignType.RE_Wall;
            }
        }

        public void Delete_Folder(string folder)
        {
            MyList.Delete_Folder(folder);
        }

        #endregion Chiranjit [2016 09 07]


    }
}
