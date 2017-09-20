using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//namespace AstraFunctionOne.BridgeDesign.SteelTruss
namespace AstraInterface.TrussBridge
{
    public class CreateSteel_Warren_1_TrussData
    {
        public CreateSteel_Warren_1_TrussData()
        {
            Length = 50.0;
            Breadth = 5.40;
            Height = 6.0;
        }
        public double Length { get; set; }
        public double PanelLength
        {
            get
            {
                return (Length /(double) NoOfPanel);
            }
        }
        public double Breadth { get; set; }
        public int NoOfStringerBeam 
        {
            get { return 3; } 
        }
        public double StringerSpacing 
        {
            get
            {
                return (Breadth / (NoOfStringerBeam + 1));
            }
        }
        public double Height { get; set; }
        public int NoOfPanel
        {
            get
            {
                return (10);
            }
        }

        public bool CreateData(string fileName)
        {
            try
            {
                List<string> list = new List<string>();
                list.Add("ASTRA SPACE FRAME WITH TRUSS DEAD LOAD+SIDL+MOVING LOAD IRC 24R TRACK");
                list.Add("UNIT KN MET");
                //list.AddRange(GetJointCoordinates());
                list.AddRange(Get_MyntduBridge_Data());
                list.AddRange(GetMemberConnectivityAndOthers());
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
            //list.Add("1	0.000	0.000	8.430");
            //list.Add("2	6.000	0.000	8.430");
            //list.Add("3	12.000	0.000	8.430");
            //list.Add("4	18.000	0.000	8.430");
            //list.Add("5	24.000	0.000	8.430");
            //list.Add("6	30.000	0.000	8.430");
            //list.Add("7	36.000	0.000	8.430");
            //list.Add("8	42.000	0.000	8.430");
            //list.Add("9	48.000	0.000	8.430");
            //list.Add("10	54.000	0.000	8.430");
            //list.Add("11	60.000	0.000	8.430");
            //list.Add("12	0.000	0.000	0.000");
            //list.Add("13	6.000	0.000	0.000");
            //list.Add("14	12.000	0.000	0.000");
            //list.Add("15	18.000	0.000	0.000");
            //list.Add("16	24.000	0.000	0.000");
            //list.Add("17	30.000	0.000	0.000");
            //list.Add("18	36.000	0.000	0.000");
            //list.Add("19	42.000	0.000	0.000");
            //list.Add("20	48.000	0.000	0.000");
            //list.Add("21	54.000	0.000	0.000");
            //list.Add("22	60.000	0.000	0.000");
            //list.Add("23	6.000	6.350	8.430");
            //list.Add("24	12.000	6.350	8.430");
            //list.Add("25	18.000	6.350	8.430");
            //list.Add("26	24.000	6.350	8.430");
            //list.Add("27	30.000	6.350	8.430");
            //list.Add("28	36.000	6.350	8.430");
            //list.Add("29	42.000	6.350	8.430");
            //list.Add("30	48.000	6.350	8.430");
            //list.Add("31	54.000	6.350	8.430");
            //list.Add("32	6.000	6.350	0.000");
            //list.Add("33	12.000	6.350	0.000");
            //list.Add("34	18.000	6.350	0.000");
            //list.Add("35	24.000	6.350	0.000");
            //list.Add("36	30.000	6.350	0.000");
            //list.Add("37	36.000	6.350	0.000");
            //list.Add("38	42.000	6.350	0.000");
            //list.Add("39	48.000	6.350	0.000");
            //list.Add("40	54.000	6.350	0.000");
            //list.Add("41	0.000	0.000	6.990");
            //list.Add("42	6.000	0.000	6.990");
            //list.Add("43	12.000	0.000	6.990");
            //list.Add("44	18.000	0.000	6.990");
            //list.Add("45	24.000	0.000	6.990");
            //list.Add("46	30.000	0.000	6.990");
            //list.Add("47	36.000	0.000	6.990");
            //list.Add("48	42.000	0.000	6.990");
            //list.Add("49	48.000	0.000	6.990");
            //list.Add("50	54.000	0.000	6.990");
            //list.Add("51	60.000	0.000	6.990");
            //list.Add("52	0.000	0.000	5.140");
            //list.Add("53	6.000	0.000	5.140");
            //list.Add("54	12.000	0.000	5.140");
            //list.Add("55	18.000	0.000	5.140");
            //list.Add("56	24.000	0.000	5.140");
            //list.Add("57	30.000	0.000	5.140");
            //list.Add("58	36.000	0.000	5.140");
            //list.Add("59	42.000	0.000	5.140");
            //list.Add("60	48.000	0.000	5.140");
            //list.Add("61	54.000	0.000	5.140");
            //list.Add("62	60.000	0.000	5.140");
            //list.Add("63	0.000	0.000	3.290");
            //list.Add("64	6.000	0.000	3.290");
            //list.Add("65	12.000	0.000	3.290");
            //list.Add("66	18.000	0.000	3.290");
            //list.Add("67	24.000	0.000	3.290");
            //list.Add("68	30.000	0.000	3.290");
            //list.Add("69	36.000	0.000	3.290");
            //list.Add("70	42.000	0.000	3.290");
            //list.Add("71	48.000	0.000	3.290");
            //list.Add("72	54.000	0.000	3.290");
            //list.Add("73	60.000	0.000	3.290");
            //list.Add("74	0.000	0.000	1.440");
            //list.Add("75	6.000	0.000	1.440");
            //list.Add("76	12.000	0.000	1.440");
            //list.Add("77	18.000	0.000	1.440");
            //list.Add("78	24.000	0.000	1.440");
            //list.Add("79	30.000	0.000	1.440");
            //list.Add("80	36.000	0.000	1.440");
            //list.Add("81	42.000	0.000	1.440");
            //list.Add("82	48.000	0.000	1.440");
            //list.Add("83	54.000	0.000	1.440");
            //list.Add("84	60.000	0.000	1.440");
            //list.Add("MEMBER CONNECTIVITYS");
            list.Add("MEMBER CONNECTIVITY");
            list.Add("1	1	2");
            list.Add("2	2	3");
            list.Add("3	3	4");
            list.Add("4	4	5");
            list.Add("5	5	6");
            list.Add("6	6	7");
            list.Add("7	7	8");
            list.Add("8	8	9");
            list.Add("9	9	10");
            list.Add("10	10	11");
            list.Add("11	12	13");
            list.Add("12	13	14");
            list.Add("13	14	15");
            list.Add("14	15	16");
            list.Add("15	16	17");
            list.Add("16	17	18");
            list.Add("17	18	19");
            list.Add("18	19	20");
            list.Add("19	20	21");
            list.Add("20	21	22");
            list.Add("21	2	23");
            list.Add("22	3	24");
            list.Add("23	4	25");
            list.Add("24	5	26");
            list.Add("25	6	27");
            list.Add("26	7	28");
            list.Add("27	8	29");
            list.Add("28	9	30");
            list.Add("29	10	31");
            list.Add("30	13	32");
            list.Add("31	14	33");
            list.Add("32	15	34");
            list.Add("33	16	35");
            list.Add("34	17	36");
            list.Add("35	18	37");
            list.Add("36	19	38");
            list.Add("37	20	39");
            list.Add("38	21	40");
            list.Add("39	1	23");
            list.Add("40	23	3");
            list.Add("41	24	4");
            list.Add("42	25	5");
            list.Add("43	31	11");
            list.Add("44	31	9");
            list.Add("45	30	8");
            list.Add("46	29	7");
            list.Add("47	28	6");
            list.Add("48	26	6");
            list.Add("49	40	22");
            list.Add("50	40	20");
            list.Add("51	39	19");
            list.Add("52	38	18");
            list.Add("53	12	32");
            list.Add("54	32	14");
            list.Add("55	33	15");
            list.Add("56	34	16");
            list.Add("57	35	17");
            list.Add("58	37	17");
            list.Add("59	23	24");
            list.Add("60	24	25");
            list.Add("61	25	26");
            list.Add("62	26	27");
            list.Add("63	27	28");
            list.Add("64	28	29");
            list.Add("65	29	30");
            list.Add("66	30	31");
            list.Add("67	32	33");
            list.Add("68	33	34");
            list.Add("69	34	35");
            list.Add("70	35	36");
            list.Add("71	36	37");
            list.Add("72	37	38");
            list.Add("73	38	39");
            list.Add("74	39	40");
            list.Add("75	41	42");
            list.Add("76	42	43");
            list.Add("77	43	44");
            list.Add("78	44	45");
            list.Add("79	45	46");
            list.Add("80	46	47");
            list.Add("81	47	48");
            list.Add("82	48	49");
            list.Add("83	49	50");
            list.Add("84	50	51");
            list.Add("85	52	53");
            list.Add("86	53	54");
            list.Add("87	54	55");
            list.Add("88	55	56");
            list.Add("89	56	57");
            list.Add("90	57	58");
            list.Add("91	58	59");
            list.Add("92	59	60");
            list.Add("93	60	61");
            list.Add("94	61	62");
            list.Add("95	63	64");
            list.Add("96	64	65");
            list.Add("97	65	66");
            list.Add("98	66	67");
            list.Add("99	67	68");
            list.Add("100	68	69");
            list.Add("101	69	70");
            list.Add("102	70	71");
            list.Add("103	71	72");
            list.Add("104	72	73");
            list.Add("105	74	75");
            list.Add("106	75	76");
            list.Add("107	76	77");
            list.Add("108	77	78");
            list.Add("109	78	79");
            list.Add("110	79	80");
            list.Add("111	80	81");
            list.Add("112	81	82");
            list.Add("113	82	83");
            list.Add("114	83	84");
            list.Add("115	12	74");
            list.Add("116	74	63");
            list.Add("117	63	52");
            list.Add("118	52	41");
            list.Add("119	41	1");
            list.Add("120	13	75");
            list.Add("121	75	64");
            list.Add("122	64	53");
            list.Add("123	53	42");
            list.Add("124	42	2");
            list.Add("125	14	76");
            list.Add("126	76	65");
            list.Add("127	65	54");
            list.Add("128	54	43");
            list.Add("129	43	3");
            list.Add("130	15	77");
            list.Add("131	77	66");
            list.Add("132	66	55");
            list.Add("133	55	44");
            list.Add("134	44	4");
            list.Add("135	16	78");
            list.Add("136	78	67");
            list.Add("137	67	56");
            list.Add("138	56	45");
            list.Add("139	45	5");
            list.Add("140	17	79");
            list.Add("141	79	68");
            list.Add("142	68	57");
            list.Add("143	57	46");
            list.Add("144	46	6");
            list.Add("145	18	80");
            list.Add("146	80	69");
            list.Add("147	69	58");
            list.Add("148	58	47");
            list.Add("149	47	7");
            list.Add("150	19	81");
            list.Add("151	81	70");
            list.Add("152	70	59");
            list.Add("153	59	48");
            list.Add("154	48	8");
            list.Add("155	20	82");
            list.Add("156	82	71");
            list.Add("157	71	60");
            list.Add("158	60	49");
            list.Add("159	49	9");
            list.Add("160	21	83");
            list.Add("161	83	72");
            list.Add("162	72	61");
            list.Add("163	61	50");
            list.Add("164	50	10");
            list.Add("165	22	84");
            list.Add("166	84	73");
            list.Add("167	73	62");
            list.Add("168	62	51");
            list.Add("169	51	11");
            list.Add("170	32	23");
            list.Add("171	33	24");
            list.Add("172	34	25");
            list.Add("173	35	26");
            list.Add("174	36	27");
            list.Add("175	37	28");
            list.Add("176	38	29");
            list.Add("177	39	30");
            list.Add("178	40	31");
            list.Add("179	32	24");
            list.Add("180	33	25");
            list.Add("181	34	26");
            list.Add("182	35	27");
            list.Add("183	36	28");
            list.Add("184	37	29");
            list.Add("185	38	30");
            list.Add("186	39	31");
            list.Add("187	33	23");
            list.Add("188	34	24");
            list.Add("189	35	25");
            list.Add("190	36	26");
            list.Add("191	37	27");
            list.Add("192	38	28");
            list.Add("193	39	29");
            list.Add("194	40	30");
            list.Add("195	12	2");
            list.Add("196	2	14");
            list.Add("197	14	4");
            list.Add("198	4	16");
            list.Add("199	16	6");
            list.Add("200	6	18");
            list.Add("201	18	8");
            list.Add("202	8	20");
            list.Add("203	20	10");
            list.Add("204	10	22");
            list.Add("205	1	13");
            list.Add("206	13	3");
            list.Add("207	3	15");
            list.Add("208	15	5");
            list.Add("209	5	17");
            list.Add("210	17	7");
            list.Add("211	7	19");
            list.Add("212	19	9");
            list.Add("213	9	21");
            list.Add("214	21	11");
            list.Add("START GROUP DEFINITION");
            list.Add("_L0L1	1	10	11	20");
            list.Add("_L1L2	2	9	12	19");
            list.Add("_L2L3	3	8	13	18");
            list.Add("_L3L4	4	7	14	17");
            list.Add("_L4L5	5	6	15	16");
            list.Add("_U1U2	59	66	67	74");
            list.Add("_U2U3	60	65	68	73");
            list.Add("_U3U4	61	64	69	72");
            list.Add("_U4U5	62	63	70	71");
            list.Add("_L1U1	21	29	30	38");
            list.Add("_L2U2	22	28	31	37");
            list.Add("_L3U3	23	27	32	36");
            list.Add("_L4U4	24	26	33	35");
            list.Add("_L5U5	25	34");
            list.Add("_ER	39	43	49	53");
            list.Add("_L2U1	40	44	50	54");
            list.Add("_L3U2	41	45	51	55");
            list.Add("_L4U3	42	46	52	56");
            list.Add("_L5U4	47	48	57	58");
            list.Add("_TCS_ST	170	TO	178	");
            list.Add("_TCS_DIA	179	TO	194	");
            list.Add("_BCB	195	TO	214	");
            list.Add("_STRINGER	75	TO	114	");
            list.Add("_XGIRDER	115	TO	169	");
            list.Add("END");
            list.Add("MEMBER PROPERTY INDIAN");
            list.Add("_L0L1	PRI	AX	0.024	IX	0.00001	IY	0.000741	IZ	0.001");
            list.Add("_L1L2	PRI	AX	0.024	IX	0.00001	IY	0.000741	IZ	0.001");
            list.Add("_L2L3	PRI	AX	0.030	IX	0.00001	IY	0.000946	IZ	0.001");
            list.Add("_L3L4	PRI	AX	0.039	IX	0.00001	IY	0.001	IZ	0.002");
            list.Add("_L4L5	PRI	AX	0.042	IX	0.00001	IY	0.001	IZ	0.001");
            list.Add("_U1U2	PRI	AX	0.027	IX	0.00001	IY	0.000807	IZ	0.000693");
            list.Add("_U2U3	PRI	AX	0.033	IX	0.00001	IY	0.000864	IZ	0.000922");
            list.Add("_U3U4	PRI	AX	0.036	IX	0.00001	IY	0.000896	IZ	0.001");
            list.Add("_U4U5	PRI	AX	0.038	IX	0.00001	IY	0.000914	IZ	0.001");
            list.Add("_L1U1	PRI	AX	0.006	IX	0.00001	IY	0.000182	IZ	0.000036");
            list.Add("_L2U2	PRI	AX	0.013	IX	0.00001	IY	0.000399	IZ	0.000302");
            list.Add("_L3U3	PRI	AX	0.009	IX	0.00001	IY	0.00029	IZ	0.000127");
            list.Add("_L4U4	PRI	AX	0.006	IX	0.00001	IY	0.000182	IZ	0.000036");
            list.Add("_L5U5	PRI	AX	0.004	IX	0.00001	IY	0.000134	IZ	0.000016");
            list.Add("_ER	PRI	AX	0.022	IX	0.00001	IY	0.000666	IZ	0.000579");
            list.Add("_L2U1	PRI	AX	0.019	IX	0.00001	IY	0.000621	IZ	0.000356");
            list.Add("_L3U2	PRI	AX	0.014	IX	0.00001	IY	0.000474	IZ	0.000149");
            list.Add("_L4U3	PRI	AX	0.009	IX	0.00001	IY	0.00029	IZ	0.000127");
            list.Add("_L5U4	PRI	AX	0.006	IX	0.00001	IY	0.000182	IZ	0.000036");
            list.Add("_TCS_ST	PRI	AX	0.006	IX	0.00001	IY	0.000027	IZ	0.000225");
            list.Add("_TCS_DIA	PRI	AX	0.006	IX	0.00001	IY	0.000027	IZ	0.000225");
            list.Add("_BCB	PRI	AX	0.002	IX	0.00001	IY	0.000001	IZ	0.000001");
            list.Add("_STRINGER	PRI	AX	0.048	IX	0.00001	IY	0.008	IZ	0.002");
            list.Add("_XGIRDER	PRI	AX	0.059	IX	0.00001	IY	0.009	IZ	0.005");
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
            list.Add("E  2.110E8	ALL	");
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
            //list.Add("MEMBER CONNECTIVITY");
            //list.Add("1	1	2	");
            //list.Add("2	2	3	");
            //list.Add("3	3	4	");
            //list.Add("4	4	5	");
            //list.Add("5	5	6	");
            //list.Add("6	6	7	");
            //list.Add("7	7	8");
            //list.Add("8	8	9");
            //list.Add("9	9	10");
            //list.Add("10	10	11");
            //list.Add("11	12	13");
            //list.Add("12	13	14");
            //list.Add("13	14	15");
            //list.Add("14	15	16");
            //list.Add("15	16	17");
            //list.Add("16	17	18");
            //list.Add("17	18	19");
            //list.Add("18	19	20");
            //list.Add("19	20	21");
            //list.Add("20	21	22");
            //list.Add("21	2	23");
            //list.Add("22	3	24");
            //list.Add("23	4	25");
            //list.Add("24	5	26");
            //list.Add("25	6	27");
            //list.Add("26	7	28");
            //list.Add("27	8	29");
            //list.Add("28	9	30");
            //list.Add("29	10	31");
            //list.Add("30	13	32");
            //list.Add("31	14	33");
            //list.Add("32	15	34");
            //list.Add("33	16	35");
            //list.Add("34	17	36");
            //list.Add("35	18	37");
            //list.Add("36	19	38");
            //list.Add("37	20	39");
            //list.Add("38	21	40");
            //list.Add("39	1	23");
            //list.Add("40	23	3");
            //list.Add("41	24	4");
            //list.Add("42	25	5");
            //list.Add("43	31	11");
            //list.Add("44	31	9");
            //list.Add("45	30	8");
            //list.Add("46	29	7");
            //list.Add("47	28	6");
            //list.Add("48	26	6");
            //list.Add("49	40	22");
            //list.Add("50	40	20");
            //list.Add("51	39	19");
            //list.Add("52	38	18");
            //list.Add("53	12	32");
            //list.Add("54	32	14");
            //list.Add("55	33	15");
            //list.Add("56	34	16");
            //list.Add("57	35	17");
            //list.Add("58	37	17");
            //list.Add("59	23	24");
            //list.Add("60	24	25");
            //list.Add("61	25	26");
            //list.Add("62	26	27");
            //list.Add("63	27	28");
            //list.Add("64	28	29");
            //list.Add("65	29	30");
            //list.Add("66	30	31");
            //list.Add("67	32	33");
            //list.Add("68	33	34");
            //list.Add("69	34	35");
            //list.Add("70	35	36");
            //list.Add("71	36	37");
            //list.Add("72	37	38");
            //list.Add("73	38	39");
            //list.Add("74	39	40");
            //list.Add("75	41	42");
            //list.Add("76	42	43");
            //list.Add("77	43	44");
            //list.Add("78	44	45");
            //list.Add("79	45	46");
            //list.Add("80	46	47");
            //list.Add("81	47	48");
            //list.Add("82	48	49");
            //list.Add("83	49	50");
            //list.Add("84	50	51");
            //list.Add("85	52	53");
            //list.Add("86	53	54");
            //list.Add("87	54	55");
            //list.Add("88	55	56");
            //list.Add("89	56	57");
            //list.Add("90	57	58");
            //list.Add("91	58	59");
            //list.Add("92	59	60");
            //list.Add("93	60	61");
            //list.Add("94	61	62");
            //list.Add("95	63	64");
            //list.Add("96	64	65");
            //list.Add("97	65	66");
            //list.Add("98	66	67");
            //list.Add("99	67	68");
            //list.Add("100	68	69");
            //list.Add("101	69	70");
            //list.Add("102	70	71");
            //list.Add("103	71	72");
            //list.Add("104	72	73");
            //list.Add("105	12	63");
            //list.Add("106	63	52");
            //list.Add("107	52	41");
            //list.Add("108	41	1");
            //list.Add("109	13	64");
            //list.Add("110	64	53");
            //list.Add("111	53	42");
            //list.Add("112	42	2");
            //list.Add("113	14	65");
            //list.Add("114	65	54");
            //list.Add("115	54	43");
            //list.Add("116	43	3");
            //list.Add("117	15	66");
            //list.Add("118	66	55");
            //list.Add("119	55	44");
            //list.Add("120	44	4");
            //list.Add("121	16	67");
            //list.Add("122	67	56");
            //list.Add("123	56	45");
            //list.Add("124	45	5");
            //list.Add("125	17	68");
            //list.Add("126	68	57");
            //list.Add("127	57	46");
            //list.Add("128	46	6");
            //list.Add("129	18	69");
            //list.Add("130	69	58");
            //list.Add("131	58	47");
            //list.Add("132	47	7");
            //list.Add("133	19	70");
            //list.Add("134	70	59");
            //list.Add("135	59	48");
            //list.Add("136	48	8");
            //list.Add("137	20	71");
            //list.Add("138	71	60");
            //list.Add("139	60	49");
            //list.Add("140	49	9");
            //list.Add("141	21	72");
            //list.Add("142	72	61");
            //list.Add("143	61	50");
            //list.Add("144	50	10");
            //list.Add("145	22	73");
            //list.Add("146	73	62");
            //list.Add("147	62	51");
            //list.Add("148	51	11");
            //list.Add("149	32	23");
            //list.Add("150	33	24");
            //list.Add("151	34	25");
            //list.Add("152	35	26");
            //list.Add("153	36	27");
            //list.Add("154	37	28");
            //list.Add("155	38	29");
            //list.Add("156	39	30");
            //list.Add("157	40	31");
            //list.Add("158	32	24");
            //list.Add("159	33	25");
            //list.Add("160	34	26");
            //list.Add("161	35	27");
            //list.Add("162	36	28");
            //list.Add("163	37	29");
            //list.Add("164	38	30");
            //list.Add("165	39	31");
            //list.Add("166	33	23");
            //list.Add("167	34	24");
            //list.Add("168	35	25");
            //list.Add("169	36	26");
            //list.Add("170	37	27");
            //list.Add("171	38	28");
            //list.Add("172	39	29");
            //list.Add("173	40	30");
            //list.Add("174	12	2");
            //list.Add("175	2	14");
            //list.Add("176	14	4");
            //list.Add("177	4	16");
            //list.Add("178	16	6");
            //list.Add("179	6	18");
            //list.Add("180	18	8");
            //list.Add("181	8	20");
            //list.Add("182	20	10");
            //list.Add("183	10	22");
            //list.Add("184	1	13");
            //list.Add("185	13	3");
            //list.Add("186	3	15");
            //list.Add("187	15	5");
            //list.Add("188	5	17");
            //list.Add("189	17	7");
            //list.Add("190	7	19");
            //list.Add("191	19	9");
            //list.Add("192	9	21");
            //list.Add("193	21	11	");
            //list.Add("START	GROUP	DEFINITION");
            //list.Add("_L0L1	1	10	11	20");
            //list.Add("_L1L2	2	9	12	19");
            //list.Add("_L2L3	3	8	13	18");
            //list.Add("_L3L4	4	7	14	17");
            //list.Add("_L4L5	5	6	15	16");
            //list.Add("_U1U2	59	66	67	74");
            //list.Add("_U2U3	60	65	68	73");
            //list.Add("_U3U4	61	64	69	72");
            //list.Add("_U4U5	62	63	70	71");
            //list.Add("_L1U1	21	29	30	38");
            //list.Add("_L2U2	22	28	31	37");
            //list.Add("_L3U3	23	27	32	36");
            //list.Add("_L4U4	24	26	33	35");
            //list.Add("_L5U5	25	34");
            //list.Add("_ER	39	43	49	53");
            //list.Add("_L2U1	40	44	50	54");
            //list.Add("_L3U2	41	45	51	55");
            //list.Add("_L4U3	42	46	52	56");
            //list.Add("_L5U4	47	48	57	58");
            //list.Add("_TCS_ST	        149	TO	157	");
            //list.Add("_TCS_DIA	158	TO	173	");
            //list.Add("_BCB1174	TO	179");
            //list.Add("_BCB2180	TO	193	");
            //list.Add("_STRINGER	75	TO	104	");
            //list.Add("_XGIRDER	105	TO	148");
            //list.Add("END");
            //list.Add("MEMBER	PROPERTY	INDIAN");
            //list.Add("_L0L1	PRI	AX	0.024	IX	0.00001	IY	0.000741	IZ	0.001");
            //list.Add("_L1L2	PRI	AX	0.03	IX	0.00001	IY	0.000741	IZ	0.001");
            //list.Add("_L2L3	PRI	AX	0.03	IX	0.00001	IY	0.000946	IZ	0.001");
            //list.Add("_L3L4	PRI	AX	0.039	IX	0.00001	IY	0.001	IZ	0.002");
            //list.Add("_L4L5	PRI	AX	0.042	IX	0.00001	IY	0.001	IZ	0.001");
            //list.Add("_U1U2	PRI	AX	0.027	IX	0.00001	IY	0.000807	IZ	0.000693");
            //list.Add("_U2U3	PRI	AX	0.033	IX	0.00001	IY	0.000864	IZ	0.000922");
            //list.Add("_U3U4	PRI	AX	0.036	IX	0.00001	IY	0.000896	IZ	0.001");
            //list.Add("_U4U5	PRI	AX	0.038	IX	0.00001	IY	0.000914	IZ	0.001");
            //list.Add("_L1U1	PRI	AX	0.006	IX	0.00001	IY	0.000182	IZ	0.000036");
            //list.Add("_L2U2	PRI	AX	0.013	IX	0.00001	IY	0.000399	IZ	0.000302");
            //list.Add("_L3U3	PRI	AX	0.009	IX	0.00001	IY	0.00029	IZ	0.000127");
            //list.Add("_L4U4	PRI	AX	0.006	IX	0.00001	IY	0.000182	IZ	0.000036");
            //list.Add("_L5U5	PRI	AX	0.004	IX	0.00001	IY	0.000134	IZ	0.000016");
            //list.Add("_ER	PRI	AX	0.022	IX	0.00001	IY	0.000666	IZ	0.000579");
            //list.Add("_L2U1	PRI	AX	0.019	IX	0.00001	IY	0.000621	IZ	0.0000356");
            //list.Add("_L3U2	PRI	AX	0.014	IX	0.00001	IY	0.000474	IZ	0.000149");
            //list.Add("_L4U3	PRI	AX	0.009	IX	0.00001	IY	0.00029	IZ	0.000127");
            //list.Add("_L5U4	PRI	AX	0.006	IX	0.00001	IY	0.000182	IZ	0.000036");
            //list.Add("_TCS_ST	PRI	AX	0.006	IX	0.00001	IY	0.000027	IZ	0.000225");
            //list.Add("_TCS_DIA	PRI	AX	0.006	IX	0.00001	IY	0.000027	IZ	0.000225");
            //list.Add("_BCB1	PRI	AX	0.002	IX	0.00001	IY	0.000001	IZ	0.000001");
            //list.Add("_BCB2	PRI	AX	0.002	IX	0.00001	IY	0.000001	IZ	0.000001");
            //list.Add("_STRINGER	PRI	AX	0.01048	IX	0.00001	IY	0.00001	IZ	0.000362");
            //list.Add("_XGIRDER	PRI	AX	0.02162	IX	0.00001	IY	0.000072	IZ	0.001335");
            //list.Add("MEMBER	TRUSS	");
            //list.Add("_L0L1");
            //list.Add("MEMBER	TRUSS	");
            //list.Add("_L1L2");
            //list.Add("MEMBER	TRUSS	");
            //list.Add("_L2L3	");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_L3L4	");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_L4L5	");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_U1U2	");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_U2U3	");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_U3U4	");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_U4U5	");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_L1U1");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_L2U2	");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_L3U3	");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_L4U4	");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_L5U5	");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_ER	");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_L2U1	");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_L3U2	");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_L4U3	");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_L5U4");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_TCS_ST");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_TCS_DIA");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_BCB1");
            //list.Add("MEMBER	TRUSS");
            //list.Add("_BCB2");
            //list.Add("CONSTANT");
            //list.Add("E	2.11E+08	ALL	");
            //list.Add("DENSITY	STEEL	ALL	");
            //list.Add("POISSON	STEEL	ALL	");
            //list.Add("SUPPORT");
            //list.Add("1	12	FIXED	BUT	MZ");
            //list.Add("11	22	FIXED	BUT	FX	MZ");
            ////list.Add("1	12	PINNED");
            ////list.Add("11	22	FIXED");
            //list.Add("LOAD	1	DL+SIDL	");
            //list.Add("JOINT	LOAD");
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
            list.Add("*STRINGER BEAMS AT Z = " + (Breadth-_stngr_spc));
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
            for (i = 0; i <= Math.Abs(1 - 11); i++)
            {
                counter++;
                x = i * PanelLength;
                y = 0;
                z = Breadth;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            //Joints    12  -   22
            for (i = 0; i <= Math.Abs(12 - 22); i++)
            {
                counter++;
                x = i * PanelLength;
                y = 0;
                z = 0;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            //Joints    23  -   31
            for (i = 1; i <= Math.Abs(23 - 31)+1; i++)
            {
                counter++;
                x = i * PanelLength;
                y = Height;
                z = Breadth;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            //Joints    32  -   40
            for (i = 1; i <= Math.Abs(32 - 40)+1; i++)
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
        
       
    }
    public class CreateSteel_Warren_2_TrussData
    {
        public CreateSteel_Warren_2_TrussData()
        {
            Length = 50.0;
            Breadth = 9.1;
            Height = 6.5;
        }
        public double Length { get; set; }
        public int NoOfPanel
        {
            get
            {
                return (8);
            }
        }
        public double PanelLength
        {
            get
            {
                return (Length / (double)NoOfPanel);
            }
        }
        public double Breadth { get; set; }
        public int NoOfStringerBeam
        {
            get { return 2; }
        }
        public double StringerSpacing
        {
            get
            {
                return (Breadth / (NoOfStringerBeam + 1));
            }
        }
        public double Height { get; set; }

        public bool CreateData(string fileName)
        {
            try
            {
                List<string> list = new List<string>();
                list.Add("ASTRA SPACE FRAME WITH TRUSS DEAD LOAD+SIDL+MOVING LOAD IRC 24R TRACK");
                list.Add("UNIT KN MET");
                list.AddRange(GetJointCoordinates());
                list.AddRange(GetMemberConnectivityAndOthers());
                File.WriteAllLines(fileName, list.ToArray());

                fileName = Path.GetDirectoryName(fileName);
                fileName = Path.Combine(fileName, "LL.txt");
                list.Clear();
                list.Add("FILE LL.TXT");
                list.Add("");
                list.Add("TYPE1 IRCCLASSA");
                list.Add("68 68 68 68 114 114 114 27");
                list.Add("1.10 3.20 1.20 4.30 3.00 3.00 3.00");
                list.Add("1.80");
                list.Add("");
                list.Add("TYPE 2 IRCCLASSB");
                list.Add("41 41 41 41 68 68 16 16");
                list.Add("1.10 3.20 1.20 4.30 3.00 3.00 3.00");
                list.Add("1.80");
                list.Add("");
                list.Add("TYPE 3 IRC70RTRACK");
                list.Add("70 70 70 70 70 70 70 70 70 70");
                list.Add("0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457");
                list.Add("0.84");
                list.Add("");
                list.Add("TYPE 4 IRC70RWHEEL");
                list.Add("170 170 170 170 120 120 80");
                list.Add("1.37 3.05 1.37 2.13 1.52 3.96");
                list.Add("0.450 1.480 0.450");
                list.Add("");
                list.Add("TYPE 5 IRCCLASSAATRACK");
                list.Add("70 70 70 70 70 70 70 70 70 70");
                list.Add("0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360");
                list.Add("0.85");
                list.Add("");
                list.Add("TYPE 6 IRC24RTRACK");
                list.Add("25 25 25 25 25 25 25 25 25 25");
                list.Add("0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366");
                list.Add("0.36");
                list.Add("");
                //list.Add("TYPE 7  BROADGAUGERAIL");
                //list.Add("44 66 66 93 93 93 93");
                //list.Add("3.96 1.52 2.13 1.37 3.05 1.37");
                //list.Add("1.93");
                File.WriteAllLines(fileName, list.ToArray());

                return true;
            }
            catch (Exception ex) { }
            return false;
        }
        public List<string> GetMemberConnectivityAndOthers()
        {
            List<string> list = new List<string>();
            string kStr = "";

            string format = "{0,-10} {1,10} {2,10} {3,10}";
            int i = 0;
            double x, y, z;

            x = 0.0;
            y = 0.0;
            z = 0.0;
            int counter = 0;
            list.Add("MEMBER CONNECTIVITY");
            list.Add("*BOTTOM CHORD MEMBERS");
            list.Add("1 1 33");
            list.Add("2 33 2");
            list.Add("3 2 35");
            list.Add("4 35 3");
            list.Add("5 3 37");
            list.Add("6 37 4");
            list.Add("7 4 39");
            list.Add("8 39 5");
            list.Add("9 5 41");
            list.Add("10 41 6");
            list.Add("11 6 43");
            list.Add("12 43 7");
            list.Add("13 7 45");
            list.Add("14 45 8");
            list.Add("15 8 47");
            list.Add("16 47 9");
            list.Add("17 10 49");
            list.Add("18 49 11");
            list.Add("19 11 51");
            list.Add("20 51 12");
            list.Add("21 12 53");
            list.Add("22 53 13");
            list.Add("23 13 55");
            list.Add("24 55 14");
            list.Add("25 14 57");
            list.Add("26 57 15");
            list.Add("27 15 59");
            list.Add("28 59 16");
            list.Add("29 16 61");
            list.Add("30 61 17");
            list.Add("31 17 63");
            list.Add("32 63 18");
            list.Add("*TOP CHORD MEMBERS");
            list.Add("33 19 20");
            list.Add("34 20 21");
            list.Add("35 21 22");
            list.Add("36 22 23");
            list.Add("37 23 24");
            list.Add("38 24 25");
            list.Add("39 26 27");
            list.Add("40 27 28");
            list.Add("41 28 29");
            list.Add("42 29 30");
            list.Add("43 30 31");
            list.Add("44 31 32");
            list.Add("*END RACKER & DIAGONAL MEMBERS");
            list.Add("45 19 34");
            list.Add("46 34 1");
            list.Add("47 19 36");
            list.Add("48 36 3");
            list.Add("49 20 38");
            list.Add("50 38 4");
            list.Add("51 21 40");
            list.Add("52 40 5");
            list.Add("53 23 42");
            list.Add("54 42 5");
            list.Add("55 24 44");
            list.Add("56 44 6");
            list.Add("57 25 46");
            list.Add("58 46 7");
            list.Add("59 25 48");
            list.Add("60 48 9");
            list.Add("61 26 50");
            list.Add("62 50 10");
            list.Add("63 26 52");
            list.Add("64 52 12");
            list.Add("65 27 54");
            list.Add("66 54 13");
            list.Add("67 28 56");
            list.Add("68 56 14");
            list.Add("69 30 58");
            list.Add("70 58 14");
            list.Add("71 31 60");
            list.Add("72 60 15");
            list.Add("73 32 62");
            list.Add("74 62 16");
            list.Add("75 32 64");
            list.Add("76 64 18");
            list.Add("*VERTICAL MEMBERS");
            list.Add("77 2 19");
            list.Add("78 3 20");
            list.Add("79 4 21");
            list.Add("80 5 22");
            list.Add("81 6 23");
            list.Add("82 7 24");
            list.Add("83 8 25");
            list.Add("84 11 26");
            list.Add("85 12 27");
            list.Add("86 13 28");
            list.Add("87 14 29");
            list.Add("88 15 30");
            list.Add("89 16 31");
            list.Add("90 17 32");
            list.Add("*DIAGONAL MEMBERS");
            list.Add("91 33 34");
            list.Add("92 2 34");
            list.Add("93 2 36");
            list.Add("94 35 36");
            list.Add("95 3 38");
            list.Add("96 37 38");
            list.Add("97 4 40");
            list.Add("98 39 40");
            list.Add("99 41 42");
            list.Add("100 6 42");
            list.Add("101 43 44");
            list.Add("102 7 44");
            list.Add("103 45 46");
            list.Add("104 8 46");
            list.Add("105 8 48");
            list.Add("106 47 48");
            list.Add("107 49 50");
            list.Add("108 11 50");
            list.Add("109 11 52");
            list.Add("110 51 52");
            list.Add("111 12 54");
            list.Add("112 53 54");
            list.Add("113 13 56");
            list.Add("114 55 56");
            list.Add("115 57 58");
            list.Add("116 15 58");
            list.Add("117 59 60");
            list.Add("118 16 60");
            list.Add("119 61 62");
            list.Add("120 17 62");
            list.Add("121 17 64");
            list.Add("122 63 64");
            list.Add("*XGIRDER BEAM MEMBERS");
            list.Add("123 1 65");
            list.Add("124 65 82");
            list.Add("125 82 10");
            list.Add("126 33 66");
            list.Add("127 66 83");
            list.Add("128 83 49");
            list.Add("129 2 67");
            list.Add("130 67 84");
            list.Add("131 84 11");
            list.Add("132 35 68");
            list.Add("133 68 85");
            list.Add("134 85 51");
            list.Add("135 3 69");
            list.Add("136 69 86");
            list.Add("137 86 12");
            list.Add("138 37 70");
            list.Add("139 70 87");
            list.Add("140 87 53");
            list.Add("141 4 71");
            list.Add("142 71 88");
            list.Add("143 88 13");
            list.Add("144 39 72");
            list.Add("145 72 89");
            list.Add("146 89 55");
            list.Add("147 5 73");
            list.Add("148 73 90");
            list.Add("149 90 14");
            list.Add("150 41 74");
            list.Add("151 74 91");
            list.Add("152 91 57");
            list.Add("153 6 75");
            list.Add("154 75 92");
            list.Add("155 92 15");
            list.Add("156 43 76");
            list.Add("157 76 93");
            list.Add("158 93 59");
            list.Add("159 7 77");
            list.Add("160 77 94");
            list.Add("161 94 16");
            list.Add("162 45 78");
            list.Add("163 78 95");
            list.Add("164 95 61");
            list.Add("165 8 79");
            list.Add("166 79 96");
            list.Add("167 96 17");
            list.Add("168 47 80");
            list.Add("169 80 97");
            list.Add("170 97 63");
            list.Add("171 9 81");
            list.Add("172 81 98");
            list.Add("173 98 18");
            list.Add("*STRINGER BEAM MEMBERS");
            list.Add("174 65 66");
            list.Add("175 66 67");
            list.Add("176 67 68");
            list.Add("177 68 69");
            list.Add("178 69 70");
            list.Add("179 70 71");
            list.Add("180 71 72");
            list.Add("181 72 73");
            list.Add("182 73 74");
            list.Add("183 74 75");
            list.Add("184 75 76");
            list.Add("185 76 77");
            list.Add("186 77 78");
            list.Add("187 78 79");
            list.Add("188 79 80");
            list.Add("189 80 81");
            list.Add("190 82 83");
            list.Add("191 83 84");
            list.Add("192 84 85");
            list.Add("193 85 86");
            list.Add("194 86 87");
            list.Add("195 87 88");
            list.Add("196 88 89");
            list.Add("197 89 90");
            list.Add("198 90 91");
            list.Add("199 91 92");
            list.Add("200 92 93");
            list.Add("201 93 94");
            list.Add("202 94 95");
            list.Add("203 95 96");
            list.Add("204 96 97");
            list.Add("205 97 98");
            list.Add("*BOTTOM BRACING MEMBERS");
            list.Add("206 1 11");
            list.Add("207 2 10");
            list.Add("208 2 12");
            list.Add("209 3 11");
            list.Add("210 3 13");
            list.Add("211 4 12");
            list.Add("212 4 14");
            list.Add("213 5 13");
            list.Add("214 5 15");
            list.Add("215 6 14");
            list.Add("216 6 16");
            list.Add("217 7 15");
            list.Add("218 7 17");
            list.Add("219 8 16");
            list.Add("220 8 18");
            list.Add("221 9 17");
            list.Add("*TOP BRACING MEMBERS");
            list.Add("222 19 26");
            list.Add("223 20 27");
            list.Add("224 21 28");
            list.Add("225 22 29");
            list.Add("226 23 30");
            list.Add("227 24 31");
            list.Add("228 25 32");
            list.Add("229 19 27");
            list.Add("230 20 26");
            list.Add("231 20 28");
            list.Add("232 21 27");
            list.Add("233 21 29");
            list.Add("234 22 28");
            list.Add("235 22 30");
            list.Add("236 23 29");
            list.Add("237 23 31");
            list.Add("238 24 30");
            list.Add("239 24 32");
            list.Add("240 25 31");
            list.Add("START GROUP DEFINITION");
            list.Add("_L0L1 1 2 15 16 17 18 31 32");
            list.Add("_L1L2 3 4 13 14 19 20 29 30");
            list.Add("_L2L3 5 6 11 12 21 22 27 28");
            list.Add("_L3L4 7 8 9 10 23 24 25 26");
            list.Add("_U1U2 33 38 39 44");
            list.Add("_U2U3 34 37 40 43");
            list.Add("_U3U4 35 36 41 42");
            list.Add("_L1U1 77 83 84 90");
            list.Add("_L2U2 78 82 85 89");
            list.Add("_L3U3 79 81 86 88");
            list.Add("_L4U4 80 87");
            list.Add("_L2U1 47 48 57 58 63 64 73 74");
            list.Add("_L3U2 49 50 55 56 65 66 71 72");
            list.Add("_L4U3 51 52 53 54 67 68 69 70");
            list.Add("_ER 45 46 59 60 61 62 75 76");
            list.Add("_V1V2 91 106 107 122");
            list.Add("_V3V4 92 105 108 121");
            list.Add("_V5V6 93 104 109 120");
            list.Add("_V7V8 94 103 110 119");
            list.Add("_V9V10 95 102 111 118");
            list.Add("_V11V12 96 101 112 117");
            list.Add("_V13V14 97 100 113 116");
            list.Add("_V15V16 98 99 114 115");
            list.Add("_TCS_ST 222 TO 228");
            list.Add("_TCS_DIA 229 TO 240");
            list.Add("_BCB 206 TO 221");
            list.Add("_XGIRDER 123 TO 173");
            list.Add("_STRINGER 174 TO 205");
            list.Add("END");
            list.Add("MEMBER	PROPERTY	INDIAN							");
            list.Add("_L0L1	PRI	AX	0.024	IX	0.00001	IY	0.000741	IZ	0.001");
            list.Add("_L1L2	PRI	AX	0.03	IX	0.00001	IY	0.000741	IZ	0.001");
            list.Add("_L2L3	PRI	AX	0.03	IX	0.00001	IY	0.000946	IZ	0.001");
            list.Add("_L3L4	PRI	AX	0.039	IX	0.00001	IY	0.001		IZ	0.002");
            list.Add("_U1U2	PRI	AX	0.027	IX	0.00001	IY	0.000807	IZ	0.000693");
            list.Add("_U2U3	PRI	AX	0.033	IX	0.00001	IY	0.000864	IZ	0.000922");
            list.Add("_U3U4	PRI	AX	0.036	IX	0.00001	IY	0.000896	IZ	0.001");
            list.Add("_L1U1	PRI	AX	0.006	IX	0.00001	IY	0.000182	IZ	0.000036");
            list.Add("_L2U2	PRI	AX	0.013	IX	0.00001	IY	0.000399	IZ	0.000302");
            list.Add("_L3U3	PRI	AX	0.009	IX	0.00001	IY	0.00029		IZ	0.000127");
            list.Add("_L4U4	PRI	AX	0.006	IX	0.00001	IY	0.000182	IZ	0.000036");
            list.Add("_L2U1	PRI	AX	0.019	IX	0.00001	IY	0.000621	IZ	0.0000356");
            list.Add("_L3U2	PRI	AX	0.014	IX	0.00001	IY	0.000474	IZ	0.000149");
            list.Add("_L4U3	PRI	AX	0.009	IX	0.00001	IY	0.00029		IZ	0.000127");
            list.Add("_ER	PRI	AX	0.022	IX	0.00001	IY	0.000666	IZ	0.000579");
            list.Add("_V1V2 	PRI	AX	0.019	IX	0.00001	IY	0.000621	IZ	0.0000356");
            list.Add("_V3V4 	PRI	AX	0.019	IX	0.00001	IY	0.000621	IZ	0.0000356");
            list.Add("_V5V6 	PRI	AX	0.019	IX	0.00001	IY	0.000621	IZ	0.0000356");
            list.Add("_V7V8 	PRI	AX	0.019	IX	0.00001	IY	0.000621	IZ	0.0000356");
            list.Add("_V9V10 	PRI	AX	0.019	IX	0.00001	IY	0.000621	IZ	0.0000356");
            list.Add("_V11V12 PRI	AX	0.019	IX	0.00001	IY	0.000621	IZ	0.0000356");
            list.Add("_V13V14 PRI	AX	0.019	IX	0.00001	IY	0.000621	IZ	0.0000356");
            list.Add("_V15V16 PRI	AX	0.019	IX	0.00001	IY	0.000621	IZ	0.0000356");
            list.Add("_TCS_ST	PRI	AX	0.006	IX	0.00001	IY	0.000027	IZ	0.000225");
            list.Add("_TCS_DIA  PRI	AX	0.006	IX	0.00001	IY	0.000027	IZ	0.000225");
            list.Add("_BCB	  PRI	AX	0.002	IX	0.00001	IY	0.000001	IZ	0.000001");
            list.Add("_XGIRDER  PRI	AX	0.02162	IX	0.00001	IY	0.000072	IZ	0.001335");
            list.Add("_STRINGER PRI	AX	0.01048	IX	0.00001	IY	0.00001		IZ	0.000362");
            list.Add("MEMBER	TRUSS								");
            list.Add("_L0L1									");
            list.Add("MEMBER	TRUSS								");
            list.Add("_L1L2									");
            list.Add("MEMBER	TRUSS								");
            list.Add("_L2L3	");
            list.Add("MEMBER	TRUSS");
            list.Add("_L3L4	");
            list.Add("MEMBER	TRUSS");
            list.Add("_U1U2	");
            list.Add("MEMBER	TRUSS");
            list.Add("_U2U3	");
            list.Add("MEMBER	TRUSS");
            list.Add("_U3U4	");
            list.Add("MEMBER	TRUSS");
            list.Add("_L1U1");
            list.Add("MEMBER	TRUSS");
            list.Add("_L2U2	");
            list.Add("MEMBER	TRUSS");
            list.Add("_L3U3	");
            list.Add("MEMBER	TRUSS");
            list.Add("_L4U4	");
            list.Add("MEMBER	TRUSS");
            list.Add("_L2U1	");
            list.Add("MEMBER	TRUSS");
            list.Add("_L3U2	");
            list.Add("MEMBER	TRUSS");
            list.Add("_L4U3	");
            list.Add("MEMBER	TRUSS");
            list.Add("_ER	");
            list.Add("MEMBER	TRUSS");
            list.Add("_V1V2	");
            list.Add("MEMBER	TRUSS");
            list.Add("_V3V4");
            list.Add("MEMBER	TRUSS");
            list.Add("_V5V6	");
            list.Add("MEMBER	TRUSS");
            list.Add("_V7V8	");
            list.Add("MEMBER	TRUSS");
            list.Add("_V9V10	");
            list.Add("MEMBER	TRUSS");
            list.Add("_V11V12	");
            list.Add("MEMBER	TRUSS");
            list.Add("_V13V14");
            list.Add("MEMBER	TRUSS");
            list.Add("_V15V16	");
            list.Add("MEMBER	TRUSS				");
            list.Add("_TCS_ST					");
            list.Add("MEMBER	TRUSS				");
            list.Add("_TCS_DIA					");
            list.Add("MEMBER	TRUSS				");
            list.Add("_BCB				");
            list.Add("CONSTANT					");
            list.Add("E	2.11E+08	ALL			");
            list.Add("DENSITY	STEEL	ALL			");
            list.Add("POISSON	STEEL	ALL			");
            list.Add("SUPPORT					");
            list.Add("1	10	FIXED	BUT	MZ	");
            list.Add("9	18	FIXED	BUT	FX	MZ");
            list.Add("LOAD	1	DL+SIDL	");
            list.Add("JOINT	LOAD");
            list.Add("2  TO 18     FY  -157.700");
            list.Add("65 TO 82     FY  -78.850");
            list.Add("ANALYSIS		");
            list.Add("FINISH");
            list.Add("");
            list.Add("");
            list.Add("");
            //list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            //list.Add("TYPE 1 CLA 1.179");
            //list.Add("TYPE 2 CLB 1.188");
            //list.Add("TYPE 3 A70RT 1.10");
            //list.Add("TYPE 4 CLAR 1.179");
            //list.Add("TYPE 5 A70RR 1.188");
            //list.Add("TYPE 6 IRC24RTRACK 1.188");
            //list.Add("TYPE 7 RAILBG 1.25");
            //list.Add("LOAD GENERATION 100");
            //list.Add("TYPE 6  -69.500 0 1.000 XINC 0.5");
            //list.Add("PRINT SUPPORT REACTIONS");
            //list.Add("PRINT MAX FORCE ENVELOPE LIST 174 TO 193");

            return list;
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
                z = 0;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            for (i = 0; i <= NoOfPanel; i++)
            {
                counter++;
                x = i * PanelLength;
                y = 0;
                z = Breadth;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            for (i = 1; i < NoOfPanel; i++)
            {
                counter++;
                x = i * PanelLength;
                y = Height;
                z = 0;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }

            for (i = 1; i < NoOfPanel; i++)
            {
                counter++;
                x = i * PanelLength;
                y = Height;
                z = Breadth;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }

            for (i = 0; i < NoOfPanel; i++)
            {
                counter++;
                x = i * PanelLength + (PanelLength / 2.0);
                y = 0;
                z = 0;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);

                counter++;
                //x = i * (PanelLength / 2.0) + PanelLength;
                y = Height / 2.0;
                z = 0;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            for (i = 0; i < NoOfPanel; i++)
            {
                counter++;
                x = i * PanelLength + (PanelLength / 2.0);
                //x = i * (PanelLength / 2.0);
                y = 0;
                z = Breadth;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);

                counter++;
                //x = i * (PanelLength / 2.0) + PanelLength;
                y = Height / 2.0;
                z = Breadth;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            for (i = 0; i <= (NoOfPanel * 2); i++)
            {
                counter++;
                x = i * (PanelLength / 2.0);
                y = 0;
                z = StringerSpacing;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            for (i = 0; i <= (NoOfPanel * 2); i++)
            {
                counter++;
                x = i * (PanelLength / 2.0);
                y = 0;
                z = StringerSpacing * 2.0;
                kStr = string.Format(format, counter, x, y, z);
                list.Add(kStr);
            }
            return list;
        }
    }

}
