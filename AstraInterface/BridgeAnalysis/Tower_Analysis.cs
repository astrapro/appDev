using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AstraInterface.DataStructure;

namespace AstraInterface.BridgeAnalysis
{
    class Tower_Analysis
    {
        

        //Height of Tower = 30.0 m
        //Each Bracing Panel height = 2.0m
        //Width of Each Tower at Base = 3.0m
        //Width of each Tower at Top = 1.5m
        //Lower Tower Connector at height = 10.0m (check)
        //Upper Tower Connector at height = 20.0m (check)
        #region Properties
        public double Tower_Height { get; set; }
        public double Bracing_Panel_Height { get; set; }
        public double Tower_Base_Width { get; set; }
        public double Tower_Top_Width { get; set; }
        public double Tower_Lower_Connector_Width { get; set; }
        public double Tower_Upper_Connector_Width { get; set; }
        public double Tower_Clear_Distance { get; set; }

        public double Tower_Dead_Load { get; set; }
        public double Tower_Live_Load { get; set; }
        public double Tower_Seismic_Coefficient { get; set; }

        public double Tower_SEC_VS_AX { get; set; }
        public double Tower_SEC_VS_IX { get; set; }
        public double Tower_SEC_VS_IZ { get; set; }

        public double Tower_SEC_BS_AX { get; set; }
        public double Tower_SEC_BS_IX { get; set; }
        public double Tower_SEC_BS_IZ { get; set; }


        #endregion Properties

        public Tower_Analysis()
        {
            Tower_Height = 30.0;
            Bracing_Panel_Height = 2.0;
            Tower_Base_Width = 3.0;
            Tower_Top_Width = 1.5;
            Tower_Lower_Connector_Width = 10.0;
            Tower_Upper_Connector_Width = 20.0;
            Tower_Clear_Distance = 4.5;




            Tower_Dead_Load = 55.0;
            Tower_Live_Load = 55.0;
            Tower_Seismic_Coefficient = 55.0;

            Tower_SEC_VS_AX = 68.81;
            Tower_SEC_VS_IX = 1046.5;
            Tower_SEC_VS_IZ = 1046.5;

            Tower_SEC_BS_AX = 7.44;
            Tower_SEC_BS_IX = 11.7;
            Tower_SEC_BS_IZ = 11.7;

        }

        public bool Create_Data(string file_name)
        {
            List<string> list = new List<string>();


            //con
            int Upper_Connector_Index = -1;
            int Lower_Connector_Index = -1;


            int indx = 12;



            int pnl_nos = (int)(Tower_Height / Bracing_Panel_Height);
            #region X Points


            double x_ps = (Tower_Base_Width - Tower_Top_Width) / 2;
            double X_Incr = (x_ps) / (pnl_nos);



            List<double> x_side_1 = new List<double>();
            List<double> x_side_2 = new List<double>();

            List<double> y_side = new List<double>();
            List<double> z_side_1 = new List<double>();
            List<double> z_side_2 = new List<double>();


            double x = 0, y = 0, z = 0;


            int i = 0;


            for (i = 0; i <= pnl_nos; i++)
            {
                x = i * X_Incr;
                y = i * Bracing_Panel_Height;

                x_side_1.Add(x);
                z_side_1.Add(x);

                x_side_2.Add(Tower_Base_Width - x);
                z_side_2.Add(Tower_Base_Width - x);


                y_side.Add(y);

            }


            i = 0;

            #endregion X Points


            //     Tower_Lower_Connector_Width = 10.0;
            //Tower_Upper_Connector_Width = 20.0;

            if (Tower_Upper_Connector_Width != 0.0)
            {
                for (i = 0; i < y_side.Count; i++)
                {
                    if (Tower_Upper_Connector_Width.ToString("f3") == y_side[i].ToString("f3"))
                    {
                        Upper_Connector_Index = i; break;
                    }
                }
            }

            if (Tower_Lower_Connector_Width != 0.0)
            {
                for (i = 0; i < y_side.Count; i++)
                {
                    if (Tower_Lower_Connector_Width.ToString("f3") == y_side[i].ToString("f3"))
                    {
                        Lower_Connector_Index = i; break;
                    }
                }
            }

            JointNodeCollection jnc_1, jnc_2, jnc_3, jnc_4; // First Tower


            JointNodeCollection jnc_5, jnc_6, jnc_7, jnc_8;// Second Tower

            jnc_1 = new JointNodeCollection();
            jnc_2 = new JointNodeCollection();
            jnc_3 = new JointNodeCollection();
            jnc_4 = new JointNodeCollection();

            jnc_5 = new JointNodeCollection();
            jnc_6 = new JointNodeCollection();
            jnc_7 = new JointNodeCollection();
            jnc_8 = new JointNodeCollection();


            double x_dist = Tower_Base_Width + Tower_Clear_Distance;



            JointNode jn = null;

            #region Side 1
            for (i = 0; i < y_side.Count; i++)
            {
                #region First Tower
                jn = new JointNode();

                jn.X = x_side_1[i];
                jn.Y = y_side[i];
                jn.Z = z_side_1[i];

                jnc_1.Add(jn);
                #endregion First Tower


                #region Second Tower
                jn = new JointNode();

                jn.X = x_dist + x_side_1[i];
                jn.Y = y_side[i];
                jn.Z = z_side_1[i];

                jnc_6.Add(jn);
                #endregion Second Tower

            }

            #endregion Side 1


            #region Side 2
            for (i = 0; i < y_side.Count; i++)
            {
                jn = new JointNode();

                jn.X = x_side_2[i];
                jn.Y = y_side[i];
                jn.Z = z_side_1[i];

                jnc_2.Add(jn);


                jn = new JointNode();

                jn.X = x_dist + x_side_2[i];
                jn.Y = y_side[i];
                jn.Z = z_side_1[i];

                jnc_5.Add(jn);
            }

            #endregion Side 2


            #region Side 3
            for (i = 0; i < y_side.Count; i++)
            {
                jn = new JointNode();

                jn.X = x_side_1[i];
                jn.Y = y_side[i];
                jn.Z = z_side_2[i];

                jnc_3.Add(jn);


                jn = new JointNode();

                jn.X = x_dist + x_side_1[i];
                jn.Y = y_side[i];
                jn.Z = z_side_2[i];

                jnc_8.Add(jn);
            }

            #endregion Side 3


            #region Side 4
            for (i = 0; i < y_side.Count; i++)
            {
                jn = new JointNode();

                jn.X = x_side_2[i];
                jn.Y = y_side[i];
                jn.Z = z_side_2[i];

                jnc_4.Add(jn);


                jn = new JointNode();

                jn.X = x_dist + x_side_2[i];
                jn.Y = y_side[i];
                jn.Z = z_side_2[i];

                jnc_7.Add(jn);
            }

            #endregion Side 4


            JointNodeCollection con_jnt_1 = new JointNodeCollection();


            //indx = 12;

            indx = 12;



            double mid_x = (x_dist + Tower_Base_Width) / 2.0;

            if (Upper_Connector_Index != -1)
            {
                indx = Upper_Connector_Index;

                #region  Connector 1

                #region Connector Joint 1

                jn = new JointNode();

                jn.X = mid_x;
                jn.Y = y_side[indx];
                jn.Z = z_side_2[indx];

                con_jnt_1.Add(jn);



                #endregion Connetor Joint 1

                #region Connetor Joint 2

                jn = new JointNode();

                jn.X = mid_x;
                jn.Y = y_side[indx];
                jn.Z = z_side_1[indx];

                con_jnt_1.Add(jn);



                #endregion Connetor Joint 2


                indx++;
                #region Connetor Joint 3

                jn = new JointNode();

                jn.X = mid_x;
                jn.Y = y_side[indx];
                jn.Z = z_side_1[indx];

                con_jnt_1.Add(jn);



                #endregion Connetor Joint 3

                #region Connetor Joint 4

                jn = new JointNode();

                jn.X = mid_x;
                jn.Y = y_side[indx];
                jn.Z = z_side_2[indx];

                con_jnt_1.Add(jn);



                #endregion Connetor Joint 4

                #endregion  Connector 1
            }
            indx = 5;

            JointNodeCollection con_jnt_2 = new JointNodeCollection();

            if (Lower_Connector_Index != -1)
            {
                indx = Lower_Connector_Index;

                #region  Connector 2

                #region Connector Joint 1

                jn = new JointNode();

                jn.X = mid_x;
                jn.Y = y_side[indx];
                jn.Z = z_side_2[indx];

                con_jnt_2.Add(jn);



                #endregion Connetor Joint 1

                #region Connetor Joint 2

                jn = new JointNode();

                jn.X = mid_x;
                jn.Y = y_side[indx];
                jn.Z = z_side_1[indx];

                con_jnt_2.Add(jn);



                #endregion Connetor Joint 2


                indx++;
                #region Connetor Joint 3

                jn = new JointNode();

                jn.X = mid_x;
                jn.Y = y_side[indx];
                jn.Z = z_side_1[indx];

                con_jnt_2.Add(jn);



                #endregion Connetor Joint 3

                #region Connetor Joint 4

                jn = new JointNode();

                jn.X = mid_x;
                jn.Y = y_side[indx];
                jn.Z = z_side_2[indx];

                con_jnt_2.Add(jn);



                #endregion Connetor Joint 4

                #endregion  Connector 2
            }
            list.Add(string.Format(""));
            list.Add(string.Format("ASTRA SPACE TOWER"));
            list.Add(string.Format("UNIT MTON METRES"));
            list.Add(string.Format("JOINT COORDINATES"));
            int jnt_no = 1;

            #region Joint Coordinates
            foreach (var item in jnc_1)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }

            foreach (var item in jnc_2)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }
            foreach (var item in jnc_3)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }
            foreach (var item in jnc_4)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }
            foreach (var item in jnc_5)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }

            foreach (var item in jnc_6)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }

            foreach (var item in jnc_7)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }

            foreach (var item in jnc_8)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }


            foreach (var item in con_jnt_1)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }

            foreach (var item in con_jnt_2)
            {
                item.NodeNo = jnt_no++;
                list.Add(item.ToString());
            }

            #endregion Joint Coordinates



            int mem_no = 1;


            Member mbr = new Member();

            MemberCollection m_side_1 = new MemberCollection();



            List<int> sec_1 = new List<int>();
            List<int> sec_2 = new List<int>();

            #region Tower 1
            #region Member Side 1 [1-15]
            for (i = 1; i < jnc_1.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_1[i - 1];
                mbr.EndNode = jnc_1[i];

                m_side_1.Add(mbr);


                sec_1.Add(mem_no++);
            }

            #endregion Member Side 1

            #region Member Side 2 [16-30]

            for (i = 1; i < jnc_2.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_2[i - 1];
                mbr.EndNode = jnc_2[i];

                m_side_1.Add(mbr);
                sec_1.Add(mem_no++);

            }

            #endregion Member Side 2

            #region Member Side 3 [31-45]


            for (i = 1; i < jnc_3.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_3[i - 1];
                mbr.EndNode = jnc_3[i];

                m_side_1.Add(mbr);

                sec_1.Add(mem_no++);
            }

            #endregion Member Side 3

            #region Member Side 4 [46-60]


            for (i = 1; i < jnc_4.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_4[i - 1];
                mbr.EndNode = jnc_4[i];

                m_side_1.Add(mbr);
                sec_1.Add(mem_no++);

            }

            #endregion Member Side 4

            #region Member 61-76


            for (i = 0; i < jnc_1.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_1[i];
                mbr.EndNode = jnc_2[i];

                m_side_1.Add(mbr);

                sec_2.Add(mem_no++);

            }

            #endregion Member 61-76

            #region Member 77-92


            for (i = 0; i < jnc_3.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_3[i];
                mbr.EndNode = jnc_4[i];

                m_side_1.Add(mbr);
                sec_2.Add(mem_no++);
            }

            #endregion Member 77-92

            #region Member 93-108


            for (i = 0; i < jnc_3.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_1[i];
                mbr.EndNode = jnc_3[i];

                m_side_1.Add(mbr);
                sec_2.Add(mem_no++);
            }

            #endregion Member 77-92

            #region Member 109-124


            for (i = 0; i < jnc_2.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_2[i];
                mbr.EndNode = jnc_4[i];

                m_side_1.Add(mbr);
                sec_2.Add(mem_no++);
            }

            #endregion Member 109-124

            #region Member 125-139


            for (i = 1; i < jnc_1.Count; i += 2)
            {
                try
                {

                    mbr = new Member();
                    mbr.StartNode = jnc_3[i - 1];
                    mbr.EndNode = jnc_1[i];

                    m_side_1.Add(mbr);

                    sec_2.Add(mem_no++);


                    mbr = new Member();
                    mbr.StartNode = jnc_1[i];
                    mbr.EndNode = jnc_3[i + 1];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);

                }
                catch (Exception ex) { }
            }

            #endregion Member 109-124

            #region Member 140-154


            for (i = 1; i < jnc_2.Count; i += 2)
            {
                try
                {

                    mbr = new Member();
                    mbr.StartNode = jnc_2[i - 1];
                    mbr.EndNode = jnc_4[i];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);


                    mbr = new Member();
                    mbr.StartNode = jnc_4[i];
                    mbr.EndNode = jnc_2[i + 1];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);


                }
                catch (Exception exx) { }
            }

            #endregion Member 140-154

            #region Member 155-169


            for (i = 1; i < jnc_3.Count; i += 2)
            {

                try
                {

                    mbr = new Member();
                    mbr.StartNode = jnc_4[i - 1];
                    mbr.EndNode = jnc_3[i];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);



                    mbr = new Member();
                    mbr.StartNode = jnc_3[i];
                    mbr.EndNode = jnc_4[i + 1];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);


                }
                catch (Exception exx) { }
            }

            #endregion Member 155-169

            #region Member 170-184

            for (i = 1; i < jnc_1.Count; i += 2)
            {

                try
                {

                    mbr = new Member();
                    mbr.StartNode = jnc_1[i - 1];
                    mbr.EndNode = jnc_2[i];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);


                    mbr = new Member();
                    mbr.StartNode = jnc_2[i];
                    mbr.EndNode = jnc_1[i + 1];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);

                }
                catch (Exception exx) { }
            }

            #endregion Member 155-169
            #endregion Tower 1



            #region Tower 2

            #region Member Side 1 [185-199]

            JointNodeCollection jnc = jnc_5;
            for (i = 1; i < jnc.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc[i - 1];
                mbr.EndNode = jnc[i];

                m_side_1.Add(mbr);
                sec_1.Add(mem_no++);

            }

            #endregion Member Side 1

            #region Member Side 2 [200-214]

            jnc = jnc_6;
            for (i = 1; i < jnc.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc[i - 1];
                mbr.EndNode = jnc[i];

                m_side_1.Add(mbr);
                sec_1.Add(mem_no++);

            }

            #endregion Member Side 2

            #region Member Side 3 [215-229]


            jnc = jnc_7;
            for (i = 1; i < jnc.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc[i - 1];
                mbr.EndNode = jnc[i];

                m_side_1.Add(mbr);
                sec_1.Add(mem_no++);

            }

            #endregion Member Side 3

            #region Member Side 4 [230-244]

            jnc = jnc_8;
            for (i = 1; i < jnc.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc[i - 1];
                mbr.EndNode = jnc[i];

                m_side_1.Add(mbr);
                sec_1.Add(mem_no++);

            }

            #endregion Member Side 4

            #region Member 245-260


            for (i = 0; i < jnc_5.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_5[i];
                mbr.EndNode = jnc_6[i];

                m_side_1.Add(mbr);
                sec_2.Add(mem_no++);
            }

            #endregion Member 61-76

            #region Member 261-276


            for (i = 0; i < jnc_7.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_7[i];
                mbr.EndNode = jnc_8[i];

                m_side_1.Add(mbr);
                sec_2.Add(mem_no++);
            }

            #endregion Member 77-92

            #region Member 277-292


            for (i = 0; i < jnc_7.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_5[i];
                mbr.EndNode = jnc_7[i];

                m_side_1.Add(mbr);
                sec_2.Add(mem_no++);
            }

            #endregion Member 77-92

            #region Member 293-308


            for (i = 0; i < jnc_6.Count; i++)
            {
                mbr = new Member();
                mbr.StartNode = jnc_6[i];
                mbr.EndNode = jnc_8[i];

                m_side_1.Add(mbr);
                sec_2.Add(mem_no++);
            }

            #endregion Member 109-124

            #region Member 309-323


            for (i = 1; i < jnc_5.Count; i += 2)
            {

                try
                {

                    mbr = new Member();
                    mbr.StartNode = jnc_7[i - 1];
                    mbr.EndNode = jnc_5[i];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);


                    mbr = new Member();
                    mbr.StartNode = jnc_5[i];
                    mbr.EndNode = jnc_7[i + 1];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);
                }
                catch (Exception exx) { }

            }

            #endregion Member 109-124

            #region Member 324-338


            for (i = 1; i < jnc_6.Count; i += 2)
            {

                try
                {

                    mbr = new Member();
                    mbr.StartNode = jnc_6[i - 1];
                    mbr.EndNode = jnc_8[i];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);


                    mbr = new Member();
                    mbr.StartNode = jnc_8[i];
                    mbr.EndNode = jnc_6[i + 1];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);
                }
                catch (Exception exx) { }

            }

            #endregion Member 140-154

            #region Member 339-353


            for (i = 1; i < jnc_7.Count; i += 2)
            {

                try
                {
                    mbr = new Member();
                    mbr.StartNode = jnc_8[i - 1];
                    mbr.EndNode = jnc_7[i];

                    m_side_1.Add(mbr);

                    sec_2.Add(mem_no++);

                    mbr = new Member();
                    mbr.StartNode = jnc_7[i];
                    mbr.EndNode = jnc_8[i + 1];

                    m_side_1.Add(mbr);

                    sec_2.Add(mem_no++);
                }
                catch (Exception exx) { }

            }

            #endregion Member 155-169

            #region Member 354-368

            for (i = 1; i < jnc_5.Count; i += 2)
            {

                try
                {

                    mbr = new Member();
                    mbr.StartNode = jnc_5[i - 1];
                    mbr.EndNode = jnc_6[i];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);

                    mbr = new Member();
                    mbr.StartNode = jnc_6[i];
                    mbr.EndNode = jnc_5[i + 1];

                    m_side_1.Add(mbr);
                    sec_2.Add(mem_no++);
                }
                catch (Exception exx) { }
            }

            #endregion Member 155-169

            #endregion Tower 2



            MemberCollection m_conn = new MemberCollection();


            if (Upper_Connector_Index != -1)
            {
                #region Connector 1


                indx = Upper_Connector_Index;


                mbr = new Member();

                mbr.StartNode = jnc_4[indx];
                mbr.EndNode = con_jnt_1[0];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = jnc_2[indx];
                mbr.EndNode = con_jnt_1[1];
                m_conn.Add(mbr);


                indx++;
                mbr = new Member();

                mbr.StartNode = jnc_2[indx];
                mbr.EndNode = con_jnt_1[2];
                m_conn.Add(mbr);



                //indx++;
                mbr = new Member();

                mbr.StartNode = jnc_4[indx];
                mbr.EndNode = con_jnt_1[3];
                m_conn.Add(mbr);



                //indx = 12;
                indx = Upper_Connector_Index;


                mbr = new Member();

                mbr.StartNode = jnc_8[indx];
                mbr.EndNode = con_jnt_1[0];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = jnc_6[indx];
                mbr.EndNode = con_jnt_1[1];
                m_conn.Add(mbr);


                indx++;
                mbr = new Member();

                mbr.StartNode = jnc_6[indx];
                mbr.EndNode = con_jnt_1[2];
                m_conn.Add(mbr);



                //indx++;
                mbr = new Member();

                mbr.StartNode = jnc_8[indx];
                mbr.EndNode = con_jnt_1[3];
                m_conn.Add(mbr);




                mbr = new Member();

                mbr.StartNode = con_jnt_1[0];
                mbr.EndNode = con_jnt_1[3];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = con_jnt_1[1];
                mbr.EndNode = con_jnt_1[2];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = con_jnt_1[0];
                mbr.EndNode = con_jnt_1[1];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = con_jnt_1[2];
                mbr.EndNode = con_jnt_1[3];
                m_conn.Add(mbr);







                //indx = 12;
                indx = Upper_Connector_Index;

                mbr = new Member();

                mbr.StartNode = jnc_4[indx];
                mbr.EndNode = con_jnt_1[3];
                m_conn.Add(mbr);




                mbr = new Member();

                mbr.StartNode = con_jnt_1[3];
                mbr.EndNode = jnc_8[indx];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = jnc_2[indx + 1];
                mbr.EndNode = con_jnt_1[1];
                m_conn.Add(mbr);




                mbr = new Member();

                mbr.StartNode = con_jnt_1[1];
                mbr.EndNode = jnc_6[indx + 1];
                m_conn.Add(mbr);


                #endregion Connector
            }


            if (Lower_Connector_Index != -1)
            {
                #region Connector 2

                //MemberCollection m_conn = new MemberCollection();

                //indx = 5;
                indx = Lower_Connector_Index;

                mbr = new Member();

                mbr.StartNode = jnc_4[indx];
                mbr.EndNode = con_jnt_2[0];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = jnc_2[indx];
                mbr.EndNode = con_jnt_2[1];
                m_conn.Add(mbr);


                indx++;
                mbr = new Member();

                mbr.StartNode = jnc_2[indx];
                mbr.EndNode = con_jnt_2[2];
                m_conn.Add(mbr);



                //indx++;
                mbr = new Member();

                mbr.StartNode = jnc_4[indx];
                mbr.EndNode = con_jnt_2[3];
                m_conn.Add(mbr);



                //indx = 5;
                indx = Lower_Connector_Index;


                mbr = new Member();

                mbr.StartNode = jnc_8[indx];
                mbr.EndNode = con_jnt_2[0];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = jnc_6[indx];
                mbr.EndNode = con_jnt_2[1];
                m_conn.Add(mbr);


                indx++;
                mbr = new Member();

                mbr.StartNode = jnc_6[indx];
                mbr.EndNode = con_jnt_2[2];
                m_conn.Add(mbr);



                //indx++;
                mbr = new Member();

                mbr.StartNode = jnc_8[indx];
                mbr.EndNode = con_jnt_2[3];
                m_conn.Add(mbr);




                mbr = new Member();

                mbr.StartNode = con_jnt_2[0];
                mbr.EndNode = con_jnt_2[3];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = con_jnt_2[1];
                mbr.EndNode = con_jnt_2[2];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = con_jnt_2[0];
                mbr.EndNode = con_jnt_2[1];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = con_jnt_2[2];
                mbr.EndNode = con_jnt_2[3];
                m_conn.Add(mbr);



                //indx = 5;
                indx = Lower_Connector_Index;

                mbr = new Member();

                mbr.StartNode = jnc_4[indx];
                mbr.EndNode = con_jnt_2[3];
                m_conn.Add(mbr);




                mbr = new Member();

                mbr.StartNode = con_jnt_2[3];
                mbr.EndNode = jnc_8[indx];
                m_conn.Add(mbr);


                mbr = new Member();

                mbr.StartNode = jnc_2[indx + 1];
                mbr.EndNode = con_jnt_2[1];
                m_conn.Add(mbr);




                mbr = new Member();

                mbr.StartNode = con_jnt_2[1];
                mbr.EndNode = jnc_6[indx + 1];
                m_conn.Add(mbr);


                #endregion Connector
            }
            mem_no = 1;


            #region MEMBER INCIDENCES
            list.Add("MEMBER INCIDENCES");
            foreach (var item in m_side_1)
            {
                item.MemberNo = mem_no++;
                list.Add(item.ToString());

            }
            foreach (var item in m_conn)
            {
                sec_2.Add(mem_no);

                item.MemberNo = mem_no++;
                list.Add(item.ToString());

            }

            #endregion MEMBER INCIDENCES

            #region MEMBER PROPERTY


            list.Add(string.Format("UNIT KG CM"));
            list.Add(string.Format("MEMBER PROPERTY"));
            //list.Add(string.Format("{0} PRISMATIC AX 7.44 IX 11.7 IY 11.7 IZ 11.7", MyList.Get_Array_Text(sec_2)));
            //list.Add(string.Format("{0} PRISMATIC AX 68.81 IX 1046.5 IY 1046.5 IZ 1046.5", MyList.Get_Array_Text(sec_1)));

            //list.Add(string.Format("{0} PRISMATIC AX {1} IX {2} IY {3} IZ {4}", MyList.Get_Array_Text(sec_1), Tower_SEC_VS_AX, Tower_SEC_VS_IX, Tower_SEC_VS_IX, Tower_SEC_VS_IZ));
            //list.Add(string.Format("{0} PRISMATIC AX {1} IX {2} IY {3} IZ {4}", MyList.Get_Array_Text(sec_2), Tower_SEC_BS_AX, Tower_SEC_BS_IX, Tower_SEC_BS_IX, Tower_SEC_BS_IZ));
            //list.Add(string.Format("{0} PRISMATIC AX 68.81 IX 1046.5 IY 1046.5 IZ 1046.5", MyList.Get_Array_Text(sec_1)));


            list.Add(string.Format("{0} PRISMATIC AX {1} IX {2} IZ {3}", MyList.Get_Array_Text(sec_1), Tower_SEC_VS_AX, Tower_SEC_VS_IX, Tower_SEC_VS_IZ));
            list.Add(string.Format("{0} PRISMATIC AX {1} IX {2} IZ {3}", MyList.Get_Array_Text(sec_2), Tower_SEC_BS_AX, Tower_SEC_BS_IX, Tower_SEC_BS_IZ));

            #endregion MEMBER PROPERTY


            #region CONSTANTS & Others

            List<int> supp = new List<int>();

            supp.Add(jnc_1[0].NodeNo);
            supp.Add(jnc_2[0].NodeNo);
            supp.Add(jnc_3[0].NodeNo);
            supp.Add(jnc_4[0].NodeNo);
            supp.Add(jnc_5[0].NodeNo);
            supp.Add(jnc_6[0].NodeNo);
            supp.Add(jnc_7[0].NodeNo);
            supp.Add(jnc_8[0].NodeNo);

            string supp_jnts = MyList.Get_Array_Text(supp);

            List<int> dl = new List<int>();
            indx = jnc_1.Count - 1;
            dl.Add(jnc_1[indx].NodeNo);
            dl.Add(jnc_2[indx].NodeNo);
            dl.Add(jnc_3[indx].NodeNo);
            dl.Add(jnc_4[indx].NodeNo);
            dl.Add(jnc_5[indx].NodeNo);
            dl.Add(jnc_6[indx].NodeNo);
            dl.Add(jnc_7[indx].NodeNo);
            dl.Add(jnc_8[indx].NodeNo);


            string dl_jnts = MyList.Get_Array_Text(dl);





            list.Add(string.Format("UNIT KG CM"));
            list.Add(string.Format("MATERIAL CONSTANT"));
            list.Add(string.Format("E 2000000 ALL"));
            list.Add(string.Format("DEN 78 ALL"));
            list.Add(string.Format("SUPPORTS"));
            //list.Add(string.Format("1 17 33 49 65 81 97 113 FIXED"));
            list.Add(string.Format("{0} FIXED", supp_jnts));
            //list.Add(string.Format("SELFWEIGHT Y -1.4"));
            list.Add(string.Format("UNIT MTON M"));
            list.Add(string.Format("LOAD 1 LIVE LOAD"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("{0} FY -{1}", dl_jnts, Tower_Live_Load));
            list.Add(string.Format("LOAD 2 DEAD LOAD"));
            list.Add(string.Format("JOINT LOAD"));
            list.Add(string.Format("{0} FY -{1}", dl_jnts, Tower_Dead_Load));
            list.Add(string.Format("SEISMIC COEEFICIENT {0}", Tower_Seismic_Coefficient));
            list.Add(string.Format("PRINT SUPPORT REACTION"));
            list.Add(string.Format("PERFORM ANALYSIS"));
            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));

            #endregion CONSTANTS


            File.WriteAllLines(file_name, list.ToArray());

            return true;
        }




    }

}
