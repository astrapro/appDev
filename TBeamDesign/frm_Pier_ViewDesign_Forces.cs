using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AstraInterface.Interface;
using AstraInterface.DataStructure;
using BridgeAnalysisDesign.RCC_T_Girder;
//using AstraFunctionOne.BridgeDesign.SteelTruss;
//using AstraInterface.TrussBridge;

namespace BridgeAnalysisDesign
{
    public partial class frm_Pier_ViewDesign_Forces : Form
    {
        string analysis_rep = "";
        SupportReactionTable support_reactions = null;
        string Left_support = "";
        string Right_support = "";
        IApplication iApp = null;
        public frm_Pier_ViewDesign_Forces(IApplication app, string Analysis_Report_file, string left_support, string right_support)
        {
            InitializeComponent();
            iApp = app;
            analysis_rep = Analysis_Report_file;

            
            Left_support = left_support.Replace(",", " ");
            Right_support = right_support.Replace(",", " ");
        }

        private void frm_ViewDesign_Forces_Load(object sender, EventArgs e)
        {
            support_reactions = new SupportReactionTable(iApp, analysis_rep);
            try
            {
                Show_and_Save_Data();
            }
            catch (Exception ex) { }
        }

        void Show_and_Save_Data()
        {
            if (!File.Exists(analysis_rep)) return;
            string format = "{0,27} {1,10:f3} {2,10:f3} {3,10:f3}";
            List<string> list_arr = new List<string>(File.ReadAllLines(analysis_rep));
            list_arr.Add("");
            list_arr.Add("                   =====================================");
            list_arr.Add("                     DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                   =====================================");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add(string.Format(""));
            list_arr.Add(string.Format(format, "JOINT", "VERTICAL", "MAXIMUM", "MAXIMUM"));
            list_arr.Add(string.Format(format, "NOS", "REACTIONS", "MX", "MZ"));
            list_arr.Add(string.Format(format, "   ", "  (Ton)   ", "  (Ton-m)", "  (Ton-m)"));
            list_arr.Add("");
            SupportReaction sr = null;

            MyList mlist = new MyList(MyList.RemoveAllSpaces( Left_support), ' ');

            double tot_left_vert_reac = 0.0;
            double tot_right_vert_reac = 0.0;

            double tot_left_Mx = 0.0;
            double tot_left_Mz = 0.0;

            double tot_right_Mx = 0.0;
            double tot_right_Mz = 0.0;

            list_arr.Add("LEFT END");
            list_arr.Add("--------");
            for (int i = 0; i < mlist.Count; i++)
            {
                sr = support_reactions.Get_Data(mlist.GetInt(i));
                dgv_left_end_design_forces.Rows.Add(sr.JointNo, sr.Max_Reaction, sr.Max_Mx, sr.Max_Mz);

                tot_left_vert_reac += Math.Abs(sr.Max_Reaction); ;
                tot_left_Mx += sr.Max_Mx;
                tot_left_Mz += sr.Max_Mz;
                list_arr.Add(string.Format(format, sr.JointNo, Math.Abs(sr.Max_Reaction), sr.Max_Mx, sr.Max_Mz));
            }

            list_arr.Add("");
            txt_left_total_vert_reac.Text = tot_left_vert_reac.ToString("0.000");
            txt_left_total_Mx.Text = tot_left_Mx.ToString("0.000");
            txt_left_total_Mz.Text = tot_left_Mz.ToString("0.000");
            list_arr.Add(string.Format(format, "TOTAL", tot_left_vert_reac, tot_left_Mx, tot_left_Mz));
            list_arr.Add("");

             mlist = new MyList(MyList.RemoveAllSpaces(Right_support), ' ');
            list_arr.Add("RIGHT END");
            list_arr.Add("--------");
            for (int i = 0; i < mlist.Count; i++)
            {
                sr = support_reactions.Get_Data(mlist.GetInt(i));
                dgv_right_end_design_forces.Rows.Add(sr.JointNo, Math.Abs(sr.Max_Reaction), sr.Max_Mx, sr.Max_Mz);

                tot_right_vert_reac += Math.Abs(sr.Max_Reaction);
                tot_right_Mx += sr.Max_Mx;
                tot_right_Mz += sr.Max_Mz;
                list_arr.Add(string.Format(format, sr.JointNo, Math.Abs(sr.Max_Reaction), sr.Max_Mx, sr.Max_Mz));
            }
            list_arr.Add("");
            txt_right_total_vert_reac.Text = tot_right_vert_reac.ToString("0.000");
            txt_right_total_Mx.Text = tot_right_Mx.ToString("0.000");
            txt_right_total_Mz.Text = tot_right_Mz.ToString("0.000");
            list_arr.Add("");


            list_arr.Add(string.Format(format, "TOTAL", tot_right_vert_reac, tot_right_Mx, tot_right_Mz));
            list_arr.Add("");


            txt_both_ends_total.Text = (tot_left_vert_reac + tot_right_vert_reac).ToString("0.000");
            list_arr.Add("");
            list_arr.Add("BOTH ENDS TOTAL VERTICAL REACTION = " + txt_both_ends_total.Text + " Ton");

            txt_final_vert_reac.Text = (tot_right_vert_reac + tot_left_vert_reac).ToString("0.000");
            txt_final_vert_rec_kN.Text = ((tot_right_vert_reac + tot_left_vert_reac) * 10).ToString("0.000");


            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("FINAL DESIGN FORCES");
            list_arr.Add("-------------------");
            list_arr.Add("");
            list_arr.Add("TOTAL VERTICAL REACTION = " + txt_final_vert_reac.Text + " Ton" + "    =  " + txt_final_vert_rec_kN.Text + " kN");

            txt_final_Mx.Text = ((tot_left_Mx > tot_right_Mx) ? tot_left_Mx : tot_right_Mx).ToString("0.000");
            txt_max_Mx_kN.Text = MyList.StringToDouble(txt_final_Mx.Text, 0.0) * 10.0 + "";


            list_arr.Add("        MAXIMUM  MX     = " + txt_final_Mx.Text + " Ton-M" + "  =  " + txt_max_Mx_kN.Text + " kN-m");
            txt_final_Mz.Text = ((tot_left_Mz > tot_right_Mz) ? tot_left_Mz : tot_right_Mz).ToString("0.000");
            txt_max_Mz_kN.Text = MyList.StringToDouble(txt_final_Mz.Text, 0.0) * 10.0 + "";

            list_arr.Add("        MAXIMUM  MZ     = " + txt_final_Mz.Text + " Ton-M" + "  =  " + txt_max_Mz_kN.Text + " kN-m");
            list_arr.Add("");
            list_arr.Add("");
            list_arr.Add("                  ========================================");
            list_arr.Add("                  END OF DESIGN FORCES FOR RCC PIER DESIGN");
            list_arr.Add("                  ========================================");
            list_arr.Add("");
            




            File.WriteAllLines(analysis_rep, list_arr.ToArray());

            list_arr.Clear();
            list_arr.Add("W1=" + txt_final_vert_rec_kN.Text);
            list_arr.Add("Mx1=" + txt_max_Mx_kN.Text);
            list_arr.Add("Mz1=" + txt_max_Mz_kN.Text);
            string f_path = Path.Combine(Path.GetDirectoryName(analysis_rep), "Forces.fil");
            File.WriteAllLines(f_path, list_arr.ToArray());
            Environment.SetEnvironmentVariable("PIER", f_path);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }



    public class SupportReaction
    {
        public SupportReaction()
        {
            JointNo = 0;
            Max_Reaction_LoadCase = 0;
            Max_Reaction = 0;
            Max_Positive_Mx = 0;
            Max_Positive_Mx_LoadCase = 0;
            Max_Negative_Mx = 0;
            Max_Negative_Mx_LoadCase = 0;
            Max_Positive_Mz = 0;
            Max_Positive_Mz_LoadCase = 0;
            Max_Negative_Mz = 0;
            Max_Negative_Mz_LoadCase = 0;
        }

        #region Properties
        public int JointNo { get; set; }
        public int Max_Reaction_LoadCase { get; set; }
        public double Max_Reaction { get; set; }
        public double Max_Positive_Mx { get; set; }
        public int Max_Positive_Mx_LoadCase { get; set; }
        public double Max_Negative_Mx { get; set; }
        public int Max_Negative_Mx_LoadCase { get; set; }

        public double Max_Positive_Mz { get; set; }
        public int Max_Positive_Mz_LoadCase { get; set; }
        public double Max_Negative_Mz { get; set; }
        public int Max_Negative_Mz_LoadCase { get; set; }

        public double Max_Mx
        {
            get
            {
                return ((Math.Abs(Max_Negative_Mx) > Math.Abs(Max_Positive_Mx)) ? Math.Abs(Max_Negative_Mx) : Math.Abs(Max_Positive_Mx));
            }
        }
        public double Max_Mz
        {
            get
            {
                return ((Math.Abs(Max_Negative_Mz) > Math.Abs(Max_Positive_Mz)) ? Math.Abs(Max_Negative_Mz) : Math.Abs(Max_Positive_Mz));
            }
        }
        #endregion
    }
    public class SupportReactionTable : IList<SupportReaction>
    {
        List<SupportReaction> list = null;
        AstraInterface.Interface.IApplication iApp = null;
        public SupportReactionTable(AstraInterface.Interface.IApplication app ,string Analysis_file_name)
        {
            list = new List<SupportReaction>();
            iApp = app;
            ReadData_from_File(Analysis_file_name);
        }

        public SupportReaction Get_Data(int JointNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].JointNo == JointNo)
                {
                    return list[i];
                }
            }
            return null;
        }
        public void ReadData_from_File(string file_name)
        {
            if (!File.Exists(file_name)) return;
            string kStr = "";
            //StreamReader sr = new StreamReader(new FileStream(file_name, FileMode.Open));
            List<string> sr = new List<string>(File.ReadAllLines(file_name));
            try
            {
                //Sample Data
                //At Support=   18  For Load Case=   15  Maximum Reaction= 3.642E+002
                //At Support=   18  For Load Case=   36  Maximum +ve Mx= 1.779E+002  For Load Case=   36  Maximum -ve Mx=-1.565E+002
                //At Support=   18  For Load Case=   44  Maximum +ve Mz= 1.402E+002  For Load Case=    1  Maximum -ve Mz=-4.812E+001

                //At Support=   52  For Load Case=   34  Maximum Reaction= 4.549E+002
                //At Support=   52  For Load Case=   36  Maximum +ve Mx= 1.950E+002  For Load Case=   36  Maximum -ve Mx=-2.136E+002
                //At Support=   52  For Load Case=   33  Maximum +ve Mz= 2.471E+002  For Load Case=   14  Maximum -ve Mz=-7.351E+001
                MyList mlist = null;
                MyList mlist_2 = null;
                SupportReaction sup_rc = null;
                int ln_no = 0; // for 3 times Read line nos

                //iApp.Progress_ON("Reading support forces... ");
                int i = -1;
                bool flag = false;
                while (i != sr.Count - 1 && i != 0)
                {

                    if (i == -1)
                        i = sr.Count - 1;
                    kStr = MyList.RemoveAllSpaces(sr[i]);

                    if (kStr.ToUpper().Contains("SUMMARY OF MAXIMUM SUPPORT FORCES"))
                    {
                        flag = true;
                    }
                    if (!flag) i--;
                    else i++;

                    if (flag)
                    {
                        //kStr = sr[i];
                        if (kStr == "" || kStr.StartsWith("=====") || kStr.StartsWith("*****")) continue;

                        kStr = MyList.RemoveAllSpaces(kStr.ToUpper());
                        mlist = new MyList(kStr, '=');


                        if (mlist.StringList[0].StartsWith("AT SUPPORT"))
                        {
                            if (mlist.Count == 4)
                            {
                                kStr = kStr.Replace("AT SUPPORT=", " ");
                                kStr = kStr.Replace("FOR LOAD CASE=", " ");
                                kStr = kStr.Replace("MAXIMUM REACTION=", " ");

                                kStr = MyList.RemoveAllSpaces(kStr.ToUpper());

                                mlist = new MyList(kStr, ' ');

                                sup_rc = new SupportReaction();
                                sup_rc.JointNo = mlist.GetInt(0);
                                sup_rc.Max_Reaction_LoadCase = mlist.GetInt(1);
                                sup_rc.Max_Reaction = mlist.GetDouble(2);
                                ln_no = 1; continue;
                            }

                            //At Support=   18  For Load Case=   36  Maximum +ve Mx= 1.779E+002  For Load Case=   36  Maximum -ve Mx=-1.565E+002
                            if (ln_no == 1)
                            {
                                kStr = kStr.Replace("AT SUPPORT=", " ");
                                kStr = kStr.Replace("FOR LOAD CASE=", " ");
                                kStr = kStr.Replace("MAXIMUM +VE MX=", " ");
                                kStr = kStr.Replace("MAXIMUM -VE MX=", " ");

                                kStr = MyList.RemoveAllSpaces(kStr.ToUpper());

                                mlist = new MyList(kStr, ' ');

                                //sup_rc = new SupportReaction();
                                //sup_rc.JointNo = mlist.GetInt(0);
                                sup_rc.Max_Positive_Mx_LoadCase = mlist.GetInt(1);
                                sup_rc.Max_Positive_Mx = mlist.GetDouble(2);

                                sup_rc.Max_Negative_Mx_LoadCase = mlist.GetInt(3);
                                sup_rc.Max_Negative_Mx = mlist.GetDouble(4);

                                ln_no = 2; continue;
                            }
                            if (ln_no == 2)
                            {
                                kStr = kStr.Replace("AT SUPPORT=", " ");
                                kStr = kStr.Replace("FOR LOAD CASE=", " ");
                                kStr = kStr.Replace("MAXIMUM +VE MZ=", " ");
                                kStr = kStr.Replace("MAXIMUM -VE MZ=", " ");

                                kStr = MyList.RemoveAllSpaces(kStr.ToUpper());

                                mlist = new MyList(kStr, ' ');

                                //sup_rc = new SupportReaction();
                                //sup_rc.JointNo = mlist.GetInt(0);
                                sup_rc.Max_Positive_Mz_LoadCase = mlist.GetInt(1);
                                sup_rc.Max_Positive_Mz = mlist.GetDouble(2);

                                sup_rc.Max_Negative_Mz_LoadCase = mlist.GetInt(3);
                                sup_rc.Max_Negative_Mz = mlist.GetDouble(4);
                                ln_no = 0;

                                list.Add(sup_rc);
                            }
                            //iApp.SetProgressValue((double)sr.BaseStream.Position, (double)sr.BaseStream.Length);

                            //Chiranjit [2013 05 15]
                            //if (iApp.Is_Progress_Cancel) break;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                sr.Clear();
                sr = null;
            }
        }

        #region IList<SupportReaction> Members

        public int IndexOf(SupportReaction item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (item.JointNo == list[i].JointNo)
                    return i;
            }
            return -1;
        }

        public void Insert(int index, SupportReaction item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public SupportReaction this[int index]
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

        #region ICollection<SupportReaction> Members

        public void Add(SupportReaction item)
        {
            int indx = IndexOf(item);
            if (indx == -1)
            {
                list.Add(item);
            }
            else
                list[indx] = item;
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(SupportReaction item)
        {
            return (IndexOf(item) != -1);
        }

        public void CopyTo(SupportReaction[] array, int arrayIndex)
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

        public bool Remove(SupportReaction item)
        {
            int indx = IndexOf(item);
            RemoveAt(indx);

            return (indx != -1);
        }

        #endregion

        #region IEnumerable<SupportReaction> Members

        public IEnumerator<SupportReaction> GetEnumerator()
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
