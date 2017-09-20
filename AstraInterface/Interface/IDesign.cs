using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.Interface
{
    public interface IDesign
    {
        void Calculate_Program(string file_name);
        string FilePath { set; }
        string GetAstraDirectoryPath(string userpath);
        void Read_User_Input();
        void Write_Drawing_File();
        void Write_User_input();
    }


    public interface ICreateSteel_Warren_1_TrussData
    {
        //AstraInterface.DataStructure.MemberCollection Bottom_Chord_Bracing { get; set; }
        //AstraInterface.DataStructure.MemberCollection Bottom_Chord1 { get; set; }
        //AstraInterface.DataStructure.MemberCollection Bottom_Chord2 { get; set; }
        double Breadth { get; set; }
        bool CreateData(string fileName);
        //AstraInterface.DataStructure.MemberCollection Cross_Girders { get; set; }
        //AstraInterface.DataStructure.MemberCollection Diagonal1 { get; set; }
        //AstraInterface.DataStructure.MemberCollection Diagonal2 { get; set; }
        string End_Support { get; set; }
        //AstraInterface.DataStructure.MemberCollection EndRakers { get; set; }
        System.Collections.Generic.List<string> Get_Bridge_Data();
        System.Collections.Generic.List<string> Get_Bridge_Data(string data);
        //System.Collections.Generic.List<string> Get_Bridge_Data_Top_Bottom(string data);
        //System.Collections.Generic.List<string> Get_Bridge_Data_Vertical_Axis_Z(string data);
        //System.Collections.Generic.List<string> Get_MyntduBridge_Data();
        System.Collections.Generic.List<string> GetJointCoordinates();
        System.Collections.Generic.List<string> GetMemberConnectivityAndOthers();
        string HA_Loading_Members { get; set; }
        double Height { get; set; }
        //void Initialize();
        bool Is_Add_BCB { get; set; }
        bool Is_Add_TCB_DIA { get; set; }
        bool Is_Add_TCB_ST { get; set; }
        bool IS_VERTICAL_AXIS_Y { get; set; }
        //string Joint_Load_Edge { get; set; }
        //string Joint_Load_Support { get; set; }
        double Length { get; set; }
        int NoOfPanel { get; set; }
        int NoOfStringerBeam { get; set; }
        double PanelLength { get; }
        string Start_Support { get; set; }
        //AstraInterface.DataStructure.MemberCollection Stringers { get; set; }
        double StringerSpacing { get; }
        //AstraInterface.DataStructure.MemberCollection Top_Chord_Bracing { get; set; }
        //AstraInterface.DataStructure.MemberCollection Top_Chord_Bracing_Diagonal { get; set; }
        //AstraInterface.DataStructure.MemberCollection Top_Chord1 { get; set; }
        //AstraInterface.DataStructure.MemberCollection Top_Chord2 { get; set; }
        //AstraInterface.DataStructure.MemberCollection Vertical1 { get; set; }
        //AstraInterface.DataStructure.MemberCollection Vertical2 { get; set; }
    }

    public interface ICompleteDesign
    {
        double AddWeightPercent { get; set; }
        AstraInterface.DataStructure.TotalDeadLoad DeadLoads { get; set; }
        double ForceEachEndJoint { get; }
        double ForceEachInsideJoints { get; }
        double GussetAndLacingWeight { get; }
        bool Is_Dead_Load { get; set; }
        bool Is_Live_Load { get; set; }
        bool Is_Super_Imposed_Dead_Load { get; set; }
        bool IsRailBridge { get; set; }
        AstraInterface.DataStructure.MembersDesign Members { get; set; }
        int NoOfEndJointsOnBothSideAtBottomChord { get; }
        int NoOfInsideJointsOnBothSideAtBottomChord { get; }
        int NoOfJointsAtTrussFloor { get; set; }
        void ReadFromFile(string file_name);
        void ToStream(System.IO.StreamWriter sw);
        double TotalBridgeWeight { get; }
        double TotalSteelWeight { get; }
        void WriteForces_Capacity_Summery(System.IO.StreamWriter sw);
        void WriteForces_Capacity_Summery(string file_name);
        void WriteForcesSummery(System.IO.StreamWriter sw);
        void WriteForcesSummery(string file_name);
        void WriteGroupSummery(System.IO.StreamWriter sw);
        void WriteGroupSummery(string file_name);
    }


}
