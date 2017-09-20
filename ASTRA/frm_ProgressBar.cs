using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace AstraFunctionOne
{
    public partial class frm_ProgressBar : Form
    {
        static frm_ProgressBar fins = new frm_ProgressBar();
        public static bool On = false;
        public bool Total_Time_On = false;

        public static DateTime dt = DateTime.Now;
        public static frm_ProgressBar Instance
        {
            get
            {
                if (fins == null)
                    fins = new frm_ProgressBar();
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

                    if (!fins.Disposing && !fins.IsDisposed)
                    {
                        fins.Invoke(fins.ctrl_text, fins.lbl_percentage, ((value >= 100.0)? "100" : value.ToString("f3")) + "%"); 
                        fins.Invoke(fins.ctrl_text, fins.progressBar1, value);
                    }
                    //if (!fins.Disposing && !fins.IsDisposed && thd.IsAlive)
                    //{
                    //    fins.Invoke(fins.ctrl_text, fins.lbl_percentage, value + "%");
                    //    fins.Invoke(fins.ctrl_text, fins.progressBar1, value);
                    //}
                }
                catch (Exception ex)
                {
                    //try
                    //{
                    //    thd.Abort();
                    //    thd = new Thread(new ThreadStart(fins.RunThread));
                    //    thd.Start();
                    //}
                    //catch (Exception ex1) { }
                }
                //else
                //    MessageBox.Show(fins.Disposing.ToString());
                    //}
                //catch (Exception xx)
                //{
                //    //OFF();
                //}
            }
        }
        public static double A, B;
        double last_A = 0.0;
        public static void SetValue(double a, double b)
        {
            //if (!On) return;
            //if (On) ON(Title);

            //On = fins.IsAccessible;

            //if (!On) ON(Title);
            A = (a + 1);
            B = b;

            Value = (((a + 1) / b) * 100.0);
            //Value = (int)(((a + 1) / b) * 100.0);
            //if (fins.InvokeRequired)
            //{
            //    fins.Invoke(fins.ctrl_text, fins, Title + " " + (a + 1).ToString() + " out of " + b.ToString());
            //}
            //if (!On)  ON(Title);
        }

        static string Title = "";
        public static void ON(string title)
        {
            //OFF();

            //On = true;
            Title = title;
            if (fins == null)
            {
                fins = new frm_ProgressBar();
            }
            try
            {
                thd.Abort();

            }
            catch (Exception ex) { }
            thd = null;
            thd = new Thread(new ThreadStart(fins.RunThread));
            thd.Start();
            //thd.Join();
        }
        public static void OFF()
        {
            try
            {
                //On = false;
                Instance.Close();
                if (thd.IsAlive)
                    thd.Abort();
            }
            catch (Exception ex) { }

        }

        public frm_ProgressBar()
        {
            InitializeComponent();
            ctrl_text = new DChangeControlText(ChangeUser_Text);
            cl_frm = new DCloseForm(Close_Form);
            grfx = this.CreateGraphics();
            progressBar1.Value = progressBar1.Maximum;
            lbl_percentage.Text = "99%";
            dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
        }

        public static Thread thd = null;

        DChangeControlText ctrl_text = null;
        DCloseForm cl_frm = null;
        Graphics grfx = null;


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (!timer2.Enabled) timer2.Enabled = true;
            }
            catch (Exception ex) { }
        }

        private void AbortThread()
        {
            try
            {
                On = false;
                if (thd.IsAlive) ;
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
                if (ctrl is ProgressBar)
                {
                    ProgressBar p = ctrl as ProgressBar;
                    p.Value = (int) (double)val;
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
            //progressBar1.Invoke(ctrl_text, progressBar1, 0);
            ////progressBar1.Value = 0;
            //for (int i = 0; i <= 100; i++)
            //{
            //    progressBar1.Invoke(ctrl_text, progressBar1, i);
            //    label1.Invoke(ctrl_text, label1, i + "%");
            //    //grfx.DrawLine(Pens.Red, new Point(progressBar1.Value * 4, 0), new Point(progressBar1.Value * 4, 400));
            //    Thread.Sleep(200);
            //}
            try
            {
                fins.Invoke(cl_frm, fins);
            }
            catch (Exception ex) { }
            finally
            {
                fins = new frm_ProgressBar();
                fins.Text = Title;
                fins.ShowDialog();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (thd == null) return;
                if (!thd.IsAlive)
                    return;
            }
            catch (Exception ex) { }
            //return;
            double hours = (DateTime.Now - dt).Hours;
            double minutes = (DateTime.Now - dt).Minutes;
            double seconds = (DateTime.Now - dt).Seconds;

            //string tm = ", tm : ";
            string tm = "";
            //tm += " " + ((hours == 0.0) ? "" : hours.ToString() + " h,") +
            //      " " + ((minutes == 0.0) ? "" : minutes.ToString() + " m,") +
            //      " " + ((seconds == 0.0) ? "" : seconds.ToString() + " s");
            tm += " " + ((hours == 0.0) ? "" : hours.ToString() + " h,") +
                " " + ((minutes == 0.0) ? "" : minutes.ToString() + " m,") +
                " " + ((seconds == 0.0) ? "" : seconds.ToString() + " s");




            tm = "" + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
            lbl_total_time.Invoke(ctrl_text, lbl_total_time, tm);

            double progrs_val = A;
            if (thd != null)
            {
                if (thd.Priority != ThreadPriority.Highest)
                    thd.Priority = ThreadPriority.Highest;
            }
            if (progrs_val > 0)
            {
                double total_estimate_time = (hours * 60 * 60 + minutes * 60 + seconds);
                seconds = (int)((total_estimate_time / (A)) * (B - progrs_val));

                hours = minutes = 0.0;
                if (seconds > 59)
                {
                    minutes = (int)(seconds / 60);
                    seconds = (int)(seconds % 60);
                }
                if (minutes > 59)
                {
                    hours = (int)(minutes / 60);
                    minutes = (int)(minutes % 60);
                }
                //tm += ", rem :";
                tm = "";
                tm += " " + ((hours == 0.0) ? "" : hours.ToString() + " h,") +
                     " " + ((minutes == 0.0) ? "" : minutes.ToString() + " m,") +
                     " " + ((seconds == 0.0) ? "" : seconds.ToString() + " s");
                tm = "" + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
                lbl_time_remain.Invoke(ctrl_text, lbl_time_remain, tm);

                if (Total_Time_On)
                {
                    hours += DateTime.Now.Hour;
                    minutes += DateTime.Now.Minute;
                    seconds += DateTime.Now.Second;

                    if (seconds > 59)
                    {
                        minutes++;
                        seconds -= 60;
                    }

                    if (minutes > 59)
                    {
                        hours++;
                        minutes -= 60;
                    }

                    tm = "END : " + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
                    lbl_tr.Invoke(ctrl_text, lbl_tr, tm);

                    //tm = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
                    tm = "START : " + dt.Hour.ToString("00") + ":" + dt.Minute.ToString("00") + ":" + dt.Second.ToString("00");
                    lbl_tm.Invoke(ctrl_text, lbl_tm, tm);
                }

            }
            try
            {
                if (last_A == A)
                {
                    fins.Invoke(fins.ctrl_text, fins, Title + " " + A.ToString() + " out of " + B.ToString());
                    OFF();
                }
                if (!fins.IsDisposed && !fins.Disposing)
                    fins.Invoke(fins.ctrl_text, fins, Title + " " + A.ToString() + " out of " + B.ToString());
                last_A = A;
            }
            catch (Exception ex) { }
            //}
        }
        private void timer1_Tick1(object sender, EventArgs e)
        {
            //return;
            double hours = (DateTime.Now - dt).Hours;
            double minutes = (DateTime.Now - dt).Minutes;
            double seconds = (DateTime.Now - dt).Seconds;

            //string tm = ", tm : ";
            string tm = "";
            tm += " " + ((hours == 0.0) ? "" : hours.ToString() + " h,") +
                  " " + ((minutes == 0.0) ? "" : minutes.ToString() + " m,") +
                  " " + ((seconds == 0.0) ? "" : seconds.ToString() + " s");
            lbl_total_time.Invoke(ctrl_text, lbl_total_time, tm);

            double progrs_val = A;
            if (thd != null)
                thd.Priority = ThreadPriority.Highest;
            if (progrs_val > 0)
            {
                double total_estimate_time = (hours * 60 * 60 + minutes * 60 + seconds);
                seconds = (int)((total_estimate_time / (A)) * (B - progrs_val));

                hours = minutes = 0.0;
                if (seconds > 59)
                {
                    minutes = (int)(seconds / 60);
                    seconds = (int)(seconds % 60);
                }
                if (minutes > 59)
                {
                    hours = (int)(minutes / 60);
                    minutes = (int)(minutes % 60);
                }
                //tm += ", rem :";
                tm = "";
                tm += " " + ((hours == 0.0) ? "" : hours.ToString() + " h,") +
                     " " + ((minutes == 0.0) ? "" : minutes.ToString() + " m,") +
                     " " + ((seconds == 0.0) ? "" : seconds.ToString() + " s");
                lbl_time_remain.Invoke(ctrl_text, lbl_time_remain, tm);

                if (Total_Time_On)
                {
                    hours += DateTime.Now.Hour;
                    minutes += DateTime.Now.Minute;
                    seconds += DateTime.Now.Second;

                    if (seconds > 59)
                    {
                        minutes++;
                        seconds -= 60;
                    }

                    if (minutes > 59)
                    {
                        hours++;
                        minutes -= 60;
                    }

                    tm = "EST.TM :" + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
                    lbl_tr.Invoke(ctrl_text, lbl_tr, tm);

                    tm = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
                    lbl_tm.Invoke(ctrl_text, lbl_tm, tm);
                }

            }
            //if (fins.InvokeRequired)
            //{
            fins.Invoke(fins.ctrl_text, fins, Title + " " + A.ToString() + " out of " + B.ToString());
            //}
        }

        private void lbl_percentage_Click(object sender, EventArgs e)
        {
            Total_Time_On = !Total_Time_On;
            lbl_tm.Invoke(ctrl_text, lbl_tm, "Total Time :");
            lbl_tr.Invoke(ctrl_text, lbl_tr, "Time Remaining :");
        }
        double _a = -11.0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            //if (A == B)
            if (A == B || _a == A)
                this.Dispose();
            _a = A;
        }

        private void frm_ProgressBar_MouseMove(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("");

            timer2.Interval = 786;
        }
    }
    public delegate void DChangeControlText(Object ctrl, object val);
    public delegate void DCloseForm(Form f);
}
