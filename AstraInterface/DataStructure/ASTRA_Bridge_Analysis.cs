using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using System.Windows.Forms;

namespace AstraInterface.DataStructure
{
    public class MemberSectionProperty
    {
        public MemberSectionProperty()
        {
            Breadth = 0.0;
            Depth = 0.0;
            Thickness = 0.0;
            Diameter = 0.0;
            UnitWeight = 2.4;
            TotalNos = 0;
            Length = 0.0;
            Weight = 0.0;
            PropertyName = "PRI";
            GroupName = "GroupName";
            AppliedSection = eAppliedSection.None;
            SectionDetails = new SectionData();

            IX = 0.0;
        }
        public void Calculate_Moment_Of_Inertia()
        {
            AX_Area = (ax1 - ax2);
            IX = ix1 - ix2;
            IY = iy1 - iy2;

            if (AppliedSection == eAppliedSection.Reactangular_Section || AppliedSection == eAppliedSection.Circular_Section)
                Weight = Length * UnitWeight * TotalNos;
            else
                Weight = Length * AX_Area * UnitWeight * TotalNos;
        }
        #region Properties
        public string GroupName { get; set; }
        public string PropertyName { get; set; }


        public double Breadth { get; set; }
        public double Depth { get; set; }
        public double Thickness { get; set; }
        public double Diameter { get; set; }

        //Chiranjit [2011 11 15]
        public double UnitWeight { get; set; }
        public int TotalNos { get; set; }
        public double Weight { get; set; }
        //{
        //    get
        //    {
        //        //if (AppliedSection == eAppliedSection.Steel)
        //        //    return Length * UnitWeight * TotalNos;
        //        //else
        //        //    return Length * AX_Area * UnitWeight * TotalNos;
        //        //return 0.0;
        //    }
        //}
        public double Length { get; set; }


         double ax1
        {
            get
            {
                double a = 0.0;

                if (Diameter != 0.0)
                {
                    a = (Math.PI * Diameter * Diameter / 4.0);
                }
                else
                {
                    a = Breadth * Depth;
                }

                return a;
            }
        }
         double ax2
         {
             get
             {
                 if (Thickness == 0.0) return 0.0;

                 double a = 0.0;

                 if (Diameter != 0.0)
                 {
                     a = (Math.PI * Math.Pow((Diameter - Thickness), 2.0) / 4.0);
                 }
                 else
                 {
                     a = (Breadth - 2 * Thickness) * (Depth - 2 * Thickness);
                 }

                 return a;
             }
         }
         public double AX_Area { get; set; }

        double ix1
        {
            get
            {
                double ix = 0.0;

                if (Diameter != 0.0)
                {
                    ix = Math.PI * Math.Pow(Diameter, 4.0) / 64.0;
                }
                else
                {
                    ix = Breadth * Math.Pow(Depth, 3.0) / 12.0;
                }

                return ix;
            }
        }
        double ix2
        {
            get
            {
                if (Thickness == 0.0) return 0.0;

                double ix = 0.0;

                if (Diameter != 0.0)
                {
                    ix = Math.PI * Math.Pow(Diameter - Thickness, 4.0) / 64.0;
                }
                else
                {
                    ix = (Breadth - Thickness * 2) * Math.Pow((Depth - Thickness * 2), 3.0) / 12.0;
                }

                return ix;
            }
        }
        /// <summary>
        /// Moment of Innertia about X-Direction
        /// </summary>
        public double IX { get; set; }


        double iy1
        {
            get
            {
                double iy = 0.0;

                if (Diameter != 0.0)
                {
                    iy = Math.PI * Math.Pow(Diameter, 4.0) / 64.0;
                }
                else
                {
                    iy = Depth * Math.Pow(Breadth, 3.0) / 12.0;
                }

                return iy;
            }
        }
        double iy2
        {
            get
            {
                if (Thickness == 0.0) return 0.0;

                double iy = 0.0;

                if (Diameter != 0.0)
                {
                    iy = Math.PI * Math.Pow(Diameter - Thickness, 4.0) / 64.0;
                }
                else
                {
                    iy = (Depth - Thickness * 2) * Math.Pow((Breadth - Thickness * 2), 3.0) / 12.0;
                }

                return iy;
            }
        }
        /// <summary>
        /// Moment of Innertia about Y-Direction
        /// </summary>
        public double IY { get; set; }
        
        /// <summary>
        /// Moment of Innertia about Z-Direction
        /// </summary>
        public double IZ
        {
            get
            {
                return (IX + IY);
            }
        }


        public SectionData SectionDetails { get; set; }

        public eAppliedSection AppliedSection { get; set; }
        #endregion Properties

        public override string ToString()
        {
            return string.Format("{0}   {1}    AX {2:f5}   IX {3:f5}   IY {4:f5}   IZ {5:f5}",
                GroupName, PropertyName, AX_Area, IX, IY, IZ);

            //return string.Format("{0}  {1}   AX 2.52 IX 0.9261 IY 0.3024 IZ 1.2285",
            //    GroupName, PropertyName, AX_Area, IX, IY, IZ);
        }

        public string ToString(string format)
        {
            format = "ALL";
            string kStr = "";
            if (format == "ALL")
            {
                kStr = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26}",
                    GroupName,       //0                    
                    (int)AppliedSection,       //1                    
                    SectionDetails.NoOfElements, //2
                    (SectionDetails.SectionName), //3
                    (SectionDetails.SectionCode), //4
                    //SectionDetails.SectionCode, //4
                    SectionDetails.AngleThickness, //5
                    SectionDetails.LateralSpacing, //6
                    SectionDetails.TopPlate.Width, //7
                    SectionDetails.TopPlate.Thickness, //8
                    SectionDetails.BottomPlate.Width, //9
                    SectionDetails.BottomPlate.Thickness, //10
                    SectionDetails.SidePlate.Width, //11
                    SectionDetails.SidePlate.Thickness, //12
                    SectionDetails.VerticalStiffenerPlate.Width, //13
                    SectionDetails.VerticalStiffenerPlate.Thickness, //14
                    Length, //15
                    Breadth, //16
                    Depth,  // 17
                    Diameter, //18
                    Thickness, //19
                    UnitWeight, //20
                    TotalNos, //21
                    AX_Area, //22
                    IX, //23
                    IY, //24
                    IZ, //25
                    Weight); //26
            }
            return kStr;

            //return string.Format("{0}  {1}   AX 2.52 IX 0.9261 IY 0.3024 IZ 1.2285",
            //    GroupName, PropertyName, AX_Area, IX, IY, IZ);
        }

        public object[] ToArray()
        {
            List<object> list = new List<object>();

            //list.Add(SectionDetails.SectionName);//1
            //list.Add(SectionDetails.SectionCode);//2
            //list.Add(SectionDetails.AngleThickness);//3
            //list.Add(SectionDetails.TopPlate.Width);//4
            //list.Add(SectionDetails.TopPlate.Thickness);//5
            //list.Add(SectionDetails.BottomPlate.Width);//6
            //list.Add(SectionDetails.BottomPlate.Thickness);//7
            //list.Add(SectionDetails.SidePlate.Width);//8
            //list.Add(SectionDetails.SidePlate.Thickness);//9
            //list.Add(SectionDetails.VerticalStiffenerPlate.Width);//10
            //list.Add(SectionDetails.VerticalStiffenerPlate.Thickness);//11
            list.Add(0);//12
            list.Add(GroupName);//12
            list.Add(Length);//12
            list.Add(Breadth);//13
            list.Add(Depth); // 14
            list.Add(Diameter);//15
            list.Add(Thickness);//16
            list.Add(UnitWeight);//17
            list.Add(TotalNos);//18
            list.Add(AX_Area.ToString("E3"));//19
            list.Add(IX.ToString("E3"));//20
            list.Add(IY.ToString("E3"));//21
            list.Add(IZ.ToString("E3"));//22
            list.Add(Weight.ToString("E3")); //23

            return list.ToArray();
        }
        public static MemberSectionProperty Parse(string txt)
        {
            MyList mlist = new MyList(MyList.RemoveAllSpaces(txt),',');
            if (!txt.Contains(","))
                mlist = new MyList(MyList.RemoveAllSpaces(txt), ' ');

            MemberSectionProperty msec = new MemberSectionProperty();
            try
            {
                int col_index = 0;
                msec.GroupName =  mlist.StringList[col_index++]; //0                    
                msec.AppliedSection = (eAppliedSection)mlist.GetInt(col_index++); //1                    
                msec.SectionDetails.NoOfElements = mlist.GetInt(col_index++); //2
                msec.SectionDetails.SectionName = mlist.StringList[col_index++]; //3
                msec.SectionDetails.SectionCode = mlist.StringList[col_index++]; //4
                msec.SectionDetails.AngleThickness = mlist.GetDouble(col_index++); //5
                msec.SectionDetails.LateralSpacing = mlist.GetDouble(col_index++); //6
                msec.SectionDetails.TopPlate.Width = mlist.GetDouble(col_index++);  //7
                msec.SectionDetails.TopPlate.Thickness = mlist.GetDouble(col_index++);  //8
                msec.SectionDetails.BottomPlate.Width = mlist.GetDouble(col_index++);  //9
                msec.SectionDetails.BottomPlate.Thickness = mlist.GetDouble(col_index++);  //10
                msec.SectionDetails.SidePlate.Width = mlist.GetDouble(col_index++);  //11
                msec.SectionDetails.SidePlate.Thickness = mlist.GetDouble(col_index++);  //12
                msec.SectionDetails.VerticalStiffenerPlate.Width = mlist.GetDouble(col_index++);  //13
                msec.SectionDetails.VerticalStiffenerPlate.Thickness = mlist.GetDouble(col_index++);  //14
                msec.Length = mlist.GetDouble(col_index++);  //15
                msec.Breadth = mlist.GetDouble(col_index++);  //16
                msec.Depth = mlist.GetDouble(col_index++);   // 17
                msec.Diameter = mlist.GetDouble(col_index++);  //18
                msec.Thickness = mlist.GetDouble(col_index++);  //19
                msec.UnitWeight = mlist.GetDouble(col_index++);  //20
                msec.TotalNos = mlist.GetInt(col_index++);  //21
                msec.AX_Area = mlist.GetDouble(col_index++);  //22
                msec.IX = mlist.GetDouble(col_index++);  //23
                msec.IY = mlist.GetDouble(col_index++);  //24
                //msec.IZ = mlist.GetDouble(22);  //25
                msec.Weight = mlist.GetDouble(++col_index);  //26
            }
            catch (Exception ex) { }

            return msec;
        }

    }

    [Serializable]
    public class MyList 
    {
        string _str = "";
        List<string> strList = null;
        char sp_char;
        public MyList(string s, char splitChar)
        {
            _str = s;
            strList = new List<string>(s.Split(new char[] { splitChar }));
            sp_char = splitChar;

            for (int i = 0; i < strList.Count; i++)
            {
                strList[i] = strList[i].Trim().TrimEnd().TrimStart();
            }
        }
        public static void Fill_List_to_Grid(DataGridView dgv, List<string> list, char splitchar)
        {
            MyList mlist = null;
            if (list.Count > 0)
            {
                dgv.Rows.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    mlist = new MyList(list[i], splitchar);
                    dgv.Rows.Add(mlist.StringList.ToArray());
                }
            }
        }


        public static void Modified_Cell(DataGridView dgv)
        {
            string s1, s2, s3, s4;
            int sl_no = 1;




            #region Format Input Data
            //dgv = dgv_base_pressure;

            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {
                    
                    if (dgv[0, i].Value == null) dgv[0, i].Value = "";
                    if (dgv[1, i].Value == null) dgv[1, i].Value = "";
                    if (dgv[2, i].Value == null) dgv[2, i].Value = "";

                    if (dgv[0, i].Value == "") dgv[0, i].Value = "";
                    if (dgv[1, i].Value == "") dgv[1, i].Value = "";
                    if (dgv[2, i].Value == "") dgv[2, i].Value = "";

                    //if (dgv_box_input_data[3, i].Value == "") dgv_box_input_data[3, i].Value = "";



                    s1 = dgv[0, i].Value.ToString();
                    s2 = dgv[1, i].Value.ToString();
                    s3 = dgv[2, i].Value.ToString();

                    if(dgv.ColumnCount == 4)
                    {

                        s2 = dgv[2, i].Value.ToString();
                        s3 = dgv[3, i].Value.ToString();
                    }
                    //s4 = dgv_box_input_data[3, i].Value.ToString();
                    if (s1 != "" && s2 == "")
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                        dgv.Rows[i].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.0f, System.Drawing.FontStyle.Bold);
                        sl_no = 1;

                        dgv.Rows[i].ReadOnly = true;
                    }
                    else if (s1 == "" && s2 == "" && s3 == "")
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Gray;
                        //dgv.Rows[i].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.0f, System.Drawing.FontStyle.Bold);
                        sl_no = 1;

                        dgv.Rows[i].ReadOnly = true;
                    }

                    //else
                    //{
                    //if (s2 != "") dgv[0, i].Value = sl_no++;
                    //}
                }
                catch (Exception exx) { }
            }

            #endregion Format Input Data

            //if (dgv[1, 0].Value == "") dgv[2, i].Value = "";

        }

        public static void Modified_Cell(DataGridView dgv, int value_index)
        {
            string s1, s2, s3, s4;
            int sl_no = 1;




            #region Format Input Data
            //dgv = dgv_base_pressure;

            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {

                    if (dgv[0, i].Value == null) dgv[0, i].Value = "";
                    if (dgv[1, i].Value == null) dgv[1, i].Value = "";
                    if (dgv[2, i].Value == null) dgv[2, i].Value = "";

                    if (dgv[0, i].Value == "") dgv[0, i].Value = "";
                    if (dgv[1, i].Value == "") dgv[1, i].Value = "";
                    if (dgv[2, i].Value == "") dgv[2, i].Value = "";

                    //if (dgv_box_input_data[3, i].Value == "") dgv_box_input_data[3, i].Value = "";



                    s1 = dgv[0, i].Value.ToString();
                    s2 = dgv[value_index, i].Value.ToString();
                    s3 = dgv[2, i].Value.ToString();
                    //s4 = dgv_box_input_data[3, i].Value.ToString();
                    if (s1 != "" && s2 == "")
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                        dgv.Rows[i].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.0f, System.Drawing.FontStyle.Bold);
                        sl_no = 1;

                        dgv.Rows[i].ReadOnly = true;
                    }
                    else if (s1 == "" && s2 == "" && s3 == "")
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Gray;
                        //dgv.Rows[i].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.0f, System.Drawing.FontStyle.Bold);
                        sl_no = 1;

                        dgv.Rows[i].ReadOnly = true;
                    }

                    //else
                    //{
                    //if (s2 != "") dgv[0, i].Value = sl_no++;
                    //}
                }
                catch (Exception exx) { }
            }

            #endregion Format Input Data

            //if (dgv[1, 0].Value == "") dgv[2, i].Value = "";

        }

        public Match Ger_All_Matches()
        {
            Regex regex = new Regex(@"\d+");
            //Match match = regex.Match("Dot 55 Perls");
            Match match = regex.Match(_str);
            //if (match.Success)
            //{
            //    Console.WriteLine(match.Value);
            //}


            MatchCollection mc = regex.Matches(_str);


            //mc[1]
            return match;

        }

        public static List<string> Sort_Int_Array(List<string> list)
        {
            if (list.Count == 0) return list;

            string kStr = "";
            Regex regex = new Regex(@"^\d+");


            kStr = list[0];

            Match match = regex.Match(kStr);

            if(!match.Success)
            {
                return Sort_Int_Array_2(list);
            }



            int ks = 0;

            Hashtable ht = new Hashtable();
            List<string> lst_1 = new List<string>();



            List<int> lst_int = new List<int>();
             

            foreach (var item in list)
            {
                match = regex.Match(item);

                if (match.Success)
                {
                    ks = MyList.StringToInt(match.Value, 0);

                    //lst_int.Add(MyList.StringToInt(item, 0));

                    lst_1 = ht[ks] as List<string>;
                    if (lst_1 == null)
                    {
                        lst_1 = new List<string>();
                        ht.Add(ks, lst_1);
                        lst_int.Add(ks);
                    }
                    lst_1.Add(item);
                }
            }

            lst_int.Sort();
            //list.Clear();

            List<string> lst_2 = new List<string>();
            foreach (var item in lst_int)
            {
                lst_1 = ht[item] as List<string>;
                if (lst_1 != null)
                {
                    foreach (var ite in lst_1)
                    {
                        list.Remove(ite);
                        lst_2.Add(ite);
                    }
                }
            }

            //return list;

            lst_2.AddRange(list.ToArray());

            //list.Clear();
            //list = lst_2;
            return lst_2;
        }


        public static List<string> Sort_Int_Array_2(List<string> list)
        {

            if (list.Count == 0) return list;

            string kStr = "";
            //Regex regex = new Regex(@"^\d+");
            Regex regex = new Regex(@"\d+$");



            Match match = regex.Match(kStr);


            //Regex regex1 = new Regex(@"(\d)\w+");
            Regex regex1 = new Regex(@"\d+");

            Match match1 = regex1.Match("X[10]");


            int ks = 0;

            Hashtable ht = new Hashtable();
            List<string> lst_1 = new List<string>();


            List<int> lst_int = new List<int>();


            Hashtable ht1 = new Hashtable();
            Hashtable ht2 = new Hashtable();
            Hashtable ht3 = new Hashtable();

            List<string> lst_str = new List<string>();
            List<string> lst_lbls = new List<string>();





            string st3 = "";
            foreach (var item in list)
            {
                match = regex.Match(item);

                if (match.Success)
                {
                    st3 = item.Remove(match.Index, match.Length);

                    ks = MyList.StringToInt(match.Value, 0);

                    lst_str = ht1[st3] as List<string>;
                    if (lst_str == null)
                    {
                        lst_str = new List<string>();
                        ht1.Add(st3, lst_str);
                        lst_lbls.Add(st3);
                    }
                    //else
                    lst_str.Add(item);
                }
                else
                {
                    if (item.Contains("[") && item.Contains("]"))
                    {
                        MyList ml = new MyList(item, '=');
                        kStr = ml[0];
                        match = regex1.Match(kStr);

                        MatchCollection mcl = regex1.Matches(kStr);

                        if(mcl.Count > 1)
                        {
                            match = mcl[mcl.Count - 1];
                        }
                        if (match.Success)
                        {
                            st3 = kStr.Remove(match.Index, match.Length);

                            ks = MyList.StringToInt(match.Value, 0);

                            lst_str = ht1[st3] as List<string>;
                            if (lst_str == null)
                            {
                                lst_str = new List<string>();
                                ht1.Add(st3, lst_str);
                                lst_lbls.Add(st3);
                            }
                            //else
                            //lst_str.Add(item);
                        }
                    }
                    else if (item.Contains("="))
                    {
                        MyList ml = new MyList(item, '=');
                        kStr = ml[0];
                        match = regex.Match(kStr);

                        if (match.Success)
                        {
                            st3 = kStr.Remove(match.Index, match.Length);

                            ks = MyList.StringToInt(match.Value, 0);

                            lst_str = ht1[st3] as List<string>;
                            if (lst_str == null)
                            {
                                lst_str = new List<string>();
                                ht1.Add(st3, lst_str);
                                lst_lbls.Add(st3);
                            }
                            //else
                            //lst_str.Add(item);
                        }
                    }
                    else
                    {
                        lst_str = ht1[item] as List<string>;
                        if (lst_str == null)
                        {
                            lst_str = new List<string>();
                            ht1.Add(item, lst_str);
                            lst_lbls.Add(item);
                        }
                    }
                    //else
                    lst_str.Add(item);

                }
            }



            //lst_str.Add(st3);


            List<string> lst_2 = new List<string>();


            lst_lbls.Sort();

            foreach (var item in lst_lbls)
            {
                lst_str = ht1[item] as List<string>;
                if(lst_str != null)
                {
                    lst_str = Sort_Int_Array_3(lst_str);
                    lst_2.AddRange(lst_str.ToArray());
                }
                
            }


            return lst_2;
        }


        public static List<string> Sort_Int_Array_3(List<string> list)
        {

            if (list.Count == 0) return list;

            string kStr = "";
            //Regex regex = new Regex(@"^\d+");
            Regex regex = new Regex(@"\d+");
            //Regex regex = new Regex(@"\d+$");



            Match match = regex.Match(kStr);

            int ks = 0;

            Hashtable ht = new Hashtable();
            List<string> lst_1 = new List<string>();



            List<int> lst_int = new List<int>();

            MatchCollection mcl = null;

            foreach (var item in list)
            {
                match = regex.Match(item);

                mcl = regex.Matches(item);

                if(mcl.Count > 1)
                {
                    match = mcl[mcl.Count - 1];

                }
                if (match.Success)
                {
                    ks = MyList.StringToInt(match.Value, 0);

                    //lst_int.Add(MyList.StringToInt(item, 0));

                    lst_1 = ht[ks] as List<string>;
                    if (lst_1 == null)
                    {
                        lst_1 = new List<string>();
                        ht.Add(ks, lst_1);
                        lst_int.Add(ks);
                    }
                    lst_1.Add(item);
                }
                else
                {
                    if(item.Contains("="))
                    {

                        MyList ml = new MyList(item, '=');
                        kStr = ml[0];
                        match = regex.Match(kStr);

                        if (match.Success)
                        {
                            ks = MyList.StringToInt(match.Value, 0);

                            //lst_int.Add(MyList.StringToInt(item, 0));

                            lst_1 = ht[ks] as List<string>;
                            if (lst_1 == null)
                            {
                                lst_1 = new List<string>();
                                ht.Add(ks, lst_1);
                                lst_int.Add(ks);
                            }
                            lst_1.Add(item);
                        }

                    }
                }
            }

            lst_int.Sort();
            //list.Clear();

            List<string> lst_2 = new List<string>();
            foreach (var item in lst_int)
            {
                lst_1 = ht[item] as List<string>;
                if (lst_1 != null)
                {
                    foreach (var ite in lst_1)
                    {
                        list.Remove(ite);
                        lst_2.Add(ite);
                    }
                }
            }

            //return list;

            lst_2.AddRange(list.ToArray());

            //list.Clear();
            //list = lst_2;
            return lst_2;
        }

        public static void Delete_Folder(string folder)
        {
            try
            {
                if (Directory.Exists(folder))
                {
                    foreach (var item in Directory.GetDirectories(folder))
                    {
                        Delete_Folder(item);
                    }
                    foreach (var item in Directory.GetFiles(folder))
                    {
                        File.Delete(item);
                    }
                    Directory.Delete(folder);
                }
            }
            catch (Exception exx) { }
        }


        public static string Get_Project_Name( string dir)
        {
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

            return prj_name;

        }

        public static List<string> Get_List_from_Grid(DataGridView dgv, char splitchar)
        {
            List<string> list = new List<string>();
            string kStr = "";
            try
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    kStr = dgv[0, i].Value.ToString();
                    for (int c = 1; c < dgv.ColumnCount; c++)
                    {
                        if (dgv[c, i].Value == null)
                            dgv[c, i].Value = "";
                        kStr = kStr + splitchar + dgv[c, i].Value.ToString();
                    }
                    list.Add(kStr);
                }
            }
            catch (Exception exx) { }
            return list;
        }

        public static double Get_Expression_Result(string exp)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                string res = exp.ToLower().Replace("x", "*").Trim();

                res = res.ToLower().Replace("*", " * ").Trim();
                res = res.ToLower().Replace("+", " + ").Trim();
                res = res.ToLower().Replace("/", " / ").Trim();
                res = res.ToLower().Replace("-", " - ").Trim();

                var v = dt.Compute(res, "");
                return (double.Parse(v.ToString()));
            }
            catch (Exception exx) { }
            return 0.0;
        }
        public MyList(List<string> lstStr)
        {
            strList = new List<string>();
            strList = lstStr;
        }
        public MyList(MyList obj)
        {
            strList = new List<string>(obj.StringList);
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
        public bool IsDouble(int index)
        {
            try
            {
                double d = double.Parse(strList[index]);
                return true;
            }
            catch (Exception exx) { }
            return false;
        }
        public bool IsInt(int index)
        {
            try
            {
                int v = int.Parse(strList[index]);
                return true;
            }
            catch (Exception exx) { }
            return false;
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
        public static double StringToDouble(TextBox txt, double defaultValue)
        {
            return StringToDouble(txt.Text, defaultValue);
        }
        public static double StringToDouble(TextBox txt)
        {
            return StringToDouble(txt.Text, 0.0);
        }
        public static double StringToDouble(string txt)
        {
            return StringToDouble(txt, 0.0);
        }

        public static double StringToDouble(string s, double defaultValue)
        {
            //if (s == null) return defaultValue;
            if (s == "") 
                return defaultValue;
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
            {s = s.Replace('\t', ' ').TrimStart().TrimEnd().Trim();
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

        public string GetString(int fromIndex, int toIndex)
        {
            string kStr = "";
            for (int i = fromIndex; i <= toIndex; i++)
            {
                kStr += strList[i] + sp_char;
            }
            return kStr;
        }
        public string GetString(int fromIndex, string findText)
        {
            int toIndex = strList.IndexOf(findText);

            string kStr = "";
            for (int i = fromIndex; i <= toIndex; i++)
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


            if (list_int.Count == 0)
                return "";


            int val = 0;

            List<int> temp_list = new List<int>();
            for (int i = 0; i < list_int.Count; i++)
            {
                if(!temp_list.Contains(list_int[i]))
                    temp_list.Add(list_int[i]);
            }
             
            list_int = temp_list;


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

                    if ((end_mbr_no - start_mbr_no) == 1)
                        kStr += " " + start_mbr_no + "   " + end_mbr_no;
                    else if (end_mbr_no != start_mbr_no)
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


            if ((end_mbr_no - start_mbr_no) == 1)
                kStr += "   " + start_mbr_no + "   " + end_mbr_no;
            else if (end_mbr_no != start_mbr_no)
                kStr += "   " + start_mbr_no + " TO " + (end_mbr_no);
            else
                kStr += "   " + start_mbr_no;
            

            return RemoveAllSpaces(kStr);
        }
        public static  List<int> Get_Array_Intiger(string list_text)
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
        public double SUM
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < Count; i++)
                {
                    try
                    {
                        sum += GetDouble(i);
                    }
                    catch (Exception ex) { }
                }

                return sum;
            }
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
        //Chiranjit [2013 07 14]
        public static double Get_Array_Sum(List<double> list)
        {
            double val = 0.0;
            for (int i = 0; i < list.Count; i++)
            {
                val += list[i];
            }
            return val;

        }
        //Chiranjit [2013 07 14]
        public static void Array_Multiply_With(ref List<double> list, double m_val)
        {
            double val = 0.0;
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = list[i] * m_val;
            }
        }
        //Chiranjit [2013 07 14]
        public static void Array_Format_With(ref List<double> list, string format)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = double.Parse(list[i].ToString(format));
            }
        }

        public static string Get_Analysis_Report_File(string input_path)
        {
            if (!File.Exists(input_path)) return "";
            return Path.Combine(Path.GetDirectoryName(input_path), "ANALYSIS_REP.TXT");
        }
        public static string Get_LL_TXT_File(string input_path)
        {
            if (!File.Exists(input_path)) return "";
            return Path.Combine(Path.GetDirectoryName(input_path), "LL.TXT");
        }

        public static implicit operator MyList(string rhs)
        {
            MyList c = new MyList(rhs,' '); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(MyList rhs)
        {
            return rhs.ToString();
        }

        public static void Folder_Copy(string src_fold, string dest_fold)
        {
            if (!Directory.Exists(src_fold)) return;

            if (!Directory.Exists(dest_fold)) Directory.CreateDirectory(dest_fold);

            string dest_file = "";
            foreach (var item in Directory.GetFiles(src_fold))
            {
                dest_file = Path.Combine(dest_fold, Path.GetFileName(item));
                File.Copy(item, dest_file, true);
            }

            foreach (var item in Directory.GetDirectories(src_fold))
            {
                dest_file = Path.Combine(dest_fold, Path.GetFileName(item));
                Folder_Copy(item, dest_file);
            }
        }
        public static List<TextBox> Get_TextBoxes(Control ctrl)
        {
            List<TextBox> list = new List<TextBox>();
            for (int i = 0; i < ctrl.Controls.Count; i++)
            {
                var c = ctrl.Controls[i];
                if (c.Controls.Count > 0)
                {
                    list.AddRange(Get_TextBoxes(c));
                }
                if (c is TextBox)
                {
                    list.Add(c as TextBox);
                }
            }
            return list;
        }

        public static List<TextBox> Get_TextBoxes(Control ctrl, string startsWith)
        {
            List<TextBox> list = new List<TextBox>();
            for (int i = 0; i < ctrl.Controls.Count; i++)
            {
                var c = ctrl.Controls[i];
                if (c.Controls.Count > 0)
                {
                    list.AddRange(Get_TextBoxes(c));
                }
                if (c is TextBox)
                {
                    if (c.Name.StartsWith(startsWith))
                        list.Add(c as TextBox);
                }
            }
            return list;
        }
        public static List<TextBox> Get_TextBoxes(Form frm)
        {
            List<TextBox> list = new List<TextBox>();
            for (int i = 0; i < frm.Controls.Count; i++)
            {
                var c = frm.Controls[i];
                list.AddRange(Get_TextBoxes(c));
            }
            return list;
        }

        public static List<TextBox> Get_TextBoxes(Form frm, string startWith)
        {
            List<TextBox> list = new List<TextBox>();
            for (int i = 0; i < frm.Controls.Count; i++)
            {
                var c = frm.Controls[i];
                list.AddRange(Get_TextBoxes(c, startWith));
            }
            return list;
        }
        public static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

    }

    /// <summary>
    /// This is a SYMBOL class contains special symbols like ALPHA, BETA, DELTA etc
    /// ° 
    /// θ
    /// α
    /// π
    /// √
    /// δ
    /// τ
    /// γ
    /// √
    /// ±
    /// ≠
    /// ≤
    /// ≥
    /// ÷
    /// ∞
    /// µ
    /// β
    /// ∑
    /// ɸ
    /// ϒ
    /// η
    /// </summary>
    public sealed class SYMBOLS
    {
        //° θαπ√δτγ√±≠≤≥÷∞µβ∑ɸϒη
        /// <summary>
        /// DEGREE = "°";
        /// </summary>
        public const string DEGREE = "°";
        /// <summary>
        /// THETA = "θ";
        /// </summary>
        public const string THETA = "θ";
        /// <summary>
        /// ALPHA = "α";
        /// </summary>
        public const string ALPHA = "α";
        /// <summary>
        /// PI = "π";
        /// </summary>
        public const string PI = "π";
        /// <summary>
        /// SQRT = "√";
        /// </summary>
        public const string SQRT = "√";

        /// <summary>
        /// SQAURE = "²";
        /// </summary>
        public const string SQAURE = "²";
        /// <summary>
        /// CUBE = "³";
        /// </summary>
        public const string CUBE = "³";
        /// <summary>
        /// DELTA = "δ";
        /// </summary>
        public const string DELTA = "δ";
        /// <summary>
        /// TAU = "τ";
        /// </summary>
        public const string TAU = "τ";
        /// <summary>
        /// GAMMA = "γ";
        /// </summary>
        public const string GAMMA = "γ";
        /// <summary>
        /// SQUAREROOT = "√";
        /// </summary>
        public const string SQUAREROOT = "√";
        /// <summary>
        /// PLUS_MINUS = "±";
        /// </summary>
        public const string PLUS_MINUS = "±";
        /// <summary>
        /// NOT_EQUALTO = "≠";
        /// </summary>
        public const string NOT_EQUALTO = "≠";
        /// <summary>
        /// LESS_THAN_EQUAL = "≤";
        /// </summary>
        public const string LESS_THAN_EQUAL = "≤";
        /// <summary>
        /// GREATER_THAN_EQUAL = "≥";
        /// </summary>
        public const string GREATER_THAN_EQUAL = "≥";
        /// <summary>
        /// DIVISION = "÷";
        /// </summary>
        public const string DIVISION = "÷";
        /// <summary>
        /// INFINITY = "∞";
        /// </summary>
        public const string INFINITY = "∞";
        /// <summary>
        /// 
        /// </summary>
        public const string MU = "µ";
        /// <summary>
        /// BETA = "β";
        /// </summary>
        public const string BETA = "β";
        /// <summary>
        /// SIGMA_SUM = "∑";
        /// </summary>
        public const string SIGMA_SUM = "∑";
        //public const string PHI = "ɸ";
        /// <summary>
        /// GAMA = "ϒ";
        /// </summary>
        public const string GAMA = "ϒ";

        /// <summary>
        /// ETA = "η";
        /// </summary>
        public const string ETA = "η";
        /// <summary>
        /// LAMDA = "λ";
        /// </summary>
        public const string LAMDA = "λ";
        /// <summary>
        /// XI = "ξ";
        /// </summary>
        public const string XI = "ξ";
        /// <summary>
        /// RHO = "ρ";
        /// </summary>
        public const string RHO = "ρ";
        /// <summary>
        /// SIGMA = "σ";
        /// </summary>
        public const string SIGMA = "σ";

        /// <summary>
        /// PHI = "φ";
        /// </summary>
        public const string PHI = "φ";
        /// <summary>
        /// PSI = "ψ";
        /// </summary>
        public const string PSI = "ψ";
        /// <summary>
        /// OMWGA = "ω";
        /// </summary>
        public const string OMWGA = "ω";
        /// <summary>
        /// INVERSE = "⁻¹";
        /// </summary>
        public const string INVERSE = "⁻¹"; 
    }
    public class BridgeMemberAnalysis
    {
        Thread thd = null;
        IApplication iApp = null;
        public delegate void SetProgressValue(ProgressBar pbr, int val);

        SetProgressValue spv;

        ProgressBar pbr;
        List<AnalysisData> list_analysis = null;
        List<AstraBeamMember> list_beams = null;
        List<AstraSolidMember> list_solids = null;
        List<AstraTrussMember> list_trusses = null;
        List<AstraTrussMember> list_cables = null;
        List<AstraPlateMember> list_plates = null;

        public bool IsCurve = false;

        public List<AnalysisData> Analysis_Forces
        {
            get
            {
                return list_analysis;
            }
        }

        public List<AstraPlateMember> Plates_Forces
        {
            get
            {
                return list_plates;
            }
        }
        public List<AstraSolidMember> Solids_Forces
        {
            get
            {
                return list_solids;
            }
        }
        public List<AstraBeamMember> Beams_Forces
        {
            get
            {
                return list_beams;
            }
        }
        public List<AstraTrussMember> Trusses_Forces
        {
            get
            {
                return list_trusses;
            }
        }

        public List<AstraTrussMember> Cables_Forces
        {
            get
            {
                return list_cables;
            }
        }

        List<int> list_mem_no = null;
        public Hashtable hash_table = null;

        public List<AstraMember> ASTRA_USER_Member_table { get; set; }



        CBridgeStructure structure = null;
        public ReadForceType ForceType { get; set; }


        //Chiranjit [2013 06 27]
        public NodeResults Node_Displacements { get; set; }
        public NodeResults Eigen_Displacements { get; set; }
        //N O D E   D I S P L A C E M E N T S / R O T A T I O N S

        public List<int> Get_LoadCases()
        {
            List<int> lcs = new List<int>();

            if (Node_Displacements == null) return lcs;

            if (Node_Displacements.Count > 0)
            {
                for (int i = 0; i < Node_Displacements.Count; i++)
                {
                    if (!lcs.Contains(Node_Displacements[i].LoadCase))
                        lcs.Add(Node_Displacements[i].LoadCase);
                }
            }

            return lcs;

            #region Chiranjit [2014 12 12]

            //if (Beams_Forces.Count > 0)
            //{
            //    int mno = Beams_Forces[0].BeamNo;

            //    for (int i = 0; i < Beams_Forces.Count; i++)
            //    {
            //        if (Beams_Forces[i].BeamNo == mno)
            //            lcs.Add(Beams_Forces[i].LoadNo);
            //    }
            //}
            //else if (Trusses_Forces.Count > 0)
            //{
            //    int mno = Trusses_Forces[0].TrussMemberNo;

            //    for (int i = 0; i < Trusses_Forces.Count; i++)
            //    {
            //        if (Trusses_Forces[i].TrussMemberNo == mno)
            //            lcs.Add(Trusses_Forces[i].LoadNo);
            //    }
            //}
            //else if (Cables_Forces.Count > 0)
            //{
            //    int mno = Cables_Forces[0].TrussMemberNo;

            //    for (int i = 0; i < Cables_Forces.Count; i++)
            //    {
            //        if (Cables_Forces[i].TrussMemberNo == mno)
            //            lcs.Add(Cables_Forces[i].LoadNo);
            //    }
            //}
            //return lcs;
            #endregion Chiranjit [2014 12 12]
        }

        //public SteelTrussMemberAnalysis(string analysis_file)
        //{
        //    list_mem_no = new List<int>();
        //    //this.pbr = this_pbr;

        //    Analysis_File = analysis_file;
        //    if (ReadFromFile())
        //    {
        //        truss_analysis = new CTrussAnalysis(Analysis_File);


        //        for (int i = 0; i < truss_analysis.Members.Count; i++)
        //        {

        //            for (int j = 0; j < list_beams.Count; j++)
        //            {
        //                if (list_beams[j].BeamNo == truss_analysis.Members[i].MemberNo)
        //                {
        //                    list_beams[j].EndNodeForce.JointNo = truss_analysis.Members[i].EndNode.NodeNo;
        //                    list_beams[j].StartNodeForce.JointNo = truss_analysis.Members[i].StartNode.NodeNo;
        //                }
        //            }
        //        }
        //        SetAnalysisData();
        //    }
        //}

        public AstraMember Get_User_Member_Number(int astra_mem_no, eAstraMemberType memType)
        {

            //if (ASTRA_USER_Member_table.Count == 0)
            //{
            //    foreach (var item in this.Beams_Forces)
            //    {
            //        AstraMember amr = new AstraMember();

            //        amr.AstraMemberNo
            //        ASTRA_USER_Member_table.Add()
		 
            //    }
            foreach (var item in ASTRA_USER_Member_table)
            {
                if (item.AstraMemberNo == astra_mem_no && item.AstraMemberType == memType)
                    return item;
            }
            //return null;

            //2017 02 17
            AstraMember ambr = new AstraMember();

            ambr.AstraMemberNo = astra_mem_no;
            ambr.UserNo = astra_mem_no;
            ambr.AstraMemberType = memType;

            return ambr;
            //this.Trusses_Forces

        }
        public AstraMember Get_ASTRA_Member_Number(int user_mem_no)
        {
            foreach (var item in ASTRA_USER_Member_table)
            {
                if (item.UserNo == user_mem_no)
                    return item;
            }
            return null;
        }
        public AstraMember Get_User_Member_Number(int astra_mem_no)
        {
            if (ASTRA_USER_Member_table.Count == 0)
            {
                var am = new AstraMember();
                am.AstraMemberNo = astra_mem_no;
                am.UserNo = astra_mem_no;
                return am;
            }
            foreach (var item in ASTRA_USER_Member_table)
            {
                if (item.AstraMemberNo == astra_mem_no && item.AstraMemberType == eAstraMemberType.BEAM)
                    return item;
            }
            return null;
        }
        public BridgeMemberAnalysis(IApplication app, string analysis_file, ReadForceType rft)
        {
            ForceType = rft;

            iApp = app;

            list_mem_no = new List<int>();
            //this.pbr = this_pbr;

            Analysis_File = analysis_file;

            try
            {

                if (File.Exists(analysis_file) && ReadFromFile())
                {
                    structure = new CBridgeStructure(Analysis_File);


                    #region Chiranjit [2011 10 30] Calculate the Effective Depth and Cantilever Width

                    JointNodeCollection jn_col = structure.Joints;


                    double supp_x_coor = structure.Supports.Count > 0 ? structure.Supports[0].X : 0.0;

                    supp_x_coor = (supp_x_coor - Math.Tan(structure.Skew_Angle * Math.PI / 180.0) * structure.Supports[0].Z);


                    supp_x_coor = MyList.StringToDouble(supp_x_coor.ToString("0.000"), 0.0);

                    structure.Support_Distance = supp_x_coor;

                    List<double> _X_joints = new List<double>();
                    List<double> _Z_joints = new List<double>();

                    for (int i = 0; i < jn_col.Count; i++)
                    {
                        //if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                        if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);
                    }

                    _X_joints.Clear();


                    for (int i = 0; i < jn_col.Count; i++)
                    {
                        if (_Z_joints[0] == jn_col[i].Z)
                        {
                            _X_joints.Add(jn_col[i].X);
                        }
                    }


                    try
                    {
                        structure.Effective_Depth = _X_joints.Count > 1 ? _X_joints[_X_joints.IndexOf(supp_x_coor) + 1] : 0.0;
                        structure.Width_Cantilever = _Z_joints.Count > 1 ? _Z_joints[2] : 0.0;
                    }
                    catch (Exception ex)
                    {
                        structure.Effective_Depth = 0.0;
                        structure.Width_Cantilever = 0.0;
                    }



                    #endregion Chiranjit [2011 10 30] Calculate the Effective Depth and Cantilever Width


                    //iApp.Progress_ON("Reading members...");
                    for (int i = 0; i < structure.Members.Count; i++)
                    {

                        for (int j = 0; j < list_beams.Count; j++)
                        {
                            if (list_beams[j].BeamNo == structure.Members[i].MemberNo)
                            {
                                list_beams[j].EndNodeForce.JointNo = structure.Members[i].EndNode.NodeNo;
                                list_beams[j].StartNodeForce.JointNo = structure.Members[i].StartNode.NodeNo;
                            }
                        }
                        //iApp.Progress_ON("Reading members...");
                        //iApp.SetProgressValue(i, structure.Members.Count);
                        //Chiranjit [2013 05 15]
                        if (iApp.Is_Progress_Cancel) break;
                    }
                    //iApp.Progress_OFF();
                    SetAnalysisData();
                }

            }
            catch (ThreadAbortException ex1) { Thread.ResetAbort(); }
            catch (Exception ex) { }
        }
        public BridgeMemberAnalysis(IApplication app, string analysis_file)
        {
            ForceType = new ReadForceType();
            ForceType.M2 = true;
            ForceType.M3 = true;
            ForceType.R3 = true;
            ForceType.R2 = true;

            iApp = app;

            list_mem_no = new List<int>();
            //this.pbr = this_pbr;

            Analysis_File = analysis_file;
            //MessageBox.Show("7.0");

            try
            {

                if (File.Exists(analysis_file) && ReadFromFile())
                {
                    //MessageBox.Show("7.1");
                    structure = new CBridgeStructure(Analysis_File);
                    //MessageBox.Show("7.2");

                    #region Chiranjit [2011 10 30] Calculate the Effective Depth and Cantilever Width

                    JointNodeCollection jn_col = structure.Joints;


                    try
                    {
                        double supp_x_coor = structure.Supports.Count > 0 ? structure.Supports[0].X : 0.0;
                        if (structure.Supports.Count > 0)
                        {
                            supp_x_coor = (supp_x_coor - Math.Tan(structure.Skew_Angle * Math.PI / 180.0) * structure.Supports[0].Z);


                            supp_x_coor = MyList.StringToDouble(supp_x_coor.ToString("0.000"), 0.0);

                            structure.Support_Distance = supp_x_coor;

                            List<double> _X_joints = new List<double>();
                            List<double> _Z_joints = new List<double>();

                            for (int i = 0; i < jn_col.Count; i++)
                            {
                                //if (_X_joints.Contains(jn_col[i].X) == false) _X_joints.Add(jn_col[i].X);
                                if (_Z_joints.Contains(jn_col[i].Z) == false) _Z_joints.Add(jn_col[i].Z);
                            }

                            _X_joints.Clear();


                            for (int i = 0; i < jn_col.Count; i++)
                            {
                                if (_Z_joints[0] == jn_col[i].Z)
                                {
                                    _X_joints.Add(jn_col[i].X);
                                }
                            }


                            structure.Effective_Depth = _X_joints.Count > 1 ? _X_joints[_X_joints.IndexOf(supp_x_coor) + 1] : 0.0;
                            structure.Width_Cantilever = _Z_joints.Count > 1 ? _Z_joints[2] : 0.0;
                        }
                    }
                    catch (Exception ex)
                    {
                        structure.Effective_Depth = 0.0;
                        structure.Width_Cantilever = 0.0;
                    }

                    //Chiranjit [2013 05 03]

                    #endregion Chiranjit [2011 10 30] Calculate the Effective Depth and Cantilever Width
                    try
                    {
                        //MessageBox.Show("7.3");
                        //iApp.Progress_ON("Reading members...");

                        for (int i = 0; i < structure.Members.Count; i++)
                        {

                            for (int j = 0; j < list_beams.Count; j++)
                            {

                                var dd = Get_User_Member_Number(list_beams[j].BeamNo);
                                
                                //if (list_beams[j].BeamNo == structure.Members[i].MemberNo)
                                if (dd.UserNo == structure.Members[i].MemberNo)
                                {
                                    list_beams[j].EndNodeForce.JointNo = structure.Members[i].EndNode.NodeNo;
                                    list_beams[j].StartNodeForce.JointNo = structure.Members[i].StartNode.NodeNo;
                                }
                            }
                            //iApp.Progress_ON("Reading members...");
                            //iApp.SetProgressValue(i, structure.Members.Count);
                            if (iApp.Is_Progress_Cancel) break;
                        }
                        //MessageBox.Show("7.4");
                        //iApp.Progress_OFF();
                        SetAnalysisData();
                    }
                    catch (Exception ex) { }
                    //MessageBox.Show("7.5");
                }
            }
            catch (ThreadAbortException ex1) { Thread.ResetAbort(); }
            catch (Exception ex2) { }
        }

        public BridgeMemberAnalysis(string analysis_file, ProgressBar this_pbr)
        {
            list_mem_no = new List<int>();
            this.pbr = this_pbr;

            Analysis_File = analysis_file;
            spv = new SetProgressValue(SetProgressBar);
            //RunThread();
        }
        public string Analysis_File { get; set; }

        public CBridgeStructure Analysis
        {
            get
            {
                return structure;
            }
        }
        bool ReadFromFile()
        {
            if (!File.Exists(Analysis_File)) return false;
            List<string> file_content = new List<string>(File.ReadAllLines(Analysis_File));
            //bool IsRun_ProgressBar = file_content.Count > 999;
            bool IsRun_ProgressBar = true;
            try
            {
                bool mem_no_flag = false;
                bool truss_force_flag = false;
                bool cable_force_flag = false;
                bool beam_force_flag = false;
                bool plate_force_flag = false;
                bool solid_force_flag = false;

                bool node_displace_flag = false; //Chiranjit [2013 06 27]
                bool eigen_displace_flag = false; //Chiranjit [2014 11 24]

                

                string kStr = "";
                hash_table = new Hashtable();
                ASTRA_USER_Member_table = new List<AstraMember>();
                AstraMember ast_mem = new AstraMember();
                MyList mList = null;

                list_trusses = new List<AstraTrussMember>();
                list_cables = new List<AstraTrussMember>();
                list_beams = new List<AstraBeamMember>();
                list_solids = new List<AstraSolidMember>();
                list_plates = new List<AstraPlateMember>();

                AstraBeamMember beam_member = null;
                AstraTrussMember truss_member = null;
                AstraPlateMember plate_member = null;




                if (IsRun_ProgressBar)
                {
                    if (iApp != null)
                    {
                        if (Path.GetFileNameWithoutExtension(Analysis_File).ToUpper() == "ANALYSIS_REP")
                        {
                            iApp.Progress_ON("Reading Analysis Report...");

                        }
                        else
                        {
                            //iApp.Progress_ON("Reading Analysis Inputs...");
                            IsRun_ProgressBar = false;
                        }
                    }
                }
                else
                    iApp.Progress_Works.Next();

                for (int i = 0; i < file_content.Count; i++)
                {
                    kStr = file_content[i].ToUpper();
                    kStr = kStr.Replace(',', ' ');
                    kStr = MyList.RemoveAllSpaces(kStr);
                    kStr = kStr.Replace("MEMBER NUMBER", " ");

                    //if (i == 22783)
                    //    kStr = kStr.Replace(',', ' ');


                    kStr = MyList.RemoveAllSpaces(kStr);
                    if (kStr == "") continue;

                    if (kStr.Contains("END OF USER'S DATA"))
                    {
                        mem_no_flag = true;
                    }
                    if (kStr.Contains("::") && mem_no_flag)
                    {
                        mem_no_flag = true;
                        mList = new MyList(kStr, ' ');
                        ast_mem = new AstraMember();
                        //0          1     2   3   4    5    6
                        //TRUSS:: User's TRUSS 1 ASTRA TRUSS 1
                        if (kStr.Contains("TRUSS/CABLE"))
                        {
                            ast_mem.AstraMemberType = eAstraMemberType.CABLE;
                            ast_mem.UserNo = mList.GetInt(3);
                            ast_mem.AstraMemberNo = mList.GetInt(3);
                            ast_mem.AstraMemberNo = mList.GetInt(6);
                            hash_table.Add(ast_mem.UserNo, ast_mem);
                            ASTRA_USER_Member_table.Add(ast_mem);
                        }
                        else if (kStr.StartsWith("TRUSS"))
                        {
                            ast_mem.AstraMemberType = eAstraMemberType.TRUSS;
                            ast_mem.UserNo = mList.GetInt(3);
                            ast_mem.AstraMemberNo = mList.GetInt(3);
                            ast_mem.AstraMemberNo = mList.GetInt(6);
                            hash_table.Add(ast_mem.UserNo, ast_mem);

                            ASTRA_USER_Member_table.Add(ast_mem);
                        }
                        else if (kStr.StartsWith("CABLE")) // Chiranjit [2011 11 12] this is a new Item for ASTRA Members
                        {
                            try
                            {
                                ast_mem.AstraMemberType = eAstraMemberType.CABLE;
                                ast_mem.UserNo = mList.GetInt(3);
                                ast_mem.AstraMemberNo = mList.GetInt(3);
                                ast_mem.AstraMemberNo = mList.GetInt(6);
                                hash_table.Add(ast_mem.UserNo, ast_mem);
                                ASTRA_USER_Member_table.Add(ast_mem);
                            }
                            catch (Exception ex) { }
                        }
                        else if (kStr.StartsWith("BEAM"))
                        {
                            ast_mem.AstraMemberType = eAstraMemberType.BEAM;
                            ast_mem.UserNo = mList.GetInt(4);
                            ast_mem.AstraMemberNo = mList.GetInt(7);
                            hash_table.Add(ast_mem.UserNo, ast_mem);
                            ASTRA_USER_Member_table.Add(ast_mem);
                        }
                        list_mem_no.Add(ast_mem.UserNo);

                    }
                    else if (kStr.Contains("******************************") && mem_no_flag)
                    {
                        mem_no_flag = false;
                    }
                    else if (kStr.Contains("TRUSS MEMBER ACTIONS"))
                    {
                        truss_force_flag = true;
                        cable_force_flag = false;
                        beam_force_flag = false;
                        plate_force_flag = false;
                        node_displace_flag = false;//Chiranjit [2013 06 27]
                        solid_force_flag = false;
                    }
                    else if (kStr.Contains("CABLE MEMBER ACTIONS"))
                    {
                        cable_force_flag = true;
                        truss_force_flag = false;
                        beam_force_flag = false;
                        plate_force_flag = false;
                        node_displace_flag = false;//Chiranjit [2013 06 27]
                        solid_force_flag = false;
                    }
                    else if (kStr.Contains(".....BEAM FORCES AND MOMENTS"))
                    {
                        beam_force_flag = true;
                        truss_force_flag = false;
                        cable_force_flag = false;
                        plate_force_flag = false;
                        node_displace_flag = false;//Chiranjit [2013 06 27]
                        solid_force_flag = false;
                    }
                    else if (kStr.Contains("N O D E D I S P L A C E M E N T S / R O T A T I O N S"))
                    {


                        kStr = file_content[i+3].ToUpper();
                        kStr = kStr.Replace(',', ' ');
                        kStr = MyList.RemoveAllSpaces(kStr);

                        if (kStr.Contains("EIGEN"))
                        {
                            eigen_displace_flag = true;
                        }
                        else
                            eigen_displace_flag = false;

                        beam_force_flag = false;
                        truss_force_flag = false;
                        cable_force_flag = false;
                        plate_force_flag = false;
                        node_displace_flag = !eigen_displace_flag; //Chiranjit [2013 06 27]
                        solid_force_flag = false;
                        continue;
                    }

                    else if (kStr.Contains("SHELL ELEMENT STRESSES"))
                    {
                        beam_force_flag = false;
                        truss_force_flag = false;
                        cable_force_flag = false;
                        plate_force_flag = true;
                        node_displace_flag = false;
                        solid_force_flag = false;
                    }
                    else if (kStr.Contains("8-NODE SOLID ELEMENT STRESSES")) // chiranjit [2014 12 02]
                    {
                        beam_force_flag = false;
                        truss_force_flag = false;
                        cable_force_flag = false;
                        plate_force_flag = false;
                        node_displace_flag = false;
                        solid_force_flag = true;
                    }



                    if (truss_force_flag)
                    {
                        try
                        {
                            list_trusses.Add(AstraTrussMember.Parse(kStr));
                        }
                        catch (Exception ex) { }
                    }
                    if (cable_force_flag)
                    {
                        try
                        {
                            list_cables.Add(AstraTrussMember.Parse(kStr));
                        }
                        catch (Exception ex) { }
                    }
                    if (plate_force_flag)
                    {
                        try
                        {
                            list_plates.Add(AstraPlateMember.Parse(kStr));
                        }
                        catch (Exception ex) { }
                    }
                    if (solid_force_flag)
                    {
                        try
                        {
                            list_solids.Add(AstraSolidMember.Parse(kStr));
                        }
                        catch (Exception ex) { }
                    }
                    if (beam_force_flag)
                    {
                        try
                        {
                            mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                            if (mList.Count == 8)
                            {
                                beam_member = new AstraBeamMember(ForceType);
                                beam_member.BeamNo = mList.GetInt(0);
                                beam_member.LoadNo = mList.GetInt(1);
                                beam_member.StartNodeForce = BeamMemberForce.Parse(kStr);
                                beam_member.StartNodeForce.ForceType = ForceType;
                            }
                            else if (mList.Count == 6)
                            {
                                beam_member.EndNodeForce = BeamMemberForce.Parse(kStr);
                                beam_member.EndNodeForce.ForceType = ForceType;
                                list_beams.Add(beam_member);
                            }
                        }
                        catch (Exception ex) { }
                    }

                    if (node_displace_flag)
                    {
                        try
                        {
                            mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                            if (mList.Count == 8)
                            {
                                int cols = 0;
                                NodeResultData nrd = new NodeResultData();
                                nrd.NodeNo = mList.GetInt(cols++);
                                nrd.LoadCase = mList.GetInt(cols++);
                                nrd.X_Translation = mList.GetDouble(cols++);
                                nrd.Y_Translation = mList.GetDouble(cols++);
                                nrd.Z_Translation = mList.GetDouble(cols++);
                                nrd.X_Rotation = mList.GetDouble(cols++);
                                nrd.Y_Rotation = mList.GetDouble(cols++);
                                nrd.Z_Rotation = mList.GetDouble(cols++);

                                if (Node_Displacements == null)
                                    Node_Displacements = new NodeResults();

                                Node_Displacements.Add(nrd);
                            }
                            else if (mList.Count == 7)
                            {

                                int cols = 0;
                                NodeResultData nrd = new NodeResultData();
                                nrd.NodeNo = Node_Displacements[Node_Displacements.Count - 1].NodeNo;

                                nrd.LoadCase = mList.GetInt(cols++);
                                nrd.X_Translation = mList.GetDouble(cols++);
                                nrd.Y_Translation = mList.GetDouble(cols++);
                                nrd.Z_Translation = mList.GetDouble(cols++);
                                nrd.X_Rotation = mList.GetDouble(cols++);
                                nrd.Y_Rotation = mList.GetDouble(cols++);
                                nrd.Z_Rotation = mList.GetDouble(cols++);
                                Node_Displacements.Add(nrd);
                            }
                        }
                        catch (Exception ex) { }
                    }
                    else if (eigen_displace_flag)
                    {
                        try
                        {
                            mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                            if (mList.Count == 8)
                            {
                                int cols = 0;
                                NodeResultData nrd = new NodeResultData();
                                nrd.NodeNo = mList.GetInt(cols++);
                                nrd.LoadCase = mList.GetInt(cols++);
                                nrd.X_Translation = mList.GetDouble(cols++);
                                nrd.Y_Translation = mList.GetDouble(cols++);
                                nrd.Z_Translation = mList.GetDouble(cols++);
                                nrd.X_Rotation = mList.GetDouble(cols++);
                                nrd.Y_Rotation = mList.GetDouble(cols++);
                                nrd.Z_Rotation = mList.GetDouble(cols++);

                                if (Eigen_Displacements == null)
                                    Eigen_Displacements = new NodeResults();

                                Eigen_Displacements.Add(nrd);
                            }
                            else if (mList.Count == 7)
                            {

                                int cols = 0;
                                NodeResultData nrd = new NodeResultData();
                                nrd.NodeNo = Eigen_Displacements[Eigen_Displacements.Count - 1].NodeNo;

                                nrd.LoadCase = mList.GetInt(cols++);
                                nrd.X_Translation = mList.GetDouble(cols++);
                                nrd.Y_Translation = mList.GetDouble(cols++);
                                nrd.Z_Translation = mList.GetDouble(cols++);
                                nrd.X_Rotation = mList.GetDouble(cols++);
                                nrd.Y_Rotation = mList.GetDouble(cols++);
                                nrd.Z_Rotation = mList.GetDouble(cols++);
                                Eigen_Displacements.Add(nrd);
                            }
                        }
                        catch (Exception ex) { }
                    }

                    if (IsRun_ProgressBar)
                    {
                        iApp.SetProgressValue(i, file_content.Count);
                        if (iApp.Is_Progress_Cancel)
                            break;
                    }
                }
                SetMemberForces();

            }
            catch (ThreadAbortException ex1)
            {

                //MessageBox.Show("Error in Data reading...please OK", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error in Data reading...please OK", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                file_content.Clear();
                file_content = null;
                if (IsRun_ProgressBar)
                {
                    if (iApp != null)
                    {
                        iApp.Progress_OFF();
                    }
                }

                if(Node_Displacements == null)
                {
                    Node_Displacements = Eigen_Displacements;
                }
            }
            return true;
        }

        public JointNodeCollection Supports
        {
            get
            {
                return structure.Supports;
            }
        }
        void SetMemberForces()
        {
            string temp_file = "";
            string src_file = "";

            temp_file = MemberForce_File;
            src_file = Path.Combine(Path.GetDirectoryName(Analysis_File), "ANALYSIS_REP.txt");
            if (!File.Exists(src_file)) return;

            StreamWriter sw = new StreamWriter(new FileStream(temp_file, FileMode.Create));
            StreamReader sr = new StreamReader(new FileStream(Analysis_File, FileMode.Open));

            string str = "";
            string kstr = "";
            bool flag = false;

            bool truss_title = false;
            bool beam_title = false;
            bool cable_title = false;
            try
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Taken from file ANALYSIS_REP.TXT");
                //sw.WriteLine("       Member   Load     Node    AXIAL       SHEAR       SHEAR     TORSION     BENDING     BENDING");
                //sw.WriteLine("       No:      Case     Nos:       R1          R2          R3          M1          M2          M3");
                sw.WriteLine();
                sw.WriteLine();
                while (!sr.EndOfStream)
                {
                    str = sr.ReadLine().ToUpper();
                    kstr = MyList.RemoveAllSpaces(str);

                    //str = str.Replace("\t\t", "\t    ");


                    if (kstr.Contains("T I M E L O G"))
                    {
                        //if (kstr.Contains("S T A T I C S O L U T I O N T I M E L O G")
                        sw.WriteLine();
                        sw.WriteLine();
                        sw.WriteLine("********************         END OF REPORT      **********************");
                        sw.WriteLine();
                        sw.WriteLine();

                        break;
                    }
                    if (str.Contains("TRUSS MEMBER ACTIONS"))
                    {
                        flag = true;
                        if (!truss_title)
                        {
                            sw.WriteLine();
                            sw.WriteLine("TRUSS MEMBER ACTIONS");
                            sw.WriteLine();
                            sw.WriteLine("    MEMBER      LOAD         STRESS              FORCE");
                            sw.WriteLine();
                            truss_title = true;
                        }
                        continue;
                    }
                    if (str.Contains("BEAM FORCES AND MOMENTS"))
                    {
                        flag = true;
                        if (!beam_title)
                        {
                            sw.WriteLine();
                            sw.WriteLine("BEAM MEMBER FORCES AND MOMENTS");
                            sw.WriteLine();
                            sw.WriteLine(" BEAM LOAD     AXIAL       SHEAR       SHEAR     TORSION     BENDING     BENDING");
                            sw.WriteLine("  NO.  NO.        R1          R2          R3          M1          M2          M3");
                            sw.WriteLine();
                            beam_title = true;
                        }
                        continue;
                    }
                    if (str.Contains("CABLE MEMBER ACTIONS"))
                    {
                        flag = true;
                        if (!cable_title)
                        {
                            sw.WriteLine("CABLE MEMBER ACTIONS");
                            sw.WriteLine();
                            sw.WriteLine("    MEMBER      LOAD         STRESS              FORCE");
                            sw.WriteLine();
                            cable_title = true;
                        }
                        continue;
                    }

                    if (kstr == "BEAM LOAD AXIAL SHEAR SHEAR TORSION BENDING BENDING") continue;
                    if (kstr == "NO. NO. R1 R2 R3 M1 M2 M3") continue;
                    if (kstr == "MEMBER LOAD STRESS FORCE") continue;


                    if (flag && !str.Contains("*") && str.Length > 9)
                    {
                        if ((kstr != "MEMBER LOAD STRESS FORCE") ||
                            (kstr != ".....BEAM FORCES AND MOMENTS"))
                            sw.WriteLine(str);
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
                sr.Close();
            }
        }
        public string MemberForce_File
        {
            get
            {
                if (File.Exists(Analysis_File))
                    return Path.Combine(Path.GetDirectoryName(Analysis_File), "MemberForces.txt");
                return "";
            }
        }

        void SetAnalysisData()
        {
            try
            {
                MemberAnalysis = new Hashtable();
                list_analysis = new List<AnalysisData>();
                AnalysisData ana_data = null;
                AstraMember mem = null;
                //if (list_mem_no.Count > 1)
                //    iApp.Progress_ON("Read Member forces from the Analysis Report");

                int mno = -1;
                int mno_cnt = 1;

                if (list_mem_no.Count == 0)
                {

                    list_mem_no.Clear();
                    hash_table.Clear();

                    for (int i = 0; i < list_trusses.Count; i++)
                    {
                        if (list_trusses[i].LoadNo > 1) continue;
                        mno = list_trusses[i].TrussMemberNo;

                        mem = new AstraMember();
                        mem.AstraMemberType = eAstraMemberType.TRUSS;
                        mem.UserNo = mno_cnt++;
                        mem.AstraMemberNo = mno;

                        list_mem_no.Add(mem.UserNo);
                        hash_table.Add(mem.UserNo, mem);
                    }
                    for (int i = 0; i < list_beams.Count; i++)
                    {
                        if (list_beams[i].LoadNo > 1) continue;

                        mno = list_beams[i].BeamNo;
                        mem = new AstraMember();
                        mem.AstraMemberType = eAstraMemberType.BEAM;
                        mem.UserNo = mno_cnt++;
                        mem.AstraMemberNo = mno;

                        list_mem_no.Add(mem.UserNo);
                        hash_table.Add(mem.UserNo, mem);

                    }
                    for (int i = 0; i < list_cables.Count; i++)
                    {
                        if (list_cables[i].LoadNo > 1) continue;


                        mno = list_cables[i].TrussMemberNo;

                        mem = new AstraMember();
                        mem.AstraMemberType = eAstraMemberType.CABLE;
                        mem.UserNo = mno_cnt++;
                        mem.AstraMemberNo = mno;
                        list_mem_no.Add(mem.UserNo);
                        hash_table.Add(mem.UserNo, mem);
                    }


                }

                for (int i = 0; i < list_mem_no.Count; i++)
                {
                    mem = (AstraMember)hash_table[list_mem_no[i]];


                    ana_data = new AnalysisData();
                    ana_data.UserMemberNo = mem.UserNo;
                    ana_data.AstraMemberNo = mem.AstraMemberNo;

                    ana_data.CompressionForce = GetMaxCompressionForce(mem);
                    ana_data.TensileForce = GetMaxTensileForce(mem);
                    ana_data.MaxTorsion = GetMaxTorsion(mem);
                    ana_data.MaxBendingMoment = GetMaxBendingMoment(mem);
                    ana_data.MaxAxialForce = GetMaxAxialForce(mem);
                    ana_data.MaxShearForce = GetMaxShearForce(mem);
                    ana_data.MaxCompStress = GetMaxStress(mem);

                    //if (ana_data.CompressionForce == 0.0)
                    //    ana_data.CompressionForce = -ana_data.TensileForce;
                    //if (ana_data.TensileForce == 0.0)
                    //    ana_data.TensileForce = Math.Abs(ana_data.CompressionForce);

                    ana_data.TensileForce = ana_data.TensileForce;
                    ana_data.CompressionForce = ana_data.CompressionForce;

                    list_analysis.Add(ana_data);
                    ana_data.AstraMemberType = mem.AstraMemberType;
                    MemberAnalysis.Add(ana_data.UserMemberNo, ana_data);
                    //int v = (int)(((double)(i + 1) /(double) list_mem_no.Count) * 100.0);
                    //pbr.Invoke(spv, pbr, v);

                    //if (list_mem_no.Count > 1)
                    //{
                    //    iApp.SetProgressValue(i, list_mem_no.Count);
                    //    //Chiranjit [2013 05 15]
                    //    if (iApp.Is_Progress_Cancel) break;
                    //}
                }
                //if (list_mem_no.Count > 1)
                //    iApp.Progress_OFF();
            }
            catch (ThreadAbortException ex) { }
            catch (Exception ex)
            {
                //MessageBox.Show("Input Data is not in Correct format.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }
        MaxForce GetMaxStress(AstraMember mem)
        {
            double comp_force = 0.0;
            double frc = 0.0;
            MaxForce max_frc = new MaxForce();
            switch (mem.AstraMemberType)
            {
                case eAstraMemberType.TRUSS:
                    for (int i = 0; i < list_trusses.Count; i++)
                    {
                        if (list_trusses[i].TrussMemberNo == mem.AstraMemberNo)
                        {
                            frc = list_trusses[i].Stress;
                            //if (frc < 0)
                            //{
                            if (Math.Abs(frc) > Math.Abs(comp_force))
                            {
                                comp_force = frc;

                                max_frc.Force = frc;
                                max_frc.Stress = list_trusses[i].Stress;
                                max_frc.Loadcase = list_trusses[i].LoadNo;
                                max_frc.MemberNo = mem.UserNo;
                            }
                            //}
                        }
                    }
                    break;
                case eAstraMemberType.CABLE:
                    for (int i = 0; i < list_cables.Count; i++)
                    {
                        if (list_cables[i].TrussMemberNo == mem.AstraMemberNo)
                        {
                            frc = list_cables[i].Stress;
                            if (Math.Abs(frc) > Math.Abs(comp_force))
                            {
                                comp_force = frc;

                                max_frc.Force = frc;
                                max_frc.Loadcase = list_cables[i].LoadNo;
                                max_frc.MemberNo = mem.UserNo;
                            }
                        }
                    }
                    break;
            }
            return max_frc;
        }

        MaxForce GetMaxCompressionForce(AstraMember mem)
        {
            double comp_force = 0.0;
            double frc = 0.0;

            MaxForce max_frc = new MaxForce();

            if (mem.AstraMemberType == eAstraMemberType.BEAM)
            {
                for (int i = 0; i < list_beams.Count; i++)
                {
                    if (list_beams[i].BeamNo == mem.AstraMemberNo)
                    {
                        frc = list_beams[i].MaxCompressionForce;
                        if (Math.Abs(frc) > Math.Abs(comp_force))
                        {
                            comp_force = frc;
                            max_frc.Force = frc;
                            max_frc.Loadcase = list_beams[i].LoadNo;
                            max_frc.MemberNo = mem.UserNo;
                        }
                    }
                }
            }
            else if (mem.AstraMemberType == eAstraMemberType.TRUSS)
            {
                for (int i = 0; i < list_trusses.Count; i++)
                {
                    if (list_trusses[i].TrussMemberNo == mem.AstraMemberNo)
                    {
                        frc = list_trusses[i].Force;
                        if (frc < 0)
                        {
                            if (Math.Abs(frc) > Math.Abs(comp_force))
                            {
                                comp_force = frc;
                                max_frc.Force = frc;
                                max_frc.Stress = list_trusses[i].Stress;
                                max_frc.Loadcase = list_trusses[i].LoadNo;
                                max_frc.MemberNo = mem.UserNo;
                            }
                        }
                    }
                }
            }
            else if (mem.AstraMemberType == eAstraMemberType.CABLE)
            {
                for (int i = 0; i < list_cables.Count; i++)
                {
                    if (list_cables[i].TrussMemberNo == mem.AstraMemberNo)
                    {
                        frc = list_cables[i].Force;
                        if (frc < 0)
                        {
                            if (Math.Abs(frc) > Math.Abs(comp_force))
                            {
                                comp_force = frc;
                                max_frc.Force = frc;
                                max_frc.Loadcase = list_cables[i].LoadNo;
                                max_frc.MemberNo = mem.UserNo;
                            }
                        }
                    }
                }
            }
            //return comp_force;
            return max_frc;
        }
        MaxForce GetMaxTensileForce(AstraMember mem)
        {
            double tens_force = 0.0;
            double frc = 0.0;

            MaxForce max_frc = new MaxForce();

            if (mem.AstraMemberType == eAstraMemberType.BEAM)
            {
                for (int i = 0; i < list_beams.Count; i++)
                {
                    if (list_beams[i].BeamNo == mem.AstraMemberNo)
                    {
                        list_beams[i].StartNodeForce.ForceType = ForceType;
                        list_beams[i].EndNodeForce.ForceType = ForceType;

                        frc = list_beams[i].MaxTensileForce;
                        if (Math.Abs(frc) > Math.Abs(tens_force))
                        {
                            tens_force = frc;

                            max_frc.Force = frc;
                            max_frc.Loadcase = list_beams[i].LoadNo;
                            max_frc.MemberNo = mem.UserNo;
                        }
                    }
                }
            }
            if (mem.AstraMemberType == eAstraMemberType.TRUSS)
            {
                for (int i = 0; i < list_trusses.Count; i++)
                {
                    if (list_trusses[i].TrussMemberNo == mem.AstraMemberNo)
                    {
                        frc = list_trusses[i].Force;
                        if (frc > 0)
                        {
                            if (Math.Abs(frc) > Math.Abs(tens_force))
                            {
                                max_frc.Stress = list_trusses[i].Stress;

                                tens_force = frc;
                                max_frc.Force = frc;
                                max_frc.Stress = list_trusses[i].Stress;
                                max_frc.Loadcase = list_trusses[i].LoadNo;
                                max_frc.MemberNo = mem.UserNo;
                            }
                        }
                    }
                }
            }
            if (mem.AstraMemberType == eAstraMemberType.CABLE)
            {
                for (int i = 0; i < list_cables.Count; i++)
                {
                    if (list_cables[i].TrussMemberNo == mem.AstraMemberNo)
                    {
                        frc = list_cables[i].Force;
                        if (frc > 0)
                        {
                            if (Math.Abs(frc) > Math.Abs(tens_force))
                            {
                                tens_force = frc;
                                max_frc.Force = frc;
                                max_frc.Loadcase = list_cables[i].LoadNo;
                                max_frc.MemberNo = mem.UserNo;
                            }
                        }
                    }
                }
            }
            return max_frc;
        }

        MaxForce GetMaxBendingMoment(AstraMember mem)
        {
            double bend_frc = 0.0;
            double frc = 0.0;

            MaxForce max_frc = new MaxForce();


            if (mem.AstraMemberType == eAstraMemberType.BEAM)
            {
                for (int i = 0; i < list_beams.Count; i++)
                {
                    if (list_beams[i].BeamNo == mem.AstraMemberNo)
                    {
                        list_beams[i].StartNodeForce.ForceType = ForceType;
                        list_beams[i].EndNodeForce.ForceType = ForceType;

                        frc = list_beams[i].MaxBendingMoment;
                        if (Math.Abs(frc) > Math.Abs(bend_frc))
                        {
                            bend_frc = frc;
                            max_frc.Force = Math.Abs(bend_frc);
                            if (bend_frc > 0)
                                max_frc.Positive_Force = bend_frc;
                            else
                                max_frc.Negative_Force = bend_frc;

                            max_frc.Loadcase = list_beams[i].LoadNo;
                            max_frc.MemberNo = mem.UserNo;
                        }
                    }
                }
            }
            //return Math.Abs(bend_frc);
            return max_frc;
        }

        MaxForce GetMaxTorsion(AstraMember mem)
        {

            double bend_frc = 0.0;
            double frc = 0.0;

            MaxForce max_frc = new MaxForce();


            if (mem.AstraMemberType == eAstraMemberType.BEAM)
            {
                for (int i = 0; i < list_beams.Count; i++)
                {
                    if (list_beams[i].BeamNo == mem.AstraMemberNo)
                    {
                        list_beams[i].StartNodeForce.ForceType = ForceType;
                        list_beams[i].EndNodeForce.ForceType = ForceType;

                        frc = list_beams[i].MaxTorsion;
                        if (Math.Abs(frc) > Math.Abs(bend_frc))
                        {
                            bend_frc = frc;
                            max_frc.Force = Math.Abs(bend_frc);
                            if (bend_frc > 0)
                                max_frc.Positive_Force = bend_frc;
                            else
                                max_frc.Negative_Force = bend_frc;

                            max_frc.Loadcase = list_beams[i].LoadNo;
                            max_frc.MemberNo = mem.UserNo;
                        }
                    }
                }
            }
            //return Math.Abs(bend_frc);
            return max_frc;
        }
        MaxForce GetMaxAxialForce(AstraMember mem)
        {
            double shr_frc = 0.0;
            double frc = 0.0;
            MaxForce max_frc = new MaxForce();
            if (mem.AstraMemberType == eAstraMemberType.BEAM)
            {
                for (int i = 0; i < list_beams.Count; i++)
                {
                    if (list_beams[i].BeamNo == mem.AstraMemberNo)
                    {
                        frc = list_beams[i].MaxAxialForce;
                        //Chiranjit [2013 05 03]
                        //if (Math.Abs(frc) > Math.Abs(shr_frc))
                        //{
                        //    shr_frc = frc;
                        //    max_frc.Force = Math.Abs(shr_frc);
                        //    max_frc.Loadcase = list_beams[i].LoadNo;
                        //    max_frc.MemberNo = mem.UserNo;
                        //}

                        //Take negetive value, according to sandipan sir
                        if (frc > 0)
                        {
                            if (Math.Abs(frc) > Math.Abs(shr_frc))
                            {
                                shr_frc = frc;
                                max_frc.Force = Math.Abs(shr_frc);
                                max_frc.Loadcase = list_beams[i].LoadNo;
                                max_frc.MemberNo = mem.UserNo;
                            }
                        }
                    }
                }
            }
            //return Math.Abs(shr_frc);
            return max_frc;
        }

        MaxForce GetMaxShearForce(AstraMember mem)
        {
            double shr_frc = 0.0;
            double frc = 0.0;
            MaxForce max_frc = new MaxForce();
            if (mem.AstraMemberType == eAstraMemberType.BEAM)
            {
                for (int i = 0; i < list_beams.Count; i++)
                {
                    if (list_beams[i].BeamNo == mem.AstraMemberNo)
                    {
                        frc = list_beams[i].MaxShearForce;
                        //Chiranjit [2013 05 03]
                        //if (Math.Abs(frc) > Math.Abs(shr_frc))
                        //{
                        //    shr_frc = frc;
                        //    max_frc.Force = Math.Abs(shr_frc);
                        //    max_frc.Loadcase = list_beams[i].LoadNo;
                        //    max_frc.MemberNo = mem.UserNo;
                        //}

                        //Take negetive value, according to sandipan sir
                        if (frc > 0)
                        {
                            if (Math.Abs(frc) > Math.Abs(shr_frc))
                            {
                                shr_frc = frc;
                                max_frc.Force = Math.Abs(shr_frc);
                                max_frc.Loadcase = list_beams[i].LoadNo;
                                max_frc.MemberNo = mem.UserNo;
                            }
                        }
                    }
                }
            }
            //return Math.Abs(shr_frc);
            return max_frc;
        }

        #region GetForce [2011 11 21]

        /*
        public string GetForce(ref CMember mem)
        {
            //string str = MyList.RemoveAllSpaces(memNos);
            //MyList mList = new MyList(str, ' ');

            List<int> list_memNo = new List<int>();
            string str = MyList.RemoveAllSpaces(mem.Group.MemberNosText);
            MyList mList = new MyList(str, ' ');

            int indx = -1;
            int count = 0;
            //do
            //{
            //    indx = mList.StringList.IndexOf("TO");
            //    if (indx == -1)
            //    {
            //        indx = mList.StringList.IndexOf("-");
            //    }
            //    if (indx != -1)
            //    {
            //        count += mList.GetInt(indx + 1) - mList.GetInt(indx - 1);
            //        count++;
            //        mList.StringList.RemoveRange(0, indx + 2);
            //    }
            //    else if (mList.Count > 1)
            //    {
            //        count += mList.Count;
            //        mList.StringList.Clear();
            //    }
            //}
            //while (mList.Count != 0);

            double max_frc = 0.00;
            double max_val = -9999999999999999.0;

            MaxForce max_bend, max_shr, max_comp, max_tens, max_stress;
            MaxForce max_comp_stress, max_tensile_stress;

            max_bend = new MaxForce();
            max_shr = new MaxForce();
            max_comp = new MaxForce();
            max_tens = new MaxForce();
            max_stress = new MaxForce();
            max_comp_stress = new MaxForce();
            max_tensile_stress = new MaxForce();

            max_bend.Force = max_val;
            max_shr.Force = max_val;
            max_comp.Force = max_val;
            max_tens.Force = max_val;
            max_stress.Force = max_val;
            max_comp_stress.Force = max_val;
            max_tensile_stress.Force = max_val;

            AnalysisData ana_data;
            mem.Group.SetMemNos();
            if (mem.Group.MemberNos.Count > 0)
            {
                ana_data = (AnalysisData)MemberAnalysis[mem.Group.MemberNos[0]];
                if (ana_data == null) return "";
                for (int i = 0; i < mem.Group.MemberNos.Count; i++)
                {
                    ana_data = (AnalysisData)MemberAnalysis[mem.Group.MemberNos[i]];

                    if (ana_data.MaxBendingMoment.Force > max_bend.Force)
                    {
                        max_bend.Force = ana_data.MaxBendingMoment.Force;


                        max_bend.Loadcase = ana_data.MaxBendingMoment.Loadcase;
                        max_bend.MemberNo = ana_data.MaxBendingMoment.MemberNo;
                    }

                    if (ana_data.MaxShearForce.Force > max_shr.Force)
                    {
                        max_shr.Force = ana_data.MaxShearForce.Force;

                        max_shr.Loadcase = ana_data.MaxShearForce.Loadcase;
                        max_shr.MemberNo = ana_data.MaxShearForce.MemberNo;
                    }

                    if (ana_data.CompressionForce.Force > max_comp.Force)
                    {
                        max_comp.Force = ana_data.CompressionForce.Force;

                        max_comp.Loadcase = ana_data.CompressionForce.Loadcase;
                        max_comp.MemberNo = ana_data.CompressionForce.MemberNo;
                    }

                    if (ana_data.TensileForce.Force > max_tens.Force)
                    {
                        max_tens.Force = ana_data.TensileForce.Force;

                        max_tens.Loadcase = ana_data.TensileForce.Loadcase;
                        max_tens.MemberNo = ana_data.TensileForce.MemberNo;
                    }

                    //if (ana_data.MaxStress > 0)
                    //{
                    //    if (ana_data.MaxStress > max_comp_stress)
                    //        max_comp_stress = ana_data.MaxStress;
                    //}
                    //if (ana_data.MaxStress < 0)
                    //{
                    //    if (Math.Abs(ana_data.MaxStress) > Math.Abs(max_comp_stress))
                    //        max_tensile_stress = ana_data.MaxStress;
                    //}
                    if (Math.Abs(ana_data.MaxStress.Force) > max_stress.Force)
                    {
                        max_stress.Force = Math.Abs(ana_data.MaxStress.Force);
                        max_stress.Loadcase = ana_data.MaxStress.Loadcase;
                        max_stress.MemberNo = ana_data.MaxStress.MemberNo;
                    }

                }
                str = "";
                if (ana_data.AstraMemberType == eAstraMemberType.BEAM)
                {
                    if (max_bend.Force != 0.0)
                    {
                        str = " Moment = " + max_bend.Force.ToString("0.000") + " kN-m";
                    }
                    if (max_shr.Force != 0.0)
                    {
                        if (str != "")
                            str += ", Shear = " + max_shr.Force.ToString("0.000") + " kN";
                        else
                            str += " Shear = " + max_shr.Force.ToString("0.000") + " kN";

                    }
                }
                else if (ana_data.AstraMemberType == eAstraMemberType.TRUSS)
                {
                    if (max_comp.Force != 0.0)
                    {
                        str += " Compression = " + max_comp.Force.ToString("0.000") + " kN";
                    }
                    if (max_tens.Force != 0.0)
                    {
                        if (str != "")
                            str += ", Tension = " + max_tens.Force.ToString("0.000") + " kN";
                        else
                            str += " Tension = " + max_tens.Force.ToString("0.000") + " kN";
                    }
                }
                else if (ana_data.AstraMemberType == eAstraMemberType.CABLE)
                {
                    //if (max_comp.Force != 0.0)
                    //{
                    //    str += " Compression = " + max_comp.Force.ToString("0.000") + " kN";
                    //}
                    if (max_tens.Force != 0.0)
                    {
                        if (str != "")
                            str += ", Tension = " + max_tens.Force.ToString("0.000") + " kN";
                        else
                            str += " Tension = " + max_tens.Force.ToString("0.000") + " kN";
                    }
                }
            }

            mem.MaxCompForce = max_comp;
            mem.MaxTensionForce = max_tens;
            mem.MaxMoment = max_bend;
            mem.MaxShearForce = max_shr;
            mem.MaxStress = max_stress;



            mem.MaxCompForce.Force = (max_comp.Force == max_val) ? 0.0 : Math.Abs(max_comp.Force);
            mem.MaxTensionForce.Force = (max_tens.Force == max_val) ? 0.0 : max_tens.Force;
            mem.MaxMoment.Force = (max_bend.Force == max_val) ? 0.0 : max_bend.Force;
            mem.MaxShearForce.Force = (max_shr.Force == max_val) ? 0.0 : max_shr.Force;

            //Chiranjit [2011 10 20]
            // Checking for Stress of a Truss Member
            mem.MaxStress.Force = (max_shr.Force == max_val) ? 0.0 : Math.Abs(max_stress.Force);

            //mem.Compressive_Stress = (max_comp_stress == max_val) ? 0.0 : max_comp_stress;
            //mem.Tensile_Stress = (max_comp_stress == max_val) ? 0.0 : max_tensile_stress;
            return str;
        }
        /**/
        #endregion
        public string GetForce(ref CMember mem)
        {
            //string str = MyList.RemoveAllSpaces(memNos);
            //MyList mList = new MyList(str, ' ');

            //Chiranjit [2012 02 08]
            bool is_cross_girder = (mem.MemberType == eMemberType.CrossGirder);

            double _max_x = structure.Joints.MaxX;
            double _min_x = structure.Joints.MinX;

            List<int> list_memNo = new List<int>();
            string str = MyList.RemoveAllSpaces(mem.Group.MemberNosText);
            MyList mList = new MyList(str, ' ');

            int indx = -1;
            int count = 0;
            //do
            //{
            //    indx = mList.StringList.IndexOf("TO");
            //    if (indx == -1)
            //    {
            //        indx = mList.StringList.IndexOf("-");
            //    }
            //    if (indx != -1)
            //    {
            //        count += mList.GetInt(indx + 1) - mList.GetInt(indx - 1);
            //        count++;
            //        mList.StringList.RemoveRange(0, indx + 2);
            //    }
            //    else if (mList.Count > 1)
            //    {
            //        count += mList.Count;
            //        mList.StringList.Clear();
            //    }
            //}
            //while (mList.Count != 0);

            double max_frc = 0.00;
            //double max_val = -9999999999999999.0;
            double max_val = 0.0;

            MaxForce max_bend, max_tors, max_shr, max_comp, max_tens, max_stress;
            MaxForce max_comp_stress, max_tensile_stress;
            MaxForce max_axial;


            max_tors = new MaxForce();
            max_bend = new MaxForce();
            max_axial = new MaxForce();
            max_shr = new MaxForce();
            max_comp = new MaxForce();
            max_tens = new MaxForce();
            max_stress = new MaxForce();
            max_comp_stress = new MaxForce();
            max_tensile_stress = new MaxForce();

            max_tors.Force = max_val;
            max_bend.Force = max_val;
            max_axial.Force = max_val;
            max_shr.Force = max_val;
            max_comp.Force = max_val;
            max_tens.Force = max_val;
            max_stress.Force = max_val;
            max_comp_stress.Force = max_val;
            max_tensile_stress.Force = max_val;



            AnalysisData ana_data;
            mem.Group.SetMemNos();
            //mem.Group.SetMemNos();

            List<int> list_cc = new List<int>();
            Member _mbr = null;
            List<int> list_cc1 = new List<int>();

            if (is_cross_girder)
            {
                for (int i = 0; i < mem.Group.MemberNos.Count; i++)
                {
                    _mbr = structure.Get_Member(mem.Group.MemberNos[i]);

                    if (_mbr != null)
                    {
                        if ((_mbr.EndNode.X == _mbr.StartNode.X) &&
                            (_mbr.StartNode.X == _min_x || _mbr.StartNode.X == _max_x))
                        {
                            _mbr = new Member();
                        }
                        else
                        {
                            list_cc.Add(_mbr.MemberNo);
                        }
                    }
                }
                list_cc1.AddRange(mem.Group.MemberNos);
                mem.Group.MemberNos.Clear();
                mem.Group.MemberNos.AddRange(list_cc);
            }



            if (mem.Group.MemberNos.Count > 0)
            {
                if (MemberAnalysis == null) MemberAnalysis = new Hashtable();

                ana_data = (AnalysisData)MemberAnalysis[mem.Group.MemberNos[0]];
                if (ana_data == null) return "";
                for (int i = 0; i < mem.Group.MemberNos.Count; i++)
                {
                    ana_data = (AnalysisData)MemberAnalysis[mem.Group.MemberNos[i]];
                    if (ana_data == null) continue;


                    if (Math.Abs(ana_data.MaxTorsion.Force) > Math.Abs(max_tors.Force))
                    {
                        //max_tors = ana_data.MaxTorsion;

                        max_tors.Force = ana_data.MaxTorsion.Force;

                        max_tors.Loadcase = ana_data.MaxTorsion.Loadcase;
                        max_tors.MemberNo = ana_data.MaxTorsion.MemberNo;
                    }

                    if (Math.Abs(ana_data.MaxBendingMoment.Force) > Math.Abs(max_bend.Force))
                    {
                        max_bend.Force = ana_data.MaxBendingMoment.Force;


                        max_bend.Loadcase = ana_data.MaxBendingMoment.Loadcase;
                        max_bend.MemberNo = ana_data.MaxBendingMoment.MemberNo;
                    }

                    if (Math.Abs(ana_data.MaxShearForce.Force) > Math.Abs(max_shr.Force))
                    {
                        max_shr.Force = ana_data.MaxShearForce.Force;

                        max_shr.Loadcase = ana_data.MaxShearForce.Loadcase;
                        max_shr.MemberNo = ana_data.MaxShearForce.MemberNo;
                    }
                    if (Math.Abs(ana_data.MaxAxialForce.Force) > Math.Abs(max_axial.Force))
                    {
                        max_axial.Force = ana_data.MaxAxialForce.Force;

                        max_axial.Loadcase = ana_data.MaxAxialForce.Loadcase;
                        max_axial.MemberNo = ana_data.MaxAxialForce.MemberNo;
                    }

                    if (Math.Abs(ana_data.CompressionForce.Force) > Math.Abs(max_comp.Force))
                    {
                        max_comp.Force = ana_data.CompressionForce.Force;
                        max_comp.Stress = ana_data.CompressionForce.Stress;

                        max_comp.Loadcase = ana_data.CompressionForce.Loadcase;
                        max_comp.MemberNo = ana_data.CompressionForce.MemberNo;
                    }

                    if (Math.Abs(ana_data.TensileForce.Force) > Math.Abs(max_tens.Force))
                    {
                        max_tens.Force = ana_data.TensileForce.Force;
                        max_tens.Stress = ana_data.TensileForce.Stress;

                        max_tens.Loadcase = ana_data.TensileForce.Loadcase;
                        max_tens.MemberNo = ana_data.TensileForce.MemberNo;
                    }

                    //if (ana_data.MaxStress > 0)
                    //{
                    //    if (ana_data.MaxStress > max_comp_stress)
                    //        max_comp_stress = ana_data.MaxStress;
                    //}
                    //if (ana_data.MaxStress < 0)
                    //{
                    //    if (Math.Abs(ana_data.MaxStress) > Math.Abs(max_comp_stress))
                    //        max_tensile_stress = ana_data.MaxStress;
                    //}
                    if (Math.Abs(ana_data.MaxCompStress.Force) > Math.Abs(max_stress.Force))
                    {
                        max_stress.Force = Math.Abs(ana_data.MaxCompStress.Force);
                        max_stress.Loadcase = ana_data.MaxCompStress.Loadcase;
                        max_stress.MemberNo = ana_data.MaxCompStress.MemberNo;
                    }

                }
                str = "";
                if (ana_data == null) return str;

                if (ana_data.AstraMemberType == eAstraMemberType.BEAM)
                {
                    if (max_bend.Force != 0.0)
                    {
                        str = " Moment = " + max_bend.Force.ToString("0.000") + " kN-m";
                    }
                    if (max_shr.Force != 0.0)
                    {
                        if (str != "")
                            str += ", Shear = " + max_shr.Force.ToString("0.000") + " kN";
                        else
                            str += " Shear = " + max_shr.Force.ToString("0.000") + " kN";

                    }
                }
                else if (ana_data.AstraMemberType == eAstraMemberType.TRUSS)
                {
                    if (max_comp.Force != 0.0)
                    {
                        str += " Compression = " + max_comp.Force.ToString("0.000") + " kN";
                    }
                    if (max_tens.Force != 0.0)
                    {
                        if (str != "")
                            str += ", Tension = " + max_tens.Force.ToString("0.000") + " kN";
                        else
                            str += " Tension = " + max_tens.Force.ToString("0.000") + " kN";
                    }
                }
                
            }
            if (is_cross_girder)
            {
                mem.Group.MemberNos.Clear();
                mem.Group.MemberNos.AddRange(list_cc1);
            }
            mem.MaxCompForce = max_comp;
            mem.MaxTensionForce = max_tens;
            mem.MaxTorsion = max_tors;
            mem.MaxBendingMoment = max_bend;
            mem.MaxAxialForce = max_axial;
            mem.MaxShearForce = max_shr;
            mem.MaxStress = max_stress;



            mem.MaxCompForce.Force = (max_comp.Force == max_val) ? 0.0 : Math.Abs(max_comp.Force);
            mem.MaxTensionForce.Force = (max_tens.Force == max_val) ? 0.0 : max_tens.Force;
            mem.MaxTorsion.Force = (max_tors.Force == max_val) ? 0.0 : max_tors.Force;
            mem.MaxBendingMoment.Force = (max_bend.Force == max_val) ? 0.0 : max_bend.Force;
            mem.MaxAxialForce.Force = (max_axial.Force == max_val) ? 0.0 : max_axial.Force;
            mem.MaxShearForce.Force = (max_shr.Force == max_val) ? 0.0 : max_shr.Force;



            if (mem.MemberType == eMemberType.BottomChord)
            {
                if (mem.MaxTensionForce == 0.0)
                    mem.MaxTensionForce = mem.MaxCompForce;
                mem.MaxCompForce = 0.0;
            }
            if (mem.MemberType == eMemberType.TopChord)
            {
                if (mem.MaxCompForce == 0.0)
                    mem.MaxCompForce = mem.MaxTensionForce;
                mem.MaxTensionForce = 0.0;
            }


            //Chiranjit [2011 10 20]
            // Checking for Stress of a Truss Member
            mem.MaxStress.Force = (max_stress.Force == max_val) ? 0.0 : Math.Abs(max_stress.Force);

            //mem.Compressive_Stress = (max_comp_stress == max_val) ? 0.0 : max_comp_stress;
            //mem.Tensile_Stress = (max_comp_stress == max_val) ? 0.0 : max_tensile_stress;
            return str;
        }

        public string GetForce(ref CMember mem, bool Tower_Analysis)
        {
            //string str = MyList.RemoveAllSpaces(memNos);
            //MyList mList = new MyList(str, ' ');

            //Chiranjit [2012 02 08]
            bool is_cross_girder = (mem.MemberType == eMemberType.CrossGirder);

            double _max_x = structure.Joints.MaxX;
            double _min_x = structure.Joints.MinX;

            List<int> list_memNo = new List<int>();
            string str = MyList.RemoveAllSpaces(mem.Group.MemberNosText);
            MyList mList = new MyList(str, ' ');

            int indx = -1;
            int count = 0;
            //do
            //{
            //    indx = mList.StringList.IndexOf("TO");
            //    if (indx == -1)
            //    {
            //        indx = mList.StringList.IndexOf("-");
            //    }
            //    if (indx != -1)
            //    {
            //        count += mList.GetInt(indx + 1) - mList.GetInt(indx - 1);
            //        count++;
            //        mList.StringList.RemoveRange(0, indx + 2);
            //    }
            //    else if (mList.Count > 1)
            //    {
            //        count += mList.Count;
            //        mList.StringList.Clear();
            //    }
            //}
            //while (mList.Count != 0);

            double max_frc = 0.00;
            //double max_val = -9999999999999999.0;
            double max_val = 0.0;

            MaxForce max_bend, max_tors, max_shr, max_comp, max_tens, max_stress;
            MaxForce max_comp_stress, max_tensile_stress;
            MaxForce max_axial;


            max_tors = new MaxForce();
            max_bend = new MaxForce();
            max_axial = new MaxForce();
            max_shr = new MaxForce();
            max_comp = new MaxForce();
            max_tens = new MaxForce();
            max_stress = new MaxForce();
            max_comp_stress = new MaxForce();
            max_tensile_stress = new MaxForce();

            max_tors.Force = max_val;
            max_bend.Force = max_val;
            max_axial.Force = max_val;
            max_shr.Force = max_val;
            max_comp.Force = max_val;
            max_tens.Force = max_val;
            max_stress.Force = max_val;
            max_comp_stress.Force = max_val;
            max_tensile_stress.Force = max_val;



            AnalysisData ana_data;
            mem.Group.SetMemNos();
            //mem.Group.SetMemNos();

            List<int> list_cc = new List<int>();
            Member _mbr = null;
            List<int> list_cc1 = new List<int>();

            if (is_cross_girder)
            {
                for (int i = 0; i < mem.Group.MemberNos.Count; i++)
                {
                    _mbr = structure.Get_Member(mem.Group.MemberNos[i]);

                    if (_mbr != null)
                    {
                        if ((_mbr.EndNode.X == _mbr.StartNode.X) &&
                            (_mbr.StartNode.X == _min_x || _mbr.StartNode.X == _max_x))
                        {
                            _mbr = new Member();
                        }
                        else
                        {
                            list_cc.Add(_mbr.MemberNo);
                        }
                    }
                }
                list_cc1.AddRange(mem.Group.MemberNos);
                mem.Group.MemberNos.Clear();
                mem.Group.MemberNos.AddRange(list_cc);
            }



            if (mem.Group.MemberNos.Count > 0)
            {
                if (MemberAnalysis == null) MemberAnalysis = new Hashtable();

                ana_data = (AnalysisData)MemberAnalysis[mem.Group.MemberNos[0]];
                if (ana_data == null) return "";
                for (int i = 0; i < mem.Group.MemberNos.Count; i++)
                {
                    ana_data = (AnalysisData)MemberAnalysis[mem.Group.MemberNos[i]];
                    if (ana_data == null) continue;


                    if (Math.Abs(ana_data.MaxTorsion.Force) > Math.Abs(max_tors.Force))
                    {
                        //max_tors = ana_data.MaxTorsion;

                        max_tors.Force = ana_data.MaxTorsion.Force;

                        max_tors.Loadcase = ana_data.MaxTorsion.Loadcase;
                        max_tors.MemberNo = ana_data.MaxTorsion.MemberNo;
                    }

                    if (Math.Abs(ana_data.MaxBendingMoment.Force) > Math.Abs(max_bend.Force))
                    {
                        max_bend.Force = ana_data.MaxBendingMoment.Force;


                        max_bend.Loadcase = ana_data.MaxBendingMoment.Loadcase;
                        max_bend.MemberNo = ana_data.MaxBendingMoment.MemberNo;
                    }

                    if (Math.Abs(ana_data.MaxShearForce.Force) > Math.Abs(max_shr.Force))
                    {
                        max_shr.Force = ana_data.MaxShearForce.Force;

                        max_shr.Loadcase = ana_data.MaxShearForce.Loadcase;
                        max_shr.MemberNo = ana_data.MaxShearForce.MemberNo;
                    }
                    if (Math.Abs(ana_data.MaxAxialForce.Force) > Math.Abs(max_axial.Force))
                    {
                        max_axial.Force = ana_data.MaxAxialForce.Force;

                        max_axial.Loadcase = ana_data.MaxAxialForce.Loadcase;
                        max_axial.MemberNo = ana_data.MaxAxialForce.MemberNo;
                    }

                    if (Math.Abs(ana_data.CompressionForce.Force) > Math.Abs(max_comp.Force))
                    {
                        max_comp.Force = ana_data.CompressionForce.Force;
                        max_comp.Stress = ana_data.CompressionForce.Stress;

                        max_comp.Loadcase = ana_data.CompressionForce.Loadcase;
                        max_comp.MemberNo = ana_data.CompressionForce.MemberNo;
                    }

                    if (Math.Abs(ana_data.TensileForce.Force) > Math.Abs(max_tens.Force))
                    {
                        max_tens.Force = ana_data.TensileForce.Force;
                        max_tens.Stress = ana_data.TensileForce.Stress;

                        max_tens.Loadcase = ana_data.TensileForce.Loadcase;
                        max_tens.MemberNo = ana_data.TensileForce.MemberNo;
                    }

                    //if (ana_data.MaxStress > 0)
                    //{
                    //    if (ana_data.MaxStress > max_comp_stress)
                    //        max_comp_stress = ana_data.MaxStress;
                    //}
                    //if (ana_data.MaxStress < 0)
                    //{
                    //    if (Math.Abs(ana_data.MaxStress) > Math.Abs(max_comp_stress))
                    //        max_tensile_stress = ana_data.MaxStress;
                    //}
                    if (Math.Abs(ana_data.MaxCompStress.Force) > Math.Abs(max_stress.Force))
                    {
                        max_stress.Force = Math.Abs(ana_data.MaxCompStress.Force);
                        max_stress.Loadcase = ana_data.MaxCompStress.Loadcase;
                        max_stress.MemberNo = ana_data.MaxCompStress.MemberNo;
                    }

                }
                str = "";
                if (ana_data == null) return str;

                //if (ana_data.AstraMemberType == eAstraMemberType.BEAM)
                //{
                if (max_axial.Force != 0.0)
                {
                    str = " Axial = " + max_axial.Force.ToString("0.000") + " kN";
                    max_comp = max_axial;
                    max_tens = max_axial;
                }
                if (max_bend.Force != 0.0)
                {
                    if (str != "")
                        str += ", Moment = " + max_bend.Force.ToString("0.000") + " kN-m";
                    else
                        str = " Moment = " + max_bend.Force.ToString("0.000") + " kN-m";
                }
                if (max_shr.Force != 0.0)
                {
                    if (str != "")
                        str += ", Shear = " + max_shr.Force.ToString("0.000") + " kN";
                    else
                        str += " Shear = " + max_shr.Force.ToString("0.000") + " kN";

                }
                //}
                //else if (ana_data.AstraMemberType == eAstraMemberType.TRUSS)
                //{
                //if (max_comp.Force != 0.0)
                //{
                //    str += " Compression = " + max_comp.Force.ToString("0.000") + " kN";
                //}
                //if (max_tens.Force != 0.0)
                //{
                //    if (str != "")
                //        str += ", Tension = " + max_tens.Force.ToString("0.000") + " kN";
                //    else
                //        str += " Tension = " + max_tens.Force.ToString("0.000") + " kN";
                //}
                //}

            }
            if (is_cross_girder)
            {
                mem.Group.MemberNos.Clear();
                mem.Group.MemberNos.AddRange(list_cc1);
            }
            mem.MaxCompForce = max_comp;
            mem.MaxTensionForce = max_tens;
            mem.MaxTorsion = max_tors;
            mem.MaxBendingMoment = max_bend;
            mem.MaxAxialForce = max_axial;
            mem.MaxShearForce = max_shr;
            mem.MaxStress = max_stress;



            mem.MaxCompForce.Force = (max_comp.Force == max_val) ? 0.0 : Math.Abs(max_comp.Force);
            mem.MaxTensionForce.Force = (max_tens.Force == max_val) ? 0.0 : max_tens.Force;
            mem.MaxTorsion.Force = (max_tors.Force == max_val) ? 0.0 : max_tors.Force;
            mem.MaxBendingMoment.Force = (max_bend.Force == max_val) ? 0.0 : max_bend.Force;
            mem.MaxAxialForce.Force = (max_axial.Force == max_val) ? 0.0 : max_axial.Force;
            mem.MaxShearForce.Force = (max_shr.Force == max_val) ? 0.0 : max_shr.Force;



            //if (mem.MemberType == eMemberType.BottomChord)
            //{
            //    if (mem.MaxTensionForce == 0.0)
            //        mem.MaxTensionForce = mem.MaxCompForce;
            //    mem.MaxCompForce = 0.0;
            //}
            //if (mem.MemberType == eMemberType.TopChord)
            //{
            //    if (mem.MaxCompForce == 0.0)
            //        mem.MaxCompForce = mem.MaxTensionForce;
            //    mem.MaxTensionForce = 0.0;
            //}


            //Chiranjit [2011 10 20]
            // Checking for Stress of a Truss Member
            mem.MaxStress.Force = (max_stress.Force == max_val) ? 0.0 : Math.Abs(max_stress.Force);

            //mem.Compressive_Stress = (max_comp_stress == max_val) ? 0.0 : max_comp_stress;
            //mem.Tensile_Stress = (max_comp_stress == max_val) ? 0.0 : max_tensile_stress;
            return str;
        }

        public string GetForceTower(ref CMember mem, bool Tower_Analysis)
        {
            //string str = MyList.RemoveAllSpaces(memNos);
            //MyList mList = new MyList(str, ' ');

            //Chiranjit [2012 02 08]
            bool is_cross_girder = (mem.MemberType == eMemberType.CrossGirder);

            double _max_x = structure.Joints.MaxX;
            double _min_x = structure.Joints.MinX;

            List<int> list_memNo = new List<int>();
            string str = MyList.RemoveAllSpaces(mem.Group.MemberNosText);
            MyList mList = new MyList(str, ' ');

            int indx = -1;
            int count = 0;
            //do
            //{
            //    indx = mList.StringList.IndexOf("TO");
            //    if (indx == -1)
            //    {
            //        indx = mList.StringList.IndexOf("-");
            //    }
            //    if (indx != -1)
            //    {
            //        count += mList.GetInt(indx + 1) - mList.GetInt(indx - 1);
            //        count++;
            //        mList.StringList.RemoveRange(0, indx + 2);
            //    }
            //    else if (mList.Count > 1)
            //    {
            //        count += mList.Count;
            //        mList.StringList.Clear();
            //    }
            //}
            //while (mList.Count != 0);

            double max_frc = 0.00;
            //double max_val = -9999999999999999.0;
            double max_val = 0.0;

            MaxForce max_bend, max_tors, max_shr, max_comp, max_tens, max_stress;
            MaxForce max_comp_stress, max_tensile_stress;
            MaxForce max_axial;


            max_tors = new MaxForce();
            max_bend = new MaxForce();
            max_axial = new MaxForce();
            max_shr = new MaxForce();
            max_comp = new MaxForce();
            max_tens = new MaxForce();
            max_stress = new MaxForce();
            max_comp_stress = new MaxForce();
            max_tensile_stress = new MaxForce();

            max_tors.Force = max_val;
            max_bend.Force = max_val;
            max_axial.Force = max_val;
            max_shr.Force = max_val;
            max_comp.Force = max_val;
            max_tens.Force = max_val;
            max_stress.Force = max_val;
            max_comp_stress.Force = max_val;
            max_tensile_stress.Force = max_val;



            AnalysisData ana_data;
            mem.Group.SetMemNos();
            //mem.Group.SetMemNos();

            List<int> list_cc = new List<int>();
            Member _mbr = null;
            List<int> list_cc1 = new List<int>();

            if (is_cross_girder)
            {
                for (int i = 0; i < mem.Group.MemberNos.Count; i++)
                {
                    _mbr = structure.Get_Member(mem.Group.MemberNos[i]);

                    if (_mbr != null)
                    {
                        if ((_mbr.EndNode.X == _mbr.StartNode.X) &&
                            (_mbr.StartNode.X == _min_x || _mbr.StartNode.X == _max_x))
                        {
                            _mbr = new Member();
                        }
                        else
                        {
                            list_cc.Add(_mbr.MemberNo);
                        }
                    }
                }
                list_cc1.AddRange(mem.Group.MemberNos);
                mem.Group.MemberNos.Clear();
                mem.Group.MemberNos.AddRange(list_cc);
            }



            if (mem.Group.MemberNos.Count > 0)
            {
                if (MemberAnalysis == null) MemberAnalysis = new Hashtable();

                ana_data = (AnalysisData)MemberAnalysis[mem.Group.MemberNos[0]];
                if (ana_data == null) return "";
                for (int i = 0; i < mem.Group.MemberNos.Count; i++)
                {
                    ana_data = (AnalysisData)MemberAnalysis[mem.Group.MemberNos[i]];
                    if (ana_data == null) continue;


                    if (Math.Abs(ana_data.MaxTorsion.Force) > Math.Abs(max_tors.Force))
                    {
                        //max_tors = ana_data.MaxTorsion;

                        max_tors.Force = ana_data.MaxTorsion.Force;

                        max_tors.Loadcase = ana_data.MaxTorsion.Loadcase;
                        max_tors.MemberNo = ana_data.MaxTorsion.MemberNo;
                    }

                    if (Math.Abs(ana_data.MaxBendingMoment.Force) > Math.Abs(max_bend.Force))
                    {
                        max_bend.Force = ana_data.MaxBendingMoment.Force;


                        max_bend.Loadcase = ana_data.MaxBendingMoment.Loadcase;
                        max_bend.MemberNo = ana_data.MaxBendingMoment.MemberNo;
                    }

                    if (Math.Abs(ana_data.MaxShearForce.Force) > Math.Abs(max_shr.Force))
                    {
                        max_shr.Force = ana_data.MaxShearForce.Force;

                        max_shr.Loadcase = ana_data.MaxShearForce.Loadcase;
                        max_shr.MemberNo = ana_data.MaxShearForce.MemberNo;
                    }
                    if (Math.Abs(ana_data.MaxAxialForce.Force) > Math.Abs(max_axial.Force))
                    {
                        max_axial.Force = ana_data.MaxAxialForce.Force;

                        max_axial.Loadcase = ana_data.MaxAxialForce.Loadcase;
                        max_axial.MemberNo = ana_data.MaxAxialForce.MemberNo;
                    }

                    if (Math.Abs(ana_data.CompressionForce.Force) > Math.Abs(max_comp.Force))
                    {
                        max_comp.Force = ana_data.CompressionForce.Force;
                        max_comp.Stress = ana_data.CompressionForce.Stress;

                        max_comp.Loadcase = ana_data.CompressionForce.Loadcase;
                        max_comp.MemberNo = ana_data.CompressionForce.MemberNo;
                    }

                    if (Math.Abs(ana_data.TensileForce.Force) > Math.Abs(max_tens.Force))
                    {
                        max_tens.Force = ana_data.TensileForce.Force;
                        max_tens.Stress = ana_data.TensileForce.Stress;

                        max_tens.Loadcase = ana_data.TensileForce.Loadcase;
                        max_tens.MemberNo = ana_data.TensileForce.MemberNo;
                    }

                    //if (ana_data.MaxStress > 0)
                    //{
                    //    if (ana_data.MaxStress > max_comp_stress)
                    //        max_comp_stress = ana_data.MaxStress;
                    //}
                    //if (ana_data.MaxStress < 0)
                    //{
                    //    if (Math.Abs(ana_data.MaxStress) > Math.Abs(max_comp_stress))
                    //        max_tensile_stress = ana_data.MaxStress;
                    //}
                    if (Math.Abs(ana_data.MaxCompStress.Force) > Math.Abs(max_stress.Force))
                    {
                        max_stress.Force = Math.Abs(ana_data.MaxCompStress.Force);
                        max_stress.Loadcase = ana_data.MaxCompStress.Loadcase;
                        max_stress.MemberNo = ana_data.MaxCompStress.MemberNo;
                    }

                }
                str = "";
                if (ana_data == null) return str;

                //if (ana_data.AstraMemberType == eAstraMemberType.BEAM)
                //{
                if (max_axial.Force != 0.0)
                {
                    str = " Axial = " + max_bend.Force.ToString("0.000") + " kN";
                }
                
                if (max_bend.Force != 0.0)
                {
                    str = " Moment = " + max_bend.Force.ToString("0.000") + " kN-m";
                }
                if (max_shr.Force != 0.0)
                {
                    if (str != "")
                        str += ", Shear = " + max_shr.Force.ToString("0.000") + " kN";
                    else
                        str += " Shear = " + max_shr.Force.ToString("0.000") + " kN";

                }
                //}
                //else if (ana_data.AstraMemberType == eAstraMemberType.TRUSS)
                //{
                //if (max_comp.Force != 0.0)
                //{
                //    str += " Compression = " + max_comp.Force.ToString("0.000") + " kN";
                //}
                //if (max_tens.Force != 0.0)
                //{
                //    if (str != "")
                //        str += ", Tension = " + max_tens.Force.ToString("0.000") + " kN";
                //    else
                //        str += " Tension = " + max_tens.Force.ToString("0.000") + " kN";
                //}
                //}

            }
            if (is_cross_girder)
            {
                mem.Group.MemberNos.Clear();
                mem.Group.MemberNos.AddRange(list_cc1);
            }
            mem.MaxCompForce = max_comp;
            mem.MaxTensionForce = max_tens;
            mem.MaxTorsion = max_tors;
            mem.MaxBendingMoment = max_bend;
            mem.MaxAxialForce = max_axial;
            mem.MaxShearForce = max_shr;
            mem.MaxStress = max_stress;



            mem.MaxCompForce.Force = (max_comp.Force == max_val) ? 0.0 : Math.Abs(max_comp.Force);
            mem.MaxTensionForce.Force = (max_tens.Force == max_val) ? 0.0 : max_tens.Force;
            mem.MaxTorsion.Force = (max_tors.Force == max_val) ? 0.0 : max_tors.Force;
            mem.MaxBendingMoment.Force = (max_bend.Force == max_val) ? 0.0 : max_bend.Force;
            mem.MaxAxialForce.Force = (max_axial.Force == max_val) ? 0.0 : max_axial.Force;
            mem.MaxShearForce.Force = (max_shr.Force == max_val) ? 0.0 : max_shr.Force;



            //if (mem.MemberType == eMemberType.BottomChord)
            //{
            //    if (mem.MaxTensionForce == 0.0)
            //        mem.MaxTensionForce = mem.MaxCompForce;
            //    mem.MaxCompForce = 0.0;
            //}
            //if (mem.MemberType == eMemberType.TopChord)
            //{
            //    if (mem.MaxCompForce == 0.0)
            //        mem.MaxCompForce = mem.MaxTensionForce;
            //    mem.MaxTensionForce = 0.0;
            //}


            //Chiranjit [2011 10 20]
            // Checking for Stress of a Truss Member
            mem.MaxStress.Force = (max_stress.Force == max_val) ? 0.0 : Math.Abs(max_stress.Force);

            //mem.Compressive_Stress = (max_comp_stress == max_val) ? 0.0 : max_comp_stress;
            //mem.Tensile_Stress = (max_comp_stress == max_val) ? 0.0 : max_tensile_stress;
            return str;
        }

        public MaxForce GetJoint_Torsion(List<int> joint_array)
        {
            MaxForce mfrc = new MaxForce();


            double max_torsion = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    list_beams[j].StartNodeForce.ForceType = ForceType;
                    list_beams[j].EndNodeForce.ForceType = ForceType;


                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_torsion < Math.Abs(list_beams[j].StartNodeForce.M1_Torsion))
                        {
                            max_torsion = Math.Abs(list_beams[j].StartNodeForce.M1_Torsion);
                            mfrc.Force = max_torsion;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }

                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_torsion < Math.Abs(list_beams[j].EndNodeForce.M1_Torsion))
                        {
                            max_torsion = Math.Abs(list_beams[j].EndNodeForce.M1_Torsion);
                            mfrc.Force = max_torsion;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }

                    }
                }
            }

            //return (max_moment == double.MinValue) ? 0.0d : max_moment;

            return mfrc;
        }

        public MaxForce GetJoint_MomentForce(List<int> joint_array)
        {
            MaxForce mfrc = new MaxForce();


            double max_moment = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    list_beams[j].StartNodeForce.ForceType = ForceType;
                    list_beams[j].EndNodeForce.ForceType = ForceType;


                    //if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    //{
                    //    if (max_moment < Math.Abs(list_beams[j].StartNodeForce.M2_Bending))
                    //    {
                    //        max_moment = Math.Abs(list_beams[j].StartNodeForce.M2_Bending);
                    //        mfrc.Force = max_moment;
                    //        mfrc.MemberNo = list_beams[j].BeamNo;
                    //        mfrc.Loadcase = list_beams[j].LoadNo;
                    //        mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                    //    }

                    //    if (max_moment < Math.Abs(list_beams[j].StartNodeForce.M3_Bending))
                    //    {
                    //        max_moment = Math.Abs(list_beams[j].StartNodeForce.M3_Bending);
                    //        mfrc.Force = max_moment;
                    //        mfrc.MemberNo = list_beams[j].BeamNo;
                    //        mfrc.Loadcase = list_beams[j].LoadNo;
                    //        mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                    //    }
                    //}
                    //if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    //{
                    //    if (max_moment < Math.Abs(list_beams[j].EndNodeForce.M2_Bending))
                    //    {
                    //        max_moment = Math.Abs(list_beams[j].EndNodeForce.M2_Bending);
                    //        mfrc.Force = max_moment;
                    //        mfrc.MemberNo = list_beams[j].BeamNo;
                    //        mfrc.Loadcase = list_beams[j].LoadNo;
                    //        mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                    //    }

                    //    if (max_moment < Math.Abs(list_beams[j].EndNodeForce.M3_Bending))
                    //    {
                    //        max_moment = Math.Abs(list_beams[j].EndNodeForce.M3_Bending);
                    //        mfrc.Force = max_moment;
                    //        mfrc.MemberNo = list_beams[j].BeamNo;
                    //        mfrc.Loadcase = list_beams[j].LoadNo;
                    //        mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                    //    }
                    //}

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment))
                        {
                            max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }
 
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment))
                        {
                            max_moment = Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment);
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }
 
                    }
                }
            }

            //return (max_moment == double.MinValue) ? 0.0d : max_moment;

            return mfrc;

        }
        public MaxForce GetJoint_ShearForce(List<int> joint_array)
        {
            joint_array.Sort();
            MaxForce mfrc = new MaxForce();
            double max_shear = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {


                        //if (max_shear < Math.Abs(list_beams[j].StartNodeForce.R2_Shear))
                        //{
                        //    max_shear = Math.Abs(list_beams[j].StartNodeForce.R2_Shear);
                        //    mfrc.Force = max_shear;
                        //    mfrc.Loadcase = list_beams[j].LoadNo;
                        //    mfrc.MemberNo = list_beams[j].BeamNo;
                        //    mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        //}

                        //if (max_shear < Math.Abs(list_beams[j].StartNodeForce.R3_Shear))
                        //{
                        //    max_shear = Math.Abs(list_beams[j].StartNodeForce.R3_Shear);

                        //    mfrc.Force = max_shear;
                        //    mfrc.Loadcase = list_beams[j].LoadNo;
                        //    mfrc.MemberNo = list_beams[j].BeamNo;
                        //    mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        //}

                        //Chiranjit [2012 02 08]

                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;

                        //Chiranjit [2013 05 03]
                        //if (list_beams[j].StartNodeForce.MaxShearForce < 0)
                        //{
                        if (max_shear < Math.Abs(list_beams[j].StartNodeForce.MaxShearForce))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.MaxShearForce);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }
                        //}
                    }
                    else if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_shear < Math.Abs(list_beams[j].EndNodeForce.MaxShearForce))
                        {

                            list_beams[j].StartNodeForce.ForceType = ForceType;
                            list_beams[j].EndNodeForce.ForceType = ForceType;

                            max_shear = Math.Abs(list_beams[j].EndNodeForce.MaxShearForce);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }
                    }
                }
            }
            //return (max_shear == double.MinValue) ? 0.0d : max_shear;
            return mfrc;
        }

        public MaxForce GetJoint_R1_Axial(List<int> joint_array)
        {
            joint_array.Sort();
            MaxForce mfrc = new MaxForce();
            double max_shear = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_shear < Math.Abs(list_beams[j].StartNodeForce.R1_Axial))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.R1_Axial);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }
                    }
                    else if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_shear < Math.Abs(list_beams[j].EndNodeForce.R1_Axial))
                        {
                            max_shear = Math.Abs(list_beams[j].EndNodeForce.R1_Axial);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }
                    }
                }
            }
            return mfrc;
        }
        public MaxForce GetJoint_R1_Axial(List<int> joint_array, int loadcase)
        {
            joint_array.Sort();
            MaxForce mfrc = new MaxForce();
            double max_shear = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    if (list_beams[j].LoadNo == loadcase)
                    {
                        if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                        {
                            if (max_shear < Math.Abs(list_beams[j].StartNodeForce.R1_Axial))
                            {
                                max_shear = Math.Abs(list_beams[j].StartNodeForce.R1_Axial);
                                mfrc.Force = max_shear;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                            }
                        }
                    }
                }
            }
            return mfrc;
        }

        public MaxForce GetJoint_R2_Shear(List<int> joint_array)
        {
            joint_array.Sort();
            MaxForce mfrc = new MaxForce();
            double max_shear = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_shear < Math.Abs(list_beams[j].StartNodeForce.R2_Shear))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.R2_Shear);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }
                    }
                    else if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_shear < Math.Abs(list_beams[j].EndNodeForce.R2_Shear))
                        {
                            max_shear = Math.Abs(list_beams[j].EndNodeForce.R2_Shear);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }
                    }
                }
            }
            return mfrc;
        }

        public MaxForce GetJoint_R2_Shear(List<int> joint_array, int loadcase)
        {
            joint_array.Sort();
            MaxForce mfrc = new MaxForce();
            double max_shear = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    if (list_beams[j].LoadNo != loadcase) continue;

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_shear < Math.Abs(list_beams[j].StartNodeForce.R2_Shear))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.R2_Shear);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }
                    }
                    else if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_shear < Math.Abs(list_beams[j].EndNodeForce.R2_Shear))
                        {
                            max_shear = Math.Abs(list_beams[j].EndNodeForce.R2_Shear);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }
                    }
                }
            }
            return mfrc;
        }

        public MaxForce GetJoint_R3_Shear(List<int> joint_array)
        {
            joint_array.Sort();
            MaxForce mfrc = new MaxForce();
            double max_shear = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_shear < Math.Abs(list_beams[j].StartNodeForce.R3_Shear))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.R3_Shear);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }
                    }
                    else if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_shear < Math.Abs(list_beams[j].EndNodeForce.R3_Shear))
                        {
                            max_shear = Math.Abs(list_beams[j].EndNodeForce.R3_Shear);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }
                    }
                }
            }
            return mfrc;
        }
        public MaxForce GetJoint_R3_Shear(List<int> joint_array, int loadcase)
        {
            joint_array.Sort();
            MaxForce mfrc = new MaxForce();
            double max_shear = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    if (list_beams[j].LoadNo != loadcase) continue;

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_shear < Math.Abs(list_beams[j].StartNodeForce.R3_Shear))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.R3_Shear);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }
                    }
                    else if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_shear < Math.Abs(list_beams[j].EndNodeForce.R3_Shear))
                        {
                            max_shear = Math.Abs(list_beams[j].EndNodeForce.R3_Shear);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }
                    }
                }
            }
            return mfrc;
        }

        //Chiranjit [2013 05 08]
        public MaxForce GetJoint_MomentForce(List<int> joint_array, bool is_long_girder)
        {
            MaxForce mfrc = new MaxForce();


            double max_moment = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    list_beams[j].StartNodeForce.ForceType = ForceType;
                    list_beams[j].EndNodeForce.ForceType = ForceType;


                    if (is_long_girder)
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z != m.EndNode.Z) continue;
                    }
                    else
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z == m.EndNode.Z) continue;
                    }

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment))
                        {
                            max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }

                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment))
                        {
                            max_moment = Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment);
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }

                    }
                }
            }

            //return (max_moment == double.MinValue) ? 0.0d : max_moment;

            return mfrc;

        }


        public MaxForce GetJoint_Max_Hogging(int joint_no, bool is_long_girder)
        {
            List<int> joint_array = new List<int>();
            joint_array.Add(joint_no);
            return GetJoint_Max_Hogging(joint_array, is_long_girder);
        }
        public MaxForce GetJoint_Max_Hogging(List<int> joint_array, bool is_long_girder)
        {
            MaxForce mfrc = new MaxForce();

            double max_moment = 0.0;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    list_beams[j].StartNodeForce.ForceType = ForceType;
                    list_beams[j].EndNodeForce.ForceType = ForceType;


                    if (is_long_girder)
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z != m.EndNode.Z) continue;
                    }
                    else
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z == m.EndNode.Z) continue;
                    }

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (list_beams[j].StartNodeForce.MaxBendingMoment < 0) continue;
                        if (Math.Abs(max_moment) < Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment))
                        {
                            //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                            max_moment = list_beams[j].StartNodeForce.MaxBendingMoment;
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }

                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (list_beams[j].EndNodeForce.MaxBendingMoment < 0) continue;
                        if (Math.Abs(max_moment) < Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment))
                        {
                            //max_moment = Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment);
                            max_moment = list_beams[j].EndNodeForce.MaxBendingMoment;
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }

                    }
                }
            }

            //return (max_moment == double.MinValue) ? 0.0d : max_moment;

            return mfrc;

        }
        
        public MaxForce GetJoint_Max_Sagging(int joint_no, bool is_long_girder)
        {
            List<int> joint_array = new List<int>();
            joint_array.Add(joint_no);
            return GetJoint_Max_Sagging(joint_array, is_long_girder);
        }
        public MaxForce GetJoint_Max_Sagging(List<int> joint_array, bool is_long_girder)
        {
            MaxForce mfrc = new MaxForce();

            double max_moment = 0.0;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    list_beams[j].StartNodeForce.ForceType = ForceType;
                    list_beams[j].EndNodeForce.ForceType = ForceType;


                    if (is_long_girder)
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z != m.EndNode.Z) continue;
                    }
                    else
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z == m.EndNode.Z) continue;
                    }

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (list_beams[j].StartNodeForce.MaxBendingMoment > 0) continue;
                        if (Math.Abs(max_moment) < Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment))
                        {
                            //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                            max_moment = list_beams[j].StartNodeForce.MaxBendingMoment;
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }

                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (list_beams[j].EndNodeForce.MaxBendingMoment > 0) continue;
                        if (Math.Abs(max_moment) < Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment))
                        {
                            //max_moment = Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment);
                            max_moment = list_beams[j].EndNodeForce.MaxBendingMoment;
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }
                    }
                }
            }

            //return (max_moment == double.MinValue) ? 0.0d : max_moment;

            return mfrc;

        }

        public MaxForce GetMember_Max_Positive_Moment(string members)
        {
            return GetMember_Max_Positive_Moment(MyList.Get_Array_Intiger(members));
        }
        public MaxForce GetMember_Max_Positive_Moment(List<int> member_array)
        {
            MaxForce mfrc = new MaxForce();

            double max_moment = 0.0;

            for (int i = 0; i < member_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    bf.StartNodeForce.ForceType = ForceType;
                    bf.EndNodeForce.ForceType = ForceType;

                    if (bf.BeamNo == member_array[i])
                    {
                        if (bf.StartNodeForce.MaxBendingMoment > 0)
                        {
                            if (Math.Abs(max_moment) < Math.Abs(bf.StartNodeForce.MaxBendingMoment))
                            {
                                //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                                max_moment = bf.StartNodeForce.MaxBendingMoment;
                                mfrc.Force = max_moment;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = bf.StartNodeForce.JointNo;
                            }
                        }
                        if (bf.EndNodeForce.MaxBendingMoment > 0)
                        {
                            if (Math.Abs(max_moment) < Math.Abs(bf.EndNodeForce.MaxBendingMoment))
                            {
                                //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                                max_moment = bf.EndNodeForce.MaxBendingMoment;
                                mfrc.Force = max_moment;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = bf.EndNodeForce.JointNo;
                            }
                        }

                    }
                }
            }
            return mfrc;
        }

        public MaxForce GetJoint_Max_Positive_Moment(List<int> joint_array)
        {
            MaxForce mfrc = new MaxForce();

            double max_moment = 0.0;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    bf.StartNodeForce.ForceType = ForceType;
                    bf.EndNodeForce.ForceType = ForceType;

                    if (bf.StartNodeForce.JointNo == joint_array[i])
                    {
                        if (bf.StartNodeForce.MaxBendingMoment > 0)
                        {
                            if (Math.Abs(max_moment) < Math.Abs(bf.StartNodeForce.MaxBendingMoment))
                            {
                                //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                                max_moment = bf.StartNodeForce.MaxBendingMoment;
                                mfrc.Force = max_moment;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = bf.StartNodeForce.JointNo;
                            }
                        }
                    }
                    else if (bf.EndNodeForce.JointNo == joint_array[i])
                    {
                        if (bf.EndNodeForce.MaxBendingMoment > 0)
                        {
                            if (Math.Abs(max_moment) < Math.Abs(bf.EndNodeForce.MaxBendingMoment))
                            {
                                //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                                max_moment = bf.EndNodeForce.MaxBendingMoment;
                                mfrc.Force = max_moment;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = bf.EndNodeForce.JointNo;
                            }
                        }

                    }
                }
            }
            return mfrc;
        }


        public MaxForce GetMember_Max_Negative_Moment(string members)
        {
            return GetMember_Max_Negative_Moment(MyList.Get_Array_Intiger(members));

        }
        public MaxForce GetMember_Max_Negative_Moment(List<int> member_array)
        {
            MaxForce mfrc = new MaxForce();

            double max_moment = 0.0;

            for (int i = 0; i < member_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    bf.StartNodeForce.ForceType = ForceType;
                    bf.EndNodeForce.ForceType = ForceType;

                    if (bf.BeamNo == member_array[i])
                    {
                        if (bf.StartNodeForce.MaxBendingMoment < 0)
                        {
                            if (Math.Abs(max_moment) < Math.Abs(bf.StartNodeForce.MaxBendingMoment))
                            {
                                //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                                max_moment = bf.StartNodeForce.MaxBendingMoment;
                                mfrc.Force = max_moment;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = bf.StartNodeForce.JointNo;
                            }
                        }
                        if (bf.EndNodeForce.MaxBendingMoment < 0)
                        {
                            if (Math.Abs(max_moment) < Math.Abs(bf.EndNodeForce.MaxBendingMoment))
                            {
                                //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                                max_moment = bf.EndNodeForce.MaxBendingMoment;
                                mfrc.Force = max_moment;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = bf.EndNodeForce.JointNo;
                            }
                        }

                    }
                }
            }
            return mfrc;
        }

        public MaxForce GetJoint_Max_Negative_Moment(List<int> joint_array)
        {
            MaxForce mfrc = new MaxForce();

            double max_moment = 0.0;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    bf.StartNodeForce.ForceType = ForceType;
                    bf.EndNodeForce.ForceType = ForceType;

                    if (bf.StartNodeForce.JointNo == joint_array[i])
                    {
                        if (bf.StartNodeForce.MaxBendingMoment < 0)
                        {
                            if (Math.Abs(max_moment) < Math.Abs(bf.StartNodeForce.MaxBendingMoment))
                            {
                                //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                                max_moment = bf.StartNodeForce.MaxBendingMoment;
                                mfrc.Force = max_moment;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = bf.StartNodeForce.JointNo;
                            }
                        }
                    }
                    if (bf.EndNodeForce.JointNo == joint_array[i])
                    {
                        if (bf.EndNodeForce.MaxBendingMoment < 0)
                        {
                            if (Math.Abs(max_moment) < Math.Abs(bf.EndNodeForce.MaxBendingMoment))
                            {
                                //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                                max_moment = bf.EndNodeForce.MaxBendingMoment;
                                mfrc.Force = max_moment;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = bf.EndNodeForce.JointNo;
                            }
                        }

                    }
                }
            }
            return mfrc;
        }

        public MaxForce GetMember_Max_Positive_Shear(string members)
        {
            return GetMember_Max_Positive_Shear(MyList.Get_Array_Intiger(members));
        }

        public MaxForce GetMember_Max_Positive_Shear(List<int> member_array)
        {
            MaxForce mfrc = new MaxForce();

            double max_shear = 0.0;

            for (int i = 0; i < member_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    bf.StartNodeForce.ForceType = ForceType;
                    bf.EndNodeForce.ForceType = ForceType;

                    if (bf.BeamNo == member_array[i])
                    {
                        if (bf.StartNodeForce.MaxShearForce > 0)
                        {
                            if (Math.Abs(max_shear) < Math.Abs(bf.StartNodeForce.MaxShearForce))
                            {
                                //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                                max_shear = bf.StartNodeForce.MaxShearForce;
                                mfrc.Force = max_shear;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = bf.StartNodeForce.JointNo;
                            }
                        }
                        if (bf.EndNodeForce.MaxShearForce > 0)
                        {
                            if (Math.Abs(max_shear) < Math.Abs(bf.EndNodeForce.MaxShearForce))
                            {
                                //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                                max_shear = bf.EndNodeForce.MaxShearForce;
                                mfrc.Force = max_shear;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = bf.EndNodeForce.JointNo;
                            }
                        }
                    }
                }
            }

            //return (max_moment == double.MinValue) ? 0.0d : max_moment;

            return mfrc;

        }

        public MaxForce GetJoint_Max_Positive_Shear(List<int> joint_array)
        {
            MaxForce mfrc = new MaxForce();

            double max_shear = 0.0;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    bf.StartNodeForce.ForceType = ForceType;
                    bf.EndNodeForce.ForceType = ForceType;

                    if (bf.StartNodeForce.JointNo == joint_array[i])
                    {
                        if (bf.StartNodeForce.MaxShearForce > 0)
                        {
                            if (Math.Abs(max_shear) < Math.Abs(bf.StartNodeForce.MaxShearForce))
                            {
                                //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                                max_shear = bf.StartNodeForce.MaxShearForce;
                                mfrc.Force = max_shear;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = bf.StartNodeForce.JointNo;
                            }
                        }
                    }
                    if (bf.EndNodeForce.JointNo == joint_array[i])
                    {
                        if (bf.EndNodeForce.MaxShearForce > 0)
                        {
                            if (Math.Abs(max_shear) < Math.Abs(bf.EndNodeForce.MaxShearForce))
                            {
                                //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                                max_shear = bf.EndNodeForce.MaxShearForce;
                                mfrc.Force = max_shear;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = bf.EndNodeForce.JointNo;
                            }
                        }
                    }
                }
            }

            //return (max_moment == double.MinValue) ? 0.0d : max_moment;

            return mfrc;

        }

        public MaxForce GetMember_Max_Negative_Shear(string members)
        {
            return GetMember_Max_Negative_Shear(MyList.Get_Array_Intiger(members));

        }
        public MaxForce GetMember_Max_Negative_Shear(List<int> member_array)
        {

            MaxForce mfrc = new MaxForce();

            double max_shear = 0.0;

            for (int i = 0; i < member_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    bf.StartNodeForce.ForceType = ForceType;
                    bf.EndNodeForce.ForceType = ForceType;

                    if (bf.BeamNo == member_array[i])
                    {
                        if (bf.StartNodeForce.MaxShearForce < 0)
                        {
                            if (Math.Abs(max_shear) < Math.Abs(bf.StartNodeForce.MaxShearForce))
                            {
                                //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                                max_shear = bf.StartNodeForce.MaxShearForce;
                                mfrc.Force = max_shear;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = bf.StartNodeForce.JointNo;
                            }
                        }
                        if (bf.EndNodeForce.MaxShearForce < 0)
                        {
                            if (Math.Abs(max_shear) < Math.Abs(bf.EndNodeForce.MaxShearForce))
                            {
                                //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                                max_shear = bf.EndNodeForce.MaxShearForce;
                                mfrc.Force = max_shear;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = bf.EndNodeForce.JointNo;
                            }
                        }
                    }
                }
            }
            return mfrc;
        }

        public MaxForce GetJoint_Max_Negative_Shear(List<int> joint_array)
        {

            MaxForce mfrc = new MaxForce();

            double max_shear = 0.0;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    bf.StartNodeForce.ForceType = ForceType;
                    bf.EndNodeForce.ForceType = ForceType;

                    if (bf.StartNodeForce.JointNo == joint_array[i])
                    {
                        if (bf.StartNodeForce.MaxShearForce < 0)
                        {
                            if (Math.Abs(max_shear) < Math.Abs(bf.StartNodeForce.MaxShearForce))
                            {
                                //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                                max_shear = bf.StartNodeForce.MaxShearForce;
                                mfrc.Force = max_shear;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = bf.StartNodeForce.JointNo;
                            }
                        }
                    }
                    if (bf.EndNodeForce.JointNo == joint_array[i])
                    {
                        if (bf.EndNodeForce.MaxShearForce < 0)
                        {
                            if (Math.Abs(max_shear) < Math.Abs(bf.EndNodeForce.MaxShearForce))
                            {
                                //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                                max_shear = bf.EndNodeForce.MaxShearForce;
                                mfrc.Force = max_shear;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = bf.EndNodeForce.JointNo;
                            }
                        }
                    }
                }
            }
            return mfrc;
        }

        public MaxForce GetJoint_Max_Hogging(List<int> joint_array, List<int> loads)
        {
            MaxForce mfrc = new MaxForce();

            double max_moment = 0.0;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    list_beams[j].StartNodeForce.ForceType = ForceType;
                    list_beams[j].EndNodeForce.ForceType = ForceType;


                    if (!loads.Contains(list_beams[j].LoadNo)) continue;

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (list_beams[j].StartNodeForce.MaxBendingMoment < 0) continue;
                        if (Math.Abs(max_moment) < Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment))
                        {
                            //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                            max_moment = list_beams[j].StartNodeForce.MaxBendingMoment;
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }

                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (list_beams[j].EndNodeForce.MaxBendingMoment < 0) continue;
                        if (Math.Abs(max_moment) < Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment))
                        {
                            //max_moment = Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment);
                            max_moment = list_beams[j].EndNodeForce.MaxBendingMoment;
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }

                    }
                }
            }

            //return (max_moment == double.MinValue) ? 0.0d : max_moment;

            return mfrc;

        }
        public MaxForce GetJoint_Max_Sagging(List<int> joint_array, List<int> loads)
        {
            MaxForce mfrc = new MaxForce();

            double max_moment = 0.0;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    list_beams[j].StartNodeForce.ForceType = ForceType;
                    list_beams[j].EndNodeForce.ForceType = ForceType;

                    if (!loads.Contains(list_beams[j].LoadNo)) continue;

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (list_beams[j].StartNodeForce.MaxBendingMoment > 0) continue;
                        if (Math.Abs(max_moment) < Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment))
                        {
                            //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                            max_moment = list_beams[j].StartNodeForce.MaxBendingMoment;
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }

                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (list_beams[j].EndNodeForce.MaxBendingMoment > 0) continue;
                        if (Math.Abs(max_moment) < Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment))
                        {
                            //max_moment = Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment);
                            max_moment = list_beams[j].EndNodeForce.MaxBendingMoment;
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }
                    }
                }
            }

            //return (max_moment == double.MinValue) ? 0.0d : max_moment;

            return mfrc;

        }


        //Chiranjit [2013 07 26]
        public MaxForce GetJoint_MomentForce(int joint, bool is_long_girder)
        {
            MaxForce mfrc = new MaxForce();

            List<int> joint_array = new List<int>();
            joint_array.Add(joint);
            double max_moment = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    list_beams[j].StartNodeForce.ForceType = ForceType;
                    list_beams[j].EndNodeForce.ForceType = ForceType;


                    if (is_long_girder)
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z != m.EndNode.Z) continue;
                    }
                    else
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z == m.EndNode.Z) continue;
                    }

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment))
                        {
                            max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }

                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment))
                        {
                            max_moment = Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment);
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }

                    }
                }
            }

            //return (max_moment == double.MinValue) ? 0.0d : max_moment;

            return mfrc;

        }
        public MaxForce GetJoint_MomentForce(int joint, bool is_long_girder, int loadcase)
        {
            MaxForce mfrc = new MaxForce();

            List<int> joint_array = new List<int>();
            joint_array.Add(joint);
            double max_moment = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    if (list_beams[j].LoadNo != loadcase) continue;

                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    list_beams[j].StartNodeForce.ForceType = ForceType;
                    list_beams[j].EndNodeForce.ForceType = ForceType;


                    if (is_long_girder)
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z != m.EndNode.Z) continue;
                    }
                    else
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z == m.EndNode.Z) continue;
                    }

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment))
                        {
                            max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }

                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment))
                        {
                            max_moment = Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment);
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }

                    }
                }
            }

            //return (max_moment == double.MinValue) ? 0.0d : max_moment;

            return mfrc;

        }


        public MaxForce GetJoint_MomentForce(int joint, bool is_long_girder, bool is_negative)
        {
            MaxForce mfrc = new MaxForce();

            List<int> joint_array = new List<int>();
            joint_array.Add(joint);
            double max_moment = 0.0;
            //double max_moment = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];
                    list_beams[j].StartNodeForce.ForceType = ForceType;
                    list_beams[j].EndNodeForce.ForceType = ForceType;

                    if (is_long_girder)
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z != m.EndNode.Z) continue;
                    }
                    else
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z == m.EndNode.Z) continue;
                    }


                    if (is_negative && list_beams[j].StartNodeForce.MaxBendingMoment > 0) continue;
                    if (!is_negative && list_beams[j].StartNodeForce.MaxBendingMoment < 0) continue;

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (Math.Abs(max_moment) < Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment))
                        {
                            //max_moment = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                            max_moment = list_beams[j].StartNodeForce.MaxBendingMoment;
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }

                    }

                    if (is_negative && list_beams[j].StartNodeForce.MaxBendingMoment < 0) continue;
                    if (!is_negative && list_beams[j].StartNodeForce.MaxBendingMoment > 0) continue;

                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (Math.Abs(max_moment) < Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment))
                        {
                            //max_moment = Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment);
                            max_moment = list_beams[j].EndNodeForce.MaxBendingMoment ;
                            mfrc.Force = max_moment;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }

                    }
                }
            }

            //return (max_moment == double.MinValue) ? 0.0d : max_moment;

            return mfrc;

        }

        //Chiranjit [2013 05 05]
        public MaxForce GetJoint_ShearForce(List<int> joint_array, bool is_long_girder)
        {

            joint_array.Sort();
            MaxForce mfrc = new MaxForce();
            double max_shear = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    #region Chiranjit [2013 05 04]
                    #endregion Chiranjit [2013 05 04]


                    if (is_long_girder)
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z != m.EndNode.Z) continue;
                    }
                    else
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z == m.EndNode.Z) continue;
                    }

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {


                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;

                        if (max_shear < Math.Abs(list_beams[j].StartNodeForce.MaxShearForce))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.MaxShearForce);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;


                        if (max_shear < Math.Abs(list_beams[j].EndNodeForce.MaxShearForce))
                        {
                            max_shear = Math.Abs(list_beams[j].EndNodeForce.MaxShearForce);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }
                    }
                }
            }
            //return (max_shear == double.MinValue) ? 0.0d : max_shear;
            return mfrc;
        }


        public BeamMemberForce GetJoint_ShearForce_Corrs_BendingMoment(List<int> joint_array, bool is_long_girder)
        {

            joint_array.Sort();
            BeamMemberForce mfrc = new BeamMemberForce();
            double max_shear = 0.0;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    #region Chiranjit [2013 05 04]
                    #endregion Chiranjit [2013 05 04]


                    if (is_long_girder)
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z != m.EndNode.Z) continue;
                    }
                    else
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z == m.EndNode.Z) continue;
                    }

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {


                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;

                        if (Math.Abs(max_shear) < Math.Abs(list_beams[j].StartNodeForce.MaxShearForce))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.MaxShearForce);
                            mfrc = list_beams[j].StartNodeForce;
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;


                        if (Math.Abs(max_shear) < Math.Abs(list_beams[j].EndNodeForce.MaxShearForce))
                        {
                            max_shear = Math.Abs(list_beams[j].EndNodeForce.MaxShearForce);
                            mfrc = list_beams[j].EndNodeForce;
                        }
                    }
                }
            }
            //return (max_shear == double.MinValue) ? 0.0d : max_shear;
            return mfrc;
        }
        public BeamMemberForce GetJoint_BendingMoment_Corrs_ShearForce(List<int> joint_array, bool is_long_girder)
        {


            joint_array.Sort();
            BeamMemberForce mfrc = new BeamMemberForce();
            double max_shear = 0.0;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    #region Chiranjit [2013 05 04]
                    #endregion Chiranjit [2013 05 04]


                    if (is_long_girder)
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z != m.EndNode.Z) continue;
                    }
                    else
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z == m.EndNode.Z) continue;
                    }

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {

                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;

                        if (Math.Abs(max_shear) < Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                            mfrc = list_beams[j].StartNodeForce;
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;

                        if (Math.Abs(max_shear) < Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment))
                        {
                            max_shear = Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment);
                            mfrc = list_beams[j].EndNodeForce;
                        }
                    }
                }
            }
            //return (max_shear == double.MinValue) ? 0.0d : max_shear;
            return mfrc;
        }


        public BeamMemberForce GetJoint_MaxShearForce_Corrs_BendingMoment(List<int> joint_array, bool is_long_girder, bool is_negative)
        {

            joint_array.Sort();
            BeamMemberForce mfrc = new BeamMemberForce();
            double max_shear = 0.0;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    #region Chiranjit [2013 05 04]
                    #endregion Chiranjit [2013 05 04]


                    if (is_long_girder)
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z != m.EndNode.Z) continue;
                    }
                    else
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z == m.EndNode.Z) continue;
                    }
                     
                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {

                        if (is_negative)
                        {
                            if (list_beams[j].StartNodeForce.MaxShearForce >= 0) continue;
                        }
                        else
                        {
                            if (list_beams[j].StartNodeForce.MaxShearForce <= 0) continue;
                        }
                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;

                        if (Math.Abs(max_shear) < Math.Abs(list_beams[j].StartNodeForce.MaxShearForce))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.MaxShearForce);
                            mfrc = list_beams[j].StartNodeForce;
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (is_negative)
                        {
                            if (list_beams[j].EndNodeForce.MaxShearForce >= 0) continue;
                        }
                        else
                        {
                            if (list_beams[j].EndNodeForce.MaxShearForce <= 0) continue;
                        }
                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;


                        if (Math.Abs(max_shear) < Math.Abs(list_beams[j].EndNodeForce.MaxShearForce))
                        {
                            max_shear = Math.Abs(list_beams[j].EndNodeForce.MaxShearForce);
                            mfrc = list_beams[j].EndNodeForce;
                        }
                    }
                }
            }
            //return (max_shear == double.MinValue) ? 0.0d : max_shear;
            return mfrc;
        }

        public BeamMemberForce GetJoint_MaxBendingMoment_Corrs_ShearForce(List<int> joint_array, bool is_long_girder, bool is_negative)
        {
            joint_array.Sort();
            BeamMemberForce mfrc = new BeamMemberForce();
            double max_shear = 0.0;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {

                    if (is_long_girder)
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z != m.EndNode.Z) continue;
                    }
                    else
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z == m.EndNode.Z) continue;
                    }

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (is_negative)
                        {
                            if (list_beams[j].StartNodeForce.MaxBendingMoment >= 0) continue;
                        }
                        else
                        {
                            if (list_beams[j].StartNodeForce.MaxBendingMoment <= 0) continue;
                        }

                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;

                        if (Math.Abs(max_shear) < Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                            mfrc = list_beams[j].StartNodeForce;
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (is_negative)
                        {
                            if (list_beams[j].EndNodeForce.MaxBendingMoment > 0) continue;
                        }
                        else
                        {
                            if (list_beams[j].EndNodeForce.MaxBendingMoment < 0) continue;
                        }

                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;

                        if (Math.Abs(max_shear) < Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment))
                        {
                            max_shear = Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment);
                            mfrc = list_beams[j].EndNodeForce;
                        }
                    }
                }
            }
            //return (max_shear == double.MinValue) ? 0.0d : max_shear;
            return mfrc;
        }


        public BeamMemberForce GetJoint_MinimumShearForce_Corrs_BendingMoment(List<int> joint_array, bool is_long_girder, bool is_negative)
        {

            joint_array.Sort();
            BeamMemberForce mfrc = new BeamMemberForce();
            double max_shear = double.MaxValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    #region Chiranjit [2013 05 04]
                    #endregion Chiranjit [2013 05 04]


                    if (is_long_girder)
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z != m.EndNode.Z) continue;
                    }
                    else
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z == m.EndNode.Z) continue;
                    }

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {


                        if (is_negative)
                        {
                            if (list_beams[j].StartNodeForce.MaxShearForce >= 0) continue;
                        }
                        else
                        {
                            if (list_beams[j].StartNodeForce.MaxShearForce <= 0) continue;
                        }


                        

                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;

                        if (Math.Abs(max_shear) > Math.Abs(list_beams[j].StartNodeForce.MaxShearForce))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.MaxShearForce);
                            mfrc = list_beams[j].StartNodeForce;
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (is_negative)
                        {
                            if (list_beams[j].EndNodeForce.MaxShearForce >= 0) continue;
                        }
                        else
                        {
                            if (list_beams[j].EndNodeForce.MaxShearForce <= 0) continue;
                        }

                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;


                        if (Math.Abs(max_shear) > Math.Abs(list_beams[j].EndNodeForce.MaxShearForce))
                        {
                            max_shear = Math.Abs(list_beams[j].EndNodeForce.MaxShearForce);
                            mfrc = list_beams[j].EndNodeForce;
                        }
                    }
                }
            }
            //return (max_shear == double.MinValue) ? 0.0d : max_shear;
            return mfrc;
        }

        public BeamMemberForce GetJoint_MinimumBendingMoment_Corrs_ShearForce(List<int> joint_array, bool is_long_girder, bool is_negative)
        {


            joint_array.Sort();
            BeamMemberForce mfrc = new BeamMemberForce();
            double max_shear = double.MaxValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    if (is_long_girder)
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z != m.EndNode.Z) continue;
                    }
                    else
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z == m.EndNode.Z) continue;
                    }


                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (is_negative)
                        {
                            if (list_beams[j].StartNodeForce.MaxBendingMoment >= 0) continue;
                        }
                        else
                        {
                            if (list_beams[j].StartNodeForce.MaxBendingMoment <= 0) continue;
                        }

                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;

                        if (Math.Abs(max_shear) > Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment);
                            mfrc = list_beams[j].StartNodeForce;
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {

                        if (is_negative)
                        {
                            if (list_beams[j].EndNodeForce.MaxBendingMoment >= 0) continue;
                        }
                        else
                        {
                            if (list_beams[j].EndNodeForce.MaxBendingMoment <= 0) continue;
                        }

                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;

                        if (Math.Abs(max_shear) > Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment))
                        {
                            max_shear = Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment);
                            mfrc = list_beams[j].EndNodeForce;
                        }
                    }
                }
            }
            //return (max_shear == double.MinValue) ? 0.0d : max_shear;
            return mfrc;
        }

        public MaxForce GetJoint_ShearForce(int joint, bool is_long_girder)
        {
            List<int> joint_array = new List<int>();


            joint_array.Add(joint);


            joint_array.Sort();
            MaxForce mfrc = new MaxForce();
            double max_shear = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    #region Chiranjit [2013 05 04]
                    #endregion Chiranjit [2013 05 04]


                    if (is_long_girder)
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z != m.EndNode.Z) continue;
                    }
                    else
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z == m.EndNode.Z) continue;
                    }

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {


                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;

                        if (max_shear < Math.Abs(list_beams[j].StartNodeForce.MaxShearForce))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.MaxShearForce);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;


                        if (max_shear < Math.Abs(list_beams[j].EndNodeForce.MaxShearForce))
                        {
                            max_shear = Math.Abs(list_beams[j].EndNodeForce.MaxShearForce);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }
                    }
                }
            }
            //return (max_shear == double.MinValue) ? 0.0d : max_shear;
            return mfrc;
        }
        public MaxForce GetJoint_ShearForce(int joint, bool is_long_girder, bool is_negative)
        {
            List<int> joint_array = new List<int>();


            joint_array.Add(joint);


            joint_array.Sort();
            MaxForce mfrc = new MaxForce();
            //double max_shear = double.MinValue;
            double max_shear = 0.0;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    #region Chiranjit [2013 05 04]
                    #endregion Chiranjit [2013 05 04]


                    if (is_long_girder)
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z != m.EndNode.Z) continue;
                    }
                    else
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z == m.EndNode.Z) continue;
                    }


                    if (is_negative && list_beams[j].StartNodeForce.MaxShearForce > 0) continue;
                    if (!is_negative && list_beams[j].StartNodeForce.MaxShearForce < 0) continue;


                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {


                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;

                        if (Math.Abs(max_shear) < Math.Abs(list_beams[j].StartNodeForce.MaxShearForce))
                        {
                            //max_shear = Math.Abs(list_beams[j].StartNodeForce.MaxShearForce);
                            max_shear = list_beams[j].StartNodeForce.MaxShearForce;
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }
                    }
                    if (is_negative && list_beams[j].EndNodeForce.MaxShearForce > 0) continue;
                    if (!is_negative && list_beams[j].EndNodeForce.MaxShearForce < 0) continue;


                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;


                        if (Math.Abs(max_shear) < Math.Abs(list_beams[j].EndNodeForce.MaxShearForce))
                        {
                            //max_shear = Math.Abs(list_beams[j].EndNodeForce.MaxShearForce);
                            max_shear = list_beams[j].EndNodeForce.MaxShearForce;
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }
                    }
                }
            }
            //return (max_shear == double.MinValue) ? 0.0d : max_shear;
            return mfrc;
        }

        public MaxForce GetJoint_ShearForce(int joint, bool is_long_girder, int loadcase)
        {
            List<int> joint_array = new List<int>();


            joint_array.Add(joint);


            joint_array.Sort();
            MaxForce mfrc = new MaxForce();
            double max_shear = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    #region Chiranjit [2013 05 04]
                    #endregion Chiranjit [2013 05 04]

                    if (list_beams[j].LoadNo != loadcase) continue;

                    if (is_long_girder)
                    {
                        Member m = structure.Get_Member(list_beams[j].BeamNo);
                        if (m.StartNode.Z != m.EndNode.Z) continue;
                    }
                    else
                    {
                        //Member m = structure.Get_Member(list_beams[j].BeamNo);
                        //if (m.StartNode.Z == m.EndNode.Z) continue;
                    }

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {


                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;

                        if (max_shear < Math.Abs(list_beams[j].StartNodeForce.MaxShearForce))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.MaxShearForce);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        list_beams[j].StartNodeForce.ForceType = ForceType;
                        list_beams[j].EndNodeForce.ForceType = ForceType;


                        if (max_shear < Math.Abs(list_beams[j].EndNodeForce.MaxShearForce))
                        {
                            max_shear = Math.Abs(list_beams[j].EndNodeForce.MaxShearForce);
                            mfrc.Force = max_shear;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }
                    }
                }
            }
            //return (max_shear == double.MinValue) ? 0.0d : max_shear;
            return mfrc;
        }

        public MaxForce GetJoint_Torsion(int jointNo, int loadcase)
        {
            List<int> joint_array = new List<int>();
            joint_array.Add(jointNo);
            return GetJoint_Torsion(joint_array, loadcase);
        }
        public MaxForce GetJoint_Torsion(List<int> joint_array, int loadcase)
        {
            MaxForce mfrc = new MaxForce();


            joint_array.Sort();


            double max_torsion = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    if (list_beams[j].LoadNo != loadcase) continue;
                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];


                    list_beams[j].StartNodeForce.ForceType = ForceType;
                    list_beams[j].EndNodeForce.ForceType = ForceType;
                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        //Chiranjit [2012 06 25]
                        //if (list_beams[j].StartNodeForce.M1_Torsion < 0)
                        //{
                            if (max_torsion < Math.Abs(list_beams[j].StartNodeForce.M1_Torsion))
                            {
                                max_torsion = Math.Abs(list_beams[j].StartNodeForce.M1_Torsion);
                                mfrc.Force = max_torsion;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                            }
                        //}
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {

                        //if (list_beams[j].StartNodeForce.M1_Torsion < 0)
                        //{
                            if (max_torsion < Math.Abs(list_beams[j].EndNodeForce.M1_Torsion))
                            {
                                max_torsion = Math.Abs(list_beams[j].EndNodeForce.M1_Torsion);
                                mfrc.Force = max_torsion;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                            }
                        //}
                    }
                }
            }

            //return (max_moment == double.MinValue) ? 0.0d : max_moment;
            return mfrc;

        }

        public MaxForce GetJoint_MomentForce(int joint, int loadcase)
        {
            List<int> jnt_arr = new List<int>();
            jnt_arr.Add(joint);

            if (IsCurve) return GetJoint_MomentForce(jnt_arr, loadcase);
            return GetJoint_MomentForce(joint, true, loadcase);
        }

        public MaxForce GetJoint_MomentForce(List<int> joint_array, int loadcase)
        {
            MaxForce mfrc = new MaxForce();

            joint_array.Sort();

            double max_moment = 0;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    if (list_beams[j].LoadNo != loadcase) continue;

                    if (list_beams[j].StartNodeForce.JointNo != joint_array[i] &&
                        list_beams[j].EndNodeForce.JointNo != joint_array[i]) continue;


                    AstraBeamMember bf = new AstraBeamMember(ForceType);
                    bf = list_beams[j];


                    list_beams[j].StartNodeForce.ForceType = ForceType;
                    list_beams[j].EndNodeForce.ForceType = ForceType;
                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                       
                        //Chiranjit [2012 06 25]
                        //if (list_beams[j].StartNodeForce.MaxBendingMoment < 0)
                        //{
                        if (Math.Abs(max_moment) < Math.Abs(list_beams[j].StartNodeForce.MaxBendingMoment))
                        {
                            max_moment = list_beams[j].StartNodeForce.MaxBendingMoment;
                            mfrc.Force = max_moment;
                            mfrc.Stress = list_beams[j].EndNodeForce.MaxShearForce;
                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                        }
                        //}
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        //Chiranjit [2012 06 25]
                        //if (list_beams[j].StartNodeForce.MaxBendingMoment < 0)
                        if (Math.Abs(max_moment) < Math.Abs(list_beams[j].EndNodeForce.MaxBendingMoment))
                        {
                            max_moment = list_beams[j].EndNodeForce.MaxBendingMoment;
                            mfrc.Force = max_moment;
                            mfrc.Stress = list_beams[j].EndNodeForce.MaxShearForce;

                            mfrc.MemberNo = list_beams[j].BeamNo;
                            mfrc.Loadcase = list_beams[j].LoadNo;
                            mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                        }
                    }
                }
            }

            //return (max_moment == double.MinValue) ? 0.0d : max_moment;
            return mfrc;

        }
        public MaxForce GetJoint_ShearForce(List<int> joint_array, int loadcase)
        {
            MaxForce mfrc = new MaxForce();

            joint_array.Sort();

            double max_shear = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    if (list_beams[j].LoadNo == loadcase)
                    {
                        if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                        {
                            //if (max_shear < Math.Abs(list_beams[j].StartNodeForce.R2_Shear))
                            //{
                            //    max_shear = Math.Abs(list_beams[j].StartNodeForce.R2_Shear);


                            //    mfrc.Force = max_shear;
                            //    mfrc.Loadcase = list_beams[j].LoadNo;
                            //    mfrc.MemberNo = list_beams[j].BeamNo;
                            //    mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                            //}

                            //if (max_shear < Math.Abs(list_beams[j].StartNodeForce.R3_Shear))
                            //{
                            //    max_shear = Math.Abs(list_beams[j].StartNodeForce.R3_Shear);

                            //    mfrc.Force = max_shear;
                            //    mfrc.Loadcase = list_beams[j].LoadNo;
                            //    mfrc.MemberNo = list_beams[j].BeamNo;
                            //    mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                            //}

                            //Chiranjit [2012 02 08]
                            list_beams[j].StartNodeForce.ForceType = ForceType;
                            list_beams[j].EndNodeForce.ForceType = ForceType;

                            //if (list_beams[j].StartNodeForce.MaxShearForce < 0)
                            if (max_shear < Math.Abs(list_beams[j].StartNodeForce.MaxShearForce))
                            {
                                max_shear = Math.Abs(list_beams[j].StartNodeForce.MaxShearForce);

                                mfrc.Force = max_shear;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.NodeNo = list_beams[j].StartNodeForce.JointNo;
                            }

                        }
                        else if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                        {
                            list_beams[j].EndNodeForce.ForceType = ForceType;
                            list_beams[j].EndNodeForce.ForceType = ForceType;

                            //if (list_beams[j].StartNodeForce.MaxShearForce < 0)
                            if (max_shear < Math.Abs(list_beams[j].EndNodeForce.MaxShearForce))
                            {
                                max_shear = Math.Abs(list_beams[j].EndNodeForce.MaxShearForce);

                                mfrc.Force = max_shear;
                                mfrc.Loadcase = list_beams[j].LoadNo;
                                mfrc.MemberNo = list_beams[j].BeamNo;
                                mfrc.NodeNo = list_beams[j].EndNodeForce.JointNo;
                            }

                        }
                    }
                }
            }

            //return (max_shear == double.MinValue) ? 0.0d : max_shear;
            return mfrc;

        }

        public MaxForce GetJoint_ShearForce(int joint, int loadcase)
        {
            List<int> jnt_arr = new List<int>();
            jnt_arr.Add(joint);

            //return GetJoint_ShearForce(jnt_arr, loadcase,);
            if (IsCurve) return GetJoint_ShearForce(jnt_arr, loadcase);

            return GetJoint_ShearForce(joint, true, loadcase);
        }



        public AstraBeamMember Get_BeamMember_Force(int mem_no, int load_case)
        {
            AnalysisData ad = new AnalysisData();


            for (int i = 0; i < list_beams.Count; i++)
            {
                if (list_beams[i].BeamNo == mem_no &&
                    list_beams[i].LoadNo == load_case)
                    return list_beams[i];
            }

            return null;
            
        }
        public Hashtable MemberAnalysis { get; set; }



        void LoadData()
        {
            if (ReadFromFile())
                SetAnalysisData();
        }
        void RunThread()
        {
            ThreadStart ths = new ThreadStart(LoadData);
            thd = new Thread(ths);
            thd.Start();
            //thd.Join();
            //thd.
        }
        //public delegate void SetProgressValue(ProgressBar pbr, int val);

        //SetProgressValue spv;
        public void SetProgressBar(ProgressBar pbr, int val)
        {
            pbr.Value = val;
        }
    }
    public class TrussDesignResult
    {
        public TrussDesignResult()
        {
            CompressionForce = 0.0;
            TensileForce = 0.0;
            Moment = 0.0;
            Shear = 0.0;
            Result = "";
        }
        public double CompressionForce { get; set; }
        public double TensileForce { get; set; }
        public double Moment { get; set; }
        public double Shear { get; set; }
        public string Result { get; set; }
    }

    public enum eAstraMemberType
    {
        TRUSS = 0,
        BEAM = 1,
        CABLE = 2,
    }
    public class AstraMember
    {
        public AstraMember()
        {
            UserNo = -1;
            AstraMemberType = eAstraMemberType.TRUSS;
            AstraMemberNo = -1;
        }
        public int UserNo { get; set; }
        public eAstraMemberType AstraMemberType { get; set; }
        public int AstraMemberNo { get; set; }

        public override string ToString()
        {
            //return string.Format("{0}:: User's TRUSS Member number     1 ASTRA TRUSS Member number      1");
            return string.Format("{0}:: User's {0} Member number     {1} ASTRA {0} Member number      {2}", AstraMemberType, UserNo, AstraMemberNo);
        }
    }
    public class AstraTrussMember
    {
        //MEMBER    LOAD              STRESS               FORCE
        //1         1                     0.1504968266E+05    0.3611923837E+03
        public AstraTrussMember()
        {
            TrussMemberNo = -1;
            LoadNo = -1;
            Stress = -1.0;
            Force = -1.0;
        }
        public int TrussMemberNo { get; set; }
        public int LoadNo { get; set; }
        public double Stress { get; set; }
        public double Force { get; set; }
        public static AstraTrussMember Parse(string s)
        {
            string kStr = s.ToUpper();
            kStr = MyList.RemoveAllSpaces(kStr);

            MyList mlist = new MyList(kStr, ' ');

            AstraTrussMember ast_trss = new AstraTrussMember();
            ast_trss.TrussMemberNo = mlist.GetInt(0);
            //if (Last_Member_No != -1)
            //{
            //    if (ast_trss.TrussMemberNo != Last_Member_No)
            //    {
            //        ast_trss.TrussMemberNo = Last_Member_No;
            //    }
            //    Last_Member_No++;
            //}
            //else
            //    Last_Member_No = ast_trss.TrussMemberNo;

            ast_trss.LoadNo = mlist.GetInt(1);
            ast_trss.Stress = mlist.GetDouble(2);
            ast_trss.Force = mlist.GetDouble(3);
            return ast_trss;
        }
        static int Last_Member_No = -1;
    }
    public enum eAppliedSection
    {
        None = -1,
        Angle_Section1 = 0,
        Angle_Section2 = 1,
        Angle_Section3 = 2,
        Beam_Section1 = 3,
        Beam_Section2 = 4,
        Channel_Section1 = 5,
        Channel_Section2 = 6,
        Reactangular_Section = 7,
        Circular_Section = 8,
        Builtup_LongGirder = 9,
        Builtup_CrossGirder = 10,
    }

    public class AstraPlateMember
    {
    //   ELEMENT      LOAD             MEMBRANE STRESS COMPONENTS                      BENDING MOMENT COMPONENTS     
    //NUMBER      CASE        SXX             SYY             SXY            MXX             MYY             MXY  


    //     1         1 -0.32880141E+02 -0.21237268E+03 -0.13321289E+02  0.54702512E+02  0.67273299E+02 -0.12641519E+00
    //     1         2 -0.30895162E+02 -0.21237262E+03 -0.12241097E-10  0.54702514E+02  0.67273290E+02 -0.26702196E-10
    //     1         3 -0.34025843E+02 -0.21237274E+03 -0.17128905E+02  0.54702503E+02  0.67273301E+02 -0.94622247E-01

        public AstraPlateMember()
        {
            PlateNo = -1;
            LoadNo = -1;
            SXX = -1.0;
            SYY = -1.0;
            SXY = -1.0;
            MXX = -1.0;
            MYY = -1.0;
            MXY = -1.0;
        }
        public int PlateNo { get; set; }
        public int LoadNo { get; set; }
        public double SXX { get; set; }
        public double SYY { get; set; }
        public double SXY { get; set; }
        public double MXX { get; set; }
        public double MYY { get; set; }
        public double MXY { get; set; }
        public static AstraPlateMember Parse(string s)
        {
            string kStr = s.ToUpper();
            kStr = MyList.RemoveAllSpaces(kStr);

            MyList mlist = new MyList(kStr, ' ');

            AstraPlateMember ast_trss = new AstraPlateMember();

            int c = 0;
            ast_trss.PlateNo = mlist.GetInt(c++);

            ast_trss.LoadNo = mlist.GetInt(c++);
            ast_trss.SXX = mlist.GetDouble(c++);
            ast_trss.SYY = mlist.GetDouble(c++);
            ast_trss.SXY = mlist.GetDouble(c++);
            ast_trss.MXX = mlist.GetDouble(c++);
            ast_trss.MYY = mlist.GetDouble(c++);
            ast_trss.MXY = mlist.GetDouble(c++);
            return ast_trss;
        }
        static int Last_Member_No = -1;
    }

    public class AstraSolidMember
    {

        //.....8-NODE SOLID ELEMENT STRESSES 

        // ELEMENT   LOAD NO      FACE      SIG-XX      SIG-YY      SIG-ZZ      SIG-XY      SIG-YZ      SIG-ZX     SIG-MAX     SIG-MIN    S2/ANGLE
        //       1         1         0    9.83E+04    9.06E+04    7.87E+04    9.42E+04   -7.56E-01   -5.01E+00    1.89E+05    1.82E+02    7.87E+04

        public AstraSolidMember()
        {
            ElementNo = -1;
            LoadNo = -1;
            Face = 0;
            SIG_XX = -1.0;
            SIG_YY = -1.0;
            SIG_ZZ = -1.0;
            SIG_XY = -1.0;
            SIG_YZ = -1.0;
            SIG_ZX = -1.0;
            SIG_MAX = -1.0;
            SIG_MIN = -1.0;
            S2_Angle = -1.0;
        }
        public int ElementNo { get; set; }
        public int LoadNo { get; set; }
        public int Face { get; set; }
        public double SIG_XX { get; set; }
        public double SIG_YY { get; set; }
        public double SIG_ZZ { get; set; }
        public double SIG_XY { get; set; }
        public double SIG_YZ { get; set; }
        public double SIG_ZX { get; set; }
        public double SIG_MAX { get; set; }
        public double SIG_MIN { get; set; }
        public double S2_Angle { get; set; }
        public static AstraSolidMember Parse(string s)
        {

            string kStr = s.ToUpper();
            kStr = MyList.RemoveAllSpaces(kStr);

            MyList mlist = new MyList(kStr, ' ');

            AstraSolidMember solid = new AstraSolidMember();

            int c = 0;
            solid.ElementNo = mlist.GetInt(c++);

            solid.LoadNo = mlist.GetInt(c++);
            solid.Face = mlist.GetInt(c++);
            solid.SIG_XX = mlist.GetDouble(c++);
            solid.SIG_YY = mlist.GetDouble(c++);
            solid.SIG_ZZ = mlist.GetDouble(c++);
            solid.SIG_XY = mlist.GetDouble(c++);
            solid.SIG_YZ = mlist.GetDouble(c++);
            solid.SIG_ZX = mlist.GetDouble(c++);
            solid.SIG_MAX = mlist.GetDouble(c++);
            solid.SIG_MIN = mlist.GetDouble(c++);
            solid.S2_Angle = mlist.GetDouble(c++);
            return solid;
        }
        static int Last_Member_No = -1;
    }


    public class AstraBeamMember
    {
        //BEAM LOAD     AXIAL       SHEAR       SHEAR     TORSION     BENDING     BENDING
        //NO.  NO.        R1          R2          R3          M1          M2          M3
        //  1   1 -1.503E-01   5.740E-01   2.074E-01  -3.828E-02   1.599E-01   6.521E-02
        //         1.503E-01  -5.740E-01  -2.074E-01   3.828E-02  -1.405E+00   3.379E+00

        public AstraBeamMember(ReadForceType rft)
        {
            BeamNo = -1;
            LoadNo = -1;
            StartNodeForce = new BeamMemberForce();
            EndNodeForce = new BeamMemberForce();

            ForceType = rft;
            StartNodeForce.ForceType = rft;
            EndNodeForce.ForceType = rft;
        }
        public int BeamNo { get; set; }
        public int LoadNo { get; set; }
        public BeamMemberForce StartNodeForce { get; set; }
        public BeamMemberForce EndNodeForce { get; set; }
        public ReadForceType ForceType { get; set; }
        public double MaxCompressionForce
        {
            get
            {
                if (Math.Abs(StartNodeForce.MaxCompressionForce) > Math.Abs(EndNodeForce.MaxCompressionForce))
                    return StartNodeForce.MaxCompressionForce;
                return EndNodeForce.MaxCompressionForce;
            }
        }
        public double MaxTensileForce
        {
            get
            {
                if (Math.Abs(StartNodeForce.MaxTensileForce) > Math.Abs(EndNodeForce.MaxTensileForce))
                    return StartNodeForce.MaxTensileForce;
                return EndNodeForce.MaxTensileForce;
            }
        }
        public double MaxBendingMoment
        {
            get
            {
                StartNodeForce.ForceType = ForceType;
                EndNodeForce.ForceType = ForceType;

                if (Math.Abs(StartNodeForce.MaxBendingMoment) > Math.Abs(EndNodeForce.MaxBendingMoment))
                    return StartNodeForce.MaxBendingMoment;
                return EndNodeForce.MaxBendingMoment;
            }
        }
        public double MaxTorsion
        {
            get
            {
                StartNodeForce.ForceType = ForceType;
                EndNodeForce.ForceType = ForceType;

                if (Math.Abs(StartNodeForce.M1_Torsion) > Math.Abs(EndNodeForce.M1_Torsion))
                    return StartNodeForce.M1_Torsion;
                return EndNodeForce.M1_Torsion;
            }
        }
        public double MaxAxialForce
        {
            get
            {
                StartNodeForce.ForceType = ForceType;
                EndNodeForce.ForceType = ForceType;

                if (Math.Abs(StartNodeForce.MaxAxialForce) > Math.Abs(EndNodeForce.MaxAxialForce))
                    return StartNodeForce.MaxAxialForce;
                return EndNodeForce.MaxAxialForce;
            }
        }
        public double MaxShearForce
        {
            get
            {
                StartNodeForce.ForceType = ForceType;
                EndNodeForce.ForceType = ForceType;

                if (Math.Abs(StartNodeForce.MaxShearForce) > Math.Abs(EndNodeForce.MaxShearForce))
                    return StartNodeForce.MaxShearForce;
                return EndNodeForce.MaxShearForce;
            }
        }

    }
    public class BeamMemberForce
    {
        public BeamMemberForce()
        {
            JointNo = -1;
            R1_Axial = 0.0;
            R2_Shear = 0.0;
            R3_Shear = 0.0;
            M1_Torsion = 0.0;
            M2_Bending = 0.0;
            M3_Bending = 0.0;
            ForceType = new ReadForceType();
        }
        public int JointNo { get; set; }
        public double R1_Axial { get; set; }
        public double R2_Shear { get; set; }
        public double R3_Shear { get; set; }
        public double M1_Torsion { get; set; }
        public double M2_Bending { get; set; }
        public double M3_Bending { get; set; }
        //Chiranjit [2012 02 08]
        public ReadForceType ForceType { get; set; }
        public static BeamMemberForce Parse(string s)
        {
            string kStr = s.ToUpper();
            kStr = MyList.RemoveAllSpaces(kStr);
            MyList mList = new MyList(kStr, ' ');

            BeamMemberForce bf = new BeamMemberForce();
            if (mList.Count == 8)
            {

                bf.R1_Axial = mList.GetDouble(2);
                bf.R2_Shear = mList.GetDouble(3);
                bf.R3_Shear = mList.GetDouble(4);
                bf.M1_Torsion = mList.GetDouble(5);
                bf.M2_Bending = mList.GetDouble(6);
                bf.M3_Bending = mList.GetDouble(7);
            }
            else if (mList.Count == 6)
            {

                bf.R1_Axial = mList.GetDouble(0);
                bf.R2_Shear = mList.GetDouble(1);
                bf.R3_Shear = mList.GetDouble(2);
                bf.M1_Torsion = mList.GetDouble(3);
                bf.M2_Bending = mList.GetDouble(4);
                bf.M3_Bending = mList.GetDouble(5);
            }
            return bf;
        }

        public double MaxCompressionForce
        {
            get
            {

                #region Chiranjit [2012 02 08]
                double frc = 0.0;
                double comp_frc = 0.0;

                frc = R2_Shear;
                if (frc < 0)
                {
                    if (Math.Abs(frc) > Math.Abs(comp_frc))
                        comp_frc = frc;
                }
                frc = R3_Shear;
                if (frc < 0)
                {
                    if (Math.Abs(frc) > Math.Abs(comp_frc))
                        comp_frc = frc;
                }
                frc = M2_Bending;
                if (frc < 0)
                {
                    if (Math.Abs(frc) > Math.Abs(comp_frc))
                        comp_frc = frc;
                }
                frc = M3_Bending;
                if (frc < 0)
                {
                    if (Math.Abs(frc) > Math.Abs(comp_frc))
                        comp_frc = frc;
                }
                #endregion Chiranjit [2012 02 08]
                return comp_frc;
            }
        }
        public double MaxTensileForce
        {
            get
            {
                double frc = 0.0;
                double tens_frc = 0.0;

                frc = R2_Shear;
                if (frc > 0)
                {
                    if (Math.Abs(frc) > Math.Abs(tens_frc))
                        tens_frc = frc;
                }
                frc = R3_Shear;
                if (frc > 0)
                {
                    if (Math.Abs(frc) > Math.Abs(tens_frc))
                        tens_frc = frc;
                }
                frc = M2_Bending;
                if (frc > 0)
                {
                    if (Math.Abs(frc) > Math.Abs(tens_frc))
                        tens_frc = frc;
                }
                frc = M3_Bending;
                if (frc > 0)
                {
                    if (Math.Abs(frc) > Math.Abs(tens_frc))
                        tens_frc = frc;
                }

                return tens_frc;
            }
        }

      

        public double MaxBendingMoment
        {
            get
            {
                //Chiranjit [2012 02 08]

                if (ForceType.M2 && ForceType.M3)
                {
                    if (Math.Abs(M2_Bending) > Math.Abs(M3_Bending)) return M2_Bending;
                    return M3_Bending;
                }

                if (ForceType.M2 && !ForceType.M3)
                {
                    return M2_Bending;
                }
                else if (ForceType.M3 && !ForceType.M2)
                {
                    return M3_Bending;
                }

                return 0.0;

            }
        }
        public double MaxShearForce
        {
            get
            {
                if (ForceType.R2 && ForceType.R3)
                {
                    if (Math.Abs(R2_Shear) > Math.Abs(R3_Shear)) return R2_Shear;
                    return R3_Shear;
                }
                else if (ForceType.R2 && !ForceType.R3)
                {
                    return R2_Shear;
                }
                else if (!ForceType.R2 && ForceType.R3)
                {
                    return R3_Shear;
                }
                return 0.0;
            }
        }
        public double MaxAxialForce
        {
            get
            {
                return R1_Axial;
            }
        }
    }
    public class ReadForceType
    {
        public ReadForceType()
        {
            M1 = true;
            M2 = true;
            M3 = true;
            R1 = true;
            R2 = true;
            R3 = true;
        }
        public bool M1 { get; set; }
        public bool M2 { get; set; }
        public bool M3 { get; set; }
        public bool R1 { get; set; }
        public bool R2 { get; set; }
        public bool R3 { get; set; }
    }
    public class AnalysisData
    {
        //eAstraMemberType mType;
        public AnalysisData()
        {
            UserMemberNo = -1;
            AstraMemberNo = -1;
            LoadNo = -1;
            AstraMemberType = eAstraMemberType.TRUSS;

            CompressionForce = new MaxForce();
            TensileForce = new MaxForce();
            MaxBendingMoment = new MaxForce();
            MaxAxialForce = new MaxForce();
            MaxShearForce = new MaxForce();
            MaxCompStress = new MaxForce();
            MaxTensileStress = new MaxForce();
            MaxTorsion = new MaxForce();
            Result = "";

            MaxCompStress = new MaxForce();
        }
        public int  UserMemberNo { get; set; }
        public MaxForce CompressionForce { get; set; }
        public MaxForce TensileForce { get; set; }
        public int AstraMemberNo { get; set; }
        public int LoadNo { get; set; }
        public MaxForce MaxTorsion { get; set; }
        public MaxForce MaxBendingMoment { get; set; }
        public MaxForce MaxAxialForce { get; set; }
        public MaxForce MaxShearForce { get; set; }
        public eAstraMemberType AstraMemberType { get; set; }
        public string Result { get; set; }
        public MaxForce MaxCompStress { get; set; }
        public MaxForce MaxTensileStress { get; set; }

    
    }

    public class CompleteDesign : ICompleteDesign
    {
        public CompleteDesign()
        {
            Members = new MembersDesign();
            DeadLoads = new TotalDeadLoad();
            //TotalSteelWeight = 0.0;
            AddWeightPercent = 24.42;
            NoOfJointsAtTrussFloor = 0;
            NoOfJointsAtTrussFloor = 0;
            IsRailBridge = false;
            //NoOfEndJointsOnBothSideAtBottomChord = 0;
            //ForceEachInsideJoints = 0.0;
            //ForceEachEndJoint = 0.0;
            Is_Live_Load = Is_Super_Imposed_Dead_Load = Is_Dead_Load = false;

        }
        public MembersDesign Members { get; set; }
        public TotalDeadLoad DeadLoads { get; set; }
        public double TotalSteelWeight
        {
            get
            {
                return Members.Weight;
            }
        }
        public double AddWeightPercent { get; set; }
        public double TotalBridgeWeight
        {
            get
            {
                //return TotalSteelWeight + GussetAndLacingWeight + DeadLoads.Weight;
                
                
                
                //Chiranjit [2011 07 05]
                //Calculate Total Bridge weight
                double tot_wgt = 0;

                if (Is_Dead_Load)
                {
                    tot_wgt += TotalSteelWeight + GussetAndLacingWeight;
                }
                if (Is_Super_Imposed_Dead_Load)
                {
                    tot_wgt += DeadLoads.Weight;
                }
                return tot_wgt;
            }
        }
        public double GussetAndLacingWeight
        {
            get
            {
                return (TotalSteelWeight * AddWeightPercent / 100.0);
            }
        }
        //public int NoOfJointsAtTrussFloor { get; set; }
        public int NoOfInsideJointsOnBothSideAtBottomChord
        {
            get
            {
                return (NoOfJointsAtTrussFloor - NoOfEndJointsOnBothSideAtBottomChord);
            }
        }
        public int NoOfJointsAtTrussFloor { get; set; }
        public int NoOfEndJointsOnBothSideAtBottomChord
        {

            get
            {
                return 4;
            }
        }

        public double ForceEachInsideJoints
        {
            get
            {
                //return (TotalBridgeWeight / (NoOfJointsAtTrussFloor + 2.0));
                return (TotalBridgeWeight / (NoOfInsideJointsOnBothSideAtBottomChord + 2.0));
            }
        }

        public double ForceEachEndJoint
        {
            get
            {
                return ForceEachInsideJoints / 2.0;
            }

        }
        public bool IsRailBridge { get; set; }
        //Chiranjit[2011 07 05] 
        //For Defferent Loads like LL, SIDL, DL
        public bool Is_Live_Load { get; set; }
        public bool Is_Super_Imposed_Dead_Load { get; set; }
        public bool Is_Dead_Load { get; set; }

        public void ToStream(StreamWriter sw)
        {
            string kStr = "";
            try
            {
                //if(DeadLoads
                if (DeadLoads.Load_List.Count > 0 && DeadLoads.IsRailLoad == false)
                {
                    sw.WriteLine();
                    sw.WriteLine("---------------------------------------------");
                    sw.WriteLine("WEIGHT CALCULATION OF SUPER IMPOSED DEAD LOAD");
                    sw.WriteLine("---------------------------------------------");
                    sw.WriteLine();
                    DeadLoads.ToStream(sw);
                }
                if (DeadLoads.IsRailLoad)
                {
                    DeadLoads.ToStream(sw);
                }
                sw.WriteLine();
                sw.WriteLine("---------------------------------------------");
                sw.WriteLine("WEIGHT CALCULATION OF STEEL STRUCTURE LOAD");
                sw.WriteLine("---------------------------------------------");
                sw.WriteLine();
                Members.ToStream(sw);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Total Weight of Structural Steel  = {0:f3} kN = {1:f3} MTon", 
                    TotalSteelWeight, 
                    (TotalSteelWeight / 10.0), 
                    (TotalSteelWeight / 0.00981));
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Adding {0}% for Gusset Plates, Lacing etc  = {1:f3} kN = {2:f3} MTon", 
                    AddWeightPercent, 
                    GussetAndLacingWeight,
                    (GussetAndLacingWeight / 10.0), 
                    (GussetAndLacingWeight / 0.00981));
                sw.WriteLine();
                sw.WriteLine("Total Weight  = {0:f3} + {1:f3} = {2:f3} MTon",
                    (TotalSteelWeight / 10.0), (GussetAndLacingWeight / 10.0),
                    ((TotalSteelWeight / 10.0) + (GussetAndLacingWeight / 10.0)));

                sw.WriteLine();
                sw.WriteLine("Weight of Deck Slab + S.I.D.L  = {0:f3} kN = {1:f3} MTon",
                    DeadLoads.Weight, 
                    (DeadLoads.Weight / 10.0), 
                    (DeadLoads.Weight / 0.00981));
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Total Weight of Bridge = {0:f3} + {1:f3} + {2:f3} = {3:f3} kN = {4:f3} MTon",
                    (Is_Dead_Load) ? TotalSteelWeight : 0.0,
                    (Is_Dead_Load)? GussetAndLacingWeight : 0.0,
                    (Is_Super_Imposed_Dead_Load) ? DeadLoads.Weight : 0.0,
                    TotalBridgeWeight, 
                    (TotalBridgeWeight/10.0),
                    (TotalBridgeWeight/0.00981));
                sw.WriteLine();
                sw.WriteLine("No of inside Joints on both sides of Truss at Bottom Chord = {0}", NoOfInsideJointsOnBothSideAtBottomChord);
                sw.WriteLine();
                sw.WriteLine("No of End Joints on both sides of Truss at Bottom Chord = 4");
                sw.WriteLine();
                sw.WriteLine("Force in each inside joints = {0:f3}/({1} + 2) = {2:f3} kN",
                    TotalBridgeWeight, NoOfInsideJointsOnBothSideAtBottomChord, ForceEachInsideJoints);
                sw.WriteLine("Force in each End joints = {0:f3}/2 = {1:f3} kN", ForceEachInsideJoints, ForceEachEndJoint);
            }
            catch (Exception ex) { }
        }
        public void WriteGroupSummery(string file_name)
        {
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                WriteGroupSummery(sw);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void WriteGroupSummery(StreamWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("==============================================================");
            sw.WriteLine("                        DESIGN SECTION SUMMARY");
            sw.WriteLine("==============================================================");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine();
            string kStr = "";
            string format = "";
            format = "{0,-9} {1,-21} {2,-8} {3,-8} {4,-8} {5,-10} {6,12:f2} {7,12:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:e2} {13,10:e2}  {14,-7}";


            //sw.WriteLine(format, "      ", "      ", "     ", "     ", "      ", " Vertical", "  Member  ", " Capacity  ", " Member", "Capacity", " Member", "Capacity", "Member", "Required", "");
            //sw.WriteLine(format, "Member", "      ", " Top ", "Side ", "Bottom", "Stiffener", "Compressive", "Compressive", "Tensile ", "Tensile ", " Shear  ", " Shear  ", "Section ", "Secion  ", "Remarks");
            //sw.WriteLine(format, "Group", "Section", "Plate", "Plate", "Plate ", "  Plate  ", "  Force    ", "   Force   ", " Force  ", " Force  ", " Stress ", " Stress ", "Modulus ", "Modulus ", "       ");
            //sw.WriteLine(format, "-----", "-------", "-----", "-----", "------", "---------", "-----------", "-----------", "--------", "--------", "--------", "--------", "--------", "--------", "-------");

            //Chiranjit [2013 08 16]  Check Comp force to Comp Stress
            sw.WriteLine(format, "      ", "      ", "     ", "     ", "      ", " Vertical", "  Member  ", " Capacity  ", " Member", "Capacity", " Member", "Capacity", "Member", "Required", "");
            sw.WriteLine(format, "Member", "      ", " Top ", "Side ", "Bottom", "Stiffener", "Compressive", "Compressive", "Tensile ", "Tensile ", " Shear  ", " Shear  ", "Section ", "Secion  ", "Remarks");
            sw.WriteLine(format, "Group", "Section", "Plate", "Plate", "Plate ", "  Plate  ", "  Stress   ", "   Stress  ", " Force  ", " Force  ", " Stress ", " Stress ", "Modulus ", "Modulus ", "       ");
            sw.WriteLine(format, "-----", "-------", "-----", "-----", "------", "---------", "-----------", "-----------", "--------", "--------", "--------", "--------", "--------", "--------", "-------");

           

            bool flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.BottomChord)
                {
                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("         ASTRA Pro :    SUMMARY OF BOTTOM CHORDS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce.Force,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress/1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce.Force,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.BottomChordBracings)
                {
                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("            ASTRA Pro :   SUMMARY OF BOTTOM CHORD BRACINGS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }

                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.CantileverBrackets)
                {
                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("          ASTRA Pro :    SUMMARY OF CANTILEVER BRACKETS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }

                    kStr = string.Format(format,
                         item.Group.GroupName,
                         item.SectionDetails.ToString(),
                         item.SectionDetails.TopPlate.ToString(),
                         item.SectionDetails.SidePlate.ToString(),
                         item.SectionDetails.BottomPlate.ToString(),
                         item.SectionDetails.VerticalStiffenerPlate.ToString(),
                         //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                         item.MaxTensionForce,
                         item.Capacity_TensionForce,
                         item.Required_ShearStress,
                         item.Capacity_ShearStress,
                         item.Capacity_SectionModulus,
                         item.Required_SectionModulus,
                         item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }

            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.CrossGirder)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("          ASTRA Pro :    SUMMARY OF CROSS GIRDERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }

                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }


            #region Diagonal Members
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.DiagonalMember)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("           ASTRA Pro :   SUMMARY OF DIAGONAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            #endregion Diagonal Members

            #region Top Diagonal Members
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.TopDiagonalMember)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("           ASTRA Pro :   SUMMARY OF TOP DIAGONAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            #endregion Top Diagonal Members


            #region BOTTOM Diagonal Members
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.BottomDiagonalMember)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("           ASTRA Pro :   SUMMARY OF BOTTOM DIAGONAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            #endregion BOTTOM Diagonal Members


            #region Short Diagonal Members
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.ShortDiagonalMember)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("           ASTRA Pro :   SUMMARY OF SHORT DIAGONAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            #endregion Short Diagonal Members

            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.EndRakers)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("          ASTRA Pro :     SUMMARY OF END RAKERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }

                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.StringerBeam)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("        ASTRA Pro :      SUMMARY OF STRINGER BEAMS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }

                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        item.MaxCompForce,
                        item.Capacity_CompForce,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.TopChord)
                {


                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("        ASTRA Pro :    SUMMARY OF TOP CHORDS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                         item.Group.GroupName,
                         item.SectionDetails.ToString(),
                         item.SectionDetails.TopPlate.ToString(),
                         item.SectionDetails.SidePlate.ToString(),
                         item.SectionDetails.BottomPlate.ToString(),
                         item.SectionDetails.VerticalStiffenerPlate.ToString(),
                         //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                         item.MaxTensionForce,
                         item.Capacity_TensionForce,
                         item.Required_ShearStress,
                         item.Capacity_ShearStress,
                         item.Capacity_SectionModulus,
                         item.Required_SectionModulus,
                         item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.TopChordBracings)
                {


                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("         ASTRA Pro :    SUMMARY OF TOP CHORD BRACINGS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.VerticalMember)
                {


                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("         ASTRA Pro :    SUMMARY OF VERTICAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }



            #region TOP VERTICAL Members
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.TopVerticalMember)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("           ASTRA Pro :   SUMMARY OF TOP VERTICAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            #endregion Top VERTICAL Members


            #region BOTTOM VERTICAL Members
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.BottomVerticalMember)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("           ASTRA Pro :   SUMMARY OF BOTTOM VERTICAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            #endregion BOTTOM VERTICAL Members



            #region SHORT VERTICAL Members
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.ShortVerticalMember)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("           ASTRA Pro :   SUMMARY OF SHORT VERTICAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        //item.MaxCompForce,
                        //item.Capacity_CompForce,
                        Math.Abs(item.MaxCompForce.Stress / 1000),
                        item.Capacity_Compressive_Stress,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            #endregion SHORT VERTICAL Members



            sw.WriteLine();
            sw.WriteLine("-----------------------------------------------------------------------------");
            sw.WriteLine("            ASTRA Pro :   SUMMARY OF WEIGHTS");
            sw.WriteLine("-----------------------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("Total Weight of Structural Steel = {0:f3} kN  = {1:f3} MTon", TotalSteelWeight, (TotalSteelWeight / 10.0));
            sw.WriteLine();
            sw.WriteLine("Adding {0}% for Gusset Plates, Lacing etc = {1:f3} kN  = {2:f3} MTon", AddWeightPercent, GussetAndLacingWeight, GussetAndLacingWeight / 10.0);
            sw.WriteLine();
            sw.WriteLine("Weight of Deck Slab + Super Imposed Dead Load = {0:f3} kN  = {1:f3} MTon", DeadLoads.Weight, DeadLoads.Weight / 10.0);
            sw.WriteLine();
            sw.WriteLine("Total Weight of Bridge = {0:f3} + {1:f3} + {2:f3} = {3:f3} kN  = {4:f3} MTon",
                TotalSteelWeight, GussetAndLacingWeight, DeadLoads.Weight, TotalBridgeWeight, TotalBridgeWeight/10.0);
            sw.WriteLine();
            sw.WriteLine("No of inside Joints on both sides of Truss at Bottom Chord = {0}", NoOfInsideJointsOnBothSideAtBottomChord);
            sw.WriteLine();
            sw.WriteLine("No of End Joints on both sides of Truss at Bottom Chord = 4");
            sw.WriteLine();
            sw.WriteLine("Force in each inside joints = {0:f3}/({1} + 2) = {2:f3} kN",
                TotalBridgeWeight, NoOfInsideJointsOnBothSideAtBottomChord, ForceEachInsideJoints);
            sw.WriteLine("Force in each End joints = {0:f3}/2 = {1:f3} kN", ForceEachInsideJoints, ForceEachEndJoint);
          
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("==============================================================");
            sw.WriteLine("                      END DESIGN SECTION SUMMARY");
            sw.WriteLine("==============================================================");
            sw.WriteLine();

        }
        public void WriteForcesSummery(string file_name)
        {
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                WriteForcesSummery(sw);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }


        }
        public void WriteForcesSummery(StreamWriter sw)
        {
            //StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("==============================================================");
                sw.WriteLine("                        DESIGN FORCE SUMMARY");
                sw.WriteLine("==============================================================");
                sw.WriteLine();
                sw.WriteLine();

                string DL_SIDL_LL = "";

                DL_SIDL_LL = Is_Dead_Load ? "DL " : "";
                DL_SIDL_LL += Is_Super_Imposed_Dead_Load ? Is_Dead_Load ? " + SIDL " : "SIDL" : "";
                DL_SIDL_LL += Is_Live_Load ? (Is_Dead_Load || Is_Super_Imposed_Dead_Load) ? " + LL " : "LL" : "";

                sw.WriteLine("Applied force and Check for member section for " + DL_SIDL_LL);
                sw.WriteLine();
                string kStr = "";
                string format = "";

                //format = "{0,-18} {1,-25} {2,-15} {3,-15} {4,-15} {5,-20} {6,20:f2} {7,20:f2} {8,20:f2} {9,20:f2} {10,20:f2} {11,20:f2} {12,20:f2} {13,20:f2} {14,7}";
                //format = "{0,-18} {1,-25} {2,-10} {3,-10} {4,-10} {5,-15} {6,15:f2} {7,15:f2} {8,15:f2} {9,15:f2} {10,10:f2} {11,10:f2} {12,15:f2} {13,15:f2}   {14,-7}";
                //format = "{0,-9} {1,-20} {2,-10} {3,-10} {4,-10} {5,-10} {6,12:f2} {7,12:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}   {14,-7}";



                format = "{0,-10} {1,10:f3} {2,15:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,15:f3} {7,15:f3} {8,10:f3}";
                //      0             1                 2                  3                 4           5                6                   7              8
                //MemberGroup    TensionForce    CaompressionForce    TensileStress   CapTensileStress Result     CompressiveStress   CapCompressiveStress Result  
                sw.WriteLine(format,
                    "Member",
                    "Tension",
                    "Compression",
                    "Tensile",
                    "Capacity",
                    "",
                    "Compressive",
                    "Capacity",
                    "");
                sw.WriteLine(format,
                   "Group",
                   "Force",
                   "Force",
                   "Stress",
                   "Tensile",
                   "Result",
                   "Stress",
                   "Compression",
                   "Result");
                sw.WriteLine(format,
                     "",
                     "",
                     "",
                     "",
                     "Stress",
                     "",
                     "",
                     "Stress",
                     "");
                sw.WriteLine(format,
                                    "",
                                    " (kN)",
                                    " (kN)",
                                    " (N/sq.mm)",
                                    " (N/sq.mm)",
                                    "  ",
                                    " (N/sq.mm)",
                                    " (N/sq.mm)",
                                    "");

                sw.WriteLine();
                bool flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.BottomChord)
                    {
                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF BOTTOM CHORDS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                            item.Group.GroupName,
                            item.MaxTensionForce.Force,
                            item.MaxCompForce.Force,
                            item.Tensile_Stress,
                            item.Capacity_Tensile_Stress,
                            item.Result_Tensile_Stress,
                            item.Compressive_Stress,
                            item.Capacity_Compressive_Stress,
                            item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.BottomChordBracings)
                    {
                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF BOTTOM CHORD BRACINGS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce.Force,
                           item.MaxCompForce.Force,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.CantileverBrackets)
                    {
                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF CANTILEVER BRACKETS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce.Force,
                           item.MaxCompForce.Force,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }


                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.DiagonalMember)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF DIAGONAL MEMBERS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce.Force,
                           item.MaxCompForce.Force,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.EndRakers)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF END RAKERS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce.Force,
                           item.MaxCompForce.Force,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.TopChord)
                    {
                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF TOP CHORDS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce.Force,
                           item.MaxCompForce.Force,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.TopChordBracings)
                    {


                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF TOP CHORD BRACINGS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce.Force,
                           item.MaxCompForce.Force,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.VerticalMember)
                    {


                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF VERTICAL MEMBERS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce.Force,
                           item.MaxCompForce.Force,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                format = "{0,-10} {1,10:f3} {2,10:f3} {3,15:e3} {4,15:e3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3}";

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("-----------------------------------------------------------------------------");
                sw.WriteLine("                                 BEAM MEMBERS  ");
                sw.WriteLine("-----------------------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine(format,
                    "Member",
                    "Bending",
                    "Shear",
                    "Section",
                    "Capacity",
                    "",
                    "Shear",
                    "Capacity",
                    "");
                sw.WriteLine(format,
                   "Group",
                   "Moment",
                   "Force",
                   "Modulas",
                   "Section",
                   "Result",
                   "Stress",
                   "Shear",
                   "Result");
                sw.WriteLine(format,
                     "",
                     "",
                     "",
                     "",
                     "",
                     "",
                     "",
                     "Stress",
                     "");
                sw.WriteLine(format,
                                    "",
                                    " (kN-m)",
                                    " (kN)",
                                    " (cu.mm)",
                                    " (cu.mm)",
                                    "  ",
                                    " (N/sq.mm)",
                                    " (N/sq.mm)",
                                    "");
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.StringerBeam)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF STRINGER BEAMS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxBendingMoment.Force,
                           item.MaxShearForce.Force,
                           item.Required_SectionModulus,
                           item.Capacity_SectionModulus,
                           item.Result_Section_Modulas,
                           item.Capacity_ShearStress,
                           item.Required_ShearStress,
                           item.Result_Shear_Stress);
                        sw.WriteLine(kStr);
                    }
                }

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.CrossGirder)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF CROSS GIRDERS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                          item.Group.GroupName,
                          item.MaxBendingMoment.Force,
                          item.MaxShearForce.Force,
                          item.Required_SectionModulus,
                          item.Capacity_SectionModulus,
                          item.Result_Section_Modulas,
                          item.Capacity_ShearStress,
                          item.Required_ShearStress,
                          item.Result_Shear_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                //sw.WriteLine();
                //sw.WriteLine("-----------------------------------------------------------------------------");
                //sw.WriteLine("                        SUMMARY OF WEIGHTS");
                //sw.WriteLine("-----------------------------------------------------------------------------");
                //sw.WriteLine();
                //sw.WriteLine("Total Weight of Structural Steel = {0:f3} kN  = {1:f3} MTon", TotalSteelWeight, (TotalSteelWeight / 10.0));
                //sw.WriteLine();
                //sw.WriteLine("Adding {0}% for Gusset Plates, Lacing etc = {1:f3} kN  = {2:f3} MTon", AddWeightPercent, GussetAndLacingWeight, GussetAndLacingWeight / 10.0);
                //sw.WriteLine();
                //sw.WriteLine("Weight of Deck Slab + Super Imposed Dead Load = {0:f3} kN  = {1:f3} MTon", DeadLoads.Weight, DeadLoads.Weight / 10.0);
                //sw.WriteLine();
                //sw.WriteLine("Total Weight of Bridge = {0:f3} + {1:f3} + {2:f3} = {3:f3} kN  = {4:f3} MTon",
                //    TotalSteelWeight, GussetAndLacingWeight, DeadLoads.Weight, TotalBridgeWeight, TotalBridgeWeight / 10.0);
                //sw.WriteLine();
                //sw.WriteLine("No of inside Joints on both sides of Truss at Bottom Chord = {0}", NoOfInsideJointsOnBothSideAtBottomChord);
                //sw.WriteLine();
                //sw.WriteLine("No of End Joints on both sides of Truss at Bottom Chord = 4");
                //sw.WriteLine();
                //sw.WriteLine("Force in each inside joints = {0:f3}/({1} + 2) = {2:f3} kN",
                //    TotalBridgeWeight, NoOfInsideJointsOnBothSideAtBottomChord, ForceEachInsideJoints);
                //sw.WriteLine("Force in each End joints = {0:f3}/2 = {1:f3} kN", ForceEachInsideJoints, ForceEachEndJoint);

                sw.WriteLine();
                sw.WriteLine("==============================================================");
                sw.WriteLine("                      END DESIGN FORCE SUMMARY");
                sw.WriteLine("==============================================================");
                sw.WriteLine();
            }
            catch (Exception ex) { }
            finally
            {
                //sw.Flush();
                //sw.Close();
            }


        }
        public void WriteForces_Capacity_Summery(string file_name)
        {
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                WriteForces_Capacity_Summery(sw);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }


        }
        public void WriteForces_Capacity_Summery(StreamWriter sw)
        {
            try
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("==============================================================");
                sw.WriteLine("                        DESIGN FORCE SUMMARY");
                sw.WriteLine("==============================================================");
                sw.WriteLine();
                sw.WriteLine();

                string DL_SIDL_LL = "";

                DL_SIDL_LL = Is_Dead_Load ? "DL " : "";
                DL_SIDL_LL += Is_Super_Imposed_Dead_Load ? Is_Dead_Load ? " + SIDL " : "SIDL" : "";
                DL_SIDL_LL += Is_Live_Load ? (Is_Dead_Load || Is_Super_Imposed_Dead_Load) ? " + LL " : "LL" : "";

                sw.WriteLine("Applied force and Check for member section for " + DL_SIDL_LL);
                sw.WriteLine();
                string kStr = "";
                string format = "";



                //format = "{0,-10} {1,10:f3} {2,15:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,15:f3} {7,15:f3} {8,10:f3} {9,10:f3} {10,10:f3}";
                format = "{0,-10} {1,10:f3} {2,15:f3} {3,10:f3} {4,15:f3} {5,15:f3} {6,5:f3}";

                sw.WriteLine("");
                sw.WriteLine("-------------------------------------------------------------------------------------------");
                sw.WriteLine("Member    Compressive     Compressive     Compressive   Tensile     Tensile       Tensile");
                sw.WriteLine("Group        Force       Force Capacity      Result      Force   Force Capacity    Result");
                sw.WriteLine("             (kN)             (kN)                       (kN)         (kN)      ");
                sw.WriteLine("-------------------------------------------------------------------------------------------");
                
                bool flag = true;

                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.BottomChord)
                    {
                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF BOTTOM CHORDS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                        item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        //Math.Abs(item.MaxCompForce.Stress / 1000).ToString("f3"),
                        //item.Capacity_Compressive_Stress.ToString("f3"),


                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        //Math.Abs(item.MaxTensionForce.Stress / 1000).ToString("f3"),
                        //item.Capacity_Tensile_Stress.ToString("f3"),



                        item.Result_Tensile,
                        item.Result);


                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.BottomChordBracings)
                    {
                        if (flag)
                        {
                              sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF BOTTOM CHORD BRACINGS");
                              sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                        item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        //Math.Abs(item.MaxCompForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Compressive_Stress.ToString("f3"),


                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        //Math.Abs(item.MaxTensionForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Tensile_Stress.ToString("f3"),



                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.CantileverBrackets)
                    {
                        if (flag)
                        {
                              sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF CANTILEVER BRACKETS");
                              sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                                item.Group.GroupName,


                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        //Math.Abs(item.MaxCompForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Compressive_Stress.ToString("f3"),


                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        //Math.Abs(item.MaxTensionForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Tensile_Stress.ToString("f3"),



                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }


                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.DiagonalMember)
                    {

                        if (flag)
                        {
                              sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF DIAGONAL MEMBERS");
                              sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                         item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        //Math.Abs(item.MaxCompForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Compressive_Stress.ToString("f3"),


                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        //Math.Abs(item.MaxTensionForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Tensile_Stress.ToString("f3"),



                        item.Result_Tensile,
                        item.Result);


                        sw.WriteLine(kStr);
                    }
                }
                #region Short Diagonal

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.ShortDiagonalMember)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF SHORT DIAGONAL MEMBERS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                         item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }
                #endregion Short Diagonal


                #region TOP Diagonal

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.TopDiagonalMember)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF TOP DIAGONAL MEMBERS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                         item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }
                #endregion TOP Diagonal



                #region BOTTOM Diagonal

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.BottomDiagonalMember)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF BOTTOM DIAGONAL MEMBERS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                         item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }
                #endregion BOTTOM Diagonal


                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.EndRakers)
                    {

                        if (flag)
                        {
                              sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF END RAKERS");
                              sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                        item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        //Math.Abs(item.MaxCompForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Compressive_Stress.ToString("f3"),


                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        //Math.Abs(item.MaxTensionForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Tensile_Stress.ToString("f3"),



                        item.Result_Tensile,
                        item.Result);


                        sw.WriteLine(kStr);
                    }
                }

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.TopChord)
                    {
                        if (flag)
                        {
                              sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF TOP CHORDS");
                              sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                        item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        //Math.Abs(item.MaxCompForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Compressive_Stress.ToString("f3"),


                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        //Math.Abs(item.MaxTensionForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Tensile_Stress.ToString("f3"),



                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.TopChordBracings)
                    {


                        if (flag)
                        {
                              sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF TOP CHORD BRACINGS");
                              sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                        item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        //Math.Abs(item.MaxCompForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Compressive_Stress.ToString("f3"),


                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        //Math.Abs(item.MaxTensionForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Tensile_Stress.ToString("f3"),



                        item.Result_Tensile,
                        item.Result);


                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.VerticalMember)
                    {


                        if (flag)
                        {
                              sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF VERTICAL MEMBERS");
                              sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                       item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        //Math.Abs(item.MaxCompForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Compressive_Stress.ToString("f3"),


                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        //Math.Abs(item.MaxTensionForce.Stress / 1000).ToString("f3"),
                            //item.Capacity_Tensile_Stress.ToString("f3"),



                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }

                #region TOP VERTICAL

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.TopVerticalMember)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF TOP VERTICAL MEMBERS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                         item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }
                #endregion TOP VERTICAL



                #region BOTTOM VERTICAL

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.BottomVerticalMember)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF BOTTOM VERTICAL MEMBERS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                         item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }
                #endregion BOTTOM VERTICAL

                #region SHORT VERTICAL

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.ShortVerticalMember)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF SHORT VERTICAL MEMBERS");
                            sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                         item.Group.GroupName,

                        item.MaxCompForce.Force.ToString("f3"),
                        item.Capacity_CompForce.ToString("f3"),

                        item.Result_Compressive,


                        item.MaxTensionForce.Force.ToString("f3"),
                        item.Capacity_TensionForce.ToString("f3"),

                        item.Result_Tensile,
                        item.Result);

                        sw.WriteLine(kStr);
                    }
                }
                #endregion SHORT VERTICAL


                format = "{0,-10} {1,10:f3} {2,10:f3} {3,15:e3} {4,15:e3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3}";

                sw.WriteLine("-------------------------------------------------------------------------------------------");
                sw.WriteLine("-------------------------------------------------------------------------------------------");
                sw.WriteLine();
                  sw.WriteLine("-------------------------------------------------------------------------------------------");
                sw.WriteLine("                                 BEAM MEMBERS  ");
                  sw.WriteLine("-------------------------------------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine(format,
                    "Member",
                    "Bending",
                    "Shear",
                    "Section",
                    "Capacity",
                    "",
                    "Shear",
                    "Capacity",
                    "");
                sw.WriteLine(format,
                   "Group",
                   "Moment",
                   "Force",
                   "Modulas",
                   "Section",
                   "Result",
                   "Stress",
                   "Shear",
                   "Result");
                sw.WriteLine(format,
                     "",
                     "",
                     "",
                     "",
                     "",
                     "",
                     "",
                     "Stress",
                     "");
                sw.WriteLine(format,
                                    "",
                                    " (kN-m)",
                                    " (kN)",
                                    " (cu.mm)",
                                    " (cu.mm)",
                                    "  ",
                                    " (N/sq.mm)",
                                    " (N/sq.mm)",
                                    "");
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.StringerBeam)
                    {

                        if (flag)
                        {
                              sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF STRINGER BEAMS");
                              sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxBendingMoment.Force,
                           item.MaxShearForce.Force,
                           item.Required_SectionModulus,
                           item.Capacity_SectionModulus,
                           item.Result_Section_Modulas,
                           item.Capacity_ShearStress,
                           item.Required_ShearStress,
                           item.Result_Shear_Stress);
                        sw.WriteLine(kStr);
                    }
                }

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.CrossGirder)
                    {

                        if (flag)
                        {
                              sw.WriteLine("-------------------------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF CROSS GIRDERS");
                              sw.WriteLine("-------------------------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                          item.Group.GroupName,
                          item.MaxBendingMoment.Force,
                          item.MaxShearForce.Force,
                          item.Required_SectionModulus,
                          item.Capacity_SectionModulus,
                          item.Result_Section_Modulas,
                          item.Capacity_ShearStress,
                          item.Required_ShearStress,
                          item.Result_Shear_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                sw.WriteLine();
                sw.WriteLine("==============================================================");
                sw.WriteLine("                      END DESIGN FORCE SUMMARY");
                sw.WriteLine("==============================================================");
                sw.WriteLine();
            }
            catch (Exception ex) { }
            finally
            {
                //sw.Flush();
                //sw.Close();
            }

        }

        public void ReadFromFile(string file_name)
        {
            this.DeadLoads.ReadFromFile(file_name);
            this.Members.ReadFromFile(file_name);
        }
    }
    [Serializable]
    public class MembersDesign : IList<CMember>
    {
        List<CMember> list = null;

        public MembersDesign()
        {
            list = new List<CMember>();
        }

        #region IList<Member> Members

        public int IndexOf(string MemberNo)
        {
            

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Group.GroupName == MemberNo)
                {
                    return i;
                }
            }
            return -1;
        }

        public int IndexOf(CMember item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i].Group.GroupName == item.Group.GroupName))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, CMember item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public CMember this[int index]
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

        #region ICollection<Member> Members

        public void Add(CMember item)
        {
            try
            {
                if (IndexOf(item) != -1) throw new Exception("Member Group. " + item.Group.GroupName + " already exist.");
                if (item.Group.GroupName == "") throw new Exception("Invalid Member Information");
                list.Add(item);
            }
            catch (Exception ex) { }
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(CMember item)
        {
            return (IndexOf(item) != -1);
        }

        public void CopyTo(CMember[] array, int arrayIndex)
        {
            CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(CMember item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<Member> Members

        public IEnumerator<CMember> GetEnumerator()
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

        public void ReadFromFile(string file_name)
        {
            List<string> file_con = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            MyList mLIst = null;
            CMember mem = null;
            int index = 0;
            bool flag = false;
            bool flag2 = false;
            eMemberType memType = eMemberType.NoSelection;

            for (int i = 0; i < file_con.Count; i++)
            {
                kStr = file_con[i];

                if (kStr == "") continue;

                if (i >= 56)
                {
                    kStr = file_con[i];
                }

                //list[6].Capacity_ShearStress;
                kStr = MyList.RemoveAllSpaces(kStr.ToUpper());


                if (kStr == "") continue;
                mLIst = new MyList(kStr, ' ');
                //1      Section1     ISJB 150                              1.0000                1.0000     1.0000    
                //                    T PL 500 x 22   500        22         0.8628     1         

                //2      Section1     ISJB 150                              1.0000                1.0000     1.0000    
                //                    T PL 500 x 22   500        22         0.8628     1         

                //3      Section4     ISA 2020 x 3                          1.0000                1.0000     1.0000    
                //                    T PL 350 x 25   350        25         0.6864     1         
                //                    S PL 420 x 16   420        16         0.5271     2         
                //                    VS PL 120 x 25  120        25         0.2353     2         
                //                    S PL 320 x 10   320        10         0.2510    

                try
                {
                    if (mLIst.StringList[0].StartsWith("MEMBER"))
                    {
                        flag2 = true;
                        continue;
                    }
                    if (!flag2) continue;
                    if (mLIst.Count == 2 || mLIst.Count == 3)
                    {
                        kStr = kStr.ToUpper();
                        #region Selection Member
                        if (kStr == "TOP CHORD")
                        {
                            memType = eMemberType.TopChord; continue;
                        }
                        else if (kStr == "BOTTOM CHORD")
                        {
                            memType = eMemberType.BottomChord; continue;
                        }
                        else if (kStr == "STRINGER BEAM")
                        {
                            memType = eMemberType.StringerBeam;
                        }
                        else if (kStr == "CROSS GIRDER")
                        {
                            memType = eMemberType.CrossGirder;
                        }
                        else if (kStr == "END RAKERS")
                        {
                            memType = eMemberType.EndRakers;
                        }
                        else if (kStr == "DIAGONAL MEMBER")
                        {
                            memType = eMemberType.DiagonalMember;
                        }
                        else if (kStr == "SHORT DIAGONAL MEMBER")
                        {
                            memType = eMemberType.DiagonalMember;
                        }
                        else if (kStr == "TOP DIAGONAL MEMBER")
                        {
                            memType = eMemberType.TopDiagonalMember;
                        }
                        else if (kStr == "BOTTOM DIAGONAL MEMBER")
                        {
                            memType = eMemberType.BottomDiagonalMember;
                        }
                        else if (kStr == "VERTICAL MEMBER")
                        {
                            memType = eMemberType.VerticalMember;
                        }
                        else if (kStr == "TOP VERTICAL MEMBER")
                        {
                            memType = eMemberType.TopVerticalMember;
                        }
                        else if (kStr == "BOTTOM VERTICAL MEMBER")
                        {
                            memType = eMemberType.BottomVerticalMember;
                        }
                        else if (kStr == "SHORT VERTICAL MEMBER")
                        {
                            memType = eMemberType.ShortVerticalMember;
                        }
                        else if (kStr == "TOP CHORD BRACINGS")
                        {
                            memType = eMemberType.TopChordBracings;
                        }
                        else if (kStr == "BOTTOM CHORD BRACINGS")
                        {
                            memType = eMemberType.BottomChordBracings;
                        }
                        else if (kStr == "CANTILEVER BRACKETS")
                        {
                            memType = eMemberType.CantileverBrackets;
                        }
                        //Chiranjit [2014 01 22]
                        else if (kStr == "ARCH CABLE MEMBERS")
                        {
                            memType = eMemberType.ArchMembers;
                        }
                        else if (kStr == "ARCH MEMBERS")
                        {
                            memType = eMemberType.ArchMembers;
                        }
                        else if (kStr == "SUSPENSION CABLE MEMBERS")
                        {
                            memType = eMemberType.SuspensionCables;
                        }
                        else if (kStr == "TRANSVERS MEMBERS")
                        {
                            memType = eMemberType.TransverseMember;
                        }
                        #endregion Selection Member
                    }
                    else if (mLIst.Count == 14 && (mLIst.StringList[2] == "UKA" || mLIst.StringList[2] == "ISA"))
                    {
                        if (flag)
                        {
                            Add(mem);
                            flag = false;
                        }
                        mem = new CMember();
                        index = 0;
                        mem.Group.GroupName = mLIst.StringList[index]; index++;
                        mem.SectionDetails.DefineSection = GetDefineSection(mLIst.StringList[index]); index++;
                        mem.SectionDetails.SectionName = mLIst.StringList[index]; index++;
                        mem.SectionDetails.SectionCode = mLIst.StringList[index]; index++; index++;
                        mem.SectionDetails.AngleThickness = mLIst.GetDouble(index); index++;
                        mem.WeightPerMetre = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.NoOfElements = mLIst.GetDouble(index); index++; index++;
                        mem.SectionDetails.LateralSpacing = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.BoltDia = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.NoOfBolts = mLIst.GetInt(index); index++;
                        mem.Length = mLIst.GetDouble(index); index++;
                        //mem.Weight = mLIst.GetDouble(index); index++;

                        mem.MemberType = memType;
                        flag = true;
                    }

                    else if (mLIst.Count == 13 && (mLIst.StringList[2] == "CABLE" || mLIst.StringList[4] == "STRAND"))
                    {
                        if (flag)
                        {
                            Add(mem);
                            flag = false;
                        }
                        mem = new CMember();
                        index = 0;
                        mem.Group.GroupName = mLIst.StringList[index]; index++;
                        mem.SectionDetails.DefineSection = GetDefineSection(mLIst.StringList[index]); index++;
                        mem.SectionDetails.SectionName = mLIst.StringList[index]; index++;
                        mem.SectionDetails.SectionCode = mLIst.StringList[index]; index++; index++;
                        //mem.SectionDetails.AngleThickness = mLIst.GetDouble(index); index++;
                        mem.WeightPerMetre = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.NoOfElements = mLIst.GetDouble(index); index++; index++;
                        mem.SectionDetails.LateralSpacing = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.BoltDia = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.NoOfBolts = mLIst.GetInt(index); index++;
                        mem.Length = mLIst.GetDouble(index); index++;
                        //mem.Weight = mLIst.GetDouble(index); index++;

                        mem.MemberType = memType;
                        flag = true;
                    }
                    else if (mLIst.Count == 13 && (mLIst.StringList[2] == "STEEL" || mLIst.StringList[4] == "TUBE"))
                    {
                        if (flag)
                        {
                            Add(mem);
                            flag = false;
                        }
                        mem = new CMember();
                        index = 0;
                        mem.Group.GroupName = mLIst.StringList[index]; index++;
                        mem.SectionDetails.DefineSection = GetDefineSection(mLIst.StringList[index]); index++;
                        mem.SectionDetails.SectionName = mLIst.StringList[index]; index++;
                        mem.SectionDetails.SectionCode = mLIst.StringList[index]; index++; index++;
                        //mem.SectionDetails.AngleThickness = mLIst.GetDouble(index); index++;
                        mem.WeightPerMetre = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.NoOfElements = mLIst.GetDouble(index); index++; index++;
                        mem.SectionDetails.LateralSpacing = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.BoltDia = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.NoOfBolts = mLIst.GetInt(index); index++;
                        mem.Length = mLIst.GetDouble(index); index++;
                        //mem.Weight = mLIst.GetDouble(index); index++;

                        mem.MemberType = memType;
                        flag = true;
                    }
                    //if ((mLIst.Count == 12 || mLIst.Count == 14) && kStr.Contains("SECTION"))
                    else if ((mLIst.Count == 12) && kStr.Contains("SECTION"))
                    {
                        if (flag)
                        {
                            Add(mem);
                            flag = false;
                        }
                        mem = new CMember();
                        index = 0;
                        mem.Group.GroupName = mLIst.StringList[index]; index++;
                        mem.SectionDetails.DefineSection = GetDefineSection(mLIst.StringList[index]); index++;
                        mem.SectionDetails.SectionName = mLIst.StringList[index]; index++;
                        mem.SectionDetails.SectionCode = mLIst.StringList[index]; index++;
                        try
                        {
                            //mem.SectionDetails.AngleThickness = mLIst.GetDouble(index); index++;
                            //if (mLIst.Count == 14)
                            //{
                            //    index++;
                            //    index++;
                            mem.WeightPerMetre = mLIst.GetDouble(index); index++;
                            //}
                            //else if (mLIst.Count == 12)
                            //{
                            //    mem.WeightPerMetre = mLIst.GetDouble(index); index++;
                            //}
                            mem.SectionDetails.NoOfElements = mLIst.GetDouble(index); index++; index++;
                            mem.SectionDetails.LateralSpacing = mLIst.GetDouble(index); index++;
                            mem.SectionDetails.BoltDia = mLIst.GetDouble(index); index++;
                            mem.SectionDetails.NoOfBolts = mLIst.GetInt(index); index++;
                            mem.Length = mLIst.GetDouble(index); index++;
                            //mem.Weight = mLIst.GetDouble(index); index++;
                            mem.MemberType = memType;
                        }
                        catch (Exception ex) { }
                        flag = true;
                    }
                    //if ((mLIst.Count == 12 || mLIst.Count == 14) && kStr.Contains("SECTION"))
                    else if ((mLIst.Count == 14) && kStr.Contains("SECTION"))
                    {
                        if (flag)
                        {
                            Add(mem);
                            flag = false;
                        }
                        mem = new CMember();
                        index = 0;
                        mem.Group.GroupName = mLIst.StringList[index]; index++;
                        mem.SectionDetails.DefineSection = GetDefineSection(mLIst.StringList[index]); index++;
                        mem.SectionDetails.SectionName = mLIst.StringList[index]; index++;
                        mem.SectionDetails.SectionCode = mLIst.StringList[index]; index++;
                        try
                        {
                            //mem.SectionDetails.AngleThickness = mLIst.GetDouble(index); index++;
                            //if (mLIst.Count == 14)
                            //{
                            index++;
                            index++;
                            mem.WeightPerMetre = mLIst.GetDouble(index); index++;
                            //}
                            //else if (mLIst.Count == 12)
                            //{
                            //    mem.WeightPerMetre = mLIst.GetDouble(index); index++;
                            //}
                            mem.SectionDetails.NoOfElements = mLIst.GetDouble(index); index++; index++;
                            mem.SectionDetails.LateralSpacing = mLIst.GetDouble(index); index++;
                            mem.SectionDetails.BoltDia = mLIst.GetDouble(index); index++;
                            mem.SectionDetails.NoOfBolts = mLIst.GetInt(index); index++;
                            mem.Length = mLIst.GetDouble(index); index++;
                            //mem.Weight = mLIst.GetDouble(index); index++;
                            mem.MemberType = memType;
                        }
                        catch (Exception ex) { }
                        flag = true;
                    }
                    else if (mLIst.Count == 11 && mLIst.StringList[2] != "ISA" && mLIst.StringList[2] != "UKA")
                    {
                        switch (mLIst.StringList[0].ToUpper())
                        {
                            case "T":
                            case "C":
                                mem.SectionDetails.TopPlate.Width = mLIst.GetDouble(2);
                                mem.SectionDetails.TopPlate.Thickness = mLIst.GetDouble(4);
                                mem.SectionDetails.TopPlate.Length = mLIst.GetDouble(9);
                                mem.SectionDetails.TopPlate.TotalPlates = 1;
                                break;
                            case "B":
                                mem.SectionDetails.BottomPlate.Width = mLIst.GetDouble(2);
                                mem.SectionDetails.BottomPlate.Thickness = mLIst.GetDouble(4);
                                mem.SectionDetails.BottomPlate.Length = mLIst.GetDouble(9);
                                mem.SectionDetails.BottomPlate.TotalPlates = 1;
                                break;
                            case "S":
                                mem.SectionDetails.SidePlate.Width = mLIst.GetDouble(2);
                                mem.SectionDetails.SidePlate.Thickness = mLIst.GetDouble(4);
                                mem.SectionDetails.SidePlate.Length = mLIst.GetDouble(9);
                                mem.SectionDetails.SidePlate.TotalPlates = 2;
                                break;
                            case "VS":
                                mem.SectionDetails.VerticalStiffenerPlate.Width = mLIst.GetDouble(2);
                                mem.SectionDetails.VerticalStiffenerPlate.Thickness = mLIst.GetDouble(4);
                                mem.SectionDetails.VerticalStiffenerPlate.Length = mLIst.GetDouble(9);
                                mem.SectionDetails.VerticalStiffenerPlate.TotalPlates = 2;
                                break;
                        }
                    }
                    else if (mLIst.Count == 6 && mLIst.StringList[0] == "D" && mLIst.StringList[2] == "T")
                    {
                        switch (mLIst.StringList[0].ToUpper())
                        {
                            case "D":
                            case "T":
                                mem.SectionDetails.Cables_Strands.Diameter = mLIst.GetDouble(1);
                                mem.SectionDetails.Cables_Strands.Thickness = mLIst.GetDouble(3);
                                break;
                        }
                    }
                    else if (mLIst.Count == 4 && mLIst.StringList[0] == "D")
                    {
                        switch (mLIst.StringList[0].ToUpper())
                        {
                            case "D":
                                mem.SectionDetails.Cables_Strands.Diameter = mLIst.GetDouble(1);
                                break;
                        }
                    }

                }
                catch (Exception ex) { }
            }
            if (flag)
            {
                Add(mem);
                //flag = false;
            }
        }
        public void ToStream(StreamWriter sw)
        {
            string title_line1, title_line2;
            //StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");
                title_line1 = string.Format("{0, -16} {1, -12} {2, -15} {3, -10} {4, -10} {5, -10} {6, -10} {7, -10} {8, 10} {9, 10} {10, 10} {11, 10} {12, 10}",
                    "Member", //0
                    "Section",//1
                    "",//2
                    "Plate",//3
                    "Plate",//4
                    "Unit",//5
                    "No Of",//6
                    "No Of",//7
                    "Lateral",//8
                    "Bolt",//9
                    "No Of",//10
                    "Member",//11
                    "Member");//12
                sw.WriteLine(title_line1);

                //str = string.Format("{0, -6} {1, -15} {2, -12} {3, -15} {4, -10} {5, -10} {6, -10} {7, -10} {8, -10} {9, -10}",
                title_line2 = string.Format("{0, -16} {1, -12} {2, -15} {3, -10} {4, -10} {5, -10} {6, -10} {7, -10} {8, 10} {9, 10} {10, 10} {11, 10} {12, 10}",
                    "Group", //0
                    "Type",//1
                    "Element",//2
                    "Width",//3
                    "Thickness",//4
                    "Weight",//5
                    "Elements",//6
                    "Members",//7
                    "Spacing",//8
                    "Diameter",//9
                    "Bolt",//10
                    "Length",//11
                    "Weight");//12
                sw.WriteLine(title_line2);

                title_line2 = string.Format("{0, -16} {1, -12} {2, -15} {3, -10} {4, -10} {5, -10} {6, -10} {7, -10} {8, 10} {9, 10} {10, 10} {11, 10} {12, 10}",
                   "", //0
                   "",//1
                   "",//2
                   " (mm) ",//3
                   " (mm) ",//4
                   "(kN/m)",//5
                   "",//6
                   "",//7
                   " (mm) ",//8
                   " (mm) ",//9
                   "",//10
                   " (m) ",//11
                   "(kN)");//12
                sw.WriteLine(title_line2);
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");

                sw.WriteLine();

                int i = 0;
                double total_weight, weight;
                #region CROSS GIRDER
                weight = 0.0;
                total_weight = 0.0;
                bool print_flag = true;
                for (i = 0; i < list.Count; i++)
                {

                    if (list[i].MemberType == eMemberType.CrossGirder)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("CROSS GIRDER");
                            sw.WriteLine("------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion CROSS GIRDER
                weight = 0.0;
                print_flag = true;

                #region STRINGER BEAM
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.StringerBeam)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("STRINGER BEAM");
                            sw.WriteLine("-------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion STRINGER BEAM
                weight = 0.0;
                print_flag = true;

                #region BOTTOM CHORD
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.BottomChord)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("BOTTOM CHORD");
                            sw.WriteLine("------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion BOTTOM CHORD
                weight = 0.0;
                print_flag = true;

                #region TOP CHORD
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.TopChord)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("TOP CHORD");
                            sw.WriteLine("---------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion TOP CHORD
                weight = 0.0;
                print_flag = true;

                #region END RAKERS
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.EndRakers)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("END RAKERS");
                            sw.WriteLine("----------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion END RAKERS
                weight = 0.0;
                print_flag = true;




                #region DIAGONAL MEMBER
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.DiagonalMember)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("DIAGONAL MEMBER");
                            sw.WriteLine("---------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion DIAGONAL MEMBER
                weight = 0.0;
                print_flag = true;


                #region TOP  DIAGONAL MEMBER
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.TopDiagonalMember)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("TOP DIAGONAL MEMBER");
                            sw.WriteLine("-------------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion DIAGONAL MEMBER
                weight = 0.0;
                print_flag = true;


                #region BOTTOM  DIAGONAL MEMBER
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.BottomDiagonalMember)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("BOTTOM DIAGONAL MEMBER");
                            sw.WriteLine("----------------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion BOTTOM DIAGONAL MEMBER
                weight = 0.0;
                print_flag = true;





                #region VERTICAL MEMBER
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.VerticalMember)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("VERTICAL MEMBER");
                            sw.WriteLine("------------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion VERTICAL MEMBER
                weight = 0.0;
                print_flag = true;

                #region TOP VERTICAL MEMBER
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.TopVerticalMember)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("TOP VERTICAL MEMBER");
                            sw.WriteLine("-------------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion VERTICAL MEMBER
                weight = 0.0;
                print_flag = true;


                #region BOTTOM VERTICAL MEMBER
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.BottomVerticalMember)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("BOTTOM VERTICAL MEMBER");
                            sw.WriteLine("----------------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion VERTICAL MEMBER
                weight = 0.0;
                print_flag = true;



                #region SHORT VERTICAL MEMBER
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.ShortVerticalMember)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("SHORT VERTICAL MEMBER");
                            sw.WriteLine("---------------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion VERTICAL MEMBER
                weight = 0.0;
                print_flag = true;

                #region ARCH CABLE BRACINGS
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.ArchMembers)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("ARCH MEMBERS");
                            sw.WriteLine("-------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion ARCH CABLE BRACINGS
                weight = 0.0;
                print_flag = true;

                #region SUSPENSION CABLE MEMBERS
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.SuspensionCables)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("SUSPENSION CABLE MEMBERS");
                            sw.WriteLine("------------------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion SUSPENSION CABLE MEMBERS
                weight = 0.0;
                print_flag = true;

                #region TRANSVERS MEMBERS
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.TransverseMember)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("TRANSVERS MEMBERS");
                            sw.WriteLine("-----------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion TRANSVERS MEMBERS
                weight = 0.0;
                print_flag = true;


                #region TOP CHORD BRACINGS
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.TopChordBracings)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("TOP CHORD BRACINGS");
                            sw.WriteLine("------------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion TOP CHORD BRACINGS
                weight = 0.0;
                print_flag = true;

                #region BOTTOM CHORD BRACINGS
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.BottomChordBracings)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("BOTTOM CHORD BRACINGS");
                            sw.WriteLine("---------------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion BOTTOM CHORD BRACINGS
                weight = 0.0;
                print_flag = true;

                #region CANTILEVER BRACKETS
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.CantileverBrackets)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("CANTILEVER BRACKETS");
                            sw.WriteLine("-------------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion CANTILEVER BRACKETS


                #region TOWER MEMBER
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.TowerMember)
                    {
                        if (print_flag)
                        {
                            //sw.WriteLine("CANTILEVER BRACKETS");
                            //sw.WriteLine("-------------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion TOWER MEMBER


                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
            catch (Exception ex) { }
            //finally
            //{
            //    sw.Flush();
            //    sw.Close();
            //}

        }

        public eDefineSection GetDefineSection(string s)
        {
            eDefineSection ds = eDefineSection.NoSelection;
            switch (s.ToUpper())
            {
                case "SECTION1":
                    ds = eDefineSection.Section1;
                    break;
                case "SECTION2":
                    ds = eDefineSection.Section2;
                    break;
                case "SECTION3":
                    ds = eDefineSection.Section3;
                    break;
                case "SECTION4":
                    ds = eDefineSection.Section4;
                    break;
                case "SECTION5":
                    ds = eDefineSection.Section5;
                    break;
                case "SECTION6":
                    ds = eDefineSection.Section6;
                    break;
                case "SECTION7":
                    ds = eDefineSection.Section7;
                    break;
                case "SECTION8":
                    ds = eDefineSection.Section8;
                    break;
                case "SECTION9":
                    ds = eDefineSection.Section9;
                    break;
                case "SECTION10":
                    ds = eDefineSection.Section10;
                    break;
                case "SECTION11":
                    ds = eDefineSection.Section11;
                    break;
                case "SECTION12":
                    ds = eDefineSection.Section12;
                    break;
                case "SECTION13":
                    ds = eDefineSection.Section13;
                    break;
                case "SECTION14":
                    ds = eDefineSection.Section14;
                    break;
                case "SECTION15":
                    ds = eDefineSection.Section15;
                    break;
                case "SECTION16":
                    ds = eDefineSection.Section16;
                    break;
                case "SECTION17":
                    ds = eDefineSection.Section17;
                    break;
                case "SECTION18":
                    //ds = eDefineSection.Section15;
                    break;
            }
            return ds;
        }
        public eMemberType GetMemberType(string s)
        {
            eMemberType ds = eMemberType.NoSelection;
            if (s.ToUpper() == eMemberType.BottomChord.ToString().ToUpper())
                ds = eMemberType.BottomChord;
            else if (s.ToUpper() == eMemberType.BottomChordBracings.ToString().ToUpper())
                ds = eMemberType.BottomChordBracings;
            else if (s.ToUpper() == eMemberType.CantileverBrackets.ToString().ToUpper())
                ds = eMemberType.CantileverBrackets;
            else if (s.ToUpper() == eMemberType.CrossGirder.ToString().ToUpper())
                ds = eMemberType.CrossGirder;
            else if (s.ToUpper() == eMemberType.DiagonalMember.ToString().ToUpper())
                ds = eMemberType.DiagonalMember;
            else if (s.ToUpper() == eMemberType.EndRakers.ToString().ToUpper())
                ds = eMemberType.EndRakers;
            else if (s.ToUpper() == eMemberType.StringerBeam.ToString().ToUpper())
                ds = eMemberType.StringerBeam;
            else if (s.ToUpper() == eMemberType.TopChord.ToString().ToUpper())
                ds = eMemberType.TopChord;
            else if (s.ToUpper() == eMemberType.TopChordBracings.ToString().ToUpper())
                ds = eMemberType.TopChordBracings;
            else if (s.ToUpper() == eMemberType.VerticalMember.ToString().ToUpper())
                ds = eMemberType.VerticalMember;

            return ds;

        }
        //public void Serialise(string file_name)
        //{
        //    Stream stream = new FileStream(file_name, FileMode.Create);
        //    BinaryFormatter bfor = new BinaryFormatter();
        //    bfor.Serialize(stream, this);
        //}
        //public static MembersDesign DeSerialise(string file_name)
        //{
        //    MembersDesign cmp_des = null;

        //    Stream stream = new FileStream(file_name, FileMode.Open);
        //    try
        //    {
        //        BinaryFormatter bfor = new BinaryFormatter();
        //        cmp_des = (MembersDesign)bfor.Deserialize(stream);
        //    }
        //    catch (Exception ex) { }
        //    finally
        //    {
        //        stream.Close();
        //    }
        //    return cmp_des;
        //}

        public double Weight
        {
            get
            {
                double wgt = 0.0;
                for (int i = 0; i < list.Count; i++)
                {
                    wgt += list[i].Weight;
                }
                return wgt;
            }
        }

        public CMember Get_Member(string group_name)
        {
            foreach (var item in this)
            {
                if(item.Group.GroupName.ToUpper() == group_name)
                {
                    return item;
                }
            }
            return null;
        }
    }
    [Serializable]
    public class CMember
    {
        public CMember()
        {

            Group = new MemberGroup();
            MemberType = eMemberType.NoSelection;
            SectionDetails = new SectionData();
            Length = 0.0;
            //Weight = 0.0;
            WeightPerMetre = 0.0;
            Force = "";
            Result = "";

            Capacity_CompForce = 0.0;
            Capacity_TensionForce = 0.0;
            Capacity_SectionModulus = 0.0;
            Required_SectionModulus = 0.0;
            Required_ShearStress = 0.0;
            Capacity_ShearStress = 0.0;

            Tensile_Stress = 0.0;
            Capacity_Tensile_Stress = 0.0;
            Compressive_Stress = 0.0;
            Capacity_Compressive_Stress = 0.0;
            DesignReport = new List<string>();


            MemLengths = new List<double>();

            MaxBendingMoment = new MaxForce();
            MaxCompForce = new MaxForce();
            MaxAxialForce = new MaxForce();
            MaxShearForce = new MaxForce();
            MaxStress = new MaxForce();
            MaxTensionForce = new MaxForce();
            MaxTorsion = new MaxForce();
        }

        //Chiranjit [2013 08 19]
        public IApplication iApp;
        public CMember(IApplication app)
        {

            iApp = app;
            Group = new MemberGroup();
            MemberType = eMemberType.NoSelection;
            SectionDetails = new SectionData();
            MemLengths = new List<double>();

            Length = 0.0;
            //Weight = 0.0;
            WeightPerMetre = 0.0;
            Force = "";
            Result = "";

            Capacity_CompForce = 0.0;
            Capacity_TensionForce = 0.0;
            Capacity_SectionModulus = 0.0;
            Required_SectionModulus = 0.0;
            Required_ShearStress = 0.0;
            Capacity_ShearStress = 0.0;

            Tensile_Stress = 0.0;
            Capacity_Tensile_Stress = 0.0;
            Compressive_Stress = 0.0;
            Capacity_Compressive_Stress = 0.0;
            DesignReport = new List<string>();

            MaxBendingMoment = new MaxForce();
            MaxAxialForce = new MaxForce();
            MaxShearForce = new MaxForce();
            MaxCompForce = new MaxForce();
            MaxStress = new MaxForce();
            MaxTensionForce = new MaxForce();
            MaxTorsion = new MaxForce();
        }


        public MemberGroup Group { get; set; }
        //public string GroupName { get; set; }


        public override string ToString()
        {
            return Group.ToString();
        }
        public static eMemberType Get_MemberType(string groupName)
        {
            #region New Selections
            if (true)
            {
                //START                GROUP                DEFINITION                                
                //_L0L1                1                10                11                20
                //_L1L2                2                9                12                19
                //_L2L3                3                8                13                18
                //_L3L4                4                7                14                17
                //_L4L5                5                6                15                16
                //_U1U2                59                66                67                74
                //_U2U3                60                65                68                73
                //_U3U4                61                64                69                72
                //_U4U5                62                63                70                71
                //_L1U1                21                29                30                38
                //_L2U2                22                28                31                37
                //_L3U3                23                27                32                36
                //_L4U4                24                26                33                35
                //_L5U5                25                34                                
                //_ER                39                43                49                53
                //_L2U1                40                44                50                54
                //_L3U2                41                45                51                55
                //_L4U3                42                46                52                56
                //_L5U4                47                48                57                58
                //_TCS_ST                170                TO                178                
                //_TCS_DIA                179                TO                194                
                //_BCB                195                TO                214                
                //_STRINGER                75                TO                114                
                //_XGIRDER                115                TO                169                                                                                                
                //END                
                if (groupName.StartsWith("_BC") && !groupName.StartsWith("_BCB"))
                    return eMemberType.BottomChord;
                else if (groupName.StartsWith("_TC") && !groupName.StartsWith("_TCB") && !groupName.StartsWith("_TCS"))
                    return eMemberType.TopChord;
                else if (groupName.StartsWith("_DIA"))
                    return eMemberType.DiagonalMember;
                else if (groupName.StartsWith("_SHORT_DIA"))
                    return eMemberType.DiagonalMember;
                //return eMemberType.ShortDiagonalMember;
                else if (groupName.StartsWith("_VER"))
                    return eMemberType.VerticalMember;
                else if (groupName.StartsWith("_SHORT_VER"))
                    return eMemberType.VerticalMember;
                //return eMemberType.ShortVerticalMember;
                else if (groupName.StartsWith("_BCB"))
                    return eMemberType.BottomChordBracings;
                else if (groupName.StartsWith("_TCB"))
                    return eMemberType.TopChordBracings;
                else if (groupName.StartsWith("_ER"))
                    return eMemberType.EndRakers;

                switch (groupName)
                {
                    case "_L0L1":
                        return eMemberType.BottomChord;
                        break;
                    case "_L1L2":
                        return eMemberType.BottomChord;
                        break;
                    case "_L2L3":
                        return eMemberType.BottomChord;
                        break;
                    case "_L3L4":
                        return eMemberType.BottomChord;
                        break;
                    case "_L4L5":
                    case "_L5L6":
                    case "_L6L7":
                    case "_L7L8":
                    case "_L8L9":
                    case "_L9L10":
                    case "_L10L11":
                    case "_L11L12":
                    case "_L12L13":
                    case "_L13L14":
                    case "_L14L15":
                        return eMemberType.BottomChord;
                        break;
                    case "_U1U2":
                        return eMemberType.TopChord;
                        break;
                    case "_U2U3":
                        return eMemberType.TopChord;
                        break;
                    case "_U3U4":
                        return eMemberType.TopChord;
                        break;
                    case "_U4U5":
                    case "_U5U6":
                    case "_U6U7":
                    case "_U7U8":
                    case "_U8U9":
                    case "_U9U10":
                    case "_U11U12":
                    case "_U12U13":
                    case "_U13U14":
                    case "_U14U15":
                    case "_U15U16":
                        return eMemberType.TopChord;
                        break;
                    case "_L1U1":
                        return eMemberType.VerticalMember;
                        break;
                    case "_L2U2":
                        return eMemberType.VerticalMember;
                        break;
                    case "_L3U3":
                        return eMemberType.VerticalMember;
                        break;
                    case "_L4U4":
                        return eMemberType.VerticalMember;
                        break;
                    case "_L5U5":
                        return eMemberType.VerticalMember;
                        break;
                    case "_L6U6":
                        return eMemberType.VerticalMember;
                        break;
                    case "_L7U7":
                        return eMemberType.VerticalMember;
                        break;
                    case "_L8U8":
                        return eMemberType.VerticalMember;
                        break;
                    case "_L9U9":
                    case "_L10U10":
                    case "_L11U11":
                    case "_L12U12":
                    case "_L13U13":
                    case "_L14U14":
                    case "_L15U15":
                        return eMemberType.VerticalMember;
                        break;





                    case "_V1V2":
                    case "_V3V4":
                    case "_V5V6":
                    case "_V7V8":
                    case "_V9V10":
                    case "_V11V12":
                    case "_V13V14":
                    case "_V15V16":
                    case "_V17V18":
                    case "_V19V20":
                        return eMemberType.VerticalMember;
                        break;

                    case "_M1U1":
                    case "_M2U2":
                    case "_M3U3":
                    case "_M4U4":
                    case "_M5U5":
                    case "_M6U6":
                    case "_M7U7":
                    case "_M8U8":
                    case "_M9U9":
                    case "_M10U10":
                    case "_U1M1":
                    case "_U2M2":
                    case "_U3M3":
                    case "_U4M4":
                    case "_U5M5":
                    case "_U6M6":
                    case "_U7M7":
                    case "_U8M8":
                    case "_U9M9":
                    case "_U10M10":
                        return eMemberType.TopVerticalMember;


                    case "_M1L1":
                    case "_M2L2":
                    case "_M3L3":
                    case "_M4L4":
                    case "_M5L5":
                    case "_M6L6":
                    case "_M7L7":
                    case "_M8L8":
                    case "_M9L9":
                    case "_M10L10":
                    case "_L1M1":
                    case "_L2M2":
                    case "_L3M3":
                    case "_L4M4":
                    case "_L5M5":
                    case "_L6M6":
                    case "_L7M7":
                    case "_L8M8":
                    case "_L9M9":
                    case "_L10M10":
                        return eMemberType.BottomVerticalMember;



                    case "_M1L2":
                    case "_M2L3":
                    case "_M3L4":
                    case "_M4L5":
                    case "_M5L6":
                    case "_M6L7":
                    case "_M7L8":
                    case "_M8L9":
                    case "_M9L10":
                    case "_M10L11":
                    case "_M11L12":
                    case "_M12L13":
                    case "_M13L14":

                    case "_L2M1":
                    case "_L3M2":
                    case "_L4M3":
                    case "_L5M4":
                    case "_L6M5":
                    case "_L7M6":
                    case "_L8M7":
                    case "_L9M8":
                    case "_L10M9":
                    case "_L11M10":
                    case "_L12M11":
                    case "_L13M12":
                    case "_L14M13":
                        return eMemberType.BottomDiagonalMember;

                    case "_M1U2":
                    case "_M2U3":
                    case "_M3U4":
                    case "_M4U5":
                    case "_M5U6":
                    case "_M6U7":
                    case "_M7U8":
                    case "_M8U9":
                    case "_M9U10":
                    case "_M10U11":
                    case "_M11U12":
                    case "_M12U13":
                    case "_M13U14":

                    case "_U2M1":
                    case "_U3M2":
                    case "_U4M3":
                    case "_U5M4":
                    case "_U6M5":
                    case "_U7M6":
                    case "_U8M7":
                    case "_U9M8":
                    case "_U10M9":
                    case "_U11M10":
                    case "_U12M11":
                    case "_U13M12":
                    case "_U14M13":
                        return eMemberType.TopDiagonalMember;


                    case "_ER":
                        return eMemberType.EndRakers;
                        break;
                    case "_L2U1":
                        return eMemberType.DiagonalMember;
                        break;
                    case "_L3U2":
                        return eMemberType.DiagonalMember;
                        break;
                    case "_L4U3":
                    case "_L5U4":
                    case "_L6U5":
                    case "_L7U6":
                    case "_L8U7":
                    case "_L9U8":
                    case "_L10U9":
                    case "_L11U10":
                    case "_L12U11":
                    case "_L13U12":
                    case "_L14U13":
                    case "_L15U14":
                    case "_L16U15":

                        return eMemberType.DiagonalMember;
                        break;
                    case "_TCS_ST":
                    case "_TCS_ST1":
                    case "_TCS_ST2":
                    case "_TCS_ST3":
                    case "_TCS_ST4":
                    case "_TCS_ST5":
                    case "_TCS_ST6":
                        return eMemberType.TopChordBracings;
                    case "_TCS_DIA":
                    case "_TCS_DIA1":
                    case "_TCS_DIA2":
                    case "_TCS_DIA3":
                    case "_TCS_DIA4":
                    case "_TCS_DIA5":
                        return eMemberType.TopChordBracings;
                        break;
                    case "_BCB":
                    case "_BCB1":
                    case "_BCB2":
                    case "_BCB3":
                    case "_BCB4":
                    case "_BCB5":
                    case "_BCB6":
                    case "_BCB7":
                    case "_BCB8":
                    case "_BCB9":
                        return eMemberType.BottomChordBracings;
                    case "_STRINGERS":
                    case "_STRINGER":
                    case "_STRINGER1":
                    case "_STRINGER2":
                    case "_STRINGER3":
                    case "_STRINGER4":
                    case "_STRINGER5":
                    case "_STRINGER6":
                    case "_STRINGER7":
                    case "_STRINGER8":
                    case "_STRINGER9":
                        return eMemberType.StringerBeam;
                    case "_XGIRDER_IN":
                    case "_CROSSGIRDER":
                    case "_CROSSGIRDERS":
                    case "_XGIRDER":
                    case "_XGIRDERS":
                    case "_XGIR1":
                    case "_XGIR2":
                    case "_XGIR3":
                    case "_XGIR4":
                    case "_XGIR5":
                    case "_XGIR6":
                    case "_XGIRDER1":
                    case "_XGIRDER2":
                    case "_XGIRDER3":
                    case "_XGIRDER4":
                    case "_XGIRDER5":
                    case "_XGIRDER6":
                    case "_XGIRDER7":
                    case "_XGIRDER8":
                    case "_XGIRDER_END":
                        return eMemberType.CrossGirder;
                        break;
                }
                return eMemberType.NoSelection;
            }
            #endregion New Selections


            return eMemberType.NoSelection;



            //START                GROUP                DEFINITION                                
            //_L0L1                1                10                11                20
            //_L1L2                2                9                12                19
            //_L2L3                3                8                13                18
            //_L3L4                4                7                14                17
            //_L4L5                5                6                15                16
            //_U1U2                59                66                67                74
            //_U2U3                60                65                68                73
            //_U3U4                61                64                69                72
            //_U4U5                62                63                70                71
            //_L1U1                21                29                30                38
            //_L2U2                22                28                31                37
            //_L3U3                23                27                32                36
            //_L4U4                24                26                33                35
            //_L5U5                25                34                                
            //_ER                39                43                49                53
            //_L2U1                40                44                50                54
            //_L3U2                41                45                51                55
            //_L4U3                42                46                52                56
            //_L5U4                47                48                57                58
            //_TCS_ST                170                TO                178                
            //_TCS_DIA                179                TO                194                
            //_BCB                195                TO                214                
            //_STRINGER                75                TO                114                
            //_XGIRDER                115                TO                169                                                                                                
            //END                

            
                if (groupName.StartsWith("_BC") && !groupName.StartsWith("_BCB"))
                    return eMemberType.BottomChord;
                else if (groupName.StartsWith("_TC") && !groupName.StartsWith("_TCB") && !groupName.StartsWith("_TCS"))
                    return eMemberType.TopChord;
                else if (groupName.StartsWith("_DIA"))
                    return eMemberType.DiagonalMember;
                else if (groupName.StartsWith("_SHORT_DIA"))
                    return eMemberType.DiagonalMember;
                //return eMemberType.ShortDiagonalMember;
                else if (groupName.StartsWith("_VER"))
                    return eMemberType.VerticalMember;
                else if (groupName.StartsWith("_SHORT_VER"))
                    return eMemberType.VerticalMember;
                //return eMemberType.ShortVerticalMember;
                else if (groupName.StartsWith("_BCB"))
                    return eMemberType.BottomChordBracings;
                else if (groupName.StartsWith("_TCB"))
                    return eMemberType.TopChordBracings;
                else if (groupName.StartsWith("_ER"))
                    return eMemberType.EndRakers;


            switch (groupName)
            {
                case "_L0L1":
                    return eMemberType.BottomChord;
                    break;
                case "_L1L2":
                    return eMemberType.BottomChord;
                    break;
                case "_L2L3":
                    return eMemberType.BottomChord;
                    break;
                case "_L3L4":
                    return eMemberType.BottomChord;
                    break;
                case "_L4L5":
                case "_L5L6":
                case "_L6L7":
                case "_L7L8":
                case "_L8L9":
                case "_L9L10":
                case "_L10L11":
                case "_L11L12":
                case "_L12L13":
                case "_L13L14":
                case "_L14L15":
                    return eMemberType.BottomChord;
                    break;
                case "_U1U2":
                    return eMemberType.TopChord;
                    break;
                case "_U2U3":
                    return eMemberType.TopChord;
                    break;
                case "_U3U4":
                    return eMemberType.TopChord;
                    break;
                case "_U4U5":
                case "_U5U6":
                case "_U6U7":
                case "_U7U8":
                case "_U8U9":
                case "_U9U10":
                case "_U11U12":
                case "_U12U13":
                case "_U13U14":
                case "_U14U15":
                case "_U15U16":
                    return eMemberType.TopChord;
                    break;
                case "_L1U1":
                    return eMemberType.VerticalMember;
                    break;
                case "_L2U2":
                    return eMemberType.VerticalMember;
                    break;
                case "_L3U3":
                    return eMemberType.VerticalMember;
                    break;
                case "_L4U4":
                    return eMemberType.VerticalMember;
                    break;
                case "_L5U5":
                    return eMemberType.VerticalMember;
                    break;
                case "_L6U6":
                    return eMemberType.VerticalMember;
                    break;
                case "_L7U7":
                    return eMemberType.VerticalMember;
                    break;
                case "_L8U8":
                    return eMemberType.VerticalMember;
                    break;
                case "_L9U9":
                case "_L10U10":
                case "_L11U11":
                case "_L12U12":
                case "_L13U13":
                case "_L14U14":
                case "_L15U15":
                    return eMemberType.VerticalMember;
                    break;
                case "_V1V2":
                case "_V3V4":
                case "_V5V6":
                case "_V7V8":
                case "_V9V10":
                case "_V11V12":
                case "_V13V14":
                case "_V15V16":
                case "_V17V18":
                case "_V19V20":
                    return eMemberType.VerticalMember;
                    break;
                case "_ER":
                    return eMemberType.EndRakers;
                    break;
                case "_L2U1":
                    return eMemberType.DiagonalMember;
                    break;
                case "_L3U2":
                    return eMemberType.DiagonalMember;
                    break;
                case "_L4U3":
                case "_L5U4":
                case "_L6U5":
                case "_L7U6":
                case "_L8U7":
                case "_L9U8":
                case "_L10U9":
                case "_L11U10":
                case "_L12U11":
                case "_L13U12":
                case "_L14U13":
                case "_L15U14":
                case "_L16U15":

                    return eMemberType.DiagonalMember;
                    break;
                case "_TCS_ST":
                case "_TCS_ST1":
                case "_TCS_ST2":
                case "_TCS_ST3":
                case "_TCS_ST4":
                case "_TCS_ST5":
                case "_TCS_ST6":
                    return eMemberType.TopChordBracings;
                case "_TCS_DIA":
                case "_TCS_DIA1":
                case "_TCS_DIA2":
                case "_TCS_DIA3":
                case "_TCS_DIA4":
                case "_TCS_DIA5":
                    return eMemberType.TopChordBracings;
                    break;
                case "_BCB":
                case "_BCB1":
                case "_BCB2":
                case "_BCB3":
                case "_BCB4":
                case "_BCB5":
                case "_BCB6":
                case "_BCB7":
                case "_BCB8":
                case "_BCB9":
                    return eMemberType.BottomChordBracings;
                case "_STRINGERS":
                case "_STRINGER":
                case "_STRINGER1":
                case "_STRINGER2":
                case "_STRINGER3":
                case "_STRINGER4":
                case "_STRINGER5":
                case "_STRINGER6":
                case "_STRINGER7":
                case "_STRINGER8":
                case "_STRINGER9":
                    return eMemberType.StringerBeam;
                case "_XGIRDER_IN":
                case "_CROSSGIRDER":
                case "_CROSSGIRDERS":
                case "_XGIRDER":
                case "_XGIRDERS":
                case "_XGIR1":
                case "_XGIR2":
                case "_XGIR3":
                case "_XGIR4":
                case "_XGIR5":
                case "_XGIR6":
                case "_XGIRDER1":
                case "_XGIRDER2":
                case "_XGIRDER3":
                case "_XGIRDER4":
                case "_XGIRDER5":
                case "_XGIRDER6":
                case "_XGIRDER7":
                case "_XGIRDER8":
                case "_XGIRDER_END":
                    return eMemberType.CrossGirder;
                    break;
            }
            return eMemberType.NoSelection;
        }
        public void Add_SectionsData(ref CMember member)
        {
            member.SectionDetails.TopPlate.TotalPlates = 1;
            member.SectionDetails.BottomPlate.TotalPlates = 1;
            member.SectionDetails.SidePlate.TotalPlates = 2;
            member.SectionDetails.VerticalStiffenerPlate.TotalPlates = 2;

            switch (member.Group.GroupName)
            {
                #region Bottom Chord
                case "_L0L1":
                case "_L1L2":
                    member.SectionDetails.DefineSection = eDefineSection.Section4;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0.0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;


                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 480.0;
                    member.SectionDetails.SidePlate.Thickness = 10.0;

                    member.SectionDetails.VerticalStiffenerPlate.Width = 300.0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 25.0;

                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    member.SectionDetails.NoOfElements = 4.0;


                    break;
                case "_L2L3":
                    member.SectionDetails.DefineSection = eDefineSection.Section4;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0.0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;


                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 480.0;
                    member.SectionDetails.SidePlate.Thickness = 10.0;

                    member.SectionDetails.VerticalStiffenerPlate.Width = 300;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 25.0;

                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    member.SectionDetails.NoOfElements = 4.0;


                    break;
                case "_L3L4":


                    member.SectionDetails.DefineSection = eDefineSection.Section4;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.VerticalStiffenerPlate.Width = 300.0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 480.0;
                    member.SectionDetails.SidePlate.Thickness = 10.0;

                    member.SectionDetails.TopPlate.Width = 0.0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;


                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    member.SectionDetails.NoOfElements = 4.0;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section4;
                    //cmb_code1.SelectedItem = "150150";
                    //cmb_sec_thk.SelectedItem = 10.0d;

                    //txt_vsp_wd.Text = "300";
                    //txt_vsp_thk.Text = "25";
                    //txt_sp_wd.Text = "600";
                    //txt_sp_thk.Text = "10";
                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";
                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_L4L5":
                case "_L5L6":
                case "_L6L7":
                case "_L7L8":
                case "_L8L9":
                case "_L9L10":
                case "_L10L11":
                case "_L11L12":
                case "_L12L13":
                case "_L13L14":
                case "_L14L15":
                case "_L15L16":
                    member.SectionDetails.DefineSection = eDefineSection.Section4;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.VerticalStiffenerPlate.Width = 300;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 480.0;
                    member.SectionDetails.SidePlate.Thickness = 10.0;

                    member.SectionDetails.TopPlate.Width = 0.0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;


                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    member.SectionDetails.NoOfElements = 4.0;

                    break;
                #endregion Bottom Chord

                #region Top Chord

                case "_U1U2":


                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section3;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.SidePlate.Width = 420.0;
                    member.SectionDetails.SidePlate.Thickness = 16.0;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;


                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    member.SectionDetails.NoOfElements = 4.0;

                    break;
                case "_U2U3":

                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section3;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.SidePlate.Width = 420.0;
                    member.SectionDetails.SidePlate.Thickness = 16.0;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;


                    break;
                case "_U3U4":
                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section3;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.SidePlate.Width = 400.0;
                    member.SectionDetails.SidePlate.Thickness = 16.0;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;


                    break;
                case "_U4U5":
                case "_U5U6":
                case "_U6U7":
                case "_U7U8":
                case "_U8U9":
                case "_U9U10":
                case "_U10U11":
                case "_U11U12":
                case "_U12U13":
                case "_U13U14":
                case "_U14U15":
                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section3;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.SidePlate.Width = 420.0;
                    member.SectionDetails.SidePlate.Thickness = 16.0;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section3;
                    //cmb_code1.SelectedItem = "150150";
                    //cmb_sec_thk.SelectedItem = 12.0d;

                    //txt_tp_width.Text = "420";
                    //txt_tp_thk.Text = "16";

                    //txt_sp_wd.Text = "350";
                    //txt_sp_thk.Text = "30";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                #endregion Top Chord

                #region Vertical

                case "_L1U1":
                    member.SectionDetails.NoOfElements = 2.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "300";
                    //mem.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 240.0;
                    //member.SectionDetails.SidePlate.Thickness = 10.0;
                    member.SectionDetails.SidePlate.Thickness = 12.0; //Chiranjit [2013 05 31] Kolkata


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;


                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    //cmb_code1.SelectedItem = "200";
                    ////cmb_sec_thk.SelectedItem = "12";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_L2U2":

                    member.SectionDetails.NoOfElements = 2.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "300";
                    //mem.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 240.0;
                    member.SectionDetails.SidePlate.Thickness = 10.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    //cmb_code1.SelectedItem = "400";
                    ////cmb_sec_thk.SelectedItem = "12";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_L3U3":

                    member.SectionDetails.NoOfElements = 2.0;

                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "300";
                    //mem.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    //cmb_code1.SelectedItem = "300";
                    ////cmb_sec_thk.SelectedItem = "12";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;
                case "_L4U4":
                    member.SectionDetails.NoOfElements = 2.0;

                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "300";
                    //mem.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 240.0;
                    member.SectionDetails.SidePlate.Thickness = 10.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;


                    break;
                case "_L5U5":
                case "_L6U6":
                case "_L7U7":
                case "_L8U8":
                case "_L9U9":
                case "_L10U10":
                case "_L11U11":
                case "_L12U12":
                case "_L13U13":
                case "_L14U14":
                case "_L15U15":
                case "_V1V2":
                case "_V3V4":
                case "_V5V6":
                case "_V7V8":
                case "_V9V10":
                case "_V11V12":
                case "_V13V14":
                case "_V15V16":
                case "_V17V18":
                case "_V19V20":
                case "_V21V22":
                case "_V23V24":
                case "_V25V26":
                case "_V27V28":
                case "_V29V30":
                    member.SectionDetails.NoOfElements = 2.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "300";
                    //mem.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 100.0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 8.0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section2;
                    //cmb_code1.SelectedItem = "150";
                    ////cmb_sec_thk.SelectedItem = "12";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "400";
                    break;


                case "_L1M1":
                case "_L2M2":
                case "_L3M3":
                case "_L4M4":
                case "_L5M5":
                case "_L6M6":
                case "_L7M7":
                case "_L8M8":
                case "_L9M9":
                case "_M1L1":
                case "_M2L2":
                case "_M3L3":
                case "_M4L4":
                case "_M5L5":
                case "_M6L6":
                case "_M7L7":
                case "_M8L8":
                case "_M9L9":

                case "_U1M1":
                case "_U2M2":
                case "_U3M3":
                case "_U4M4":
                case "_U5M5":
                case "_U6M6":
                case "_U7M7":
                case "_U8M8":
                case "_U9M9":
                case "_M1U1":
                case "_M2U2":
                case "_M3U3":
                case "_M4U4":
                case "_M5U5":
                case "_M6U6":
                case "_M7U7":
                case "_M8U8":
                case "_M9U9":

                    member.SectionDetails.NoOfElements = 2.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section2;
                    member.SectionDetails.SectionName = "ISMC";
                    member.SectionDetails.SectionCode = "300";
                    //mem.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 350.0;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 100.0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 8.0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;


                    break;
                #endregion Vertical

                #region End Racker


                case "_ER":
                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section3;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "200200";
                    member.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 400.0;
                    member.SectionDetails.TopPlate.Thickness = 12.0;

                    member.SectionDetails.SidePlate.Width = 430.0;
                    member.SectionDetails.SidePlate.Thickness = 12.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 200.0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 12.0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    break;
                #endregion End Racker

                #region Diagonal


                case "_L2U1":

                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section4;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 350;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 420.0;
                    member.SectionDetails.SidePlate.Thickness = 16.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 120;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 25.0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    break;
                //case "_L3U2":
                //    member.SectionDetails.NoOfElements = 2.0;
                //    member.SectionDetails.DefineSection = eDefineSection.Section2;
                //    member.SectionDetails.SectionName = "ISMC";
                //    member.SectionDetails.SectionCode = "300";
                //    //mem.SectionDetails.AngleThickness = 10.0d;

                //    member.SectionDetails.TopPlate.Width = 0;
                //    member.SectionDetails.TopPlate.Thickness = 0.0;

                //    member.SectionDetails.SidePlate.Width = 220.0;
                //    member.SectionDetails.SidePlate.Thickness = 12.0;


                //    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                //    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                //    member.SectionDetails.BottomPlate.Width = 0.0;
                //    member.SectionDetails.BottomPlate.Thickness = 0.0;


                //    member.SectionDetails.LateralSpacing = 400.0;
                //    member.SectionDetails.NoOfBolts = 2;
                //    member.SectionDetails.BoltDia = 20;


                //    break;
                //case "_L4U3":
                //member.SectionDetails.NoOfElements = 2.0;
                //member.SectionDetails.DefineSection = eDefineSection.Section2;
                //member.SectionDetails.SectionName = "ISMC";
                //member.SectionDetails.SectionCode = "300";
                ////mem.SectionDetails.AngleThickness = 10.0d;

                //member.SectionDetails.TopPlate.Width = 0;
                //member.SectionDetails.TopPlate.Thickness = 0.0;

                //member.SectionDetails.SidePlate.Width = 0.0;
                //member.SectionDetails.SidePlate.Thickness = 0.0;


                //member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                //member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                //member.SectionDetails.BottomPlate.Width = 0.0;
                //member.SectionDetails.BottomPlate.Thickness = 0.0;


                //member.SectionDetails.LateralSpacing = 400.0;
                //member.SectionDetails.NoOfBolts = 2;
                //member.SectionDetails.BoltDia = 20;


                //break;
                case "_L3U2":
                case "_L4U3":
                case "_L5U4":
                case "_L6U5":
                case "_L7U6":
                case "_L8U7":
                case "_L9U8":
                case "_L10U9":
                case "_L11U10":
                case "_L12U11":
                case "_L13U12":
                case "_L14U13":
                case "_L15U14":
                case "_L16U15":

                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section4;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 350;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 420.0;
                    member.SectionDetails.SidePlate.Thickness = 16.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 120;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 25.0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    break;



                case "_M1U2":
                case "_M2U3":
                case "_M3U4":
                case "_M4U5":
                case "_M5U6":
                case "_M6U7":
                case "_M7U8":
                case "_M8U9":
                case "_M9U10":

                case "_U2M1":
                case "_U3M2":
                case "_U4M3":
                case "_U5M4":
                case "_U6M5":
                case "_U7M6":
                case "_U8M7":
                case "_U9M8":
                case "_U10M9":



                case "_M1L2":
                case "_M2L3":
                case "_M3L4":
                case "_M4L5":
                case "_M5L6":
                case "_M6L7":
                case "_M7L8":
                case "_M8L9":
                case "_M9L10":

                case "_L2M1":
                case "_L3M2":
                case "_L4M3":
                case "_L5M4":
                case "_L6M5":
                case "_L7M6":
                case "_L8M7":
                case "_L9M8":
                case "_L10M9":

                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section4;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "150150";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 350;
                    member.SectionDetails.TopPlate.Thickness = 25.0;

                    member.SectionDetails.SidePlate.Width = 420.0;
                    member.SectionDetails.SidePlate.Thickness = 16.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 120;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 25.0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 400.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;
                    break;



                #endregion Diagonal

                #region Bracings

                case "_TCS_ST":
                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section7;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "9090";
                    member.SectionDetails.AngleThickness = 8.0d;

                    member.SectionDetails.TopPlate.Width = 150.0;
                    member.SectionDetails.TopPlate.Thickness = 16.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 180.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;


                    break;
                case "_TCS_DIA":
                    member.SectionDetails.NoOfElements = 4.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section7;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "9090";
                    member.SectionDetails.AngleThickness = 8.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 180.0;
                    member.SectionDetails.NoOfBolts = 2;
                    member.SectionDetails.BoltDia = 20;

                    break;
                case "_BCB":
                    member.SectionDetails.NoOfElements = 2.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section8;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "200200";
                    member.SectionDetails.AngleThickness = 12.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;
                    //member.SectionDetails.SidePlate.TotalPlates = 1;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;
                    //member.SectionDetails.VerticalStiffenerPlate.TotalPlates = 1;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 0.0;
                    member.SectionDetails.NoOfBolts = 0;
                    member.SectionDetails.BoltDia = 0;


                    break;
                case "_BCB1":
                    member.SectionDetails.NoOfElements = 1.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section8;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "9090";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 0.0;
                    member.SectionDetails.NoOfBolts = 0;
                    member.SectionDetails.BoltDia = 0;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section8;
                    //cmb_code1.SelectedItem = "9090";
                    //cmb_sec_thk.SelectedItem = 10.0d;

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "0";
                    break;
                case "_BCB2":
                case "_BCB3":
                case "_BCB4":
                case "_BCB5":
                case "_BCB6":
                case "_BCB7":
                case "_BCB8":
                case "_BCB9":
                case "_BCB10":
                case "_BOTTOM_CHORD_BRACING":
                case "_BOTTOM_CHORD_BRACING1":
                case "_BOTTOM_CHORD_BRACING2":
                case "_BOTTOM_CHORD_BRACING3":
                    member.SectionDetails.NoOfElements = 1.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section8;
                    member.SectionDetails.SectionName = "ISA";
                    member.SectionDetails.SectionCode = "9090";
                    member.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 0.0;
                    member.SectionDetails.BottomPlate.Thickness = 0.0;


                    member.SectionDetails.LateralSpacing = 0.0;
                    member.SectionDetails.NoOfBolts = 0;
                    member.SectionDetails.BoltDia = 0;

                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section8;
                    //cmb_code1.SelectedItem = "9090";
                    //cmb_sec_thk.SelectedItem = 10.0d;

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "0";
                    //txt_bp_thk.Text = "0";
                    //txt_sec_lat_spac.Text = "0";
                    break;
                #endregion Bracings

                #region _STRINGER & Cross Girder

                case "_STRINGER":
                case "_STRINGER1":
                case "_STRINGER2":
                case "_STRINGER3":
                case "_STRINGER4":
                case "_STRINGER5":
                case "_STRINGER6":
                case "_STRINGER7":
                case "_STRINGER8":
                case "_STRINGER_START":
                case "_STRINGER_END":
                    member.SectionDetails.NoOfElements = 1.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section5;
                    member.SectionDetails.SectionName = "ISMB";
                    member.SectionDetails.SectionCode = "450";
                    //mem.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 150.0;
                    member.SectionDetails.BottomPlate.Thickness = 40.0;


                    member.SectionDetails.LateralSpacing = 0.0;
                    member.SectionDetails.NoOfBolts = 0;
                    member.SectionDetails.BoltDia = 0;


                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section5;
                    //cmb_code1.SelectedItem = "450";
                    ////cmb_sec_thk.SelectedItem = "10";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "125";
                    //txt_bp_thk.Text = "10";
                    //txt_sec_lat_spac.Text = "0";
                    break;
                case "_XGIRDER_IN":
                case "_XGIR":
                case "_XGIR1":
                case "_XGIR2":
                case "_XGIR3":
                case "_XGIRDER":
                case "_XGIRDERS":
                case "_CROSSGIRDERS":
                case "_CROSSGIRDER":

                    member.SectionDetails.NoOfElements = 1.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section6;
                    member.SectionDetails.SectionName = "ISMB";
                    member.SectionDetails.SectionCode = "600";
                    //mem.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 0.0;
                    member.SectionDetails.SidePlate.Thickness = 0.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 350.0;
                    member.SectionDetails.BottomPlate.Thickness = 32.0;


                    member.SectionDetails.LateralSpacing = 0.0;
                    member.SectionDetails.NoOfBolts = 0;
                    member.SectionDetails.BoltDia = 0;


                    //cmb_sections_define.SelectedIndex = (int)eDefineSection.Section6;
                    //cmb_code1.SelectedItem = "600";
                    ////cmb_sec_thk.SelectedItem = "10";

                    //txt_tp_width.Text = "0";
                    //txt_tp_thk.Text = "0";

                    //txt_sp_wd.Text = "0";
                    //txt_sp_thk.Text = "0";

                    //txt_vsp_wd.Text = "0";
                    //txt_vsp_thk.Text = "0";

                    //txt_bp_wd.Text = "300";
                    //txt_bp_thk.Text = "20";
                    //txt_sec_lat_spac.Text = "0";
                    break;
                case "_XGIRDER_END":
                case "_XGIRDER_EDGE":

                    member.SectionDetails.NoOfElements = 1.0;
                    member.SectionDetails.DefineSection = eDefineSection.Section6;
                    member.SectionDetails.SectionName = "ISMB";
                    member.SectionDetails.SectionCode = "600";
                    //mem.SectionDetails.AngleThickness = 10.0d;

                    member.SectionDetails.TopPlate.Width = 0;
                    member.SectionDetails.TopPlate.Thickness = 0.0;

                    member.SectionDetails.SidePlate.Width = 490.0;
                    member.SectionDetails.SidePlate.Thickness = 12.0;


                    member.SectionDetails.VerticalStiffenerPlate.Width = 0;
                    member.SectionDetails.VerticalStiffenerPlate.Thickness = 0;

                    member.SectionDetails.BottomPlate.Width = 350.0;
                    member.SectionDetails.BottomPlate.Thickness = 32.0;


                    member.SectionDetails.LateralSpacing = 0.0;
                    member.SectionDetails.NoOfBolts = 0;
                    member.SectionDetails.BoltDia = 0;


                    break;
                #endregion _STRINGER & Cross Girder
            }
        }


        public eMemberType MemberType { get; set; }
        public SectionData SectionDetails { get; set; }
        public double Length { get; set; }

        public List<double> MemLengths { get; set; }
        public double Weight
        {
            get
            {
                if (SectionDetails.DefineSection == eDefineSection.Section16 ||
                    SectionDetails.DefineSection == eDefineSection.Section17)
                    return NoOfMember * (WeightPerMetre * Length);

                double wgt = 0.0;
                if(MemLengths.Count > 0)
                {
                    foreach (var item in MemLengths)
                    {
                        wgt += (WeightPerMetre * SectionDetails.NoOfElements * item + SectionDetails.TopPlate.Weight + SectionDetails.BottomPlate.Weight + SectionDetails.SidePlate.Weight + SectionDetails.VerticalStiffenerPlate.Weight);
                    }
                    return wgt;
                }
                //Chiranjit [2011 05 27]
                return NoOfMember * (WeightPerMetre * SectionDetails.NoOfElements * Length + SectionDetails.TopPlate.Weight + SectionDetails.BottomPlate.Weight + SectionDetails.SidePlate.Weight + SectionDetails.VerticalStiffenerPlate.Weight);
                //return NoOfMember * WeightPerMetre * SectionDetails.NoOfElements * Length + SectionDetails.TopPlate.Weight + SectionDetails.BottomPlate.Weight + SectionDetails.SidePlate.Weight + SectionDetails.VerticalStiffenerPlate.Weight;
            }
        }
        public double SectionWeight
        {
            get
            {
                //Chiranjit [2013 11 07]
                return NoOfMember * (WeightPerMetre * SectionDetails.NoOfElements * Length);
                //return NoOfMember * WeightPerMetre * SectionDetails.NoOfElements * Length + SectionDetails.TopPlate.Weight + SectionDetails.BottomPlate.Weight + SectionDetails.SidePlate.Weight + SectionDetails.VerticalStiffenerPlate.Weight;
            }
        }
        public double WeightPerMetre { get; set; }
        public void ToStream(StreamWriter sw)
        {
            string str = "";


            int tot_mem = Group.MemberNos.Count;


            if (SectionDetails.DefineSection == eDefineSection.Section3 ||
                SectionDetails.DefineSection == eDefineSection.Section4 ||
                 SectionDetails.DefineSection == eDefineSection.Section7 ||
                 SectionDetails.DefineSection == eDefineSection.Section8 ||
                 SectionDetails.DefineSection == eDefineSection.Section9 ||
                 SectionDetails.DefineSection == eDefineSection.Section11 ||
                 SectionDetails.DefineSection == eDefineSection.Section12 ||
                 SectionDetails.DefineSection == eDefineSection.Section13 ||
                 SectionDetails.DefineSection == eDefineSection.Section14)
            {

                //str = string.Format("{0, -6} {1, -15} {2, -12} {3, -15} {4, -10} {5, -10} {6, -10:f4} {7, -10:f4} {8,10:f4} {9, -10:f4}",
                str = string.Format("{0,-16} {1,-12} {2,-15} {3,-10} {4,-10} {5,-10:f4} {6,-10} {7,-10:f0} {8,10} {9,10} {10,10:f0} {11,10:f4} {12,10:f4}",
                    Group.GroupName,//0
                    SectionDetails.DefineSection.ToString(),//1
                    SectionDetails.SectionName + " " + SectionDetails.SectionCode + " x " + SectionDetails.AngleThickness,//2
                    " ",//3
                    " ",//4
                    WeightPerMetre,//5
                    SectionDetails.NoOfElements,//6
                    Group.MemberNos.Count,//7
                    SectionDetails.LateralSpacing,//8
                    SectionDetails.BoltDia,//9
                    SectionDetails.NoOfBolts,//10
                    Length,//11
                    SectionWeight);//12
                    //Weight);//12
            }
            else if (SectionDetails.DefineSection == eDefineSection.Section1 ||
                 SectionDetails.DefineSection == eDefineSection.Section2 ||
                 SectionDetails.DefineSection == eDefineSection.Section10 ||
                SectionDetails.DefineSection == eDefineSection.Section5 ||
                 SectionDetails.DefineSection == eDefineSection.Section6 )
            {

                str = string.Format("{0,-16} {1,-12} {2,-15} {3,-10} {4,-10} {5,-10:f4} {6,-10} {7,-10:f0} {8,10} {9,10} {10,10:f0} {11,10:f4} {12,10:f4}",
                    Group.GroupName,//0
                    SectionDetails.DefineSection.ToString(),//1
                    SectionDetails.SectionName + " " + SectionDetails.SectionCode,//2
                    " ",//3
                    " ",//4
                    WeightPerMetre,//5
                    SectionDetails.NoOfElements,//6
                    Group.MemberNos.Count,//7
                    SectionDetails.LateralSpacing,//8
                    SectionDetails.BoltDia,//9
                    SectionDetails.NoOfBolts,//10
                    Length,//11
                    SectionWeight);//12
                    //Weight);//12
            }
            else if (SectionDetails.DefineSection == eDefineSection.Section16 )
            {

                str = string.Format("{0,-16} {1,-12} {2,-20} {3,-5} {4,-10} {5,-10:f4} {6,-10} {7,-10:f0} {8,10} {9,10} {10,10:f0} {11,10:f4} {12,10:f4}",
                    Group.GroupName,//0
                    SectionDetails.DefineSection.ToString(),//1
                    //SectionDetails.SectionName + " " + SectionDetails.SectionCode,//2
                    "STEEL BEAM TUBE",//2
                    " ",//3
                    " ",//4
                    WeightPerMetre,//5
                    SectionDetails.NoOfElements,//6
                    Group.MemberNos.Count,//7
                    SectionDetails.LateralSpacing,//8
                    SectionDetails.BoltDia,//9
                    SectionDetails.NoOfBolts,//10
                    Length,//11
                    SectionWeight);//12
                //Weight);//12
            }
            else if (SectionDetails.DefineSection == eDefineSection.Section17)
            {

                str = string.Format("{0,-16} {1,-12} {2,-15} {3,-10} {4,-10} {5,-10:f4} {6,-10} {7,-10:f0} {8,10} {9,10} {10,10:f0} {11,10:f4} {12,10:f4}",
                    Group.GroupName,//0
                    SectionDetails.DefineSection.ToString(),//1
                    //SectionDetails.SectionName + " " + SectionDetails.SectionCode,//2
                    "CABLE / STRAND",//2
                    " ",//3
                    " ",//4
                    WeightPerMetre,//5
                    SectionDetails.NoOfElements,//6
                    Group.MemberNos.Count,//7
                    SectionDetails.LateralSpacing,//8
                    SectionDetails.BoltDia,//9
                    SectionDetails.NoOfBolts,//10
                    Length,//11
                    SectionWeight);//12
                //Weight);//12
            }
            sw.WriteLine(str);
            if (SectionDetails.TopPlate.Area > 0)
            {
                //str = string.Format("{0, -6} {1, -15} {2, -12} {3, -15} {4, -10} {5, -10} {6, -10:f4} {7, -10:f0}",
                str = string.Format("{0, -16} {1, -12} {2, -15} {3, -10} {4, -10} {5, -10:f4} {6, -10:f0} {7, -10:f0} {8,10} {9,10} {10,10:f4} {11,10:f4} {12,10:f4}",
                   "",
                    //"",
                   "",
                   (SectionDetails.DefineSection == eDefineSection.Section12) ? "C PL" : "T PL " + SectionDetails.TopPlate.Width + " x " + SectionDetails.TopPlate.Thickness,
                   SectionDetails.TopPlate.Width,
                   SectionDetails.TopPlate.Thickness,
                   SectionDetails.TopPlate.WeightPerMetre,
                   SectionDetails.TopPlate.TotalPlates,
                   "",
                   "",
                   "",
                   "",
                   SectionDetails.TopPlate.Length,
                   SectionDetails.TopPlate.Weight * tot_mem);
                sw.WriteLine(str);
            }
            if (SectionDetails.BottomPlate.Area > 0)
            {
                str = string.Format("{0, -16} {1, -12} {2, -15} {3, -10} {4, -10} {5, -10:f4} {6, -10:f0} {7, -10:f0} {8,10} {9,10:f0} {10,10:f4} {11,10:f4} {12,10:f4}",
                   "",
                    //"",
                   "",
                   "B PL " + SectionDetails.BottomPlate.Width + " x " + SectionDetails.BottomPlate.Thickness,
                   SectionDetails.BottomPlate.Width,
                   SectionDetails.BottomPlate.Thickness,
                   SectionDetails.BottomPlate.WeightPerMetre,
                   SectionDetails.BottomPlate.TotalPlates, 
                   "", 
                   "", 
                   "", 
                   "",
                   SectionDetails.BottomPlate.Length,
                   SectionDetails.BottomPlate.Weight * tot_mem);
                sw.WriteLine(str);
            }
            if (SectionDetails.SidePlate.Area > 0)
            {
                str = string.Format("{0, -16} {1, -12} {2, -15} {3, -10} {4, -10} {5, -10:f4} {6, -10:f0} {7, -10:f0} {8,10} {9,10} {10,10:f4} {11,10:f4} {12,10:f4}",
                   "",
                    //"",
                   "",
                  ( SectionDetails.SidePlate.TotalPlates == 1 ? "W PL " : "S PL ") + SectionDetails.SidePlate.Width + " x " + SectionDetails.SidePlate.Thickness,
                   SectionDetails.SidePlate.Width,
                   SectionDetails.SidePlate.Thickness,
                   SectionDetails.SidePlate.WeightPerMetre,
                   SectionDetails.SidePlate.TotalPlates, "", "", "", "",
                   SectionDetails.SidePlate.Length,
                   SectionDetails.SidePlate.Weight * tot_mem);
                sw.WriteLine(str);
            }
            if (SectionDetails.VerticalStiffenerPlate.Area > 0)
            {
                str = string.Format("{0, -16} {1, -12} {2, -15} {3, -10} {4, -10} {5, -10:f4} {6, -10:f0} {7, -10:f0} {8,10} {9,10} {10,10:f4} {11,10:f4} {12,10:f4}",
                   "",
                    //"",
                   "",
                   "VS PL " + SectionDetails.VerticalStiffenerPlate.Width + " x " + SectionDetails.VerticalStiffenerPlate.Thickness,
                   SectionDetails.VerticalStiffenerPlate.Width,
                   SectionDetails.VerticalStiffenerPlate.Thickness,
                   SectionDetails.VerticalStiffenerPlate.WeightPerMetre,
                   SectionDetails.VerticalStiffenerPlate.TotalPlates, "", "", "", "",
                   SectionDetails.VerticalStiffenerPlate.Length,
                   SectionDetails.VerticalStiffenerPlate.Weight * tot_mem);
                sw.WriteLine(str);
            }
            if (SectionDetails.Cables_Strands.Area > 0)
            {
                //str = string.Format("{0, -16} {1, -12} {2, -15} {3, -10} {4, -10} {5, -10:f4} {6, -10:f0} {7, -10:f0} {8,10} {9,10} {10,10:f4} {11,10:f4} {12,10:f4}",
                //   "",
                //    //"",
                //   "",
                //   "VS PL " + SectionDetails.VerticalStiffenerPlate.Width + " x " + SectionDetails.VerticalStiffenerPlate.Thickness,
                //   SectionDetails.VerticalStiffenerPlate.Width,
                //   SectionDetails.VerticalStiffenerPlate.Thickness,
                //   SectionDetails.VerticalStiffenerPlate.WeightPerMetre,
                //   SectionDetails.VerticalStiffenerPlate.TotalPlates, "", "", "", "",
                //   SectionDetails.VerticalStiffenerPlate.Length,
                //   SectionDetails.VerticalStiffenerPlate.Weight * tot_mem);
                //sw.WriteLine(str);

                str = string.Format("{0, -16} {1, -12} {2, -15} {3, -10} {4, -10} {5, -10:f4} {6, -10:f0} {7, -10:f0} {8,10} {9,10} {10,10:f4} {11,10:f4} {12,10:f4}",
                   "",
                    //"",
                   "",
                   "D " + SectionDetails.Cables_Strands.Diameter + 
                   (SectionDetails.Cables_Strands.Thickness > 0 ? (", T " + SectionDetails.Cables_Strands.Thickness) : ""),
                   "",
                   "",
                   SectionDetails.Cables_Strands.WeightPerMetre,
                   "", "", "", "", "",
                   "",
                   SectionDetails.Cables_Strands.Weight * tot_mem);
                sw.WriteLine(str);
            }
            sw.WriteLine();
        }

        public void ToStreamTower(StreamWriter sw)
        {
            string str = "";


            int tot_mem = Group.MemberNos.Count;


            if (SectionDetails.DefineSection == eDefineSection.Section3 ||
                SectionDetails.DefineSection == eDefineSection.Section4 ||
                 SectionDetails.DefineSection == eDefineSection.Section7 ||
                 SectionDetails.DefineSection == eDefineSection.Section8 ||
                 SectionDetails.DefineSection == eDefineSection.Section9 ||
                 SectionDetails.DefineSection == eDefineSection.Section11 ||
                 SectionDetails.DefineSection == eDefineSection.Section12 ||
                 SectionDetails.DefineSection == eDefineSection.Section13 ||
                 SectionDetails.DefineSection == eDefineSection.Section14)
            {

                //str = string.Format("{0, -6} {1, -15} {2, -12} {3, -15} {4, -10} {5, -10} {6, -10:f4} {7, -10:f4} {8,10:f4} {9, -10:f4}",
                str = string.Format("{0,-36} {1,-12} {2,-15} {3,0} {4,0} {5,-10:f4} {6,-10} {7,-10:f0} {8,0} {9,4} {10,10:f0} {11,10:f4} {12,10:f4}",
                    Group.GroupName,//0
                    SectionDetails.DefineSection.ToString(),//1
                    SectionDetails.SectionName + " " + SectionDetails.SectionCode + " x " + SectionDetails.AngleThickness,//2
                    " ",//3
                    " ",//4
                    WeightPerMetre,//5
                    SectionDetails.NoOfElements,//6
                    Group.MemberNos.Count,//7
                    "",//8
                    SectionDetails.BoltDia,//9
                    SectionDetails.NoOfBolts,//10
                    Length,//11
                    SectionWeight);//12
                //Weight);//12
            }
            sw.WriteLine(str);
            
            sw.WriteLine();
        }

        public string Force { get; set; }
        public string Result { get; set; }
        public MaxForce MaxCompForce { get; set; }
        public MaxForce MaxTensionForce { get; set; }
        public MaxForce MaxAxialForce { get; set; } 

        public MaxForce MaxShearForce { get; set; }
        public MaxForce MaxBendingMoment { get; set; }
        public MaxForce MaxTorsion { get; set; }

        public MaxForce MaxStress { get; set; }

        public double Capacity_CompForce { get; set; }
        public double Capacity_TensionForce { get; set; }
        public double Capacity_ShearStress { get; set; }
        public double Required_ShearStress { get; set; }
        public double Capacity_SectionModulus { get; set; }
        public double Required_SectionModulus { get; set; }

        //Chiranjit [2011 07 01] for Tensile & Compressive stresses
        public double Tensile_Stress { get; set; }
        public double Capacity_Tensile_Stress { get; set; }
        public double Compressive_Stress { get; set; }
        public double Capacity_Compressive_Stress { get; set; }


        public string Result_Tensile_Stress
        {
            get
            {
                if (Capacity_Tensile_Stress > Tensile_Stress)
                    return "OK";
                else if (Capacity_Tensile_Stress < Tensile_Stress)
                    return "NOT OK";

                return "";
            }
        }
        public string Result_Compressive_Stress
        {
            get
            {
                if (Capacity_Compressive_Stress > Compressive_Stress)
                    return "OK";
                else if (Capacity_Compressive_Stress < Compressive_Stress)
                    return "NOT OK";
                return "";
            }
        }


        public string Result_Compressive
        {

            get
            {
                if (Capacity_Compressive_Stress == 0.0 &&
                    //Compressive_Stress == 0.0 &&
                    Capacity_CompForce == 0.0)
                    //&&                     MaxCompForce == 0.0

                    return "";

                //if ((Capacity_Compressive_Stress > Math.Abs(MaxCompForce.Stress/1000)) || (Capacity_CompForce > MaxCompForce))
                //    return "OK";
                //else
                //    return "NOT OK";


                if ((Capacity_CompForce > MaxCompForce))
                    return "OK";
                else
                    return "NOT OK";
            }
        }

        public string Result_Tensile
        {

            get
            {
                if (Capacity_Tensile_Stress == 0.0 &&
                    //Tensile_Stress == 0.0 &&
                  Capacity_TensionForce == 0.0)
                    //&&  MaxTensionForce == 0.0
                    return "";



                //if ((Capacity_Tensile_Stress > Math.Abs(MaxTensionForce.Stress/1000)) || (Capacity_TensionForce > MaxTensionForce))
                //    return "OK";
                //else
                //    return "NOT OK";


                if ((Capacity_TensionForce > MaxTensionForce))
                    return "OK";
                else
                    return "NOT OK";


                return "";
            }
        }



        public string Result_Section_Modulas
        {
            get
            {
                if (Capacity_SectionModulus > Required_SectionModulus)
                    return "OK";
                else if (Capacity_SectionModulus < Required_SectionModulus)
                    return "NOT OK";

                return "";
            }
        }
        public string Result_Shear_Stress
        {
            get
            {
                if (Required_ShearStress > Capacity_ShearStress)
                    return "OK";
                else if (Required_ShearStress< Capacity_ShearStress )
                    return "NOT OK";

                return "";
            }
        }


        #region For Arch Members
        public double Secondary_Stress_Ratio { get; set; }
        public double Secondary_Stress_Ratio_Capacity { get; set; }
        public string Result_Secondary_Stress_Ratio
        {
            get
            {
                if (Secondary_Stress_Ratio < Secondary_Stress_Ratio_Capacity)
                    return "OK";
                else if (Secondary_Stress_Ratio > Secondary_Stress_Ratio_Capacity)
                    return "NOT OK";

                return "";
            }
        }
        public double Breaking_Force_Capacity { get; set; }
        public string Result_Breaking_Force
        {
            get
            {
                if (Breaking_Force_Capacity == 0.0)
                    return "";

                if (MaxTensionForce.Force < Breaking_Force_Capacity)
                    return "OK";
                else if (MaxTensionForce.Force > Breaking_Force_Capacity)
                    return "NOT OK";

                return "";
            }
        }
        public double Permissible_Breaking_Force_Capacity { get; set; }
        public string Result_Permissible_Breaking_Force
        {
            get
            {
                if (Permissible_Breaking_Force_Capacity == 0.0)
                    return "";


                if (MaxTensionForce.Force < Permissible_Breaking_Force_Capacity)
                    return "OK";
                else if (MaxTensionForce.Force > Permissible_Breaking_Force_Capacity)
                    return "NOT OK";

                return "";
            }
        }
        //0.0, //Secondary Stress Ratio
          //                      1.0,//Secondary Stress Ratio Capacity
          //                      "OK",//Secondary Stress Ratio Resul

          //                      tens.ToString("f3"),
          //                      0.0, //Breaking Force Capacity
          //                      1.0,//Breaking Force Result
          //                      1.0,//Permissible Breaking Force
          //                      1.0,//Permissible Breaking Force Result
          //                      "OK",//Secondary Stress Ratio Resul
        #endregion For Arch Members


        #region Chiranjit [2013 08 19]


        double Iy, Ix, A, Anet, ry;


        public double Area
        {
            get
            {
                //if (A == 0)
                Calculate_Area_Properties(iApp);

                return A / 1000000.0;
            }
        }

        public double IXX
        {
            get
            {
                if (Ix == 0)
                    Calculate_Area_Properties(iApp);

                return Ix / (1000000.0 * 1000000.0);
            }
        }

        public double IYY
        {
            get
            {
                if (Iy == 0)
                    Calculate_Area_Properties(iApp);

                return Iy / (1000000.0 * 1000000.0);
            }
        }
        
        #endregion


        public void Calculate_Area_Properties(IApplication app)
        {
            iApp = app;
            A = 0;
            if (iApp == null) return;

            double Rxx, Cxx, a, Iyy, Ixx, Zxx, t, tf, D; // From Table

            double l, nb, n, S, bolt_dia, ry_anet, sigma_b;

            double tp, Bp, np; // Top Plate
            double tbp, Bbp, nbp; // Bottom Plate
            double ts, Bs, ns; // Side Plate
            double tss, Bss, nss; // Vertical Stiffener Plate
            double Z = 0.0;

            double M, F;

            RolledSteelAnglesRow tabAngle;
            RolledSteelBeamsRow tabBeam;
            RolledSteelChannelsRow tabChannel;


            string kStr =Group.MemberNosText;
            MyList mList = new MyList(kStr, ' ');







            nb = SectionDetails.NoOfBolts;
            S = SectionDetails.LateralSpacing;
            bolt_dia = SectionDetails.BoltDia;
            n = SectionDetails.NoOfElements;

            Bp = SectionDetails.TopPlate.Width;
            tp = SectionDetails.TopPlate.Thickness;
            np = SectionDetails.TopPlate.TotalPlates;

            Bbp = SectionDetails.BottomPlate.Width;
            tbp = SectionDetails.BottomPlate.Thickness;
            nbp = SectionDetails.BottomPlate.TotalPlates;

            Bs = SectionDetails.SidePlate.Width;
            ts = SectionDetails.SidePlate.Thickness;
            ns = SectionDetails.SidePlate.TotalPlates;

            Bss = SectionDetails.VerticalStiffenerPlate.Width;
            tss = SectionDetails.VerticalStiffenerPlate.Thickness;
            nss = SectionDetails.VerticalStiffenerPlate.TotalPlates;







            Ixx = 0;


            #region Define Section
            switch (SectionDetails.DefineSection)
            {
                case eDefineSection.Section1:
                    #region Section 1
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    DesignReport.Add(string.Format("---------------------"));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("{0} x {1} {2}", n, SectionDetails.SectionName, SectionDetails.SectionCode));

                    //tabBeam = iApp.Tables.Get_BeamData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode);
                    tabBeam = iApp.Tables.Get_BeamData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode);


                    a = tabBeam.Area;
                    D = tabBeam.Depth;
                    Iyy = tabBeam.Iyy;
                    t = tabBeam.WebThickness;
                    tf = tabBeam.FlangeThickness;

                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    DesignReport.Add(string.Format("Depth = D = {0} mm", D));
                    DesignReport.Add(string.Format("Web Thickness = t = {0} mm", t));
                    DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));


                    //Iy = (Iyy * 10000 + a * 100 * (D / 2.0) * (S / 2.0)) * n + (tp * Bp * Bp * Bp / 12.0);
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    DesignReport.Add(string.Format("------------------"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (S / 2.0) * (S / 2.0)) * n + (tp * Bp * Bp * Bp / 12.0)"));
                    //DesignReport.Add(string.Format("                  = ({0} * 10000 + {1} * 100 * ({2} / 2.0) * ({2} / 2.0)) * {3} + ({4} * {5} * {5} * {5} / 12.0)",
                    //    Iyy, a, S, n, tp, Bp));
                    //DesignReport.Add(string.Format("                  = {0:f3} sq.sq.mm", Iy));
                    DesignReport.Add(string.Format(""));



                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * (D / 2.0) * (D / 2.0)) * n;
                    DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (D / 2)^2) * n"));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((D / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((D / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    // Top Plate
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ({2} / 2.0) * ({2} / 2.0)) * {3}  ", Iyy, a, D, n));
                    Iy = (Iyy * 10000 + a * 100 * (D / 2.0) * (D / 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((D / 2) + (tp / 2)) * ((D / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, D, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((D / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, D, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]





                    A = n * a * 100 + (tp * Bp * np);
                    DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np)"));
                    DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4})",
                        n, a, tp, Bp, np));
                    DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    DesignReport.Add(string.Format(""));


                    Anet = A - nb * ((bolt_dia + 1.5) * (tf + tp));
                    DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (tf + tp))"));
                    DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, tf, tp));
                    DesignReport.Add(string.Format("     = {0:f3} sq.mm ", Anet));
                    DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    DesignReport.Add(string.Format("   = SQRT({0:f3}/ {1:f3})", Iy, Anet));
                    DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    DesignReport.Add(string.Format(""));


                    #endregion Section 1
                    break;
                case eDefineSection.Section2:
                    #region Section 2

                    //tabChannel = iApp.Tables.Get_ChannelData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode);
                    tabChannel = iApp.Tables.Get_ChannelData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode);

                    a = tabChannel.Area;
                    D = tabChannel.Depth;
                    Iyy = tabChannel.Iyy;
                    t = tabChannel.WebThickness;
                    tf = tabChannel.FlangeThickness;
                    Cxx = tabChannel.CentreOfGravity;
                    DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //DesignReport.Add(string.Format("---------------------"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("{0} x {1} {2}", n, SectionDetails.SectionName, SectionDetails.SectionCode));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Bottom Plate Width = Bp = {0} mm", Bbp));
                    //DesignReport.Add(string.Format("Bottom Plate Thickness = tp = {0} mm", tbp));
                    //DesignReport.Add(string.Format("No Of Bottom Plate = np = {0}", nbp));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    DesignReport.Add(string.Format("Depth = D = {0} mm", D));
                    DesignReport.Add(string.Format("Web Thickness = t = {0} mm", t));
                    DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    DesignReport.Add(string.Format("------------------"));
                    DesignReport.Add(string.Format(""));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n
                        + ((tp * Bp * Bp * Bp / 12.0) * np) + ((tbp * Bbp * Bbp * Bbp / 12.0) * nbp)
                        + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;




                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((D / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((D / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    DesignReport.Add(string.Format(""));
                    // Top Plate
                    DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((D / 2) + (tp / 2)) * ((D / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, D, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((D / 2) + (tbp / 2)) * ((D / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, D, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("                       = {0:E3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]








                    //DesignReport.Add(string.Format("Moment of Inertia = Iy"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np)  + ((tbp * Bbp**3 / 12.0) * nbp) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns"));
                    //DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + (({8} * {8}^3 / 12.0) * {10}) + ({11} * {12} * ((({2} / 2.0) + ({12} / 2.0))^2)) * {13}",
                    //    //DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10}",
                    //                            Iyy, a, S, Cxx, n, tp, Bp, np, tbp, Bbp, nbp, Bs, ts, ns));

                    //DesignReport.Add(string.Format("           = {0:E3} sq.sq.mm", Iy));
                    DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss) + (tbp * Bbp * nbp);
                    DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns)"));
                    DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                        n, a, tp, Bp, np, ts, Bs, ns));
                    DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))"));
                    DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, t, ts));
                    DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    DesignReport.Add(string.Format(""));

                    //Iy = (Iyy * 10000 + a * 100 * (S / 2.0) * (S / 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * (Math.Pow((S - t - (tp / 2.0)), 2))) * np;
                    //DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (S / 2.0) * (S / 2.0)) * n + ((tp * Bp**3 / 12.0) * (S - t - (tp / 2.0))^2)) * np"));
                    //DesignReport.Add(string.Format("                  = ({0} * 10000 + {1} * 100 * ({2} / 2.0) * ({2} / 2.0)) * {3} + (({4} * {5}^3 / 12.0) * ({2} - {6} - ({4} / 2.0))^2)) * {7}",
                    //                                                              Iyy, a, S, n, tp, Bp, t, np));
                    //DesignReport.Add(string.Format("                  = {0:f3} sq.sq.mm", Iy));
                    //DesignReport.Add(string.Format(""));

                    //A = n * a * 100 + (tp * Bp * np));
                    //DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np)"));
                    //DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4})", n, a, tp, Bp, np));
                    //DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    //DesignReport.Add(string.Format(""));
                    //Anet = A - nb * ((bolt_dia + 1.5) * (t + tp)));
                    //DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + tp))"));
                    //DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))", A, nb, bolt_dia, t, tp));
                    //DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    //DesignReport.Add(string.Format(""));
                    ry_anet = Math.Sqrt(Iy / Anet);
                    ry = Math.Sqrt(Iy / A);
                    DesignReport.Add(string.Format("ry = SQRT(Iy / A)"));
                    DesignReport.Add(string.Format("   = SQRT({0:E3} / {1:f3})", Iy, A));
                    DesignReport.Add(string.Format("   = {0:f3} mm", ry));

                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));

                    DesignReport.Add(string.Format("ry_Anet = SQRT(Iy / Anet)"));
                    DesignReport.Add(string.Format("   = SQRT({0:E3} / {1:f3})", Iy, Anet));
                    DesignReport.Add(string.Format("   = {0:f3} mm", ry_anet));

                    DesignReport.Add(string.Format(""));
                    #endregion Section 2
                    break;
                case eDefineSection.Section3:
                    #region Section 3
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //DesignReport.Add(string.Format("---------------------"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, SectionDetails.SectionName, SectionDetails.SectionSize, SectionDetails.AngleThickness));


                    //tabAngle = iApp.Tables.Get_AngleData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode, SectionDetails.AngleThickness);
                    tabAngle = iApp.Tables.Get_AngleData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode, SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;

                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    DesignReport.Add(string.Format("Iyy = {0} sq.sq.cm", Iyy));
                    DesignReport.Add(string.Format("Cxx = {0}", Cxx));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    DesignReport.Add(string.Format("------------------"));
                    DesignReport.Add(string.Format(""));

                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    //DesignReport.Add(string.Format("Moment of Inertia = Iy"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns"));
                    //DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10}",
                    //                            Iyy, a, S, Cxx, n, tp, Bp, np, Bs, ts, ns));

                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    DesignReport.Add(string.Format(""));
                    // Top Plate
                    DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]




                    DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns); ;
                    DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns)"));
                    DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                        n, a, tp, Bp, np, ts, Bs, ns));
                    DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))"));
                    DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, t, ts));
                    DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    DesignReport.Add(string.Format("   = SQRT({0} / {1})", Iy, Anet));
                    DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    DesignReport.Add(string.Format(""));
                    #endregion Section 3
                    break;
                case eDefineSection.Section4:
                    #region Section 4
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //DesignReport.Add(string.Format("---------------------"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, SectionDetails.SectionName, SectionDetails.SectionSize, SectionDetails.AngleThickness));

                    tabAngle = iApp.Tables.Get_AngleData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode, SectionDetails.AngleThickness);

                    a = tabAngle.Area;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    //n = 4;
                    t = tabAngle.Thickness;
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Vertical Stiffener Plate Width = Bss = {0} mm", Bss));
                    //DesignReport.Add(string.Format("Vertical Stiffener Plate Thickness = tss = {0} mm", tss));
                    //DesignReport.Add(string.Format("No Of Vertical Stiffener Plate = nss = {0}", nss));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    DesignReport.Add(string.Format("Moment of Inertia = Iyy = {0} sq.sq.cm", Iyy));
                    DesignReport.Add(string.Format("Centre of Gravity = Cxx = {0} cm", Cxx));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    DesignReport.Add(string.Format("------------------"));
                    DesignReport.Add(string.Format(""));

                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns + (tss * Bss * Math.Pow(((S / 2) - t - (tss / 2.0)), 2.0)) * nss;
                    ////Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns + ((Bss * tss * tss * tss / 12.0) + tss * Bss * Math.Pow(((S / 2) - t - (tss / 2.0)), 2.0)) * nss;
                    //DesignReport.Add(string.Format("Moment of Intertia = Iy"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("          Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns + (tss * Bss * (((S / 2) - t - (tss / 2.0))^2)) * nss"));
                    //DesignReport.Add(string.Format("             = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10} + ({11} * {12} * ((({2} / 2) - {13} - ({11} / 2.0))^2)) * {14}",
                    //                                                                            Iyy, a, S, Cxx, n, tp, Bp, np, Bs, ts, ns, tss, Bss, t, nss));


                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    DesignReport.Add(string.Format(""));
                    // Top Plate
                    DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]



                    //DesignReport.Add(string.Format("             = {0:f3} sq.sq.mm", Iy));
                    DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss);
                    DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss)"));
                    DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7}) + ({8} * {9} * {10})",
                        n, a, tp, Bp, np, ts, Bs, ns, tss, Bss, nss));
                    DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))"));
                    DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, t, ts));
                    DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    DesignReport.Add(string.Format("   = SQRT({0:f3} / {1:f3})", Iy, Anet));
                    DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    DesignReport.Add(string.Format(""));

                    #endregion Section 4
                    break;

                case eDefineSection.Section5: // Stringer
                    #region Section 5
                      DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //DesignReport.Add(string.Format("---------------------"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("{0} x {1} {2}", n, SectionDetails.SectionName, SectionDetails.SectionCode));

                    tabBeam = iApp.Tables.Get_BeamData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode);
                    a = tabBeam.Area;
                    D = tabBeam.Depth;
                    Iyy = tabBeam.Iyy;
                    Ixx = tabBeam.Ixx;
                    Zxx = tabBeam.Zxx;

                    t = tabBeam.WebThickness;
                    tf = tabBeam.FlangeThickness;

                    DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //DesignReport.Add(string.Format("Bottom Plate Width = Bbp = {0} mm", Bbp));
                    //DesignReport.Add(string.Format("Bottom Plate Thickness = tbp = {0} mm", tbp));
                    //DesignReport.Add(string.Format("No Of Bottom Plate = np = {0}", nbp));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    DesignReport.Add(string.Format("Depth = D = {0} mm", D));
                    DesignReport.Add(string.Format("Web Thickness = t = {0} mm", t));
                    DesignReport.Add(string.Format("Moment of Inertia = Iyy = {0} sq.sq.cm.", Iyy));
                    DesignReport.Add(string.Format("Flange Thickness = tf = {0}", tf));


                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    DesignReport.Add(string.Format("------------------"));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));


                    // Chiranjit [2011 10 21] this formula is wrong
                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia

                    DesignReport.Add(string.Format("Moment of Inertia = Ix = Ixx * 10000 "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((D / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((D / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    // Top Plate
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("                       = {0} * 10000 ", Ixx));
                    Ix = Ixx * 10000.0;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Ix += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((D / 2) + (tp / 2)) * ((D / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, D, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Ix += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((D / 2) + (tbp / 2)) * ((D / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, D, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Ix += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Ix += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Ix));



                    #endregion Sandiapan Goswami [2011 10 26]

                    //DesignReport.Add(string.Format("Moment of Inertia = Ix = Ixx * 10000 + ((Bbp * tbp^3 / 12.0) + (tbp * Bbp) * ((((D / 2) + (tbp / 2))^2))) * nbp "));
                    //DesignReport.Add(string.Format("                       = {0} * 10000 + (({1} * {2}^3 / 12.0) + ({1} * {2}) * ((({3} / 2) + ({2} / 2))^2))) * {4} ",
                    //                Ixx, Bbp, tbp, D, nbp));
                    //DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Ix));
                    DesignReport.Add(string.Format(""));

                    //Ix = Ixx * 10000 + ((Bbp * tbp * tbp * tbp / 12.0) + (tbp * Bbp) * (Math.Pow(((D / 2) + (tbp / 2)), 2.0))) * nbp + (nss * (tss * Bss) * ((t / 2.0) + (tss / 2.0)));

                    A = n * a * 100 + (tbp * Bbp * nbp);

                    A = n * a * 100 + (tbp * Bbp * nbp) + (tss * Bss * nss);

                    Iy = Ix;
                    #endregion Section 5
                    break;

                case eDefineSection.Section6: // Cross Girder
                    #region Section 6
                    //DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //DesignReport.Add(string.Format("---------------------"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("{0} x {1} {2}", n, SectionDetails.SectionName, SectionDetails.SectionCode));

                    tabBeam = iApp.Tables.Get_BeamData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode);
                    a = tabBeam.Area;
                    D = tabBeam.Depth;
                    Iyy = tabBeam.Iyy;
                    Ixx = tabBeam.Ixx;
                    Zxx = tabBeam.Zxx;

                    t = tabBeam.WebThickness;
                    tf = tabBeam.FlangeThickness;

                    DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //DesignReport.Add(string.Format("Bottom Plate Width = Bbp = {0} mm", Bbp));
                    //DesignReport.Add(string.Format("Bottom Plate Thickness = tbp = {0} mm", tbp));
                    //DesignReport.Add(string.Format("No Of Bottom Plate = nbp = {0}", nbp));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Vertical Stiffener Plate Width = Bss = {0} mm", Bss));
                    //DesignReport.Add(string.Format("Vertical Stiffener Plate Thickness = tss = {0} mm", tss));
                    //DesignReport.Add(string.Format("No Of Vertical Stiffener Plates = nss = {0}", nss));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    DesignReport.Add(string.Format("Depth = D = {0} mm", D));
                    DesignReport.Add(string.Format("Web Thickness = t = {0} mm", t));
                    DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    DesignReport.Add(string.Format("Moment of Inertia = Ixx = {0} sq.sq.mm", Ixx));


                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    DesignReport.Add(string.Format("------------------"));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));

                    // Chiranjit [2011 10 21] this formula is wrong
                    //Ix = Ixx * 10000 + ((tbp * Bbp * Bbp * Bbp / 12.0) + (tbp * Bbp) * (Math.Pow(((D / 2) + (tbp / 2)), 2.0))) * nbp + (nss * (tss * Bss) * ((t / 2.0) + (tss / 2.0)));
                    Ix = Ixx * 10000 + ((Bbp * tbp * tbp * tbp / 12.0) + (tbp * Bbp) * (Math.Pow(((D / 2) + (tbp / 2)), 2.0))) * nbp + (nss * (tss * Bss) * ((t / 2.0) + (tss / 2.0)));

                    Ix = Ixx * 10000.0;
                    Ix += ((Bbp * tbp * tbp * tbp / 12.0) + (tbp * Bbp) * (Math.Pow(((D / 2) + (tbp / 2)), 2.0)));
                    Ix += (tss * Bss * Bss * Bss / 12.0) * nss;


                    #region Chiranjit [2011 10 26]
                    //According to Mr. Sandipan Goswami
                    //DesignReport.Add(string.Format("Moment of Inertia = Ix = Ixx * 10000 + ((Bbp * tbp^3 / 12.0) + (tbp * Bbp) * (((D / 2) + (tbp / 2))^2)) * nbp + (nss * (tss * Bss) * ((t / 2.0) + (tss / 2.0)))"));
                    //DesignReport.Add(string.Format("                       = {0} * 10000 + (({1} * {2}^3 / 12.0) + ({1} * {2}) * ((({3} / 2) + ({2} / 2))^2))) * {4} + ({5} * ({6} * {7}) * (({8} / 2.0) + ({6} / 2.0))) ",
                    //                                                                                                            Ixx, Bbp, tbp, D, nbp, nss, tss, Bss, t));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Ix));
                    //DesignReport.Add(string.Format(""));
                    #endregion Chiranjit [2011 10 26]
                    //DesignReport.Add(string.Format("Moment of Inertia = Ix = Ixx * 10000 + (Bbp * tbp^3 / 12.0) + (tbp * Bbp) * (((D / 2) + (tbp / 2))^2)) * nbp"));
                    //DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12.0) * nss"));
                    //DesignReport.Add(string.Format("                       = {0} * 10000 + ({1} * {2}^3 / 12.0) + ({1} * {2}) * ((({3} / 2) + (tbp / 2))^2)) * {4}", Ixx, Bbp, tbp, D, nbp));
                    //DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Ix));
                    #region Sandiapan Goswami [2011 10 26]   Moment of Inertia

                    DesignReport.Add(string.Format("Moment of Inertia = Ix = Ixx * 10000 "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((D / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((D / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    DesignReport.Add(string.Format(""));
                    // Top Plate
                    DesignReport.Add(string.Format("                       = {0} * 10000 ", Ixx));
                    Ix = Ixx * 10000.0;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Ix += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((D / 2) + (tp / 2)) * ((D / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, D, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Ix += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((D / 2) + (tbp / 2)) * ((D / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, D, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Ix += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Ix += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Ix));

                    
                    A = n * a * 100 + (tbp * Bbp * nbp);

                    A = n * a * 100 + (tbp * Bbp * nbp) + (tss * Bss * nss);
                    Iy = Ix;
                    #endregion Sandiapan Goswami [2011 10 26]

                    DesignReport.Add(string.Format(""));
                    #endregion Section 6
                    break;
                case eDefineSection.Section7:
                    #region Section 7
                    DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //DesignReport.Add(string.Format("---------------------"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, SectionDetails.SectionName, SectionDetails.SectionSize, SectionDetails.AngleThickness));

                    tabAngle = iApp.Tables.Get_AngleData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode, SectionDetails.AngleThickness);

                    a = tabAngle.Area;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    //n = 4;
                    t = tabAngle.Thickness;
                    DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //DesignReport.Add(string.Format("No Of Top Plate = np = {0} mm", np));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //DesignReport.Add(string.Format("No Of Side Plate = ns = {0} mm", ns));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Vertical Stiffener Plate Width = Bss = {0} mm", Bss));
                    //DesignReport.Add(string.Format("Vertical Stiffener Plate Thickness = tss = {0} mm", tss));
                    //DesignReport.Add(string.Format("No Of Vertical Stiffener Plate = nss = {0} mm", nss));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    DesignReport.Add(string.Format("Iyy = {0} sq.sq.cm", Iyy));
                    DesignReport.Add(string.Format("Cxx = {0} cm", Cxx));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    DesignReport.Add(string.Format("------------------"));
                    DesignReport.Add(string.Format(""));


                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns + ((Bss * tss * tss * tss / 12.0) + tss * Bss * Math.Pow(((S / 2) - t - (tss / 2.0)), 2.0)) * nss;
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //DesignReport.Add(string.Format("Moment of Inertia = Iy "));
                    //DesignReport.Add(string.Format("      Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    //DesignReport.Add(string.Format("                  = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4}",
                    //                Iyy, a, S, Cxx, n));





                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    DesignReport.Add(string.Format(""));
                    // Top Plate
                    DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]







                    //DesignReport.Add(string.Format("         = {0:f3} sq.sq.mm", Iy));
                    DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss);
                    DesignReport.Add(string.Format("A = n * a * 100"));
                    DesignReport.Add(string.Format("  = {0} * {1} * 100",
                        n, a));
                    DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * t)"));
                    DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * {3})",
                        A, nb, bolt_dia, t));
                    DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    DesignReport.Add(string.Format("   = SQRT({0:f3} / {1:f3})", Iy, Anet));
                    DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    DesignReport.Add(string.Format(""));

                    #endregion Section 7
                    break;
                case eDefineSection.Section8:
                    #region Section 8
                    DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //DesignReport.Add(string.Format("---------------------"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, SectionDetails.SectionName, SectionDetails.SectionSize, SectionDetails.AngleThickness));

                    tabAngle = iApp.Tables.Get_AngleData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode, SectionDetails.AngleThickness);

                    a = tabAngle.Area;
                    Iyy = tabAngle.Iyy;
                    Ixx = tabAngle.Ixx;
                    Cxx = tabAngle.Cxx;
                    //n = 4;
                    t = tabAngle.Thickness;
                    DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //DesignReport.Add(string.Format("No Of Top Plate = np = {0} mm", np));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //DesignReport.Add(string.Format("No Of Side Plate = ns = {0} mm", ns));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Vertical Stiffener Plate Width = Bss = {0} mm", Bss));
                    //DesignReport.Add(string.Format("Vertical Stiffener Plate Thickness = tss = {0} mm", tss));
                    //DesignReport.Add(string.Format("No Of Vertical Stiffener Plate = nss = {0} mm", nss));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    DesignReport.Add(string.Format("Iyy = {0} sq.sq.cm", Iyy));
                    DesignReport.Add(string.Format("Cxx = {0} cm", Cxx));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    DesignReport.Add(string.Format("------------------"));
                    DesignReport.Add(string.Format(""));

                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns + ((Bss * tss * tss * tss / 12.0) + tss * Bss * Math.Pow(((S / 2) - t - (tss / 2.0)), 2.0)) * nss;
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //DesignReport.Add(string.Format("Moment of Inertia = Iy "));
                    //DesignReport.Add(string.Format("    Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    //DesignReport.Add(string.Format("       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4}",
                    //                Iyy, a, S, Cxx, n));





                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    DesignReport.Add(string.Format(""));
                    // Top Plate
                    DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]





                    Ixx = Ixx * 10000;


                    //DesignReport.Add(string.Format("       = {0:f3} sq.sq.mm", Iy));
                    DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns) + (tss * Bss * nss);
                    DesignReport.Add(string.Format("A = n * a * 100"));
                    DesignReport.Add(string.Format("  = {0} * {1} * 100",
                        n, a));
                    DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * t)"));
                    DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * {3})",
                        A, nb, bolt_dia, t));
                    DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    DesignReport.Add(string.Format("   = SQRT({0:f3} / {1:f3})", Iy, Anet));
                    DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    DesignReport.Add(string.Format(""));

                    #endregion Section 8
                    break;
                case eDefineSection.Section9:
                    #region Section 9
                    DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //DesignReport.Add(string.Format("---------------------"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, SectionDetails.SectionName, SectionDetails.SectionSize, SectionDetails.AngleThickness));


                    tabAngle = iApp.Tables.Get_AngleData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode, SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;

                    DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    DesignReport.Add(string.Format("Iyy = {0} sq.sq.cm", Iyy));
                    DesignReport.Add(string.Format("Cxx = {0} cm", Cxx));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    DesignReport.Add(string.Format("------------------"));
                    DesignReport.Add(string.Format(""));

                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    //DesignReport.Add(string.Format("Moment of Inertia = Iy"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns"));
                    //DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10}",
                    //                            Iyy, a, S, Cxx, n, tp, Bp, np, Bs, ts, ns));
                    //DesignReport.Add(string.Format("           = {0:f3} sq.sq.mm", Iy));




                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    DesignReport.Add(string.Format(""));
                    // Top Plate
                    DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]






                    DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns);
                    DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np) + (ts * Bs * ns)"));
                    DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                        n, a, tp, Bp, np, ts, Bs, ns));
                    DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))"));
                    DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, t, ts));
                    DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    DesignReport.Add(string.Format(""));
                    ry = Math.Sqrt(Iy / Anet);
                    DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    DesignReport.Add(string.Format("   = SQRT({0} / {1})", Iy, Anet));
                    DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    DesignReport.Add(string.Format(""));
                    #endregion Section 9
                    break;
                case eDefineSection.Section10:
                    #region Section 10

                    tabChannel = iApp.Tables.Get_ChannelData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode);
                    a = tabChannel.Area;
                    D = tabChannel.Depth;
                    Iyy = tabChannel.Iyy;
                    t = tabChannel.WebThickness;
                    tf = tabChannel.FlangeThickness;
                    Cxx = tabChannel.CentreOfGravity;
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //DesignReport.Add(string.Format("---------------------"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("{0} x {1} {2}", n, SectionDetails.SectionName, SectionDetails.SectionCode));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Bottom Plate Width = Bbp = {0} mm", Bbp));
                    //DesignReport.Add(string.Format("Bottom Plate Thickness = tbp = {0} mm", tbp));
                    //DesignReport.Add(string.Format("No Of Bottom Plate = nbp = {0}", nbp));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    DesignReport.Add(string.Format("Web Thickness = t = {0} mm", t));
                    DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    DesignReport.Add(string.Format("------------------"));
                    DesignReport.Add(string.Format(""));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((tbp * Bbp * Bbp * Bbp / 12.0) * nbp) + (Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    //DesignReport.Add(string.Format("Moment of Inertia = Iy"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np)  + ((tbp * Bbp**3 / 12.0) * nbp) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns"));
                    //DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + (({8} * {9}^3 / 12.0) * {10}) + ({11} * {12} * ((({2} / 2.0) + ({12} / 2.0))^2)) * {13}",
                    //                            Iyy, a, S, Cxx, n, tp, Bp, np, tbp, Bbp, nbp, Bs, ts, ns));

                    //DesignReport.Add(string.Format("           = {0:E3} sq.sq.mm", Iy));







                    #region Sandiapan Goswami [2011 10 26] Calculation of Moment of Inertia
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    DesignReport.Add(string.Format("Moment of Inertia = Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n "));
                    // Top Plate
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ((Bp * tp^3 / 12.0) + (Bp * tp) * (((S / 2) + (tp / 2))^2)) * np"));
                    }
                    // Bottom Plate
                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (Bbp * tbp^3 / 12.0) + (Bbp * tbp) * (((S / 2) + (tbp / 2))^2)) * nbp"));
                    }
                    // Side Plate
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (ts * Bs^3 / 12) * ns"));
                    }
                    // Vertical Stiffener Plate
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + (tss * Bss^3 / 12) * nss"));
                    }

                    DesignReport.Add(string.Format(""));
                    // Top Plate
                    DesignReport.Add(string.Format("                       = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} ", Iyy, a, S, Cxx, n));
                    Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n;
                    //Area of Top Plate   A = tp*Bp,    r = (D/2 + tp/2)
                    //Moment of Inertia   Ix  = (Bp*tp^3)/12 + Ar^2  
                    Iy += (Bp * tp * tp * tp / 12 + (Bp * tp) * ((S / 2) + (tp / 2)) * ((S / 2) + (tp / 2))) * np;
                    if ((Bp * tp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bp, tp, S, np));
                    }
                    // Bottom Plate
                    //Area of Bottom Plate   A = tbp*Bbp,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (Bbp*tbp^3/12) + Ar^2  
                    Iy += (Bbp * tbp * tbp * tbp / 12 + (Bbp * tbp) * ((S / 2) + (tbp / 2)) * ((S / 2) + (tbp / 2))) * nbp;

                    if ((Bbp * tbp) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) + ({0} * {1}) * ((({2} / 2) + ({1} / 2))^2)) * {3}", Bbp, tbp, S, nbp));
                    }
                    // Side Plate
                    //Area of Side Plate   A = ts*Bs
                    //Moment of Inertia   Ix  = (ts*Bs^3/12)*ns
                    Iy += (ts * Bs * Bs * Bs / 12) * ns;
                    if ((Bs * ts) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", ts, Bs, ns));
                    }
                    // Vertical Stiffener Plate
                    //Area of Vertical Stiffener Plate   A = tss*Bss,    r = (D/2 + tbp/2)
                    //Moment of Inertia   Ix  = (ts*Bs^3)*ns

                    Iy += (tss * Bss * Bss * Bss / 12) * nss;
                    if ((Bss * tss) != 0.0)
                    {
                        DesignReport.Add(string.Format("                         + ({0} * {1}^3 / 12.0) * {2} ", tss, Bss, nss));
                    }
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("                       = {0:e3} sq.sq.mm", Iy));



                    #endregion Sandiapan Goswami [2011 10 26]








                    DesignReport.Add(string.Format(""));

                    A = n * a * 100 + (tp * Bp * np) + (tbp * Bbp * nbp) + (ts * Bs * ns);
                    DesignReport.Add(string.Format("A = n * a * 100 + (tp * Bp * np) + (tbp * Bbp * nbp) + (ts * Bs * ns)"));
                    DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7}) + ({8} * {9} * {10})",
                        n, a, tp, Bp, np, ts, Bs, ns, tbp, Bbp, nbp));
                    DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    DesignReport.Add(string.Format(""));

                    Anet = A - nb * ((bolt_dia + 1.5) * (t + ts));
                    DesignReport.Add(string.Format("Anet = A - nb * ((bolt_dia + 1.5) * (t + ts))"));
                    DesignReport.Add(string.Format("     = {0:f3} - {1} * (({2} + 1.5) * ({3} + {4}))",
                        A, nb, bolt_dia, t, ts));
                    DesignReport.Add(string.Format("     = {0:f3} sq.mm", Anet));
                    DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / Anet);
                    DesignReport.Add(string.Format("ry = SQRT(Iy / Anet)"));
                    DesignReport.Add(string.Format("   = SQRT({0:f3} / {1:f3})", Iy, Anet));
                    DesignReport.Add(string.Format("   = {0:f3} mm", ry));

                    DesignReport.Add(string.Format(""));
                    #endregion Section 10
                    break;
                case eDefineSection.Section11:
                    #region Section 11
                    DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //DesignReport.Add(string.Format("---------------------"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, SectionDetails.SectionName, SectionDetails.SectionSize, SectionDetails.AngleThickness));


                    tabAngle = iApp.Tables.Get_AngleData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode, SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;

                    DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    DesignReport.Add(string.Format("Ixx = Iyy = {0} sq.sq.cm", Iyy));
                    DesignReport.Add(string.Format("Cxx = {0} cm", Cxx));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    DesignReport.Add(string.Format("------------------"));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("For Combined Section :"));
                    DesignReport.Add(string.Format("----------------------"));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));

                    Iy = 2 * Iyy * 10000;
                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    DesignReport.Add(string.Format("Moment of Inertia = Iy = 2 * Iyy *10000"));
                    DesignReport.Add(string.Format("                       = 2 * {0} *10000", Iyy));
                    DesignReport.Add(string.Format("                       = {0} sq.sq.mm", Iy));
                    DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("         Iy = (Iyy * 10000 + a * 100 * (((S / 2.0) - (Cxx * 10 / 2.0))^2)) * n + ((tp * Bp**3 / 12.0) * np) + (Bs * ts * (((S / 2.0) + (ts / 2.0))^2)) * ns"));
                    //DesignReport.Add(string.Format("            = ({0} * 10000 + {1} * 100 * ((({2} / 2.0) - ({3} * 10 / 2.0))^2)) * {4} + (({5} * {6}^3 / 12.0) * {7}) + ({8} * {9} * ((({2} / 2.0) + ({9} / 2.0))^2)) * {10}",
                    //                            Iyy, a, S, Cxx, n, tp, Bp, np, Bs, ts, ns));

                    //DesignReport.Add(string.Format("           = {0:f3} sq.sq.mm", Iy));
                    DesignReport.Add(string.Format(""));

                    A = 2 * a * 100;
                    Anet = A;
                    DesignReport.Add(string.Format("A = 2 * a a * 100 = 2 * {0} a * 100 = {1} sq.mm", a, A));
                    //DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                    //    n, a, tp, Bp, np, ts, Bs, ns));
                    //DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / A);
                    DesignReport.Add(string.Format("Minimum Radius of Gyration = ry = SQRT(Iy / Anet)"));
                    DesignReport.Add(string.Format("   = SQRT({0} / {1})", Iy, Anet));
                    DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    DesignReport.Add(string.Format(""));
                    l = (Length * 1000);

                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    #endregion Section 11
                    break;
                case eDefineSection.Section12:
                    #region Section 12
                    DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    //DesignReport.Add(string.Format("---------------------"));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, SectionDetails.SectionName, SectionDetails.SectionSize, SectionDetails.AngleThickness));


                    tabAngle = iApp.Tables.Get_AngleData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode, SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;

                    DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    //DesignReport.Add(string.Format("Top Plate Width = Bp = {0} mm", Bp));
                    //DesignReport.Add(string.Format("Top Plate Thickness = tp = {0} mm", tp));
                    //DesignReport.Add(string.Format("No Of Top Plate = np = {0}", np));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    DesignReport.Add(string.Format("Ixx = Iyy = {0} sq.sq.cm", Iyy));
                    DesignReport.Add(string.Format("Cxx = {0} cm", Cxx));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    DesignReport.Add(string.Format("------------------"));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("For Combined Section :"));
                    DesignReport.Add(string.Format("----------------------"));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));

                    Iy = 2 * Iyy * 10000;
                    //Iy = (Iyy * 10000 + a * 100 * Math.Pow(((S / 2.0) - (Cxx * 10 / 2.0)), 2.0)) * n + ((tp * Bp * Bp * Bp / 12.0) * np) + ((Bs * ts * ts * ts / 12.0) + Bs * ts * Math.Pow(((S / 2.0) + (ts / 2.0)), 2.0)) * ns;
                    DesignReport.Add(string.Format("Moment of Inertia = Iy = 2 * Iyy * 10000"));
                    DesignReport.Add(string.Format("                       = 2 * {0} * 10000", Iyy));
                    DesignReport.Add(string.Format("                       = {0} sq.sq.mm", Iy));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));

                    A = 2 * a * 100;
                    Anet = A;
                    DesignReport.Add(string.Format("A = 2 * a * 100 = 2 * {0} * 100 = {1} sq.mm", a, A));
                    //DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                    //    n, a, tp, Bp, np, ts, Bs, ns));
                    //DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / A);
                    DesignReport.Add(string.Format("Minimum Radius of Gyration = ry = SQRT(Iy / Anet)"));
                    DesignReport.Add(string.Format("   = SQRT({0} / {1})", Iy, Anet));
                    DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    DesignReport.Add(string.Format(""));
                    l = (Length * 1000);

                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    #endregion Section 12
                    break;

                case eDefineSection.Section13:
                    #region Section 13
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    DesignReport.Add(string.Format("---------------------"));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, SectionDetails.SectionName, SectionDetails.SectionSize, SectionDetails.AngleThickness));


                    tabAngle = iApp.Tables.Get_AngleData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode, SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;


                    Bp = Bs;
                    tp = ts;



                    DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    DesignReport.Add(string.Format("Central Plate Width = Bp = {0} mm", Bp));
                    DesignReport.Add(string.Format("Central Plate Thickness = tp = {0} mm", tp));
                    DesignReport.Add(string.Format("No Of Central Plate = np = {0}", np));
                    //DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Side Plate Width = Bs = {0} mm", Bs));
                    //DesignReport.Add(string.Format("Side Plate Thickness = ts = {0} mm", ts));
                    //DesignReport.Add(string.Format("No Of Side Plate = ns = {0}", ns));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    DesignReport.Add(string.Format("Ixx = Iyy = {0} sq.sq.cm", Iyy));
                    DesignReport.Add(string.Format("Cxx = {0} mm", Cxx));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    DesignReport.Add(string.Format("------------------"));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("For Combined Section :"));
                    DesignReport.Add(string.Format("----------------------"));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));

                    Iy = n * (Iyy * 10000 + a * 100 * Math.Pow((Cxx * 10 + tp / 2), 2.0)) + tp * (Bp * Bp * Bp / 12.0) * np;
                    DesignReport.Add(string.Format("Moment of Inertia = Iy =  n * (Iyy * 10000 + a * 100 * ((Cxx * 10 + tp / 2)^2)) + tp * (Bp * Bp * Bp / 12.0) * np"));
                    DesignReport.Add(string.Format("                       =  {0} * ({1} * 10000 + {2} * 100 * (({3} * 10 + {4} / 2)^2)) + {4} * ({5} * {5} * {5} / 12.0) * {6}", n,
                        Iyy, a, Cxx, tp, Bp, np));
                    DesignReport.Add(string.Format("                       = {0:E3} sq.sq.mm", Iy));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));

                    A = a * n * 100 + tp * Bp * np;
                    DesignReport.Add(string.Format("A = {0} * {1} * 100 + {2} * {3} * {4} = {5:f3} sq.mm", a, n, tp, Bp, np, A));
                    //DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                    //    n, a, tp, Bp, np, ts, Bs, ns));
                    //DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));


                    Anet = A - (bolt_dia + 1.5) * n * (2 * t + tp);
                    DesignReport.Add(string.Format("Anet = A - (bolt_dia + 1.5) * n * (2 * t + tp)"));
                    DesignReport.Add(string.Format("     = {0} - ({1} + 1.5) * {2} * (2 * {3} + {4})", A, bolt_dia, n, t, tp));
                    DesignReport.Add(string.Format("     = {0} sq.mm", Anet));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / A);
                    DesignReport.Add(string.Format("Minimum Radius of Gyration = ry = SQRT(Iy / Anet)"));
                    DesignReport.Add(string.Format("   = SQRT({0} / {1})", Iy, Anet));
                    DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    DesignReport.Add(string.Format(""));
                    l = (Length * 1000);

                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    #endregion Section 12
                    break;
                case eDefineSection.Section14:
                    #region Section 14
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    DesignReport.Add(string.Format("---------------------"));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("{0} x {1} {2}x{3}", n, SectionDetails.SectionName, SectionDetails.SectionSize, SectionDetails.AngleThickness));


                    tabAngle = iApp.Tables.Get_AngleData_FromTable(SectionDetails.SectionName, SectionDetails.SectionCode, SectionDetails.AngleThickness);
                    a = tabAngle.Area;
                    //D = tabAngle.Depth;
                    Iyy = tabAngle.Iyy;
                    Cxx = tabAngle.Cxx;
                    t = tabAngle.Thickness;


                    ns = 1;
                    DesignReport.Add(string.Format(""));
                    //DesignReport.Add(string.Format("Spacing = S = {0} mm", S));
                    DesignReport.Add(string.Format("Central Plate Width = Bp = {0} mm", Bs));
                    DesignReport.Add(string.Format("Central Plate Thickness = tp = {0} mm", ts));
                    DesignReport.Add(string.Format("No Of Central Plate = np = {0}", ns));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Top Plate Width = Bs = {0} mm", Bp));
                    DesignReport.Add(string.Format("Top Plate Thickness = ts = {0} mm", tp));
                    DesignReport.Add(string.Format("No Of Top Plate = ns = {0}", np));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Bottom Plate Width = Bs = {0} mm", Bbp));
                    DesignReport.Add(string.Format("Bottom Plate Thickness = ts = {0} mm", tbp));
                    DesignReport.Add(string.Format("No Of Bottom Plate = ns = {0}", nbp));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Area = a = {0} sq.cm", a));
                    DesignReport.Add(string.Format("Thickness = t = {0} mm", t));
                    //DesignReport.Add(string.Format("Flange Thickness = tf = {0} mm", tf));
                    DesignReport.Add(string.Format("Ixx = Iyy = {0} sq.sq.cm", Iyy));
                    DesignReport.Add(string.Format("Cxx = {0} mm", Cxx));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    DesignReport.Add(string.Format("------------------"));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("For Combined Section :"));
                    DesignReport.Add(string.Format("----------------------"));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));

                    Iy = n * (Iyy * 10000 + a * 100 * Math.Pow((Cxx * 10 + ts / 2), 2.0)) + ts * (Bs * Bs * Bs / 12.0) * ns + tp * (Bp * Bp * Bp / 12.0) * np + tbp * (Bbp * Bbp * Bbp / 12.0) * nbp;
                    DesignReport.Add(string.Format("Moment of Inertia = Iy =  n * (Iyy * 10000 + a * 100 * ((Cxx * 10 + tp / 2)^2)) + tp * (Bp * Bp * Bp / 12.0) * np + ts * (Bs * Bs * Bs / 12.0) * ns + tbp * (Bbp * Bbp * Bbp / 12.0) * nbp"));
                    DesignReport.Add(string.Format("                       =  {0} * ({1} * 10000 + {2} * 100 * (({3} * 10 + {4} / 2)^2)) + {4} * ({5}^3 / 12.0) * {6} + {7} * ({8}^3 / 12.0) * {9} + {10} * ({11}^3 / 12.0) * {12}", n,
                        Iyy, a, Cxx, tp, Bp, np, ts, Bs, ns, tbp, Bbp, nbp));
                    DesignReport.Add(string.Format("                       = {0:E3} sq.sq.mm", Iy));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));

                    A = a * n * 100 + tp * Bp * np + tbp * Bbp * nbp + ts * Bs * ns;
                    DesignReport.Add(string.Format("A = a * n * 100 + tp * Bp * np + tbp * Bbp * nbp + ts * Bs * ns"));
                    DesignReport.Add(string.Format("  = {0} * {1} * 100 + {2} * {3} * {4} + {5} * {6} * {7} + {8} * {9} * {10}", a, n, tp, Bp, np, tbp, Bbp, nbp, ts, Bs, ns));
                    DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));
                    //DesignReport.Add(string.Format("  = {0} * {1} * 100 + ({2} * {3} * {4}) + ({5} * {6} * {7})",
                    //    n, a, tp, Bp, np, ts, Bs, ns));
                    //DesignReport.Add(string.Format("  = {0:f3} sq.mm", A));


                    Anet = A - ((bolt_dia + 1.5) * n * (2 * t + ts));
                    DesignReport.Add(string.Format("Anet = A - (bolt_dia + 1.5) * n * (2 * t + ts)"));
                    DesignReport.Add(string.Format("     = {0} - ({1} + 1.5) * {2} * (2 * {3} + {4})", A, bolt_dia, n, t, ts));
                    DesignReport.Add(string.Format("     = {0} sq.mm", Anet));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / A);
                    DesignReport.Add(string.Format("Minimum Radius of Gyration = ry = SQRT(Iy / Anet)"));
                    DesignReport.Add(string.Format("   = SQRT({0:E3} / {1})", Iy, Anet));
                    DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    DesignReport.Add(string.Format(""));
                    l = (Length * 1000);

                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    #endregion Section 12
                    break;
                case eDefineSection.Section16:
                    #region Section 16
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    DesignReport.Add(string.Format("---------------------"));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Diameter of Arch Section = D1 = {0} mm", SectionDetails.Cables_Strands.Diameter));
                    DesignReport.Add(string.Format("Thickness of Arch Section = t = {0} mm", SectionDetails.Cables_Strands.Thickness));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));


                    DesignReport.Add(string.Format("Area of Cross Section = A = (π x D1^2 / 4) - (π x (D1 - 2 x t)^2 / 4)"));
                    DesignReport.Add(string.Format("                          = ({0:f4} x {1}^2 / 4) - (π x ({1} - 2 x {2})^2 / 4)",
                               Math.PI, SectionDetails.Cables_Strands.Diameter, SectionDetails.Cables_Strands.Thickness));

                    a = SectionDetails.Cables_Strands.Area;
                    Iyy = SectionDetails.Cables_Strands.Iyy;
                    Cxx = 0;
                    A = a;
                    t = SectionDetails.Cables_Strands.Thickness;

                    DesignReport.Add(string.Format("                          = {0:f4} Sq.mm", a));

                    ns = 1;
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    DesignReport.Add(string.Format("------------------"));
                    DesignReport.Add(string.Format(""));
                    Iy = Iyy;
                    //DesignReport.Add(string.Format("Moment of Inertia = Iy = (π/4) x (D1^4 - ((D1 - 2 x t)^4)"));
                    DesignReport.Add(string.Format("                   Ixx = (π/4) x (D1^4 - ((D1 - 2 x t)^4)"));
                    DesignReport.Add(string.Format("                       = ({0:f3}/4) x ({1}^4 - (({1} - 2 x {2})^4)", Math.PI, SectionDetails.Cables_Strands.Diameter
                                                                        , SectionDetails.Cables_Strands.Thickness));
                    DesignReport.Add(string.Format("                       = {0:f3} Sq.Sq.mm", Iy));
                    DesignReport.Add(string.Format(""));


                    DesignReport.Add(string.Format("                   Iyy = (π/4) x (D1^4 - ((D1 - 2 x t)^4)"));
                    DesignReport.Add(string.Format("                       = ({0:f3}/4) x ({1}^4 - (({1} - 2 x {2})^4)", Math.PI, SectionDetails.Cables_Strands.Diameter
                                                                        , SectionDetails.Cables_Strands.Thickness));
                    DesignReport.Add(string.Format("                       = {0:f3} Sq.Sq.mm", Iy));
                    DesignReport.Add(string.Format(""));

                    DesignReport.Add(string.Format("                   Zxx = Ixx / (D1/2.0)"));
                    DesignReport.Add(string.Format("                       = {0:E3} / ({1}/2.0)", Iyy, SectionDetails.Cables_Strands.Diameter));
                    DesignReport.Add(string.Format("                       = {0:f3} Cu.mm", SectionDetails.Cables_Strands.Zxx));
                    DesignReport.Add(string.Format(""));

                    DesignReport.Add(string.Format("                   Zyy = Iyy / (D1/2.0)"));
                    DesignReport.Add(string.Format("                       = {0:E3} / ({1}/2.0)", Iyy, SectionDetails.Cables_Strands.Diameter));
                    DesignReport.Add(string.Format("                       = {0:f3} Cu.mm", SectionDetails.Cables_Strands.Zxx));

                    DesignReport.Add(string.Format(""));

                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / A);


                    DesignReport.Add(string.Format("Minimum Radius of Gyration = ry = SQRT(Iy / A)"));
                    DesignReport.Add(string.Format("   = SQRT({0:E3} / {1})", Iy, A));
                    DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    DesignReport.Add(string.Format(""));
                    l = (Length * 1000);

                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    #endregion Section 16
                    break;
                case eDefineSection.Section17:
                    #region Section 17
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("SELECTED SECTION DATA"));
                    DesignReport.Add(string.Format("---------------------"));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("Diameter of Cable = D1 = {0} mm", SectionDetails.Cables_Strands.Diameter));
                    //DesignReport.Add(string.Format("Thickness of Arch Section = t = {0} mm", SectionDetails.Cables_Strands.Thickness));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));


                    DesignReport.Add(string.Format("Area of Cross Section = A = (π x D1^2 / 4)"));
                    DesignReport.Add(string.Format("                          = ({0:f4} x {1}^2 / 4)", Math.PI, SectionDetails.Cables_Strands.Diameter));

                    a = SectionDetails.Cables_Strands.Area;
                    Iyy = SectionDetails.Cables_Strands.Iyy;
                    Cxx = 0;
                    A = a;
                    t = SectionDetails.Cables_Strands.Thickness;

                    DesignReport.Add(string.Format("                          = {0:f4} Sq.mm", a));

                    ns = 1;
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format("DESIGN CALCULATION"));
                    DesignReport.Add(string.Format("------------------"));
                    DesignReport.Add(string.Format(""));
                    Iy = Iyy;
                    //DesignReport.Add(string.Format("Moment of Inertia = Iy = (π/4) x (D1^4 - ((D1 - 2 x t)^4)"));
                 
                    DesignReport.Add(string.Format("                   Ixx = (π/4) x (D1^4)"));
                    DesignReport.Add(string.Format("                       = ({0:f3}/4) x ({1}^4)", Math.PI, SectionDetails.Cables_Strands.Diameter));
                    DesignReport.Add(string.Format("                       = {0:f3} Sq.Sq.mm", Iy));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));


                    DesignReport.Add(string.Format("                   Iyy = (π/4) x (D1^4)"));
                    DesignReport.Add(string.Format("                       = ({0:f3}/4) x ({1}^4)", Math.PI, SectionDetails.Cables_Strands.Diameter));
                    DesignReport.Add(string.Format("                       = {0:f3} Sq.Sq.mm", Iy));
                    DesignReport.Add(string.Format(""));

                    DesignReport.Add(string.Format("                   Zxx = Ixx / (D1/2.0)"));
                    DesignReport.Add(string.Format("                       = {0:E3} / ({1}/2.0)", Iyy, SectionDetails.Cables_Strands.Diameter));
                    DesignReport.Add(string.Format("                       = {0:f3} Cu.mm",  SectionDetails.Cables_Strands.Zxx));
                    DesignReport.Add(string.Format(""));

                    DesignReport.Add(string.Format("                   Zyy = Iyy / (D1/2.0)"));
                    DesignReport.Add(string.Format("                       = {0:E3} / ({1}/2.0)", Iyy, SectionDetails.Cables_Strands.Diameter));
                    DesignReport.Add(string.Format("                       = {0:f3} Cu.mm",  SectionDetails.Cables_Strands.Zxx));

                    DesignReport.Add(string.Format(""));

                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));

                    ry = Math.Sqrt(Iy / A);


                    DesignReport.Add(string.Format("Minimum Radius of Gyration = ry = SQRT(Iy / A)"));
                    DesignReport.Add(string.Format("   = SQRT({0:E3} / {1})", Iy, A));
                    DesignReport.Add(string.Format("   = {0:f3} mm", ry));
                    DesignReport.Add(string.Format(""));
                    l = (Length * 1000);

                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    DesignReport.Add(string.Format(""));
                    #endregion Section 17
                    break;
            }
            Ix = Ixx;
            #endregion Define Section
        }



        public int NoOfMember
        {
            get
            {
                return Group.MemberNos.Count;
            }
        }
        public List<string> DesignReport { get; set; }
    }
    [Serializable]
    public enum eMemberType
    {
        NoSelection = -1,
        StringerBeam = 0,
        CrossGirder = 1,
        BottomChord = 2,
        TopChord = 3,
        EndRakers = 4,
        DiagonalMember = 5,
        VerticalMember = 6,
        TopChordBracings = 7,
        BottomChordBracings = 8,
        CantileverBrackets = 9,
        ShortVerticalMember = 10,


        TopDiagonalMember = 11,
        BottomDiagonalMember = 12,
        TopVerticalMember = 13,
        BottomVerticalMember = 14,

        ShortDiagonalMember = 15,

        ArchMembers = 16,
        SuspensionCables = 17,
        TransverseMember = 18,

        AllMember = 19,


        TowerMember = 20,


    }
    [Serializable]

    public enum eDefineSection
    {
        NoSelection = -1,
        Section1 = 0,
        Section2 = 1,
        Section3 = 2,
        Section4 = 3,
        Section5 = 4,
        Section6 = 5,
        Section7 = 6,
        Section8 = 7,
        Section9 = 8,
        Section10 = 9,
        Section11 = 10,
        Section12 = 11,
        Section13 = 12,
        Section14 = 13,
        Section15 = 14,
        Section16 = 15,
        Section17 = 16,
    }
    [Serializable]
    public class SectionData
    {
        public SectionData()
        {
            DefineSection = eDefineSection.NoSelection;
            SectionName = "";
            SectionCode = "";
            AngleThickness = 0.0;
            TopPlate = new Plate();
            BottomPlate = new Plate();
            SidePlate = new Plate();
            VerticalStiffenerPlate = new Plate();
            BoltDia = 20;
            LateralSpacing = 0.0;
            NoOfBolts = 2;
            //NoOfElements = 2;


            Cables_Strands = new Cables();
        }
        public eDefineSection DefineSection { get; set; }
        public string SectionName { get; set; }
        public string SectionCode { get; set; }
        public string SectionSize
        {
            get
            {
                string kStr = SectionCode;
                if (!kStr.ToUpper().Contains("X"))
                {
                    if (kStr.Length % 2 != 0)
                    {
                        int i = kStr.Length / 2 + 1;
                        kStr = SectionCode.Substring(0, i) + "X" + SectionCode.Substring(i, SectionCode.Length - (i));
                    }
                    else
                    {
                        int i = kStr.Length / 2;
                        kStr = SectionCode.Substring(0, i) + "X" + SectionCode.Substring(i, SectionCode.Length - (i));
                    }
                }
                return kStr;
            }
        }

        public double AngleThickness { get; set; }
        public Plate TopPlate { get; set; }
        public Plate BottomPlate { get; set; }
        public Plate SidePlate { get; set; }
        public Plate VerticalStiffenerPlate { get; set; }

        public Cables Cables_Strands { get; set; }


        public double NoOfElements { get; set; }
        public double BoltDia { get; set; }
        public double LateralSpacing { get; set; }
        public int NoOfBolts { get; set; }

        //Chiranjit [2011 10 25]
        //
        //public override string ToString()
        //{
        //    string kStr = string.Format("{0} x {1} {2}", NoOfElements, SectionName, SectionCode);
        //    if (SectionName.Contains("ISA"))
        //    {
        //        kStr = string.Format("{0} x {1} {2}x{3}", NoOfElements, SectionName, SectionCode, AngleThickness);
        //    }
        //    return kStr;
        //}
        public override string ToString()
        {
            string kStr = string.Format("{0} x {1} {2}", NoOfElements, SectionName, SectionCode);
            if (SectionName.Contains("ISA"))
            {
                kStr = string.Format("{0} x {1} {2}x{3}", NoOfElements, SectionName, SectionSize, AngleThickness);
            }
            return kStr;
        }

    }
    [Serializable]
    public class Plate
    {
        public Plate()
        {
            Width = 0.0;
            Thickness = 0.0;
            Length = 0.0;
            TotalPlates = 0;
        }
        public double Width { get; set; }
        public double Thickness { get; set; }
        public double Length { get; set; }
        public int TotalPlates { get; set; }

        public double Area
        {
            get
            {
                return Width * Thickness;
            }
        }

        public double Weight
        {
            get
            {
                return WeightPerMetre * Length * TotalPlates;
            }
        }
        public double WeightPerMetre
        {
            get
            {
                return Width * Thickness * 0.00007844d;
            }
        }
        public override string ToString()
        {
            if (Area == 0.0) return "";
            return string.Format("{0}x{1}", Width, Thickness);
        }
    }

    public class Cables
    {
        public Cables()
        {
            Diameter = 0.0;
            Thickness = 0.0;
        }
        public double Diameter { get; set; }
        public double Thickness { get; set; }
        public double Area
        {
            get
            {

                double d = 0.0;
                if (Thickness == 0.0)
                    d = (Math.PI * Math.Pow(Diameter, 2.0) / 4.0);
                else
                    d = (Math.PI * Math.Pow(Diameter, 2.0) / 4.0) - (Math.PI * (Math.Pow((Diameter - 2 * Thickness), 2.0) / 4.0));

                return d;
            }
        }
        public double Ixx
        {
            get
            {

                double d = 0.0;
                if(Thickness > 0)
                    d = (Math.PI / 64.0) * (Math.Pow(Diameter, 4.0) - Math.Pow(Diameter - 2 * Thickness, 4.0));
                else
                    d = (Math.PI / 64.0) * (Math.Pow(Diameter, 4.0));

                return d;
            }
        }
        public double Iyy
        {
            get
            {
                return Ixx;
            }
        }
        public double Zxx
        {
            get
            {
                return Ixx / (Diameter / 2.0);
            }
        }

        public double Zyy
        {
            get
            {
                return Iyy / (Diameter / 2.0);
            }
        }

        public double rxx
        {
            get
            {
                return Math.Sqrt(Ixx / Area);
            }
        }

        public double ryy
        {
            get
            {
                return Math.Sqrt(Iyy / Area);
            }
        }

        public double Weight
        {
            get
            {
                return WeightPerMetre * 1;
            }
        }
        public double WeightPerMetre
        {
            get
            {
                return Area * 0.00007844d;
            }
        }
        public override string ToString()
        {
            if (Area == 0.0) return "";
            return string.Format("{0}x{1}", Diameter, Thickness);
        }
    }


    public class SuperInposedDeadLoad
    {
        public SuperInposedDeadLoad(string dead_load_name)
        {
            Name = dead_load_name;
            Length = 0.0;
            Breadth = 0.0;
            Depth = 1;
            TotalNo = 0;
            Gamma = 24.0;
        }
        public string Name { get; set; }
        public double Length { get; set; }
        public double Breadth { get; set; }
        public double Depth { get; set; }
        public int TotalNo { get; set; }
        public double Volume
        {
            get
            {
                if (Breadth == 0.0)
                {
                    return Length * TotalNo;
                }
                if (Depth == 0.0)
                {
                    return Length * Breadth * TotalNo;
                }
                return Length * Breadth * Depth * TotalNo;
            }
        }
        /// <summary>
        /// Unit Weight
        /// </summary>
        public double Gamma { get; set; }
        public double UnitWeight
        {
            get
            {
                return Gamma;
            }
            set
            {
                Gamma = value;
            }
        }
        public double Weight
        {
            get
            {
                return Volume * Gamma;
            }
        }
    }
    public class TotalDeadLoad
    {
        public TotalDeadLoad()
        {
            IsRailLoad = false;
            Load_List = new List<SuperInposedDeadLoad>();

            DeckSlab = new SuperInposedDeadLoad("DECK SLAB");
            Kerb = new SuperInposedDeadLoad("KERB");
            FootPathSlab = new SuperInposedDeadLoad("FOOTPATH SLAB");
            OuterBeam = new SuperInposedDeadLoad("OUTER BEAM");
            WearingCoat = new SuperInposedDeadLoad("WEARING COAT");
            Railing = new SuperInposedDeadLoad("RAILING");
            LiveLoadOnFootPath = new SuperInposedDeadLoad("LIVE LOAD FOOT PATH");
            Rail = new RailLoad();
            SetLoads();


        }
        public void SetLoads()
        {
            if(DeckSlab.Weight > 0)
            Load_List.Add(DeckSlab);
            if (Kerb.Weight > 0)
                Load_List.Add(Kerb);
            if (FootPathSlab.Weight > 0)
                Load_List.Add(FootPathSlab);
            if (OuterBeam.Weight > 0)
                Load_List.Add(OuterBeam);
            if (WearingCoat.Weight > 0)
                Load_List.Add(WearingCoat);
            if (Railing.Weight > 0)
                Load_List.Add(Railing);
            if (LiveLoadOnFootPath.Weight > 0)
                Load_List.Add(LiveLoadOnFootPath);
        }
        public List<SuperInposedDeadLoad> Load_List { get; set; }
        public RailLoad Rail { get; set; }
        public SuperInposedDeadLoad DeckSlab { get; set; }
        public SuperInposedDeadLoad Kerb { get; set; }
        public SuperInposedDeadLoad FootPathSlab { get; set; }
        public SuperInposedDeadLoad OuterBeam { get; set; }
        public SuperInposedDeadLoad WearingCoat { get; set; }
        public SuperInposedDeadLoad Railing { get; set; }
        public SuperInposedDeadLoad LiveLoadOnFootPath { get; set; }
        public SuperInposedDeadLoad RailPermanentLoadAsOpenFloor { get; set; }
        public SuperInposedDeadLoad RailBendingMoment { get; set; }
        public SuperInposedDeadLoad RailShearForce { get; set; }
        public bool IsRailLoad { get; set; }

        public double TotalWeight
        {
            get
            {
                if (IsRailLoad)
                    return RailPermanentLoadAsOpenFloor.Weight + RailShearForce.Weight + RailBendingMoment.Weight;

                return DeckSlab.Weight + Kerb.Weight + FootPathSlab.Weight + OuterBeam.Weight + WearingCoat.Weight + Railing.Weight + LiveLoadOnFootPath.Weight;
            }
        }

        public void ToStream(StreamWriter sw)
        {
            string kStr = "";
            try
            {
                if (IsRailLoad == false)
                {
                    sw.WriteLine("------------------------------------------------------------------------------------------------------");
                    kStr = string.Format("{0,-25} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10} {7,10}",
                        "",
                        "",
                        "",
                        "",
                        "Total",
                        "",
                        "Unit",
                        "");
                    sw.WriteLine(kStr);
                    kStr = string.Format("{0,-25} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10} {7,10}",
                        "Name",
                        "Length",
                        "Breadth",
                        "Depth",
                        "Nos",
                        "Volume",
                        "Weight",
                        "Weight");
                    sw.WriteLine(kStr);
                    kStr = string.Format("{0,-25} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10} {7,10}",
                        "",
                        " (m)",
                        " (m)",
                        " (m)",
                        " Nos",
                        " (cu.m)",
                        " (kN/m) ",
                        "  (kN)");
                    sw.WriteLine(kStr);
                    sw.WriteLine("------------------------------------------------------------------------------------------------------");

                    double total_vol = 0.0;
                    double total_wgt = 0.0;
                    foreach (SuperInposedDeadLoad item in Load_List)
                    {
                        sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        item.Name,
                        item.Length,
                        item.Breadth,
                        item.Depth,
                        item.TotalNo,
                        item.Volume,
                        item.Gamma,
                        item.Weight);

                        total_vol += item.Volume;
                        total_wgt += item.Weight;
                    }

                    sw.WriteLine("------------------------------------------------------------------------------------------------------");
                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        "",
                        "",
                        "",
                        "",
                        "TOTAL",
                        total_vol,
                        "",
                        total_wgt);

                    sw.WriteLine("------------------------------------------------------------------------------------------------------");
                }
                else
                {
                    Rail.ToStream(sw);
                }
                sw.WriteLine();
                sw.WriteLine();
            }
            catch (Exception ex) { }
        }
        public void ToStream1(StreamWriter sw)
        {
            string kStr = "";
            try
            {
                if (IsRailLoad == false)
                {
                    sw.WriteLine("------------------------------------------------------------------------------------------------------");
                    kStr = string.Format("{0,-25} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10} {7,10}",
                        "",
                        "",
                        "",
                        "",
                        "Total",
                        "",
                        "Unit",
                        "");
                    sw.WriteLine(kStr);
                    kStr = string.Format("{0,-25} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10} {7,10}",
                        "Name",
                        "Length",
                        "Breadth",
                        "Depth",
                        "Nos",
                        "Volume",
                        "Weight",
                        "Weight");
                    sw.WriteLine(kStr);
                    sw.WriteLine("------------------------------------------------------------------------------------------------------");

                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        DeckSlab.Name,
                        DeckSlab.Length,
                        DeckSlab.Breadth,
                        DeckSlab.Depth,
                        DeckSlab.TotalNo,
                        DeckSlab.Volume,
                        DeckSlab.Gamma,
                        DeckSlab.Weight);

                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        Kerb.Name,
                        Kerb.Length,
                        Kerb.Breadth,
                        Kerb.Depth,
                        Kerb.TotalNo,
                        Kerb.Volume,
                        Kerb.Gamma,
                        Kerb.Weight);

                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        FootPathSlab.Name,
                        FootPathSlab.Length,
                        FootPathSlab.Breadth,
                        FootPathSlab.Depth,
                        FootPathSlab.TotalNo,
                        FootPathSlab.Volume,
                        FootPathSlab.Gamma,
                        FootPathSlab.Weight);
                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        OuterBeam.Name,
                        OuterBeam.Length,
                        OuterBeam.Breadth,
                        OuterBeam.Depth,
                        OuterBeam.TotalNo,
                        OuterBeam.Volume,
                        OuterBeam.Gamma,
                        OuterBeam.Weight);

                    double total_vol = DeckSlab.Volume + Kerb.Volume + FootPathSlab.Volume + OuterBeam.Volume;
                    double total_wgt = DeckSlab.Weight + Kerb.Weight + FootPathSlab.Weight + OuterBeam.Weight;
                    sw.WriteLine("------------------------------------------------------------------------------------------------------");
                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        "",
                        "",
                        "",
                        "",
                        "TOTAL",
                        total_vol,
                        "",
                        total_wgt);

                    sw.WriteLine("------------------------------------------------------------------------------------------------------");
                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        WearingCoat.Name,
                        WearingCoat.Length,
                        WearingCoat.Breadth,
                        "",
                        WearingCoat.TotalNo,
                        "",
                        WearingCoat.Gamma,
                        WearingCoat.Weight);
                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        Railing.Name,
                        Railing.Length,
                        Railing.Breadth,
                        "",
                        Railing.TotalNo,
                        "",
                        Railing.Gamma,
                        Railing.Weight);
                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        LiveLoadOnFootPath.Name,
                        LiveLoadOnFootPath.Length,
                        LiveLoadOnFootPath.Breadth,
                        "",
                        LiveLoadOnFootPath.TotalNo,
                        "",
                        LiveLoadOnFootPath.Gamma,
                        LiveLoadOnFootPath.Weight);

                    sw.WriteLine("------------------------------------------------------------------------------------------------------");
                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                       "",
                       "",
                       "",
                       "",
                       "TOTAL",
                       "WEIGHT",
                       "",
                       TotalWeight);
                    sw.WriteLine("------------------------------------------------------------------------------------------------------");
                }
                else
                {
                    Rail.ToStream(sw);
                }
                sw.WriteLine();
                sw.WriteLine();
            }
            catch (Exception ex) { }
        }
        public void ReadFromFile(string file_name)
        {
            List<string> file_content = new List<string>(File.ReadAllLines(file_name));
            try
            {
                string kStr = "";
                MyList mList = null;
                int index = 0;

                Load_List.Clear();
                foreach (string line in file_content)
                {
                    kStr = MyList.RemoveAllSpaces(line.ToUpper());
                    //Name                Length    Breadth      Depth  Total Nos     Volume Unit Weight  Weight
                    //DECK SLAB           61.000      3.750      0.230          2    105.225         24   2525.400
                    //KERB                61.000      0.230      0.510          2     14.311         24    343.454
                    //FOOTPATH SLAB       61.000      1.880      0.100          2     22.936         24    550.464
                    //OUTER BEAM          61.000      0.150      0.510          2      9.333         24    223.992
                    //WEARING COAT        61.000      3.750                     2                     2    915.000
                    //RAILING             61.000      0.000                     2                   1.6    195.200
                    //LIVE LOAD FOOT PATH 61.000      1.980                     2                  1.92    463.795
                    mList = new MyList(kStr, ' ');
                    if (mList.StringList[0].StartsWith("RAIL PER"))
                    {
                        mList = new MyList(kStr, '=');
                        kStr = MyList.RemoveAllSpaces(mList.StringList[1]);
                        mList = new MyList(kStr, ' ');
                        Rail.PermanentLoad = mList.GetDouble(0);
                        IsRailLoad = true;
                    }
                    if (kStr.StartsWith("EFFECTIVE SPAN"))
                    {
                        mList = new MyList(kStr, '=');
                        kStr = MyList.RemoveAllSpaces(mList.StringList[1]);
                        mList = new MyList(kStr, ' ');
                        Rail.EffectiveSpan = mList.GetDouble(0);
                        IsRailLoad = true;
                        continue;
                    }
                    if (kStr.Contains("BENDING MOMENT"))
                    {
                        mList = new MyList(kStr, '=');
                        kStr = MyList.RemoveAllSpaces(mList.StringList[1]);
                        mList = new MyList(kStr, ' ');
                        Rail.BendingMoment = mList.GetDouble(0);
                        IsRailLoad = true;
                        continue;
                    }
                    if (kStr.Contains("SHEAR FORCE"))
                    {
                        mList = new MyList(kStr, '=');
                        kStr = MyList.RemoveAllSpaces(mList.StringList[1]);
                        mList = new MyList(kStr, ' ');
                        Rail.ShearForce = mList.GetDouble(0);
                        IsRailLoad = true;
                        continue;
                    }
                    if (mList.StringList[0].StartsWith("DECK"))
                    {
                        index = 2;
                        DeckSlab.Length = mList.GetDouble(index); index++;
                        DeckSlab.Breadth = mList.GetDouble(index); index++;
                        DeckSlab.Depth = mList.GetDouble(index); index++;
                        DeckSlab.TotalNo = mList.GetInt(index); index++; index++;
                        DeckSlab.Gamma = mList.GetDouble(index); index++;
                        IsRailLoad = false;
                        continue;
                    }
                    if (mList.StringList[0].StartsWith("KERB"))
                    {
                        index = 1;
                        Kerb.Length = mList.GetDouble(index); index++;
                        Kerb.Breadth = mList.GetDouble(index); index++;
                        Kerb.Depth = mList.GetDouble(index); index++;
                        Kerb.TotalNo = mList.GetInt(index); index++; index++;
                        Kerb.Gamma = mList.GetDouble(index); index++;
                        IsRailLoad = false;
                        continue;
                    }
                    if (mList.StringList[0].StartsWith("FOOTPATH"))
                    {
                        index = 2;
                        FootPathSlab.Length = mList.GetDouble(index); index++;
                        FootPathSlab.Breadth = mList.GetDouble(index); index++;
                        FootPathSlab.Depth = mList.GetDouble(index); index++;
                        FootPathSlab.TotalNo = mList.GetInt(index); index++; index++;
                        FootPathSlab.Gamma = mList.GetDouble(index); index++;
                        IsRailLoad = false;
                        continue;
                    }
                    if (mList.StringList[0].StartsWith("OUTER"))
                    {
                        index = 2;
                        OuterBeam.Length = mList.GetDouble(index); index++;
                        OuterBeam.Breadth = mList.GetDouble(index); index++;
                        OuterBeam.Depth = mList.GetDouble(index); index++;
                        OuterBeam.TotalNo = mList.GetInt(index); index++; index++;
                        OuterBeam.Gamma = mList.GetDouble(index); index++;
                        IsRailLoad = false;
                        continue;
                    }
                    if (mList.StringList[0].StartsWith("WEARING"))
                    {
                        index = 2;
                        WearingCoat.Length = mList.GetDouble(index); index++;
                        WearingCoat.Breadth = mList.GetDouble(index); index++;
                        WearingCoat.Depth = mList.GetDouble(index); index++;
                        WearingCoat.TotalNo = mList.GetInt(index); index++; index++;
                        WearingCoat.Gamma = mList.GetDouble(index); index++;
                        IsRailLoad = false;
                        continue;
                    }
                    if (mList.StringList[0].StartsWith("RAILING"))
                    {
                        index = 1;
                        Railing.Length = mList.GetDouble(index); index++;
                        Railing.Breadth = mList.GetDouble(index); index++;
                        Railing.Depth = mList.GetDouble(index); index++;
                        Railing.TotalNo = mList.GetInt(index); index++; index++;
                        Railing.Gamma = mList.GetDouble(index); index++;
                        IsRailLoad = false;
                        continue;
                    }
                    if (mList.StringList[0].StartsWith("LIVE"))
                    {
                        index = 4;
                        LiveLoadOnFootPath.Length = mList.GetDouble(index); index++;
                        LiveLoadOnFootPath.Breadth = mList.GetDouble(index); index++;
                        LiveLoadOnFootPath.Depth = mList.GetDouble(index); index++;
                        LiveLoadOnFootPath.TotalNo = mList.GetInt(index); index++; index++; 
                        LiveLoadOnFootPath.Gamma = mList.GetDouble(index); index++;
                        throw new Exception();
                        IsRailLoad = false;
                        continue;
                    }
                }
                //SetLoads();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                SetLoads();
                file_content.Clear();
                file_content = null;
            }

        }
        public double Weight
        {
            get
            {
                if (IsRailLoad)
                    return Rail.ShearForce + Rail.BendingMoment;
                double wgt = 0.0;

                for (int i = 0; i < Load_List.Count; i++)
                {
                    wgt += Load_List[i].Weight;
                }
                //return DeckSlab.Weight + Kerb.Weight + FootPathSlab.Weight + OuterBeam.Weight + WearingCoat.Weight + Railing.Weight + LiveLoadOnFootPath.Weight;
                return wgt;
            }
        }
    }
    public class RailLoad
    {
        public RailLoad()
        {
            EffectiveSpan = 0.0;
            PermanentLoad = 0.0;
            BendingMoment = 0.0;
            ShearForce = 0.0;
            //EffectiveSpan = 6.0;
            //PermanentLoad = 7.5;
            //BendingMoment = 2727.0;
            //ShearForce = 2927.0;
        }
        public double EffectiveSpan { get; set; }
        public double PermanentLoad { get; set; }
        public double BendingMoment { get; set; }
        public double ShearForce { get; set; }
        public double TotalLoad 
        {
            get
            {
                return 0;
            }
        }
        public void ToStream(StreamWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Effective span of each stringer beam segment (c/c spacing of cross girder) = {0} m", EffectiveSpan);
            sw.WriteLine();
            sw.WriteLine("Rail Permanent Load as Open Floor = {0} kN/m", PermanentLoad);
            sw.WriteLine();
            sw.WriteLine("Equivalent Total moving load per track for Bending Moment Calculation = {0} kN", BendingMoment);
            sw.WriteLine();
            sw.WriteLine("Equivalent Total moving load per track for Shear force Calculation = {0} kN", ShearForce);
            sw.WriteLine();
        }
    }
    public static class MemberString
    {
        const string StringerBeam = "STRINGER BEAM";
        const string CrossGirder = "CROSS GIRDER";
        const string BottomChord = "BOTTOM CHORD";
        const string TopChord = "TOP CHORD";
        const string EndRakers = "END RAKERS";
        const string DiagonalMember = "DIAGONAL MEMBER";
        const string VerticalMember = "VERTICAL MEMBER";
        const string TopChordBracing = "TOP CHORD BRACING";
        const string BottomChordBracing = "BOTTOM CHORD BRACING";
        const string CantileverBracket = "CANTILEVER BRACKET";



        const string ShortVerticalMember = "SHORT VERTICAL MEMBER";


        const string TopDiagonalMember = "TOP DIAGONAL MEMBER";
        const string BottomDiagonalMember = "BOTTOM DIAGONAL MEMBER";
        const string TopVerticalMember = "TOP VERTICAL MEMBER";
        const string BottomVerticalMember = "BOTTOM VERTICAL MEMBER";

        const string ShortDiagonalMember = "SHORT DIAGONAL MEMBER";

        const string ArchMembers = "ARCH MEMBERS";
        const string SuspensionCables = "SUSPENSION CABLES";
        const string TransverseMember = "TRANSVERSE MEMBER";



        public const string Section1 = "SECTION-1, [FIG-1]";
        public const string Section2 = "SECTION-2, [FIG-2]";
        public const string Section3 = "SECTION-3, [FIG-3]";
        public const string Section4 = "SECTION-4, [FIG-4]";
        public const string Section5 = "SECTION-5, [FIG-5]";
        public const string Section6 = "SECTION-6, [FIG-6]";
        public const string Section7 = "SECTION-7, [FIG-7]";
        public const string Section8 = "SECTION-8, [FIG-8]";
        public const string Section9 = "SECTION-9, [FIG-9]";
        public const string Section10 = "SECTION-10, [FIG-10]";
        public const string Section11 = "SECTION-11, [FIG-11]";
        public const string Section12 = "SECTION-12, [FIG-12]";
        public const string Section13 = "SECTION-13, [FIG-13]";
        public const string Section14 = "SECTION-14, [FIG-14]";
        public const string Section15 = "SECTION-15, [FIG-15]";
        public const string Section16 = "SECTION-16, [FIG-16]";
        public const string Section17 = "SECTION-17, [FIG-17]";
        public const string Section18 = "SECTION-18, [FIG-18]";
        public const string Section19 = "SECTION-19, [FIG-19]";
        public const string Section20 = "SECTION-20, [FIG-20]";


        public static string GerMemberString(CMember mbr)
        {
            string str = "";
            switch (mbr.MemberType)
            {
                case eMemberType.BottomChord:
                    str = BottomChord;
                    break;
                case eMemberType.TopChord:
                    str = TopChord;
                    break;
                case eMemberType.CrossGirder:
                    str = CrossGirder;
                    break;
                case eMemberType.StringerBeam:
                    str = StringerBeam;
                    break;
                case eMemberType.BottomChordBracings:
                    str = BottomChordBracing;
                    break;
                case eMemberType.TopChordBracings:
                    str = TopChordBracing;
                    break;
                case eMemberType.CantileverBrackets:
                    str = CantileverBracket;
                    break;
                case eMemberType.DiagonalMember:
                    str = DiagonalMember;
                    break;
                case eMemberType.EndRakers:
                    str = EndRakers;
                    break;
                case eMemberType.VerticalMember:
                    str = VerticalMember;
                    break;
                case eMemberType.ShortVerticalMember:
                    str = ShortVerticalMember;
                    break;
                case eMemberType.BottomDiagonalMember:
                    str = BottomDiagonalMember;
                    break;
                case eMemberType.BottomVerticalMember:
                    str = BottomVerticalMember;
                    break;
                case eMemberType.TopVerticalMember:
                    str = TopVerticalMember;
                    break;
                case eMemberType.TopDiagonalMember:
                    str = TopDiagonalMember;
                    break;
                case eMemberType.ShortDiagonalMember:
                    str = ShortDiagonalMember;
                    break;
                case eMemberType.ArchMembers:
                    str = ArchMembers;
                    break;
                case eMemberType.SuspensionCables:
                    str = SuspensionCables;
                    break;
                case eMemberType.TransverseMember:
                    str = TransverseMember;
                    break;
            }

            switch (mbr.SectionDetails.DefineSection)
            {
                case eDefineSection.Section1:
                    str = str + "    " + Section1;
                    break;
                case eDefineSection.Section2:
                    str = str + "    " + Section2;
                    break;
                case eDefineSection.Section3:
                    str = str + "    " + Section3;
                    break;
                case eDefineSection.Section4:
                    str = str + "    " + Section4;
                    break;
                case eDefineSection.Section5:
                    str = str + "    " + Section5;
                    break;
                case eDefineSection.Section6:
                    str = str + "    " + Section6;
                    break;
                case eDefineSection.Section7:
                    str = str + "    " + Section7;
                    break;
                case eDefineSection.Section8:
                    str = str + "    " + Section8;
                    break;
                case eDefineSection.Section9:
                    str = str + "    " + Section9;
                    break;
                case eDefineSection.Section10:
                    str = str + "    " + Section10;
                    break;
                case eDefineSection.Section11:
                    str = str + "    " + Section11;
                    break;
                case eDefineSection.Section12:
                    str = str + "    " + Section12;
                    break;
                case eDefineSection.Section13:
                    str = str + "    " + Section13;
                    break;
                case eDefineSection.Section14:
                    str = str + "    " + Section14;
                    break;
                case eDefineSection.Section15:
                    str = str + "    " + Section15;
                    break;
                case eDefineSection.Section16:
                    str = str + "    " + Section16;
                    break;
                case eDefineSection.Section17:
                    str = str + "    " + Section17;
                    break;
            }

            return str;
        }


    }

    //Chiranjit [2011 10 25] this class is used for force and stress
    public class MaxForce
    {
        public MaxForce(double force, int loadcase, int mem_no)
        {
            Force = force;
            Loadcase = loadcase;
            MemberNo = mem_no;
            NodeNo = mem_no;
        }
        public MaxForce()
        {
            Stress = 0.0;
            Force = 0.0;
            Loadcase = 0;
            MemberNo = 0;
            NodeNo = 0;
        }
        public double Force { get; set; }

        //Chiranjit [2013 07 26]
        public double Positive_Force { get; set; }
        public double Negative_Force { get; set; }


        //Store file index for Arch Bridge
        //Chiranjit [2014 01 25]
        public int File_Index { get; set; }

        //Chiranjit [2013 08 06]
        public double Stress { get; set; }

        public int Loadcase { get; set; }
        public int MemberNo { get; set; }
        public int NodeNo { get; set; }
        public override string ToString()
        {
            return Force.ToString("f3");
            //return Force.ToString("f3") + " " + Loadcase + " " + MemberNo;
            //return Force.ToString("f3");
        }

        public static MaxForce operator +(MaxForce c1, MaxForce c2)
        {
            MaxForce pfd = new MaxForce();
            pfd.Force = c1.Force + c2.Force;
            return pfd;
        }
        public static MaxForce operator -(MaxForce c1, MaxForce c2)
        {
            MaxForce pfd = new MaxForce();
            pfd.Force = c1.Force - c2.Force;
            return pfd;
        }

        public static MaxForce operator /(MaxForce c1, MaxForce c2)
        {

            MaxForce pfd = new MaxForce();
            pfd.Force = c1.Force / c2.Force;
            return pfd;
        }

        public static MaxForce operator *(MaxForce c1, MaxForce c2)
        {
            MaxForce pfd = new MaxForce();
            pfd.Force = c1.Force * c2.Force;
            return pfd;
        }
        public static implicit operator MaxForce(double x)
        {
            MaxForce pfd = new MaxForce();
            pfd.Force = x;
            return pfd;
        }
        public static implicit operator double(MaxForce x)
        {
           
            return x.Force;
        }



        public List<string> GetDetails(string caption, List<int> joints, string forceUnit)
        {


            joints.Sort();
            List<string> list = new List<string>();
            MyList mlist = new MyList(caption, ':');
            string title = "", forc_title = "";

            if (mlist.Count > 0)
            {
                if (mlist.Count == 2)
                {
                    title = mlist.StringList[0];
                    forc_title = mlist.StringList[1];
                }
            }

            list.Add("----------------------------------------------------------------------------");
            string str = "";
            string str2 = string.Format("{0} : {1} = {2:f3} {3}", title, forc_title, Force, forceUnit);

            list.Add(str2);

            //L/2 :  MAX SHEAR FORCE = 15.12  Ton
            //------------
            //Joints at L/2 : 59 60 61 62 63 
            //Member No : 176
            //Load Case No : 30
            //Node No : 62
            //list.Add(caption);

            for (int i = 0; i < str2.Length; i++)
            {
                str += "-";
            }
            list.Add(str);
            str = MyList.Get_Array_Text(joints);
            //foreach (var item in joints)
            //{
            //    str += item.ToString() + " ";
            //}
            if (joints.Count > 0)
            {
                if (NodeNo != 0)
                    list.Add("Joints at " + title + " : " + str.Trim().TrimStart().TrimEnd());
                else
                    list.Add("Members at " + title + " : " + str.Trim().TrimStart().TrimEnd());
            }
            list.Add("Member No      : " + MemberNo);
            if(NodeNo != 0)
            list.Add("Joint No       : " + NodeNo);
            if (Loadcase != 0)
                list.Add("Load Case No   : " + Loadcase);
            list.Add("----------------------------------------------------------------------------");
            //list.Add("");
            return list;
        }
    }


    //Chiranjit [2013 06 27] this class is used for Node Translation & Rotation
    public class NodeResultData
    {
        public int NodeNo { get; set; }
        public int LoadCase { get; set; }
        public double X_Translation { get; set; }
        public double Y_Translation { get; set; }
        public double Z_Translation { get; set; }
        public double X_Rotation { get; set; }
        public double Y_Rotation { get; set; }
        public double Z_Rotation { get; set; }

        public NodeResultData()
        {
            NodeNo = -1;
            LoadCase = -1;
            X_Translation = 0.0;
            Y_Translation = 0.0;
            Z_Translation = 0.0;
            X_Rotation = 0.0;
            Y_Rotation = 0.0;
            Z_Rotation = 0.0;
        }

        public double Max_Translation
        {
            get
            {
                return Math.Abs(Y_Translation) > Math.Abs(Z_Translation) ? Math.Abs(Y_Translation) : Math.Abs(Z_Translation);
 
            }
        }
        public double Max_Rotation
        {
            get
            {
                return Math.Abs(Y_Rotation) > Math.Abs(Z_Rotation) ? Math.Abs(Y_Rotation) : Math.Abs(Z_Rotation);

            }
        }

        public override string ToString()
        {
            return string.Format("{0,5} {1,5}     {2,14:E5} {3,14:E5} {4,14:E5} {5,14:E5} {6,14:E5} {7,14:E5}",
                NodeNo, LoadCase, X_Translation, Y_Translation, Z_Translation,
                X_Rotation, Y_Rotation, Z_Rotation);
        }


        public static NodeResultData Parse(string txt)
        {

            try
            {
                NodeResultData nrd = new NodeResultData();

                MyList ml = new MyList(MyList.RemoveAllSpaces(txt), ' ');
                int cnt = 0;

                nrd.NodeNo = ml.GetInt(cnt++);
                nrd.LoadCase = ml.GetInt(cnt++);
                nrd.X_Translation = ml.GetDouble(cnt++);
                nrd.Y_Translation = ml.GetDouble(cnt++);
                nrd.Z_Translation = ml.GetDouble(cnt++);
                nrd.X_Rotation = ml.GetDouble(cnt++);
                nrd.Y_Rotation = ml.GetDouble(cnt++);
                nrd.Z_Rotation = ml.GetDouble(cnt++);

                return nrd;
            }
            catch (Exception ex) { }

            return null;

        }
    }


    //Chiranjit [2013 06 27] this class is used for Node Translation & Rotation Results
    public class NodeResults : List<NodeResultData>
    {

        public NodeResults() : base()
        {

        }
        public NodeResultData Get_Max_Deflection()
        {
            NodeResultData nrd = new NodeResultData();

            foreach (var item in this)
            {
                if (item.Max_Translation > nrd.Max_Translation)
                    nrd = item;
            }
            return nrd;
        }
        public NodeResultData Get_Node_Deflection(int node_no)
        {
            NodeResultData nrd = null;

            foreach (var item in this)
            {
                if (item.NodeNo == node_no)
                {
                    if (nrd == null)
                        nrd = item;

                    if (item.Max_Translation > nrd.Max_Translation)
                        nrd = item;
                }
            }
            return nrd;
        }

        public List<int> Get_LoadCases()
        {
            List<int> list = new List<int>();
            foreach (var item in this)
            {
                if(!list.Contains(item.LoadCase))
                {
                    list.Add(item.LoadCase);
                }
            }
            return list;
        }

        public NodeResults Get_NodeResults(int loadcase)
        {
            NodeResults nr = new NodeResults();

            foreach (var item in this)
            {
                if (item.LoadCase == loadcase)
                {
                    nr.Add(item);
                }
            }
            return nr;
        }
    }

}
//Chiranjit [2013 07 14] MyList Add Get Array Sum
        