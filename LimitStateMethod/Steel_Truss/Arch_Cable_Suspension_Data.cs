using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using AstraInterface.DataStructure;

namespace LimitStateMethod.Steel_Truss
{
    public class Arch_Cable_Suspension_Data
    {
        public double _L, _B, _aw, _lfw, _rfw, _hrw, _cw, _rw, _cgw, _NS, _NP;

        public Arch_Cable_Suspension_Data()
        {
            Length = 50.0;
            Breadth = 5.40;
            Height = 6.0;
            Is_Add_BCB = true;
            Initialize();
        }


        public string Start_Support { get; set; }
        public string End_Support { get; set; }

        public bool Is_Add_BCB { get; set; }


        public double Length { get; set; }
        public double D1 { get; set; }
        public double D2 { get; set; }
        public double ds { get; set; }
        public double PanelLength
        {
            get
            {
                return ((_L - (2 * _cgw)) / (_NP - 2));
            }
        }

        public double PanelWidth
        {
            get
            {
                return (_cw / (_NS - 1));
            }
        }

        public double Breadth { get; set; }
        public double Footpath_Width { get; set; }


        //Default 3
        public int NoOfStringerBeam { get; set; }

        public void Initialize()
        {
            #region Variable Declarations
            Member mbr = new Member();
            Cross_Girders = new MemberCollection();
            Xgirder1 = new MemberCollection();
            Xgirder2 = new MemberCollection();
            Xgirder3 = new MemberCollection();
            Stringers1 = new MemberCollection();
            Stringers2 = new MemberCollection();
            Bottom_Chord1 = new MemberCollection();
            Bottom_Chord2 = new MemberCollection();
            Bottom_Chord_Bracing = new MemberCollection();
            END_BCB = new MemberCollection();
            //Top_Chord1 = new MemberCollection();
            //Top_Chord2 = new MemberCollection();
            Transverse_Members = new MemberCollection();
            Top_Chord_Bracing_Diagonal = new MemberCollection();
            //Diagonal1 = new MemberCollection();
            //Diagonal2 = new MemberCollection();
            //Vertical1 = new MemberCollection();
            //Vertical2 = new MemberCollection();
            //EndRakers = new MemberCollection();




            LeftArches = new MemberCollection();
            RightArches = new MemberCollection();




            Left_Inner_Strands = new MemberCollection();
            Right_Inner_Strands = new MemberCollection();
            Left_Outer_Strands = new MemberCollection();
            Right_Outer_Strands = new MemberCollection();
            #endregion Variable Declarations
        }
        #region Chiranjit [2013 11 18] Members Declarations
        Member mbr = new Member();
        public MemberCollection Cross_Girders { get; set; }


        public MemberCollection Xgirder1 { get; set; }
        public MemberCollection Xgirder2 { get; set; }
        public MemberCollection Xgirder3 { get; set; }

        public MemberCollection Stringers1 { get; set; }

        public MemberCollection Stringers2 { get; set; }

        public MemberCollection Bottom_Chord1 { get; set; }
        public MemberCollection Bottom_Chord2 { get; set; }
        public MemberCollection Bottom_Chord_Bracing { get; set; }
        public MemberCollection END_BCB { get; set; }
        //public MemberCollection Top_Chord1 { get; set; }
        //public MemberCollection Top_Chord2 { get; set; }
        public MemberCollection Transverse_Members { get; set; }
        public MemberCollection Top_Chord_Bracing_Diagonal { get; set; }

        public MemberCollection Left_Inner_Strands { get; set; }
        public MemberCollection Right_Inner_Strands { get; set; }
        public MemberCollection Left_Outer_Strands { get; set; }
        public MemberCollection Right_Outer_Strands { get; set; }


        public MemberCollection LeftArches { get; set; }
        public MemberCollection RightArches { get; set; }


        public string Joint_Load_Support { get; set; }
        public string Joint_Load_Edge { get; set; }


        //public MemberCollection Vertical_Top1 { get; set; }
        //public MemberCollection Vertical_Bottom1  { get; set; }
        //public MemberCollection Diagonal_Top1  { get; set; }
        //public MemberCollection Diagonal_Bottom1 { get; set; }



        //public MemberCollection Vertical_Top2 { get; set; }
        //public MemberCollection Vertical_Bottom2  { get; set; }
        //public MemberCollection Diagonal_Top2  { get; set; }
        //public MemberCollection Diagonal_Bottom2  { get; set; }




        #endregion Chiranjit [2013 11 18] Members Declarations
        public double StringerSpacing
        {
            get
            {
                //return (Breadth / (NoOfStringerBeam + 1));
                //return ((Breadth - ((Footpath_Width + 0.25) * 2.0)) / (NoOfStringerBeam - 1));
                //return (((Breadth - Footpath_Width) - ((Footpath_Width + 0.25) * 2.0)) / (NoOfStringerBeam - 1));
                return (_cw / (_NS - 1));
            }
        }
        public double Height { get; set; }
        //Default 10
        public int NoOfPanel { get; set; }

        public bool CreateData(string fileName)
        {
            try
            {
                //NoOfStringerBeam = 4;
                //NoOfPanel = 6;

                List<string> list = new List<string>();
                list.Add("ASTRA SPACE ARCH CABLE SUSPENSION BRIDGE");
                list.Add("UNIT KN MET");

                list.AddRange(Get_All_Bridge_Data());

                //list.AddRange(Get_Bridge_Data_without_Inner_Outer_cable());
                //list.AddRange(Get_Bridge_Data_without_Left_Outer_cable());
                //list.AddRange(Get_Bridge_Data_without_Left_Inner_cable());
                //list.AddRange(Get_Bridge_Data_without_Right_Inner_cable());
                //list.AddRange(Get_Bridge_Data_without_Right_Outer_cable());

                
                File.WriteAllLines(fileName, list.ToArray());

                Path.GetDirectoryName(fileName);
                return true;
            }
            catch (Exception ex) { }
            return false;
        }

        public bool CreateData(string fileName, int index)
        {
            try
            {
                //NoOfStringerBeam = 4;
                //NoOfPanel = 6;

                List<string> list = new List<string>();
                list.Add("ASTRA SPACE ARCH CABLE SUSPENSION BRIDGE");
                list.Add("UNIT KN MET");
                Initialize();
                if (index == 0)
                    list.AddRange(Get_All_Bridge_Data());
                else if (index == 1)
                    list.AddRange(Get_Bridge_Data_Removing_One_Side_Inner_Cables());
                else if (index == 2)
                    list.AddRange(Get_Bridge_Data_Removing_One_Side_Outer_Cables());
                else if (index == 3)
                    list.AddRange(Get_Bridge_Data_Removing_Both_Side_Inner_Cables());
                else if (index == 4)
                    list.AddRange(Get_Bridge_Data_Removing_Both_Side_Outer_Cables());


                //else if (index == 1)
                //    list.AddRange(Get_Bridge_Data_Removing_Left_Inner_Cables());
                //else if (index == 2)
                //    list.AddRange(Get_Bridge_Data_Removing_Left_Outer_Cables());
                //else if (index == 3)
                //    list.AddRange(Get_Bridge_Data_Removing_Both_Inner_Cables());
                //else if (index == 4)
                //    list.AddRange(Get_Bridge_Data_Removing_Both_Outer_Cables());

                //list.AddRange(Get_Bridge_Data_without_Inner_Outer_cable());
                //list.AddRange(Get_Bridge_Data_without_Left_Outer_cable());
                //list.AddRange(Get_Bridge_Data_without_Left_Inner_cable());
                //list.AddRange(Get_Bridge_Data_without_Right_Inner_cable());
                //list.AddRange(Get_Bridge_Data_without_Right_Outer_cable());


                File.WriteAllLines(fileName, list.ToArray());

                Path.GetDirectoryName(fileName);
                return true;
            }
            catch (Exception ex) { }
            return false;
        }

        public List<string> GetMemberConnectivityAndOthers()
        {
            List<string> list = new List<string>();
            string kStr = "";
            //Chiranjit [2011 10 21] Set new Data (Myntdu Bridge)
            #region Myntdu Bridge Data
            //list.Add("ASTRA SPACE TRUSS");
            //list.Add("UNIT METER KNS");
            //list.Add("JOINT COORDINATES");
            //list.Add("1                0.000                0.000                8.430");
            //list.Add("2                6.000                0.000                8.430");
            //list.Add("3                12.000                0.000                8.430");
            //list.Add("4                18.000                0.000                8.430");
            //list.Add("5                24.000                0.000                8.430");
            //list.Add("6                30.000                0.000                8.430");
            //list.Add("7                36.000                0.000                8.430");
            //list.Add("8                42.000                0.000                8.430");
            //list.Add("9                48.000                0.000                8.430");
            //list.Add("10                54.000                0.000                8.430");
            //list.Add("11                60.000                0.000                8.430");
            //list.Add("12                0.000                0.000                0.000");
            //list.Add("13                6.000                0.000                0.000");
            //list.Add("14                12.000                0.000                0.000");
            //list.Add("15                18.000                0.000                0.000");
            //list.Add("16                24.000                0.000                0.000");
            //list.Add("17                30.000                0.000                0.000");
            //list.Add("18                36.000                0.000                0.000");
            //list.Add("19                42.000                0.000                0.000");
            //list.Add("20                48.000                0.000                0.000");
            //list.Add("21                54.000                0.000                0.000");
            //list.Add("22                60.000                0.000                0.000");
            //list.Add("23                6.000                6.350                8.430");
            //list.Add("24                12.000                6.350                8.430");
            //list.Add("25                18.000                6.350                8.430");
            //list.Add("26                24.000                6.350                8.430");
            //list.Add("27                30.000                6.350                8.430");
            //list.Add("28                36.000                6.350                8.430");
            //list.Add("29                42.000                6.350                8.430");
            //list.Add("30                48.000                6.350                8.430");
            //list.Add("31                54.000                6.350                8.430");
            //list.Add("32                6.000                6.350                0.000");
            //list.Add("33                12.000                6.350                0.000");
            //list.Add("34                18.000                6.350                0.000");
            //list.Add("35                24.000                6.350                0.000");
            //list.Add("36                30.000                6.350                0.000");
            //list.Add("37                36.000                6.350                0.000");
            //list.Add("38                42.000                6.350                0.000");
            //list.Add("39                48.000                6.350                0.000");
            //list.Add("40                54.000                6.350                0.000");
            //list.Add("41                0.000                0.000                6.990");
            //list.Add("42                6.000                0.000                6.990");
            //list.Add("43                12.000                0.000                6.990");
            //list.Add("44                18.000                0.000                6.990");
            //list.Add("45                24.000                0.000                6.990");
            //list.Add("46                30.000                0.000                6.990");
            //list.Add("47                36.000                0.000                6.990");
            //list.Add("48                42.000                0.000                6.990");
            //list.Add("49                48.000                0.000                6.990");
            //list.Add("50                54.000                0.000                6.990");
            //list.Add("51                60.000                0.000                6.990");
            //list.Add("52                0.000                0.000                5.140");
            //list.Add("53                6.000                0.000                5.140");
            //list.Add("54                12.000                0.000                5.140");
            //list.Add("55                18.000                0.000                5.140");
            //list.Add("56                24.000                0.000                5.140");
            //list.Add("57                30.000                0.000                5.140");
            //list.Add("58                36.000                0.000                5.140");
            //list.Add("59                42.000                0.000                5.140");
            //list.Add("60                48.000                0.000                5.140");
            //list.Add("61                54.000                0.000                5.140");
            //list.Add("62                60.000                0.000                5.140");
            //list.Add("63                0.000                0.000                3.290");
            //list.Add("64                6.000                0.000                3.290");
            //list.Add("65                12.000                0.000                3.290");
            //list.Add("66                18.000                0.000                3.290");
            //list.Add("67                24.000                0.000                3.290");
            //list.Add("68                30.000                0.000                3.290");
            //list.Add("69                36.000                0.000                3.290");
            //list.Add("70                42.000                0.000                3.290");
            //list.Add("71                48.000                0.000                3.290");
            //list.Add("72                54.000                0.000                3.290");
            //list.Add("73                60.000                0.000                3.290");
            //list.Add("74                0.000                0.000                1.440");
            //list.Add("75                6.000                0.000                1.440");
            //list.Add("76                12.000                0.000                1.440");
            //list.Add("77                18.000                0.000                1.440");
            //list.Add("78                24.000                0.000                1.440");
            //list.Add("79                30.000                0.000                1.440");
            //list.Add("80                36.000                0.000                1.440");
            //list.Add("81                42.000                0.000                1.440");
            //list.Add("82                48.000                0.000                1.440");
            //list.Add("83                54.000                0.000                1.440");
            //list.Add("84                60.000                0.000                1.440");
            //list.Add("MEMBER CONNECTIVITYS");
            list.Add("MEMBER INCIDENCES");
            list.Add("1                1                2");
            list.Add("2                2                3");
            list.Add("3                3                4");
            list.Add("4                4                5");
            list.Add("5                5                6");
            list.Add("6                6                7");
            list.Add("7                7                8");
            list.Add("8                8                9");
            list.Add("9                9                10");
            list.Add("10                10                11");
            list.Add("11                12                13");
            list.Add("12                13                14");
            list.Add("13                14                15");
            list.Add("14                15                16");
            list.Add("15                16                17");
            list.Add("16                17                18");
            list.Add("17                18                19");
            list.Add("18                19                20");
            list.Add("19                20                21");
            list.Add("20                21                22");
            list.Add("21                2                23");
            list.Add("22                3                24");
            list.Add("23                4                25");
            list.Add("24                5                26");
            list.Add("25                6                27");
            list.Add("26                7                28");
            list.Add("27                8                29");
            list.Add("28                9                30");
            list.Add("29                10                31");
            list.Add("30                13                32");
            list.Add("31                14                33");
            list.Add("32                15                34");
            list.Add("33                16                35");
            list.Add("34                17                36");
            list.Add("35                18                37");
            list.Add("36                19                38");
            list.Add("37                20                39");
            list.Add("38                21                40");
            list.Add("39                1                23");
            list.Add("40                23                3");
            list.Add("41                24                4");
            list.Add("42                25                5");
            list.Add("43                31                11");
            list.Add("44                31                9");
            list.Add("45                30                8");
            list.Add("46                29                7");
            list.Add("47                28                6");
            list.Add("48                26                6");
            list.Add("49                40                22");
            list.Add("50                40                20");
            list.Add("51                39                19");
            list.Add("52                38                18");
            list.Add("53                12                32");
            list.Add("54                32                14");
            list.Add("55                33                15");
            list.Add("56                34                16");
            list.Add("57                35                17");
            list.Add("58                37                17");
            list.Add("59                23                24");
            list.Add("60                24                25");
            list.Add("61                25                26");
            list.Add("62                26                27");
            list.Add("63                27                28");
            list.Add("64                28                29");
            list.Add("65                29                30");
            list.Add("66                30                31");
            list.Add("67                32                33");
            list.Add("68                33                34");
            list.Add("69                34                35");
            list.Add("70                35                36");
            list.Add("71                36                37");
            list.Add("72                37                38");
            list.Add("73                38                39");
            list.Add("74                39                40");
            list.Add("75                41                42");
            list.Add("76                42                43");
            list.Add("77                43                44");
            list.Add("78                44                45");
            list.Add("79                45                46");
            list.Add("80                46                47");
            list.Add("81                47                48");
            list.Add("82                48                49");
            list.Add("83                49                50");
            list.Add("84                50                51");
            list.Add("85                52                53");
            list.Add("86                53                54");
            list.Add("87                54                55");
            list.Add("88                55                56");
            list.Add("89                56                57");
            list.Add("90                57                58");
            list.Add("91                58                59");
            list.Add("92                59                60");
            list.Add("93                60                61");
            list.Add("94                61                62");
            list.Add("95                63                64");
            list.Add("96                64                65");
            list.Add("97                65                66");
            list.Add("98                66                67");
            list.Add("99                67                68");
            list.Add("100                68                69");
            list.Add("101                69                70");
            list.Add("102                70                71");
            list.Add("103                71                72");
            list.Add("104                72                73");
            list.Add("105                74                75");
            list.Add("106                75                76");
            list.Add("107                76                77");
            list.Add("108                77                78");
            list.Add("109                78                79");
            list.Add("110                79                80");
            list.Add("111                80                81");
            list.Add("112                81                82");
            list.Add("113                82                83");
            list.Add("114                83                84");
            list.Add("115                12                74");
            list.Add("116                74                63");
            list.Add("117                63                52");
            list.Add("118                52                41");
            list.Add("119                41                1");
            list.Add("120                13                75");
            list.Add("121                75                64");
            list.Add("122                64                53");
            list.Add("123                53                42");
            list.Add("124                42                2");
            list.Add("125                14                76");
            list.Add("126                76                65");
            list.Add("127                65                54");
            list.Add("128                54                43");
            list.Add("129                43                3");
            list.Add("130                15                77");
            list.Add("131                77                66");
            list.Add("132                66                55");
            list.Add("133                55                44");
            list.Add("134                44                4");
            list.Add("135                16                78");
            list.Add("136                78                67");
            list.Add("137                67                56");
            list.Add("138                56                45");
            list.Add("139                45                5");
            list.Add("140                17                79");
            list.Add("141                79                68");
            list.Add("142                68                57");
            list.Add("143                57                46");
            list.Add("144                46                6");
            list.Add("145                18                80");
            list.Add("146                80                69");
            list.Add("147                69                58");
            list.Add("148                58                47");
            list.Add("149                47                7");
            list.Add("150                19                81");
            list.Add("151                81                70");
            list.Add("152                70                59");
            list.Add("153                59                48");
            list.Add("154                48                8");
            list.Add("155                20                82");
            list.Add("156                82                71");
            list.Add("157                71                60");
            list.Add("158                60                49");
            list.Add("159                49                9");
            list.Add("160                21                83");
            list.Add("161                83                72");
            list.Add("162                72                61");
            list.Add("163                61                50");
            list.Add("164                50                10");
            list.Add("165                22                84");
            list.Add("166                84                73");
            list.Add("167                73                62");
            list.Add("168                62                51");
            list.Add("169                51                11");
            list.Add("170                32                23");
            list.Add("171                33                24");
            list.Add("172                34                25");
            list.Add("173                35                26");
            list.Add("174                36                27");
            list.Add("175                37                28");
            list.Add("176                38                29");
            list.Add("177                39                30");
            list.Add("178                40                31");
            list.Add("179                32                24");
            list.Add("180                33                25");
            list.Add("181                34                26");
            list.Add("182                35                27");
            list.Add("183                36                28");
            list.Add("184                37                29");
            list.Add("185                38                30");
            list.Add("186                39                31");
            list.Add("187                33                23");
            list.Add("188                34                24");
            list.Add("189                35                25");
            list.Add("190                36                26");
            list.Add("191                37                27");
            list.Add("192                38                28");
            list.Add("193                39                29");
            list.Add("194                40                30");
            list.Add("195                12                2");
            list.Add("196                2                14");
            list.Add("197                14                4");
            list.Add("198                4                16");
            list.Add("199                16                6");
            list.Add("200                6                18");
            list.Add("201                18                8");
            list.Add("202                8                20");
            list.Add("203                20                10");
            list.Add("204                10                22");
            list.Add("205                1                13");
            list.Add("206                13                3");
            list.Add("207                3                15");
            list.Add("208                15                5");
            list.Add("209                5                17");
            list.Add("210                17                7");
            list.Add("211                7                19");
            list.Add("212                19                9");
            list.Add("213                9                21");
            list.Add("214                21                11");
            list.Add("START GROUP DEFINITION");
            list.Add("_L0L1                1                10                11                20");
            list.Add("_L1L2                2                9                12                19");
            list.Add("_L2L3                3                8                13                18");
            list.Add("_L3L4                4                7                14                17");
            list.Add("_L4L5                5                6                15                16");
            list.Add("_U1U2                59                66                67                74");
            list.Add("_U2U3                60                65                68                73");
            list.Add("_U3U4                61                64                69                72");
            list.Add("_U4U5                62                63                70                71");
            list.Add("_L1U1                21                29                30                38");
            list.Add("_L2U2                22                28                31                37");
            list.Add("_L3U3                23                27                32                36");
            list.Add("_L4U4                24                26                33                35");
            list.Add("_L5U5                25                34");
            list.Add("_ER                39                43                49                53");
            list.Add("_L2U1                40                44                50                54");
            list.Add("_L3U2                41                45                51                55");
            list.Add("_L4U3                42                46                52                56");
            list.Add("_L5U4                47                48                57                58");
            list.Add("_TCS_ST                170                TO                178                ");
            list.Add("_TCS_DIA                179                TO                194                ");
            list.Add("_BCB                195                TO                214                ");
            list.Add("_STRINGER                75                TO                114                ");
            list.Add("_XGIRDER                115                TO                169                ");
            list.Add("END");
            list.Add("MEMBER PROPERTY INDIAN");
            list.Add("_L0L1                PRI                AX                0.024                IX                0.00001                IY                0.000741                IZ                0.001");
            list.Add("_L1L2                PRI                AX                0.024                IX                0.00001                IY                0.000741                IZ                0.001");
            list.Add("_L2L3                PRI                AX                0.030                IX                0.00001                IY                0.000946                IZ                0.001");
            list.Add("_L3L4                PRI                AX                0.039                IX                0.00001                IY                0.001                IZ                0.002");
            list.Add("_L4L5                PRI                AX                0.042                IX                0.00001                IY                0.001                IZ                0.001");
            list.Add("_U1U2                PRI                AX                0.027                IX                0.00001                IY                0.000807                IZ                0.000693");
            list.Add("_U2U3                PRI                AX                0.033                IX                0.00001                IY                0.000864                IZ                0.000922");
            list.Add("_U3U4                PRI                AX                0.036                IX                0.00001                IY                0.000896                IZ                0.001");
            list.Add("_U4U5                PRI                AX                0.038                IX                0.00001                IY                0.000914                IZ                0.001");
            list.Add("_L1U1                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            list.Add("_L2U2                PRI                AX                0.013                IX                0.00001                IY                0.000399                IZ                0.000302");
            list.Add("_L3U3                PRI                AX                0.009                IX                0.00001                IY                0.00029                IZ                0.000127");
            list.Add("_L4U4                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            list.Add("_L5U5                PRI                AX                0.004                IX                0.00001                IY                0.000134                IZ                0.000016");
            list.Add("_ER                PRI                AX                0.022                IX                0.00001                IY                0.000666                IZ                0.000579");
            list.Add("_L2U1                PRI                AX                0.019                IX                0.00001                IY                0.000621                IZ                0.000356");
            list.Add("_L3U2                PRI                AX                0.014                IX                0.00001                IY                0.000474                IZ                0.000149");
            list.Add("_L4U3                PRI                AX                0.009                IX                0.00001                IY                0.00029                IZ                0.000127");
            list.Add("_L5U4                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            list.Add("_TCS_ST                PRI                AX                0.006                IX                0.00001                IY                0.000027                IZ                0.000225");
            list.Add("_TCS_DIA                PRI                AX                0.006                IX                0.00001                IY                0.000027                IZ                0.000225");
            list.Add("_BCB                PRI                AX                0.002                IX                0.00001                IY                0.000001                IZ                0.000001");
            list.Add("_STRINGER                PRI                AX                0.048                IX                0.00001                IY                0.008                IZ                0.002");
            list.Add("_XGIRDER                PRI                AX                0.059                IX                0.00001                IY                0.009                IZ                0.005");
            list.Add("MEMBER TRUSS");
            list.Add("_L0L1");
            list.Add("MEMBER TRUSS");
            list.Add("_L1L2");
            list.Add("MEMBER TRUSS");
            list.Add("_L2L3");
            list.Add("MEMBER TRUSS");
            list.Add("_L3L4");
            list.Add("MEMBER TRUSS");
            list.Add("_L4L5");
            list.Add("MEMBER TRUSS");
            list.Add("_U1U2");
            list.Add("MEMBER TRUSS");
            list.Add("_U2U3");
            list.Add("MEMBER TRUSS");
            list.Add("_U3U4");
            list.Add("MEMBER TRUSS");
            list.Add("_U4U5");
            list.Add("MEMBER TRUSS");
            list.Add("_L1U1");
            list.Add("MEMBER TRUSS");
            list.Add("_L2U2");
            list.Add("MEMBER TRUSS");
            list.Add("_L3U3");
            list.Add("MEMBER TRUSS");
            list.Add("_L4U4");
            list.Add("MEMBER TRUSS");
            list.Add("_L5U5");
            list.Add("MEMBER TRUSS");
            list.Add("_ER");
            list.Add("MEMBER TRUSS");
            list.Add("_L2U1");
            list.Add("MEMBER TRUSS");
            list.Add("_L3U2");
            list.Add("MEMBER TRUSS");
            list.Add("_L4U3");
            list.Add("MEMBER TRUSS");
            list.Add("_L5U4");
            list.Add("MEMBER TRUSS");
            list.Add("_TCS_ST");
            list.Add("MEMBER TRUSS");
            list.Add("_TCS_DIA");
            list.Add("MEMBER TRUSS");
            list.Add("_BCB");
            list.Add("CONSTANT");
            list.Add("E  2.110E8                ALL                ");
            list.Add("DENSITY STEEL ALL");
            list.Add("POISSON STEEL ALL");
            list.Add("SUPPORT");
            list.Add("1 12 FIXED BUT MZ");
            list.Add("11 22 FIXED BUT EX MZ");
            list.Add("LOAD 1 WIND");
            list.Add("JOINT LOAD");
            list.Add("2 TO 10 23 TO 31 FZ -26.7");
            list.Add("PERFORM ANALYSIS");
            list.Add("FINISH");
            return list;
            #endregion Myntdu Bridge Data

            #region Comment

            #region Chiranjit [2011 10 21] Close this data




            //string format = "{0,-10} {1,10} {2,10} {3,10}";
            //int i = 0;
            //double x, y, z;

            //x = 0.0;
            //y = 0.0;
            //z = 0.0;
            //int counter = 0;
            //list.Add("MEMBER INCIDENCES");
            //list.Add("1                1                2                ");
            //list.Add("2                2                3                ");
            //list.Add("3                3                4                ");
            //list.Add("4                4                5                ");
            //list.Add("5                5                6                ");
            //list.Add("6                6                7                ");
            //list.Add("7                7                8");
            //list.Add("8                8                9");
            //list.Add("9                9                10");
            //list.Add("10                10                11");
            //list.Add("11                12                13");
            //list.Add("12                13                14");
            //list.Add("13                14                15");
            //list.Add("14                15                16");
            //list.Add("15                16                17");
            //list.Add("16                17                18");
            //list.Add("17                18                19");
            //list.Add("18                19                20");
            //list.Add("19                20                21");
            //list.Add("20                21                22");
            //list.Add("21                2                23");
            //list.Add("22                3                24");
            //list.Add("23                4                25");
            //list.Add("24                5                26");
            //list.Add("25                6                27");
            //list.Add("26                7                28");
            //list.Add("27                8                29");
            //list.Add("28                9                30");
            //list.Add("29                10                31");
            //list.Add("30                13                32");
            //list.Add("31                14                33");
            //list.Add("32                15                34");
            //list.Add("33                16                35");
            //list.Add("34                17                36");
            //list.Add("35                18                37");
            //list.Add("36                19                38");
            //list.Add("37                20                39");
            //list.Add("38                21                40");
            //list.Add("39                1                23");
            //list.Add("40                23                3");
            //list.Add("41                24                4");
            //list.Add("42                25                5");
            //list.Add("43                31                11");
            //list.Add("44                31                9");
            //list.Add("45                30                8");
            //list.Add("46                29                7");
            //list.Add("47                28                6");
            //list.Add("48                26                6");
            //list.Add("49                40                22");
            //list.Add("50                40                20");
            //list.Add("51                39                19");
            //list.Add("52                38                18");
            //list.Add("53                12                32");
            //list.Add("54                32                14");
            //list.Add("55                33                15");
            //list.Add("56                34                16");
            //list.Add("57                35                17");
            //list.Add("58                37                17");
            //list.Add("59                23                24");
            //list.Add("60                24                25");
            //list.Add("61                25                26");
            //list.Add("62                26                27");
            //list.Add("63                27                28");
            //list.Add("64                28                29");
            //list.Add("65                29                30");
            //list.Add("66                30                31");
            //list.Add("67                32                33");
            //list.Add("68                33                34");
            //list.Add("69                34                35");
            //list.Add("70                35                36");
            //list.Add("71                36                37");
            //list.Add("72                37                38");
            //list.Add("73                38                39");
            //list.Add("74                39                40");
            //list.Add("75                41                42");
            //list.Add("76                42                43");
            //list.Add("77                43                44");
            //list.Add("78                44                45");
            //list.Add("79                45                46");
            //list.Add("80                46                47");
            //list.Add("81                47                48");
            //list.Add("82                48                49");
            //list.Add("83                49                50");
            //list.Add("84                50                51");
            //list.Add("85                52                53");
            //list.Add("86                53                54");
            //list.Add("87                54                55");
            //list.Add("88                55                56");
            //list.Add("89                56                57");
            //list.Add("90                57                58");
            //list.Add("91                58                59");
            //list.Add("92                59                60");
            //list.Add("93                60                61");
            //list.Add("94                61                62");
            //list.Add("95                63                64");
            //list.Add("96                64                65");
            //list.Add("97                65                66");
            //list.Add("98                66                67");
            //list.Add("99                67                68");
            //list.Add("100                68                69");
            //list.Add("101                69                70");
            //list.Add("102                70                71");
            //list.Add("103                71                72");
            //list.Add("104                72                73");
            //list.Add("105                12                63");
            //list.Add("106                63                52");
            //list.Add("107                52                41");
            //list.Add("108                41                1");
            //list.Add("109                13                64");
            //list.Add("110                64                53");
            //list.Add("111                53                42");
            //list.Add("112                42                2");
            //list.Add("113                14                65");
            //list.Add("114                65                54");
            //list.Add("115                54                43");
            //list.Add("116                43                3");
            //list.Add("117                15                66");
            //list.Add("118                66                55");
            //list.Add("119                55                44");
            //list.Add("120                44                4");
            //list.Add("121                16                67");
            //list.Add("122                67                56");
            //list.Add("123                56                45");
            //list.Add("124                45                5");
            //list.Add("125                17                68");
            //list.Add("126                68                57");
            //list.Add("127                57                46");
            //list.Add("128                46                6");
            //list.Add("129                18                69");
            //list.Add("130                69                58");
            //list.Add("131                58                47");
            //list.Add("132                47                7");
            //list.Add("133                19                70");
            //list.Add("134                70                59");
            //list.Add("135                59                48");
            //list.Add("136                48                8");
            //list.Add("137                20                71");
            //list.Add("138                71                60");
            //list.Add("139                60                49");
            //list.Add("140                49                9");
            //list.Add("141                21                72");
            //list.Add("142                72                61");
            //list.Add("143                61                50");
            //list.Add("144                50                10");
            //list.Add("145                22                73");
            //list.Add("146                73                62");
            //list.Add("147                62                51");
            //list.Add("148                51                11");
            //list.Add("149                32                23");
            //list.Add("150                33                24");
            //list.Add("151                34                25");
            //list.Add("152                35                26");
            //list.Add("153                36                27");
            //list.Add("154                37                28");
            //list.Add("155                38                29");
            //list.Add("156                39                30");
            //list.Add("157                40                31");
            //list.Add("158                32                24");
            //list.Add("159                33                25");
            //list.Add("160                34                26");
            //list.Add("161                35                27");
            //list.Add("162                36                28");
            //list.Add("163                37                29");
            //list.Add("164                38                30");
            //list.Add("165                39                31");
            //list.Add("166                33                23");
            //list.Add("167                34                24");
            //list.Add("168                35                25");
            //list.Add("169                36                26");
            //list.Add("170                37                27");
            //list.Add("171                38                28");
            //list.Add("172                39                29");
            //list.Add("173                40                30");
            //list.Add("174                12                2");
            //list.Add("175                2                14");
            //list.Add("176                14                4");
            //list.Add("177                4                16");
            //list.Add("178                16                6");
            //list.Add("179                6                18");
            //list.Add("180                18                8");
            //list.Add("181                8                20");
            //list.Add("182                20                10");
            //list.Add("183                10                22");
            //list.Add("184                1                13");
            //list.Add("185                13                3");
            //list.Add("186                3                15");
            //list.Add("187                15                5");
            //list.Add("188                5                17");
            //list.Add("189                17                7");
            //list.Add("190                7                19");
            //list.Add("191                19                9");
            //list.Add("192                9                21");
            //list.Add("193                21                11                ");
            //list.Add("START                GROUP                DEFINITION");
            //list.Add("_L0L1                1                10                11                20");
            //list.Add("_L1L2                2                9                12                19");
            //list.Add("_L2L3                3                8                13                18");
            //list.Add("_L3L4                4                7                14                17");
            //list.Add("_L4L5                5                6                15                16");
            //list.Add("_U1U2                59                66                67                74");
            //list.Add("_U2U3                60                65                68                73");
            //list.Add("_U3U4                61                64                69                72");
            //list.Add("_U4U5                62                63                70                71");
            //list.Add("_L1U1                21                29                30                38");
            //list.Add("_L2U2                22                28                31                37");
            //list.Add("_L3U3                23                27                32                36");
            //list.Add("_L4U4                24                26                33                35");
            //list.Add("_L5U5                25                34");
            //list.Add("_ER                39                43                49                53");
            //list.Add("_L2U1                40                44                50                54");
            //list.Add("_L3U2                41                45                51                55");
            //list.Add("_L4U3                42                46                52                56");
            //list.Add("_L5U4                47                48                57                58");
            //list.Add("_TCS_ST                        149                TO                157                ");
            //list.Add("_TCS_DIA                158                TO                173                ");
            //list.Add("_BCB1174                TO                179");
            //list.Add("_BCB2180                TO                193                ");
            //list.Add("_STRINGER                75                TO                104                ");
            //list.Add("_XGIRDER                105                TO                148");
            //list.Add("END");
            //list.Add("MEMBER                PROPERTY                INDIAN");
            //list.Add("_L0L1                PRI                AX                0.024                IX                0.00001                IY                0.000741                IZ                0.001");
            //list.Add("_L1L2                PRI                AX                0.03                IX                0.00001                IY                0.000741                IZ                0.001");
            //list.Add("_L2L3                PRI                AX                0.03                IX                0.00001                IY                0.000946                IZ                0.001");
            //list.Add("_L3L4                PRI                AX                0.039                IX                0.00001                IY                0.001                IZ                0.002");
            //list.Add("_L4L5                PRI                AX                0.042                IX                0.00001                IY                0.001                IZ                0.001");
            //list.Add("_U1U2                PRI                AX                0.027                IX                0.00001                IY                0.000807                IZ                0.000693");
            //list.Add("_U2U3                PRI                AX                0.033                IX                0.00001                IY                0.000864                IZ                0.000922");
            //list.Add("_U3U4                PRI                AX                0.036                IX                0.00001                IY                0.000896                IZ                0.001");
            //list.Add("_U4U5                PRI                AX                0.038                IX                0.00001                IY                0.000914                IZ                0.001");
            //list.Add("_L1U1                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            //list.Add("_L2U2                PRI                AX                0.013                IX                0.00001                IY                0.000399                IZ                0.000302");
            //list.Add("_L3U3                PRI                AX                0.009                IX                0.00001                IY                0.00029                IZ                0.000127");
            //list.Add("_L4U4                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            //list.Add("_L5U5                PRI                AX                0.004                IX                0.00001                IY                0.000134                IZ                0.000016");
            //list.Add("_ER                PRI                AX                0.022                IX                0.00001                IY                0.000666                IZ                0.000579");
            //list.Add("_L2U1                PRI                AX                0.019                IX                0.00001                IY                0.000621                IZ                0.0000356");
            //list.Add("_L3U2                PRI                AX                0.014                IX                0.00001                IY                0.000474                IZ                0.000149");
            //list.Add("_L4U3                PRI                AX                0.009                IX                0.00001                IY                0.00029                IZ                0.000127");
            //list.Add("_L5U4                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            //list.Add("_TCS_ST                PRI                AX                0.006                IX                0.00001                IY                0.000027                IZ                0.000225");
            //list.Add("_TCS_DIA                PRI                AX                0.006                IX                0.00001                IY                0.000027                IZ                0.000225");
            //list.Add("_BCB1                PRI                AX                0.002                IX                0.00001                IY                0.000001                IZ                0.000001");
            //list.Add("_BCB2                PRI                AX                0.002                IX                0.00001                IY                0.000001                IZ                0.000001");
            //list.Add("_STRINGER                PRI                AX                0.01048                IX                0.00001                IY                0.00001                IZ                0.000362");
            //list.Add("_XGIRDER                PRI                AX                0.02162                IX                0.00001                IY                0.000072                IZ                0.001335");
            //list.Add("MEMBER                TRUSS                ");
            //list.Add("_L0L1");
            //list.Add("MEMBER                TRUSS                ");
            //list.Add("_L1L2");
            //list.Add("MEMBER                TRUSS                ");
            //list.Add("_L2L3                ");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_L3L4                ");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_L4L5                ");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_U1U2                ");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_U2U3                ");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_U3U4                ");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_U4U5                ");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_L1U1");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_L2U2                ");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_L3U3                ");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_L4U4                ");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_L5U5                ");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_ER                ");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_L2U1                ");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_L3U2                ");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_L4U3                ");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_L5U4");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_TCS_ST");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_TCS_DIA");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_BCB1");
            //list.Add("MEMBER                TRUSS");
            //list.Add("_BCB2");
            //list.Add("CONSTANT");
            //list.Add("E                2.11E+08                ALL                ");
            //list.Add("DENSITY                STEEL                ALL                ");
            //list.Add("POISSON                STEEL                ALL                ");
            //list.Add("SUPPORT");
            //list.Add("1                12                FIXED                BUT                MZ");
            //list.Add("11                22                FIXED                BUT                FX                MZ");
            ////list.Add("1                12                PINNED");
            ////list.Add("11                22                FIXED");
            //list.Add("LOAD 1DL+SIDL                ");
            //list.Add("JOINT LOAD");
            //list.Add("2   TO  10  13   TO  21      FY  -125.534");
            //list.Add("1  11  12  22     FY  -62.767");
            //list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //list.Add("TYPE 1 CLA 1.179");
            //list.Add("TYPE 2 CLB 1.188");
            //list.Add("TYPE 3 A70RT 1.10");
            //list.Add("TYPE 4 CLAR 1.179");
            //list.Add("TYPE 5 A70RR 1.188");
            //list.Add("TYPE 6 IRC24RTRACK 1.188");
            //list.Add("TYPE 7 RAILBG 1.25");
            //list.Add("LOAD GENERATION 100");
            //list.Add("TYPE 2  -50.0 0 0.5 XINC 0.5");
            //list.Add("PRINT SUPPORT REACTIONS");
            //list.Add("PRINT MAX FORCE ENVELOPE LIST 59 TO 62");
            //list.Add("PRINT MAX FORCE ENVELOPE LIST 1 TO 5");
            //list.Add("PRINT MAX FORCE ENVELOPE LIST 21 TO 25");
            //list.Add("PRINT MAX FORCE ENVELOPE LIST 40 TO 42");
            //list.Add("ANALYSIS");
            //list.Add("FINISH");
            //return list;
            #endregion Chiranjit [2011 10 21] Close this data

            #endregion Comment
        }
        public List<string> GetJointCoordinates()
        {
            List<string> list = new List<string>();
            string kStr = "";

            string format = "{0,-10} {1,10:f3} {2,10:f3} {3,10:f3}";
            int i = 0;
            double x, y, z;

            x = 0.0;
            y = 0.0;
            z = 0.0;
            int counter = 0;
            list.Add("JOINT COORDINATE");
            //Length    0  -   50
            for (i = 0; i <= NoOfPanel; i++)
            {
                counter++;
                x = i * PanelLength;
                y = 0;
                z = Breadth;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }

            //Length    0  -   50
            for (i = 0; i <= NoOfPanel; i++)
            {
                counter++;
                x = i * PanelLength;
                y = 0;
                z = 0;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            //Length    5  -   45
            for (i = 1; i < NoOfPanel; i++)
            {
                counter++;
                x = i * PanelLength;
                y = Height;
                z = Breadth;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            //Length    5  -   45
            for (i = 1; i < NoOfPanel; i++)
            {
                counter++;
                x = i * PanelLength;
                y = Height;
                z = 0;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            double _stngr_spc = 1.025;
            //list.Add("*STRINGER BEAMS AT Z = " + (StringerSpacing * 3));
            list.Add("*STRINGER BEAMS AT Z = " + (Breadth - _stngr_spc));
            //Stringer Beams At Z = 
            for (i = 0; i <= NoOfPanel; i++)
            {
                counter++;
                x = i * PanelLength;
                y = 0;
                //z = StringerSpacing * 3;
                z = Breadth - _stngr_spc;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            //list.Add("*STRINGER BEAMS AT Z = " + (StringerSpacing * 2));
            list.Add("*STRINGER BEAMS AT Z = " + (Breadth / 2));
            for (i = 0; i <= NoOfPanel; i++)
            {
                counter++;
                x = i * PanelLength;
                y = 0;
                //z = StringerSpacing * 2;
                z = (Breadth / 2);
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }

            list.Add("*STRINGER BEAMS AT Z = " + StringerSpacing);
            for (i = 0; i <= NoOfPanel; i++)
            {
                counter++;
                x = i * PanelLength;
                y = 0;
                z = _stngr_spc;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            return list;
        }

        public double Arch_Length
        {
            get
            {
                return Arch_Radius * 2 * Arch_Angle;
            }
        }
        public double Arch_Radius
        {
            get
            {
                return (Height * Height + (Length / 2) * (Length / 2)) / (2 * Height);
            }
        }
        public double Arch_Angle
        {
            get
            {
                return Math.Asin(Length / 2 / Arch_Radius);
            }
        }
        public List<string> Get_All_Bridge_Data()
        {
            double x_inc = PanelLength;
            double z_inc = StringerSpacing;

            JointNode jn = null;

            int i = 0;
            int j = 0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            List<double> list_end_z = new List<double>();

            JointNodeCollection Joints = new JointNodeCollection();
            List<JointNodeCollection> Joints_Bottom = new List<JointNodeCollection>();
            List<JointNodeCollection> Joints_Top = new List<JointNodeCollection>();

            List<string> ASTRA_Data = new List<string>();
            Get_Coordinates(ref i, ref list_x, ref list_z, ref list_y, ref list_end_z);

            JointNodeCollection All_Joints = new JointNodeCollection();


            int x_count = 0;
            int z_count = 0;
            int mem_no = 1;
            int node_no = 1;
            x_count = 0;

            #region Calculate Bottom Coordinates
            //Calculate Bottom Coordinates
            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[0];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);


            Joints = new JointNodeCollection();
            for (j = 1; j < list_x.Count - 1; j++)
            {
                Joints = new JointNodeCollection();
                for (i = 0; i < list_z.Count; i++)
                {
                    jn = new JointNode();
                    jn.X = list_x[j];
                    jn.Y = 0;
                    jn.Z = list_z[i];
                    Joints.Add(jn);
                }
                Joints_Bottom.Add(Joints);
            }


            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[list_x.Count - 1];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);
            #endregion Calculate Bottom Coordinates

            #region Calculate Top Coordinates


            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[0];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);



            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[list_end_z.Count - 1];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);

            #endregion Calculate Top Coordinates

            #region Generate Node / Joint  Numbers

            node_no = 0;

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 0; j < Joints_Bottom[i].Count; j++)
                {
                    jn = Joints_Bottom[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }


            //i = 0;
            //for (j = 0; j < Joints_Bottom[i].Count; j++)
            //{
            //    jn = Joints_Bottom[i][j];

            //    jn.NodeNo = ++node_no;
            //    All_Joints.Add(jn);
            //}


            //for (j = 0; j < Joints_Bottom[1].Count; j++)
            //{
            //    for (i = 1; i < Joints_Bottom.Count - 1; i++)
            //    {
            //        jn = Joints_Bottom[i][j];

            //        jn.NodeNo = ++node_no;
            //        All_Joints.Add(jn);
            //    }
            //}

            //i = Joints_Bottom.Count - 1;
            //for (j = 0; j < Joints_Bottom[i].Count; j++)
            //{
            //    jn = Joints_Bottom[i][j];

            //    jn.NodeNo = ++node_no;
            //    All_Joints.Add(jn);
            //}


            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count; j++)
                {
                    jn = Joints_Top[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }
            #endregion Generate Node / Joint  Numbers

            #region Bottom Chord Members
            Bottom_Chord1.Clear();
            Bottom_Chord2.Clear();
            Member mbr = new Member();

            for (i = 1; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                if (i == 1)
                {
                    mbr.StartNode = Joints_Bottom[i - 1][0];
                    mbr.EndNode = Joints_Bottom[i][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i - 1][Joints_Bottom[i - 1].Count - 1];
                    mbr.EndNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else if (i == Joints_Bottom.Count - 1 - 1)
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
            }

            #endregion Bottom Chord Members

            #region Cross Girder Members
            Cross_Girders.Clear();
            Xgirder1.Clear();
            Xgirder2.Clear();
            Xgirder3.Clear();

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 1; j < Joints_Bottom[i].Count; j++)
                {
                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][j - 1];
                    mbr.EndNode = Joints_Bottom[i][j];

                    if (j == 1 && i == 0)
                        Xgirder1.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i == 0)
                        Xgirder1.Add(mbr);
                    else if (j == 1 && i == Joints_Bottom.Count - 1)
                        Xgirder1.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i == Joints_Bottom.Count - 1)
                        Xgirder1.Add(mbr);

                    else if (j == 1 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder2.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder2.Add(mbr);
                    else if (j == 2 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder3.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 2 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder3.Add(mbr);
                    else
                        Cross_Girders.Add(mbr);
                }

            }

            #endregion Cross Girder Members

            #region Stringer Members
            Stringers1.Clear();
            Stringers2.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                for (j = 1; j < Joints_Bottom[0].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        mbr.StartNode = Joints_Bottom[i][j];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                        Stringers1.Add(mbr);
                    }
                    else if (i == Joints_Bottom.Count - 1 - 1)
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j];
                        Stringers1.Add(mbr);

                    }
                    else
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                        Stringers2.Add(mbr);
                    }
                }

            }

            #endregion Stringer Members

            #region Arch Members
       
            LeftArches.Clear();
            RightArches.Clear();

            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            mbr.StartNode = Joints_Bottom[0][0];
                            mbr.EndNode = Joints_Top[i][0];
                            LeftArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                        else if (j == Joints_Top[0].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][0];
                            LeftArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            mbr = new Member();
                            mbr.StartNode = Joints_Bottom[0][Joints_Bottom[0].Count - 1];
                            mbr.EndNode = Joints_Top[i][0];
                            RightArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                        }
                        else if (j == Joints_Top[i].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][Joints_Bottom[0].Count - 1];
                            RightArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);
                        }
                    }
                }

            }

            #endregion Left Arch Members

            #region Inner & Outer Cables / Strands

            for (i = 0; i < Joints_Top[0].Count; i++)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][0];
                mbr.EndNode = Joints_Top[0][i];
                Left_Outer_Strands.Add(mbr);

                //Left Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][1];
                mbr.EndNode = Joints_Top[0][i];
                Left_Inner_Strands.Add(mbr);


                //Right Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Outer_Strands.Add(mbr);

                //Right Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1 - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Inner_Strands.Add(mbr);
            }

            #endregion Inner  & Outer Cables / Strands

            #region Bottom_Chord_Bracing
            Bottom_Chord_Bracing.Clear();
            END_BCB = new MemberCollection();
            if (Is_Add_BCB)
            {
                for (i = 0; i < Joints_Bottom.Count - 1; i++)
                {

                    if (i == 0 || i == Joints_Bottom.Count - 2)
                    {
                        mbr = new Member();
                        mbr.StartNode = Joints_Bottom[i][0];
                        mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                        END_BCB.Add(mbr);

                        mbr = new Member();
                        mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                        mbr.EndNode = Joints_Bottom[i + 1][0];
                        END_BCB.Add(mbr);
                    }
                    else
                    {
                        mbr = new Member();
                        mbr.StartNode = Joints_Bottom[i][0];
                        mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                        Bottom_Chord_Bracing.Add(mbr);

                        mbr = new Member();
                        mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                        mbr.EndNode = Joints_Bottom[i + 1][0];
                        Bottom_Chord_Bracing.Add(mbr);
                    }
                }
            }

            #endregion Bottom_Chord_Bracing

            #region Transverse Members
            Transverse_Members.Clear();

            for (i = 1; i < Joints_Top[0].Count; i += 2)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i];
                Transverse_Members.Add(mbr);
            }

            #endregion Transverse Members

            #region Top_Chord_Bracing_Diagonal
            Top_Chord_Bracing_Diagonal.Clear();

            for (i = 0; i < Joints_Top[0].Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Top[1][i];
                mbr.EndNode = Joints_Top[0][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);
            }
            Top_Chord_Bracing_Diagonal.Clear();

            #endregion Top_Chord_Bracing_Diagonal

            x_count = 0;

            #region Write Data
            ASTRA_Data.Clear();
            ASTRA_Data.Add("JOINT COORDINATE");
            foreach (var item in All_Joints)
            {
                ASTRA_Data.Add(item.ToString());
            }

            mem_no = 0;
            ASTRA_Data.Add("MEMBER INCIDENCES");
            foreach (var item in Bottom_Chord1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in END_BCB)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord_Bracing)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Top_Chord_Bracing_Diagonal)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Right_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Right_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in LeftArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in RightArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Transverse_Members)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Xgirder1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Xgirder2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Xgirder3)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Cross_Girders)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            #endregion Write Data

            #region START GROUP DEFINITION
            //ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));
            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 129 TO 140 153 TO 164"));
            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER 393 TO 397"));
            //ASTRA_Data.Add(string.Format("_OUTER_CABLE 212 214 216 218 220 222 224 226 228 230 232 235 237 239 241 243 245 247 249 251 253 255"));
            //ASTRA_Data.Add(string.Format("_INNER_CABLE 213 215 217 219 221 223 225 227 229 231 233 234 236 238 240 242 244 246 248 250 252 254"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));
            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));
            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            //ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));

            List<string> _groups = new List<string>();

            ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));

            //_groups.Add(kStr);

            string kStr = "";
            #region Bottom Chord

            for (i = 0; i < (NoOfPanel / 2); i++)
            {
                kStr = "_L" + i + "L" + (i + 1);
                _groups.Add(kStr);

                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Bottom_Chord1[i].MemberNo,
                    Bottom_Chord1[Bottom_Chord1.Count - (i + 1)].MemberNo,
                    Bottom_Chord2[i].MemberNo,
                    Bottom_Chord2[Bottom_Chord2.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }

            #endregion Bottom Chord

            #region Arch Segment

            for (i = 0; i < (LeftArches.Count / 2); i++)
            {
                //kStr = "_AC" + (i + 1) + "A" + (i + 2);
                kStr = "_AR" + (i + 1);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    LeftArches[i].MemberNo,
                    LeftArches[LeftArches.Count - (i + 1)].MemberNo,
                    RightArches[i].MemberNo,
                    RightArches[RightArches.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }
            #endregion Arch Segment

            #region Inner Strands

            for (i = 0; i < (Left_Inner_Strands.Count / 2) + 1; i++)
            {
                kStr = "_ICAB" + (i + 1);
                _groups.Add(kStr);

                if (i == ((Left_Inner_Strands.Count / 2)))
                {
                    kStr += string.Format("{0,10} {1,10}",
                        Left_Inner_Strands[i].MemberNo,
                        Right_Inner_Strands[i].MemberNo);
                }
                else
                {

                    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                        Left_Inner_Strands[i].MemberNo,
                        Left_Inner_Strands[Left_Inner_Strands.Count - (i + 1)].MemberNo,
                        Right_Inner_Strands[i].MemberNo,
                        Right_Inner_Strands[Right_Inner_Strands.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            #endregion Inner Strands

            #region Outer Strands

            for (i = 0; i < (Left_Outer_Strands.Count / 2) + 1; i++)
            {
                kStr = "_OCAB" + (i + 1);
                _groups.Add(kStr);

                if (i == ((Left_Inner_Strands.Count / 2)))
                {
                    kStr += string.Format("{0,10} {1,10}",
                        Left_Outer_Strands[i].MemberNo,
                        Right_Outer_Strands[i].MemberNo);

                }
                else
                {

                    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                        Left_Outer_Strands[i].MemberNo,
                        Left_Outer_Strands[Left_Outer_Strands.Count - (i + 1)].MemberNo,
                        Right_Outer_Strands[i].MemberNo,
                        Right_Outer_Strands[Right_Outer_Strands.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            #endregion Outer Strands

            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 {0} TO {1} {2} TO {3}",
            //    LeftArches[0].MemberNo, LeftArches[LeftArches.Count - 1].MemberNo,
            //    RightArches[0].MemberNo, RightArches[RightArches.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER {0} TO {1}", Top_Chord_Bracing[0].MemberNo, 
            //    Top_Chord_Bracing[Top_Chord_Bracing.Count - 1].MemberNo));

            _groups.Add("_TRANSVERSE");

            ASTRA_Data.Add(string.Format("_TRANSVERSE {0} TO {1}", Transverse_Members[0].MemberNo,
                Transverse_Members[Transverse_Members.Count - 1].MemberNo));


            //ASTRA_Data.Add(string.Format("_OUTER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Outer_Strands[0].MemberNo, Left_Outer_Strands[Left_Outer_Strands.Count - 1].MemberNo,
            //    Right_Outer_Strands[0].MemberNo, Right_Outer_Strands[Right_Outer_Strands.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INNER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Inner_Strands[0].MemberNo, Left_Inner_Strands[Left_Inner_Strands.Count - 1].MemberNo,
            //    Right_Inner_Strands[0].MemberNo, Right_Inner_Strands[Right_Inner_Strands.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_END");
            ASTRA_Data.Add(string.Format("_XGIRDERS_END {0} TO {1}", Xgirder1[0].MemberNo,
                Xgirder1[Xgirder1.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_EDGE");
            ASTRA_Data.Add(string.Format("_XGIRDERS_EDGE {0} TO {1}", Xgirder2[0].MemberNo,
                Xgirder2[Xgirder2.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_1");
            ASTRA_Data.Add(string.Format("_XGIRDERS_1 {0} TO {1}", Xgirder3[0].MemberNo,
                Xgirder3[Xgirder3.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_2");
            ASTRA_Data.Add(string.Format("_XGIRDERS_2 {0} TO {1}", Cross_Girders[0].MemberNo,
                Cross_Girders[Cross_Girders.Count - 1].MemberNo));



            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));


            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));


            _groups.Add("_STRINGER_1");
            ASTRA_Data.Add(string.Format("_STRINGER_1 {0} TO {1}", Stringers1[0].MemberNo,
                Stringers1[Stringers1.Count - 1].MemberNo));


            _groups.Add("_STRINGER_2");
            ASTRA_Data.Add(string.Format("_STRINGER_2 {0} TO {1}", Stringers2[0].MemberNo,
                Stringers2[Stringers2.Count - 1].MemberNo));




            if (Is_Add_BCB)
            {
                _groups.Add("_BCB_1");
                ASTRA_Data.Add(string.Format("_BCB_1 {0} TO {1}", END_BCB[0].MemberNo,
                    END_BCB[END_BCB.Count - 1].MemberNo));

                _groups.Add("_BCB_2");
                ASTRA_Data.Add(string.Format("_BCB_2 {0} TO {1}", Bottom_Chord_Bracing[0].MemberNo,
                    Bottom_Chord_Bracing[Bottom_Chord_Bracing.Count - 1].MemberNo));
            }
            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));
            #endregion END GROUP DEFINITION


            #region Member_Properties
            Hashtable hash_tab = new Hashtable();

            ASTRA_Data.Add("MEMBER PROPERTY");
            for (i = 0; i < _groups.Count; i++)
            {
                ASTRA_Data.Add(string.Format("{0,-10}  PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001", _groups[i], kStr));
            }
            //ASTRA_Data.Add("_BCB      PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001");
            //ASTRA_Data.Add("_STRINGER PRI  AX  0.048  IX  0.00001  IY  0.008000  IZ  0.002000");
            //ASTRA_Data.Add("_XGIRDER  PRI  AX  0.059  IX  0.00001  IY  0.009000  IZ  0.005000");
            for (i = 0; i < _groups.Count; i++)
            {
                if (_groups[i].StartsWith("_IC") ||
                    _groups[i].StartsWith("_OC"))
                {
                    ASTRA_Data.Add("MEMBER CABLE");
                    ASTRA_Data.Add(_groups[i]);
                }
                else if (_groups[i].StartsWith("_STR") ||
                    _groups[i].StartsWith("_XG") ||
                    _groups[i].StartsWith("_AR") ||
                    _groups[i].StartsWith("_TR"))
                {
                    //ASTRA_Data.Add("MEMBER CABLE");
                    //ASTRA_Data.Add(_groups[i]);
                }
                else
                {
                    ASTRA_Data.Add("MEMBER TRUSS");
                    ASTRA_Data.Add(_groups[i]);
                }
            }
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_ST");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_DIA");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_BCB");
            #endregion Member_Properties

            Get_Support_LOAD_FINISH(Joints_Bottom, ASTRA_Data);
            return ASTRA_Data;
        }

        private void Get_Coordinates(ref int i, ref List<double> list_x, ref List<double> list_z, ref List<double> list_y, ref List<double> list_end_z)
        {


            #region Define Arch Coordinates
            double R = 0, h = 0, L = 0, theta = 0;
            //double arch_len = 0.0;

            h = Height;
            L = Length;

            //R = (h * h + (L / 2) * (L / 2)) / (2 * h);
            R = Arch_Radius;


            //theta = Math.Asin(L / 2 / R);
            theta = Arch_Angle;

            //arch_len = R * 2 * theta;

            double l, th_l;

            l = L / NoOfPanel;
            th_l = theta / (NoOfPanel / 2);



            double d1 = R - h;

            double R1 = d1 / Math.Cos(th_l);
            double R2 = R - R1;

            double d2 = R2 * Math.Cos(th_l);


            for (i = 1; i <= NoOfPanel / 2; i++)
            {
                d1 = R - h;
                R1 = d1 / Math.Cos(th_l * i);
                R2 = R - R1;
                d2 = R2 * Math.Cos(th_l * i);
                list_y.Add(d2);
            }


            list_y.Reverse();

            list_y.Add(h);



            for (i = (NoOfPanel / 2) - 1; i >= 0; i--)
            {
                list_y.Add(list_y[i]);
            }


            #endregion Define Arch

            #region Calculate Coordinates

            list_x.Clear();
            list_x.Add(0);
            list_x.Add(_cgw);
            //list_x.Add(_L - _cgw);
            list_x.Add(_L);

            for (i = 1; i <= NoOfPanel - 2; i++)
            {
                list_x.Add(_cgw + PanelLength * i);
            }
            list_x.Sort();
            MyList.Array_Format_With(ref list_x, "f3");


            //double Fw = Footpath_Width + 0.25; //Footpath Width
            d1 = (_lfw + _hrw + (_rw / 2.0));
            double Fw = _hrw + _lfw + _rw - (d1 / 2.0); //Footpath Width

            //double W = Breadth - Footpath_Width;
            //double W = Breadth - (_rfw / 2.0 + _lfw / 2.0);
            double W = _aw;


            list_end_z.Clear();
            list_z.Clear();
            list_end_z.Add(0);
            //list_z.Add(Fw);
            //list_z.Add(Breadth - Fw);
            list_end_z.Add(W);

            
            for (i = 0; i < NoOfStringerBeam; i++)
            {
                d1 = Fw + StringerSpacing * i;
                if (!list_end_z.Contains(d1))
                {
                    list_end_z.Add(d1);
                    list_z.Add(d1);
                }
            }
            list_end_z.Sort();

            d1 = (_lfw + _hrw + (_rw / 2.0));

            list_z.Add(-d1 / 2.0);
            list_z.Add(d1 / 2.0);

            d1 = (_rfw + _hrw + (_rw / 2.0));



            list_z.Add(W - (d1 / 2.0));
            list_z.Add(W + (d1 / 2.0));

            list_z.Sort();

            MyList.Array_Format_With(ref list_end_z, "f3");
            MyList.Array_Format_With(ref list_z, "f3");
            MyList.Array_Format_With(ref list_x, "f3");
            MyList.Array_Format_With(ref list_y, "f3");
            #endregion Calculate Coordinates
        }

        public List<string> Get_Bridge_Data_Removing_One_Side_Inner_Cables()
        {
            double x_inc = PanelLength;
            double z_inc = StringerSpacing;

            JointNode jn = null;

            int i = 0;
            int j = 0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            List<double> list_end_z = new List<double>();

            JointNodeCollection Joints = new JointNodeCollection();
            List<JointNodeCollection> Joints_Bottom = new List<JointNodeCollection>();
            List<JointNodeCollection> Joints_Top = new List<JointNodeCollection>();

            List<string> ASTRA_Data = new List<string>();
            Get_Coordinates(ref i, ref list_x, ref list_z, ref list_y, ref list_end_z);

            JointNodeCollection All_Joints = new JointNodeCollection();


            int x_count = 0;
            int z_count = 0;
            int mem_no = 1;
            int node_no = 1;
            x_count = 0;

            #region Calculate Bottom Coordinates
            //Calculate Bottom Coordinates
            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[0];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);


            Joints = new JointNodeCollection();
            for (j = 1; j < list_x.Count - 1; j++)
            {
                Joints = new JointNodeCollection();
                for (i = 0; i < list_z.Count; i++)
                {
                    jn = new JointNode();
                    jn.X = list_x[j];
                    jn.Y = 0;
                    jn.Z = list_z[i];
                    Joints.Add(jn);
                }
                Joints_Bottom.Add(Joints);
            }


            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[list_x.Count - 1];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);
            #endregion Calculate Bottom Coordinates

            #region Calculate Top Coordinates


            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[0];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);



            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[list_end_z.Count - 1];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);

            #endregion Calculate Top Coordinates

            #region Generate Node / Joint  Numbers

            node_no = 0;

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 0; j < Joints_Bottom[i].Count; j++)
                {
                    jn = Joints_Bottom[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }

            //i = 0;
            //for (j = 0; j < Joints_Bottom[i].Count; j++)
            //{
            //    jn = Joints_Bottom[i][j];

            //    jn.NodeNo = ++node_no;
            //    All_Joints.Add(jn);
            //}



            //for (j = 0; j < Joints_Bottom[1].Count; j++)
            //{
            //    for (i = 1; i < Joints_Bottom.Count - 1; i++)
            //    {
            //        jn = Joints_Bottom[i][j];

            //        jn.NodeNo = ++node_no;
            //        All_Joints.Add(jn);
            //    }
            //}
            //i = Joints_Bottom.Count - 1;
            //for (j = 0; j < Joints_Bottom[i].Count; j++)
            //{
            //    jn = Joints_Bottom[i][j];

            //    jn.NodeNo = ++node_no;
            //    All_Joints.Add(jn);
            //}


            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count; j++)
                {
                    jn = Joints_Top[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }
            #endregion Generate Node / Joint  Numbers

            #region Bottom Chord Members
            Bottom_Chord1.Clear();
            Bottom_Chord2.Clear();
            Member mbr = new Member();

            for (i = 1; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                if (i == 1)
                {
                    mbr.StartNode = Joints_Bottom[i - 1][0];
                    mbr.EndNode = Joints_Bottom[i][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i - 1][Joints_Bottom[i - 1].Count - 1];
                    mbr.EndNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else if (i == Joints_Bottom.Count - 1 - 1)
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
            }

            #endregion Bottom Chord Members

            #region Cross Girder Members
            Cross_Girders.Clear();
            Xgirder1.Clear();
            Xgirder2.Clear();
            Xgirder3.Clear();

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 1; j < Joints_Bottom[i].Count; j++)
                {
                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][j - 1];
                    mbr.EndNode = Joints_Bottom[i][j];

                    if (j == 1 && i == 0)
                        Xgirder1.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i == 0)
                        Xgirder1.Add(mbr);
                    else if (j == 1 && i == Joints_Bottom.Count - 1)
                        Xgirder1.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i == Joints_Bottom.Count - 1)
                        Xgirder1.Add(mbr);

                    else if (j == 1 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder2.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder2.Add(mbr);
                    else if (j == 2 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder3.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 2 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder3.Add(mbr);
                    else
                        Cross_Girders.Add(mbr);
                }

            }

            #endregion Cross Girder Members

            #region Stringer Members
            Stringers1.Clear();
            Stringers2.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                for (j = 1; j < Joints_Bottom[0].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        mbr.StartNode = Joints_Bottom[i][j];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                        Stringers1.Add(mbr);
                    }
                    else if (i == Joints_Bottom.Count - 1 - 1)
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j];
                        Stringers1.Add(mbr);

                    }
                    else
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                        Stringers2.Add(mbr);
                    }
                }

            }

            #endregion Stringer Members

            #region Arch Members

            LeftArches.Clear();
            RightArches.Clear();

            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            mbr.StartNode = Joints_Bottom[0][0];
                            mbr.EndNode = Joints_Top[i][0];
                            LeftArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                        else if (j == Joints_Top[0].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][0];
                            LeftArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            mbr = new Member();
                            mbr.StartNode = Joints_Bottom[0][Joints_Bottom[0].Count - 1];
                            mbr.EndNode = Joints_Top[i][0];
                            RightArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                        }
                        else if (j == Joints_Top[i].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][Joints_Bottom[0].Count - 1];
                            RightArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);
                        }
                    }
                }

            }

            #endregion Left Arch Members

            #region Inner & Outer Cables / Strands

            for (i = 0; i < Joints_Top[0].Count; i++)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][0];
                mbr.EndNode = Joints_Top[0][i];
                Left_Outer_Strands.Add(mbr);

                //Left Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][1];
                mbr.EndNode = Joints_Top[0][i];
                //Left_Inner_Strands.Add(mbr);


                //Right Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Outer_Strands.Add(mbr);

                //Right Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1 - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Inner_Strands.Add(mbr);
            }

            #endregion Inner  & Outer Cables / Strands

            #region Bottom_Chord_Bracing
                Bottom_Chord_Bracing.Clear();
                END_BCB = new MemberCollection();

                if (Is_Add_BCB)
                {

                    for (i = 0; i < Joints_Bottom.Count - 1; i++)
                    {

                        if (i == 0 || i == Joints_Bottom.Count - 2)
                        {
                            mbr = new Member();
                            mbr.StartNode = Joints_Bottom[i][0];
                            mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                            END_BCB.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                            mbr.EndNode = Joints_Bottom[i + 1][0];
                            END_BCB.Add(mbr);
                        }
                        else
                        {
                            mbr = new Member();
                            mbr.StartNode = Joints_Bottom[i][0];
                            mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                            Bottom_Chord_Bracing.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                            mbr.EndNode = Joints_Bottom[i + 1][0];
                            Bottom_Chord_Bracing.Add(mbr);
                        }
                    }

                }
                #endregion Bottom_Chord_Bracing


            #region Transverse Members
            Transverse_Members.Clear();

            for (i = 1; i < Joints_Top[0].Count; i += 2)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i];
                Transverse_Members.Add(mbr);
            }

            #endregion Transverse Members

            #region Top_Chord_Bracing_Diagonal
            Top_Chord_Bracing_Diagonal.Clear();

            for (i = 0; i < Joints_Top[0].Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Top[1][i];
                mbr.EndNode = Joints_Top[0][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);
            }
            Top_Chord_Bracing_Diagonal.Clear();

            #endregion Top_Chord_Bracing_Diagonal

            x_count = 0;

            #region Write Data
            ASTRA_Data.Clear();
            ASTRA_Data.Add("JOINT COORDINATE");
            foreach (var item in All_Joints)
            {
                ASTRA_Data.Add(item.ToString());
            }

            mem_no = 0;
            ASTRA_Data.Add("MEMBER INCIDENCES");
            foreach (var item in Bottom_Chord1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in END_BCB)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord_Bracing)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Top_Chord_Bracing_Diagonal)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Right_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Right_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }


            foreach (var item in LeftArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in RightArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Transverse_Members)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Stringers1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Xgirder1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Xgirder2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Xgirder3)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Cross_Girders)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            #endregion Write Data

            #region START GROUP DEFINITION
            //ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));
            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 129 TO 140 153 TO 164"));
            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER 393 TO 397"));
            //ASTRA_Data.Add(string.Format("_OUTER_CABLE 212 214 216 218 220 222 224 226 228 230 232 235 237 239 241 243 245 247 249 251 253 255"));
            //ASTRA_Data.Add(string.Format("_INNER_CABLE 213 215 217 219 221 223 225 227 229 231 233 234 236 238 240 242 244 246 248 250 252 254"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));
            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));
            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            //ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));

            List<string> _groups = new List<string>();

            ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));

            //_groups.Add(kStr);

            string kStr = "";
            #region Bottom Chord

            for (i = 0; i < (NoOfPanel / 2); i++)
            {
                kStr = "_L" + i + "L" + (i + 1);
                _groups.Add(kStr);

                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Bottom_Chord1[i].MemberNo,
                    Bottom_Chord1[Bottom_Chord1.Count - (i + 1)].MemberNo,
                    Bottom_Chord2[i].MemberNo,
                    Bottom_Chord2[Bottom_Chord2.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }

            #endregion Bottom Chord

            #region Arch Segment

            for (i = 0; i < (LeftArches.Count / 2); i++)
            {
                //kStr = "_AC" + (i + 1) + "A" + (i + 2);
                kStr = "_AR" + (i + 1);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    LeftArches[i].MemberNo,
                    LeftArches[LeftArches.Count - (i + 1)].MemberNo,
                    RightArches[i].MemberNo,
                    RightArches[RightArches.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }
            #endregion Arch Segment

            #region Inner Strands

            for (i = 0; i < (Right_Inner_Strands.Count / 2) + 1; i++)
            {
                kStr = "_ICAB" + (i + 1);
                _groups.Add(kStr);

                if (i == ((Right_Inner_Strands.Count / 2)))
                {
                    kStr += string.Format("{0,10}",
                        //Left_Inner_Strands[i].MemberNo,
                        Right_Inner_Strands[i].MemberNo);
                }
                else
                {

                    kStr += string.Format("{0,10} {1,10}",
                        //Left_Inner_Strands[i].MemberNo,
                        //Left_Inner_Strands[Left_Inner_Strands.Count - (i + 1)].MemberNo,
                        Right_Inner_Strands[i].MemberNo,
                        Right_Inner_Strands[Right_Inner_Strands.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            #endregion Inner Strands

            #region Outer Strands

            for (i = 0; i < (Left_Outer_Strands.Count / 2) + 1; i++)
            {
                kStr = "_OCAB" + (i + 1);
                _groups.Add(kStr);

                if (i == ((Right_Outer_Strands.Count / 2)))
                {
                    kStr += string.Format("{0,10} {1,10}",
                        Left_Outer_Strands[i].MemberNo,
                        Right_Outer_Strands[i].MemberNo);

                }
                else
                {

                    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                        Left_Outer_Strands[i].MemberNo,
                        Left_Outer_Strands[Left_Outer_Strands.Count - (i + 1)].MemberNo,
                        Right_Outer_Strands[i].MemberNo,
                        Right_Outer_Strands[Right_Outer_Strands.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            #endregion Outer Strands

            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 {0} TO {1} {2} TO {3}",
            //    LeftArches[0].MemberNo, LeftArches[LeftArches.Count - 1].MemberNo,
            //    RightArches[0].MemberNo, RightArches[RightArches.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER {0} TO {1}", Top_Chord_Bracing[0].MemberNo, 
            //    Top_Chord_Bracing[Top_Chord_Bracing.Count - 1].MemberNo));

            _groups.Add("_TRANSVERSE");

            ASTRA_Data.Add(string.Format("_TRANSVERSE {0} TO {1}", Transverse_Members[0].MemberNo,
                Transverse_Members[Transverse_Members.Count - 1].MemberNo));


            //ASTRA_Data.Add(string.Format("_OUTER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Outer_Strands[0].MemberNo, Left_Outer_Strands[Left_Outer_Strands.Count - 1].MemberNo,
            //    Right_Outer_Strands[0].MemberNo, Right_Outer_Strands[Right_Outer_Strands.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INNER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Inner_Strands[0].MemberNo, Left_Inner_Strands[Left_Inner_Strands.Count - 1].MemberNo,
            //    Right_Inner_Strands[0].MemberNo, Right_Inner_Strands[Right_Inner_Strands.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_END");
            ASTRA_Data.Add(string.Format("_XGIRDERS_END {0} TO {1}", Xgirder1[0].MemberNo,
                Xgirder1[Xgirder1.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_EDGE");
            ASTRA_Data.Add(string.Format("_XGIRDERS_EDGE {0} TO {1}", Xgirder2[0].MemberNo,
                Xgirder2[Xgirder2.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_1");
            ASTRA_Data.Add(string.Format("_XGIRDERS_1 {0} TO {1}", Xgirder3[0].MemberNo,
                Xgirder3[Xgirder3.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_2");
            ASTRA_Data.Add(string.Format("_XGIRDERS_2 {0} TO {1}", Cross_Girders[0].MemberNo,
                Cross_Girders[Cross_Girders.Count - 1].MemberNo));



            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));


            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));


            _groups.Add("_STRINGER_1");
            ASTRA_Data.Add(string.Format("_STRINGER_1 {0} TO {1}", Stringers1[0].MemberNo,
                Stringers1[Stringers1.Count - 1].MemberNo));


            _groups.Add("_STRINGER_2");
            ASTRA_Data.Add(string.Format("_STRINGER_2 {0} TO {1}", Stringers2[0].MemberNo,
                Stringers2[Stringers2.Count - 1].MemberNo));

            if (Is_Add_BCB)
            {
                _groups.Add("_BCB_1");
                ASTRA_Data.Add(string.Format("_BCB_1 {0} TO {1}", END_BCB[0].MemberNo,
                    END_BCB[END_BCB.Count - 1].MemberNo));

                _groups.Add("_BCB_2");
                ASTRA_Data.Add(string.Format("_BCB_2 {0} TO {1}", Bottom_Chord_Bracing[0].MemberNo,
                    Bottom_Chord_Bracing[Bottom_Chord_Bracing.Count - 1].MemberNo));
            }
            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));
            #endregion END GROUP DEFINITION


            #region Member_Properties
            Hashtable hash_tab = new Hashtable();

            ASTRA_Data.Add("MEMBER PROPERTY");
            for (i = 0; i < _groups.Count; i++)
            {
                ASTRA_Data.Add(string.Format("{0,-10}  PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001", _groups[i], kStr));
            }
            //ASTRA_Data.Add("_BCB      PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001");
            //ASTRA_Data.Add("_STRINGER PRI  AX  0.048  IX  0.00001  IY  0.008000  IZ  0.002000");
            //ASTRA_Data.Add("_XGIRDER  PRI  AX  0.059  IX  0.00001  IY  0.009000  IZ  0.005000");
            for (i = 0; i < _groups.Count; i++)
            {
                if (_groups[i].StartsWith("_IC") ||
                    _groups[i].StartsWith("_OC"))
                {
                    ASTRA_Data.Add("MEMBER CABLE");
                    ASTRA_Data.Add(_groups[i]);
                }
                else if (_groups[i].StartsWith("_STR") ||
                    _groups[i].StartsWith("_XG") ||
                    _groups[i].StartsWith("_AR") ||
                    _groups[i].StartsWith("_TR"))
                {
                    //ASTRA_Data.Add("MEMBER CABLE");
                    //ASTRA_Data.Add(_groups[i]);
                }
                else
                {
                    ASTRA_Data.Add("MEMBER TRUSS");
                    ASTRA_Data.Add(_groups[i]);
                }
            }
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_ST");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_DIA");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_BCB");
            #endregion Member_Properties
            Get_Support_LOAD_FINISH(Joints_Bottom, ASTRA_Data);
            return ASTRA_Data;
        }

        private void Get_Support_LOAD_FINISH(List<JointNodeCollection> Joints_Bottom, List<string> ASTRA_Data)
        {

            ASTRA_Data.Add("CONSTANT");
            ASTRA_Data.Add("E 2.110E8 ALL ");
            ASTRA_Data.Add("DENSITY STEEL ALL");
            ASTRA_Data.Add("POISSON STEEL ALL");
            ASTRA_Data.Add("SUPPORTS");
            ASTRA_Data.Add(string.Format("{0} {1} {2}",
                Joints_Bottom[0][0].NodeNo,
                Joints_Bottom[0][Joints_Bottom[0].Count - 1].NodeNo, Start_Support));

            ASTRA_Data.Add(string.Format("{0} {1} {2}",
                         Joints_Bottom[Joints_Bottom.Count - 1][0].NodeNo,
                         Joints_Bottom[Joints_Bottom.Count - 1][Joints_Bottom[Joints_Bottom.Count - 1].Count - 1].NodeNo, End_Support));

            //Joint_Load_Support = joint_loads1;
            //Joint_Load_Edge = joint_loads2;
            Joint_Load_Support = "1 10";
            Joint_Load_Edge = "11 30";
            ASTRA_Data.Add("LOAD  1  DL + SIDL");
            ASTRA_Data.Add("JOINT LOAD");
            ASTRA_Data.Add(Joint_Load_Edge + " FY -100.0");
            ASTRA_Data.Add(Joint_Load_Support + " FY  -50.0");
            ASTRA_Data.Add("PRINT SUPPORT REACTIONS");
            ASTRA_Data.Add("PERFORM ANALYSIS");
            ASTRA_Data.Add("FINISH");
        }
        public List<string> Get_Bridge_Data_Removing_One_Side_Outer_Cables()
        {
            double x_inc = PanelLength;
            double z_inc = StringerSpacing;

            JointNode jn = null;

            int i = 0;
            int j = 0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            List<double> list_end_z = new List<double>();

            JointNodeCollection Joints = new JointNodeCollection();
            List<JointNodeCollection> Joints_Bottom = new List<JointNodeCollection>();
            List<JointNodeCollection> Joints_Top = new List<JointNodeCollection>();

            List<string> ASTRA_Data = new List<string>();
            Get_Coordinates(ref i, ref list_x, ref list_z, ref list_y, ref list_end_z);

            JointNodeCollection All_Joints = new JointNodeCollection();


            int x_count = 0;
            int z_count = 0;
            int mem_no = 1;
            int node_no = 1;
            x_count = 0;

            #region Calculate Bottom Coordinates
            //Calculate Bottom Coordinates
            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[0];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);


            Joints = new JointNodeCollection();
            for (j = 1; j < list_x.Count - 1; j++)
            {
                Joints = new JointNodeCollection();
                for (i = 0; i < list_z.Count; i++)
                {
                    jn = new JointNode();
                    jn.X = list_x[j];
                    jn.Y = 0;
                    jn.Z = list_z[i];
                    Joints.Add(jn);
                }
                Joints_Bottom.Add(Joints);
            }


            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[list_x.Count - 1];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);
            #endregion Calculate Bottom Coordinates

            #region Calculate Top Coordinates


            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[0];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);



            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[list_end_z.Count - 1];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);

            #endregion Calculate Top Coordinates

            #region Generate Node / Joint  Numbers

            node_no = 0;

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 0; j < Joints_Bottom[i].Count; j++)
                {
                    jn = Joints_Bottom[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }

            //i = 0;
            //for (j = 0; j < Joints_Bottom[i].Count; j++)
            //{
            //    jn = Joints_Bottom[i][j];

            //    jn.NodeNo = ++node_no;
            //    All_Joints.Add(jn);
            //}


            //for (j = 0; j < Joints_Bottom[1].Count; j++)
            //{
            //    for (i = 1; i < Joints_Bottom.Count - 1; i++)
            //    {
            //        jn = Joints_Bottom[i][j];

            //        jn.NodeNo = ++node_no;
            //        All_Joints.Add(jn);
            //    }
            //}

            //i = Joints_Bottom.Count - 1;
            //for (j = 0; j < Joints_Bottom[i].Count; j++)
            //{
            //    jn = Joints_Bottom[i][j];

            //    jn.NodeNo = ++node_no;
            //    All_Joints.Add(jn);
            //}


            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count; j++)
                {
                    jn = Joints_Top[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }
            #endregion Generate Node / Joint  Numbers

            #region Bottom Chord Members
            Bottom_Chord1.Clear();
            Bottom_Chord2.Clear();
            Member mbr = new Member();

            for (i = 1; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                if (i == 1)
                {
                    mbr.StartNode = Joints_Bottom[i - 1][0];
                    mbr.EndNode = Joints_Bottom[i][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i - 1][Joints_Bottom[i - 1].Count - 1];
                    mbr.EndNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else if (i == Joints_Bottom.Count - 1 - 1)
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
            }

            #endregion Bottom Chord Members

            #region Cross Girder Members
            Cross_Girders.Clear();
            Xgirder1.Clear();
            Xgirder2.Clear();
            Xgirder3.Clear();

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 1; j < Joints_Bottom[i].Count; j++)
                {
                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][j - 1];
                    mbr.EndNode = Joints_Bottom[i][j];

                    if (j == 1 && i == 0)
                        Xgirder1.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i == 0)
                        Xgirder1.Add(mbr);
                    else if (j == 1 && i == Joints_Bottom.Count - 1)
                        Xgirder1.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i == Joints_Bottom.Count - 1)
                        Xgirder1.Add(mbr);

                    else if (j == 1 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder2.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder2.Add(mbr);
                    else if (j == 2 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder3.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 2 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder3.Add(mbr);
                    else
                        Cross_Girders.Add(mbr);
                }

            }

            #endregion Cross Girder Members

            #region Stringer Members
            Stringers1.Clear();
            Stringers2.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                for (j = 1; j < Joints_Bottom[0].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        mbr.StartNode = Joints_Bottom[i][j];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                        Stringers1.Add(mbr);
                    }
                    else if (i == Joints_Bottom.Count - 1 - 1)
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j];
                        Stringers1.Add(mbr);

                    }
                    else
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                        Stringers2.Add(mbr);
                    }
                }

            }

            #endregion Stringer Members

            #region Arch Members

            LeftArches.Clear();
            RightArches.Clear();

            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            mbr.StartNode = Joints_Bottom[0][0];
                            mbr.EndNode = Joints_Top[i][0];
                            LeftArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                        else if (j == Joints_Top[0].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][0];
                            LeftArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            mbr = new Member();
                            mbr.StartNode = Joints_Bottom[0][Joints_Bottom[0].Count - 1];
                            mbr.EndNode = Joints_Top[i][0];
                            RightArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                        }
                        else if (j == Joints_Top[i].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][Joints_Bottom[0].Count - 1];
                            RightArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);
                        }
                    }
                }

            }

            #endregion Left Arch Members

            #region Inner & Outer Cables / Strands

            for (i = 0; i < Joints_Top[0].Count; i++)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][0];
                mbr.EndNode = Joints_Top[0][i];
                //Left_Outer_Strands.Add(mbr);

                //Left Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][1];
                mbr.EndNode = Joints_Top[0][i];
                Left_Inner_Strands.Add(mbr);


                //Right Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Outer_Strands.Add(mbr);

                //Right Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1 - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Inner_Strands.Add(mbr);
            }

            #endregion Inner  & Outer Cables / Strands

            #region Bottom_Chord_Bracing
            Bottom_Chord_Bracing.Clear();
            END_BCB.Clear();
            if (Is_Add_BCB)
            {
                for (i = 0; i < Joints_Bottom.Count - 1; i++)
                {

                    if (i == 0 || i == Joints_Bottom.Count - 2)
                    {
                        mbr = new Member();
                        mbr.StartNode = Joints_Bottom[i][0];
                        mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                        END_BCB.Add(mbr);

                        mbr = new Member();
                        mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                        mbr.EndNode = Joints_Bottom[i + 1][0];
                        END_BCB.Add(mbr);
                    }
                    else
                    {
                        mbr = new Member();
                        mbr.StartNode = Joints_Bottom[i][0];
                        mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                        Bottom_Chord_Bracing.Add(mbr);

                        mbr = new Member();
                        mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                        mbr.EndNode = Joints_Bottom[i + 1][0];
                        Bottom_Chord_Bracing.Add(mbr);
                    }
                }
            }
            #endregion Bottom_Chord_Bracing

            #region Transverse Members
            Transverse_Members.Clear();

            for (i = 1; i < Joints_Top[0].Count; i += 2)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i];
                Transverse_Members.Add(mbr);
            }

            #endregion Transverse Members

            #region Top_Chord_Bracing_Diagonal
            Top_Chord_Bracing_Diagonal.Clear();

            for (i = 0; i < Joints_Top[0].Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Top[1][i];
                mbr.EndNode = Joints_Top[0][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);
            }
            Top_Chord_Bracing_Diagonal.Clear();

            #endregion Top_Chord_Bracing_Diagonal

            x_count = 0;

            #region Write Data
            ASTRA_Data.Clear();
            ASTRA_Data.Add("JOINT COORDINATE");
            foreach (var item in All_Joints)
            {
                ASTRA_Data.Add(item.ToString());
            }

            mem_no = 0;
            ASTRA_Data.Add("MEMBER INCIDENCES");
            foreach (var item in Bottom_Chord1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in END_BCB)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord_Bracing)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Top_Chord_Bracing_Diagonal)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Right_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Right_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }


            foreach (var item in LeftArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in RightArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Transverse_Members)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Xgirder1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Xgirder2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Xgirder3)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Cross_Girders)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            #endregion Write Data

            #region START GROUP DEFINITION
            //ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));
            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 129 TO 140 153 TO 164"));
            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER 393 TO 397"));
            //ASTRA_Data.Add(string.Format("_OUTER_CABLE 212 214 216 218 220 222 224 226 228 230 232 235 237 239 241 243 245 247 249 251 253 255"));
            //ASTRA_Data.Add(string.Format("_INNER_CABLE 213 215 217 219 221 223 225 227 229 231 233 234 236 238 240 242 244 246 248 250 252 254"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));
            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));
            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            //ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));

            List<string> _groups = new List<string>();

            ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));

            //_groups.Add(kStr);

            string kStr = "";
            #region Bottom Chord

            for (i = 0; i < (NoOfPanel / 2); i++)
            {
                kStr = "_L" + i + "L" + (i + 1);
                _groups.Add(kStr);

                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Bottom_Chord1[i].MemberNo,
                    Bottom_Chord1[Bottom_Chord1.Count - (i + 1)].MemberNo,
                    Bottom_Chord2[i].MemberNo,
                    Bottom_Chord2[Bottom_Chord2.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }

            #endregion Bottom Chord

            #region Arch Segment

            for (i = 0; i < (LeftArches.Count / 2); i++)
            {
                //kStr = "_AC" + (i + 1) + "A" + (i + 2);
                kStr = "_AR" + (i + 1);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    LeftArches[i].MemberNo,
                    LeftArches[LeftArches.Count - (i + 1)].MemberNo,
                    RightArches[i].MemberNo,
                    RightArches[RightArches.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }
            #endregion Arch Segment

            #region Inner Strands

            for (i = 0; i < (Left_Inner_Strands.Count / 2) + 1; i++)
            {
                kStr = "_ICAB" + (i + 1);
                _groups.Add(kStr);

                if (i == ((Left_Inner_Strands.Count / 2)))
                {
                    kStr += string.Format("{0,10} {1,10}",
                        Left_Inner_Strands[i].MemberNo,
                        Right_Inner_Strands[i].MemberNo);
                }
                else
                {

                    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                        Left_Inner_Strands[i].MemberNo,
                        Left_Inner_Strands[Left_Inner_Strands.Count - (i + 1)].MemberNo,
                        Right_Inner_Strands[i].MemberNo,
                        Right_Inner_Strands[Right_Inner_Strands.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            #endregion Inner Strands

            #region Outer Strands

            for (i = 0; i < (Right_Outer_Strands.Count / 2) + 1; i++)
            {
                kStr = "_OCAB" + (i + 1);
                _groups.Add(kStr);

                if (i == ((Right_Outer_Strands.Count / 2)))
                {
                    kStr += string.Format("{0,10}",
                        //Left_Outer_Strands[i].MemberNo,
                        Right_Outer_Strands[i].MemberNo);

                }
                else
                {

                    kStr += string.Format("{0,10} {1,10}",
                        //Left_Outer_Strands[i].MemberNo,
                        //Left_Outer_Strands[Left_Outer_Strands.Count - (i + 1)].MemberNo,
                        Right_Outer_Strands[i].MemberNo,
                        Right_Outer_Strands[Right_Outer_Strands.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            #endregion Outer Strands

            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 {0} TO {1} {2} TO {3}",
            //    LeftArches[0].MemberNo, LeftArches[LeftArches.Count - 1].MemberNo,
            //    RightArches[0].MemberNo, RightArches[RightArches.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER {0} TO {1}", Top_Chord_Bracing[0].MemberNo, 
            //    Top_Chord_Bracing[Top_Chord_Bracing.Count - 1].MemberNo));

            _groups.Add("_TRANSVERSE");

            ASTRA_Data.Add(string.Format("_TRANSVERSE {0} TO {1}", Transverse_Members[0].MemberNo,
                Transverse_Members[Transverse_Members.Count - 1].MemberNo));


            //ASTRA_Data.Add(string.Format("_OUTER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Outer_Strands[0].MemberNo, Left_Outer_Strands[Left_Outer_Strands.Count - 1].MemberNo,
            //    Right_Outer_Strands[0].MemberNo, Right_Outer_Strands[Right_Outer_Strands.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INNER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Inner_Strands[0].MemberNo, Left_Inner_Strands[Left_Inner_Strands.Count - 1].MemberNo,
            //    Right_Inner_Strands[0].MemberNo, Right_Inner_Strands[Right_Inner_Strands.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_END");
            ASTRA_Data.Add(string.Format("_XGIRDERS_END {0} TO {1}", Xgirder1[0].MemberNo,
                Xgirder1[Xgirder1.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_EDGE");
            ASTRA_Data.Add(string.Format("_XGIRDERS_EDGE {0} TO {1}", Xgirder2[0].MemberNo,
                Xgirder2[Xgirder2.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_1");
            ASTRA_Data.Add(string.Format("_XGIRDERS_1 {0} TO {1}", Xgirder3[0].MemberNo,
                Xgirder3[Xgirder3.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_2");
            ASTRA_Data.Add(string.Format("_XGIRDERS_2 {0} TO {1}", Cross_Girders[0].MemberNo,
                Cross_Girders[Cross_Girders.Count - 1].MemberNo));



            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));


            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));


            _groups.Add("_STRINGER_1");
            ASTRA_Data.Add(string.Format("_STRINGER_1 {0} TO {1}", Stringers1[0].MemberNo,
                Stringers1[Stringers1.Count - 1].MemberNo));


            _groups.Add("_STRINGER_2");
            ASTRA_Data.Add(string.Format("_STRINGER_2 {0} TO {1}", Stringers2[0].MemberNo,
                Stringers2[Stringers2.Count - 1].MemberNo));

            if (Is_Add_BCB)
            {
                _groups.Add("_BCB_1");
                ASTRA_Data.Add(string.Format("_BCB_1 {0} TO {1}", END_BCB[0].MemberNo,
                    END_BCB[END_BCB.Count - 1].MemberNo));

                _groups.Add("_BCB_2");
                ASTRA_Data.Add(string.Format("_BCB_2 {0} TO {1}", Bottom_Chord_Bracing[0].MemberNo,
                    Bottom_Chord_Bracing[Bottom_Chord_Bracing.Count - 1].MemberNo));
            }
            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));
            #endregion END GROUP DEFINITION


            #region Member_Properties
            Hashtable hash_tab = new Hashtable();

            ASTRA_Data.Add("MEMBER PROPERTY");
            for (i = 0; i < _groups.Count; i++)
            {
                ASTRA_Data.Add(string.Format("{0,-10}  PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001", _groups[i], kStr));
            }
            //ASTRA_Data.Add("_BCB      PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001");
            //ASTRA_Data.Add("_STRINGER PRI  AX  0.048  IX  0.00001  IY  0.008000  IZ  0.002000");
            //ASTRA_Data.Add("_XGIRDER  PRI  AX  0.059  IX  0.00001  IY  0.009000  IZ  0.005000");
            for (i = 0; i < _groups.Count; i++)
            {
                if (_groups[i].StartsWith("_IC") ||
                    _groups[i].StartsWith("_OC"))
                {
                    ASTRA_Data.Add("MEMBER CABLE");
                    ASTRA_Data.Add(_groups[i]);
                }
                else if (_groups[i].StartsWith("_STR") ||
                    _groups[i].StartsWith("_XG") ||
                    _groups[i].StartsWith("_AR") ||
                    _groups[i].StartsWith("_TR"))
                {
                    //ASTRA_Data.Add("MEMBER CABLE");
                    //ASTRA_Data.Add(_groups[i]);
                }
                else
                {
                    ASTRA_Data.Add("MEMBER TRUSS");
                    ASTRA_Data.Add(_groups[i]);
                }
            }
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_ST");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_DIA");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_BCB");
            #endregion Member_Properties

            Get_Support_LOAD_FINISH(Joints_Bottom, ASTRA_Data);

            return ASTRA_Data;
        }
        public List<string> Get_Bridge_Data_Removing_Both_Side_Inner_Cables()
        {
            double x_inc = PanelLength;
            double z_inc = StringerSpacing;

            JointNode jn = null;

            int i = 0;
            int j = 0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            List<double> list_end_z = new List<double>();

            JointNodeCollection Joints = new JointNodeCollection();
            List<JointNodeCollection> Joints_Bottom = new List<JointNodeCollection>();
            List<JointNodeCollection> Joints_Top = new List<JointNodeCollection>();

            List<string> ASTRA_Data = new List<string>();
            Get_Coordinates(ref i, ref list_x, ref list_z, ref list_y, ref list_end_z);

            JointNodeCollection All_Joints = new JointNodeCollection();


            int x_count = 0;
            int z_count = 0;
            int mem_no = 1;
            int node_no = 1;
            x_count = 0;

            #region Calculate Bottom Coordinates
            //Calculate Bottom Coordinates
            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[0];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);


            Joints = new JointNodeCollection();
            for (j = 1; j < list_x.Count - 1; j++)
            {
                Joints = new JointNodeCollection();
                for (i = 0; i < list_z.Count; i++)
                {
                    jn = new JointNode();
                    jn.X = list_x[j];
                    jn.Y = 0;
                    jn.Z = list_z[i];
                    Joints.Add(jn);
                }
                Joints_Bottom.Add(Joints);
            }


            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[list_x.Count - 1];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);
            #endregion Calculate Bottom Coordinates

            #region Calculate Top Coordinates


            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[0];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);



            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[list_end_z.Count - 1];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);

            #endregion Calculate Top Coordinates

            #region Generate Node / Joint  Numbers

            node_no = 0;

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 0; j < Joints_Bottom[i].Count; j++)
                {
                    jn = Joints_Bottom[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }

            //i = 0;
            //for (j = 0; j < Joints_Bottom[i].Count; j++)
            //{
            //    jn = Joints_Bottom[i][j];

            //    jn.NodeNo = ++node_no;
            //    All_Joints.Add(jn);
            //}


            //for (j = 0; j < Joints_Bottom[1].Count; j++)
            //{
            //    for (i = 1; i < Joints_Bottom.Count - 1; i++)
            //    {
            //        jn = Joints_Bottom[i][j];

            //        jn.NodeNo = ++node_no;
            //        All_Joints.Add(jn);
            //    }
            //}

            //i = Joints_Bottom.Count - 1;
            //for (j = 0; j < Joints_Bottom[i].Count; j++)
            //{
            //    jn = Joints_Bottom[i][j];

            //    jn.NodeNo = ++node_no;
            //    All_Joints.Add(jn);
            //}


            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count; j++)
                {
                    jn = Joints_Top[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }
            #endregion Generate Node / Joint  Numbers

            #region Bottom Chord Members
            Bottom_Chord1.Clear();
            Bottom_Chord2.Clear();
            Member mbr = new Member();

            for (i = 1; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                if (i == 1)
                {
                    mbr.StartNode = Joints_Bottom[i - 1][0];
                    mbr.EndNode = Joints_Bottom[i][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i - 1][Joints_Bottom[i - 1].Count - 1];
                    mbr.EndNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else if (i == Joints_Bottom.Count - 1 - 1)
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
            }

            #endregion Bottom Chord Members

            #region Cross Girder Members
            Cross_Girders.Clear();
            Xgirder1.Clear();
            Xgirder2.Clear();
            Xgirder3.Clear();

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 1; j < Joints_Bottom[i].Count; j++)
                {
                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][j - 1];
                    mbr.EndNode = Joints_Bottom[i][j];

                    if (j == 1 && i == 0)
                        Xgirder1.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i == 0)
                        Xgirder1.Add(mbr);
                    else if (j == 1 && i == Joints_Bottom.Count - 1)
                        Xgirder1.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i == Joints_Bottom.Count - 1)
                        Xgirder1.Add(mbr);

                    else if (j == 1 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder2.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder2.Add(mbr);
                    else if (j == 2 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder3.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 2 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder3.Add(mbr);
                    else
                        Cross_Girders.Add(mbr);
                }

            }

            #endregion Cross Girder Members

            #region Stringer Members
            Stringers1.Clear();
            Stringers2.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                for (j = 1; j < Joints_Bottom[0].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        mbr.StartNode = Joints_Bottom[i][j];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                        Stringers1.Add(mbr);
                    }
                    else if (i == Joints_Bottom.Count - 1 - 1)
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j];
                        Stringers1.Add(mbr);

                    }
                    else
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                        Stringers2.Add(mbr);
                    }
                }

            }

            #endregion Stringer Members

            #region Arch Members

            LeftArches.Clear();
            RightArches.Clear();

            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            mbr.StartNode = Joints_Bottom[0][0];
                            mbr.EndNode = Joints_Top[i][0];
                            LeftArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                        else if (j == Joints_Top[0].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][0];
                            LeftArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            mbr = new Member();
                            mbr.StartNode = Joints_Bottom[0][Joints_Bottom[0].Count - 1];
                            mbr.EndNode = Joints_Top[i][0];
                            RightArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                        }
                        else if (j == Joints_Top[i].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][Joints_Bottom[0].Count - 1];
                            RightArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);
                        }
                    }
                }

            }

            #endregion Left Arch Members

            #region Inner & Outer Cables / Strands

            for (i = 0; i < Joints_Top[0].Count; i++)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][0];
                mbr.EndNode = Joints_Top[0][i];
                Left_Outer_Strands.Add(mbr);

                //Left Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][1];
                mbr.EndNode = Joints_Top[0][i];
                //Left_Inner_Strands.Add(mbr);


                //Right Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Outer_Strands.Add(mbr);

                //Right Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1 - 1];
                mbr.EndNode = Joints_Top[1][i];
                //Right_Inner_Strands.Add(mbr);
            }

            #endregion Inner  & Outer Cables / Strands

            #region Bottom_Chord_Bracing
            Bottom_Chord_Bracing.Clear();
            END_BCB = new MemberCollection();
            if (Is_Add_BCB)
            {
                for (i = 0; i < Joints_Bottom.Count - 1; i++)
                {

                    if (i == 0 || i == Joints_Bottom.Count - 2)
                    {
                        mbr = new Member();
                        mbr.StartNode = Joints_Bottom[i][0];
                        mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                        END_BCB.Add(mbr);

                        mbr = new Member();
                        mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                        mbr.EndNode = Joints_Bottom[i + 1][0];
                        END_BCB.Add(mbr);
                    }
                    else
                    {
                        mbr = new Member();
                        mbr.StartNode = Joints_Bottom[i][0];
                        mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                        Bottom_Chord_Bracing.Add(mbr);

                        mbr = new Member();
                        mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                        mbr.EndNode = Joints_Bottom[i + 1][0];
                        Bottom_Chord_Bracing.Add(mbr);
                    }
                }
            }
            #endregion Bottom_Chord_Bracing

            #region Transverse Members
            Transverse_Members.Clear();

            for (i = 1; i < Joints_Top[0].Count; i += 2)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i];
                Transverse_Members.Add(mbr);
            }

            #endregion Transverse Members

            #region Top_Chord_Bracing_Diagonal
            Top_Chord_Bracing_Diagonal.Clear();

            for (i = 0; i < Joints_Top[0].Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Top[1][i];
                mbr.EndNode = Joints_Top[0][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);
            }
            Top_Chord_Bracing_Diagonal.Clear();

            #endregion Top_Chord_Bracing_Diagonal

            x_count = 0;

            #region Write Data
            ASTRA_Data.Clear();
            ASTRA_Data.Add("JOINT COORDINATE");
            foreach (var item in All_Joints)
            {
                ASTRA_Data.Add(item.ToString());
            }

            mem_no = 0;
            ASTRA_Data.Add("MEMBER INCIDENCES");
            foreach (var item in Bottom_Chord1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in END_BCB)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord_Bracing)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Top_Chord_Bracing_Diagonal)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Right_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Right_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in LeftArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in RightArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Transverse_Members)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Stringers1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Xgirder1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Xgirder2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Xgirder3)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Cross_Girders)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            #endregion Write Data

            #region START GROUP DEFINITION
            //ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));
            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 129 TO 140 153 TO 164"));
            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER 393 TO 397"));
            //ASTRA_Data.Add(string.Format("_OUTER_CABLE 212 214 216 218 220 222 224 226 228 230 232 235 237 239 241 243 245 247 249 251 253 255"));
            //ASTRA_Data.Add(string.Format("_INNER_CABLE 213 215 217 219 221 223 225 227 229 231 233 234 236 238 240 242 244 246 248 250 252 254"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));
            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));
            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            //ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));

            List<string> _groups = new List<string>();

            ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));

            //_groups.Add(kStr);

            string kStr = "";
            #region Bottom Chord

            for (i = 0; i < (NoOfPanel / 2); i++)
            {
                kStr = "_L" + i + "L" + (i + 1);
                _groups.Add(kStr);

                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Bottom_Chord1[i].MemberNo,
                    Bottom_Chord1[Bottom_Chord1.Count - (i + 1)].MemberNo,
                    Bottom_Chord2[i].MemberNo,
                    Bottom_Chord2[Bottom_Chord2.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }

            #endregion Bottom Chord

            #region Arch Segment

            for (i = 0; i < (LeftArches.Count / 2); i++)
            {
                //kStr = "_AC" + (i + 1) + "A" + (i + 2);
                kStr = "_AR" + (i + 1);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    LeftArches[i].MemberNo,
                    LeftArches[LeftArches.Count - (i + 1)].MemberNo,
                    RightArches[i].MemberNo,
                    RightArches[RightArches.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }
            #endregion Arch Segment

            #region Inner Strands

            //for (i = 0; i < (Left_Inner_Strands.Count / 2) + 1; i++)
            //{
            //    kStr = "_ICAB" + (i + 1);
            //    _groups.Add(kStr);

            //    if (i == ((Left_Inner_Strands.Count / 2)))
            //    {
            //        kStr += string.Format("{0,10} {1,10}",
            //            Left_Inner_Strands[i].MemberNo,
            //            Right_Inner_Strands[i].MemberNo);
            //    }
            //    else
            //    {

            //        kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
            //            Left_Inner_Strands[i].MemberNo,
            //            Left_Inner_Strands[Left_Inner_Strands.Count - (i + 1)].MemberNo,
            //            Right_Inner_Strands[i].MemberNo,
            //            Right_Inner_Strands[Right_Inner_Strands.Count - (i + 1)].MemberNo);
            //    }
            //    ASTRA_Data.Add(kStr);
            //}

            #endregion Inner Strands

            #region Outer Strands

            for (i = 0; i < (Left_Outer_Strands.Count / 2) + 1; i++)
            {
                kStr = "_OCAB" + (i + 1);
                _groups.Add(kStr);

                if (i == ((Left_Outer_Strands.Count / 2)))
                {
                    kStr += string.Format("{0,10} {1,10}",
                        Left_Outer_Strands[i].MemberNo,
                        Right_Outer_Strands[i].MemberNo);

                }
                else
                {

                    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                        Left_Outer_Strands[i].MemberNo,
                        Left_Outer_Strands[Left_Outer_Strands.Count - (i + 1)].MemberNo,
                        Right_Outer_Strands[i].MemberNo,
                        Right_Outer_Strands[Right_Outer_Strands.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            #endregion Outer Strands

            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 {0} TO {1} {2} TO {3}",
            //    LeftArches[0].MemberNo, LeftArches[LeftArches.Count - 1].MemberNo,
            //    RightArches[0].MemberNo, RightArches[RightArches.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER {0} TO {1}", Top_Chord_Bracing[0].MemberNo, 
            //    Top_Chord_Bracing[Top_Chord_Bracing.Count - 1].MemberNo));

            _groups.Add("_TRANSVERSE");

            ASTRA_Data.Add(string.Format("_TRANSVERSE {0} TO {1}", Transverse_Members[0].MemberNo,
                Transverse_Members[Transverse_Members.Count - 1].MemberNo));


            //ASTRA_Data.Add(string.Format("_OUTER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Outer_Strands[0].MemberNo, Left_Outer_Strands[Left_Outer_Strands.Count - 1].MemberNo,
            //    Right_Outer_Strands[0].MemberNo, Right_Outer_Strands[Right_Outer_Strands.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INNER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Inner_Strands[0].MemberNo, Left_Inner_Strands[Left_Inner_Strands.Count - 1].MemberNo,
            //    Right_Inner_Strands[0].MemberNo, Right_Inner_Strands[Right_Inner_Strands.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_END");
            ASTRA_Data.Add(string.Format("_XGIRDERS_END {0} TO {1}", Xgirder1[0].MemberNo,
                Xgirder1[Xgirder1.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_EDGE");
            ASTRA_Data.Add(string.Format("_XGIRDERS_EDGE {0} TO {1}", Xgirder2[0].MemberNo,
                Xgirder2[Xgirder2.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_1");
            ASTRA_Data.Add(string.Format("_XGIRDERS_1 {0} TO {1}", Xgirder3[0].MemberNo,
                Xgirder3[Xgirder3.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_2");
            ASTRA_Data.Add(string.Format("_XGIRDERS_2 {0} TO {1}", Cross_Girders[0].MemberNo,
                Cross_Girders[Cross_Girders.Count - 1].MemberNo));



            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));


            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));


            _groups.Add("_STRINGER_1");
            ASTRA_Data.Add(string.Format("_STRINGER_1 {0} TO {1}", Stringers1[0].MemberNo,
                Stringers1[Stringers1.Count - 1].MemberNo));


            _groups.Add("_STRINGER_2");
            ASTRA_Data.Add(string.Format("_STRINGER_2 {0} TO {1}", Stringers2[0].MemberNo,
                Stringers2[Stringers2.Count - 1].MemberNo));





            if (Is_Add_BCB)
            {
                _groups.Add("_BCB_1");
                ASTRA_Data.Add(string.Format("_BCB_1 {0} TO {1}", END_BCB[0].MemberNo,
                    END_BCB[END_BCB.Count - 1].MemberNo));

                _groups.Add("_BCB_2");
                ASTRA_Data.Add(string.Format("_BCB_2 {0} TO {1}", Bottom_Chord_Bracing[0].MemberNo,
                    Bottom_Chord_Bracing[Bottom_Chord_Bracing.Count - 1].MemberNo));
            }
            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));
            #endregion END GROUP DEFINITION


            #region Member_Properties
            Hashtable hash_tab = new Hashtable();

            ASTRA_Data.Add("MEMBER PROPERTY");
            for (i = 0; i < _groups.Count; i++)
            {
                ASTRA_Data.Add(string.Format("{0,-10}  PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001", _groups[i], kStr));
            }
            //ASTRA_Data.Add("_BCB      PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001");
            //ASTRA_Data.Add("_STRINGER PRI  AX  0.048  IX  0.00001  IY  0.008000  IZ  0.002000");
            //ASTRA_Data.Add("_XGIRDER  PRI  AX  0.059  IX  0.00001  IY  0.009000  IZ  0.005000");
            for (i = 0; i < _groups.Count; i++)
            {
                if (_groups[i].StartsWith("_IC") ||
                    _groups[i].StartsWith("_OC"))
                {
                    ASTRA_Data.Add("MEMBER CABLE");
                    ASTRA_Data.Add(_groups[i]);
                }
                else if (_groups[i].StartsWith("_STR") ||
                    _groups[i].StartsWith("_XG") ||
                    _groups[i].StartsWith("_AR") ||
                    _groups[i].StartsWith("_TR"))
                {
                    //ASTRA_Data.Add("MEMBER CABLE");
                    //ASTRA_Data.Add(_groups[i]);
                }
                else
                {
                    ASTRA_Data.Add("MEMBER TRUSS");
                    ASTRA_Data.Add(_groups[i]);
                }
            }
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_ST");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_DIA");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_BCB");
            #endregion Member_Properties

            Get_Support_LOAD_FINISH(Joints_Bottom, ASTRA_Data);

            return ASTRA_Data;
        }

        public List<string> Get_Bridge_Data_Removing_Both_Side_Outer_Cables()
        {
            double x_inc = PanelLength;
            double z_inc = StringerSpacing;

            JointNode jn = null;

            int i = 0;
            int j = 0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            List<double> list_y = new List<double>();
            List<double> list_end_z = new List<double>();

            JointNodeCollection Joints = new JointNodeCollection();
            List<JointNodeCollection> Joints_Bottom = new List<JointNodeCollection>();
            List<JointNodeCollection> Joints_Top = new List<JointNodeCollection>();

            List<string> ASTRA_Data = new List<string>();
            Get_Coordinates(ref i, ref list_x, ref list_z, ref list_y, ref list_end_z);

            JointNodeCollection All_Joints = new JointNodeCollection();


            int x_count = 0;
            int z_count = 0;
            int mem_no = 1;
            int node_no = 1;
            x_count = 0;

            #region Calculate Bottom Coordinates
            //Calculate Bottom Coordinates
            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[0];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);


            Joints = new JointNodeCollection();
            for (j = 1; j < list_x.Count - 1; j++)
            {
                Joints = new JointNodeCollection();
                for (i = 0; i < list_z.Count; i++)
                {
                    jn = new JointNode();
                    jn.X = list_x[j];
                    jn.Y = 0;
                    jn.Z = list_z[i];
                    Joints.Add(jn);
                }
                Joints_Bottom.Add(Joints);
            }


            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[list_x.Count - 1];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);
            #endregion Calculate Bottom Coordinates

            #region Calculate Top Coordinates


            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[0];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);



            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[list_end_z.Count - 1];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);

            #endregion Calculate Top Coordinates

            #region Generate Node / Joint  Numbers

            node_no = 0;

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 0; j < Joints_Bottom[i].Count; j++)
                {
                    jn = Joints_Bottom[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }

            //i = 0;
            //for (j = 0; j < Joints_Bottom[i].Count; j++)
            //{
            //    jn = Joints_Bottom[i][j];

            //    jn.NodeNo = ++node_no;
            //    All_Joints.Add(jn);
            //}

            //for (j = 0; j < Joints_Bottom[1].Count; j++)
            //{
            //    for (i = 1; i < Joints_Bottom.Count - 1; i++)
            //    {
            //        jn = Joints_Bottom[i][j];

            //        jn.NodeNo = ++node_no;
            //        All_Joints.Add(jn);
            //    }
            //}

            //i = Joints_Bottom.Count - 1;
            //for (j = 0; j < Joints_Bottom[i].Count; j++)
            //{
            //    jn = Joints_Bottom[i][j];

            //    jn.NodeNo = ++node_no;
            //    All_Joints.Add(jn);
            //}



            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count; j++)
                {
                    jn = Joints_Top[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }
            #endregion Generate Node / Joint  Numbers

            #region Bottom Chord Members
            Bottom_Chord1.Clear();
            Bottom_Chord2.Clear();
            Member mbr = new Member();

            for (i = 1; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                if (i == 1)
                {
                    mbr.StartNode = Joints_Bottom[i - 1][0];
                    mbr.EndNode = Joints_Bottom[i][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i - 1][Joints_Bottom[i - 1].Count - 1];
                    mbr.EndNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else if (i == Joints_Bottom.Count - 1 - 1)
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
            }

            #endregion Bottom Chord Members

            #region Cross Girder Members
            Cross_Girders.Clear();
            Xgirder1.Clear();
            Xgirder2.Clear();
            Xgirder3.Clear();

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 1; j < Joints_Bottom[i].Count; j++)
                {
                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][j - 1];
                    mbr.EndNode = Joints_Bottom[i][j];

                    if (j == 1 && i == 0)
                        Xgirder1.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i == 0)
                        Xgirder1.Add(mbr);
                    else if (j == 1 && i == Joints_Bottom.Count - 1)
                        Xgirder1.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i == Joints_Bottom.Count - 1)
                        Xgirder1.Add(mbr);

                    else if (j == 1 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder2.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder2.Add(mbr);
                    else if (j == 2 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder3.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 2 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder3.Add(mbr);
                    else
                        Cross_Girders.Add(mbr);
                }

            }

            #endregion Cross Girder Members

            #region Stringer Members
            Stringers1.Clear();
            Stringers2.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                for (j = 1; j < Joints_Bottom[0].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        mbr.StartNode = Joints_Bottom[i][j];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                        Stringers1.Add(mbr);
                    }
                    else if (i == Joints_Bottom.Count - 1 - 1)
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j];
                        Stringers1.Add(mbr);

                    }
                    else
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                        Stringers2.Add(mbr);
                    }
                }

            }

            #endregion Stringer Members

            #region Arch Members

            LeftArches.Clear();
            RightArches.Clear();

            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            mbr.StartNode = Joints_Bottom[0][0];
                            mbr.EndNode = Joints_Top[i][0];
                            LeftArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                        else if (j == Joints_Top[0].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][0];
                            LeftArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            mbr = new Member();
                            mbr.StartNode = Joints_Bottom[0][Joints_Bottom[0].Count - 1];
                            mbr.EndNode = Joints_Top[i][0];
                            RightArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                        }
                        else if (j == Joints_Top[i].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][Joints_Bottom[0].Count - 1];
                            RightArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);
                        }
                    }
                }

            }

            #endregion Left Arch Members

            #region Inner & Outer Cables / Strands

            for (i = 0; i < Joints_Top[0].Count; i++)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][0];
                mbr.EndNode = Joints_Top[0][i];
                //Left_Outer_Strands.Add(mbr);

                //Left Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][1];
                mbr.EndNode = Joints_Top[0][i];
                Left_Inner_Strands.Add(mbr);


                //Right Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                mbr.EndNode = Joints_Top[1][i];
                //Right_Outer_Strands.Add(mbr);

                //Right Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1 - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Inner_Strands.Add(mbr);
            }

            #endregion Inner  & Outer Cables / Strands

            #region Bottom_Chord_Bracing
            Bottom_Chord_Bracing.Clear();
            END_BCB = new MemberCollection();
            if (Is_Add_BCB)
            {
                for (i = 0; i < Joints_Bottom.Count - 1; i++)
                {

                    if (i == 0 || i == Joints_Bottom.Count - 2)
                    {
                        mbr = new Member();
                        mbr.StartNode = Joints_Bottom[i][0];
                        mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                        END_BCB.Add(mbr);

                        mbr = new Member();
                        mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                        mbr.EndNode = Joints_Bottom[i + 1][0];
                        END_BCB.Add(mbr);
                    }
                    else
                    {
                        mbr = new Member();
                        mbr.StartNode = Joints_Bottom[i][0];
                        mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                        Bottom_Chord_Bracing.Add(mbr);

                        mbr = new Member();
                        mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                        mbr.EndNode = Joints_Bottom[i + 1][0];
                        Bottom_Chord_Bracing.Add(mbr);
                    }
                }
            }
            #endregion Bottom_Chord_Bracing

            #region Transverse Members
            Transverse_Members.Clear();

            for (i = 1; i < Joints_Top[0].Count; i += 2)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i];
                Transverse_Members.Add(mbr);
            }

            #endregion Transverse Members

            #region Top_Chord_Bracing_Diagonal
            Top_Chord_Bracing_Diagonal.Clear();

            for (i = 0; i < Joints_Top[0].Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Top[1][i];
                mbr.EndNode = Joints_Top[0][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);
            }
            Top_Chord_Bracing_Diagonal.Clear();

            #endregion Top_Chord_Bracing_Diagonal

            x_count = 0;

            #region Write Data
            ASTRA_Data.Clear();
            ASTRA_Data.Add("JOINT COORDINATE");
            foreach (var item in All_Joints)
            {
                ASTRA_Data.Add(item.ToString());
            }

            mem_no = 0;
            ASTRA_Data.Add("MEMBER INCIDENCES");
            foreach (var item in Bottom_Chord1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in END_BCB)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord_Bracing)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Top_Chord_Bracing_Diagonal)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Right_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Right_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }


            foreach (var item in LeftArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in RightArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Transverse_Members)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Xgirder1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Xgirder2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Xgirder3)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Cross_Girders)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            #endregion Write Data

            #region START GROUP DEFINITION
            //ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));
            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 129 TO 140 153 TO 164"));
            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER 393 TO 397"));
            //ASTRA_Data.Add(string.Format("_OUTER_CABLE 212 214 216 218 220 222 224 226 228 230 232 235 237 239 241 243 245 247 249 251 253 255"));
            //ASTRA_Data.Add(string.Format("_INNER_CABLE 213 215 217 219 221 223 225 227 229 231 233 234 236 238 240 242 244 246 248 250 252 254"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));
            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));
            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            //ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));

            List<string> _groups = new List<string>();

            ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));

            //_groups.Add(kStr);

            string kStr = "";
            #region Bottom Chord

            for (i = 0; i < (NoOfPanel / 2); i++)
            {
                kStr = "_L" + i + "L" + (i + 1);
                _groups.Add(kStr);

                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Bottom_Chord1[i].MemberNo,
                    Bottom_Chord1[Bottom_Chord1.Count - (i + 1)].MemberNo,
                    Bottom_Chord2[i].MemberNo,
                    Bottom_Chord2[Bottom_Chord2.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }

            #endregion Bottom Chord

            #region Arch Segment

            for (i = 0; i < (LeftArches.Count / 2); i++)
            {
                //kStr = "_AC" + (i + 1) + "A" + (i + 2);
                kStr = "_AR" + (i + 1);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    LeftArches[i].MemberNo,
                    LeftArches[LeftArches.Count - (i + 1)].MemberNo,
                    RightArches[i].MemberNo,
                    RightArches[RightArches.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }
            #endregion Arch Segment

            #region Inner Strands

            for (i = 0; i < (Left_Inner_Strands.Count / 2) + 1; i++)
            {
                kStr = "_ICAB" + (i + 1);
                _groups.Add(kStr);

                if (i == ((Left_Inner_Strands.Count / 2)))
                {
                    kStr += string.Format("{0,10} {1,10}",
                        Left_Inner_Strands[i].MemberNo,
                        Right_Inner_Strands[i].MemberNo);
                }
                else
                {

                    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                        Left_Inner_Strands[i].MemberNo,
                        Left_Inner_Strands[Left_Inner_Strands.Count - (i + 1)].MemberNo,
                        Right_Inner_Strands[i].MemberNo,
                        Right_Inner_Strands[Right_Inner_Strands.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            #endregion Inner Strands

            #region Outer Strands

            for (i = 0; i < (Left_Outer_Strands.Count / 2) + 1; i++)
            {
                //kStr = "_OCAB" + (i + 1);
                //_groups.Add(kStr);

                //if (i == ((Left_Inner_Strands.Count / 2)))
                //{
                //    kStr += string.Format("{0,10} {1,10}",
                //        Left_Outer_Strands[i].MemberNo,
                //        Right_Outer_Strands[i].MemberNo);

                //}
                //else
                //{

                //    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                //        Left_Outer_Strands[i].MemberNo,
                //        Left_Outer_Strands[Left_Outer_Strands.Count - (i + 1)].MemberNo,
                //        Right_Outer_Strands[i].MemberNo,
                //        Right_Outer_Strands[Right_Outer_Strands.Count - (i + 1)].MemberNo);
                //}
                //ASTRA_Data.Add(kStr);
            }

            #endregion Outer Strands

            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 {0} TO {1} {2} TO {3}",
            //    LeftArches[0].MemberNo, LeftArches[LeftArches.Count - 1].MemberNo,
            //    RightArches[0].MemberNo, RightArches[RightArches.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER {0} TO {1}", Top_Chord_Bracing[0].MemberNo, 
            //    Top_Chord_Bracing[Top_Chord_Bracing.Count - 1].MemberNo));

            _groups.Add("_TRANSVERSE");

            ASTRA_Data.Add(string.Format("_TRANSVERSE {0} TO {1}", Transverse_Members[0].MemberNo,
                Transverse_Members[Transverse_Members.Count - 1].MemberNo));


            //ASTRA_Data.Add(string.Format("_OUTER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Outer_Strands[0].MemberNo, Left_Outer_Strands[Left_Outer_Strands.Count - 1].MemberNo,
            //    Right_Outer_Strands[0].MemberNo, Right_Outer_Strands[Right_Outer_Strands.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INNER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Inner_Strands[0].MemberNo, Left_Inner_Strands[Left_Inner_Strands.Count - 1].MemberNo,
            //    Right_Inner_Strands[0].MemberNo, Right_Inner_Strands[Right_Inner_Strands.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_END");
            ASTRA_Data.Add(string.Format("_XGIRDERS_END {0} TO {1}", Xgirder1[0].MemberNo,
                Xgirder1[Xgirder1.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_EDGE");
            ASTRA_Data.Add(string.Format("_XGIRDERS_EDGE {0} TO {1}", Xgirder2[0].MemberNo,
                Xgirder2[Xgirder2.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_1");
            ASTRA_Data.Add(string.Format("_XGIRDERS_1 {0} TO {1}", Xgirder3[0].MemberNo,
                Xgirder3[Xgirder3.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_2");
            ASTRA_Data.Add(string.Format("_XGIRDERS_2 {0} TO {1}", Cross_Girders[0].MemberNo,
                Cross_Girders[Cross_Girders.Count - 1].MemberNo));



            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));


            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));


            _groups.Add("_STRINGER_1");
            ASTRA_Data.Add(string.Format("_STRINGER_1 {0} TO {1}", Stringers1[0].MemberNo,
                Stringers1[Stringers1.Count - 1].MemberNo));


            _groups.Add("_STRINGER_2");
            ASTRA_Data.Add(string.Format("_STRINGER_2 {0} TO {1}", Stringers2[0].MemberNo,
                Stringers2[Stringers2.Count - 1].MemberNo));
            if (Is_Add_BCB)
            {
                _groups.Add("_BCB_1");
                ASTRA_Data.Add(string.Format("_BCB_1 {0} TO {1}", END_BCB[0].MemberNo,
                    END_BCB[END_BCB.Count - 1].MemberNo));

                _groups.Add("_BCB_2");
                ASTRA_Data.Add(string.Format("_BCB_2 {0} TO {1}", Bottom_Chord_Bracing[0].MemberNo,
                    Bottom_Chord_Bracing[Bottom_Chord_Bracing.Count - 1].MemberNo));
            }
            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));
            #endregion END GROUP DEFINITION


            #region Member_Properties
            Hashtable hash_tab = new Hashtable();

            ASTRA_Data.Add("MEMBER PROPERTY");
            for (i = 0; i < _groups.Count; i++)
            {
                ASTRA_Data.Add(string.Format("{0,-10}  PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001", _groups[i], kStr));
            }
            //ASTRA_Data.Add("_BCB      PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001");
            //ASTRA_Data.Add("_STRINGER PRI  AX  0.048  IX  0.00001  IY  0.008000  IZ  0.002000");
            //ASTRA_Data.Add("_XGIRDER  PRI  AX  0.059  IX  0.00001  IY  0.009000  IZ  0.005000");
            for (i = 0; i < _groups.Count; i++)
            {
                if (_groups[i].StartsWith("_IC") ||
                    _groups[i].StartsWith("_OC"))
                {
                    ASTRA_Data.Add("MEMBER CABLE");
                    ASTRA_Data.Add(_groups[i]);
                }
                else if (_groups[i].StartsWith("_STR") ||
                    _groups[i].StartsWith("_XG") ||
                    _groups[i].StartsWith("_AR") ||
                    _groups[i].StartsWith("_TR"))
                {
                    //ASTRA_Data.Add("MEMBER CABLE");
                    //ASTRA_Data.Add(_groups[i]);
                }
                else
                {
                    ASTRA_Data.Add("MEMBER TRUSS");
                    ASTRA_Data.Add(_groups[i]);
                }
            }
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_ST");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_DIA");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_BCB");
            #endregion Member_Properties
            Get_Support_LOAD_FINISH(Joints_Bottom, ASTRA_Data);

            return ASTRA_Data;
        }
        public List<string> Get_All_Bridge_Data_2014_02_05()
        {

            double x_inc = PanelLength;
            double z_inc = StringerSpacing;

            JointNode jn = null;

            int i = 0;
            int j = 0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            JointNodeCollection Joints = new JointNodeCollection();
            List<JointNodeCollection> Joints_Bottom = new List<JointNodeCollection>();
            List<JointNodeCollection> Joints_Top = new List<JointNodeCollection>();

            List<string> ASTRA_Data = new List<string>();

            List<double> list_y = new List<double>();
            List<double> list_end_z = new List<double>();


            #region Define Arch Coordinates
            double R = 0, h = 0, L = 0, theta = 0;
            double arch_len = 0.0;

            h = Height;
            L = Length;

            //R = (h * h + (L / 2) * (L / 2)) / (2 * h);
            R = Arch_Radius;


            //theta = Math.Asin(L / 2 / R);
            theta = Arch_Angle;

            //arch_len = R * 2 * theta;

            double l, th_l;

            l = L / NoOfPanel;
            th_l = theta / (NoOfPanel / 2);



            double d1 = R - h;

            double R1 = d1 / Math.Cos(th_l);
            double R2 = R - R1;

            double d2 = R2 * Math.Cos(th_l);


            for (i = 1; i <= NoOfPanel / 2; i++)
            {
                d1 = R - h;
                R1 = d1 / Math.Cos(th_l * i);
                R2 = R - R1;
                d2 = R2 * Math.Cos(th_l * i);
                list_y.Add(d2);
            }


            list_y.Reverse();

            list_y.Add(h);



            for (i = (NoOfPanel / 2) - 1; i >= 0; i--)
            {
                list_y.Add(list_y[i]);
            }


            #endregion Define Arch

            #region Calculate Coordinates
            for (i = 0; i <= NoOfPanel; i++)
            {
                list_x.Add(PanelLength * i);
            }

            double Fw = Footpath_Width + 0.25; //Footpath Width

            double W = Breadth - Footpath_Width;
            list_end_z.Add(0);
            //list_z.Add(Fw);
            //list_z.Add(Breadth - Fw);
            list_end_z.Add(W);
            for (i = 0; i < NoOfStringerBeam; i++)
            {
                d1 = Fw + StringerSpacing * i;
                if (!list_end_z.Contains(d1))
                {
                    list_end_z.Add(d1);
                    list_z.Add(d1);
                }
            }
            list_end_z.Sort();



            list_z.Add(-Footpath_Width / 2.0);
            list_z.Add(Footpath_Width / 2.0);

            list_z.Add(W - (Footpath_Width / 2.0));
            list_z.Add(W + (Footpath_Width / 2.0));

            list_z.Sort();

            MyList.Array_Format_With(ref list_end_z, "f3");
            MyList.Array_Format_With(ref list_z, "f3");
            MyList.Array_Format_With(ref list_x, "f3");
            MyList.Array_Format_With(ref list_y, "f3");
            #endregion Calculate Coordinates

            JointNodeCollection All_Joints = new JointNodeCollection();


            int x_count = 0;
            int z_count = 0;
            int mem_no = 1;
            int node_no = 1;
            x_count = 0;

            #region Calculate Bottom Coordinates
            //Calculate Bottom Coordinates
            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[0];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);


            Joints = new JointNodeCollection();
            for (j = 1; j < list_x.Count - 1; j++)
            {
                Joints = new JointNodeCollection();
                for (i = 0; i < list_z.Count; i++)
                {
                    jn = new JointNode();
                    jn.X = list_x[j];
                    jn.Y = 0;
                    jn.Z = list_z[i];
                    Joints.Add(jn);
                }
                Joints_Bottom.Add(Joints);
            }


            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[list_x.Count - 1];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);
            #endregion Calculate Bottom Coordinates

            #region Calculate Top Coordinates


            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[0];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);



            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[list_end_z.Count - 1];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);

            #endregion Calculate Top Coordinates

            #region Generate Node / Joint  Numbers

            node_no = 0;

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 0; j < Joints_Bottom[i].Count; j++)
                {
                    jn = Joints_Bottom[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }


            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count; j++)
                {
                    jn = Joints_Top[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }
            #endregion Generate Node / Joint  Numbers

            #region Bottom Chord Members
            Bottom_Chord1.Clear();
            Bottom_Chord2.Clear();
            Member mbr = new Member();

            for (i = 1; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                if (i == 1)
                {
                    mbr.StartNode = Joints_Bottom[i - 1][0];
                    mbr.EndNode = Joints_Bottom[i][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i - 1][Joints_Bottom[i - 1].Count - 1];
                    mbr.EndNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else if (i == Joints_Bottom.Count - 1 - 1)
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
            }

            #endregion Bottom Chord Members

            #region Cross Girder Members
            Cross_Girders.Clear();

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 1; j < Joints_Bottom[i].Count; j++)
                {
                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][j - 1];
                    mbr.EndNode = Joints_Bottom[i][j];
                    Cross_Girders.Add(mbr);
                }

            }

            #endregion Cross Girder Members

            #region Stringer Members
            Stringers1.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                for (j = 1; j < Joints_Bottom[0].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        mbr.StartNode = Joints_Bottom[i][j];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                    }
                    else if (i == Joints_Bottom.Count - 1 - 1)
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j];

                    }
                    else
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                    }
                    Stringers1.Add(mbr);
                }

            }

            #endregion Stringer Members

            #region Arch Members

            LeftArches.Clear();
            RightArches.Clear();

            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            mbr.StartNode = Joints_Bottom[0][0];
                            mbr.EndNode = Joints_Top[i][0];
                            LeftArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                        else if (j == Joints_Top[0].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][0];
                            LeftArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            mbr = new Member();
                            mbr.StartNode = Joints_Bottom[0][Joints_Bottom[0].Count - 1];
                            mbr.EndNode = Joints_Top[i][0];
                            RightArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                        }
                        else if (j == Joints_Top[i].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][Joints_Bottom[0].Count - 1];
                            RightArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);
                        }
                    }
                }

            }

            #endregion Left Arch Members

            #region Inner & Outer Cables / Strands

            for (i = 0; i < Joints_Top[0].Count; i++)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][0];
                mbr.EndNode = Joints_Top[0][i];
                Left_Outer_Strands.Add(mbr);

                //Left Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][1];
                mbr.EndNode = Joints_Top[0][i];
                Left_Inner_Strands.Add(mbr);


                //Right Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Outer_Strands.Add(mbr);

                //Right Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1 - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Inner_Strands.Add(mbr);
            }

            #endregion Inner  & Outer Cables / Strands

            #region Bottom_Chord_Bracing
            Bottom_Chord_Bracing.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i][0];
                mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                Bottom_Chord_Bracing.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                mbr.EndNode = Joints_Bottom[i + 1][0];
                Bottom_Chord_Bracing.Add(mbr);
            }

            #endregion Bottom_Chord_Bracing

            #region Transverse Members
            Transverse_Members.Clear();

            for (i = 1; i < Joints_Top[0].Count; i += 2)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i];
                Transverse_Members.Add(mbr);
            }

            #endregion Transverse Members

            #region Top_Chord_Bracing_Diagonal
            Top_Chord_Bracing_Diagonal.Clear();

            for (i = 0; i < Joints_Top[0].Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Top[1][i];
                mbr.EndNode = Joints_Top[0][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);
            }
            Top_Chord_Bracing_Diagonal.Clear();

            #endregion Top_Chord_Bracing_Diagonal

            x_count = 0;

            #region Write Data
            ASTRA_Data.Clear();
            ASTRA_Data.Add("JOINT COORDINATE");
            foreach (var item in All_Joints)
            {
                ASTRA_Data.Add(item.ToString());
            }

            mem_no = 0;
            ASTRA_Data.Add("MEMBER INCIDENCES");
            foreach (var item in Bottom_Chord1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Cross_Girders)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in LeftArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in RightArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord_Bracing)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Transverse_Members)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Top_Chord_Bracing_Diagonal)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Right_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Right_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            #endregion Write Data


            #region START GROUP DEFINITION
            //ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));
            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 129 TO 140 153 TO 164"));
            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER 393 TO 397"));
            //ASTRA_Data.Add(string.Format("_OUTER_CABLE 212 214 216 218 220 222 224 226 228 230 232 235 237 239 241 243 245 247 249 251 253 255"));
            //ASTRA_Data.Add(string.Format("_INNER_CABLE 213 215 217 219 221 223 225 227 229 231 233 234 236 238 240 242 244 246 248 250 252 254"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));
            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));
            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            //ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));

            List<string> _groups = new List<string>();

            ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));

            //_groups.Add(kStr);

            string kStr = "";
            #region Bottom Chord

            for (i = 0; i < (NoOfPanel / 2); i++)
            {
                kStr = "_L" + i + "L" + (i + 1);
                _groups.Add(kStr);

                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Bottom_Chord1[i].MemberNo,
                    Bottom_Chord1[Bottom_Chord1.Count - (i + 1)].MemberNo,
                    Bottom_Chord2[i].MemberNo,
                    Bottom_Chord2[Bottom_Chord2.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }

            #endregion Bottom Chord

            #region Arch Segment

            for (i = 0; i < (LeftArches.Count / 2); i++)
            {
                //kStr = "_AC" + (i + 1) + "A" + (i + 2);
                kStr = "_AR" + (i + 1);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    LeftArches[i].MemberNo,
                    LeftArches[LeftArches.Count - (i + 1)].MemberNo,
                    RightArches[i].MemberNo,
                    RightArches[RightArches.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }
            #endregion Arch Segment

            #region Inner Strands

            for (i = 0; i < (Left_Inner_Strands.Count / 2) + 1; i++)
            {
                kStr = "_ICAB" + (i + 1);
                _groups.Add(kStr);

                if (i == ((Left_Inner_Strands.Count / 2)))
                {
                    kStr += string.Format("{0,10} {1,10}",
                        Left_Inner_Strands[i].MemberNo,
                        Right_Inner_Strands[i].MemberNo);
                }
                else
                {

                    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                        Left_Inner_Strands[i].MemberNo,
                        Left_Inner_Strands[Left_Inner_Strands.Count - (i + 1)].MemberNo,
                        Right_Inner_Strands[i].MemberNo,
                        Right_Inner_Strands[Right_Inner_Strands.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            #endregion Inner Strands

            #region Outer Strands

            for (i = 0; i < (Left_Outer_Strands.Count / 2) + 1; i++)
            {
                kStr = "_OCAB" + (i + 1);
                _groups.Add(kStr);

                if (i == ((Left_Inner_Strands.Count / 2)))
                {
                    kStr += string.Format("{0,10} {1,10}",
                        Left_Outer_Strands[i].MemberNo,
                        Right_Outer_Strands[i].MemberNo);

                }
                else
                {

                    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                        Left_Outer_Strands[i].MemberNo,
                        Left_Outer_Strands[Left_Outer_Strands.Count - (i + 1)].MemberNo,
                        Right_Outer_Strands[i].MemberNo,
                        Right_Outer_Strands[Right_Outer_Strands.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            #endregion Outer Strands

            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 {0} TO {1} {2} TO {3}",
            //    LeftArches[0].MemberNo, LeftArches[LeftArches.Count - 1].MemberNo,
            //    RightArches[0].MemberNo, RightArches[RightArches.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER {0} TO {1}", Top_Chord_Bracing[0].MemberNo, 
            //    Top_Chord_Bracing[Top_Chord_Bracing.Count - 1].MemberNo));

            _groups.Add("_TRANSVERSE");

            ASTRA_Data.Add(string.Format("_TRANSVERSE {0} TO {1}", Transverse_Members[0].MemberNo,
                Transverse_Members[Transverse_Members.Count - 1].MemberNo));


            //ASTRA_Data.Add(string.Format("_OUTER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Outer_Strands[0].MemberNo, Left_Outer_Strands[Left_Outer_Strands.Count - 1].MemberNo,
            //    Right_Outer_Strands[0].MemberNo, Right_Outer_Strands[Right_Outer_Strands.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INNER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Inner_Strands[0].MemberNo, Left_Inner_Strands[Left_Inner_Strands.Count - 1].MemberNo,
            //    Right_Inner_Strands[0].MemberNo, Right_Inner_Strands[Right_Inner_Strands.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS");
            ASTRA_Data.Add(string.Format("_XGIRDERS {0} TO {1}", Cross_Girders[0].MemberNo,
                Cross_Girders[Cross_Girders.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));


            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));


            _groups.Add("_STRINGER");
            ASTRA_Data.Add(string.Format("_STRINGER {0} TO {1}", Stringers1[0].MemberNo,
                Stringers1[Stringers1.Count - 1].MemberNo));

            _groups.Add("_BCB");
            ASTRA_Data.Add(string.Format("_BCB {0} TO {1}", Bottom_Chord_Bracing[0].MemberNo,
                Bottom_Chord_Bracing[Bottom_Chord_Bracing.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));
            #endregion END GROUP DEFINITION



            #region Member_Properties
            Hashtable hash_tab = new Hashtable();

            ASTRA_Data.Add("MEMBER PROPERTY");
            for (i = 0; i < _groups.Count; i++)
            {
                ASTRA_Data.Add(string.Format("{0,-10}  PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001", _groups[i], kStr));
            }
            //ASTRA_Data.Add("_BCB      PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001");
            //ASTRA_Data.Add("_STRINGER PRI  AX  0.048  IX  0.00001  IY  0.008000  IZ  0.002000");
            //ASTRA_Data.Add("_XGIRDER  PRI  AX  0.059  IX  0.00001  IY  0.009000  IZ  0.005000");
            for (i = 0; i < _groups.Count; i++)
            {
                if (_groups[i].StartsWith("_IC") ||
                    _groups[i].StartsWith("_OC"))
                {
                    ASTRA_Data.Add("MEMBER CABLE");
                    ASTRA_Data.Add(_groups[i]);
                }
                else if (_groups[i].StartsWith("_STR") ||
                    _groups[i].StartsWith("_XG") ||
                    _groups[i].StartsWith("_AR") ||
                    _groups[i].StartsWith("_TR"))
                {
                    //ASTRA_Data.Add("MEMBER CABLE");
                    //ASTRA_Data.Add(_groups[i]);
                }
                else
                {
                    ASTRA_Data.Add("MEMBER TRUSS");
                    ASTRA_Data.Add(_groups[i]);
                }
            }
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_ST");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_DIA");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_BCB");
            #endregion Member_Properties



            ASTRA_Data.Add("CONSTANT");
            ASTRA_Data.Add("E 2.110E8 ALL ");
            ASTRA_Data.Add("DENSITY STEEL ALL");
            ASTRA_Data.Add("POISSON STEEL ALL");
            ASTRA_Data.Add("SUPPORT");
            ASTRA_Data.Add(string.Format("{0} {1} {2} {3} FIXED BUT FX MZ",
                Joints_Bottom[0][0].NodeNo,
                Joints_Bottom[0][Joints_Bottom[0].Count - 1].NodeNo,
                Joints_Bottom[Joints_Bottom.Count - 1][0].NodeNo,
                Joints_Bottom[Joints_Bottom.Count - 1][Joints_Bottom[Joints_Bottom.Count - 1].Count - 1].NodeNo));

            //Joint_Load_Support = joint_loads1;
            //Joint_Load_Edge = joint_loads2;
            Joint_Load_Support = "1 10";
            Joint_Load_Edge = "11 30";
            ASTRA_Data.Add("LOAD  1  DL + SIDL");
            ASTRA_Data.Add("JOINT LOAD");
            ASTRA_Data.Add(Joint_Load_Edge + " FY -100.0");
            ASTRA_Data.Add(Joint_Load_Support + " FY  -50.0");
            //ASTRA_Data.Add("PRINT SUPPORT REACTIONS");
            ASTRA_Data.Add("PERFORM ANALYSIS");
            ASTRA_Data.Add("FINISH");
            return ASTRA_Data;
        }

        public List<string> Get_Bridge_Data_Removing_Left_Inner_Cables()
        {
            double x_inc = PanelLength;
            double z_inc = StringerSpacing;

            JointNode jn = null;

            int i = 0;
            int j = 0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            JointNodeCollection Joints = new JointNodeCollection();
            List<JointNodeCollection> Joints_Bottom = new List<JointNodeCollection>();
            List<JointNodeCollection> Joints_Top = new List<JointNodeCollection>();

            List<string> ASTRA_Data = new List<string>();

            List<double> list_y = new List<double>();
            List<double> list_end_z = new List<double>();

            Get_Coordinates(ref i, ref list_x, ref list_z, ref list_y, ref list_end_z);


            JointNodeCollection All_Joints = new JointNodeCollection();


            int x_count = 0;
            int z_count = 0;
            int mem_no = 1;
            int node_no = 1;
            x_count = 0;

            #region Calculate Bottom Coordinates
            //Calculate Bottom Coordinates
            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[0];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);


            Joints = new JointNodeCollection();
            for (j = 1; j < list_x.Count - 1; j++)
            {
                Joints = new JointNodeCollection();
                for (i = 0; i < list_z.Count; i++)
                {
                    jn = new JointNode();
                    jn.X = list_x[j];
                    jn.Y = 0;
                    jn.Z = list_z[i];
                    Joints.Add(jn);
                }
                Joints_Bottom.Add(Joints);
            }


            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[list_x.Count - 1];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);
            #endregion Calculate Bottom Coordinates

            #region Calculate Top Coordinates


            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[0];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);



            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[list_end_z.Count - 1];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);

            #endregion Calculate Top Coordinates

            #region Generate Node / Joint  Numbers

            node_no = 0;

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 0; j < Joints_Bottom[i].Count; j++)
                {
                    jn = Joints_Bottom[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }


            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count; j++)
                {
                    jn = Joints_Top[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }
            #endregion Generate Node / Joint  Numbers

            #region Bottom Chord Members
            Bottom_Chord1.Clear();
            Bottom_Chord2.Clear();
            Member mbr = new Member();

            for (i = 1; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                if (i == 1)
                {
                    mbr.StartNode = Joints_Bottom[i - 1][0];
                    mbr.EndNode = Joints_Bottom[i][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i - 1][Joints_Bottom[i - 1].Count - 1];
                    mbr.EndNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else if (i == Joints_Bottom.Count - 1 - 1)
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
            }

            #endregion Bottom Chord Members

            #region Cross Girder Members
            Cross_Girders.Clear();

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 1; j < Joints_Bottom[i].Count; j++)
                {
                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][j - 1];
                    mbr.EndNode = Joints_Bottom[i][j];
                    Cross_Girders.Add(mbr);
                }

            }

            #endregion Cross Girder Members

            #region Stringer Members
            Stringers1.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                for (j = 1; j < Joints_Bottom[0].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        mbr.StartNode = Joints_Bottom[i][j];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                    }
                    else if (i == Joints_Bottom.Count - 1 - 1)
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j];

                    }
                    else
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                    }
                    Stringers1.Add(mbr);
                }

            }

            #endregion Stringer Members

            #region Arch Members

            LeftArches.Clear();
            RightArches.Clear();

            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            mbr.StartNode = Joints_Bottom[0][0];
                            mbr.EndNode = Joints_Top[i][0];
                            LeftArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                        else if (j == Joints_Top[0].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][0];
                            LeftArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            mbr = new Member();
                            mbr.StartNode = Joints_Bottom[0][Joints_Bottom[0].Count - 1];
                            mbr.EndNode = Joints_Top[i][0];
                            RightArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                        }
                        else if (j == Joints_Top[i].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][Joints_Bottom[0].Count - 1];
                            RightArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);
                        }
                    }
                }

            }

            #endregion Left Arch Members

            #region Inner & Outer Cables / Strands

            for (i = 0; i < Joints_Top[0].Count; i++)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][0];
                mbr.EndNode = Joints_Top[0][i];
                Left_Outer_Strands.Add(mbr);

                //Left Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][1];
                mbr.EndNode = Joints_Top[0][i];
                Left_Inner_Strands.Add(mbr);


                //Right Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Outer_Strands.Add(mbr);

                //Right Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1 - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Inner_Strands.Add(mbr);
            }

            #endregion Inner  & Outer Cables / Strands

            #region Bottom_Chord_Bracing
            Bottom_Chord_Bracing.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i][0];
                mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                Bottom_Chord_Bracing.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                mbr.EndNode = Joints_Bottom[i + 1][0];
                Bottom_Chord_Bracing.Add(mbr);
            }

            #endregion Bottom_Chord_Bracing

            #region Transverse Members
            Transverse_Members.Clear();

            for (i = 1; i < Joints_Top[0].Count; i += 2)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i];
                Transverse_Members.Add(mbr);
            }

            #endregion Transverse Members

            #region Top_Chord_Bracing_Diagonal
            Top_Chord_Bracing_Diagonal.Clear();

            for (i = 0; i < Joints_Top[0].Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Top[1][i];
                mbr.EndNode = Joints_Top[0][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);
            }
            Top_Chord_Bracing_Diagonal.Clear();

            #endregion Top_Chord_Bracing_Diagonal

            x_count = 0;

            #region Write Data
            ASTRA_Data.Clear();
            ASTRA_Data.Add("JOINT COORDINATE");
            foreach (var item in All_Joints)
            {
                ASTRA_Data.Add(item.ToString());
            }

            mem_no = 0;
            ASTRA_Data.Add("MEMBER INCIDENCES");
            foreach (var item in Bottom_Chord1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Xgirder1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Xgirder2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Xgirder3)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Cross_Girders)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in LeftArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in RightArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Inner_Strands)
            {
                //item.MemberNo = ++mem_no;
                //ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Right_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Right_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in END_BCB)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord_Bracing)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Transverse_Members)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Top_Chord_Bracing_Diagonal)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            #endregion Write Data


            #region START GROUP DEFINITION
            //ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));
            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 129 TO 140 153 TO 164"));
            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER 393 TO 397"));
            //ASTRA_Data.Add(string.Format("_OUTER_CABLE 212 214 216 218 220 222 224 226 228 230 232 235 237 239 241 243 245 247 249 251 253 255"));
            //ASTRA_Data.Add(string.Format("_INNER_CABLE 213 215 217 219 221 223 225 227 229 231 233 234 236 238 240 242 244 246 248 250 252 254"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));
            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));
            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            //ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));

            List<string> _groups = new List<string>();

            ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));

            //_groups.Add(kStr);

            string kStr = "";
            #region Bottom Chord

            for (i = 0; i < (NoOfPanel / 2); i++)
            {
                kStr = "_L" + i + "L" + (i + 1);
                _groups.Add(kStr);

                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Bottom_Chord1[i].MemberNo,
                    Bottom_Chord1[Bottom_Chord1.Count - (i + 1)].MemberNo,
                    Bottom_Chord2[i].MemberNo,
                    Bottom_Chord2[Bottom_Chord2.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }

            #endregion Bottom Chord

            #region Arch Segment

            for (i = 0; i < (LeftArches.Count / 2); i++)
            {
                //kStr = "_AC" + (i + 1) + "A" + (i + 2);
                kStr = "_AR" + (i + 1);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    LeftArches[i].MemberNo,
                    LeftArches[LeftArches.Count - (i + 1)].MemberNo,
                    RightArches[i].MemberNo,
                    RightArches[RightArches.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }
            #endregion Arch Segment

            #region Inner Strands

            for (i = 0; i < (Right_Inner_Strands.Count / 2) + 1; i++)
            {
                kStr = "_ICAB" + (i + 1);
                _groups.Add(kStr);
                if (i == (Right_Inner_Strands.Count / 2))
                {
                    kStr += string.Format("{0,10}",
                        Right_Inner_Strands[i].MemberNo);
                }
                else
                {
                    kStr += string.Format("{0,10} {1,10}",
                        //Left_Inner_Strands[i].MemberNo,
                        //Left_Inner_Strands[Left_Inner_Strands.Count - (i + 1)].MemberNo,
                        Right_Inner_Strands[i].MemberNo,
                        Right_Inner_Strands[Right_Inner_Strands.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            #endregion Inner Strands

            #region Outer Strands

            for (i = 0; i < (Left_Outer_Strands.Count / 2) + 1; i++)
            {
                kStr = "_OCAB" + (i + 1);
                _groups.Add(kStr);

                if (i == (Right_Outer_Strands.Count / 2))
                {

                    kStr += string.Format("{0,10} {1,10}",
                        Left_Outer_Strands[i].MemberNo,
                        Right_Outer_Strands[i].MemberNo);
                }
                else
                {
                    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                        Left_Outer_Strands[i].MemberNo,
                        Left_Outer_Strands[Left_Outer_Strands.Count - (i + 1)].MemberNo,
                        Right_Outer_Strands[i].MemberNo,
                        Right_Outer_Strands[Right_Outer_Strands.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            #endregion Outer Strands

            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 {0} TO {1} {2} TO {3}",
            //    LeftArches[0].MemberNo, LeftArches[LeftArches.Count - 1].MemberNo,
            //    RightArches[0].MemberNo, RightArches[RightArches.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER {0} TO {1}", Top_Chord_Bracing[0].MemberNo, 
            //    Top_Chord_Bracing[Top_Chord_Bracing.Count - 1].MemberNo));

            _groups.Add("_TRANSVERSE");

            ASTRA_Data.Add(string.Format("_TRANSVERSE {0} TO {1}", Transverse_Members[0].MemberNo,
                Transverse_Members[Transverse_Members.Count - 1].MemberNo));


            //ASTRA_Data.Add(string.Format("_OUTER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Outer_Strands[0].MemberNo, Left_Outer_Strands[Left_Outer_Strands.Count - 1].MemberNo,
            //    Right_Outer_Strands[0].MemberNo, Right_Outer_Strands[Right_Outer_Strands.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INNER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Inner_Strands[0].MemberNo, Left_Inner_Strands[Left_Inner_Strands.Count - 1].MemberNo,
            //    Right_Inner_Strands[0].MemberNo, Right_Inner_Strands[Right_Inner_Strands.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_1");
            ASTRA_Data.Add(string.Format("_XGIRDERS_1 {0} TO {1}", Xgirder1[0].MemberNo,
                Xgirder1[Xgirder1.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_2");
            ASTRA_Data.Add(string.Format("_XGIRDERS_2 {0} TO {1}", Xgirder2[0].MemberNo,
                Xgirder2[Xgirder2.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_3");
            ASTRA_Data.Add(string.Format("_XGIRDERS_3 {0} TO {1}", Xgirder3[0].MemberNo,
                Xgirder3[Xgirder3.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS");
            ASTRA_Data.Add(string.Format("_XGIRDERS {0} TO {1}", Cross_Girders[0].MemberNo,
                Cross_Girders[Cross_Girders.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));


            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));


            _groups.Add("_STRINGER_1");
            ASTRA_Data.Add(string.Format("_STRINGER_1 {0} TO {1}", Stringers1[0].MemberNo,
                Stringers1[Stringers1.Count - 1].MemberNo));


            _groups.Add("_STRINGER_2");
            ASTRA_Data.Add(string.Format("_STRINGER_2 {0} TO {1}", Stringers2[0].MemberNo,
                Stringers2[Stringers2.Count - 1].MemberNo));






            _groups.Add("_BCB_1");
            ASTRA_Data.Add(string.Format("_BCB_1 {0} TO {1}", END_BCB[0].MemberNo,
                END_BCB[END_BCB.Count - 1].MemberNo));

            _groups.Add("_BCB_2");
            ASTRA_Data.Add(string.Format("_BCB_2 {0} TO {1}", Bottom_Chord_Bracing[0].MemberNo,
                Bottom_Chord_Bracing[Bottom_Chord_Bracing.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));
            #endregion END GROUP DEFINITION



            #region Member_Properties
            Hashtable hash_tab = new Hashtable();

            ASTRA_Data.Add("MEMBER PROPERTY");
            for (i = 0; i < _groups.Count; i++)
            {
                ASTRA_Data.Add(string.Format("{0,-10}  PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001", _groups[i], kStr));
            }
            //ASTRA_Data.Add("_BCB      PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001");
            //ASTRA_Data.Add("_STRINGER PRI  AX  0.048  IX  0.00001  IY  0.008000  IZ  0.002000");
            //ASTRA_Data.Add("_XGIRDER  PRI  AX  0.059  IX  0.00001  IY  0.009000  IZ  0.005000");
            for (i = 0; i < _groups.Count; i++)
            {
                for (i = 0; i < _groups.Count; i++)
                {
                    if (_groups[i].StartsWith("_IC") ||
                        _groups[i].StartsWith("_OC"))
                    {
                        ASTRA_Data.Add("MEMBER CABLE");
                        ASTRA_Data.Add(_groups[i]);
                    }
                    else if (_groups[i].StartsWith("_STR") ||
                        _groups[i].StartsWith("_XG") ||
                        _groups[i].StartsWith("_AR") ||
                        _groups[i].StartsWith("_TR"))
                    {
                        //ASTRA_Data.Add("MEMBER CABLE");
                        //ASTRA_Data.Add(_groups[i]);
                    }
                    else
                    {
                        ASTRA_Data.Add("MEMBER TRUSS");
                        ASTRA_Data.Add(_groups[i]);
                    }
                }
            }
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_ST");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_DIA");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_BCB");
            #endregion Member_Properties


            Get_Support_LOAD_FINISH(Joints_Bottom, ASTRA_Data);

            return ASTRA_Data;
        }
        public List<string> Get_Bridge_Data_Removing_Left_Outer_Cables()
        {
            double x_inc = PanelLength;
            double z_inc = StringerSpacing;

            JointNode jn = null;

            int i = 0;
            int j = 0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            JointNodeCollection Joints = new JointNodeCollection();
            List<JointNodeCollection> Joints_Bottom = new List<JointNodeCollection>();
            List<JointNodeCollection> Joints_Top = new List<JointNodeCollection>();

            List<string> ASTRA_Data = new List<string>();

            List<double> list_y = new List<double>();
            List<double> list_end_z = new List<double>();

            Get_Coordinates(ref i, ref list_x, ref list_z, ref list_y, ref list_end_z);

            JointNodeCollection All_Joints = new JointNodeCollection();


            int x_count = 0;
            int z_count = 0;
            int mem_no = 1;
            int node_no = 1;
            x_count = 0;

            #region Calculate Bottom Coordinates
            //Calculate Bottom Coordinates
            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[0];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);


            Joints = new JointNodeCollection();
            for (j = 1; j < list_x.Count - 1; j++)
            {
                Joints = new JointNodeCollection();
                for (i = 0; i < list_z.Count; i++)
                {
                    jn = new JointNode();
                    jn.X = list_x[j];
                    jn.Y = 0;
                    jn.Z = list_z[i];
                    Joints.Add(jn);
                }
                Joints_Bottom.Add(Joints);
            }


            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[list_x.Count - 1];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);
            #endregion Calculate Bottom Coordinates

            #region Calculate Top Coordinates


            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[0];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);



            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[list_end_z.Count - 1];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);

            #endregion Calculate Top Coordinates

            #region Generate Node / Joint  Numbers

            node_no = 0;

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 0; j < Joints_Bottom[i].Count; j++)
                {
                    jn = Joints_Bottom[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }


            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count; j++)
                {
                    jn = Joints_Top[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }
            #endregion Generate Node / Joint  Numbers

            #region Bottom Chord Members
            Bottom_Chord1.Clear();
            Bottom_Chord2.Clear();
            Member mbr = new Member();

            for (i = 1; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                if (i == 1)
                {
                    mbr.StartNode = Joints_Bottom[i - 1][0];
                    mbr.EndNode = Joints_Bottom[i][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i - 1][Joints_Bottom[i - 1].Count - 1];
                    mbr.EndNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else if (i == Joints_Bottom.Count - 1 - 1)
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
            }

            #endregion Bottom Chord Members


            #region Cross Girder Members
            Cross_Girders.Clear();
            Xgirder1.Clear();
            Xgirder2.Clear();
            Xgirder3.Clear();

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 1; j < Joints_Bottom[i].Count; j++)
                {
                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][j - 1];
                    mbr.EndNode = Joints_Bottom[i][j];

                    if (j == 1 && i == 0)
                        Xgirder1.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i == 0)
                        Xgirder1.Add(mbr);
                    else if (j == 1 && i == Joints_Bottom.Count - 1)
                        Xgirder1.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i == Joints_Bottom.Count - 1)
                        Xgirder1.Add(mbr);

                    else if (j == 1 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder2.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 1 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder2.Add(mbr);
                    else if (j == 2 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder3.Add(mbr);
                    else if (j == Joints_Bottom[i].Count - 2 && i != 0 && i != Joints_Bottom.Count - 1)
                        Xgirder3.Add(mbr);
                    else
                        Cross_Girders.Add(mbr);
                }

            }

            #endregion Cross Girder Members

            #region Stringer Members
            Stringers1.Clear();
            Stringers2.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                for (j = 1; j < Joints_Bottom[0].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        mbr.StartNode = Joints_Bottom[i][j];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                        Stringers1.Add(mbr);
                    }
                    else if (i == Joints_Bottom.Count - 1 - 1)
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j];
                        Stringers1.Add(mbr);

                    }
                    else
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                        Stringers2.Add(mbr);
                    }
                }

            }

            #endregion Stringer Members

            #region Arch Members

            LeftArches.Clear();
            RightArches.Clear();

            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            mbr.StartNode = Joints_Bottom[0][0];
                            mbr.EndNode = Joints_Top[i][0];
                            LeftArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                        else if (j == Joints_Top[0].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][0];
                            LeftArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            mbr = new Member();
                            mbr.StartNode = Joints_Bottom[0][Joints_Bottom[0].Count - 1];
                            mbr.EndNode = Joints_Top[i][0];
                            RightArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                        }
                        else if (j == Joints_Top[i].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][Joints_Bottom[0].Count - 1];
                            RightArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);
                        }
                    }
                }

            }

            #endregion Left Arch Members

            #region Inner & Outer Cables / Strands

            for (i = 0; i < Joints_Top[0].Count; i++)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][0];
                mbr.EndNode = Joints_Top[0][i];
                Left_Outer_Strands.Add(mbr);

                //Left Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][1];
                mbr.EndNode = Joints_Top[0][i];
                Left_Inner_Strands.Add(mbr);


                //Right Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Outer_Strands.Add(mbr);

                //Right Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1 - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Inner_Strands.Add(mbr);
            }

            #endregion Inner  & Outer Cables / Strands

            #region Bottom_Chord_Bracing
            Bottom_Chord_Bracing.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i][0];
                mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                Bottom_Chord_Bracing.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                mbr.EndNode = Joints_Bottom[i + 1][0];
                Bottom_Chord_Bracing.Add(mbr);
            }

            #endregion Bottom_Chord_Bracing

            #region Transverse Members
            Transverse_Members.Clear();

            for (i = 1; i < Joints_Top[0].Count; i += 2)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i];
                Transverse_Members.Add(mbr);
            }

            #endregion Transverse Members

            #region Top_Chord_Bracing_Diagonal
            Top_Chord_Bracing_Diagonal.Clear();

            for (i = 0; i < Joints_Top[0].Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Top[1][i];
                mbr.EndNode = Joints_Top[0][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);
            }
            Top_Chord_Bracing_Diagonal.Clear();

            #endregion Top_Chord_Bracing_Diagonal

            x_count = 0;

            #region Write Data
            ASTRA_Data.Clear();
            ASTRA_Data.Add("JOINT COORDINATE");
            foreach (var item in All_Joints)
            {
                ASTRA_Data.Add(item.ToString());
            }

            mem_no = 0;
            ASTRA_Data.Add("MEMBER INCIDENCES");
            foreach (var item in Bottom_Chord1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Xgirder1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Xgirder2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Xgirder3)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Cross_Girders)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in LeftArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in RightArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Outer_Strands)
            {
                //item.MemberNo = ++mem_no;
                //ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Right_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Right_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in END_BCB)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord_Bracing)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Transverse_Members)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Top_Chord_Bracing_Diagonal)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            #endregion Write Data


            #region START GROUP DEFINITION
            //ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));
            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 129 TO 140 153 TO 164"));
            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER 393 TO 397"));
            //ASTRA_Data.Add(string.Format("_OUTER_CABLE 212 214 216 218 220 222 224 226 228 230 232 235 237 239 241 243 245 247 249 251 253 255"));
            //ASTRA_Data.Add(string.Format("_INNER_CABLE 213 215 217 219 221 223 225 227 229 231 233 234 236 238 240 242 244 246 248 250 252 254"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));
            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));
            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            //ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));

            List<string> _groups = new List<string>();

            ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));

            //_groups.Add(kStr);

            string kStr = "";
            #region Bottom Chord

            for (i = 0; i < (NoOfPanel / 2); i++)
            {
                kStr = "_L" + i + "L" + (i + 1);
                _groups.Add(kStr);

                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Bottom_Chord1[i].MemberNo,
                    Bottom_Chord1[Bottom_Chord1.Count - (i + 1)].MemberNo,
                    Bottom_Chord2[i].MemberNo,
                    Bottom_Chord2[Bottom_Chord2.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }

            #endregion Bottom Chord

            #region Arch Segment

            for (i = 0; i < (LeftArches.Count / 2); i++)
            {
                //kStr = "_AC" + (i + 1) + "A" + (i + 2);
                kStr = "_AR" + (i + 1);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    LeftArches[i].MemberNo,
                    LeftArches[LeftArches.Count - (i + 1)].MemberNo,
                    RightArches[i].MemberNo,
                    RightArches[RightArches.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }
            #endregion Arch Segment

            #region Inner Strands

            for (i = 0; i < (Left_Inner_Strands.Count / 2) + 1; i++)
            {
                kStr = "_ICAB" + (i + 1);
                _groups.Add(kStr);
                if (i == (Left_Inner_Strands.Count / 2))
                {

                    kStr += string.Format("{0,10} {1,10}",
                        Left_Inner_Strands[i].MemberNo,
                        Right_Inner_Strands[i].MemberNo);
                }
                else
                {
                    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                        Left_Inner_Strands[i].MemberNo,
                        Left_Inner_Strands[Left_Inner_Strands.Count - (i + 1)].MemberNo,
                        Right_Inner_Strands[i].MemberNo,
                        Right_Inner_Strands[Right_Inner_Strands.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            #endregion Inner Strands

            #region Outer Strands

            for (i = 0; i < (Right_Outer_Strands.Count / 2) + 1; i++)
            {
                kStr = "_OCAB" + (i + 1);
                _groups.Add(kStr);

                if (i == (Right_Outer_Strands.Count / 2))
                {

                    kStr += string.Format("{0,10}",
                        Right_Outer_Strands[i].MemberNo);
                }
                else
                {
                    kStr += string.Format("{0,10} {1,10}",
                        //Left_Outer_Strands[i].MemberNo,
                        //Left_Outer_Strands[Left_Outer_Strands.Count - (i + 1)].MemberNo,
                        Right_Outer_Strands[i].MemberNo,
                        Right_Outer_Strands[Right_Outer_Strands.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            #endregion Outer Strands

            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 {0} TO {1} {2} TO {3}",
            //    LeftArches[0].MemberNo, LeftArches[LeftArches.Count - 1].MemberNo,
            //    RightArches[0].MemberNo, RightArches[RightArches.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER {0} TO {1}", Top_Chord_Bracing[0].MemberNo, 
            //    Top_Chord_Bracing[Top_Chord_Bracing.Count - 1].MemberNo));

            _groups.Add("_TRANSVERSE");

            ASTRA_Data.Add(string.Format("_TRANSVERSE {0} TO {1}", Transverse_Members[0].MemberNo,
                Transverse_Members[Transverse_Members.Count - 1].MemberNo));


            //ASTRA_Data.Add(string.Format("_OUTER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Outer_Strands[0].MemberNo, Left_Outer_Strands[Left_Outer_Strands.Count - 1].MemberNo,
            //    Right_Outer_Strands[0].MemberNo, Right_Outer_Strands[Right_Outer_Strands.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INNER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Inner_Strands[0].MemberNo, Left_Inner_Strands[Left_Inner_Strands.Count - 1].MemberNo,
            //    Right_Inner_Strands[0].MemberNo, Right_Inner_Strands[Right_Inner_Strands.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_1");
            ASTRA_Data.Add(string.Format("_XGIRDERS_1 {0} TO {1}", Xgirder1[0].MemberNo,
                Xgirder1[Xgirder1.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_2");
            ASTRA_Data.Add(string.Format("_XGIRDERS_2 {0} TO {1}", Xgirder2[0].MemberNo,
                Xgirder2[Xgirder2.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS_3");
            ASTRA_Data.Add(string.Format("_XGIRDERS_3 {0} TO {1}", Xgirder3[0].MemberNo,
                Xgirder3[Xgirder3.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS");
            ASTRA_Data.Add(string.Format("_XGIRDERS {0} TO {1}", Cross_Girders[0].MemberNo,
                Cross_Girders[Cross_Girders.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));


            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));



            _groups.Add("_STRINGER_1");
            ASTRA_Data.Add(string.Format("_STRINGER_1 {0} TO {1}", Stringers1[0].MemberNo,
                Stringers1[Stringers1.Count - 1].MemberNo));


            _groups.Add("_STRINGER_2");
            ASTRA_Data.Add(string.Format("_STRINGER_2 {0} TO {1}", Stringers2[0].MemberNo,
                Stringers2[Stringers2.Count - 1].MemberNo));

            _groups.Add("_BCB_1");
            ASTRA_Data.Add(string.Format("_BCB_1 {0} TO {1}", END_BCB[0].MemberNo,
                END_BCB[END_BCB.Count - 1].MemberNo));

            _groups.Add("_BCB_2");
            ASTRA_Data.Add(string.Format("_BCB_2 {0} TO {1}", Bottom_Chord_Bracing[0].MemberNo,
                Bottom_Chord_Bracing[Bottom_Chord_Bracing.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));
            #endregion END GROUP DEFINITION

            #region Member_Properties
            Hashtable hash_tab = new Hashtable();

            ASTRA_Data.Add("MEMBER PROPERTY");
            for (i = 0; i < _groups.Count; i++)
            {
                ASTRA_Data.Add(string.Format("{0,-10}  PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001", _groups[i], kStr));
            }
            //ASTRA_Data.Add("_BCB      PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001");
            //ASTRA_Data.Add("_STRINGER PRI  AX  0.048  IX  0.00001  IY  0.008000  IZ  0.002000");
            //ASTRA_Data.Add("_XGIRDER  PRI  AX  0.059  IX  0.00001  IY  0.009000  IZ  0.005000");
            for (i = 0; i < _groups.Count; i++)
            {
                for (i = 0; i < _groups.Count; i++)
                {
                    if (_groups[i].StartsWith("_IC") ||
                        _groups[i].StartsWith("_OC"))
                    {
                        ASTRA_Data.Add("MEMBER CABLE");
                        ASTRA_Data.Add(_groups[i]);
                    }
                    else if (_groups[i].StartsWith("_STR") ||
                        _groups[i].StartsWith("_XG") ||
                        _groups[i].StartsWith("_AR") ||
                        _groups[i].StartsWith("_TR"))
                    {
                        //ASTRA_Data.Add("MEMBER CABLE");
                        //ASTRA_Data.Add(_groups[i]);
                    }
                    else
                    {
                        ASTRA_Data.Add("MEMBER TRUSS");
                        ASTRA_Data.Add(_groups[i]);
                    }
                }
            }
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_ST");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_DIA");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_BCB");
            #endregion Member_Properties

            Get_Support_LOAD_FINISH(Joints_Bottom, ASTRA_Data);

            return ASTRA_Data;
        }
        public List<string> Get_Bridge_Data_Removing_Both_Inner_Cables()
        {
            double x_inc = PanelLength;
            double z_inc = StringerSpacing;

            JointNode jn = null;

            int i = 0;
            int j = 0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            JointNodeCollection Joints = new JointNodeCollection();
            List<JointNodeCollection> Joints_Bottom = new List<JointNodeCollection>();
            List<JointNodeCollection> Joints_Top = new List<JointNodeCollection>();

            List<string> ASTRA_Data = new List<string>();

            List<double> list_y = new List<double>();
            List<double> list_end_z = new List<double>();
            Get_Coordinates(ref i, ref list_x, ref list_z, ref list_y, ref list_end_z);


            JointNodeCollection All_Joints = new JointNodeCollection();


            int x_count = 0;
            int z_count = 0;
            int mem_no = 1;
            int node_no = 1;
            x_count = 0;

            #region Calculate Bottom Coordinates
            //Calculate Bottom Coordinates
            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[0];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);


            Joints = new JointNodeCollection();
            for (j = 1; j < list_x.Count - 1; j++)
            {
                Joints = new JointNodeCollection();
                for (i = 0; i < list_z.Count; i++)
                {
                    jn = new JointNode();
                    jn.X = list_x[j];
                    jn.Y = 0;
                    jn.Z = list_z[i];
                    Joints.Add(jn);
                }
                Joints_Bottom.Add(Joints);
            }


            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[list_x.Count - 1];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);
            #endregion Calculate Bottom Coordinates

            #region Calculate Top Coordinates


            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[0];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);



            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[list_end_z.Count - 1];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);

            #endregion Calculate Top Coordinates

            #region Generate Node / Joint  Numbers

            node_no = 0;

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 0; j < Joints_Bottom[i].Count; j++)
                {
                    jn = Joints_Bottom[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }


            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count; j++)
                {
                    jn = Joints_Top[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }
            #endregion Generate Node / Joint  Numbers

            #region Bottom Chord Members
            Bottom_Chord1.Clear();
            Bottom_Chord2.Clear();
            Member mbr = new Member();

            for (i = 1; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                if (i == 1)
                {
                    mbr.StartNode = Joints_Bottom[i - 1][0];
                    mbr.EndNode = Joints_Bottom[i][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i - 1][Joints_Bottom[i - 1].Count - 1];
                    mbr.EndNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else if (i == Joints_Bottom.Count - 1 - 1)
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
            }

            #endregion Bottom Chord Members

            #region Cross Girder Members
            Cross_Girders.Clear();

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 1; j < Joints_Bottom[i].Count; j++)
                {
                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][j - 1];
                    mbr.EndNode = Joints_Bottom[i][j];
                    Cross_Girders.Add(mbr);
                }

            }

            #endregion Cross Girder Members

            #region Stringer Members
            Stringers1.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                for (j = 1; j < Joints_Bottom[0].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        mbr.StartNode = Joints_Bottom[i][j];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                    }
                    else if (i == Joints_Bottom.Count - 1 - 1)
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j];

                    }
                    else
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                    }
                    Stringers1.Add(mbr);
                }

            }

            #endregion Stringer Members

            #region Arch Members

            LeftArches.Clear();
            RightArches.Clear();

            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            mbr.StartNode = Joints_Bottom[0][0];
                            mbr.EndNode = Joints_Top[i][0];
                            LeftArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                        else if (j == Joints_Top[0].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][0];
                            LeftArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            mbr = new Member();
                            mbr.StartNode = Joints_Bottom[0][Joints_Bottom[0].Count - 1];
                            mbr.EndNode = Joints_Top[i][0];
                            RightArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                        }
                        else if (j == Joints_Top[i].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][Joints_Bottom[0].Count - 1];
                            RightArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);
                        }
                    }
                }

            }

            #endregion Left Arch Members

            #region Inner & Outer Cables / Strands

            for (i = 0; i < Joints_Top[0].Count; i++)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][0];
                mbr.EndNode = Joints_Top[0][i];
                Left_Outer_Strands.Add(mbr);

                //Left Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][1];
                mbr.EndNode = Joints_Top[0][i];
                Left_Inner_Strands.Add(mbr);


                //Right Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Outer_Strands.Add(mbr);

                //Right Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1 - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Inner_Strands.Add(mbr);
            }

            #endregion Inner  & Outer Cables / Strands

            #region Bottom_Chord_Bracing
            Bottom_Chord_Bracing.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i][0];
                mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                Bottom_Chord_Bracing.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                mbr.EndNode = Joints_Bottom[i + 1][0];
                Bottom_Chord_Bracing.Add(mbr);
            }

            #endregion Bottom_Chord_Bracing

            #region Transverse Members
            Transverse_Members.Clear();

            for (i = 1; i < Joints_Top[0].Count; i += 2)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i];
                Transverse_Members.Add(mbr);
            }

            #endregion Transverse Members

            #region Top_Chord_Bracing_Diagonal
            Top_Chord_Bracing_Diagonal.Clear();

            for (i = 0; i < Joints_Top[0].Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Top[1][i];
                mbr.EndNode = Joints_Top[0][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);
            }
            Top_Chord_Bracing_Diagonal.Clear();

            #endregion Top_Chord_Bracing_Diagonal

            x_count = 0;

            #region Write Data
            ASTRA_Data.Clear();
            ASTRA_Data.Add("JOINT COORDINATE");
            foreach (var item in All_Joints)
            {
                ASTRA_Data.Add(item.ToString());
            }

            mem_no = 0;
            ASTRA_Data.Add("MEMBER INCIDENCES");
            foreach (var item in Bottom_Chord1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Cross_Girders)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in LeftArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in RightArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Inner_Strands)
            {
                //item.MemberNo = ++mem_no;
                //ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Right_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Right_Inner_Strands)
            {
                //item.MemberNo = ++mem_no;
                //ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord_Bracing)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Transverse_Members)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Top_Chord_Bracing_Diagonal)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            #endregion Write Data


            #region START GROUP DEFINITION
            //ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));
            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 129 TO 140 153 TO 164"));
            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER 393 TO 397"));
            //ASTRA_Data.Add(string.Format("_OUTER_CABLE 212 214 216 218 220 222 224 226 228 230 232 235 237 239 241 243 245 247 249 251 253 255"));
            //ASTRA_Data.Add(string.Format("_INNER_CABLE 213 215 217 219 221 223 225 227 229 231 233 234 236 238 240 242 244 246 248 250 252 254"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));
            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));
            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            //ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));

            List<string> _groups = new List<string>();

            ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));

            //_groups.Add(kStr);

            string kStr = "";
            #region Bottom Chord

            for (i = 0; i < (NoOfPanel / 2); i++)
            {
                kStr = "_L" + i + "L" + (i + 1);
                _groups.Add(kStr);

                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Bottom_Chord1[i].MemberNo,
                    Bottom_Chord1[Bottom_Chord1.Count - (i + 1)].MemberNo,
                    Bottom_Chord2[i].MemberNo,
                    Bottom_Chord2[Bottom_Chord2.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }

            #endregion Bottom Chord

            #region Arch Segment

            for (i = 0; i < (LeftArches.Count / 2); i++)
            {
                //kStr = "_AC" + (i + 1) + "A" + (i + 2);
                kStr = "_AR" + (i + 1);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    LeftArches[i].MemberNo,
                    LeftArches[LeftArches.Count - (i + 1)].MemberNo,
                    RightArches[i].MemberNo,
                    RightArches[RightArches.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }
            #endregion Arch Segment

            #region Inner Strands

            for (i = 0; i < (Left_Inner_Strands.Count / 2); i++)
            {
                //kStr = "_ICAB" + (i + 1);
                //_groups.Add(kStr);
                //kStr += string.Format("{0,10} {1,10}",
                //    Left_Inner_Strands[i].MemberNo,
                //    Left_Inner_Strands[Left_Inner_Strands.Count - (i + 1)].MemberNo);

                //ASTRA_Data.Add(kStr);
            }

            #endregion Inner Strands

            #region Outer Strands

            for (i = 0; i < (Left_Outer_Strands.Count / 2) + 1; i++)
            {
                kStr = "_OCAB" + (i + 1);
                _groups.Add(kStr);
                if (i == (Left_Outer_Strands.Count / 2))
                {

                    kStr += string.Format("{0,10} {1,10}",
                        Left_Outer_Strands[i].MemberNo,
                        Right_Outer_Strands[i].MemberNo);
                }
                else
                {
                    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                        Left_Outer_Strands[i].MemberNo,
                        Left_Outer_Strands[Left_Outer_Strands.Count - (i + 1)].MemberNo,
                        Right_Outer_Strands[i].MemberNo,
                        Right_Outer_Strands[Right_Outer_Strands.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            #endregion Outer Strands

            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 {0} TO {1} {2} TO {3}",
            //    LeftArches[0].MemberNo, LeftArches[LeftArches.Count - 1].MemberNo,
            //    RightArches[0].MemberNo, RightArches[RightArches.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER {0} TO {1}", Top_Chord_Bracing[0].MemberNo, 
            //    Top_Chord_Bracing[Top_Chord_Bracing.Count - 1].MemberNo));

            _groups.Add("_TRANSVERSE");

            ASTRA_Data.Add(string.Format("_TRANSVERSE {0} TO {1}", Transverse_Members[0].MemberNo,
                Transverse_Members[Transverse_Members.Count - 1].MemberNo));


            //ASTRA_Data.Add(string.Format("_OUTER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Outer_Strands[0].MemberNo, Left_Outer_Strands[Left_Outer_Strands.Count - 1].MemberNo,
            //    Right_Outer_Strands[0].MemberNo, Right_Outer_Strands[Right_Outer_Strands.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INNER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Inner_Strands[0].MemberNo, Left_Inner_Strands[Left_Inner_Strands.Count - 1].MemberNo,
            //    Right_Inner_Strands[0].MemberNo, Right_Inner_Strands[Right_Inner_Strands.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS");
            ASTRA_Data.Add(string.Format("_XGIRDERS {0} TO {1}", Cross_Girders[0].MemberNo,
                Cross_Girders[Cross_Girders.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));


            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));


            _groups.Add("_STRINGER");
            ASTRA_Data.Add(string.Format("_STRINGER {0} TO {1}", Stringers1[0].MemberNo,
                Stringers1[Stringers1.Count - 1].MemberNo));

            _groups.Add("_BCB");
            ASTRA_Data.Add(string.Format("_BCB {0} TO {1}", Bottom_Chord_Bracing[0].MemberNo,
                Bottom_Chord_Bracing[Bottom_Chord_Bracing.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));
            #endregion END GROUP DEFINITION

            #region Member_Properties
            Hashtable hash_tab = new Hashtable();

            ASTRA_Data.Add("MEMBER PROPERTY");
            for (i = 0; i < _groups.Count; i++)
            {
                ASTRA_Data.Add(string.Format("{0,-10}  PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001", _groups[i], kStr));
            }
            //ASTRA_Data.Add("_BCB      PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001");
            //ASTRA_Data.Add("_STRINGER PRI  AX  0.048  IX  0.00001  IY  0.008000  IZ  0.002000");
            //ASTRA_Data.Add("_XGIRDER  PRI  AX  0.059  IX  0.00001  IY  0.009000  IZ  0.005000");
            for (i = 0; i < _groups.Count; i++)
            {
                if (_groups[i].StartsWith("_IC") ||
                    _groups[i].StartsWith("_OC"))
                {
                    ASTRA_Data.Add("MEMBER CABLE");
                    ASTRA_Data.Add(_groups[i]);
                }
                else if (_groups[i].StartsWith("_A") ||
                    _groups[i].StartsWith("_TR") ||
                    _groups[i].StartsWith("_XG") ||
                    _groups[i].StartsWith("_ST"))
                {
                    //ASTRA_Data.Add("MEMBER CABLE");
                    //ASTRA_Data.Add(_groups[i]);
                }
                else
                {
                    ASTRA_Data.Add("MEMBER TRUSS");
                    ASTRA_Data.Add(_groups[i]);
                }
            }
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_ST");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_DIA");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_BCB");
            #endregion Member_Properties
            Get_Support_LOAD_FINISH(Joints_Bottom, ASTRA_Data);

            return ASTRA_Data;
        }
        public List<string> Get_Bridge_Data_Removing_Both_Outer_Cables()
        {
            double x_inc = PanelLength;
            double z_inc = StringerSpacing;

            JointNode jn = null;

            int i = 0;
            int j = 0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            JointNodeCollection Joints = new JointNodeCollection();
            List<JointNodeCollection> Joints_Bottom = new List<JointNodeCollection>();
            List<JointNodeCollection> Joints_Top = new List<JointNodeCollection>();

            List<string> ASTRA_Data = new List<string>();

            List<double> list_y = new List<double>();
            List<double> list_end_z = new List<double>();
            Get_Coordinates(ref i, ref list_x, ref list_z, ref list_y, ref list_end_z);


            JointNodeCollection All_Joints = new JointNodeCollection();


            int x_count = 0;
            int z_count = 0;
            int mem_no = 1;
            int node_no = 1;
            x_count = 0;

            #region Calculate Bottom Coordinates
            //Calculate Bottom Coordinates
            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[0];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);


            Joints = new JointNodeCollection();
            for (j = 1; j < list_x.Count - 1; j++)
            {
                Joints = new JointNodeCollection();
                for (i = 0; i < list_z.Count; i++)
                {
                    jn = new JointNode();
                    jn.X = list_x[j];
                    jn.Y = 0;
                    jn.Z = list_z[i];
                    Joints.Add(jn);
                }
                Joints_Bottom.Add(Joints);
            }


            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[list_x.Count - 1];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);
            #endregion Calculate Bottom Coordinates

            #region Calculate Top Coordinates


            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[0];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);



            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[list_end_z.Count - 1];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);

            #endregion Calculate Top Coordinates

            #region Generate Node / Joint  Numbers

            node_no = 0;

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 0; j < Joints_Bottom[i].Count; j++)
                {
                    jn = Joints_Bottom[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }


            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count; j++)
                {
                    jn = Joints_Top[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }
            #endregion Generate Node / Joint  Numbers

            #region Bottom Chord Members
            Bottom_Chord1.Clear();
            Bottom_Chord2.Clear();
            Member mbr = new Member();

            for (i = 1; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                if (i == 1)
                {
                    mbr.StartNode = Joints_Bottom[i - 1][0];
                    mbr.EndNode = Joints_Bottom[i][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i - 1][Joints_Bottom[i - 1].Count - 1];
                    mbr.EndNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else if (i == Joints_Bottom.Count - 1 - 1)
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
            }

            #endregion Bottom Chord Members

            #region Cross Girder Members
            Cross_Girders.Clear();

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 1; j < Joints_Bottom[i].Count; j++)
                {
                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][j - 1];
                    mbr.EndNode = Joints_Bottom[i][j];
                    Cross_Girders.Add(mbr);
                }

            }

            #endregion Cross Girder Members

            #region Stringer Members
            Stringers1.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                for (j = 1; j < Joints_Bottom[0].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        mbr.StartNode = Joints_Bottom[i][j];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                    }
                    else if (i == Joints_Bottom.Count - 1 - 1)
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j];

                    }
                    else
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                    }
                    Stringers1.Add(mbr);
                }

            }

            #endregion Stringer Members

            #region Arch Members

            LeftArches.Clear();
            RightArches.Clear();

            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            mbr.StartNode = Joints_Bottom[0][0];
                            mbr.EndNode = Joints_Top[i][0];
                            LeftArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                        else if (j == Joints_Top[0].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][0];
                            LeftArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            mbr = new Member();
                            mbr.StartNode = Joints_Bottom[0][Joints_Bottom[0].Count - 1];
                            mbr.EndNode = Joints_Top[i][0];
                            RightArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                        }
                        else if (j == Joints_Top[i].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][Joints_Bottom[0].Count - 1];
                            RightArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);
                        }
                    }
                }

            }

            #endregion Left Arch Members

            #region Inner & Outer Cables / Strands

            for (i = 0; i < Joints_Top[0].Count; i++)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][0];
                mbr.EndNode = Joints_Top[0][i];
                Left_Outer_Strands.Add(mbr);

                //Left Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][1];
                mbr.EndNode = Joints_Top[0][i];
                Left_Inner_Strands.Add(mbr);


                //Right Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Outer_Strands.Add(mbr);

                //Right Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1 - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Inner_Strands.Add(mbr);
            }

            #endregion Inner  & Outer Cables / Strands

            #region Bottom_Chord_Bracing
            Bottom_Chord_Bracing.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i][0];
                mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                Bottom_Chord_Bracing.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                mbr.EndNode = Joints_Bottom[i + 1][0];
                Bottom_Chord_Bracing.Add(mbr);
            }

            #endregion Bottom_Chord_Bracing

            #region Transverse Members
            Transverse_Members.Clear();

            for (i = 1; i < Joints_Top[0].Count; i += 2)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i];
                Transverse_Members.Add(mbr);
            }

            #endregion Transverse Members

            #region Top_Chord_Bracing_Diagonal
            Top_Chord_Bracing_Diagonal.Clear();

            for (i = 0; i < Joints_Top[0].Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Top[1][i];
                mbr.EndNode = Joints_Top[0][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);
            }
            Top_Chord_Bracing_Diagonal.Clear();

            #endregion Top_Chord_Bracing_Diagonal

            x_count = 0;

            #region Write Data
            ASTRA_Data.Clear();
            ASTRA_Data.Add("JOINT COORDINATE");
            foreach (var item in All_Joints)
            {
                ASTRA_Data.Add(item.ToString());
            }

            mem_no = 0;
            ASTRA_Data.Add("MEMBER INCIDENCES");
            foreach (var item in Bottom_Chord1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Cross_Girders)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in LeftArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in RightArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Outer_Strands)
            {
                //item.MemberNo = ++mem_no;
                //ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Right_Outer_Strands)
            {
                //item.MemberNo = ++mem_no;
                //ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Right_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord_Bracing)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Transverse_Members)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Top_Chord_Bracing_Diagonal)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            #endregion Write Data


            #region START GROUP DEFINITION
            //ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));
            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 129 TO 140 153 TO 164"));
            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER 393 TO 397"));
            //ASTRA_Data.Add(string.Format("_OUTER_CABLE 212 214 216 218 220 222 224 226 228 230 232 235 237 239 241 243 245 247 249 251 253 255"));
            //ASTRA_Data.Add(string.Format("_INNER_CABLE 213 215 217 219 221 223 225 227 229 231 233 234 236 238 240 242 244 246 248 250 252 254"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));
            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));
            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            //ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));

            List<string> _groups = new List<string>();

            ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));

            //_groups.Add(kStr);

            string kStr = "";
            #region Bottom Chord

            for (i = 0; i < (NoOfPanel / 2); i++)
            {
                kStr = "_L" + i + "L" + (i + 1);
                _groups.Add(kStr);

                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Bottom_Chord1[i].MemberNo,
                    Bottom_Chord1[Bottom_Chord1.Count - (i + 1)].MemberNo,
                    Bottom_Chord2[i].MemberNo,
                    Bottom_Chord2[Bottom_Chord2.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }

            #endregion Bottom Chord

            #region Arch Segment

            for (i = 0; i < (LeftArches.Count / 2); i++)
            {
                //kStr = "_AC" + (i + 1) + "A" + (i + 2);
                kStr = "_AR" + (i + 1);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    LeftArches[i].MemberNo,
                    LeftArches[LeftArches.Count - (i + 1)].MemberNo,
                    RightArches[i].MemberNo,
                    RightArches[RightArches.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }
            #endregion Arch Segment

            #region Inner Strands

            for (i = 0; i < (Left_Inner_Strands.Count / 2) + 1; i++)
            {
                kStr = "_ICAB" + (i + 1);
                _groups.Add(kStr);

                if (i == (Left_Inner_Strands.Count / 2))
                {

                    kStr += string.Format("{0,10} {1,10}",
                        Left_Inner_Strands[i].MemberNo,
                        Right_Inner_Strands[i].MemberNo);
                }
                else
                {

                    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                        Left_Inner_Strands[i].MemberNo,
                        Left_Inner_Strands[Left_Inner_Strands.Count - (i + 1)].MemberNo,
                        Right_Inner_Strands[i].MemberNo,
                        Right_Inner_Strands[Right_Inner_Strands.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            #endregion Inner Strands

            #region Outer Strands

            for (i = 0; i < (Left_Outer_Strands.Count / 2); i++)
            {
                //kStr = "_OCAB" + (i + 1);
                //_groups.Add(kStr);
                //kStr += string.Format("{0,10} {1,10}",
                //    Left_Outer_Strands[i].MemberNo,
                //    Left_Outer_Strands[Left_Outer_Strands.Count - (i + 1)].MemberNo);

                //ASTRA_Data.Add(kStr);
            }

            #endregion Outer Strands

            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 {0} TO {1} {2} TO {3}",
            //    LeftArches[0].MemberNo, LeftArches[LeftArches.Count - 1].MemberNo,
            //    RightArches[0].MemberNo, RightArches[RightArches.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER {0} TO {1}", Top_Chord_Bracing[0].MemberNo, 
            //    Top_Chord_Bracing[Top_Chord_Bracing.Count - 1].MemberNo));

            _groups.Add("_TRANSVERSE");

            ASTRA_Data.Add(string.Format("_TRANSVERSE {0} TO {1}", Transverse_Members[0].MemberNo,
                Transverse_Members[Transverse_Members.Count - 1].MemberNo));


            //ASTRA_Data.Add(string.Format("_OUTER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Outer_Strands[0].MemberNo, Left_Outer_Strands[Left_Outer_Strands.Count - 1].MemberNo,
            //    Right_Outer_Strands[0].MemberNo, Right_Outer_Strands[Right_Outer_Strands.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INNER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Inner_Strands[0].MemberNo, Left_Inner_Strands[Left_Inner_Strands.Count - 1].MemberNo,
            //    Right_Inner_Strands[0].MemberNo, Right_Inner_Strands[Right_Inner_Strands.Count - 1].MemberNo));

            _groups.Add("_XGIRDERS");
            ASTRA_Data.Add(string.Format("_XGIRDERS {0} TO {1}", Cross_Girders[0].MemberNo,
                Cross_Girders[Cross_Girders.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));


            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));


            _groups.Add("_STRINGER");
            ASTRA_Data.Add(string.Format("_STRINGER {0} TO {1}", Stringers1[0].MemberNo,
                Stringers1[Stringers1.Count - 1].MemberNo));

            _groups.Add("_BCB");
            ASTRA_Data.Add(string.Format("_BCB {0} TO {1}", Bottom_Chord_Bracing[0].MemberNo,
                Bottom_Chord_Bracing[Bottom_Chord_Bracing.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));
            #endregion END GROUP DEFINITION



            #region Member_Properties
            Hashtable hash_tab = new Hashtable();

            ASTRA_Data.Add("MEMBER PROPERTY");
            for (i = 0; i < _groups.Count; i++)
            {
                ASTRA_Data.Add(string.Format("{0,-10}  PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001", _groups[i], kStr));
            }
            //ASTRA_Data.Add("_BCB      PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001");
            //ASTRA_Data.Add("_STRINGER PRI  AX  0.048  IX  0.00001  IY  0.008000  IZ  0.002000");
            //ASTRA_Data.Add("_XGIRDER  PRI  AX  0.059  IX  0.00001  IY  0.009000  IZ  0.005000");
            for (i = 0; i < _groups.Count; i++)
            {
                for (i = 0; i < _groups.Count; i++)
                {
                    if (_groups[i].StartsWith("_IC") ||
                        _groups[i].StartsWith("_OC"))
                    {
                        ASTRA_Data.Add("MEMBER CABLE");
                        ASTRA_Data.Add(_groups[i]);
                    }
                    else if (_groups[i].StartsWith("_STR") ||
                        _groups[i].StartsWith("_XG") ||
                        _groups[i].StartsWith("_AR") ||
                        _groups[i].StartsWith("_TR"))
                    {
                        //ASTRA_Data.Add("MEMBER CABLE");
                        //ASTRA_Data.Add(_groups[i]);
                    }
                    else
                    {
                        ASTRA_Data.Add("MEMBER TRUSS");
                        ASTRA_Data.Add(_groups[i]);
                    }
                }

            }
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_ST");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_DIA");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_BCB");
            #endregion Member_Properties

            Get_Support_LOAD_FINISH(Joints_Bottom, ASTRA_Data);

            return ASTRA_Data;
        }
        public List<string> Get_Bridge_Data_Removing_Inner_Outer_Cables()
        {
            double x_inc = PanelLength;
            double z_inc = StringerSpacing;

            JointNode jn = null;

            int i = 0;
            int j = 0;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            JointNodeCollection Joints = new JointNodeCollection();
            List<JointNodeCollection> Joints_Bottom = new List<JointNodeCollection>();
            List<JointNodeCollection> Joints_Top = new List<JointNodeCollection>();

            List<string> ASTRA_Data = new List<string>();

            List<double> list_y = new List<double>();
            List<double> list_end_z = new List<double>();

            Get_Coordinates(ref i, ref list_x, ref list_z, ref list_y, ref list_end_z);


            JointNodeCollection All_Joints = new JointNodeCollection();


            int x_count = 0;
            int z_count = 0;
            int mem_no = 1;
            int node_no = 1;
            x_count = 0;

            #region Calculate Bottom Coordinates
            //Calculate Bottom Coordinates
            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[0];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);


            Joints = new JointNodeCollection();
            for (j = 1; j < list_x.Count - 1; j++)
            {
                Joints = new JointNodeCollection();
                for (i = 0; i < list_z.Count; i++)
                {
                    jn = new JointNode();
                    jn.X = list_x[j];
                    jn.Y = 0;
                    jn.Z = list_z[i];
                    Joints.Add(jn);
                }
                Joints_Bottom.Add(Joints);
            }


            Joints = new JointNodeCollection();
            for (i = 0; i < list_end_z.Count; i++)
            {
                jn = new JointNode();
                jn.X = list_x[list_x.Count - 1];
                jn.Y = 0;
                jn.Z = list_end_z[i];
                Joints.Add(jn);
            }
            Joints_Bottom.Add(Joints);
            #endregion Calculate Bottom Coordinates

            #region Calculate Top Coordinates


            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[0];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);



            Joints = new JointNodeCollection();
            for (i = 1; i < list_x.Count - 1; i++)
            {
                jn = new JointNode();
                jn.X = list_x[i];
                jn.Y = list_y[i];
                jn.Z = list_end_z[list_end_z.Count - 1];
                Joints.Add(jn);
            }
            Joints_Top.Add(Joints);

            #endregion Calculate Top Coordinates

            #region Generate Node / Joint  Numbers

            node_no = 0;

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 0; j < Joints_Bottom[i].Count; j++)
                {
                    jn = Joints_Bottom[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }


            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count; j++)
                {
                    jn = Joints_Top[i][j];

                    jn.NodeNo = ++node_no;
                    All_Joints.Add(jn);
                }
            }
            #endregion Generate Node / Joint  Numbers

            #region Bottom Chord Members
            Bottom_Chord1.Clear();
            Bottom_Chord2.Clear();
            Member mbr = new Member();

            for (i = 1; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                if (i == 1)
                {
                    mbr.StartNode = Joints_Bottom[i - 1][0];
                    mbr.EndNode = Joints_Bottom[i][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i - 1][Joints_Bottom[i - 1].Count - 1];
                    mbr.EndNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else if (i == Joints_Bottom.Count - 1 - 1)
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);

                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
                else
                {
                    mbr.StartNode = Joints_Bottom[i][0];
                    mbr.EndNode = Joints_Bottom[i + 1][0];
                    Bottom_Chord1.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                    mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i].Count - 1];
                    Bottom_Chord2.Add(mbr);
                }
            }

            #endregion Bottom Chord Members

            #region Cross Girder Members
            Cross_Girders.Clear();

            for (i = 0; i < Joints_Bottom.Count; i++)
            {
                for (j = 1; j < Joints_Bottom[i].Count; j++)
                {
                    mbr = new Member();
                    mbr.StartNode = Joints_Bottom[i][j - 1];
                    mbr.EndNode = Joints_Bottom[i][j];
                    Cross_Girders.Add(mbr);
                }

            }

            #endregion Cross Girder Members

            #region Stringer Members
            Stringers1.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                for (j = 1; j < Joints_Bottom[0].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        mbr.StartNode = Joints_Bottom[i][j];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                    }
                    else if (i == Joints_Bottom.Count - 1 - 1)
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j];

                    }
                    else
                    {
                        mbr.StartNode = Joints_Bottom[i][j + 1];
                        mbr.EndNode = Joints_Bottom[i + 1][j + 1];
                    }
                    Stringers1.Add(mbr);
                }

            }

            #endregion Stringer Members

            #region Arch Members

            LeftArches.Clear();
            RightArches.Clear();

            for (i = 0; i < Joints_Top.Count; i++)
            {
                for (j = 0; j < Joints_Top[i].Count - 1; j++)
                {
                    mbr = new Member();

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            mbr.StartNode = Joints_Bottom[0][0];
                            mbr.EndNode = Joints_Top[i][0];
                            LeftArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                        else if (j == Joints_Top[0].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][0];
                            LeftArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            LeftArches.Add(mbr);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            mbr = new Member();
                            mbr.StartNode = Joints_Bottom[0][Joints_Bottom[0].Count - 1];
                            mbr.EndNode = Joints_Top[i][0];
                            RightArches.Add(mbr);
                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                        }
                        else if (j == Joints_Top[i].Count - 1 - 1)
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);

                            mbr = new Member();
                            mbr.StartNode = Joints_Top[i][j + 1];
                            mbr.EndNode = Joints_Bottom[Joints_Bottom.Count - 1][Joints_Bottom[0].Count - 1];
                            RightArches.Add(mbr);
                        }
                        else
                        {
                            mbr.StartNode = Joints_Top[i][j];
                            mbr.EndNode = Joints_Top[i][j + 1];
                            RightArches.Add(mbr);
                        }
                    }
                }

            }

            #endregion Left Arch Members

            #region Inner & Outer Cables / Strands

            for (i = 0; i < Joints_Top[0].Count; i++)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][0];
                mbr.EndNode = Joints_Top[0][i];
                Left_Outer_Strands.Add(mbr);

                //Left Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][1];
                mbr.EndNode = Joints_Top[0][i];
                Left_Inner_Strands.Add(mbr);


                //Right Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Outer_Strands.Add(mbr);

                //Right Inner Strands
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1 - 1];
                mbr.EndNode = Joints_Top[1][i];
                Right_Inner_Strands.Add(mbr);
            }

            #endregion Inner  & Outer Cables / Strands

            #region Bottom_Chord_Bracing
            Bottom_Chord_Bracing.Clear();

            for (i = 0; i < Joints_Bottom.Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i][0];
                mbr.EndNode = Joints_Bottom[i + 1][Joints_Bottom[i + 1].Count - 1];
                Bottom_Chord_Bracing.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Bottom[i][Joints_Bottom[i].Count - 1];
                mbr.EndNode = Joints_Bottom[i + 1][0];
                Bottom_Chord_Bracing.Add(mbr);
            }

            #endregion Bottom_Chord_Bracing

            #region Transverse Members
            Transverse_Members.Clear();

            for (i = 1; i < Joints_Top[0].Count; i += 2)
            {
                //Left Outer Strands
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i];
                Transverse_Members.Add(mbr);
            }

            #endregion Transverse Members

            #region Top_Chord_Bracing_Diagonal
            Top_Chord_Bracing_Diagonal.Clear();

            for (i = 0; i < Joints_Top[0].Count - 1; i++)
            {
                mbr = new Member();
                mbr.StartNode = Joints_Top[0][i];
                mbr.EndNode = Joints_Top[1][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);

                mbr = new Member();
                mbr.StartNode = Joints_Top[1][i];
                mbr.EndNode = Joints_Top[0][i + 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);
            }
            Top_Chord_Bracing_Diagonal.Clear();

            #endregion Top_Chord_Bracing_Diagonal

            x_count = 0;

            #region Write Data
            ASTRA_Data.Clear();
            ASTRA_Data.Add("JOINT COORDINATE");
            foreach (var item in All_Joints)
            {
                ASTRA_Data.Add(item.ToString());
            }

            mem_no = 0;
            ASTRA_Data.Add("MEMBER INCIDENCES");
            foreach (var item in Bottom_Chord1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord2)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers1)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Cross_Girders)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in LeftArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in RightArches)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Left_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Right_Outer_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Right_Inner_Strands)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord_Bracing)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Transverse_Members)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Top_Chord_Bracing_Diagonal)
            {
                item.MemberNo = ++mem_no;
                ASTRA_Data.Add(item.ToString());
            }
            #endregion Write Data


            #region START GROUP DEFINITION
            //ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));
            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 129 TO 140 153 TO 164"));
            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER 393 TO 397"));
            //ASTRA_Data.Add(string.Format("_OUTER_CABLE 212 214 216 218 220 222 224 226 228 230 232 235 237 239 241 243 245 247 249 251 253 255"));
            //ASTRA_Data.Add(string.Format("_INNER_CABLE 213 215 217 219 221 223 225 227 229 231 233 234 236 238 240 242 244 246 248 250 252 254"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));
            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));
            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            //ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));

            List<string> _groups = new List<string>();

            ASTRA_Data.Add(string.Format("START GROUP DEFINITION"));

            //_groups.Add(kStr);

            string kStr = "";
            #region Bottom Chord

            for (i = 0; i < (NoOfPanel / 2); i++)
            {
                kStr = "_L" + i + "L" + (i + 1);
                _groups.Add(kStr);

                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Bottom_Chord1[i].MemberNo,
                    Bottom_Chord1[Bottom_Chord1.Count - (i + 1)].MemberNo,
                    Bottom_Chord2[i].MemberNo,
                    Bottom_Chord2[Bottom_Chord2.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }

            #endregion Bottom Chord

            #region Arch Segment

            for (i = 0; i < (LeftArches.Count / 2); i++)
            {
                //kStr = "_AC" + (i + 1) + "A" + (i + 2);
                kStr = "_AR" + (i + 1);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    LeftArches[i].MemberNo,
                    LeftArches[LeftArches.Count - (i + 1)].MemberNo,
                    RightArches[i].MemberNo,
                    RightArches[RightArches.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }
            #endregion Arch Segment

            #region Inner Strands

            for (i = 0; i < (Left_Inner_Strands.Count / 2); i++)
            {
                kStr = "_ICAB" + (i + 1);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Left_Inner_Strands[i].MemberNo,
                    Left_Inner_Strands[Left_Inner_Strands.Count - (i + 1)].MemberNo,
                    Right_Inner_Strands[i].MemberNo,
                    Right_Inner_Strands[Right_Inner_Strands.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }

            #endregion Inner Strands

            #region Outer Strands

            for (i = 0; i < (Left_Outer_Strands.Count / 2); i++)
            {
                kStr = "_OCAB" + (i + 1);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Left_Outer_Strands[i].MemberNo,
                    Left_Outer_Strands[Left_Outer_Strands.Count - (i + 1)].MemberNo,
                    Right_Outer_Strands[i].MemberNo,
                    Right_Outer_Strands[Right_Outer_Strands.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }

            #endregion Outer Strands

            //ASTRA_Data.Add(string.Format("_ARCH_MEMBER_1 {0} TO {1} {2} TO {3}",
            //    LeftArches[0].MemberNo, LeftArches[LeftArches.Count - 1].MemberNo,
            //    RightArches[0].MemberNo, RightArches[RightArches.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_TRANSVERSE_MEMBER {0} TO {1}", Top_Chord_Bracing[0].MemberNo, 
            //    Top_Chord_Bracing[Top_Chord_Bracing.Count - 1].MemberNo));

            _groups.Add("_TRANSVERSE");

            ASTRA_Data.Add(string.Format("_TRANSVERSE {0} TO {1}", Transverse_Members[0].MemberNo,
                Transverse_Members[Transverse_Members.Count - 1].MemberNo));


            //ASTRA_Data.Add(string.Format("_OUTER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Outer_Strands[0].MemberNo, Left_Outer_Strands[Left_Outer_Strands.Count - 1].MemberNo,
            //    Right_Outer_Strands[0].MemberNo, Right_Outer_Strands[Right_Outer_Strands.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INNER_CABLE  {0} TO {1} {2} TO {3}",
            //    Left_Inner_Strands[0].MemberNo, Left_Inner_Strands[Left_Inner_Strands.Count - 1].MemberNo,
            //    Right_Inner_Strands[0].MemberNo, Right_Inner_Strands[Right_Inner_Strands.Count - 1].MemberNo));

            //_groups.Add("_XGIRDERS");
            ASTRA_Data.Add(string.Format("_XGIRDERS {0} TO {1}", Cross_Girders[0].MemberNo,
                Cross_Girders[Cross_Girders.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_1 257 TO 289 293 TO 314 332 334 336 338 340 342 344 346 348 350 352 357 359 361 363 365 367 369 371 373 375 377"));
            //ASTRA_Data.Add(string.Format("_INT_CROSS_GIRDER_2 410 TO 419"));
            //ASTRA_Data.Add(string.Format("_END_CROSS_GIRDER 256 290 315 TO 318 331 354 356 379"));


            //ASTRA_Data.Add(string.Format("_STRINGER_1 320 TO 329 333 335 337 339 341 343 345 347 349 351 353 355 358 360 362 364 366 368 370 372 374 376 378 380 382 TO 391 402 TO 405"));
            //ASTRA_Data.Add(string.Format("_STRINGER_2 398 TO 401 406 TO 409"));


            //_groups.Add("_STRINGER");
            ASTRA_Data.Add(string.Format("_STRINGER {0} TO {1}", Stringers1[0].MemberNo,
                Stringers1[Stringers1.Count - 1].MemberNo));

            _groups.Add("_BCB");
            ASTRA_Data.Add(string.Format("_BCB {0} TO {1}", Bottom_Chord_Bracing[0].MemberNo,
                Bottom_Chord_Bracing[Bottom_Chord_Bracing.Count - 1].MemberNo));

            //ASTRA_Data.Add(string.Format("_ELEMENT 420 TO 427"));
            ASTRA_Data.Add(string.Format("END GROUP DEFINITION"));
            #endregion END GROUP DEFINITION



            #region Member_Properties
            Hashtable hash_tab = new Hashtable();

            ASTRA_Data.Add("MEMBER PROPERTY");
            for (i = 0; i < _groups.Count; i++)
            {
                ASTRA_Data.Add(string.Format("{0,-10}  PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001", _groups[i], kStr));
            }
            //ASTRA_Data.Add("_BCB      PRI  AX  0.002  IX  0.00001  IY  0.000001  IZ  0.000001");
            //ASTRA_Data.Add("_STRINGER PRI  AX  0.048  IX  0.00001  IY  0.008000  IZ  0.002000");
            //ASTRA_Data.Add("_XGIRDER  PRI  AX  0.059  IX  0.00001  IY  0.009000  IZ  0.005000");
            for (i = 0; i < _groups.Count; i++)
            {
                if (_groups[i].StartsWith("_A") ||
                    _groups[i].StartsWith("_TR") ||
                    _groups[i].StartsWith("_IC") ||
                    _groups[i].StartsWith("_OC"))
                {
                    ASTRA_Data.Add("MEMBER CABLE");
                    ASTRA_Data.Add(_groups[i]);
                }
                else
                {
                    ASTRA_Data.Add("MEMBER TRUSS");
                    ASTRA_Data.Add(_groups[i]);
                }

            }
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_ST");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_TCS_DIA");
            //ASTRA_Data.Add("MEMBER TRUSS");
            //ASTRA_Data.Add("_BCB");
            #endregion Member_Properties
            Get_Support_LOAD_FINISH(Joints_Bottom, ASTRA_Data);

            return ASTRA_Data;
        }
    }
}
