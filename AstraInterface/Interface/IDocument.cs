using System;
using System.Collections.Generic;
using System.Text;
using AstraInterface.DataStructure;

namespace AstraInterface.Interface
{
    public interface IDocument
    {
        CBasicInfo BasicInfo { get; set; }
        string ProjectTitle{get; set;}
        StructureType SType { get; set;}
        CNodeDataCollection NodeData { get; set;}
        CMemberConnectivityCollection BeamConnectivity { get; set;}
        CSectionPropertyCollection SectionProperty { get; set;}
        CMaterialPropertyCollection MaterialProperty { get; set;}
        CJointNodalLoadCollection JointNodalLoad { get;set;}
        CMemberBeamLoadingCollection MemberBeamLoad { get;set;}
        CMemberTrussCollection MemberTruss {get;set;}
        CSelfWeight SelfWeight { get; set;}
        CAnalysis Analysis { get;set;}
        CTimeHistoryAnalysis TimeHistory { get; set; }
        CSupportCollection Support { get;set;}
        CMovingLoadCollection MovingLoad { get;set;}
        CLoadGenerationCollection LoadGeneration { get;set;}
        CBeamConnectivityReleaseCollection BeamConnectivityRelease { get;set;}
        CLoadCombinationCollection LoadCombination { get;set;}
        CAreaLoadCollection AreaLoad { get; set;}
        CMaterialPropertyInformationCollection MatPropertyInfo { get;set;}
        CElementDataCollection ElementData { get;set;}
        CElementMultiplierCollection ElementMultiplier { get;set;}
        CResponse Response { get; set; }
        bool Read(string FilePath);
        bool Write(string FilePath);
        void ClearVars();
        double LFact { get; set;}
        double WFact { get; set;}
        short wunit_flag { get; set;}
        short lunit_flag { get; set;}
        short Modex { get;set;}
        string LUnit { get;set;}
        string MUnit { get;set;}
        string FilePath { get; set; }

    }
}
