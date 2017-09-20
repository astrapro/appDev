using System;
using System.Collections.Generic;
using System.IO;

using AstraInterface.Interface;
namespace BridgeAnalysisDesign.BearingDesign
{
    public class Elastomeric_Bearing
    {
        public Elastomeric_Bearing()
        {
            Is_Standard = true;
        }
        public bool Is_Standard { get; set; }
        public void Calculate()
        {
            double Intro_G23 = 15.52;
            double Intro_G24 = 15.50;
            double Intro_G25 = 14.70;


            List<string> list = new List<string>();



            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("The longitudinal members having the maximum bending moments is selected "));
            list.Add(string.Format("for the design and proven to be safe for all the loads envisaged."));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Span arrangement"));
            list.Add(string.Format("-----------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(" Center to Center of expansion joints = {0:f3} m", Intro_G23));
            list.Add(string.Format(" Total length of Precast Girder = {0:f3} m", Intro_G24));
            list.Add(string.Format(" Center to Center of Bearings (Effective Span) = {0:f3} m", Intro_G25));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Spring constant calculation"));
            list.Add(string.Format("----------------------------"));
            list.Add(string.Format(""));
            if (Is_Standard) list.Add(string.Format("( Ref : Concrete Bridge Practice - Analysis , Design , by V K RAINA, Page No 183)"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            double _l0 = 500;
            double _b0 = 320;
            double _hi = 12;
            double _he = 4;
            double _hs = 4;
            double _n = 4;
            double _c = 6;
            
            list.Add(string.Format("Assumed bearing size ( l0 x b0 )"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));


            list.Add(string.Format("Assumed bearing size ( l0 x b0 ) = 500 x 320 mm "));
            list.Add(string.Format(""));
            list.Add(string.Format("Thickness of individual internal layer of elastomer (hi) = 12 mm "));
            list.Add(string.Format("Thickness of bottom/top outer layer of elastomer (he) = 6 mm (Should not be less than hi/2) "));
            list.Add(string.Format("Thickness steel laminate (hs) = 4 mm "));
            list.Add(string.Format("Total number of internal elastomer layer (n) = 4 No's "));
            list.Add(string.Format("Total number of steel laminates = 5 No's "));
            list.Add(string.Format("Side cover ( c ) = 6 mm "));
            list.Add(string.Format("Effective length of the bearing ( l = l0-2c) = 488 mm "));
            list.Add(string.Format("Effective width of the bearing ( b = b0-2c) = 308 mm "));
            list.Add(string.Format("Effective plan area excluding any holes( Ae = l x b) = 150304 mm^2 "));
            list.Add(string.Format(""));



            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
        }
    }
}
