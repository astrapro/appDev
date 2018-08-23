namespace AstraInterface.DataStructure
{
    public enum eASTRADesignType
    {
        NOT_DEFINED = -1,
        RCC_T_Girder_Bridge_LS = 1,
        RCC_T_Girder_Bridge_WS = 2,
        Composite_Bridge_LS = 3,
        Composite_Bridge_WS = 4,
        PSC_I_Girder_Bridge_LS = 5,
        PSC_I_Girder_Bridge_WS_Long_Span = 6,
        PSC_I_Girder_Bridge_WS_Short_Span = 7,
        PSC_Box_Girder_Bridge_WS = 8,
        Continuous_PSC_Box_Girder_Bridge_WS = 9,
        Steel_Truss_Bridge_Warren_1 = 10,
        Steel_Truss_Bridge_Warren_2 = 11,
        Steel_Truss_Bridge_Warren_3 = 12,
        Steel_Truss_Bridge_K_Type = 13,
        Arch_Cable_Suspension_Bridge = 14,
        Cable_Suspension_Bridge = 15,
        Cable_Stayed_Bridge = 16,
        Railway_Steel_Plate_Girder_Bridge = 17,
        ROB_Underpasses = 18,
        RCC_Minor_Bridge_WS = 19,

        RCC_Box_Culvert_Single_Cell = 20,
        RCC_Box_Culvert_Multi_Cell = 21,
        RCC_Slab_Culvert = 22,
        RCC_Pipe_Culvert = 23,
        Abutment_Counterfort = 24,
        Abutment_Cantilever = 25,
        RCC_Pier = 26,
        RCC_Stone_Masonry_Pier = 27,
        RCC_Pier_with_Piles = 28,
        RE_Wall = 29,
        MONO_AXIAL_Bearing_Trans = 30,
        MONO_AXIAL_Bearing_Long = 31,
        BI_AXIAL_Bearing_Long = 32,
        Fixed_Bearing_Long = 33,
        Foundation_Well = 34,
        Foundation_Pile = 35,
        Hydrolic_Calculation = 36,
        Cost_Estimation = 37,

        RCC_Minor_Bridge_LS = 38,
        Extradossed_Side_Towers_Bridge_LS = 39,
        Extradossed_Central_Towers_Bridge_LS = 40,

        RCC_Box_Pushed_Underpass = 41,

        Combined_Footing = 42,
        Raft_Foundation = 43,

        Transmission_Tower = 44,
        Microwave_Tower = 45,
        CableCar_Tower = 46,
        Structure_Modeling = 47,

        RCC_Pier_Limit_State = 48,

        Eigen_Value_Analysis = 49,
        Time_History_Analysis = 50,
        Response_Spectrum_Analysis = 51,
        P_Delta_Analysis = 52,
        Stream_Hydrology = 53,


        BOX_CULVERT_LSM = 54,


        Structural_Analysis = 55,
        Structural_Analysis_SAP = 56,

        Steel_Beam = 57,
        Steel_Column = 58,
        Stage_Analysis = 59,

        Retaining_Wall = 60,
    }

    public enum eSLAB
    {
        SLAB01 = 1,
        ONE_WAY_RCC_SLAB = 2,
        ONE_WAY_CONTINUOUS_RCC_SLAB = 3,
        TWO_WAY_RCC_SLAB = 4
    }

    public enum eSLAB_Part
    {
        VIEW = 0,
        DRAWING = 1,
        BoQ = 2
    }

    public enum eASTRAImage
    {
        Steel_Truss_Bridge = 0,
        Composite_Bridge = 1,
        PSC_I_Girder_Short_Span = 2,
        RCC_T_Beam_Bridge = 3,
        Rail_Plate_Girder = 4,
        PSC_Box_Girder = 5,
        PSC_I_Girder_Long_Span = 6,
        Cable_Stayed_Bridge = 7, //Chiranjit [2011 11 11]
        RE_Wall = 8, //Chiranjit [2013 04 18]
        Continuous_PSC_Box_Girder = 9, //Chiranjit [2013 07 23]

        Steel_Truss_Bridge_K_type = 10, //Chiranjit [2013 11 18]
        RCC_Culverts = 11, //Chiranjit [2013 11 18]

        Arch_Cable_Suspension = 12, //Chiranjit [2014 01 16]

        Cable_Suspension = 13, //Chiranjit [2014 01 16]
        Extradosed = 14, //Chiranjit [2014 01 16]
        Extradosed_SideTowers = 15, //Chiranjit [2014 01 16]
        Extradosed_CentralTowers = 16, //Chiranjit [2014 01 16]


        Jetty = 17, //Chiranjit [2017 03 08]

        Transmission_Tower = 18, //Chiranjit [2017 05 28]
        Cable_Car_Tower = 19, //Chiranjit [2017 05 28]
        Microwave_Tower = 20, //Chiranjit [2017 05 28]
        Transmission_Tower_3_Cables = 21, //Chiranjit [2017 05 28]

    }

    public enum eDrawingsType
    {
        RCC_T_Girder = 0,
        RCC_BOX_Culvert = 1
    }

    public enum eVersionType
    {
        Demo = 0,
        Activation_Trial = 1,

        Professional_Bridge = 11,
        Enterprise_Bridge = 31,

        Professional_Structure = 12,
        Enterprise_Structure = 32,
    }

    public enum eDesignOption
    {
        None = -1,
        New_Design = 0,
        Open_Design = 1
    }

    public enum eOpenDrawingOption
    {
        Sample_Drawings = 0,
        Design_Drawings = 1,
        Cancel = 2
    }
}