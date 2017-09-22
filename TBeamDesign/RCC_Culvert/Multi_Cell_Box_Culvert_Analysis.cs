using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AstraFunctionOne.BridgeDesign;
using AstraFunctionOne.BridgeDesign.SteelTruss;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using AstraInterface.TrussBridge;
using BridgeAnalysisDesign.Composite;
using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign.RCC_T_Girder;

namespace BridgeAnalysisDesign.RCC_Culvert
{
    public class MultiCell_BoxCulvert_Analysis
    {
        /// <summary>
        /// Height of Earth Caushion
        /// </summary>
        public double De { get; set; }
        /// <summary>
        /// Inside Clear Span
        /// </summary>
        public double b { get; set; }
        /// <summary>
        /// Inside Clear Width
        /// </summary>
        public double w { get; set; }
        /// <summary>
        /// Inside Clear Depth
        /// </summary>
        public double d 
        {
            get { return _H - d1 - d2; }
        }
        /// <summary>
        /// Thickness of Top Slab
        /// </summary>
        public double d1 { get; set; }
        /// <summary>
        /// Thickness of Bottom Slab
        /// </summary>
        public double d2 { get; set; }
        /// <summary>
        /// Thickness of Side Walls
        /// </summary>
        public double d3 { get; set; }
        /// <summary>
        /// Thickness of Intermediate Walls
        /// </summary>
        public double d4 { get; set; }
        /// <summary>
        /// Rankine's Active Earth Pressure Coefficient [Ka]
        /// </summary>
        public double Ka { get; set; }

        /// <summary>
        /// Unit Weight of Earth
        /// </summary>
        public double Gama_b { get; set; }
        /// <summary>
        /// Unit Weight of Concrete
        /// </summary>
        public double Gama_c { get; set; }

        public int Cell_Nos { get; set; }

        public double SIDL { get; set; }

        public bool Is_Only_Sidewall { get; set; }

        #region Seismic Data
        public bool Apply_Seismic;
        public double _Z, _I, _R, _Sa_by_g;
        #endregion Seismic Data


        public BridgeMemberAnalysis Structure = null;

        #region Loads Variables

        public Applied_Load_Collection DL_Loads = new Applied_Load_Collection();
        public Applied_Load_Collection SIDL_Loads = new Applied_Load_Collection();
        public Earth_Pressure_Load_Collection EP_Loads = new Earth_Pressure_Load_Collection();
        public List<Applied_Load_Collection> Moving_Loads = new List<Applied_Load_Collection>();

        public Load_Combination_Collection Combine_Loads = new Load_Combination_Collection();
        #endregion Loads Variables

        //flag true if user opens previous design
        public bool IsOpen = false;


        public string rep_file_name = "";
        public string file_path = "";
        public string user_input_file = "";
        public string system_path = "";
        public string drawing_path = "";
        public string user_path = "";

        IApplication iApp;
        public MultiCell_BoxCulvert_Analysis(IApplication app)
        {
            iApp = app;

            De = 0;
            w = 1.0;
            b = 0;
            //d = 0;
            d1 = 0;
            d2 = 0;
            d3 = 0;
            Cell_Nos = 1;

            Top_Members = new List<int>();
            Bottom_Members = new List<int>();
            Left_Side_Members = new List<int>();
            Right_Side_Members = new List<int>();
            Intermediate_Members = new List<int>();

            Project_Name = "Design of Multi Cell Box Culvert";
        }

        //Chiranjit [2016 08 02] Add Project Name for multiple design folder
        public string Project_Name { get; set; }
        public string FilePath
        {
            set
            {
                
                user_path = value;
                file_path = Path.Combine(user_path, Project_Name);

                if (!Directory.Exists(file_path))
                {
                    if (IsOpen) return;
                    Directory.CreateDirectory(file_path);
                }
                //system_path = Path.Combine(file_path, "AstraSys");
                //if (!Directory.Exists(system_path))
                //    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Culvert_Rcc_Multicell_Box.TXT");
                user_input_file = Path.Combine(system_path, "BOX_CULVERT.FIL");
                drawing_path = Path.Combine(system_path, "BOX_CULVERT_DRAWING.FIL");
            }
        }

        public string User_Input_File
        {
            get
            {
                if (file_path == "") return "";
                string fname = Path.Combine(file_path, "Analysis Multicell Box Culvert");

                if (!Directory.Exists(fname))
                {
                    Directory.CreateDirectory(fname);
                }

                //user_input_file = Path.Combine(fname, "Multi_Cell_Box_Culvert_Input_Data.txt");
                user_input_file = Path.Combine(fname, "Analysis_Input_Data.txt");
                return user_input_file;
            }
        }

        #region Properties

        public JointNodeCollection jnc_bottom = new JointNodeCollection();
        public JointNodeCollection jnc_top = new JointNodeCollection();
        public JointNodeCollection jnc_vertical = new JointNodeCollection();
        public JointNodeCollection jnc_internediate = new JointNodeCollection();

        public MemberCollection mc_bottom = new MemberCollection();
        public MemberCollection mc_top = new MemberCollection();
        public MemberCollection mc_vertical = new MemberCollection();
        public MemberCollection mc_internediate = new MemberCollection();

        public List<int> supports = new List<int>();

        public List<double> y_factors = new List<double>();

        public List<double> x_factors = new List<double>();

        public List<string> Member_Properties = new List<string>();




        public List<int> Top_Members { get; set; }
        public List<int> Bottom_Members { get; set; }
        public List<int> Intermediate_Members { get; set; }
        public List<int> Left_Side_Members { get; set; }
        public List<int> Right_Side_Members { get; set; }


        public MaxForce BM_TS { get; set; } //Top Slab Bending Moment
        public MaxForce BM_BS { get; set; } //Bottom Slab Bending Moment
        public MaxForce BM_SW { get; set; } //Side wall Bending Moment
        public MaxForce BM_IW { get; set; } //Intermediate wall Bending Moment
        public MaxForce SF_TS { get; set; } //Top Slab Shear Force
        public MaxForce SF_BS { get; set; } //Bottom Slab Shear Force
        public MaxForce SF_SW { get; set; } //Side wall Shear Force
        public MaxForce SF_IW { get; set; } //Intermediate wall Shear Force



        public double Soil_Bearing_Pressure { get; set; }
        #endregion Properties


        public void Create_Data()
        {
            double x, y, z;

            //double Length = b + d3;
            //double Height = d + d1 / 2.0 + d2 / 2.0;


            double Length = b;
            double Height = _H;

            x_factors = new List<double>();

            //x_factors.Add(0);
            //x_factors.Add(0.110465);
            //x_factors.Add(0.505814);
            //x_factors.Add(0.901163);
            //x_factors.Add(1);

            x_factors.Add(Length * 0.0);
            x_factors.Add(Length * 0.110465);
            x_factors.Add(Length * 0.505814);
            x_factors.Add(Length * 0.901163);
            x_factors.Add(Length * 1.0);


             y_factors = new List<double>();
            
                #region Chiranjit [2016 08 05]
             if (_HA > 0)
             {
                 y_factors.Add(0);
                 y_factors.Add(_HA);
                 y_factors.Add(Height * 0.5);
                 y_factors.Add(Height * 1);

                 y_factors.Sort();
             }
             else
             {

                 y_factors.Add(0);
                 y_factors.Add(Height * 0.109195);
                 y_factors.Add(Height * 0.5);
                 y_factors.Add(Height * 0.890805);
                 y_factors.Add(Height * 1);
             }
                #endregion Chiranjit [2016 08 05]

             MyList.Array_Format_With(ref y_factors, "f5");

            List<double> x_coordinates = new List<double>();

            List<double> x_crd_diff = new List<double>();

            int i = 0;
            int j = 0;

            for (i = 0; i < x_factors.Count - 1; i++)
            {
                x_crd_diff.Add(x_factors[i + 1] - x_factors[i]);
            }
            //Cell_Nos = 2;
            x = 0;
            x_coordinates.Add(x);
            for (i = 0; i < Cell_Nos; i++)
            {
                for (j = 0; j < x_crd_diff.Count; j++)
                {
                    x += x_crd_diff[j];
                    x = double.Parse(x.ToString("f5"));
                    if (!x_coordinates.Contains(x))
                    {
                        x_coordinates.Add(x);
                    }
                }
                x_crd_diff.Reverse();
            }
            x_coordinates.Sort();

            jnc_bottom = new JointNodeCollection();

            JointNode jn = new JointNode();

            int jnt_cnt = 0;
            for (i = 0; i < x_coordinates.Count; i++)
            {
                jn = new JointNode();
                jnt_cnt++;
                jn.NodeNo = jnt_cnt;
                jn.X = x_coordinates[i];
                jn.Y = 0.0;
                jn.Z = 0.0;
                jnc_bottom.Add(jn);
            }

            jnc_top = new JointNodeCollection();

            for (i = 0; i < x_coordinates.Count; i++)
            {
                jn = new JointNode();
                jnt_cnt++;
                jn.NodeNo = jnt_cnt;
                jn.X = x_coordinates[i];
                jn.Y = Height;
                jn.Z = 0.0;
                jnc_top.Add(jn);
            }

            jnc_vertical = new JointNodeCollection();

            for (j = 0; j <= Cell_Nos; j++)
            {
                for (i = 1; i < y_factors.Count - 1; i++)
                {
                    jn = new JointNode();
                    jnt_cnt++;
                    jn.NodeNo = jnt_cnt;
                    jn.X = j * Length;
                    jn.Y = y_factors[i];
                    jn.Z = 0.0;
                    jnc_vertical.Add(jn);
                }
            }


            mc_bottom = new MemberCollection();


            Member mbr = new Member();

            int mbr_cnt = 0;
            for (i = 0; i < jnc_bottom.Count - 1; i++)
            {
                mbr = new Member();
                mbr_cnt++;
                mbr.MemberNo = mbr_cnt;
                mbr.StartNode = jnc_bottom[i];
                mbr.EndNode = jnc_bottom[i + 1];
                mc_bottom.Add(mbr);
            }


            mc_top = new MemberCollection();


            for (i = 0; i < jnc_top.Count - 1; i++)
            {
                mbr = new Member();
                mbr_cnt++;
                mbr.MemberNo = mbr_cnt;
                mbr.StartNode = jnc_top[i];
                mbr.EndNode = jnc_top[i + 1];
                mc_top.Add(mbr);
            }


            mc_vertical = new MemberCollection();

            int counter = 0;
            int top_mbr = 0;
            int bot_mbr = 0;

            supports = new List<int>();
            mc_vertical.Clear();
            counter = 0;
            for (i = 0; i < jnc_vertical.Count; i++)
            {

                counter++;

                if (counter == 1)
                {
                    mbr = new Member();
                    mbr_cnt++;
                    mbr.MemberNo = mbr_cnt;
                    mbr.StartNode = jnc_bottom[bot_mbr];
                    mbr.EndNode = jnc_vertical[i];
                    mc_vertical.Add(mbr);

                    supports.Add(jnc_bottom[top_mbr].NodeNo);

                }
                if (counter == 2)
                {
                    mbr = new Member();
                    mbr_cnt++;
                    mbr.MemberNo = mbr_cnt;
                    mbr.StartNode = jnc_vertical[i];
                    mbr.EndNode = jnc_top[top_mbr];
                    mc_vertical.Add(mbr);
                    counter = 0;
                    bot_mbr += 4;
                    top_mbr += 4;
                    continue;
                }
                mbr = new Member();
                mbr_cnt++;
                mbr.MemberNo = mbr_cnt;
                mbr.StartNode = jnc_vertical[i];
                mbr.EndNode = jnc_vertical[i + 1];
                mc_vertical.Add(mbr);
            }



            string kStr = "";
            //Bottom Members
            kStr = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;

            Bottom_Members = MyList.Get_Array_Intiger(kStr);


            //Top Members
            kStr = mc_top[0].MemberNo + " TO " + mc_top[mc_top.Count - 1].MemberNo;
            Top_Members = MyList.Get_Array_Intiger(kStr);

            Left_Side_Members.Clear();
            Right_Side_Members.Clear();
            Intermediate_Members.Clear();
            for (i = 0; i < 1; i++)
            {
                Left_Side_Members.Add(jnc_vertical[i].NodeNo);
                Right_Side_Members.Add(jnc_vertical[jnc_vertical.Count - 2 + i].NodeNo);
            }

            for (i = 0; i < jnc_vertical.Count; i++)
            {
                if (!Left_Side_Members.Contains(jnc_vertical[i].NodeNo) && !Right_Side_Members.Contains(jnc_vertical[i].NodeNo))
                    Intermediate_Members.Add(jnc_vertical[i].NodeNo);
            }




            //Set_Default_Load_Data();
        }

        public void Create_Data_2016_08_05()
        {
            double x, y, z;

            double Length = b + d3;
            double Height = d + d1 / 2.0 + d2 / 2.0;

            x_factors = new List<double>();

            //x_factors.Add(0);
            //x_factors.Add(0.110465);
            //x_factors.Add(0.505814);
            //x_factors.Add(0.901163);
            //x_factors.Add(1);

            x_factors.Add(Length * 0.0);
            x_factors.Add(Length * 0.110465);
            x_factors.Add(Length * 0.505814);
            x_factors.Add(Length * 0.901163);
            x_factors.Add(Length * 1.0);


            y_factors = new List<double>();

                y_factors.Add(0);
                y_factors.Add(Height * 0.109195);
                y_factors.Add(Height * 0.5);
                y_factors.Add(Height * 0.890805);
                y_factors.Add(Height * 1);

            MyList.Array_Format_With(ref y_factors, "f3");

            List<double> x_coordinates = new List<double>();

            List<double> x_crd_diff = new List<double>();

            int i = 0;
            int j = 0;

            for (i = 0; i < x_factors.Count - 1; i++)
            {
                x_crd_diff.Add(x_factors[i + 1] - x_factors[i]);
            }
            //Cell_Nos = 2;
            x = 0;
            x_coordinates.Add(x);
            for (i = 0; i < Cell_Nos; i++)
            {
                for (j = 0; j < x_crd_diff.Count; j++)
                {
                    x += x_crd_diff[j];
                    x = double.Parse(x.ToString("f3"));
                    if (!x_coordinates.Contains(x))
                    {
                        x_coordinates.Add(x);
                    }
                }
                x_crd_diff.Reverse();
            }
            x_coordinates.Sort();

            jnc_bottom = new JointNodeCollection();

            JointNode jn = new JointNode();

            int jnt_cnt = 0;
            for (i = 0; i < x_coordinates.Count; i++)
            {
                jn = new JointNode();
                jnt_cnt++;
                jn.NodeNo = jnt_cnt;
                jn.X = x_coordinates[i];
                jn.Y = 0.0;
                jn.Z = 0.0;
                jnc_bottom.Add(jn);
            }

            jnc_top = new JointNodeCollection();

            for (i = 0; i < x_coordinates.Count; i++)
            {
                jn = new JointNode();
                jnt_cnt++;
                jn.NodeNo = jnt_cnt;
                jn.X = x_coordinates[i];
                jn.Y = Height;
                jn.Z = 0.0;
                jnc_top.Add(jn);
            }

            jnc_vertical = new JointNodeCollection();

            for (j = 0; j <= Cell_Nos; j++)
            {
                for (i = 1; i < y_factors.Count - 1; i++)
                {
                    jn = new JointNode();
                    jnt_cnt++;
                    jn.NodeNo = jnt_cnt;
                    jn.X = j * Length;
                    jn.Y = y_factors[i];
                    jn.Z = 0.0;
                    jnc_vertical.Add(jn);
                }
            }


            mc_bottom = new MemberCollection();


            Member mbr = new Member();

            int mbr_cnt = 0;
            for (i = 0; i < jnc_bottom.Count - 1; i++)
            {
                mbr = new Member();
                mbr_cnt++;
                mbr.MemberNo = mbr_cnt;
                mbr.StartNode = jnc_bottom[i];
                mbr.EndNode = jnc_bottom[i + 1];
                mc_bottom.Add(mbr);
            }


            mc_top = new MemberCollection();


            for (i = 0; i < jnc_top.Count - 1; i++)
            {
                mbr = new Member();
                mbr_cnt++;
                mbr.MemberNo = mbr_cnt;
                mbr.StartNode = jnc_top[i];
                mbr.EndNode = jnc_top[i + 1];
                mc_top.Add(mbr);
            }


            mc_vertical = new MemberCollection();

            int counter = 0;
            int top_mbr = 0;
            int bot_mbr = 0;

            supports = new List<int>();
            for (i = 0; i < jnc_vertical.Count; i++)
            {

                counter++;

                if (counter == 1)
                {
                    mbr = new Member();
                    mbr_cnt++;
                    mbr.MemberNo = mbr_cnt;
                    mbr.StartNode = jnc_bottom[bot_mbr];
                    mbr.EndNode = jnc_vertical[i];
                    mc_vertical.Add(mbr);

                    supports.Add(jnc_bottom[top_mbr].NodeNo);

                }
                else if (counter == 3)
                {
                    mbr = new Member();
                    mbr_cnt++;
                    mbr.MemberNo = mbr_cnt;
                    mbr.StartNode = jnc_vertical[i];
                    mbr.EndNode = jnc_top[top_mbr];
                    mc_vertical.Add(mbr);
                    counter = 0;
                    bot_mbr += 4;
                    top_mbr += 4;
                    continue;
                }
                mbr = new Member();
                mbr_cnt++;
                mbr.MemberNo = mbr_cnt;
                mbr.StartNode = jnc_vertical[i];
                mbr.EndNode = jnc_vertical[i + 1];
                mc_vertical.Add(mbr);
            }



            string kStr = "";
            //Bottom Members
            kStr = jnc_bottom[0].NodeNo + " TO " + jnc_bottom[jnc_bottom.Count - 1].NodeNo;

            Bottom_Members = MyList.Get_Array_Intiger(kStr);


            //Top Members
            kStr = jnc_top[0].NodeNo + " TO " + jnc_top[jnc_top.Count - 1].NodeNo;
            Top_Members = MyList.Get_Array_Intiger(kStr);

            Left_Side_Members.Clear();
            Right_Side_Members.Clear();
            for (i = 0; i < 3; i++)
            {
                Left_Side_Members.Add(jnc_vertical[i].NodeNo);
                Right_Side_Members.Add(jnc_vertical[jnc_vertical.Count - 3 + i].NodeNo);
            }

            for (i = 0; i < jnc_vertical.Count; i++)
            {
                if (!Left_Side_Members.Contains(jnc_vertical[i].NodeNo) && !Right_Side_Members.Contains(jnc_vertical[i].NodeNo))
                    Intermediate_Members.Add(jnc_vertical[i].NodeNo);
            }




            //Set_Default_Load_Data();
        }

        public void Write_Data(string file_name)
        {
            if (jnc_bottom.Count == 0) Create_Data();

            List<string> list = new List<string>();

            list.Add(string.Format("ASTRA SPACE BOX CULVERT ANALYSIS"));
            list.Add(string.Format("UNIT KN METER"));
            list.Add(string.Format("JOINT  COORDINATES"));

            foreach (var item in jnc_bottom)
            {
                //item.Z = 8.5;
                list.Add(item.ToString());
            }
            foreach (var item in jnc_top)
            {
                //item.Z = 8.5;
                list.Add(item.ToString());
            }
            foreach (var item in jnc_vertical)
            {
                //item.Z = 8.5;
                list.Add(item.ToString());
            }
            list.Add(string.Format("MEMBER INCIDENCES"));

            foreach (var item in mc_bottom)
            {
                list.Add(item.ToString());
            }
            foreach (var item in mc_top)
            {
                list.Add(item.ToString());
            }
            foreach (var item in mc_vertical)
            {
                list.Add(item.ToString());
            }


            list.Add(string.Format("CONSTANTS"));
            //list.Add(string.Format("E  2.5E+007 ALL"));
            //list.Add(string.Format("POISSON   O.18 ALL"));
            //list.Add(string.Format("DENSITY  25 ALL"));
            //list.Add(string.Format("ALPHA 1E-005 ALL"));
            //list.Add(string.Format("DAMP 0.05 ALL"));

            list.AddRange(Member_Properties.ToArray());


            list.Add(string.Format("MEMBER PROPERTY"));
            //list.Add(string.Format("1 TO 16 PRIS YD 0.4 ZD 1"));
            //list.Add(string.Format("17 TO 20 25 TO 28 PRIS YD 0.35 ZD 1"));
            //list.Add(string.Format("21 TO 24 PRIS YD 0.25 ZD 1"));
            //list.Add(string.Format("{0} TO {1} PRIS YD {2} ZD 1", mc_bottom[0].MemberNo, mc_bottom[mc_bottom.Count - 1].MemberNo, d2));
            //list.Add(string.Format("{0} TO {1} PRIS YD {2} ZD 1", mc_top[0].MemberNo, mc_top[mc_top.Count - 1].MemberNo, d1));
            //list.Add(string.Format("{0} TO {1} {2} TO {3} PRIS YD {4} ZD 1", mc_vertical[0].MemberNo
            //                                                                , mc_vertical[3].MemberNo,
            //                                                                mc_vertical[mc_vertical.Count - 4].MemberNo,
            //                                                                mc_vertical[mc_vertical.Count - 1].MemberNo, d3));


            list.Add(string.Format("{0} PRIS YD {1} ZD 1", MyList.Get_Array_Text(Bottom_Members), d2));
            list.Add(string.Format("{0} PRIS YD {1} ZD 1", MyList.Get_Array_Text(Top_Members), d2));
            list.Add(string.Format("{0} TO {1} {2} TO {3} PRIS YD {4} ZD 1", mc_vertical[0].MemberNo
                                                                            , mc_vertical[2].MemberNo,
                                                                            mc_vertical[mc_vertical.Count - 3].MemberNo,
                                                                            mc_vertical[mc_vertical.Count - 1].MemberNo, d3));

            if (Cell_Nos > 1)
            {
                //list.Add(string.Format("{0} TO {1}  PRIS YD {2} ZD 1", mc_vertical[4].MemberNo, mc_vertical[mc_vertical.Count - 5].MemberNo, (d3 - 0.05)));

                list.Add(string.Format("{0} TO {1}  PRIS YD {2} ZD 1", mc_vertical[3].MemberNo, 
                    mc_vertical[mc_vertical.Count - 4].MemberNo, (d4)));
            
            }
            else
            {
                
                Left_Side_Members.AddRange(Intermediate_Members.ToArray());
                Intermediate_Members.Clear();
            }





            //list.Add(string.Format("CONSTANTS"));
            //list.Add(string.Format("MATERIAL CONCRETE ALL"));


            list.Add(string.Format("SUPPORTS"));
            //list.Add(string.Format("1 5 9 PINNED"));
            //list.Add(string.Format("{0} PINNED", MyList.Get_Array_Text(supports)));
            //list.Add(string.Format("{0} FIXED BUT MZ KFY {1}", MyList.Get_Array_Text(supports), _K));
            //list.Add(string.Format("{0} FIXED BUT MZ", MyList.Get_Array_Text(supports), _K));
            //list.Add(string.Format("{0} FIXED", MyList.Get_Array_Text(supports), _K));
            list.Add(string.Format("{0} FIXED KFY {1}", MyList.Get_Array_Text(supports), _K));

            //list.Add(string.Format("SELFWEIGHT Y -1"));
            if (Self_Weight_Factor != 0.0)
                list.Add(string.Format("SELFWEIGHT Y -{0}", Self_Weight_Factor));
            
            
            //list.AddRange(Get_Load_data().ToArray());

            #region

            List<string> lst = Get_Load_Calculation();

            list.Add(string.Format("LOAD 1 LOADING DATA", Self_Weight_Factor));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("{0} FX {1:f3}", Left_Side_Members[0], _PA/10));
            list.Add(string.Format("{0} FX {1:f3}", Right_Side_Members[0], _PA / 10));
            if (_DL1 != 0.0)
            {
                //list.Add(string.Format("{0} FX {1}", MyList.Get_Array_Text(supports), _DL1));

                //list.Add(string.Format("{0} FX {1}", Left_Side_Members[0], _DL1));
                //list.Add(string.Format("{0} FX {1}", Right_Side_Members[0], _DL1));

                List<int> lint = new List<int>();

                foreach (var item in jnc_vertical)
                {
                    if(item.Y == _H/2.0)
                    {
                        lint.Add(item.NodeNo);
                    }
                }
                list.Add(string.Format("{0} FX {1}", MyList.Get_Array_Text(lint), _DL1 / 10));
            }
            list.Add(string.Format("MEMBER LOAD"));
            //list.Add(string.Format("{0} UNI GY -{1:f3}", MyList.Get_Array_Text(Top_Members), _top_udl_dl));
            //list.Add(string.Format("{0} UNI GY -{1:f3}", MyList.Get_Array_Text(Top_Members), _top_udl_ec));
            //list.Add(string.Format("{0} UNI GY -{1:f3}", MyList.Get_Array_Text(Top_Members), _top_udl_ll));
            //list.Add(string.Format("{0} UNI GY -{1:f3}", MyList.Get_Array_Text(Top_Members), SIDL));
            //list.Add(string.Format("{0} UNI GY {1:f3}", MyList.Get_Array_Text(Bottom_Members), _brng_presr));
            //list.Add(string.Format("{0} UNI GY -{1:f3}", MyList.Get_Array_Text(Bottom_Members), _oth_lds));



            list.Add(string.Format("{0} UNI GY -{1:f3}", MyList.Get_Array_Text(Top_Members), _top_udl_dl/3));
            list.Add(string.Format("{0} UNI GY -{1:f3}", MyList.Get_Array_Text(Top_Members), _top_udl_ec/3));
            list.Add(string.Format("{0} UNI GY -{1:f3}", MyList.Get_Array_Text(Top_Members), _top_udl_ll/3));
            list.Add(string.Format("{0} UNI GY -{1:f3}", MyList.Get_Array_Text(Top_Members), SIDL));
            list.Add(string.Format("{0} UNI GY {1:f3}", MyList.Get_Array_Text(Bottom_Members), _brng_presr));
            list.Add(string.Format("{0} UNI GY -{1:f3}", MyList.Get_Array_Text(Bottom_Members), _oth_lds));
            #endregion

            list.Add(string.Format("PERFORM ANALYSIS"));
            list.Add(string.Format("PRINT MAXFORCE"));
            list.Add(string.Format("FINISH"));

            File.WriteAllLines(file_name, list.ToArray());
        
        }
        public void Set_Default_Load_Data()
        {
            Applied_Load al = new Applied_Load();
            DL_Loads = new Applied_Load_Collection();
            SIDL_Loads = new Applied_Load_Collection();

            DL_Loads.Load_Title = "LOAD 1 DEAD LOAD DL";

            double dval = 0.0;

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;

            al.Direction = "GY";
            al.LoadType = "UNI";
            //al.Load = 29.631;
            dval = (De * Gama_b + d1 * Gama_c + (d * d3 * 2 * Gama_c) / (1 * (b + d3 + d3)));
            //al.Load = -(dval * Cell_Nos);
            al.Load = -(dval);
            DL_Loads.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[0].MemberNo + " TO " + mc_top[mc_top.Count - 1].MemberNo;

            al.Direction = "GY";
            al.LoadType = "UNI";
            //al.Load = 29.631;
            dval = (De * Gama_b + d1 * Gama_c);
            al.Load = -(dval);
            //al.Load = -(dval * Cell_Nos);
            DL_Loads.Add(al);

            #region Bearing Pressure of Soil
            if (Soil_Bearing_Pressure > 0)
            {
                al = new Applied_Load();
                al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;

                al.Direction = "GY";
                al.LoadType = "UNI";
                //al.Load = 29.631;
                dval = Soil_Bearing_Pressure;
                al.Load = (dval);
                //al.Load = -(dval * Cell_Nos);
                DL_Loads.Add(al);
            }
            #endregion Bearing Pressure of Soil


            SIDL_Loads.Load_Title = "LOAD 2 SUPER IMPOSED DEAD LOAD SIDL";

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;

            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = 4.59;
            SIDL_Loads.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[0].MemberNo + " TO " + mc_top[mc_top.Count - 1].MemberNo;

            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -12.063;
            SIDL_Loads.Add(al);


            List<double> ld_p1 = new List<double>();
            List<double> ld_p2 = new List<double>();


            //double surcharge = Ka * Gama_b * H * (d + d3 + d3);
            double surcharge = Ka * Gama_b * De * (d + d1 + d2);

            //double earth_pressure = 0.5 * Ka * Gama_b * H * (d + d3 + d3) * (d + d3 + d3);
            //double earth_pressure = 0.5 * Ka * Gama_b * H * (d + d1 + d2) * (d + d1 + d2);

            //chiranjit [2016 08 03]
            double earth_pressure = 0.5 * Ka * Gama_b * (d + d1 + d2) * (d + d1 + d2);




            int i = 0;
            for (i = 0; i < y_factors.Count; i++)
            {
                ld_p1.Add((y_factors[i] / y_factors[y_factors.Count - 1]) * earth_pressure + surcharge);
            }


            Earth_Pressure_Load epl = new Earth_Pressure_Load();
            EP_Loads = new Earth_Pressure_Load_Collection();

            ld_p1.Reverse();

            for (i = 0; i < 4; i++)
            {
                epl = new Earth_Pressure_Load();
                epl.MemberNos = mc_vertical[i].MemberNo.ToString();
                epl.Load_P1 = -ld_p1[i];
                epl.Load_P2 = -ld_p1[i + 1];
                EP_Loads.Add(epl);
            }

            for (i = 0; i < 4; i++)
            {
                epl = new Earth_Pressure_Load();
                epl.MemberNos = mc_vertical[mc_vertical.Count - 4 + i].MemberNo.ToString();
                epl.Load_P1 = ld_p1[i];
                epl.Load_P2 = ld_p1[i + 1];
                EP_Loads.Add(epl);
            }


            Applied_Load_Collection alc = new Applied_Load_Collection();
            Moving_Loads = new List<Applied_Load_Collection>();

            #region  LOAD 4

            alc.Load_Title = "LOAD 4 70R TRACK HOGG1 LL1";

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = 14.025;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[0].MemberNo + " TO " + mc_top[4].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -24.996;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[5].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -24.996;
            al.Start_Distance = 0.0;
            al.End_Distance = 0.058;
            alc.Add(al);


            Moving_Loads.Add(alc);


            #endregion

            #region  LOAD 5
            alc = new Applied_Load_Collection();
            alc.Load_Title = "LOAD 5 70R TRACK SAGG1 LL2";

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = 13.932;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[0].MemberNo + " TO " + mc_top[3].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -25.008;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[4].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -25.008;
            al.Start_Distance = 0.0;
            al.End_Distance = 0.371;
            alc.Add(al);


            Moving_Loads.Add(alc);


            #endregion

            #region  LOAD 6
            alc = new Applied_Load_Collection();
            alc.Load_Title = "LOAD 6 70R TRACK HOGG2 LL3";

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -19.786;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[1].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -32.197;
            al.Start_Distance = 0.622;
            al.End_Distance = 1.70;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[2].MemberNo + " TO " + mc_top[5].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -32.197;
            alc.Add(al);


            al = new Applied_Load();
            al.MemberNos = mc_top[6].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -32.197;
            al.Start_Distance = 0.0;
            al.End_Distance = 0.172;
            alc.Add(al);


            Moving_Loads.Add(alc);


            #endregion

            #region  LOAD 7
            alc = new Applied_Load_Collection();
            alc.Load_Title = "LOAD 7 70R TRACK SAGG2 LL4";

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = 12.184;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[3].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -22.47;
            al.Start_Distance = 0.196;
            al.End_Distance = 0.425;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[4].MemberNo + " TO " + mc_top[7].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -22.47;
            alc.Add(al);

            Moving_Loads.Add(alc);


            #endregion

            #region  LOAD 8
            alc = new Applied_Load_Collection();
            alc.Load_Title = "LOAD 8 70R TRACK WALL";

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = 11.732;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[3].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -21.861;
            al.Start_Distance = 0.271;
            al.End_Distance = 0.425;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[4].MemberNo + " TO " + mc_top[7].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -21.861;
            alc.Add(al);

            Moving_Loads.Add(alc);


            #endregion

            #region  LOAD 9
            alc = new Applied_Load_Collection();
            alc.Load_Title = "LOAD 9 70R WHEEL HOGG1";

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = 10.186;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[1].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -36.701;
            al.Start_Distance = 1.453;
            al.End_Distance = 1.70;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[1].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -39.654;
            al.Start_Distance = 0.083;
            al.End_Distance = 1.277;
            alc.Add(al);


            al = new Applied_Load();
            al.MemberNos = mc_top[2].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -36.701;
            al.Start_Distance = 0.0;
            al.End_Distance = 0.947;
            alc.Add(al);

            Moving_Loads.Add(alc);


            #endregion

            #region  LOAD 10
            alc = new Applied_Load_Collection();
            alc.Load_Title = "LOAD 10 70R WHEEL SAGG1";

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = 10.263;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[1].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -36.579;
            al.Start_Distance = 0.783;
            al.End_Distance = 1.70;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[2].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -36.579;
            al.Start_Distance = 0.0;
            al.End_Distance = 0.277;
            alc.Add(al);



            al = new Applied_Load();
            al.MemberNos = mc_top[2].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -40.347;
            al.Start_Distance = 0.453;
            al.End_Distance = 1.647;
            alc.Add(al);


            Moving_Loads.Add(alc);


            #endregion

            #region  LOAD 11
            alc = new Applied_Load_Collection();
            alc.Load_Title = "LOAD 11 70R WHEEL HOGG2";

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = 20.126;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[1].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -37.969;
            al.Start_Distance = 0.338;
            al.End_Distance = 1.532;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[2].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -37.457;
            al.Start_Distance = 0.008;
            al.End_Distance = 1.202;
            alc.Add(al);



            al = new Applied_Load();
            al.MemberNos = mc_top[5].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -37.416;
            al.Start_Distance = 0.508;
            al.End_Distance = 1.70;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[6].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -38.021;
            al.Start_Distance = 0.178;
            al.End_Distance = 1.372;
            alc.Add(al);


            Moving_Loads.Add(alc);


            #endregion

            #region  LOAD 12
            alc = new Applied_Load_Collection();
            alc.Load_Title = "LOAD 12 70R WHEEL SAGG2";

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = 23.472;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[2].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -39.573;
            al.Start_Distance = 0.363;
            al.End_Distance = 1.557;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[3].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -49.749;
            al.Start_Distance = 0.395;
            al.End_Distance = 0.425;
            alc.Add(al);


            al = new Applied_Load();
            al.MemberNos = mc_top[4].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -49.749;
            alc.Add(al);



            al = new Applied_Load();
            al.MemberNos = mc_top[5].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -49.749;
            al.Start_Distance = 0.0;
            al.End_Distance = 0.739;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[5].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -36.663;
            al.Start_Distance = 1.463;
            al.End_Distance = 1.7;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[6].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -36.663;
            al.Start_Distance = 0.0;
            al.End_Distance = 0.957;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[6].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -49.985;
            al.Start_Distance = 1.133;
            al.End_Distance = 1.7;
            alc.Add(al);


            al = new Applied_Load();
            al.MemberNos = mc_top[7].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -49.985;
            al.Start_Distance = 0.508;
            al.End_Distance = 1.70;
            alc.Add(al);

            Moving_Loads.Add(alc);


            #endregion

            #region  LOAD 13
            alc = new Applied_Load_Collection();
            alc.Load_Title = "LOAD 13 70R WHEEL WALL";

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = 10.069;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[1].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -37.318;
            al.Start_Distance = 0.483;
            al.End_Distance = 1.677;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[2].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -38.157;
            al.Start_Distance = 0.153;
            al.End_Distance = 1.194;
            alc.Add(al);


            Moving_Loads.Add(alc);


            #endregion

            #region  LOAD 14
            alc = new Applied_Load_Collection();
            alc.Load_Title = "LOAD 14 40T HOGG1";

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = 10.971;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[1].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -42.307;
            al.Start_Distance = 0.134;
            al.End_Distance = 1.327;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[1].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -40.001;
            al.Start_Distance = 1.354;
            al.End_Distance = 1.7;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[2].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -40.001;
            al.Start_Distance = 0.0;
            al.End_Distance = 0.847;
            alc.Add(al);


            Moving_Loads.Add(alc);


            #endregion

            #region  LOAD 15
            alc = new Applied_Load_Collection();
            alc.Load_Title = "LOAD 15 40T SAGG1";

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = 11.235;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[0].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -44.297;
            al.Start_Distance = 0.384;
            al.End_Distance = 0.475;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[1].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -44.297;
            al.Start_Distance = 0.0;
            al.End_Distance = 1.103;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[1].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -39.989;
            al.Start_Distance = 1.129;
            al.End_Distance = 1.7;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[2].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -39.989;
            al.Start_Distance = 0.0;
            al.End_Distance = 0.622;
            alc.Add(al);


            Moving_Loads.Add(alc);


            #endregion

            #region  LOAD 16
            alc = new Applied_Load_Collection();
            alc.Load_Title = "LOAD 16 40T HOGG2";

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = 10.908;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[1].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -40.051;
            al.Start_Distance = 1.429;
            al.End_Distance = 1.7;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[1].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -41.785;
            al.Start_Distance = 0.209;
            al.End_Distance = 1.402;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[2].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -40.051;
            al.Start_Distance = 0.0;
            al.End_Distance = 0.922;
            alc.Add(al);


            Moving_Loads.Add(alc);


            #endregion

            #region  LOAD 17
            alc = new Applied_Load_Collection();
            alc.Load_Title = "LOAD 17 40T SAGG2";

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = 11.701;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[4].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -47.802;
            al.Start_Distance = 0.109;
            al.End_Distance = 0.425;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[5].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -47.802;
            al.Start_Distance = 0.0;
            al.End_Distance = 0.817;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[5].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -39.98;
            al.Start_Distance = 1.354;
            al.End_Distance = 1.7;
            alc.Add(al);


            al = new Applied_Load();
            al.MemberNos = mc_top[6].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -39.98;
            al.Start_Distance = 0.0;
            al.End_Distance = 0.847;
            alc.Add(al);


            Moving_Loads.Add(alc);


            #endregion

            #region  LOAD 18
            alc = new Applied_Load_Collection();
            alc.Load_Title = "LOAD 18 40T WALL";

            al = new Applied_Load();
            al.MemberNos = mc_bottom[0].MemberNo + " TO " + mc_bottom[mc_bottom.Count - 1].MemberNo;
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = 10.808;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[1].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -40.254;
            al.Start_Distance = 0.559;
            al.End_Distance = 1.7;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[2].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -40.254;
            al.Start_Distance = 0.0;
            al.End_Distance = 0.052;
            alc.Add(al);

            al = new Applied_Load();
            al.MemberNos = mc_top[2].MemberNo + "";
            al.Direction = "GY";
            al.LoadType = "UNI";
            al.Load = -40.827;
            al.Start_Distance = 0.079;
            al.End_Distance = 1.272;
            alc.Add(al);

            Moving_Loads.Add(alc);


            #endregion

        }

        #region Chiranjit [2016 08 05] Load Calculation
        public double _H, _gama_b, _phi, _Ka, _q1, _q2, _Hw, _gama_w, _gama_d;

        public double _Hs, _Ds, _gama_c, _De, _gama_e;
        public double _FS, _BC, _AS, _oth_lds, _brng_presr, _Ah;
        public double _DL1;
        public double _K, _top_udl_total, _top_udl_ll, _top_udl_ec, _top_udl_dl, _HA, _PA, _Hb2, _Pb2, _Hb1, _Pb1;

        public List<string> Get_Load_Calculation()
        {
            List<string> list = new List<string>();
            #region Calculation Details

            if (false)
            {
                _brng_presr = 100;
                _H = 6.0;
                _gama_b = 19.0;
                _phi = 30;


                _q1 = 5.0;
                //Approach Slab Load for No Earth Cushion
                _q1 = 20.0;


                _q2 = 20.0;

                _Hw = 4.0;
                _gama_w = 10.0;

                _gama_d = 19.0;

                _Hs = 4.0;

                _Ds = 0.6;
                _gama_c = 25.0;
                _De = 1.0;

                _gama_e = 19.0;

            }


            list.Add(string.Format(""));
            list.Add(string.Format("Load Calculation for Multicell Box Culvert"));
            list.Add(string.Format("-------------------------------------------"));
            list.Add(string.Format(""));
            //list.Add(string.Format("User Input Data:"));
            //list.Add(string.Format("-------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("Height of Side Walls = H = 6.0 m"));
            list.Add(string.Format("Height of Side Walls = H = {0} m",_H));
            //list.Add(string.Format("Density of Backfill Material = Yb = 19.0 kN / Cum."));
            list.Add(string.Format("Density of Backfill Material = Yb = {0} kN/Cu.m.", _gama_b));
            //list.Add(string.Format("Angle of Internal Friction = ɸ = 30 Degrees"));
            list.Add(string.Format("Angle of Internal Friction = phi = {0} Degrees", _phi));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Forces on Side Walls"));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format(@"                        ___________        _                 "));
            list.Add(string.Format(@"                       /   |  |   |        |\                  "));
            list.Add(string.Format(@"                      /    |  |   |        | \                 "));
            list.Add(string.Format(@"                     /     |  |   |        |  \                 "));
            list.Add(string.Format(@"                    /      |  h   |        |   \                "));
            list.Add(string.Format(@"                   /       |  |   |        |    \               "));
            list.Add(string.Format(@"                  /        |  |   |        |     \              "));
            list.Add(string.Format(@"                 /         |  |   |        |      \              "));
            list.Add(string.Format(@"                /          | ---  H        |       \              "));
            list.Add(string.Format(@"               /           |      |        |        \              "));
            list.Add(string.Format(@"              /            |      |     ---|         \              "));
            list.Add(string.Format(@"             /             |      |      | |          \              "));
            list.Add(string.Format(@"            /              |      |      | |           \             "));
            list.Add(string.Format(@"           /               |      |     H/3|            \             "));
            list.Add(string.Format(@"          /                |      |      | |             \           "));
            list.Add(string.Format(@"         /                 |      |      | |              \           "));
            list.Add(string.Format(@"        --------------------     ---    --------------------     "));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


             _Ka = (1 - Math.Sin(MyList.Convert_Degree_To_Radian(_phi))) / (1 + Math.Sin(MyList.Convert_Degree_To_Radian(_phi)));
            list.Add(string.Format("Rankine’s Active Earth Pressure Coefficient = Ka "));
            list.Add(string.Format(""));
            list.Add(string.Format("      Ka = (1 - sin ɸ) / (1 + sin ɸ) "));
            //list.Add(string.Format("         = (1 - sin 30) / (1 + sin 30) = 0.5/1.5 = 0.33"));
            list.Add(string.Format("         = (1 - sin {0}) / (1 + sin {0})", _phi));
            list.Add(string.Format("         = {0:f3} / {1:f3}", (1 - Math.Sin(MyList.Convert_Degree_To_Radian(_phi))), (1 + Math.Sin(MyList.Convert_Degree_To_Radian(_phi)))));
            list.Add(string.Format("         = {0:f3}", _Ka));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("1. Lateral Force for Surcharge Load "));
            list.Add(string.Format("------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("1.1        Earth Cushion Surcharge Load = q1 = 5.0 kN/m."));
            //list.Add(string.Format("1.1 Earth Cushion Surcharge Load = q1 = {0} kN/m.", _q1));
            list.Add(string.Format(""));
            //list.Add(string.Format("     Or    Approach Slab Load for No Earth Cushion = q1 = 20.0 kN/m."));
            if (Is_Only_Sidewall)
            {
                list.Add(string.Format("Approach Slab Load for No Earth Cushion = q1 = {0} kN/m.", _q1));
            }
            else
            {
                //list.Add(string.Format("Earth Cushion Surcharge Load = q1 = {0} kN/m.", _q1));

                list.Add(string.Format("Thickness of Earth Cushion = De = {0} m.", _De));
                list.Add(string.Format("Density of Earth in Earth Cushion = Ye = {0} kN/Cu.m.", _gama_e));
                list.Add(string.Format(""));
                _q1 = _De * _gama_e;
                list.Add(string.Format("UDL on Top Members for Earth Cushion Load = q1 = De x Ye = {0} x 1.0 x {1} = {2:f3} kN/m. (per metre length)", _De, _gama_e, _q1));

            }






            //list.Add(string.Format("Thickness of Earth Cushion = De = {0} m.", _De));
            //list.Add(string.Format("Density of Earth in Earth Cushion = Ye = {0} kN/m.", _gama_e));



            
//Thickness of Earth Cushion = De = 1.000 m.
//Density of Earth in Earth Cushion = Ye = 19.000 kN/Cum.

//UDL on Top Members for Earth Cushion Load = q1 = De x Ye = 1.000 x 1.000 x 19.000 = 19.000 kN/m. (per metre length)




            //list.Add(string.Format("1.2        Live Load for Vehicles = q2 = 20.0 kN/m."));
            list.Add(string.Format("Live Load for Vehicles = q2 = {0} kN/m.", _q2));

            double _q = _q1 + _q2;
            //list.Add(string.Format("Pressure on Side Walls = q = q1 + q2 = 20.0 + 20.0 = 40.0 kN/m"));
            list.Add(string.Format("Pressure on Side Walls = q = q1 + q2 = {0} + {1} = {2} kN/m", _q1, _q2, _q));

            double _Qs = _Ka * _q * _H;
            //list.Add(string.Format("Force on Side Walls = Qs = Ka x q x H = 0.33 x 40.0 x 6.0 = 79.2 kN"));
            list.Add(string.Format("Force on Side Walls = Qs = Ka x q x H = {0:f3} x {1:f3} x {2:f3} = {3:f3} kN", _Ka, _q, _H, _Qs));



            //list.Add(string.Format("Force Acting at height from bottom = Hs = H/2.0 = 6.0/2.0 = 3.0 m."));

            _Hs = _H / 2.0;
            list.Add(string.Format("Force Acting at height from bottom = Hs = H / 2.0 = {0:f3} / 2.0 = {1:f3} m.", _H, _Hs));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("2. Force for Water Pressure "));
            list.Add(string.Format("-----------------------------"));
            list.Add(string.Format(""));


            //list.Add(string.Format("Height of Water Table = Hw = 4.0 m."));
            list.Add(string.Format("Height of Water Table = Hw = {0:f3} m.", _Hw));
            //list.Add(string.Format("Density of Water = Yw = 10.0 kN/Cum."));
            list.Add(string.Format("Density of Water = Yw = {0:f3} kN/Cu.m.", _gama_w));

            double _p = _gama_w * _Hw;
            //list.Add(string.Format("Pressure on Side Walls = p = Yw x Hw"));
            //list.Add(string.Format("Pressure on Side Walls = p = Yw x Hw = {0:f3} x {1:f3} = {2:f3} kN/Sq.m", _gama_w, _Hw, _p));
            list.Add(string.Format("Pressure at Bottom of Side Walls = p = Yw x Hw = {0:f3} x {1:f3} = {2:f3} kN/Sq.m", _gama_w, _Hw, _p));
            //list.Add(string.Format("Force on Side Walls = Pwt = 0.5 x  Yw x Hw2 = 0.5 x 0.33 x 10.0 x 4.0 x 4.0 = 26.4 kN"));

            double _Pwt = 0.5 * _Ka * _gama_w * _Hw * _Hw;
            //list.Add(string.Format("Force on Side Walls = Pwt = 0.5 x  Yw x Hw^2 = 0.5 x 0.33 x 10.0 x 4.0 x 4.0 = 26.4 kN"));
            list.Add(string.Format("Force on Side Walls = Pwt = 0.5 x Ka x Yw x Hw^2 = 0.5 x {0:f3} x  {1:f3} x  {2:f3} ^ 2 = {3:f3} kN", _Ka, _gama_w, _Hw, _Pwt));

            list.Add(string.Format(""));

            double _Hwt = _Hw / 3.0;
            //list.Add(string.Format("Force Acting at height from bottom = Hwt = Hw/3.0 = 4.0/3.0 = 1.33 m."));
            list.Add(string.Format("Force Acting at height from bottom = Hwt = Hw/3.0 = {0}/3 = {1:f3} m.", _Hw, _Hwt));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("3. Backfill Pressure "));
            list.Add(string.Format("---------------------------"));
            list.Add(string.Format(""));
            double _Hd = _H - _Hw;

            list.Add(string.Format("Height of Dry Backfill = Hd = H - Hw = {0} - {1} = {2} m.", _H, _Hw, _Hd));
            list.Add(string.Format(""));

            //list.Add(string.Format("Density of Dry Backfill = Yd = 19.0 kN / Cum. (from above)"));
            list.Add(string.Format("Density of Dry Backfill = Yd = {0} kN / Cum. (from above)", _gama_d));
            list.Add(string.Format(""));
            list.Add(string.Format("Pressure on Side Walls for Dry Backfill = pb1 = Ka x Yd x Hd "));
            list.Add(string.Format(""));


            _Pb1 = 0.5 *  _Ka * _gama_d * _Hd * _Hd;
            
            list.Add(string.Format("Force on Side Walls for Dry Backfill = Pb1 = 0.5 x  Ka x Yd x Hd^2 "));
            //list.Add(string.Format("                                     = 0.5 x 0.33 x 19.0 x 2.0 x 2.0 = 12.54 kN"));
            list.Add(string.Format("                                     = 0.5 x {0:f3} x {1:f3} x {2:f3}^2 = {3:f3} kN", _Ka, _gama_d, _Hd, _Pb1));
            list.Add(string.Format(""));

            _Hb1 = (_Hd / 3.0) + _Hw;
            //list.Add(string.Format("Force Acting at height from bottom = Hb1 = (Hd/3.0) + Hw = (2.0/3.0) + 4.0 = 4.67 m."));
            list.Add(string.Format("Force Acting at height from bottom = Hb1 = (Hd/3.0) + Hw = ({0:f3}/3.0) + {1:f3} = {2:f3} m.", _Hd, _Hw, _Hb1));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            //list.Add(string.Format("3.2        Height of Submerged Backfill = Hs = 4.0 m.  (Hs = Hw)"));
            list.Add(string.Format("Height of Submerged Backfill = Hs = {0:f3} m.  {1}", _Hs, (_Hs == _Hw) ? "Hs = Hw" : ""));

            double _gama_s = _gama_d - _gama_w;
            //list.Add(string.Format("       Density of Submerged Backfill = Ys = 9.0 kN / Cum. (Ys = Yd - Yw)"));
            list.Add(string.Format("Density of Submerged Backfill = Ys = {0:f3} kN / Cum. (Ys = Yd - Yw)", _gama_s));



            list.Add(string.Format("Pressure on Side Walls for Dry Backfill = pb2 = Ka x Ys x Hs "));
            list.Add(string.Format("Force on Side Walls for Dry Backfill = Pb2 = 0.5 x  Ka x Ys x Hs^2 "));

            _Pb2 = 0.5 * _Ka * _gama_s * _Hs * _Hs;
            //list.Add(string.Format("                                           = 0.5 x 0.33 x 9.0 x 4.0 x 4.0 = 23.76 kN"));
            list.Add(string.Format("                                           = 0.5 x {0:f3} x {1:f3} x {2:f3} ^ 2 = {3:f3} kN", _Ka, _gama_s, _Hs, _Pb2));


            _Hb2 = _Hs / 3.0;
            //list.Add(string.Format("Force Acting at height from bottom = Hb2 = Hs/3.0 = 4.0/3.0 = 1.33 m."));
            list.Add(string.Format("Force Acting at height from bottom = Hb2 = Hs/3.0 = {0:f3}/3.0 = {1:f3} m.", _Hs, _Hb2));

            _PA = _Qs + _Pwt + _Pb1 + _Pb2;
            //list.Add(string.Format("Resultant Force = PA = Qs + Pwt + Pb1 + Pb2 = 79.2 + 26.4 + 12.54 + 23.76 = 141.9 kN"));
            list.Add(string.Format("Resultant Force = PA = Qs + Pwt + Pb1 + Pb2 = {0:f3} + {1:f3} + {2:f3} + {3:f3} = {4:f3} kN", _Qs, _Pwt, _Pb1, _Pb2, _PA));
            list.Add(string.Format(""));
            list.Add(string.Format("Taking Moment about bottom of the wall,"));
            list.Add(string.Format("Acting at Height = HA = ((Qs x Hs) + (Pwt x Hwt) + (Pb1 x Hb1) + (Pb2 x Hb2)) / PA"));
            list.Add(string.Format(""));

            _HA = ((_Qs * _Hs) + (_Pwt * _Hwt) + (_Pb1 * _Hb1) + (_Pb2 * _Hb2)) / _PA;
            //list.Add(string.Format("                      = ((79.2 x 3.0) + (26.4 x 1.33) + (12.54 x 4.67) + (23.76 x 1.33)) / 141.9 =  362.8746 / 141.9 = 2.56 m."));
            list.Add(string.Format("                      = (({0:f3} x {1:f3}) + ({2:f3} x {3:f3}) + ({4:f3} x {5:f3}) + ({6:f3} x {7:f3})) / {8:f3}",
                _Qs, _Hs, _Pwt, _Hwt, _Pb1, _Hb1, _Pb2, _Hb2, _PA));
            //list.Add(string.Format("                      = 362.8746 / 141.9 = 2.56 m."));
            list.Add(string.Format("                      = {0:f3} / {1:f3} = {2:f3} m.", _HA * _PA, _PA, _HA));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("(Two Nodes are to be created at height of 2.56 m from bottom on two side walls, "));
            //list.Add(string.Format("where JOINT LOAD of PA = 141.9 kN will be applied from two opposite directions, on two walls.)"));
            list.Add(string.Format("4. Forces on Top Slabs"));
            list.Add(string.Format("-----------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Dead Load"));
            list.Add(string.Format("---------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            //list.Add(string.Format("Thickness of Top Slab = Ds = 0.60 m."));
            list.Add(string.Format("Thickness of Top Slab = Ds = {0:f3} m.", _Ds));
            //list.Add(string.Format("Density of Concrete = Yc = 25.0 kN/Cum."));
            list.Add(string.Format("Density of Concrete = Yc = {0} kN/Cu.m.", _gama_c));
            list.Add(string.Format(""));
            //list.Add(string.Format("UDL on Top Members for Dead Load = Ds x Yc = 0.60 x 25.0 = 15.0 kN/m."));

            _top_udl_dl = _Ds * _gama_c * w;
            list.Add(string.Format("UDL on Top Members for Dead Load = Ds x w x Yc = {0} x {1} x {2} = {3:f3} kN/m. (per metre length)", _Ds, w, _gama_c, _top_udl_dl));
            list.Add(string.Format(""));
            
            list.Add(string.Format(""));
            list.Add(string.Format("Live Load"));
            list.Add(string.Format("------------"));
            list.Add(string.Format(""));
            //list.Add(string.Format("Vehicle Live Load = LL = q2 = 20.0 kN/m.  (from 1.3 above)"));
            list.Add(string.Format("Vehicle Live Load = LL = q2 = {0:f3} kN/m.  (from above)", _q2));
            list.Add(string.Format(""));

            _top_udl_ll = _q2;

            //list.Add(string.Format("UDL on Top Members for Live Load = 20.0 kN/m.  "));
            list.Add(string.Format("UDL on Top Members for Live Load = {0:f3} kN/m.  ", _top_udl_ll));
            list.Add(string.Format(""));

            list.Add(string.Format(""));
            list.Add(string.Format("Earth Cushion Load"));
            list.Add(string.Format("----------------------"));
            list.Add(string.Format(""));
            _top_udl_ec = 0.0;
            if (Is_Only_Sidewall == false)
            {
                //list.Add(string.Format(""));
                //list.Add(string.Format("Earth Cushion Load"));
                //list.Add(string.Format("----------------------"));
                //list.Add(string.Format(""));

                //list.Add(string.Format("Thickness of Earth Cushion = De = 1.0 m."));
                list.Add(string.Format("Thickness of Earth Cushion = De = {0:f3} m.", _De));
                //list.Add(string.Format("Density of Earth in Earth Cushion = Ye = 19.0 kN/Cum."));
                list.Add(string.Format("Density of Earth in Earth Cushion = Ye = {0:f3} kN/Cum.", _gama_e));
                list.Add(string.Format(""));

                _top_udl_ec = _De * _gama_e * w;
                //list.Add(string.Format("UDL on Top Members for Earth Cushion Load = De x Ye = 1.0 x 19.0 = 19.0 kN/m."));
                list.Add(string.Format("UDL on Top Members for Earth Cushion Load = De x W x Ye = {0:f3} x {1:f3} x {2:f3} = {3:f3} kN/m. (per metre length)", _De, w, _gama_e, _top_udl_ec));
                list.Add(string.Format(""));
            }
            else
            {
                list.Add(string.Format("UDL on Top Members for Earth Cushion Load = 0.0 kN/m. (per metre width)", _De, b, _gama_e, _top_udl_ec));
            }
            _top_udl_total = _top_udl_dl + _top_udl_ec + _top_udl_ll + SIDL;
            //list.Add(string.Format("Total UDL on Top Members = 15.0 + 19.0 + 20.0 = 54.0 kN/m."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("SIDL on Top Slab(s)"));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("UDL on Top Members for SIDL = {0:f3} kN/m. (per metre width)", SIDL));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Total UDL on Top Slab(s)"));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("Total UDL on Top Members = {0:f3} + {1:f3} + {2:f3} + {3:f3} = {4:f3} kN/m.", _top_udl_dl, _top_udl_ec, _top_udl_ll, SIDL, _top_udl_total));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("5. Forces on Bottom Slabs"));
            list.Add(string.Format("-------------------------"));
            list.Add(string.Format(""));

            //list.Add(string.Format("7. Bearing Pressure = 100.0 kN/m"));
            list.Add(string.Format("Bearing Pressure = {0} kN/m", _brng_presr));
            list.Add(string.Format(""));
            list.Add(string.Format("Other Loads = {0:f3} kN/m.", _oth_lds));
            list.Add(string.Format(""));
            //list.Add(string.Format("FS = Factor of Safety = 1.0"));
            list.Add(string.Format("FS = Factor of Safety = {0}", _FS));
            //list.Add(string.Format("BC = Bearing Capacity = 100.0 kN/m"));
            list.Add(string.Format("BC = Bearing Capacity = {0} kN/m", _BC));
            list.Add(string.Format("AS = Allowable Settlement = {0} m.", _AS));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

             _K = (_FS * _BC) / _AS;

            list.Add(string.Format("K = Modulus of Subgrade Reaction = Bearing Capacity per unit settlement"));
            list.Add(string.Format(""));

            list.Add(string.Format("  = (FS x BC) / AS"));
            //list.Add(string.Format("  = 1.0 x 100.0 / 0.025"));
            list.Add(string.Format("  = {0} x {1} / {2}", _FS, _BC, _AS));
            list.Add(string.Format("  = {0} kN / Sqm. / m.", _K));
            list.Add(string.Format(""));
            list.Add(string.Format("(Refer to Page 49, ASTRA Pro User’s manual)"));
            list.Add(string.Format(""));
            list.Add(string.Format("Specifications to define Spring Constant at Support"));
            list.Add(string.Format(""));
            list.Add(string.Format("This Constant is applied as Joint “Loads/Unit Displacement” on specified supports."));
            //list.Add(string.Format("The essential data syntax is:"));
            list.Add(string.Format(""));
            //list.Add(string.Format("Example Data 1:"));
            //list.Add(string.Format("SUPPORT"));
            //list.Add(string.Format("1 TO 6 FIXED BUT MZ KFY 2000 (This may be : At bottom Nodes  E, F, G, H   PINNED KFY 4000)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            _DL1 = 0.0;
            if (Apply_Seismic)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("For Applying Seismic Force:"));
                list.Add(string.Format("----------------------------"));
                //list.Add(string.Format("Z  = Zone factor = {0}", _Z));
                //list.Add(string.Format("I  = Importance factor = {0}", _I));
                //list.Add(string.Format("R  = Response reduction factor = {0}", _R));
                //list.Add(string.Format(" (sa/ g) = {0}", _Sa_by_g));
                //list.Add(string.Format(""));

                //double _Ah = (_Z / 2.0) * (_Sa_by_g) / (_R / _I);
                //list.Add(string.Format("Coefficient of Horizontal Seismic Force = Ah = (Z/2) x (sa/ g) / (R/I)"));
                //list.Add(string.Format("                                             = ({0}/2) x ({1}) / ({2}/{3})", _Z, _Sa_by_g, _R, _I));
                //list.Add(string.Format("                                             = {0:f3}", _Ah));
                //list.Add(string.Format(""));

                list.Add(string.Format("Coefficient of Horizontal Seismic Force = Ah = (Z/2) x (sa/ g) / (R/I) = {0}", _Ah));


                list.Add(string.Format("Horizontal Seismic Force = Ah x (Dead Load + Live Load)"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("Thickness of Top Slab = d1 = {0:f3} m.", _Ds));
                list.Add(string.Format("Thickness of Bottom Slab = d2 = {0:f3} m.", _Ds));
                list.Add(string.Format("Thickness of Side Wall = d3 = {0:f3} m.", d3));
                list.Add(string.Format("Thickness of Intermediate Wall = d4 = {0:f3} m.", d4));
                //list.Add(string.Format("Density of Concrete = Yc = 25.0 kN/Cum."));
                list.Add(string.Format("Density of Concrete = Yc = {0} kN/Cu.m.", _gama_c));
                list.Add(string.Format(""));
                list.Add(string.Format("Inside Clear Span = b = {0:f3} m", b));
                //list.Add(string.Format("Inside Clear Depth = d = {0:f3} m", d));
                list.Add(string.Format("Height of Wall = H = {0:f3} m", _H));
                list.Add(string.Format(""));
                list.Add(string.Format("Width of Culvert = W = {0:f3} m", w));
                list.Add(string.Format(""));
                list.Add(string.Format("Total No. of Cells = n = {0}", Cell_Nos));
                list.Add(string.Format(""));


                #region Top Slab
                list.Add(string.Format(""));
                list.Add(string.Format("Dead Load on Top Slab"));
                list.Add(string.Format("----------------------"));
                list.Add(string.Format(""));
                //double vv1 = (b * Cell_Nos) + (d3 * 2) + (d4 * (Cell_Nos - 1));

                //list.Add(string.Format("Total Length = (b * n) + (d3 * 2) + (d4 * (n - 1))"));
                //list.Add(string.Format("             = ({0} * {1}) + ({2} * 2) + ({3} * ({1} - 1))", b, Cell_Nos, d3, d4));
                //list.Add(string.Format("             = {0:f3} m", vv1));
                //list.Add(string.Format(""));
                //list.Add(string.Format(""));


                //double vv1 = (b * Cell_Nos) + (d3 * 2) + (d4 * (Cell_Nos - 1));

                //list.Add(string.Format("Total Length = (b * n) + (d3 * 2) + (d4 * (n - 1))"));
                //list.Add(string.Format("             = ({0} * {1}) + ({2} * 2) + ({3} * ({1} - 1))", b, Cell_Nos, d3, d4));
                //list.Add(string.Format("             = {0:f3} m", vv1));
                //list.Add(string.Format(""));
                //list.Add(string.Format(""));

                double dl1 = b * d1 * w * Gama_c * Cell_Nos;
                list.Add(string.Format("Applied Load  = b X d1 X W X n X Yc = {0:f3} X {1} X {2} X {3} X {4} = {5:f3} kN", b, d1, w, Cell_Nos, Gama_c, dl1));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

            #endregion Top Slab


                #region Bottom Slab
                list.Add(string.Format("Dead Load on Bottom Slab"));
                list.Add(string.Format("----------------------"));
                list.Add(string.Format(""));

                //double vv2 = (b * Cell_Nos) + (d3 * 2) + (d4 * (Cell_Nos - 1));

                //list.Add(string.Format("Total Length = (b * n) + (d3 * 2) + (d4 * (n - 1))"));
                //list.Add(string.Format("             = ({0} * {1}) + ({2} * 2) + ({3} * ({1} - 1))", b, Cell_Nos, d3, d4));
                //list.Add(string.Format("             = {0:f3} m", vv1));
                //list.Add(string.Format(""));
                //list.Add(string.Format(""));




                double dl2 = b * d2 * w * Gama_c * Cell_Nos;
                list.Add(string.Format("Applied Load = b X d2 X W X n X Yc = {0} X {1} X {2} X {3} X {4} = {5:f3} kN", b, d2, w, Cell_Nos, Gama_c, dl2));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                #endregion Top Slab








                #region Side Wall
                list.Add(string.Format("Dead Load on Side Wall"));
                list.Add(string.Format("----------------------"));
                list.Add(string.Format(""));

                double dl3 = (_H * d3 * 2 * w) * Gama_c;
                list.Add(string.Format("Applied Load = (H X d3 X W X 2) X Yc = {0} X {1} X {2}  X 2 X {3} = {4:f3} kN", _H, d3, w, Gama_c, dl3));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                #endregion Side Wall


                #region Intermediate Wall
                list.Add(string.Format("Dead Load on Intermediate Wall"));
                list.Add(string.Format("-------------------------------"));
                list.Add(string.Format(""));

                double dl4 = (_H * d4 * (Cell_Nos - 1)) * w * Gama_c;
                list.Add(string.Format("Applied Load = (H X d4 X W X (n - 1)) X Yc = {0} X {1} X {2} X ({3}-1) X {4} = {5:f3} kN", _H, d4, w, Cell_Nos, Gama_c, dl4));

                #endregion Intermediate Wall


                list.Add(string.Format(""));
                list.Add(string.Format(""));
                list.Add(string.Format("Earth Cushion Load = q1 = {0} kN/m", _q1));
                list.Add(string.Format("Applied Live Load = q2  = {0} kN/m", _q2));


                list.Add(string.Format(""));
                //list.Add(string.Format("Total Applied Load = dl1 + dl2 + dl3 + dl4 + (_q1 * vv1) + (_q2 * vv1)"));


                double vv1 = (b * Cell_Nos) + 2 * d3 + (Cell_Nos - 1) * d4;

                _DL1 = dl1 + dl2 + dl3 + dl4 + (_q1 * vv1) + (_q2 * vv1);



                list.Add(string.Format("Total Applied Force = {0:f3} + {1:f3} + {2:f3} + {3:f3} + ({4:f3} * {5}) + ({6} * {7:f3})", dl1, dl2, dl3, dl4, _q1, vv1, _q2, vv1));
                list.Add(string.Format("                    = {0:f3} kN", _DL1));
                list.Add(string.Format(""));

                //list.Add(string.Format(""));
                //list.Add(string.Format("Total Applied Load = (Ds X Yc) + (d3 X Yc) + (d4 X Yc) + _q1 + _q2"));
                //list.Add(string.Format("Total Applied Load = ({0} X {1}) + ({0} X {1}) + ({0} X {1}) + _q1 + _q2"));
                list.Add(string.Format(""));
                //list.Add(string.Format("UDL on Top Members for Dead Load = Ds x Yc = 0.60 x 25.0 = 15.0 kN/m."));

                //_DL1 = (d3 * _gama_c * 2 * d) + (_Ds * _gama_c * Cell_Nos * b) + (d4 * _gama_c * (Cell_Nos - 1) * d) + _q1 * b + _q2 * b;


                _DL1 = _DL1 * _Ah;

                list.Add(string.Format(""));
                list.Add(string.Format("Applied Seismic Force = {0:f3} X {1:f3} = {2:f3} kN", _DL1 / _Ah, _Ah, _DL1));
                list.Add(string.Format(""));
                list.Add(string.Format(""));

                list.Add(string.Format("(To be applied as Joint Load on Support Joints only)."));

            }

            #endregion Calculation Details


            File.WriteAllLines(Path.Combine(file_path, "LOAD_CALCULATION.TXT"), list.ToArray());
            return list;
        }

        #endregion Chiranjit [2016 08 05] Load Calculation

        public void Set_Loads_From_Grid(DataGridView dgv_dl_sidl, DataGridView dgv_epl, DataGridView dgv_ll, DataGridView dgv_load_comb)
        {

            double ld = 0.0;
            double val1 = 0.0;
            double val2 = 0.0;

            string kStr = "";
            string dir = "";

            int i = 0;

            Applied_Load ap = new Applied_Load();

            bool check = true;

            DL_Loads.Clear();
            SIDL_Loads.Clear();
            int flag = 0;

            #region DL + SIDL
            for (i = 0; i < dgv_dl_sidl.RowCount; i++)
            {
                check = (bool.Parse(dgv_dl_sidl[0, i].Value.ToString()));
                kStr = dgv_dl_sidl[1, i].Value.ToString();

                if (!kStr.StartsWith("LOAD"))
                {
                    dir = dgv_dl_sidl[2, i].Value.ToString();
                    ld = MyList.StringToDouble(dgv_dl_sidl[4, i].Value.ToString(), 0.0);

                    if (dir.Contains("-"))
                        ld = -ld;

                    if (check)
                    {
                        ap = new Applied_Load();
                        ap.MemberNos = kStr;
                        ap.Load = ld;
                        if (flag == 1)
                            DL_Loads.Add(ap);
                        else
                            SIDL_Loads.Add(ap);
                    }
                }
                else
                    flag++;
            }
            #endregion DL + SIDL

            #region Earth_Pressure_Load
            Earth_Pressure_Load ep = new Earth_Pressure_Load();

            EP_Loads.Clear();
            for (i = 0; i < dgv_epl.RowCount; i++)
            {
                check = (bool.Parse(dgv_epl[0, i].Value.ToString()));
                kStr = dgv_epl[1, i].Value.ToString();

                if (!kStr.StartsWith("LOAD"))
                {
                    dir = dgv_epl[2, i].Value.ToString();
                    val1 = MyList.StringToDouble(dgv_epl[4, i].Value.ToString(), 0.0);
                    val2 = MyList.StringToDouble(dgv_epl[5, i].Value.ToString(), 0.0);

                    if (dir.Contains("-"))
                    {
                        val1 = -val1;
                        val2 = -val2;
                    }
                    if (check)
                    {
                        ep = new Earth_Pressure_Load();
                        ep.MemberNos = kStr;
                        ep.Load_P1 = val1;
                        ep.Load_P2 = val2;

                        EP_Loads.Add(ep);
                    }
                }
            }

            #endregion Earth_Pressure_Load

            #region Moving_Loads

            Moving_Loads.Clear();

            Applied_Load_Collection alc = null;

            for (i = 0; i < dgv_ll.RowCount; i++)
            {
                check = (bool.Parse(dgv_ll[0, i].Value.ToString()));
                kStr = dgv_ll[1, i].Value.ToString();

                if (!kStr.StartsWith("LOAD"))
                {

                    dir = dgv_ll[2, i].Value.ToString();
                    ld = MyList.StringToDouble(dgv_ll[4, i].Value.ToString(), 0.0);
                    val1 = MyList.StringToDouble(dgv_ll[5, i].Value.ToString(), 0.0);
                    val2 = MyList.StringToDouble(dgv_ll[6, i].Value.ToString(), 0.0);

                    if (dir.Contains("-"))
                        ld = -ld;

                    if (check)
                    {
                        ap = new Applied_Load();
                        ap.MemberNos = kStr;
                        ap.Load = ld;
                        ap.Start_Distance = val1;
                        ap.End_Distance = val2;
                        alc.Add(ap);
                    }
                }
                else
                {
                    if (alc != null)
                        Moving_Loads.Add(alc);

                    alc = new Applied_Load_Collection();
                    alc.Load_Title = kStr;
                }
            }
            if (alc != null)
                Moving_Loads.Add(alc);
            #endregion Moving_Loads


            #region Earth_Pressure_Load
            Load_Combination lcmb = new Load_Combination();

            Combine_Loads.Clear();
            for (i = 0; i < dgv_load_comb.RowCount; i++)
            {
                check = (bool.Parse(dgv_load_comb[0, i].Value.ToString()));
                kStr = dgv_load_comb[1, i].Value.ToString();

                if (!kStr.StartsWith("LOAD"))
                {
                    if (check)
                    {
                        lcmb = new Load_Combination();
                        lcmb.LoadCase = MyList.StringToInt(kStr, 1);

                        for (int c = 2; c < dgv_load_comb.ColumnCount; c++)
                        {
                            if (dgv_load_comb[c, i].Value != null)
                            {
                                kStr = dgv_load_comb[c, i].Value.ToString();
                                if (kStr != "")
                                    lcmb.Load_Comb.Add(dgv_load_comb[c, i].Value.ToString());
                            }
                        }
                        Combine_Loads.Add(lcmb);
                    }
                }
            }

            #endregion Earth_Pressure_Load


        }

        public List<string> Get_Load_data()
        {
            List<string> loads = new List<string>();
            int i = 0;
            loads.Clear();

            if (DL_Loads.Count > 1)
            {
                loads.Add("LOAD 1 DEAD LOAD");
                loads.Add("MEMBER LOAD");
                for (i = 0; i < DL_Loads.Count; i++)
                {
                    loads.Add(DL_Loads[i].ToString());
                }
            }

            if (SIDL_Loads.Count > 1)
            {
                loads.Add("LOAD 2 SUPER IMPOSED DEAD LOAD");
                loads.Add("MEMBER LOAD");

                for (i = 0; i < SIDL_Loads.Count; i++)
                {
                    loads.Add(SIDL_Loads[i].ToString());
                }
            }

            if (EP_Loads.Count > 1)
            {
                loads.Add("LOAD 3 EARTH PRESSURE");
                loads.Add("MEMBER LOAD");
                for (i = 0; i < EP_Loads.Count; i++)
                {
                    loads.Add(EP_Loads[i].ToString());
                }
            }

            if (Moving_Loads.Count > 1)
            {
                for (i = 0; i < Moving_Loads.Count; i++)
                {
                    loads.Add(Moving_Loads[i].Load_Title);
                    loads.Add("MEMBER LOAD");
                    for (int j = 0; j < Moving_Loads[i].Count; j++)
                    {
                        loads.Add(Moving_Loads[i][j].ToString());
                    }
                }
            }


            if (Combine_Loads.Count > 1)
            {
                loads.AddRange(Combine_Loads.Get_Report().ToArray());
            }


            #region Combine Loads
            //loads.Add("LOAD COMB 19 DL+EP");
            //loads.Add("1  1.0    3  1.0");
            //loads.Add("LOAD COMB 20 DL+EP+SIDL");
            //loads.Add("1  1.0   2  1.0    3  1.0");
            //loads.Add("LOAD COMB 21 DL+EP+SIDL+LL1");
            //loads.Add("1  1.0   2  1.0    3  1.0    4  1.0");
            //loads.Add("LOAD COMB 22 DL+EP+SIDL+LL2");
            //loads.Add("1  1.0   2  1.0 3 1.0    5  1.0");
            //loads.Add("LOAD COMB 23 DL+EP+SIDL+LL3");
            //loads.Add("1  1.0   2  1.0 3 1.0    6  1.0");
            //loads.Add("LOAD COMB 24 DL+EP+SIDL+LL4");
            //loads.Add("1  1.0   2  1.0 3  1.0   7  1.0");
            //loads.Add("LOAD COMB 25 DL+EP+SIDL+LL5");
            //loads.Add("1  1.0   2  1.0 3  1.0   8  1.0");
            //loads.Add("LOAD COMB 26 DL+EP+SIDL+LL6");
            //loads.Add("1  1.0   2   1.0    3  1.0   9  1.0");
            //loads.Add("LOAD COMB 27 DL+EP+SIDL+LL7");
            //loads.Add("1  1.0   2   1.0    3  1.0   10  1.0");
            //loads.Add("LOAD COMB 28 DL+EP+SIDL+LL8");
            //loads.Add("1  1.0    2  1.0    3  1.0   11   1.0");
            //loads.Add("LOAD COMB 29 DL+EP+SIDL+LL9");
            //loads.Add("1  1.0    2  1.0    3  1.0   12   1.0");
            //loads.Add("LOAD COMB 30 DL+EP+SIDL+LL10");
            //loads.Add("1  1.0   2  1.0    3  1.0   13   1.0");
            //loads.Add("LOAD COMB 31 DL+EP+SIDL+LL11");
            //loads.Add("1  1.0   2  1.0 3 1.0   14   1.0");
            //loads.Add("LOAD COMB 32 DL+EP+SIDL+LL12");
            //loads.Add("1  1.0   2   1.0    3  1.0   15    1.0");
            //loads.Add("LOAD COMB 33 DL+EP+SIDL+LL13");
            //loads.Add("1  1.0    2  1.0    3  1.0    16   1.0");
            //loads.Add("LOAD COMB 34 DL+EP+SIDL+LL14");
            //loads.Add("1 1.0    2  1.0 3 1.0    17   1.0");
            //loads.Add("LOAD COMB 35 DL+EP+SIDL+LL15");
            //loads.Add("1  1.0   2  1.0 3  1.0   18   1.0");
            #endregion Combine Loads

            return loads;
        }


        
        #region Variable Initialization

        //public double H, b, d, d1, d2, d3, gamma_b, gamma_c, R, t, j, cover;
        public double gamma_b, gamma_c, m, R, t, j, cover;


        public double b1, b2, a1, w1, w2, b3, F, S, sbc, sigma_st, sigma_c;
        public double bar_dia_top, bar_dia_side, bar_dia_bottom, bar_dia_intermediate;

        public double dia_shr, fy, fck;
        public double Self_Weight_Factor { get; set; }

        public CONCRETE_GRADE Con_Grade;

        public List<double> lst_Bar_Dia = null;
        public List<double> lst_Bar_Space = null;

        #endregion

        #region Drawing Variables

        double bd1, bd2, bd3, bd4, bd5, bd6, bd7, bd8, bd9, bd10, bd11, bd12, bd13, bd14, bd15;
        double sp1, sp2, sp3, sp4, sp5, sp6, sp7, sp8, sp9, sp10, sp11, sp12, sp13, sp14, sp15;
        public double _pressure = 0.0;

        #endregion

        public void Calculate_Program()
        {
            string file_name = "";
            file_name = rep_file_name;
            gamma_b = Gama_b;
            gamma_c = Gama_c;
            lst_Bar_Dia = new List<double>();
            lst_Bar_Space = new List<double>();

            string ref_string = "";
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));

            try
            {
                #region TechSOFT Banner
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t*            ASTRA Pro Release 22             *");
                sw.WriteLine("\t\t*        TechSOFT Engineering Services        *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t*       DESIGN OF MULTI CELL BOX CULVERT      *");
                sw.WriteLine("\t\t*                                             *");
                sw.WriteLine("\t\t***********************************************");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t----------------------------------------------");
                sw.WriteLine();

                #endregion




                #region USER INPUT DATA
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("USER'S DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Total Number of Cells = {0} nos ", Cell_Nos);
                sw.WriteLine();

                sw.WriteLine("{0} = {1:f3} {2} ",
                    "Depth of Earth Cushion = De",
                    De,
                    "m");

                sw.WriteLine(string.Format("Height of Side Walls = H = {0} m", _H));

                sw.WriteLine("{0} = {1:f3} {2}  ",
                    "Inside Clear Span = b",
                    b,
                    "m");

                sw.WriteLine("{0} = {1:f3} {2}  ",
                    "Inside Clear Depth = d",
                    d,
                    "m");

                sw.WriteLine("{0} = {1:f3} {2}  ",
                    "Inside Clear Width = W",
                    w,
                    "m");

                sw.WriteLine();

                sw.WriteLine("{0} = {1:f3} {2}",
                    "Thickness of Top Slab = d1",
                    d1,
                    "m");
                sw.WriteLine("{0} = {1:f3} {2}",
                    "Thickness of Bottom Slab = d2",
                    d2,
                    "m");
                sw.WriteLine("{0} = {1:f3} {2} ",
                    "Thickness of Side Walls = d3",
                    d3,
                    "m");
                sw.WriteLine("{0} = {1:f3} {2} ",
                    "Thickness of Intermediate Walls = d4",
                    d4,
                    "m");
                sw.WriteLine();
                sw.WriteLine("{0} = {1:f3} {2}",
                    "Unit weight of Earth = γ_b",
                    gamma_b,
                    "kN/cu.m");
                sw.WriteLine("{0} = {1:f3} {2}",
                    "Unit weight of Concrete = γ_c",
                    gamma_c,
                    "kN/cu.m");
                sw.WriteLine("{0} = {1:f3} {2}",
                    "R",
                    R,
                    "");
                //sw.WriteLine("{0} = {1:f3} {2}",
                //    "t",
                //    t,
                //    "");
                sw.WriteLine();
                //sw.WriteLine("{0} = {1:f3} {2}",
                //    "Rankine's Active Earth Pressure Coefficient  = Ka",
                //    Ka,
                //    "");

                sw.WriteLine("{0} = {1} {2}",
                    "Clear Cover = cover",
                    cover,
                    "mm");
                sw.WriteLine();
                sw.WriteLine("{0} = {1} {2}",
                    "Top Bar Diameter  = bar_dia_top",
                    bar_dia_top,
                    "mm");
                sw.WriteLine("{0} = {1} {2}",
                    "Bottom Bar Diameter  = bar_dia_bottom",
                    bar_dia_bottom,
                    "mm");
                sw.WriteLine("{0} = {1} {2}",
                    "Side Bar Diameter  = bar_dia_side",
                    bar_dia_side,
                    "mm");
                sw.WriteLine("{0} = {1} {2}",
                    "Intermediate Bar Diameter  = bar_dia_intermediate",
                    bar_dia_intermediate,
                    "mm");


                sw.WriteLine();
                sw.WriteLine("{0} = {1} {2}",
                    "Concrete Grade",
                    "M" + fck.ToString("0"),
                    "");


                sw.WriteLine("{0} = {1} {2}",
                    "Allowable Flexural Stress in Concrete [σ_c] ", sigma_c,
                    "N/Sq.mm");


                sw.WriteLine("{0} = {1} {2}",
                    "Steel Grade",
                    "Fe " + fy.ToString("0"),
                    "");

                sw.WriteLine("{0} = {1} {2}",
                    "Permissible Stress in Steel [σ_st]", sigma_st.ToString("0"), "N/Sq.mm");


                // For single Track Loading in 2-Lane

                sw.WriteLine("{0} = {1:f3} {2}",
                    "Modular Ratio = m",
                    m,
                    "");
                //sw.WriteLine("{0} = {1:f3} {2}",
                //    "Equivalent Earth height for Live Load Surchage = S",
                //    S,
                //    "m");
                //sw.WriteLine("{0} = {1:f3} {2}",
                //    "Safe bearing capacity of Ground = sbc",
                //    sbc,
                //    "kN/sq.m.");

                sw.WriteLine();

                #region inputs

                //sw.WriteLine(string.Format("Density of Backfill Material = Yb = {0} kN/Cu.m.", _gama_b));
                sw.WriteLine(string.Format("Angle of Internal Friction = phi = {0} Degrees", _phi));
                sw.WriteLine();


                if (Is_Only_Sidewall)
                {

                    sw.WriteLine("Selected option: On Side Walls only");
                    sw.WriteLine();
                    sw.WriteLine(string.Format("Approach Slab Load for No Earth Cushion = q1 = {0} kN/m.", _q1));
                }
                else
                {
                    sw.WriteLine("Selected option: On Top Slabs and Side Walls");
                    sw.WriteLine();
                    sw.WriteLine(string.Format("Earth Cushion Surcharge Load = q1 = {0} kN/m.", _q1));
                }

                sw.WriteLine(string.Format("Live Load for Vehicles = q2 = {0} kN/m.", _q2));
                sw.WriteLine();
                sw.WriteLine(string.Format("Height of Water Table = Hw = {0:f3} m.", _Hw));
                sw.WriteLine(string.Format("Density of Water = Yw = {0:f3} kN/Cu.m.", _gama_w));
                //sw.WriteLine(string.Format("Height of Dry Backfill = Hd = H - Hw = {0} - {1} = {2} m.", _H, _Hw, _Hd));
                sw.WriteLine();
                sw.WriteLine(string.Format("Density of Dry Backfill = Yd = {0} kN / Cum. (from above)", _gama_d));
                sw.WriteLine();
                //sw.WriteLine(string.Format("Thickness of Top Slab = Ds = {0:f3} m.", _Ds));
                //sw.WriteLine(string.Format("Density of Concrete = Yc = {0} kN/Cu.m.", _gama_c));
                sw.WriteLine();

                sw.WriteLine(string.Format("Vehicle Live Load = LL = q2 = {0:f3} kN/m.  (from above)", _q2));
                sw.WriteLine();
                sw.WriteLine(string.Format("Thickness of Earth Cushion = De = {0:f3} m.", _De));
                sw.WriteLine(string.Format("Density of Earth in Earth Cushion = Ye = {0:f3} kN/Cum.", _gama_e));
                sw.WriteLine();

                sw.WriteLine(string.Format("UDL on Top Members for SIDL = 0.0 kN/m. (per metre width)", SIDL));
                sw.WriteLine();
                sw.WriteLine(string.Format("Bearing Pressure = {0} kN/m", _brng_presr));
                sw.WriteLine(string.Format("Other Loads = {0:f3} kN/m.", _oth_lds));
                sw.WriteLine();
                sw.WriteLine(string.Format("FS = Factor of Safety = {0}", _FS));
                sw.WriteLine(string.Format("BC = Bearing Capacity = {0} kN/m", _BC));
                sw.WriteLine(string.Format("AS = Allowable Settlement = {0} m.", _AS));

                if(Apply_Seismic)
                {

                    sw.WriteLine();
                    sw.WriteLine(string.Format("Seismic Coefficient = Ah = {0} m.", _Ah));
                    sw.WriteLine();
                }
                #endregion inputs

                #endregion

                double p1 = 0.0;
                double p5 = 0.0;

                //p1 = Math.Abs(DL_Loads[1].Load);
                //p5 = Math.Abs(DL_Loads[0].Load);
                //EP_Loads
                #region STEP 1
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN CALCULATIONS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 1 : APPLIED LOADING AND ANALYSIS FORCES");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                int i = 0;
                
                if (false)
                {
                    #region Chiranjit [2016 08 05

                    sw.WriteLine("");
                    sw.WriteLine("TABLE  1.1 : APPLIED LOADING IN ANALYSIS");
                    sw.WriteLine("-----------------------------------------");

                    sw.WriteLine();
                    //int i = 0;
                    List<string> list = DL_Loads.Get_Report();
                    for (i = 0; i < list.Count; i++)
                    {
                        sw.WriteLine(list[i]);
                    }
                    sw.WriteLine();
                    list = SIDL_Loads.Get_Report();
                    for (i = 0; i < list.Count; i++)
                    {
                        sw.WriteLine(list[i]);
                    }
                    sw.WriteLine();
                    list = EP_Loads.Get_Report();
                    for (i = 0; i < list.Count; i++)
                    {
                        sw.WriteLine(list[i]);
                    }
                    sw.WriteLine();

                    foreach (var item in Moving_Loads)
                    {
                        sw.WriteLine();
                        list = item.Get_Report();
                        for (i = 0; i < list.Count; i++)
                        {
                            sw.WriteLine(list[i]);
                        }
                        sw.WriteLine();
                    }
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMBINATIONS");
                    sw.WriteLine("------------------");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 19 DL+EP");
                    sw.WriteLine("-------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 3 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 20 DL+EP+SIDL");
                    sw.WriteLine("------------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 2 X 1.0, LOAD 3 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 21 DL+EP+SIDL+LL1");
                    sw.WriteLine("---------------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 2 X 1.0, LOAD 3 X 1.0, LOAD 4 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 22 DL+EP+SIDL+LL2");
                    sw.WriteLine("---------------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 2 X 1.0, LOAD 3 X 1.0, LOAD 5 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 23 DL+EP+SIDL+LL3");
                    sw.WriteLine("----------------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 2 X 1.0, LOAD 3 X 1.0, LOAD 6 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 24 DL+EP+SIDL+LL4");
                    sw.WriteLine("----------------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 2 X 1.0, LOAD 3 X 1.0, LOAD 7 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 25 DL+EP+SIDL+LL5");
                    sw.WriteLine("----------------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 2 X 1.0, LOAD 3 X 1.0, LOAD 8 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 26 DL+EP+SIDL+LL6");
                    sw.WriteLine("----------------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 2 X  1.0, LOAD 3 X 1.0, LOAD 9 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 27 DL+EP+SIDL+LL7");
                    sw.WriteLine("----------------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 2 X 1.0, LOAD 3 X 1.0, LOAD 10 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 28 DL+EP+SIDL+LL8");
                    sw.WriteLine("----------------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 2 X 1.0, LOAD 3 X 1.0, LOAD 11 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 29 DL+EP+SIDL+LL9");
                    sw.WriteLine("----------------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 2 X 1.0, LOAD 3 X 1.0, LOAD 12 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 30 DL+EP+SIDL+LL10");
                    sw.WriteLine("----------------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 2 X 1.0, LOAD 3 X 1.0, LOAD 13 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 31 DL+EP+SIDL+LL11");
                    sw.WriteLine("----------------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 2 X 1.0, LOAD 3 X 1.0, LOAD 14 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 32 DL+EP+SIDL+LL12");
                    sw.WriteLine("----------------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 2 X  1.0, LOAD 3 X 1.0, LOAD 15 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 33 DL+EP+SIDL+LL13");
                    sw.WriteLine("----------------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 2 X 1.0, LOAD 3 X 1.0, LOAD 16 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 34 DL+EP+SIDL+LL14");
                    sw.WriteLine("----------------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 2 X 1.0, LOAD 3 X 1.0, LOAD 17 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("LOAD COMB 35 DL+EP+SIDL+LL15");
                    sw.WriteLine("-----------------------------");
                    sw.WriteLine("LOAD 1 X 1.0, LOAD 2 X 1.0, LOAD 3 X 1.0, LOAD 18 X 1.0");
                    sw.WriteLine("");
                    sw.WriteLine("-------------------------------------------------------------");
                    sw.WriteLine("");

                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("");
                    sw.WriteLine("RESULT FOR FORCES FROM ANALYSIS");
                    sw.WriteLine("---------------------------------------------");
                    sw.WriteLine("");
                    sw.WriteLine("Member wise Load Case wise Maximum Bending Moment (kN_m) and Shear Forces (kN),");
                    sw.WriteLine("as obtained from the Structural Analysis.");
                    sw.WriteLine("");
                    sw.WriteLine("-------------------------------------------------------------------------------------------------------------");
                    sw.WriteLine("       MEMBER     MAX BENDING   LOAD       MEMBER      JOINT   MAX SHEAR     LOAD     MEMBER       JOINT    ");
                    sw.WriteLine("                 MOMENT (kN-m)  CASE         NO         NO    FORCE (kN)     CASE        NO          NO    ");
                    sw.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    sw.WriteLine("{0,15} {1,10:f3} {2,10} {3,10} {4,10} {5,10:f3} {6,10} {7,10} {8,10}",
                        "TOP SLAB", BM_TS.Force, BM_TS.Loadcase, BM_TS.MemberNo, BM_TS.NodeNo, SF_TS.Force,
                        SF_TS.Loadcase, SF_TS.MemberNo, SF_TS.NodeNo);

                    sw.WriteLine("{0,15} {1,10:f3} {2,10} {3,10} {4,10} {5,10:f3} {6,10} {7,10} {8,10}",
                                        "BOTTOM SLAB", BM_BS.Force, BM_BS.Loadcase, BM_BS.MemberNo, BM_BS.NodeNo, SF_BS.Force,
                                        SF_BS.Loadcase, SF_BS.MemberNo, SF_BS.NodeNo);

                    sw.WriteLine("{0,15} {1,10:f3} {2,10} {3,10} {4,10} {5,10:f3} {6,10} {7,10} {8,10}",
                                        "SIDE WALL", BM_SW.Force, BM_SW.Loadcase, BM_SW.MemberNo, BM_SW.NodeNo, SF_SW.Force,
                                        SF_SW.Loadcase, SF_SW.MemberNo, SF_SW.NodeNo);

                    sw.WriteLine("-------------------------------------------------------------------------------------------------------------");

                    #endregion Chiranjit [2016 08 05
                }
                else
                {
                    foreach (var item in Get_Load_Calculation())
                    {
                        sw.WriteLine(item);
                    }
                }

                //sw.WriteLine("S. No.   Load Case No:    Member No.    Joint No.        Max. Bending Moment (kN_m)      Max. Shear Force (kN)");
                //sw.WriteLine("1            1                1                1        ");


                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("");
                sw.WriteLine("TABLE  1 : RESULT FOR FORCES FROM ANALYSIS");
                sw.WriteLine("---------------------------------------------");
                sw.WriteLine("");
                sw.WriteLine("Member wise Load Case wise Maximum Bending Moment (kN_m) and Shear Forces (kN),");
                sw.WriteLine("as obtained from the Structural Analysis.");
                sw.WriteLine("");
                sw.WriteLine("-------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine("       MEMBER          MAX BENDING   LOAD       MEMBER      JOINT   MAX SHEAR     LOAD     MEMBER       JOINT    ");
                sw.WriteLine("                       MOMENT (kN-m)  CASE         NO         NO    FORCE (kN)     CASE        NO          NO    ");
                sw.WriteLine("-------------------------------------------------------------------------------------------------------------------");

                sw.WriteLine("{0,-20} {1,10:f3} {2,10} {3,10} {4,10} {5,10:f3} {6,10} {7,10} {8,10}",
                    "TOP SLAB", BM_TS.Force, BM_TS.Loadcase, BM_TS.MemberNo, BM_TS.NodeNo, SF_TS.Force, SF_TS.Loadcase, SF_TS.MemberNo, SF_TS.NodeNo);

                sw.WriteLine("{0,-20} {1,10:f3} {2,10} {3,10} {4,10} {5,10:f3} {6,10} {7,10} {8,10}",
                                    "BOTTOM SLAB", BM_BS.Force, BM_BS.Loadcase, BM_BS.MemberNo, BM_BS.NodeNo, SF_BS.Force, SF_BS.Loadcase, SF_BS.MemberNo, SF_BS.NodeNo);

                sw.WriteLine("{0,-20} {1,10:f3} {2,10} {3,10} {4,10} {5,10:f3} {6,10} {7,10} {8,10}",
                                    "SIDE WALL", BM_SW.Force, BM_SW.Loadcase, BM_SW.MemberNo, BM_SW.NodeNo, SF_SW.Force, SF_SW.Loadcase, SF_SW.MemberNo, SF_SW.NodeNo);

                if (Cell_Nos > 1)
                {
                    sw.WriteLine("{0,-20} {1,10:f3} {2,10} {3,10} {4,10} {5,10:f3} {6,10} {7,10} {8,10}",
                                        "INTERMEDIATE WALL", BM_IW.Force, BM_IW.Loadcase, BM_IW.MemberNo, BM_IW.NodeNo, SF_IW.Force, SF_IW.Loadcase, SF_IW.MemberNo, SF_IW.NodeNo);
                }

                sw.WriteLine("-------------------------------------------------------------------------------------------------------------------");

                sw.WriteLine();
                sw.WriteLine();


                char ch = 'A';
                int indx = (int)ch;

                List<char> lst_ts = new List<char>();
                List<char> lst_bs = new List<char>();
                for (i = 0; i <= Cell_Nos; i++)
                {
                    ch = (char)indx;
                    lst_ts.Add(ch);

                    ch = (char)(indx + Cell_Nos + 1);
                    lst_bs.Add(ch);
                    indx++;
                }

                for (i = 0; i <= Cell_Nos; i++)
                {
                    ch = (char)indx;
                    lst_ts.Add(ch);

                    ch = (char)(indx + Cell_Nos + 1);
                    lst_bs.Add(ch);
                    indx++;
                }


                sw.WriteLine("");


                sw.WriteLine("----------------------------");
                //sw.WriteLine("TABLE  1.3 : MEMBERS DIAGRAM");
                sw.WriteLine("MEMBERS DIAGRAM ");
                sw.WriteLine("----------------------------");
                sw.WriteLine();


                #region Figure
                sw.WriteLine(string.Format(""));
                if (Cell_Nos == 1)
                {
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("        A                             B"));
                    sw.WriteLine(string.Format("        -------------------------------"));
                    sw.WriteLine(string.Format("        |                             |"));
                    sw.WriteLine(string.Format("        |                             |"));
                    sw.WriteLine(string.Format("        |                             |"));
                    sw.WriteLine(string.Format("        |                             |"));
                    sw.WriteLine(string.Format("        |                             |"));
                    sw.WriteLine(string.Format("        |                             |"));
                    sw.WriteLine(string.Format("        |                             |"));
                    sw.WriteLine(string.Format("        |                             |"));
                    sw.WriteLine(string.Format("        |                             |"));
                    sw.WriteLine(string.Format("        |                             |"));
                    sw.WriteLine(string.Format("        |                             |"));
                    sw.WriteLine(string.Format("        |                             |"));
                    sw.WriteLine(string.Format("        -------------------------------"));
                    sw.WriteLine(string.Format("        D                             C"));
                    sw.WriteLine(string.Format("              Box Culvert"));
                    sw.WriteLine(string.Format(""));

                }
                else if (Cell_Nos == 2)
                {
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("        A                             B                            C"));
                    sw.WriteLine(string.Format("        ------------------------------------------------------------"));
                    sw.WriteLine(string.Format("        |                             |                            |"));
                    sw.WriteLine(string.Format("        |                             |                            |"));
                    sw.WriteLine(string.Format("        |                             |                            |"));
                    sw.WriteLine(string.Format("        |                             |                            |"));
                    sw.WriteLine(string.Format("        |                             |                            |"));
                    sw.WriteLine(string.Format("        |                             |                            |"));
                    sw.WriteLine(string.Format("        |                             |                            |"));
                    sw.WriteLine(string.Format("        |                             |                            |"));
                    sw.WriteLine(string.Format("        |                             |                            |"));
                    sw.WriteLine(string.Format("        |                             |                            |"));
                    sw.WriteLine(string.Format("        |                             |                            |"));
                    sw.WriteLine(string.Format("        |                             |                            |"));
                    sw.WriteLine(string.Format("        ------------------------------------------------------------"));
                    sw.WriteLine(string.Format("        D                             E                            F"));
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("                            Multi Cell Box Culvert"));
                    sw.WriteLine(string.Format(""));

                }
                else if (Cell_Nos == 3)
                {
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("        A                             B                           C                          D"));
                    sw.WriteLine(string.Format("        --------------------------------------------------------------------------------------"));
                    sw.WriteLine(string.Format("        |                             |                           |                          |  "));
                    sw.WriteLine(string.Format("        |                             |                           |                          |  "));
                    sw.WriteLine(string.Format("        |                             |                           |                          |  "));
                    sw.WriteLine(string.Format("        |                             |                           |                          |  "));
                    sw.WriteLine(string.Format("        |                             |                           |                          |  "));
                    sw.WriteLine(string.Format("        |                             |                           |                          |  "));
                    sw.WriteLine(string.Format("        |                             |                           |                          |  "));
                    sw.WriteLine(string.Format("        |                             |                           |                          |  "));
                    sw.WriteLine(string.Format("        |                             |                           |                          |  "));
                    sw.WriteLine(string.Format("        --------------------------------------------------------------------------------------"));
                    sw.WriteLine(string.Format("        E                             F                           G                          H"));
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("                                         Multi Cell Box Culvert"));
                    sw.WriteLine(string.Format(""));
                }
                else if (Cell_Nos == 4)
                {
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("        A                      B                      C                      D                      E"));
                    sw.WriteLine(string.Format("        ---------------------------------------------------------------------------------------------"));
                    sw.WriteLine(string.Format("        |                      |                      |                      |                      |"));
                    sw.WriteLine(string.Format("        |                      |                      |                      |                      |"));
                    sw.WriteLine(string.Format("        |                      |                      |                      |                      |"));
                    sw.WriteLine(string.Format("        |                      |                      |                      |                      |"));
                    sw.WriteLine(string.Format("        |                      |                      |                      |                      |"));
                    sw.WriteLine(string.Format("        |                      |                      |                      |                      |"));
                    sw.WriteLine(string.Format("        |                      |                      |                      |                      |"));
                    sw.WriteLine(string.Format("        |                      |                      |                      |                      |"));
                    sw.WriteLine(string.Format("        ---------------------------------------------------------------------------------------------"));
                    sw.WriteLine(string.Format("        F                      G                      H                      I                      J"));
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("                                         Multi Cell Box Culvert"));
                    sw.WriteLine(string.Format(""));
                }
                else if (Cell_Nos == 5)
                {
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("        A                B                C                D                E                F"));
                    sw.WriteLine(string.Format("        --------------------------------------------------------------------------------------"));
                    sw.WriteLine(string.Format("        |                |                |                |                |                |"));
                    sw.WriteLine(string.Format("        |                |                |                |                |                |"));
                    sw.WriteLine(string.Format("        |                |                |                |                |                |"));
                    sw.WriteLine(string.Format("        |                |                |                |                |                |"));
                    sw.WriteLine(string.Format("        |                |                |                |                |                |"));
                    sw.WriteLine(string.Format("        |                |                |                |                |                |"));
                    sw.WriteLine(string.Format("        --------------------------------------------------------------------------------------"));
                    sw.WriteLine(string.Format("        G                H                I                J                K                L"));
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("                                         Multi Cell Box Culvert"));
                    sw.WriteLine(string.Format(""));
                }
                else if (Cell_Nos == 6)
                {
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("        A            C            D            E            F            G            H"));
                    sw.WriteLine(string.Format("        -------------------------------------------------------------------------------"));
                    sw.WriteLine(string.Format("        |            |            |            |            |            |            |"));
                    sw.WriteLine(string.Format("        |            |            |            |            |            |            |"));
                    sw.WriteLine(string.Format("        |            |            |            |            |            |            |"));
                    sw.WriteLine(string.Format("        |            |            |            |            |            |            |"));
                    sw.WriteLine(string.Format("        |            |            |            |            |            |            |"));
                    sw.WriteLine(string.Format("        -------------------------------------------------------------------------------"));
                    sw.WriteLine(string.Format("        I            J            K            L            M            N            O"));
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("                                  Multi Cell Box Culvert"));
                    sw.WriteLine(string.Format(""));
                }
                else if (Cell_Nos == 7)
                {
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("        A            C            D            E            F            G            H            I"));
                    sw.WriteLine(string.Format("        --------------------------------------------------------------------------------------------"));
                    sw.WriteLine(string.Format("        |            |            |            |            |            |            |            |"));
                    sw.WriteLine(string.Format("        |            |            |            |            |            |            |            |"));
                    sw.WriteLine(string.Format("        |            |            |            |            |            |            |            |"));
                    sw.WriteLine(string.Format("        |            |            |            |            |            |            |            |"));
                    sw.WriteLine(string.Format("        |            |            |            |            |            |            |            |"));
                    sw.WriteLine(string.Format("        --------------------------------------------------------------------------------------------"));
                    sw.WriteLine(string.Format("        J            K            L            M            N            O            P            Q"));
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("                                          Multi Cell Box Culvert"));
                    sw.WriteLine(string.Format(""));
                }
                else if (Cell_Nos == 8)
                {
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("        A         B         C         D         E         F         G         H         I"));
                    sw.WriteLine(string.Format("        ---------------------------------------------------------------------------------"));
                    sw.WriteLine(string.Format("        |         |         |         |         |         |         |         |         |"));
                    sw.WriteLine(string.Format("        |         |         |         |         |         |         |         |         |"));
                    sw.WriteLine(string.Format("        |         |         |         |         |         |         |         |         |"));
                    sw.WriteLine(string.Format("        |         |         |         |         |         |         |         |         |"));
                    sw.WriteLine(string.Format("        ---------------------------------------------------------------------------------"));
                    sw.WriteLine(string.Format("        J         K         L         M         N         O         P         Q         R"));
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("                                          Multi Cell Box Culvert"));
                    sw.WriteLine(string.Format(""));
                }
                else if (Cell_Nos == 9)
                {
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("        A         B         C         D         E         F         G         H         I         J"));
                    sw.WriteLine(string.Format("        -------------------------------------------------------------------------------------------"));
                    sw.WriteLine(string.Format("        |         |         |         |         |         |         |         |         |         |"));
                    sw.WriteLine(string.Format("        |         |         |         |         |         |         |         |         |         |"));
                    sw.WriteLine(string.Format("        |         |         |         |         |         |         |         |         |         |"));
                    sw.WriteLine(string.Format("        |         |         |         |         |         |         |         |         |         |"));
                    sw.WriteLine(string.Format("        -------------------------------------------------------------------------------------------"));
                    sw.WriteLine(string.Format("        K         L         M         N         O         P         Q         R         S         T"));
                    sw.WriteLine(string.Format(""));
                    sw.WriteLine(string.Format("                                          Multi Cell Box Culvert"));
                    sw.WriteLine(string.Format(""));
                }
                sw.WriteLine(string.Format(""));
                #endregion Figure

                sw.WriteLine("TOP SLAB MEMBERS");
                sw.WriteLine("----------------");
                sw.WriteLine();
                sw.WriteLine("--------------------------------------------");
                sw.WriteLine("     {0,-15} {1,-20}", "Members", "Member Nos.");
                sw.WriteLine("--------------------------------------------");

                string kStr = "";
                indx = 0;
                int c = 0;
                for (i = 0; i < mc_top.Count; i++)
                {
                    indx++;
                    if (indx == 4)
                    {
                        kStr += mc_top[i].MemberNo;
                        sw.WriteLine("     {0,-15} {1,-20}", lst_ts[c].ToString() + lst_ts[++c].ToString(), kStr);
                        indx = 0;
                        kStr = "";
                    }
                    else
                    {
                        kStr += mc_top[i].MemberNo + ",";
                    }
                }
                sw.WriteLine("--------------------------------------------");

                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("BOOTOM SLAB MEMBERS");
                sw.WriteLine("--------------------");
                sw.WriteLine();
                sw.WriteLine("--------------------------------------------");
                sw.WriteLine("     {0,-15} {1,-20}", "Members", "Member Nos.");
                sw.WriteLine("--------------------------------------------");

                kStr = "";
                indx = 0;
                c = 0;
                for (i = 0; i < mc_bottom.Count; i++)
                {
                    indx++;
                    if (indx == 4)
                    {
                        kStr += mc_bottom[i].MemberNo;
                        sw.WriteLine("     {0,-15} {1,-20}", lst_bs[c].ToString() + lst_bs[++c].ToString(), kStr);
                        indx = 0;
                        kStr = "";
                    }
                    else
                    {
                        kStr += mc_bottom[i].MemberNo + ",";
                    }
                }
                sw.WriteLine("--------------------------------------------");

                sw.WriteLine();
                sw.WriteLine();

                sw.WriteLine("VERTICAL SIDE MEMBERS");
                sw.WriteLine("----------------------");
                sw.WriteLine();
                sw.WriteLine("--------------------------------------------");
                sw.WriteLine("     {0,-15} {1,-20}", "Members", "Member Nos.");
                sw.WriteLine("--------------------------------------------");
                kStr = "";
                indx = 0;
                c = 0;
                for (i = 0; i < mc_vertical.Count; i++)
                {
                    indx++;
                    if (indx == 3)
                    {
                        kStr += mc_vertical[i].MemberNo;
                        sw.WriteLine("     {0,-15} {1,-20}", lst_ts[c].ToString() + lst_bs[c++].ToString(), kStr);
                        indx = 0;
                        kStr = "";
                    }
                    else
                    {
                        kStr += mc_vertical[i].MemberNo + ",";
                    }
                }
                sw.WriteLine("--------------------------------------------");


                //DL_Loads
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();

                //sw.WriteLine("Refer to Analysis Input Data file : Multi_Cell_Box_Culvert_Input_Data.TXT    and   Analysis Report File: ANALYSIS_REP.TXT");
                //sw.WriteLine("");
                //sw.WriteLine("TABLE  1.1 : Applied Loading in Analysis");
                //sw.WriteLine("");
                //sw.WriteLine("S. No.        Load Case No:            Member/Joint           Member/Joint Nos.    Loading Type       Load Direction          Load (kN) ");
                //sw.WriteLine("1                     1                Member                        1                UNI                  GY                 -38.922");
                //sw.WriteLine("");
                //sw.WriteLine("TABLE  1.2");
                //sw.WriteLine("Result for forces from Analysis:");
                //sw.WriteLine("Member wise Load Case wise Maximum Bending Moment (kN_m) and Shear Forces (kN), as obtained from the Structural Analysis.");
                //sw.WriteLine("");
                //sw.WriteLine("S. No.   Load Case No:    Member No.    Joint No.        Max. Bending Moment (kN_m)      Max. Shear Force (kN)");
                //sw.WriteLine("1            1                1                1        ");
                //sw.WriteLine("");
                //sw.WriteLine("TABLE  1.3");
                //sw.WriteLine("Top Slab                                Bottom Slab                         Vertical Side");
                //sw.WriteLine("Members                Member Nos.        Members         Member Nos        Members                Member Nos.");
                //sw.WriteLine("");
                //sw.WriteLine("AB                17,18,19,20        FG                1,2,3,4                AF                33,34,35,36");
                //sw.WriteLine("BC                21,22,23,24        GH                5,6,7,8                BG                37,38,39,40");
                //sw.WriteLine("CD                25,26,27,28        HI                9.10,11,12             CH                41,42,43,44");
                //sw.WriteLine("DE                29,30,31,32        IJ                13,14,15,16            DI                45,46,47,48");
                //sw.WriteLine("                                                                              EJ                49,50,51,52");
                //sw.WriteLine("");
                //sw.WriteLine("");
                //sw.WriteLine("");
                //sw.WriteLine("");
                //sw.WriteLine("");
                //sw.WriteLine("");
                //sw.WriteLine("");
                //sw.WriteLine("A                      B                      C                      D                      E");
                //sw.WriteLine("---------------------------------------------------------------------------------------------");
                //sw.WriteLine("|                      |                      |                      |                      |");
                //sw.WriteLine("|                      |                      |                      |                      |");
                //sw.WriteLine("|                      |                      |                      |                      |");
                //sw.WriteLine("|                      |                      |                      |                      |");
                //sw.WriteLine("|                      |                      |                      |                      |");
                //sw.WriteLine("|                      |                      |                      |                      |");
                //sw.WriteLine("|                      |                      |                      |                      |");
                //sw.WriteLine("|                      |                      |                      |                      |");
                //sw.WriteLine("---------------------------------------------------------------------------------------------");
                //sw.WriteLine("F                      G                      H                      I                      J");
                //sw.WriteLine("");
                //sw.WriteLine("Multi Cell Box Culvert");
                sw.WriteLine("");


                #endregion



                #region STEP 2 : DESIGN OF TOP SLAB

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 2 : DESIGN OF TOP SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();

                //double M = SMabx;
                //Chiranjit [2012 05 04]
                double M = Math.Abs(BM_TS);


                sw.WriteLine("");
                sw.WriteLine("  Maximum Bending Moment at Support / Midspan = M = {0:f2} kN-m", M);
                double depth = Math.Sqrt((M * 10E5) / (1000 * R));
                sw.WriteLine();
                sw.WriteLine("  Depth = d = √((M * 10^6) / (1000 * R)");
                sw.WriteLine("            = √(({0:f2} * 10^6) / (1000 * {1:f3}))", M, R);
                sw.WriteLine("            = {0:f3} mm", depth);
                sw.WriteLine();
                sw.WriteLine("  Provided Slab Thickness = {0} mm", (d1 * 1000));
                double bar_dia = bar_dia_top;
                sw.WriteLine();
                sw.WriteLine("  Considering Clear Cover = {0} mm and Reinforcement bar dia = {1} mm",
                    cover, bar_dia);
                double deff = (d1 * 1000) - cover - (bar_dia / 2.0);
                sw.WriteLine();
                if (deff > depth)
                {
                    sw.WriteLine("  Effective Depth = deff = {0} - {1} - {2:f0}/2 = {3:f2} mm > {4:f2} mm OK",
                        (d1 * 1000), cover, bar_dia, deff, depth);
                }
                else
                {
                    sw.WriteLine("  Effective Depth = deff = {0} - {1} - {2:f2} = {3:f2} mm < {4:f2} mm NOT OK",
                        (d1 * 1000), cover, (bar_dia / 2.0), deff, depth);
                }

                //j = 0.5 + Math.Sqrt((0.25) - ((M * 10E5) / (0.87 * fck * 1000 * deff * deff)));
                //j = 0.5 + Math.Sqrt(Math.Abs((0.25) - ((M * 1000000) / (0.87 * fck * 1000 * deff * deff))));
                j = 0.5 + Math.Sqrt(((0.25) - ((M * 1000000) / (0.87 * fck * 1000 * deff * deff))));
                sw.WriteLine();
                sw.WriteLine("  Lever Arm = j = 0.5 + √(0.25 - ((M*10^6)/(0.87*fck*1000*deff^2))) = {0:f3}", j);
                sw.WriteLine();
                double req_st_ast = (M * 10E5) / (0.87 * fy * j * deff);
                sw.WriteLine();

                sw.WriteLine();
                sw.WriteLine("  Required Main Reinforcement Steel per metre width,");
                sw.WriteLine();
                sw.WriteLine("        Ast = (M*10^6)/(0.87 * fy * j * d)");
                sw.WriteLine("            = ({0:f2}*10^6)/(0.87 * {1:f2} * {2:f3} * {3:f2})",
                    M,
                    fy, j, deff);
                sw.WriteLine("            = {0:f3} sq.mm.", req_st_ast);
                double pro_st_ast = (Math.PI * bar_dia * bar_dia / 4);

                double spacing = 1000 / (pro_st_ast / req_st_ast);
                //spacing = (int)((1000.0 / spacing) / 10.0);
                //spacing = (int)(spacing);
                //spacing = spacing * 10.0;
                sw.WriteLine();
                sw.WriteLine("Area of a Reinforcement Steel bar = Abar = π*{0}^2/4 = {1:f3} sq.mm", bar_dia, pro_st_ast);
                sw.WriteLine();

                spacing = 1000 / (req_st_ast /pro_st_ast);
                sw.WriteLine("Required spacing using bar T{0:f0}", bar_dia);
                sw.WriteLine();
                sw.WriteLine("          = 1000/({0:f3}/{1:f3})", req_st_ast, pro_st_ast);
                sw.WriteLine("          = {0:f3} mm", spacing);
                sw.WriteLine();
                spacing = (int)(spacing / 10);
                spacing = spacing * 10.0;
                sw.WriteLine("          = {0} mm", spacing);
                sw.WriteLine();
                if (spacing < 200)
                {
                    pro_st_ast = pro_st_ast * (1000.0 / spacing);
                    sw.WriteLine("  Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm per metre width.", bar_dia, spacing, pro_st_ast);
                }
                else
                {
                    spacing = 200;
                    pro_st_ast = pro_st_ast * (1000.0 / spacing);
                    sw.WriteLine("  As the required spacing per metre width is more than 200,");
                    sw.WriteLine("  Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm per metre width.", bar_dia, spacing, pro_st_ast);
                }

                lst_Bar_Dia.Add(bar_dia);
                lst_Bar_Space.Add(spacing);
                bd6 = bar_dia;
                bd7 = bar_dia;
                bd8 = bar_dia;
                sp6 = sp7 = sp8 = spacing * 2;


                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN OF SHEAR REINFORCEMENT");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                //sw.WriteLine("  Total Load per unit Area = p1 = {0:f2} kN/sq.m", p1);
                sw.WriteLine();
                //sw.WriteLine("  Shear Force at distance = deff ,from face of wall");

                sw.WriteLine();
                double earth_cusion = De * gamma_b;
                sw.WriteLine("    Earth Cushion = De * γ_b = {0:f2} * {1:f3} = {2:f2} kN/sq.m",
                    De,
                    gamma_b,
                    earth_cusion);

                double self_weight_top_slab = d1 * gamma_c;

                sw.WriteLine("    Self weight of Top Slab = d1 * γ_c ");
                sw.WriteLine("                            = {0:f3} * {1:f3}", d1, gamma_c);
                sw.WriteLine("                            = {0:f2} kN/sq.m", self_weight_top_slab);

                p1 = earth_cusion + self_weight_top_slab;

                sw.WriteLine();
                sw.WriteLine("    Total Permanent Load per unit area for one track = p1");
                sw.WriteLine("                                                 = {0:f3} + {1:f3}",
                                    earth_cusion, self_weight_top_slab);
                sw.WriteLine("                                                 = {0:f3} kN/sq.m", p1);



                double shear_force = SF_TS;

                sw.WriteLine("  Shear Force  = {0:f2} kN", shear_force);
                double shear_stress = shear_force * 1000 / (1000 * deff);
                sw.WriteLine();
                sw.WriteLine("  Shear Stress = {0:f2}*1000/(1000*{1:f2})",
                    shear_force, deff);
                sw.WriteLine("               = {0:f2} N/sq.mm", shear_stress);

                sw.WriteLine();
                //sw.WriteLine("  Using Table 1, IS 456 : 2000 (Page 84)");
                double percent = req_st_ast * 100 / (1000 * deff);

                double tau_c = Get_Table_1_Value(percent, Con_Grade, ref ref_string);
                //double tau_c = iApp.Tables.Permissible_Shear_Stress(percent, St_Grade);
                sw.WriteLine("  Using Table 1, given at end of this report  {0}", ref_string);
                sw.WriteLine();
                sw.WriteLine("  percent = Ast*100/(1000*deff)");
                sw.WriteLine("          = {0:f2}*100/(1000*{1:f2})",
                    req_st_ast, deff);
                sw.WriteLine("          = {0:f0}%", (percent * 100));

                sw.WriteLine();
                sw.WriteLine("  τ_c = {0:f3} N/sq.mm.", tau_c);
                double shear_capacity = tau_c * 1000 * deff;
                sw.WriteLine();
                sw.WriteLine("  Shear Capacity of section = τ_c*1000*deff");
                sw.WriteLine("                            = {0:f2}*1000*{1:f2} N",
                    tau_c, deff);
                sw.WriteLine("                            = {0:f2} N", shear_capacity);

                shear_capacity = shear_capacity / 1000.0;
                sw.WriteLine("                            = {0:f2} kN", shear_capacity);

                double balance_shear = (shear_force - shear_capacity);

                double ast_2 = 0.0;
                double Asw = 0.0;
                double x = 0.0;
                double _x = 0.0;
                ast_2 = Math.PI * dia_shr * dia_shr / 4.0;

                sw.WriteLine();
                if (balance_shear > 0)
                {
                    sw.WriteLine();
                    sw.WriteLine("  Balance Shear = V = {0:f2} - {1:f2} = {2:f2} kN = {3:f2} N",
                        shear_force, shear_capacity, balance_shear, (balance_shear * 1000));

                    balance_shear = balance_shear * 1000;
                    sw.WriteLine();

                    //double ast_2 = Math.PI * 8 * 8 / 4 * (1000 / 250);

                    ast_2 = Math.PI * dia_shr * dia_shr / 4.0;



                    //sw.WriteLine("  Provide T10 @ 200 mm c/c, Ast = {0:f2} sq.mm.", ast_2);
                    bd9 = bd10 = dia_shr;
                    sp9 = sp10 = 200;


                    #region Bar_Dia And Spacing for Drawing Table
                    lst_Bar_Dia.Add(bd9);
                    lst_Bar_Space.Add(sp10);
                    #endregion


                    // Asw = balance_shear * 250 / (200 * deff);
                    //sw.WriteLine();
                    //sw.WriteLine("  Asw = {0:f3} * 250 / ({1:f2} * {2:f2})",
                    //    balance_shear, ast_2, deff);

                    //sw.WriteLine("      = {0:f3} sq.mm.", Asw);


                    //x = -((((shear_capacity * 2) / p1) - b) * (1000 / 2) + cover);
                    //x = Math.Abs(x / 1000.0);
                    //sw.WriteLine();
                    //sw.WriteLine("  p1*[b-(x+cover)*(2/1000)]/2 = {0:f3}", shear_capacity);
                    //sw.WriteLine();

                    //_x = x * 1000;
                    ////double _x = x / 100;
                    ////_x = (int)(_x + 1);
                    ////_x = _x * 100;


                    //sw.WriteLine("  x = -((((shear_capacity * 2) / p1) - b) * (1000 / 2) + cover)", x, _x);
                    //sw.WriteLine();
                    //sw.WriteLine("    = -(((({0:f3} * 2) / {1:f3}) - {2:f3}) * (1000 / 2) + {3})", shear_capacity, p1, b, cover);


                    //sw.WriteLine("    = {0:f4} m = {1:f0} mm", x, _x);
                    //sw.WriteLine();
                    //if (_x < 300)
                    //    sw.WriteLine("  Provided Shear reinforcements for a distance of {0:f2} mm from", _x);
                    //else
                    //{
                    //    sw.WriteLine("  As the required spacing per metre width is more than 200,");
                    //    sw.WriteLine("  Provided Shear reinforcements for a distance of 200mm from", _x);
                    //}
                    //sw.WriteLine("  the face of wall on both sides.");

                    
                    double Asv = ast_2;
                    double V = balance_shear;
                    sw.WriteLine("Area of a Steel Reinforcement bar of {0} mm dia = Asv = {1:f2} sq.mm.", dia_shr, Asv);

                    double Sv = sigma_st * Asv * deff * m / V;
                    sw.WriteLine("Spacing = Sv = σ_st * Asv * deff * m / V");
                    sw.WriteLine("             = {0} * {1:f3} * {2:f3} * {3} / {4:f3}", sigma_st, Asv, deff, m, V);
                    sw.WriteLine("             = {0:f3} mm", Sv);

                    sw.WriteLine();
                    spacing = Sv;
                    if (spacing < 200)
                    {
                        pro_st_ast = ast_2 * (1000.0 / spacing);
                        sw.WriteLine("  Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm per metre width.", dia_shr, spacing, pro_st_ast);
                    }
                    else
                    {
                        spacing = 200;
                        pro_st_ast = ast_2 * (1000.0 / spacing);
                        sw.WriteLine("  As the required spacing per metre width is more than 200,");
                        sw.WriteLine("  Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm per metre width.", dia_shr, spacing, pro_st_ast);
                    }


                }
                else
                {
                    //ast_2 = Math.PI * dia_shr * dia_shr / 4.0;
                    sw.WriteLine("  Provide T{0} @ 200 mm c/c, Ast = {1:f2} sq.mm , ({1:f2} * (1000/200)) = {2:f2} sq.mm per metre width", dia_shr, ast_2, (ast_2 * 5));
                    bd9 = bd10 = dia_shr;
                    sp9 = sp10 = 200;


                    #region Bar_Dia And Spacing for Drawing Table
                    lst_Bar_Dia.Add(bd9);
                    lst_Bar_Space.Add(sp9);
                    #endregion
                }

                #endregion

                #region STEP 3 : DESIGN OF BOTTOM SLAB


                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 3 : DESIGN OF BOTTOM SLAB");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("");


                M = Math.Abs(BM_BS);


                sw.WriteLine("  Maximum Bending Moment at Support / Mid Span =  M = {0:f2} kN-m", M);
                depth = Math.Sqrt((M * 10E5) / (1000 * R));

                sw.WriteLine();
                sw.WriteLine("  Depth Required = √((M * 10^6) / (1000 * R)");
                sw.WriteLine("            = √(({0:f2} * 10^6) / (1000 * {1:f3}))", M, R);
                sw.WriteLine("            = {0:f3} mm", depth);
                sw.WriteLine();
                sw.WriteLine("  Provided Slab Thickness = {0:f2} mm", (d2 * 1000));

                bar_dia = bar_dia_bottom;
                sw.WriteLine("  Considering Clear Cover = {0} mm and Reinforcement bar dia = {1} mm",
                cover, bar_dia);
                deff = (d2 * 1000) - cover - (bar_dia / 2.0);
                sw.WriteLine();
                if (deff > depth)
                {
                    sw.WriteLine("  Effective Depth = deff = {0} - {1} - {2:f0}/2 = {3:f2} mm > {4:f2} mm OK",
                        (d2 * 1000), cover, bar_dia, deff, depth);
                }
                else
                {
                    sw.WriteLine("  Effective Depth = deff = {0} - {1} - {2:f0}/2 = {3:f2} mm < {4:f2} mm NOT OK",
                        (d2 * 1000), cover, bar_dia, deff, depth);
                }

                sw.WriteLine();
                sw.WriteLine("  Lever Arm = j = 0.5 + √(0.25 - ((M*10^6)/(0.87*fck*1000*deff^2))) = {0:f3}", j);
                req_st_ast = (M * 10E5) / (0.87 * fy * j * deff);
                sw.WriteLine();
                //sw.WriteLine("  Required Main Reinforcement Steel Ast = (M*10^6)/(t*j*d)");
                //sw.WriteLine("                                        = ({0:f2}*10^6)/({1:f2}*{2:f3}*{3:f2})",
                //    M,
                //    t, j, deff);
                //sw.WriteLine("                                        = {0:f3} sq.mm.", req_st_ast);
                sw.WriteLine("  Required Main Reinforcement Steel per metre width,");
                sw.WriteLine("        Ast = (M*10^6)/(0.87 * fy * j * d)");
                sw.WriteLine("            = ({0:f2}*10^6)/(0.87 * {1:f2} * {2:f3} * {3:f2})",
                    M,
                    sigma_st, j, deff);
                sw.WriteLine("            = {0:f3} sq.mm.", req_st_ast);

                pro_st_ast = (Math.PI * bar_dia * bar_dia / 4);
                sw.WriteLine();
                sw.WriteLine("Area of a Reinforcement Steel bar = Abar = π*{0}^2/4 = {1:f3} sq.mm", bar_dia, pro_st_ast);
                sw.WriteLine();
                spacing = 1000.0 / (req_st_ast / pro_st_ast);
                //spacing = 1000.0 / spacing;
                //spacing = (int)(spacing / 10.0);
                //spacing = (spacing * 10.0);
                //sw.WriteLine("Abar = π*{0}^2/4 = {1:f3}", bar_dia, pro_st_ast);
                sw.WriteLine();
                sw.WriteLine("Required spacing using bar T{0:f0}", bar_dia);
                sw.WriteLine();
                sw.WriteLine("          = 1000/({0:f3}/{1:f3})", req_st_ast, pro_st_ast);
                sw.WriteLine("          = {0:f3} mm", spacing);
                spacing = (int)(spacing / 10.0);
                spacing = (spacing * 10.0);
                sw.WriteLine();
                sw.WriteLine("          = {0:f0} mm", spacing);



                if (spacing < 200)
                {
                    pro_st_ast = (Math.PI * bar_dia * bar_dia / 4) * (1000.0 / spacing);
                    sw.WriteLine("Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm per metre width", bar_dia, spacing, pro_st_ast);
                }
                else
                {
                    spacing = 200;
                    //pro_st_ast = (req_st_ast) * (1000.0 / spacing);
                    pro_st_ast = (Math.PI * bar_dia * bar_dia / 4) * (1000.0 / spacing);
                    sw.WriteLine("As the required spacing per metre width is more than 200,");
                    sw.WriteLine("Provided T{0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm per metre width", bar_dia, spacing, pro_st_ast);
                }

                sw.WriteLine();
                //sw.WriteLine("  Provided T{0} bars @ {1:f0} mm c/c", bar_dia, spacing);

                bd1 = bd3 = bar_dia;
                sp1 = spacing * 2;
                sp3 = spacing;


                #region Bar_Dia And Spacing for Drawing Table
                lst_Bar_Dia.Add(bar_dia);
                lst_Bar_Space.Add(spacing);
                #endregion



                sw.WriteLine();
                //sw.WriteLine("  Provided Ast = {0:f3} sq.mm", pro_st_ast);

                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN OF SHEAR REINFORCEMENT");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                //sw.WriteLine("  Total Load per unit Area = p4 = {0:f2} kN/sq.m", p4);
                //sw.WriteLine("  Shear Force at distance = deff ,from face of wall");
                sw.WriteLine();
                sw.WriteLine("  Load from Top Slab = p1 = {0:f3} kN/sq.m", p1);
                double loads_walls = (d * d3 * 2 * gamma_c) / (1 * b + d3 + d3);
                sw.WriteLine();
                sw.WriteLine("  Load of Walls = (d * d3 * 2 * γ_c)/(1 * (b + d3 + d3))");
                sw.WriteLine("                = ({0:f2} * {1:f2} * 2 * {2:f2})/(1 * ({3:f2} + {1:f2} + {1:f2}))",
                    d,
                    d3,
                    gamma_c,
                    b);
                sw.WriteLine("                = {0:f2} kN/sq.m", loads_walls);

                p5 = p1 + loads_walls;
                sw.WriteLine();
                sw.WriteLine("  Total Load per unit Area = p5 ");
                sw.WriteLine("                           = {0:f3} + {1:f3}", p1, loads_walls);
                sw.WriteLine("                           = {0:f2} kN/sq.m", p5);

                shear_force = SF_BS;
                //sw.WriteLine("  = p6*[b-(deff+cover)*2/1000] / 2");
                //sw.WriteLine("  = {0:f3}*[{1:f2}-({2:f2}+{3:f2})*2/1000] / 2",
                //    p6,
                //    b,
                //    deff,
                //    cover);
                sw.WriteLine("  Shear Force   = {0:f2} kN", shear_force);

                shear_stress = shear_force * 1000 / (1000 * deff);
                sw.WriteLine();
                sw.WriteLine("  Shear Stress = {0:f2}*1000/(1000*{1:f2})",
                    shear_force, deff);
                sw.WriteLine("               = {0:f2} N/sq.mm", shear_stress);

                sw.WriteLine();
                //sw.WriteLine("  Using Table 23, IS 456 : 2000 (Page 84)");

                percent = pro_st_ast * 100 / (1000 * deff);
                //percent = req_st_ast * 100 / (1000 * deff);
                //tau_c = iApp.Tables.Permissible_Shear_Stress(percent, Con_Grade);
                tau_c = Get_Table_1_Value(percent, Con_Grade, ref ref_string);

                sw.WriteLine("  Using Table 1, given at end of this report. {0}", ref_string);
                sw.WriteLine();
                sw.WriteLine("  percent = Ast*100/(1000*deff)");
                sw.WriteLine("          = {0:f2}*100/(1000*{1:f2})",
                    req_st_ast, deff);
                sw.WriteLine("          = {0:f0}%", (percent * 100));

                sw.WriteLine();
                sw.WriteLine("  τ_c = {0:f3} N/sq.mm.", tau_c);
                shear_capacity = tau_c * 1000 * deff;
                sw.WriteLine();
                sw.WriteLine("  Shear Capacity of section = τ_c*1000*deff");
                sw.WriteLine("                            = {0:f3}*1000*{1:f3} N",
                    tau_c, deff);
                sw.WriteLine("                            = {0:f2} N", shear_capacity);

                shear_capacity = shear_capacity / 1000.0;
                sw.WriteLine("                            = {0:f2} kN", shear_capacity);

                //balance_shear = shear_force - shear_capacity;
                balance_shear = (shear_force - shear_capacity);

                if (balance_shear > 0)
                {
                    sw.WriteLine();
                    sw.WriteLine("  Balance Shear = {0:f2} - {1:f2} = {2:f2} kN = {3:f2} N",
                        shear_force, shear_capacity, balance_shear, (balance_shear * 1000));

                    balance_shear = balance_shear * 1000;
                    sw.WriteLine();
                    ast_2 = Math.PI * dia_shr * dia_shr / 4 * (1000 / 200);

                    Asw = balance_shear * 200 / (200 * deff);
                    //sw.WriteLine("  Asw = {0:f3} * 200 / ({1:f2} * {2:f2})",
                    //    balance_shear, 200, deff);

                    //sw.WriteLine("      = {0:f3} sq.mm.", Asw);

                    //sw.WriteLine();
                    //sw.WriteLine("  Provide T10 @ 200 mm c/c, Ast = {0:f2} sq.mm per metre width.", ast_2);

                    bd13 = bd11 = bd14 = bd12 = dia_shr;
                    sp13 = sp11 = sp14 = sp12 = 200;

                    #region Bar_Dia And Spacing for Drawing Table
                    lst_Bar_Dia.Add(10);
                    lst_Bar_Space.Add(250);
                    #endregion

                    //x = -((((shear_capacity * 2) / p5) - b) * (1000 / 2) + cover);
                    ////x = x / 1000;
                    //x = Math.Abs(x / 1000.0);
                    //sw.WriteLine();
                    //sw.WriteLine("  p5*[b-(x+cover)*(2/1000)]/2 = {0:f3}", shear_capacity);

                    //_x = x * 1000;
                    ////double _x = x / 100;
                    ////_x = (int)(_x + 1);
                    ////_x = _x * 100;

                    //sw.WriteLine();
                    //sw.WriteLine("  x = -((((shear_capacity * 2) / p5) - b) * (1000 / 2) + cover)", x, _x);
                    //sw.WriteLine();
                    //sw.WriteLine("    = -(((({0:f3} * 2) / {1:f3}) - {2:f3}) * (1000 / 2) + {3})", shear_capacity, p5, b, cover);



                    //sw.WriteLine();
                    //sw.WriteLine("  x = {0:f2} m = {1:f2} mm ", x, _x);
                    //sw.WriteLine();
                    //if (_x < 300)
                    //    sw.WriteLine("  Provided Shear reinforcements for a distance of {0:f2} mm from", _x);
                    //else
                    //{
                    //    sw.WriteLine("  As the required spacing per metre width is more than 200,");
                    //    sw.WriteLine("  Provided Shear reinforcements for a distance of 200mm from", _x);
                    //}
                    //sw.WriteLine("  the face of wall on both sides.");



                    ast_2 = Math.PI * dia_shr * dia_shr / 4.0;
                    double Asv = ast_2;
                    double V = balance_shear;
                    sw.WriteLine("Area of a Steel Reinforcement bar of 10 mm dia = Asv = {0:f2} sq.mm.", Asv);

                    double Sv = sigma_st * Asv * deff * m / V;
                    sw.WriteLine("Spacing = Sv = σ_st * Asv * deff * m / V");
                    sw.WriteLine("             = {0} * {1:f3} * {2:f3} * {3} / {4:f3}", sigma_st, Asv, deff, m, V);
                    sw.WriteLine("             = {0:f3} mm", Sv);

                    sw.WriteLine();
                    spacing = Sv;
                    if (spacing < 200)
                    {
                        pro_st_ast = ast_2 * (1000.0 / spacing);
                        sw.WriteLine("  Provided T{0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm per metre width.", dia_shr, spacing, pro_st_ast);
                    }
                    else
                    {
                        spacing = 200;
                        pro_st_ast = ast_2 * (1000.0 / spacing);
                        sw.WriteLine("  As the required spacing per metre width is more than 200,");
                        sw.WriteLine("  Provided T{0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm per metre width.", dia_shr, spacing, pro_st_ast);
                    }




                }
                else
                {
                    sw.WriteLine("  Provide T{0} @ 200 mm c/c, Ast = {1:f2} sq.mm , ({1:f2} * (1000/200)) = {2:f2} sq.mm per metre width.",dia_shr, ast_2, (ast_2 * 5));
                    bd9 = bd10 = dia_shr;
                    sp9 = sp10 = 200;


                    #region Bar_Dia And Spacing for Drawing Table
                    lst_Bar_Dia.Add(bd9);
                    lst_Bar_Space.Add(sp9);
                    #endregion
                }
                
                #endregion

                #region STEP 4 : DESIGN OF SIDE WALLS

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("STEP 4 : DESIGN OF SIDE WALLS");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Maximum moments at joints with top slab and bottom slab are save as");
                sw.WriteLine("  taken for Design of Slabs. So provide same Reinforments.");

                sw.WriteLine();
                //sw.WriteLine("  Midspan moments are calculated as:");
                //sw.WriteLine("      {0:f2}, {1:f2} and {2:f2}", lst_Mad[0], lst_Mad[1], lst_Mad[2]);


                M = Math.Abs(BM_SW);
                sw.WriteLine("  Maximum Bending Moment at Support / Mid Span =  M = {0:f2} kN-m", M);

                double eff_thickness = Math.Sqrt((M * 10e5) / (1000 * R));

                sw.WriteLine();
                sw.WriteLine("  Effective Thickness of wall required = √((M * 10^6) / (1000 * R))");
                sw.WriteLine("                                       = √(({0:f2} * 10^6) / (1000 * {1:f3}))",
                    M, R);
                sw.WriteLine("                                       = {0:f2} mm", eff_thickness);


                sw.WriteLine();
                sw.WriteLine("  Provided Wall Thickness = {0:f2} mm", (d3 * 1000));
                sw.WriteLine();

                bar_dia = bar_dia_side;
                sw.WriteLine("  Considering Clear Cover = {0} mm and Reinforcement bar dia = {1} mm",
                cover, bar_dia);
                deff = (d3 * 1000) - cover - (bar_dia / 2.0);


                sw.WriteLine();
                if (deff > eff_thickness)
                {
                    //sw.WriteLine(" Provided Effective thickness = {0:f2} > {1:f2} mm , OK", deff, eff_thickness);

                    sw.WriteLine("  Effective thickness  = {0} - {1} - {2:f0}/2 = {3:f2} mm > {4:f2} mm OK",
                        (d3 * 1000), cover, bar_dia, deff, eff_thickness);
             
                }
                else
                {
                    //sw.WriteLine("      Provided Effective thickness = {0:f2} < {1:f2} mm , NOT OK", deff, eff_thickness);


                    sw.WriteLine("  Effective thickness  = {0} - {1} - {2:f0}/2 = {3:f2} mm < {4:f2} mm     NOT OK",
                        (d3 * 1000), cover, bar_dia, deff, eff_thickness);
             
                }


                sw.WriteLine();
                sw.WriteLine("  Lever Arm = j = 0.5 + √(0.25 - ((M*10^6)/(0.87*fck*1000*deff^2))) = {0:f3}", j);
                sw.WriteLine();
                req_st_ast = (M * 10E5) / (0.87 * fy * j * deff);
                sw.WriteLine();
                sw.WriteLine("  Required Main Reinforcement Steel per metre width,");
                sw.WriteLine("        Ast = (M*10^6)/(0.87 * fy * j * d)");
                sw.WriteLine("            = ({0:f2}*10^6)/(0.87 * {1:f2} * {2:f3} * {3:f2})",
                    M,
                    fy, j, deff);
                sw.WriteLine("            = {0:f3} sq.mm.", req_st_ast);

                pro_st_ast = (Math.PI * bar_dia * bar_dia / 4);

                spacing = 1000 / (req_st_ast / pro_st_ast);
                //spacing = (int)((1000.0 / spacing) / 10.0);
                //spacing = spacing * 10.0;
                sw.WriteLine();
                sw.WriteLine("Area of a Reinforcement Steel bar = Abar = π*{0}^2/4 = {1:f3} sq.mm", bar_dia, pro_st_ast);
                sw.WriteLine();
                sw.WriteLine("Required spacing using bar T{0:f0}", bar_dia);
                sw.WriteLine();
                sw.WriteLine("          = 1000/({0:f3}/{1:f3})", req_st_ast, pro_st_ast);
                sw.WriteLine("          = {0:f3} mm", spacing);
                spacing = (int)(spacing / 10.0);
                spacing = (spacing * 10.0);
                sw.WriteLine();
                sw.WriteLine("          = {0:f0} mm", spacing);
                sw.WriteLine();
                sw.WriteLine();
                if (spacing < 200)
                {
                    pro_st_ast = pro_st_ast * (1000.0 / spacing);
                    sw.WriteLine("  Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm per metre width.", bar_dia, spacing, pro_st_ast);
                }
                else
                {
                    spacing = 200;
                    pro_st_ast = pro_st_ast * (1000.0 / spacing);
                    sw.WriteLine("  As the required spacing per metre width is more than 200,");
                    sw.WriteLine("  Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm per metre width.", bar_dia, spacing, pro_st_ast);
                }
                bar_dia = bar_dia_side;
                //pro_st_ast = (Math.PI * bar_dia * bar_dia / 4) * (1000.0 / 300.0);
                //sw.WriteLine("  Provided Ast = {0:f0} sq.mm ", pro_st_ast);
                sw.WriteLine();

                bd2 = bd5 = bar_dia;
                sp2 = sp5 = 300;


                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("DESIGN OF SHEAR REINFORCEMENT");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("  Total Load per unit Area for Top Slab = p1 = {0:f2} kN/sq.m", p1);
                //sw.WriteLine("  Shear Force at distance = deff ,from face of wall");
                loads_walls = (d * d3 * (Cell_Nos+1) * gamma_c) / (1 * b + d3 + d3);
                sw.WriteLine();
                sw.WriteLine("  Load of Walls = (d * d3 *  (Cell_Nos+1) * γ_c)/(1 * (b + d3 + d3))");
                sw.WriteLine("                = ({0:f2} * {1:f2} * {2} * {3:f2})/(1 * ({4:f2} + {1:f2} + {1:f2}))",
                    d,
                    d3,
                    (Cell_Nos + 1),
                    gamma_c,
                    b);
                sw.WriteLine("                = {0:f2} kN/sq.m", loads_walls);

                double p6 = p1 + loads_walls;
                sw.WriteLine();
                sw.WriteLine("  Total Load per unit Area = p6");
                sw.WriteLine("                           = {0:f3} + {1:f3}", p1, loads_walls);
                sw.WriteLine("                           = {0:f2} kN/sq.m", p6);

                shear_force = SF_SW;
                //sw.WriteLine("  = p6*[b-(deff+cover)*2/1000] / 2");
                //sw.WriteLine("  = {0:f3}*[{1:f2}-({2:f2}+{3:f2})*2/1000] / 2",
                //    p6,
                //    b,
                //    deff,
                //    cover);
                sw.WriteLine("  Shear Force   = {0:f2} kN", shear_force);

                shear_stress = shear_force * 1000 / (1000 * deff);
                sw.WriteLine();
                sw.WriteLine("  Shear Stress = {0:f2}*1000/(1000*{1:f2})",
                    shear_force, deff);
                sw.WriteLine("               = {0:f2} N/sq.mm", shear_stress);

                sw.WriteLine();
                //sw.WriteLine("  Using Table 23, IS 456 : 2000 (Page 84)");

                percent = pro_st_ast * 100 / (1000 * deff);
                //percent = req_st_ast * 100 / (1000 * deff);
                //tau_c = iApp.Tables.Permissible_Shear_Stress(percent, Con_Grade);
                tau_c = Get_Table_1_Value(percent, Con_Grade, ref ref_string);

                sw.WriteLine("  Using Table 1, given at end of this report. {0}", ref_string);
                sw.WriteLine();
                sw.WriteLine("  percent = Ast*100/(1000*deff)");
                sw.WriteLine("          = {0:f2}*100/(1000*{1:f2})",
                    req_st_ast, deff);
                sw.WriteLine("          = {0:f0}%", (percent * 100));

                sw.WriteLine();
                sw.WriteLine("  τ_c = {0:f3} N/sq.mm.", tau_c);
                shear_capacity = tau_c * 1000 * deff;
                sw.WriteLine();
                sw.WriteLine("  Shear Capacity of section = τ_c*1000*deff");
                sw.WriteLine("                            = {0:f3}*1000*{1:f3} N",
                    tau_c, deff);
                sw.WriteLine("                            = {0:f2} N", shear_capacity);

                shear_capacity = shear_capacity / 1000.0;
                sw.WriteLine("                            = {0:f2} kN", shear_capacity);

                //balance_shear = shear_force - shear_capacity;
                balance_shear = (shear_force - shear_capacity);
                if (balance_shear > 0)
                {
                    sw.WriteLine();
                    sw.WriteLine("  Balance Shear = {0:f2} - {1:f2} = {2:f2} kN = {3:f2} N",
                        shear_force, shear_capacity, balance_shear, (balance_shear * 1000));

                    balance_shear = balance_shear * 1000;
                    sw.WriteLine();
                    //ast_2 = Math.PI * 10 * 10 / 4 * (1000 / 250);
                    ast_2 = Math.PI * dia_shr * dia_shr / 4;

                    //Asw = balance_shear * 250 / (200 * deff);
                    //sw.WriteLine("  Asw = {0:f3} * 250 / ({1:f2} * {2:f2})",
                    //    balance_shear, 200, deff);

                    //sw.WriteLine("      = {0:f3} sq.mm.", Asw);

                    //sw.WriteLine();
                    //sw.WriteLine("  Provide T10 @ 250 mm c/c, Ast = {0:f2} sq.mm per metre width.", ast_2);

                    bd13 = bd11 = bd14 = bd12 = dia_shr;
                    sp13 = sp11 = sp14 = sp12 = 200;

                    #region Bar_Dia And Spacing for Drawing Table
                    lst_Bar_Dia.Add(10);
                    lst_Bar_Space.Add(250);
                    #endregion
                    //p5 = ((d * d3 * 2 * gamma_c) / (1 * (b + d3 + d3)));


                    //x = -((((shear_capacity * 2) / p6) - b) * (1000 / 2) + cover);

                    //x = Math.Abs(x / 1000);
                    //sw.WriteLine();
                    //sw.WriteLine("  p6*[b-(x+cover)*(2/1000)]/2 = {0:f3}", shear_capacity);

                    //_x = x * 1000;
                    ////double _x = x / 100;
                    ////_x = (int)(_x + 1);
                    ////_x = _x * 100;

                    //sw.WriteLine();
                    //sw.WriteLine("  x = -((((shear_capacity * 2) / p6) - b) * (1000 / 2) + cover)", x, _x);
                    //sw.WriteLine();
                    //sw.WriteLine("    = -(((({0:f3} * 2) / {1:f3}) - {2:f3}) * (1000 / 2) + {3})", shear_capacity, p6, b, cover);

                    //sw.WriteLine();
                    //sw.WriteLine("  x = {0:f2} m = {1:f2} mm ", x, _x);
                    //sw.WriteLine();
                    //if (_x < 300)
                    //    sw.WriteLine("  Provided Shear reinforcements for a distance of {0:f2} mm from", _x);
                    //else
                    //{
                    //    sw.WriteLine("  As the required spacing per metre width is more than 300,");
                    //    sw.WriteLine("  Provided Shear reinforcements for a distance of 300mm from", _x);
                    //}
                    //sw.WriteLine("  the face of wall on both sides.");




                    ast_2 = Math.PI * dia_shr * dia_shr / 4.0;
                    double Asv = ast_2;
                    double V = balance_shear;
                    sw.WriteLine("Area of a Steel Reinforcement bar of {0} mm dia = Asv = {1:f2} sq.mm.",dia_shr, Asv);

                    double Sv = sigma_st * Asv * deff * m / V;
                    sw.WriteLine("Spacing = Sv = σ_st * Asv * deff * m / V");
                    sw.WriteLine("             = {0} * {1:f3} * {2:f3} * {3} / {4:f3}", sigma_st, Asv, deff, m, V);
                    sw.WriteLine("             = {0:f3} mm", Sv);

                    sw.WriteLine();
                    spacing = Sv;
                    if (spacing < 200)
                    {
                        pro_st_ast = ast_2 * (1000.0 / spacing);
                        sw.WriteLine("  Provided T{0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm per metre width.", dia_shr, spacing, pro_st_ast);
                    }
                    else
                    {
                        spacing = 200;
                        pro_st_ast = ast_2 * (1000.0 / spacing);
                        sw.WriteLine("  As the required spacing per metre width is more than 200,");
                        sw.WriteLine("  Provided T{0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm per metre width.", dia_shr, spacing, pro_st_ast);
                    }
                }
                else
                {


                    sw.WriteLine("  Provide T{0} @ 200 mm c/c, Ast = {1:f2} sq.mm , ({1:f2} * (1000/200)) = {2:f2} sq.mm per metre width.",dia_shr, ast_2, (ast_2 * 5));
                    bd9 = bd10 = dia_shr;
                    sp9 = sp10 = 200;


                    #region Bar_Dia And Spacing for Drawing Table
                    lst_Bar_Dia.Add(bd9);
                    lst_Bar_Space.Add(sp9);
                    #endregion
                }
                #endregion

                if (Cell_Nos > 1)
                {
                    #region STEP 5 : DESIGN OF INTERMEDIATE WALLS

                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("STEP 4 : DESIGN OF INTERMEDIATE WALLS");
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine("  Maximum moments at joints with top slab and bottom slab are save as");
                    sw.WriteLine("  taken for Design of Slabs. So provide same Reinforments.");

                    sw.WriteLine();
                    //sw.WriteLine("  Midspan moments are calculated as:");
                    //sw.WriteLine("      {0:f2}, {1:f2} and {2:f2}", lst_Mad[0], lst_Mad[1], lst_Mad[2]);


                    M = Math.Abs(BM_IW);
                    sw.WriteLine("  Maximum Bending Moment =  M = {0:f2} kN-m", M);

                    eff_thickness = Math.Sqrt((M * 10e5) / (1000 * R));

                    sw.WriteLine();
                    sw.WriteLine("  Effective Thickness of wall required = √((M * 10^6) / (1000 * R))");
                    sw.WriteLine("                                       = √(({0:f2} * 10^6) / (1000 * {1:f3}))",
                        M, R);
                    sw.WriteLine("                                       = {0:f2} mm", eff_thickness);


                    sw.WriteLine();
                    sw.WriteLine("  Provided Wall Thickness = {0:f2} mm", (d4 * 1000));
                    sw.WriteLine();

                    bar_dia = bar_dia_intermediate;
                    sw.WriteLine("  Considering Clear Cover = {0:f2} mm and Reinforcement bar dia = {1} mm",
                    cover, bar_dia);
                    sw.WriteLine();
                    deff = (d4 * 1000) - cover - (bar_dia / 2.0);


                    if (deff > eff_thickness)
                    {
                        //sw.WriteLine(" Provided Effective thickness = {0:f2} > {1:f2} mm , OK", deff, eff_thickness);

                        sw.WriteLine("  Effective thickness  = {0} - {1} - {2:f0}/2 = {3:f2} mm > {4:f2} mm OK",
                            (d4 * 1000), cover, bar_dia, deff, eff_thickness);

                    }
                    else
                    {
                        //sw.WriteLine("      Provided Effective thickness = {0:f2} < {1:f2} mm , NOT OK", deff, eff_thickness);


                        sw.WriteLine("  Effective thickness  = {0} - {1} - {2:f0}/2 = {3:f2} mm < {4:f2} mm     NOT OK",
                            (d4 * 1000), cover, bar_dia, deff, eff_thickness);

                    }


                    sw.WriteLine();
                    sw.WriteLine("  Lever Arm = j = 0.5 + √(0.25 - ((M*10^6)/(0.87*fck*1000*deff^2))) = {0:f3}", j);
                    sw.WriteLine();
                    req_st_ast = (M * 10E5) / (0.87 * fy * j * deff);
                    sw.WriteLine();
                    sw.WriteLine("  Required Main Reinforcement Steel per metre width,");
                    sw.WriteLine("        Ast = (M*10^6)/(0.87 * fy * j * d)");
                    sw.WriteLine("            = ({0:f2}*10^6)/(0.87 * {1:f2} * {2:f3} * {3:f2})",
                        M,
                        fy, j, deff);
                    sw.WriteLine("            = {0:f3} sq.mm.", req_st_ast);

                    pro_st_ast = (Math.PI * bar_dia * bar_dia / 4);

                    spacing = 1000 / (req_st_ast / pro_st_ast);
                    //spacing = (int)((1000.0 / spacing) / 10.0);
                    //spacing = spacing * 10.0;
                    sw.WriteLine();
                    sw.WriteLine("Area of a Reinforcement Steel bar = Abar = π*{0}^2/4 = {1:f3} sq.mm", bar_dia, pro_st_ast);
                    sw.WriteLine();
                    sw.WriteLine("Required spacing using bar T{0:f0}", bar_dia);
                    sw.WriteLine();
                    sw.WriteLine("          = 1000/({0:f3}/{1:f3})", req_st_ast, pro_st_ast);
                    sw.WriteLine("          = {0:f3} mm", spacing);
                    spacing = (int)(spacing / 10.0);
                    spacing = (spacing * 10.0);
                    sw.WriteLine();
                    sw.WriteLine("          = {0:f0} mm", spacing);
                    sw.WriteLine();
                    sw.WriteLine();
                    if (spacing < 200)
                    {
                        pro_st_ast = pro_st_ast * (1000.0 / spacing);
                        sw.WriteLine("  Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm per metre width.", bar_dia, spacing, pro_st_ast);
                    }
                    else
                    {
                        spacing = 200;
                        pro_st_ast = pro_st_ast * (1000.0 / spacing);
                        sw.WriteLine("  As the required spacing per metre width is more than 200,");
                        sw.WriteLine("  Provided T{0:f0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm per metre width.", bar_dia, spacing, pro_st_ast);
                    }
                    bar_dia = bar_dia_side;
                    //pro_st_ast = (Math.PI * bar_dia * bar_dia / 4) * (1000.0 / 300.0);
                    //sw.WriteLine("  Provided Ast = {0:f0} sq.mm ", pro_st_ast);
                    sw.WriteLine();

                    bd2 = bd5 = bar_dia;
                    sp2 = sp5 = 300;


                    sw.WriteLine();
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine("DESIGN OF SHEAR REINFORCEMENT");
                    sw.WriteLine("------------------------------------------------------------");
                    sw.WriteLine();
                    sw.WriteLine("  Total Load per unit Area for Top Slab = p1 = {0:f2} kN/sq.m", p1);
                    //sw.WriteLine("  Shear Force at distance = deff ,from face of wall");
                    loads_walls = (d * d4 * (Cell_Nos + 1) * gamma_c) / (1 * b + d4 + d4);
                    sw.WriteLine();
                    sw.WriteLine("  Load of Walls = (d * d4 *  (Cell_Nos+1) * γ_c)/(1 * (b + d4 + d4))");
                    sw.WriteLine("                = ({0:f2} * {1:f2} * {2} * {3:f2})/(1 * ({4:f2} + {1:f2} + {1:f2}))",
                        d,
                        d4,
                        (Cell_Nos + 1),
                        gamma_c,
                        b);
                    sw.WriteLine("                = {0:f2} kN/sq.m", loads_walls);

                    p6 = p1 + loads_walls;
                    sw.WriteLine();
                    sw.WriteLine("  Total Load per unit Area = p6");
                    sw.WriteLine("                           = {0:f3} + {1:f3}", p1, loads_walls);
                    sw.WriteLine("                           = {0:f2} kN/sq.m", p6);

                    shear_force = SF_IW;
                    //sw.WriteLine("  = p6*[b-(deff+cover)*2/1000] / 2");
                    //sw.WriteLine("  = {0:f3}*[{1:f2}-({2:f2}+{3:f2})*2/1000] / 2",
                    //    p6,
                    //    b,
                    //    deff,
                    //    cover);
                    sw.WriteLine("  Shear Force   = {0:f2} kN", shear_force);

                    shear_stress = shear_force * 1000 / (1000 * deff);
                    sw.WriteLine();
                    sw.WriteLine("  Shear Stress = {0:f2}*1000/(1000*{1:f2})",
                        shear_force, deff);
                    sw.WriteLine("               = {0:f2} N/sq.mm", shear_stress);

                    sw.WriteLine();
                    //sw.WriteLine("  Using Table 23, IS 456 : 2000 (Page 84)");

                    percent = pro_st_ast * 100 / (1000 * deff);
                    //percent = req_st_ast * 100 / (1000 * deff);
                    //tau_c = iApp.Tables.Permissible_Shear_Stress(percent, Con_Grade);
                    tau_c = Get_Table_1_Value(percent, Con_Grade, ref ref_string);

                    sw.WriteLine("  Using Table 1, given at end of this report. {0}", ref_string);
                    sw.WriteLine();
                    sw.WriteLine("  percent = Ast*100/(1000*deff)");
                    sw.WriteLine("          = {0:f2}*100/(1000*{1:f2})",
                        req_st_ast, deff);
                    sw.WriteLine("          = {0:f0}%", (percent * 100));

                    sw.WriteLine();
                    sw.WriteLine("  τ_c = {0:f3} N/sq.mm.", tau_c);
                    shear_capacity = tau_c * 1000 * deff;
                    sw.WriteLine();
                    sw.WriteLine("  Shear Capacity of section = τ_c*1000*deff");
                    sw.WriteLine("                            = {0:f3}*1000*{1:f3} N",
                        tau_c, deff);
                    sw.WriteLine("                            = {0:f2} N", shear_capacity);

                    shear_capacity = shear_capacity / 1000.0;
                    sw.WriteLine("                            = {0:f2} kN", shear_capacity);

                    //balance_shear = shear_force - shear_capacity;
                    balance_shear = (shear_force - shear_capacity);
                    if (balance_shear > 0)
                    {
                        sw.WriteLine();
                        sw.WriteLine("  Balance Shear = {0:f2} - {1:f2} = {2:f2} kN = {3:f2} N",
                            shear_force, shear_capacity, balance_shear, (balance_shear * 1000));

                        balance_shear = balance_shear * 1000;
                        sw.WriteLine();
                        //ast_2 = Math.PI * 10 * 10 / 4 * (1000 / 250);
                        ast_2 = Math.PI * dia_shr * dia_shr / 4;

                        //Asw = balance_shear * 250 / (200 * deff);
                        //sw.WriteLine("  Asw = {0:f3} * 250 / ({1:f2} * {2:f2})",
                        //    balance_shear, 200, deff);

                        //sw.WriteLine("      = {0:f3} sq.mm.", Asw);

                        //sw.WriteLine();
                        //sw.WriteLine("  Provide T10 @ 250 mm c/c, Ast = {0:f2} sq.mm per metre width.", ast_2);

                        bd13 = bd11 = bd14 = bd12 = dia_shr;
                        sp13 = sp11 = sp14 = sp12 = 200;

                        #region Bar_Dia And Spacing for Drawing Table
                        lst_Bar_Dia.Add(10);
                        lst_Bar_Space.Add(250);
                        #endregion
                        //p5 = ((d * d3 * 2 * gamma_c) / (1 * (b + d3 + d3)));


                        //x = -((((shear_capacity * 2) / p6) - b) * (1000 / 2) + cover);

                        //x = Math.Abs(x / 1000);
                        //sw.WriteLine();
                        //sw.WriteLine("  p6*[b-(x+cover)*(2/1000)]/2 = {0:f3}", shear_capacity);

                        //_x = x * 1000;
                        ////double _x = x / 100;
                        ////_x = (int)(_x + 1);
                        ////_x = _x * 100;

                        //sw.WriteLine();
                        //sw.WriteLine("  x = -((((shear_capacity * 2) / p6) - b) * (1000 / 2) + cover)", x, _x);
                        //sw.WriteLine();
                        //sw.WriteLine("    = -(((({0:f3} * 2) / {1:f3}) - {2:f3}) * (1000 / 2) + {3})", shear_capacity, p6, b, cover);

                        //sw.WriteLine();
                        //sw.WriteLine("  x = {0:f2} m = {1:f2} mm ", x, _x);
                        //sw.WriteLine();
                        //if (_x < 300)
                        //    sw.WriteLine("  Provided Shear reinforcements for a distance of {0:f2} mm from", _x);
                        //else
                        //{
                        //    sw.WriteLine("  As the required spacing per metre width is more than 300,");
                        //    sw.WriteLine("  Provided Shear reinforcements for a distance of 300mm from", _x);
                        //}
                        //sw.WriteLine("  the face of wall on both sides.");




                        ast_2 = Math.PI * dia_shr * dia_shr / 4.0;
                        double Asv = ast_2;
                        double V = balance_shear;
                        sw.WriteLine("Area of a Steel Reinforcement bar of {0} mm dia = Asv = {1:f2} sq.mm.", dia_shr, Asv);

                        double Sv = sigma_st * Asv * deff * m / V;
                        sw.WriteLine("Spacing = Sv = σ_st * Asv * deff * m / V");
                        sw.WriteLine("             = {0} * {1:f3} * {2:f3} * {3} / {4:f3}", sigma_st, Asv, deff, m, V);
                        sw.WriteLine("             = {0:f3} mm", Sv);

                        sw.WriteLine();
                        spacing = Sv;
                        if (spacing < 200)
                        {
                            pro_st_ast = ast_2 * (1000.0 / spacing);
                            sw.WriteLine("  Provided T{0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm per metre width.", dia_shr, spacing, pro_st_ast);
                        }
                        else
                        {
                            spacing = 200;
                            pro_st_ast = ast_2 * (1000.0 / spacing);
                            sw.WriteLine("  As the required spacing per metre width is more than 200,");
                            sw.WriteLine("  Provided T{0} bars @{1:f0} mm c/c, Provided Ast = {2:f3} sq.mm per metre width.", dia_shr, spacing, pro_st_ast);
                        }
                    }
                    else
                    {


                        sw.WriteLine("  Provide T{0} @ 200 mm c/c, Ast = {1:f2} sq.mm , ({1:f2} * (1000/200)) = {2:f2} sq.mm per metre width.", dia_shr, ast_2, (ast_2 * 5));
                        bd9 = bd10 = dia_shr;
                        sp9 = sp10 = 200;


                        #region Bar_Dia And Spacing for Drawing Table
                        lst_Bar_Dia.Add(bd9);
                        lst_Bar_Space.Add(sp9);
                        #endregion
                    }
                    #endregion

                }
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("TABLE 2 : PERMISSIBLE SHEAR STRESS ");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine();
                Write_Table_1(sw);

                sw.WriteLine();
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
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public double Get_Table_1_Value(double percent, CONCRETE_GRADE con_grade, ref string ref_string)
        {
            return iApp.Tables.Permissible_Shear_Stress(percent, con_grade, ref ref_string);
        }

        public void Write_Table_1(StreamWriter sw)
        {
            //string table_file = Path.Combine(Application.StartupPath, "TABLES");
            //table_file = Path.Combine(table_file, "Box_Culvert_Table_1.txt");

            List<string> lst_content = iApp.Tables.Get_Tables_Permissible_Shear_Stress();

            for (int i = 0; i < lst_content.Count; i++)
            {
                sw.WriteLine(lst_content[i]);
            }
            lst_content.Clear();
        }

        public void WriteUserInput()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));
            try
            {

                #region WRITE USER INPUT DATA

                sw.WriteLine("CODE : BOX CULVERT");
                sw.WriteLine("USER INPUT DATA");
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("De = {0:f3}", De);
                sw.WriteLine("b = {0:f3}", b);
                sw.WriteLine("d = {0:f3}", d);
                sw.WriteLine("d1 = {0:f3}", d1);
                sw.WriteLine("d2 = {0:f3}", d2);
                sw.WriteLine("d3 = {0:f3}", d3);
                sw.WriteLine("gamma_b = {0:f3}", gamma_b);
                sw.WriteLine("gamma_c = {0:f3}", gamma_c);
                sw.WriteLine("R = {0:f3}", R);
                sw.WriteLine("t = {0:f3}", t);
                sw.WriteLine("j = {0:f3}", j);
                sw.WriteLine("cover = {0:f3}", cover);
                sw.WriteLine("sigma_st = {0:f3}", sigma_st);
                sw.WriteLine("sigma_c = {0:f3}", sigma_c);
                sw.WriteLine("b1 = {0:f3}", b1);
                sw.WriteLine("b2 = {0:f3}", b2);
                sw.WriteLine("a1 = {0:f3}", a1);
                sw.WriteLine("w1 = {0:f3}", w1);
                sw.WriteLine("w2 = {0:f3}", w2);
                sw.WriteLine("b3 = {0:f3}", b3);
                sw.WriteLine("F = {0:f3}", F);
                sw.WriteLine("S = {0:f3}", S);
                sw.WriteLine("sbc = {0:f3}", sbc);
                sw.WriteLine();
                sw.WriteLine("FINISH");

                #endregion
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public void Write_Drawing()
        {
            drawing_path = Path.Combine(system_path, "BOX_CULVERT_DRAWING.FIL");
            StreamWriter sw = new StreamWriter(new FileStream(drawing_path, FileMode.Create));
            #region Variable
            //string _box1 = "1/3 x 3/5";
            //string _box2 = "[1/3 x 3/5]";
            string _box1 = String.Format("1/{0:f0} x {1:f0}/{2:f0}", b, d, De);
            string _box2 = "[" + _box1 + "]";
            double _a = b * 1000.0;
            double _b = d * 1000.0;
            double _c = 0;
            double _d = d1 * 1000.0;
            double _e = d2 * 1000.0;
            double _f = d3 * 1000.0;
            double _h = De * 1000.0;
            //double _pressure = 130.43;

            #endregion

            try
            {
                sw.WriteLine("_box1={0:f0}", _box1);
                sw.WriteLine("_box2={0:f0}", _box2);
                sw.WriteLine("_a={0:f0}", _a);
                sw.WriteLine("_b={0:f0}", _b);
                sw.WriteLine("_c={0:f0}", _c);
                sw.WriteLine("_d={0:f0}", _d);
                sw.WriteLine("_e={0:f0}", _e);
                sw.WriteLine("_f={0:f0}", _f);
                sw.WriteLine("_h={0:f0}", _h);
                sw.WriteLine("_pressure={0:f2}", _pressure);

                sw.WriteLine("_b1={0:f0}", lst_Bar_Dia[1]);
                sw.WriteLine("_s1={0:f0}", lst_Bar_Space[1]);
                sw.WriteLine("_b2={0:f0}", lst_Bar_Dia[2]);
                sw.WriteLine("_s2={0:f0}", lst_Bar_Space[2]);
                sw.WriteLine("_b3={0:f0}", lst_Bar_Dia[3]);
                sw.WriteLine("_s3={0:f0}", lst_Bar_Space[3]);
                sw.WriteLine("_b4={0:f0}", lst_Bar_Dia[4]);
                sw.WriteLine("_s4={0:f0}", lst_Bar_Space[4]);
                sw.WriteLine("_b5={0:f0}", lst_Bar_Dia[5]);
                sw.WriteLine("_s5={0:f0}", lst_Bar_Space[5]);
                sw.WriteLine("_b6={0:f0}", lst_Bar_Dia[6]);
                sw.WriteLine("_s6={0:f0}", lst_Bar_Space[6]);
                sw.WriteLine("_b7={0:f0}", lst_Bar_Dia[7]);
                sw.WriteLine("_s7={0:f0}", lst_Bar_Space[7]);
                sw.WriteLine("_b8={0:f0}", lst_Bar_Dia[8]);
                sw.WriteLine("_s8={0:f0}", lst_Bar_Space[8]);
                sw.WriteLine("_b9={0:f0}", lst_Bar_Dia[9]);
                sw.WriteLine("_s9={0:f0}", lst_Bar_Space[9]);
                sw.WriteLine("_b10={0:f0}", lst_Bar_Dia[10]);
                sw.WriteLine("_s10={0:f0}", lst_Bar_Space[10]);
                sw.WriteLine("_b11={0:f0}", lst_Bar_Dia[11]);
                sw.WriteLine("_s11={0:f0}", lst_Bar_Space[11]);
                sw.WriteLine("_b12={0:f0}", lst_Bar_Dia[12]);
                sw.WriteLine("_s12={0:f0}", lst_Bar_Space[12]);
                sw.WriteLine("_b13={0:f0}", lst_Bar_Dia[13]);
                sw.WriteLine("_s13={0:f0}", lst_Bar_Space[13]);
                sw.WriteLine("_b14={0:f0}", lst_Bar_Dia[14]);
                sw.WriteLine("_s14={0:f0}", lst_Bar_Space[14]);
                sw.WriteLine("_b15={0:f0}", lst_Bar_Dia[15]);
                sw.WriteLine("_s15={0:f0}", lst_Bar_Space[15]);
                sw.WriteLine("_b16={0:f0}", lst_Bar_Dia[16]);
                sw.WriteLine("_s16={0:f0}", lst_Bar_Space[16]);
                sw.WriteLine("_b17={0:f0}", lst_Bar_Dia[17]);
                sw.WriteLine("_s17={0:f0}", lst_Bar_Space[17]);
                sw.WriteLine("_b18={0:f0}", lst_Bar_Dia[18]);
                sw.WriteLine("_s18={0:f0}", lst_Bar_Space[18]);
                sw.WriteLine("_b19={0:f0}", lst_Bar_Dia[19]);
                sw.WriteLine("_s19={0:f0}", lst_Bar_Space[19]);
                sw.WriteLine("_b20={0:f0}", lst_Bar_Dia[20]);
                sw.WriteLine("_s20={0:f0}", lst_Bar_Space[20]);
                sw.WriteLine("_b21={0:f0}", lst_Bar_Dia[21]);
                sw.WriteLine("_s21={0:f0}", lst_Bar_Space[21]);



            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
                lst_Bar_Dia.Clear();
                lst_Bar_Space.Clear();
            }
        }
    }

    public class Applied_Load
    {
        #region Properties
        public bool Check { get; set; }
        public string MemberNos { get; set; }
        public string Direction { get; set; }
        public string LoadType { get; set; }
        public double Load { get; set; }
        public double Start_Distance { get; set; }
        public double End_Distance { get; set; }
        #endregion Properties

        public Applied_Load()
        {
            Check = true;
            MemberNos = "";
            Direction = "GY";
            LoadType = "UNI";
            Load = 0.0;
            Start_Distance = 0.0;
            End_Distance = 0.0;
        }



        public override string ToString()
        {
            //if (Start_Distance != 0.0 && End_Distance != 0.0)
            if (End_Distance != 0.0)
            {
                return string.Format("{0} {1} {2} {3} {4:f3} {5:f3}", MemberNos, LoadType, Direction,
                                        Load, Start_Distance, End_Distance);
            }
            else if (Start_Distance != 0.0 && End_Distance == 0.0)
            {
                return string.Format("{0} {1} {2} {3} {4:f3}", MemberNos, LoadType, Direction.Replace("-", ""),
                                        Load, Start_Distance);
            }
            else if (Start_Distance == 0.0 && End_Distance != 0.0)
            {
                return string.Format("{0} {1} {2} {3} {4:f3} {5:f3}", MemberNos, LoadType, Direction.Replace("-", ""),
                                        Load, Start_Distance, End_Distance);
            }

            return string.Format("{0} {1} {2} {3}", MemberNos, LoadType, Direction.Replace("-", ""), Load);
        }
    }
    public class Applied_Load_Collection : List<Applied_Load>
    {
        public string Load_Title { get; set; }
        public Applied_Load_Collection()
            : base()
        {
            Load_Title = "";
        }

        public override string ToString()
        {
            return Load_Title;
        }

        public List<string> Get_Report()
        {
            List<string> list = new List<string>();

            //list.Add(string.Format("S. No.        Load Case No:            Member/Joint           Member/Joint Nos.    Loading Type       Load Direction          Load (kN) "));
            //list.Add(string.Format("1        1                Member                        1        UNI                 GY                 -38.922"));
            //list.Add(string.Format(""));
            list.Add(string.Format(Load_Title));
            list.Add(string.Format("".PadLeft(Load_Title.Length, '-')));


            bool ff = false;

            foreach (var item in this)
            {
                if (item.End_Distance != 0.0 || item.Start_Distance != 0.0)
                {
                    ff = true;
                    break;
                }
            }

            if (ff)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("------------------------------------------------------------------------"));
                list.Add(string.Format("MEMBER NOS       LOAD        LOAD       LOAD        START       END"));
                list.Add(string.Format("                 TYPE     DIRECTION    (KN/M)      DISTANCE   DISTANCE"));
                list.Add(string.Format("------------------------------------------------------------------------"));
                foreach (var item in this)
                {
                    //list.Add(string.Format("", item.MemberNos));
                    list.Add(string.Format("{0,10} {1,10}  {2,10}  {3,10}  {4,10:f3}  {5,10:f3} ", item.MemberNos, item.LoadType, item.Direction, item.Load, item.Start_Distance, item.End_Distance));
                }
                list.Add(string.Format("------------------------------------------------------------------------"));
            }
            else
            {
                list.Add(string.Format(""));
                list.Add(string.Format("-----------------------------------------------"));
                list.Add(string.Format("MEMBER NOS       LOAD        LOAD       LOAD   "));
                list.Add(string.Format("                 TYPE     DIRECTION    (KN/M)  "));
                list.Add(string.Format("-----------------------------------------------"));
                foreach (var item in this)
                {
                    //list.Add(string.Format("", item.MemberNos));
                    list.Add(string.Format("{0,10} {1,10}  {2,10}  {3,10:f3}", item.MemberNos, item.LoadType, item.Direction, item.Load));
                }
                list.Add(string.Format("-----------------------------------------------"));
            }

            return list;
        }

    }

    public class Earth_Pressure_Load
    {
        #region Properties
        public bool Check { get; set; }
        public string MemberNos { get; set; }
        public string Direction { get; set; }
        public string LoadType { get; set; }
        public double Load_P1 { get; set; }
        public double Load_P2 { get; set; }
        #endregion Properties

        public Earth_Pressure_Load()
        {
            Check = true;
            MemberNos = "";
            Direction = "Y";
            LoadType = "LIN";
            Load_P1 = 0.0;
            Load_P2 = 0.0;
        }
        public override string ToString()
        {

            return string.Format("{0} {1} {2} {3:f3} {4:f3}", MemberNos, LoadType, Direction.Replace("-", ""), Load_P1, Load_P2);
        }
    }
    public class Earth_Pressure_Load_Collection : List<Earth_Pressure_Load>
    {
        public Earth_Pressure_Load_Collection()
            : base()
        {
        }
        public List<string> Get_Report()
        {
            List<string> list = new List<string>();

            //list.Add(string.Format("S. No.        Load Case No:            Member/Joint           Member/Joint Nos.    Loading Type       Load Direction          Load (kN) "));
            //list.Add(string.Format("1        1                Member                        1        UNI                 GY                 -38.922"));
            //list.Add(string.Format(""));

            string Load_Title = "LOAD 3 EARTH PRESSURE";

            list.Add(string.Format(Load_Title));
            list.Add(string.Format("".PadLeft(Load_Title.Length, '-')));


            list.Add(string.Format("----------------------------------------------------------"));
            list.Add(string.Format("     MEMBER      LOAD        LOAD        P1         P2"));
            list.Add(string.Format("       NO        TYPE     DIRECTION    (KN/M)     (KN/M)"));
            list.Add(string.Format("----------------------------------------------------------"));
            //list.Add(string.Format("        33        LIN           Y     -53.848     -49.723"));

            foreach (var item in this)
            {
                //list.Add(string.Format("", item.MemberNos));
                list.Add(string.Format("{0,10} {1,10}  {2,10}  {3,10:f3}  {4,10:f3}", item.MemberNos, item.LoadType, item.Direction, item.Load_P1, item.Load_P2));
            }
            list.Add(string.Format("----------------------------------------------------------"));


            return list;
        }
    }

    public class Load_Combination
    {
        public int LoadCase { get; set; }
        public string Load_Title { get; set; }
        public List<string> Load_Comb { get; set; }

        public Load_Combination()
        {
            LoadCase = 0;
            Load_Title = "";
            Load_Comb = new List<string>();
        }

        public string[] ToArray()
        {
            List<string> list = new List<string>();

            int l = 0;
            string kStr = "";

            for (int i = 0; i < Load_Comb.Count; i += 2)
            {
                l = MyList.StringToInt(Load_Comb[i], 1);

                if (l == 1)
                    kStr += "DL+";
                else if (l == 2)
                    kStr += "SIDL+";
                else if (l == 3)
                    kStr += "EP+";
                else
                    kStr += "LL" + (l - 3) + "+";
            }
            Load_Title = kStr.Remove(kStr.Length - 1);



            list.Add(string.Format("LOAD COMB {0} {1}", LoadCase, Load_Title));
            kStr = "";
            foreach (var item in Load_Comb)
            {
                kStr += item + "  ";
            }
            list.Add(kStr);
            return list.ToArray();
        }
    }
    public class Load_Combination_Collection : List<Load_Combination>
    {
        public Load_Combination_Collection()
            : base()
        {
        }
        public List<string> Get_Report()
        {
            List<string> list = new List<string>();
            foreach (var item in this)
            {
                list.AddRange(item.ToArray());
            }
            return list;
        }
    }
}
