using System;
namespace LimitStateMethod.Composite
{
    interface IComposite_LS_Analysis
    {
        string _Cross_Girder_End { get; set; }
        string _Cross_Girder_Inter { get; set; }
        string _DeckSlab { get; set; }
        string _Inner_Girder_Mid { get; set; }
        string _Inner_Girder_Support { get; set; }
        string _Outer_Girder_Mid { get; set; }
        string _Outer_Girder_Support { get; set; }
        string Analysis_Report { get; }
        void CreateData();
        void CreateData(bool isPSC_I_Girder);
        void CreateData_2013_05_03();
        void CreateData_British();
        void CreateData_Indian();
        void CreateData_Orthotropic();
        void CreateData_Straight_Indian();
        void CreateData_StraightBritish();
        string Cross_Girders_as_String { get; set; }
        string DeadLoad_Analysis_Report { get; }
        string DeadLoadAnalysis_Input_File { get; }
        string Deff_Girders_as_String { get; set; }
        string Density { get; set; }
        double Ds { get; set; }
        string E_Modulus { get; set; }
        double Effective_Depth { get; set; }
        string End_Support { get; set; }
        string Get_LHS_Outer_Girder();
        string Get_Live_Load_Analysis_Input_File(int analysis_no);
        string Get_Live_Load_Analysis_Input_File(int analysis_no, bool IsStageFile);
        string Get_RHS_Outer_Girder();
        string GetAnalysis_Input_File(int p, bool IsStageFile);
        string Inner_Girders_as_String { get; set; }
        string Input_File { get; set; }
        AstraInterface.DataStructure.JointNodeCollection Joints { get; set; }
        double Length { get; set; }
        string LiveLoad_Analysis_Report { get; }
        string LiveLoad_File { get; }
        string LiveLoadAnalysis_Input_File { get; }
        void LoadReadFromGrid(System.Windows.Forms.DataGridView dgv_live_load);
        AstraInterface.DataStructure.MemberCollection MemColls { get; set; }
        double Mid_Span_Length { get; }
        double NCG { get; set; }
        double NMG { get; set; }
        int NoOfInsideJoints { get; }
        string Orthotropic_Input_File { get; }
        string Outer_Girders_as_String { get; set; }
        double Penultimate_Span_Length { get; }
        string Poission_Ration { get; set; }
        BridgeAnalysisDesign.PSC_I_Girder.PreStressedConcrete_SectionProperties PSC_Cross { get; set; }
        BridgeAnalysisDesign.PSC_I_Girder.PreStressedConcrete_SectionProperties PSC_End { get; set; }
        BridgeAnalysisDesign.PSC_I_Girder.PreStressedConcrete_SectionProperties PSC_Mid_Span { get; set; }
        double Skew_Angle { get; set; }
        double Spacing_Cross_Girder { get; }
        double Spacing_Long_Girder { get; }
        System.Collections.Generic.List<double> Spans { get; set; }
        string Start_Support { get; set; }
        BridgeAnalysisDesign.Composite.CompositeSection Steel_Section { get; set; }
        string Straight_DL_File { get; }
        string Straight_LL_File { get; }
        string Straight_TL_File { get; }
        string TempAnalysis_Input_File { get; }
        string Total_Analysis_Report { get; }
        int Total_Columns { get; set; }
        double Total_Length { get; }
        int Total_Rows { get; set; }
        string TotalAnalysis_Input_File { get; }
        string User_Input_Data { get; }
        double Width_LeftCantilever { get; set; }
        double Width_RightCantilever { get; set; }
        double WidthBridge { get; set; }
        void WriteData(string file_name);
        void WriteData_DeadLoad_Analysis(string file_name);
        void WriteData_DeadLoad_Analysis(string file_name, bool is_PSC_I_Gider);
        void WriteData_LiveLoad_Analysis(string file_name);
        void WriteData_LiveLoad_Analysis(string file_name, bool is_psc_I_Girder);
        void WriteData_LiveLoad_Analysis(string file_name, System.Collections.Generic.List<string> ll_data);
        void WriteData_Orthotropic_Analysis(string file_name);
        void WriteData_Orthotropic_Analysis(string file_name, bool is_british);
        void WriteData_Total_Analysis(string file_name);
        void WriteData_Total_Analysis(string file_name, bool is_british, System.Collections.Generic.List<string> ll_data);
        void WriteData_Total_Analysis(string file_name, bool is_PSC_I_Girder);
        void WriteData_Total_Analysis(string file_name, bool is_PSC_I_Girder, bool is_british);
    }
}
