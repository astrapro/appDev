using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using AstraInterface.Interface;
using AstraInterface.TrussBridge;
using AstraInterface.DataStructure;
using AstraFunctionOne.BridgeDesign.Piers;

using AstraFunctionOne.BridgeDesign.SteelTruss;
using BridgeAnalysisDesign.Abutment;
using BridgeAnalysisDesign.Pier;
using BridgeAnalysisDesign.RCC_T_Girder;
using BridgeAnalysisDesign;


namespace LimitStateMethod.TowerDesign
{
    public partial class frm_TransmissionTower : Form
    {
        IApplication iApp = null;

        public frm_TransmissionTower(IApplication app)
        {
            InitializeComponent();
            iApp = app;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CBridgeStructure ca = new CBridgeStructure(Get_TowerData());
            

            TransmissionTower tt = new TransmissionTower();

            double Len = MyList.StringToDouble(txt_Length.Text, 11.844);
            double Height = MyList.StringToDouble(txt_Height.Text, 26.81);


            //Hashtable ht = new Hashtable();

            //object obj = null;
            double min_X = 0.0;

            for (int i = 0; i < ca.Joints.Count; i++)
            {

                //ca.Joints[i].X = tt.Get_X_Coordinates(ca.Joints[i].X, 11.8440);
                //ca.Joints[i].Y = tt.Get_Y_Coordinates(ca.Joints[i].Y, 26.81);
                //ca.Joints[i].Z = tt.Get_Z_Coordinates(ca.Joints[i].Y, 11.8440);



                ca.Joints[i].X = tt.Get_X_Coordinates(ca.Joints[i].X, Len);
                ca.Joints[i].Y = tt.Get_Y_Coordinates(ca.Joints[i].Y, Height);
                ca.Joints[i].Z = tt.Get_Z_Coordinates(ca.Joints[i].Z, Len);


                if (min_X > ca.Joints[i].X) min_X = ca.Joints[i].X;
            }


            foreach (var item in ca.Joints)
            {
                item.X += Math.Abs(min_X);
            }


            List<string> list = new List<string>();
            list.Add(string.Format("ASTRA SPACE ANALYSIS INPUT DATA FILE"));
            list.Add(string.Format("UNIT KN METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            foreach (var item in ca.Joints)
            {
                list.Add(item.ToString());
            }
            list.Add(string.Format("MEMBER INCIDENCES"));
            foreach (var item in ca.Members)
            {
                list.Add(item.ToString());
            }
            list.Add(string.Format("FINISH"));

            string fileName = Path.Combine(iApp.LastDesignWorkingFolder, "TRANSMISSION TOWER");

            Directory.CreateDirectory(fileName);

            fileName = Path.Combine(fileName, "TRANSMISSION_TOWER_ANALYSIS.TXT");

            File.WriteAllLines(fileName, list.ToArray());

            //iApp.View_Input_File(fileName);
            iApp.OpenWork(fileName, false);


        }

        public List<string> Get_TowerData1()
        {
            List<string> list = new List<string>();

            #region List
            list.Add(string.Format("ASTRA SPACE ANALYSIS INPUT DATA FILE"));
            list.Add(string.Format("UNIT KN METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            list.Add(string.Format("1         0.0000     0.0000     0.0000"));
            list.Add(string.Format("2        11.8440     0.0000     0.0000"));
            list.Add(string.Format("3         1.1820     0.7880     0.0000"));
            list.Add(string.Format("4        10.6620     0.7880     0.0000"));
            list.Add(string.Format("5         0.5060     1.8020     0.0000"));
            list.Add(string.Format("6        11.3380     1.8020     0.0000"));
            list.Add(string.Format("7         2.9610     1.9750     0.0000"));
            list.Add(string.Format("8         8.8830     1.9750     0.0000"));
            list.Add(string.Format("9         1.9480     2.6510     0.0000"));
            list.Add(string.Format("10        9.8960     2.6510     0.0000"));
            list.Add(string.Format("11        0.9340     3.3280     0.0000"));
            list.Add(string.Format("12       10.9100     3.3280     0.0000"));
            list.Add(string.Format("13        2.4150     4.3150     0.0000"));
            list.Add(string.Format("14        9.4290     4.3150     0.0000"));
            list.Add(string.Format("15        3.8950     5.3030     0.0000"));
            list.Add(string.Format("16        7.9490     5.3030     0.0000"));
            list.Add(string.Format("17        1.5410     5.4870     0.0000"));
            list.Add(string.Format("18       10.3030     5.4870     0.0000"));
            list.Add(string.Format("19        2.9500     5.9340     0.0000"));
            list.Add(string.Format("20        8.8940     5.9340     0.0000"));
            list.Add(string.Format("21        1.8680     6.6550     0.0000"));
            list.Add(string.Format("22        9.9750     6.6550     0.0000"));
            list.Add(string.Format("23        2.2150     7.8900     0.0000"));
            list.Add(string.Format("24        9.6290     7.8900     0.0000"));
            list.Add(string.Format("25        3.5260     8.4230     0.0000"));
            list.Add(string.Format("26        8.3180     8.4230     0.0000"));
            list.Add(string.Format("27        2.6850     9.5620     0.0000"));
            list.Add(string.Format("28        4.5940     9.5620     0.0000"));
            list.Add(string.Format("29        7.2500     9.5620     0.0000"));
            list.Add(string.Format("30        9.1590     9.5620     0.0000"));
            list.Add(string.Format("31        3.0820    10.9790     0.0000"));
            list.Add(string.Format("32        4.5020    10.9790     0.0000"));
            list.Add(string.Format("33        5.9220    10.9790     0.0000"));
            list.Add(string.Format("34        7.3420    10.9790     0.0000"));
            list.Add(string.Format("35        8.7610    10.9790     0.0000"));
            list.Add(string.Format("36        3.3210    11.8290     0.0000"));
            list.Add(string.Format("37        8.5230    11.8290     0.0000"));
            list.Add(string.Format("38        4.6050    12.3840     0.0000"));
            list.Add(string.Format("39        7.2390    12.3840     0.0000"));
            list.Add(string.Format("40       -0.7940    13.3100     0.0000"));
            list.Add(string.Format("41       -0.0210    13.3100     0.0000"));
            list.Add(string.Format("42        0.4220    13.3100     0.0000"));
            list.Add(string.Format("43        0.9250    13.3100     0.0000"));
            list.Add(string.Format("44        1.5060    13.3100     0.0000"));
            list.Add(string.Format("45        2.2940    13.3100     0.0000"));
            list.Add(string.Format("46        3.0820    13.3100     0.0000"));
            list.Add(string.Format("47        3.7370    13.3100     0.0000"));
            list.Add(string.Format("48        5.9220    13.3100     0.0000"));
            list.Add(string.Format("49        8.1070    13.3100     0.0000"));
            list.Add(string.Format("50        8.7620    13.3100     0.0000"));
            list.Add(string.Format("51        9.5490    13.3100     0.0000"));
            list.Add(string.Format("52       10.3370    13.3100     0.0000"));
            list.Add(string.Format("53       10.9190    13.3100     0.0000"));
            list.Add(string.Format("54       11.4220    13.3100     0.0000"));
            list.Add(string.Format("55       11.8650    13.3100     0.0000"));
            list.Add(string.Format("56       12.6380    13.3100     0.0000"));
            list.Add(string.Format("57       -0.0210    13.5710     0.0000"));
            list.Add(string.Format("58       11.8650    13.5710     0.0000"));
            list.Add(string.Format("59        0.4220    13.7200     0.0000"));
            list.Add(string.Format("60       11.4220    13.7200     0.0000"));
            list.Add(string.Format("61        0.9250    13.8890     0.0000"));
            list.Add(string.Format("62       10.9190    13.8890     0.0000"));
            list.Add(string.Format("63        1.5060    14.0850     0.0000"));
            list.Add(string.Format("64        3.7720    14.0850     0.0000"));
            list.Add(string.Format("65        4.8640    14.0850     0.0000"));
            list.Add(string.Format("66        6.9790    14.0850     0.0000"));
            list.Add(string.Format("67        8.0720    14.0850     0.0000"));
            list.Add(string.Format("68       10.3370    14.0850     0.0000"));
            list.Add(string.Format("69        2.2940    14.3500     0.0000"));
            list.Add(string.Format("70        9.5490    14.3500     0.0000"));
            list.Add(string.Format("71        3.0820    14.6160     0.0000"));
            list.Add(string.Format("72        8.7620    14.6160     0.0000"));
            list.Add(string.Format("73        3.8070    14.8600     0.0000"));
            list.Add(string.Format("74        5.9220    14.8600     0.0000"));
            list.Add(string.Format("75        8.0370    14.8600     0.0000"));
            list.Add(string.Format("76        3.9070    15.5620     0.0000"));
            list.Add(string.Format("77        4.9190    15.5620     0.0000"));
            list.Add(string.Format("78        6.9250    15.5620     0.0000"));
            list.Add(string.Format("79        7.9370    15.5620     0.0000"));
            list.Add(string.Format("80        3.9980    16.1960     0.0000"));
            list.Add(string.Format("81        5.9220    16.1960     0.0000"));
            list.Add(string.Format("82        7.8460    16.1960     0.0000"));
            list.Add(string.Format("83        4.0880    16.8290     0.0000"));
            list.Add(string.Format("84        4.9190    16.8290     0.0000"));
            list.Add(string.Format("85        6.9250    16.8290     0.0000"));
            list.Add(string.Format("86        7.7560    16.8290     0.0000"));
            list.Add(string.Format("87        0.1770    17.3100     0.0000"));
            list.Add(string.Format("88        1.0940    17.3100     0.0000"));
            list.Add(string.Format("89        1.4920    17.3100     0.0000"));
            list.Add(string.Format("90        1.9300    17.3100     0.0000"));
            list.Add(string.Format("91        2.4630    17.3100     0.0000"));
            list.Add(string.Format("92        3.0430    17.3100     0.0000"));
            list.Add(string.Format("93        3.6860    17.3100     0.0000"));
            list.Add(string.Format("94        4.1570    17.3100     0.0000"));
            list.Add(string.Format("95        7.6870    17.3100     0.0000"));
            list.Add(string.Format("96        8.1570    17.3100     0.0000"));
            list.Add(string.Format("97        8.8000    17.3100     0.0000"));
            list.Add(string.Format("98        9.3810    17.3100     0.0000"));
            list.Add(string.Format("99        9.9140    17.3100     0.0000"));
            list.Add(string.Format("100      10.3510    17.3100     0.0000"));
            list.Add(string.Format("101      10.7500    17.3100     0.0000"));
            list.Add(string.Format("102      11.6670    17.3100     0.0000"));
            list.Add(string.Format("103       1.0940    17.6460     0.0000"));
            list.Add(string.Format("104      10.7500    17.6460     0.0000"));
            list.Add(string.Format("105       1.4920    17.7920     0.0000"));
            list.Add(string.Format("106      10.3510    17.7920     0.0000"));
            list.Add(string.Format("107       1.9300    17.9520     0.0000"));
            list.Add(string.Format("108       9.9140    17.9520     0.0000"));
            list.Add(string.Format("109       4.2880    18.1100     0.0000"));
            list.Add(string.Format("110       5.0680    18.1100     0.0000"));
            list.Add(string.Format("111       6.7750    18.1100     0.0000"));
            list.Add(string.Format("112       7.5560    18.1100     0.0000"));
            list.Add(string.Format("113       2.4630    18.1470     0.0000"));
            list.Add(string.Format("114       9.3810    18.1470     0.0000"));
            list.Add(string.Format("115       3.0430    18.3600     0.0000"));
            list.Add(string.Format("116       8.8000    18.3600     0.0000"));
            list.Add(string.Format("117       3.6860    18.5950     0.0000"));
            list.Add(string.Format("118       8.1570    18.5950     0.0000"));
            list.Add(string.Format("119       4.4100    18.8600     0.0000"));
            list.Add(string.Format("120       5.9220    18.8600     0.0000"));
            list.Add(string.Format("121       7.4340    18.8600     0.0000"));
            list.Add(string.Format("122       0.8630    20.3100     0.0000"));
            list.Add(string.Format("123       1.7280    20.3100     0.0000"));
            list.Add(string.Format("124       2.2230    20.3100     0.0000"));
            list.Add(string.Format("125       2.7920    20.3100     0.0000"));
            list.Add(string.Format("126       3.4540    20.3100     0.0000"));
            list.Add(string.Format("127       4.1210    20.3100     0.0000"));
            list.Add(string.Format("128       4.6470    20.3100     0.0000"));
            list.Add(string.Format("129       5.9220    20.3100     0.0000"));
            list.Add(string.Format("130       7.1970    20.3100     0.0000"));
            list.Add(string.Format("131       7.7230    20.3100     0.0000"));
            list.Add(string.Format("132       8.3900    20.3100     0.0000"));
            list.Add(string.Format("133       9.0510    20.3100     0.0000"));
            list.Add(string.Format("134       9.6210    20.3100     0.0000"));
            list.Add(string.Format("135      10.1160    20.3100     0.0000"));
            list.Add(string.Format("136      10.9810    20.3100     0.0000"));
            list.Add(string.Format("137       1.7280    20.6580     0.0000"));
            list.Add(string.Format("138      10.1160    20.6580     0.0000"));
            list.Add(string.Format("139       2.2230    20.8560     0.0000"));
            list.Add(string.Format("140       9.6210    20.8560     0.0000"));
            list.Add(string.Format("141       2.7920    21.0850     0.0000"));
            list.Add(string.Format("142       9.0510    21.0850     0.0000"));
            list.Add(string.Format("143       5.9220    21.1080     0.0000"));
            list.Add(string.Format("144       3.4540    21.3510     0.0000"));
            list.Add(string.Format("145       8.3900    21.3510     0.0000"));
            list.Add(string.Format("146       4.1210    21.6190     0.0000"));
            list.Add(string.Format("147       7.7230    21.6190     0.0000"));
            list.Add(string.Format("148       4.7220    21.8600     0.0000"));
            list.Add(string.Format("149       7.1220    21.8600     0.0000"));
            list.Add(string.Format("150       6.8830    22.8460     0.0000"));
            list.Add(string.Format("151       5.1860    23.6850     0.0000"));
            list.Add(string.Format("152       6.5300    24.2560     0.0000"));
            list.Add(string.Format("153       5.4700    24.9000     0.0000"));
            list.Add(string.Format("154       6.3100    25.2380     0.0000"));
            list.Add(string.Format("155       5.6900    25.8120     0.0000"));
            list.Add(string.Format("156       5.9220    26.8100     0.0000"));
            list.Add(string.Format("MEMBER INCIDENCES"));
            list.Add(string.Format("1             41         57"));
            list.Add(string.Format("2             42         59"));
            list.Add(string.Format("3             43         61"));
            list.Add(string.Format("4             44         63"));
            list.Add(string.Format("5             45         69"));
            list.Add(string.Format("6             46         71"));
            list.Add(string.Format("7             50         72"));
            list.Add(string.Format("8             51         70"));
            list.Add(string.Format("9             52         68"));
            list.Add(string.Format("10            53         62"));
            list.Add(string.Format("11            54         60"));
            list.Add(string.Format("12            55         58"));
            list.Add(string.Format("13            88        103"));
            list.Add(string.Format("14            89        105"));
            list.Add(string.Format("15            90        107"));
            list.Add(string.Format("16            91        113"));
            list.Add(string.Format("17            92        115"));
            list.Add(string.Format("18            93        117"));
            list.Add(string.Format("19            96        118"));
            list.Add(string.Format("20            97        116"));
            list.Add(string.Format("21            98        114"));
            list.Add(string.Format("22            99        108"));
            list.Add(string.Format("23           100        106"));
            list.Add(string.Format("24           101        104"));
            list.Add(string.Format("25           123        137"));
            list.Add(string.Format("26           124        139"));
            list.Add(string.Format("27           125        141"));
            list.Add(string.Format("28           126        144"));
            list.Add(string.Format("29           127        146"));
            list.Add(string.Format("30           129        143"));
            list.Add(string.Format("31           131        147"));
            list.Add(string.Format("32           132        145"));
            list.Add(string.Format("33           133        142"));
            list.Add(string.Format("34           134        140"));
            list.Add(string.Format("35           135        138"));
            list.Add(string.Format("36             1         22"));
            list.Add(string.Format("37             1         47"));
            list.Add(string.Format("38             2         21"));
            list.Add(string.Format("39             2         49"));
            list.Add(string.Format("40             3          5"));
            list.Add(string.Format("41             3          9"));
            list.Add(string.Format("42             4          6"));
            list.Add(string.Format("43             4         10"));
            list.Add(string.Format("44             5          9"));
            list.Add(string.Format("45             6         10"));
            list.Add(string.Format("46             7         11"));
            list.Add(string.Format("47             7         15"));
            list.Add(string.Format("48             8         12"));
            list.Add(string.Format("49             8         16"));
            list.Add(string.Format("50            11         15"));
            list.Add(string.Format("51            12         16"));
            list.Add(string.Format("52            13         17"));
            list.Add(string.Format("53            13         19"));
            list.Add(string.Format("54            14         18"));
            list.Add(string.Format("55            14         20"));
            list.Add(string.Format("56            17         19"));
            list.Add(string.Format("57            18         20"));
            list.Add(string.Format("58            21         49"));
            list.Add(string.Format("59            22         47"));
            list.Add(string.Format("60            23         25"));
            list.Add(string.Format("61            24         26"));
            list.Add(string.Format("62            25         27"));
            list.Add(string.Format("63            26         30"));
            list.Add(string.Format("64            28         31"));
            list.Add(string.Format("65            29         35"));
            list.Add(string.Format("66            32         36"));
            list.Add(string.Format("67            32         38"));
            list.Add(string.Format("68            34         37"));
            list.Add(string.Format("69            34         39"));
            list.Add(string.Format("70            36         38"));
            list.Add(string.Format("71            37         39"));
            list.Add(string.Format("72            40         73"));
            list.Add(string.Format("73            42         57"));
            list.Add(string.Format("74            43         59"));
            list.Add(string.Format("75            44         61"));
            list.Add(string.Format("76            45         63"));
            list.Add(string.Format("77            46         69"));
            list.Add(string.Format("78            47         71"));
            list.Add(string.Format("79            47         73"));
            list.Add(string.Format("80            47         74"));
            list.Add(string.Format("81            48         73"));
            list.Add(string.Format("82            48         75"));
            list.Add(string.Format("83            49         72"));
            list.Add(string.Format("84            49         74"));
            list.Add(string.Format("85            49         75"));
            list.Add(string.Format("86            50         70"));
            list.Add(string.Format("87            51         68"));
            list.Add(string.Format("88            52         62"));
            list.Add(string.Format("89            53         60"));
            list.Add(string.Format("90            54         58"));
            list.Add(string.Format("91            56         75"));
            list.Add(string.Format("92            73         94"));
            list.Add(string.Format("93            73         95"));
            list.Add(string.Format("94            75         94"));
            list.Add(string.Format("95            75         95"));
            list.Add(string.Format("96            77         80"));
            list.Add(string.Format("97            78         82"));
            list.Add(string.Format("98            80         84"));
            list.Add(string.Format("99            82         85"));
            list.Add(string.Format("100           87        119"));
            list.Add(string.Format("101           89        103"));
            list.Add(string.Format("102           90        105"));
            list.Add(string.Format("103           91        107"));
            list.Add(string.Format("104           92        113"));
            list.Add(string.Format("105           93        115"));
            list.Add(string.Format("106           94        117"));
            list.Add(string.Format("107           94        120"));
            list.Add(string.Format("108           94        128"));
            list.Add(string.Format("109           95        118"));
            list.Add(string.Format("110           95        120"));
            list.Add(string.Format("111           95        130"));
            list.Add(string.Format("112           96        116"));
            list.Add(string.Format("113           97        114"));
            list.Add(string.Format("114           98        108"));
            list.Add(string.Format("115           99        106"));
            list.Add(string.Format("116          100        104"));
            list.Add(string.Format("117          102        121"));
            list.Add(string.Format("118          110        119"));
            list.Add(string.Format("119          111        121"));
            list.Add(string.Format("120          122        148"));
            list.Add(string.Format("121          124        137"));
            list.Add(string.Format("122          125        139"));
            list.Add(string.Format("123          126        141"));
            list.Add(string.Format("124          127        144"));
            list.Add(string.Format("125          128        146"));
            list.Add(string.Format("126          128        148"));
            list.Add(string.Format("127          128        149"));
            list.Add(string.Format("128          130        147"));
            list.Add(string.Format("129          130        148"));
            list.Add(string.Format("130          130        149"));
            list.Add(string.Format("131          131        145"));
            list.Add(string.Format("132          132        142"));
            list.Add(string.Format("133          133        140"));
            list.Add(string.Format("134          134        138"));
            list.Add(string.Format("135          136        149"));
            list.Add(string.Format("136          148        150"));
            list.Add(string.Format("137          148        156"));
            list.Add(string.Format("138          149        156"));
            list.Add(string.Format("139          150        151"));
            list.Add(string.Format("140          151        152"));
            list.Add(string.Format("141          152        153"));
            list.Add(string.Format("142          153        154"));
            list.Add(string.Format("143          154        155"));
            list.Add(string.Format("144           27         28"));
            list.Add(string.Format("145           29         30"));
            list.Add(string.Format("146           31         33"));
            list.Add(string.Format("147           33         35"));
            list.Add(string.Format("148           40         47"));
            list.Add(string.Format("149           47         49"));
            list.Add(string.Format("150           49         56"));
            list.Add(string.Format("151           64         65"));
            list.Add(string.Format("152           66         67"));
            list.Add(string.Format("153           73         75"));
            list.Add(string.Format("154           76         77"));
            list.Add(string.Format("155           78         79"));
            list.Add(string.Format("156           80         81"));
            list.Add(string.Format("157           83         84"));
            list.Add(string.Format("158           85         86"));
            list.Add(string.Format("159           87         94"));
            list.Add(string.Format("160           94         95"));
            list.Add(string.Format("161           95        102"));
            list.Add(string.Format("162          109        110"));
            list.Add(string.Format("163          111        112"));
            list.Add(string.Format("164          119        121"));
            list.Add(string.Format("165          122        128"));
            list.Add(string.Format("166          128        130"));
            list.Add(string.Format("167          130        136"));
            list.Add(string.Format("168          148        149"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            #endregion List

            return list;
        }

        public List<string> Get_TowerData()
        {
            string file_name = Path.Combine(Application.StartupPath, @"DESIGN\Transmission Tower\Model Input Data.txt");

            if (File.Exists(file_name)) return new List<string>(File.ReadAllLines(file_name));

            List<string> list = new List<string>();

            #region List
            list.Add(string.Format("ASTRA SPACE ANALYSIS INPUT DATA FILE"));
            list.Add(string.Format("UNIT KN METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            list.Add(string.Format("1         0.0000     0.0000     0.0000"));
            list.Add(string.Format("2        11.8440     0.0000     0.0000"));
            list.Add(string.Format("3         1.1820     0.7880     0.0000"));
            list.Add(string.Format("4        10.6620     0.7880     0.0000"));
            list.Add(string.Format("5         0.5060     1.8020     0.0000"));
            list.Add(string.Format("6        11.3380     1.8020     0.0000"));
            list.Add(string.Format("7         2.9610     1.9750     0.0000"));
            list.Add(string.Format("8         8.8830     1.9750     0.0000"));
            list.Add(string.Format("9         1.9480     2.6510     0.0000"));
            list.Add(string.Format("10        9.8960     2.6510     0.0000"));
            list.Add(string.Format("11        0.9340     3.3280     0.0000"));
            list.Add(string.Format("12       10.9100     3.3280     0.0000"));
            list.Add(string.Format("13        5.9190     3.9530     0.0000"));
            list.Add(string.Format("14        2.4150     4.3150     0.0000"));
            list.Add(string.Format("15        9.4290     4.3150     0.0000"));
            list.Add(string.Format("16        3.8950     5.3030     0.0000"));
            list.Add(string.Format("17        7.9490     5.3030     0.0000"));
            list.Add(string.Format("18        1.5410     5.4870     0.0000"));
            list.Add(string.Format("19       10.3030     5.4870     0.0000"));
            list.Add(string.Format("20        2.9500     5.9340     0.0000"));
            list.Add(string.Format("21        8.8940     5.9340     0.0000"));
            list.Add(string.Format("22        1.8680     6.6550     0.0000"));
            list.Add(string.Format("23        9.9750     6.6550     0.0000"));
            list.Add(string.Format("24        2.2150     7.8900     0.0000"));
            list.Add(string.Format("25        9.6290     7.8900     0.0000"));
            list.Add(string.Format("26        3.5260     8.4230     0.0000"));
            list.Add(string.Format("27        8.3180     8.4230     0.0000"));
            list.Add(string.Format("28        2.6850     9.5620     0.0000"));
            list.Add(string.Format("29        4.5940     9.5620     0.0000"));
            list.Add(string.Format("30        7.2500     9.5620     0.0000"));
            list.Add(string.Format("31        9.1590     9.5620     0.0000"));
            list.Add(string.Format("32        3.0820    10.9790     0.0000"));
            list.Add(string.Format("33        4.5020    10.9790     0.0000"));
            list.Add(string.Format("34        5.9220    10.9790     0.0000"));
            list.Add(string.Format("35        7.3420    10.9790     0.0000"));
            list.Add(string.Format("36        8.7610    10.9790     0.0000"));
            list.Add(string.Format("37        3.3210    11.8290     0.0000"));
            list.Add(string.Format("38        8.5230    11.8290     0.0000"));
            list.Add(string.Format("39        4.6050    12.3840     0.0000"));
            list.Add(string.Format("40        7.2390    12.3840     0.0000"));
            list.Add(string.Format("41        3.7370    13.3100     0.0000"));
            list.Add(string.Format("42        5.9220    13.3100     0.0000"));
            list.Add(string.Format("43        8.1070    13.3100     0.0000"));
            list.Add(string.Format("44        3.7720    14.0850     0.0000"));
            list.Add(string.Format("45        4.8640    14.0850     0.0000"));
            list.Add(string.Format("46        6.9790    14.0850     0.0000"));
            list.Add(string.Format("47        8.0720    14.0850     0.0000"));
            list.Add(string.Format("48        3.8070    14.8600     0.0000"));
            list.Add(string.Format("49        5.9220    14.8600     0.0000"));
            list.Add(string.Format("50        8.0370    14.8600     0.0000"));
            list.Add(string.Format("51        3.9070    15.5620     0.0000"));
            list.Add(string.Format("52        4.9190    15.5620     0.0000"));
            list.Add(string.Format("53        6.9250    15.5620     0.0000"));
            list.Add(string.Format("54        7.9370    15.5620     0.0000"));
            list.Add(string.Format("55        3.9980    16.1960     0.0000"));
            list.Add(string.Format("56        5.9220    16.1960     0.0000"));
            list.Add(string.Format("57        7.8460    16.1960     0.0000"));
            list.Add(string.Format("58        4.0880    16.8290     0.0000"));
            list.Add(string.Format("59        4.9190    16.8290     0.0000"));
            list.Add(string.Format("60        6.9250    16.8290     0.0000"));
            list.Add(string.Format("61        7.7560    16.8290     0.0000"));
            list.Add(string.Format("62        4.1570    17.3100     0.0000"));
            list.Add(string.Format("63        7.6870    17.3100     0.0000"));
            list.Add(string.Format("64        4.2880    18.1100     0.0000"));
            list.Add(string.Format("65        5.0680    18.1100     0.0000"));
            list.Add(string.Format("66        6.7750    18.1100     0.0000"));
            list.Add(string.Format("67        7.5560    18.1100     0.0000"));
            list.Add(string.Format("68        4.4100    18.8600     0.0000"));
            list.Add(string.Format("69        5.9220    18.8600     0.0000"));
            list.Add(string.Format("70        7.4340    18.8600     0.0000"));
            list.Add(string.Format("71        4.6470    20.3100     0.0000"));
            list.Add(string.Format("72        5.9220    20.3100     0.0000"));
            list.Add(string.Format("73        7.1970    20.3100     0.0000"));
            list.Add(string.Format("74        5.9220    21.1080     0.0000"));
            list.Add(string.Format("75        4.7220    21.8600     0.0000"));
            list.Add(string.Format("76        7.1220    21.8600     0.0000"));
            list.Add(string.Format("77        6.8830    22.8460     0.0000"));
            list.Add(string.Format("78        5.1860    23.6850     0.0000"));
            list.Add(string.Format("79        6.5300    24.2560     0.0000"));
            list.Add(string.Format("80        5.4700    24.9000     0.0000"));
            list.Add(string.Format("81        6.3100    25.2380     0.0000"));
            list.Add(string.Format("82        5.6900    25.8120     0.0000"));
            list.Add(string.Format("83        5.9220    26.8100     0.0000"));
            list.Add(string.Format("MEMBER INCIDENCES"));
            list.Add(string.Format("1             72         74"));
            list.Add(string.Format("2              1          3"));
            list.Add(string.Format("3              1          5"));
            list.Add(string.Format("4              2          4"));
            list.Add(string.Format("5              2          6"));
            list.Add(string.Format("6              3          5"));
            list.Add(string.Format("7              3          7"));
            list.Add(string.Format("8              3          9"));
            list.Add(string.Format("9              4          6"));
            list.Add(string.Format("10             4          8"));
            list.Add(string.Format("11             4         10"));
            list.Add(string.Format("12             5          9"));
            list.Add(string.Format("13             5         11"));
            list.Add(string.Format("14             6         10"));
            list.Add(string.Format("15             6         12"));
            list.Add(string.Format("16             7          9"));
            list.Add(string.Format("17             7         13"));
            list.Add(string.Format("18             7         16"));
            list.Add(string.Format("19             8         12"));
            list.Add(string.Format("20             8         13"));
            list.Add(string.Format("21             8         17"));
            list.Add(string.Format("22             9         11"));
            list.Add(string.Format("23            11         14"));
            list.Add(string.Format("24            11         18"));
            list.Add(string.Format("25            12         17"));
            list.Add(string.Format("26            12         19"));
            list.Add(string.Format("27            13         16"));
            list.Add(string.Format("28            13         17"));
            list.Add(string.Format("29            14         16"));
            list.Add(string.Format("30            14         18"));
            list.Add(string.Format("31            14         20"));
            list.Add(string.Format("32            15         19"));
            list.Add(string.Format("33            15         21"));
            list.Add(string.Format("34            16         20"));
            list.Add(string.Format("35            17         23"));
            list.Add(string.Format("36            18         20"));
            list.Add(string.Format("37            18         22"));
            list.Add(string.Format("38            19         21"));
            list.Add(string.Format("39            19         23"));
            list.Add(string.Format("40            20         22"));
            list.Add(string.Format("41            22         24"));
            list.Add(string.Format("42            22         26"));
            list.Add(string.Format("43            23         25"));
            list.Add(string.Format("44            23         27"));
            list.Add(string.Format("45            24         26"));
            list.Add(string.Format("46            24         28"));
            list.Add(string.Format("47            25         27"));
            list.Add(string.Format("48            25         31"));
            list.Add(string.Format("49            26         28"));
            list.Add(string.Format("50            26         29"));
            list.Add(string.Format("51            27         30"));
            list.Add(string.Format("52            27         31"));
            list.Add(string.Format("53            28         32"));
            list.Add(string.Format("54            29         32"));
            list.Add(string.Format("55            29         34"));
            list.Add(string.Format("56            30         34"));
            list.Add(string.Format("57            30         36"));
            list.Add(string.Format("58            31         36"));
            list.Add(string.Format("59            32         37"));
            list.Add(string.Format("60            33         37"));
            list.Add(string.Format("61            33         39"));
            list.Add(string.Format("62            34         39"));
            list.Add(string.Format("63            34         40"));
            list.Add(string.Format("64            35         38"));
            list.Add(string.Format("65            35         40"));
            list.Add(string.Format("66            36         38"));
            list.Add(string.Format("67            37         39"));
            list.Add(string.Format("68            37         41"));
            list.Add(string.Format("69            38         40"));
            list.Add(string.Format("70            38         43"));
            list.Add(string.Format("71            39         41"));
            list.Add(string.Format("72            40         43"));
            list.Add(string.Format("73            41         44"));
            list.Add(string.Format("74            41         45"));
            list.Add(string.Format("75            42         45"));
            list.Add(string.Format("76            42         46"));
            list.Add(string.Format("77            43         46"));
            list.Add(string.Format("78            43         47"));
            list.Add(string.Format("79            44         48"));
            list.Add(string.Format("80            45         48"));
            list.Add(string.Format("81            45         49"));
            list.Add(string.Format("82            46         49"));
            list.Add(string.Format("83            46         50"));
            list.Add(string.Format("84            47         50"));
            list.Add(string.Format("85            48         51"));
            list.Add(string.Format("86            48         52"));
            list.Add(string.Format("87            50         53"));
            list.Add(string.Format("88            50         54"));
            list.Add(string.Format("89            51         55"));
            list.Add(string.Format("90            52         55"));
            list.Add(string.Format("91            52         56"));
            list.Add(string.Format("92            53         56"));
            list.Add(string.Format("93            53         57"));
            list.Add(string.Format("94            54         57"));
            list.Add(string.Format("95            55         58"));
            list.Add(string.Format("96            55         59"));
            list.Add(string.Format("97            56         59"));
            list.Add(string.Format("98            56         60"));
            list.Add(string.Format("99            57         60"));
            list.Add(string.Format("100           57         61"));
            list.Add(string.Format("101           58         62"));
            list.Add(string.Format("102           59         62"));
            list.Add(string.Format("103           60         63"));
            list.Add(string.Format("104           61         63"));
            list.Add(string.Format("105           62         64"));
            list.Add(string.Format("106           62         65"));
            list.Add(string.Format("107           63         66"));
            list.Add(string.Format("108           63         67"));
            list.Add(string.Format("109           64         68"));
            list.Add(string.Format("110           65         68"));
            list.Add(string.Format("111           65         69"));
            list.Add(string.Format("112           66         69"));
            list.Add(string.Format("113           66         70"));
            list.Add(string.Format("114           67         70"));
            list.Add(string.Format("115           68         71"));
            list.Add(string.Format("116           70         73"));
            list.Add(string.Format("117           71         74"));
            list.Add(string.Format("118           71         75"));
            list.Add(string.Format("119           73         74"));
            list.Add(string.Format("120           73         76"));
            list.Add(string.Format("121           74         75"));
            list.Add(string.Format("122           74         76"));
            list.Add(string.Format("123           75         77"));
            list.Add(string.Format("124           75         78"));
            list.Add(string.Format("125           76         77"));
            list.Add(string.Format("126           77         78"));
            list.Add(string.Format("127           77         79"));
            list.Add(string.Format("128           78         79"));
            list.Add(string.Format("129           78         80"));
            list.Add(string.Format("130           79         80"));
            list.Add(string.Format("131           79         81"));
            list.Add(string.Format("132           80         81"));
            list.Add(string.Format("133           80         82"));
            list.Add(string.Format("134           81         82"));
            list.Add(string.Format("135           81         83"));
            list.Add(string.Format("136           82         83"));
            list.Add(string.Format("137           28         29"));
            list.Add(string.Format("138           30         31"));
            list.Add(string.Format("139           32         33"));
            list.Add(string.Format("140           33         34"));
            list.Add(string.Format("141           34         35"));
            list.Add(string.Format("142           35         36"));
            list.Add(string.Format("143           41         42"));
            list.Add(string.Format("144           42         43"));
            list.Add(string.Format("145           44         45"));
            list.Add(string.Format("146           46         47"));
            list.Add(string.Format("147           48         49"));
            list.Add(string.Format("148           49         50"));
            list.Add(string.Format("149           51         52"));
            list.Add(string.Format("150           53         54"));
            list.Add(string.Format("151           55         56"));
            list.Add(string.Format("152           56         57"));
            list.Add(string.Format("153           58         59"));
            list.Add(string.Format("154           60         61"));
            list.Add(string.Format("155           62         63"));
            list.Add(string.Format("156           64         65"));
            list.Add(string.Format("157           66         67"));
            list.Add(string.Format("158           68         69"));
            list.Add(string.Format("159           69         70"));
            list.Add(string.Format("160           71         72"));
            list.Add(string.Format("161           72         73"));
            list.Add(string.Format("162           75         76"));
            list.Add(string.Format("163           23         23"));
            list.Add(string.Format("164           50         50"));
            list.Add(string.Format("165           69         69"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion List

            return list;
        }
    }

    public class TransmissionTower
    {
        
        public TransmissionTower()
        {
            Length = 50.0;
            Breadth = 5.40;
            Height = 6.0;
            Is_Add_TCB_ST = true;
            Is_Add_TCB_DIA = true;
            Is_Add_BCB = true;


            Initialize();
        }


        public string Start_Support { get; set; }
        public string End_Support { get; set; }

        public bool Is_Add_TCB_ST { get; set; }
        public bool Is_Add_TCB_DIA { get; set; }
        public bool Is_Add_BCB { get; set; }

        public double Length { get; set; }
        public double PanelLength
        {
            get
            {
                return (Length / (double)NoOfPanel);
            }
        }
        public double Breadth { get; set; }
        //Default 3
        public int NoOfStringerBeam { get; set; }

        public string HA_Loading_Members { get; set; }

        public void Initialize()
        {
            #region Variable Declarations
            Member mbr = new Member();
            Cross_Girders = new MemberCollection();
            Stringers = new MemberCollection();
            Bottom_Chord1 = new MemberCollection();
            Bottom_Chord2 = new MemberCollection();
            Bottom_Chord_Bracing = new MemberCollection();
            Top_Chord1 = new MemberCollection();
            Top_Chord2 = new MemberCollection();
            Top_Chord_Bracing = new MemberCollection();
            Top_Chord_Bracing_Diagonal = new MemberCollection();
            Diagonal1 = new MemberCollection();
            Diagonal2 = new MemberCollection();
            Vertical1 = new MemberCollection();
            Vertical2 = new MemberCollection();
            EndRakers = new MemberCollection();





            Vertical_Top1 = new MemberCollection();
            Vertical_Bottom1 = new MemberCollection();
            Diagonal_Top1 = new MemberCollection();
            Diagonal_Bottom1 = new MemberCollection();



            Vertical_Top2 = new MemberCollection();
            Vertical_Bottom2 = new MemberCollection();
            Diagonal_Top2 = new MemberCollection();
            Diagonal_Bottom2 = new MemberCollection();


            #endregion Variable Declarations
        }
        #region Chiranjit [2013 11 18] Members Declarations
        Member mbr = new Member();
        public MemberCollection Cross_Girders { get; set; }
        public MemberCollection Stringers { get; set; }
        public MemberCollection Bottom_Chord1 { get; set; }
        public MemberCollection Bottom_Chord2 { get; set; }
        public MemberCollection Bottom_Chord_Bracing { get; set; }
        public MemberCollection Top_Chord1 { get; set; }
        public MemberCollection Top_Chord2 { get; set; }
        public MemberCollection Top_Chord_Bracing { get; set; }
        public MemberCollection Top_Chord_Bracing_Diagonal { get; set; }
        public MemberCollection Diagonal1 { get; set; }
        public MemberCollection Diagonal2 { get; set; }
        public MemberCollection Vertical1 { get; set; }
        public MemberCollection Vertical2 { get; set; }
        public MemberCollection EndRakers { get; set; }
        public string Joint_Load_Support { get; set; }
        public string Joint_Load_Edge { get; set; }







        public MemberCollection Vertical_Top1 { get; set; }
        public MemberCollection Vertical_Bottom1 { get; set; }
        public MemberCollection Diagonal_Top1 { get; set; }
        public MemberCollection Diagonal_Bottom1 { get; set; }



        public MemberCollection Vertical_Top2 { get; set; }
        public MemberCollection Vertical_Bottom2 { get; set; }
        public MemberCollection Diagonal_Top2 { get; set; }
        public MemberCollection Diagonal_Bottom2 { get; set; }




        #endregion Chiranjit [2013 11 18] Members Declarations

        public double StringerSpacing
        {
            get
            {
                return (Breadth / (NoOfStringerBeam + 1));
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
                list.Add("ASTRA SPACE STEEL TRUSS K TYPE BRIDGE");
                list.Add("UNIT KN MET");
                //list.AddRange(GetJointCoordinates());
                //list.AddRange(Get_Bridge_Data_Copy_2012_01_25(""));
                list.AddRange(Get_Bridge_Data("ASTRA"));
                //list.AddRange(Get_MyntduBridge_Data());
                //list.AddRange(GetMemberConnectivityAndOthers());
                File.WriteAllLines(fileName, list.ToArray());

                Path.GetDirectoryName(fileName);
                return true;
            }
            catch (Exception ex) { }
            return false;
        }
        public double Get_Y_Coordinates(double val, double MaxHeight)
        {
            double y = 0.0;

            double maxVal = 26.81;

            y = MaxHeight * val / maxVal;

            return y;

            #region List Y Coordinate
            List<string> list = new List<string>();

            list.Add(string.Format("0"));
            list.Add(string.Format("0"));
            list.Add(string.Format("0.788"));
            list.Add(string.Format("0.788"));
            list.Add(string.Format("1.802"));
            list.Add(string.Format("1.802"));
            list.Add(string.Format("1.975"));
            list.Add(string.Format("1.975"));
            list.Add(string.Format("2.651"));
            list.Add(string.Format("2.651"));
            list.Add(string.Format("3.328"));
            list.Add(string.Format("3.328"));
            list.Add(string.Format("4.315"));
            list.Add(string.Format("4.315"));
            list.Add(string.Format("5.303"));
            list.Add(string.Format("5.303"));
            list.Add(string.Format("5.487"));
            list.Add(string.Format("5.487"));
            list.Add(string.Format("5.934"));
            list.Add(string.Format("5.934"));
            list.Add(string.Format("6.655"));
            list.Add(string.Format("6.655"));
            list.Add(string.Format("7.89"));
            list.Add(string.Format("7.89"));
            list.Add(string.Format("8.423"));
            list.Add(string.Format("8.423"));
            list.Add(string.Format("9.562"));
            list.Add(string.Format("9.562"));
            list.Add(string.Format("9.562"));
            list.Add(string.Format("9.562"));
            list.Add(string.Format("10.979"));
            list.Add(string.Format("10.979"));
            list.Add(string.Format("10.979"));
            list.Add(string.Format("10.979"));
            list.Add(string.Format("10.979"));
            list.Add(string.Format("11.829"));
            list.Add(string.Format("11.829"));
            list.Add(string.Format("12.384"));
            list.Add(string.Format("12.384"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.31"));
            list.Add(string.Format("13.571"));
            list.Add(string.Format("13.571"));
            list.Add(string.Format("13.72"));
            list.Add(string.Format("13.72"));
            list.Add(string.Format("13.889"));
            list.Add(string.Format("13.889"));
            list.Add(string.Format("14.085"));
            list.Add(string.Format("14.085"));
            list.Add(string.Format("14.085"));
            list.Add(string.Format("14.085"));
            list.Add(string.Format("14.085"));
            list.Add(string.Format("14.085"));
            list.Add(string.Format("14.35"));
            list.Add(string.Format("14.35"));
            list.Add(string.Format("14.616"));
            list.Add(string.Format("14.616"));
            list.Add(string.Format("14.86"));
            list.Add(string.Format("14.86"));
            list.Add(string.Format("14.86"));
            list.Add(string.Format("15.562"));
            list.Add(string.Format("15.562"));
            list.Add(string.Format("15.562"));
            list.Add(string.Format("15.562"));
            list.Add(string.Format("16.196"));
            list.Add(string.Format("16.196"));
            list.Add(string.Format("16.196"));
            list.Add(string.Format("16.829"));
            list.Add(string.Format("16.829"));
            list.Add(string.Format("16.829"));
            list.Add(string.Format("16.829"));
            list.Add(string.Format("17.31"));
            list.Add(string.Format("17.31"));
            list.Add(string.Format("17.31"));
            list.Add(string.Format("17.31"));
            list.Add(string.Format("17.31"));
            list.Add(string.Format("17.31"));
            list.Add(string.Format("17.31"));
            list.Add(string.Format("17.31"));
            list.Add(string.Format("17.31"));
            list.Add(string.Format("17.31"));
            list.Add(string.Format("17.31"));
            list.Add(string.Format("17.31"));
            list.Add(string.Format("17.31"));
            list.Add(string.Format("17.31"));
            list.Add(string.Format("17.31"));
            list.Add(string.Format("17.31"));
            list.Add(string.Format("17.646"));
            list.Add(string.Format("17.646"));
            list.Add(string.Format("17.792"));
            list.Add(string.Format("17.792"));
            list.Add(string.Format("17.952"));
            list.Add(string.Format("17.952"));
            list.Add(string.Format("18.11"));
            list.Add(string.Format("18.11"));
            list.Add(string.Format("18.11"));
            list.Add(string.Format("18.11"));
            list.Add(string.Format("18.147"));
            list.Add(string.Format("18.147"));
            list.Add(string.Format("18.36"));
            list.Add(string.Format("18.36"));
            list.Add(string.Format("18.595"));
            list.Add(string.Format("18.595"));
            list.Add(string.Format("18.86"));
            list.Add(string.Format("18.86"));
            list.Add(string.Format("18.86"));
            list.Add(string.Format("20.31"));
            list.Add(string.Format("20.31"));
            list.Add(string.Format("20.31"));
            list.Add(string.Format("20.31"));
            list.Add(string.Format("20.31"));
            list.Add(string.Format("20.31"));
            list.Add(string.Format("20.31"));
            list.Add(string.Format("20.31"));
            list.Add(string.Format("20.31"));
            list.Add(string.Format("20.31"));
            list.Add(string.Format("20.31"));
            list.Add(string.Format("20.31"));
            list.Add(string.Format("20.31"));
            list.Add(string.Format("20.31"));
            list.Add(string.Format("20.31"));
            list.Add(string.Format("20.658"));
            list.Add(string.Format("20.658"));
            list.Add(string.Format("20.856"));
            list.Add(string.Format("20.856"));
            list.Add(string.Format("21.085"));
            list.Add(string.Format("21.085"));
            list.Add(string.Format("21.108"));
            list.Add(string.Format("21.351"));
            list.Add(string.Format("21.351"));
            list.Add(string.Format("21.619"));
            list.Add(string.Format("21.619"));
            list.Add(string.Format("21.86"));
            list.Add(string.Format("21.86"));
            list.Add(string.Format("22.846"));
            list.Add(string.Format("23.685"));
            list.Add(string.Format("24.256"));
            list.Add(string.Format("24.9"));
            list.Add(string.Format("25.238"));
            list.Add(string.Format("25.812"));
            list.Add(string.Format("26.81"));
            #endregion List Y Coordinate

            List<double> lst_Y = new List<double>();
           
            foreach (var item in list)
            {
                y = MyList.StringToDouble(item, 0.0);
                if (!lst_Y.Contains(y)) lst_Y.Add(y);
            }

            return y;
        }



        public double Get_Z_Coordinates(double val, double MaxWidth)
        {
            double z = 0.0;

            double maxVal = 11.844;

            z = MaxWidth * val / maxVal;

            return z;
        }
        public double Get_X_Coordinates(double val, double MaxLen)
        {
            double x = 0.0;
            //double maxVal = 12.638;
            double maxVal = 11.8440;

            

            x = MaxLen * val / maxVal;

            if (false)
            {
                #region List Y Coordinate
                List<string> list = new List<string>();

                list.Add(string.Format("-0.794"));
                list.Add(string.Format("-0.021"));
                list.Add(string.Format("-0.021"));
                list.Add(string.Format("0"));
                list.Add(string.Format("0.177"));
                list.Add(string.Format("0.422"));
                list.Add(string.Format("0.422"));
                list.Add(string.Format("0.506"));
                list.Add(string.Format("0.863"));
                list.Add(string.Format("0.925"));
                list.Add(string.Format("0.925"));
                list.Add(string.Format("0.934"));
                list.Add(string.Format("1.094"));
                list.Add(string.Format("1.094"));
                list.Add(string.Format("1.182"));
                list.Add(string.Format("1.492"));
                list.Add(string.Format("1.492"));
                list.Add(string.Format("1.506"));
                list.Add(string.Format("1.506"));
                list.Add(string.Format("1.541"));
                list.Add(string.Format("1.728"));
                list.Add(string.Format("1.728"));
                list.Add(string.Format("1.868"));
                list.Add(string.Format("1.93"));
                list.Add(string.Format("1.93"));
                list.Add(string.Format("1.948"));
                list.Add(string.Format("2.215"));
                list.Add(string.Format("2.223"));
                list.Add(string.Format("2.223"));
                list.Add(string.Format("2.294"));
                list.Add(string.Format("2.294"));
                list.Add(string.Format("2.415"));
                list.Add(string.Format("2.463"));
                list.Add(string.Format("2.463"));
                list.Add(string.Format("2.685"));
                list.Add(string.Format("2.792"));
                list.Add(string.Format("2.792"));
                list.Add(string.Format("2.95"));
                list.Add(string.Format("2.961"));
                list.Add(string.Format("3.043"));
                list.Add(string.Format("3.043"));
                list.Add(string.Format("3.082"));
                list.Add(string.Format("3.082"));
                list.Add(string.Format("3.082"));
                list.Add(string.Format("3.321"));
                list.Add(string.Format("3.454"));
                list.Add(string.Format("3.454"));
                list.Add(string.Format("3.526"));
                list.Add(string.Format("3.686"));
                list.Add(string.Format("3.686"));
                list.Add(string.Format("3.737"));
                list.Add(string.Format("3.772"));
                list.Add(string.Format("3.807"));
                list.Add(string.Format("3.895"));
                list.Add(string.Format("3.907"));
                list.Add(string.Format("3.998"));
                list.Add(string.Format("4.088"));
                list.Add(string.Format("4.121"));
                list.Add(string.Format("4.121"));
                list.Add(string.Format("4.157"));
                list.Add(string.Format("4.288"));
                list.Add(string.Format("4.41"));
                list.Add(string.Format("4.502"));
                list.Add(string.Format("4.594"));
                list.Add(string.Format("4.605"));
                list.Add(string.Format("4.647"));
                list.Add(string.Format("4.722"));
                list.Add(string.Format("4.864"));
                list.Add(string.Format("4.919"));
                list.Add(string.Format("4.919"));
                list.Add(string.Format("5.068"));
                list.Add(string.Format("5.186"));
                list.Add(string.Format("5.47"));
                list.Add(string.Format("5.69"));
                list.Add(string.Format("5.922"));
                list.Add(string.Format("5.922"));
                list.Add(string.Format("5.922"));
                list.Add(string.Format("5.922"));
                list.Add(string.Format("5.922"));
                list.Add(string.Format("5.922"));
                list.Add(string.Format("5.922"));
                list.Add(string.Format("5.922"));
                list.Add(string.Format("6.31"));
                list.Add(string.Format("6.53"));
                list.Add(string.Format("6.775"));
                list.Add(string.Format("6.883"));
                list.Add(string.Format("6.925"));
                list.Add(string.Format("6.925"));
                list.Add(string.Format("6.979"));
                list.Add(string.Format("7.122"));
                list.Add(string.Format("7.197"));
                list.Add(string.Format("7.239"));
                list.Add(string.Format("7.25"));
                list.Add(string.Format("7.342"));
                list.Add(string.Format("7.434"));
                list.Add(string.Format("7.556"));
                list.Add(string.Format("7.687"));
                list.Add(string.Format("7.723"));
                list.Add(string.Format("7.723"));
                list.Add(string.Format("7.756"));
                list.Add(string.Format("7.846"));
                list.Add(string.Format("7.937"));
                list.Add(string.Format("7.949"));
                list.Add(string.Format("8.037"));
                list.Add(string.Format("8.072"));
                list.Add(string.Format("8.107"));
                list.Add(string.Format("8.157"));
                list.Add(string.Format("8.157"));
                list.Add(string.Format("8.318"));
                list.Add(string.Format("8.39"));
                list.Add(string.Format("8.39"));
                list.Add(string.Format("8.523"));
                list.Add(string.Format("8.761"));
                list.Add(string.Format("8.762"));
                list.Add(string.Format("8.762"));
                list.Add(string.Format("8.8"));
                list.Add(string.Format("8.8"));
                list.Add(string.Format("8.883"));
                list.Add(string.Format("8.894"));
                list.Add(string.Format("9.051"));
                list.Add(string.Format("9.051"));
                list.Add(string.Format("9.159"));
                list.Add(string.Format("9.381"));
                list.Add(string.Format("9.381"));
                list.Add(string.Format("9.429"));
                list.Add(string.Format("9.549"));
                list.Add(string.Format("9.549"));
                list.Add(string.Format("9.621"));
                list.Add(string.Format("9.621"));
                list.Add(string.Format("9.629"));
                list.Add(string.Format("9.896"));
                list.Add(string.Format("9.914"));
                list.Add(string.Format("9.914"));
                list.Add(string.Format("9.975"));
                list.Add(string.Format("10.116"));
                list.Add(string.Format("10.116"));
                list.Add(string.Format("10.303"));
                list.Add(string.Format("10.337"));
                list.Add(string.Format("10.337"));
                list.Add(string.Format("10.351"));
                list.Add(string.Format("10.351"));
                list.Add(string.Format("10.662"));
                list.Add(string.Format("10.75"));
                list.Add(string.Format("10.75"));
                list.Add(string.Format("10.91"));
                list.Add(string.Format("10.919"));
                list.Add(string.Format("10.919"));
                list.Add(string.Format("10.981"));
                list.Add(string.Format("11.338"));
                list.Add(string.Format("11.422"));
                list.Add(string.Format("11.422"));
                list.Add(string.Format("11.667"));
                list.Add(string.Format("11.844"));
                list.Add(string.Format("11.865"));
                list.Add(string.Format("11.865"));
                list.Add(string.Format("12.638"));
                #endregion List Y Coordinate

                List<double> lst_X = new List<double>();

                foreach (var item in list)
                {
                    x = MyList.StringToDouble(item, 0.0);
                    if (!lst_X.Contains(x)) lst_X.Add(x);

                    if (maxVal < x) maxVal = x;
                }
                lst_X.Sort();
            }


            return x;
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

        //Chiranjit [2011 10 21] This  
        public List<string> Get_MyntduBridge_Data()
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
            //Joints    1  -   11
            //for (i = 0; i <= Math.Abs(1 - 11); i++)
            for (i = 0; i <= NoOfPanel; i++)
            {
                counter++;
                x = i * PanelLength;
                y = 0;
                z = Breadth;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            //Joints    12  -   22
            for (i = 0; i <= NoOfPanel; i++)
            {
                counter++;
                x = i * PanelLength;
                y = 0;
                z = 0;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            //Joints    23  -   31
            for (i = 1; i <= Math.Abs(23 - 31) + 1; i++)
            {
                counter++;
                x = i * PanelLength;
                y = Height;
                z = Breadth;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            //Joints    32  -   40
            for (i = 1; i <= Math.Abs(32 - 40) + 1; i++)
            {
                counter++;
                x = i * PanelLength;
                y = Height;
                z = 0;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            //Joints    41  -   51
            for (i = 0; i <= Math.Abs(41 - 51); i++)
            {
                counter++;
                x = i * PanelLength;
                y = 0;
                z = Breadth / 1.206009;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }

            //Joints    52  -   62
            for (i = 0; i <= Math.Abs(52 - 62); i++)
            {
                counter++;
                x = i * PanelLength;
                y = 0;
                z = Breadth / 1.640078;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            //Joints    63  -   73
            for (i = 0; i <= Math.Abs(63 - 73); i++)
            {
                counter++;
                x = i * PanelLength;
                y = 0;
                z = Breadth / 2.56231;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            //Joints    74  -   84
            for (i = 0; i <= Math.Abs(74 - 84); i++)
            {
                counter++;
                x = i * PanelLength;
                y = 0;
                z = Breadth / 5.854167;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            return list;
        }
        //Chiranjit [2012 01 20]
        public List<string> Get_Bridge_Data()
        {
            double x_inc = PanelLength;
            double z_inc = StringerSpacing;

            JointNode jn = null;

            JointNodeCollection Joints = new JointNodeCollection();
            List<JointNodeCollection> Joints_Bottom = new List<JointNodeCollection>();
            List<JointNodeCollection> Joints_Top = new List<JointNodeCollection>();

            List<string> ASTRA_Data = new List<string>();

            int node_no = 1;

            int x_count = 0;
            int z_count = 0;
            #region Bottom Joints
            for (x_count = 0; x_count <= NoOfPanel; x_count++)
            {
                Joints = new JointNodeCollection();
                for (z_count = 0; z_count <= (NoOfStringerBeam + 1); z_count++)
                {
                    jn = new JointNode();
                    jn.NodeNo = node_no++;
                    jn.X = x_count * x_inc;
                    jn.Y = 0;
                    jn.Z = z_count * z_inc;
                    Joints.Add(jn);
                }
                if (Joints.Count > 0)
                {
                    Joints_Bottom.Add(Joints);
                }
            }
            #endregion Bottom Joints

            #region Top Joint Coordinates
            for (x_count = 1; x_count < Joints_Bottom.Count - 1; x_count++)
            {
                Joints = new JointNodeCollection();

                jn = new JointNode();
                jn.NodeNo = node_no++;
                jn.X = Joints_Bottom[x_count][0].X;
                jn.Y = Height;
                jn.Z = Joints_Bottom[x_count][0].Z;
                Joints.Add(jn);

                jn = new JointNode();
                jn.NodeNo = node_no++;
                jn.X = Joints_Bottom[x_count][Joints_Bottom[x_count].Count - 1].X;
                jn.Y = Height;
                jn.Z = Joints_Bottom[x_count][Joints_Bottom[x_count].Count - 1].Z;
                Joints.Add(jn);
                if (Joints.Count > 0)
                {
                    Joints_Top.Add(Joints);
                }
            }
            #endregion Top Joint Coordinates

            #region Cross Girders
            int mem_no = 1;
            for (x_count = 0; x_count < Joints_Bottom.Count; x_count++)
            {
                Joints = Joints_Bottom[x_count];
                for (z_count = 1; z_count < Joints.Count; z_count++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_no++;
                    mbr.StartNode = Joints[z_count - 1];
                    mbr.EndNode = Joints[z_count];
                    Cross_Girders.Add(mbr);
                }
            }
            #endregion Cross Girders

            #region Stringers && Bottom Chord
            for (x_count = 0; x_count < Joints_Bottom[0].Count; x_count++)
            {

                for (z_count = 1; z_count < Joints_Bottom.Count; z_count++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_no++;
                    mbr.StartNode = Joints_Bottom[z_count - 1][x_count];
                    mbr.EndNode = Joints_Bottom[z_count][x_count];

                    //if (x_count == 0 || x_count == Joints_Bottom[0].Count - 1)
                    //{
                    //Bottom_Chord.Add(mbr);
                    //}
                    //else
                    //{
                    Stringers.Add(mbr);
                    //}
                }
            }
            #endregion Stringers

            #region Vertical Members

            for (x_count = 0; x_count < Joints_Top.Count; x_count++)
            {

                mbr = new Member();
                mbr.MemberNo = mem_no++;
                mbr.StartNode = Joints_Bottom[x_count + 1][0];
                mbr.EndNode = Joints_Top[x_count][0];
                Vertical1.Add(mbr);

                mbr = new Member();
                mbr.MemberNo = mem_no++;
                mbr.StartNode = Joints_Bottom[x_count + 1][Joints_Bottom[x_count + 1].Count - 1];
                mbr.EndNode = Joints_Top[x_count][Joints_Top[x_count].Count - 1];
                Vertical2.Add(mbr);
            }

            #endregion Vertical Members

            #region Top_Chord Members

            for (x_count = 0; x_count < 2; x_count++)
            {
                for (z_count = 1; z_count < Joints_Top.Count; z_count++)
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_no++;
                    mbr.StartNode = Joints_Top[z_count - 1][x_count];
                    mbr.EndNode = Joints_Top[z_count][x_count];
                    if (x_count == 1)
                        Top_Chord2.Add(mbr);
                    else
                        Top_Chord1.Add(mbr);
                }
            }

            #endregion  Top Chord Members

            #region Top Chord Straight Bracing Members

            for (x_count = 0; x_count < Joints_Top.Count; x_count++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_no++;
                mbr.StartNode = Joints_Top[x_count][0];
                mbr.EndNode = Joints_Top[x_count][Joints_Top[x_count].Count - 1];
                Top_Chord_Bracing.Add(mbr);
            }

            #endregion  Top Chord Straight Members


            #region Bottom Chord Straight Bracing Members

            for (x_count = 1; x_count < Joints_Bottom.Count; x_count++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_no++;
                mbr.StartNode = Joints_Bottom[x_count - 1][0];
                mbr.EndNode = Joints_Bottom[x_count][Joints_Bottom[x_count].Count - 1];
                Bottom_Chord_Bracing.Add(mbr);

                mbr = new Member();
                mbr.MemberNo = mem_no++;
                mbr.StartNode = Joints_Bottom[x_count - 1][Joints_Bottom[x_count].Count - 1];
                mbr.EndNode = Joints_Bottom[x_count][0];
                Bottom_Chord_Bracing.Add(mbr);
            }

            #endregion  Bottom Chord Straight Bracing Members

            #region TOP Chord Straight Bracing Members

            for (x_count = 1; x_count < Joints_Top.Count; x_count++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_no++;
                mbr.StartNode = Joints_Top[x_count - 1][0];
                mbr.EndNode = Joints_Top[x_count][Joints_Top[x_count].Count - 1];
                Top_Chord_Bracing_Diagonal.Add(mbr);

                mbr = new Member();
                mbr.MemberNo = mem_no++;
                mbr.StartNode = Joints_Top[x_count - 1][Joints_Top[x_count].Count - 1];
                mbr.EndNode = Joints_Top[x_count][0];
                Top_Chord_Bracing_Diagonal.Add(mbr);
            }

            #endregion  Bottom Chord Straight Bracing Members

            #region Diagonal Members
            for (x_count = 0; x_count < Joints_Top.Count - 1; x_count++)
            {
                if (x_count < (Joints_Top.Count / 2))
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_no++;
                    mbr.StartNode = Joints_Top[x_count][0];
                    mbr.EndNode = Joints_Bottom[x_count + 2][0];
                    Diagonal1.Add(mbr);

                    mbr = new Member();
                    mbr.MemberNo = mem_no++;
                    mbr.StartNode = Joints_Top[x_count][Joints_Top[x_count].Count - 1];
                    mbr.EndNode = Joints_Bottom[x_count + 2][Joints_Bottom[x_count + 2].Count - 1];
                    Diagonal2.Add(mbr);
                }
                else
                {
                    mbr = new Member();
                    mbr.MemberNo = mem_no++;
                    mbr.StartNode = Joints_Top[x_count + 1][0];
                    mbr.EndNode = Joints_Bottom[x_count + 1][0];
                    Diagonal1.Add(mbr);

                    mbr = new Member();
                    mbr.MemberNo = mem_no++;
                    mbr.StartNode = Joints_Top[x_count + 1][Joints_Top[x_count + 1].Count - 1];
                    mbr.EndNode = Joints_Bottom[x_count + 1][Joints_Bottom[x_count + 1].Count - 1];
                    Diagonal2.Add(mbr);
                }
            }
            #endregion Diagonal Members



            #region End Rakers

            mbr = new Member();
            mbr.MemberNo = mem_no++;
            mbr.StartNode = Joints_Bottom[0][0];
            mbr.EndNode = Joints_Top[0][0];
            EndRakers.Add(mbr);

            mbr = new Member();
            mbr.MemberNo = mem_no++;
            mbr.StartNode = Joints_Bottom[0][Joints_Bottom[0].Count - 1];
            mbr.EndNode = Joints_Top[0][Joints_Top[0].Count - 1];
            EndRakers.Add(mbr);


            mbr = new Member();
            mbr.MemberNo = mem_no++;
            mbr.StartNode = Joints_Bottom[Joints_Bottom.Count - 1][0];
            mbr.EndNode = Joints_Top[Joints_Top.Count - 1][0];
            EndRakers.Add(mbr);

            mbr = new Member();
            mbr.MemberNo = mem_no++;
            mbr.StartNode = Joints_Bottom[Joints_Bottom.Count - 1][Joints_Bottom[0].Count - 1];
            mbr.EndNode = Joints_Top[Joints_Top.Count - 1][Joints_Top[0].Count - 1];
            EndRakers.Add(mbr);


            #endregion End Rakers

            #region Write Joints
            ASTRA_Data.Clear();
            ASTRA_Data.Add("JOINT COORDINATE");
            for (x_count = 0; x_count < Joints_Bottom.Count; x_count++)
            {
                Joints = Joints_Bottom[x_count];
                for (z_count = 0; z_count < Joints.Count; z_count++)
                {
                    ASTRA_Data.Add(Joints[z_count].ToString());
                }
            }

            for (x_count = 0; x_count < Joints_Top.Count; x_count++)
            {
                Joints = Joints_Top[x_count];
                for (z_count = 0; z_count < Joints.Count; z_count++)
                {
                    ASTRA_Data.Add(Joints[z_count].ToString());
                }
            }


            #endregion Write Joints

            #region Write Cross_Girders
            ASTRA_Data.Add("MEMBER INCIDENCES");
            for (x_count = 0; x_count < Cross_Girders.Count; x_count++)
            {
                ASTRA_Data.Add(Cross_Girders[x_count].ToString());
            }
            #endregion Write Cross_Girders

            #region Write Stringers
            for (x_count = 0; x_count < Stringers.Count; x_count++)
            {
                ASTRA_Data.Add(Stringers[x_count].ToString());
            }
            #endregion Write Stringers

            #region Write Vertical
            for (x_count = 0; x_count < Vertical1.Count; x_count++)
            {
                ASTRA_Data.Add(Vertical1[x_count].ToString());
            }
            for (x_count = 0; x_count < Vertical2.Count; x_count++)
            {
                ASTRA_Data.Add(Vertical2[x_count].ToString());
            }
            #endregion Write Vertical

            #region Write Top Chord
            for (x_count = 0; x_count < Top_Chord1.Count; x_count++)
            {
                ASTRA_Data.Add(Top_Chord1[x_count].ToString());
            }
            for (x_count = 0; x_count < Top_Chord2.Count; x_count++)
            {
                ASTRA_Data.Add(Top_Chord2[x_count].ToString());
            }
            #endregion Write Top Chord

            #region Write Top_Chord_Bracing
            for (x_count = 0; x_count < Top_Chord_Bracing.Count; x_count++)
            {
                ASTRA_Data.Add(Top_Chord_Bracing[x_count].ToString());
            }
            #endregion Write Top_Chord_Bracing

            #region Write Bottom Chord Bracing
            for (x_count = 0; x_count < Bottom_Chord_Bracing.Count; x_count++)
            {
                ASTRA_Data.Add(Bottom_Chord_Bracing[x_count].ToString());
            }
            #endregion Write Bottom Chord Bracing

            #region Write Bottom Chord Bracing
            for (x_count = 0; x_count < Top_Chord_Bracing_Diagonal.Count; x_count++)
            {
                ASTRA_Data.Add(Top_Chord_Bracing_Diagonal[x_count].ToString());
            }
            #endregion Write Bottom Chord Bracing

            #region Write Diagonals
            for (x_count = 0; x_count < Diagonal1.Count; x_count++)
            {
                ASTRA_Data.Add(Diagonal1[x_count].ToString());
            }
            for (x_count = 0; x_count < Diagonal2.Count; x_count++)
            {
                ASTRA_Data.Add(Diagonal2[x_count].ToString());
            }
            #endregion Write Diagonals

            #region Write End Rakers
            for (x_count = 0; x_count < EndRakers.Count; x_count++)
            {
                ASTRA_Data.Add(EndRakers[x_count].ToString());
            }
            #endregion Write End Rakers


            List<string> _groups = new List<string>();
            #region Member Grouping
            ASTRA_Data.Add("START GROUP DEFINITION");
            string kStr = "";
            kStr = "";

            for (int i = 0; i < NoOfPanel; i++)
            {
                Bottom_Chord1.Add(Stringers[i]);
            }

            for (int i = (Stringers.Count - NoOfPanel); i < Stringers.Count; i++)
            {
                Bottom_Chord2.Add(Stringers[i]);
            }



            Stringers.RemoveRange((Stringers.Count - NoOfPanel), NoOfPanel);
            Stringers.RemoveRange(0, NoOfPanel);

            for (int i = 0; i < (NoOfPanel / 2); i++)
            {
                kStr = "_L" + i + "L" + (i + 1);
                _groups.Add(kStr);

                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Bottom_Chord1[i].MemberNo,
                    Bottom_Chord2[i].MemberNo,
                    Bottom_Chord1[Bottom_Chord1.Count - (i + 1)].MemberNo,
                    Bottom_Chord2[Bottom_Chord2.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }


            for (int i = 0; i < (Top_Chord1.Count / 2); i++)
            {
                kStr = "_U" + (i + 1) + "U" + (i + 2);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Top_Chord1[i].MemberNo,
                    Top_Chord2[i].MemberNo,
                    Top_Chord1[Top_Chord1.Count - (i + 1)].MemberNo,
                    Top_Chord2[Top_Chord2.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }

            for (int i = 0; i <= (Vertical1.Count / 2); i++)
            {
                kStr = "_L" + (i + 1) + "U" + (i + 1);
                _groups.Add(kStr);
                if (i == (Vertical1.Count / 2))
                {
                    kStr += string.Format("{0,10} {1,10}",
                    Vertical1[i].MemberNo,
                    Vertical2[i].MemberNo);

                }
                else
                {
                    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                        Vertical1[i].MemberNo,
                        Vertical2[i].MemberNo,
                        Vertical1[Vertical1.Count - (i + 1)].MemberNo,
                        Vertical2[Vertical2.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }


            for (int i = 0; i < (Diagonal1.Count / 2); i++)
            {
                kStr = "_L" + (i + 2) + "U" + (i + 1);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Diagonal1[i].MemberNo,
                    Diagonal2[i].MemberNo,
                    Diagonal1[Diagonal1.Count - (i + 1)].MemberNo,
                    Diagonal2[Diagonal2.Count - (i + 1)].MemberNo);
                ASTRA_Data.Add(kStr);
            }
            if (EndRakers.Count > 0)
            {
                _groups.Add("_ER");

                ASTRA_Data.Add(string.Format("_ER         {0,10} {1,10} {2,10} ", EndRakers[0].MemberNo, "TO", EndRakers[EndRakers.Count - 1].MemberNo));
            }
            if (Top_Chord_Bracing.Count > 0)
                ASTRA_Data.Add(string.Format("_TCS_ST     {0,10} {1,10} {2,10} ", Top_Chord_Bracing[0].MemberNo, "TO", Top_Chord_Bracing[Top_Chord_Bracing.Count - 1].MemberNo));
            if (Top_Chord_Bracing_Diagonal.Count > 0)
                ASTRA_Data.Add(string.Format("_TCS_DIA    {0,10} {1,10} {2,10} ", Top_Chord_Bracing_Diagonal[0].MemberNo, "TO", Top_Chord_Bracing_Diagonal[Top_Chord_Bracing_Diagonal.Count - 1].MemberNo));
            if (Bottom_Chord_Bracing.Count > 0)
                ASTRA_Data.Add(string.Format("_BCB        {0,10} {1,10} {2,10} ", Bottom_Chord_Bracing[0].MemberNo, "TO", Bottom_Chord_Bracing[Bottom_Chord_Bracing.Count - 1].MemberNo));
            if (Stringers.Count > 0)
                ASTRA_Data.Add(string.Format("_STRINGER   {0,10} {1,10} {2,10} ", Stringers[0].MemberNo, "TO", Stringers[Stringers.Count - 1].MemberNo));
            if (Cross_Girders.Count > 0)
                ASTRA_Data.Add(string.Format("_XGIRDER    {0,10} {1,10} {2,10} ", Cross_Girders[0].MemberNo, "TO", Cross_Girders[Cross_Girders.Count - 1].MemberNo));
            ASTRA_Data.Add("END");

            #region Member_Properties
            Hashtable hash_tab = new Hashtable();
            hash_tab.Add("", "");

            hash_tab.Add("_L0L1", "                PRI                AX                0.024                IX                0.00001                IY                0.000741                IZ                0.001");
            hash_tab.Add("_L1L2", "                PRI                AX                0.024                IX                0.00001                IY                0.000741                IZ                0.001");
            hash_tab.Add("_L2L3", "                PRI                AX                0.030                IX                0.00001                IY                0.000946                IZ                0.001");
            hash_tab.Add("_L3L4", "                PRI                AX                0.039                IX                0.00001                IY                0.001000                IZ                0.002");
            hash_tab.Add("_L4L5", "                PRI                AX                0.042                IX                0.00001                IY                0.001000                IZ                0.001");
            hash_tab.Add("_L5L6", "                PRI                AX                0.042                IX                0.00001                IY                0.001000                IZ                0.001");
            hash_tab.Add("_L6L7", "                PRI                AX                0.042                IX                0.00001                IY                0.001000                IZ                0.001");
            hash_tab.Add("_L7L8", "                PRI                AX                0.042                IX                0.00001                IY                0.001000                IZ                0.001");
            hash_tab.Add("_L8L9", "                PRI                AX                0.042                IX                0.00001                IY                0.001000                IZ                0.001");
            hash_tab.Add("_L9L10", "                PRI                AX                0.042                IX                0.00001                IY                0.001000                IZ                0.001");
            hash_tab.Add("_L10L11", "                PRI                AX                0.042                IX                0.00001                IY                0.001000                IZ                0.001");
            hash_tab.Add("_U1U2", "                PRI                AX                0.027                IX                0.00001                IY                0.000807                IZ                0.000693");
            hash_tab.Add("_U2U3", "                PRI                AX                0.033                IX                0.00001                IY                0.000864                IZ                0.000922");
            hash_tab.Add("_U3U4", "                PRI                AX                0.036                IX                0.00001                IY                0.000896                IZ                0.001");
            hash_tab.Add("_U4U5", "                PRI                AX                0.038                IX                0.00001                IY                0.000914                IZ                0.001");
            hash_tab.Add("_U5U6", "                PRI                AX                0.038                IX                0.00001                IY                0.000914                IZ                0.001");
            hash_tab.Add("_U6U7", "                PRI                AX                0.038                IX                0.00001                IY                0.000914                IZ                0.001");
            hash_tab.Add("_U7U8", "                PRI                AX                0.038                IX                0.00001                IY                0.000914                IZ                0.001");
            hash_tab.Add("_U8U9", "                PRI                AX                0.038                IX                0.00001                IY                0.000914                IZ                0.001");
            hash_tab.Add("_U9U10", "                PRI                AX                0.038                IX                0.00001                IY                0.000914                IZ                0.001");
            hash_tab.Add("_U10U11", "                PRI                AX                0.038                IX                0.00001                IY                0.000914                IZ                0.001");
            hash_tab.Add("_L1U1", "                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_L2U2", "                PRI                AX                0.013                IX                0.00001                IY                0.000399                IZ                0.000302");
            hash_tab.Add("_L3U3", "                PRI                AX                0.009                IX                0.00001                IY                0.000290                IZ                0.000127");
            hash_tab.Add("_L4U4", "                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_L5U5", "                PRI                AX                0.004                IX                0.00001                IY                0.000134                IZ                0.000016");
            hash_tab.Add("_L6U6", "                PRI                AX                0.004                IX                0.00001                IY                0.000134                IZ                0.000016");
            hash_tab.Add("_L7U7", "                PRI                AX                0.004                IX                0.00001                IY                0.000134                IZ                0.000016");
            hash_tab.Add("_L8U8", "                PRI                AX                0.004                IX                0.00001                IY                0.000134                IZ                0.000016");
            hash_tab.Add("_L9U9", "                PRI                AX                0.004                IX                0.00001                IY                0.000134                IZ                0.000016");
            hash_tab.Add("_L10U10", "                PRI                AX                0.004                IX                0.00001                IY                0.000134                IZ                0.000016");
            hash_tab.Add("_L11U11", "                PRI                AX                0.004                IX                0.00001                IY                0.000134                IZ                0.000016");
            hash_tab.Add("_ER", "                PRI                AX                0.022                IX                0.00001                IY                0.000666                IZ                0.000579");
            hash_tab.Add("_L2U1", "                PRI                AX                0.019                IX                0.00001                IY                0.000621                IZ                0.000356");
            hash_tab.Add("_L3U2", "                PRI                AX                0.014                IX                0.00001                IY                0.000474                IZ                0.000149");
            hash_tab.Add("_L4U3", "                PRI                AX                0.009                IX                0.00001                IY                0.000290                IZ                0.000127");
            hash_tab.Add("_L5U4", "                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_L6U5", "                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_L7U6", "                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_L8U7", "                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_L9U8", "                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_L10U9", "                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_L11U10", "                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_TCS_ST", "                PRI                AX                0.006                IX                0.00001                IY                0.000027                IZ                0.000225");
            hash_tab.Add("_TCS_DIA", "                PRI                AX                0.006                IX                0.00001                IY                0.000027                IZ                0.000225");
            hash_tab.Add("_BCB", "                PRI                AX                0.002                IX                0.00001                IY                0.000001                IZ                0.000001");
            hash_tab.Add("_STRINGER", "                PRI                AX                0.048                IX                0.00001                IY                0.008                IZ                0.002");
            hash_tab.Add("_XGIRDER", "                PRI                AX                0.059                IX                0.00001                IY                0.009                IZ                0.005");
            #endregion Member_Properties


            ASTRA_Data.Add("MEMBER PROPERTY");
            for (int i = 0; i < _groups.Count; i++)
            {
                kStr = hash_tab[_groups[i]] as string;
                if (kStr != null)
                    ASTRA_Data.Add(string.Format("{0}   {1,-10}", _groups[i], kStr));
            }
            ASTRA_Data.Add("_TCS_ST                        PRI                AX                0.006                IX                0.00001                IY                0.000027                IZ                0.000225");
            ASTRA_Data.Add("_TCS_DIA                PRI                AX                0.006                IX                0.00001                IY                0.000027                IZ                0.000225");
            ASTRA_Data.Add("_BCB                        PRI                AX                0.002                IX                0.00001                IY                0.000001                IZ                0.000001");
            ASTRA_Data.Add("_STRINGER                PRI                AX                0.048                IX                0.00001                IY                0.008                IZ                0.002");
            ASTRA_Data.Add("_XGIRDER                PRI                AX                0.059                IX                0.00001                IY                0.009                IZ                0.005");
            for (int i = 0; i < _groups.Count; i++)
            {
                ASTRA_Data.Add("MEMBER TRUSS");
                ASTRA_Data.Add(_groups[i]);
            }
            ASTRA_Data.Add("MEMBER TRUSS");
            ASTRA_Data.Add("_TCS_ST");
            ASTRA_Data.Add("MEMBER TRUSS");
            ASTRA_Data.Add("_TCS_DIA");
            ASTRA_Data.Add("MEMBER TRUSS");
            ASTRA_Data.Add("_BCB");
            ASTRA_Data.Add("CONSTANT");
            ASTRA_Data.Add("E  2.110E8                ALL                ");
            ASTRA_Data.Add("DENSITY STEEL ALL");
            ASTRA_Data.Add("POISSON STEEL ALL");
            ASTRA_Data.Add("SUPPORT");
            ASTRA_Data.Add(string.Format("{0} {1} FIXED BUT MZ", Bottom_Chord1[0].StartNode.NodeNo,
                Bottom_Chord1[Bottom_Chord1.Count - 1].EndNode.NodeNo));
            ASTRA_Data.Add(string.Format("{0} {1} FIXED BUT MZ", Bottom_Chord2[0].StartNode.NodeNo,
                Bottom_Chord2[Bottom_Chord2.Count - 1].EndNode.NodeNo));
            //ASTRA_Data.Add("FINISH");
            #endregion Member Grouping

            string joint_loads1 = (string.Format("{0} {1} {2} {3}   ",
                Bottom_Chord1[0].StartNode.NodeNo,
                Bottom_Chord2[0].StartNode.NodeNo,
                Bottom_Chord1[Bottom_Chord1.Count - 1].EndNode.NodeNo,
                Bottom_Chord2[Bottom_Chord2.Count - 1].EndNode.NodeNo));



            string joint_loads2 = (string.Format("{0} TO {1} {2} TO {3}   ",
                Bottom_Chord1[1].StartNode.NodeNo,
                Bottom_Chord2[1].StartNode.NodeNo,
                Bottom_Chord1[Bottom_Chord1.Count - 2].EndNode.NodeNo,
                Bottom_Chord2[Bottom_Chord2.Count - 2].EndNode.NodeNo));
            joint_loads2 = "";
            for (int i = 1; i < Bottom_Chord1.Count; i++)
            {
                joint_loads2 += Bottom_Chord1[i].StartNode.NodeNo + " ";
            }

            for (int i = 1; i < Bottom_Chord2.Count; i++)
            {
                joint_loads2 += " " + Bottom_Chord2[i].StartNode.NodeNo;
            }

            Joint_Load_Support = joint_loads1;
            Joint_Load_Edge = joint_loads2;
            ASTRA_Data.Add("LOAD  1  DL + SIDL");
            ASTRA_Data.Add("JOINT LOAD");
            ASTRA_Data.Add(Joint_Load_Edge + " FY -100.0");
            ASTRA_Data.Add(Joint_Load_Support + " FY  -50.0");
            ASTRA_Data.Add("PERFORM ANALYSIS");
            ASTRA_Data.Add("FINISH");
            return ASTRA_Data;
        }
        public List<string> Get_Bridge_Data(string data)
        {
            double x_inc = PanelLength;
            double z_inc = StringerSpacing;

            JointNode jn = null;

            double height_1 = Height / 2;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            JointNodeCollection Joints = new JointNodeCollection();
            List<JointNodeCollection> Joints_Bottom = new List<JointNodeCollection>();
            List<JointNodeCollection> Joints_Top = new List<JointNodeCollection>();

            List<string> ASTRA_Data = new List<string>();



            for (int i = 0; i <= NoOfPanel; i++)
            {
                list_x.Add(PanelLength * i);
            }
            for (int i = 0; i <= NoOfStringerBeam + 1; i++)
            {
                list_z.Add(StringerSpacing * i);
            }
            JointNodeCollection All_Joints = new JointNodeCollection();



            int x_count = 0;
            int z_count = 0;
            int mem_no = 1;
            int node_no = 1;
            x_count = 0;
            #region X = 0 to 60, Y = 0, Z = 8.43
            for (x_count = 0; x_count < list_x.Count; x_count++)
            {
                jn = new JointNode();
                jn.NodeNo = node_no++;
                jn.X = list_x[x_count];
                jn.Y = 0;
                jn.Z = list_z[list_z.Count - 1];
                All_Joints.Add(jn);
            }
            #endregion
            x_count = 0;

            #region X = 0 to 60, Y = 0, Z = 0
            for (x_count = 0; x_count < list_x.Count; x_count++)
            {
                jn = new JointNode();
                jn.NodeNo = node_no++;
                jn.X = list_x[x_count];
                jn.Y = 0;
                jn.Z = list_z[0];
                All_Joints.Add(jn);
            }
            #endregion
            x_count = 0;

            #region X = 6 to 54, Y = 6.35, Z = 8.43
            for (x_count = 1; x_count < (list_x.Count - 1); x_count++)
            {
                jn = new JointNode();
                jn.NodeNo = node_no++;
                jn.X = list_x[x_count];
                jn.Y = Height;
                jn.Z = list_z[list_z.Count - 1];
                All_Joints.Add(jn);
            }
            #endregion
            x_count = 0;


            #region X = 6 to 54, Y = 6.35 / 2, Z = 8.43
            for (x_count = 1; x_count < (list_x.Count - 1); x_count++)
            {
                jn = new JointNode();
                jn.NodeNo = node_no++;
                jn.X = list_x[x_count];
                jn.Y = height_1;
                jn.Z = list_z[list_z.Count - 1];
                All_Joints.Add(jn);
            }
            #endregion
            x_count = 0;


            #region X = 6 to 54, Y = 6.35, Z = 0
            for (x_count = 1; x_count < (list_x.Count - 1); x_count++)
            {
                jn = new JointNode();
                jn.NodeNo = node_no++;
                jn.X = list_x[x_count];
                jn.Y = Height;
                jn.Z = list_z[0];
                All_Joints.Add(jn);
            }
            #endregion
            x_count = 0;


            #region X = 6 to 54, Y = 6.35 / 2, Z = 0
            for (x_count = 1; x_count < (list_x.Count - 1); x_count++)
            {
                jn = new JointNode();
                jn.NodeNo = node_no++;
                jn.X = list_x[x_count];
                jn.Y = height_1;
                jn.Z = list_z[0];
                All_Joints.Add(jn);
            }
            #endregion
            x_count = 0;

            #region X = 0 to 60, Y = 6.35, Z = 0

            for (z_count = list_z.Count - 2; z_count > 0; z_count--)
            {
                for (x_count = 0; x_count < (list_x.Count); x_count++)
                {
                    jn = new JointNode();
                    jn.NodeNo = node_no++;
                    jn.X = list_x[x_count];
                    jn.Y = 0;
                    jn.Z = list_z[z_count];
                    All_Joints.Add(jn);
                }
            }
            #endregion
            x_count = 0;

            #region Write Joints
            ASTRA_Data.Clear();
            ASTRA_Data.Add("JOINT COORDINATE");
            for (x_count = 0; x_count < All_Joints.Count; x_count++)
            {
                ASTRA_Data.Add(All_Joints[x_count].ToString());
            }




            #endregion Write Joints
            ASTRA_Data.Add("MEMBER INCIDENCES");

            //Bottom Chord
            for (x_count = 0; x_count < NoOfPanel; x_count++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_no;
                mbr.StartNode = All_Joints[x_count];
                mbr.EndNode = All_Joints[x_count + 1];
                Bottom_Chord1.Add(mbr);

                mbr = new Member();
                mbr.MemberNo = NoOfPanel + mem_no;
                mbr.StartNode = All_Joints[NoOfPanel + x_count + 1];
                mbr.EndNode = All_Joints[NoOfPanel + x_count + 2];
                Bottom_Chord2.Add(mbr);
                mem_no++;
            }

            Vertical1.Clear();
            Vertical2.Clear();
            //Vertical 


            for (x_count = 1; x_count < NoOfPanel; x_count++)
            {
                mbr = new Member();
                mbr.StartNode.X = list_x[x_count];
                mbr.StartNode.Y = 0;
                mbr.StartNode.Z = list_z[list_z.Count - 1];
                mbr.EndNode.X = list_x[x_count];
                mbr.EndNode.Y = height_1;
                mbr.EndNode.Z = list_z[list_z.Count - 1];
                //Vertical1.Add(mbr);
                Vertical_Bottom1.Add(mbr);


                mbr = new Member();
                mbr.StartNode.X = list_x[x_count];
                mbr.StartNode.Y = height_1;
                mbr.StartNode.Z = list_z[list_z.Count - 1];
                mbr.EndNode.X = list_x[x_count];
                mbr.EndNode.Y = Height;
                mbr.EndNode.Z = list_z[list_z.Count - 1];
                //Vertical1.Add(mbr);
                Vertical_Top1.Add(mbr);


                mbr = new Member();
                mbr.StartNode.X = list_x[x_count];
                mbr.StartNode.Y = 0;
                mbr.StartNode.Z = list_z[0];
                mbr.EndNode.X = list_x[x_count];
                mbr.EndNode.Y = height_1;
                mbr.EndNode.Z = list_z[0];
                //Vertical2.Add(mbr);
                Vertical_Bottom2.Add(mbr);

                mbr = new Member();
                mbr.StartNode.X = list_x[x_count];
                mbr.StartNode.Y = height_1;
                mbr.StartNode.Z = list_z[0];
                mbr.EndNode.X = list_x[x_count];
                mbr.EndNode.Y = Height;
                mbr.EndNode.Z = list_z[0];
                //Vertical2.Add(mbr);
                Vertical_Top2.Add(mbr);
            }


            //Diagonal 
            for (x_count = 0; x_count < NoOfPanel / 2; x_count++)
            {
                if (x_count == 0)
                {
                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[list_z.Count - 1];

                    mbr.EndNode.X = list_x[x_count + 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[list_z.Count - 1];
                    Diagonal1.Add(mbr);


                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[0];

                    mbr.EndNode.X = list_x[x_count + 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[0];
                    Diagonal2.Add(mbr);
                }
                else
                {
                    mbr = new Member();

                    mbr.StartNode.X = list_x[x_count + 1];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[list_z.Count - 1];

                    mbr.EndNode.X = list_x[x_count];
                    mbr.EndNode.Y = height_1;
                    mbr.EndNode.Z = list_z[list_z.Count - 1];

                    //Diagonal1.Add(mbr);
                    Diagonal_Bottom1.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = height_1;
                    mbr.StartNode.Z = list_z[list_z.Count - 1];

                    mbr.EndNode.X = list_x[x_count + 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[list_z.Count - 1];
                    //Diagonal1.Add(mbr);
                    Diagonal_Top1.Add(mbr);


                    mbr = new Member();

                    mbr.StartNode.X = list_x[x_count + 1];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[0];

                    mbr.EndNode.X = list_x[x_count];
                    mbr.EndNode.Y = height_1;
                    mbr.EndNode.Z = list_z[0];

                    //Diagonal2.Add(mbr);
                    Diagonal_Bottom2.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = height_1;
                    mbr.StartNode.Z = list_z[0];

                    mbr.EndNode.X = list_x[x_count + 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[0];
                    //Diagonal2.Add(mbr);
                    Diagonal_Top2.Add(mbr);
                }
            }


            //for (x_count = NoOfPanel; x_count > (NoOfPanel / 2); x_count--)
            for (x_count = (NoOfPanel / 2) + 1; x_count <= (NoOfPanel); x_count++)
            {
                if (x_count == NoOfPanel)
                {
                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[list_z.Count - 1];

                    mbr.EndNode.X = list_x[x_count - 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[list_z.Count - 1];
                    Diagonal1.Add(mbr);




                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[0];
                    mbr.EndNode.X = list_x[x_count - 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[0];
                    Diagonal2.Add(mbr);
                }
                else
                {
                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count - 1];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[list_z.Count - 1];

                    mbr.EndNode.X = list_x[x_count];
                    mbr.EndNode.Y = height_1;
                    mbr.EndNode.Z = list_z[list_z.Count - 1];
                    //Diagonal1.Add(mbr);
                    Diagonal_Bottom1.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = height_1;
                    mbr.StartNode.Z = list_z[list_z.Count - 1];

                    mbr.EndNode.X = list_x[x_count - 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[list_z.Count - 1];
                    //Diagonal1.Add(mbr);
                    Diagonal_Top1.Add(mbr);





                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count - 1];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[0];

                    mbr.EndNode.X = list_x[x_count];
                    mbr.EndNode.Y = height_1;
                    mbr.EndNode.Z = list_z[0];
                    //Diagonal2.Add(mbr);
                    Diagonal_Bottom2.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = height_1;
                    mbr.StartNode.Z = list_z[0];

                    mbr.EndNode.X = list_x[x_count - 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[0];
                    //Diagonal2.Add(mbr);
                    Diagonal_Top2.Add(mbr);


                }
            }

            //Top Chord
            for (x_count = 1; x_count < NoOfPanel - 1; x_count++)
            {
                mbr = new Member();
                mbr.StartNode.X = list_x[x_count];
                mbr.StartNode.Y = Height;
                mbr.StartNode.Z = list_z[list_z.Count - 1];

                mbr.EndNode.X = list_x[x_count + 1];
                mbr.EndNode.Y = Height;
                mbr.EndNode.Z = list_z[list_z.Count - 1];
                Top_Chord1.Add(mbr);


                mbr = new Member();
                mbr.StartNode.X = list_x[x_count];
                mbr.StartNode.Y = Height;
                mbr.StartNode.Z = list_z[0];

                mbr.EndNode.X = list_x[x_count + 1];
                mbr.EndNode.Y = Height;
                mbr.EndNode.Z = list_z[0];
                Top_Chord2.Add(mbr);
            }

            //Stringers
            for (z_count = list_z.Count - 2; z_count > 0; z_count--)
            {
                for (x_count = 0; x_count < NoOfPanel; x_count++)
                {

                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[z_count];

                    mbr.EndNode.X = list_x[x_count + 1];
                    mbr.EndNode.Y = 0;
                    mbr.EndNode.Z = list_z[z_count];
                    Stringers.Add(mbr);
                }
            }


            //Coss Girders
            for (x_count = 0; x_count < list_x.Count; x_count++)
            {
                for (z_count = 0; z_count < list_z.Count - 1; z_count++)
                {
                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[z_count];

                    mbr.EndNode.X = list_x[x_count];
                    mbr.EndNode.Y = 0;
                    mbr.EndNode.Z = list_z[z_count + 1];
                    Cross_Girders.Add(mbr);
                }
            }

            if (Is_Add_TCB_ST)
            {
                //Top Chord Bracing Straight
                for (x_count = 1; x_count < NoOfPanel; x_count++)
                {
                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = Height;
                    mbr.StartNode.Z = list_z[z_count];

                    mbr.EndNode.X = list_x[x_count];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[0];
                    Top_Chord_Bracing.Add(mbr);
                }
            }

            if (Is_Add_TCB_DIA)
            {
                //Top Chord Bracing Diagonal
                for (x_count = 1; x_count < NoOfPanel - 1; x_count++)
                {
                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count + 1];
                    mbr.StartNode.Y = Height;
                    mbr.StartNode.Z = list_z[z_count];

                    mbr.EndNode.X = list_x[x_count];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[0];
                    Top_Chord_Bracing_Diagonal.Add(mbr);
                }

                //Top Chord Bracing Diagonal
                for (x_count = 1; x_count < NoOfPanel - 1; x_count++)
                {
                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = Height;
                    mbr.StartNode.Z = list_z[z_count];

                    mbr.EndNode.X = list_x[x_count + 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[0];
                    Top_Chord_Bracing_Diagonal.Add(mbr);
                }
            }

            if (Is_Add_BCB)
            {
                //Bottom Chord Bracing Diagonal
                for (x_count = 0; x_count < NoOfPanel; x_count++)
                {
                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count + 1];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[z_count];

                    mbr.EndNode.X = list_x[x_count];
                    mbr.EndNode.Y = 0;
                    mbr.EndNode.Z = list_z[0];
                    Bottom_Chord_Bracing.Add(mbr);
                }

                //Bottom Chord Bracing Diagonal
                for (x_count = 0; x_count < NoOfPanel; x_count++)
                {
                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[z_count];

                    mbr.EndNode.X = list_x[x_count + 1];
                    mbr.EndNode.Y = 0;
                    mbr.EndNode.Z = list_z[0];
                    Bottom_Chord_Bracing.Add(mbr);
                }
            }



            mem_no = Bottom_Chord2[Bottom_Chord2.Count - 1].MemberNo + 1;


            foreach (var item in Bottom_Chord1)
            {
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord2)
            {
                ASTRA_Data.Add(item.ToString());
            }


            //foreach (var item in Vertical1)
            //{
            //    item.MemberNo = mem_no++;
            //    item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
            //    item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
            //    ASTRA_Data.Add(item.ToString());
            //}


            //foreach (var item in Vertical2)
            //{
            //    item.MemberNo = mem_no++;
            //    item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
            //    item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
            //    ASTRA_Data.Add(item.ToString());
            //}


            for (int i = 0; i < Vertical_Bottom1.Count; i++)
            {
                var item = Vertical_Bottom1[i];

                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());


                item = Vertical_Top1[i];

                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }

            for (int i = 0; i < Vertical_Bottom2.Count; i++)
            {
                var item = Vertical_Bottom2[i];

                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());


                item = Vertical_Top2[i];

                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }

            //foreach (var item in Vertical2)
            //{
            //    item.MemberNo = mem_no++;
            //    item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
            //    item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
            //    ASTRA_Data.Add(item.ToString());
            //}

            EndRakers.Clear();
            var item1 = Diagonal1[0];

            item1.MemberNo = mem_no++;
            item1.StartNode = All_Joints.GetJoints(item1.StartNode.X, item1.StartNode.Y, item1.StartNode.Z, 0.5);
            item1.EndNode = All_Joints.GetJoints(item1.EndNode.X, item1.EndNode.Y, item1.EndNode.Z, 0.5);
            ASTRA_Data.Add(item1.ToString());


            //foreach (var item in Diagonal1)
            //{

            //    item.MemberNo = mem_no++;
            //    item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
            //    item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
            //    ASTRA_Data.Add(item.ToString());
            //}

            for (int i = 0; i < Diagonal_Bottom1.Count; i++)
            {
                var item = Diagonal_Bottom1[i];

                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());


                item = Diagonal_Top1[i];

                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }

            item1 = Diagonal1[1];

            item1.MemberNo = mem_no++;
            item1.StartNode = All_Joints.GetJoints(item1.StartNode.X, item1.StartNode.Y, item1.StartNode.Z, 0.5);
            item1.EndNode = All_Joints.GetJoints(item1.EndNode.X, item1.EndNode.Y, item1.EndNode.Z, 0.5);
            ASTRA_Data.Add(item1.ToString());


            item1 = Diagonal2[0];

            item1.MemberNo = mem_no++;
            item1.StartNode = All_Joints.GetJoints(item1.StartNode.X, item1.StartNode.Y, item1.StartNode.Z, 0.5);
            item1.EndNode = All_Joints.GetJoints(item1.EndNode.X, item1.EndNode.Y, item1.EndNode.Z, 0.5);
            ASTRA_Data.Add(item1.ToString());


            for (int i = 0; i < Diagonal_Bottom2.Count; i++)
            {
                var item = Diagonal_Bottom2[i];

                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());


                item = Diagonal_Top2[i];

                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }



            item1 = Diagonal2[1];

            item1.MemberNo = mem_no++;
            item1.StartNode = All_Joints.GetJoints(item1.StartNode.X, item1.StartNode.Y, item1.StartNode.Z, 0.5);
            item1.EndNode = All_Joints.GetJoints(item1.EndNode.X, item1.EndNode.Y, item1.EndNode.Z, 0.5);
            ASTRA_Data.Add(item1.ToString());

            //foreach (var item in Diagonal2)
            //{
            //    item.MemberNo = mem_no++;
            //    item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
            //    item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
            //    ASTRA_Data.Add(item.ToString());
            //}




            EndRakers.Clear();
            EndRakers.AddRange(Diagonal1.ToArray());
            EndRakers.AddRange(Diagonal2.ToArray());


            //EndRakers.Add(Diagonal1[0]);
            //Diagonal1.RemoveAt(0);

            //EndRakers.Add(Diagonal1[Diagonal1.Count - 1]);
            //Diagonal1.RemoveAt(Diagonal1.Count - 1);

            //EndRakers.Add(Diagonal2[0]);
            //Diagonal2.RemoveAt(0);

            //EndRakers.Add(Diagonal2[Diagonal2.Count - 1]);
            //Diagonal2.RemoveAt(Diagonal2.Count - 1);

            foreach (var item in Top_Chord1)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Top_Chord2)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Top_Chord_Bracing)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Top_Chord_Bracing_Diagonal)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord_Bracing)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Stringers)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Cross_Girders)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }



            List<string> _groups = new List<string>();
            #region Member Grouping
            ASTRA_Data.Add("START GROUP DEFINITION");
            string kStr = "";
            kStr = "";


            for (int i = 0; i < (Bottom_Chord1.Count / 2); i++)
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


            for (int i = 0; i < (Top_Chord1.Count / 2); i++)
            {
                kStr = "_U" + (i + 1) + "U" + (i + 2);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Top_Chord1[i].MemberNo,
                    Top_Chord1[Top_Chord1.Count - (i + 1)].MemberNo,
                    Top_Chord2[i].MemberNo,
                    Top_Chord2[Top_Chord2.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }




            //for (int i = 0; i <= (Vertical1.Count / 2); i++)
            //{
            //    kStr = "_L" + (i + 1) + "U" + (i + 1);
            //    _groups.Add(kStr);
            //    if (i == (Vertical1.Count / 2))
            //    {
            //        kStr += string.Format("{0,10} {1,10}",
            //        Vertical1[i].MemberNo,
            //        Vertical2[i].MemberNo);

            //    }
            //    else
            //    {
            //        kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
            //            Vertical1[i].MemberNo,
            //            Vertical1[Vertical1.Count - (i + 1)].MemberNo,
            //            Vertical2[i].MemberNo,
            //            Vertical2[Vertical2.Count - (i + 1)].MemberNo);
            //    }
            //    ASTRA_Data.Add(kStr);
            //}


            for (int i = 0; i <= (Vertical_Bottom1.Count / 2); i++)
            {
                kStr = "_L" + (i + 1) + "M" + (i + 1);
                _groups.Add(kStr);
                if (i == (Vertical_Bottom1.Count / 2))
                {
                    kStr += string.Format("{0,10} {1,10}",
                    Vertical_Bottom1[i].MemberNo,
                    Vertical_Bottom2[i].MemberNo);
                }
                else
                {
                    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                        Vertical_Bottom1[i].MemberNo,
                        Vertical_Bottom1[Vertical_Bottom1.Count - (i + 1)].MemberNo,
                        Vertical_Bottom2[i].MemberNo,
                        Vertical_Bottom2[Vertical_Bottom2.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }

            for (int i = 0; i <= (Vertical_Top1.Count / 2); i++)
            {
                kStr = "_M" + (i + 1) + "U" + (i + 1);
                _groups.Add(kStr);
                if (i == (Vertical_Top1.Count / 2))
                {
                    kStr += string.Format("{0,10} {1,10}",
                    Vertical_Top1[i].MemberNo,
                    Vertical_Top2[i].MemberNo);
                }
                else
                {
                    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                        Vertical_Top1[i].MemberNo,
                        Vertical_Top1[Vertical_Top1.Count - (i + 1)].MemberNo,
                        Vertical_Top2[i].MemberNo,
                        Vertical_Top2[Vertical_Top2.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }





            if (EndRakers.Count > 0)
            {
                _groups.Add("_ER");
                if (EndRakers.Count == 4)
                    ASTRA_Data.Add(string.Format("_ER {0,10} {1,10} {2,10} {3,10}", EndRakers[0].MemberNo, EndRakers[1].MemberNo, EndRakers[2].MemberNo, EndRakers[3].MemberNo));
            }

            //for (int i = 0; i < (Diagonal1.Count / 2); i++)
            //{
            //    kStr = "_L" + (i + 2) + "U" + (i + 1);
            //    _groups.Add(kStr);
            //    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
            //        Diagonal1[i].MemberNo,
            //        Diagonal1[Diagonal1.Count - (i + 1)].MemberNo,
            //        Diagonal2[i].MemberNo,
            //        Diagonal2[Diagonal2.Count - (i + 1)].MemberNo);
            //    ASTRA_Data.Add(kStr);
            //}



            for (int i = 0; i < (Diagonal_Bottom1.Count / 2); i++)
            {
                kStr = "_L" + (i + 2) + "M" + (i + 1);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Diagonal_Bottom1[i].MemberNo,
                    Diagonal_Bottom1[Diagonal_Bottom1.Count - (i + 1)].MemberNo,
                    Diagonal_Bottom2[i].MemberNo,
                    Diagonal_Bottom2[Diagonal_Bottom2.Count - (i + 1)].MemberNo);
                ASTRA_Data.Add(kStr);
            }




            for (int i = 0; i < (Diagonal_Top1.Count / 2); i++)
            {
                kStr = "_M" + (i + 1) + "U" + (i + 2);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Diagonal_Top1[i].MemberNo,
                    Diagonal_Top1[Diagonal_Top1.Count - (i + 1)].MemberNo,
                    Diagonal_Top2[i].MemberNo,
                    Diagonal_Top2[Diagonal_Top2.Count - (i + 1)].MemberNo);
                ASTRA_Data.Add(kStr);
            }






            if (Top_Chord_Bracing.Count > 0)
                ASTRA_Data.Add(string.Format("_TCS_ST     {0,10} {1,10} {2,10} ", Top_Chord_Bracing[0].MemberNo, "TO", Top_Chord_Bracing[Top_Chord_Bracing.Count - 1].MemberNo));
            if (Top_Chord_Bracing_Diagonal.Count > 0)
                ASTRA_Data.Add(string.Format("_TCS_DIA    {0,10} {1,10} {2,10} ", Top_Chord_Bracing_Diagonal[0].MemberNo, "TO", Top_Chord_Bracing_Diagonal[Top_Chord_Bracing_Diagonal.Count - 1].MemberNo));
            if (Bottom_Chord_Bracing.Count > 0)
                ASTRA_Data.Add(string.Format("_BCB        {0,10} {1,10} {2,10} ", Bottom_Chord_Bracing[0].MemberNo, "TO", Bottom_Chord_Bracing[Bottom_Chord_Bracing.Count - 1].MemberNo));
            if (Stringers.Count > 0)
                ASTRA_Data.Add(string.Format("_STRINGER   {0,10} {1,10} {2,10} ", Stringers[0].MemberNo, "TO", Stringers[Stringers.Count - 1].MemberNo));
            if (Cross_Girders.Count > 0)
                ASTRA_Data.Add(string.Format("_XGIRDER    {0,10} {1,10} {2,10} ", Cross_Girders[0].MemberNo, "TO", Cross_Girders[Cross_Girders.Count - 1].MemberNo));
            ASTRA_Data.Add("END");

            #region Member_Properties
            Hashtable hash_tab = new Hashtable();
            hash_tab.Add("", "");

            hash_tab.Add("_L0L1", "   PRI   AX   0.024   IX   0.00001   IY   0.000741   IZ   0.001");
            hash_tab.Add("_L1L2", "   PRI   AX   0.024   IX   0.00001   IY   0.000741   IZ   0.001");
            hash_tab.Add("_L2L3", "   PRI   AX   0.030   IX   0.00001   IY   0.000946   IZ   0.001");
            hash_tab.Add("_L3L4", "   PRI   AX   0.039   IX   0.00001   IY   0.001000   IZ   0.002");
            hash_tab.Add("_L4L5", "   PRI   AX   0.042   IX   0.00001   IY   0.001000   IZ   0.001");
            hash_tab.Add("_L5L6", "   PRI   AX   0.042   IX   0.00001   IY   0.001000   IZ   0.001");
            hash_tab.Add("_L6L7", "   PRI   AX   0.042   IX   0.00001   IY   0.001000   IZ   0.001");
            hash_tab.Add("_L7L8", "   PRI   AX   0.042   IX   0.00001   IY   0.001000   IZ   0.001");
            hash_tab.Add("_L8L9", "   PRI   AX   0.042   IX   0.00001   IY   0.001000   IZ   0.001");
            hash_tab.Add("_L9L10", "               PRI   AX   0.042   IX   0.00001   IY   0.001000   IZ   0.001");
            hash_tab.Add("_L10L11", "              PRI   AX   0.042   IX   0.00001   IY   0.001000   IZ   0.001");
            hash_tab.Add("_U1U2", "   PRI   AX   0.027   IX   0.00001   IY   0.000807   IZ   0.000693");
            hash_tab.Add("_U2U3", "   PRI   AX   0.033   IX   0.00001   IY   0.000864   IZ   0.000922");
            hash_tab.Add("_U3U4", "   PRI   AX   0.036   IX   0.00001   IY   0.000896   IZ   0.001");
            hash_tab.Add("_U4U5", "   PRI   AX   0.038   IX   0.00001   IY   0.000914   IZ   0.001");
            hash_tab.Add("_U5U6", "   PRI   AX   0.038   IX   0.00001   IY   0.000914   IZ   0.001");
            hash_tab.Add("_U6U7", "   PRI   AX   0.038   IX   0.00001   IY   0.000914   IZ   0.001");
            hash_tab.Add("_U7U8", "   PRI   AX   0.038   IX   0.00001   IY   0.000914   IZ   0.001");
            hash_tab.Add("_U8U9", "   PRI   AX   0.038   IX   0.00001   IY   0.000914   IZ   0.001");
            hash_tab.Add("_U9U10", "               PRI   AX   0.038   IX   0.00001   IY   0.000914   IZ   0.001");
            hash_tab.Add("_U10U11", "              PRI   AX   0.038   IX   0.00001   IY   0.000914   IZ   0.001");
            //hash_tab.Add("_L1U1", "   PRI   AX   0.006   IX   0.00001   IY   0.000182   IZ   0.000036");
            //hash_tab.Add("_L2U2", "   PRI   AX   0.013   IX   0.00001   IY   0.000399   IZ   0.000302");
            //hash_tab.Add("_L3U3", "   PRI   AX   0.009   IX   0.00001   IY   0.000290   IZ   0.000127");
            //hash_tab.Add("_L4U4", "   PRI   AX   0.006   IX   0.00001   IY   0.000182   IZ   0.000036");
            //hash_tab.Add("_L5U5", "   PRI   AX   0.004   IX   0.00001   IY   0.000134   IZ   0.000016");
            //hash_tab.Add("_L6U6", "   PRI   AX   0.004   IX   0.00001   IY   0.000134   IZ   0.000016");
            //hash_tab.Add("_L7U7", "   PRI   AX   0.004   IX   0.00001   IY   0.000134   IZ   0.000016");
            //hash_tab.Add("_L8U8", "   PRI   AX   0.004   IX   0.00001   IY   0.000134   IZ   0.000016");
            //hash_tab.Add("_L9U9", "   PRI   AX   0.004   IX   0.00001   IY   0.000134   IZ   0.000016");
            //hash_tab.Add("_L10U10", "              PRI   AX   0.004   IX   0.00001   IY   0.000134   IZ   0.000016");
            //hash_tab.Add("_L11U11", "              PRI   AX   0.004   IX   0.00001   IY   0.000134   IZ   0.000016");

            //Chiranjit [2013 08 16]
            hash_tab.Add("_L1U1", "   PRI   AX   0.019   IX   0.00001   IY   0.000182   IZ   0.000036");
            hash_tab.Add("_L2U2", "   PRI   AX   0.019   IX   0.00001   IY   0.000399   IZ   0.000302");
            hash_tab.Add("_L3U3", "   PRI   AX   0.019   IX   0.00001   IY   0.000290   IZ   0.000127");
            hash_tab.Add("_L4U4", "   PRI   AX   0.019   IX   0.00001   IY   0.000182   IZ   0.000036");
            hash_tab.Add("_L5U5", "   PRI   AX   0.019   IX   0.00001   IY   0.000134   IZ   0.000016");
            hash_tab.Add("_L6U6", "   PRI   AX   0.019   IX   0.00001   IY   0.000134   IZ   0.000016");
            hash_tab.Add("_L7U7", "   PRI   AX   0.019   IX   0.00001   IY   0.000134   IZ   0.000016");
            hash_tab.Add("_L8U8", "   PRI   AX   0.019   IX   0.00001   IY   0.000134   IZ   0.000016");
            hash_tab.Add("_L9U9", "   PRI   AX   0.019   IX   0.00001   IY   0.000134   IZ   0.000016");
            hash_tab.Add("_L10U10", "              PRI   AX   0.019   IX   0.00001   IY   0.000134   IZ   0.000016");
            hash_tab.Add("_L11U11", "              PRI   AX   0.019   IX   0.00001   IY   0.000134   IZ   0.000016");

            hash_tab.Add("_ER", "     PRI   AX   0.022   IX   0.00001   IY   0.000666   IZ   0.000579");



            //hash_tab.Add("_L2U1", "   PRI   AX   0.019   IX   0.00001   IY   0.000621   IZ   0.000356");
            //hash_tab.Add("_L3U2", "   PRI   AX   0.014   IX   0.00001   IY   0.000474   IZ   0.000149");
            //hash_tab.Add("_L4U3", "   PRI   AX   0.009   IX   0.00001   IY   0.000290   IZ   0.000127");
            //hash_tab.Add("_L5U4", "   PRI   AX   0.006   IX   0.00001   IY   0.000182   IZ   0.000036");
            //hash_tab.Add("_L6U5", "   PRI   AX   0.006   IX   0.00001   IY   0.000182   IZ   0.000036");
            //hash_tab.Add("_L7U6", "   PRI   AX   0.006   IX   0.00001   IY   0.000182   IZ   0.000036");
            //hash_tab.Add("_L8U7", "   PRI   AX   0.006   IX   0.00001   IY   0.000182   IZ   0.000036");
            //hash_tab.Add("_L9U8", "   PRI   AX   0.006   IX   0.00001   IY   0.000182   IZ   0.000036");
            //hash_tab.Add("_L10U9", "               PRI   AX   0.006   IX   0.00001   IY   0.000182   IZ   0.000036");
            //hash_tab.Add("_L11U10", "              PRI   AX   0.006   IX   0.00001   IY   0.000182   IZ   0.000036");

            //Chiranjit [2013 08 16]
            hash_tab.Add("_L2U1", "   PRI   AX   0.019   IX   0.00001   IY   0.000621   IZ   0.000356");
            hash_tab.Add("_L3U2", "   PRI   AX   0.019   IX   0.00001   IY   0.000474   IZ   0.000149");
            hash_tab.Add("_L4U3", "   PRI   AX   0.019   IX   0.00001   IY   0.000290   IZ   0.000127");
            hash_tab.Add("_L5U4", "   PRI   AX   0.019   IX   0.00001   IY   0.000182   IZ   0.000036");
            hash_tab.Add("_L6U5", "   PRI   AX   0.019   IX   0.00001   IY   0.000182   IZ   0.000036");
            hash_tab.Add("_L7U6", "   PRI   AX   0.019   IX   0.00001   IY   0.000182   IZ   0.000036");
            hash_tab.Add("_L8U7", "   PRI   AX   0.019   IX   0.00001   IY   0.000182   IZ   0.000036");
            hash_tab.Add("_L9U8", "   PRI   AX   0.019   IX   0.00001   IY   0.000182   IZ   0.000036");
            hash_tab.Add("_L10U9", "               PRI   AX   0.019   IX   0.00001   IY   0.000182   IZ   0.000036");
            hash_tab.Add("_L11U10", "              PRI   AX   0.019   IX   0.00001   IY   0.000182   IZ   0.000036");


            hash_tab.Add("_TCS_ST", "              PRI   AX   0.006   IX   0.00001   IY   0.000027   IZ   0.000225");
            hash_tab.Add("_TCS_DIA", "             PRI   AX   0.006   IX   0.00001   IY   0.000027   IZ   0.000225");
            hash_tab.Add("_BCB", "    PRI   AX   0.002   IX   0.00001   IY   0.000001       IZ   0.000001");
            hash_tab.Add("_STRINGER", "            PRI   AX   0.048   IX   0.00001   IY   0.008           IZ   0.002");
            hash_tab.Add("_XGIRDER", "             PRI   AX   0.059   IX   0.00001   IY   0.009           IZ   0.005");




            //Bottom Vertical Members
            hash_tab.Add("_L1M1", "              PRI   AX   0.019   IX   0.00001   IY   0.000182   IZ   0.000036");
            hash_tab.Add("_L2M2", "              PRI   AX   0.019   IX   0.00001   IY   0.000182   IZ   0.000036");
            hash_tab.Add("_L3M3", "              PRI   AX   0.019   IX   0.00001   IY   0.000182   IZ   0.000036");
            hash_tab.Add("_L4M4", "              PRI   AX   0.019   IX   0.00001   IY   0.000182   IZ   0.000036");
            hash_tab.Add("_L5M5", "              PRI   AX   0.019   IX   0.00001   IY   0.000182   IZ   0.000036");









            #endregion Member_Properties


            ASTRA_Data.Add("MEMBER PROPERTY");
            for (int i = 0; i < _groups.Count; i++)
            {
                kStr = hash_tab[_groups[i]] as string;
                if (kStr != null)
                    ASTRA_Data.Add(string.Format("{0,-10} {1,-10}", _groups[i], kStr));
                else
                {
                    kStr = " PRI   AX   0.019   IX   0.00001   IY   0.000182   IZ   0.000036";
                    ASTRA_Data.Add(string.Format("{0,-10} {1,-10}", _groups[i], kStr));
                }
            }
            if (Top_Chord_Bracing.Count > 0)
                ASTRA_Data.Add("_TCS_ST    PRI   AX   0.006   IX   0.00001   IY   0.000027   IZ   0.000225");
            if (Top_Chord_Bracing_Diagonal.Count > 0)
                ASTRA_Data.Add("_TCS_DIA   PRI   AX   0.006   IX   0.00001   IY   0.000027   IZ   0.000225");
            if (Bottom_Chord_Bracing.Count > 0)
                ASTRA_Data.Add("_BCB       PRI   AX   0.002   IX   0.00001   IY   0.000001   IZ   0.000001");
            ASTRA_Data.Add("_STRINGER               PRI   AX   0.048   IX   0.00001   IY   0.008000   IZ   0.002000");
            ASTRA_Data.Add("_XGIRDER   PRI   AX   0.059   IX   0.00001   IY   0.009000   IZ   0.005000");
            for (int i = 0; i < _groups.Count; i++)
            {
                ASTRA_Data.Add("MEMBER TRUSS");
                ASTRA_Data.Add(_groups[i]);
            }
            if (Top_Chord_Bracing.Count > 0)
            {
                ASTRA_Data.Add("MEMBER TRUSS");
                ASTRA_Data.Add("_TCS_ST");
            }
            if (Top_Chord_Bracing_Diagonal.Count > 0)
            {
                ASTRA_Data.Add("MEMBER TRUSS");
                ASTRA_Data.Add("_TCS_DIA");
            }
            if (Bottom_Chord_Bracing.Count > 0)
            {

                ASTRA_Data.Add("MEMBER TRUSS");
                ASTRA_Data.Add("_BCB");
            }
            ASTRA_Data.Add("CONSTANT");
            ASTRA_Data.Add("E  2.110E8   ALL   ");
            ASTRA_Data.Add("DENSITY STEEL ALL");
            ASTRA_Data.Add("POISSON STEEL ALL");
            ASTRA_Data.Add("SUPPORT");
            //         ASTRA_Data.Add(string.Format("{0} {1} {2} {3} FIXED BUT FX MZ",
            //Bottom_Chord1[0].StartNode.NodeNo,
            //Bottom_Chord2[0].StartNode.NodeNo,
            //Bottom_Chord1[Bottom_Chord1.Count - 1].EndNode.NodeNo,
            //Bottom_Chord2[Bottom_Chord2.Count - 1].EndNode.NodeNo));



            ASTRA_Data.Add(string.Format("{0} {1} {2}",
   Bottom_Chord1[0].StartNode.NodeNo,
   Bottom_Chord2[0].StartNode.NodeNo, Start_Support));

            ASTRA_Data.Add(string.Format("{0} {1} {2}",
   Bottom_Chord1[Bottom_Chord1.Count - 1].EndNode.NodeNo,
   Bottom_Chord2[Bottom_Chord2.Count - 1].EndNode.NodeNo, End_Support));

            //ASTRA_Data.Add("FINISH");
            #endregion Member Grouping

            string joint_loads1 = (string.Format("{0} {1} {2} {3}   ",
   Bottom_Chord1[0].StartNode.NodeNo,
   Bottom_Chord2[0].StartNode.NodeNo,
   Bottom_Chord1[Bottom_Chord1.Count - 1].EndNode.NodeNo,
   Bottom_Chord2[Bottom_Chord2.Count - 1].EndNode.NodeNo));



            string joint_loads2 = (string.Format("{0} TO {1} {2} TO {3}   ",
   Bottom_Chord1[1].StartNode.NodeNo,
   Bottom_Chord2[1].StartNode.NodeNo,
   Bottom_Chord1[Bottom_Chord1.Count - 2].EndNode.NodeNo,
   Bottom_Chord2[Bottom_Chord2.Count - 2].EndNode.NodeNo));
            joint_loads2 = "";
            for (int i = 1; i < Bottom_Chord1.Count; i++)
            {
                joint_loads2 += Bottom_Chord1[i].StartNode.NodeNo + " ";
            }

            for (int i = 1; i < Bottom_Chord2.Count; i++)
            {
                joint_loads2 += " " + Bottom_Chord2[i].StartNode.NodeNo;
            }

            Joint_Load_Support = joint_loads1;
            Joint_Load_Edge = joint_loads2;
            ASTRA_Data.Add("LOAD  1  DL + SIDL");
            ASTRA_Data.Add("JOINT LOAD");
            ASTRA_Data.Add(Joint_Load_Edge + " FY -100.0");
            ASTRA_Data.Add(Joint_Load_Support + " FY  -50.0");
            ASTRA_Data.Add("PRINT SUPPORT REACTIONS");
            ASTRA_Data.Add("PERFORM ANALYSIS");
            ASTRA_Data.Add("FINISH");
            return ASTRA_Data;
        }


        public List<string> Get_Bridge_Data_2013_11_18(string data)
        {
            double x_inc = PanelLength;
            double z_inc = StringerSpacing;

            JointNode jn = null;

            double height_1 = Height / 2;

            List<double> list_x = new List<double>();
            List<double> list_z = new List<double>();
            JointNodeCollection Joints = new JointNodeCollection();
            List<JointNodeCollection> Joints_Bottom = new List<JointNodeCollection>();
            List<JointNodeCollection> Joints_Top = new List<JointNodeCollection>();

            List<string> ASTRA_Data = new List<string>();



            for (int i = 0; i <= NoOfPanel; i++)
            {
                list_x.Add(PanelLength * i);
            }
            for (int i = 0; i <= NoOfStringerBeam + 1; i++)
            {
                list_z.Add(StringerSpacing * i);
            }
            JointNodeCollection All_Joints = new JointNodeCollection();



            int x_count = 0;
            int z_count = 0;
            int mem_no = 1;
            int node_no = 1;
            x_count = 0;
            #region X = 0 to 60, Y = 0, Z = 8.43
            for (x_count = 0; x_count < list_x.Count; x_count++)
            {
                jn = new JointNode();
                jn.NodeNo = node_no++;
                jn.X = list_x[x_count];
                jn.Y = 0;
                jn.Z = list_z[list_z.Count - 1];
                All_Joints.Add(jn);
            }
            #endregion
            x_count = 0;

            #region X = 0 to 60, Y = 0, Z = 0
            for (x_count = 0; x_count < list_x.Count; x_count++)
            {
                jn = new JointNode();
                jn.NodeNo = node_no++;
                jn.X = list_x[x_count];
                jn.Y = 0;
                jn.Z = list_z[0];
                All_Joints.Add(jn);
            }
            #endregion
            x_count = 0;

            #region X = 6 to 54, Y = 6.35, Z = 8.43
            for (x_count = 1; x_count < (list_x.Count - 1); x_count++)
            {
                jn = new JointNode();
                jn.NodeNo = node_no++;
                jn.X = list_x[x_count];
                jn.Y = Height;
                jn.Z = list_z[list_z.Count - 1];
                All_Joints.Add(jn);
            }
            #endregion
            x_count = 0;


            #region X = 6 to 54, Y = 6.35 / 2, Z = 8.43
            for (x_count = 1; x_count < (list_x.Count - 1); x_count++)
            {
                jn = new JointNode();
                jn.NodeNo = node_no++;
                jn.X = list_x[x_count];
                jn.Y = height_1;
                jn.Z = list_z[list_z.Count - 1];
                All_Joints.Add(jn);
            }
            #endregion
            x_count = 0;


            #region X = 6 to 54, Y = 6.35, Z = 0
            for (x_count = 1; x_count < (list_x.Count - 1); x_count++)
            {
                jn = new JointNode();
                jn.NodeNo = node_no++;
                jn.X = list_x[x_count];
                jn.Y = Height;
                jn.Z = list_z[0];
                All_Joints.Add(jn);
            }
            #endregion
            x_count = 0;


            #region X = 6 to 54, Y = 6.35 / 2, Z = 0
            for (x_count = 1; x_count < (list_x.Count - 1); x_count++)
            {
                jn = new JointNode();
                jn.NodeNo = node_no++;
                jn.X = list_x[x_count];
                jn.Y = height_1;
                jn.Z = list_z[0];
                All_Joints.Add(jn);
            }
            #endregion
            x_count = 0;

            #region X = 0 to 60, Y = 6.35, Z = 0

            for (z_count = list_z.Count - 2; z_count > 0; z_count--)
            {
                for (x_count = 0; x_count < (list_x.Count); x_count++)
                {
                    jn = new JointNode();
                    jn.NodeNo = node_no++;
                    jn.X = list_x[x_count];
                    jn.Y = 0;
                    jn.Z = list_z[z_count];
                    All_Joints.Add(jn);
                }
            }
            #endregion
            x_count = 0;

            #region Write Joints
            ASTRA_Data.Clear();
            ASTRA_Data.Add("JOINT COORDINATE");
            for (x_count = 0; x_count < All_Joints.Count; x_count++)
            {
                ASTRA_Data.Add(All_Joints[x_count].ToString());
            }




            #endregion Write Joints
            ASTRA_Data.Add("MEMBER INCIDENCES");

            //Bottom Chord
            for (x_count = 0; x_count < NoOfPanel; x_count++)
            {
                mbr = new Member();
                mbr.MemberNo = mem_no;
                mbr.StartNode = All_Joints[x_count];
                mbr.EndNode = All_Joints[x_count + 1];
                Bottom_Chord1.Add(mbr);

                mbr = new Member();
                mbr.MemberNo = NoOfPanel + mem_no;
                mbr.StartNode = All_Joints[NoOfPanel + x_count + 1];
                mbr.EndNode = All_Joints[NoOfPanel + x_count + 2];
                Bottom_Chord2.Add(mbr);
                mem_no++;
            }

            Vertical1.Clear();
            Vertical2.Clear();
            //Vertical 



            MemberCollection Vertical = new MemberCollection();

            for (x_count = 1; x_count < NoOfPanel; x_count++)
            {
                mbr = new Member();
                mbr.StartNode.X = list_x[x_count];
                mbr.StartNode.Y = 0;
                mbr.StartNode.Z = list_z[list_z.Count - 1];
                mbr.EndNode.X = list_x[x_count];
                mbr.EndNode.Y = height_1;
                mbr.EndNode.Z = list_z[list_z.Count - 1];
                Vertical1.Add(mbr);


                mbr = new Member();
                mbr.StartNode.X = list_x[x_count];
                mbr.StartNode.Y = height_1;
                mbr.StartNode.Z = list_z[list_z.Count - 1];
                mbr.EndNode.X = list_x[x_count];
                mbr.EndNode.Y = Height;
                mbr.EndNode.Z = list_z[list_z.Count - 1];
                Vertical1.Add(mbr);


                mbr = new Member();
                mbr.StartNode.X = list_x[x_count];
                mbr.StartNode.Y = 0;
                mbr.StartNode.Z = list_z[0];
                mbr.EndNode.X = list_x[x_count];
                mbr.EndNode.Y = height_1;
                mbr.EndNode.Z = list_z[0];
                Vertical2.Add(mbr);

                mbr = new Member();
                mbr.StartNode.X = list_x[x_count];
                mbr.StartNode.Y = height_1;
                mbr.StartNode.Z = list_z[0];
                mbr.EndNode.X = list_x[x_count];
                mbr.EndNode.Y = Height;
                mbr.EndNode.Z = list_z[0];
                Vertical2.Add(mbr);
            }


            //Diagonal 
            for (x_count = 0; x_count < NoOfPanel / 2; x_count++)
            {
                if (x_count == 0)
                {
                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[list_z.Count - 1];

                    mbr.EndNode.X = list_x[x_count + 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[list_z.Count - 1];
                    Diagonal1.Add(mbr);


                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[0];

                    mbr.EndNode.X = list_x[x_count + 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[0];
                    Diagonal2.Add(mbr);
                }
                else
                {
                    mbr = new Member();

                    mbr.StartNode.X = list_x[x_count + 1];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[list_z.Count - 1];

                    mbr.EndNode.X = list_x[x_count];
                    mbr.EndNode.Y = height_1;
                    mbr.EndNode.Z = list_z[list_z.Count - 1];

                    Diagonal1.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = height_1;
                    mbr.StartNode.Z = list_z[list_z.Count - 1];

                    mbr.EndNode.X = list_x[x_count + 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[list_z.Count - 1];
                    Diagonal1.Add(mbr);


                    mbr = new Member();

                    mbr.StartNode.X = list_x[x_count + 1];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[0];

                    mbr.EndNode.X = list_x[x_count];
                    mbr.EndNode.Y = height_1;
                    mbr.EndNode.Z = list_z[0];

                    Diagonal2.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = height_1;
                    mbr.StartNode.Z = list_z[0];

                    mbr.EndNode.X = list_x[x_count + 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[0];
                    Diagonal2.Add(mbr);
                }
            }


            for (x_count = NoOfPanel; x_count > (NoOfPanel / 2); x_count--)
            {
                if (x_count == NoOfPanel)
                {
                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[list_z.Count - 1];

                    mbr.EndNode.X = list_x[x_count - 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[list_z.Count - 1];
                    Diagonal1.Add(mbr);




                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[0];
                    mbr.EndNode.X = list_x[x_count - 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[0];
                    Diagonal2.Add(mbr);
                }
                else
                {
                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count - 1];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[list_z.Count - 1];

                    mbr.EndNode.X = list_x[x_count];
                    mbr.EndNode.Y = height_1;
                    mbr.EndNode.Z = list_z[list_z.Count - 1];
                    Diagonal1.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = height_1;
                    mbr.StartNode.Z = list_z[list_z.Count - 1];

                    mbr.EndNode.X = list_x[x_count - 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[list_z.Count - 1];
                    Diagonal1.Add(mbr);





                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count - 1];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[0];

                    mbr.EndNode.X = list_x[x_count];
                    mbr.EndNode.Y = height_1;
                    mbr.EndNode.Z = list_z[0];
                    Diagonal2.Add(mbr);



                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = height_1;
                    mbr.StartNode.Z = list_z[0];

                    mbr.EndNode.X = list_x[x_count - 1];
                    mbr.EndNode.Y = Height;
                    mbr.EndNode.Z = list_z[0];
                    Diagonal2.Add(mbr);


                }
            }

            //Top Chord
            for (x_count = 1; x_count < NoOfPanel - 1; x_count++)
            {
                mbr = new Member();
                mbr.StartNode.X = list_x[x_count];
                mbr.StartNode.Y = Height;
                mbr.StartNode.Z = list_z[list_z.Count - 1];

                mbr.EndNode.X = list_x[x_count + 1];
                mbr.EndNode.Y = Height;
                mbr.EndNode.Z = list_z[list_z.Count - 1];
                Top_Chord1.Add(mbr);


                mbr = new Member();
                mbr.StartNode.X = list_x[x_count];
                mbr.StartNode.Y = Height;
                mbr.StartNode.Z = list_z[0];

                mbr.EndNode.X = list_x[x_count + 1];
                mbr.EndNode.Y = Height;
                mbr.EndNode.Z = list_z[0];
                Top_Chord2.Add(mbr);
            }

            //Stringers
            for (z_count = list_z.Count - 2; z_count > 0; z_count--)
            {
                for (x_count = 0; x_count < NoOfPanel; x_count++)
                {

                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[z_count];

                    mbr.EndNode.X = list_x[x_count + 1];
                    mbr.EndNode.Y = 0;
                    mbr.EndNode.Z = list_z[z_count];
                    Stringers.Add(mbr);
                }
            }


            //Coss Girders
            for (x_count = 0; x_count < list_x.Count; x_count++)
            {
                for (z_count = 0; z_count < list_z.Count - 1; z_count++)
                {
                    mbr = new Member();
                    mbr.StartNode.X = list_x[x_count];
                    mbr.StartNode.Y = 0;
                    mbr.StartNode.Z = list_z[z_count];

                    mbr.EndNode.X = list_x[x_count];
                    mbr.EndNode.Y = 0;
                    mbr.EndNode.Z = list_z[z_count + 1];
                    Cross_Girders.Add(mbr);
                }
            }


            //Top Chord Bracing Straight
            for (x_count = 1; x_count < NoOfPanel; x_count++)
            {
                mbr = new Member();
                mbr.StartNode.X = list_x[x_count];
                mbr.StartNode.Y = Height;
                mbr.StartNode.Z = list_z[z_count];

                mbr.EndNode.X = list_x[x_count];
                mbr.EndNode.Y = Height;
                mbr.EndNode.Z = list_z[0];
                Top_Chord_Bracing.Add(mbr);
            }

            //Top Chord Bracing Diagonal
            for (x_count = 1; x_count < NoOfPanel - 1; x_count++)
            {
                mbr = new Member();
                mbr.StartNode.X = list_x[x_count + 1];
                mbr.StartNode.Y = Height;
                mbr.StartNode.Z = list_z[z_count];

                mbr.EndNode.X = list_x[x_count];
                mbr.EndNode.Y = Height;
                mbr.EndNode.Z = list_z[0];
                Top_Chord_Bracing_Diagonal.Add(mbr);
            }

            //Top Chord Bracing Diagonal
            for (x_count = 1; x_count < NoOfPanel - 1; x_count++)
            {
                mbr = new Member();
                mbr.StartNode.X = list_x[x_count];
                mbr.StartNode.Y = Height;
                mbr.StartNode.Z = list_z[z_count];

                mbr.EndNode.X = list_x[x_count + 1];
                mbr.EndNode.Y = Height;
                mbr.EndNode.Z = list_z[0];
                Top_Chord_Bracing_Diagonal.Add(mbr);
            }


            //Bottom Chord Bracing Diagonal
            for (x_count = 0; x_count < NoOfPanel; x_count++)
            {
                mbr = new Member();
                mbr.StartNode.X = list_x[x_count + 1];
                mbr.StartNode.Y = 0;
                mbr.StartNode.Z = list_z[z_count];

                mbr.EndNode.X = list_x[x_count];
                mbr.EndNode.Y = 0;
                mbr.EndNode.Z = list_z[0];
                Bottom_Chord_Bracing.Add(mbr);
            }

            //Bottom Chord Bracing Diagonal
            for (x_count = 0; x_count < NoOfPanel; x_count++)
            {
                mbr = new Member();
                mbr.StartNode.X = list_x[x_count];
                mbr.StartNode.Y = 0;
                mbr.StartNode.Z = list_z[z_count];

                mbr.EndNode.X = list_x[x_count + 1];
                mbr.EndNode.Y = 0;
                mbr.EndNode.Z = list_z[0];
                Bottom_Chord_Bracing.Add(mbr);
            }




            mem_no = Bottom_Chord2[Bottom_Chord2.Count - 1].MemberNo + 1;


            foreach (var item in Bottom_Chord1)
            {
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord2)
            {
                ASTRA_Data.Add(item.ToString());
            }


            foreach (var item in Vertical1)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }


            foreach (var item in Vertical2)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }

            EndRakers.Clear();

            foreach (var item in Diagonal1)
            {

                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Diagonal2)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }
            EndRakers.Clear();
            EndRakers.Add(Diagonal1[0]);
            Diagonal1.RemoveAt(0);

            EndRakers.Add(Diagonal1[Diagonal1.Count - 1]);
            Diagonal1.RemoveAt(Diagonal1.Count - 1);

            EndRakers.Add(Diagonal2[0]);
            Diagonal2.RemoveAt(0);

            EndRakers.Add(Diagonal2[Diagonal2.Count - 1]);
            Diagonal2.RemoveAt(Diagonal2.Count - 1);

            foreach (var item in Top_Chord1)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Top_Chord2)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Stringers)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Cross_Girders)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Top_Chord_Bracing)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }

            foreach (var item in Top_Chord_Bracing_Diagonal)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }
            foreach (var item in Bottom_Chord_Bracing)
            {
                item.MemberNo = mem_no++;
                item.StartNode = All_Joints.GetJoints(item.StartNode.X, item.StartNode.Y, item.StartNode.Z, 0.5);
                item.EndNode = All_Joints.GetJoints(item.EndNode.X, item.EndNode.Y, item.EndNode.Z, 0.5);
                ASTRA_Data.Add(item.ToString());
            }




            List<string> _groups = new List<string>();
            #region Member Grouping
            ASTRA_Data.Add("START GROUP DEFINITION");
            string kStr = "";
            kStr = "";


            for (int i = 0; i < (NoOfPanel / 2); i++)
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


            for (int i = 0; i < (Top_Chord1.Count / 2); i++)
            {
                kStr = "_U" + (i + 1) + "U" + (i + 2);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Top_Chord1[i].MemberNo,
                    Top_Chord1[Top_Chord1.Count - (i + 1)].MemberNo,
                    Top_Chord2[i].MemberNo,
                    Top_Chord2[Top_Chord2.Count - (i + 1)].MemberNo);

                ASTRA_Data.Add(kStr);
            }

            for (int i = 0; i <= (Vertical1.Count / 2); i++)
            {
                kStr = "_L" + (i + 1) + "U" + (i + 1);
                _groups.Add(kStr);
                if (i == (Vertical1.Count / 2))
                {
                    kStr += string.Format("{0,10} {1,10}",
                    Vertical1[i].MemberNo,
                    Vertical2[i].MemberNo);

                }
                else
                {
                    kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                        Vertical1[i].MemberNo,
                        Vertical1[Vertical1.Count - (i + 1)].MemberNo,
                        Vertical2[i].MemberNo,
                        Vertical2[Vertical2.Count - (i + 1)].MemberNo);
                }
                ASTRA_Data.Add(kStr);
            }
            if (EndRakers.Count > 0)
            {
                _groups.Add("_ER");
                if (EndRakers.Count == 4)
                    ASTRA_Data.Add(string.Format("_ER {0,10} {1,10} {2,10} {3,10}", EndRakers[0].MemberNo, EndRakers[1].MemberNo, EndRakers[2].MemberNo, EndRakers[3].MemberNo));
            }

            for (int i = 0; i < (Diagonal1.Count / 2); i++)
            {
                kStr = "_L" + (i + 2) + "U" + (i + 1);
                _groups.Add(kStr);
                kStr += string.Format("{0,10} {1,10} {2,10} {3,10}",
                    Diagonal1[i].MemberNo,
                    Diagonal1[Diagonal1.Count - (i + 1)].MemberNo,
                    Diagonal2[i].MemberNo,
                    Diagonal2[Diagonal2.Count - (i + 1)].MemberNo);
                ASTRA_Data.Add(kStr);
            }

            if (Top_Chord_Bracing.Count > 0)
                ASTRA_Data.Add(string.Format("_TCS_ST     {0,10} {1,10} {2,10} ", Top_Chord_Bracing[0].MemberNo, "TO", Top_Chord_Bracing[Top_Chord_Bracing.Count - 1].MemberNo));
            if (Top_Chord_Bracing_Diagonal.Count > 0)
                ASTRA_Data.Add(string.Format("_TCS_DIA    {0,10} {1,10} {2,10} ", Top_Chord_Bracing_Diagonal[0].MemberNo, "TO", Top_Chord_Bracing_Diagonal[Top_Chord_Bracing_Diagonal.Count - 1].MemberNo));
            if (Bottom_Chord_Bracing.Count > 0)
                ASTRA_Data.Add(string.Format("_BCB        {0,10} {1,10} {2,10} ", Bottom_Chord_Bracing[0].MemberNo, "TO", Bottom_Chord_Bracing[Bottom_Chord_Bracing.Count - 1].MemberNo));
            if (Stringers.Count > 0)
                ASTRA_Data.Add(string.Format("_STRINGER   {0,10} {1,10} {2,10} ", Stringers[0].MemberNo, "TO", Stringers[Stringers.Count - 1].MemberNo));
            if (Cross_Girders.Count > 0)
                ASTRA_Data.Add(string.Format("_XGIRDER    {0,10} {1,10} {2,10} ", Cross_Girders[0].MemberNo, "TO", Cross_Girders[Cross_Girders.Count - 1].MemberNo));
            ASTRA_Data.Add("END");

            #region Member_Properties
            Hashtable hash_tab = new Hashtable();
            hash_tab.Add("", "");

            hash_tab.Add("_L0L1", "                PRI                AX                0.024                IX                0.00001                IY                0.000741                IZ                0.001");
            hash_tab.Add("_L1L2", "                PRI                AX                0.024                IX                0.00001                IY                0.000741                IZ                0.001");
            hash_tab.Add("_L2L3", "                PRI                AX                0.030                IX                0.00001                IY                0.000946                IZ                0.001");
            hash_tab.Add("_L3L4", "                PRI                AX                0.039                IX                0.00001                IY                0.001000                IZ                0.002");
            hash_tab.Add("_L4L5", "                PRI                AX                0.042                IX                0.00001                IY                0.001000                IZ                0.001");
            hash_tab.Add("_L5L6", "                PRI                AX                0.042                IX                0.00001                IY                0.001000                IZ                0.001");
            hash_tab.Add("_L6L7", "                PRI                AX                0.042                IX                0.00001                IY                0.001000                IZ                0.001");
            hash_tab.Add("_L7L8", "                PRI                AX                0.042                IX                0.00001                IY                0.001000                IZ                0.001");
            hash_tab.Add("_L8L9", "                PRI                AX                0.042                IX                0.00001                IY                0.001000                IZ                0.001");
            hash_tab.Add("_L9L10", "               PRI                AX                0.042                IX                0.00001                IY                0.001000                IZ                0.001");
            hash_tab.Add("_L10L11", "              PRI                AX                0.042                IX                0.00001                IY                0.001000                IZ                0.001");
            hash_tab.Add("_U1U2", "                PRI                AX                0.027                IX                0.00001                IY                0.000807                IZ                0.000693");
            hash_tab.Add("_U2U3", "                PRI                AX                0.033                IX                0.00001                IY                0.000864                IZ                0.000922");
            hash_tab.Add("_U3U4", "                PRI                AX                0.036                IX                0.00001                IY                0.000896                IZ                0.001");
            hash_tab.Add("_U4U5", "                PRI                AX                0.038                IX                0.00001                IY                0.000914                IZ                0.001");
            hash_tab.Add("_U5U6", "                PRI                AX                0.038                IX                0.00001                IY                0.000914                IZ                0.001");
            hash_tab.Add("_U6U7", "                PRI                AX                0.038                IX                0.00001                IY                0.000914                IZ                0.001");
            hash_tab.Add("_U7U8", "                PRI                AX                0.038                IX                0.00001                IY                0.000914                IZ                0.001");
            hash_tab.Add("_U8U9", "                PRI                AX                0.038                IX                0.00001                IY                0.000914                IZ                0.001");
            hash_tab.Add("_U9U10", "               PRI                AX                0.038                IX                0.00001                IY                0.000914                IZ                0.001");
            hash_tab.Add("_U10U11", "              PRI                AX                0.038                IX                0.00001                IY                0.000914                IZ                0.001");
            //hash_tab.Add("_L1U1", "                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            //hash_tab.Add("_L2U2", "                PRI                AX                0.013                IX                0.00001                IY                0.000399                IZ                0.000302");
            //hash_tab.Add("_L3U3", "                PRI                AX                0.009                IX                0.00001                IY                0.000290                IZ                0.000127");
            //hash_tab.Add("_L4U4", "                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            //hash_tab.Add("_L5U5", "                PRI                AX                0.004                IX                0.00001                IY                0.000134                IZ                0.000016");
            //hash_tab.Add("_L6U6", "                PRI                AX                0.004                IX                0.00001                IY                0.000134                IZ                0.000016");
            //hash_tab.Add("_L7U7", "                PRI                AX                0.004                IX                0.00001                IY                0.000134                IZ                0.000016");
            //hash_tab.Add("_L8U8", "                PRI                AX                0.004                IX                0.00001                IY                0.000134                IZ                0.000016");
            //hash_tab.Add("_L9U9", "                PRI                AX                0.004                IX                0.00001                IY                0.000134                IZ                0.000016");
            //hash_tab.Add("_L10U10", "              PRI                AX                0.004                IX                0.00001                IY                0.000134                IZ                0.000016");
            //hash_tab.Add("_L11U11", "              PRI                AX                0.004                IX                0.00001                IY                0.000134                IZ                0.000016");

            //Chiranjit [2013 08 16]
            hash_tab.Add("_L1U1", "                PRI                AX                0.019                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_L2U2", "                PRI                AX                0.019                IX                0.00001                IY                0.000399                IZ                0.000302");
            hash_tab.Add("_L3U3", "                PRI                AX                0.019                IX                0.00001                IY                0.000290                IZ                0.000127");
            hash_tab.Add("_L4U4", "                PRI                AX                0.019                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_L5U5", "                PRI                AX                0.019                IX                0.00001                IY                0.000134                IZ                0.000016");
            hash_tab.Add("_L6U6", "                PRI                AX                0.019                IX                0.00001                IY                0.000134                IZ                0.000016");
            hash_tab.Add("_L7U7", "                PRI                AX                0.019                IX                0.00001                IY                0.000134                IZ                0.000016");
            hash_tab.Add("_L8U8", "                PRI                AX                0.019                IX                0.00001                IY                0.000134                IZ                0.000016");
            hash_tab.Add("_L9U9", "                PRI                AX                0.019                IX                0.00001                IY                0.000134                IZ                0.000016");
            hash_tab.Add("_L10U10", "              PRI                AX                0.019                IX                0.00001                IY                0.000134                IZ                0.000016");
            hash_tab.Add("_L11U11", "              PRI                AX                0.019                IX                0.00001                IY                0.000134                IZ                0.000016");

            hash_tab.Add("_ER", "                  PRI                AX                0.022                IX                0.00001                IY                0.000666                IZ                0.000579");



            //hash_tab.Add("_L2U1", "                PRI                AX                0.019                IX                0.00001                IY                0.000621                IZ                0.000356");
            //hash_tab.Add("_L3U2", "                PRI                AX                0.014                IX                0.00001                IY                0.000474                IZ                0.000149");
            //hash_tab.Add("_L4U3", "                PRI                AX                0.009                IX                0.00001                IY                0.000290                IZ                0.000127");
            //hash_tab.Add("_L5U4", "                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            //hash_tab.Add("_L6U5", "                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            //hash_tab.Add("_L7U6", "                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            //hash_tab.Add("_L8U7", "                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            //hash_tab.Add("_L9U8", "                PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            //hash_tab.Add("_L10U9", "               PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");
            //hash_tab.Add("_L11U10", "              PRI                AX                0.006                IX                0.00001                IY                0.000182                IZ                0.000036");

            //Chiranjit [2013 08 16]
            hash_tab.Add("_L2U1", "                PRI                AX                0.019                IX                0.00001                IY                0.000621                IZ                0.000356");
            hash_tab.Add("_L3U2", "                PRI                AX                0.019                IX                0.00001                IY                0.000474                IZ                0.000149");
            hash_tab.Add("_L4U3", "                PRI                AX                0.019                IX                0.00001                IY                0.000290                IZ                0.000127");
            hash_tab.Add("_L5U4", "                PRI                AX                0.019                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_L6U5", "                PRI                AX                0.019                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_L7U6", "                PRI                AX                0.019                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_L8U7", "                PRI                AX                0.019                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_L9U8", "                PRI                AX                0.019                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_L10U9", "               PRI                AX                0.019                IX                0.00001                IY                0.000182                IZ                0.000036");
            hash_tab.Add("_L11U10", "              PRI                AX                0.019                IX                0.00001                IY                0.000182                IZ                0.000036");


            hash_tab.Add("_TCS_ST", "              PRI                AX                0.006                IX                0.00001                IY                0.000027                IZ                0.000225");
            hash_tab.Add("_TCS_DIA", "             PRI                AX                0.006                IX                0.00001                IY                0.000027                IZ                0.000225");
            hash_tab.Add("_BCB", "                 PRI                AX                0.002                IX                0.00001                IY                0.000001                    IZ                0.000001");
            hash_tab.Add("_STRINGER", "            PRI                AX                0.048                IX                0.00001                IY                0.008           IZ                0.002");
            hash_tab.Add("_XGIRDER", "             PRI                AX                0.059                IX                0.00001                IY                0.009           IZ                0.005");
            #endregion Member_Properties


            ASTRA_Data.Add("MEMBER PROPERTY");
            for (int i = 0; i < _groups.Count; i++)
            {
                kStr = hash_tab[_groups[i]] as string;
                if (kStr != null)
                    ASTRA_Data.Add(string.Format("{0,-10} {1,-10}", _groups[i], kStr));
            }
            ASTRA_Data.Add("_TCS_ST                 PRI                AX                0.006                IX                0.00001                IY                0.000027                IZ                0.000225");
            ASTRA_Data.Add("_TCS_DIA                PRI                AX                0.006                IX                0.00001                IY                0.000027                IZ                0.000225");
            ASTRA_Data.Add("_BCB                    PRI                AX                0.002                IX                0.00001                IY                0.000001                IZ                0.000001");
            ASTRA_Data.Add("_STRINGER               PRI                AX                0.048                IX                0.00001                IY                0.008000                IZ                0.002000");
            ASTRA_Data.Add("_XGIRDER                PRI                AX                0.059                IX                0.00001                IY                0.009000                IZ                0.005000");
            for (int i = 0; i < _groups.Count; i++)
            {
                ASTRA_Data.Add("MEMBER TRUSS");
                ASTRA_Data.Add(_groups[i]);
            }
            ASTRA_Data.Add("MEMBER TRUSS");
            ASTRA_Data.Add("_TCS_ST");
            ASTRA_Data.Add("MEMBER TRUSS");
            ASTRA_Data.Add("_TCS_DIA");
            ASTRA_Data.Add("MEMBER TRUSS");
            ASTRA_Data.Add("_BCB");
            ASTRA_Data.Add("CONSTANT");
            ASTRA_Data.Add("E  2.110E8                ALL                ");
            ASTRA_Data.Add("DENSITY STEEL ALL");
            ASTRA_Data.Add("POISSON STEEL ALL");
            ASTRA_Data.Add("SUPPORT");
            ASTRA_Data.Add(string.Format("{0} {1} {2} {3} FIXED BUT FX MZ",
                Bottom_Chord1[0].StartNode.NodeNo,
                Bottom_Chord2[0].StartNode.NodeNo,
                Bottom_Chord1[Bottom_Chord1.Count - 1].EndNode.NodeNo,
                Bottom_Chord2[Bottom_Chord2.Count - 1].EndNode.NodeNo));
            //ASTRA_Data.Add("FINISH");
            #endregion Member Grouping

            string joint_loads1 = (string.Format("{0} {1} {2} {3}   ",
                Bottom_Chord1[0].StartNode.NodeNo,
                Bottom_Chord2[0].StartNode.NodeNo,
                Bottom_Chord1[Bottom_Chord1.Count - 1].EndNode.NodeNo,
                Bottom_Chord2[Bottom_Chord2.Count - 1].EndNode.NodeNo));



            string joint_loads2 = (string.Format("{0} TO {1} {2} TO {3}   ",
                Bottom_Chord1[1].StartNode.NodeNo,
                Bottom_Chord2[1].StartNode.NodeNo,
                Bottom_Chord1[Bottom_Chord1.Count - 2].EndNode.NodeNo,
                Bottom_Chord2[Bottom_Chord2.Count - 2].EndNode.NodeNo));
            joint_loads2 = "";
            for (int i = 1; i < Bottom_Chord1.Count; i++)
            {
                joint_loads2 += Bottom_Chord1[i].StartNode.NodeNo + " ";
            }

            for (int i = 1; i < Bottom_Chord2.Count; i++)
            {
                joint_loads2 += " " + Bottom_Chord2[i].StartNode.NodeNo;
            }

            Joint_Load_Support = joint_loads1;
            Joint_Load_Edge = joint_loads2;
            ASTRA_Data.Add("LOAD  1  DL + SIDL");
            ASTRA_Data.Add("JOINT LOAD");
            ASTRA_Data.Add(Joint_Load_Edge + " FY -100.0");
            ASTRA_Data.Add(Joint_Load_Support + " FY  -50.0");
            ASTRA_Data.Add("PRINT SUPPORT REACTIONS");
            ASTRA_Data.Add("PERFORM ANALYSIS");
            ASTRA_Data.Add("FINISH");
            return ASTRA_Data;
        }




        public bool IS_VERTICAL_AXIS_Y { get; set; }
    }

}
