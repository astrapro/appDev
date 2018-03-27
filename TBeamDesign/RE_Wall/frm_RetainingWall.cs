using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraFunctionOne.BridgeDesign;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraInterface.TrussBridge;
using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;


namespace BridgeAnalysisDesign.RE_Wall
{
    public partial class frm_RetainingWall : Form
    {
        RCC_RetainingWall Abut = null;
        IApplication iApp;
        bool IsCreateData = true;




        public frm_RetainingWall(IApplication app, bool isPropped)
        {
            InitializeComponent();
            this.iApp = app;

            Abut = new RCC_RetainingWall(iApp);
            Abut.IsPropped = isPropped;
            //Abut.IsPropped = true;
            pic_cantilever.BackgroundImage = AstraFunctionOne.ImageCollection.Abutment;


        }

        private void Abutment_Initialize_InputData()
        {
            #region Variables Initialize with default data

            Abut.d1 = MyList.StringToDouble(txt_abut_DMG.Text, 0.0);
            Abut.t = MyList.StringToDouble(txt_abut_t.Text, 0.0);
            Abut.H = MyList.StringToDouble(txt_abut_H.Text, 0.0);
            Abut.a = MyList.StringToDouble(txt_abut_a.Text, 0.0);
            Abut.gamma_b = MyList.StringToDouble(txt_abut_gamma_b.Text, 0.0);
            Abut.gamma_c = MyList.StringToDouble(txt_abut_gamma_c.Text, 0.0);
            Abut.phi = MyList.StringToDouble(txt_abut_phi.Text, 0.0);
            Abut.p = MyList.StringToDouble(txt_abut_p_bearing_capacity.Text, 0.0);
            Abut.f_ck = MyList.StringToDouble(cmb_abut_fck.Text, 0.0);
            Abut.f_y = MyList.StringToDouble(cmb_abut_fy.Text, 0.0);
            Abut.w6 = MyList.StringToDouble(txt_abut_w6.Text, 0.0);
            Abut.w5 = MyList.StringToDouble(txt_abut_w5.Text, 0.0);
            Abut.F = MyList.StringToDouble(txt_abut_F.Text, 0.0);
            Abut.d2 = MyList.StringToDouble(txt_abut_d2.Text, 0.0);
            Abut.d3 = MyList.StringToDouble(txt_abut_d3.Text, 0.0);
            Abut.B = MyList.StringToDouble(txt_abut_B.Text, 0.0);
            Abut.theta = MyList.StringToDouble(txt_abut_theta.Text, 0.0);
            Abut.delta = MyList.StringToDouble(txt_abut_delta.Text, 0.0);
            Abut.z = MyList.StringToDouble(txt_abut_z.Text, 0.0);
            Abut.mu = MyList.StringToDouble(txt_abut_mu.Text, 0.0);
            Abut.L1 = MyList.StringToDouble(txt_abut_L1.Text, 0.0);
            Abut.L2 = MyList.StringToDouble(txt_abut_L2.Text, 0.0);
            Abut.L3 = MyList.StringToDouble(txt_abut_L3.Text, 0.0);
            Abut.L4 = MyList.StringToDouble(txt_abut_L4.Text, 0.0);
            Abut.h1 = MyList.StringToDouble(txt_abut_h1.Text, 0.0);
            Abut.L = MyList.StringToDouble(txt_abut_L.Text, 0.0);
            Abut.d4 = MyList.StringToDouble(txt_abut_d4.Text, 0.0);
            Abut.cover = MyList.StringToDouble(txt_abut_cover.Text, 0.0);
            Abut.factor = MyList.StringToDouble(txt_abut_fact.Text, 0.0);
            Abut.sc = MyList.StringToDouble(txt_abut_sc.Text, 0.0);

            #endregion
        }

        private void btn_cnt_Process_Click(object sender, EventArgs e)
        {
            Write_All_Data();
            Abut.FilePath = user_path;
            Abutment_Initialize_InputData();
            Abut.Write_Cantilever__User_input();
            Abut.Calculate_Program(Abut.rep_file_name);
            Abut.Write_Cantilever_Drawing_File();
            //iApp.Save_Form_Record(this, user_path);
            MessageBox.Show(this, "Report file written in " + Abut.rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            iApp.View_Result(Abut.rep_file_name);
            Abut.is_process = true;
            Button_Enable_Disable();
        }

        private void btn_dwg_open_GAD_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            eOpenDrawingOption opt = iApp.Open_Drawing_Option();

            if (opt == eOpenDrawingOption.Cancel) return;
            string draw = Drawing_Folder;


            string copy_path = Abut.rep_file_name;


            if (opt == eOpenDrawingOption.Design_Drawings)
            {
                #region Design Drawings
                if (b.Name == btn_dwg_open_Cantilever.Name)
                {
                    iApp.Form_Drawing_Editor(eBaseDrawings.RCC_T_GIRDER_LS_CANTILEVER_ABUTMENT, draw, copy_path).ShowDialog();
                }
                #endregion Design Drawings
            }
            else if (opt == eOpenDrawingOption.Sample_Drawings)
            {
                #region Sample Drawings
                if (b.Name == btn_dwg_open_Cantilever.Name)
                {
                    iApp.RunViewer(Path.Combine(Drawing_Folder, "Retaining Wall Drawings"), "TBeam_Abutment");
                }
                #endregion Sample Drawings
            }
        }

        #region Create Project / Open Project


        public string Title
        {
            get
            {
                if (iApp.DesignStandard == eDesignStandard.BritishStandard)
                    return "DESIGN OF RETAINING WALL [BS]";
                return "DESIGN OF RETAINING WALL [IRC]";
            }
        }

        public string user_path
        {
            get
            {
                return iApp.user_path;
            }
            set
            {
                iApp.user_path = value;
            }
        }

        public string Drawing_Folder
        {
            get
            {
                if (user_path != iApp.LastDesignWorkingFolder)
                    if (Directory.Exists(Path.Combine(user_path, "DRAWINGS")) == false)
                        Directory.CreateDirectory(Path.Combine(user_path, "DRAWINGS"));
                return Path.Combine(user_path, "DRAWINGS");
            }
        }

        public void Create_Project()
        {
            //user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            //if (!Directory.Exists(user_path))
            //{
            //    Directory.CreateDirectory(user_path);
            //}

            //user_path = Path.Combine(user_path, Project_Name);
            //if (!Directory.Exists(user_path))
            //{
            //    Directory.CreateDirectory(user_path);
            //}

            //string fname = Path.Combine(user_path, Project_Name + ".apr");

            //int ty = (int)Project_Type;
            //File.WriteAllText(fname, ty.ToString());


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

            Write_All_Data();


            MessageBox.Show(Project_Name + " is Created.", "ASTRA", MessageBoxButtons.OK);
        }


        public void Write_All_Data()
        {
            Write_All_Data(true);
        }
        private void DemoCheck()
        {
            if (iApp.Check_Demo_Version())
            {
                //txt_Ana_L.Text = "0.0";
                //txt_Ana_L.Text = "19.2";
                //txt_Ana_B.Text = "12.1";
                //txt_Ana_CW.Text = "11.0";

                //string str = "ASTRA Pro USB Dongle not found at any port.\n\nThis is Unauthorized Version of ASTRA Pro.\n This will process the default Data only as sample input data.\n\n";
                //str += "For Professional Version of ASTRA Pro please contact : \n\n";
                //str += "Email at : techsoft@consultant.com, dataflow@mail.com\n\n";
                //str += "Website  : http://www.headsview.com\n\n";
                //str += "Tel. No  : +91 33 4008 3349,  +91 33 6526 1190\n\n";
                //str += "\n\nTechSOFT Engineering Services\n\n";
                //MessageBox.Show(this, str, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void Write_All_Data(bool showMessage)
        {
            if (showMessage) DemoCheck();
            iApp.Save_Form_Record(this, user_path);
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
                    txt_project_name.Text = Path.GetFileName(frm.Example_Path);
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
                    txt_project_name.Text = Path.GetFileName(user_path);
                    Write_All_Data();
                }
            }
            else if (btn.Name == btn_new_design.Name)
            {
                IsCreateData = true;
                Create_Project();
            }
            Button_Enable_Disable();
        }


        public void Button_Enable_Disable()
        {
            btn_cnt_Process.Enabled = Directory.Exists(user_path);
            btn_cnt_Report.Enabled = File.Exists(Abut.rep_file_name);
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
                return eASTRADesignType.Retaining_Wall;
            }
        }

        public void Delete_Folder(string folder)
        {
            MyList.Delete_Folder(folder);
        }

        #endregion Create Project
        private void btn_cnt_Report_Click(object sender, EventArgs e)
        {
            iApp.RunExe(Abut.rep_file_name);
        }

        private void frm_RetainingWall_Load(object sender, EventArgs e)
        {
            Set_Project_Name();

            cmb_abut_fck.SelectedIndex = 2;

            cmb_abut_fy.SelectedIndex = 1;

            Button_Enable_Disable();

        }
    }


    public class RCC_RetainingWall
    {
        #region Variable Declaration
        public string rep_file_name = "";
        public string file_path = "";
        public string user_path = "";
        public string system_path = "";
        public string drawing_path = "";
        public string user_input_file = "";
        public bool is_process = false;


        public double d1, t, H, a, gamma_b, gamma_c, phi, p, f_ck, f_y, w6, w5, F, d2, d3, d4, B;
        public double theta, delta, z, mu, L1, L2, L3, L4, h1, L, cover, factor, sc;

        #endregion

        #region Drawing Variable

        double _bd1, _bd2, _bd3, _bd4, _bd5, _bd6, _bd7;
        double _sp1, _sp2, _sp3, _sp4, _sp5, _sp6, _sp7;

        string _L1, _L2, _L3, _D, _d4, _H, _d3, _d1, _L4;
        #endregion

        IApplication iApp = null;

        public bool IsExecuteBridge = true;
        public RCC_RetainingWall(IApplication app)
        {
            this.iApp = app;
            _bd1 = 0.0;
            _bd2 = 0.0;
            _bd3 = 0.0;
            _bd4 = 0.0;
            _bd5 = 0.0;
            _bd6 = 0.0;
            _bd7 = 0.0;

            _sp1 = 0.0;
            _sp2 = 0.0;
            _sp3 = 0.0;
            _sp4 = 0.0;
            _sp5 = 0.0;
            _sp6 = 0.0;
            _sp7 = 0.0;
            sc = 0.18;
        }
        public RCC_RetainingWall(IApplication app, bool isBridge)
        {
            this.iApp = app;
            IsExecuteBridge = isBridge;

            _bd1 = 0.0;
            _bd2 = 0.0;
            _bd3 = 0.0;
            _bd4 = 0.0;
            _bd5 = 0.0;
            _bd6 = 0.0;
            _bd7 = 0.0;

            _sp1 = 0.0;
            _sp2 = 0.0;
            _sp3 = 0.0;
            _sp4 = 0.0;
            _sp5 = 0.0;
            _sp6 = 0.0;
            _sp7 = 0.0;
            sc = 0.18;
        }



        public void Write_Cantilever__User_input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region USER Data

                sw.WriteLine("d1 = {0} ", d1);
                sw.WriteLine("t = {0}", t);
                sw.WriteLine("H = {0}", H);
                sw.WriteLine("B = {0}", B);
                sw.WriteLine("d2 = {0}", d2);
                sw.WriteLine("d3 = {0}", d3);
                sw.WriteLine("L1 = {0}", L1);
                sw.WriteLine("L2 = {0}", L2);
                sw.WriteLine("L3 = {0}", L3);
                sw.WriteLine("L4 = {0}", L4);
                sw.WriteLine("L = {0}", L);
                sw.WriteLine("d4 = {0:f2}", d4);
                sw.WriteLine("theta = {0}", theta);
                sw.WriteLine("delta = {0}", delta);
                sw.WriteLine("z = {0}", z);
                sw.WriteLine("mu = {0}", mu);
                sw.WriteLine("gamma_b = {0}", gamma_b);
                sw.WriteLine("gamma_c = {0}", gamma_c);
                sw.WriteLine("phi = {0}", phi);
                sw.WriteLine("p = {0}", p);
                sw.WriteLine("f_ck = {0:f0}", f_ck);
                sw.WriteLine("f_y = {0:f0}", f_y);
                sw.WriteLine("w6 = {0:f2}", w6);
                sw.WriteLine("w5 = {0:f2}", w5);
                sw.WriteLine("F = {0:f2}", F);
                sw.WriteLine("factor = {0:f2}", factor);
                sw.WriteLine("cover = {0:f2}", cover);
                #endregion
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }

        }

       public  bool IsPropped = false;

        public void Calculate_Program(string file_name)
        {
            string ref_string = "";
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            #region TechSOFT Banner
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t*                  ASTRA Pro                  *");
            sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
            sw.WriteLine("\t\t*                                             *");
            if (IsExecuteBridge)
            {
                sw.WriteLine("\t\t*          DESIGN OF RCC ABUTMENT             *");
            }
            else
            {
                sw.WriteLine("\t\t*          DESIGN OF RCC CANTILEVER           *");
                sw.WriteLine("\t\t*                RETAINING WALL               *");
            }
            sw.WriteLine("\t\t*                                             *");
            sw.WriteLine("\t\t***********************************************");
            sw.WriteLine("\t\t----------------------------------------------");
            sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            sw.WriteLine("\t\t----------------------------------------------");

            #endregion

            try
            {
                //sw.WriteLine("DESIGN OF RCC ABUTMENT");
                //sw.WriteLine("----------------------");
                sw.WriteLine();
                sw.WriteLine();

                #region USER Data
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Refer to  Diagram");
                sw.WriteLine();
                sw.WriteLine("Depth of Girder Seat [d1] = {0} m.             Marked as d1 in the Drawing", d1.ToString("0.000"));
                _d1 = string.Format("{0}", d1 * 1000);

                sw.WriteLine("Thickness of wall [t] = {0} m", t);
                sw.WriteLine("Height of Retained Earth [H] = {0} m           Marked as H in the Drawing", H);
                _H = string.Format("{0}", H * 1000);

                sw.WriteLine("Width of wall [B] = {0} m", B);
                sw.WriteLine("Equivalent height of Earth for Live Load Surcharge [d2] = {0} m", d2);
                sw.WriteLine("Thickness of Approach Slab [d3] = {0} m        Marked as d3 in the Drawing", d3);
                _d3 = string.Format("{0}", d3 * 1000);


                sw.WriteLine("Length of base in back of wall [L1] = {0} m   Marked as L1 in the Drawing", L1);
                _L1 = string.Format("{0}", L1 * 1000);

                sw.WriteLine("Length of base in wall location [L2] = {0} m   Marked as L2 in the Drawing", L2);
                _L2 = string.Format("{0}", L2 * 1000);

                sw.WriteLine("Length of base at front of wall [L3]  = {0} m   Marked as L3 in the Drawing", L3);
                _L3 = string.Format("{0}", L3 * 1000);

                sw.WriteLine("Total Length of Base [D]= L1 + L2 + L3 = {0} m Marked as D in the Drawing", (L1 + L2 + L3));
                _D = string.Format("{0}", (L1 + L2 + L3) * 1000);

                sw.WriteLine("Thickness of wall at the Top [L4] = {0} m      Marked as L4 in the Drawing", L4);
                _L4 = string.Format("{0}", L4 * 1000);

                sw.WriteLine("Thickness of Base [d4] = {0:f2} m              Marked as d4 in the Drawing", d4);
                _d4 = string.Format("{0}", d4 * 1000);

                sw.WriteLine("Angle between wall and Horizontal base on Earth side [θ] = {0}°", theta);
                sw.WriteLine();
                sw.WriteLine("Inclination of Earth fill side with the Horizontal [δ] = {0}°", delta);
                sw.WriteLine();
                sw.WriteLine("Angle of friction between Earth and Wall [z] = {0}°", z);
                sw.WriteLine();
                sw.WriteLine("Coefficient of friction between Earth and wall [µ] = {0}", mu);
                sw.WriteLine();
                sw.WriteLine("Unit weight of Back fill Earth [γ_b] = {0} kN/cu.m", gamma_b);
                sw.WriteLine();
                sw.WriteLine("Unit weight of Concrete [γ_c] = {0} kN/cu.m", gamma_c);
                sw.WriteLine();
                sw.WriteLine("Angle of Internal friction of backfill [φ] = {0}°", phi);
                sw.WriteLine();
                sw.WriteLine("Bearing Capacity [p] = {0} kN/sq.m", p);
                sw.WriteLine();
                sw.WriteLine("Concrete Grade [f_ck] = M {0:f0} = {0:f0}", f_ck);
                sw.WriteLine();
                sw.WriteLine("Steel Grade [f_y] = Fe {0:f0} = {0:f0}", f_y);
                sw.WriteLine();
                sw.WriteLine("Live Load from vehicles [w6] = {0:f2} kN/m", w6);
                sw.WriteLine();
                sw.WriteLine("Permanent Load from Super Structure [w5] = {0:f2} kN/m", w5);
                sw.WriteLine();
                sw.WriteLine("Vehicle Braking Force [F] = {0:f2} kN", F);
                sw.WriteLine();
                sw.WriteLine("Bending Moment and Shear Force Factor [Fact] = {0:f2}", factor);
                sw.WriteLine();
                sw.WriteLine("Reinf. Clear Cover [cover] = {0:f2} mm", cover);
                sw.WriteLine();
                sw.WriteLine("Seismic Coefficient [sc] = {0:f2} mm", sc);
                #endregion

                double rad = Math.PI / 180;

                phi = phi * rad;
                delta = delta * rad;
                z *= rad;
                theta *= rad;



                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                #region STEP 1
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : Approximate Sizing (dimensions)");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Using Rankine's formula for the depth of foundation base");
                sw.WriteLine();

                double hd = (p / gamma_b) * Math.Pow(((1 - Math.Sin(phi)) / (1 + Math.Sin(phi))), 2.0);
                sw.WriteLine("hd = (p/γ_b) * ((1 - Sin φ)/(1 + Sin φ))^2");
                sw.WriteLine("   = ({0}/{1}) * ((1 - Sin {2})/(1 + Sin {2}))^2",
                    p,
                    gamma_b,
                    phi / rad);

                if (hd < a)
                    sw.WriteLine("   = {0} m < a = {1} m , OK", hd.ToString("0.000"), a.ToString("0.000"));
                else
                    sw.WriteLine("   = {0} m > a = {1} m, NOT OK", hd.ToString("0.000"), a.ToString("0.000"));

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Assuming Thickness of base to be 10% of total height {0} m", H);

                double d = d4;
                sw.WriteLine();
                sw.WriteLine("Let us Provide thickness of base = d = {0} cm = {1} m",
                    d * 100,
                    d);

                sw.WriteLine();
                sw.WriteLine("Length of base = l = H √((Ca * Cos δ)/((1 - m) * (1 + 3 * m)))"); //Ca = ?
                //TO DO

                sw.WriteLine();
                sw.WriteLine("Angle of Back fill is horizontal, δ = {0}, m = 1 - (4/(9*q))", delta);
                sw.WriteLine();

                double q = (gamma_b / p) * (H - d);
                sw.WriteLine("where q = γ*h/p = {0}/{1} * (H - d) = {0}/{1} * ({2} - {3}) = {4}",
                    gamma_b,
                    p,
                    H,
                    d,
                    q.ToString("0.000"));
                double Ca = 0.0;
                Ca = (1 - Math.Sin(phi)) / (1 + Math.Sin(phi));
                sw.WriteLine("     Ca = (1 - Sin φ)/(1 + Sin φ)) ");
                sw.WriteLine("        = (1 - Sin {0:f0})/(1 + Sin {0:f0})", phi / rad);
                sw.WriteLine("        = {0:f3} ", Ca);
                sw.WriteLine();

                double m = 1 - (4 / (9 * q));
                sw.WriteLine("      m = 1 - (4/(9*{0:f3}))", q);
                sw.WriteLine("        = {0} ", m.ToString("0.000"));


                double l = 0.0;
                l = H * (Math.Sqrt(Math.Abs((Ca * Math.Cos(delta)) / ((1 - m) * (1 + 3 * m)))));
                //sw.WriteLine("Length of base = l = H √((Ca * Cos δ)/(1 - m) * (1 + 3 * m))");
                sw.WriteLine("      l = {0} √(({1} * Cos {2})/(1 - {3}) * (1 + 3 * {3}))",
                    H.ToString("0.000"),
                    Ca.ToString("0.000"),
                    delta / rad,
                    m.ToString("0.000"));
                sw.WriteLine("        = {0:f2} ", l);
                double provided_l = L1 + L2 + L3;
                sw.WriteLine();
                sw.WriteLine("Provided l = L1 + L2 + L3 = {0} + {1} + {2} = {3} m",
                    L1, L2, L3, provided_l);

                double ml = m * provided_l;
                sw.WriteLine();
                sw.WriteLine("        ml = {0:f2} * {1:f2} = {2:f2}",
                    m,
                    provided_l,
                    ml);
                double adopting_average_thickness = t; //m 100 cm
                sw.WriteLine();
                sw.WriteLine("Adopting average thickness of wall = {0:f2} cm = {1:f2} m",
                    t * 100,
                    t);

                sw.WriteLine();
                sw.WriteLine();

                #endregion

                #region STEP 2

                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : Stability Check Weight of wall ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Weight of Right part of Stem Wall");
                sw.WriteLine();
                double w1 = (H - d1 - d4) * (L2 - L4) * gamma_c;
                sw.WriteLine("   w1 = (H - d1 - d4) * (L2 - L4) * γ_c");
                sw.WriteLine("      = ({0} - {1} - {2}) * ({3} - {4}) * {5}",
                    H,
                    d1,
                    d4,
                    L2,
                    L4,
                    gamma_c);
                sw.WriteLine("      = {0:f3} kN", w1);

                sw.WriteLine();
                sw.WriteLine("Distance of its centroid from Toe");
                sw.WriteLine();
                double D1 = ((L2 - L4) / 2) + L3;
                sw.WriteLine("    D1 = (L2-L4)/2 + L3");
                sw.WriteLine("       = ({0}-{1})/2 + {2} = {3:f3} m",
                    L2,
                    L4,
                    L3,
                    D1);

                sw.WriteLine();
                sw.WriteLine("Weight of Left part of Stem Wall");
                sw.WriteLine();
                double w2 = (H - d4) * L4 * gamma_c;
                sw.WriteLine("    w2 = (H - d4) * L4 * γ_c");
                sw.WriteLine("       = ({0} - {1}) * {2} * {3}",
                    H,
                    d4,
                    L4,
                    gamma_c);
                sw.WriteLine("       = {0} kN", w2);

                double D2 = L4 / 2 + (L2 - L4) + L3;
                sw.WriteLine();
                sw.WriteLine("Distance of its centroid from Toe");
                sw.WriteLine();
                sw.WriteLine("    D2 = L4 / 2 + (L2 - L4) + L3");
                sw.WriteLine("       = {0} / 2 + ({1} - {0}) + {2}",
                    L4,
                    L2,
                    L3);
                sw.WriteLine("       = {0} m", D2);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Weight of Base");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double w3 = (L1 + L2 + L3) * d4 * gamma_c;
                sw.WriteLine("    w3 = (L1 + L2 + L3) * d3 * γ_c");
                sw.WriteLine("       = ({0} + {1} + {2}) * {3} * {4}",
                    L1,
                    L2,
                    L3,
                    d4,
                    gamma_c);
                sw.WriteLine("       = {0:f2} kN", w3);
                double D3 = provided_l / 2;
                sw.WriteLine();
                sw.WriteLine("Distance of its centroid from Toe");
                sw.WriteLine();
                sw.WriteLine("    D3 = (L1+L2+L3)/2 = {0} m", D3);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Weight of Earth on Heel Slab");
                sw.WriteLine("------------------------------------------------------------");

                double w4 = (H - d4) * L1 * gamma_b;
                sw.WriteLine();
                sw.WriteLine("   w4 = (H - d4) * L1 * γ_b");
                sw.WriteLine("      = ({0} - {1}) * {2} * {3}",
                    H,
                    d4,
                    L1,
                    gamma_b);
                sw.WriteLine();
                sw.WriteLine("      = {0:f2} kN", w4);

                double D4 = (L1 / 2) + L2 + L3;
                sw.WriteLine();
                sw.WriteLine("Distance of its centroid from Toe");
                sw.WriteLine();
                sw.WriteLine("   D4 = (L1 / 2) + L2 + L3");
                sw.WriteLine("      = ({0} / 2) + {1} + {2}",
                    L1,
                    L2,
                    L3);
                sw.WriteLine("      = {0:f2} m", D4);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Permanent Load from Super Structure = w5 = {0} kN", w5);
                sw.WriteLine();
                sw.WriteLine("Distance of its centroid from Toe");
                sw.WriteLine();
                double D5 = L3 + ((L2 - L4) / 2);
                sw.WriteLine("                                  D5 = L3 + ((L2 - L4) / 2)");
                sw.WriteLine("                                     = {0} + (({1} - {2}) / 2)",
                    L3,
                    L2,
                    L4);
                sw.WriteLine("                                     = {0:f2} m", D5);
                sw.WriteLine();
                sw.WriteLine("Vertical Live Load from Vehicle = w6 = {0} kN", w6);
                double D6 = L3 + (L2 - L4) / 2;
                sw.WriteLine();
                sw.WriteLine("Distance of its centroid from Toe");
                sw.WriteLine();
                sw.WriteLine("                                  D6 = L3 + (L2 - L4) / 2");
                sw.WriteLine("                                     = {0} + ({1} - {2}) / 2",
                    L3,
                    L2,
                    L4);
                sw.WriteLine("                                        = {0:f2} m", D6);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Force due to  braking");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Braking Force = F = {0} kN", F);

                double one_abut_force = F / 2;
                sw.WriteLine();
                sw.WriteLine("Force on one Abutment wall = {0}/2 = {1} kN",
                    F,
                    one_abut_force.ToString("0.0"));
                sw.WriteLine();
                sw.WriteLine("Transverse Width of Abutment wall = B = {0} m", B);

                sw.WriteLine();
                double P2 = one_abut_force / B;
                sw.WriteLine("Horizontal Force per m. of wall = P2 = {0:f2}/{1:f2} = {2:f3} kN/m",
                    one_abut_force,
                    B,
                    P2);

                double d7 = H - d1;
                sw.WriteLine();
                sw.WriteLine("Height of the Force above Toe Level");
                sw.WriteLine();
                sw.WriteLine("D7 = H - d1 = {0} - {1} = {2:f3} m",
                    H,
                    d1,
                    d7);
                //double h1 = 1.2;
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Break is applied at height = h1 = {0} m", h1);// ? to be check
                sw.WriteLine("Span of Longitudinal Girder = {0} m = L", L);// ? to be check

                sw.WriteLine();
                sw.WriteLine("Vertical break force reaction on one abutment");
                double w7 = (F * (h1 + d1 + d3)) / (L * B);
                sw.WriteLine("      W7 = (F * (h1 + d1 + d3)) / (L * B)");
                sw.WriteLine("         = ({0} * ({1} + {2} + {3})) / ({4} * {5})",
                    F,
                    h1,
                    d1,
                    d3,
                    L,
                    B);
                sw.WriteLine("         = {0:f2} kN/m", w7);

                double D8 = L3 + (L2 - L4) / 2;
                sw.WriteLine();
                sw.WriteLine("Distance of its centroid from Toe");
                sw.WriteLine();
                sw.WriteLine("D8 = L3 + (L2 - L4) / 2");
                sw.WriteLine("   = {0} + ({1} - {2}) / 2",
                    L3,
                    L2,
                    L4);
                sw.WriteLine("   = {0:f2} m", D8);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Active Earth Pressure");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("P1 = 0.5 * H * H * γ_b * ka");

                //double radian = Math.PI / 180;
                double radian = 1;

                double Ka = (Math.Sin(theta * radian - phi * radian) / Math.Sin(theta * radian)) * (1 / (Math.Sqrt(Math.Sin(theta * radian + z * radian)) +
                    (Math.Sqrt((Math.Sin(phi * radian + z * radian)) * (Math.Sin(phi * radian - delta * radian)) / (Math.Sin(theta * radian - delta * radian))))));

                //sw.WriteLine("P1 = 0.5 * H * H * gamma_b * ka");
                //sw.WriteLine("γσµφδπρ√τ≈αβθ = 0.5 * H * H * gamma_b * ka");
                sw.WriteLine();
                sw.WriteLine("[ θ = {0}° , φ = {1}°, z = {2}°, δ = {3}",
                    (theta / rad),
                    phi / rad,
                    z / rad,
                    delta / rad);
                sw.WriteLine();
                sw.WriteLine("ka = (Sin(θ - φ) / Sin(θ)) / ((√(Sin(θ + z)) + (√(Sin(φ + z) * Sin(φ - δ)/Sin(θ - δ)))");
                sw.WriteLine("   = {0:f3}", Ka);
                sw.WriteLine();

                double P1 = 0.5 * H * H * gamma_b * Ka;

                sw.WriteLine("P1 = 0.5 * {0} * {0} * {1} * {2:f3} = {3:f2} kN/m",
                    H, gamma_b, Ka, P1);

                sw.WriteLine();
                double D9 = 0.42 * H;
                sw.WriteLine("Height of the Force above Toe Level");
                sw.WriteLine();
                sw.WriteLine("D9 = 0.42 * H = 0.42 * {0} = {1} m", H, D9.ToString("0.000"));
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Load from Vehicle and Approach Slab");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                double h2 = d2;

                sw.WriteLine("Equivalent height of earth for Vehicle Load Surcharge = d2 = {0:f2} m", d2);

                double hor_force_vehicle = d2 * gamma_b * Ka * H;
                sw.WriteLine();
                sw.WriteLine("Horizontal force for Vehicle Load Surcharge = d2 * γ_b * Ka * H");
                sw.WriteLine("                                            = {0:f2} * {1} * {2:f4} * {3:f2}",
                    h2,
                    gamma_b,
                    Ka,
                    H);
                sw.WriteLine("                                            = {0:f2} ", hor_force_vehicle);

                double hor_force_approach_slab = d3 * gamma_c * Ka * H;
                sw.WriteLine();
                sw.WriteLine("Horizontal force for Approach slab = d3 * γ_c * ka * H");
                sw.WriteLine("                                   = {0} * {1} * {2:f3} * {3}",
                    d3, gamma_c, Ka, H);
                sw.WriteLine("                                   = {0:f2} ", hor_force_approach_slab);

                double P3_total_hor_force = hor_force_approach_slab + hor_force_vehicle;
                sw.WriteLine();
                sw.WriteLine("Total Horizontal Force = {0:f3} + {1:f3} = {2:f3} kN/m.",
                    hor_force_vehicle,
                    hor_force_approach_slab,
                    P3_total_hor_force);
                double D10 = H / 2.0;
                sw.WriteLine();
                sw.WriteLine("Height of the Force above Toe Level");
                sw.WriteLine();
                sw.WriteLine("D10 = H / 2.0 = {0} / 2.0 = {1} m",
                    H,
                    D10);
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("Verticle Force for Vehicle Load Surcharge");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                //d3
                double w8 = (h2 * gamma_b + d3 * gamma_c) * L1;
                sw.WriteLine("w8 = (h2 * γ_b + d3 * γ_c) * L1;");
                sw.WriteLine("   = ({0:f2} * {1} + {2:f2} * {3}) * {4:f2}",
                    h2, gamma_b, d3, gamma_c, L1);
                sw.WriteLine("   = {0:f2} kN/m", w8);

                double D11 = (L1 / 2) + L2 + L3;
                sw.WriteLine();
                sw.WriteLine("Distance of its centroid from Toe");
                sw.WriteLine();
                sw.WriteLine("D11 = (L1 / 2) + L2 + L3");
                sw.WriteLine("    = ({0:f2} / 2) + {1:f2} + {2:f2}",
                    L1,
                    L2,
                    L3);
                sw.WriteLine("    = {0:f2} m.",
                    D11);

                sw.WriteLine();
                sw.WriteLine();


                string table_format = "{0,3}{1,-27}{2,11}{3,10}{4,10}  {5,-16}{6,10}{7,10}{8,10}";
                sw.WriteLine("_______________________________________________________________________________________________________________");
                sw.WriteLine(table_format,
                               "", "", "V   ", "H:Long", "H:Trans", "Distance  ", "Mv  ", "Mh:Long", "Mh:Trans");
                sw.WriteLine(table_format,
                               "", "", "(kN) ", "(kN)  ", "(kN)  ", "/Height (m)", "(kN-m)", "(kN-m)", "(kN-m) ");
                sw.WriteLine("_______________________________________________________________________________________________________________");

                double Mv1 = (w1 * D1);
                sw.WriteLine(table_format,
                                  "1.",
                                  "Self Weight (w1)",
                                  w1.ToString("0.000"),
                                  "",
                                  "",
                                  "D1=" + D1.ToString("0.00"),
                                  Mv1.ToString("0.000"),
                                  "",
                                  "",
                                  "");

                double Mv2 = (w2 * D2);
                sw.WriteLine(table_format,
                                     "2.",
                                     "Self Weight (w2)",
                                     w2.ToString("0.000"),
                                     "",
                                     "",
                                     "D2=" + D2.ToString("0.00"),
                                     Mv2.ToString("0.000"),
                                     "",
                                     "",
                                     "");

                double Mv3 = (w3 * D3);
                sw.WriteLine(table_format,
                                  "3.",
                                  "Self Weight (w3)",
                                  w3.ToString("0.000"),
                                  "",
                                  "",
                                  "D3=" + D3.ToString("0.00"),
                                  Mv3.ToString("0.000"),
                                  "",
                                  "",
                                  "");

                double Mv4 = (w4 * D4);
                sw.WriteLine(table_format,
                                  "4.",
                                  "Weight of ",
                                  "",
                                  "",
                                  "",
                                  "",
                                  "",
                                  "",
                                  "");
                sw.WriteLine(table_format,
                                  "",
                                  "Earth on Heel Slab (w4)",
                                  w4.ToString("0.000"),
                                  "",
                                  "",
                                  "D4=" + D4.ToString("0.00"),
                                  Mv4.ToString("0.000"),
                                  "",
                                  "",
                                  "");

                double Mv5 = (w5 * D5);
                //sw.WriteLine("{0,5}{1,-27}{2,15}{3,13}{4,13}{5,13}{6,13}",
                sw.WriteLine(table_format,
                                   "5.",
                                   "Permanent Load from ",
                                   "",
                                   "",
                                   "",
                                   "",
                                   "",
                                   "",
                                   "");
                sw.WriteLine(table_format,
                                                 "",
                                                 "Super Structure (w5)",
                                                 w5.ToString("0.000"),
                                                 "",
                                                 "",
                                                 "D5=" + D5.ToString("0.00"),
                                                 Mv5.ToString("0.000"),
                                                 "",
                                                 "",
                                                 "");

                double Mh1 = (P1 * D9);
                sw.WriteLine(table_format,
                                  "6.",
                                  "Active Earth Pressure (P1)",
                                  "",
                                  P1.ToString("0.000"),
                                  "",
                                  "D9=" + D9.ToString("0.00"),
                                  "",
                                  Mh1.ToString("0.000"),
                                  "");

                double Mv7 = (w8 * D11);
                sw.WriteLine(table_format,
                                  "7.",
                                  "Vertical Load for ",
                                  "",
                                  "",
                                  "",
                                  "",
                                  "",
                                  "",
                                  "");
                sw.WriteLine(table_format,
                                                  "",
                                                  "Vehicle Load Surcharge (w8)",
                                                  w8.ToString("0.000"),
                                                  "",
                                                  "",
                                                  "D11=" + D11.ToString("0.00"),
                                                  Mv7.ToString("0.000"),
                                                  "",
                                                  "");

                double Mh2 = (P3_total_hor_force * D10);
                sw.WriteLine(table_format,
                                  "8.",
                                  "Horizontal Force for ",
                                  "",
                                  "",
                                  "",
                                  "",
                                  "",
                                  "",
                                  "");

                sw.WriteLine(table_format,
                                  "",
                                  "Vehicle Load Surcharge (P3)",
                                  "",
                                  P3_total_hor_force.ToString("0.000"),
                                  "",
                                  "D10=" + D10.ToString("0.00"),
                                  "",
                                  Mh2.ToString("0.000"),
                                  "");


                sw.WriteLine();
                sw.WriteLine(table_format,
                                 "9.",
                                 "Seismic Force in ",
                                 "",
                                 "",
                                 "",
                                 "",
                                 "",
                                 "",
                                 "");
                sw.WriteLine(table_format,
                                 "",
                                 "Longitudinal Direction ",
                                 "",
                                 "",
                                 "",
                                 "",
                                 "",
                                 "",
                                 "");

                double PSL = ((w1 + w2 + w3 + w4 + w5) * sc) / 2.0;
                sw.WriteLine(table_format,
                                "",
                                "=((w1+w2+w3+w4+w5)*sc)/2",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "");
                double D12 = H - d1;
                sw.WriteLine(table_format,
                                "",
                                string.Format("=({0:f3}*{1})/2.0 = PSL", (w1 + w2 + w3 + w4 + w5), sc, ""),
                                "",
                                PSL.ToString("f2"),
                                "",
                                "D12=H-d1",
                                "",
                                string.Format("{0:f2}*{1}", PSL, D12),
                                "");
                double Mh3 = PSL * D12;
                sw.WriteLine(table_format,
                               "",
                               "",
                               "",
                               "",
                               "",
                               string.Format("={0:f2}-{1:f2}", H, d1),
                               "",
                               string.Format("={0:f2}", Mh3),
                               "");
                sw.WriteLine(table_format,
                              "",
                              "",
                              "",
                              "",
                              "",
                              string.Format("={0:f2}", D12),
                              "",
                              "",
                              "");
                sw.WriteLine();
                sw.WriteLine(table_format,
                                 "10.",
                                 "Seismic Force in ",
                                 "",
                                 "",
                                 "",
                                 "",
                                 "",
                                 "",
                                 "");
                sw.WriteLine(table_format,
                                 "",
                                 "Transverse Direction ",
                                 "",
                                 "",
                                 "",
                                 "",
                                 "",
                                 "",
                                 "");

                double PST1 = ((w1 + w2 + w3 + w4 + w5 + w8) * sc) / 2.0;
                sw.WriteLine(table_format,
                                "",
                                "=((w1+w2+w3+w4+w5+w8)*sc)/2",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "",
                                "");
                double D13 = B;
                sw.WriteLine(table_format,
                                "",
                                string.Format("=({0:f3}*{1})/2.0 = PST1", (w1 + w2 + w3 + w4 + w5 + w8), sc, ""),
                                "",
                                "",
                                PST1.ToString("f2"),
                                "D13=B",
                                "",
                                "",
                                string.Format("{0:f2}*{1}", PST1, D13));
                double Mh10 = PST1 * D13;
                sw.WriteLine(table_format,
                               "",
                               "",
                               "",
                               "",
                               "",
                              "=" + D13,
                               "",
                               "",
                               string.Format("={0:f2}", Mh10));


                sw.WriteLine("_______________________________________________________________________________________________________________");
                double V1 = w1 + w2 + w3 + w4 + w5 + w8;
                double H1 = P1 + P3_total_hor_force + PSL;
                double MV1_SUM = Mv1 + Mv2 + Mv3 + Mv4 + Mv5 + Mv7;
                double MH1_SUM = Mh1 + Mh2 + Mh3;
                if (IsPropped)
                {
                    sw.WriteLine(table_format,
                             "Sum",
                             " of Items in",
                              "V1=",
                             "H1:Long",
                             "H1:Trans",
                             "",
                             "MV1=",
                             "MH1:Long",
                             "MH1:Trans");

                    sw.WriteLine(table_format,
                                   "Spa",
                                  "n Unloaded Condition",
                                  "" + V1.ToString("0.00"),
                                  "" + H1.ToString("0.00"),
                                  "" + PST1.ToString("0.00"),
                                  "",
                                  "" + MV1_SUM.ToString("0.00"),
                                  "" + MH1_SUM.ToString("0.00"),
                                  "" + Mh10.ToString("0.000"));
                }
                else
                {
                    sw.WriteLine(table_format,
                             "Sum",
                             " of Items",
                              "V1=",
                             "H1:Long",
                             "H1:Trans",
                             "",
                             "MV1=",
                             "MH1:Long",
                             "MH1:Trans");

                    //sw.WriteLine(table_format,
                    //               "Spa",
                    //              "n Unloaded Condition",
                    //              "" + V1.ToString("0.00"),
                    //              "" + H1.ToString("0.00"),
                    //              "" + PST1.ToString("0.00"),
                    //              "",
                    //              "" + MV1_SUM.ToString("0.00"),
                    //              "" + MH1_SUM.ToString("0.00"),
                    //              "" + Mh10.ToString("0.000"));
                }
                sw.WriteLine();
                sw.WriteLine(table_format,
                               "Des",
                              "ign Values",
                              "V1=",
                              "H1=",
                              "",
                              "",
                              "MV1=",
                              "MH1",
                              "");

                H1 = H1 > PST1 ? H1 : PST1;
                //Chiranjit [2013 02 25]
                //MH1_SUM = MH1_SUM > Mh10 ? MH1_SUM : Mh10;
                sw.WriteLine(table_format,
                               "",
                              "",
                              "" + V1.ToString("0.00"),
                              "" + H1.ToString("0.00"),
                              "",
                              "",
                              "" + MV1_SUM.ToString("0.00"),
                              "" + MH1_SUM.ToString("0.00"),
                              "");
                sw.WriteLine("_______________________________________________________________________________________________________________");
                double Mh12 = (P2 * d7);

                double V2, H2, MV2_SUM, MH2_SUM,Mv8,Mv9,D14,PST2,Mh14;


                V2 = H2 = MV2_SUM = MH2_SUM = 0.0;
                Mv8 = Mv9 = D14 = PST2 = Mh14 = 0.0;


                if (IsPropped)
                {
                    sw.WriteLine(table_format,
                                                      "11.",
                                                      "Horizontal ",
                                                      "",
                                                      "",
                                                      "",
                                                      "",
                                                      "",
                                                      "",
                                                      "");

                    sw.WriteLine(table_format,
                                                      "",
                                                      "Break Force (P2)",
                                                      "",
                                                      P2.ToString("0.000"),
                                                      "",
                                                      "D7=" + d7.ToString("0.00"),
                                                      "",
                                                      Mh12.ToString("0.000"),
                                                      "");

                     Mv8 = (w7 * D8);
                    sw.WriteLine(table_format,
                                                      "12.",
                                                      "Vehicle",
                                                      "",
                                                      "",
                                                      "",
                                                      "",
                                                      "",
                                                      "",
                                                      "");

                    sw.WriteLine(table_format,
                                                      "",
                                                      "Braking Force (w7)",
                                                      w7.ToString("0.000"),
                                                      "",
                                                      "",
                                                      "D8=" + D8.ToString("0.00"),
                                                      Mv8.ToString("0.000"),
                                                      "",
                                                      "");

                     Mv9 = (w6 * D6);
                    sw.WriteLine(table_format,
                                                      "13.",
                                                      "Vehicle Load from ",
                                                      "",
                                                      "",
                                                      "",
                                                      "",
                                                      "",
                                                      "",
                                                      "");

                    sw.WriteLine(table_format,
                                                     "",
                                                     "Super Structure (w6)",
                                                     w6.ToString("0.000"),
                                                     "",
                                                     "",
                                                     "D6=" + D6.ToString("0.00"),
                                                     Mv9.ToString("0.000"),
                                                     "",
                                                     "");

                    sw.WriteLine();
                     D14 = H + d3;
                    sw.WriteLine(table_format,
                                                      "14.",
                                                      "Additional Seismic Force",
                                                      "",
                                                      "",
                                                      "",
                                                      "D14",
                                                      "",
                                                      "",
                                                      "");

                    sw.WriteLine(table_format,
                                                      "",
                                                      "Transverse Direction = PST2",
                                                      "",
                                                      "",
                                                      "",
                                                      "=H+d3",
                                                      "",
                                                      "",
                                                      "");
                     PST2 = ((w6 + w7) * sc) / 2.0;
                    sw.WriteLine(table_format,
                                                      "",
                                                      "=((w6+w7)*sc)/2.0",
                                                      "",
                                                      "",
                                                      "",
                                                      string.Format("={0:f2} + {1:f2}", H, d3),
                                                      "",
                                                      "",
                                                      "");
                     Mh14 = PST2 * D14;
                    sw.WriteLine(table_format,
                                                      "",
                                                      string.Format("=(({0:f2}+{1:f2})*{2})/2.0", w6, w7, sc),
                                                      "",
                                                      "",
                                                      "",
                                                      string.Format("={0:f2}", D14),
                                                      "",
                                                      "",
                                                      string.Format("{0:f2}*{1:f2}", PST2, D14));

                    sw.WriteLine(table_format,
                                                      "",
                                                      string.Format("={0:f2}", PST2),
                                                      "",
                                                      "",
                                                      "",
                                                      "",
                                                      "",
                                                      "",
                                                      Mh14.ToString("f2"));

                    //sw.WriteLine(table_format,
                    //                                 "",
                    //                                 "Super Structure (w6)",
                    //                                 w6.ToString("0.000"),
                    //                                 "",
                    //                                 "",
                    //                                 "D6=" + D6.ToString("0.00"),
                    //                                 Mv9.ToString("0.000"),
                    //                                 "",
                    //                                 "");

                    sw.WriteLine("_______________________________________________________________________________________________________________");
                     V2 = V1 + w7 + w6;
                     H2 = H1 + P2;
                     MV2_SUM = MV1_SUM + Mv8 + Mv9;
                     MH2_SUM = MH1_SUM + Mh12;



                    Mh14 += Mh10;
                    PST2 += PST1;
                    sw.WriteLine(table_format,
                                                     "Sum",
                                                     " of Items in",
                                                     "V2=",
                                                     "H2:Long",
                                                     "H2:Trans",
                                                     "",
                                                     "MV2=",
                                                     "MH2:Long",
                                                     "MH2:Trans");
                    sw.WriteLine(table_format,
                                                      "Spa",
                                                      "n Loaded Condition",
                                                      "" + V2.ToString("0.00"),
                                                      "" + H2.ToString("0.00"),
                                                      "" + PST2.ToString("0.00"),
                                                      "",
                                                      "" + MV2_SUM.ToString("0.00"),
                                                      "" + MH2_SUM.ToString("0.00"),
                                                      "" + Mh14.ToString("0.000"));
                    sw.WriteLine();
                    sw.WriteLine(table_format,
                                   "Des",
                                  "ign Values",
                                  "V2=",
                                  "H2=",
                                  "",
                                  "",
                                  "MV2=",
                                  "MH2",
                                  "");

                    H2 = H2 > PST2 ? H2 : PST2;
                    //Chiranjit [2013 02 25]
                    //MH2_SUM = MH2_SUM > Mh14 ? MH2_SUM : Mh14;
                    sw.WriteLine(table_format,
                                   "",
                                  "",
                                  "" + V2.ToString("0.00"),
                                  "" + H2.ToString("0.00"),
                                  "",
                                  "",
                                  "" + MV2_SUM.ToString("0.00"),
                                  "" + MH2_SUM.ToString("0.00"),
                                  "");
                    sw.WriteLine("_______________________________________________________________________________________________________________");
                }
                sw.WriteLine("_______________________________________________________________________________________________________________");

                #endregion

                #region STEP 3
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : Check for Stability against Overturning");
                sw.WriteLine("------------------------------------------------------------");
                if (!IsPropped)
                {
                    V2 = V1;
                    H2 = H1;
                    PST2 = PST1;
                    MV2_SUM = MV1_SUM;
                    MH2_SUM = MH1_SUM;
                }
                double safety_factor, Xo, emax, e1;
                Xo = (MV2_SUM - MH2_SUM) / V2;
                emax = (L1 + L2 + L3) / 6.0;
                e1 = (L1 + L2 + L3) / 2 - Xo;

                #region CASE I
                sw.WriteLine();

                if (IsPropped)
                {
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("CASE I : Span Unloaded Condition");
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                }
                sw.WriteLine("Overturning Moment about toe = MH1 = {0}", MH1_SUM.ToString("0.000"));
                sw.WriteLine();
                sw.WriteLine("Restoring Moment about toe   = MV1 = {0}", MV1_SUM.ToString("0.000"));

                safety_factor = MV1_SUM / MH1_SUM;
                sw.WriteLine();
                if (safety_factor >= 2.0)
                {
                    sw.WriteLine("Factor of Safety against overturning = MV1/MH1 = {0} / {1} = {2} > 2.0 , OK",
                        MV1_SUM.ToString("0.000"),
                        MH1_SUM.ToString("0.000"),
                        safety_factor.ToString("0.000"));
                }
                else
                {
                    sw.WriteLine("Factor of Safety against overturning = {0} / {1} = {2} < 2.0, NOT OK",
                        MV1_SUM.ToString("0.00"),
                        MH1_SUM.ToString("0.00"),
                        safety_factor.ToString("0.000"));
                }

                sw.WriteLine();
                sw.WriteLine("Location of Resultant for toe = Xo = (MV1 - MH1)/V1");
                Xo = (MV1_SUM - MH1_SUM) / V1;
                sw.WriteLine("                                   = ({0:f3} - {1:f3})/{2:f3}", MV1_SUM, MH1_SUM, V1);
                sw.WriteLine("                                   =  {0:f3}", Xo);

                emax = (L1 + L2 + L3) / 6.0;
                sw.WriteLine();
                sw.WriteLine("Maximum permissible Eccentricity  = emax = (L1 + L2 + L3)/6.0");
                sw.WriteLine("                                         = ({0} + {1} + {2})/6.0",
                    L1,
                    L2,
                    L3);
                sw.WriteLine("                                         = {0:f4}", emax);

                double e2 = (L1 + L2 + L3) / 2 - Xo;

                sw.WriteLine();
                sw.WriteLine("Eccentricity of Resultant  =  e2 = (L1 + L2 + L3)/2 - Xo");
                sw.WriteLine("                                 = ({0} + {1} + {2})/2 - {3:f2}",
                    L1,
                    L2,
                    L3,
                    Xo);
                if (e2 < emax)
                    sw.WriteLine("                                 = {0} < {1}(emax) , OK", e2.ToString("0.000"), emax.ToString("0.000"));
                else
                {
                    sw.WriteLine("                                 = {0} > {1}(emax), NOT OK", e2.ToString("0.000"), emax.ToString("0.000"));
                    sw.WriteLine();
                    sw.WriteLine("Increase the length of base of wall on Earth Retaining Side,");
                    sw.WriteLine("more than present length of {0} m (L1)", L1);
                }
                #endregion CASE I

                #region CASE II

                if (IsPropped)
                {
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("CASE II : Span Loaded Condition");
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine("Overturning Moment about toe = MH2 = {0} kN-m.", MH2_SUM.ToString("0.000"));
                    sw.WriteLine("Restoring Moment about toe = MV2 = {0} kN-m.", MV2_SUM.ToString("0.000"));
                    sw.WriteLine();

                    safety_factor = MV2_SUM / MH2_SUM;
                    sw.WriteLine("Factor of Safety against overturning = MV2/MH2");

                    if (safety_factor > 2.0)
                    {
                        sw.WriteLine("                                     = {0}/{1} = {2} > 2.0 , OK",
                            MV2_SUM.ToString("0.000"),
                            MH2_SUM.ToString("0.000"),
                            safety_factor.ToString("0.000"));
                    }
                    else
                    {

                        sw.WriteLine("                                     = {0}/{1} = {2} < 2.0, NOT OK",
                            MV2_SUM.ToString("0.000"),
                            MH2_SUM.ToString("0.000"),
                            safety_factor.ToString("0.000"));
                        sw.WriteLine("Increase the Length of base wall on Earth Retaining Side,");
                        sw.WriteLine("more than present length of {0} m (L1)", L1);
                    }

                    sw.WriteLine();
                    sw.WriteLine("Location of Resultant from toe = Xo = (MV2 - MH2)/V2 ");
                    Xo = (MV2_SUM - MH2_SUM) / V2;
                    sw.WriteLine("                                    = ({0} - {1})/{2} = {3} m",
                        MV2_SUM.ToString("0.000"),
                        MH2_SUM.ToString("0.000"),
                        V2.ToString("0.000"),
                        Xo.ToString("0.000"));

                    emax = (L1 + L2 + L3) / 6.0;
                    sw.WriteLine();
                    sw.WriteLine("Maximum permissible Eccentricity = emax  = (L1 + L2 + L3)/6.0");
                    sw.WriteLine("                                         = ({0} + {1} + {2})/6.0",
                        L1,
                        L2,
                        L3);
                    sw.WriteLine("                                         = {0}", emax.ToString("0.000"));

                    e1 = (L1 + L2 + L3) / 2 - Xo;

                    sw.WriteLine();
                    sw.WriteLine("Eccentricity of Resultant = e1 = (L1 + L2 + L3)/2 - Xo");
                    sw.WriteLine("                               = ({0} + {1} + {2})/2 - {3:f2}",
                        L1,
                        L2,
                        L3,
                        Xo);
                    if (e1 < emax)
                        sw.WriteLine("                               = {0} < {1}(emax) , OK", e1.ToString("0.00"), emax.ToString("0.000"));
                    else
                    {
                        sw.WriteLine("                               = {0} > {1}(emax), NOT OK", e1.ToString("0.00"), emax.ToString("0.000"));
                        sw.WriteLine();
                        sw.WriteLine("Increase the length of base of wall on Earth Retaining Side,");
                        sw.WriteLine("more than present length of {0} m (L1)", L1);
                    }
                }
                #endregion CASE I

                #endregion


                #region STEP 4

                sw.WriteLine();
                sw.WriteLine();
                if (IsPropped)
                {
                    sw.WriteLine("-------------------------------------------------------------");
                    sw.WriteLine("STEP 4 : Check for Stresses at Base For Span Loaded Condition ");
                    sw.WriteLine("-------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("Total downward forces = V2 = {0:f3} kN", V2);
                }
                else
                {
                    sw.WriteLine("-------------------------------------------------------------");
                    sw.WriteLine("STEP 4 : Check for Stresses at Base ");
                    sw.WriteLine("-------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("Total downward forces = V2 = V1 = {0:f3} kN", V1);

                    sw.WriteLine("H2:Long = H1:Long = {0:f3} kN", H2);
                    sw.WriteLine("H2:Trans = H1:Trans = {0:f3} kN", PST2);
                }
                double val1, val2, Pr1, Pr5;

                sw.WriteLine();
                sw.WriteLine("Bearing Capacity = p = {0:f3} kN/sq.m.", p);
                sw.WriteLine();
                val1 = V2 / (L1 + L2 + L3);
                sw.WriteLine("Stress at base = V2/[(L1+L2+L3)*1.0]");
                sw.WriteLine("               = {0:f3}/{1} ", V2, (L1 + L2 + L3), (V2 / (L1 + L2 + L3)));
                if (val1 < p)
                {
                    sw.WriteLine("               = {0:f3} kN/sq.m < {1:f3} kN/sq.m, OK", val1, p);
                }
                else
                {
                    sw.WriteLine("               = {0:f3} kN/sq.m > {1:f3} kN/sq.m, NOT OK", val1, p);
                }

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Extreme Stresses at Base = (V2 / ((L1+L2+L3)*1.0)) *( 1.0 ± ((6 * e1)/(L1+L2+L3)))");
                sw.WriteLine("                         = ({0} / {1}*1.0) *( 1.0 ± ((6 * {2})/{1}))",
                    V2.ToString("0.000"),
                    provided_l,
                    e1.ToString("0.000"));

                val1 = V2 / provided_l;
                val2 = (6 * e1) / provided_l;

                //p2
                Pr1 = val1 * (1 + val2);
                Pr5 = val1 * (1 - val2);


                sw.WriteLine("                         = {0} * (1 ± {1})", val1.ToString("0.000"), val2.ToString("0.000"));
                sw.WriteLine();
                sw.WriteLine("                         = {0:f3} and {1:f3}", Pr1, Pr5);
                sw.WriteLine();
                sw.WriteLine("from the above equation,");
                sw.WriteLine();


                if (Pr1 > p)
                {
                    //sw.WriteLine("p1 < {0} kN = Bearing Capacity, OK", p);
                    //sw.WriteLine("             p1          = {0:f3} kN/sq.m > {1:f3}  kN/sq.m , NOT OK", Pr1, p);
                    sw.WriteLine("             p1          = {0:f3} kN/sq.m > {1:f3}  kN/sq.m ", Pr1, p); //Chiranjit [2013 07 01]
                }
                else
                {
                    //sw.WriteLine("p1 < {0} kN = Bearing Capacity, NOT OK", p);
                    sw.WriteLine("             p1          = {0:f3} kN/sq.m < {1:f3} kN/sq.m , OK", Pr1, p);
                }
                sw.WriteLine();
                sw.WriteLine();
                if (Pr5 >= p)
                {
                    sw.WriteLine("             p2          = {0:f3} kN/sq.m  >  {1:f3} kN/sq.m, NOT OK", Pr5, p);
                }
                else
                {
                    sw.WriteLine("             p2          = {0:f3} kN/sq.m  <  {1:f3} kN/sq.m, OK", Pr5, p);
                }
                //if (Pr1 < p)
                //{
                //    sw.WriteLine("p1 < {0} kN = Bearing Capacity, OK", p);
                //}
                //else
                //{
                //    sw.WriteLine("p1 > {0} kN = Bearing Capacity, NOT OK", p);
                //}
                //if (Pr5 >= 0)
                //{
                //    sw.WriteLine("p2 > 0 = Tension, OK");
                //}
                //else
                //    sw.WriteLine("p2 < 0 = No Tension, NOT OK");



                #endregion


                #region STEP 5
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 5 : Check for Sliding");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine("Longitudinal Sliding Force = H2:Long = {0} kN", H2.ToString("0.000"));
                double FF = mu * V2;
                sw.WriteLine();
                sw.WriteLine("Force resisting Sliding = µ * V2 = {0} * {1} = {2} = FF",
                    mu.ToString("0.00"),
                    V2.ToString("0.00"),
                    FF.ToString("0.000"));
                safety_factor = FF / H2;
                sw.WriteLine();
                if (safety_factor > 1.5)
                {
                    sw.WriteLine("Factor of Safety against Sliding = FF/H2:Long = {0}/{1} = {2} > 1.5 , OK",
                        FF.ToString("0.000"),
                        H2.ToString("0.000"),
                        safety_factor.ToString("0.000"));
                }
                else
                {
                    sw.WriteLine("Factor of Safety against Sliding = FF/H2:Long = {0}/{1} = {2} < 1.5 , NOT OK",
                        FF.ToString("0.000"),
                        H2.ToString("0.000"),
                        safety_factor.ToString("0.000"));
                    sw.WriteLine("Shear key will be required.");
                }

                sw.WriteLine();

                sw.WriteLine("Transverse Sliding Force = H2:Trans = {0} kN", PST2.ToString("0.000"));
                FF = mu * V2;
                sw.WriteLine();
                sw.WriteLine("Force resisting Sliding = µ * V2 = {0} * {1} = {2} = FF",
                    mu.ToString("0.00"),
                    V2.ToString("0.00"),
                    FF.ToString("0.000"));
                safety_factor = FF / PST2;
                sw.WriteLine();
                if (safety_factor > 1.5)
                {
                    sw.WriteLine("Factor of Safety against Sliding = FF/H2:Trans = {0}/{1} = {2} > 1.5 , OK",
                        FF.ToString("0.000"),
                        PST2.ToString("0.000"),
                        safety_factor.ToString("0.000"));
                }
                else
                {
                    sw.WriteLine("Factor of Safety against Sliding = FF/H2:Trans = {0}/{1} = {2} < 1.5 , NOT OK",
                        FF.ToString("0.000"),
                        PST2.ToString("0.000"),
                        safety_factor.ToString("0.000"));
                    sw.WriteLine("Shear key will be required.");
                }
                #endregion


                #region STEP 6
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 6 : Reinforcement Steel Bars");
                sw.WriteLine("------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine("Design of Base Slab at Front Toe for Steel requirements.");
                sw.WriteLine();
                sw.WriteLine("Thickness of Base Slab = d4 = {0:f3} m", d4);

                double deff = d4 - (cover / 1000);
                sw.WriteLine("Deff = d4 - cover = {0:f3} - {1:f3} = {2:f3} m",
                    d4,
                    (cover / 1000),
                    deff);

                double Pr2 = ((Pr1 - Pr5) / provided_l) * (provided_l - (L3 - deff));
                //Pr2 += Pr5;
                double Pr3 = ((Pr1 - Pr5) / provided_l) * (L1 + L2);
                //Pr3 += Pr5;
                double Pr4 = ((Pr1 - Pr5) / provided_l) * (L1);
                //Pr4 += Pr5;

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("ON BASE :");
                sw.WriteLine();
                sw.WriteLine("Pr1 = Upward pressure at Toe = {0} kN/sq.m.", Pr1.ToString("0.000"));
                sw.WriteLine();
                sw.WriteLine("Pr2 = Upward Pressure at a distance of effective depth from Front of wall = {0:F3} kN/sq.m.",
                                       Pr2);
                sw.WriteLine();
                sw.WriteLine("Pr3 = Upward Pressure at The Front Face of wall = {0:f3} kN/sq.m.",
                                    Pr3);

                sw.WriteLine();
                sw.WriteLine("Pr4 = Upward Pressure at The Backfill Face of wall = {0:f3} kN/sq.m.",
                                    Pr4);
                sw.WriteLine();
                sw.WriteLine("Pr5 = Upward Pressure at Heel = {0:f3} kN/sq.m.",
                                                    Pr5);
                double Dpr = d4 * gamma_c;
                sw.WriteLine();
                sw.WriteLine("Dpr = downward Pressure by Self weight of Base = {0:f2} * {1} = {2:f3} kN/sq.m.",
                    d4,
                    gamma_c,
                    Dpr);

                double Vu = factor * (((Pr1 + Pr2) / 2) - Dpr) * (L3 - deff);

                sw.WriteLine();
                Vu = Math.Abs(Vu);
                sw.WriteLine("Design Shear Force ");
                sw.WriteLine("   = Vu = Shear Force Factor * [((Pr1 + Pr2) / 2) - Dpr) * (L3 - deff)");
                sw.WriteLine("   = {0:f2} * [(({1:f3} + {2:f3}) / 2) - {3:f3}) * ({4:f3} - {5:f2})",
                    factor,
                    Pr1,
                    Pr2,
                    Dpr,
                    L3,
                    deff);
                sw.WriteLine();
                sw.WriteLine("   = {0:f3} kN", Vu);
                double Mu = factor * (((L3 - deff) * Pr1 * L3 * L3 * 0.67 +
                    (L3 - deff) * Pr2 * L3 * L3 * (L3 - deff) - Dpr * L3 * L3 * (L3 - deff)));

                sw.WriteLine();
                sw.WriteLine("Design Bending Moment");
                sw.WriteLine("  = Mu = Bending Moment Factor * [(L3-deff) * Pr1 * L3 * L3 * 0.67 + ");
                sw.WriteLine("        (L3-deff) * Pr2 * L3 * L3 * (L3-deff) - Dpr * L3 * L3 * (L3 - deff)]");

                sw.WriteLine("       = {0:f2} * [({1:f2}-{2:f2}) * {3:f3} * {1:f3} * {1:f3} * 0.67 + ",
                    factor,
                    L3,
                    deff,
                    Pr1);

                sw.WriteLine("        ({0:f2}-{1:f2}) * {2:f2} * {0:f2} * {0:f2} * ({0:f2}-{1:f2}) - {3:f2} * {0:f2} * {0:f2} * ({0:f2} - {1:f2})]",

                    L3,
                    deff,
                    Pr2,
                    Dpr);


                sw.WriteLine();
                sw.WriteLine("      = {0:f3} kN-m. {1}", Math.Abs(Mu), (Mu < 0 ? " (taking Absolute value)" : ""));

                double b = 1000;


                Mu = Math.Abs(Mu);
                double eff_depth = Math.Sqrt((Mu * 10E5) / (0.138 * f_ck * b));
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Effective Depth of Base Slab");
                sw.WriteLine("         = √((Mu * 10^6)/(0.138*σ_c*b))");
                sw.WriteLine("         = √(({0:f2} * 10^6)/(0.138*{1}*{2}))",
                    Mu,
                    f_ck,
                    b);
                sw.WriteLine();
                if (eff_depth <= deff * 1000)
                {
                    sw.WriteLine("         = {0:f3} <= {1:f3} , Provided Deff , OK.",
                        eff_depth, (deff * 1000));
                }
                else
                {
                    sw.WriteLine("         = {0:f3} > {1:f3} , Provided Deff, NOT OK.",
                        eff_depth, (deff * 1000));
                }

                sw.WriteLine();
                sw.WriteLine("Provide Base Thick {0:f2} mm", (d4 * 1000));
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("-------------------------------------------------");
                sw.WriteLine("Area of Steel required at bottom Base slab at Toe");
                sw.WriteLine("-------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Mu = 0.87 * f_y * Ast * [d - (f_y * Ast)/(f_ck * b)]");
                sw.WriteLine();
                sw.WriteLine("{0:f2} * 10^6 = 0.87 * {1:f3} * Ast * [{2:f2} - ({1:f2} * Ast)/({3:f2} * {4:F2})]",
                    Mu,
                    f_y,
                    deff * 1000,
                    f_ck,
                    b);
                double _a, _b, _c, _d, _Mu;

                _Mu = Mu;

                _a = 0.87 * f_y;
                _b = (f_y / (f_ck * b));

                _c = _a * deff * 1000;
                _d = _a * _b;

                _b = _c / _d;

                _c = _Mu / _d;

                sw.WriteLine();
                sw.WriteLine("Ast * Ast - {0:f2} * Ast + {1:f2}*10^6 = 0",
                    _b,
                    _c);

                double Ast1, Ast2;
                sw.WriteLine();
                sw.WriteLine("Ast = ({0:f2} ± √({0:f2}*{0:f2} - 4*{1:f3}*10^6))/2",
                    _b,
                    _c);

                _d = Math.Sqrt((_b * _b - 4 * _c * 10E5));
                sw.WriteLine("    =  ({0:f2} ± {1:f2})/2",
                    _b,
                    _d);

                Ast1 = (_b + _d) / 2;
                Ast2 = (_b - _d) / 2;

                sw.WriteLine("    = {0:f2}, {1:f2}", Ast1, Ast2);

                double Ast_provided = Math.PI * 20 * 20 / 4;
                int no_bar = (1000 / 200);

                double bar_dia = 15;


                double spacing = 200.0;
                for (int i = 0; i < iApp.Bar_Dia.Count; i++)
                {
                    bar_dia = iApp.Bar_Dia[i];
                    if (bar_dia >= 32)
                    {
                        spacing -= 10;
                    }

                    if (spacing <= 150.0) spacing = 150;

                    Ast_provided = Math.PI * bar_dia * bar_dia / 4;
                    no_bar = (int)(1000 / spacing) + 1;
                    Ast_provided = Ast_provided * no_bar;
                    sw.WriteLine();
                    if (Ast_provided > Ast2)
                    {
                        sw.WriteLine("Provided T{0:f0} bars @ {1:f0} mm c/c at bottom of Base Slab at Toe      Marked as (4) in the Drawing", bar_dia, spacing);
                        break;
                    }
                    else
                    {
                        if (bar_dia >= 32.0 && spacing <= 150)
                        {
                            sw.WriteLine("Reinforment Bars with diameter {0:f0} and spacing {1:f0} mm is not sufficient.", bar_dia, spacing);
                            break;
                        }
                    }
                }
                sw.WriteLine();
                //sw.WriteLine("Provided T{0:f0} bars @ {1:f0} mm c/c at bottom of Base Slab at Toe      Marked as (4) in the Drawing", bar_dia, spacing);

                _bd4 = bar_dia;
                _sp4 = 200;

                sw.WriteLine();
                sw.WriteLine("Provided Provided Ast = {0:f2} sq.mm.", Ast_provided);

                double Pst = Ast_provided * 100 / (b * deff * 1000);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Percent of Tension Steel = Pst = Ast_provided * 100 / (b * deff * 1000) = {0:f2}%,", Pst, f_ck);

                double tau_c = iApp.Tables.Permissible_Shear_Stress(Pst, (CONCRETE_GRADE)(int)f_ck, ref ref_string);
                sw.WriteLine();
                sw.WriteLine("Allowable Shear Stress of M{0:f0} Concrete = τ_c = {1:f3} Refer to TABLE 1 (given at the end of this report)", f_ck, tau_c, ref_string);

                sw.WriteLine();
                double tau_v = Vu * 10E2 / (b * deff * 1000);
                sw.WriteLine();
                if (tau_v <= tau_c)
                {
                    sw.WriteLine("Applied Shear Stress τ_v = Vu/b*d = {0:f3} * 1000/({1:f2}*{2:f2}) = {3:f3} <= {4:f3} , OK",
                    Vu,
                    b,
                    deff * 1000,
                    tau_v,
                    tau_c);

                }
                else
                {
                    sw.WriteLine("Applied Shear Stress τ_v = Vu/b*d = {0:f3} * 1000/({1:f2}*{2:f2}) = {3:f3} > {4:f3}, NOT OK",
                        Vu,
                        b,
                        deff * 1000,
                        tau_v,
                        tau_c);
                }
                double dist_steel = 0.12 / 100 * b * deff * 1000;
                sw.WriteLine();
                sw.WriteLine("Distribution Steel = 0.12/100 * {0} * {1} = {2:f2} sq.mm.",
                    b,
                    deff,
                    dist_steel);

                sw.WriteLine();
                sw.WriteLine("Provide T10 @ 90 mm c/c           Marked as (5) in the Drawing");
                sw.WriteLine();
                //_bd3 = 10;
                //_sp3 = 90;

                _bd5 = 10;
                _sp5 = 90;

                Ast_provided = Math.PI * 10 * 10 / 4;
                no_bar = (int)(1000.0 / 90.0) + 1;
                Ast_provided = Ast_provided * no_bar;
                sw.WriteLine("Steel Area Provided = {0:f3} sq.mm", Ast_provided);

                #endregion


                #region STEP 7
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("--------------------------------------------------------------------------");
                sw.WriteLine("STEP 7 : Design of Base Slab at Backfill Heel Side for Steel Reinforcement");
                sw.WriteLine("--------------------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Upward Pressure varies from Pr3 = {0:f2} to Pr5 = {1:f2}",
                    Pr3,
                    Pr5);
                sw.WriteLine("downward Pressure is Earth Load + Surcharge + Self Weight");

                double Pr6 = (H - d4) * gamma_b + h2 * gamma_b + d3 * gamma_c + d4 * gamma_c;
                sw.WriteLine("             Pr6 = (H-d4)*γ_b + h2 * γ_b  + d3*γ_c + d4*γ_c");
                sw.WriteLine("                 = ({0:f3}-{1:f2})*{2:f2} + {3:f2} * {2:f2}  + {4:f2}*{5:f2} + {1:f3}*{5:f3}",
                    H,
                    d4,
                    gamma_b,
                    h2,
                    d3,
                    gamma_c);
                sw.WriteLine("                 = {0:f2}", Pr6);

                sw.WriteLine();
                sw.WriteLine("Here downward pressure Pr6 = {0:f2} is more than Pr4 = {1:f2} and Pr5 = {2:f2}",
                    Pr6, Pr4, Pr5);//? Pr3 = 157 and Pr6 = 135 , How
                sw.WriteLine();
                sw.WriteLine("So, tension reinforcement steel will be required at the top");

                Vu = factor * (Pr6 * L1 - 0.5 * Pr4 * L1 - 0.5 * Pr5 * L1);

                Vu = Math.Abs(Vu);

                sw.WriteLine();
                sw.WriteLine("Design Shear Force");
                sw.WriteLine("           Vu = Shear Force Factor * (Pr6 * L1 - 0.5 * Pr4 * L1 - 0.5 * Pr5 * L1)");
                sw.WriteLine("              = {0:f2} * ({1:f2} * {2:f2} - 0.5 * {3:f3} * {2:f3} - 0.5 * {4:f3} * {2:f3})",
                    factor,
                    Pr6,
                    L1,
                    Pr4,
                    Pr5);

                sw.WriteLine("              = {0:f2} kN", Vu);

                Mu = factor * (Pr6 * L1 * L1 * 0.5 - 0.5 * Pr4 * L1 * L1 * 0.33 - 0.5 * Pr5 * L1 * L1 * 0.67);

                sw.WriteLine();
                sw.WriteLine("Design Bending Moment = Mu");
                sw.WriteLine();
                sw.WriteLine("      Mu = Bending Moment Factor * ((Pr6 * L1 * L1 * 0.5)");
                sw.WriteLine("           - (0.5 * Pr4 * L1 * L1 * 0.33) - 0.5 * Pr5 * L1 * L1 * 0.67))");
                sw.WriteLine();

                sw.WriteLine("         = {0:f2} * (({1:f2} * {2:f3} * {2:f3} * 0.5)",
                    factor,
                    Pr6,
                    L1);


                sw.WriteLine("             - (0.5 * {0:f3} * {1:f3} * {1:f3} * 0.33)",
                    Pr4,
                    L1);

                sw.WriteLine("              - (0.5 * {0:f3} * {1:f3} * {1:f3} * 0.67))",
                    Pr6,
                    L1);





                sw.WriteLine();
                sw.WriteLine("         = {0:f2} kN-m", Mu);


                eff_depth = Math.Sqrt((Mu * 10E5) / (0.138 * f_ck * b));
                sw.WriteLine();
                sw.WriteLine("Effective Depth of Base Slab at Heel = √((Mu * 10^6)/(0.138*f_ck*b))");
                sw.WriteLine("                                     = √(({0:f2} * 10^6)/(0.138*{1:f2}*{2:f2}))",
                    Mu,
                    f_ck, b);

                if (eff_depth < (deff * 1000))
                    sw.WriteLine("                                     = {0:f2} mm < {1:f2}", eff_depth,
                        (deff * 1000));

                sw.WriteLine();
                sw.WriteLine("Area of Steel required at top of base slab at Heel");
                //σ_st

                sw.WriteLine();
                sw.WriteLine("  Mu = 0.87 * σ_st * Ast * (d-((f_y*Ast)/(f_ck*b))");
                sw.WriteLine();
                sw.WriteLine("  {0:f2}*10^6 = 0.87 * {1:f2} * Ast * ({2:f2}-(({1:f2}*Ast)/({3}*{4}))",
                    Mu,
                    f_y,
                    deff * 1000,
                    f_ck,
                    b);

                _c = 0.87 * f_y * deff * 1000;
                _d = (0.87 * f_y * f_y) / (f_ck * b);


                _b = _c / _d;

                sw.WriteLine();
                sw.WriteLine("Ast*Ast - {0:f2}*Ast + {1:f2}*10^6 = 0",
                    _b, _c);

                _c = (Mu / _d) * 10E5;
                _d = Math.Sqrt((_b * _b - 4 * _c));

                Ast1 = (_b + _d) / 2;
                Ast2 = (_b - _d) / 2;

                sw.WriteLine();
                sw.WriteLine("Ast = {0:f2} and {1:f2} sq.mm.", Ast1, Ast2);
                bar_dia = 15;
                //do
                //{
                //    bar_dia += 5;
                //    if (bar_dia == 30) bar_dia += 2;
                //    Ast_provided = (Math.PI * bar_dia * bar_dia / 4) * 10;
                //}
                //while (Ast_provided < Ast2);


                spacing = 200;
                double percent = 0.0;
                bool Is_2_Layer = false;
                for (int i = 0; i < iApp.Bar_Dia.Count; i++)
                {
                    bar_dia = iApp.Bar_Dia[i];
                    if (bar_dia >= 32)
                    {
                        spacing -= 10; i--;
                    }

                    if (spacing <= 150.0) spacing = 150;

                    Ast_provided = Math.PI * bar_dia * bar_dia / 4;
                    no_bar = (int)(1000 / spacing) + 1;
                    Ast_provided = Ast_provided * no_bar;


                    if (Is_2_Layer)
                        Ast_provided = 2 * Ast_provided;

                    percent = (Ast_provided * 100) / (1000 * deff * 1000);
                    tau_c = iApp.Tables.Permissible_Shear_Stress(percent, (CONCRETE_GRADE)(int)f_ck, ref ref_string);
                    tau_v = Vu * 1000 / (b * deff * 1000);

                    if (Ast_provided > Ast2 && tau_c > tau_v)
                    {
                        if (Is_2_Layer)
                            sw.WriteLine("Provide 2 Layers T{0} bars @{1:f0} mm c/c at Top of bar slab at Heel.     Marked as (6) in the Drawing", bar_dia, spacing);
                        else
                            sw.WriteLine("Provide T{0} bars @{1:f0} mm c/c at Top of bar slab at Heel.     Marked as (6) in the Drawing", bar_dia, spacing);
                        break;
                    }
                    else
                    {
                        if (bar_dia >= 32.0 && spacing <= 150)
                        {
                            if (!Is_2_Layer)
                            {
                                spacing = 200; i = 0;
                                Is_2_Layer = true;
                            }
                            else
                            {
                                sw.WriteLine("Reinforment Bars with diameter {0:f0} and spacing {1:f0} mm is not sufficient.", bar_dia, spacing);
                                break;
                            }
                        }
                    }
                }


                sw.WriteLine();
                sw.WriteLine();
                //sw.WriteLine("Provide T{0} bars @ 100 mm c/c at Top of bar slab at Heel.     Marked as (6) in the Drawing", bar_dia);

                _bd6 = bar_dia;
                _sp6 = 100;


                sw.WriteLine();
                sw.WriteLine("Provide Ast = {0:f2} sq.mm", Ast_provided);

                percent = (Ast_provided * 100) / (1000 * deff * 1000);
                sw.WriteLine();
                sw.WriteLine("Percentage = Ast_provided * 100 / (1000 * deff * 1000)");
                sw.WriteLine("           = {0:f3} * 100 / (1000 * {1:f3} * 1000)", Ast_provided, deff);
                sw.WriteLine("           = {0:f3}%", percent);

                tau_c = iApp.Tables.Permissible_Shear_Stress(percent, (CONCRETE_GRADE)(int)f_ck, ref ref_string);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Allowable Shear Stress of M{0:f0} Concrete = τ_c = {1:f2}  (Refer to TABLE 1) ", f_ck, tau_c);

                tau_v = Vu * 1000 / (b * deff * 1000);

                sw.WriteLine();
                sw.WriteLine("Applied Shear Stress τ_v = Vu * 1000 / (b * deff * 1000)");
                if (tau_v <= tau_c)
                {
                    sw.WriteLine("                         = ({0:f2} * 1000)/(1000*{1:f1}) = {2:f2} N/sq.mm. <  {3:f2} (τ_c) , OK",
                        Vu,
                        deff * 1000,
                        tau_v,
                        tau_c);
                }
                else
                {
                    sw.WriteLine("                         = ({0:f2} * 1000)/(1000*{1:f1}) = {2:f2} N/sq.mm. >  {3:f2} (τ_c)  , NOT OK",
                        Vu,
                        deff * 1000,
                        tau_v,
                        tau_c);
                }
                sw.WriteLine();
                sw.WriteLine("Distribution Steel = 0.12/100 * {0} * {1} = {2:f2} sq.mm.",
                    b,
                    deff,
                    dist_steel);

                sw.WriteLine();
                sw.WriteLine("Provide T10 @ 90 mm c/c       Marked as (7) in the Drawing");

                _bd7 = 10;
                _sp7 = 90;

                Ast_provided = Math.PI * 10 * 10 / 4;
                no_bar = (int)(1000.0 / 90.0);
                Ast_provided = Ast_provided * no_bar;
                sw.WriteLine();
                sw.WriteLine("Ast Provided = {0:f2} sq.mm", Ast_provided);

                #endregion


                #region STEP 8
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 8 : Design of Wall Reinforcement");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("At the bottom of the front face of the wall");
                sw.WriteLine();
                sw.WriteLine("Design Bending Moment ");



                //sw.WriteLine("     = (1/6) * Ca * γ_b * H * H * H + (1/2) * Ca * γ_b * h1 * H * H");
                //sw.WriteLine("     = (1/6) * {0:f3} * {1:f0} * {2:f2} * {2:f2} * {2:f2} + (1/2) * {0:f3} * {1:f0} * {3:f2} * {2:f2} * {2:f2}",
                //    Ca,
                //    gamma_b,
                //    H,
                //    h1);
                //double deg_bend_mom = (1.0 / 6.0) * Ca * gamma_b * H * H * H + (1.0 / 2.0) * Ca * gamma_b * h2 * H * H;


                //Chiranjit [2012 10 04]


                sw.WriteLine("     = (1/6) * Ka * γ_b * H * H * H + F * (H + h1)");
                sw.WriteLine("     = (1/6) * {0:f3} * {1:f0} * {2:f2} * {2:f2} * {2:f2} + {3:f3} * ({2:f3}+{4:f3})",
                    Ka,
                    gamma_b,
                    H,
                    F,
                    h1);


                double deg_bend_mom = (1.0 / 6.0) * Ka * gamma_b * H * H * H + F * (H + h1);
                sw.WriteLine();
                sw.WriteLine("     = {0:f2} kN-m", deg_bend_mom);


                double deg_shear = Ka * gamma_b * h1 * H + 0.5 * Ka * gamma_b * H * H;
                sw.WriteLine();
                sw.WriteLine("Design Shear ");
                sw.WriteLine("      = Ka * γ_b * h1 * H + 0.5 * Ka * γ_b * H * H");
                sw.WriteLine("      = {0:f2} * {1:f0} * {2:f2} * {3:f2} + 0.5 * {0:f2} * {1:f0} * {3:f2} * {3:f2}",
                    Ka,
                    gamma_b,
                    h1,
                    H);
                sw.WriteLine("      = {0:f2} kN", deg_shear);

                sw.WriteLine();
                sw.WriteLine();
                double fact_bend_mom = factor * deg_bend_mom;
                sw.WriteLine("   Mu = Factored Bending Moment = {0:f2} * {1:f2} = {2:f2} kN-m",
                    factor,
                    deg_bend_mom,
                    fact_bend_mom);

                double fact_shear_force = factor * deg_shear;
                sw.WriteLine("   Vu = Factored Shear Force = {0:f2} * {1:f2} = {2:f2} kN",
                                    factor,
                                    deg_shear,
                                    fact_shear_force);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Effective Thickness of wall at the base");
                _d = Math.Sqrt((Mu * 10E5) / (0.138 * f_ck * b));
                sw.WriteLine(" d = √((Mu * 10^6)/(0.138*f_ck*b))");
                sw.WriteLine("   = √(({0:f2} * 10^6)/(0.138*{1:f2}*{2}))",
                    Mu, f_ck, b);
                if (_d <= 1000)
                {
                    sw.WriteLine("   = {0:f2} mm <= 1000 mm , OK",
                        _d);
                }
                else
                {
                    sw.WriteLine("   = {0:f2} mm > 1000 mm, NOT OK",
                    _d);
                }
                //sw.WriteLine("Area of steel required = Ast = (0.36 * σ_c * b * 0.48 * (L2-cover))/(0.87 * σ_st)");
                sw.WriteLine();
                sw.WriteLine("Area of steel required = Ast = (0.36 * f_ck * b * 0.48 * d)/(0.87 * f_y)");
                double _ast = (0.36 * f_ck * b * 0.48 * _d) / (0.87 * f_y);

                sw.WriteLine("                             = (0.36 * {0} * {1} * 0.48 * d)/(0.87 * {3})",
                    f_ck,
                    b,
                    _d,
                    f_y);
                sw.WriteLine("                             = {0:f2} sq.mm.", _ast);

                bar_dia = 15;


                //do
                //{
                //    bar_dia += 5;
                //    Ast_provided = (Math.PI * bar_dia * bar_dia / 4) * (1000 / 120);
                //}
                //while (Ast_provided < _ast);


                spacing = 200.0;
                //for (int i = 0; i < iApp.Bar_Dia.Count; i++)
                //{
                //    bar_dia = iApp.Bar_Dia[i];
                //    if (bar_dia >= 32)
                //    {
                //        spacing -= 10;
                //    }

                //    if (spacing <= 100.0) spacing = 100;

                //    Ast_provided = Math.PI * bar_dia * bar_dia / 4;
                //    no_bar = (int)(1000 / spacing) + 1;
                //    Ast_provided = Ast_provided * no_bar;
                //    if (Ast_provided > _ast)
                //    {
                //        sw.WriteLine("Provided T{0:f0} bars @ {1:f0} mm c/c", bar_dia, spacing);
                //        break;
                //    }
                //    else
                //    {
                //        if (bar_dia >= 32.0 && spacing <= 150)
                //        {
                //            sw.WriteLine("Reinforment Bars with diameter {0:f0} and spacing {1:f0} mm is not sufficient.", bar_dia, spacing);
                //            break;
                //        }
                //    }
                //}

                Is_2_Layer = false;
                for (int i = 0; i < iApp.Bar_Dia.Count; i++)
                {
                    bar_dia = iApp.Bar_Dia[i];
                    if (bar_dia >= 32)
                    {
                        spacing -= 10; i--;
                    }

                    if (spacing <= 150.0) spacing = 150;

                    Ast_provided = Math.PI * bar_dia * bar_dia / 4;
                    no_bar = (int)(1000 / spacing) + 1;
                    Ast_provided = Ast_provided * no_bar;


                    if (Is_2_Layer)
                        Ast_provided = 2 * Ast_provided;

                    percent = (Ast_provided * 100) / (1000 * deff * 1000);
                    tau_c = iApp.Tables.Permissible_Shear_Stress(percent, (CONCRETE_GRADE)(int)f_ck, ref ref_string);
                    tau_v = Vu * 1000 / (b * deff * 1000);

                    if (Ast_provided > _ast && tau_c > tau_v)
                    {
                        if (Is_2_Layer)
                            sw.WriteLine("Provide 2 Layers T{0} bars @{1:f0} mm c/c at Top of bar slab at Heel.     Marked as (6) in the Drawing", bar_dia, spacing);
                        else
                            sw.WriteLine("Provide T{0} bars @{1:f0} mm c/c at Top of bar slab at Heel.     Marked as (6) in the Drawing", bar_dia, spacing);
                        break;
                    }
                    else
                    {
                        if (bar_dia >= 32.0 && spacing <= 150)
                        {
                            if (!Is_2_Layer)
                            {
                                spacing = 200; i = 0;
                                Is_2_Layer = true;
                            }
                            else
                            {
                                sw.WriteLine("Reinforment Bars with diameter {0:f0} and spacing {1:f0} mm is not sufficient.", bar_dia, spacing);
                                break;
                            }
                        }
                    }
                }

                sw.WriteLine();
                //sw.WriteLine("Provide T{0} bars @120 mm c/c               Marked as (1) in the Drawing", bar_dia);

                _bd1 = bar_dia;
                _sp1 = 120;


                sw.WriteLine("Provided Ast = {0:f2} sq.mm",
                    Ast_provided);

                //percent = (Ast_provided * 100) / (b * (t * 1000 - cover));
                //percent = (Ast_provided * 100) / (b * _d);
                sw.WriteLine();
                sw.WriteLine("Percentage of Steel provided = p = {0:f2}*100/({1}*{2}) = {3:f2}% ",
                    Ast_provided,
                    b,
                    (t * 1000 - cover),
                    percent, f_ck);
                //tau_c = iApp.Tables.Permissible_Shear_Stress(percent, (CONCRETE_GRADE)(int)f_ck, ref ref_string);
                sw.WriteLine();
                //sw.WriteLine("{0}", ref_string);
                sw.WriteLine();
                sw.WriteLine("Allowable Shear Stress of M{0:f0} Concrete = τ_c = {1:f2} N/sq.mm  Refer to TABLE 1", f_ck, tau_c);
                //tau_v = Vu * 1000 / (b * (t * 1000 - cover));

                sw.WriteLine();
                sw.WriteLine("Applied Shear Stress = τ_v = Vu * 1000 / (b * (t * 1000 - cover))");
                sw.WriteLine("                           = {0:f3}*1000 / ({1:f3} * ({2:f3} * 1000 - {3:f3}))",
                    Vu,
                    b,
                    t,
                    cover);
                if (tau_v <= tau_c)
                {

                    sw.WriteLine("                           = {0:f3} <= τ_c , OK", tau_v);

                    //sw.WriteLine("Applied Shear Stress = τ_v = Vu/b*d = {0:f2} * 1000/({1} * {2:f2}) = {3:f2} <= τ_c , OK",
                    //    Vu,
                    //    b,
                    //    (t * 1000 - cover),
                    //    tau_v);
                }
                else
                {
                    sw.WriteLine("                           = {0:f3} > τ_c , NOT OK", tau_v);

                    //sw.WriteLine("Applied Shear Stress = τ_v = Vu/b*d = {0:f2} * 1000/({1} * {2:f2}) = {3:f2} N/sqmm > τ_c ,  NOT OK",
                    //Vu,
                    //b,
                    //_d,
                    //tau_v);
                }
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Distribution Steel for Temperature Reinforcements:");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Total Sectional Area of the wall");

                double total_sectional_area = (H - d4) * d3 + (H - d4 - d1) * deff;
                sw.WriteLine("    = (H - d4) * d3 + (H - d4 - h1) * deff");
                sw.WriteLine("    = ({0:f2} - {1:f2}) * {2:f2} + ({0:f2} - {1:f2} - {3:f2}) * {4:F2}",
                    H,
                    d4,
                    d3,
                    h1,
                    deff);
                sw.WriteLine("    = {0:f2} sq.m",
                    total_sectional_area);
                sw.WriteLine("    = {0:f2} sq.mm",
                    total_sectional_area * 1000000);

                _a = (0.12 / 100) * total_sectional_area * 100 * 10e3;

                sw.WriteLine();
                sw.WriteLine("Area of Temperature Steel  = 0.12% = {0:f2} sq.mm", _a);

                _ast = Math.PI * 100 / 4;
                no_bar = (int)(_a / _ast);
                sw.WriteLine();
                sw.WriteLine("Use 10 mm bars, Number of bars = {0:f2}/{1:f2} = {2} nos",
                    _a,
                    _ast,
                    no_bar);




                int front_bar = (int)(no_bar * (2.0 / 3.0));
                int back_fill_bar = (int)(no_bar * (1.0 / 3.0));
                sw.WriteLine();
                sw.WriteLine("Provide {0} bars horizontally on the Front face", front_bar);

                _c = (H - d1 - d4) * 1000 / front_bar;
                sw.WriteLine("    = (H - d1 - d4) * 1000 / {0}", front_bar);
                sw.WriteLine("    = ({0:f2} - {1:f2} - {2:f2}) * 1000 / {3}",
                    H,
                    h1,
                    d4,
                    front_bar);
                sw.WriteLine("    = {0:f2} mm", _c);

                _c = (int)(_c / 10.0);
                _c += 1.0;
                _c *= 10;
                sw.WriteLine("    ≈ {0:f2} mm                  Marked as (3) in the Drawing", _c);
                _bd3 = 10;
                _sp3 = _c;



                sw.WriteLine();
                sw.WriteLine("Provide {0} bars horizontally on the Backfill side face", back_fill_bar);

                _c = (H - d4) * 1000 / back_fill_bar;
                sw.WriteLine("    = (H - d4) * 1000 / {0}", back_fill_bar);
                sw.WriteLine("    = ({0:f2} - {1:f2}) * 1000 / {2:f2}",
                    H,
                    d4,
                    back_fill_bar);
                sw.WriteLine("    = {0:f2} mm", _c);

                _c = (int)(_c / 10.0);
                _c += 1.0;
                _c *= 10;
                sw.WriteLine("    ≈ {0:f2} mm c/c       Marked as (2) in the Drawing", _c);

                _bd2 = 10;
                _sp2 = _c;
                #endregion

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("TABLE 1 : PERMISSIBLE SHEAR STRESS");
                sw.WriteLine("----------------------------------");
                sw.WriteLine();

                foreach (string str in iApp.Tables.Get_Tables_Permissible_Shear_Stress())
                {
                    sw.WriteLine(str);
                }

                //tau_c = iApp.Tables.;

                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------------------       END OF REPORT        --------------------------");
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------------------------------------");
                sw.WriteLine("---------            Thank you for using ASTRA Pro          ---------------");
                sw.WriteLine("---------------------------------------------------------------------------");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public string FilePath
        {
            set
            {
                user_path = value;
                //file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "Design of RCC Abutment");

                //file_path = Path.Combine(user_path, "Working Stress Design");
                file_path = user_path;


                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                if (IsExecuteBridge) file_path = Path.Combine(file_path, "Design of Retaining Wall");
                else file_path = Path.Combine(user_path, "Design of Cantilever Retaining Wall");


                if (!Directory.Exists(file_path))
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (!Directory.Exists(system_path))
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Bridge_RCC_ReturnWall.TXT");
                user_input_file = Path.Combine(system_path, "RCC_ABUTMENT.FIL");
            }
        }
        public void Write_Cantilever_Drawing_File()
        {
            drawing_path = Path.Combine(system_path, "ABUTMENT_DRAWING.FIL");

            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
            try
            {
                sw.WriteLine("_L1={0}", L1 * 1000);
                sw.WriteLine("_L2={0}", L2 * 1000);
                sw.WriteLine("_L3={0}", L3 * 1000);
                sw.WriteLine("_D={0}", ((L1 + L2 + L3) * 1000));
                sw.WriteLine("_d4={0}", d4 * 1000);
                sw.WriteLine("_H={0}", H * 1000);
                sw.WriteLine("_d3={0}", d3 * 1000);
                sw.WriteLine("_d1={0}", d1 * 1000);
                sw.WriteLine("_L4={0}", L4 * 1000);


                sw.WriteLine("_bd1={0}", _bd1);
                sw.WriteLine("_bd2={0}", _bd2);
                sw.WriteLine("_bd3={0}", _bd3);
                sw.WriteLine("_bd4={0}", _bd4);
                sw.WriteLine("_bd5={0}", _bd5);
                sw.WriteLine("_bd6={0}", _bd6);
                sw.WriteLine("_bd7={0}", _bd7);


                sw.WriteLine("_sp1={0}", _sp1);
                sw.WriteLine("_sp2={0}", _sp2);
                sw.WriteLine("_sp3={0}", _sp3);
                sw.WriteLine("_sp4={0}", _sp4);
                sw.WriteLine("_sp5={0}", _sp5);
                sw.WriteLine("_sp6={0}", _sp6);
                sw.WriteLine("_sp7={0}", _sp7);




            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }



    }
}
