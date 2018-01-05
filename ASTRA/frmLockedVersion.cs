using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Aladdin.Hasp;

using AstraInterface.DataStructure;

namespace AstraFunctionOne
{
    public partial class frmLockedVersion : Form
    {
        int Service;
        int LptNum = 0, SeedCode;
        int Pass1, Pass2;
        int p1, p2, p3, p4;
        int lock_flag;
        long IDNum;

        //string authorization_code = "864a3142b031011f";

        //string authorization_code_high = "982a3142c031b11f";
        //string authorization_code_structure = "326s3142b031011f";
        //string authorization_code_structure_only = "326s5698b012511f";
        //string authorization_code_bridge = "859b3142b031011f";
        //string authorization_code_all = "529a3142b031011f";


        //string authorization_code_structure_high = "254g8751r031011f";
        //string authorization_code_structure_low = "425s6325d012548r";
        //string authorization_code_bridge_high = "785e9854w023658q";
        //string authorization_code_bridge_low = "365z4127t031011f";





        bool licMenu = false;
        public frmLockedVersion(bool isLicenseMenu)
        {
            InitializeComponent();
            licMenu = isLicenseMenu;
        }

        private void frmCheckHasp_Load(object sender, EventArgs e)
        {
            int ps1, ps2, p1, p2, p3, p4;

            ps1 = 4561;
            ps2 = 9261;
            p1 = p2 = p3 = p4 = 0;

            if (LockProgram.CheckHasp())
            {
                //MemoLock();
                if (LockProgram.Get_Authorization_Code() && LockProgram.Check_ASTRA_Structure_Lock() && LockProgram.Check_ASTRA_Bridge_Lock())
                {
                    if(!licMenu)
                        this.Close();
                }
                else
                {
                    frm_Authorised_Message faum = new frm_Authorised_Message();
                    faum.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show(this, "ASTRA Pro USB Dongle not found at any port.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
        //Chiranjit [2012 01 16]
        public int activation { get; set; }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txt_Auth_code_Bridge.Enabled)
            {
                #region Chiranjit Bridge Design


                //Application.Exit();
                if (txt_Auth_code_Bridge.Text == ASTRA_R21.Authorization_Code)
                {
          
                    //Chiranjit [2012 11 28]
                    #region Write ASTRA Code

                    LockProgram.WriteToLock(51, 18);
                    LockProgram.WriteToLock(52, 54);

                    #endregion

                    LockProgram.WriteToLock(10, 1);//authorize address
                    LockProgram.WriteToLock(27, 0);//authorize bridge high value address
                    //MessageBox.Show(this, "ASTRA Pro Authorized for Bridge Analysis & Design." + "\n\nPlease Contact: techsoft@consultant.com, dataflow@mail.com", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show(this, "ASTRA Pro Authorized for Bridge Analysis & Design. ", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show(this, "Dongle successfully authorized for : ASTRA Pro Bridge Design. ", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (txt_Auth_code_Bridge.Text == ASTRA_R21.NewCode_Enterprise_Bridge)
                {

                    //if (LockProgram.Check_ASTRA_Lock_19())
                    if (LockProgram.Check_Previuos_Version())
                    {
                        MessageBox.Show("Previous Version lock found. This Authorization Code is not valid. Enter Upgrade Code.", "ASTRA", MessageBoxButtons.OK);
                    }
                    else
                    {
                        //LockProgram.Write_ASTRA_Code_19();
                        //LockProgram.Write_ASTRA_AuthorisedCode_19();
                        //LockProgram.Write_ASTRA_Bridge_Code_19();
                        //LockProgram.Write_ASTRA_Enterprise_Code_19();

                        LockProgram.Write_ASTRA_Code_21();
                        LockProgram.Write_ASTRA_AuthorisedCode_21();
                        LockProgram.Write_ASTRA_Bridge_Code_21();
                        LockProgram.Write_ASTRA_Enterprise_Code_21();

                        MessageBox.Show(this, "ASTRA Pro Upgraded to full authorised. ", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (txt_Auth_code_Bridge.Text == ASTRA_R21.UpgradeCode_Enterprise_Bridge)
                {

                    //LockProgram.Write_ASTRA_Code_19();

                    //LockProgram.Write_ASTRA_AuthorisedCode_19();
                    //LockProgram.Write_ASTRA_Bridge_Code_19();
                    //LockProgram.Write_ASTRA_Enterprise_Code_19();

                    //LockProgram.Write_ASTRA_Code_20();
                    //LockProgram.Write_ASTRA_AuthorisedCode_20();
                    //LockProgram.Write_ASTRA_Bridge_Code_20();
                    //LockProgram.Write_ASTRA_Enterprise_Code_20();



                    LockProgram.Write_ASTRA_Code_21();
                    LockProgram.Write_ASTRA_AuthorisedCode_21();
                    LockProgram.Write_ASTRA_Bridge_Code_21();
                    LockProgram.Write_ASTRA_Enterprise_Code_21();


                    MessageBox.Show(this, "Dongle successfully authorized for : ASTRA Pro Bridge Design. ", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (txt_Auth_code_Bridge.Text == ASTRA_R21.NewCode_Professional_Bridge)
                {
                    if (LockProgram.Check_Previuos_Version())
                    {
                        MessageBox.Show("Previous Version lock found. This Authorization Code is not valid. Enter Upgrade Code.", "ASTRA", MessageBoxButtons.OK);
                        //MessageBox.Show("This Authorization Code is not valid.", "ASTRA", MessageBoxButtons.OK);
                    }
                    else
                    {
                        //LockProgram.Write_ASTRA_Code_19();
                        //LockProgram.Write_ASTRA_AuthorisedCode_19();
                        //LockProgram.Write_ASTRA_Bridge_Code_19();
                        //LockProgram.Write_ASTRA_Professional_Code_19();


                        LockProgram.Write_ASTRA_Code_21();
                        LockProgram.Write_ASTRA_AuthorisedCode_21();
                        LockProgram.Write_ASTRA_Bridge_Code_21();
                        LockProgram.Write_ASTRA_Professional_Code_21();

                        MessageBox.Show(this, "Dongle successfully authorized for : ASTRA Pro Bridge Design. ", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                else if (txt_Auth_code_Bridge.Text == ASTRA_R21.UpgradeCode_Professional_Bridge)
                {

                    LockProgram.Write_ASTRA_Code_21();
                    LockProgram.Write_ASTRA_AuthorisedCode_21();
                    LockProgram.Write_ASTRA_Bridge_Code_21();
                    LockProgram.Write_ASTRA_Professional_Code_21();

                    MessageBox.Show(this, "Dongle successfully authorized for : ASTRA Pro Bridge Design. ", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (txt_Auth_code_Bridge.Text == ASTRA_R21.UpgradeCode_Professional_Bridge_SP) // Special Code generated for Rahul Malviya
                {
                    int actvContrl = LockProgram.Get_Activation_Control_No();

                    if (actvContrl == 2)
                    {
                        LockProgram.Write_ASTRA_Code_21();
                        LockProgram.Write_ASTRA_AuthorisedCode_21();
                        LockProgram.Write_ASTRA_Bridge_Code_21();
                        LockProgram.Write_ASTRA_Professional_Code_21();

                        MessageBox.Show(this, "Dongle successfully authorized for : ASTRA Pro Bridge Design. ", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //MessageBox.Show(this, "Dongle successfully authorized for : ASTRA Pro Bridge Design. ", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show(this, "Sorry! Bridge Authorization Code is not valid." + "\n\nPlease Contact: techsoft@consultant.com, dataflow@mail.com", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if (txt_Auth_code_Bridge.Text == "")
                {
                }
                else
                {
                    //WriteToLock(10, 0);
                    MessageBox.Show(this, "Sorry! Bridge Authorization Code is not valid." + "\n\nPlease Contact: techsoft@consultant.com, dataflow@mail.com", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                #endregion Chiranjit Bridge Design
            }

            if (txt_Auth_code_Building.Enabled)
            {
                #region Chiranjit Builing Design

                if (txt_Auth_code_Building.Text == ASTRA_R21.NewCode_Enterprise_Structure)
                {
                    if (LockProgram.Check_Previuos_Version())
                    {
                        MessageBox.Show("This Authorization Code is not valid.", "ASTRA", MessageBoxButtons.OK);

                    }
                    else
                    {
                        //LockProgram.Write_ASTRA_Code_19();
                        //LockProgram.Write_ASTRA_AuthorisedCode_19();
                        //LockProgram.Write_ASTRA_Structure_Code_19();
                        //LockProgram.Write_ASTRA_Enterprise_Code_19();


                        //LockProgram.Write_ASTRA_Code_20();
                        //LockProgram.Write_ASTRA_AuthorisedCode_20();
                        //LockProgram.Write_ASTRA_Structure_Code_20();
                        //LockProgram.Write_ASTRA_Enterprise_Code_20();


                        LockProgram.Write_ASTRA_Code_21();
                        LockProgram.Write_ASTRA_AuthorisedCode_21();
                        LockProgram.Write_ASTRA_Structure_Code_21();
                        LockProgram.Write_ASTRA_Enterprise_Code_21();

                        MessageBox.Show(this, "Dongle successfully authorized for : ASTRA Pro RCC Framed Building Design. ", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (txt_Auth_code_Building.Text == ASTRA_R21.NewCode_Professional_Structure)
                {
                    if (LockProgram.Get_Speacial_Code_ASTRA_R21())
                    {
                        MessageBox.Show("This Building Authorization Code is not valid. Please Contact TechSOFT, email at dataflow@mail.com", "ASTRA", MessageBoxButtons.OK);
                    }
                    else
                    {
                        if (LockProgram.Check_Previuos_Version())
                        {
                            MessageBox.Show("This Authorization Code is not valid.", "ASTRA", MessageBoxButtons.OK);
                        }
                        else
                        {
                            //LockProgram.Write_ASTRA_Code_19();
                            //LockProgram.Write_ASTRA_AuthorisedCode_19();
                            //LockProgram.Write_ASTRA_Structure_Code_19();
                            //LockProgram.Write_ASTRA_Professional_Code_19();


                            //LockProgram.Write_ASTRA_Code_20();
                            //LockProgram.Write_ASTRA_AuthorisedCode_20();
                            //LockProgram.Write_ASTRA_Structure_Code_20();
                            //LockProgram.Write_ASTRA_Professional_Code_20();


                            LockProgram.Write_ASTRA_Code_21();
                            LockProgram.Write_ASTRA_AuthorisedCode_21();
                            LockProgram.Write_ASTRA_Structure_Code_21();
                            LockProgram.Write_ASTRA_Professional_Code_21();




                            MessageBox.Show(this, "Dongle successfully authorized for : ASTRA Pro RCC Framed Building Design. ", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else if (txt_Auth_code_Building.Text == ASTRA_R21.NewCode_Professional_Structure2)
                {
                    //if (LockProgram.Get_Speacial_Code_ASTRA_R21())
                    //{
                    //    MessageBox.Show("This Authorization Code is not valid. Please Contact TechSOFT, email at dataflow@mail.com", "ASTRA", MessageBoxButtons.OK);
                    //}
                    if (LockProgram.Check_Previuos_Version())
                    {
                        MessageBox.Show("This Authorization Code is not valid.", "ASTRA", MessageBoxButtons.OK);
                    }
                    else
                    {
                        //LockProgram.Write_ASTRA_Code_19();
                        //LockProgram.Write_ASTRA_AuthorisedCode_19();
                        //LockProgram.Write_ASTRA_Structure_Code_19();
                        //LockProgram.Write_ASTRA_Professional_Code_19();


                        //LockProgram.Write_ASTRA_Code_20();
                        //LockProgram.Write_ASTRA_AuthorisedCode_20();
                        //LockProgram.Write_ASTRA_Structure_Code_20();
                        //LockProgram.Write_ASTRA_Professional_Code_20();


                        LockProgram.Write_ASTRA_Code_21();
                        LockProgram.Write_ASTRA_AuthorisedCode_21();
                        LockProgram.Write_ASTRA_Structure_Code_21();
                        LockProgram.Write_ASTRA_Professional_Code_21();

                        MessageBox.Show(this, "Dongle successfully authorized for : ASTRA Pro RCC Framed Building Design. ", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (txt_Auth_code_Building.Text == ASTRA_R21.UpgradeCode_Enterprise_Structure)
                {
                    //LockProgram.Write_ASTRA_Code_19();
                    //LockProgram.Write_ASTRA_AuthorisedCode_19();
                    //LockProgram.Write_ASTRA_Structure_Code_19();
                    //LockProgram.Write_ASTRA_Enterprise_Code_19();


                    //LockProgram.Write_ASTRA_Code_20();
                    //LockProgram.Write_ASTRA_AuthorisedCode_20();
                    //LockProgram.Write_ASTRA_Structure_Code_20();
                    //LockProgram.Write_ASTRA_Enterprise_Code_20();


                    LockProgram.Write_ASTRA_Code_21();
                    LockProgram.Write_ASTRA_AuthorisedCode_21();
                    LockProgram.Write_ASTRA_Structure_Code_21();
                    LockProgram.Write_ASTRA_Enterprise_Code_21();



                    MessageBox.Show(this, "Dongle successfully authorized for : ASTRA Pro RCC Framed Building Design. ", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
       
           
                }
                else if (txt_Auth_code_Building.Text == ASTRA_R21.UpgradeCode_Professional_Structure)
                {
                    //LockProgram.Write_ASTRA_Code_19();
                    //LockProgram.Write_ASTRA_AuthorisedCode_19();
                    //LockProgram.Write_ASTRA_Structure_Code_19();
                    //LockProgram.Write_ASTRA_Professional_Code_19();


                    //LockProgram.Write_ASTRA_Code_20();
                    //LockProgram.Write_ASTRA_AuthorisedCode_20();
                    //LockProgram.Write_ASTRA_Structure_Code_20();
                    //LockProgram.Write_ASTRA_Professional_Code_20();


                    LockProgram.Write_ASTRA_Code_21();
                    LockProgram.Write_ASTRA_AuthorisedCode_21();
                    LockProgram.Write_ASTRA_Structure_Code_21();
                    LockProgram.Write_ASTRA_Professional_Code_21();



                    MessageBox.Show(this, "Dongle successfully authorized for : ASTRA Pro RCC Framed Building Design. ", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else if (txt_Auth_code_Building.Text == ASTRA_R21.UpgradeCode_Professional_Structure_SP)
                {
                    //LockProgram.Write_ASTRA_Code_19();
                    //LockProgram.Write_ASTRA_AuthorisedCode_19();
                    //LockProgram.Write_ASTRA_Structure_Code_19();
                    //LockProgram.Write_ASTRA_Professional_Code_19();


                    //LockProgram.Write_ASTRA_Code_20();
                    //LockProgram.Write_ASTRA_AuthorisedCode_20();
                    //LockProgram.Write_ASTRA_Structure_Code_20();
                    //LockProgram.Write_ASTRA_Professional_Code_20();

                    if (LockProgram.Get_Activation_Control_No() == 2)
                    {
                        LockProgram.Write_ASTRA_Code_21();
                        LockProgram.Write_ASTRA_AuthorisedCode_21();
                        LockProgram.Write_ASTRA_Structure_Code_21();
                        LockProgram.Write_ASTRA_Professional_Code_21();
                        MessageBox.Show(this, "Dongle successfully authorized for : ASTRA Pro RCC Framed Building Design. ", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(this, "Sorry! RCC Framed Building Authorization Code is not valid." + "\n\nPlease Contact: techsoft@consultant.com, dataflow@mail.com", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (txt_Auth_code_Building.Text == "")
                {
                }
                else
                {
                    //WriteToLock(10, 0);
                    MessageBox.Show(this, "Sorry! RCC Framed Building Authorization Code is not valid." + "\n\nPlease Contact: techsoft@consultant.com, dataflow@mail.com", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                #endregion Chiranjit Bridge Design

            }


            if (LockProgram.Get_Authorization_Code() == false && licMenu == false)
            {
                activation = LockProgram.Get_Activation();
                if (activation == 0)
                {
                    //Chiranjit [2012 12 20]
                    MessageBox.Show(this, "ASTRA Demo Version will run.", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Application.Exit();
                }
                else if (activation >= 1 && activation <= 1000)
                {
                    string str = (activation - 1) + " more Activation Left.";
                    MessageBox.Show(str, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    activation--;
                    LockProgram.Set_Activation(activation);
                }
                else if (activation >= 1)
                {
                    activation--;
                    LockProgram.Set_Activation(activation);
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public string Autho_Code
        {
            get
            {
                return txt_Auth_code_Bridge.Text;
            }
        }
        public bool IsActivate
        {
            get
            {
                return LockProgram.IsActivate_19;
            }
        }
        public static eVersionType Version_Type { get; set; }

        private void timer1_Tick(object sender, EventArgs e)
        {


            //timer1.Stop();
            //return;
            if (lbl_code.ForeColor != Color.Red)
                lbl_code.ForeColor = Color.Red;
            else
                lbl_code.ForeColor = Color.Blue;

            if (LockProgram.CheckHasp())
            {
                chk_bridge.Enabled = !LockProgram.Check_ASTRA_Bridge_Lock_19();
                chk_strucrure.Enabled = !LockProgram.Check_ASTRA_Structure_Lock_19();

                chk_bridge.Checked = !chk_bridge.Enabled;
                chk_strucrure.Checked = !chk_strucrure.Enabled;

                if (chk_bridge.Checked && chk_strucrure.Checked)
                {
                    //this.Close();
                    timer1.Stop();
                }

            }
            else
            {
                //MessageBox.Show(this, "ASTRA Pro USB Dongle not found at any port.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                chk_bridge.Enabled = true;
                chk_strucrure.Enabled = true;

                chk_bridge.Checked = !chk_bridge.Enabled;
                chk_strucrure.Checked = !chk_strucrure.Enabled;
            }

            //txt_Auth_code_Bridge.Enabled = !chk_bridge.Checked;
            //txt_Auth_code_Building.Enabled = !chk_strucrure.Checked;


            //if (!txt_Auth_code_Bridge.Enabled)
            //{
            //    txt_Auth_code_Bridge.Text = "AUTHORISED";
            //}
            //if (!txt_Auth_code_Building.Enabled)
            //{
            //    txt_Auth_code_Building.Text = "AUTHORISED";
            //}
        }


    }



    public static class LockProgram
    {
        static int Service;
        static int LptNum = 0, SeedCode;
        static int Pass1, Pass2;
        static int p1, p2, p3, p4;
        static int lock_flag;
        static long IDNum;

        //static string authorization_code = "864a3142b031011f";
        static string authorization_code = "984a2015b092517d";
        public static eVersionType Version_Type { get; set; }

        #region ASTRA R 19
        //static string authorization_code_high = "982a3142c031b11f";
        //static string authorization_code_structure = "326s3142b031011f";
        //static string authorization_code_structure_only = "326s5698b012511f";
        //static string authorization_code_bridge = "859b3142b031011f";
        //static string authorization_code_all = "529a3142b031011f";

        //static string authorization_code_structure_high = "254g8751r031011f";
        //static string authorization_code_structure_low = "425s6325d012548r";
        //static string authorization_code_bridge_high = "785e9854w023658q";
        //static string authorization_code_bridge_low = "365z4127t031011f";

        #endregion ASTRA R 19

        #region ASTRA R 20



        //static string authorization_code_high = "784a7848c031b11f";
        //static string authorization_code_structure = "985s6525b031011f";
        //static string authorization_code_structure_only = "245s9874b012511f";
        //static string authorization_code_bridge = "748b7845b031011f";
        //static string authorization_code_all = "365a4587b031011f";

        //static string authorization_code_structure_high = "865g7458r031011f";
        //static string authorization_code_structure_low = "789s5648d012548r";
        //static string authorization_code_bridge_high = "564e7845w023658q";
        //static string authorization_code_bridge_low = "798z7845t031011f";


        #endregion ASTRA R 20


        #region ASTRA R 21



        static string authorization_code_high = "983a1254c321542f";
        static string authorization_code_structure = "283s2021b265412g";
        static string authorization_code_structure_only = "874s4854b01246f";
        static string authorization_code_bridge = "843b7264b21516f";
        static string authorization_code_all = "124a4586b031011f";

        static string authorization_code_structure_high = "597g5488r031011f";
        static string authorization_code_structure_low = "845s6984e012987t";
        static string authorization_code_bridge_high = "326e6881w323748q";
        static string authorization_code_bridge_low = "421z7958t041022e";


        #endregion ASTRA R 21


        public static bool Check_Previuos_Version()
        {
            //return Check_ASTRA_Lock_18();
            if (Check_ASTRA_Lock_18()) return true;
            if (Check_ASTRA_Lock_19()) return true;
            if (Check_ASTRA_Lock_20()) return true;

            return false;
        }


        #region Lock Activation

        #region ASTRA_Pro R18.0

        public static bool Check_ASTRA_Lock_18()
        {
            if (!CheckHasp()) return false;

            if (!IsActivate_18 && Get_Activation() <= 0) return false;



            //else if (Version_Type == eVersionType.High_Value_Version)
            //{
            //    if (!Is_High_Value()) return false;
            //}


            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            // Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 18; //for heads
            //p2 == 23
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 23)
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
            }

            Service = 3;
            SeedCode = 0;

            p1 = 24; //for heads
            //p2 == 86
            param1 = (object)p1;
            param2 = (object)data;
            param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 86)
            {
                lock_flag = 1;
            }
            else
            {
                lock_flag = 0;
                return false;
            }

            if (Version_Type == eVersionType.High_Value_Version)
            {
                if (!Is_High_Value_18()) return false;
            }

            if (Version_Type == eVersionType.Low_Value_Version)
            {
                if (Is_High_Value_18()) return true;
            }


            return true;
        }
       
        public static void MemoLock_18()
        {
            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //                Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 18; //for heads
            //p2 == 23
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 23)
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                //MessageBox.Show(this, "Lock not found at any port for ASTRA Release 6.0...!!!", "ASTRA");
                //Application.Exit();
            }

            Service = 3;
            SeedCode = 0;

            p1 = 24; //for heads
            //p2 == 86
            param1 = (object)p1;
            param2 = (object)data;
            param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 86)
            {
                lock_flag = 1;

            }
            else
            {
                lock_flag = 0;
            }
        }

        public static bool Check_ASTRA_Structure_Lock_18()
        {
            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //	Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 27; // new address for Structural Analysis
            //p2 == 1 //Structure Analysis
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 1 || p2 == 2)
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
                //MessageBox.Show(this, "Lock not found at any port for ASTRA Release 6.0...!!!", "ASTRA");
                //Application.Exit();
            }



            if (Version_Type == eVersionType.High_Value_Version)
            {
                if (!Is_High_Value_18()) return false;
            }

            if (Version_Type == eVersionType.Low_Value_Version)
            {
                if (Is_High_Value_18()) return true;
            }


            return true;
        }
        public static bool Check_ASTRA_Bridge_Lock_18()
        {
            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //	Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 27; // new address for Structural Analysis
            //p2 == 1 //Structure Analysis
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 0 || p2 == 2 || p2 == 65535) //for Bridge
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
                //MessageBox.Show(this, "Lock not found at any port for ASTRA Release 6.0...!!!", "ASTRA");
                //Application.Exit();
            }


            if (Version_Type == eVersionType.High_Value_Version)
            {
                if (!Is_High_Value_18()) return false;
            }

            if (Version_Type == eVersionType.Low_Value_Version)
            {
                if (Is_High_Value_18()) return true;
            }

            return true;
        }
        public static bool IsProfessional_StructuralVersion_18()
        {
            try
            {
                int ps1, ps2, p1, p2, p3, p4;

                ps1 = 4561;
                ps2 = 9261;
                p1 = p2 = p3 = p4 = 0;
                //HASP_Lock f = new HASP_Lock();
                if (CheckHasp())
                {
                    //return f.Check_ASTRA_Lock();
                    if (Check_ASTRA_Lock_18())
                        return Check_ASTRA_Structure_Lock_18();
                }
            }
            catch (Exception ex) { }
            return false;
        }
        public static bool IsProfessional_BridgeVersion_18()
        {
            try
            {
                int ps1, ps2, p1, p2, p3, p4;

                ps1 = 4561;
                ps2 = 9261;
                p1 = p2 = p3 = p4 = 0;
                //HASP_Lock f = new HASP_Lock();
                if (CheckHasp())
                {
                    //return f.Check_ASTRA_Lock();
                    if (Check_ASTRA_Lock_18())
                        return Check_ASTRA_Bridge_Lock_18();
                }
            }
            catch (Exception ex) { }
            return false;
        }

        public static bool Get_Authorization_Code_18()
        {
            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //                Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 10; //for heads
            //p2 == 1 // If Authorizasion Code entered
            //p2 == 0 // If Authorizasion Code not entered
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            //p2 == 1 // If Authorizasion Code entered
            //p2 == 0 // If Authorizasion Code not entered
            if (p2 == 1)
            {
                lock_flag = 1;
            }
            else
            {
                lock_flag = 0;

            }
            return (lock_flag == 1);
        }

        public static bool IsActivate_18
        {
            get
            {
                return Get_Authorization_Code_18();
            }
        }
        public static bool Is_High_Value_18()
        {

            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            //p1=17 //for heads
            p1 = 25;
            //p2 == 1
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;

            return (p2 == 1);
        }
        public static bool Write_ASTRA_Code_18()
        {
            WriteToLock(18, 23);
            WriteToLock(24, 86);
            return true;
        }

        #endregion ASTRA_Pro R18.0

        #region ASTRA_Pro R19.0

        public static bool Write_ASTRA_Code_19()
        {
            WriteToLock(15, 18);
            WriteToLock(14, 54);
            return true;
        }

        public static bool Write_ASTRA_AuthorisedCode_19()
        {
            WriteToLock(8, 1);
            return true;
        }

        public static bool Write_ASTRA_Bridge_Code_19()
        {
            WriteToLock(12, 1);
            return true;
        }
        public static bool Write_ASTRA_Structure_Code_19()
        {
            WriteToLock(7, 1);
            return true;
        }
        public static bool Write_ASTRA_Enterprise_Code_19()
        {
            WriteToLock(13, 1);
            return true;
        }
        public static bool Write_ASTRA_Professional_Code_19()
        {
            WriteToLock(13, 0);
            return true;
        }

        public static bool Check_ASTRA_Lock_19()
        {
            if (!CheckHasp()) return false;

            if (!IsActivate_19 && Get_Activation() <= 0) return false;

            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            // Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 15; //for ASTRA Pro R19.0
            //p2 == 18
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 18)
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
            }

            Service = 3;
            SeedCode = 0;

            p1 = 14; //for heads
            //p2 == 54
            param1 = (object)p1;
            param2 = (object)data;
            param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 54)
            {
                lock_flag = 1;
            }
            else
            {
                lock_flag = 0;
                return false;
            }

            if (Version_Type == eVersionType.High_Value_Version)
            {
                if (!Is_High_Value_19()) return false;
            }

            if (Version_Type == eVersionType.Low_Value_Version)
            {
                if (Is_High_Value_19()) return true;
            }


            return true;
        }
        public static bool Check_ASTRA_Structure_Lock_19()
        {
            short lock_flag = 0;
            LptNum = 0;

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //	Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 7; // new address for Structural Analysis
            //p2 == 1 //Structure Analysis
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 1)
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
            }

            if (Version_Type == eVersionType.High_Value_Version)
            {
                if (!Is_High_Value_19()) return false;
            }

            if (Version_Type == eVersionType.Low_Value_Version)
            {
                if (Is_High_Value_19()) return true;
            }

            return true;
        }
        public static bool Check_ASTRA_Bridge_Lock_19()
        {
            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //	Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 12; // ASTRA Pro R 19.0
            //p2 == 1 //Structure Analysis
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 1) //for Bridge
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
            }


            if (Version_Type == eVersionType.High_Value_Version)
            {
                if (!Is_High_Value_19()) return false;
            }

            if (Version_Type == eVersionType.Low_Value_Version)
            {
                if (Is_High_Value_19()) return true;
            }

            return true;
        }
        public static bool IsProfessional_StructuralVersion_19()
        {
            try
            {
                int ps1, ps2, p1, p2, p3, p4;

                ps1 = 4561;
                ps2 = 9261;
                p1 = p2 = p3 = p4 = 0;
                //HASP_Lock f = new HASP_Lock();
                if (CheckHasp())
                {
                    //return f.Check_ASTRA_Lock();
                    if (Check_ASTRA_Lock_19())
                        return Check_ASTRA_Structure_Lock_19();
                }
            }
            catch (Exception ex) { }
            return false;
        }
        public static bool IsProfessional_BridgeVersion_19()
        {
            try
            {
                int ps1, ps2, p1, p2, p3, p4;

                ps1 = 4561;
                ps2 = 9261;
                p1 = p2 = p3 = p4 = 0;
                //HASP_Lock f = new HASP_Lock();
                if (CheckHasp())
                {
                    if (Check_ASTRA_Lock_19()) return Check_ASTRA_Bridge_Lock_19();
                }
            }
            catch (Exception ex) { }
            return false;
        }
        public static bool Get_Authorization_Code_19()
        {
            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;

            // Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 8; //for heads
            //p2 == 1 // If Authorizasion Code entered
            //p2 == 0 // If Authorizasion Code not entered
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            //p2 == 1 // If Authorizasion Code entered
            //p2 == 0 // If Authorizasion Code not entered
            if (p2 == 1)
            {
                lock_flag = 1;
            }
            else
            {
                lock_flag = 0;

            }
            return (lock_flag == 1);
        }

        public static bool IsActivate_19
        {
            get
            {
                return Get_Authorization_Code_19();
            }
        }

        public static bool Is_High_Value_19()
        {
            LptNum = 0;

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 13; //ASTRA Pro R 19.0
            //p2 == 1
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;

            return (p2 == 1);
        }


        #endregion ASTRA_Pro R19.0

        #region ASTRA_Pro R 20

        public static bool Write_ASTRA_Bridge_Code_20()
        {
            WriteToLock(49, 1);
            return true;
        }
        public static bool Write_ASTRA_Code_20()
        {
            WriteToLock(51, 18);
            WriteToLock(52, 54);
            return true;
        }
        public static bool Write_ASTRAStructure_Code_20()
        {
            Write_ASTRA_Code_20();
            Write_ASTRA_AuthorisedCode_20();
            Write_ASTRA_Structure_Code_20();
            Write_ASTRA_Enterprise_Code_20();
            return true;
        }


                    

        public static bool Write_ASTRA_AuthorisedCode_20()
        {
            WriteToLock(54, 1);
            return true;
        }

        public static bool Write_ASTRA_Structure_Code_20()
        {
            WriteToLock(48, 1);
            return true;
        }

        public static bool Write_ASTRA_Enterprise_Code_20()
        {
            WriteToLock(55, 1);
            return true;
        }

        public static bool Write_ASTRA_Professional_Code_20()
        {
            WriteToLock(55, 0);
            return true;
        }

        public static bool Check_ASTRA_Lock_20()
        {
            if (!CheckHasp()) return false;

            if (!IsActivate_20 && Get_Activation() <= 0) return false;

            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            // Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            //p1 = 15; //for ASTRA Pro R19.0
            p1 = 51; //for ASTRA Pro R20.0
            //p2 == 18
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 18)
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
            }

            Service = 3;
            SeedCode = 0;

            //p1 = 14; //for  ASTRA Pro R19.0
            p1 = 52; //for  ASTRA Pro R20.0
            //p2 == 54
            param1 = (object)p1;
            param2 = (object)data;
            param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 54)
            {
                lock_flag = 1;
            }
            else
            {
                lock_flag = 0;
                return false;
            }

            if (Version_Type == eVersionType.High_Value_Version)
            {
                if (!Is_High_Value_20()) return false;
            }

            if (Version_Type == eVersionType.Low_Value_Version)
            {
                if (Is_High_Value_20()) return true;
            }


            return true;
        }

        public static bool Check_ASTRA_Bridge_Lock_20()
        {
            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //	Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 49; // ASTRA Pro R 20.0
            //p2 == 1 //Structure Analysis
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 1) //for Bridge
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
            }


            if (Version_Type == eVersionType.High_Value_Version)
            {
                if (!Is_High_Value_20()) return false;
            }

            if (Version_Type == eVersionType.Low_Value_Version)
            {
                if (Is_High_Value_20()) return true;
            }

            return true;
        }
        public static bool Check_ASTRA_Structure_Lock_20()
        {
            short lock_flag = 0;
            LptNum = 0;

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //	Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 48; // new address for Structural Analysis
            //p2 == 1 //Structure Analysis
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 1)
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
            }

            if (Version_Type == eVersionType.High_Value_Version)
            {
                //if (!Is_High_Value_19()) return false;
                if (!Is_High_Value_20()) return false;
            }

            if (Version_Type == eVersionType.Low_Value_Version)
            {
                //if (Is_High_Value_19()) return true;
                if (Is_High_Value_20()) return true;
            }

            return true;
        }

        public static bool IsProfessional_StructuralVersion_20()
        {
            try
            {
                int ps1, ps2, p1, p2, p3, p4;

                ps1 = 4561;
                ps2 = 9261;
                p1 = p2 = p3 = p4 = 0;
                //HASP_Lock f = new HASP_Lock();
                if (CheckHasp())
                {
                    //return f.Check_ASTRA_Lock();
                    if (Check_ASTRA_Lock_20())
                        return Check_ASTRA_Structure_Lock_20();
                }
            }
            catch (Exception ex) { }
            return false;
        }

        public static bool IsProfessional_BridgeVersion_20()
        {
            try
            {
                int ps1, ps2, p1, p2, p3, p4;

                ps1 = 4561;
                ps2 = 9261;
                p1 = p2 = p3 = p4 = 0;
                //HASP_Lock f = new HASP_Lock();
                if (CheckHasp())
                {
                    if (Check_ASTRA_Lock_20()) return Check_ASTRA_Bridge_Lock_20();
                }
            }
            catch (Exception ex) { }
            return false;
        }

        public static bool Get_Authorization_Code_20()
        {
            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;

            // Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 54;
            //p2 == 1 // If Authorizasion Code entered
            //p2 == 0 // If Authorizasion Code not entered
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            //p2 == 1 // If Authorizasion Code entered
            //p2 == 0 // If Authorizasion Code not entered
            if (p2 == 1)
            {
                lock_flag = 1;
            }
            else
            {
                lock_flag = 0;

            }
            return (lock_flag == 1);
        }

        public static bool Is_High_Value_20()
        {
            LptNum = 0;

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 55; //ASTRA Pro R 20
            //p2 == 1
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;

            return (p2 == 1);
        }

        public static bool IsActivate_20
        {
            get
            {
                return Get_Authorization_Code_20();
            }
        }
        public static int Get_Activation_ASTRA_R20()
        {
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //                Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            //p1=17 //for heads
            p1 = 53;
            //p2 == 23
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;

            if (p2 > 60000) return 0;
            return p2;
        }

        public static void Set_Activation_ASTRA_R20(int actv)
        {
            WriteToLock(53, actv);
        }
        #endregion ASTRA_Pro R 20

        #region ASTRA_Pro R 21

        public static bool Write_ASTRA_Bridge_Code_21()
        {
            WriteToLock(49, 1);
            return true;
        }
        public static bool Write_ASTRA_Structure_Code_21()
        {
            WriteToLock(48, 1);
            return true;
        }

        public static bool Write_ASTRA_Code_21()
        {
            WriteToLock(4, 18);
            WriteToLock(5, 54);
            return true;
        }



        public static bool Write_ASTRA_AuthorisedCode_21()
        {
            WriteToLock(10, 1);
            return true;
        }


        public static bool Write_ASTRA_Enterprise_Code_21()
        {
            WriteToLock(19, 1);
            return true;
        }

        public static bool Write_ASTRA_Professional_Code_21()
        {
            WriteToLock(19, 0);
            return true;
        }

        public static bool Check_ASTRA_Lock_21()
        {
            if (!CheckHasp()) return false;

            if (!IsActivate_21 && Get_Activation() <= 0) return false;

            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            // Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 4; //for ASTRA Pro R21
            //p2 == 18
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 18)
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
            }

            Service = 3;
            SeedCode = 0;

            p1 = 5; //for  ASTRA Pro R21.0
            //p2 == 54
            param1 = (object)p1;
            param2 = (object)data;
            param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 54)
            {
                lock_flag = 1;
            }
            else
            {
                lock_flag = 0;
                return false;
            }

            if (Version_Type == eVersionType.High_Value_Version)
            {
                if (!Is_High_Value_21()) return false;
            }

            if (Version_Type == eVersionType.Low_Value_Version)
            {
                if (Is_High_Value_21()) return true;
            }


            return true;
        }

        public static bool Check_ASTRA_Bridge_Lock_21()
        {
            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //	Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 49; // ASTRA Pro R 21.0
            //p2 == 1 //Structure Analysis
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 1) //for Bridge
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
            }


            if (Version_Type == eVersionType.High_Value_Version)
            {
                if (!Is_High_Value_21()) return false;
            }

            if (Version_Type == eVersionType.Low_Value_Version)
            {
                if (Is_High_Value_21()) return true;
            }

            return true;
        }
        public static bool Check_ASTRA_Structure_Lock_21()
        {
            short lock_flag = 0;
            LptNum = 0;

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //	Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 48; // new address for Structural Analysis
            //p2 == 1 //Structure Analysis
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 1)
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
            }

            if (Version_Type == eVersionType.High_Value_Version)
            {
                //if (!Is_High_Value_19()) return false;
                if (!Is_High_Value_20()) return false;
            }

            if (Version_Type == eVersionType.Low_Value_Version)
            {
                //if (Is_High_Value_19()) return true;
                if (Is_High_Value_20()) return true;
            }

            return true;
        }

        public static bool IsProfessional_StructuralVersion_21()
        {
            try
            {
                if (CheckHasp())
                {
                    if (Check_ASTRA_Lock_21())
                        return Check_ASTRA_Structure_Lock_21();
                }
            }
            catch (Exception ex) { }
            return false;
        }

        public static bool IsProfessional_BridgeVersion_21()
        {
            try
            {

                if (CheckHasp())
                {
                    if (Check_ASTRA_Lock_21()) return Check_ASTRA_Bridge_Lock_21();
                }
            }
            catch (Exception ex) { }
            return false;
        }

        public static bool Get_Authorization_Code_21()
        {
            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;

            // Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 10;
            //p2 == 1 // If Authorizasion Code entered
            //p2 == 0 // If Authorizasion Code not entered
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            //p2 == 1 // If Authorizasion Code entered
            //p2 == 0 // If Authorizasion Code not entered
            if (p2 == 1)
            {
                lock_flag = 1;
            }
            else
            {
                lock_flag = 0;

            }
            return (lock_flag == 1);
        }

        public static bool Is_High_Value_21()
        {
            LptNum = 0;

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 19; //ASTRA Pro R 21
            //p2 == 1
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;

            return (p2 == 1);
        }

        public static bool IsActivate_21
        {
            get
            {
                return Get_Authorization_Code_21();
            }
        }


        public static int Get_Activation_ASTRA_R21()
        {
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //                Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 6;
            //p2 == 23
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;

            if (p2 > 60000) return 0;
            return p2;
        }


        public static int Get_Activation_Control_No()
        {
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //                Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 50;
            //p2 == 23
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;

            if (p2 > 60000) return 0;
            return p2;
        }


        public static void Set_Activation_ASTRA_R21(int actv)
        {
            WriteToLock(6, actv);
        }


        public static bool Write_ASTRAStructure_Code_21()
        {
            Write_ASTRA_Code_21();
            Write_ASTRA_AuthorisedCode_21();
            Write_ASTRA_Structure_Code_21();
            Write_ASTRA_Enterprise_Code_21();
            return true;
        }




        public static bool Get_Speacial_Code_ASTRA_R21()
        {
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 21; // 21 for new Structure Code
            //p2 == 1
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;

            if (p2 == 1) return true;
            return false;
        }

        #endregion ASTRA_Pro R 21


        #region Common Functions


        public static bool Write_ASTRA_Code_Bridge()
        {

            //return Write_ASTRA_Code_19();
            //return Write_ASTRA_Code_20();
            //return Write_ASTRA_Code_21();


            //Write_ASTRA_Code_20();
            //Write_ASTRA_AuthorisedCode_20();
            //Write_ASTRA_Bridge_Code_20();
            //Write_ASTRA_Professional_Code_20();

            if (CheckHasp())
            {
                Set_Activation(10);
                Write_ASTRA_Code_21();
                Write_ASTRA_AuthorisedCode_21();
                Write_ASTRA_Bridge_Code_21();
                Write_ASTRA_Professional_Code_21();
                return true;
            }
            return false;
        }

        public static bool Write_ASTRA_Code_Structure()
        {

            //return Write_ASTRA_Code_19();
            //return Write_ASTRA_Code_20();
            //return Write_ASTRA_Code_21();


            //Write_ASTRA_Code_20();
            //Write_ASTRA_AuthorisedCode_20();
            //Write_ASTRA_Bridge_Code_20();
            //Write_ASTRA_Professional_Code_20();

            if (CheckHasp())
            {
                Set_Activation(10);
                Write_ASTRA_Code_21();
                Write_ASTRA_AuthorisedCode_21();
                Write_ASTRA_Structure_Code_21();
                Write_ASTRA_Professional_Code_21();
                return true;
            }
            return false;
        }
         
        public static bool Check_ASTRA_Lock()
        {
            //return Check_ASTRA_Lock_19();
            //return Check_ASTRA_Lock_20();
            return Check_ASTRA_Lock_21();
        }
        public static bool Check_ASTRA_Structure_Lock()
        {
            //return Check_ASTRA_Structure_Lock_19();
            //return Check_ASTRA_Structure_Lock_20();
            return Check_ASTRA_Structure_Lock_21();
        }
        public static bool Check_ASTRA_Bridge_Lock()
        {
            //return Check_ASTRA_Bridge_Lock_19();
            //return Check_ASTRA_Bridge_Lock_20();
            return Check_ASTRA_Bridge_Lock_21();
        }
        public static bool IsProfessional_StructuralVersion()
        {
            //return IsProfessional_StructuralVersion_19();
            //return IsProfessional_StructuralVersion_20();
            return IsProfessional_StructuralVersion_21();
        }
        public static bool IsProfessional_BridgeVersion()
        {
            //return IsProfessional_BridgeVersion_19();
            //return IsProfessional_BridgeVersion_20();
            return IsProfessional_BridgeVersion_21();
        }

        public static bool Get_Authorization_Code()
        {
            //return Get_Authorization_Code_19();
            //return Get_Authorization_Code_20();
            return Get_Authorization_Code_21();
        }

        public static bool IsActivate
        {
            get
            {
                //return IsActivate_19;
                //return IsActivate_20;
                return IsActivate_21;
            }
        }

        public static bool Is_High_Value()
        {
            //return Is_High_Value_19();
            //return Is_High_Value_20();
            return Is_High_Value_21();
        }

        #endregion  Common Functions

        #region General Functions

        public static bool Is_AASHTO()
        {
            p2 = 0;
            //if (p2 != 1)
            //{
            if (TimeZone.CurrentTimeZone.StandardName.ToUpper().StartsWith("INDIA")) return false;
            else p2 = 1;
            //}

            return (p2 == 1);
        }
        public static int Get_Activation()
        {
            return Get_Activation_ASTRA_R21();
        }
        public static void Set_Activation(int actv)
        {
            //WriteToLock(53, actv);
            //Set_Activation_ASTRA_R20(actv);
            Set_Activation_ASTRA_R21(actv);
        }
        public static bool WriteToLock(int p1Val, int p2Val)
        {
            int service, seed, lptnum, passw1, passw2, p1, p2, p3, p4;
            service = 0;
            seed = 0;
            lptnum = 0;
            //passw1 = Pass1;
            //passw2 = Pass2;

            passw1 = 4651;
            passw2 = 9261;

            p1 = p1Val;
            p2 = p2Val;
            p3 = 0;
            p4 = 0;

            bool bSuccess = true;

            HaspKey.Hasp(HaspService.WriteWord, seed, lptnum, passw1, passw2, p1, p2, p3, p4);
            if (p3 != 0)
            {
                bSuccess = false;
                //MessageBox.Show(this, "Error.", "TechSOFT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return bSuccess;
        }
        public static bool CheckHasp()
        {
            //int Service = 0;
            int LptNum = 0, SeedCode = 0;
            //int Pass1, Pass2;
            //int p1, p2, p3, p4;
            //int lock_flag;
            //long IDNum;

            int ps1, ps2, p1, p2, p3, p4;

            ps1 = 4561;
            ps2 = 9261;
            p1 = p2 = p3 = p4 = 0;

            bool findHasp = false;

            int result = 0;
            int status = 0;

            object param1 = (object)result;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.IsHasp,
                SeedCode,
                LptNum,
                0,
                0,
                param1,
                null,
                param3,
                null);


            result = (int)param1;
            status = (int)param3;

            //if (0 == status)
            //    findHasp = true;
            if (result == 1)
                findHasp = true;
            return findHasp;
        }
        public static int activations { get; set; }

        #endregion General Functions

        #endregion Lock Activation


    }

    //public static class ASTRA_R19
    //{
    //    public static string Authorization_Code = "864a3142b031011f";

    //    public static string NewCode_Enterprise_Bridge = "895a7849c031b48p";

    //    public static string NewCode_Enterprise_Structure = "458a8456c031b96q";

    //    public static string NewCode_Professional_Bridge = "987a9748c031b15w";
    //    public static string NewCode_Professional_Structure = "754a1423c031b74k";

    //    public static string UpgradeCode_Enterprise_Bridge = "659a7485c031b35h";
    //    public static string UpgradeCode_Enterprise_Structure = "897a9648c031b79g";

    //    public static string UpgradeCode_Professional_Bridge = "326a9147c031b46d";
    //    public static string UpgradeCode_Professional_Structure = "485a5412c874b74s";

    //}



    public static class ASTRA_R20
    {
        public static string Authorization_Code = "987a3142b031011f";

        public static string NewCode_Enterprise_Bridge = "478a7849c031b48p";

        public static string NewCode_Enterprise_Structure = "326a8456c031b96q";

        public static string NewCode_Professional_Bridge = "548a9748c031b15w";
        public static string NewCode_Professional_Structure = "874a1423c031b74k";

        public static string UpgradeCode_Enterprise_Bridge = "364a7485c031b35h";
        public static string UpgradeCode_Enterprise_Structure = "795a9648c031b79g";

        public static string UpgradeCode_Professional_Bridge = "854a9147c031b46d";
        public static string UpgradeCode_Professional_Structure = "345a5412c874b74s";
    }


    public static class ASTRA_R21
    {
        public static string Authorization_Code = "983a1254c321542f";

        public static string NewCode_Enterprise_Bridge = "283s2021b265412g";
        public static string NewCode_Enterprise_Structure = "874s4854b01246f";

        public static string NewCode_Professional_Bridge = "843b7264b21516f";
        public static string NewCode_Professional_Structure = "124a4586b031011f";

        //This new Structure Code is added for Mr. Dhananjay and Dhruva 
        public static string NewCode_Professional_Structure2 = "594a3561b218151w"; 

        public static string UpgradeCode_Enterprise_Bridge = "597g5488r031011f";
        public static string UpgradeCode_Enterprise_Structure = "845s6984e012987t";




        public static string UpgradeCode_Professional_Bridge = "326e6881w323748q";
        public static string UpgradeCode_Professional_Structure = "421z7958t041022e";



        //Special Code for Rahul Malviya
        public static string UpgradeCode_Professional_Bridge_SP = "716e5478t985236w";
        public static string UpgradeCode_Professional_Structure_SP = "254s9659q965342p";
    }
}
