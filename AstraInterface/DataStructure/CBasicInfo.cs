using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.DataStructure
{
    public class CBasicInfo
    {
        public StructureType Type;
        public string UsersTitle;
        public short RunningOption;
        public eLengthUnits LengthUnit;
        public EMassUnits MassUnit;

        public CBasicInfo()
        {
            Type = StructureType.SPACE;
            UsersTitle = "";
            RunningOption = 0;
            LengthUnit = eLengthUnits.METRES;
            MassUnit = EMassUnits.KG;
        }
        public void Clear()
        {
            Type = StructureType.SPACE;
            UsersTitle = "";
            RunningOption = 0;
            LengthUnit = eLengthUnits.METRES;
            MassUnit = EMassUnits.KG;
        }
    }
}
