using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using System.;
//using System.Threading.Tasks;
using HEADSNeed.ASTRA.CadToAstra.FORMS;
using HEADSNeed.ASTRA.ASTRAForms;

using AstraInterface.Interface;
using AstraInterface.DataStructure;
using AstraAccess.StructureAnalysisDesign;
namespace AstraAccess
{
    public static class ViewerFunctions
    {
        
        public static void ASTRA_Input_Data(string file_name, bool IsDrawingFile)
        {
            frm_DrawingToData frm = new frm_DrawingToData(file_name, IsDrawingFile);
            frm.Show();
        }
        public static void ASTRA_Input_Data(string file_name)
        {
            frm_DrawingToData frm = new frm_DrawingToData(file_name);
            frm.Show();
        }
        public static void ASTRA_Analysis_Process(string file_name, bool IsMoving_Load)
        {
            frm_ASTRA_Analysis frm = new frm_ASTRA_Analysis(file_name, IsMoving_Load);
            frm.Is_Analysis = false;
            frm.Show();
        }

        public static void ASTRA_Analysis_Process(string file_name, string caption)
        {
            frm_ASTRA_Analysis frm = new frm_ASTRA_Analysis(file_name, false);
            frm.Is_Analysis = false;
            frm.Process_Caption = caption;
            frm.Show();
        }

        public static Form Form_ASTRA_Input_Data(string file_name, bool IsDrawingFile)
        {
            frm_DrawingToData frm = new frm_DrawingToData(file_name, IsDrawingFile);
            return frm;
        }
        public static Form Form_ASTRA_TEXT_Input_Data(string file_name, bool IsDrawingFile)
        {
            frm_ASTRA_Inputs frm = new frm_ASTRA_Inputs(file_name, IsDrawingFile);
            return frm;
        }
        public static Form Form_ASTRA_TEXT_Input_Data(IApplication app, string file_name, bool IsDrawingFile)
        {
            frm_ASTRA_Inputs frm = new frm_ASTRA_Inputs(file_name, IsDrawingFile);
            frm.Set_SAP_Data(new frm_ASTRA_Inputs.dWrite_Data_to_File(app.Write_Data_to_File));
            return frm;
        }
        public static Form Form_ASTRA_Structure_Input_Data(IApplication app, string working_folder)
        {
            frm_StructureDesign frm = new frm_StructureDesign();
            frm.StageAnalysisForm = Form_Stage_Analysis(app);
            frm.Working_Folder = working_folder;
            frm.LastDesignWorkingFolder = working_folder;
            return frm;
        }
        public static Form Form_ASTRA_Tunnel_Input_Data(IApplication app,string working_folder)
        {
            frm_Tunnel_Design frm = new frm_Tunnel_Design(app);
            frm.Working_Folder = working_folder;
            //frm.Working_Folder = working_folder;
            return frm;
        }
        public static Form Form_ASTRA_Analysis_Process(string file_name, bool IsMoving_Load)
        {
            frm_ASTRA_Analysis frm = new frm_ASTRA_Analysis(file_name, IsMoving_Load);
            return frm;
        }
        public static Form Form_ASTRA_Moving_Load(string file_name)
        {
            frm_ASTRA_MovingLoad frm = new frm_ASTRA_MovingLoad(file_name, true);
            return frm;
        }
        public static Form Form_SAP_Editor(IApplication app)
        {
            frmSAP_Editor frm = new frmSAP_Editor(app);
            return frm;
        }
        //public static Form Form_TEXT_Editor(string file_name, bool isDrawin)
        //{
        //    frm_ASTRA_Inputs frm = new frm_ASTRA_Inputs(app, );
        //    return frm;
        //}
        public static Form Form_SAP_Editor(IApplication app, string file_name)
        {
            frmSAP_Editor frm = new frmSAP_Editor(app, file_name);
            return frm;
        }


        public static Form Form_Stage_Analysis(IApplication app)
        {
            StageAnalysis.frm_ProcessStageAnalysis frm = new StageAnalysis.frm_ProcessStageAnalysis(app);
            return frm;
        }
        public static Form Form_Stage_Analysis(IApplication app, string file_name)
        {
            StageAnalysis.frm_ProcessStageAnalysis frm = new StageAnalysis.frm_ProcessStageAnalysis(app, file_name);
            return frm;
        }

        public static Form Form_Drawing_Editor(IApplication app, eBaseDrawings DrawingType, string drawing_folder, string report_file)
        {
            AstraAccess.DrawingWorkspace.frmDrawingsEditor frm = new AstraAccess.DrawingWorkspace.frmDrawingsEditor(app, DrawingType, drawing_folder, report_file);
            return frm;
        }

        public static Form Form_Drawing_Editor(IApplication app, eBaseDrawings DrawingType, string Title, string drawing_folder, string report_file)
        {
            AstraAccess.DrawingWorkspace.frmDrawingsEditor frm = new AstraAccess.DrawingWorkspace.frmDrawingsEditor(app, DrawingType,Title, drawing_folder, report_file);
            return frm;
        }


        public static Form Form_ResponseSpectrumAnalysis(IApplication app, eASTRADesignType projType)
        {
            AstraAccess.DynamicAnalysis.frmResponseSpectrumAnalysis frm = new AstraAccess.DynamicAnalysis.frmResponseSpectrumAnalysis(app, projType);
            //AstraAccess.DynamicAnalysis.frmDynamicAnalysis frm = new AstraAccess.DynamicAnalysis.frmDynamicAnalysis(app, projType);
            return frm;
        }
        public static Form Form_DynamicAnalysis(IApplication app, eASTRADesignType projType)
        {
            //AstraAccess.DynamicAnalysis.frmResponseSpectrumAnalysis frm = new AstraAccess.DynamicAnalysis.frmResponseSpectrumAnalysis(app, projType);
            AstraAccess.DynamicAnalysis.frmDynamicAnalysis frm = new AstraAccess.DynamicAnalysis.frmDynamicAnalysis(app, projType);
            return frm;
        }
        public static Form Form_TimeHistoryAnalysis(IApplication app)
        {
            AstraAccess.DynamicAnalysis.frmTimeHostoryAnalysis frm = new AstraAccess.DynamicAnalysis.frmTimeHostoryAnalysis(app);
            //AstraAccess.DynamicAnalysis.frmDynamicAnalysis frm = new AstraAccess.DynamicAnalysis.frmDynamicAnalysis(app, projType);
            return frm;
        }
        public static Form Form_EigenValueAnalysis(IApplication app)
        {
            AstraAccess.DynamicAnalysis.frmEigenValueAnalysis frm = new AstraAccess.DynamicAnalysis.frmEigenValueAnalysis(app);
            //AstraAccess.DynamicAnalysis.frmDynamicAnalysis frm = new AstraAccess.DynamicAnalysis.frmDynamicAnalysis(app, projType);
            return frm;
        }
        public static Form Form_OrthotropicEditor(IApplication app)
        {
            AstraAccess.frmOrthotropicEditor frm = new AstraAccess.frmOrthotropicEditor(app);
            //AstraAccess.DynamicAnalysis.frmDynamicAnalysis frm = new AstraAccess.DynamicAnalysis.frmDynamicAnalysis(app, projType);
            return frm;
        }
    }
}
