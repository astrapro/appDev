using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.Interface;

namespace AstraInterface.DataStructure
{
    public class Save_FormRecord : List<Control>
    {
        public Hashtable Controls { get; set; }
        public static IApplication iApp;

        string UC_NAME = "";

        List<string> ControlNames = new List<string>();
        public Save_FormRecord()
            : base()
        {
            Controls = new Hashtable();
            ControlNames = new List<string>();
        }
        public void Add(Control ctrl)
        {
            base.Add(ctrl);

            //Controls.Add(ctrl.Name, ctrl);
            if (UC_NAME != "")
            {
                Controls.Add(UC_NAME + "." + ctrl.Name, ctrl);
                ControlNames.Add(UC_NAME + "." + ctrl.Name);
            }
            else
            {
                Controls.Add(ctrl.Name, ctrl);
                ControlNames.Add(ctrl.Name);
            }
        }
        public void AddControls(Control ctrl)
        {
            //list_txt.Count
            //if(list_txt == null)
            //    list_txt = new List<Control>();

            UserControl uc = ctrl as UserControl;

            if(uc != null)
            {
                UC_NAME = uc.Name;
                //ctrl.Parent
            }

            for (int i = 0; i < ctrl.Controls.Count; i++)
            {
                try
                {
                    if (ctrl.Controls[i].Controls.Count > 0)
                        AddControls(ctrl.Controls[i]);

                    //if (ctrl.Controls[i].Name.StartsWith("txt") || ctrl.Controls[i].Name.StartsWith("cmb"))
                    //{
                    //    Add(ctrl.Controls[i]);
                    //}
                    ////Chiranjit [2012 12 18]
                    //if (ctrl.Controls[i].Name.StartsWith("dgv"))
                    //{
                    //    Add(ctrl.Controls[i]);
                    //}
                    ////Chiranjit [2013 06 25]
                    //if (ctrl.Controls[i].Name.StartsWith("rbtn") || ctrl.Controls[i].Name.StartsWith("chk"))
                    //{
                    //    Add(ctrl.Controls[i]);
                    //}
                    ////Chiranjit [2013 06 25]
                    //if (ctrl.Controls[i].Name.StartsWith("rtb"))
                    //{
                    //    Add(ctrl.Controls[i]);
                    //}
                    ////Chiranjit [2013 06 25]
                    //if (ctrl.Controls[i].Name.ToLower().StartsWith("uc"))
                    //{
                    //    Add(ctrl.Controls[i]);
                    //}

                    //Chiranjit [2013 06 25]
                    if ((ctrl.Controls[i] is TextBox ) ||
                        (ctrl.Controls[i] is RichTextBox) ||
                        (ctrl.Controls[i] is ComboBox) ||
                        (ctrl.Controls[i] is CheckBox ) ||
                        (ctrl.Controls[i] is DataGridView ) ||
                        (ctrl.Controls[i] is RadioButton ) ||
                        (ctrl.Controls[i] is UserControl ))
                    {
                        Add(ctrl.Controls[i]);
                    }

                }
                catch (Exception exx) { }
            }
        }
        public void AddControls(Form frm)
        {
            UC_NAME = frm.Name;
            for (int i = 0; i < frm.Controls.Count; i++)
            {
                AddControls(frm.Controls[i]);
            }
        }

        public void AddControls(object obj)
        {
            Form frm = obj as Form;
            if (frm != null)
            {
                UC_NAME = frm.Name;
                for (int i = 0; i < frm.Controls.Count; i++)
                {
                    AddControls(frm.Controls[i]);
                }
            }
            else
            {
                Control ctrl = obj as Control;

                if (ctrl != null)
                {
                    AddControls(ctrl);
                }
            }
        }

        public void Clear()
        {
            base.Clear();
            Controls.Clear();
        }

        public static void Read_All_Data(object frm, string folder_path)
        {
            string User_Input_Data = Path.Combine(folder_path, "ASTRA_Data_Input.txt");

            string data_file = User_Input_Data;

            Form f = frm as Form;

            if (f == null)
            {
                Control ctrl = frm as Control;
                if (ctrl != null)
                {
                    data_file = Path.Combine(folder_path, ctrl.Text + ".apr");
                }
                if (!File.Exists(data_file))
                    data_file = User_Input_Data;
            }
            else
            {
                data_file = Path.Combine(folder_path, f.Name + ".apr");
                if (!File.Exists(data_file))
                    data_file = User_Input_Data;
            }


            data_file = Path.Combine(folder_path, "Process\\Design.sys");


            if (!File.Exists(data_file)) return;


            Read_All_Data(frm, data_file, true);
        }

        public static void Read_All_Data(object frm, string User_Input_Data, bool IsFile)
        {

            //string User_Input_Data = Path.Combine(folder_path, "ASTRA_Data_Input.txt");

            string data_file = User_Input_Data;

            if (!File.Exists(data_file)) return;


            List<string> file_content = new List<string>(File.ReadAllLines(data_file));

            bool IsRun_ProgressBar = file_content.Count > 9999;

            

            MyList mlist = null;
            string kStr = "";


            Save_FormRecord rec = new Save_FormRecord();

            rec.Clear();
            rec.AddControls(frm);
            //rec.ControlNames
            if (IsRun_ProgressBar)
            {
                if (iApp != null)
                {
                    iApp.Progress_Works.Clear();
                    iApp.Progress_ON("Reading Previous Data...");
                }
            }
            for (int i = 0; i < file_content.Count; i++)
            {
                try
                {
                    kStr = MyList.RemoveAllSpaces(file_content[i].Trim());
                    if(i == 0)
                    {
                        mlist = new MyList(kStr, ':');

                        if(mlist.Count == 3)
                        {
                            if (mlist[2].Trim() == eDesignStandard.IndianStandard.ToString())
                            {
                                iApp.DesignStandard = eDesignStandard.IndianStandard;
                                iApp.LiveLoads = new LiveLoadCollections(iApp.LL_TXT_Path);
                            }
                            if (mlist[2].Trim() == eDesignStandard.BritishStandard.ToString())
                            {
                                iApp.DesignStandard = eDesignStandard.BritishStandard;
                                iApp.LiveLoads = new LiveLoadCollections(iApp.LL_TXT_Path);
                            }
                            continue;
                        }

                    }

                    mlist = new MyList(kStr, '=');
                    if (kStr.Contains("---")) continue;
                    //if (i >= 163)
                    //    mlist = new MyList(kStr, '=');

                    if (mlist.Count == 2)
                    {
                        try
                        {
                            Control c = rec.Controls[mlist.StringList[0]] as Control;
                            if (c != null)
                            {
                                //if (c.Name.StartsWith("txt"))
                                //    c.Text = mlist.StringList[1];
                                if (c.Name.StartsWith("cmb") || c is ComboBox)
                                {
                                    ComboBox cmb = (c as ComboBox);

                                    int row = mlist.GetInt(1);
                                    cmb.Items.Clear();
                                    string sel_text = "";
                                    for (int j = 0; j < row; j++)
                                    {
                                        i++;
                                        if (j == 0)
                                        {
                                            sel_text = file_content[i];
                                            i++;
                                        }
                                        cmb.Items.Add(file_content[i]);
                                    }
                                    //cmb.SelectedItem = mlist.StringList[1];
                                    cmb.SelectedIndex = MyList.StringToInt(sel_text, -1);

                                }
                                else if (c.Name.StartsWith("dgv") || c is DataGridView)
                                {

                                    DataGridView dgv = c as DataGridView;
                                    int row = mlist.GetInt(1);
                                    dgv.Rows.Clear();
                                    for (int j = 0; j < row; j++)
                                    {
                                        i++;
                                        kStr = MyList.RemoveAllSpaces(file_content[i].Trim());
                                        mlist = new MyList(kStr, '$');
                                        dgv.Rows.Add(mlist.StringList.ToArray());

                                        //kStr = dgv[2, dgv.Rows.Count - 1].Value.ToString(); ;
                                    }
                                    MyList.Modified_Cell(dgv);
                                }
                                else if (c.Name.StartsWith("txt") || c is TextBox)
                                {

                                    TextBox txt = c as TextBox;
                                    int row = mlist.GetInt(1);
                                    txt.Text = "";
                                    List<string> ln = new List<string>();

                                    for (int j = 0; j < row; j++)
                                    {
                                        i++;
                                        ln.Add(file_content[i]);
                                    }
                                    txt.Lines = ln.ToArray();

                                }
                                else if (c.Name.StartsWith("rtb") || c is RichTextBox)
                                {

                                    RichTextBox txt = c as RichTextBox;
                                    int row = mlist.GetInt(1);
                                    txt.Text = "";
                                    List<string> ln = new List<string>();

                                    for (int j = 0; j < row; j++)
                                    {
                                        i++;
                                        ln.Add(file_content[i]);
                                    }
                                    txt.Lines = ln.ToArray();

                                }
                                else if (c.Name.StartsWith("rbtn") || c is RadioButton)
                                {
                                    //ComboBox cmb = (c as ComboBox);
                                    (c as RadioButton).Checked = mlist.StringList[1] == "true";
                                }
                                else if (c.Name.StartsWith("chk") || c is CheckBox)
                                {
                                    //ComboBox cmb = (c as ComboBox);
                                    (c as CheckBox).Checked = mlist.StringList[1] == "true";
                                }

                            }
                        }
                        catch (Exception ex) { }
                    }
                    if (IsRun_ProgressBar)
                    {
                        if (iApp != null)
                        {
                            iApp.SetProgressValue(i, file_content.Count);
                            //Chiranjit [2013 05 15]
                            if (iApp.Is_Progress_Cancel) break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("ERROR : " + kStr);
                }
                finally
                {
                    if (IsRun_ProgressBar)
                    {
                        if (iApp != null) iApp.Progress_OFF();
                    }
                }
            }
        }



        public static void Write_All_Data(object frm, string folder_path)
        {
            string User_Input_Data = Path.Combine(folder_path, "ASTRA_Data_Input.txt");


            Form fm = frm as Form;
            Control ctrl = frm as Control;


            string data_file = User_Input_Data;


            //if (fm == null)
            //{
            //    data_file = Path.Combine(folder_path, ctrl.Text + ".apr");
            //}
            //else
            //{
            //    data_file = Path.Combine(folder_path, ctrl.Name + ".apr");
            //}
            //Change for Restructuring Folders

            data_file = Path.Combine(folder_path, "Process");

            if (!Directory.Exists(data_file)) Directory.CreateDirectory(data_file);

            data_file = Path.Combine(data_file, "Design.sys");


            //if (!File.Exists(data_file)) return;

            Write_All_Data(frm, data_file, true);
        }
        //public static void Write_All_Data(Form frm, string User_Input_Data, bool is_file)
        public static void Write_All_Data(object obj, string User_Input_Data, bool is_file)
        {

            Form frm = obj as Form;


            #region Simple Section Data


            //string User_Input_Data = Path.Combine(folder_path, "ASTRA_Data_Input.txt");
            //string User_Input_Data = Path.Combine(folder_path, "ASTRA_Data_Input.ast");


            Save_FormRecord rec = new Save_FormRecord();

            rec.Clear();
            string project_name = "";
            bool Is_control = false;
            if (frm != null)
            {
                rec.AddControls(frm);
                project_name = frm.Name;
            }
            else
            {
                Control ctrl = obj as Control;
                if (ctrl != null)
                {
                    rec.AddControls(ctrl);
                    project_name = ctrl.Name;
                    Is_control = true;
                }
            }

            //rec
            //SaveRec.AddControls(tab_Post_Tension_Main_Girder);
          
            string str = "";
            List<string> file_content = new List<string>();
            if (iApp != null)
            {
                file_content.Add(string.Format("PROJECT NAME : {0} : {1}", project_name, iApp.DesignStandard));
            }
            file_content.Add(string.Format("ASTRA INPUT DATA CREATED AT " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
            file_content.Add(string.Format(""));
            file_content.Add(string.Format(""));
            //foreach (var item in rec)
            foreach (var ctrl in rec.ControlNames)
            {
                try
                {
                    var item = rec.Controls[ctrl] as Control;

                    if (item.Name.ToLower().StartsWith("dgv") || item is DataGridView)
                    {
                        DataGridView dgv = item as DataGridView;

                        file_content.Add(string.Format("{0} = {1}", ctrl, ((dgv.AllowUserToAddRows) ? (dgv.RowCount - 1) : dgv.RowCount)));
                        for (int r = 0; r < dgv.RowCount; r++)
                        {
                            str = "";
                            try
                            {
                                for (int c = 0; c < dgv.ColumnCount; c++)
                                {

                                    if (dgv[c, r].Value == null)
                                        dgv[c, r].Value = "";

                                    if (c == dgv.ColumnCount - 1)
                                        str += dgv[c, r].Value.ToString();
                                    else
                                        str += dgv[c, r].Value.ToString() + "$";
                                }
                                file_content.Add(string.Format("{0}", str));
                            }
                            catch (Exception exx) { }
                        }
                    }
                    else if (item.Name.ToLower().StartsWith("txt") || item is TextBox)
                    {
                        TextBox txt = item as TextBox;
                        file_content.Add(string.Format("{0} = {1}", ctrl, txt.Lines.Length));
                        file_content.AddRange(txt.Lines);
                        //for (int r = 0; r < txt.Lines.Length; r++)
                        //{
                        //    try
                        //    {
                        //        file_content.Add(string.Format("{0}", txt.Lines[r]));
                        //    }
                        //    catch (Exception exwa) { }
                        //}
                    }

                    else if (item.Name.ToLower().StartsWith("cmb") || item is ComboBox)
                    {
                        ComboBox txt = item as ComboBox;
                        file_content.Add(string.Format("{0} = {1}", ctrl, txt.Items.Count));
                        //file_content.AddRange(txt.Lines);
                        for (int r = 0; r < txt.Items.Count; r++)
                        {
                            try
                            {
                                if (r == 0)
                                {
                                    file_content.Add(string.Format("{0}", txt.SelectedIndex.ToString()));
                                }
                                file_content.Add(string.Format("{0}", txt.Items[r].ToString()));
                            }
                            catch (Exception exwa) { }
                        }
                    }
                    else if (item.Name.ToLower().StartsWith("rtb") || item is RichTextBox)
                    {
                        RichTextBox txt = item as RichTextBox;
                        file_content.Add(string.Format("{0} = {1}", ctrl, txt.Lines.Length));


                        file_content.AddRange(txt.Lines);

                        //for (int r = 0; r < txt.Lines.Length; r++)
                        //{
                        //    try
                        //    {
                        //        file_content.Add(string.Format("{0}", txt.Lines[r]));
                        //    }
                        //    catch (Exception exwa) { }
                        //}
                    }
                    else if (item.Name.ToLower().StartsWith("chk") || item is CheckBox)
                    {
                        CheckBox chk = item as CheckBox;
                        file_content.Add(string.Format("{0} = {1}", ctrl, chk.Checked.ToString().ToLower()));
                    }
                    else if (item.Name.ToLower().StartsWith("rbtn") || item is RadioButton)
                    {
                        RadioButton rbtn = item as RadioButton;
                        file_content.Add(string.Format("{0} = {1}", ctrl, rbtn.Checked.ToString().ToLower()));
                    }
                    else
                        file_content.Add(string.Format("{0} = {1}", ctrl, item.Text));
                }
                catch (Exception exxxs) { }
            }
            file_content.Add(string.Format("NEW DATA"));


            #endregion Simple Section Data

            //tab_Analysis_DL.Controls[

            try
            {
                File.WriteAllLines(User_Input_Data, file_content.ToArray());
            }
            catch(Exception exx)
            {
            }
        }

        public static List<string> Demo_Data { get; set; }

        public static void Set_Demo_Data(Form frm)
        {
            #region Simple Section Data
            //string User_Input_Data = Path.Combine(folder_path, "ASTRA_Data_Input.txt");
            //string User_Input_Data = Path.Combine(folder_path, "ASTRA_Data_Input.ast");

            Demo_Data = new List<string>();
            Save_FormRecord rec = new Save_FormRecord();

            rec.Clear();
            rec.AddControls(frm);

            //SaveRec.AddControls(tab_Post_Tension_Main_Girder);

            string str = "";
            if (iApp != null)
            {
                Demo_Data.Add(string.Format("PROJECT NAME : {0} : {1}", frm.Name, iApp.DesignStandard));
            }
            Demo_Data.Add(string.Format("ASTRA INPUT DATA CREATED AT " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
            Demo_Data.Add(string.Format(""));
            Demo_Data.Add(string.Format(""));
            foreach (var item in rec)
            {
                if (item.Name.ToLower().StartsWith("dgv"))
                {
                    DataGridView dgv = item as DataGridView;

                    Demo_Data.Add(string.Format("{0} = {1}", item.Name, dgv.RowCount));
                    for (int r = 0; r < dgv.RowCount; r++)
                    {
                        str = "";
                        try
                        {
                            for (int c = 0; c < dgv.ColumnCount; c++)
                            {
                                if (c == dgv.ColumnCount - 1)
                                    str += dgv[c, r].Value.ToString();
                                else
                                    str += dgv[c, r].Value.ToString() + "$";
                            }
                            Demo_Data.Add(string.Format("{0}", str));
                        }
                        catch (Exception exx) { }
                    }
                }
                else if (item.Name.ToLower().StartsWith("txt"))
                {
                    TextBox txt = item as TextBox;
                    Demo_Data.Add(string.Format("{0} = {1}", item.Name, txt.Lines.Length));
                    for (int r = 0; r < txt.Lines.Length; r++)
                    {
                        Demo_Data.Add(string.Format("{0}", txt.Lines[r]));
                    }
                }
                else
                    Demo_Data.Add(string.Format("{0} = {1}", item.Name, item.Text));
            }
            Demo_Data.Add(string.Format("NEW DATA"));


            #endregion Simple Section Data
        }
        public static bool Get_Demo_Data(Form frm)
        {
            if (Demo_Data == null) Demo_Data = new List<string>();

            if (Demo_Data.Count == 0) return false;

            //List<string> Demo_Data = new List<string>(File.ReadAllLines(data_file));


            MyList mlist = null;
            string kStr = "";


            Save_FormRecord rec = new Save_FormRecord();

            rec.Clear();
            rec.AddControls(frm);
            for (int i = 0; i < Demo_Data.Count; i++)
            {
                try
                {
                    kStr = MyList.RemoveAllSpaces(Demo_Data[i].Trim());
                    mlist = new MyList(kStr, '=');
                    if (kStr.Contains("---")) continue;
                    //if (i >= 163)
                    //    mlist = new MyList(kStr, '=');

                    if (mlist.Count == 2)
                    {
                        try
                        {
                            Control c = rec.Controls[mlist.StringList[0]] as Control;
                            if (c != null)
                            {
                                //if (c.Name.StartsWith("txt"))
                                //    c.Text = mlist.StringList[1];
                                if (c.Name.StartsWith("cmb"))
                                {
                                    //ComboBox cmb = (c as ComboBox);
                                    (c as ComboBox).SelectedItem = mlist.StringList[1];
                                }
                                else if (c.Name.StartsWith("dgv"))
                                {

                                    DataGridView dgv = c as DataGridView;
                                    int row = mlist.GetInt(1);
                                    dgv.Rows.Clear();

                                    if (dgv.AllowUserToAddRows) row--;
                                    for (int j = 0; j < row; j++)
                                    {
                                        i++;
                                        kStr = MyList.RemoveAllSpaces(Demo_Data[i].Trim());
                                        mlist = new MyList(kStr, '$');
                                        dgv.Rows.Add(mlist.StringList.ToArray());

                                    }
                                }
                                else if (c.Name.StartsWith("txt"))
                                {

                                    TextBox txt = c as TextBox;
                                    int row = mlist.GetInt(1);
                                    txt.Text = "";
                                    List<string> ln = new List<string>();

                                    for (int j = 0; j < row; j++)
                                    {
                                        i++;
                                        ln.Add(Demo_Data[i]);
                                    }
                                    txt.Lines = ln.ToArray();
                                }
                            }
                        }
                        catch (Exception ex) { }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR : " + kStr);
                }
                finally
                {
                    if (iApp != null) iApp.Progress_OFF();
                }
            }
            return true;
        }

    }

}
