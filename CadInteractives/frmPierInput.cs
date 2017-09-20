using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using VectorDraw.Professional.Actions;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdPrimaries;

namespace CadInteractives
{
    public partial class frmPierInput : Form
    {
        vdDocument vdoc1
        {
            get
            {
                return vdScrollableControl1.BaseControl.ActiveDocument;
            }
        }

        public static double Inp1 = 0.0;
        public static double Inp11 = 0.0;
        public static double Inp2 = 0.0;
        public static double Inp3 = 0.0;

        public frmPierInput()
        {
            InitializeComponent();
            //this.opt = opt; 

        }
        public string SetText1_Tab1
        {
            get
            {
                return lbl_tab11.Text;
            }
            set
            {
                lbl_tab11.Text = value;
            }
        }
        public string SetText1_Tab11
        {
            get
            {
                return lbl_tab111.Text;
            }
            set
            {
                lbl_tab111.Text = value;
            }
        }
        public string SetText2_Tab1
        {
            get
            {
                return lbl_tab12.Text;
            }
            set
            {
                lbl_tab12.Text = value;
            }
        }
        public string SetText2_Tab11
        {
            get
            {
                return lbl_tab112.Text;
            }
            set
            {
                lbl_tab112.Text = value;
            }
        }
        public string SetText3_Tab1
        {
            get
            {
                return lbl_tab13.Text;
            }
            set
            {
                lbl_tab13.Text = value;
            }
        }
        public string SetText3_Tab11
        {
            get
            {
                return lbl_tab113.Text;
            }
            set
            {
                lbl_tab113.Text = value;
            }
        }


        public string SetText1_Tab2
        {
            get
            {
                return lbl_tab21.Text;
            }
            set
            {
                lbl_tab21.Text = value;
            }
        }
        public string SetText2_Tab2
        {
            get
            {
                return lbl_tab22.Text;
            }
            set
            {
                lbl_tab22.Text = value;
            }
        }


        public string SetText1_Tab3
        {
            get
            {
                return lbl_tab31.Text;
            }
            set
            {
                lbl_tab31.Text = value;
            }
        }
        public string SetText2_Tab3
        {
            get
            {
                return lbl_tab32.Text;
            }
            set
            {
                lbl_tab32.Text = value;
            }
        }


        public double InputValue1
        {
            get
            {
                try
                {
                    return Double.Parse(txt_tab1.Text);
                }
                catch (Exception ex) { }
                return 0.0;
            }
            set
            {
                txt_tab1.Text = value.ToString("0.0000");
            }
        }
        public double InputValue11
        {
            get
            {
                try
                {
                    return Double.Parse(txt_tab11.Text);
                }
                catch (Exception ex) { }
                return 0.0;
            }
            set
            {
                txt_tab11.Text = value.ToString("0.0000");
            }
        }
        public double InputValue2
        {
            get
            {
                try
                {
                    return Double.Parse(txt_tab2.Text);
                }
                catch (Exception ex) { }
                return 0.0;
                
            }
            set
            {
                txt_tab2.Text = value.ToString("0.0000");
            }
        }
        public double InputValue3
        {
            get
            {
                try
                {
                    return Double.Parse(txt_tab3.Text);
                }
                catch (Exception ex) { }
                return 0.0;
            }
            set
            {
                txt_tab3.Text = value.ToString("0.0000");
            }
        }

        public string DrawingPath { get { return Path.Combine(Application.StartupPath, "Interaction Diagrams"); } }
       
        string Chart2 = "", Chart3 = "";

        string temp_file { get { return Path.Combine(Application.StartupPath, "pier.tmp"); } }

        private void frmPierInput_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(DrawingPath);
            MyList mlist = null;

            try
            {
                if (!File.Exists(temp_file)) return;
                List<string> list = new List<string>(File.ReadAllLines(temp_file));

                string kStr = "";
                for (int i = 0; i < list.Count; i++)
                {
                    kStr = list[i];
                    mlist = new MyList(kStr, '$');
                    if (mlist.Count == 2)
                    {
                        if (mlist[0] == "SetText1_Tab1") SetText1_Tab1 = mlist[1];
                        else if (mlist[0] == "SetText2_Tab1") SetText2_Tab1 = mlist[1];
                        else if (mlist[0] == "SetText3_Tab1") SetText3_Tab1 = mlist[1];

                        else if (mlist[0] == "SetText1_Tab11") SetText1_Tab11 = mlist[1];
                        else if (mlist[0] == "SetText2_Tab11") SetText2_Tab11 = mlist[1];
                        else if (mlist[0] == "SetText3_Tab11") SetText3_Tab11 = mlist[1];

                        else if (mlist[0] == "SetText1_Tab2") SetText1_Tab2 = mlist[1];
                        else if (mlist[0] == "SetText2_Tab2") SetText2_Tab2 = mlist[1];

                        else if (mlist[0] == "SetText1_Tab3") SetText1_Tab3 = mlist[1];
                        else if (mlist[0] == "SetText2_Tab3") SetText2_Tab3 = mlist[1];

                        else if (mlist[0] == "fy1") fy1 = mlist.GetDouble(1);
                        else if (mlist[0] == "fy2") fy2 = mlist.GetDouble(1);
                        else if (mlist[0] == "fck1") fck1 = mlist.GetDouble(1);
                        else if (mlist[0] == "fck1") fck1 = mlist.GetDouble(1);
                        else if (mlist[0] == "b") b = mlist.GetDouble(1);
                        else if (mlist[0] == "D") D = mlist.GetDouble(1);
                        else if (mlist[0] == "d_dash") d_dash = mlist.GetDouble(1);
                        else if (mlist[0] == "fck2") fck2 = mlist.GetDouble(1);
                        else if (mlist[0] == "Pu_top") Pu_top = mlist.GetDouble(1);
                        else if (mlist[0] == "Muy") Muy = mlist.GetDouble(1);
                        else if (mlist[0] == "Mux") Mux = mlist.GetDouble(1);
                        else if (mlist[0] == "InputValue1") InputValue1 = mlist.GetDouble(1);
                        else if (mlist[0] == "InputValue2") InputValue2 = mlist.GetDouble(1);
                        else if (mlist[0] == "InputValue3") InputValue3 = mlist.GetDouble(1);
                    }
                }
            }
            catch (Exception ex) { }
            Calculate();
            int select_index = -1;

            if (Directory.Exists(DrawingPath))
            {
                foreach (var item in Directory.GetFiles(DrawingPath))
                {
                    if (Path.GetFileName(item).StartsWith("37")) Chart2 = item;
                    else if (Path.GetFileName(item).StartsWith("38")) Chart3 = (item);
                    else
                    {
                        lst_drawings1.Items.Add(Path.GetFileNameWithoutExtension(item));
                    }

                    mlist = new MyList(item.Replace("_", " ").Replace(".vdml", "").Replace("d'D", ""), '=');

                    double da = d_dash / D;
                    if (da < 0.05) da = 0.05;
                    else if (da > 0.05 && da <= 0.10) da = 0.10;
                    else if (da > 0.10 && da <= 0.15) da = 0.15;
                    else if (da > 0.15 && da <= 0.20) da = 0.20;
                    if (fy1 == 240) fy1 = 250;
                    try
                    {
                        if (fy1 == mlist.GetDouble(1) && (da.ToString("f2") == mlist[2]) && select_index == -1)
                        {
                            select_index = lst_drawings1.Items.Count - 1;
                        }
                    }
                    catch (Exception ex) { }

                }
            }
            if (select_index != -1)
            {
                lst_drawings1.SelectedIndex = select_index;
            }
            else
            {
                if (lst_drawings1.Items.Count > 0)
                {
                    lst_drawings1.SelectedIndex = 0;
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_Chart1();
        }

        private void Select_Chart1()
        {

            string filename = "";
            try
            {


                if (lst_drawings1.SelectedItems.Count > 0)
                    filename = lst_drawings1.Items[lst_drawings1.SelectedIndex].ToString();

                filename = Path.Combine(DrawingPath, filename) + ".vdml";
                if (!File.Exists(filename)) filename = Path.Combine(DrawingPath, Path.GetFileNameWithoutExtension(filename)) + ".dwg";
                if (!File.Exists(filename)) filename = Path.Combine(DrawingPath, Path.GetFileNameWithoutExtension(filename)) + ".vdcl";
                if (File.Exists(filename))
                {
                    vdoc1.Open(filename);
                    vdoc1.GlobalRenderProperties.AxisSize = 1000;
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vdoc1);

                }
            }
            catch (Exception ex) { }
        }
        private void btn_OK_Click(object sender, EventArgs e)
        {
            List<string> file_cont = new List<string>();
            file_cont.Add(string.Format("InputValue1${0}", InputValue1));
            file_cont.Add(string.Format("InputValue11${0}", InputValue11));
            file_cont.Add(string.Format("InputValue2${0}", InputValue2));
            file_cont.Add(string.Format("InputValue3${0}", InputValue3));
            File.WriteAllLines(temp_file, file_cont.ToArray());
            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Dispose();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {
                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                    case 1:
                        lst_drawings1.Visible = true;
                        Select_Chart1();
                        break;
                    case 2:
                        lst_drawings1.Visible = false;
                        if (File.Exists(Chart2))
                            vdoc1.Open(Chart2);
                        break;
                    case 3:
                        lst_drawings1.Visible = false;
                        if (File.Exists(Chart3))
                            vdoc1.Open(Chart3);
                        break;
                }
            }
            catch (Exception ex) { }
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vdoc1);
        }

        public double fy1 = 415.0;
        public double fy2 = 415.0;
        public double fck1 = 0, b = 0, D = 0, d_dash = 0, fck2 = 0, Pu_top = 0;
        public double Muy = 0, Mux = 0;

        private void txt_tab1_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }
        
        private void Calculate()
        {
            double inp_val1 = InputValue1;

            double Mux1 = inp_val1 * (fck1 * b * D * D);
            Mux1 = Mux1 / 1000000;

            double val4 = d_dash / b;
            double val5 = (Pu_top * 1000) / (fck2 * b * D);

            double Muy1 = inp_val1 * (fck1 * b * D * D);
            Muy1 = Muy1 / 10E5;
            inp_val1 = InputValue2;
            double Puz = inp_val1 * b * D;
            Puz = Puz / 1000.0;
            double val6 = Pu_top / Puz;
            double val7 = Mux / Mux1;
            double val8 = Muy / Muy1;
            double val9 = Pu_top / Puz;
            double val10 = Muy / Muy1;
            double val11 = Mux / Mux1;
            SetText1_Tab3 = string.Format("Refer to Interaction diagram chart for Pu / Puz = {0:f4} and Muy / Muy1 = {1:f4}", val9, val10);
        }
    }
    [Serializable]
    public class MyList
    {
        List<string> strList = null;
        char sp_char;
        public MyList(string s, char splitChar)
        {
            strList = new List<string>(s.Split(new char[] { splitChar }));
            sp_char = splitChar;

            for (int i = 0; i < strList.Count; i++)
            {
                strList[i] = strList[i].Trim().TrimEnd().TrimStart();
            }
        }
        public MyList(List<string> lstStr)
        {
            strList = new List<string>();

            strList = lstStr;
        }
        public string this[int index]
        {
            get { return strList[index]; }
            set { strList[index] = value; }
        }
        public List<string> StringList
        {
            get
            {
                return strList;
            }
        }
        public int GetInt(int index)
        {
            //try
            //{
            return int.Parse(strList[index]);
            //}
            //catch(Exception exx)
            //{
            //}
            //return -1;
        }
        public decimal GetDecimal(int index)
        {
            //try
            //{
            return decimal.Parse(strList[index]);
            //}
            //catch(Exception exx)
            //{
            //}
            //return -1;
        }
        public double GetDouble(int index)
        {
            //try
            //{
            return double.Parse(strList[index]);
            //}
            //catch(Exception exx)
            //{
            //}
            //return -1;
        }
        public static string RemoveAllSpaces(string s)
        {
            s = s.Replace('\t', ' ').TrimStart().TrimEnd().Trim();
            while (s.Contains("  "))
            {
                s = s.Replace("  ", " ");
            }
            return s;
        }
        public static double StringToDouble(string s, double defaultValue)
        {
            //if (s == null) return defaultValue;

            double d = 0.0d;
            try
            {
                s = s.Replace('\t', ' ').TrimStart().TrimEnd().Trim();
                while (s.Contains("  "))
                {
                    s = s.Replace("  ", " ");
                }
                d = double.Parse(s);
            }
            catch (Exception ex)
            {
                d = defaultValue;
            }
            return d;
        }
        public static decimal StringToDecimal(string s, decimal defaultValue)
        {
            decimal d = 0.0m;
            try
            {
                s = s.Replace('\t', ' ').TrimStart().TrimEnd().Trim();
                while (s.Contains("  "))
                {
                    s = s.Replace("  ", " ");
                }
                d = decimal.Parse(s);
            }
            catch (Exception ex)
            {
                d = defaultValue;
            }
            return d;
        }
        public static int StringToInt(string s, int defaultValue)
        {

            int i = 0;
            try
            {
                s = s.Replace('\t', ' ').TrimStart().TrimEnd().Trim();
                while (s.Contains("  "))
                {
                    s = s.Replace("  ", " ");
                }
                i = int.Parse(s);
            }
            catch (Exception ex)
            {
                i = defaultValue;
            }
            return i;
        }
        public int Count
        {
            get
            {
                return StringList.Count;
            }
        }

        public string GetString(int afterIndex)
        {
            string kStr = "";
            for (int i = afterIndex; i < strList.Count; i++)
            {
                kStr += strList[i] + sp_char;
            }
            return kStr;
        }
        public static string Get_Modified_Path(string full_path)
        {
            try
            {
                string tst1 = Path.GetFileName(full_path);
                string tst2 = Path.GetFileName(Path.GetDirectoryName(full_path));
                string tst3 = Path.GetPathRoot(full_path);
                if (tst2 != "")
                    full_path = tst3 + "....\\" + Path.Combine(tst2, tst1);
            }
            catch (Exception ex) { }
            return full_path;
        }
        public static string Get_Array_Text(List<int> list_int)
        {
            string kStr = "";
            int end_mbr_no, start_mbr_no;

            start_mbr_no = 0;
            end_mbr_no = 0;
            list_int.Sort();
            for (int i = 0; i < list_int.Count; i++)
            {
                if (i == 0)
                {
                    start_mbr_no = list_int[i];
                    end_mbr_no = list_int.Count == 1 ? start_mbr_no : start_mbr_no + 1;
                    continue;
                }
                if (end_mbr_no == list_int[i])
                    end_mbr_no++;
                else
                {
                    end_mbr_no = list_int[i - 1];

                    if (end_mbr_no != start_mbr_no)
                        kStr += "   " + start_mbr_no + " TO " + (end_mbr_no);
                    else
                        kStr += " " + start_mbr_no;

                    start_mbr_no = list_int[i];
                    end_mbr_no = start_mbr_no + 1;
                }
                if (i == list_int.Count - 1)
                {
                    end_mbr_no = list_int[i];
                }
            }
            if (end_mbr_no != start_mbr_no)
                kStr += "   " + start_mbr_no + " TO " + (end_mbr_no);
            else
                kStr += " " + start_mbr_no;
            //kStr += " " + start_mbr_no + " TO " + end_mbr_no;
            return RemoveAllSpaces(kStr);
        }
        public static List<int> Get_Array_Intiger(string list_text)
        {
            //11 12 15 TO 21 43 89 98, 45 43  

            List<int> list = new List<int>();
            string kStr = RemoveAllSpaces(list_text.ToUpper().Trim());


            try
            {
                kStr = kStr.Replace("T0", "TO");
                kStr = kStr.Replace("-", "TO");
                kStr = kStr.Replace(",", " ");
                kStr = RemoveAllSpaces(kStr.ToUpper().Trim());

                MyList mlist = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                int start = 0;
                int end = 0;

                for (int i = 0; i < mlist.Count; i++)
                {
                    try
                    {
                        start = mlist.GetInt(i);
                        if (i < mlist.Count - 1)
                        {
                            if (mlist.StringList[i + 1] == "TO")
                            {
                                //start = mlist.GetInt(i);
                                end = mlist.GetInt(i + 2);
                                for (int j = start; j <= end; j++) list.Add(j);
                                i += 2;
                            }
                            else
                                list.Add(start);
                        }
                        else
                            list.Add(start);
                    }
                    catch (Exception ex) { }
                }
                mlist = null;
            }
            catch (Exception ex) { }
            list.Sort();
            return list;
        }


        public override string ToString()
        {
            return GetString(0);
        }

        public static string GetSectionSize(string s)
        {

            string kStr = s;

            if (kStr.ToUpper().Contains("X")) return kStr;

            if (kStr.Length % 2 != 0)
            {
                int i = kStr.Length / 2 + 1;
                kStr = s.Substring(0, i) + "X" + s.Substring(i, s.Length - (i));
            }
            else
            {
                int i = kStr.Length / 2;
                kStr = s.Substring(0, i) + "X" + s.Substring(i, s.Length - (i));
            }
            return kStr;
        }

        public static double Convert_Degree_To_Radian(double degree)
        {
            return (degree * (Math.PI / 180.0));
        }
        public static double Convert_Radian_To_Degree(double radian)
        {
            return (radian * (180.0 / Math.PI));
        }

    }
}
