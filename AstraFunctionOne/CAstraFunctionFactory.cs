using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AstraInterface.Interface;
namespace AstraFunctionOne
{
    public class CAstraFunctionFactory
    {
        private static CAstraFunctionFactory singleton;

        private CAstraFunctionFactory()
        {

        }

        public static CAstraFunctionFactory Instance
        {
            get
            {
                if (singleton == null) singleton = new CAstraFunctionFactory();
                return singleton;
            }
        }

        public void ShowBasicInfoDialog(IApplication aApp)
        {
            frmBasicInfo proj = new frmBasicInfo(aApp);
            proj.Owner = aApp.AppWindow;
            try
            {
                proj.ShowDialog();
            }
            catch (Exception ex) { }
        }
        public void ShowNodalDataDialog(IApplication aApp)
        {
            frmNodalData nd = new frmNodalData(aApp);
            nd.Owner = aApp.AppWindow;
            nd.ShowDialog();
        }
        public void ShowBeamMembersDialog(IApplication aApp)
        {
            frm3DBeamMembers bmMemb = new frm3DBeamMembers(aApp);
            bmMemb.Owner = aApp.AppWindow;
            bmMemb.ShowDialog();
        }
        public void ShowSelfWeightDialog(IApplication aApp)
        {
            frmSelfWeight fslf = new frmSelfWeight(aApp);
            fslf.Owner = aApp.AppWindow;
            fslf.ShowDialog();
        }
        public void ShowMemberBeamLoadDialog(IApplication aApp)
        {
            frmNodalLoadData nld = new frmNodalLoadData(aApp);
            nld.Owner = aApp.AppWindow;
            nld.ShowDialog();
        }
        public void ShowAnalysisDialog(IApplication aApp)
        {
            frmAnalysisType anat = new frmAnalysisType(aApp);
            anat.Owner = aApp.AppWindow;
            anat.ShowDialog();
        }
        public void ShowSupportDialog(IApplication aApp)
        {
            frmSupport srpt = new frmSupport(aApp);
            srpt.Owner = aApp.AppWindow;
            srpt.ShowDialog();
        }
        public void ShowPlateAndShellDialog(IApplication aApp)
        {
            frmPlateAndShell ps = new frmPlateAndShell(aApp);
            ps.Owner = aApp.AppWindow;
            ps.ShowDialog();
        }

        public void ShowNewAnalysisDialog(IApplication aApp)
        {
            frmViewFile ps = new frmViewFile(aApp);
            ps.Owner = aApp.AppWindow;
            ps.ShowDialog();
        }
        public void ShowAnalysisExamplesDialog(IApplication aApp, bool IsProcess)
        {
            frm_Examples ps = new frm_Examples(aApp, IsProcess);
            ps.Owner = aApp.AppWindow;
            ps.ShowDialog();
        }


        

        public void ShowStageAnalysisDialog(IApplication aApp)
        {
            //frmStageAnalysis ps = new frmStageAnalysis(aApp);
            frmStageAnalysis ps = new frmStageAnalysis(aApp);
            ps.Owner = aApp.AppWindow;
            ps.ShowDialog();
        }

        public void OpenStageAnalysisDialog(IApplication aApp, string filename)
        {
            frmStageAnalysis ps = new frmStageAnalysis(aApp, filename);
            ps.Owner = aApp.AppWindow;
            ps.ShowDialog();
        }

    }
    public class ImageCollection
    {
        public static Bitmap _01_General { get { return AstraFunctionOne.Properties.Resources._01_General; } }
        public static Bitmap _02_Top_Chord_U2U3 { get { return AstraFunctionOne.Properties.Resources._02_Top_Chord_U2U3; } }
        public static Bitmap _03_Inclined_Member_U2L3 { get { return AstraFunctionOne.Properties.Resources._03_Inclined_Member_U2L3; } }
        public static Bitmap _04_Vertical_Member_U3L3 { get { return AstraFunctionOne.Properties.Resources._04_Vertical_Member_U3L3; } }
        public static Bitmap _05_Bottom_Chord_L2L3 { get { return AstraFunctionOne.Properties.Resources._05_Bottom_Chord_L2L3; } }
        public static Bitmap _06_Stringer_Beams { get { return AstraFunctionOne.Properties.Resources._06_Stringer_Beams; } }
        public static Bitmap _07_Cross_Girders { get { return AstraFunctionOne.Properties.Resources._07_Cross_Girders; } }
        public static Bitmap _08_Cross_Girder { get { return AstraFunctionOne.Properties.Resources._08_Cross_Girder; } }
        public static Bitmap Abutment { get { return AstraFunctionOne.Properties.Resources.Abutment; } }
        public static Bitmap Bottom_Chord_Bracing { get { return AstraFunctionOne.Properties.Resources.Bottom_Chord_Bracing; } }
        public static Bitmap Box_Culvert { get { return AstraFunctionOne.Properties.Resources.Box_Culvert; } }
        public static Bitmap Cantilever_Slab { get { return AstraFunctionOne.Properties.Resources.Cantilever_Slab; } }
        public static Bitmap CompoBridge { get { return AstraFunctionOne.Properties.Resources.CompoBridge; } }
        public static Bitmap Composite_Bridge_Image01 { get { return AstraFunctionOne.Properties.Resources.Composite_Bridge_Image01; } }
        public static Bitmap Cross_Girder { get { return AstraFunctionOne.Properties.Resources.Cross_Girder; } }
        public static Bitmap DCP_3935 { get { return AstraFunctionOne.Properties.Resources.DCP_3935; } }
        public static Bitmap Dialog_Box_Pile_Foundation_Image01 { get { return AstraFunctionOne.Properties.Resources.Dialog_Box_Pile_Foundation_Image01; } }
        public static Bitmap Pile_Layout_Image01 { get { return AstraFunctionOne.Properties.Resources.Pile_Layout_Image; } }
        public static Bitmap Fig_1 { get { return AstraFunctionOne.Properties.Resources.Fig_1; } }
        public static Bitmap Fig_2 { get { return AstraFunctionOne.Properties.Resources.Fig_2; } }
        public static Bitmap Fig_3 { get { return AstraFunctionOne.Properties.Resources.Fig_3; } }
        public static Bitmap Fig_4 { get { return AstraFunctionOne.Properties.Resources.Fig_4; } }
        public static Bitmap Fig_5 { get { return AstraFunctionOne.Properties.Resources.Fig_5; } }
        public static Bitmap Fig_6 { get { return AstraFunctionOne.Properties.Resources.Fig_6; } }
        public static Bitmap Fully_Loaded { get { return AstraFunctionOne.Properties.Resources.Fully_Loaded; } }
        public static Bitmap Image1 { get { return AstraFunctionOne.Properties.Resources.Image1; } }
        public static Bitmap K_0_4 { get { return AstraFunctionOne.Properties.Resources.K_0_4; } }
        public static Bitmap K_0_5 { get { return AstraFunctionOne.Properties.Resources.K_0_5; } }
        public static Bitmap K_0_6 { get { return AstraFunctionOne.Properties.Resources.K_0_6; } }
        public static Bitmap K_0_7 { get { return AstraFunctionOne.Properties.Resources.K_0_7; } }
        public static Bitmap K_0_8 { get { return AstraFunctionOne.Properties.Resources.K_0_8; } }
        public static Bitmap K_0_9 { get { return AstraFunctionOne.Properties.Resources.K_0_9; } }
        public static Bitmap K_1_0 { get { return AstraFunctionOne.Properties.Resources.K_1_0; } }
        public static Bitmap PIER { get { return AstraFunctionOne.Properties.Resources.PIER; } }
        public static Bitmap Pier_drawing { get { return AstraFunctionOne.Properties.Resources.Pier_drawing; } }
        public static Bitmap Pier_Box_drawing { get { return AstraFunctionOne.Properties.Resources.Pier_Box_drawing; } }
        public static Bitmap pile_fond_graph { get { return AstraFunctionOne.Properties.Resources.pile_fond_graph; } }
        public static Bitmap PIPE_Culvert { get { return AstraFunctionOne.Properties.Resources.PIPE_Culvert; } }
        public static Bitmap PIPE_Culvert_1 { get { return AstraFunctionOne.Properties.Resources.PIPE_Culvert_1; } }
        public static Bitmap PIPE_Culvert_2 { get { return AstraFunctionOne.Properties.Resources.PIPE_Culvert_2; } }
        public static Bitmap Pre_Stressed_Bridge { get { return AstraFunctionOne.Properties.Resources.Pre_Stressed_Bridge; } }
        public static Bitmap prestressed_bridge_image01 { get { return AstraFunctionOne.Properties.Resources.prestressed_bridge_image01; } }
        public static Bitmap Prestressed_Girder_Dialog_Box { get { return AstraFunctionOne.Properties.Resources.Prestressed_Girder_Dialog_Box; } }
        public static Bitmap Rail_BridgeImage2 { get { return AstraFunctionOne.Properties.Resources.Rail_BridgeImage2; } }
        public static Bitmap RCC_T_Beam_Bridge { get { return AstraFunctionOne.Properties.Resources.RCC_T_Beam_Bridge; } }
        
        public static Bitmap Rcc_Well_Foundation { get { return AstraFunctionOne.Properties.Resources.Rcc_Well_Foundation; } }
        public static Bitmap Rcc_Well_Foundation1 { get { return AstraFunctionOne.Properties.Resources.Rcc_Well_Foundation1; } }


        //public static Bitmap section10 { get { return AstraFunctionOne.Properties.Resources.section10; } }
        public static Bitmap Truss_Section1 { get { return AstraFunctionOne.Properties.Resources.section11; } }
        public static Bitmap Truss_Section2 { get { return AstraFunctionOne.Properties.Resources.section21; } }
        public static Bitmap Truss_Section3 { get { return AstraFunctionOne.Properties.Resources.section31; } }
        public static Bitmap Truss_Section4 { get { return AstraFunctionOne.Properties.Resources.section41; } }
        public static Bitmap Truss_Section5 { get { return AstraFunctionOne.Properties.Resources.Stringer_Beam; } }
        public static Bitmap Truss_Section6 { get { return AstraFunctionOne.Properties.Resources.Cross_Girder; } }
        public static Bitmap Truss_Section7 { get { return AstraFunctionOne.Properties.Resources.Top_Chord_Bracing; } }
        public static Bitmap Truss_Section8 { get { return AstraFunctionOne.Properties.Resources.Bottom_Chord_Bracing; } }
        public static Bitmap Truss_Section9 { get { return AstraFunctionOne.Properties.Resources.section9; } }
        public static Bitmap Truss_Section10 { get { return AstraFunctionOne.Properties.Resources.section10; } }
        public static Bitmap Truss_Section11 { get { return AstraFunctionOne.Properties.Resources.section111; } }
        public static Bitmap Truss_Section12 { get { return AstraFunctionOne.Properties.Resources.section12; } }
        public static Bitmap Truss_Section13 { get { return AstraFunctionOne.Properties.Resources.section13; } }
        public static Bitmap Truss_Section14 { get { return AstraFunctionOne.Properties.Resources.section14; } }
        public static Bitmap Truss_Section15 { get { return AstraFunctionOne.Properties.Resources.section15; } }
        //public static Bitmap section111 { get { return AstraFunctionOne.Properties.Resources.section111; } }

        public static Bitmap Cable_Section16 { get { return AstraFunctionOne.Properties.Resources.Cable_section_15; } }

        public static Bitmap Cable_Section17 { get { return AstraFunctionOne.Properties.Resources.suspension_section_17; } }




        public static Bitmap section12 { get { return AstraFunctionOne.Properties.Resources.section12; } }
        public static Bitmap section13 { get { return AstraFunctionOne.Properties.Resources.section13; } }
        public static Bitmap section14 { get { return AstraFunctionOne.Properties.Resources.section14; } }
        public static Bitmap section15 { get { return AstraFunctionOne.Properties.Resources.section15; } }
        public static Bitmap section21 { get { return AstraFunctionOne.Properties.Resources.section21; } }
        public static Bitmap section3 { get { return AstraFunctionOne.Properties.Resources.section3; } }
        public static Bitmap section31 { get { return AstraFunctionOne.Properties.Resources.section31; } }
        public static Bitmap section4 { get { return AstraFunctionOne.Properties.Resources.section4; } }
        public static Bitmap section41 { get { return AstraFunctionOne.Properties.Resources.section41; } }
        public static Bitmap section9 { get { return AstraFunctionOne.Properties.Resources.section9; } }
        //public static Bitmap section111 { get { return AstraFunctionOne.Properties.Resources.section111; } }

        public static Bitmap SLAB_Culvert { get { return AstraFunctionOne.Properties.Resources.SLAB_Culvert; } }
        public static Bitmap Steel_Truss_Bridge { get { return AstraFunctionOne.Properties.Resources.Steel_Truss_Bridge; } }
        public static Bitmap Stringer_Beam { get { return AstraFunctionOne.Properties.Resources.Stringer_Beam; } }
        public static Bitmap T_Beam_Secion_for_Dimensions { get { return AstraFunctionOne.Properties.Resources.T_Beam_Secion_for_Dimensions; } }
        public static Bitmap T_Beam_Slab_Long_Cross_Girders { get { return AstraFunctionOne.Properties.Resources.T_Beam_Slab_Long_Cross_Girders; } }
        public static Bitmap TBeam_Main_Girder_Bottom_Flange { get { return AstraFunctionOne.Properties.Resources.TBeam_Main_Girder_Bottom_Flange; } }
        public static Bitmap Top_Chord_Bracing { get { return AstraFunctionOne.Properties.Resources.Top_Chord_Bracing; } }
        public static Bitmap Warren1 { get { return AstraFunctionOne.Properties.Resources.Warren1; } }
        public static Bitmap Warren2 { get { return AstraFunctionOne.Properties.Resources.Warren2; } }
        //public static Bitmap Warren3 { get { return AstraFunctionOne.Properties.Resources.Warrens; } }
        //Chiranjit [2011 11 11] Cable_Stayed_Bridge
        public static Bitmap Cable_Stayed_Bridge { get { return AstraFunctionOne.Properties.Resources.Cable_Stayed_Bridge; } }


        public static Bitmap suspension_section_16 { get { return AstraFunctionOne.Properties.Resources.suspension_section_16; } }
        public static Bitmap suspension_section_17 { get { return AstraFunctionOne.Properties.Resources.suspension_section_17; } }


        //Chiranjit [2016 09 27]
        public static Bitmap Steel_Truss_K_Type_Diagram { get { return AstraFunctionOne.Properties.Resources.Steel_Truss_K_Type_2; } }
        public static Bitmap Steel_Truss_K_Type_Invert_Diagram { get { return AstraFunctionOne.Properties.Resources.Steel_Truss_K_Type_Invert; } }
        public static Bitmap Warren3 { get { return AstraFunctionOne.Properties.Resources.Steel_Truss_Warre3; } }
        public static Bitmap Steel_Truss_K_Type { get { return AstraFunctionOne.Properties.Resources.K_truss_2; } }



    }

}
