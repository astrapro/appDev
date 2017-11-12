using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using AstraInterface.DataStructure;

namespace AstraInterface.Interface
{
    public interface IApplication
    {
        
        IDocument AppDocument { get; set; }
        Form AppWindow { get; }
        string AppFolder { get; }
        string WorkingFolder { get; set; }
        string LastDesignWorkingFolder { get; set; }
        string WorkingFile { get; set; }
        void RunViewer();
        void ShowPleaseWait(string str);
        void ClosePleaseWait();
        void RunViewer(string working_folder, string drawing_path);
        void RunViewer(string drawing_path);
        void RunExe(string exeFileName);
        void ShowAnalysisResult(string exeFileName);

        void RunExe(string exeFileName, string env_file_path);
        void OpenDefaultDrawings(string working_folder, string drawing_path);
        void SetApp_Design_Slab(string filePath, eSLAB slab, eSLAB_Part part);
        void SetDrawingFile_Path(string filePath, string code, string default_drawing_path);
        void SetDrawingFile_Path(string filePath, string code, string drawing_path, string default_drawing_code);
        void View_Input_File(string file_name);
        void View_Result(string file_name);
        void View_SAP_Data(string file_name);
        void View_Result(string file_name, bool isLandScape);
        bool ShowTimerScreen(eASTRAImage ast_img_type);
        bool OpenExcelFile(string ExcelFileName, string password);
        bool OpenExcelFile(string CopyPath, string ExcelFileName, string password);
        void OpenWork(string opnfileName, bool IsOpenWithMovingLoad);
        void OpenWork(string opnfileName, bool IsOpenWithMovingLoad, string caption);
        void OpenWork(string opnfileName, string feature);
        DesignDrawings Des_Drawings { get; }
        void Open_Drawings(eDrawingsType item_draw);
        void Open_Drawings(eDrawingsType item_draw, string user_path);
        void Open_WorkSheet_Design();
        void Progress_ON(string title);
        void Progress_OFF();
        double ProgressValue { get; set; }
        void SetProgressValue(double a, double b);
        void Delete_Temporary_Files();
        void Delete_Temporary_Files(string folder_path);
        void Open_ASTRA_Worksheet_Dialog();
        //void Write_LiveLoad_LL_TXT(string folder_path, bool isMTon, bool IsAASHTO);
        void Write_LiveLoad_LL_TXT(string folder_path, bool isMTon, eDesignStandard des_stn);
        void Write_LiveLoad_LL_TXT(string folder_path);
        void Write_Default_LiveLoad_Data();
        LiveLoadCollections LiveLoads { get; set; }

        //void SET_ENV();

        //Chiranjit [2011 11 08] 
        //For Design Standard  [ There are two types of Design Standard,   1>  Indian Standard  2> British Standard
        eDesignStandard DesignStandard { get; set; }

        ASTRA_Tables Tables { get; set; }

        //Chiranjit [2012 07 20]
        bool IsAASTHO { get; set; }
        bool IsDemo { get; set; }

        bool Is_BridgeDemo { get; set; }
        bool Is_StructureDemo { get; set; }


        List<double> Bar_Dia { get; }
        bool Check_Demo_Version();
        bool Check_Demo_Version(bool IsAnalysis);

        bool Check_Coordinate(int JointNo, int MemberNo);

        //Chiranjit [2013 01 03]
        bool Read_Form_Record(object frm, string work_folder);
        bool Save_Form_Record(object frm, string work_folder);
        bool Save_Form_Record(Control ctrl, string work_folder);

        //Chiranjit [2013 05 14]
        ProgressList Progress_Works { get; set; }
        bool Is_Progress_Cancel { get; set; }

        bool Show_and_Run_Process_List(ProcessCollection pcol);

        IProgress Progress { get; }

        eVersionType Version_Type { get; set; }

        string LL_TXT_Path { get; }
        //void Show_LL_Dialog();


        void Show_LL_Dialog();
        void Excel_Open_Message();

        //Close Excel Dialog
        void Excel_Close_Message();

        //Chiranjit [2014 09 30] for British Standard
        Image Get_Image(string img_name);


        //Chiranjit [2014 10 06]

        eDesignOption Get_Design_Option(string title);


        Form Form_ASTRA_TEXT_Data(string file_name, bool IsDrawingFile);
        Form Form_ASTRA_TEXT_Data(string file_name);
        Form Form_ASTRA_Input_Data(string file_name, bool IsDrawingFile);

        Form Form_ASTRA_Analysis_Process(string file_name, bool IsMoving_Load);

        void ASTRA_Analysis_Process(string file_name, string caption);


        string TEXT_File { get; set; }
        string Drawing_File { get; set; }
        string SAP_File { get; set; }
        string EXAMPLE_File { get; set; }


        string user_path { get; set; }

        Form Form_Stage_Analysis(string filename);

        int Timer_Interval { get; set; }

        Form Form_Drawing_Editor(eBaseDrawings DrawingType, string Drawing_Path, string Report_File);
        Form Form_Drawing_Editor(eBaseDrawings DrawingType, string Title, string Drawing_Path, string Report_File);

        eOpenDrawingOption Open_Drawing_Option();


        //[2017 02 16]
        //Stage Selection
        int StageNo { get; set; }
        string Stage_File { get; set; }

        //Open Project Dialog
        string Open_Project_Dialog(string ProjectType, string WorkingFolder);

         string Create_Project(string Title, string ProjectName, eASTRADesignType Project_Type);
         string Set_Project_Name(string Title);
         bool RunAnalysis(string fName);


         void Open_Excel_Macro_Notes();

         void Write_Data_to_File(string fname, string sap_path);
    }

}
//Chiranjit [2011 11 08] 
//For Design Standard  [ There are two types of Design Standard,   1>  Indian Standard  2> British Standard

//Chiranjit [2011 06 23] 
//This class used for Maintain the Design Drawing/ Interactive Drawings

