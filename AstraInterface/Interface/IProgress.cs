using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.Interface
{

    public interface IProgress
    {
        int Progress_Value { get; set; }
        double Progress_Value_A { get; set; }
        double Progress_Value_B { get; set; }

        bool Close_Flag { get; set; }
    }
}
