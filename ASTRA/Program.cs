using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace AstraFunctionOne
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main(String[] args)
        {
            try
            {
                Application.ThreadException += new ThreadExceptionEventHandler(Form1_UIThreadException);

                //    // Set the unhandled exception mode to force all Windows Forms errors to go through 
                //    // our handler.
                //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);


                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException, true);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                frmMain frm = new frmMain();
                if (args.Length > 0)
                {
                    frm.FromFileClick = args[0];
                }
                else
                {
                    frm.FromFileClick = "";
                }
                Application.Run(frm);
                //Application.Run(new frmMain());
            }
            catch (Exception ex)
            {
                MessageBox.Show("CAUGHT EXCEPTIONS :\n\n" + ex.Message);
            }
        }

        // Handle the UI exceptions by showing a dialog box, and asking the user whether 
        // or not they wish to abort execution. 
        private static void Form1_UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                //result = MessageBox.Show("Windows Forms Error", t.Exception);
               //MessageBox.Show("Windows Forms Error", t.Exception.Message);
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
    }
}



//// Starts the application. 
//public static void Main(string[] args)
//{
//    // Add the event handler for handling UI thread exceptions to the event.
//    Application.ThreadException += new ThreadExceptionEventHandler(ErrorHandlerForm.Form1_UIThreadException);

//    // Set the unhandled exception mode to force all Windows Forms errors to go through 
//    // our handler.
//    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

//    // Add the event handler for handling non-UI thread exceptions to the event. 
//    AppDomain.CurrentDomain.UnhandledException +=
//        new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

//    // Runs the application.
//    Application.Run(new ErrorHandlerForm());
//}

//// Programs the button to throw an exception when clicked. 
//private void button1_Click(object sender, System.EventArgs e)
//{
//    throw new ArgumentException("The parameter was invalid");
//}

//// Start a new thread, separate from Windows Forms, that will throw an exception. 
//private void button2_Click(object sender, System.EventArgs e)
//{
//    ThreadStart newThreadStart = new ThreadStart(newThread_Execute);
//    newThread = new Thread(newThreadStart);
//    newThread.Start();
//}

//// The thread we start up to demonstrate non-UI exception handling.  
//void newThread_Execute()
//{
//    throw new Exception("The method or operation is not implemented.");
//}

//// Handle the UI exceptions by showing a dialog box, and asking the user whether 
//// or not they wish to abort execution. 
//private static void Form1_UIThreadException(object sender, ThreadExceptionEventArgs t)
//{
//    DialogResult result = DialogResult.Cancel;
//    try
//    {
//        result = ShowThreadExceptionDialog("Windows Forms Error", t.Exception);
//    }
//    catch
//    {
//        try
//        {
//            MessageBox.Show("Fatal Windows Forms Error",
//                "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
//        }
//        finally
//        {
//            Application.Exit();
//        }
//    }

//    // Exits the program when the user clicks Abort. 
//    if (result == DialogResult.Abort)
//        Application.Exit();
//}

// Handle the UI exceptions by showing a dialog box, and asking the user whether 
// or not they wish to abort execution. 
// NOTE: This exception cannot be kept from terminating the application - it can only  
// log the event, and inform the user about it.  

// Creates the error message and displays it. 
//private static DialogResult ShowThreadExceptionDialog(string title, Exception e)
//{
//    string errorMsg = "An application error occurred. Please contact the adminstrator " +
//        "with the following information:\n\n";
//    errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;
//    return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore,
//        MessageBoxIcon.Stop);
//}
//}
