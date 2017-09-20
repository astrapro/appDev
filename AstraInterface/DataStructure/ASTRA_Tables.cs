using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace AstraInterface.DataStructure
{
    public class ASTRA_Tables
    {
        Thread thd = null;
        public ASTRA_Tables(eDesignStandard Standard)
        {
            //DesignStandard = eDesignStandard.IndianStandard;
            DesignStandard = Standard;

            //if(Standard == eDesignStandard.LRFDStandard)
            //DesignStandard =  eDesignStandard.IndianStandard;


            //IS_SteelAngles = Get_SteelAngles_IndianTable();
            //BS_SteelAngles = Get_SteelAngles_BritishTable();

            //IS_SteelBeams = Get_SteelBeams_IndianTable();
            //BS_SteelBeams = Get_SteelBeams_BritishTable();

            //IS_SteelChannels = Get_SteelChannels_IndianTable();
            //BS_SteelChannels = Get_SteelChannels_BritishTable();

            //Steel_Convert = new SteelConverter();
            //RunThread();
            thd = new Thread(new ThreadStart(RunThread));
            thd.Priority = ThreadPriority.Highest;
            thd.Start();
        }

        void RunThread()
        {
            IS_SteelAngles = Get_SteelAngles_IndianTable();
            BS_SteelAngles = Get_SteelAngles_BritishTable();
            AISC_SteelAngles = Get_SteelAngles_AmericanTable();

            IS_SteelBeams = Get_SteelBeams_IndianTable();
            BS_SteelBeams = Get_SteelBeams_BritishTable();
            AISC_SteelBeams = Get_SteelBeams_AmericanTable();

            IS_SteelChannels = Get_SteelChannels_IndianTable();
            BS_SteelChannels = Get_SteelChannels_BritishTable();
            AISC_SteelChannels = Get_SteelChannels_AmericanTable();



            Steel_Convert = new SteelConverter();

            //Chiranjit [2012 11 13]
            PipeCulvert = new PipeCulvertStrength(ASTRA_Table_Path);
        }
        public string ASTRA_Table_Path
        {
            get
            {
                string pp = "";
                if (DesignStandard == eDesignStandard.IndianStandard ||
                    DesignStandard == eDesignStandard.LRFDStandard)
                {
                    pp = Path.Combine(Application.StartupPath, "TABLES\\Indian Standard");
                }
                if (DesignStandard == eDesignStandard.BritishStandard)
                    pp = Path.Combine(Application.StartupPath, "TABLES\\British Standard");

                if (DesignStandard == eDesignStandard.LRFDStandard)
                    pp = Path.Combine(Application.StartupPath, "TABLES\\American Standard");

                return Directory.Exists(pp) ? pp : Path.GetFileName(pp) + " Folder not found in Application folder";
            }
        }
        public eDesignStandard DesignStandard { get; set; }

        public double Indian_Standard_Angles(double thickness, string size, ref double A, ref double Rxx, ref double Ryy, ref string ref_string)
        {
            thickness = Double.Parse(thickness.ToString("0.000"));

            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "ISA.txt");
            table_file = Path.Combine(table_file, "Indian_Standard_Angles.txt");

            List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, b1, a2, b2, returned_value;

            a1 = 0.0;
            b1 = 0.0;
            a2 = 0.0;
            b2 = 0.0;
            returned_value = 0.0;

            List<MyList> lst_list = new List<MyList>();

            for (int i = 0; i < lst_content.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(lst_content[i]);
                mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');

                if (double.TryParse(mList.StringList[0], out a2) && mList.Count == 9)
                {
                    find = true;
                }
                else
                {
                    find = false;
                }
                if (find)
                {
                    lst_list.Add(mList);
                }
            }

            for (int i = 0; i < lst_list.Count; i++)
            {
                kStr = lst_list[i].StringList[0];

                if (size == kStr)
                {
                    a1 = lst_list[i].GetDouble(1);
                    if (a1 == thickness)
                    {
                        A = lst_list[i].GetDouble(2);
                        Rxx = lst_list[i].GetDouble(5);
                        Ryy = lst_list[i].GetDouble(6);
                        returned_value = Ryy;
                        break;
                    }
                }

            }

            lst_list.Clear();
            lst_content.Clear();
            return returned_value;
        }
        public List<string> Get_Tables_Indian_Standard_Angles()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Indian_Standard_Angles.txt");
            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }

        public double Pre_Post_Tensioning_with_EffectiveBond(double ratio, int indx, ref string ref_string)
        {
            ratio = Double.Parse(ratio.ToString("0.000"));

            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "PreStressedBridge_Table_2.txt");
            table_file = Path.Combine(table_file, "Pre_Post_Tensioning_with_EffectiveBond.txt");


            ratio = 0.218;
            List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, b1, a2, b2, returned_value;

            a1 = 0.0;
            b1 = 0.0;
            a2 = 0.0;
            b2 = 0.0;
            returned_value = 0.0;

            List<MyList> lst_list = new List<MyList>();

            for (int i = 0; i < lst_content.Count; i++)
            {
                mList = new MyList(MyList.RemoveAllSpaces(lst_content[i].Trim()), ' ');
                find = (double.TryParse(mList.StringList[0], out a2) && mList.Count == 5);
                if (i == 0)
                {

                    ref_string = lst_content[i];
                    //ref_string = kStr;
                }

                if (find)
                {
                    //mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                    lst_list.Add(mList);
                }
            }

            for (int i = 0; i < lst_list.Count; i++)
            {
                a1 = lst_list[i].GetDouble(0);
                if (ratio < lst_list[0].GetDouble(0))
                {
                    returned_value = lst_list[0].GetDouble(indx);
                    break;
                }
                else if (ratio > (lst_list[lst_list.Count - 1].GetDouble(0)))
                {
                    returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
                    break;
                }

                if (a1 == ratio)
                {
                    returned_value = lst_list[i].GetDouble(indx);
                    break;
                }
                else if (a1 > ratio)
                {
                    a2 = a1;
                    b2 = lst_list[i].GetDouble(indx);

                    a1 = lst_list[i - 1].GetDouble(0);
                    b1 = lst_list[i - 1].GetDouble(indx);

                    returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (ratio - a1);
                    break;
                }
            }

            lst_list.Clear();
            lst_content.Clear();


            returned_value = Double.Parse(returned_value.ToString("0.000"));
            return returned_value;
        }
        public List<string> Get_Tables_Pre_Post_Tensioning_with_EffectiveBond()
        {
            List<string> list = new List<string>();


            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "PreStressedBridge_Table_2.txt");
            table_file = Path.Combine(table_file, "Pre_Post_Tensioning_with_EffectiveBond.txt");


            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }


        public double Broad_Gauge_BendingMoment(double L, ref List<string> res)
        {
            double P_bm = 0.0;


            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "PreStressedBridge_Table_2.txt");
            table_file = Path.Combine(table_file, "Broad Gauge.txt");


            bool flag = false;

            if (!File.Exists(table_file)) return 0.0;

            List<string> list = new List<string>(File.ReadAllLines(table_file));
            MyList ml = null;
            MyList ml1 = null;

            string kStr = "";

            double P1 = 0.0;
            double P2 = 0.0;
            double L1 = 0.0;
            double L2 = 0.0;

            
                double P1_BM_kN, P1_BM_Ton, P1_SF_kN, P1_SF_Ton, CDA1;
                double P2_BM_kN, P2_BM_Ton, P2_SF_kN, P2_SF_Ton, CDA2;


            for (int i = 0; i < list.Count; i++)
            {
                kStr = list[i].Trim();

                if (!flag)
                {
                    if (kStr.StartsWith("-----------"))
                    {
                        flag = true;
                        continue;
                    }
                }

                kStr = MyList.RemoveAllSpaces(kStr);

                if(flag)
                {
                    ml = new MyList(kStr, ' ');
                    if (ml.Count > 4)
                    {
                        L1 = ml.GetDouble(0);
                        if (L1 > L)
                        {
                            L1 = ml1.GetDouble(0);
                            P1 = ml1.GetDouble(2);


                             P1_BM_kN = ml1.GetDouble(1);
                             P1_BM_Ton = ml1.GetDouble(2);
                             P1_SF_kN = ml1.GetDouble(3);
                             P1_SF_Ton = ml1.GetDouble(4);
                             CDA1 = ml1.GetDouble(5);
                            
                           
                            

                             P2_BM_kN = ml.GetDouble(1);
                             P2_BM_Ton = ml.GetDouble(2);
                             P2_SF_kN = ml.GetDouble(3);
                             P2_SF_Ton = ml.GetDouble(4);
                             CDA2 = ml.GetDouble(5);
                            


                            L2 = ml.GetDouble(0);
                            P2 = ml.GetDouble(2);
                            //res.Add(string.Format(""))

                            res.Add(string.Format(""));
                            res.Add(string.Format(""));
                            res.Add(string.Format("For Bending Moment,"));
                            res.Add(string.Format(""));
                            res.Add(string.Format("L1={0:f3}           P1(BM)kN ={1:f3}        P1(BM)Ton ={2:f3}   P1(SF)kN ={3:f3}        P1(SF)Ton ={4:f3}    CDA1={5:f3}", L1, P1_BM_kN, P1_BM_Ton, P1_SF_kN, P1_SF_Ton, CDA1));
                            res.Add(string.Format("L2={0:f3}           P2(BM)kN ={1:f3}        P2(BM)Ton ={2:f3}   P2(SF)kN ={3:f3}        P2(SF)Ton ={4:f3}    CDA2={5:f3}", L2, P2_BM_kN, P2_BM_Ton, P2_SF_kN, P2_SF_Ton, CDA2));
                            res.Add(string.Format(""));
                            res.Add(string.Format("From above values we take,"));


                            res.Add(string.Format("P1 = {0:f3} Ton and P2 = {1:f3} Ton for BM, for L1 = {2:f3} m and L2={3:f3} effective spans,", P1_BM_Ton, P2_BM_Ton, L1, L2));


                            res.Add(string.Format(""));
                            res.Add(string.Format("Therefore for {0:f3} m effective span Live Load for Maximum BM ", L));




                            P_bm = P1 + (P2 - P1) * (L - L1) / (L2 - L1);

                            res.Add(string.Format(""));
                            res.Add(string.Format("P_bm = {0:f3} + ({1:f3}-{0:f3}) x ({2:f3}-{3:f3})/({4:f3}-{3:f3})", P1_BM_Ton, P2_BM_Ton, L, L1, L2));
                            res.Add(string.Format("     = {0:f3} Tons", P_bm));

                            break;
                        }
                        else if (L1 == L)
                        {

                            ml1 = ml;

                            L1 = ml1.GetDouble(0);
                            P1 = ml1.GetDouble(2);

                            P1_BM_kN = ml1.GetDouble(1);
                            P1_BM_Ton = ml1.GetDouble(2);
                            P1_SF_kN = ml1.GetDouble(3);
                            P1_SF_Ton = ml1.GetDouble(4);
                            CDA1 = ml1.GetDouble(5);


                            res.Add(string.Format("For Bending Moment,"));
                            res.Add(string.Format(""));
                            res.Add(string.Format("L={0:f3}           P(BM)kN ={1:f3}        P(BM)Ton ={2:f3}   P(SF)kN ={3:f3}        P(SF)Ton ={4:f3}    CDA={5:f3}", L1, P1_BM_kN, P1_BM_Ton, P1_SF_kN, P1_SF_Ton, CDA1));
                            res.Add(string.Format(""));
                            res.Add(string.Format("From above values we take,"));
                           
                            res.Add(string.Format("P = {0:f3} Ton for BM, for L = {1:f3} effective spans,", P1_BM_Ton, L));

                            P_bm = P1;

                            //res.Add(string.Format(" = {0:f3} Tons", P_bm));

                            return P1;
                        }

                        ml1 = ml;
                    }
                }


            }

             
            //P_bm = P1 + (P2 - P1) * (L - L1) / (L2 - L1);

            return P_bm;
        }
        public double Broad_Gauge_ShearForce(double L, ref List<string> res)
        {
            double P_sf = 0.0;


            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "PreStressedBridge_Table_2.txt");
            table_file = Path.Combine(table_file, "Broad Gauge.txt");


            bool flag = false;

            if (!File.Exists(table_file)) return 0.0;

            List<string> list = new List<string>(File.ReadAllLines(table_file));
            MyList ml = null;
            MyList ml1 = null;

            string kStr = "";

            double P1 = 0.0;
            double P2 = 0.0;
            double L1 = 0.0;
            double L2 = 0.0;


            double P1_BM_kN, P1_BM_Ton, P1_SF_kN, P1_SF_Ton, CDA1;
            double P2_BM_kN, P2_BM_Ton, P2_SF_kN, P2_SF_Ton, CDA2;


            for (int i = 0; i < list.Count; i++)
            {
                kStr = list[i].Trim();

                if (!flag)
                {
                    if (kStr.StartsWith("-----------"))
                    {
                        flag = true;
                        continue;
                    }
                }

                kStr = MyList.RemoveAllSpaces(kStr);

                if (flag)
                {
                    ml = new MyList(kStr, ' ');
                    if (ml.Count > 4)
                    {
                        L1 = ml.GetDouble(0);
                        if (L1 > L)
                        {
                            L1 = ml1.GetDouble(0);
                            P1 = ml1.GetDouble(4);


                            P1_BM_kN = ml1.GetDouble(1);
                            P1_BM_Ton = ml1.GetDouble(2);
                            P1_SF_kN = ml1.GetDouble(3);
                            P1_SF_Ton = ml1.GetDouble(4);
                            CDA1 = ml1.GetDouble(5);




                            P2_BM_kN = ml.GetDouble(1);
                            P2_BM_Ton = ml.GetDouble(2);
                            P2_SF_kN = ml.GetDouble(3);
                            P2_SF_Ton = ml.GetDouble(4);
                            CDA2 = ml.GetDouble(5);



                            L2 = ml.GetDouble(0);
                            P2 = ml.GetDouble(4);
                            //res.Add(string.Format(""))
                            //sw.WriteLine(" For Shear Force,");

                            res.Add(string.Format(""));
                            res.Add(string.Format(""));
                            res.Add(string.Format("For Shear Force,"));
                            res.Add(string.Format(""));
                            res.Add(string.Format("L1={0:f3}           P1(BM)kN ={1:f3}        P1(BM)Ton ={2:f3}   P1(SF)kN ={3:f3}        P1(SF)Ton ={4:f3}    CDA1={5:f3}", L1, P1_BM_kN, P1_BM_Ton, P1_SF_kN, P1_SF_Ton, CDA1));
                            res.Add(string.Format("L2={0:f3}           P2(BM)kN ={1:f3}        P2(BM)Ton ={2:f3}   P2(SF)kN ={3:f3}        P2(SF)Ton ={4:f3}    CDA2={5:f3}", L2, P2_BM_kN, P2_BM_Ton, P2_SF_kN, P2_SF_Ton, CDA2));
                            res.Add(string.Format(""));
                            res.Add(string.Format("From above values we take,"));


                            res.Add(string.Format("P1 = {0:f3} Ton and P2 = {1:f3} Ton for SF, for L1 = {2:f3} m and L2={3:f3} effective spans,", P1_SF_Ton, P2_SF_Ton, L1, L2));


                            res.Add(string.Format(""));
                            res.Add(string.Format("Therefore for {0:f3} m effective span Live Load for Maximum SF ", L));

                            P_sf = P1 + (P2 - P1) * (L - L1) / (L2 - L1);

                            res.Add(string.Format(""));
                            res.Add(string.Format("P_sf = {0:f3} + ({1:f3}-{0:f3}) x ({2:f3}-{3:f3})/({4:f3}-{3:f3})", P1_SF_Ton, P2_SF_Ton, L, L1, L2));
                            res.Add(string.Format("     = {0:f3} Tons", P_sf));

                            break;
                        }
                        else if (L1 == L)
                        {
                            ml1 = ml;
                            L1 = ml1.GetDouble(0);
                            P1 = ml1.GetDouble(4);

                            P1_BM_kN = ml1.GetDouble(1);
                            P1_BM_Ton = ml1.GetDouble(2);
                            P1_SF_kN = ml1.GetDouble(3);
                            P1_SF_Ton = ml1.GetDouble(4);
                            CDA1 = ml1.GetDouble(5);


                            res.Add(string.Format("For Shear Force,"));
                            res.Add(string.Format(""));
                            res.Add(string.Format("L={0:f3}           P(BM)kN ={1:f3}        P(BM)Ton ={2:f3}   P(SF)kN ={3:f3}        P(SF)Ton ={4:f3}    CDA={5:f3}", L1, P1_BM_kN, P1_BM_Ton, P1_SF_kN, P1_SF_Ton, CDA1));
                            res.Add(string.Format(""));
                            res.Add(string.Format("From above values we take,"));

                            res.Add(string.Format("P = {0:f3} Ton for SF, for L = {1:f3} effective spans,", P1_BM_Ton, L));

                            P_sf = P1;

                            //res.Add(string.Format(" = {0:f3} Tons", P_sf));

                            return P1;
                        }

                        ml1 = ml;
                    }
                }


            }



            return P_sf;
        }

        public List<string> Get_Table_Broad_Gauge()
        {
            List<string> res = new List<string>();

            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "PreStressedBridge_Table_2.txt");
            table_file = Path.Combine(table_file, "Broad Gauge.txt");


            bool flag = false;

            if (!File.Exists(table_file)) return res;

            List<string> list = new List<string>(File.ReadAllLines(table_file));
            MyList ml = null;

            string kStr = "";



            double L1, P1_BM_kN, P1_BM_Ton, P1_SF_kN, P1_SF_Ton, CDA1;


            res.Add(string.Format(""));
            res.Add(string.Format("---------------------------------------------------------------------------"));
            res.Add(string.Format("L(m)           Total load for        Total load for     Impact Factor       "));
            res.Add(string.Format("               Bending Moment        Shear Force        CDA = 0.15+8/(6+L)"));
            res.Add(string.Format("               (kN)       (Ton)      (kN)       (Ton)"));
            res.Add(string.Format("---------------------------------------------------------------------------"));



            for (int i = 0; i < list.Count; i++)
            {
                kStr = list[i].Trim();

                if (!flag)
                {
                    if (kStr.StartsWith("-----------"))
                    {
                        flag = true;
                        continue;
                    }
                }

                kStr = MyList.RemoveAllSpaces(kStr);

                if (flag)
                {
                    try
                    {
                        ml = new MyList(kStr, ' ');
                        if (ml.Count > 4)
                        {
                            L1 = ml.GetDouble(0);
                            P1_BM_kN = ml.GetDouble(1);
                            P1_BM_Ton = ml.GetDouble(2);
                            P1_SF_kN = ml.GetDouble(3);
                            P1_SF_Ton = ml.GetDouble(4);
                            CDA1 = ml.GetDouble(5);
                            res.Add(string.Format("{0,-10:f1} {1,10:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3}", L1, P1_BM_kN, P1_BM_Ton, P1_SF_kN, P1_SF_Ton, CDA1));
                        }
                    }
                    catch (Exception ex) { }
                }
            }



            return res;
        }

        public double Bursting_Tensile_Force(double ratio, ref string ref_string)
        {
            ratio = Double.Parse(ratio.ToString("0.000"));
            int indx = 1;
            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "PreStressedBridge_Table_3.txt");
            table_file = Path.Combine(table_file, "Bursting_Tensile_Force.txt");


            List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, b1, a2, b2, returned_value;

            a1 = 0.0;
            b1 = 0.0;
            a2 = 0.0;
            b2 = 0.0;
            returned_value = 0.0;

            List<MyList> lst_list = new List<MyList>();

            for (int i = 0; i < lst_content.Count; i++)
            {
                mList = new MyList(MyList.RemoveAllSpaces(lst_content[i].Trim()), ' ');
                find = (double.TryParse(mList.StringList[0], out a2) && mList.Count == 2);
                if (i == 0)
                {

                    ref_string = lst_content[i];
                    //ref_string = kStr;
                }

                if (find)
                {
                    //mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                    lst_list.Add(mList);
                }
            }

            for (int i = 0; i < lst_list.Count; i++)
            {
                a1 = lst_list[i].GetDouble(0);
                if (ratio < lst_list[0].GetDouble(0))
                {
                    returned_value = lst_list[0].GetDouble(indx);
                    break;
                }
                else if (ratio > (lst_list[lst_list.Count - 1].GetDouble(0)))
                {
                    returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
                    break;
                }

                if (a1 == ratio)
                {
                    returned_value = lst_list[i].GetDouble(indx);
                    break;
                }
                else if (a1 > ratio)
                {
                    a2 = a1;
                    b2 = lst_list[i].GetDouble(indx);

                    a1 = lst_list[i - 1].GetDouble(0);
                    b1 = lst_list[i - 1].GetDouble(indx);

                    returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (ratio - a1);
                    break;
                }
            }

            lst_list.Clear();
            lst_content.Clear();


            returned_value = Double.Parse(returned_value.ToString("0.000"));
            return returned_value;
        }
        public List<string> Get_Tables_Bursting_Tensile_Force()
        {
            List<string> list = new List<string>();


            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "PreStressedBridge_Table_2.txt");
            table_file = Path.Combine(table_file, "Bursting_Tensile_Force.txt");


            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }

        public double K_Val_Simply_Continous_Supported_Slab(double B_by_L, int indx, ref string ref_string)
        {
            B_by_L = Double.Parse(B_by_L.ToString("0.0"));

            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "RccDeckSlab_Table_1.txt");
            table_file = Path.Combine(table_file, "K_Val_Simply_Continous_Supported_Slab.txt");

            List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, b1, a2, b2, returned_value;

            a1 = 0.0;
            b1 = 0.0;
            a2 = 0.0;
            b2 = 0.0;
            returned_value = 0.0;

            List<MyList> lst_list = new List<MyList>();

            for (int i = 0; i < lst_content.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(lst_content[i]);
                kStr = kStr.Replace("<=", "");
                if (i == 0)
                {
                    ref_string = kStr;
                }

                if (kStr.StartsWith("--------------"))
                {
                    find = !find; continue;
                }
                if (find)
                {
                    mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                    lst_list.Add(mList);
                }
            }

            for (int i = 0; i < lst_list.Count; i++)
            {
                a1 = lst_list[i].GetDouble(0);
                if (B_by_L < lst_list[0].GetDouble(0))
                {
                    returned_value = lst_list[0].GetDouble(indx);
                    break;
                }
                else if (B_by_L > (lst_list[lst_list.Count - 1].GetDouble(0)))
                {
                    returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
                    break;
                }

                if (a1 == B_by_L)
                {
                    returned_value = lst_list[i].GetDouble(indx);
                    break;
                }
                else if (a1 > B_by_L)
                {
                    a2 = a1;
                    b2 = lst_list[i].GetDouble(indx);

                    a1 = lst_list[i - 1].GetDouble(0);
                    b1 = lst_list[i - 1].GetDouble(indx);

                    returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (B_by_L - a1);
                    break;
                }
            }

            lst_list.Clear();
            lst_content.Clear();


            returned_value = Double.Parse(returned_value.ToString("0.00"));
            return returned_value;
        }
        public List<string> Get_Tables_K_Val_Simply_Continous_Supported_Slab()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "K_Val_Simply_Continous_Supported_Slab.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }

        public double Constant_Gamma(double percent_val, int fs, ref string ref_string)
        {

            percent_val = Double.Parse(percent_val.ToString("0.00"));

            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "Simply_Support_Tab_2.txt");
            table_file = Path.Combine(table_file, "Constant_Gamma.txt");


            List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, b1, a2, b2, gama;

            a1 = 0.0;
            b1 = 0.0;
            a2 = 0.0;
            b2 = 0.0;
            gama = 0.0;

            int indx = -1;



            switch (fs)
            {
                case 290:
                    indx = 2;
                    break;
                case 240:
                    indx = 3;
                    break;
                case 190:
                    indx = 4;
                    break;
                case 145:
                    indx = 5;
                    break;
                case 120:
                    indx = 6;
                    break;
            }

            List<MyList> lst_list = new List<MyList>();

            for (int i = 0; i < lst_content.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(lst_content[i]);
                kStr = kStr.Replace("<=", "");
                if (i == 0)
                {
                    ref_string = kStr;
                }
                if (kStr.StartsWith("--------------"))
                {
                    find = !find; continue;
                }
                if (find)
                {
                    mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                    lst_list.Add(mList);
                }
            }

            for (int i = 0; i < lst_list.Count; i++)
            {
                a1 = lst_list[i].GetDouble(1);

                if (a1 == percent_val)
                {
                    gama = lst_list[i].GetDouble(1);
                    break;

                }
                else if (a1 > percent_val)
                {
                    a2 = a1;
                    b2 = lst_list[i].GetDouble(indx);

                    a1 = lst_list[i - 1].GetDouble(1);
                    b1 = lst_list[i - 1].GetDouble(indx);

                    gama = b1 + ((b2 - b1) / (a2 - a1)) * (percent_val - a1);
                    break;
                }
            }

            lst_list.Clear();
            lst_content.Clear();


            gama = Double.Parse(gama.ToString("0.000"));
            return gama;
        }
        public List<string> Get_Tables_Constant_Gamma()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Constant_Gamma.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }

        public double EUDL_CDA(double span_length, int indx, ref string ref_string)
        {
            span_length = Double.Parse(span_length.ToString("0.000"));

            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_1.txt");
            table_file = Path.Combine(table_file, "EUDL_CDA.txt");

            List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, b1, a2, b2, returned_value;

            a1 = 0.0;
            b1 = 0.0;
            a2 = 0.0;
            b2 = 0.0;
            returned_value = 0.0;

            List<MyList> lst_list = new List<MyList>();

            for (int i = 0; i < lst_content.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(lst_content[i]);
                mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                if (i == 0)
                {
                    ref_string = kStr;
                }
                if (double.TryParse(mList.StringList[0], out a2) && mList.Count == 6)
                {
                    find = true;
                }
                else
                {
                    find = false;
                }
                if (find)
                {
                    lst_list.Add(mList);
                }
            }

            for (int i = 0; i < lst_list.Count; i++)
            {
                a1 = lst_list[i].GetDouble(0);

                if (a1 == span_length)
                {
                    returned_value = lst_list[i].GetDouble(indx);
                    break;

                }
                else if (a1 > span_length)
                {
                    a2 = a1;
                    b2 = lst_list[i].GetDouble(indx);

                    a1 = lst_list[i - 1].GetDouble(0);
                    b1 = lst_list[i - 1].GetDouble(indx);

                    returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (span_length - a1);
                    break;
                }
            }

            lst_list.Clear();
            lst_content.Clear();


            returned_value = Double.Parse(returned_value.ToString("0.000"));
            return returned_value;
        }
        public List<string> Get_Tables_EUDL_CDA()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "EUDL_CDA.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }

        public double Allowable_Working_Stress_Critical(double Cs_value, int indx, ref string ref_string)
        {
            Cs_value = Double.Parse(Cs_value.ToString("0.000"));

            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_2.txt");
            table_file = Path.Combine(table_file, "Allowable_Working_Stress_Critical.txt");

            List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, b1, a2, b2, returned_value;

            a1 = 0.0;
            b1 = 0.0;
            a2 = 0.0;
            b2 = 0.0;
            returned_value = 0.0;

            List<MyList> lst_list = new List<MyList>();

            for (int i = 0; i < lst_content.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(lst_content[i]);
                kStr = kStr.Replace("<=", "");
                if (kStr.StartsWith("--------------"))
                {
                    find = !find; continue;
                }
                if (find)
                {
                    mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                    lst_list.Add(mList);
                }
            }

            for (int i = 0; i < lst_list.Count; i++)
            {
                a1 = lst_list[i].GetDouble(0);
                if (Cs_value < lst_list[0].GetDouble(0))
                {
                    returned_value = lst_list[0].GetDouble(indx);
                    break;
                }
                else if (Cs_value > (lst_list[lst_list.Count - 1].GetDouble(0)))
                {
                    returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
                    break;
                }

                if (a1 == Cs_value)
                {
                    returned_value = lst_list[i].GetDouble(indx);
                    break;
                }
                else if (a1 > Cs_value)
                {
                    a2 = a1;
                    b2 = lst_list[i].GetDouble(indx);

                    a1 = lst_list[i - 1].GetDouble(0);
                    b1 = lst_list[i - 1].GetDouble(indx);

                    returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (Cs_value - a1);
                    break;
                }
            }

            lst_list.Clear();
            lst_content.Clear();


            returned_value = Double.Parse(returned_value.ToString("0.000"));
            return returned_value;
        }
        public List<string> Get_Tables_Allowable_Working_Stress_Critical()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Allowable_Working_Stress_Critical.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }

        public double Allowable_Average_Shear_Stress(double d_by_t, double d_point, ref string ref_string)
        {
            int indx = -1;

            if (d_point >= 0.4 && d_point < 0.6)
                indx = 1;
            else if (d_point >= 0.6 && d_point < 0.8)
                indx = 2;
            else if (d_point >= 0.8 && d_point < 1.0)
                indx = 3;
            else if (d_point >= 1.0 && d_point < 1.2)
                indx = 4;
            else if (d_point >= 1.2 && d_point < 1.4)
                indx = 5;
            else if (d_point >= 1.4 && d_point < 1.5)
                indx = 6;
            else if (d_point >= 1.5)
                indx = 7;


            d_by_t = Double.Parse(d_by_t.ToString("0.0"));

            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_3.txt");
            table_file = Path.Combine(table_file, "Allowable_Average_Shear_Stress.txt");

            List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, returned_value;
            //double  b1, a2, b2, returned_value;

            //a1 = 0.0;
            //b1 = 0.0;
            //a2 = 0.0;
            //b2 = 0.0;
            returned_value = 0.0;

            List<MyList> lst_list = new List<MyList>();

            for (int i = 0; i < lst_content.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(lst_content[i]);
                kStr = kStr.Replace("<=", "");
                if (i == 0)
                {
                    ref_string = kStr;
                }

                if (kStr.StartsWith("--------------"))
                {
                    find = !find; continue;
                }
                if (find)
                {
                    mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                    lst_list.Add(mList);
                }
            }

            for (int i = 0; i < lst_list.Count; i++)
            {
                a1 = lst_list[i].GetDouble(0);

                if (d_by_t < a1)
                {
                    returned_value = lst_list[i].GetDouble(indx);
                    break;
                }
            }

            lst_list.Clear();
            lst_content.Clear();


            returned_value = Double.Parse(returned_value.ToString("0.000"));
            return returned_value;
        }
        public List<string> Get_Tables_Allowable_Average_Shear_Stress()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Allowable_Average_Shear_Stress.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }

        public double Allowable_Working_Stress_Cross_Section(double lamda, int indx, ref string ref_string)
        {
            lamda = Double.Parse(lamda.ToString("0.000"));

            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_4.txt");
            table_file = Path.Combine(table_file, "Allowable_Working_Stress_Cross_Section.txt");

            List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, b1, a2, b2, returned_value;

            a1 = 0.0;
            b1 = 0.0;
            a2 = 0.0;
            b2 = 0.0;
            returned_value = 0.0;

            List<MyList> lst_list = new List<MyList>();

            for (int i = 0; i < lst_content.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(lst_content[i]);
                kStr = kStr.Replace("<=", "");
                if (i == 0)
                {
                    ref_string = kStr;
                }

                if (kStr.StartsWith("--------------"))
                {
                    find = !find; continue;
                }
                if (find)
                {
                    mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                    lst_list.Add(mList);
                }
            }

            for (int i = 0; i < lst_list.Count; i++)
            {
                a1 = lst_list[i].GetDouble(0);
                if (lamda < lst_list[0].GetDouble(0))
                {
                    returned_value = lst_list[0].GetDouble(indx);
                    break;
                }
                else if (lamda > (lst_list[lst_list.Count - 1].GetDouble(0)))
                {
                    returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
                    break;
                }

                if (a1 == lamda)
                {
                    returned_value = lst_list[i].GetDouble(indx);
                    break;
                }
                else if (a1 > lamda)
                {
                    a2 = a1;
                    b2 = lst_list[i].GetDouble(indx);

                    a1 = lst_list[i - 1].GetDouble(0);
                    b1 = lst_list[i - 1].GetDouble(indx);

                    returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (lamda - a1);
                    break;
                }
            }

            lst_list.Clear();
            lst_content.Clear();


            returned_value = Double.Parse(returned_value.ToString("0.000"));
            return returned_value;
        }
        public double Allowable_Working_Stress_Cross_Section(double lamda, double fy, ref string ref_string)
        {
            //return iApp.Tables.Allowable_Working_Stress_Cross_Section(lamda, fy);

            lamda = Double.Parse(lamda.ToString("0.000"));

            int indx = 5;

            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Allowable_Working_Stress_Cross_Section.txt");

            List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, b1, a2, b2, returned_value;

            a1 = 0.0;
            b1 = 0.0;
            a2 = 0.0;
            b2 = 0.0;
            returned_value = 0.0;

            List<MyList> lst_list = new List<MyList>();

            for (int i = 0; i < lst_content.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(lst_content[i]);
                kStr = kStr.Replace("<=", "");
                kStr = kStr.Replace("Fy = ", "");
                //if (kStr.StartsWith("--------------"))
                //{
                //    find = !find; continue;
                //}
                //if (find)
                //{
                if (i == 0)
                {
                    ref_string = kStr;
                }
                mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                try
                {
                    double d = mList.GetDouble(0);
                    lst_list.Add(mList);

                }
                catch (Exception ex) { }
                //}
            }
            indx = lst_list[0].StringList.IndexOf(fy.ToString());
            a1 = 0.0;
            a2 = 0.0;
            b1 = 0.0;
            b2 = 0.0;
            int _col_index = -1;
            for (int i = 1; i < lst_list[0].Count; i++)
            {
                if (fy >= lst_list[0].GetDouble(i - 1) && fy <= lst_list[0].GetDouble(i))
                {
                    a1 = lst_list[0].GetDouble(i - 1);
                    a2 = lst_list[0].GetDouble(i);
                    _col_index = i + 1;
                    break;
                }
            }
            double v1, v2, v3, v4, val1, val2, val3;
            v1 = 0.0; v2 = 0.0; v3 = 0.0; v4 = 0.0;
            for (int i = 2; i < lst_list.Count; i++)
            {
                b1 = lst_list[i - 1].GetDouble(0);
                b2 = lst_list[i].GetDouble(0);

                if (lamda >= b1 && lamda <= b2)
                {
                    v1 = lst_list[i - 1].GetDouble(_col_index - 1);
                    v2 = lst_list[i - 1].GetDouble(_col_index);

                    v3 = lst_list[i].GetDouble(_col_index - 1);
                    v4 = lst_list[i].GetDouble(_col_index);
                    break;
                }
            }
            if (v1 == 0.0 && v2 == 0.0 && v3 == 0.0 && v4 == 0.0)
            {
                v1 = lst_list[lst_list.Count - 1].GetDouble(_col_index - 1);
                v2 = lst_list[lst_list.Count - 1].GetDouble(_col_index);

                v3 = lst_list[lst_list.Count - 1].GetDouble(_col_index - 1);
                v4 = lst_list[lst_list.Count - 1].GetDouble(_col_index);
            }
            //a1 = 0.0; a2 = 0.0; b1 = 0.0; 
            //b2 = 0.0; v1 = 0.0; 
            //v2 = 0.0; v3 = 0.0; v4 = 0.0; 
            val1 = 0.0; val2 = 0.0; val3 = 0.0;


            val1 = v1 + ((v2 - v1) / (a2 - a1)) * (fy - a1);
            val2 = v3 + ((v4 - v3) / (a2 - a1)) * (fy - a1);

            if (v1 == v3) val1 = v1;
            if (v2 == v4) val2 = v2;

            returned_value = val1 + ((val2 - val1) / (b2 - b1)) * (lamda - b1);



            returned_value = Double.Parse(returned_value.ToString("0.000"));
            return returned_value;



        }
        public List<string> Get_Tables_Allowable_Working_Stress_Cross_Section()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Allowable_Working_Stress_Cross_Section.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }

        public double Embankment_Loading(double pipe_size, double height, ref double outer_dia, ref string ref_string)
        {
            string file_name = ASTRA_Table_Path;
            //file_name = Path.Combine(file_name, "PIPE_CULVERT_TABLE_5_3.txt");
            file_name = Path.Combine(file_name, "Embankment_Loading.txt");
            List<string> lst_cont = new List<string>(File.ReadAllLines(file_name));
            List<MyList> lst_MyList = new List<MyList>();

            MyList mList = null;
            string kStr = "";

            double earth_fill_load = 0;

            for (int i = 0; i < lst_cont.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(lst_cont[i].Trim().TrimEnd().TrimStart());
                mList = new MyList(kStr, ' ');
                if (i == 0)
                {
                    if (!kStr.Contains("--------------"))
                        ref_string = kStr;
                }
                try
                {
                    if (mList.Count == 12 && (double.TryParse(mList.StringList[0], out earth_fill_load)))
                        lst_MyList.Add(mList);
                }
                catch
                {
                }
            }
            #region FOR LOOP
            for (int i = 0; i < lst_MyList.Count; i++)
            {
                if (i == 0)
                {
                    if (pipe_size == lst_MyList[i].GetDouble(0))
                    {
                        earth_fill_load = lst_MyList[i].GetDouble(2);
                        outer_dia = lst_MyList[i].GetDouble(1); break;
                    }
                    else if (pipe_size < lst_MyList[i].GetDouble(0))
                    {
                        earth_fill_load = lst_MyList[i].GetDouble(2);
                        outer_dia = pipe_size + pipe_size * 0.20; break;
                        //outer_dia = lst_MyList[i].GetDouble(1); break;
                    }
                }
                else if (i == lst_MyList.Count - 1)
                {
                    if (pipe_size > lst_MyList[i].GetDouble(0))
                    {
                        earth_fill_load = lst_MyList[i].GetDouble(2);
                        outer_dia = pipe_size + pipe_size * 0.20; break;
                    }
                }


                if (i > 1)
                {
                    if (pipe_size >= lst_MyList[i-1].GetDouble(0) &&
                        pipe_size <= lst_MyList[i].GetDouble(0))
                    {

                        if (height <= 1)
                        {
                            earth_fill_load = lst_MyList[i].GetDouble(2);
                            outer_dia = lst_MyList[i].GetDouble(1); break;
                        }
                        else if (height <= 2)
                        {
                            earth_fill_load = lst_MyList[i].GetDouble(3);
                            outer_dia = lst_MyList[i].GetDouble(1); break;
                        }
                        else if (height <= 3)
                        {
                            earth_fill_load = lst_MyList[i].GetDouble(4);
                            outer_dia = lst_MyList[i].GetDouble(1); break;
                        }
                        else if (height <= 4)
                        {
                            earth_fill_load = lst_MyList[i].GetDouble(5);
                            outer_dia = lst_MyList[i].GetDouble(1); break;
                        }
                        else if (height <= 5)
                        {
                            earth_fill_load = lst_MyList[i].GetDouble(6);
                            outer_dia = lst_MyList[i].GetDouble(1); break;
                        }
                        else if (height <= 6)
                        {
                            earth_fill_load = lst_MyList[i].GetDouble(7);
                            outer_dia = lst_MyList[i].GetDouble(1); break;
                        }
                        else if (height <= 7)
                        {
                            earth_fill_load = lst_MyList[i].GetDouble(8);
                            outer_dia = lst_MyList[i].GetDouble(1); break;
                        }
                        else if (height <= 8)
                        {
                            earth_fill_load = lst_MyList[i].GetDouble(9);
                            outer_dia = lst_MyList[i].GetDouble(1); break;
                        }
                        else if (height <= 9)
                        {
                            earth_fill_load = lst_MyList[i].GetDouble(10);
                            outer_dia = lst_MyList[i].GetDouble(1); break;
                        }
                        else if (height <= 10)
                        {
                            outer_dia = lst_MyList[i].GetDouble(1);
                            earth_fill_load = lst_MyList[i].GetDouble(11); break;
                        }

                    }
                }
            }
            #endregion


            #region Chiranjit [2012 11 13] Comment
            //for (int i = 0; i < lst_MyList.Count; i++)
            //{
            //    if (pipe_size == lst_MyList[i].GetDouble(0))
            //    {
            //        if (height <= 1)
            //        {
            //            earth_fill_load = lst_MyList[i].GetDouble(2);
            //            outer_dia = lst_MyList[i].GetDouble(1); break;
            //        }
            //        else if (height <= 2)
            //        {
            //            earth_fill_load = lst_MyList[i].GetDouble(3);
            //            outer_dia = lst_MyList[i].GetDouble(1); break;
            //        }
            //        else if (height <= 3)
            //        {
            //            earth_fill_load = lst_MyList[i].GetDouble(4);
            //            outer_dia = lst_MyList[i].GetDouble(1); break;
            //        }
            //        else if (height <= 4)
            //        {
            //            earth_fill_load = lst_MyList[i].GetDouble(5);
            //            outer_dia = lst_MyList[i].GetDouble(1); break;
            //        }
            //        else if (height <= 5)
            //        {
            //            earth_fill_load = lst_MyList[i].GetDouble(6);
            //            outer_dia = lst_MyList[i].GetDouble(1); break;
            //        }
            //        else if (height <= 6)
            //        {
            //            earth_fill_load = lst_MyList[i].GetDouble(7);
            //            outer_dia = lst_MyList[i].GetDouble(1); break;
            //        }
            //        else if (height <= 7)
            //        {
            //            earth_fill_load = lst_MyList[i].GetDouble(8);
            //            outer_dia = lst_MyList[i].GetDouble(1); break;
            //        }
            //        else if (height <= 8)
            //        {
            //            earth_fill_load = lst_MyList[i].GetDouble(9);
            //            outer_dia = lst_MyList[i].GetDouble(1); break;
            //        }
            //        else if (height <= 9)
            //        {
            //            earth_fill_load = lst_MyList[i].GetDouble(10);
            //            outer_dia = lst_MyList[i].GetDouble(1); break;
            //        }
            //        else if (height <= 10)
            //        {
            //            outer_dia = lst_MyList[i].GetDouble(1);
            //            earth_fill_load = lst_MyList[i].GetDouble(11); break;
            //        }

            //    }
            //}
            #endregion

            lst_cont.Clear();
            lst_MyList.Clear();

            return earth_fill_load;
        }
        public List<string> Get_Tables_Embankment_Loading()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Embankment_Loading.txt");
            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }

        public double Influence_Coefficient_Highway(double pipe_size, double height, ref double outer_dia, ref string ref_string)
        {
            string file_name = ASTRA_Table_Path;
            //file_name = Path.Combine(file_name, "PIPE_CULVERT_TABLE_5_4.txt");
            file_name = Path.Combine(file_name, "Influence_Coefficient_Highway.txt");
            List<string> lst_cont = new List<string>(File.ReadAllLines(file_name));
            List<MyList> lst_MyList = new List<MyList>();

            MyList mList = null;
            string kStr = "";

            double Cs = 0;

            for (int i = 0; i < lst_cont.Count; i++)
            {


                kStr = MyList.RemoveAllSpaces(lst_cont[i].Trim().TrimEnd().TrimStart());
                mList = new MyList(kStr, ' ');
                if (i == 0)
                {
                    ref_string = kStr;
                }
                try
                {
                    if (mList.Count == 12 && (double.TryParse(mList.StringList[0], out Cs)))
                    {
                        Cs = mList.GetDouble(6);
                        lst_MyList.Add(mList);
                    }
                }
                catch
                {
                }
            }
            #region FOR LOOP
            for (int i = 0; i < lst_MyList.Count; i++)
            {
                if (pipe_size == lst_MyList[i].GetDouble(0))
                {
                    if (height <= 0.1)
                    {
                        Cs = lst_MyList[i].GetDouble(2);
                        outer_dia = lst_MyList[i].GetDouble(1); break;
                    }
                    else if (height <= 0.2)
                    {
                        Cs = lst_MyList[i].GetDouble(3);
                        outer_dia = lst_MyList[i].GetDouble(1); break;
                    }
                    else if (height <= 0.3)
                    {
                        Cs = lst_MyList[i].GetDouble(4);
                        outer_dia = lst_MyList[i].GetDouble(1); break;
                    }
                    else if (height <= 0.4)
                    {
                        Cs = lst_MyList[i].GetDouble(5);
                        outer_dia = lst_MyList[i].GetDouble(1); break;
                    }
                    else if (height <= 0.6)
                    {
                        Cs = lst_MyList[i].GetDouble(6);
                        outer_dia = lst_MyList[i].GetDouble(1); break;
                    }
                    else if (height <= 0.8)
                    {
                        Cs = lst_MyList[i].GetDouble(7);
                        outer_dia = lst_MyList[i].GetDouble(1); break;
                    }
                    else if (height <= 1.0)
                    {
                        Cs = lst_MyList[i].GetDouble(8);
                        outer_dia = lst_MyList[i].GetDouble(1); break;
                    }
                    else if (height <= 2.0)
                    {
                        Cs = lst_MyList[i].GetDouble(9);
                        outer_dia = lst_MyList[i].GetDouble(1); break;
                    }
                    else if (height <= 3.0)
                    {
                        Cs = lst_MyList[i].GetDouble(10);
                        outer_dia = lst_MyList[i].GetDouble(1); break;
                    }
                    else if (height <= 4.0)
                    {
                        outer_dia = lst_MyList[i].GetDouble(1);
                        Cs = lst_MyList[i].GetDouble(11); break;
                    }

                }
            }
            #endregion
            lst_cont.Clear();
            lst_MyList.Clear();
            return Cs;
        }
        public List<string> Get_Tables_Influence_Coefficient_Highway()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Influence_Coefficient_Highway.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }

        public double Influence_Coefficient_Railway(double height, ref string ref_string)
        {
            string file_name = ASTRA_Table_Path;
            //file_name = Path.Combine(file_name, "PIPE_CULVERT_TABLE_5_5.txt");
            file_name = Path.Combine(file_name, "Influence_Coefficient_Railway.txt");
            List<string> lst_cont = new List<string>(File.ReadAllLines(file_name));
            List<MyList> lst_MyList = new List<MyList>();

            MyList mList = null;
            string kStr = "";

            double Cs = 0;
            #region FOR LOOP
            for (int i = 0; i < lst_cont.Count; i++)
            {



                kStr = lst_cont[i].Trim().TrimEnd().TrimStart();
                mList = new MyList(kStr, ' ');
                if (i == 0)
                {
                    //ref_string = lst_content[i];
                    ref_string = kStr;
                }
                try
                {
                    if (mList.Count == 2)
                    {
                        Cs = mList.GetDouble(1);
                        lst_MyList.Add(mList);
                    }
                }
                catch
                {
                }
            }
            for (int i = 0; i < lst_MyList.Count; i++)
            {

                if (height <= lst_MyList[i].GetDouble(0))
                {
                    Cs = lst_MyList[i].GetDouble(1);
                    break;
                }
            }
            #endregion

            return Cs;
        }
        public List<string> Get_Tables_Influence_Coefficient_Railway()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Influence_Coefficient_Railway.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }

        public double Permissible_Shear_Stress(double percent, CONCRETE_GRADE con_grade, ref string ref_string)
        {
            int indx = -1;
            percent = Double.Parse(percent.ToString("0.00"));
            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "Box_Culvert_Table_1.txt");
            table_file = Path.Combine(table_file, "Permissible_Shear_Stress.txt");
            //Permissible_Shear_Stress

            List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, b1, a2, b2, returned_value;

            a1 = 0.0;
            b1 = 0.0;
            a2 = 0.0;
            b2 = 0.0;
            returned_value = 0.0;

            List<MyList> lst_list = new List<MyList>();


            #region Swith Case
            switch (con_grade)
            {
                case CONCRETE_GRADE.M15:
                    indx = 1;
                    break;
                case CONCRETE_GRADE.M20:
                    indx = 2;
                    break;
                case CONCRETE_GRADE.M25:
                    indx = 3;
                    break;

                case CONCRETE_GRADE.M30:
                    indx = 4;
                    break;

                case CONCRETE_GRADE.M35:
                    indx = 5;
                    break;

                case CONCRETE_GRADE.M40:
                    indx = 6;
                    break;
                default :
                    indx = 6; con_grade = CONCRETE_GRADE.M40;
                    break;
            }
            #endregion


            for (int i = 0; i < lst_content.Count; i++)
            {
                if (i == 0)
                {
                    ref_string = lst_content[0];
                    find = false;
                }
                kStr = MyList.RemoveAllSpaces(lst_content[i]);
                //find = ((double.TryParse(mList.StringList[0], out a2)) && (mList.Count == 7));
                kStr = kStr.ToUpper().Replace("AND ABOVE", "").Trim().TrimEnd().TrimStart();
                kStr = kStr.Replace("<=", "");
                mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                if (kStr.ToLower().Contains("m"))
                {
                    kStr = MyList.RemoveAllSpaces(lst_content[i]).ToUpper();

                    kStr = kStr.Substring(kStr.IndexOf('M'), (kStr.Length - kStr.IndexOf('M')));


                    MyList ml = new MyList(kStr, 'M');
                    indx = ml.StringList.IndexOf(((int)con_grade).ToString());

                    if (DesignStandard == eDesignStandard.BritishStandard)
                    {
                        if (con_grade == CONCRETE_GRADE.M15 || con_grade == CONCRETE_GRADE.M20)
                            indx = 1;
                        else if (con_grade == CONCRETE_GRADE.M25)
                            indx = 2;
                        else if (con_grade == CONCRETE_GRADE.M30)
                            indx = 3;
                        else if (con_grade == CONCRETE_GRADE.M35 || con_grade == CONCRETE_GRADE.M40)
                            indx = 4;
                        else
                            indx = 4;
                    }

                    if (indx != -1)
                    {
                        find = true; continue;
                    }
                }
                if (find)
                {
                    try
                    {
                        if (mList.GetDouble(0) != 0.0000001111111)
                        {
                            lst_list.Add(mList);
                        }
                    }
                    catch (Exception ex) { }
                }
            }

            for (int i = 0; i < lst_list.Count; i++)
            {
                a1 = lst_list[i].GetDouble(0);
                if (percent < lst_list[0].GetDouble(0))
                {
                    returned_value = lst_list[0].GetDouble(indx);
                    break;
                }
                else if (percent > (lst_list[lst_list.Count - 1].GetDouble(0)))
                {
                    returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
                    break;
                }

                if (a1 == percent)
                {
                    returned_value = lst_list[i].GetDouble(indx);
                    break;
                }
                else if (a1 > percent)
                {
                    a2 = a1;
                    b2 = lst_list[i].GetDouble(indx);

                    a1 = lst_list[i - 1].GetDouble(0);
                    b1 = lst_list[i - 1].GetDouble(indx);

                    returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (percent - a1);
                    break;
                }
            }

            lst_list.Clear();
            lst_content.Clear();


            returned_value = Double.Parse(returned_value.ToString("0.000"));
            return returned_value;
        }
        public List<string> Get_Tables_Permissible_Shear_Stress()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Permissible_Shear_Stress.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }




        #region Chiranjit [2014 02 02] Permissible Stress Axial Compression

        public double Permissible_Stress_Axial_Compression(double fy, double lamda, ref string ref_string)
        {
            int indx = -1;
            //percent = Double.Parse(percent.ToString("0.00"));
            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "Box_Culvert_Table_1.txt");
            table_file = Path.Combine(table_file, "Permissible_Stress_Axial_Compression.txt");
            //Permissible_Shear_Stress

            List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, b1, a2, b2, returned_value;

            a1 = 0.0;
            b1 = 0.0;
            a2 = 0.0;
            b2 = 0.0;
            returned_value = 0.0;

            List<MyList> lst_list = new List<MyList>();


            #region Swith Case
           
            #endregion


            for (int i = 0; i < lst_content.Count; i++)
            {
                if (i == 0)
                {
                    ref_string = lst_content[0];
                    find = false;
                }
                kStr = MyList.RemoveAllSpaces(lst_content[i]);

                if (kStr == "") continue;

                kStr = kStr.ToUpper().Trim().TrimEnd().TrimStart();
                kStr = kStr.Replace("->", "");
                mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                if (kStr.ToLower().StartsWith("fy"))
                {
                    kStr = MyList.RemoveAllSpaces(lst_content[i]).ToUpper();

                    //kStr = kStr.Substring(kStr.IndexOf('M'), (kStr.Length - kStr.IndexOf('M')));
                    //fy = 350;
                    MyList ml = new MyList(kStr, ' ');
                    indx = ml.StringList.IndexOf(((int)fy).ToString());
                    if (indx != -1)
                    {
                        find = true; continue;
                    }
                    else
                    {
                        for(int c = 1; c < mList.Count; c++)
                        {
                            a1 = mList.GetDouble(c);
                            if (fy < a1)
                            {
                                find = true;
                                indx = c; break;
                            }
                        }
                    }
                }
                if (find)
                {
                    try
                    {
                        if (mList.GetDouble(0) != 0.0000001111111)
                        {
                            lst_list.Add(mList);
                        }
                    }
                    catch (Exception ex) { }
                }
            }
            //lamda = 32.67;
            for (int i = 0; i < lst_list.Count; i++)
            {
                a1 = lst_list[i].GetDouble(0);
                if (lamda < lst_list[0].GetDouble(0))
                {
                    returned_value = lst_list[0].GetDouble(indx);
                    break;
                }
                else if (lamda > (lst_list[lst_list.Count - 1].GetDouble(0)))
                {
                    returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
                    break;
                }

                if (a1 == lamda)
                {
                    returned_value = lst_list[i].GetDouble(indx);
                    break;
                }
                else if (a1 > lamda)
                {
                    a2 = a1;
                    b2 = lst_list[i].GetDouble(indx);

                    a1 = lst_list[i - 1].GetDouble(0);
                    b1 = lst_list[i - 1].GetDouble(indx);

                    returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (lamda - a1);
                    break;
                }
            }

            lst_list.Clear();
            lst_content.Clear();


            returned_value = Double.Parse(returned_value.ToString("0.000"));
            return returned_value;
        }
        public List<string> Get_Permissible_Stress_Axial_Compression()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Permissible_Stress_Axial_Compression.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }


        public double Permissible_Bending_Compressive_Stresses(double D_by_t, double lamda, ref string ref_string)
        {
            int indx = -1;
            //percent = Double.Parse(percent.ToString("0.00"));
            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "Box_Culvert_Table_1.txt");
            table_file = Path.Combine(table_file, "Permissible_Bending_Compressive_Stresses.txt");
            //Permissible_Shear_Stress

            List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, b1, a2, b2, returned_value;

            a1 = 0.0;
            b1 = 0.0;
            a2 = 0.0;
            b2 = 0.0;
            returned_value = 0.0;

            List<MyList> lst_list = new List<MyList>();


            #region Swith Case

            #endregion


            for (int i = 0; i < lst_content.Count; i++)
            {
                if (i == 0)
                {
                    ref_string = lst_content[0];
                    find = false;
                }
                kStr = MyList.RemoveAllSpaces(lst_content[i]);

                if (kStr == "") continue;

                kStr = kStr.ToUpper().Trim().TrimEnd().TrimStart();
                kStr = kStr.Replace("->", "");
                mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                if (kStr.ToLower().StartsWith("d/t"))
                {
                    kStr = MyList.RemoveAllSpaces(lst_content[i]).ToUpper();

                    //kStr = kStr.Substring(kStr.IndexOf('M'), (kStr.Length - kStr.IndexOf('M')));
                    //fy = 350;
                    MyList ml = new MyList(kStr, ' ');
                    indx = ml.StringList.IndexOf(((int)D_by_t).ToString());
                    if (indx != -1)
                    {
                        find = true; continue;
                    }
                    else
                    {
                        for (int c = 1; c < mList.Count; c++)
                        {
                            a1 = mList.GetDouble(c);
                            if (D_by_t < a1)
                            {
                                find = true;
                                indx = c; break;
                            }
                        }
                    }
                }
                if (find)
                {
                    try
                    {
                        if (mList.GetDouble(0) != 0.0000001111111)
                        {
                            lst_list.Add(mList);
                        }
                    }
                    catch (Exception ex) { }
                }
            }
            //lamda = 32.67;
            for (int i = 0; i < lst_list.Count; i++)
            {
                a1 = lst_list[i].GetDouble(0);
                if (lamda < lst_list[0].GetDouble(0))
                {
                    returned_value = lst_list[0].GetDouble(indx);
                    break;
                }
                else if (lamda > (lst_list[lst_list.Count - 1].GetDouble(0)))
                {
                    returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
                    break;
                }

                if (a1 == lamda)
                {
                    returned_value = lst_list[i].GetDouble(indx);
                    break;
                }
                else if (a1 > lamda)
                {
                    a2 = a1;
                    b2 = lst_list[i].GetDouble(indx);

                    a1 = lst_list[i - 1].GetDouble(0);
                    b1 = lst_list[i - 1].GetDouble(indx);

                    returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (lamda - a1);
                    break;
                }
            }

            lst_list.Clear();
            lst_content.Clear();


            returned_value = Double.Parse(returned_value.ToString("0.000"));
            return returned_value;
        }
        public List<string> Get_Permissible_Bending_Compressive_Stresses()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Permissible_Bending_Compressive_Stresses.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }

        #endregion Chiranjit [2014 02 02]

        //Chiranjit [2012 11 13]
        public PipeCulvertStrength PipeCulvert { get; set; }

        public TableRolledSteelBeams IS_SteelBeams { get; set; }
        public TableRolledSteelBeams BS_SteelBeams { get; set; }

        public TableRolledSteelBeams AISC_SteelBeams { get; set; } //Americal Sections

        public TableRolledSteelChannels IS_SteelChannels { get; set; }
        public TableRolledSteelChannels BS_SteelChannels { get; set; }
        public TableRolledSteelChannels AISC_SteelChannels { get; set; } //Americal Sections

        public TableRolledSteelAngles IS_SteelAngles { get; set; }
        public TableRolledSteelAngles BS_SteelAngles { get; set; }
        public TableRolledSteelAngles AISC_SteelAngles { get; set; } //Americal Sections


        public TableRolledSteelBeams Get_SteelBeams()
        {
            if (DesignStandard == eDesignStandard.BritishStandard)
                return new TableRolledSteelBeams(Path.Combine(Application.StartupPath, "TABLES\\British Standard"), DesignStandard);
            if (DesignStandard == eDesignStandard.IndianStandard)
                return new TableRolledSteelBeams(Path.Combine(Application.StartupPath, "TABLES\\Indian Standard"), DesignStandard);
            if (DesignStandard == eDesignStandard.LRFDStandard)
                return new TableRolledSteelBeams(Path.Combine(Application.StartupPath, "TABLES\\American Standard"), DesignStandard);

            return new TableRolledSteelBeams();
        }
        public TableRolledSteelBeams Get_SteelBeams_IndianTable()
        {
            return new TableRolledSteelBeams(Path.Combine(Application.StartupPath, "TABLES\\Indian Standard"), eDesignStandard.IndianStandard);
        }
        public TableRolledSteelBeams Get_SteelBeams_BritishTable()
        {
            return new TableRolledSteelBeams(Path.Combine(Application.StartupPath, "TABLES\\British Standard"), eDesignStandard.BritishStandard);
        }
        public TableRolledSteelBeams Get_SteelBeams_AmericanTable()
        {
            return new TableRolledSteelBeams(Path.Combine(Application.StartupPath, "TABLES\\American Standard"), eDesignStandard.LRFDStandard);
        }
        public RolledSteelBeamsRow Get_BeamData_FromTable(string section_name, string section_code)
        {
            RolledSteelBeamsRow row = new RolledSteelBeamsRow();
            if (section_name.StartsWith("UK"))
            {
                row = BS_SteelBeams.GetDataFromTable(section_name, section_code);
            }
            else if (section_name.StartsWith("IS"))
            {
                row = IS_SteelBeams.GetDataFromTable(section_name, section_code);
            }
            else if (section_name.StartsWith("W") || section_name.StartsWith("S"))
            {
                row = AISC_SteelBeams.GetDataFromTable(section_name, section_code);
            }
            return row;
        }


        public TableRolledSteelChannels Get_SteelChannels_IndianTable()
        {
            return new TableRolledSteelChannels(Path.Combine(Application.StartupPath, "TABLES\\Indian Standard"), eDesignStandard.IndianStandard);
        }
        public TableRolledSteelChannels Get_SteelChannels_AmericanTable()
        {
            return new TableRolledSteelChannels(Path.Combine(Application.StartupPath, "TABLES\\American Standard"), eDesignStandard.LRFDStandard);
        }
        public TableRolledSteelChannels Get_SteelChannels_BritishTable()
        {
            return new TableRolledSteelChannels(Path.Combine(Application.StartupPath, "TABLES\\British Standard"), eDesignStandard.BritishStandard);
        }
        public TableRolledSteelChannels Get_SteelChannels()
        {
            if (DesignStandard == eDesignStandard.BritishStandard)
                return new TableRolledSteelChannels(Path.Combine(Application.StartupPath, "TABLES\\British Standard"), DesignStandard);
            if (DesignStandard == eDesignStandard.IndianStandard)
                return new TableRolledSteelChannels(Path.Combine(Application.StartupPath, "TABLES\\Indian Standard"), DesignStandard);
            if (DesignStandard == eDesignStandard.LRFDStandard)
                return new TableRolledSteelChannels(Path.Combine(Application.StartupPath, "TABLES\\American Standard"), DesignStandard);

            return new TableRolledSteelChannels();
        }
        public RolledSteelChannelsRow Get_ChannelData_FromTable(string section_name, string section_code)
        {
            RolledSteelChannelsRow row = new RolledSteelChannelsRow();
            if (section_name.StartsWith("UK"))
            {
                row = BS_SteelChannels.GetDataFromTable(section_name, section_code);
            }
            else if (section_name.StartsWith("IS"))
            {
                row = IS_SteelChannels.GetDataFromTable(section_name, section_code);
            }
            else if (section_name.StartsWith("C"))
            {
                row = AISC_SteelChannels.GetDataFromTable(section_name, section_code);
            }
            return row;
        }


        public TableRolledSteelAngles Get_SteelAngles_IndianTable()
        {
            return new TableRolledSteelAngles(Path.Combine(Application.StartupPath, "TABLES\\Indian Standard"), eDesignStandard.IndianStandard);
        }
        public TableRolledSteelAngles Get_SteelAngles_BritishTable()
        {
            return new TableRolledSteelAngles(Path.Combine(Application.StartupPath, "TABLES\\British Standard"), eDesignStandard.BritishStandard);
        }
        public TableRolledSteelAngles Get_SteelAngles_AmericanTable()
        {
            return new TableRolledSteelAngles(Path.Combine(Application.StartupPath, "TABLES\\American Standard"), eDesignStandard.LRFDStandard);
        }
        public TableRolledSteelAngles Get_SteelAngles()
        {
            if (DesignStandard == eDesignStandard.BritishStandard)
                return new TableRolledSteelAngles(Path.Combine(Application.StartupPath, "TABLES\\British Standard"), DesignStandard);
            if (DesignStandard == eDesignStandard.IndianStandard)
                return new TableRolledSteelAngles(Path.Combine(Application.StartupPath, "TABLES\\Indian Standard"), DesignStandard);
            if (DesignStandard == eDesignStandard.LRFDStandard)
                return new TableRolledSteelAngles(Path.Combine(Application.StartupPath, "TABLES\\American Standard"), DesignStandard);

            return new TableRolledSteelAngles();
        }
        public RolledSteelAnglesRow Get_AngleData_FromTable(string section_name, string section_code, double thickness)
        {
            RolledSteelAnglesRow row = new RolledSteelAnglesRow();
            if (section_name.StartsWith("UK"))
            {
                row = BS_SteelAngles.GetDataFromTable(section_name, section_code, thickness);
            }
            else if (section_name.StartsWith("IS"))
            {
                row = IS_SteelAngles.GetDataFromTable(section_name, section_code, thickness);
            }
            else if (section_name.StartsWith("L"))
            {
                row = AISC_SteelAngles.GetDataFromTable(section_name, section_code, thickness);
            }
            return row;
        }


        public SteelConverter Steel_Convert { get; set; }


        //Chiranjit [2013 06 17]
        public void Terzaghi_Bearing_Capacity_Factors(double phi, ref double Nc, ref double Nq, ref double Nr, ref string ref_string)
        {

            phi = Double.Parse(phi.ToString("0.000"));
            int indx = 0;

            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_4.txt");
            table_file = Path.Combine(table_file, "Terzaghi_Bearing_Capacity_Factors.txt");

            List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, b1, a2, b2, returned_value;

            a1 = 0.0;
            b1 = 0.0;
            a2 = 0.0;
            b2 = 0.0;
            returned_value = 0.0;

            List<MyList> lst_list = new List<MyList>();

            for (int i = 0; i < lst_content.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(lst_content[i]);
                kStr = kStr.Replace("<=", "");
                if (i == 0)
                {
                    ref_string = kStr;
                }

                mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                if (mList.Count == 4)
                {
                    try
                    {
                        a1 = mList.GetDouble(0);

                        lst_list.Add(mList);
                    }
                    catch (Exception ex) { }
                }
            }
            
            for (int i = 0; i < lst_list.Count; i++)
            {
                a1 = lst_list[i].GetDouble(0);
                if (phi < lst_list[0].GetDouble(0))
                {
                    Nc = lst_list[0].GetDouble(1);
                    Nq = lst_list[0].GetDouble(2);
                    Nr = lst_list[0].GetDouble(3);

                    break;
                }
                else if (phi > (lst_list[lst_list.Count - 1].GetDouble(0)))
                {
                    //returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);

                    Nc = lst_list[lst_list.Count - 1].GetDouble(1);
                    Nq = lst_list[lst_list.Count - 1].GetDouble(2);
                    Nr = lst_list[lst_list.Count - 1].GetDouble(3);

                    break;
                }

                if (a1 == phi)
                {
                    //returned_value = lst_list[i].GetDouble(indx);

                    Nc = lst_list[i].GetDouble(1);
                    Nq = lst_list[i].GetDouble(2);
                    Nr = lst_list[i].GetDouble(3);

                    break;
                }
                else if (a1 > phi)
                {
                    //a2 = a1;
                    //b2 = lst_list[i].GetDouble(indx);

                    //a1 = lst_list[i - 1].GetDouble(0);
                    //b1 = lst_list[i - 1].GetDouble(indx);

                    //returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (phi - a1);

                    Nc = lst_list[i].GetDouble(1);
                    Nq = lst_list[i].GetDouble(2);
                    Nr = lst_list[i].GetDouble(3);

                    break;
                }
            }

            lst_list.Clear();
            lst_content.Clear();
        }

        public List<string> Get_Tables_Terzaghi_Bearing_Capacity_Factors()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Terzaghi_Bearing_Capacity_Factors.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }
        //Chiranjit [2013 07 18]
        public  double Limit_State_Crack(double Grade)
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Limit_State_Crack.txt");

            if (File.Exists(table_file))
            {
                list = new  List<string>(File.ReadAllLines(table_file));
                MyList mlist = null;
                for (int i = 0; i < list.Count; i++)
                {
                    try
                    {
                        mlist = new MyList(MyList.RemoveAllSpaces(list[i]), ' ');
                        if (mlist.Count == 5)
                        {
                            if (Grade == mlist.GetDouble(1))
                                return mlist.GetDouble(4);
                        }
                    }
                    catch (Exception ex) { }
                }

            }


            return 0.0;
        }

        //Chiranjit [2013 07 18]
        public List<string> Get_Tables_Limit_State_Crack()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Limit_State_Crack.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }

        public double Depth_Factor_BS5400_Table_9(double dep)
        {


            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Depth_Factors_BS5400_Table9.txt");


            if (File.Exists(table_file))
            {
                list = new List<string>(File.ReadAllLines(table_file));

                List<double> eff_depth = new List<double>();
                    List<double>  es = new List<double>();
                MyList mlist = null;
                for (int i = 0; i < list.Count; i++)
                {
                    try
                    {
                        mlist = new MyList(MyList.RemoveAllSpaces(list[i]), ' ');
                        eff_depth.Add(mlist.GetDouble(0));
                        es.Add(mlist.GetDouble(1));
                    }
                    catch (Exception ex) { }
                }



                double _es = 0.0;
                for(int i = 0; i < eff_depth.Count - 1;i++)
                {
                    if (dep > eff_depth[i + 1] && dep <= eff_depth[i])
                    {
                        _es = es[i - 1] + ((es[i] - es[i - 1]) / (eff_depth[i] - eff_depth[i - 1])) * (dep - eff_depth[i - 1]);


                        return _es;
                    }

                }
            }
            return 0.0;
        }

        public List<string> Get_Permissible_Shear_Stress()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Permissible_Shear_Stress.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }

        public List<string> Get_Depth_Factor_BS5400_Table_9()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Depth_Factors_BS5400_Table9.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }


    }

    #region Tables Data
    public class TableRolledSteelBeams
    {
        MyList mList = null;
        List<MyList> list = null;
        List<RolledSteelBeamsRow> list_record = null;
        string file_Name = "";
        /// <summary>
        /// Initialize Rolled Steel Beams Table
        /// </summary>
        /// <param name="tab_path">Table Folder Path</param>
        /// 
        public eDesignStandard DesignStandard { get; set; }

        public TableRolledSteelBeams()
        {
            DesignStandard = eDesignStandard.IndianStandard;
            string tab_path = Path.Combine(Application.StartupPath, "TABLES");
            //tab_file = Path.Combine(tab_path, @"Steel Table\Rolled Steel Beams.txt");

            if (Directory.Exists(tab_path))
            {
                this.file_Name = Path.Combine(tab_path, @"Steel Table\Rolled Steel Beams.txt");
                if (File.Exists(file_Name))
                {
                    list = new List<MyList>();
                    SetRecords();
                    return;
                }
            }
            throw new Exception("TABLE file not found in ASTRA Application folder.");

        }
        public TableRolledSteelBeams(string tab_path, eDesignStandard des_stn)
        {
            DesignStandard = des_stn;
            //tab_file = Path.Combine(tab_path, @"Steel Table\Rolled Steel Beams.txt");
            this.file_Name = Path.Combine(tab_path, @"Steel Table\Rolled Steel Beams.txt");
            list = new List<MyList>();
            SetRecords();
            //Application.StartupPath
        }
        void SetRecords()
        {
            if (!File.Exists(file_Name)) return;
            List<string> file_content = new List<string>(File.ReadAllLines(file_Name));
            list_record = new List<RolledSteelBeamsRow>();
            list = new List<MyList>();
            for (int i = 0; i < file_content.Count; i++)
            {
                mList = new MyList(MyList.RemoveAllSpaces(file_content[i].TrimEnd().TrimStart()), ' ');
                try
                {
                    list_record.Add(RolledSteelBeamsRow.Parse(file_content[i], DesignStandard));
                    list.Add(mList);
                }
                catch (Exception ex) { }
            }

        }
        public List<MyList> List
        {
            get
            {
                return list;
            }
        }
        public List<RolledSteelBeamsRow> List_Table
        {
            get
            {
                return list_record;
            }
        }

        public RolledSteelBeamsRow GetDataFromTable(string section_name, string section_code)
        {
            for (int i = 0; i < list_record.Count; i++)
            {
                if (list_record[i].SectionName == section_name && list_record[i].SectionCode == section_code)
                    return list_record[i];
            }
            return null;
        }

        public void Read_Beam_Sections(ref ComboBox cmb)
        {
            if (List_Table.Count > 0)
            {
                cmb.Items.Clear();
                foreach (var item in List_Table)
                {
                    if (item.SectionName != "")
                        if (cmb.Items.Contains(item.SectionName) == false)
                            cmb.Items.Add(item.SectionName);
                }

            }
        }
    }
    public class RolledSteelBeamsRow
    {
        public RolledSteelBeamsRow()
        {
            SectionName = "";
            SectionCode = "";
            Weight = 0.0;
            Area = 0.0;
            Depth = 0.0;
            FlangeWidth = 0.0;
            FlangeThickness = 0.0;
            WebThickness = 0.0;
            Ixx = 0.0;
            Iyy = 0.0;
            Rxx = 0.0;
            Ryy = 0.0;
            Zxx = 0.0;
            Zyy = 0.0;

        }
        /// <summary>
        /// Provide Section Name
        /// </summary>
        public string SectionName { get; set; } //1
        public string SectionCode { get; set; }//2
        public double Weight { get; set; }//3
        public double Area { get; set; }//4
        public double Depth { get; set; }//5
        public double FlangeWidth { get; set; }//7
        public double FlangeThickness { get; set; }//8
        public double WebThickness { get; set; }//9
        public double Ixx { get; set; } //10
        public double Iyy { get; set; }//11
        public double Rxx { get; set; }//12
        public double Ryy { get; set; }//13
        public double Zxx { get; set; }//14
        public double Zyy { get; set; }//15



        public MyList Data { get; set; }
        public static RolledSteelBeamsRow Parse(string s, eDesignStandard des_standard)
        {

            string kStr = MyList.RemoveAllSpaces(s.ToUpper());

            kStr = kStr.Replace(',', ' ');
            kStr = MyList.RemoveAllSpaces(kStr.ToUpper());

            MyList mlist = new MyList(kStr, ' ');

            RolledSteelBeamsRow td = new RolledSteelBeamsRow();

            td.Data = mlist;

            int index = 0;
            if (mlist.Count > 0)
            {
                if (des_standard == eDesignStandard.IndianStandard)
                {
                    td.SectionName = mlist.StringList[index]; index++;
                    td.SectionCode = mlist.StringList[index]; index++;
                    td.Weight = mlist.GetDouble(index); index++;
                    td.Area = mlist.GetDouble(index); index++;
                    td.Depth = mlist.GetDouble(index); index++;
                    td.FlangeWidth = mlist.GetDouble(index); index++;
                    td.FlangeThickness = mlist.GetDouble(index); index++;
                    td.WebThickness = mlist.GetDouble(index); index++;
                    td.Ixx = mlist.GetDouble(index); index++;
                    td.Iyy = mlist.GetDouble(index); index++;
                    td.Rxx = mlist.GetDouble(index); index++;
                    td.Ryy = mlist.GetDouble(index); index++;
                    td.Zxx = mlist.GetDouble(index); index++;
                    td.Zyy = mlist.GetDouble(index); index++;
                }
                if (des_standard == eDesignStandard.BritishStandard)
                {
                    td.SectionName = mlist.StringList[index]; index++;
                    td.SectionCode = mlist.StringList[index]; index++;
                    td.Weight = mlist.GetDouble(index); index++;
                    td.Depth = mlist.GetDouble(index); index++;
                    td.FlangeWidth = mlist.GetDouble(index); index++;
                    td.Area = mlist.GetDouble(index); index++;
                    td.WebThickness = mlist.GetDouble(index); index++;
                    td.FlangeThickness = mlist.GetDouble(index); index++;
                    td.Rxx = mlist.GetDouble(index); index++;
                    td.Ryy = mlist.GetDouble(index); index++;
                    td.Zxx = mlist.GetDouble(index); index++;
                    td.Zyy = mlist.GetDouble(index); index++;

                    td.Ixx = mlist.GetDouble(index); index++;
                    td.Iyy = mlist.GetDouble(index); index++;

                    td.Weight = td.Weight * 10; //Convert kg to Newton
                }
                if (des_standard == eDesignStandard.LRFDStandard)
                {
                    //td.SectionName = mlist.StringList[index]; index++;
                    //td.SectionCode = mlist.StringList[index]; index++;


                    //td.Weight = mlist.GetDouble(index); index++;
                    //td.Depth = mlist.GetDouble(index); index++;
                    //td.FlangeWidth = mlist.GetDouble(index); index++;
                    //td.Area = mlist.GetDouble(index); index++;
                    //td.WebThickness = mlist.GetDouble(index); index++;
                    //td.FlangeThickness = mlist.GetDouble(index); index++;
                    //td.Rxx = mlist.GetDouble(index); index++;
                    //td.Ryy = mlist.GetDouble(index); index++;
                    //td.Zxx = mlist.GetDouble(index); index++;
                    //td.Zyy = mlist.GetDouble(index); index++;

                    //td.Ixx = mlist.GetDouble(index); index++;
                    //td.Iyy = mlist.GetDouble(index); index++;




                    td.SectionName = mlist.StringList[0];
                    td.SectionCode = mlist.StringList[1] + "X" + mlist.StringList[3];


                    td.Weight = mlist.GetDouble(4);
                    td.Area = mlist.GetDouble(5) * 0.01;
                    td.Depth = mlist.GetDouble(6);
                    td.WebThickness = mlist.GetDouble(7);
                    td.FlangeWidth = mlist.GetDouble(8); 
                    td.FlangeThickness = mlist.GetDouble(9);
                    td.Ixx = mlist.GetDouble(10) * 100;
                    td.Rxx = mlist.GetDouble(12) * 0.1;
                    td.Iyy = mlist.GetDouble(13) * 100;
                    td.Ryy = mlist.GetDouble(15) * 0.1;
                    //td.Zxx = mlist.GetDouble(index); 
                    //td.Zyy = mlist.GetDouble(index); 


                    td.Weight = td.Weight * 10; //Convert kg to Newton
                }
            }
            return td;
        }
    }
    public class TableRolledSteelChannels
    {
        MyList mList = null;
        List<MyList> list = null;
        List<RolledSteelChannelsRow> list_record = null;
        string file_Name = "";
        public TableRolledSteelChannels()
        {
            DesignStandard = eDesignStandard.IndianStandard;

            string table_path = Path.Combine(Application.StartupPath, "TABLES");
            //tab_file = Path.Combine(tab_path, @"Steel Table\Rolled Steel Beams.txt");

            if (Directory.Exists(table_path))
            {
                this.file_Name = Path.Combine(table_path, @"Steel Table\Rolled Steel Channels.txt");
                list = new List<MyList>();
                SetRecords();
                return;
            }
            throw new Exception("TABLES folder not found in ASTRA Application folder.");
        }
        public eDesignStandard DesignStandard { get; set; }
        public TableRolledSteelChannels(string table_path, eDesignStandard des_stn)
        {
            //tab_file = Path.Combine(tab_path, @"Steel Table\Rolled Steel Channels.txt");
            DesignStandard = des_stn;
            this.file_Name = Path.Combine(table_path, @"Steel Table\Rolled Steel Channels.txt");
            list = new List<MyList>();
            SetRecords();
        }


        void SetRecords()
        {
            if (!File.Exists(file_Name)) return;

            List<string> file_content = new List<string>(File.ReadAllLines(file_Name));
            list_record = new List<RolledSteelChannelsRow>();
           
            list.Clear();
            for (int i = 0; i < file_content.Count; i++)
            {
                mList = new MyList(MyList.RemoveAllSpaces(file_content[i].TrimEnd().TrimStart()), ' ');
                try
                {
                    list_record.Add(RolledSteelChannelsRow.Parse(file_content[i], DesignStandard));
                    list.Add(mList);
                }
                catch (Exception ex) { }
            }

        }
        public List<MyList> List
        {
            get
            {
                return list;
            }
        }
        public List<RolledSteelChannelsRow> List_Table
        {
            get
            {
                return list_record;
            }
        }
        public List<string> Get_Channels()
        {
            List<string> Channels = new List<string>();

            for (int i = 0; i < List_Table.Count; i++)
            {
                string sec_name = List_Table[i].SectionName;
                if (!Channels.Contains(sec_name) && sec_name != "")
                {
                    Channels.Add(sec_name);
                }
            }
            return Channels;
        }
        public List<string> Get_SectionCodes(string section_name)
        {
            List<string> code1 = new List<string>();
            string sec_code, sec_name;
            for (int i = 0; i < List_Table.Count; i++)
            {
                sec_code = List_Table[i].SectionCode;
                sec_name = List_Table[i].SectionName;
                if (sec_name == section_name && sec_name != "")
                {
                    if (code1.Contains(sec_code) == false)
                    {
                        code1.Add(sec_code);
                    }
                }
            }
            return code1;
        }
        public RolledSteelChannelsRow GetDataFromTable(string section_name, string section_code)
        {
            for (int i = 0; i < list_record.Count; i++)
            {
                if (list_record[i].SectionName == section_name && list_record[i].SectionCode == section_code)
                    return list_record[i];
            }
            return null;
        }
        public void Read_Channel_Sections(ref ComboBox cmb)
        {
            if (List_Table.Count > 0)
            {
                cmb.Items.Clear();
                foreach (var item in List_Table)
                {
                    if (item.SectionName != "")
                        if (cmb.Items.Contains(item.SectionName) == false)
                            cmb.Items.Add(item.SectionName);
                }
            }
        }
    }
    public class RolledSteelChannelsRow
    {
        public RolledSteelChannelsRow()
        {
            SectionName = "";
            SectionCode = "";
            Weight = 0.0;
            Area = 0.0;
            Depth = 0.0;
            FlangeWidth = 0.0;
            FlangeThickness = 0.0;
            WebThickness = 0.0;
            CentreOfGravity = 0.0;
            Ixx = 0.0;
            Iyy = 0.0;
            Rxx = 0.0;
            Ryy = 0.0;
            Zxx = 0.0;
            Zyy = 0.0;
        }

        public string SectionName { get; set; } //1
        public string SectionCode { get; set; }//2
        public double Weight { get; set; }//3
        public double Area { get; set; }//4
        public double Depth { get; set; }//5
        public double FlangeWidth { get; set; }//7
        public double FlangeThickness { get; set; }//8
        public double WebThickness { get; set; }//9
        public double CentreOfGravity { get; set; }//9
        public double Ixx { get; set; } //10
        public double Iyy { get; set; }//11
        public double Rxx { get; set; }//12
        public double Ryy { get; set; }//13
        public double Zxx { get; set; }//14
        public double Zyy { get; set; }//15

        public MyList Data { get; set; }
        public static RolledSteelChannelsRow Parse(string s, eDesignStandard des_standard)
        {

            string kStr = MyList.RemoveAllSpaces(s.ToUpper());

            kStr = kStr.Replace(',', ' ');
            kStr = MyList.RemoveAllSpaces(kStr.ToUpper());

            MyList mlist = new MyList(kStr, ' ');

            RolledSteelChannelsRow td = new RolledSteelChannelsRow();

            td.Data = mlist;

            int index = 0;
            if (mlist.Count >= 0)
            {
                if (des_standard == eDesignStandard.IndianStandard)
                {
                    td.SectionName = mlist.StringList[index]; index++;
                    td.SectionCode = mlist.StringList[index]; index++;
                    td.Weight = mlist.GetDouble(index); index++;
                    td.Area = mlist.GetDouble(index); index++;
                    td.Depth = mlist.GetDouble(index); index++;
                    td.FlangeWidth = mlist.GetDouble(index); index++;
                    td.FlangeThickness = mlist.GetDouble(index); index++;
                    td.WebThickness = mlist.GetDouble(index); index++;
                    td.CentreOfGravity = mlist.GetDouble(index); index++;
                    td.Ixx = mlist.GetDouble(index); index++;
                    td.Iyy = mlist.GetDouble(index); index++;
                    td.Rxx = mlist.GetDouble(index); index++;
                    td.Ryy = mlist.GetDouble(index); index++;
                    td.Zxx = mlist.GetDouble(index); index++;
                    td.Zyy = mlist.GetDouble(index); index++;
                }
                else if (des_standard == eDesignStandard.BritishStandard)
                {
                    td.SectionName = mlist.StringList[index]; index++;
                    td.SectionCode = mlist.StringList[index]; index++;
                    td.Weight = mlist.GetDouble(index); index++;

                    td.Weight = td.Weight * 10.0; // Convert kg/m  to N/m

                    td.Depth = mlist.GetDouble(index); index++;
                    td.FlangeWidth = mlist.GetDouble(index); index++;
                    td.Area = mlist.GetDouble(index); index++;
                    td.WebThickness = mlist.GetDouble(index); index++;
                    td.FlangeThickness = mlist.GetDouble(index); index++;
                    td.CentreOfGravity = mlist.GetDouble(index); index++;
                   
                    td.Rxx = mlist.GetDouble(index); index++;
                    td.Ryy = mlist.GetDouble(index); index++;
                    td.Zxx = mlist.GetDouble(index); index++;
                    td.Zyy = mlist.GetDouble(index); index++;

                    td.Ixx = mlist.GetDouble(index); index++;
                    td.Iyy = mlist.GetDouble(index); index++;
                }
                else if (des_standard == eDesignStandard.LRFDStandard)
                {
                    //td.SectionName = mlist.StringList[index]; index++;
                    //td.SectionCode = mlist.StringList[index]; index++;
                    //td.Weight = mlist.GetDouble(index); index++;

                    //td.Weight = td.Weight * 10.0; // Convert kg/m  to N/m


                    //td.Depth = mlist.GetDouble(index); index++;
                    //td.FlangeWidth = mlist.GetDouble(index); index++;
                    //td.Area = mlist.GetDouble(index); index++;
                    //td.WebThickness = mlist.GetDouble(index); index++;
                    //td.FlangeThickness = mlist.GetDouble(index); index++;
                    //td.CentreOfGravity = mlist.GetDouble(index); index++;

                    //td.Rxx = mlist.GetDouble(index); index++;
                    //td.Ryy = mlist.GetDouble(index); index++;
                    //td.Zxx = mlist.GetDouble(index); index++;
                    //td.Zyy = mlist.GetDouble(index); index++;

                    //td.Ixx = mlist.GetDouble(index); index++;
                    //td.Iyy = mlist.GetDouble(index); index++;


                    td.SectionName = mlist.StringList[0]; 
                    td.SectionCode = mlist.StringList[1] + "X" + mlist.StringList[3]; 
                    td.Weight = mlist.GetDouble(4); 

                    td.Weight = td.Weight * 10.0; // Convert kg/m  to N/m


                    td.Depth = mlist.GetDouble(6); 
                    td.FlangeWidth = mlist.GetDouble(8);
                    td.Area = mlist.GetDouble(5) * 0.01; 
                    td.WebThickness = mlist.GetDouble(7); 
                    td.FlangeThickness = mlist.GetDouble(9); 
                    td.CentreOfGravity = mlist.GetDouble(16);

                    td.Rxx = mlist.GetDouble(12) * 0.1;
                    td.Ryy = mlist.GetDouble(15) * 0.1;
                    td.Zxx = mlist.GetDouble(11);
                    td.Zyy = mlist.GetDouble(14);

                    td.Ixx = mlist.GetDouble(10) * 100;
                    td.Iyy = mlist.GetDouble(13) * 100; 
                }
            }
            return td;

        }
    }

    public class TableRolledSteelAngles
    {
        MyList mList = null;
        List<MyList> list = null;
        List<RolledSteelAnglesRow> list_record = null;
        string equal_angle_file_name = "";
        string unequal_angle_file_name = "";
        public eDesignStandard DesignStandard { get; set; }
        public TableRolledSteelAngles()
        {
            DesignStandard = eDesignStandard.IndianStandard;
            string file_path = Path.Combine(Application.StartupPath, "TABLES");
            if (Directory.Exists(file_path))
            {
                this.equal_angle_file_name = Path.Combine(file_path, @"Steel Table\Rolled Steel Equal Angles.txt");
                this.unequal_angle_file_name = Path.Combine(file_path, @"Steel Table\Rolled Steel Unequal Angles.txt");
                list = new List<MyList>();
                SetAngleTable(true);
                SetAngleTable(false);
                return;
            }
            throw new Exception("TABLES folder not found.");
        }
        public TableRolledSteelAngles(string file_path, eDesignStandard des_stn)
        {
            DesignStandard = des_stn;


            this.equal_angle_file_name = Path.Combine(file_path, @"Steel Table\Rolled Steel Equal Angles.txt");
            this.unequal_angle_file_name = Path.Combine(file_path, @"Steel Table\Rolled Steel Unequal Angles.txt");

            list = new List<MyList>();
            //if (DesignStandard == eDesignStandard.LRFDStandard)
            //{
            //    this.equal_angle_file_name = Path.Combine(file_path, @"Steel Table\Rolled Steel Angles.txt");
            //    this.unequal_angle_file_name = Path.Combine(file_path, @"Steel Table\Rolled Steel Angles.txt");
            //    SetAngleTable(true);
            //    SetAngleTable(false);
            //}
            //else
            //{
            //    SetAngleTable(true);
            //    SetAngleTable(false);
            //}
            SetAngleTable(true);
            SetAngleTable(false);
        }
        void SetAngleTable(bool IsEqualAngles)
        {

            bool flag = false;
            List<string> file_content = null;
            if (!File.Exists(equal_angle_file_name) || !File.Exists(unequal_angle_file_name))
            {
                MessageBox.Show("Table file not found in the application folder", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (IsEqualAngles) file_content = new List<string>(File.ReadAllLines(equal_angle_file_name));
            else file_content = new List<string>(File.ReadAllLines(unequal_angle_file_name));

            if (IsEqualAngles)
                list_record = new List<RolledSteelAnglesRow>();
            RolledSteelAnglesRow row = null;
            for (int i = 0; i < file_content.Count; i++)
            {
                if (DesignStandard == eDesignStandard.LRFDStandard)
                {
                    mList = new MyList(MyList.RemoveAllSpaces(file_content[i].TrimEnd().TrimStart()), ' ');
                    if (mList.Count < 2) continue; 
                    if (mList.StringList[0].StartsWith("L") || flag == true)
                    {
                        flag = true;
                        try
                        {
                            row = RolledSteelAnglesRow.Parse(file_content[i], IsEqualAngles, DesignStandard);
                            if (row.SectionName == "")
                            {
                                row.SectionName = list_record[list_record.Count - 1].SectionName;
                                row.SectionCode = list_record[list_record.Count - 1].SectionCode;
                            }
                            list_record.Add(row);
                            list.Add(mList);
                        }
                        catch (Exception ex) { }
                    }
                }
                else
                {
                    mList = new MyList(MyList.RemoveAllSpaces(file_content[i].TrimEnd().TrimStart()), ' ');
                    if (mList.StringList[0].StartsWith("ISA") || mList.StringList[0].StartsWith("UKA") || flag == true)
                    {
                        flag = true;
                        try
                        {
                            row = RolledSteelAnglesRow.Parse(file_content[i], IsEqualAngles, DesignStandard);
                            if (row.SectionName == "")
                            {
                                row.SectionName = list_record[list_record.Count - 1].SectionName;
                                row.SectionCode = list_record[list_record.Count - 1].SectionCode;
                            }
                            list_record.Add(row);
                            list.Add(mList);
                        }
                        catch (Exception ex) { }
                    }
                }

            }

        }

        public List<MyList> List
        {
            get
            {
                return list;
            }
        }
        public List<RolledSteelAnglesRow> List_Table
        {
            get
            {
                return list_record;
            }
        }
        public void Read_Angle_Sections(ref ComboBox cmb)
        {
            if (List_Table.Count > 0)
            {
                cmb.Items.Clear();
                foreach (var item in List_Table)
                {
                    if (item.SectionName != "")
                        if (cmb.Items.Contains(item.SectionName) == false)
                            cmb.Items.Add(item.SectionName);
                }
            }
        }
        public void Read_Angle_Sections(ref ComboBox cmb, bool isClearItems)
        {
            if (List_Table.Count > 0)
            {
                if (isClearItems) cmb.Items.Clear();
                foreach (var item in List_Table)
                {
                    if (item.SectionName != "")
                        if (cmb.Items.Contains(item.SectionName) == false)
                            cmb.Items.Add(item.SectionName);
                }
            }
        }
      
        public RolledSteelAnglesRow GetDataFromTable(string section_name, string section_code, double thickness)
        {
            string s_name = "", s_code = "";
            double thk = 0;

            section_code = section_code.ToUpper().Replace("X", "").Trim().TrimEnd().TrimStart();

            for (int i = 0; i < list_record.Count; i++)
            {
                s_name = list_record[i].SectionName.ToUpper().Replace("X", "").Trim().TrimEnd().TrimStart();
                s_code = list_record[i].SectionCode.ToUpper().Replace("X", "").Trim().TrimEnd().TrimStart();

                if (s_name == section_name
                    && s_code == section_code
                    && list_record[i].Thickness == thickness)
                    return list_record[i];

                s_code = list_record[i].SectionSize.ToUpper().Replace("X", "").Trim().TrimEnd().TrimStart();

                if (s_name == section_name
                    && s_code == section_code
                     && list_record[i].Thickness == thickness)
                    return list_record[i];


            }
            if (list_record.Count > 6)
                return list_record[6];
            return null;
        }
    }
    public class RolledSteelAnglesRow
    {
        public RolledSteelAnglesRow()
        {
            SectionName = "";
            SectionCode = "";

            Weight = 0.0;
            Area = 0.0;
            Ixx = 0.0;
            Iyy = 0.0;
            Cxx = 0.0;
            Cyy = 0.0;
            Exx = 0.0;
            Eyy = 0.0;

            Zxx = 0.0;
            Zyy = 0.0;
            /// doo
            double d;

        }
        public string SectionName { get; set; }
        public string SectionCode { get; set; }
        public string SectionSize
        {
            get
            {
                //string kStr = SectionCode.ToUpper().Replace("X", "");

                //string cd = kStr;
                //if (kStr.Length % 2 != 0)
                //{
                //    int i = kStr.Length / 2 + 1;

                //    //Length_1 = MyList.StringToDouble(kStr.Substring(0, i), 0.0);
                //    //Length_2 = MyList.StringToDouble(kStr.Substring(i, kStr.Length - (i)), 0.0);
                //    kStr = Length_1.ToString() + "x" + Length_2.ToString();
                //}
                //else
                //{
                //    int i = kStr.Length / 2;
                //    //Length_1 = MyList.StringToDouble(kStr.Substring(0, i), 0.0);
                //    //Length_2 = MyList.StringToDouble(kStr.Substring(i, kStr.Length - (i)), 0.0);
                //    kStr = Length_1.ToString() + "x" + Length_2.ToString();
                //}
                return Length_1.ToString() + "X" + Length_2.ToString();
            }
        }



        public double Length_1
        {
            get
            {
                string kStr = SectionCode.ToUpper().Replace("X", "");

                string cd = kStr;
                if (kStr.Length % 2 != 0)
                {
                    int i = kStr.Length / 2 + 1;

                    return MyList.StringToDouble(kStr.Substring(0, i), 0.0);
                    //Length_2 = MyList.StringToDouble(kStr.Substring(i, kStr.Length - (i)), 0.0);
                    //kStr = Length_1.ToString() + "x" + Length_2.ToString();
                }
                else
                {
                    int i = kStr.Length / 2;
                    return MyList.StringToDouble(kStr.Substring(0, i), 0.0);
                    //return MyList.StringToDouble(kStr.Substring(i, kStr.Length - i), 0.0);
                    //kStr = Length_1.ToString() + "x" + Length_2.ToString();
                }
                return 0.0;
            }
        }
        public double Length_2
        {
            get
            {
                string kStr = SectionCode.ToUpper().Replace("X", "");

                //string cd = kStr;
                if (kStr.Length % 2 != 0)
                {
                    int i = kStr.Length / 2 + 1;
                    //MyList.StringToDouble(kStr.Substring(0, i), 0.0);
                    return MyList.StringToDouble(kStr.Substring(i, kStr.Length - i), 0.0);
                    //kStr = Length_1.ToString() + "x" + Length_2.ToString();
                }
                else
                {
                    int i = kStr.Length / 2;
                    //Length_1 = MyList.StringToDouble(kStr.Substring(0, i), 0.0);
                    return MyList.StringToDouble(kStr.Substring(i, kStr.Length - i), 0.0);
                    //kStr = Length_1.ToString() + "x" + Length_2.ToString();
                }
                return 0.0;
            }
        }
        public double Thickness { get; set; } //Weight per metre
        public double Weight { get; set; } //Weight per metre
        public double Area { get; set; } // Sectoinal Area

        public double Ixx { get; set; } //Moment of Inertia
        public double Iyy { get; set; } //Moment of Inertia
        public double Cxx { get; set; } //Centre of Gravity
        public double Cyy { get; set; } //Centre of Gravity
        public double Exx { get; set; } //Distance of Extreme Fibre
        public double Eyy { get; set; }

        public double Zxx { get; set; } //Distance of Extreme Fibre
        public double Zyy { get; set; }

        public static RolledSteelAnglesRow Parse(string s, bool IsEqualAngle, eDesignStandard des_standard)
        {
            string kStr = MyList.RemoveAllSpaces(s.ToUpper());

            kStr = kStr.Replace('+', ' ');
            kStr = kStr.Replace(',', ' ');
            kStr = MyList.RemoveAllSpaces(kStr.ToUpper());

            MyList mlist = new MyList(kStr, ' ');

            RolledSteelAnglesRow td = new RolledSteelAnglesRow();
            int index = 0;
            if (mlist.Count >= 0)
            {
                if (des_standard == eDesignStandard.IndianStandard)
                {
                    if (mlist.StringList[0].StartsWith("ISA"))
                    {
                        td.SectionName = mlist.StringList[index]; index++;
                        td.SectionCode = mlist.StringList[index]; index++;
                        //if (IsEqualAngle)
                        //{
                        index++; index++; index++;
                        //}
                    }
                    td.Thickness = mlist.GetDouble(index); index++;
                    td.Area = mlist.GetDouble(index); index++;
                    td.Weight = mlist.GetDouble(index); index++;
                    td.Cxx = mlist.GetDouble(index);// index++;
                    if (!IsEqualAngle) index++;
                    td.Cyy = mlist.GetDouble(index); index++;
                    td.Exx = mlist.GetDouble(index);// index++;
                    if (!IsEqualAngle) index++;
                    td.Eyy = mlist.GetDouble(index); index++;
                    td.Ixx = mlist.GetDouble(index); // index++;
                    if (!IsEqualAngle) index++;
                    td.Iyy = mlist.GetDouble(index); index++;

                    if (IsEqualAngle)
                    {
                        if (mlist.Count == 13)
                        {
                            td.Zyy = mlist.GetDouble(11);
                            td.Zxx = mlist.GetDouble(11);
                        }
                        else if (mlist.Count == 20)
                        {
                            td.Zyy = mlist.GetDouble(16);
                            td.Zxx = mlist.GetDouble(16);
                        }
                    }
                    else
                    {
                        if (mlist.Count == 19)
                        {
                            td.Zyy = mlist.GetDouble(15);
                            td.Zxx = mlist.GetDouble(16);
                        }
                        else if (mlist.Count == 26)
                        {
                            td.Zyy = mlist.GetDouble(20);
                            td.Zxx = mlist.GetDouble(21);
                        }
                    }


                }
                if (des_standard == eDesignStandard.BritishStandard)
                {
                    index = 0;
                    if (mlist.StringList[0].StartsWith("UKA"))
                    {
                        td.SectionName = mlist.StringList[index]; index++;
                        td.SectionCode = mlist.StringList[index]; index++;
                    }

                    td.Thickness = mlist.GetDouble(index); index++;
                    td.Weight = mlist.GetDouble(index); index++;
                    td.Weight = td.Weight * 10.0;

                    double r1 = mlist.GetDouble(index); index++;
                    double r2 = mlist.GetDouble(index); index++;
                    td.Area = mlist.GetDouble(index); index++;

                    td.Cxx = mlist.GetDouble(index);// index++;
                    if (!IsEqualAngle) index++;
                    td.Cyy = mlist.GetDouble(index); index++;
                    td.Ixx = mlist.GetDouble(index); // index++;
                    if (!IsEqualAngle) index++;
                    td.Iyy = mlist.GetDouble(index); index++;
                    td.Exx = mlist.GetDouble(index);// index++;
                    if (!IsEqualAngle) index++;
                    td.Eyy = mlist.GetDouble(index); index++;
                   
                }

                if (des_standard == eDesignStandard.LRFDStandard)
                {
                    index = 0;
                    if (mlist.StringList[0].StartsWith("L"))
                    {
                        td.SectionName = mlist.StringList[0]; 
                        td.SectionCode = mlist.StringList[1] + "X" + mlist.StringList[3];
                    }

                    index = 5;

                    if (IsEqualAngle)
                    {
                        td.Thickness = mlist.GetDouble(index); index++;
                        td.Weight = mlist.GetDouble(index); index++;
                        td.Weight = td.Weight * 10.0;

                        //double r1 = mlist.GetDouble(index); index++;
                        //double r2 = mlist.GetDouble(index); index++;
                        td.Area = mlist.GetDouble(index) * 0.01 ; index++;
                        td.Ixx = mlist.GetDouble(index) * 100; // index++;
                        td.Iyy = td.Ixx; // index++;

                        index++; index++; index++;

                        td.Cxx = mlist.GetDouble(index);// index++;
                        td.Cyy = td.Cxx; // index++;

                        //if (!IsEqualAngle) index++;
                        //td.Cyy = mlist.GetDouble(index); index++;
                        //td.Ixx = mlist.GetDouble(index); // index++;
                        //if (!IsEqualAngle) index++;
                        //td.Iyy = mlist.GetDouble(index); index++;
                        //td.Exx = mlist.GetDouble(index);// index++;
                        //if (!IsEqualAngle) index++;
                        //td.Eyy = mlist.GetDouble(index); index++;
                    }
                    else
                    {
                        td.Thickness = mlist.GetDouble(index); index++;
                        td.Weight = mlist.GetDouble(index); index++;
                        td.Weight = td.Weight * 10.0;

                        //double r1 = mlist.GetDouble(index); index++;
                        //double r2 = mlist.GetDouble(index); index++;
                        td.Area = mlist.GetDouble(index)* 0.01; index++;
                        td.Ixx = mlist.GetDouble(index) * 100; // index++;
                        td.Iyy = mlist.GetDouble(12) * 0.0001;  // index++;


                        td.Cxx = mlist.GetDouble(15);// index++;
                        td.Cyy = td.Cxx; // index++;



                        //if (!IsEqualAngle) index++;
                        //td.Cyy = mlist.GetDouble(index); index++;
                        //td.Ixx = mlist.GetDouble(index); // index++;
                        //if (!IsEqualAngle) index++;
                        //td.Iyy = mlist.GetDouble(index); index++;
                        //td.Exx = mlist.GetDouble(index);// index++;
                        //if (!IsEqualAngle) index++;
                        //td.Eyy = mlist.GetDouble(index); index++;
                    }
                }
            }
            return td;

        }
    }
    #endregion Tables Data

    public class SteelConvert_Data
    {

        public SteelConvert_Data()
        {
            IS_Section_Name = "";
            BS_Section_Name = "";

            IS_Section_Code = "";
            BS_Section_Code = "";

            IS_Angle_Thickness = 0.0;
            BS_Angle_Thickness = 0.0;
        }
        //ISA                2020                3                UKA                2020                3
        //ISA                2020                4                UKA                2020                3

        //ISMB                100                UKB                127x76x13                                
        //ISMB                125                UKB                127x76x13

        //ISMC                400                UKPFC                380x100x54                                
        //ISMC                350                UKPFC                380x100x54

        public string IS_Section_Name { get; set; }
        public string BS_Section_Name { get; set; }
        public string AISC_Section_Name { get; set; }

        public string IS_Section_Code { get; set; }
        public string BS_Section_Code { get; set; }
        public string AISC_Section_Code { get; set; }

        public double IS_Angle_Thickness { get; set; }
        public double BS_Angle_Thickness { get; set; }
        public double AISC_Angle_Thickness { get; set; }




        public static SteelConvert_Data Parse(string txt)
        {
            string kStr = txt.ToUpper().Replace(",", " ").Trim().TrimEnd().TrimStart();

            SteelConvert_Data data = null;
            kStr = MyList.RemoveAllSpaces(kStr);

            MyList mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');


            try
            {
                if (mList.Count == 4 || mList.Count == 6 || mList.Count == 9)
                    data = new SteelConvert_Data();

                for (int i = 0; i < mList.Count; i++)
                {
                    kStr = mList.StringList[i];

                    if (kStr.StartsWith("IS"))
                    {
                        data.IS_Section_Name = kStr;
                        if (kStr.EndsWith("A")) // Angles
                        {
                            data.IS_Section_Code = mList.StringList[++i];
                            data.IS_Angle_Thickness = mList.GetDouble(++i);
                            //i += 1;
                        }
                        else if (kStr.EndsWith("B")) // Beams
                        {
                            data.IS_Section_Code = mList.StringList[++i];
                        }
                        else if (kStr.EndsWith("C")) //Channels
                        {
                            data.IS_Section_Code = mList.StringList[++i];
                        }
                    }
                    else if (kStr.StartsWith("UK"))
                    {
                        data.BS_Section_Name = kStr;
                        if (kStr.EndsWith("A")) // Angles
                        {
                            data.BS_Section_Code = mList.StringList[++i];
                            data.BS_Angle_Thickness = mList.GetDouble(++i);
                            //i += 1;
                        }
                        else if (kStr.EndsWith("B")) // Beams
                        {
                            data.BS_Section_Code = mList.StringList[++i];
                        }
                        else if (kStr.EndsWith("C")) //Channels
                        {
                            data.BS_Section_Code = mList.StringList[++i];
                        }
                    }
                    else if (kStr.StartsWith("AISC"))
                    {
                        data.AISC_Section_Name = kStr.Replace("AISC","");
                        if (kStr.EndsWith("L")) // Angles
                        {
                            data.AISC_Section_Code = mList.StringList[++i];
                            data.AISC_Angle_Thickness = mList.GetDouble(++i);
                            //i += 1;
                        }
                        else if (kStr.EndsWith("B") || kStr.EndsWith("W") || kStr.EndsWith("S")) // Beams
                        {
                            data.AISC_Section_Code = mList.StringList[++i];
                        }
                        else if (kStr.EndsWith("C")) //Channels
                        {
                            data.AISC_Section_Code = mList.StringList[++i];
                        }
                    }
                }
            }
            catch (Exception ex) { data = null; }
            return data;
        }

        public override string ToString()
        {
            return string.Format("{0,-10} {1,-15} {2,-10} {3,-10} {4,-15} {5,-10} {6,-15} {7,-10}",
                IS_Section_Name, IS_Section_Code, ((IS_Angle_Thickness == 0.0) ? "" : IS_Angle_Thickness.ToString()),
                BS_Section_Name, BS_Section_Code, ((BS_Angle_Thickness == 0.0) ? "" : BS_Angle_Thickness.ToString()),
                AISC_Section_Name, AISC_Section_Code, ((AISC_Angle_Thickness == 0.0) ? "" : AISC_Angle_Thickness.ToString()));
        }
    }
    public class SteelConverter : IList<SteelConvert_Data>
    {
        List<SteelConvert_Data> list = null;
        public SteelConverter()
        {
            list = new List<SteelConvert_Data>();
            //string file_name = Path.Combine(Application.StartupPath, "TABLES\\Convert_IS_to_BS.txt");
            string file_name = Path.Combine(Application.StartupPath, "TABLES\\Convert_IS_to_BS_to_AISC.txt");
            ReadFromFile(file_name);
        }

        public void ReadFromFile(string file_name)
        {
            if (!File.Exists(file_name)) return;
            SteelConvert_Data data = null;
            foreach (var item in File.ReadAllLines(file_name))
            {
                data = SteelConvert_Data.Parse(item);
                if (data != null)
                    list.Add(data);
            }
        }

        #region IList<SteelConvert_Data> Members

        public int IndexOf(SteelConvert_Data item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].IS_Section_Name == item.IS_Section_Name &&
                    list[i].IS_Section_Code == item.IS_Section_Code &&
                    list[i].BS_Section_Name == item.BS_Section_Name &&
                    list[i].BS_Section_Code == item.BS_Section_Code)
                    return i;
            }

            return -1;
        }

        public void Insert(int index, SteelConvert_Data item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public SteelConvert_Data this[int index]
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

        #region ICollection<SteelConvert_Data> Members

        public void Add(SteelConvert_Data item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(SteelConvert_Data item)
        {
            return (IndexOf(item) != -1);
        }

        public void CopyTo(SteelConvert_Data[] array, int arrayIndex)
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

        public bool Remove(SteelConvert_Data item)
        {
            int i = IndexOf(item);

            if (i != -1)
            {
                RemoveAt(i);
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<SteelConvert_Data> Members

        public IEnumerator<SteelConvert_Data> GetEnumerator()
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

        public void Convert_IS_to_BS(ref SectionData sec)
        {
            string sec_name = sec.SectionName.Replace("+", "");
            string sec_code = sec.SectionCode.Replace("+", "");

            MyList ml = new MyList(sec_code, 'X');

            foreach (var item in list)
            {
                if (sec_name.EndsWith("A"))
                {
                    if (item.IS_Section_Name == sec_name &&
                        item.IS_Section_Code == sec_code &&
                        item.IS_Angle_Thickness == sec.AngleThickness)
                    {
                        sec.SectionName = item.BS_Section_Name;
                        sec.SectionCode = item.BS_Section_Code;
                        sec.AngleThickness = item.BS_Angle_Thickness;
                        return;
                    }
                    if (ml.Count > 2)
                    {
                        sec_code = ml[0] + ml[1];
                        if (item.IS_Section_Name == sec_name &&
                            item.IS_Section_Code == sec_code &&
                            item.IS_Angle_Thickness == ml.GetDouble(2))
                        {
                            sec.SectionName = item.BS_Section_Name;
                            sec.SectionCode = item.BS_Section_Code;
                            sec.AngleThickness = item.BS_Angle_Thickness;
                            return;
                        }
                    }
                }
                else
                {
                    if (item.IS_Section_Name == sec_name &&
                        item.IS_Section_Code == sec_code)
                    {
                        sec.SectionName = item.BS_Section_Name;
                        sec.SectionCode = item.BS_Section_Code;
                        return;
                    }
                }
            }
        }

        public void Convert_IS_to_AISC(ref SectionData sec)
        {

            string sec_name = sec.SectionName.Replace("+", "");
            string sec_code = sec.SectionCode.Replace("+", "");

            MyList ml = new MyList(sec_code, 'X');

            foreach (var item in list)
            {
                if (sec_name.EndsWith("A"))
                {
                    if (item.IS_Section_Name == sec_name &&
                        item.IS_Section_Code == sec_code &&
                        item.IS_Angle_Thickness == sec.AngleThickness)
                    {
                        sec.SectionName = item.AISC_Section_Name;
                        sec.SectionCode = item.AISC_Section_Code;
                        sec.AngleThickness = item.AISC_Angle_Thickness;
                        return;
                    }
                    if (ml.Count > 2)
                    {
                        sec_code = ml[0] + ml[1];
                        if (item.IS_Section_Name == sec_name &&
                            item.IS_Section_Code == sec_code &&
                            item.IS_Angle_Thickness == ml.GetDouble(2))
                        {
                            sec.SectionName = item.AISC_Section_Name;
                            sec.SectionCode = item.AISC_Section_Code;
                            sec.AngleThickness = item.AISC_Angle_Thickness;
                            return;
                        }
                    }
                }
                else
                {
                    if (item.IS_Section_Name == sec_name &&
                        item.IS_Section_Code == sec_code)
                    {
                        sec.SectionName = item.AISC_Section_Name;
                        sec.SectionCode = item.AISC_Section_Code;
                        return;
                    }
                }
            }
        }

        public void Convert_BS_to_IS(ref SectionData sec)
        {
            string sec_name = sec.SectionName.Replace("+", "");
            string sec_code = sec.SectionCode.Replace("+", "");
            foreach (var item in list)
            {
                if (sec_name.EndsWith("A"))
                {
                    if (item.BS_Section_Name == sec_name &&
                        item.BS_Section_Code == sec_code &&
                        item.BS_Angle_Thickness == sec.AngleThickness)
                    {
                        sec.SectionName = item.IS_Section_Name;
                        sec.SectionCode = item.IS_Section_Code;
                        sec.AngleThickness = item.IS_Angle_Thickness;
                        return;
                    }
                }
                else
                {
                    if (item.BS_Section_Name == sec_name &&
                        item.BS_Section_Code == sec_code)
                    {
                        sec.SectionName = item.IS_Section_Name;
                        sec.SectionCode = item.IS_Section_Code;
                        return;
                    }
                }
            }
        }

        public void Convert_BS_to_AISC(ref SectionData sec)
        {
            string sec_name = sec.SectionName.Replace("+", "");
            string sec_code = sec.SectionCode.Replace("+", "");
            foreach (var item in list)
            {
                if (sec_name.EndsWith("A"))
                {
                    if (item.BS_Section_Name == sec_name &&
                        item.BS_Section_Code == sec_code &&
                        item.BS_Angle_Thickness == sec.AngleThickness)
                    {
                        sec.SectionName = item.AISC_Section_Name;
                        sec.SectionCode = item.AISC_Section_Code;
                        sec.AngleThickness = item.AISC_Angle_Thickness;
                        return;
                    }
                }
                else
                {
                    if (item.BS_Section_Name == sec_name &&
                        item.BS_Section_Code == sec_code)
                    {
                        sec.SectionName = item.AISC_Section_Name;
                        sec.SectionCode = item.AISC_Section_Code;
                        return;
                    }
                }
            }
        }

        public void Convert_AISC_to_IS(ref SectionData sec)
        {
            string sec_name = sec.SectionName.Replace("+", "");
            string sec_code = sec.SectionCode.Replace("+", "");
            foreach (var item in list)
            {
                if (sec_name.EndsWith("L"))
                {
                    if (item.AISC_Section_Name == sec_name &&
                        item.AISC_Section_Code == sec_code &&
                        item.AISC_Angle_Thickness == sec.AngleThickness)
                    {
                        sec.SectionName = item.IS_Section_Name;
                        sec.SectionCode = item.IS_Section_Code;
                        sec.AngleThickness = item.IS_Angle_Thickness;
                        return;
                    }
                }
                else
                {
                    if (item.AISC_Section_Name == sec_name &&
                        item.AISC_Section_Code == sec_code)
                    {
                        sec.SectionName = item.IS_Section_Name;
                        sec.SectionCode = item.IS_Section_Code;
                        return;
                    }
                }
            }
        }

        public void Convert_AISC_to_BS(ref SectionData sec)
        {
            string sec_name = sec.SectionName.Replace("+", "");
            string sec_code = sec.SectionCode.Replace("+", "");
            foreach (var item in list)
            {
                if (sec_name.EndsWith("L"))
                {
                    if (item.AISC_Section_Name == sec_name &&
                        item.AISC_Section_Code == sec_code &&
                        item.AISC_Angle_Thickness == sec.AngleThickness)
                    {
                        sec.SectionName = item.BS_Section_Name;
                        sec.SectionCode = item.BS_Section_Code;
                        sec.AngleThickness = item.BS_Angle_Thickness;
                        return;
                    }
                }
                else
                {
                    if (item.AISC_Section_Name == sec_name &&
                        item.AISC_Section_Code == sec_code)
                    {
                        sec.SectionName = item.BS_Section_Name;
                        sec.SectionCode = item.BS_Section_Code;
                        return;
                    }
                }
            }
        }

    }



    #region Board Guage


    

    #endregion Board Guage


    //Chiranjit [2012 11 13]
    //Pipe Culvert Data
    public class PipeCulvertStrength
    {
        public List<PipeCulvertData> Class_NP4 { get; set; }
        public List<PipeCulvertData> Class_NP3 { get; set; }
        public List<string> Table_NP3 { get; set; }
        public List<string> Table_NP4 { get; set; }
        public PipeCulvertStrength(string table_folder)
        {
            try
            {
                Class_NP3 = new List<PipeCulvertData>();
                Class_NP4 = new List<PipeCulvertData>();
                string str = Path.Combine(table_folder, "PipeCulvert_Strength_NP3.txt");
                if (File.Exists(str))
                    Table_NP3 = new List<string>(File.ReadAllLines(str));
                
                str = Path.Combine(table_folder, "PipeCulvert_Strength_NP4.txt");
                if (File.Exists(str))
                    Table_NP4 = new List<string>(File.ReadAllLines(str));
                Read_Table_Data();
            }
            catch(Exception ex  ){}
        }
        void Read_Table_Data()
        {
            try
            {
                PipeCulvertData mlist = null;
                string str = "";
                foreach (var item in Table_NP3)
                {
                    str = PipeCulvertData.RemoveAllSpaces(item);
                    mlist = new PipeCulvertData(str, ' ');
                    if (mlist.IsDouble(0))
                    {
                        Class_NP3.Add(mlist);
                    }
                }
                foreach (var item in Table_NP4)
                {
                    str = PipeCulvertData.RemoveAllSpaces(item);
                    mlist = new PipeCulvertData(str, ' ');
                    if (mlist.IsDouble(0))
                    {
                        Class_NP4.Add(mlist);
                    }
                }
            }
            catch (Exception ex) { }

        }

        public PipeCulvertData Get_NP3_Data(double dia)
        {
            foreach (var item in Class_NP3)
            {
                if (item.Internal_Diameter == dia) return item;
            }
            return null;
        }
        public PipeCulvertData Get_NP4_Data(double dia)
        {
            foreach (var item in Class_NP4)
            {
                if (item.Internal_Diameter == dia) return item;
            }
            return null;
        }
    }

    public class PipeCulvertData : MyList
    {
        public double Internal_Diameter
        {
            get
            {
                try
                {
                    return this.GetDouble(0);
                }
                catch (Exception ex) { }

                return 0.0;
            }
        }
        public double Longitudinal_Reinforcement
        {
            get
            {
                try
                {
                    return this.GetDouble(1);
                }
                catch (Exception ex) { }

                return 0.0;
            }
        }
        public double Spiral_Reinforcement
        {
            get
            {
                try
                {
                    return this.GetDouble(2);
                }
                catch (Exception ex) { }

                return 0.0;
            }
        }
        public double Mild_Steel_Reinforcement
        {
            get
            {
                try
                {
                    return Spiral_Reinforcement * (140.0 / 125.0);
                }
                catch (Exception ex) { }

                return 0.0;
            }
        }
        public double Ultimate_Load
        {
            get
            {
                try
                {
                    return this.GetDouble(3);
                }
                catch (Exception ex) { }

                return 0.0;
            }
        }
        public PipeCulvertData(string s, char sp)
            : base(s, sp)
        {
        }


    }
    //public class PipeCulvert
    public enum CONCRETE_GRADE
    {
        M50 = 50,
        M45 = 45,
        M40 = 40,
        M35 = 35,
        M30 = 30,
        M25 = 25,
        M20 = 20,
        M15 = 15,
    }
    public enum STEEL_GRADE
    {
        Fe240 = 240,
        Fe415 = 415,
        Fe500 = 500,
    }

}

