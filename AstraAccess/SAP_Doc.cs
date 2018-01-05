using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using HEADSNeed.ASTRA.ASTRAClasses;
using AstraInterface.DataStructure;


namespace AstraAccess.SAP_Classes
{
    public class SAP_Document
    {
        #region Properties

        public string HED { get; set; }
        public Card_Master_Control MasterControl { get; set; }

        public List<SAP_Joint> Joints { get; set; }

        public Card_Truss_Control TrussControl { get; set; }

        public List<Truss_Material_Property> Truss_Mat_Properties { get; set; }
        public List<Load_Factors> Truss_Load_Factors { get; set; }
        //public List<Truss_Element> Trusses { get; set; }
        public List<Truss_Element> Trusses;


        public Card_Beam_Control BeamControl { get; set; }

        public List<Beam_Material_Property> Beam_Mat_Properties { get; set; }
        public List<Beam_Section_Property> Beam_Sect_Properties { get; set; }
        public List<Load_Factors> Beam_Load_Factors { get; set; }
        public List<Beam_Element> Beams { get; set; }


        public Card_Solid_Control SolidControl { get; set; }
        public List<Solid_Material_Property> Solid_Mat_Properties { get; set; }
        public List<Solid_Surface_Loads> Solid_Distributed_Loads { get; set; }
        public double Solid_Acceleration { get; set; }
        public List<Load_Factors> Solid_Load_Factors { get; set; }
        public List<Solid_Element> Solids { get; set; }




        public Card_Plate_Control PlateControl { get; set; }

        public List<Plate_Material_Property> Plate_Mat_Properties { get; set; }
        public List<Load_Factors> Plate_Load_Factors { get; set; }
        public List<Plate_Element> Plates { get; set; }


        public Card_Boundary_Control BoundaryControl { get; set; }
        public List<Load_Factors> Boundary_Load_Factors { get; set; }

        public List<Boundary_Element> Boundaries { get; set; }
        

        public List<SAP_Joint_Load> Joint_Loads { get; set; }


        public SAP_DynamicAnalysis DynamicAnalysis { get; set; }



        #endregion Properties

        public SAP_Document()
        {
            MasterControl = new Card_Master_Control();
            TrussControl = new Card_Truss_Control();
            BeamControl = new Card_Beam_Control();
            SolidControl = new Card_Solid_Control();
            PlateControl = new Card_Plate_Control();
            BoundaryControl = new Card_Boundary_Control();

            Joints = new List<SAP_Joint>();

            Truss_Mat_Properties = new List<Truss_Material_Property>();
            Truss_Load_Factors = new List<Load_Factors>();
            Trusses = new List<Truss_Element>();


            Beam_Mat_Properties = new List<Beam_Material_Property>();
            Beam_Sect_Properties = new List<Beam_Section_Property>();
            Beam_Load_Factors = new List<Load_Factors>();
            Beams = new List<Beam_Element>();


            Solid_Mat_Properties = new List<Solid_Material_Property>();
            Solid_Distributed_Loads = new List<Solid_Surface_Loads>();
            Solid_Acceleration = 0.0;
            Solid_Load_Factors = new List<Load_Factors>();
            Solids = new List<Solid_Element>();

        //public List<Solid_Material_Property> Solid_Mat_Properties { get; set; }
        //public Solid_Surface_Loads Solid_Distributed_Loads { get; set; }
        //public List<Load_Factors> Solid_Load_Factors { get; set; }
        //public List<Solid_Element> Solids { get; set; }



            Plate_Mat_Properties = new List<Plate_Material_Property>();
            Plate_Load_Factors = new List<Load_Factors>();
            Plates = new List<Plate_Element>();

            Boundary_Load_Factors = new List<Load_Factors>();
            Boundaries = new List<Boundary_Element>();



            Joint_Loads = new List<SAP_Joint_Load>();

            DynamicAnalysis = new SAP_DynamicAnalysis();

        }
        public void ClearAll()
        {

            Joints.Clear();

            Truss_Mat_Properties.Clear();
            Truss_Load_Factors.Clear();
            Trusses.Clear();


            Beam_Mat_Properties.Clear();
            Beam_Sect_Properties.Clear();
            Beam_Load_Factors.Clear();
            Beams.Clear();


            Solid_Mat_Properties.Clear();
            Solid_Distributed_Loads.Clear();
            Solid_Load_Factors.Clear();
            Solids.Clear();



            Plate_Mat_Properties.Clear();
            Plate_Load_Factors.Clear();
            Plates.Clear();

            Boundary_Load_Factors.Clear();
            Boundaries.Clear();


            DynamicAnalysis = new SAP_DynamicAnalysis();
            Joint_Loads.Clear();

        }

        public void Read_SAP_Data(string file_name)
        {
            if (!File.Exists(file_name)) return;

            ClearAll();

            List<string> list = new List<string>(File.ReadAllLines(file_name));
            MyList mlist = null;

            kString kStr = "";
            int counter = -1;
            int flag = 0;

            int elmt = 0;
            string fin_stmt = "12345678901234567890123456789012345678901234567890123456789012345678901234567890";
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    if (list[i].Contains(fin_stmt))
                        break;
                    kStr = list[i];

                    if (i == 0)
                    {
                        HED = kStr;
                        flag = 1;
                        i++;
                    }
                    if (flag == 1)
                    {
                        #region Read Joint Coordinates

                        MasterControl = list[i];


                        if (MasterControl.NF >= 1)
                        {
                            DynamicAnalysis.AnalysisType = (eSAP_AnalysisType)(MasterControl.NYDN);
                            DynamicAnalysis.FREQUENCIES = (MasterControl.NF);
                        }


                        counter = 0;
                        flag = 2;

                        for (counter = 0; counter < MasterControl.NUMNP; counter++)
                        {
                            i++;
                            kStr = list[i];
                            SAP_Joint jnt = new SAP_Joint();
                            try
                            {
                                jnt.NodeNo = kStr.Get_Int(2, 5);
                                jnt.Tx = kStr.Get_Int(6, 10) == 1;
                                jnt.Ty = kStr.Get_Int(11, 15) == 1;
                                jnt.Tz = kStr.Get_Int(16, 20) == 1;
                                jnt.Rx = kStr.Get_Int(21, 25) == 1;
                                jnt.Ry = kStr.Get_Int(26, 30) == 1;
                                jnt.Rz = kStr.Get_Int(31, 35) == 1;

                                jnt.X = kStr.Get_Double(36, 45);
                                jnt.Y = kStr.Get_Double(46, 55);
                                jnt.Z = kStr.Get_Double(56, 65);

                                jnt.Node_Increment = kStr.Get_Int(66, 70);
                                jnt.Node_Temperature = kStr.Get_Double(71, 80);

                                Joints.Add(jnt);
                                if(Joints.Count > 1)
                                    if (((Joints[Joints.Count - 1].NodeNo) - Joints[Joints.Count - 2].NodeNo) > 1)
                                    {
                                        counter++;
                                        SAP_Joint jnt2 = new SAP_Joint();
                                        jnt2.NodeNo = Joints[Joints.Count - 2].NodeNo + 1;
                                        jnt2.Tx = kStr.Get_Int(6, 10) == 1;
                                        jnt2.Ty = kStr.Get_Int(11, 15) == 1;
                                        jnt2.Tz = kStr.Get_Int(16, 20) == 1;
                                        jnt2.Rx = kStr.Get_Int(21, 25) == 1;
                                        jnt2.Ry = kStr.Get_Int(26, 30) == 1;
                                        jnt2.Rz = kStr.Get_Int(31, 35) == 1;

                                        jnt2.X = kStr.Get_Double(36, 45);
                                        jnt2.Y = kStr.Get_Double(46, 55);
                                        jnt2.Z = kStr.Get_Double(56, 65);

                                        jnt2.Node_Increment = kStr.Get_Int(66, 70);
                                        jnt2.Node_Temperature = kStr.Get_Double(71, 80);

                                        Joints.Add(jnt);

                                    }
                            }
                            catch (Exception eex) { }
                        }
                        i++;

                        #endregion Read Joint Coordinates
                    }

                    if (flag == 2)
                    {
                        #region Read Elements

                        kStr = list[i];

                        int option = kStr.Get_Int(1, 5);

                        do
                        {
                            kStr = list[i];
                            option = kStr.Get_Int(1, 5);

                            if (option == 1)// Truss Element
                            {
                                #region Read truss Elements


                                TrussControl = kStr;

                                counter = 0;
                                flag = 3;

                                for (counter = 0; counter < TrussControl.Total_Mat_Props; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Truss_Mat_Properties.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }


                                counter = 0;

                                for (counter = 0; counter < 4; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Truss_Load_Factors.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }



                                counter = 0;

                                for (counter = 0; counter < TrussControl.Total_Elements; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Trusses.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }
                                #endregion Read  truss Elements
                            }
                            else if (option == 2)// Beam Element
                            {
                                #region Read Beam Elements

                                BeamControl = kStr;

                                counter = 0;

                                for (counter = 0; counter < BeamControl.Total_Mat_Props; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Beam_Mat_Properties.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }

                                counter = 0;

                                for (counter = 0; counter < BeamControl.Total_Element_Props; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Beam_Sect_Properties.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }

                                counter = 0;

                                for (counter = 0; counter < 3; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Beam_Load_Factors.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }



                                counter = 0;
                                for (counter = 0; counter < BeamControl.Total_Elements; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Beams.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }
                                #endregion Read Beam Elements
                            }

                            else if (option == 5)// Solid Element
                            {
                                #region Read Solid Elements

                                SolidControl = kStr;

                                counter = 0;

                                for (counter = 0; counter < SolidControl.Total_Mat_Props; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Solid_Mat_Properties.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }

                                counter = 0;

                                for (counter = 0; counter < SolidControl.Total_Element_Loads; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Solid_Distributed_Loads.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }



                                i++;
                                kStr = list[i];
                                try
                                {
                                    Solid_Acceleration = kStr.Get_Double(1, 10);
                                }
                                catch (Exception erer) { }


                                counter = 0;

                                for (counter = 0; counter < 5; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Solid_Load_Factors.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }



                                counter = 0;
                                for (counter = 0; counter < SolidControl.Total_Elements; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Solids.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }
                                #endregion Read Solid Elements
                            }
                            else if (option == 6)// Beam Element
                            {
                                #region Read Plate Elements

                                PlateControl = kStr;

                                counter = 0;
                                flag = 3;

                                for (counter = 0; counter < PlateControl.Total_Element_Props; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Plate_Material_Property pmp = kStr;

                                        i++;
                                        pmp.Elasticity = list[i];

                                        Plate_Mat_Properties.Add(pmp);
                                    }
                                    catch (Exception eex) { }
                                }

                                counter = 0;

                                for (counter = 0; counter < 5; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Plate_Load_Factors.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }



                                counter = 0;

                                for (counter = 0; counter < PlateControl.Total_Elements; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {


                                        Plates.Add(kStr);



                                        //string kk = Plates[0].ToString();

                                    }
                                    catch (Exception eex) { }
                                }
                                #endregion Read Beam Elements
                            }
                            else if (option == 7)// Boundary Element
                            {
                                #region Read Boundary Elements

                                BoundaryControl = kStr;

                                counter = 0;
                                flag = 3;

                                counter = 0;

                                for (counter = 0; counter < 1; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Boundary_Load_Factors.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }



                                counter = 0;

                                for (counter = 0; counter < BoundaryControl.Total_Elements; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Boundaries.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }
                                #endregion Read Boundary Elements
                            }

                            elmt++;
                            i++;
                        }
                        while (elmt < MasterControl.NELTYP);

                        flag = 3;
                        //i++;
                        #endregion Read Elements
                    }

                    if (flag == 3) // Joint Loads
                    {
                        kStr = list[i];
                        if (kStr.Get_Int(0, 5) != 0)
                            Joint_Loads.Add(kStr);
                        else
                        {
                            flag = 4;
                        }

                    }
                    if(flag == 4)
                    {
                        if(MasterControl.NYDN == 3)
                        {
                            i++;
                            for (int c = 0; c < MasterControl.LL; c++)
                                i++;
                            kStr = list[i];
                            //if (kStr.Get_Int(0, 5) != 0)
                            //    Joint_Loads.Add(kStr);
                            DynamicAnalysis.Response_spectrum.CUTOFF_FREQUENCY = kStr.Get_Double(26, 35);

                            i++;
                            kStr = list[i];
                            DynamicAnalysis.Response_spectrum.X_DIRECTION_FACTORS = kStr.Get_Double(1, 10);
                            DynamicAnalysis.Response_spectrum.Y_DIRECTION_FACTORS = kStr.Get_Double(11, 20);
                            DynamicAnalysis.Response_spectrum.Z_DIRECTION_FACTORS = kStr.Get_Double(21, 30);
                            DynamicAnalysis.Response_spectrum.SCALE_FACTOR = kStr.Get_Double(31, 35);

                            i++;
                            kStr = list[i];
                            DynamicAnalysis.Response_spectrum.IS_DISPLACEMENT = kStr.TextString.StartsWith("DISP");

                            i++;
                            kStr = list[i];
                            int count = kStr.Get_Int(1, 5);

                            for (int c = 0; c < count; c++)
                            {
                                i++;
                                kStr = list[i];
                                DynamicAnalysis.Response_spectrum.Periods.Add(kStr.Get_Double(1, 10));
                                DynamicAnalysis.Response_spectrum.Values.Add(kStr.Get_Double(11, 20));
                            }
                        }
                        else if (MasterControl.NYDN == 2)
                        {
                            i++;
                            for (int c = 0; c < MasterControl.LL; c++)
                                i++;
                            //kStr = list[i];
                            //if (kStr.Get_Int(0, 5) != 0)
                            i++;
                            kStr = list[i];
                            //    Joint_Loads.Add(kStr);
                            DynamicAnalysis.Time_History.TIME_STEPS = kStr.Get_Double(16, 20);
                            DynamicAnalysis.Time_History.PRINT_INTERVAL = kStr.Get_Double(21, 25);
                            DynamicAnalysis.Time_History.STEP_INTERVAL = kStr.Get_Double(26, 35);
                            DynamicAnalysis.Time_History.DAMPING_FACTOR = kStr.Get_Double(36, 45);

                            i++;




                            i++;
                            kStr = list[i];


                            DynamicAnalysis.Time_History.GROUND_MOTION = kStr;



                            i++;
                            i++;
                            kStr = list[i];



                            DynamicAnalysis.Time_History.X_DIVISION = kStr.Get_Double(1, 5);
                            DynamicAnalysis.Time_History.SCALE_FACTOR = kStr.Get_Double(6, 15);



                            i++;


                            MyList ml = MyList.RemoveAllSpaces(list[i]);



                            for (int j = 1; j < ml.Count; j += 2)
                            {
                                DynamicAnalysis.Time_History.TIME_VALUES.Add(ml.GetDouble(j-1));
                                DynamicAnalysis.Time_History.TIME_FUNCTION.Add(ml.GetDouble(j));
                            }




                            i++;

                            //kStr = list[i];

                            kStr = list[i];

                            int ff = kStr.Get_Int(1, 5);

                            if(ff == 1)
                            {
                                do
                                {
                                    try
                                    {
                                        i++;
                                        kStr = list[i];
                                        ff = kStr.Get_Int(1, 5);
                                        ml = MyList.RemoveAllSpaces(list[i]);
                                        if(ml.Count == 8) DynamicAnalysis.Time_History.STRESS_COMPONENTS.Add(kStr);
                                        else if (ml.Count >= 2) DynamicAnalysis.Time_History.NODAL_CONSTRAINTS.Add(kStr);
                                    }
                                    catch (Exception exx) { }
                                }
                                while (ff != 0);
                            }
                        }
                        flag = 5;
                    }

                }
                catch (Exception exx) { }
            }

        }


        public void Read_SAP_Data(string file_name, bool fd)
        {

            if (!File.Exists(file_name)) return;

            ClearAll();

            List<string> list = new List<string>(File.ReadAllLines(file_name));
            MyList mlist = null;

            kString kStr = "";
            int counter = -1;
            int flag = 0;

            int elmt = 0;
            string fin_stmt = "12345678901234567890123456789012345678901234567890123456789012345678901234567890";
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    if (list[i].Contains(fin_stmt))
                        break;
                    kStr = list[i];

                    if (i == 0)
                    {
                        HED = kStr;
                        flag = 1;
                        i++;
                    }
                    if (flag == 1)
                    {
                        #region Read Joint Coordinates

                        MasterControl = list[i];


                        if (MasterControl.NF >= 1)
                        {
                            DynamicAnalysis.AnalysisType = (eSAP_AnalysisType)(MasterControl.NYDN);
                            DynamicAnalysis.FREQUENCIES = (MasterControl.NF);
                        }


                        counter = 0;
                        flag = 2;

                        for (counter = 0; counter < MasterControl.NUMNP; counter++)
                        {
                            i++;
                            kStr = list[i];
                            SAP_Joint jnt = new SAP_Joint();
                            try
                            {
                                jnt.NodeNo = kStr.Get_Int(2, 5);
                                jnt.Tx = kStr.Get_Int(6, 10) == 1;
                                jnt.Ty = kStr.Get_Int(11, 15) == 1;
                                jnt.Tz = kStr.Get_Int(16, 20) == 1;
                                jnt.Rx = kStr.Get_Int(21, 25) == 1;
                                jnt.Ry = kStr.Get_Int(26, 30) == 1;
                                jnt.Rz = kStr.Get_Int(31, 35) == 1;

                                jnt.X = kStr.Get_Double(36, 45);
                                jnt.Y = kStr.Get_Double(46, 55);
                                jnt.Z = kStr.Get_Double(56, 65);

                                jnt.Node_Increment = kStr.Get_Int(66, 70);
                                jnt.Node_Temperature = kStr.Get_Double(71, 80);

                                Joints.Add(jnt);
                            }
                            catch (Exception eex) { }
                        }
                        i++;

                        #endregion Read Joint Coordinates
                    }

                    if (flag == 2)
                    {

                        kStr = list[i];

                        int option = kStr.Get_Int(1, 5);

                        if (option == 1)// Truss Element
                        {

                            if (elmt <= MasterControl.NELTYP)
                                elmt++;
                            else
                                continue;


                            #region Read truss Elements


                            TrussControl = kStr;

                            counter = 0;
                            flag = 3;

                            for (counter = 0; counter < TrussControl.Total_Mat_Props; counter++)
                            {
                                i++;
                                kStr = list[i];
                                try
                                {
                                    Truss_Mat_Properties.Add(kStr);
                                }
                                catch (Exception eex) { }
                            }


                            counter = 0;

                            for (counter = 0; counter < 4; counter++)
                            {
                                i++;
                                kStr = list[i];
                                try
                                {
                                    Truss_Load_Factors.Add(kStr);
                                }
                                catch (Exception eex) { }
                            }



                            counter = 0;

                            for (counter = 0; counter < TrussControl.Total_Elements; counter++)
                            {
                                i++;
                                kStr = list[i];
                                try
                                {
                                    Trusses.Add(kStr);
                                }
                                catch (Exception eex) { }
                            }
                            #endregion Read  truss Elements

                            i++;
                            kStr = list[i];

                            option = kStr.Get_Int(1, 5);
                            if (option == 2)// Beam Element
                            {

                                if (elmt <= MasterControl.NELTYP)
                                    elmt++;
                                else
                                    continue;

                                #region Read Beam Elements

                                BeamControl = kStr;

                                counter = 0;
                                flag = 3;

                                for (counter = 0; counter < BeamControl.Total_Mat_Props; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Beam_Mat_Properties.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }

                                counter = 0;
                                flag = 3;

                                for (counter = 0; counter < BeamControl.Total_Element_Props; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Beam_Sect_Properties.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }

                                counter = 0;

                                for (counter = 0; counter < 3; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Beam_Load_Factors.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }



                                counter = 0;

                                for (counter = 0; counter < BeamControl.Total_Elements; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {

                                        Beams.Add(kStr);

                                        string kk = Beams[0].ToString();

                                    }
                                    catch (Exception eex) { }
                                }
                                #endregion Read Beam Elements
                            }
                        }
                        else if (option == 2)// Beam Element
                        {

                            if (elmt <= MasterControl.NELTYP)
                                elmt++;
                            else
                                continue;


                            #region Read Beam Elements

                            BeamControl = kStr;

                            counter = 0;
                            flag = 3;

                            for (counter = 0; counter < BeamControl.Total_Mat_Props; counter++)
                            {
                                i++;
                                kStr = list[i];
                                try
                                {
                                    Beam_Mat_Properties.Add(kStr);
                                }
                                catch (Exception eex) { }
                            }

                            counter = 0;
                            flag = 3;

                            for (counter = 0; counter < BeamControl.Total_Element_Props; counter++)
                            {
                                i++;
                                kStr = list[i];
                                try
                                {
                                    Beam_Sect_Properties.Add(kStr);
                                }
                                catch (Exception eex) { }
                            }

                            counter = 0;

                            for (counter = 0; counter < 3; counter++)
                            {
                                i++;
                                kStr = list[i];
                                try
                                {
                                    Beam_Load_Factors.Add(kStr);
                                }
                                catch (Exception eex) { }
                            }



                            counter = 0;

                            for (counter = 0; counter < BeamControl.Total_Elements; counter++)
                            {
                                i++;
                                kStr = list[i];
                                try
                                {

                                    Beams.Add(kStr);

                                    string kk = Beams[0].ToString();

                                }
                                catch (Exception eex) { }
                            }
                            #endregion Read Beam Elements

                            i++;
                            kStr = list[i];
                            option = kStr.Get_Int(1, 5);
                            if (option == 6) // Plate Element
                            {

                                if (elmt <= MasterControl.NELTYP)
                                    elmt++;
                                else
                                    continue;

                                #region Read Plate Elements

                                PlateControl = kStr;

                                counter = 0;
                                flag = 3;

                                for (counter = 0; counter < PlateControl.Total_Element_Props; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Plate_Material_Property pmp = kStr;

                                        i++;
                                        pmp.Elasticity = list[i];

                                        Plate_Mat_Properties.Add(pmp);
                                    }
                                    catch (Exception eex) { }
                                }

                                counter = 0;

                                for (counter = 0; counter < 5; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Plate_Load_Factors.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }



                                counter = 0;

                                for (counter = 0; counter < PlateControl.Total_Elements; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {


                                        Plates.Add(kStr);



                                        //string kk = Plates[0].ToString();

                                    }
                                    catch (Exception eex) { }
                                }
                                #endregion Read Beam Elements
                                i++;
                            }

                            kStr = list[i];
                            option = kStr.Get_Int(1, 5);
                            if (option == 7) // Plate Element
                            {
                                #region Read Boundary Elements

                                BoundaryControl = kStr;

                                counter = 0;
                                flag = 3;

                                counter = 0;

                                for (counter = 0; counter < 1; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Boundary_Load_Factors.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }



                                counter = 0;

                                for (counter = 0; counter < BoundaryControl.Total_Elements; counter++)
                                {
                                    i++;
                                    kStr = list[i];
                                    try
                                    {
                                        Boundaries.Add(kStr);
                                    }
                                    catch (Exception eex) { }
                                }
                                #endregion Read Boundary Elements
                            }
                        }
                        flag = 3;
                        i++;
                    }

                    if (flag == 3) // Joint Loads
                    {

                        kStr = list[i];
                        if (kStr.Get_Int(0, 5) != 0)
                            Joint_Loads.Add(kStr);
                        else
                        {
                            flag = 4;

                        }

                    }

                }
                catch (Exception exx) { }
            }

        }

        public List<string> Get_SAP_Data()
        {
            List<string> list = new List<string>();
            list.Add(HED);
            #region Master Control
            MasterControl = Get_Master_Control();
            list.Add(MasterControl);
            foreach (var item in Joints)
            {
                list.Add(item.ToString());
            }
            #endregion Master Control

            #region Truss Element
            TrussControl = Get_Truss_Control();

            if (TrussControl.Total_Mat_Props > 0)
            {
                list.Add(TrussControl);
                foreach (var item in Truss_Mat_Properties)
                {
                    list.Add(item.ToString());
                }
                if (Truss_Load_Factors.Count > 0)
                {
                    foreach (var item in Truss_Load_Factors)
                    {
                        list.Add(item.ToString());
                    }
                }
                else
                {
                    list.Add(string.Format("     0.000       0.0       0.0       0.0"));
                    list.Add(string.Format("    -0.002       0.0       0.0       0.0"));
                    list.Add(string.Format("     0.000       0.0       0.0       0.0"));
                    list.Add(string.Format("     0.000       0.0       0.0       0.0"));
                }
            }
            if (TrussControl.Total_Elements > 0)
            {
                foreach (var item in Trusses)
                {
                    list.Add(item.ToString());
                }
            }
            #endregion Truss Element


            #region Beam Element
            BeamControl = Get_Beam_Control();

            if (BeamControl.Total_Mat_Props > 0)
            {
                list.Add(BeamControl);
                foreach (var item in Beam_Mat_Properties)
                {
                    list.Add(item.ToString());
                }
            }
            if (BeamControl.Total_Element_Props > 0)
            {
                foreach (var item in Beam_Sect_Properties)
                {
                    list.Add(item.ToString());
                }
                if (Beam_Load_Factors.Count > 0)
                {
                    foreach (var item in Beam_Load_Factors)
                    {
                        list.Add(item.ToString());
                    }
                }
                else
                {
                    list.Add(string.Format("     0.000       0.0       0.0       0.0"));
                    list.Add(string.Format("    -1.400       0.0       0.0       0.0"));
                    list.Add(string.Format("     0.000       0.0       0.0       0.0"));
                }
            }
            if (BeamControl.Total_Elements > 0)
            {
                foreach (var item in Beams)
                {
                    list.Add(item.ToString());
                }
            }
            #endregion Beam Element


            #region Solid Element
            SolidControl = Get_Solid_Control();

            if (SolidControl.Total_Mat_Props > 0)
            {
                list.Add(SolidControl);
                foreach (var item in Solid_Mat_Properties)
                {
                    list.Add(item.ToString());
                }
            }
            if (SolidControl.Total_Element_Loads > 0)
            {
                foreach (var item in Solid_Distributed_Loads)
                {
                    list.Add(item.ToString());
                }
                list.Add(string.Format("{0,10:f3}", Solid_Acceleration));

                if (Solid_Load_Factors.Count > 0)
                {
                    foreach (var item in Solid_Load_Factors)
                    {
                        list.Add(item.ToString());
                    }
                }
                else
                {
                    list.Add(string.Format("     1.000     0.000     0.000     0.000"));
                    list.Add(string.Format("     1.000     0.000     0.000     0.000"));
                    list.Add(string.Format("     1.000     1.000     1.000     1.000"));
                    list.Add(string.Format("     1.000     1.000     1.000     1.000"));
                    list.Add(string.Format("     1.000     1.000     1.000     1.000"));
                }


            }
            if (SolidControl.Total_Elements > 0)
            {
                foreach (var item in Solids)
                {
                    list.Add(item.ToString());
                }
            }
            #endregion Solid Element



            #region Plate Element
            PlateControl = Get_Plate_Control();

            if (PlateControl.Total_Element_Props > 0)
            {
                list.Add(PlateControl);
                foreach (var item in Plate_Mat_Properties)
                {
                    list.Add(item.ToString());
                    list.Add(item.Elasticity.ToString());
                }
                if (Plate_Load_Factors.Count > 0)
                {
                    foreach (var item in Plate_Load_Factors)
                    {
                        list.Add(item.ToString());
                    }
                }
                else
                {
                    list.Add(string.Format("     0.000       0.0       0.0       0.0"));
                    list.Add(string.Format("     0.000       0.0       0.0       0.0"));
                    list.Add(string.Format("     0.000       0.0       0.0       0.0"));
                    list.Add(string.Format("     0.000       0.0       0.0       0.0"));
                    list.Add(string.Format("     0.000       0.0       0.0       0.0"));
                }
            }
            if (PlateControl.Total_Elements > 0)
            {
                foreach (var item in Plates)
                {
                    list.Add(item.ToString());
                }
            }
            #endregion Plate Element



            #region Boundary Element

            BoundaryControl = Get_Boundary_Control();


            if (BoundaryControl.Total_Elements > 0)
            {
                list.Add(BoundaryControl);
                if (Boundary_Load_Factors.Count > 0)
                {
                    foreach (var item in Boundary_Load_Factors)
                    {
                        list.Add(item.ToString());
                    }
                }
                else
                {
                    list.Add(string.Format("     1.000     0.000     0.000     0.000"));
                }
                foreach (var item in Boundaries)
                {
                    list.Add(item.ToString());
                }
            }
            #endregion Boundary Element

            #region Joint Loads
            foreach (var item in Joint_Loads)
            {
                list.Add(item);
            }
            #endregion Joint Loads

            list.Add(string.Format("    0    0              0              0              0              0              0              0"));

            for (int i = 0; i < MasterControl.LL; i++ )
                list.Add(string.Format("     1.000     0.000     0.000     0.000"));
            if(DynamicAnalysis.AnalysisType == eSAP_AnalysisType.ResponceSpectrum)
            {
                list.AddRange(DynamicAnalysis.Response_spectrum.Get_SAP_Data().ToArray());

            } 
            else if (DynamicAnalysis.AnalysisType == eSAP_AnalysisType.TimeHistory)
            {
                list.AddRange(DynamicAnalysis.Time_History.Get_SAP_Data().ToArray());

            }

            //list.Add(string.Format(""));
            list.Add(string.Format("         1         2         3         4         5         6         7         8"));
            list.Add(string.Format("12345678901234567890123456789012345678901234567890123456789012345678901234567890"));
            return list;

        }

        Card_Master_Control Get_Master_Control()
        {
            Card_Master_Control master = new Card_Master_Control();
            master.NUMNP = Joints.Count;

            int elmt_grp = 0;
            if (Trusses.Count > 0)
                elmt_grp++;
            if (Beams.Count > 0)
                elmt_grp++;
            if (Plates.Count > 0)
                elmt_grp++;
            if (Solids.Count > 0)
                elmt_grp++;
            if (Boundaries.Count > 0)
                elmt_grp++;

            master.NELTYP = elmt_grp;
            master.LL = Get_LoadCases();
            master.NF = DynamicAnalysis.FREQUENCIES;

            master.NYDN = (int) DynamicAnalysis.AnalysisType;

            return master;
        }

        Card_Truss_Control Get_Truss_Control()
        {
            Card_Truss_Control trss_ctrl = new Card_Truss_Control();
            trss_ctrl.Element_Type = 1;
            trss_ctrl.Total_Elements = Trusses.Count;
            trss_ctrl.Total_Mat_Props = Truss_Mat_Properties.Count;

            return trss_ctrl;
        }

        Card_Beam_Control Get_Beam_Control()
        {

            Card_Beam_Control beam_ctrl = new Card_Beam_Control();
            beam_ctrl.Element_Type = 2;
            beam_ctrl.Total_Elements = Beams.Count;
            beam_ctrl.Total_Mat_Props = Beam_Mat_Properties.Count;
            beam_ctrl.Total_Element_Props = Beam_Sect_Properties.Count;

            return beam_ctrl;
        }


        Card_Solid_Control Get_Solid_Control()
        {

            Card_Solid_Control solid_ctrl = new Card_Solid_Control();
            solid_ctrl.Element_Type = 5;
            solid_ctrl.Total_Elements = Solids.Count;
            solid_ctrl.Total_Mat_Props = Solid_Mat_Properties.Count;
            solid_ctrl.Total_Element_Loads = Solid_Distributed_Loads.Count;

            return solid_ctrl;
        }

        Card_Plate_Control Get_Plate_Control()
        {

            Card_Plate_Control ctrl = new Card_Plate_Control();
            ctrl.Element_Type = 6;
            ctrl.Total_Elements = Plates.Count;
            ctrl.Total_Element_Props = Plate_Mat_Properties.Count;

            return ctrl;
        }

        Card_Boundary_Control Get_Boundary_Control()
        {

            Card_Boundary_Control ctrl = new Card_Boundary_Control();
            ctrl.Element_Type = 7;
            ctrl.Total_Elements = Boundaries.Count;
            return ctrl;
        }

        public int Get_LoadCases()
        {
            List<int> load = new List<int>();
            foreach (var item in Joint_Loads)
            {
                if (!load.Contains(item.Load_No))
                    load.Add(item.Load_No);
            }
            return load.Count;
        }
    }

    public class Card_Master_Control
    {
        public int NUMNP { get; set; }
        public int NELTYP { get; set; }
        public int LL { get; set; }
        public int NF { get; set; }
        public int NYDN { get; set; }
        public int MODEX { get; set; }
        public int NAD { get; set; }
        public int KEQ { get; set; }

        public Card_Master_Control(kString data)
        {
            Set_Data(data);
        }

        public Card_Master_Control()
        {

            NUMNP = 0;
            NELTYP = 0;
            LL = 0;
            NF = 0;
            NYDN = 0;
            MODEX = 0;
            NAD = 0;
            KEQ = 0;
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                NUMNP = kStr.Get_Int(1, 5);
                NELTYP = kStr.Get_Int(6, 10);
                LL = kStr.Get_Int(11, 15);
                NF = kStr.Get_Int(16, 20);
                NYDN = kStr.Get_Int(21, 25);
                MODEX = kStr.Get_Int(26, 30);
                NAD = kStr.Get_Int(31, 35);
                KEQ = kStr.Get_Int(36, 40);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            return string.Format("{0,5}{1,5}{2,5}{3,5}{4,5}{5,5}{6,5}{7,5}    0    0    0    0    0", NUMNP, NELTYP, LL, NF, NYDN, MODEX, NAD, KEQ);
        }

        public static implicit operator Card_Master_Control(string rhs)
        {
            Card_Master_Control c = new Card_Master_Control(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Card_Master_Control rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Card_Master_Control(kString rhs)
        {
            Card_Master_Control c = new Card_Master_Control(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(Card_Master_Control rhs)
        {
            return rhs.ToString();
        }



    }
    public class Card_Truss_Control
    {
        public int Element_Type { get; set; }
        public int Total_Elements { get; set; }
        public int Total_Mat_Props { get; set; }
        public string Ref_Text { get; set; }

        public Card_Truss_Control()
        {
            Element_Type = 1;
            Total_Elements = 0;
            Total_Mat_Props = 0;

            Ref_Text = "    0    0    0    0    0    0    0    0    0    0    0    0";
        }
        public Card_Truss_Control(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Element_Type = kStr.Get_Int(1, 5);
                Total_Elements = kStr.Get_Int(6, 10);
                Total_Mat_Props = kStr.Get_Int(11, 15);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            return string.Format("{0,5}{1,5}{2,5}{3}", Element_Type, Total_Elements, Total_Mat_Props, Ref_Text);
        }

        public static implicit operator Card_Truss_Control(string rhs)
        {
            Card_Truss_Control c = new Card_Truss_Control(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Card_Truss_Control rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Card_Truss_Control(kString rhs)
        {
            Card_Truss_Control c = new Card_Truss_Control(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(Card_Truss_Control rhs)
        {
            return rhs.ToString();
        }



    }
    public class Truss_Material_Property
    {
        public int Material_No { get; set; }
        public double Modulus_of_Elasticity { get; set; }
        public double Thermal_Coefficient { get; set; }
        public double Mass_Density { get; set; }
        public double Cross_Sectional_Area { get; set; }
        public double Weight_Density { get; set; }


        public Truss_Material_Property()
        {
            Material_No = 0;
            Modulus_of_Elasticity = 0;
            Thermal_Coefficient = 0;
            Mass_Density = 0;
            Cross_Sectional_Area = 0;
            Weight_Density = 0;
        }
        public Truss_Material_Property(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Material_No = kStr.Get_Int(1, 5);
                Modulus_of_Elasticity = kStr.Get_Double(6, 15);
                Thermal_Coefficient = kStr.Get_Double(16, 25);
                Mass_Density = kStr.Get_Double(26, 35);
                Cross_Sectional_Area = kStr.Get_Double(36, 45);
                Weight_Density = kStr.Get_Double(46, 55);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {

             //1 2.11E+008     0.000    78.3322.361E-002    78.332
            return string.Format("{0,5}{1,10:E2}{2,10:f3}{3,10:f3}{4,10:E3}{5,10:f3}",
                Material_No, Modulus_of_Elasticity, Thermal_Coefficient, Mass_Density, Cross_Sectional_Area, Weight_Density);
        }

        public static implicit operator Truss_Material_Property(string rhs)
        {
            Truss_Material_Property c = new Truss_Material_Property(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Truss_Material_Property rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Truss_Material_Property(kString rhs)
        {
            Truss_Material_Property c = new Truss_Material_Property(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(Truss_Material_Property rhs)
        {
            return rhs.ToString();
        }



    }
    public class Load_Factors
    {
        public double Element_Loadcase_A { get; set; }
        public double Element_Loadcase_B { get; set; }
        public double Element_Loadcase_C { get; set; }
        public double Element_Loadcase_D { get; set; }



        public Load_Factors()
        {
            Element_Loadcase_A = 0.0;
            Element_Loadcase_B = 0.0;
            Element_Loadcase_C = 0.0;
            Element_Loadcase_D = 0.0;
        }
        public Load_Factors(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Element_Loadcase_A = kStr.Get_Double(1, 10);
                Element_Loadcase_B = kStr.Get_Double(11, 20);
                Element_Loadcase_C = kStr.Get_Double(21, 30);
                Element_Loadcase_D = kStr.Get_Double(31, 40);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            return string.Format("{0,10:f3}{1,10:f1}{2,10:f1}{3,10:f1}",
                Element_Loadcase_A, Element_Loadcase_B, 
                Element_Loadcase_C, Element_Loadcase_D);
        }

        public static implicit operator Load_Factors(string rhs)
        {
            Load_Factors c = new Load_Factors(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Load_Factors rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Load_Factors(kString rhs)
        {
            Load_Factors c = new Load_Factors(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(Load_Factors rhs)
        {
            return rhs.ToString();
        }



    }
    [Serializable]
    public class Truss_Element
    {
        public int Element_No { get; set; }
        public double Node_I { get; set; }
        public double Node_J { get; set; }
        public int Material_Property_No { get; set; }

        public string Ref_Temp { get; set; }


        public Truss_Element()
        {
            Element_No = 0;
            Node_I = 0;
            Node_J = 0;
            Material_Property_No = 0;
            Ref_Temp = "";
        }
        public Truss_Element(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Element_No = kStr.Get_Int(1, 5);
                Node_I = kStr.Get_Int(6, 10);
                Node_J = kStr.Get_Int(11, 15);
                Material_Property_No = kStr.Get_Int(16, 20);
                Ref_Temp = kStr.Get_String(31);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {

            return string.Format("{0,5}{1,5}{2,5}{3,5}{4}",
                Element_No, Node_I, Node_J, Material_Property_No, Ref_Temp);
        }

        public static implicit operator Truss_Element(string rhs)
        {
            Truss_Element c = new Truss_Element(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Truss_Element rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Truss_Element(kString rhs)
        {
            Truss_Element c = new Truss_Element(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(Truss_Element rhs)
        {
            return rhs.ToString();
        }
    }
    public class Card_Beam_Control
    {
        public int Element_Type { get; set; }
        public int Total_Elements { get; set; }
        public int Total_Element_Props { get; set; }
        public int Total_Force_Sets { get; set; }
        public int Total_Mat_Props { get; set; }
        public string Ref_Text { get; set; }

        public Card_Beam_Control()
        {
            Element_Type = 2;
            Total_Elements = 0;
            Total_Element_Props = 0;
            Total_Force_Sets = 0;
            Total_Mat_Props = 0;
            Ref_Text = "    0    0    0    0    0    0    0    0    0    0    0    0";
        }
        public Card_Beam_Control(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Element_Type = kStr.Get_Int(1, 5);
                Total_Elements = kStr.Get_Int(6, 10);
                Total_Element_Props = kStr.Get_Int(11, 15);
                Total_Force_Sets = kStr.Get_Int(16, 20);
                Total_Mat_Props = kStr.Get_Int(21, 25);
                Ref_Text = kStr.Get_String(26);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            return string.Format("{0,5}{1,5}{2,5}{3,5}{4,5}{5}", 
                Element_Type, Total_Elements, Total_Element_Props,
                Total_Force_Sets, Total_Mat_Props, Ref_Text);
        }

        public static implicit operator Card_Beam_Control(string rhs)
        {
            Card_Beam_Control c = new Card_Beam_Control(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Card_Beam_Control rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Card_Beam_Control(kString rhs)
        {
            Card_Beam_Control c;
            if(rhs == "")
                c = new Card_Beam_Control(); //Internally call Currency constructor
            else
                c = new Card_Beam_Control(rhs); //Internally call Currency constructor


            return c;
        }
        public static implicit operator kString(Card_Beam_Control rhs)
        {
            return rhs.ToString();
        }



    }
    public class Beam_Material_Property
    {
        public int Material_No { get; set; }
        public double Youngs_Modulus { get; set; }
        public double Poisson_Ratio { get; set; }
        public double Mass_Density { get; set; }
        public double Weight_Density { get; set; }


        public Beam_Material_Property()
        {
            Material_No = 0;
            Youngs_Modulus = 0.0;
            Poisson_Ratio = 0.0;
            Mass_Density = 0.0;
            Weight_Density = 0.0;
        }
        public Beam_Material_Property(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Material_No = kStr.Get_Int(1, 5);
                Youngs_Modulus = kStr.Get_Double(6, 15);
                Poisson_Ratio = kStr.Get_Double(16, 25);
                Mass_Density = kStr.Get_Double(26, 35);
                Weight_Density = kStr.Get_Double(36, 45);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {

            //1 2.11E+008     0.000    78.3322.361E-002    78.332
            //return string.Format("{0,5}{1,10:E2}{2,10:f6}{3,10:f3}{4,10:f3}",
            return string.Format("{0,5}{1,10:E2}{2,10:f6}{3,10:f2}{4,10:f2}",
                Material_No, Youngs_Modulus, Poisson_Ratio, Mass_Density,  Weight_Density);
        }

        public static implicit operator Beam_Material_Property(string rhs)
        {
            Beam_Material_Property c = new Beam_Material_Property(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Beam_Material_Property rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Beam_Material_Property(kString rhs)
        {
            Beam_Material_Property c = new Beam_Material_Property(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(Beam_Material_Property rhs)
        {
            return rhs.ToString();
        }
    }
    public class Beam_Section_Property
    {
        public int Property_No { get; set; }
        public double AX { get; set; }
        public double AY { get; set; }
        public double AZ { get; set; }
        public double IX { get; set; }
        public double IY { get; set; }
        public double IZ { get; set; }


        public Beam_Section_Property()
        {
            Property_No = 0;
            AX = 0.0;
            AY = 0.0;
            AZ = 0.0;
            IX = 0.0;
            IY = 0.0;
            IZ = 0.0;
        }
        public Beam_Section_Property(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Property_No = kStr.Get_Int(1, 5);
                AX = kStr.Get_Double(6, 15);
                AY = kStr.Get_Double(16, 25);
                AZ = kStr.Get_Double(26, 35);
                IX = kStr.Get_Double(36, 45);
                IY = kStr.Get_Double(46, 55);
                IZ = kStr.Get_Double(56, 65);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {


            //1  0.033120  0.000000  0.000000 1.00E-004 1.41E-003 1.00E-003
            return string.Format("{0,5}{1,10:f6}{2,10:f6}{3,10:f6}{4,10:E2}{5,10:E2}{6,10:E2}",
                Property_No, AX, AY, AZ, IX, IY, IZ);
        }

        public static implicit operator Beam_Section_Property(string rhs)
        {
            Beam_Section_Property c = new Beam_Section_Property(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Beam_Section_Property rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Beam_Section_Property(kString rhs)
        {
            Beam_Section_Property c = new Beam_Section_Property(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(Beam_Section_Property rhs)
        {
            return rhs.ToString();
        }
    }
    public class Beam_Element
    {

        public int Element_No { get; set; }
        public int Node_I { get; set; }
        public int Node_J { get; set; }
        public int Node_K { get; set; }
        public int Material_Property_No { get; set; }
        public int Element_Property_No { get; set; }

        public int End_Force_No { get; set; }
        public kString NODE_I_Release_Code { get; set; }
        public kString NODE_J_Release_Code { get; set; }

        public string Ref_Text { get; set; }


        public Beam_Element()
        {
            Element_No = 0;
            Node_I = 0;
            Node_J = 0;
            Node_K = 0;
            Material_Property_No = 0;
            Element_Property_No = 0;
            NODE_I_Release_Code = "";
            NODE_J_Release_Code = "";
            Ref_Text = "";
        }
        public Beam_Element(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Element_No = kStr.Get_Int(1, 5);
                Node_I = kStr.Get_Int(6, 10);
                Node_J = kStr.Get_Int(11, 15);
                Node_K = kStr.Get_Int(16, 20);
                Material_Property_No = kStr.Get_Int(21, 25);
                Element_Property_No = kStr.Get_Int(26, 30);
                End_Force_No = kStr.Get_Int(31, 35);
                NODE_I_Release_Code = kStr.Get_String(51, 56);
                NODE_J_Release_Code = kStr.Get_String(57, 62);
                Ref_Text = kStr.Get_String(63);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            return string.Format("{0,5}{1,5}{2,5}{3,5}{4,5}{5,5}{6,5}{7,15}{8,6}{9,6}{10}",

                Element_No, Node_I, Node_J, Node_K, 
                Material_Property_No,
                Element_Property_No,
                End_Force_No,
                "",
                NODE_I_Release_Code,
                NODE_J_Release_Code, 
                Ref_Text);
        }

        public static implicit operator Beam_Element(string rhs)
        {
            Beam_Element c = new Beam_Element(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Beam_Element rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Beam_Element(kString rhs)
        {
            Beam_Element c = new Beam_Element(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(Beam_Element rhs)
        {
            return rhs.ToString();
        }
    }

    public class Card_Plate_Control
    {
        public int Element_Type { get; set; }
        public int Total_Elements { get; set; }
        public int Total_Element_Props { get; set; }
        public string Ref_Text { get; set; }

        public Card_Plate_Control()
        {
            Element_Type = 6;
            Total_Elements = 0;
            Total_Element_Props = 0;
            Ref_Text = "";
        }
        public Card_Plate_Control(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Element_Type = kStr.Get_Int(1, 5);
                Total_Elements = kStr.Get_Int(6, 10);
                Total_Element_Props = kStr.Get_Int(11, 15);
                Ref_Text = kStr.Get_String(16);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            return string.Format("{0,5}{1,5}{2,5}{3}",
                Element_Type, Total_Elements, Total_Element_Props, Ref_Text);
        }

        public static implicit operator Card_Plate_Control(string rhs)
        {
            Card_Plate_Control c = new Card_Plate_Control(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Card_Plate_Control rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Card_Plate_Control(kString rhs)
        {
            Card_Plate_Control c;
            if (rhs == "")
                c = new Card_Plate_Control(); //Internally call Currency constructor
            else
                c = new Card_Plate_Control(rhs); //Internally call Currency constructor

            return c;
        }
        public static implicit operator kString(Card_Plate_Control rhs)
        {
            return rhs.ToString();
        }



    }

    /**/
    public class Plate_Material_Property
    {
        
        public int Material_No { get; set; }
        public double Mass_Density { get; set; }
        public double Thermal_Coefficient_Ax { get; set; }
        public double Thermal_Coefficient_Ay { get; set; }
        public double Thermal_Coefficient_Axy { get; set; }
        public Plate_Elasticity_Elements Elasticity { get; set; }

        public Plate_Material_Property()
        {
            Material_No = 0;
            Mass_Density = 0.0;
            Thermal_Coefficient_Ax = 0.0;
            Thermal_Coefficient_Ay = 0.0;
            Thermal_Coefficient_Axy = 0.0;
            Elasticity = new Plate_Elasticity_Elements();
        }
        public Plate_Material_Property(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Material_No = kStr.Get_Int(1, 10);
                Mass_Density = kStr.Get_Double(11, 20);
                Thermal_Coefficient_Ax = kStr.Get_Double(21, 30);
                Thermal_Coefficient_Ay = kStr.Get_Double(31, 40);
                Thermal_Coefficient_Axy = kStr.Get_Double(41, 50);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {

            //1 2.11E+008     0.000    78.3322.361E-002    78.332
            return string.Format("{0,10}{1,10:E2}{2,10:f3}{3,10:f3}{4,10:f3}",
                Material_No, Mass_Density, Thermal_Coefficient_Ax, Thermal_Coefficient_Ay, Thermal_Coefficient_Axy);
        }

        public static implicit operator Plate_Material_Property(string rhs)
        {
            Plate_Material_Property c = new Plate_Material_Property(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Plate_Material_Property rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Plate_Material_Property(kString rhs)
        {
            Plate_Material_Property c = new Plate_Material_Property(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(Plate_Material_Property rhs)
        {
            return rhs.ToString();
        }
    }
    public class Plate_Elasticity_Elements
    {

        public double Cxx { get; set; }
        public double Cxy { get; set; }
        public double Cxs { get; set; }
        public double Cyy { get; set; }
        public double Cys { get; set; }
        public double Gxy { get; set; }


        public Plate_Elasticity_Elements()
        {
            Cxx = 0.0;
            Cxy = 0.0;
            Cxs = 0.0;
            Cyy = 0.0;
            Cys = 0.0;
            Gxy = 0.0;
        }
        public Plate_Elasticity_Elements(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Cxx = kStr.Get_Double(1, 10);
                Cxy = kStr.Get_Double(11, 20);
                Cxs = kStr.Get_Double(21, 30);
                Cyy = kStr.Get_Double(31, 40);
                Cys = kStr.Get_Double(41, 50);
                Gxy = kStr.Get_Double(51, 60);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {

            //1 2.11E+008     0.000    78.3322.361E-002    78.332
            return string.Format("{0,10:f1}{1,10:f1}{2,10:f1}{3,10:f1}{4,10:f1}{5,10:f1}",
                Cxx, Cxy, Cxs, Cyy, Cys, Gxy);
        }

        public static implicit operator Plate_Elasticity_Elements(string rhs)
        {
            Plate_Elasticity_Elements c = new Plate_Elasticity_Elements(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Plate_Elasticity_Elements rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Plate_Elasticity_Elements(kString rhs)
        {
            Plate_Elasticity_Elements c = new Plate_Elasticity_Elements(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(Plate_Elasticity_Elements rhs)
        {
            return rhs.ToString();
        }
    }

    public class Plate_Element
    {

        public int Element_No { get; set; }
        public int Node_I { get; set; }
        public int Node_J { get; set; }
        public int Node_K { get; set; }
        public int Node_L { get; set; }
        public int Node_O { get; set; }
        public int Material_Property_No { get; set; }
        public int Element_Data_Generator { get; set; }
        public double Element_Thickness { get; set; }
        public double Element_Pressure { get; set; }
        public double Mean_Temperature_Variation { get; set; }
        public double Mean_Temperature_Gradient { get; set; }
        public string Ref_Temp { get; set; }

        public Plate_Element()
        {
            Element_No = 0;
            Node_I = 0;
            Node_J = 0;
            Node_K = 0;
            Node_L = 0;
            Node_O = 0;
            Material_Property_No = 0;
            Element_Data_Generator = 0;
            Element_Thickness = 0.0;
            Element_Pressure = 0.0;
            Mean_Temperature_Variation = 0.0;
            Mean_Temperature_Gradient = 0.0;
            Mean_Temperature_Gradient = 0.0;
            Ref_Temp = "";
        }
        public Plate_Element(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Element_No = kStr.Get_Int(1, 5);
                Node_I = kStr.Get_Int(6, 10);
                Node_J = kStr.Get_Int(11, 15);
                Node_K = kStr.Get_Int(16, 20);
                Node_L = kStr.Get_Int(21, 25);
                Node_O = kStr.Get_Int(26, 30);
                Material_Property_No = kStr.Get_Int(31, 35);
                Element_Data_Generator = kStr.Get_Int(36, 40);
                Element_Thickness = kStr.Get_Double(41, 50);
                Element_Pressure = kStr.Get_Double(51, 60);
                Mean_Temperature_Variation = kStr.Get_Double(61, 70);
                Mean_Temperature_Gradient = kStr.Get_Double(71, 80);
                Ref_Temp = kStr.Get_String(81);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            return string.Format("{0,5}{1,5}{2,5}{3,5}{4,5}{5,5}{6,5}{7,5}{8,10}{9,10}{10,10}{11,10}",

                Element_No, Node_I, Node_J, Node_K, Node_L, Node_O,

                Material_Property_No, Element_Data_Generator, Element_Thickness,
                 Element_Pressure, Mean_Temperature_Variation, Mean_Temperature_Gradient,
                Ref_Temp);
        }

        public static implicit operator Plate_Element(string rhs)
        {
            Plate_Element c = new Plate_Element(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Plate_Element rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Plate_Element(kString rhs)
        {
            Plate_Element c = new Plate_Element(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(Plate_Element rhs)
        {
            return rhs.ToString();
        }
    }


    public class Card_Boundary_Control
    {

        public int Element_Type { get; set; }
        public int Total_Elements { get; set; }
        public string Ref_Text { get; set; }

        public Card_Boundary_Control()
        {
            Element_Type = 7;
            Total_Elements = 0;
           
            Ref_Text = "";
        }
        public Card_Boundary_Control(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Element_Type = kStr.Get_Int(1, 5);
                Total_Elements = kStr.Get_Int(6, 10);
                
                Ref_Text = kStr.Get_String(11);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            return string.Format("{0,5}{1,5}{2}",
                Element_Type, Total_Elements, Ref_Text);
        }

        public static implicit operator Card_Boundary_Control(string rhs)
        {
            Card_Boundary_Control c = new Card_Boundary_Control(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Card_Boundary_Control rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Card_Boundary_Control(kString rhs)
        {
            Card_Boundary_Control c;
            if (rhs == "")
                c = new Card_Boundary_Control(); //Internally call Currency constructor
            else
                c = new Card_Boundary_Control(rhs); //Internally call Currency constructor

            return c;
        }
        public static implicit operator kString(Card_Boundary_Control rhs)
        {
            return rhs.ToString();
        }



    }

    public class Boundary_Element
    {

        public int Node_N { get; set; }
        public int Node_I { get; set; }
        public int Node_J { get; set; }
        public int Node_K { get; set; }
        public int Node_L { get; set; }
        public int Displacement_Code { get; set; }
        public int Rotation_Code { get; set; }
        public int Data_Generator { get; set; }
        public double Displacement { get; set; }
        public double Roration { get; set; }
        public double Spring_Stiffness { get; set; }
        public string Ref_Temp { get; set; }

        public Boundary_Element()
        {
            Node_N = 0;
            Node_I = 0;
            Node_J = 0;
            Node_K = 0;
            Node_L = 0;
            Displacement_Code = 0;
            Rotation_Code = 0;
            Data_Generator = 0;
            Displacement = 0.0;
            Roration = 0.0;
            Spring_Stiffness = 0.0;
          
            Ref_Temp = "";
        }
        public Boundary_Element(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Node_N = kStr.Get_Int(1, 5);
                Node_I = kStr.Get_Int(6, 10);
                Node_J = kStr.Get_Int(11, 15);
                Node_K = kStr.Get_Int(16, 20);
                Node_L = kStr.Get_Int(21, 25);
                Displacement_Code = kStr.Get_Int(26, 30);
                Rotation_Code = kStr.Get_Int(31, 35);
                Data_Generator = kStr.Get_Int(36, 40);
                Displacement = kStr.Get_Double(41, 50);
                Roration = kStr.Get_Double(51, 60);
                Spring_Stiffness = kStr.Get_Double(61, 70);
               
                Ref_Temp = kStr.Get_String(81);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            return string.Format("{0,5}{1,5}{2,5}{3,5}{4,5}{5,5}{6,5}{7,5}{8,10:f3}{9,10:f3}{10,10:f3}",

                Node_N, Node_I, Node_J, Node_K, Node_L, Displacement_Code,

                Rotation_Code, Data_Generator, Displacement,
                 Roration, Spring_Stiffness,
                Ref_Temp);
        }

        public static implicit operator Boundary_Element(string rhs)
        {
            Boundary_Element c = new Boundary_Element(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Boundary_Element rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Boundary_Element(kString rhs)
        {
            Boundary_Element c = new Boundary_Element(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(Boundary_Element rhs)
        {
            return rhs.ToString();
        }
    }

    public class SAP_Joint_Load
    {

        public int Joint_No { get; set; }
        public int Load_No { get; set; }
        public double FX { get; set; }
        public double FY { get; set; }
        public double FZ { get; set; }
        public double MX { get; set; }
        public double MY { get; set; }
        public double MZ { get; set; }

        public SAP_Joint_Load()
        {
            Joint_No = 0;
            Load_No = 0;
            FX = 0.0;
            FY = 0.0;
            FZ = 0.0;
            MX = 0.0;
            MY = 0.0;
            MZ = 0.0;
        }
        public SAP_Joint_Load(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Joint_No = kStr.Get_Int(1, 5);
                Load_No = kStr.Get_Int(6, 10);
                FX = kStr.Get_Double(11, 20);
                FY = kStr.Get_Double(21, 30);
                FZ = kStr.Get_Double(31, 40);
                MX = kStr.Get_Double(41, 50);
                MY = kStr.Get_Double(51, 60);
                MZ = kStr.Get_Double(61, 70);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            return string.Format("{0,5}{1,5}{2,10:f3}{3,10:f3}{4,10:f3}{5,10:f3}{6,10:f3}{7,10:f3}",

                Joint_No, Load_No, FX, FY, FZ, MX, MY, MZ);
        }

        public static implicit operator SAP_Joint_Load(string rhs)
        {
            SAP_Joint_Load c = new SAP_Joint_Load(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(SAP_Joint_Load rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator SAP_Joint_Load(kString rhs)
        {
            SAP_Joint_Load c = new SAP_Joint_Load(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(SAP_Joint_Load rhs)
        {
            return rhs.ToString();
        }
    }

    public class kString
    {
        public string TextString { get; set; }
        public kString(string val)
        {
            TextString = val;
        }
        public int Get_Int(int start_index, int end_index)
        {
            string ss = Get_String(start_index, end_index);
            return int.Parse(ss);
        }

        public int Get_Int()
        {
            return int.Parse(TextString);
        }
        public int Get_Int(int default_value)
        {
            return MyList.StringToInt(TextString, default_value);
        }

        public double Get_Double()
        {
            return double.Parse(TextString);
        }

        public double Get_Double(double default_value)
        {
            return MyList.StringToDouble(TextString, default_value);
        }

        public double Get_Double(int start_index, int end_index)
        {
            string ss = Get_String(start_index, end_index);
            return double.Parse(ss);
        }
        public string Get_String(int start_index, int end_index)
        {
            if (start_index > 0)
                start_index--;

            if (end_index > TextString.Length)
                end_index = start_index + (TextString.Length - start_index);

            string ss = TextString.Substring(start_index, (end_index - start_index));
            return ss;
        }

        public string Get_String(int start_index)
        {
            if (start_index > 0)
                start_index--;

            int end_index = start_index + (TextString.Length - start_index);

            string ss = TextString.Substring(start_index, (end_index - start_index));
            return ss;
        }

        public static implicit operator kString(string rhs)
        {
            kString c = new kString(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(kString rhs)
        {
            return rhs.TextString;
        }

        public override string ToString()
        {
            return TextString;
        }
    }
    public class SAP_Joint
    {
        #region Properties
        public bool IsCylindrical { get; set; }

        public char Symbol
        {
            get
            {
                if (IsCylindrical) return 'C';
                return ' ';
            }
        }
        public int NodeNo { get; set; }
        public bool Tx { get; set; }
        public bool Ty { get; set; }
        public bool Tz { get; set; }
        public bool Rx { get; set; }
        public bool Ry { get; set; }
        public bool Rz { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int Node_Increment { get; set; }
        public double Node_Temperature { get; set; }
        #endregion Properties
        public SAP_Joint()
        {
            IsCylindrical = false;
            NodeNo = 0;
            Tx = false;
            Ty = false;
            Tz = false;
            Rx = false;
            Ry = false;
            Rz = false;
            X = 0.0;
            Y = 0.0;
            Z = 0.0;
            Node_Increment = 0;
            Node_Temperature = 0.0;
        }
        public override string ToString()
        {
            return string.Format("{0,1}{1,4}{2,5}{3,5}{4,5}{5,5}{6,5}{7,5}{8,10:f4}{9,10:f4}{10,10:f4}{11,5}{12,10:f1}",
            //return string.Format("{0,1}{1,4}{2,5}{3,5}{4,5}{5,5}{6,5}{7,5}{8,10:f3}{9,10:f3}{10,10:f3}{11,5}{12,5:f1}",
                Symbol,
                NodeNo,
                (Tx ? 1 : 0),
                (Ty ? 1 : 0),
                (Tz ? 1 : 0),
                (Rx ? 1 : 0),
                (Ry ? 1 : 0),
                (Rz ? 1 : 0),
                X, Y, Z,
                //X, Z, Y,
                Node_Increment,
                Node_Temperature
                );
        }
    }

    public class SAP_DynamicAnalysis
    {

        public int FREQUENCIES { get; set; }
        public Response_Spectrum_Analysis Response_spectrum { get; set; }
        //public double FX { get; set; }
        //public double FY { get; set; }
        //public double FZ { get; set; }
        //public double MX { get; set; }
        //public double MY { get; set; }
        //public double MZ { get; set; }

        public Time_History_Function Time_History { get; set; }
        
        public eSAP_AnalysisType AnalysisType { get; set; }



        public SAP_DynamicAnalysis()
        {
            FREQUENCIES = 0;
            AnalysisType = eSAP_AnalysisType.StaticAnalysis;
            Response_spectrum = new Response_Spectrum_Analysis();
            Time_History = new Time_History_Function();
            //FX = 0.0;
            //FY = 0.0;
            //FZ = 0.0;
            //MX = 0.0;
            //MY = 0.0;
            //MZ = 0.0;
        }
        public SAP_DynamicAnalysis(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                //FREQUENCIES = kStr.Get_Int(1, 5);
                //Response_spectrum = kStr.Get_Int(6, 10);
                //FX = kStr.Get_Double(11, 20);
                //FY = kStr.Get_Double(21, 30);
                //FZ = kStr.Get_Double(31, 40);
                //MX = kStr.Get_Double(41, 50);
                //MY = kStr.Get_Double(51, 60);
                //MZ = kStr.Get_Double(61, 70);
            }
            catch (Exception ex) { }
        }

       
    }

    public class Response_Spectrum_Analysis
    {
        public double CUTOFF_FREQUENCY { get; set; }
        public double X_DIRECTION_FACTORS { get; set; }
        public double Y_DIRECTION_FACTORS { get; set; }
        public double Z_DIRECTION_FACTORS { get; set; }
        public double SCALE_FACTOR { get; set; }

        public bool IS_DISPLACEMENT { get; set; }
        public List<double> Periods { get; set; }
        public List<double> Values { get; set; }
//        PERFORM RESPONSE SPECTRUM ANALYSIS
//FREQUENCIES 5
//CUTOFF FREQUENCY 10.5
//DIRECTION FACTORS X 1.0 Y 0.6667 Z 0.0 
//*SPECTRUM TYPE DISPLACEMENT
//SPECTRUM TYPE ACCELERATION
//SPECTRUM POINTS 16
//SCALE FACTOR 1.0
//*PERIOD DISPLACEMENT
//PERIOD ACCELERATION 
//      0.00    46.328
//      0.02    46.328
//      0.10   149.023
//      0.18   207.706
//      0.20   211.566
//      0.22   212.725
//      0.26   210.408
//      0.30   203.845
//      0.56   134.352
//      0.70   110.416
//      0.90    86.094
//      1.20    62.929
//      1.40    52.892
//      1.80    39.765
//      2.00    35.518
//      3.00    21.620


        public Response_Spectrum_Analysis()
        {
            CUTOFF_FREQUENCY = 0.0;
            X_DIRECTION_FACTORS = 0.0;
            Y_DIRECTION_FACTORS = 0.0;
            Z_DIRECTION_FACTORS = 0.0;
            SCALE_FACTOR = 1.0;


            IS_DISPLACEMENT = false;
            Periods = new List<double>();
            Values = new List<double>();
        }

        public List<string> Get_SAP_Data()
        {
            List<string> list = new List<string>();

            //list.Add(string.Format("    0    0    0       0.0      10.5"));


            list.Add(string.Format("    0    0    0       0.0{0,10}", CUTOFF_FREQUENCY));

            //list.Add(string.Format("       1.0       0.7       0.0    1"));
            list.Add(string.Format("{0,10}{1,10}{2,10}    1", X_DIRECTION_FACTORS,Y_DIRECTION_FACTORS,Z_DIRECTION_FACTORS));

            if(IS_DISPLACEMENT)
                list.Add(string.Format("  DISPLACEMENT SPECTRUM"));
            else
                list.Add(string.Format("  ACCELERATION SPECTRUM"));



            //list.Add(string.Format("  ACCELERATION SPECTRUM"));
            //list.Add(string.Format("   16       1.0"));
            list.Add(string.Format("{0,5}{1,10:f2}", Periods.Count, SCALE_FACTOR));
            for (int i = 0; i < Periods.Count; i++)
            {
                list.Add(string.Format("{0,10:f3}{1,10:f3}", Periods[i], Values[i]));
                
            }
            //list.Add(string.Format("       0.0      46.3"));
            //list.Add(string.Format("       0.0      46.3"));
            //list.Add(string.Format("       0.1     149.0"));
            //list.Add(string.Format("       0.2     207.7"));
            //list.Add(string.Format("       0.2     211.6"));
            //list.Add(string.Format("       0.2     212.7"));
            //list.Add(string.Format("       0.3     210.4"));
            //list.Add(string.Format("       0.3     203.8"));
            //list.Add(string.Format("       0.6     134.4"));
            //list.Add(string.Format("       0.7     110.4"));
            //list.Add(string.Format("       0.9      86.1"));
            //list.Add(string.Format("       1.2      62.9"));
            //list.Add(string.Format("       1.4      52.9"));
            //list.Add(string.Format("       1.8      39.8"));
            //list.Add(string.Format("       2.0      35.5"));
            //list.Add(string.Format("       3.0      21.6"));
            //list.Add(string.Format("       3.0      21.6"));
            return list;
                
        }
    }

    public class Time_History_Function
    {


        //TIME STEPS 200
        //PRINT_INTERVAL 10
        //STEP_INTERVAL 0.1
        //DAMPING_FACTOR 0
        //GROUND_MOTION TZ
        //X_DIVISION 3
        //SCALE_FACTOR 1000.0
        //TIME_VALUES 0.0 10.0 30.0
        //TIME_FUNCTION 0.0 1.0 -1.0
        //NODAL_CONSTRAINT_DOF
        //5 TZ RX
        //9 TZ RX
        //MEMBER_STRESS_COMPONENT
        //1 TO 4, 5, 6 TO 8 END
        public double TIME_STEPS { get; set; }
        public double PRINT_INTERVAL { get; set; }
        public double STEP_INTERVAL { get; set; }
        public double DAMPING_FACTOR { get; set; }
        public GROUND_MOTION GROUND_MOTION { get; set; }
        public double X_DIVISION { get; set; }
        public double SCALE_FACTOR { get; set; }

        public List<double> TIME_VALUES { get; set; }
        public List<double> TIME_FUNCTION { get; set; }
        public List<NODAL_CONSTRAINT_DOF> NODAL_CONSTRAINTS { get; set; }
        public List<MEMBER_STRESS_COMPONENT> STRESS_COMPONENTS { get; set; }

        public Time_History_Function()
        {
            TIME_STEPS = 0.0;
            PRINT_INTERVAL = 0.0;
            STEP_INTERVAL = 0.0;
            DAMPING_FACTOR = 0.0;

            GROUND_MOTION = new SAP_Classes.GROUND_MOTION();
            X_DIVISION = 3.0;
            SCALE_FACTOR = 2000.0;


            //IS_DISPLACEMENT = false;
            TIME_VALUES = new List<double>();
            TIME_FUNCTION = new List<double>();

            NODAL_CONSTRAINTS = new List<NODAL_CONSTRAINT_DOF>();
            STRESS_COMPONENTS = new List<MEMBER_STRESS_COMPONENT>();
        }

        public List<string> Get_SAP_Data()
        {
            List<string> list = new List<string>();

            #region FG
            //list.Add(string.Format("    1"));
            //list.Add(string.Format("    1    1    1  200   10       0.1       0.0"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("              1"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("    3    1000.0 FIRST TIME FUNCTION"));
            //list.Add(string.Format("   0.0   0.0  10.0   1.0  30.0  -1.0"));
            //list.Add(string.Format("    1"));
            //list.Add(string.Format("    5    3    4"));
            //list.Add(string.Format("    9    3    4"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("    1"));
            //list.Add(string.Format("    1    7    8    9   10   11   12    0"));
            //list.Add(string.Format("    2    7    8    9   10   11   12    0"));
            //list.Add(string.Format("    3    7    8    9   10   11   12    0"));
            //list.Add(string.Format("    4    7    8    9   10   11   12    0"));
            //list.Add(string.Format("    5    7    8    9   10   11   12    0"));
            //list.Add(string.Format("    6    7    8    9   10   11   12    0"));
            //list.Add(string.Format("    7    7    8    9   10   11   12    0"));
            //list.Add(string.Format("    8    7    8    9   10   11   12    0"));
            //list.Add(string.Format("    0"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            #endregion FG

            #region FG
            list.Add(string.Format("    1"));
            list.Add(string.Format("    1    1    1{0,5}{1,5}{2,10}{3,10}", TIME_STEPS, PRINT_INTERVAL, STEP_INTERVAL, DAMPING_FACTOR));
            list.Add(string.Format(""));
            //list.Add(string.Format("              1"));
            list.Add(string.Format("{0}", GROUND_MOTION));
            list.Add(string.Format(""));
            list.Add(string.Format("{0,5}{1,10} FIRST TIME FUNCTION", X_DIVISION, SCALE_FACTOR));

            string str = "";

            for (int i = 0; i < TIME_VALUES.Count; i++)
            {
                //str += string.Format("{0,5:f1}{1,5:f1}", TIME_VALUES[i], TIME_FUNCTION[i]);
                str += string.Format("{0,6:f1}{1,6:f1}", TIME_VALUES[i], TIME_FUNCTION[i]);
            }

            //list.Add(string.Format("   0.0   0.0  10.0   1.0  30.0  -1.0"));
            list.Add(string.Format("{0}", str));
            list.Add(string.Format("    1"));
            //list.Add(string.Format("    5    3    4"));
            //list.Add(string.Format("    9    3    4"));
            foreach (var item in NODAL_CONSTRAINTS)
            {
                list.Add(string.Format(item.ToString()));
            }
            list.Add(string.Format(""));
            list.Add(string.Format("    1"));
            foreach (var item in STRESS_COMPONENTS)
            {
                list.Add(string.Format(item.ToString()));
            }
            //list.Add(string.Format("    1    7    8    9   10   11   12    0"));
            //list.Add(string.Format("    2    7    8    9   10   11   12    0"));
            //list.Add(string.Format("    3    7    8    9   10   11   12    0"));
            //list.Add(string.Format("    4    7    8    9   10   11   12    0"));
            //list.Add(string.Format("    5    7    8    9   10   11   12    0"));
            //list.Add(string.Format("    6    7    8    9   10   11   12    0"));
            //list.Add(string.Format("    7    7    8    9   10   11   12    0"));
            //list.Add(string.Format("    8    7    8    9   10   11   12    0"));
            list.Add(string.Format("    0"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            #endregion FG


            return list;

        }
    }

    public class GROUND_MOTION
    {
        public bool Tx { get; set; }
        public bool Ty { get; set; }
        public bool Tz { get; set; }
        public bool Rx { get; set; }
        public bool Ry { get; set; }
        public bool Rz { get; set; }

        public GROUND_MOTION()
        {
            Tx = false;
            Ty = false;
            Tz = false;
            Rx = false;
            Ry = false;
            Rz = false;
        }

        public GROUND_MOTION(kString kStr)
        {
            Set_Data(kStr);
        }


        public void Set_Data(kString kStr)
        {
            try
            {
                //5    3    4
                //9    3    4



                int count = 0;

                for (int i = 1; i < kStr.TextString.Length; i += 5)
                {
                    try
                    {

                        if (count == 0) Tx = (kStr.Get_Int(i, i + 4) == 1);
                        else if (count == 1) Ty = (kStr.Get_Int(i, i + 4) == 1);
                        else if (count == 2) Tz = (kStr.Get_Int(i, i + 4) == 1);
                        else if (count == 3) Rx = (kStr.Get_Int(i, i + 4) == 1);
                        else if (count == 4) Ry = (kStr.Get_Int(i, i + 4) == 1);
                        else if (count == 5) Rz = (kStr.Get_Int(i, i + 4) == 1);

                    }
                    catch (Exception exx) { }
                    count++;
                }


                //Node_1 = kStr.Get_Int(1, 5);
                //Node_1 = kStr.Get_Int(6, 10);
                //Node_2 = kStr.Get_Int(11, 15);
                //Node_3 = kStr.Get_Int(16, 20);
                //Node_4 = kStr.Get_Int(21, 25);
                //Node_5 = kStr.Get_Int(26, 30);
                //Node_6 = kStr.Get_Int(31, 35);
                //Node_7 = kStr.Get_Int(36, 40);
                //Node_8 = kStr.Get_Int(41, 45);
                //Integration_Order = kStr.Get_Int(46, 50);
                //Material_No = kStr.Get_Int(51, 55);
                //Generation_Parameter = kStr.Get_Int(56, 60);
                //LSA = kStr.Get_Int(61, 62);
                //LSB = kStr.Get_Int(63, 64);
                //LSC = kStr.Get_Int(65, 66);
                //LSD = kStr.Get_Int(67, 68);
                //Face_Number = kStr.Get_Int(69, 70);
                //Stress_free_Temperature = kStr.Get_Double(71, 80);

                //Ref_Temp = kStr.Get_String(81);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            //5    3    4
            //9    3    4

            string str = "";

            if (Tx) str += string.Format("{0,5}", 1);
            else str += string.Format("{0,5}", "");

            if (Ty) str += string.Format("{0,5}", 1);
            else str += string.Format("{0,5}", "");

            if (Tz) str += string.Format("{0,5}", 1);
            else str += string.Format("{0,5}", "");

            if (Rx) str += string.Format("{0,5}", 1);
            else str += string.Format("{0,5}", "");

            if (Ry) str += string.Format("{0,5}", 1);
            else str += string.Format("{0,5}", "");

            if (Rz) str += string.Format("{0,5}", 1);
            else str += string.Format("{0,5}", "");
            
            return str;
        }

        public static implicit operator GROUND_MOTION(string rhs)
        {
            GROUND_MOTION c = new GROUND_MOTION(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(GROUND_MOTION rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator GROUND_MOTION(kString rhs)
        {
            GROUND_MOTION c = new GROUND_MOTION(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(GROUND_MOTION rhs)
        {
            return rhs.ToString();
        }
    }
    public class NODAL_CONSTRAINT_DOF
    {
        public int NodeNo { get; set; }
        public bool Tx { get; set; }
        public bool Ty { get; set; }
        public bool Tz { get; set; }
        public bool Rx { get; set; }
        public bool Ry { get; set; }
        public bool Rz { get; set; }

        public NODAL_CONSTRAINT_DOF()
        {
            NodeNo = 0;
            Tx = false;
            Ty = false;
            Tz = false;
            Rx = false;
            Ry = false;
            Rz = false;
        }

        public NODAL_CONSTRAINT_DOF(kString kStr)
        {
            Set_Data(kStr);
        }


        public void Set_Data(kString kStr)
        {
            try
            {
                //5    3    4
                //9    3    4

                NodeNo = kStr.Get_Int(1, 5);

                MyList ml = kStr.TextString;

                for (int i = 6; i < kStr.TextString.Length; i += 5)
                {
                    switch (kStr.Get_Int(i, i + 4))
                    {
                        case 1: Tx = true; break;
                        case 2: Ty = true; break;
                        case 3: Tz = true; break;
                        case 4: Rx = true; break;
                        case 5: Ry = true; break;
                        case 6: Rz = true; break;
                    }
                }


                //Node_1 = kStr.Get_Int(1, 5);
                //Node_1 = kStr.Get_Int(6, 10);
                //Node_2 = kStr.Get_Int(11, 15);
                //Node_3 = kStr.Get_Int(16, 20);
                //Node_4 = kStr.Get_Int(21, 25);
                //Node_5 = kStr.Get_Int(26, 30);
                //Node_6 = kStr.Get_Int(31, 35);
                //Node_7 = kStr.Get_Int(36, 40);
                //Node_8 = kStr.Get_Int(41, 45);
                //Integration_Order = kStr.Get_Int(46, 50);
                //Material_No = kStr.Get_Int(51, 55);
                //Generation_Parameter = kStr.Get_Int(56, 60);
                //LSA = kStr.Get_Int(61, 62);
                //LSB = kStr.Get_Int(63, 64);
                //LSC = kStr.Get_Int(65, 66);
                //LSD = kStr.Get_Int(67, 68);
                //Face_Number = kStr.Get_Int(69, 70);
                //Stress_free_Temperature = kStr.Get_Double(71, 80);

                //Ref_Temp = kStr.Get_String(81);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            //5    3    4
            //9    3    4

            string str = string.Format("{0,5}", NodeNo);

            if (Tx) str += string.Format("{0,5}", 1);
            if (Ty) str += string.Format("{0,5}", 2);
            if (Tz) str += string.Format("{0,5}", 3);
            if (Rx) str += string.Format("{0,5}", 4);
            if (Ry) str += string.Format("{0,5}", 5);
            if (Rz) str += string.Format("{0,5}", 6);

            return str;
        }

        public static implicit operator NODAL_CONSTRAINT_DOF(string rhs)
        {
            NODAL_CONSTRAINT_DOF c = new NODAL_CONSTRAINT_DOF(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(NODAL_CONSTRAINT_DOF rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator NODAL_CONSTRAINT_DOF(kString rhs)
        {
            NODAL_CONSTRAINT_DOF c = new NODAL_CONSTRAINT_DOF(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(NODAL_CONSTRAINT_DOF rhs)
        {
            return rhs.ToString();
        }
    }
    public class MEMBER_STRESS_COMPONENT
    {
        public int NodeNo { get; set; }
        public bool IS_START { get; set; }
        public string TEXT
        {
            get
            {
                return IS_START ? "START" : "END";

            }
        }

        public bool IS_END 
        { 
            get
            {
                return !IS_START;
            }
            set
            {
                IS_START = !value;
            }
        }

        public MEMBER_STRESS_COMPONENT()
        {
            NodeNo = 0;
            IS_START = true;
        }

        public MEMBER_STRESS_COMPONENT(kString kStr)
        {
            Set_Data(kStr);
        }


        public void Set_Data(kString kStr)
        {
            try
            {
                //1    1    2    3    4    5    6    0
                //2    1    2    3    4    5    6    0
                //3    1    2    3    4    5    6    0

                NodeNo = kStr.Get_Int(1, 5);
                IS_START = (kStr.Get_Int(6, 10) == 1);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            //5    3    4
            //9    3    4

            string str = string.Format("{0,5}", NodeNo);

            if (IS_START) str += string.Format("    1    2    3    4    5    6    0");
            else str += string.Format("    7    8    9   10   11   12    0");
           
            return str;
        }

        public static implicit operator MEMBER_STRESS_COMPONENT(string rhs)
        {
            MEMBER_STRESS_COMPONENT c = new MEMBER_STRESS_COMPONENT(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(MEMBER_STRESS_COMPONENT rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator MEMBER_STRESS_COMPONENT(kString rhs)
        {
            MEMBER_STRESS_COMPONENT c = new MEMBER_STRESS_COMPONENT(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(MEMBER_STRESS_COMPONENT rhs)
        {
            return rhs.ToString();
        }
    }


    public enum eSAP_AnalysisType
    {
        StaticAnalysis = 0,
        EigenValue = 1,
        TimeHistory = 2,
        ResponceSpectrum = 3,
        StepByStep = 4,
    }

    public class Card_Solid_Control
    {

        public int Element_Type { get; set; }
        public int Total_Elements { get; set; }
        public int Total_Mat_Props { get; set; }
        public int Total_Element_Loads { get; set; }
        public string Ref_Text { get; set; }

        public Card_Solid_Control()
        {
            Element_Type = 5;
            Total_Elements = 0;
            Total_Mat_Props = 0;
            Total_Element_Loads = 0;
           
            Ref_Text = "";
        }
        public Card_Solid_Control(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Element_Type = kStr.Get_Int(1, 5);
                Total_Elements = kStr.Get_Int(6, 10);
                Total_Mat_Props = kStr.Get_Int(11, 15);
                Total_Element_Loads = kStr.Get_Int(16, 20);
                
                Ref_Text = kStr.Get_String(21);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            return string.Format("{0,5}{1,5}{2,5}{3,5}{4}",
                Element_Type, Total_Elements, Total_Mat_Props, Total_Element_Loads, Ref_Text);
        }

        public static implicit operator Card_Solid_Control(string rhs)
        {
            Card_Solid_Control c = new Card_Solid_Control(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Card_Solid_Control rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Card_Solid_Control(kString rhs)
        {
            Card_Solid_Control c;
            if (rhs == "")
                c = new Card_Solid_Control(); //Internally call Currency constructor
            else
                c = new Card_Solid_Control(rhs); //Internally call Currency constructor

            return c;
        }
        public static implicit operator kString(Card_Solid_Control rhs)
        {
            return rhs.ToString();
        }



    }

    public class Solid_Material_Property
    {

        public int Material_No { get; set; }
        public double Modulus_of_Elasticity { get; set; }
        public double Poisson_Ratio { get; set; }
        public double Weight_Density { get; set; }
        public double Thermal_Coefficient { get; set; }
        public Solid_Material_Property()
        {
            Material_No = 0;
            Weight_Density = 0.0;
            Modulus_of_Elasticity = 0.0;
            Poisson_Ratio = 0.0;
            Thermal_Coefficient = 0.0;
        }
        public Solid_Material_Property(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Material_No = kStr.Get_Int(1, 5);
                Modulus_of_Elasticity = kStr.Get_Double(6, 15);
                Poisson_Ratio = kStr.Get_Double(16, 25);
                Weight_Density = kStr.Get_Double(26, 35);
                Thermal_Coefficient = kStr.Get_Double(36, 45);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            //1 2.85E+006      0.15       2.4       2.4                                              Brick8 Material

            return string.Format("{0,5}{1,10:E2}{2,10:f3}{3,10:f3}{4,10:f3}",
                Material_No, Modulus_of_Elasticity, Poisson_Ratio, Weight_Density, Thermal_Coefficient);
        }

        public static implicit operator Solid_Material_Property(string rhs)
        {
            Solid_Material_Property c = new Solid_Material_Property(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Solid_Material_Property rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Solid_Material_Property(kString rhs)
        {
            Solid_Material_Property c = new Solid_Material_Property(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(Solid_Material_Property rhs)
        {
            return rhs.ToString();
        }
    }

    public class Solid_Surface_Loads
    {

        public int Load_No { get; set; }

        /// <summary>
        /// LT = Load Type
        /// LT = 1 if this card specifies a uniformly distributed load
        /// LT = 2 if this card specifies a hydrostatically varing pressure
        /// </summary>
        public int LT { get; set; }

        /// <summary>
        /// if LT = 1, P is the magnitude of the uniformly distributed load
        /// if LT = 2, P is the weight density of the fluid causing the hydrostatic pressure
        /// </summary>
        public double P { get; set; }

        /// <summary>
        /// if LT = 1, Y = 0
        /// if LT = 2, Y id the global Y coordinate of the surface of fluid causing  hydrostatic pressure loading
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Element face number on which surface load acts.
        /// </summary>
        public int Element_Face_No { get; set; }

        public Solid_Surface_Loads()
        {
            Load_No = 0;
            LT = 1;
            P = 1;
            Y = 0.0;

        }
        public Solid_Surface_Loads(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Load_No = kStr.Get_Int(1, 5);
                LT = kStr.Get_Int(6, 10);
                P = kStr.Get_Double(11, 20);
                Y = kStr.Get_Double(21, 30);
                Element_Face_No = kStr.Get_Int(31, 35);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            //1    1         1         0    2                                                        Brick8 Distributed Surface Load

            return string.Format("{0,5}{1,5}{2,10}{3,10}{4,5}",
                Load_No, LT, P, Y, Element_Face_No);
        }

        public static implicit operator Solid_Surface_Loads(string rhs)
        {
            Solid_Surface_Loads c = new Solid_Surface_Loads(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Solid_Surface_Loads rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Solid_Surface_Loads(kString rhs)
        {
            Solid_Surface_Loads c = new Solid_Surface_Loads(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(Solid_Surface_Loads rhs)
        {
            return rhs.ToString();
        }
    }
    public class Solid_Element
    {
        public int Element_No { get; set; }

        public int Node_1 { get; set; }
        public int Node_2 { get; set; }
        public int Node_3 { get; set; }
        public int Node_4 { get; set; }
        public int Node_5 { get; set; }
        public int Node_6 { get; set; }
        public int Node_7 { get; set; }
        public int Node_8 { get; set; }
        public int Integration_Order { get; set; }
        public int Material_No { get; set; }
        public int Generation_Parameter { get; set; }
        public int LSA { get; set; }
        public int LSB { get; set; }
        public int LSC { get; set; }
        public int LSD { get; set; }
        public int Face_Number { get; set; }
        public double Stress_free_Temperature { get; set; }
        public string Ref_Temp { get; set; }

        public Solid_Element()
        {
            Element_No = 0;
            Node_1 = 0;
            Node_2 = 0;
            Node_3 = 0;
            Node_4 = 0;
            Node_5 = 0;
            Node_6 = 0;
            Node_7 = 0;
            Node_8 = 0;
            Integration_Order = 0;
            Material_No = 0;
            Generation_Parameter = 0;
            LSA = 0;
            LSB = 0;
            LSC = 0;
            LSD = 0;
            Face_Number = 0;
            Stress_free_Temperature = 0.0;

            Ref_Temp = "";
        }
        public Solid_Element(kString data)
        {
            Set_Data(data);
        }
        public void Set_Data(kString kStr)
        {
            try
            {
                Element_No = kStr.Get_Int(1, 5);
                Node_1 = kStr.Get_Int(1, 5);
                Node_1 = kStr.Get_Int(6, 10);
                Node_2 = kStr.Get_Int(11, 15);
                Node_3 = kStr.Get_Int(16, 20);
                Node_4 = kStr.Get_Int(21, 25);
                Node_5 = kStr.Get_Int(26, 30);
                Node_6 = kStr.Get_Int(31, 35);
                Node_7 = kStr.Get_Int(36, 40);
                Node_8 = kStr.Get_Int(41, 45);
                Integration_Order = kStr.Get_Int(46, 50);
                Material_No = kStr.Get_Int(51, 55);
                Generation_Parameter = kStr.Get_Int(56, 60);
                LSA = kStr.Get_Int(61, 62);
                LSB = kStr.Get_Int(63, 64);
                LSC = kStr.Get_Int(65, 66);
                LSD = kStr.Get_Int(67, 68);
                Face_Number = kStr.Get_Int(69, 70);
                Stress_free_Temperature = kStr.Get_Double(71, 80);

                Ref_Temp = kStr.Get_String(81);
            }
            catch (Exception ex) { }
        }

        public override string ToString()
        {
            return string.Format("{0,5}{1,5}{2,5}{3,5}{4,5}{5,5}{6,5}{7,5}{8,5}{9,5}{10,5}{11,5}{12,2}{13,2}{14,2}{15,2}{16,2}{17,10:f3}{18}",

                Element_No, 
                Node_1, Node_2, Node_3, Node_4, Node_5, Node_6, Node_7, Node_8,
                
                Integration_Order, Material_No, Generation_Parameter,
                LSA, LSB, LSC, LSD,
                Face_Number,
                Stress_free_Temperature,
                Ref_Temp);
        }

        public static implicit operator Solid_Element(string rhs)
        {
            Solid_Element c = new Solid_Element(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(Solid_Element rhs)
        {
            return rhs.ToString();
        }

        public static implicit operator Solid_Element(kString rhs)
        {
            Solid_Element c = new Solid_Element(rhs); //Internally call Currency constructor
            return c;
        }
        public static implicit operator kString(Solid_Element rhs)
        {
            return rhs.ToString();
        }
    }

}
