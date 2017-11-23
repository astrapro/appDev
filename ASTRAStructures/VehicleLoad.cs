using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


using AstraInterface.DataStructure;


using VectorDraw.Geometry;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;

namespace ASTRAStructures
{

    public class MLoadData
    {

        public MLoadData()
        {
            TypeNoText = "TYPE 6";
            Code = "IRC24RTRACK";
            X = -60.0;
            Y = 0.0d;
            Z = 1.0d;
            XINC = 0.5d;
            LoadWidth = 0d;
        }
        public string TypeNoText { get; set; }
        public int TypeNo
        {
            get
            {
                int i = 0;
                try
                {

                    string str = TypeNoText.ToUpper().Replace("TYPE", "");
                    if (TypeNoText.Length > 4)
                    {
                        return int.Parse(str);
                    }
                }
                catch (Exception ex) { }
                return 0;
            }
        }
        public string Code { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double XINC { get; set; }
        public double LoadWidth { get; set; }
        public MyList Distances { get; set; }
        public MyList LoadValues { get; set; }
        public double ImpactFactor
        {
            get
            {
                switch (TypeNoText)
                {
                    case "TYPE1":
                    case "TYPE 1":
                        return 0;
                        break;
                    case "TYPE2":
                    case "TYPE 2":
                        break;
                    case "TYPE3":
                    case "TYPE 3":
                        break;
                    case "TYPE4":
                    case "TYPE 4":
                        break;
                    case "TYPE5":
                    case "TYPE 5":
                        break;
                    case "TYPE6":
                    case "TYPE 6":
                        break;
                    case "TYPE7":
                    case "TYPE 7":
                        break;
                    case "TYPE8":
                    case "TYPE 8":
                        break;
                    case "TYPE9":
                    case "TYPE 9":
                        break;
                    case "TYPE10":
                    case "TYPE 10":
                        break;
                }
                return 0;
            }
        }

        public static List<MLoadData> GetLiveLoads(string file_path)
        {
            if (!File.Exists(file_path)) return null;

            //TYPE1 IRCCLASSA
            //68 68 68 68 114 114 114 27
            //3.00 3.00 3.00 4.30 1.20 3.20 1.10
            //1.80
            List<MLoadData> LL_list = new List<MLoadData>();
            List<string> file_content = new List<string>(File.ReadAllLines(file_path));
            MyList mlist = null;
            string kStr = "";

            int icount = 0;
            for (int i = 0; i < file_content.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(file_content[i]);
                //kStr = MyList.RemoveAllSpaces(file_content[i].Trim().TrimStart().TrimEnd());
                kStr = kStr.Replace(' ', ',');
                kStr = kStr.Replace(',', ' ');
                kStr = MyList.RemoveAllSpaces(kStr);
                mlist = new MyList(kStr, ' ');

                if (mlist.StringList[0].Contains("TYPE"))
                {
                    MLoadData ld = new MLoadData();
                    if (mlist.Count == 2)
                    {
                        kStr = mlist.StringList[0].Replace("TYPE", "");
                        ld.TypeNoText = "TYPE " + kStr;
                        ld.Code = mlist.StringList[1];
                        LL_list.Add(ld);
                    }
                    else if (mlist.Count == 3)
                    {
                        ld.TypeNoText = "TYPE " + mlist.StringList[1];
                        ld.Code = mlist.StringList[2];
                        LL_list.Add(ld);
                    }
                    if ((i + 3) < file_content.Count)
                        ld.LoadWidth = MyList.StringToDouble(file_content[i + 3], 0.0);

                    try
                    {
                        ld.LoadValues = new MyList(file_content[i + 1], ' ');
                        ld.Distances = new MyList(file_content[i + 2], ' ');
                    }
                    catch (Exception) { }
                }
            }
            return LL_list;

            //TYPE 2 IRCCLASSB
            //20.5 20.5 20.5 20.5 34.0 34.0 8.0 8.0
            //3.00 3.00 3.00 4.30 1.20 3.20 1.10
            //1.80

            //TYPE 3 IRC70RTRACK
            //70 70 70 70 70 70 70 70 70 70
            //0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457
            //0.84

            //TYPE 4 IRC70RWHEEL
            //85 85 85 85 60 60 40
            //1.37 3.05 1.37 2.13 1.52 3.96
            //0.450 1.480 0.450

            //TYPE 5 IRCCLASSAATRACK
            //70 70 70 70 70 70 70 70 70 70
            //0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360
            //0.85

            //TYPE 6 IRC24RTRACK
            //62.5 62.5 62.5 62.5 62.5 62.5 62.5 62.5 62.5 62.5
            //0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366
            //0.36
        }
    }
    public class VehicleLoad
    {
        public double Skew_Angle = 0.0;
        public double Radius = 0.0;

        vdDocument vDoc_elevation = null;
        vdDocument vDoc_Plan = null;

        public VehicleLoad(vdDocument doc_elevation, vdDocument doc_plan, MLoadData ld_data)
        {
            //Skew_Angle = skew_ang;
            Loads = ld_data;
            XINC = 0.2;
            X = 0;
            Y = 0;
            Z = 0.0;
            LoadCase = 0;

            vDoc_elevation = doc_elevation;
            vDoc_Plan = doc_plan;

            WheelAxis = new List<IAxial>();
            //WheelPlan = new List<PlanWheel>();


            //Environment.SetEnvironmentVariable("COMP_RAD", "50");
            string rad = Environment.GetEnvironmentVariable("COMP_RAD");

            if (rad != null)
            {

                if (rad != "")
                {
                    Radius = MyList.StringToDouble(rad, 0.0);
                }
            }

            Set_Wheel_Distance();
            //Axial ax = new Axial(doc_elevation);
            //ax.Width = Width;
            //ax.Load = Loads.LoadValues.StringList[0];
            //ax.WheelRadius = (Loads.LoadWidth == 0.0) ? 0.2d : Loads.LoadWidth / 9.0;

            //ax.Wheel1.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[0];
            //ax.Wheel2.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[0];
            //ax.Axis.ToolTip = Loads.Code + ", Load Width = " + Loads.LoadWidth + ", Distance = " + Loads.Distances.StringList[0];
            //WheelAxis.Add(ax);

            //double last_x = 0.0;
            //Loads.Distances.StringList.Remove("");
            //Loads.LoadValues.StringList.Remove("");
            //for (int i = 0; i < Loads.Distances.Count; i++)
            //{
            //    last_x += Loads.Distances.GetDouble(i);
            //    ax = new Axial(doc_elevation);
            //    ax.X = -last_x;
            //    ax.Width = Width;
            //    ax.Load = Loads.LoadValues.StringList[i];
            //    ax.WheelRadius = (Loads.LoadWidth == 0.0) ? 0.2d : Loads.LoadWidth / 9.0;

            //    ax.Wheel1.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[i] + ", Distance = " + Loads.Distances.StringList[i];
            //    ax.Wheel2.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[i] + ", Distance = " + Loads.Distances.StringList[i];
            //    ax.Axis.ToolTip = Loads.Code + ", Load Width = " + Loads.LoadWidth + ", Distance = " + Loads.Distances.StringList[i];
            //    WheelAxis.Add(ax);
            //}
        }

        public VehicleLoad(vdDocument doc_elevation, vdDocument doc_plan, MLoadData ld_data, double radius)
        {
            //Skew_Angle = skew_ang;
            Loads = ld_data;
            XINC = 0.2;
            X = 0;
            Y = 0;
            Z = 0.0;
            LoadCase = 0;

            vDoc_elevation = doc_elevation;
            vDoc_Plan = doc_plan;

            WheelAxis = new List<IAxial>();
            //WheelPlan = new List<PlanWheel>();


            //Environment.SetEnvironmentVariable("COMP_RAD", "50");

            Radius = radius;

            Set_Wheel_Distance();
            //Axial ax = new Axial(doc_elevation);
            //ax.Width = Width;
            //ax.Load = Loads.LoadValues.StringList[0];
            //ax.WheelRadius = (Loads.LoadWidth == 0.0) ? 0.2d : Loads.LoadWidth / 9.0;

            //ax.Wheel1.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[0];
            //ax.Wheel2.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[0];
            //ax.Axis.ToolTip = Loads.Code + ", Load Width = " + Loads.LoadWidth + ", Distance = " + Loads.Distances.StringList[0];
            //WheelAxis.Add(ax);

            //double last_x = 0.0;
            //Loads.Distances.StringList.Remove("");
            //Loads.LoadValues.StringList.Remove("");
            //for (int i = 0; i < Loads.Distances.Count; i++)
            //{
            //    last_x += Loads.Distances.GetDouble(i);
            //    ax = new Axial(doc_elevation);
            //    ax.X = -last_x;
            //    ax.Width = Width;
            //    ax.Load = Loads.LoadValues.StringList[i];
            //    ax.WheelRadius = (Loads.LoadWidth == 0.0) ? 0.2d : Loads.LoadWidth / 9.0;

            //    ax.Wheel1.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[i] + ", Distance = " + Loads.Distances.StringList[i];
            //    ax.Wheel2.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[i] + ", Distance = " + Loads.Distances.StringList[i];
            //    ax.Axis.ToolTip = Loads.Code + ", Load Width = " + Loads.LoadWidth + ", Distance = " + Loads.Distances.StringList[i];
            //    WheelAxis.Add(ax);
            //}
        }

        public void Set_Wheel_Distance()
        {
            try
            {
                if (Radius != 0.0)
                {
                    Set_Wheel_Distance_Curve();
                    return;
                }


                if (WheelAxis.Count == Loads.Distances.Count + 1)
                {
                    double last_x = 0.0;
                    WheelAxis[0].X = last_x;
                    for (int i = 0; i < Loads.Distances.Count; i++)
                    {
                        last_x += Loads.Distances.GetDouble(i);
                        WheelAxis[i + 1].X = -last_x;
                    }
                }
                else
                {
                    Axial ax = new Axial(vDoc_elevation);
                    ax.Width = Width;
                    ax.Load = Loads.LoadValues.StringList[0];
                    ax.WheelRadius = (Loads.LoadWidth == 0.0) ? 0.2d : Loads.LoadWidth / 9.0;

                    ax.Wheel1.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[0];
                    ax.Wheel2.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[0];
                    ax.Axis.ToolTip = Loads.Code + ", Load Width = " + Loads.LoadWidth + ", Distance = " + Loads.Distances.StringList[0];
                    WheelAxis.Add(ax);

                    double last_x = 0.0;
                    Loads.Distances.StringList.Remove("");
                    Loads.LoadValues.StringList.Remove("");
                    for (int i = 0; i < Loads.Distances.Count; i++)
                    {
                        last_x += Loads.Distances.GetDouble(i);
                        ax = new Axial(vDoc_elevation);
                        ax.X = -last_x;
                        ax.Width = Width;
                        ax.Load = Loads.LoadValues.StringList[i];
                        ax.WheelRadius = (Loads.LoadWidth == 0.0) ? 0.2d : Loads.LoadWidth / 9.0;

                        ax.Wheel1.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[i] + ", Distance = " + Loads.Distances.StringList[i];
                        ax.Wheel2.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[i] + ", Distance = " + Loads.Distances.StringList[i];
                        ax.Axis.ToolTip = Loads.Code + ", Load Width = " + Loads.LoadWidth + ", Distance = " + Loads.Distances.StringList[i];
                        WheelAxis.Add(ax);
                    }
                }
            }
            catch (Exception ex) { }
        }

        public void Update(double xinc)
        {
            foreach (var item in WheelAxis)
            {
                item.Z = Z;
                item.Y = Y + item.Wheel1.Radius;
                if (xinc >= 0.0)
                {
                    item.X += xinc;
                    item.X += Math.Tan(Skew_Angle * (Math.PI / 180.0)) * item.Z;
                    if (item.X > TotalLength)
                        item.X = 0.0;
                }
                else
                {
                    item.X = item.X + xinc;
                    item.X += Math.Tan(Skew_Angle * (Math.PI / 180.0)) * item.Z;
                    if (item.X <= 0)
                        item.X = TotalLength;

                }
                item.Update();
            }
            foreach (var item in WheelPlan)
            {
                item.Z = Z;
                item.Y = Y + item.Wheel1.Radius;
                if (xinc >= 0.0)
                {
                    item.X += xinc;
                    item.X += Math.Tan(Skew_Angle * (Math.PI / 180.0)) * item.Z;
                    if (item.X > TotalLength)
                        item.X = 0.0;
                }
                else
                {
                    item.X = item.X + xinc;
                    item.X += Math.Tan(Skew_Angle * (Math.PI / 180.0)) * item.Z;
                    if (item.X <= 0)
                        item.X = TotalLength;
                }
                item.Update();
            }
            vDoc_elevation.Redraw(true);
            vDoc_Plan.Redraw(true);
        }
        public void Update()
        {

            foreach (var item in WheelAxis)
            {
                item.Z = Z;
                item.Y = Y + item.Wheel1.Radius;

                item.X -= XINC;
                //item.X += XINC;
                item.X += Math.Tan(Skew_Angle * (Math.PI / 180.0)) * item.Z;

                if (item.X > TotalLength)
                    item.X = 0.0;

                item.Wheel1.visibility = (item.Wheel1.Center.x < 0) ? vdFigure.VisibilityEnum.Invisible : vdFigure.VisibilityEnum.Visible;
                item.Wheel2.visibility = item.Wheel1.visibility;
                item.Axis.visibility = item.Wheel1.visibility;
                item.Update();
            }
            //foreach (var item in WheelPlan)
            //{
            //    item.Z = Z;
            //    item.Y = Y + item.Wheel1.Radius;

            //    item.X += XINC;
            //    if (item.X > TotalLength)
            //        item.X = 0.0;

            //    item.Wheel1.visibility = (item.Wheel1.Center.x < 0) ? vdFigure.VisibilityEnum.Invisible : vdFigure.VisibilityEnum.Visible;
            //    item.Wheel2.visibility = item.Wheel1.visibility;
            //    item.Axis.visibility = item.Wheel1.visibility;

            //    item.Update();
            //}
            vDoc_elevation.Redraw(true);
            //vDoc_Plan.Redraw(true);
        }
        public void Update(bool isForward)
        {
            if (Radius > 0)
            {
                //Update Curve Bridge
                Update_Curve(isForward);
                return;
            }


            foreach (var item in WheelAxis)
            {
                item.Z = Z;
                item.Y = Y + item.Wheel1.Radius;



                if (isForward)
                    item.X += XINC;
                else
                    item.X -= XINC;


                item.X += Math.Tan(Skew_Angle * (Math.PI / 180.0)) * item.Z;

                if (item.X > TotalLength)
                    item.X = 0.0;


                //_x = item.X;
                //_z = item.Z;

                //if (Radius > 0)
                //{
                //    item.X = Radius * Math.Cos(_x / Radius);
                //    item.Z = Radius - Radius * Math.Sin(_z / Radius);
                //}

                item.Wheel1.visibility = (item.Wheel1.Center.x < 0) ? vdFigure.VisibilityEnum.Invisible : vdFigure.VisibilityEnum.Visible;
                item.Wheel2.visibility = item.Wheel1.visibility;
                item.Axis.visibility = item.Wheel1.visibility;
                item.Update();


                //item.X = _x;
                //item.Z = _z;
            }
            //foreach (var item in WheelPlan)
            //{
            //    item.Z = Z;
            //    item.Y = Y + item.Wheel1.Radius;

            //    item.X += XINC;
            //    if (item.X > TotalLength)
            //        item.X = 0.0;

            //    item.Wheel1.visibility = (item.Wheel1.Center.x < 0) ? vdFigure.VisibilityEnum.Invisible : vdFigure.VisibilityEnum.Visible;
            //    item.Wheel2.visibility = item.Wheel1.visibility;
            //    item.Axis.visibility = item.Wheel1.visibility;

            //    item.Update();
            //}
            vDoc_elevation.Redraw(true);
            //vDoc_Plan.Redraw(true);
        }

        double _xxs = 0.0;
        double _zzs = 0.0;
        public void Set_Wheel_Distance_Curve()
        {
            try
            {
                if (WheelAxis.Count == Loads.Distances.Count + 1)
                {
                    double last_x = 0.0;
                    WheelAxis[0].X = last_x;
                    for (int i = 0; i < Loads.Distances.Count; i++)
                    {
                        last_x += Loads.Distances.GetDouble(i);
                        WheelAxis[i + 1].X = -last_x;
                    }
                }
                else
                {
                    CurveAxial ax = new CurveAxial(vDoc_elevation);

                    if (Radius > 0) ax.CurveRadius = Radius;

                    ax.Width = Width;
                    ax.Load = Loads.LoadValues.StringList[0];
                    ax.WheelRadius = (Loads.LoadWidth == 0.0) ? 0.2d : Loads.LoadWidth / 9.0;

                    ax.Wheel1.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[0];
                    ax.Wheel2.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[0];
                    ax.Axis.ToolTip = Loads.Code + ", Load Width = " + Loads.LoadWidth + ", Distance = " + Loads.Distances.StringList[0];
                    WheelAxis.Add(ax);

                    double last_x = 0.0;
                    Loads.Distances.StringList.Remove("");
                    Loads.LoadValues.StringList.Remove("");
                    for (int i = 0; i < Loads.Distances.Count; i++)
                    {
                        last_x += Loads.Distances.GetDouble(i);
                        ax = new CurveAxial(vDoc_elevation);
                        if (Radius > 0) ax.CurveRadius = Radius;
                        ax.Width = Width;
                        ax.X = -last_x;
                        ax.Load = Loads.LoadValues.StringList[i];
                        ax.WheelRadius = (Loads.LoadWidth == 0.0) ? 0.2d : Loads.LoadWidth / 9.0;

                        ax.Wheel1.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[i] + ", Distance = " + Loads.Distances.StringList[i];
                        ax.Wheel2.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[i] + ", Distance = " + Loads.Distances.StringList[i];
                        ax.Axis.ToolTip = Loads.Code + ", Load Width = " + Loads.LoadWidth + ", Distance = " + Loads.Distances.StringList[i];
                        WheelAxis.Add(ax);
                    }
                }
            }
            catch (Exception ex) { }
        }

        public void Update_Curve(bool isForward)
        {
            //double Radius = 50;
            double _x, _z;

            _x = _z = 0.0;

            double ang_incr = 0.0;

            //ang_incr = list_x[iCols] / Radius;
            //var _r = Radius - list_z[iRows];
            //list.Add(_r * Math.Cos(ang_incr));
            //lst_x.Add(_r * Math.Sin(ang_incr));


            foreach (var item in WheelAxis)
            {
                item.Z = Z;
                item.Y = Y + item.Wheel1.Radius;

                var _r = Radius - item.Z;


                if (isForward)
                    item.X += XINC;
                else
                    item.X -= XINC;


                //item.X += Math.Tan(Skew_Angle * (Math.PI / 180.0)) * item.Z;

                //if (item.X > TotalLength)
                //    item.X = 0.0;


                //_x = item.X;
                //_z = item.Z;

                //if (Radius > 0)
                //{
                //    item.X = Radius * Math.Cos(_x / Radius);
                //    item.Z = Radius - Radius * Math.Sin(_z / Radius);
                //}

                item.Wheel1.visibility = (item.Wheel1.Center.x < 0) ? vdFigure.VisibilityEnum.Invisible : vdFigure.VisibilityEnum.Visible;
                item.Wheel2.visibility = item.Wheel1.visibility;
                item.Axis.visibility = item.Wheel1.visibility;
                item.Update();


                //item.X = _x;
                //item.Z = _z;
            }
            //foreach (var item in WheelPlan)
            //{
            //    item.Z = Z;
            //    item.Y = Y + item.Wheel1.Radius;

            //    item.X += XINC;
            //    if (item.X > TotalLength)
            //        item.X = 0.0;

            //    item.Wheel1.visibility = (item.Wheel1.Center.x < 0) ? vdFigure.VisibilityEnum.Invisible : vdFigure.VisibilityEnum.Visible;
            //    item.Wheel2.visibility = item.Wheel1.visibility;
            //    item.Axis.visibility = item.Wheel1.visibility;

            //    item.Update();
            //}
            vDoc_elevation.Redraw(true);
            //vDoc_Plan.Redraw(true);
        }

        public double TotalLength { get; set; }
        public double XINC { get; set; }
        public double Width { get { return Loads.LoadWidth; } }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int LoadCase { get; set; }
        public List<IAxial> WheelAxis { get; set; }
        public List<PlanWheel> WheelPlan { get; set; }
        public MLoadData Loads { get; set; }
        ~VehicleLoad()
        {

            Loads = null;
            WheelAxis = null;
            WheelPlan = null;
        }
    }

    public class Axial : IAxial
    {
        vdDocument vDoc = null;
        public Axial(vdDocument vdoc)
        {
            //X = Y = Z = 0.0;
            vDoc = vdoc;

            Wheel1 = new vdCircle();
            Wheel1.SetUnRegisterDocument(vdoc);
            Wheel1.setDocumentDefaults();
            Wheel1.Radius = WheelRadius;
            vdoc.ActiveLayOut.Entities.AddItem(Wheel1);

            Wheel2 = new vdCircle();
            Wheel2.SetUnRegisterDocument(vdoc);
            Wheel2.setDocumentDefaults();
            Wheel2.Radius = WheelRadius;
            vdoc.ActiveLayOut.Entities.AddItem(Wheel2);

            Axis = new vdLine();
            Axis.SetUnRegisterDocument(vdoc);
            Axis.setDocumentDefaults();
            Axis.StartPoint = Wheel1.Center;
            Axis.EndPoint = Wheel2.Center;
            vdoc.ActiveLayOut.Entities.AddItem(Axis);

            WheelRadius = (Width == 0.0) ? 0.2d : Width / 10.0;
        }
        public Axial(vdDocument vdoc, string loads)
        {
            //X = Y = Z = 0.0;
            vDoc = vdoc;

            Wheel1 = new vdCircle();
            Wheel1.SetUnRegisterDocument(vdoc);
            Wheel1.setDocumentDefaults();
            Wheel1.Radius = WheelRadius;
            vdoc.ActiveLayOut.Entities.AddItem(Wheel1);

            Wheel2 = new vdCircle();
            Wheel2.SetUnRegisterDocument(vdoc);
            Wheel2.setDocumentDefaults();
            Wheel2.Radius = WheelRadius;
            vdoc.ActiveLayOut.Entities.AddItem(Wheel2);

            Axis = new vdLine();
            Axis.SetUnRegisterDocument(vdoc);
            Axis.setDocumentDefaults();
            Axis.StartPoint = Wheel1.Center;
            Axis.EndPoint = Wheel2.Center;
            vdoc.ActiveLayOut.Entities.AddItem(Axis);


            WheelRadius = 0.3d;
            Load = (new MyList(loads, ' ')).StringList[0];
        }
        public double X
        {
            get
            {
                return Wheel1.Center.x;
            }
            set
            {
                Wheel1.Center.x = value;
                Wheel2.Center.x = value;
            }
        }
        public double Y
        {

            get
            {
                return Wheel1.Center.y;
            }
            set
            {
                Wheel1.Center.y = value;
                Wheel2.Center.y = value;
            }
        }
        public double Z
        {
            get
            {
                return Wheel1.Center.z;
            }
            set
            {
                Wheel1.Center.z = value;
                Wheel2.Center.z = value + Width;
            }
        }
        public double Width { get; set; }

        public vdCircle Wheel1 { get; set; }
        public vdCircle Wheel2 { get; set; }
        public vdLine Axis { get; set; }
        public double WheelRadius
        {
            get
            {
                try
                {
                    return Wheel1.Radius;
                }
                catch (Exception ex) { }
                return 0.2;
            }
            set
            {
                try
                {
                    Wheel1.Radius = value;
                    Wheel2.Radius = value;
                }
                catch (Exception ex) { }
            }
        }
        public string Load
        {
            get
            {
                try
                {
                    return Axis.ToolTip;
                }
                catch (Exception ex) { }
                return "";
            }
            set
            {
                try
                {
                    Axis.ToolTip = "Axial Load = " + value + ", Load Width = " + Width;
                }
                catch (Exception ex) { }
            }
        }

        public void Update()
        {
            Wheel1.Radius = WheelRadius;
            Wheel2.Radius = WheelRadius;

            Axis.StartPoint = Wheel1.Center;
            Axis.EndPoint = Wheel2.Center;

            Wheel1.Update();
            Wheel2.Update();
            Axis.Update();
        }
        ~Axial()
        {
            try
            {
                Wheel1.Deleted = true;
                Wheel2.Deleted = true;
                Axis.Deleted = true;
                Wheel1.Dispose();
                Wheel2.Dispose();
                Axis.Dispose();
            }
            catch (Exception ex) { }
        }

    }
    public class PlanWheel
    {
        vdDocument vDoc = null;
        public PlanWheel(vdDocument vdoc)
        {
            //X = Y = Z = 0.0;
            WheelRadius = 0.3d;
            vDoc = vdoc;

            Wheel1 = new vdCircle();
            Wheel1.SetUnRegisterDocument(vdoc);
            Wheel1.setDocumentDefaults();
            Wheel1.Radius = WheelRadius;
            Wheel1.ExtrusionVector = new Vector(0, 1, 0);
            vdoc.ActiveLayOut.Entities.AddItem(Wheel1);

            Wheel2 = new vdCircle();
            Wheel2.SetUnRegisterDocument(vdoc);
            Wheel2.setDocumentDefaults();
            Wheel2.Radius = WheelRadius;
            Wheel2.ExtrusionVector = new Vector(0, 1, 0);
            vdoc.ActiveLayOut.Entities.AddItem(Wheel2);

            Axis = new vdLine();
            Axis.SetUnRegisterDocument(vdoc);
            Axis.setDocumentDefaults();
            Axis.StartPoint = Wheel1.Center;
            Axis.EndPoint = Wheel2.Center;
            vdoc.ActiveLayOut.Entities.AddItem(Axis);
        }
        public double X
        {
            get
            {
                return Wheel1.Center.x;
            }
            set
            {
                Wheel1.Center.x = value;
                Wheel2.Center.x = value;
            }
        }
        public double Y
        {

            get
            {
                return Wheel1.Center.y;
            }
            set
            {
                Wheel1.Center.y = value;
                Wheel2.Center.y = value;
            }
        }
        public double Z
        {

            get
            {
                return Wheel1.Center.z;
            }
            set
            {
                Wheel1.Center.z = value;
                Wheel2.Center.z = value + Width;
            }
        }
        public double Width { get; set; }

        public vdCircle Wheel1 { get; set; }
        public vdCircle Wheel2 { get; set; }
        public vdLine Axis { get; set; }
        public double WheelRadius { get; set; }


        public void Update()
        {
            Axis.StartPoint = Wheel1.Center;
            Axis.EndPoint = Wheel2.Center;
            Wheel1.Radius = WheelRadius;
            Wheel2.Radius = WheelRadius;
            Wheel1.Update();
            Wheel2.Update();
            Axis.Update();
        }
        ~PlanWheel()
        {
            try
            {
                Wheel1.Deleted = true;
                Wheel2.Deleted = true;
                Axis.Deleted = true;
                Wheel1.Dispose();
                Wheel2.Dispose();
                Axis.Dispose();
            }
            catch (Exception ex) { }
        }
    }
    public class CurveAxial : IAxial
    {
        vdDocument vDoc = null;
        public CurveAxial(vdDocument vdoc)
        {
            //X = Y = Z = 0.0;
            vDoc = vdoc;

            Wheel1 = new vdCircle();
            Wheel1.SetUnRegisterDocument(vdoc);
            Wheel1.setDocumentDefaults();
            Wheel1.Radius = WheelRadius;
            vdoc.ActiveLayOut.Entities.AddItem(Wheel1);

            Wheel2 = new vdCircle();
            Wheel2.SetUnRegisterDocument(vdoc);
            Wheel2.setDocumentDefaults();
            Wheel2.Radius = WheelRadius;
            vdoc.ActiveLayOut.Entities.AddItem(Wheel2);

            Axis = new vdLine();
            Axis.SetUnRegisterDocument(vdoc);
            Axis.setDocumentDefaults();
            Axis.StartPoint = Wheel1.Center;
            Axis.EndPoint = Wheel2.Center;
            vdoc.ActiveLayOut.Entities.AddItem(Axis);

            WheelRadius = (Width == 0.0) ? 0.2d : Width / 10.0;

            _x = _y = _z = CurveRadius = 0.0;
        }
        public CurveAxial(vdDocument vdoc, string loads)
        {
            //X = Y = Z = 0.0;
            vDoc = vdoc;

            Wheel1 = new vdCircle();
            Wheel1.SetUnRegisterDocument(vdoc);
            Wheel1.setDocumentDefaults();
            Wheel1.Radius = WheelRadius;
            vdoc.ActiveLayOut.Entities.AddItem(Wheel1);

            Wheel2 = new vdCircle();
            Wheel2.SetUnRegisterDocument(vdoc);
            Wheel2.setDocumentDefaults();
            Wheel2.Radius = WheelRadius;
            vdoc.ActiveLayOut.Entities.AddItem(Wheel2);

            Axis = new vdLine();
            Axis.SetUnRegisterDocument(vdoc);
            Axis.setDocumentDefaults();
            Axis.StartPoint = Wheel1.Center;
            Axis.EndPoint = Wheel2.Center;
            vdoc.ActiveLayOut.Entities.AddItem(Axis);


            WheelRadius = 0.3d;
            Load = (new MyList(loads, ' ')).StringList[0];


            _x = _y = _z = CurveRadius = 0.0;

        }
        double _x, _y, _z;

        public double CurveRadius { get; set; }
        gPoint lastPoint;
        public void SetCenter()
        {
            if (CurveRadius == 0.0)
            {

                Wheel1.Center.x = _x;
                Wheel2.Center.x = _x;

                Wheel1.Center.z = _z;
                Wheel2.Center.z = _z;
                return;
            }
            //CurveRadius = 50;
            var _r = CurveRadius - _z;

            double ang_incr = _x / CurveRadius;

            //list.Add(_r * Math.Cos(ang_incr));
            //lst_x.Add(_r * Math.Sin(ang_incr));


            Wheel1.Center.x = _r * Math.Sin(ang_incr);

            _r = CurveRadius - _z - Width;

            Wheel2.Center.x = _r * Math.Sin(ang_incr);


            //list.Add(_r * Math.Cos(ang_incr));
            //lst_x.Add(_r * Math.Sin(ang_incr));

            _r = CurveRadius - _z;

            Wheel1.Center.z = CurveRadius - _r * Math.Cos(ang_incr);

            _r = CurveRadius - _z - Width;

            Wheel2.Center.z = CurveRadius - _r * Math.Cos(ang_incr);

            if (lastPoint != null)
            {
                //Wheel1.ExtrusionVector = lastPoint.Direction(Wheel1.Center);
                //Wheel2.ExtrusionVector = lastPoint.Direction(Wheel1.Center);


                Wheel1.ExtrusionVector = (Wheel1.ExtrusionVector - Wheel1.Center.Direction(lastPoint));
                Wheel2.ExtrusionVector = Wheel1.ExtrusionVector;
            }
            lastPoint = new gPoint(Wheel1.Center);

        }
        public double X
        {
            get
            {
                //return Wheel1.Center.x;
                return _x;
            }
            set
            {
                //Wheel1.Center.x = value;
                //Wheel2.Center.x = value;
                _x = value;

                SetCenter();

            }
        }
        public double Y
        {
            get
            {
                return Wheel1.Center.y;
            }
            set
            {
                Wheel1.Center.y = value;
                Wheel2.Center.y = value;
            }
        }
        public double Z
        {
            get
            {
                return _z;
                //return Wheel1.Center.z;
            }
            set
            {
                //Wheel1.Center.z = value;
                //Wheel2.Center.z = value + Width;
                _z = value;
                SetCenter();
            }
        }
        public double Width { get; set; }

        public vdCircle Wheel1 { get; set; }
        public vdCircle Wheel2 { get; set; }
        public vdLine Axis { get; set; }
        public double WheelRadius
        {
            get
            {
                try
                {
                    return Wheel1.Radius;
                }
                catch (Exception ex) { }
                return 0.2;
            }
            set
            {
                try
                {
                    Wheel1.Radius = value;
                    Wheel2.Radius = value;
                }
                catch (Exception ex) { }
            }
        }
        public string Load
        {
            get
            {
                try
                {
                    return Axis.ToolTip;
                }
                catch (Exception ex) { }
                return "";
            }
            set
            {
                try
                {
                    Axis.ToolTip = "Axial Load = " + value + ", Load Width = " + Width;
                }
                catch (Exception ex) { }
            }
        }

        public void Update()
        {
            Wheel1.Radius = WheelRadius;
            Wheel2.Radius = WheelRadius;

            Axis.StartPoint = Wheel1.Center;
            Axis.EndPoint = Wheel2.Center;

            Wheel1.Update();
            Wheel2.Update();
            Axis.Update();
        }

        ~CurveAxial()
        {
            try
            {
                Wheel1.Deleted = true;
                Wheel2.Deleted = true;
                Axis.Deleted = true;
                Wheel1.Dispose();
                Wheel2.Dispose();
                Axis.Dispose();
            }
            catch (Exception ex) { }
        }
    }
    public interface IAxial
    {
        VectorDraw.Professional.vdFigures.vdLine Axis { get; set; }
        string Load { get; set; }
        void Update();
        VectorDraw.Professional.vdFigures.vdCircle Wheel1 { get; set; }
        VectorDraw.Professional.vdFigures.vdCircle Wheel2 { get; set; }
        double WheelRadius { get; set; }
        double Width { get; set; }
        double X { get; set; }
        double Y { get; set; }
        double Z { get; set; }
    }

}
