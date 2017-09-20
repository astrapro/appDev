using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
namespace AstraFunctionOne
{
    public partial class frm_ProgressList : Form, IProgress
    {
        static frm_ProgressList fins = new frm_ProgressList();
        public static bool On = false;
        public bool Total_Time_On = false;

        public static DateTime dt = DateTime.Now;

        public static bool IsCancel = false;

        public static frm_ProgressList Instance
        {
            get
            {
                try
                {
                    if (fins == null && !On) 
                        fins = new frm_ProgressList();
                }
                catch (Exception ex) { }
                return fins;
            }

        }
        public static double Value
        {
            get
            {
                return fins.progressBar1.Value;
            }
            set
            {
                try
                {
                    //if ((B - A) < 100)
                    //    Thread.Sleep(12);
                    //else if ((B - A) < 300)
                    //    Thread.Sleep(9);
                    //if ((B - A) < 1000)
                    //    Thread.Sleep(3);
                    //else
                    //    Thread.Sleep(0);


                    if (!fins.Disposing && !fins.IsDisposed && Thread.CurrentThread.IsAlive && On && !IsCancel)
                    {
                        if (fins.lbl_percentage.InvokeRequired)
                        {
                            Control.CheckForIllegalCrossThreadCalls = false;

                            //System.Runtime.InteropServices.Marshal.ReleaseComObject(fins);
                            try
                            {
                                //fins.lbl_percentage.Invoke(fins.ctrl_text, fins.lbl_percentage, ((value >= 100.0) ? "100" : value.ToString("f3")) + "%");
                                fins.lbl_percentage.Invoke(fins.ctrl_text, fins.lbl_percentage, ((value >= 100.0) ? "100" : value.ToString("f0")) + "%"); //Chiranjit [2013 06 03]
                                fins.progressBar1.Invoke(fins.ctrl_text, fins.progressBar1, value);
                            }
                            catch (ThreadAbortException ex)
                            {
                                //MessageBox.Show("ASTRA ERROR MSG : ");
                                Thread.ResetAbort();
                            }
                        }
                    }

                }
                catch (ThreadAbortException ex22)
                {
                    //MessageBox.Show("ASTRA ERROR MSG : ");
                }
                catch (Exception ex)
                {
                }
            }
        }
        public static double A, B;
        public static int last_index;
        double last_A = 0.0;
        public static void SetValue(double a, double b)
        {
            try
            {
                A = (a + 1);
                B = b;
                Value = (((a + 1) / b) * 100.0);
                On = true;
            }
            catch (Exception ex) { }
        }
        static string Title = "";
        public static void ON(string title)
        {
            //OFF();
            //IsCancel = false;

            Title = title;
            //if (fins == null)
            //{
            //    fins = new frm_ProgressList();
            //    //fins = new frm_ProgressList();
            //    fins.Text = Title;
            //}
            //try
            //{
            //    thd.Abort();
            //}
            //catch (Exception ex) { }

            if (Work_List.Count > 0)
            {
                //if (Work_List.CurrentIndex >= Work_List.Count || !thd.IsAlive)
                if (thd != null)
                {


                    if (!thd.IsAlive)
                    {
                        IsCancel = false;
                        //thd = null;
                        thd = new Thread(new ThreadStart(fins.RunThread));
                        thd.SetApartmentState(ApartmentState.STA);
                        thd.Start();
                    }
                    else if (Work_List.CurrentIndex == -1)
                    {
                        //thd = null;
                        thd = new Thread(new ThreadStart(fins.RunThread));
                        thd.SetApartmentState(ApartmentState.STA);
                        thd.Start();
                    }
                    else
                    {
                        fins.Update();
                    }
                }
                else if (Work_List.CurrentIndex == -1)
                {
                    //thd = null;
                    thd = new Thread(new ThreadStart(fins.RunThread));
                    thd.SetApartmentState(ApartmentState.STA);
                    thd.Start();
                }
                else
                {
                    fins.Update();
                }
            }
            else
            {
                try
                {
                    //thd = null;
                    if (thd != null)
                    {
                        //thd = new Thread(new ThreadStart(fins.RunThread));
                        //thd.SetApartmentState(ApartmentState.STA);
                        thd.Start();
                    }
                    else
                    {
                        thd = new Thread(new ThreadStart(fins.RunThread));
                        thd.SetApartmentState(ApartmentState.STA);
                        thd.Start();
                    }
                }
                catch(Exception exx) {}
                //thd.Join();
            }
            //thd.Join();
        }
        public void Update()
        {

            if (Work_List != null)
            {
                if (Work_List.Count > 0)
                {
                    Work_List.Next();
                    //this.Size = new Size(550, 300);
                    //groupBox1.Visible = true;
                    //pnl_btn.Visible = true;


                    if (IsCancel && Work_List.CurrentIndex > 0)
                        Work_List[Work_List.CurrentIndex - 1].Status = eProgressStatus.Cancelled;


                    Work_List[Work_List.CurrentIndex].Status = eProgressStatus.Processing;
                    for (int i = 0; i < Work_List.Count; i++)
                    {
                        //dgv_list.Rows.Add(item.SerialNo, item.Work_Title, item.Status);
                        //if (IsCancel)
                        //    Work_List[i].Status = eProgressStatus.Canceling;

                        dgv_list[2, i].Value = Work_List[i].Status;

                        if (Work_List[i].Status == eProgressStatus.Done)
                            dgv_list.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                        else if (Work_List[i].Status == eProgressStatus.Cancelled)
                            dgv_list.Rows[i].DefaultCellStyle.BackColor = Color.LightCoral;



                        dgv_list.Rows[dgv_list.RowCount - 1].Selected = false;
                    }
                    //dgv_list.Rows[Work_List.CurrentIndex].Selected = true;


                    dgv_list.Rows[Work_List.CurrentIndex].DefaultCellStyle.BackColor = Color.Yellow;

                    //dgv_list.Rows[Work_List.CurrentIndex].
                    dgv_list.FirstDisplayedScrollingRowIndex = Work_List.CurrentIndex;
                
                    //if (Progress_Value_A == Progress_Value_B)
                    //{
                    //    Progress_Value_A = 0.0;
                    //    Progress_Value_B = 0.0;
                    //}
                }
            }

            this.Refresh();
        }
        public static void OFF()
        {
            try
            {
                //On = false;
                if (Work_List.Count > 0)
                {
                    //if (Work_List.CurrentIndex == Work_List.Count - 1 || (Instance.last_A == A))
                    if (Work_List.CurrentIndex == Work_List.Count - 1 || IsCancel)
                    {
                        Instance.Close();
                        if (thd.IsAlive)
                            thd.Abort();
                    }
                }
                else
                {
                    Instance.Close();
                    if (thd.IsAlive)
                        thd.Abort();
                }
            }
            catch (Exception ex) { }

        }

        public frm_ProgressList()
        {
            try
            {
                InitializeComponent();

                ctrl_text = new DChangeControlText(ChangeUser_Text);
                cl_frm = new DCloseForm(Close_Form);
                //grfx = this.CreateGraphics();
                progressBar1.Value = progressBar1.Maximum;
                lbl_percentage.Text = "99%";
                dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);

                Control.CheckForIllegalCrossThreadCalls = false;
            }
            catch (ThreadAbortException ex) { }
            catch (Exception ex1) { }
            //catch (Exception ex) { }
        }

        public static Thread thd = null;

        DChangeControlText ctrl_text = null;
        DCloseForm cl_frm = null;
        Graphics grfx = null;
        double _a = -11.0;

        //Chiranjit [2013 05 14]
        public static ProgressList Work_List;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (!timer2.Enabled)
                    timer2.Enabled = true;
            }
            catch (Exception ex) { }
        }

        private void AbortThread()
        {
            try
            {
                //On = false;
                //if (thd.IsAlive) ;
                //thd = thd.Abort;
            }
            catch (Exception ex) { }
        }

        void Close_Form(Form f)
        {
            try
            {
                f.Dispose();
            }
            catch (Exception ex) { }
        }
        void ChangeUser_Text(Object ctrl, object val)
        {
            try
            {
                if (!thd.IsAlive) return;
                if (ctrl is ProgressBar)
                {
                    ProgressBar p = ctrl as ProgressBar;
                    p.Value = (int)(double)val;
                }
                else if (ctrl is Label)
                {
                    Label l = ctrl as Label;
                    l.Text = (string)val;
                }
                else if (ctrl is Form)
                {
                    Form f = ctrl as Form;
                    f.Text = (string)val;
                }
            }
            catch (Exception ex) { }
        }
        void RunThread()
        {
            try
            {
                IsCancel = false;
                //Thread.Sleep(9);
                if (thd.IsAlive)
                {
                    fins = new frm_ProgressList();
                    fins.Text = Title;
                    if (fins.Close_Flag)
                        fins.Text = Title;
                }
                Thread.Sleep(9);
                if (thd.IsAlive)
                {
                    Thread.BeginCriticalRegion();
                    //Thread.ResetAbort();
                    //Control.CheckForIllegalCrossThreadCalls = false;
                    //Thread.Sleep(9);
                    if (!fins.InvokeRequired)
                    {
                        try
                        {

                            //Application.ThreadException += new ThreadExceptionEventHandler(Form1_UIThreadException);

                            //    // Set the unhandled exception mode to force all Windows Forms errors to go through 
                            //    // our handler.
                            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);


                            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException, true);
                            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                            Application.Run(fins);
                            fins.ShowDialog();
                            //Thread.Sleep(9);
                        }
                        catch(ThreadAbortException tae)
                        {
                            //Thread.ResetAbort();
                            //Thread.SpinWait(9);
                        }
                    }
                    Thread.EndCriticalRegion();
                    //fins.Show();
                }
            }
            catch (ThreadAbortException ex1) 
            { 
                System.Threading.Thread.ResetAbort();
                Console.WriteLine("Thread Exception.."); 
            }
            catch (Exception ex) { }
            finally
            {
                Console.WriteLine("Thread Exception.."); 

            }
        }
        private static void Form1_UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                //result = ShowThreadExceptionDialog("Windows Forms Error", t.Exception);
            }
            catch
            {
                //try
                //{
                //    MessageBox.Show("Fatal Windows Forms Error",
                //        "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                //}
                //finally
                //{
                //    Application.Exit();
                //}
            }

            // Exits the program when the user clicks Abort. 
            //if (result == DialogResult.Abort)
            //    Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (thd == null) return;
                if (!thd.IsAlive) return;
            }
            catch (ThreadAbortException ex) { }
            catch (Exception ex) { }
            //return;


            try
            {

                //double hours = (DateTime.Now - dt).Hours;
                //double minutes = (DateTime.Now - dt).Minutes;
                //double seconds = (DateTime.Now - dt).Seconds;

                ////string tm = ", tm : ";
                //string tm = "";
                ////tm += " " + ((hours == 0.0) ? "" : hours.ToString() + " h,") +
                ////      " " + ((minutes == 0.0) ? "" : minutes.ToString() + " m,") +
                ////      " " + ((seconds == 0.0) ? "" : seconds.ToString() + " s");
                //tm += " " + ((hours == 0.0) ? "" : hours.ToString() + " h,") +
                //    " " + ((minutes == 0.0) ? "" : minutes.ToString() + " m,") +
                //    " " + ((seconds == 0.0) ? "" : seconds.ToString() + " s");




                //tm = "" + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
                //lbl_total_time.Invoke(ctrl_text, lbl_total_time, tm);

                //double progrs_val = A;
                //if (thd != null)
                //{
                //    if (thd.Priority != ThreadPriority.Highest)
                //        thd.Priority = ThreadPriority.Highest;
                //}
                //if (progrs_val > 0)
                //{
                //    double total_estimate_time = (hours * 60 * 60 + minutes * 60 + seconds);
                //    seconds = (int)((total_estimate_time / (A)) * (B - progrs_val));

                //    hours = minutes = 0.0;
                //    if (seconds > 59)
                //    {
                //        minutes = (int)(seconds / 60);
                //        seconds = (int)(seconds % 60);
                //    }
                //    if (minutes > 59)
                //    {
                //        hours = (int)(minutes / 60);
                //        minutes = (int)(minutes % 60);
                //    }
                //    //tm += ", rem :";
                //    tm = "";
                //    tm += " " + ((hours == 0.0) ? "" : hours.ToString() + " h,") +
                //         " " + ((minutes == 0.0) ? "" : minutes.ToString() + " m,") +
                //         " " + ((seconds == 0.0) ? "" : seconds.ToString() + " s");
                //    tm = "" + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
                //    lbl_time_remain.Invoke(ctrl_text, lbl_time_remain, tm);

                //    if (Total_Time_On)
                //    {
                //        hours += DateTime.Now.Hour;
                //        minutes += DateTime.Now.Minute;
                //        seconds += DateTime.Now.Second;

                //        if (seconds > 59)
                //        {
                //            minutes++;
                //            seconds -= 60;
                //        }

                //        if (minutes > 59)
                //        {
                //            hours++;
                //            minutes -= 60;
                //        }

                //        tm = "END : " + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
                //        lbl_tr.Invoke(ctrl_text, lbl_tr, tm);

                //        //tm = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
                //        tm = "START : " + dt.Hour.ToString("00") + ":" + dt.Minute.ToString("00") + ":" + dt.Second.ToString("00");
                //        lbl_tm.Invoke(ctrl_text, lbl_tm, tm);
                //    }

                //}
                try
                {
                    if (last_A == A && (Work_List.Count == 0 || last_index == Work_List.CurrentIndex))
                    {
                        fins.Invoke(fins.ctrl_text, fins, Title + " " + A.ToString() + " out of " + B.ToString() + " Please wait...");
                       
                        OFF();
                        //Work_List.CurrentIndex
                    }
                    if (!fins.IsDisposed && !fins.Disposing)
                        fins.Invoke(fins.ctrl_text, fins, Title + " " + A.ToString() + " out of " + B.ToString() + " Please wait...");
                    last_A = A;
                    last_index = Work_List.CurrentIndex;
                }
                catch (ThreadAbortException ex) { }
                catch (Exception ex) { }
            }
            catch (ThreadAbortException ex) { }
            catch (Exception exp) { }
            //}
        }

        private void lbl_percentage_Click(object sender, EventArgs e)
        {
            try
            {
                Total_Time_On = !Total_Time_On;
                lbl_tm.Invoke(ctrl_text, lbl_tm, "Total Time :");
                lbl_tr.Invoke(ctrl_text, lbl_tr, "Time Remaining :");
            }
            catch (Exception ex) { }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            //if (A == B)
            try
            {
                //if (Work_List.Count > 0)
                //{
                //}
                //else
                //{
                //    if (A == B || _a == A)
                //    {
                //        this.Dispose();
                //    }
                //}


                if (A == B || _a == A)
                {
                    if (Work_List.CurrentIndex >= Work_List.Count - 1)
                        this.Dispose();

                    if (Value > 99)
                    {
                        //if (Work_List.Count > 0)
                        //    Work_List.Next();
                    }
                }
                _a = A;
            }
            catch (ThreadAbortException ex) { }
            catch (Exception ex) { }
        }

        private void frm_ProgressBar_MouseMove(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("");
            try
            {
                if (!timer2.Enabled)
                    timer2.Enabled = true;
                timer2.Interval = 786;
            }
            catch (Exception ex) { }
        }

        private void frm_ProgressList_Load(object sender, EventArgs e)
        {
            try
            {

                IsCancel = false;
                if (Work_List == null)
                {
                    Work_List = new ProgressList();
                    //IsCancel = false;
                }
                this.Size = new Size(490, 90);
                groupBox1.Visible = false;
                pnl_btn.Visible = false;

                if (Work_List != null)
                {
                    if (Work_List.Count > 0)
                    {
                        Work_List.Next();
                        this.Size = new Size(550, 300);
                        groupBox1.Visible = true;
                        pnl_btn.Visible = true;
                        Work_List[Work_List.CurrentIndex].Status = eProgressStatus.Processing;
                        foreach (var item in Work_List)
                        {
                            dgv_list.Rows.Add(item.SerialNo, item.Work_Title, item.Status);

                            if (item.Status == eProgressStatus.Done)
                                dgv_list.Rows[dgv_list.RowCount - 1].DefaultCellStyle.BackColor = Color.LightGreen;
                            dgv_list.Rows[dgv_list.RowCount - 1].Selected = false;
                        }
                        //dgv_list.Rows[Work_List.CurrentIndex].Selected = true;


                        dgv_list.Rows[Work_List.CurrentIndex].DefaultCellStyle.BackColor = Color.Yellow;

                        //dgv_list.Rows[Work_List.CurrentIndex].
                        dgv_list.FirstDisplayedScrollingRowIndex = Work_List.CurrentIndex;
                    }
                }
            }
            catch (ThreadAbortException ex1) { }
            catch (Exception ex) { }
            On = true;
            //Thread.Sleep(1000);
            timer1.Start();
            timer2.Start();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "Do you want to terminate the program ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                {

                    for (int i = Work_List.CurrentIndex; i < Work_List.Count; i++)
                    {
                        Work_List[i].Status = eProgressStatus.Canceling;
                    }
                    IsCancel = true;
                    //AbortThread();
                    //this.Close();
                }
            }
            catch (Exception ex) { }
        }

        public int Progress_Value
        {
            get
            {
                return progressBar1.Value;
            }
            set
            {
                progressBar1.Invoke(ctrl_text, value);
            }
        }

        public double Progress_Value_A
        {
            get
            {
                return A;
            }
            set
            {
                A = value;
            }
        }

        public double Progress_Value_B
        {
            get
            {
                return B;
            }
            set
            {
                B = value;
            }
        }

        public bool Close_Flag
        {
            get
            {
                return On;
            }
            set
            {
                On = value;
            }
        }
    }
}
