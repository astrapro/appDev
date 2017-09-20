//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace AstraInterface.Interface
//{
//    public interface ISLAB01
//    {

//    }
//}
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
namespace AstraInterface.Interface
{
    
    public interface ISLAB02
    {
        double L { get; set; }
        double B { get; set; }
        double LL { get; set; }
        double Sigma_ck { get; set; }
        double Slab_Load { get; set; }
        double Sigma_y { get; set; }
        double Alpha { get; set; }
        double Beta { get; set; }
        double Gamma { get; set; }
        double Delta { get; set; }
        double Lamda { get; set; }
        double D1 { get; set; }
        double D2 { get; set; }
        double H1 { get; set; }
        double H2 { get; set; }
        double Ads { get; set; }
        double Tc { get; set; }
        double WallThickness { get; set; }
    }
    public interface ISLAB03
    {
        string LengthString { get; set; }
        List<double> Lengths { get; set; }
        double Perpendicular_Length { get; set; }
        double AssumeSlabThickness { get; set; }
        double SlabLoad { get; set; }
        double FinishThickness { get; set; }
        double FinishLoad { get; set; }
        double FixedLoadfactor { get; set; }
        double ImposedLoad { get; set; }
        double ImposedLoadFactor { get; set; }
        double DiaMainReinforcement { get; set; }
        double DiaDistributionReinforcement { get; set; }
        double PercentageDistReinforcement { get; set; }
        double ConcreteGrade { get; set; }
        double SteelGrade { get; set; }
        double Alpha { get; set; }
        double Beta { get; set; }
        double Gamma { get; set; }
        double Delta { get; set; }
        double Lamda { get; set; }
        double WallThickness { get; set; }
        double ClearCover { get; set; }
        bool Calculate(string fileName);
    }
   
}
