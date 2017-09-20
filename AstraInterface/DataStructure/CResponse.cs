using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.DataStructure
{
    public class CResponse
    {
        public int TotalFrequencies;
        public double CutOffFrequencies;
        public double X, Y, Z;
        public int SpectrumPoints;
        public double ScaleFactor;
        public enum SpectrumType
        {
            Acceleration = 0,
            Displacement,

        }
        public List<double> Periods;
        public List<double> Acceleration;
        public List<double> Displacement;
        public SpectrumType Type;
        public bool IsDefault;
        public CResponse()
        {
            TotalFrequencies = 0;
            CutOffFrequencies = 0.0;
            X = 0.0;
            Y = 0.0;
            Z = 0.0;
            SpectrumPoints = 0;
            ScaleFactor = 0.0;
            Type = SpectrumType.Acceleration;
            Periods = new List<double>();
            Acceleration = new List<double>();
            Displacement = new List<double>();
            IsDefault = true;
        }
        public void Clear()
        {
            IsDefault = true;
            Periods.Clear();
            Acceleration.Clear();
            Displacement.Clear();
        }
        public string DisplacementText
        {
            get
            {
                string str = "";
                for (int i = 0; i < Displacement.Count; i++)
                {
                    str += Displacement[i] + ",";
                }
                return str.Substring(0, str.Length - 1);
            }
            set
            {
                string kStr = value.Replace(' ', ',');
                string[] values = kStr.Split(new char[] { ',' });
                Displacement.Clear();
                for (int i = 0; i < values.Length; i++)
                {
                    try
                    {
                        Displacement.Add(double.Parse(values[i]));
                    }
                    catch
                    {
                    }
                }
            }
        }
        public string AccelerationText
        {
            get
            {
                string str = "";
                for (int i = 0; i < Acceleration.Count; i++)
                {
                    str += Acceleration[i] + ",";
                }
                return str.Substring(0, str.Length - 1);
            }
            set
            {
                string kStr = value.Replace(' ', ',');
                string[] values = kStr.Split(new char[] { ',' });
                Acceleration.Clear();
                for (int i = 0; i < values.Length; i++)
                {
                    try
                    {
                        Acceleration.Add(double.Parse(values[i]));
                    }
                    catch
                    {
                    }
                }
            }
        }
        public string PeriodsText
        {
            get
            {
                string str = "";
                for (int i = 0; i < Periods.Count; i++)
                {
                    str += Periods[i] + ","; 
                }
                return str.Substring(0, str.Length - 1);
            }
            set
            {
                string kStr = value.Replace(' ', ',');
                string[] values = kStr.Split(new char[] { ',' });
                Periods.Clear();
                for (int i = 0; i < values.Length; i++)
                {
                    try
                    {
                        Periods.Add(double.Parse(values[i]));
                    }
                    catch
                    {
                    }
                }
            }
        }
        public void WriteToStream(System.IO.StreamWriter sw)
        {
            sw.WriteLine("N101    0    0    0     0.0     " + this.CutOffFrequencies);
            sw.WriteLine("N102 {0,10:f1}{1,10:f1}{2,10:f1}{3,5}", X, Y, Z, 1);
            if (Type == SpectrumType.Acceleration)
            {
                sw.WriteLine("N103   ACCELERATION SPECTRUM");
                sw.WriteLine("N104 {0,5}{1,10:f1}", SpectrumPoints, ScaleFactor);
                for (int i = 0; i < Periods.Count; i++)
                {
                    sw.WriteLine("N105 {0,10:f1}{1,10:f1}", Periods[i], Acceleration[i]);
                }
            }
            else if (Type == SpectrumType.Displacement)
            {
                sw.WriteLine("N103   DISPLACEMENT SPECTRUM");
                sw.WriteLine("N104 {0,5}{1,10:f1}", SpectrumPoints, ScaleFactor);
                for (int i = 0; i < Periods.Count; i++)
                {
                    sw.WriteLine("N105 {0,10:f1}{1,10:f1}", Periods[i], Displacement[i]);
                }
            }
        }
        public void ReadFromStream(System.IO.StreamReader sr)
        {
            string kStr = "";
            string findId = "";
            while (sr.EndOfStream == false)
            {
                try
                {
                    kStr = sr.ReadLine();
                    findId = GetID(kStr);
                    if (findId == "N101")
                    {
                        CutOffFrequencies = double.Parse(GetValues(kStr, ' ')[5]);
                    }
                    else if (findId == "N102")
                    {
                        X = double.Parse(GetValues(kStr, ' ')[1]);
                        Y = double.Parse(GetValues(kStr, ' ')[2]);
                        Z = double.Parse(GetValues(kStr, ' ')[3]);
                    }
                    else if (findId == "N103")
                    {
                        if (GetValues(kStr, ' ')[1] == "ACCELERATION")
                        {
                            Type = SpectrumType.Acceleration;
                        }
                        else if (GetValues(kStr, ' ')[1] == "DISPLACEMENT")
                        {
                            Type = SpectrumType.Displacement;
                        }
                    }
                    else if (findId == "N104")
                    {
                        SpectrumPoints = int.Parse(GetValues(kStr, ' ')[1]);
                        ScaleFactor = double.Parse(GetValues(kStr, ' ')[2]);
                    }
                    else if (findId == "N105")
                    {
                        double prd, acc, disp;
                        if (Type == SpectrumType.Acceleration)
                        {
                            prd = acc = 0.0;
                            prd = double.Parse(GetValues(kStr, ' ')[1]);
                            acc = double.Parse(GetValues(kStr, ' ')[2]);
                            Periods.Add(prd);
                            Acceleration.Add(acc);
                        }
                        else if (Type == SpectrumType.Displacement)
                        {
                            prd = disp = 0.0;
                            prd = double.Parse(GetValues(kStr, ' ')[1]);
                            disp = double.Parse(GetValues(kStr, ' ')[2]);
                            Periods.Add(prd);
                            Displacement.Add(disp);
                        }
                    }
                }
                catch (Exception ex) { }
            }
            

        }
        private string GetID(string data)
        {
            return data.Substring(0, 4);
        }
        private string[] GetValues(string data, char splitBy)
        {
            //data = data.Replace('\t', splitBy);
            //data = data.Replace(' ', splitBy);
            while (data.IndexOf("  ") != -1)
            {
                data = data.Replace("  ", " ");
            }
            string[] values = data.Split(new char[] { ' ' });
            return values;
        }
    }
}
