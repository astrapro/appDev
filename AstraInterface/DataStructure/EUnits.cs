using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.DataStructure
{
    public enum eLengthUnits
    {
        METRES = 0,
        CM,
        MM,
        YDS,
        FT,
        INCH
    }
    public enum EMassUnits
    {
        KG = 0,
        KN,
        MTON,
        NEW,
        GMS,
        LBS,
        KIP
    }
    public enum eDesignStandard
    {
        Unknown = -1,
        IndianStandard = 0,
        BritishStandard = 1,
        LRFDStandard = 2,
    }
}
