using System;
namespace AstraFunctionOne.BridgeDesign.SteelTruss
{
    interface ICreateSteel_Warren_1_TrussData1
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

}
