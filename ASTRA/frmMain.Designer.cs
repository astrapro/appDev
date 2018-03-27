 namespace AstraFunctionOne
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            //DeleteTmpFiles(this.CurrentDirectory);
            try
            {
                recent.SetFilesFromMenu(recentFilesToolStripMenuItem);
                recent.WriteFileNames();
            }
            catch (System.Exception exx) { }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mns_ASTRA_menues = new System.Windows.Forms.MenuStrip();
            this.tsmi_file = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_newAnalysisTXTDataFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_newAnalysisSAPDataFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_newAnalysisDWGDataFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_openAnalysisTXTDataFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_open_txt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_open_ast = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_open_work_folder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_openSAPDataFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_openStructureModelDrawingFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_openAnalysisExampleTXTDataFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_structure_text = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_structure_sap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_openStageAnalysisTEXTDataFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_saveTXTDataFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_openTXTDataInNotepad = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_openMSExcelDesignReport = new System.Windows.Forms.ToolStripMenuItem();
            this.openASTRAWorksheetDesignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Create_New_AST_File = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ast_data = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ast_basic_info = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ast_analysis_type = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ast_nodes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ast_elements = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ast_beam_truss = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ast_plate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ast_supports = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ast_nodal_loads = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ast_self_weight = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_license = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_process_analysis = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_viewInputData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_runTXTFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_processAnalysisASTFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_convertToASTFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_viewAnalysisASTFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_analysisInputDataFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_analysisProcessResults = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_workingFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ana_text_file = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ana_inp_text = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ana_res_text = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ana_view_text = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ana_dwg_file = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ana_inp_dwg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ana_res_dwg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_stage_ana = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ana_sap_file = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ana_inp_sap = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ana_res_sap = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ana_exmpls = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ana_inp_exm = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ana_res_exm = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processAstFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runASTFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.astDataInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.basicInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.plateAndShellElementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nodalLoadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Process_Design = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_selectWorkingFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_selectDesignStandard = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Bridge_Design = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Working_Folder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_RCC_TGirder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_RCC_TGirder_LSM = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_RCC_TGirder_WSM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_Composite = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Composite_LSM = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Composite_WSM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_PSC_IGirder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_PSC_IGirder_LSM = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_PSC_IGirder_WSM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_PSC_Box_Girder_Bridge = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_cont_PSC_Box_Girder_Bridge = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_Steel_Truss_Bridge_Warren_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.completeDesignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deckSlabDesignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Steel_Truss_Bridge_Warren_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Steel_Truss_Bridge_Warren_3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Steel_Truss_Bridge_K_Type = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_arch_cable_bridge = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_arch_steel_bridge = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_cable_suspension_bridge = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_cable_stayed_bridge = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_extradossed = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_extradossed_side_towers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_extradossed_central_towers = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_Rail_Bridge = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Underpass = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_road_box_pushed_underpass = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_rail_box_pushed_underpass = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_GADs_Underpasses = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_minor_Bridge = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_minor_Bridge_ls = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_minor_Bridge_ws = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_RCC_Culverts = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_RCC_Culverts_LS = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_RCC_Culverts_WS = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_Bridge_Abutment_Design = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Bridge_Pier_Design = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pier_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_RCC_Pier_LS = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pier_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pier_3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pier_4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pier_5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Bridge_Pier_Pile_Design = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_RE_Wall_Design = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Bridge_Bearing_Design = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Bridge_Foundation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_Hydraulic_Calculations = new System.Windows.Forms.ToolStripMenuItem();
            this.hydraulicCalculationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Material_Properties1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_CostEstimation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_RCC_Structural_Design = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Working_Folder1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rCCSLABToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oneWayRCCSlabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oneWayContinuousRCCSlabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twoWayRCCSlabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asdsdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simplySupportedSingleSpanBeamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rectangularOrFlangeBeamBS8110ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rCCCOLUMNToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.axiallyLoadedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axialLoadBiAxialMomentToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_re_wall_des_strc = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_return_wall_des_strc = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_return_wall_cant = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_return_wall_propped = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_tunnel_design = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_rcc_structure_design = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_structural_components = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_steel_beam = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_steel_column = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_isolatedFooting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_combinedFooting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_pIleFoundation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_raftFoundation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_jetty_design = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_jetty_design_RCC = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_jetty_design_PSC = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_transmissionTower = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_transmissionTower3Cables = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_microwaveTower = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_cableCarTower = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_streamHydrology = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_structureModeling = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_research_Studies = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_selectWorkingFolderResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_HSTBD = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_dynamicAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_eigenValue = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_timeHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_responseSpectrum = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_PDelta = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_streamHydrologyResearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_CAD_Viewer = new System.Windows.Forms.ToolStripMenuItem();
            this.astraProViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.foldersAndOpenFiletoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.explorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notePadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_geotechnics = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Material_Properties = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Section_Properties = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_seismic_coefficient = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ll_data = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_steel_tables = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_contents = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_contents_supplement = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_sap_manual = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_design_manual = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_users_manual = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_videos = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_send_mail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_link_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_link_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.programsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gPSTransformationGuideToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.googleEarthToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.infoAboutThisProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalMapperV11ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoAboutTheProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runProgramToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sheetPileApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_sheet_pile = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdAst = new System.Windows.Forms.OpenFileDialog();
            this.sfdAst = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnNewFile = new System.Windows.Forms.ToolStripButton();
            this.tsbtnOpenFile = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSaveFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnCut = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbtnPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRedo = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsl_text = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.btn_SendMail = new System.Windows.Forms.Button();
            this.mns_ASTRA_menues.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mns_ASTRA_menues
            // 
            this.mns_ASTRA_menues.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.mns_ASTRA_menues.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_file,
            this.tsmi_process_analysis,
            this.viewToolStripMenuItem,
            this.processAstFileToolStripMenuItem,
            this.tsmi_Process_Design,
            this.tsmi_research_Studies,
            this.tsmi_CAD_Viewer,
            this.utilityToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.programsToolStripMenuItem});
            this.mns_ASTRA_menues.Location = new System.Drawing.Point(0, 0);
            this.mns_ASTRA_menues.Name = "mns_ASTRA_menues";
            this.mns_ASTRA_menues.Size = new System.Drawing.Size(766, 24);
            this.mns_ASTRA_menues.TabIndex = 0;
            this.mns_ASTRA_menues.Text = "menuStrip1";
            // 
            // tsmi_file
            // 
            this.tsmi_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_newAnalysisTXTDataFile,
            this.tsmi_newAnalysisSAPDataFile,
            this.tsmi_newAnalysisDWGDataFile,
            this.toolStripSeparator8,
            this.tsmi_openAnalysisTXTDataFile,
            this.tsmi_open_txt,
            this.tsmi_open_ast,
            this.tsmi_open_work_folder,
            this.tsmi_openSAPDataFile,
            this.tsmi_openStructureModelDrawingFile,
            this.tsmi_openAnalysisExampleTXTDataFile,
            this.tsmi_structure_text,
            this.tsmi_structure_sap,
            this.toolStripSeparator22,
            this.tsmi_openStageAnalysisTEXTDataFile,
            this.toolStripSeparator3,
            this.tsmi_saveTXTDataFile,
            this.tsmi_openTXTDataInNotepad,
            this.toolStripSeparator4,
            this.tsmi_openMSExcelDesignReport,
            this.openASTRAWorksheetDesignToolStripMenuItem,
            this.tsmi_Create_New_AST_File,
            this.tsmi_ast_data,
            this.saveToolStripMenuItem1,
            this.saveAsToolStripMenuItem2,
            this.toolStripSeparator1,
            this.recentFilesToolStripMenuItem,
            this.toolStripSeparator2,
            this.tsmi_license,
            this.toolStripSeparator9,
            this.exitToolStripMenuItem});
            this.tsmi_file.Name = "tsmi_file";
            this.tsmi_file.Size = new System.Drawing.Size(37, 20);
            this.tsmi_file.Text = "&File";
            // 
            // tsmi_newAnalysisTXTDataFile
            // 
            this.tsmi_newAnalysisTXTDataFile.Name = "tsmi_newAnalysisTXTDataFile";
            this.tsmi_newAnalysisTXTDataFile.Size = new System.Drawing.Size(268, 22);
            this.tsmi_newAnalysisTXTDataFile.Text = "New Analysis TEXT Data File";
            this.tsmi_newAnalysisTXTDataFile.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_newAnalysisSAPDataFile
            // 
            this.tsmi_newAnalysisSAPDataFile.Name = "tsmi_newAnalysisSAPDataFile";
            this.tsmi_newAnalysisSAPDataFile.Size = new System.Drawing.Size(268, 22);
            this.tsmi_newAnalysisSAPDataFile.Text = "New Analysis SAP Data File";
            this.tsmi_newAnalysisSAPDataFile.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_newAnalysisDWGDataFile
            // 
            this.tsmi_newAnalysisDWGDataFile.Name = "tsmi_newAnalysisDWGDataFile";
            this.tsmi_newAnalysisDWGDataFile.Size = new System.Drawing.Size(268, 22);
            this.tsmi_newAnalysisDWGDataFile.Text = "New Structure Model Drawing File";
            this.tsmi_newAnalysisDWGDataFile.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(265, 6);
            // 
            // tsmi_openAnalysisTXTDataFile
            // 
            this.tsmi_openAnalysisTXTDataFile.Name = "tsmi_openAnalysisTXTDataFile";
            this.tsmi_openAnalysisTXTDataFile.Size = new System.Drawing.Size(268, 22);
            this.tsmi_openAnalysisTXTDataFile.Text = "Open Analysis TEXT Data File";
            this.tsmi_openAnalysisTXTDataFile.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_open_txt
            // 
            this.tsmi_open_txt.Name = "tsmi_open_txt";
            this.tsmi_open_txt.Size = new System.Drawing.Size(268, 22);
            this.tsmi_open_txt.Text = "Open &TXT File";
            this.tsmi_open_txt.Visible = false;
            this.tsmi_open_txt.Click += new System.EventHandler(this.tsmi_open_txt_Click);
            // 
            // tsmi_open_ast
            // 
            this.tsmi_open_ast.Name = "tsmi_open_ast";
            this.tsmi_open_ast.Size = new System.Drawing.Size(268, 22);
            this.tsmi_open_ast.Text = "Open &AST File";
            this.tsmi_open_ast.Visible = false;
            this.tsmi_open_ast.Click += new System.EventHandler(this.tsmi_open_ast_Click);
            // 
            // tsmi_open_work_folder
            // 
            this.tsmi_open_work_folder.Name = "tsmi_open_work_folder";
            this.tsmi_open_work_folder.Size = new System.Drawing.Size(268, 22);
            this.tsmi_open_work_folder.Text = "Open Working Folder";
            this.tsmi_open_work_folder.Visible = false;
            this.tsmi_open_work_folder.Click += new System.EventHandler(this.tsmi_Working_Folder_Click);
            // 
            // tsmi_openSAPDataFile
            // 
            this.tsmi_openSAPDataFile.Name = "tsmi_openSAPDataFile";
            this.tsmi_openSAPDataFile.Size = new System.Drawing.Size(268, 22);
            this.tsmi_openSAPDataFile.Text = "Open Analysis SAP Data File";
            this.tsmi_openSAPDataFile.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_openStructureModelDrawingFile
            // 
            this.tsmi_openStructureModelDrawingFile.Name = "tsmi_openStructureModelDrawingFile";
            this.tsmi_openStructureModelDrawingFile.Size = new System.Drawing.Size(268, 22);
            this.tsmi_openStructureModelDrawingFile.Text = "Open Structure Model Drawing File";
            this.tsmi_openStructureModelDrawingFile.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_openAnalysisExampleTXTDataFile
            // 
            this.tsmi_openAnalysisExampleTXTDataFile.Name = "tsmi_openAnalysisExampleTXTDataFile";
            this.tsmi_openAnalysisExampleTXTDataFile.Size = new System.Drawing.Size(268, 22);
            this.tsmi_openAnalysisExampleTXTDataFile.Text = "Open Analysis Example Data File";
            this.tsmi_openAnalysisExampleTXTDataFile.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_structure_text
            // 
            this.tsmi_structure_text.Name = "tsmi_structure_text";
            this.tsmi_structure_text.Size = new System.Drawing.Size(268, 22);
            this.tsmi_structure_text.Text = "Structural Analysis";
            // 
            // tsmi_structure_sap
            // 
            this.tsmi_structure_sap.Name = "tsmi_structure_sap";
            this.tsmi_structure_sap.Size = new System.Drawing.Size(268, 22);
            this.tsmi_structure_sap.Text = "Structural Analysis SAP Data";
            this.tsmi_structure_sap.Visible = false;
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(265, 6);
            // 
            // tsmi_openStageAnalysisTEXTDataFile
            // 
            this.tsmi_openStageAnalysisTEXTDataFile.Name = "tsmi_openStageAnalysisTEXTDataFile";
            this.tsmi_openStageAnalysisTEXTDataFile.Size = new System.Drawing.Size(268, 22);
            this.tsmi_openStageAnalysisTEXTDataFile.Text = "Open Stage Analysis TEXT Data File";
            this.tsmi_openStageAnalysisTEXTDataFile.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(265, 6);
            // 
            // tsmi_saveTXTDataFile
            // 
            this.tsmi_saveTXTDataFile.Name = "tsmi_saveTXTDataFile";
            this.tsmi_saveTXTDataFile.Size = new System.Drawing.Size(268, 22);
            this.tsmi_saveTXTDataFile.Text = "Save (TXT) Data File";
            this.tsmi_saveTXTDataFile.Visible = false;
            this.tsmi_saveTXTDataFile.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_openTXTDataInNotepad
            // 
            this.tsmi_openTXTDataInNotepad.Name = "tsmi_openTXTDataInNotepad";
            this.tsmi_openTXTDataInNotepad.Size = new System.Drawing.Size(268, 22);
            this.tsmi_openTXTDataInNotepad.Text = "Open TEXT Data in Notepad";
            this.tsmi_openTXTDataInNotepad.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(265, 6);
            // 
            // tsmi_openMSExcelDesignReport
            // 
            this.tsmi_openMSExcelDesignReport.Name = "tsmi_openMSExcelDesignReport";
            this.tsmi_openMSExcelDesignReport.Size = new System.Drawing.Size(268, 22);
            this.tsmi_openMSExcelDesignReport.Text = "Open MS Excel Design Report";
            this.tsmi_openMSExcelDesignReport.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // openASTRAWorksheetDesignToolStripMenuItem
            // 
            this.openASTRAWorksheetDesignToolStripMenuItem.Name = "openASTRAWorksheetDesignToolStripMenuItem";
            this.openASTRAWorksheetDesignToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.openASTRAWorksheetDesignToolStripMenuItem.Text = "Open User\'s saved Worksheet Design";
            this.openASTRAWorksheetDesignToolStripMenuItem.Visible = false;
            this.openASTRAWorksheetDesignToolStripMenuItem.Click += new System.EventHandler(this.tsmi_open_ASTRA_worksheet_design_Click);
            // 
            // tsmi_Create_New_AST_File
            // 
            this.tsmi_Create_New_AST_File.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsmi_Create_New_AST_File.Name = "tsmi_Create_New_AST_File";
            this.tsmi_Create_New_AST_File.Size = new System.Drawing.Size(268, 22);
            this.tsmi_Create_New_AST_File.Text = "Create &New AST File";
            this.tsmi_Create_New_AST_File.Visible = false;
            this.tsmi_Create_New_AST_File.Click += new System.EventHandler(this.tsmi_Create_New_AST_File_Click);
            // 
            // tsmi_ast_data
            // 
            this.tsmi_ast_data.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ast_basic_info,
            this.tsmi_ast_analysis_type,
            this.tsmi_ast_nodes,
            this.tsmi_ast_elements,
            this.tsmi_ast_supports,
            this.tsmi_ast_nodal_loads,
            this.tsmi_ast_self_weight});
            this.tsmi_ast_data.Name = "tsmi_ast_data";
            this.tsmi_ast_data.Size = new System.Drawing.Size(268, 22);
            this.tsmi_ast_data.Text = "Create &New AST File";
            this.tsmi_ast_data.Visible = false;
            // 
            // tsmi_ast_basic_info
            // 
            this.tsmi_ast_basic_info.Name = "tsmi_ast_basic_info";
            this.tsmi_ast_basic_info.Size = new System.Drawing.Size(170, 22);
            this.tsmi_ast_basic_info.Text = "Basic &Info...";
            this.tsmi_ast_basic_info.Click += new System.EventHandler(this.tsmi_ast_basic_info_Click);
            // 
            // tsmi_ast_analysis_type
            // 
            this.tsmi_ast_analysis_type.Name = "tsmi_ast_analysis_type";
            this.tsmi_ast_analysis_type.Size = new System.Drawing.Size(170, 22);
            this.tsmi_ast_analysis_type.Text = "Analysis &Type...";
            this.tsmi_ast_analysis_type.Click += new System.EventHandler(this.tsmi_ast_basic_info_Click);
            // 
            // tsmi_ast_nodes
            // 
            this.tsmi_ast_nodes.Name = "tsmi_ast_nodes";
            this.tsmi_ast_nodes.Size = new System.Drawing.Size(170, 22);
            this.tsmi_ast_nodes.Text = "&Nodes...";
            this.tsmi_ast_nodes.Click += new System.EventHandler(this.tsmi_ast_basic_info_Click);
            // 
            // tsmi_ast_elements
            // 
            this.tsmi_ast_elements.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ast_beam_truss,
            this.tsmi_ast_plate});
            this.tsmi_ast_elements.Name = "tsmi_ast_elements";
            this.tsmi_ast_elements.Size = new System.Drawing.Size(170, 22);
            this.tsmi_ast_elements.Text = "&Elements";
            // 
            // tsmi_ast_beam_truss
            // 
            this.tsmi_ast_beam_truss.Name = "tsmi_ast_beam_truss";
            this.tsmi_ast_beam_truss.Size = new System.Drawing.Size(239, 22);
            this.tsmi_ast_beam_truss.Text = "&1:3D Beam And Truss Members";
            this.tsmi_ast_beam_truss.Click += new System.EventHandler(this.tsmi_ast_basic_info_Click);
            // 
            // tsmi_ast_plate
            // 
            this.tsmi_ast_plate.Name = "tsmi_ast_plate";
            this.tsmi_ast_plate.Size = new System.Drawing.Size(239, 22);
            this.tsmi_ast_plate.Text = "&2:Plate And Shell Elements";
            this.tsmi_ast_plate.Click += new System.EventHandler(this.tsmi_ast_basic_info_Click);
            // 
            // tsmi_ast_supports
            // 
            this.tsmi_ast_supports.Name = "tsmi_ast_supports";
            this.tsmi_ast_supports.Size = new System.Drawing.Size(170, 22);
            this.tsmi_ast_supports.Text = "&Support";
            this.tsmi_ast_supports.Click += new System.EventHandler(this.tsmi_ast_basic_info_Click);
            // 
            // tsmi_ast_nodal_loads
            // 
            this.tsmi_ast_nodal_loads.Name = "tsmi_ast_nodal_loads";
            this.tsmi_ast_nodal_loads.Size = new System.Drawing.Size(170, 22);
            this.tsmi_ast_nodal_loads.Text = "Nodal &Loads";
            this.tsmi_ast_nodal_loads.Click += new System.EventHandler(this.tsmi_ast_basic_info_Click);
            // 
            // tsmi_ast_self_weight
            // 
            this.tsmi_ast_self_weight.Name = "tsmi_ast_self_weight";
            this.tsmi_ast_self_weight.Size = new System.Drawing.Size(170, 22);
            this.tsmi_ast_self_weight.Text = "Self &Weight Factor";
            this.tsmi_ast_self_weight.Click += new System.EventHandler(this.tsmi_ast_basic_info_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Enabled = false;
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(268, 22);
            this.saveToolStripMenuItem1.Text = "Save AST File";
            this.saveToolStripMenuItem1.Visible = false;
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // saveAsToolStripMenuItem2
            // 
            this.saveAsToolStripMenuItem2.Enabled = false;
            this.saveAsToolStripMenuItem2.Name = "saveAsToolStripMenuItem2";
            this.saveAsToolStripMenuItem2.Size = new System.Drawing.Size(268, 22);
            this.saveAsToolStripMenuItem2.Text = "Save AST File As...";
            this.saveAsToolStripMenuItem2.Visible = false;
            this.saveAsToolStripMenuItem2.Click += new System.EventHandler(this.saveAsToolStripMenuItem2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(265, 6);
            // 
            // recentFilesToolStripMenuItem
            // 
            this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.recentFilesToolStripMenuItem.Text = "&Recent Files";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(265, 6);
            // 
            // tsmi_license
            // 
            this.tsmi_license.Name = "tsmi_license";
            this.tsmi_license.Size = new System.Drawing.Size(268, 22);
            this.tsmi_license.Text = "Enter Authorization Code";
            this.tsmi_license.Click += new System.EventHandler(this.enterRenewalCodeToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(265, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tsmi_process_analysis
            // 
            this.tsmi_process_analysis.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_viewInputData,
            this.tsmi_runTXTFile,
            this.tsmi_processAnalysisASTFile,
            this.tsmi_convertToASTFile,
            this.tsmi_viewAnalysisASTFile,
            this.tsmi_analysisInputDataFile,
            this.tsmi_analysisProcessResults,
            this.tsmi_workingFolder,
            this.tsmi_ana_text_file,
            this.tsmi_ana_dwg_file,
            this.tsmi_stage_ana,
            this.tsmi_ana_sap_file,
            this.tsmi_ana_exmpls});
            this.tsmi_process_analysis.Name = "tsmi_process_analysis";
            this.tsmi_process_analysis.Size = new System.Drawing.Size(105, 20);
            this.tsmi_process_analysis.Text = "&Process Analysis";
            // 
            // tsmi_viewInputData
            // 
            this.tsmi_viewInputData.Name = "tsmi_viewInputData";
            this.tsmi_viewInputData.Size = new System.Drawing.Size(252, 22);
            this.tsmi_viewInputData.Text = "&View Analysis TXT File";
            this.tsmi_viewInputData.Visible = false;
            this.tsmi_viewInputData.Click += new System.EventHandler(this.viewInputDataToolStripMenuItem_Click);
            // 
            // tsmi_runTXTFile
            // 
            this.tsmi_runTXTFile.Name = "tsmi_runTXTFile";
            this.tsmi_runTXTFile.Size = new System.Drawing.Size(252, 22);
            this.tsmi_runTXTFile.Text = "&Process Analysis TXT File";
            this.tsmi_runTXTFile.Visible = false;
            this.tsmi_runTXTFile.Click += new System.EventHandler(this.runTXTFileToolStripMenuItem_Click);
            // 
            // tsmi_processAnalysisASTFile
            // 
            this.tsmi_processAnalysisASTFile.Name = "tsmi_processAnalysisASTFile";
            this.tsmi_processAnalysisASTFile.Size = new System.Drawing.Size(252, 22);
            this.tsmi_processAnalysisASTFile.Text = "&Process Analysis AST File";
            this.tsmi_processAnalysisASTFile.Visible = false;
            this.tsmi_processAnalysisASTFile.Click += new System.EventHandler(this.runASTFileToolStripMenuItem_Click);
            // 
            // tsmi_convertToASTFile
            // 
            this.tsmi_convertToASTFile.Name = "tsmi_convertToASTFile";
            this.tsmi_convertToASTFile.Size = new System.Drawing.Size(252, 22);
            this.tsmi_convertToASTFile.Text = "&Convert TXT to AST File";
            this.tsmi_convertToASTFile.Visible = false;
            this.tsmi_convertToASTFile.Click += new System.EventHandler(this.convertToASTFileToolStripMenuItem_Click);
            // 
            // tsmi_viewAnalysisASTFile
            // 
            this.tsmi_viewAnalysisASTFile.Name = "tsmi_viewAnalysisASTFile";
            this.tsmi_viewAnalysisASTFile.Size = new System.Drawing.Size(252, 22);
            this.tsmi_viewAnalysisASTFile.Text = "&View Analysis AST File";
            this.tsmi_viewAnalysisASTFile.Visible = false;
            this.tsmi_viewAnalysisASTFile.Click += new System.EventHandler(this.viewInputDataToolStripMenuItem_Click);
            // 
            // tsmi_analysisInputDataFile
            // 
            this.tsmi_analysisInputDataFile.Name = "tsmi_analysisInputDataFile";
            this.tsmi_analysisInputDataFile.Size = new System.Drawing.Size(252, 22);
            this.tsmi_analysisInputDataFile.Text = "Analysis Input Data File";
            this.tsmi_analysisInputDataFile.Visible = false;
            this.tsmi_analysisInputDataFile.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_analysisProcessResults
            // 
            this.tsmi_analysisProcessResults.Name = "tsmi_analysisProcessResults";
            this.tsmi_analysisProcessResults.Size = new System.Drawing.Size(252, 22);
            this.tsmi_analysisProcessResults.Text = "Analysis Process && Results";
            this.tsmi_analysisProcessResults.Visible = false;
            this.tsmi_analysisProcessResults.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_workingFolder
            // 
            this.tsmi_workingFolder.Name = "tsmi_workingFolder";
            this.tsmi_workingFolder.Size = new System.Drawing.Size(252, 22);
            this.tsmi_workingFolder.Text = "Select Working Folder";
            this.tsmi_workingFolder.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_ana_text_file
            // 
            this.tsmi_ana_text_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ana_inp_text,
            this.tsmi_ana_res_text,
            this.tsmi_ana_view_text});
            this.tsmi_ana_text_file.Name = "tsmi_ana_text_file";
            this.tsmi_ana_text_file.Size = new System.Drawing.Size(252, 22);
            this.tsmi_ana_text_file.Text = "Analysis by TEXT Data File";
            this.tsmi_ana_text_file.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_ana_inp_text
            // 
            this.tsmi_ana_inp_text.Name = "tsmi_ana_inp_text";
            this.tsmi_ana_inp_text.Size = new System.Drawing.Size(213, 22);
            this.tsmi_ana_inp_text.Text = "Analysis Input Data File";
            this.tsmi_ana_inp_text.Visible = false;
            this.tsmi_ana_inp_text.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_ana_res_text
            // 
            this.tsmi_ana_res_text.Name = "tsmi_ana_res_text";
            this.tsmi_ana_res_text.Size = new System.Drawing.Size(213, 22);
            this.tsmi_ana_res_text.Text = "Analysis Process && Results";
            this.tsmi_ana_res_text.Visible = false;
            this.tsmi_ana_res_text.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_ana_view_text
            // 
            this.tsmi_ana_view_text.Name = "tsmi_ana_view_text";
            this.tsmi_ana_view_text.Size = new System.Drawing.Size(213, 22);
            this.tsmi_ana_view_text.Text = "View Analysis TEXT File";
            this.tsmi_ana_view_text.Visible = false;
            this.tsmi_ana_view_text.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_ana_dwg_file
            // 
            this.tsmi_ana_dwg_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ana_inp_dwg,
            this.tsmi_ana_res_dwg});
            this.tsmi_ana_dwg_file.Name = "tsmi_ana_dwg_file";
            this.tsmi_ana_dwg_file.Size = new System.Drawing.Size(252, 22);
            this.tsmi_ana_dwg_file.Text = "Analysis by Structure Drawing File";
            this.tsmi_ana_dwg_file.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_ana_inp_dwg
            // 
            this.tsmi_ana_inp_dwg.Name = "tsmi_ana_inp_dwg";
            this.tsmi_ana_inp_dwg.Size = new System.Drawing.Size(213, 22);
            this.tsmi_ana_inp_dwg.Text = "Analysis Input Data";
            this.tsmi_ana_inp_dwg.Visible = false;
            this.tsmi_ana_inp_dwg.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_ana_res_dwg
            // 
            this.tsmi_ana_res_dwg.Name = "tsmi_ana_res_dwg";
            this.tsmi_ana_res_dwg.Size = new System.Drawing.Size(213, 22);
            this.tsmi_ana_res_dwg.Text = "Analysis Process && Results";
            this.tsmi_ana_res_dwg.Visible = false;
            this.tsmi_ana_res_dwg.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_stage_ana
            // 
            this.tsmi_stage_ana.Name = "tsmi_stage_ana";
            this.tsmi_stage_ana.Size = new System.Drawing.Size(252, 22);
            this.tsmi_stage_ana.Text = "&Stage Analysis";
            this.tsmi_stage_ana.Click += new System.EventHandler(this.pDeltaAnalysisToolStripMenuItem_Click);
            // 
            // tsmi_ana_sap_file
            // 
            this.tsmi_ana_sap_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ana_inp_sap,
            this.tsmi_ana_res_sap});
            this.tsmi_ana_sap_file.Name = "tsmi_ana_sap_file";
            this.tsmi_ana_sap_file.Size = new System.Drawing.Size(252, 22);
            this.tsmi_ana_sap_file.Text = "Analysis by SAP Data File";
            this.tsmi_ana_sap_file.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_ana_inp_sap
            // 
            this.tsmi_ana_inp_sap.Name = "tsmi_ana_inp_sap";
            this.tsmi_ana_inp_sap.Size = new System.Drawing.Size(213, 22);
            this.tsmi_ana_inp_sap.Text = "Analysis Input Data File";
            this.tsmi_ana_inp_sap.Visible = false;
            this.tsmi_ana_inp_sap.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_ana_res_sap
            // 
            this.tsmi_ana_res_sap.Name = "tsmi_ana_res_sap";
            this.tsmi_ana_res_sap.Size = new System.Drawing.Size(213, 22);
            this.tsmi_ana_res_sap.Text = "Analysis Process && Results";
            this.tsmi_ana_res_sap.Visible = false;
            this.tsmi_ana_res_sap.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_ana_exmpls
            // 
            this.tsmi_ana_exmpls.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ana_inp_exm,
            this.tsmi_ana_res_exm});
            this.tsmi_ana_exmpls.Name = "tsmi_ana_exmpls";
            this.tsmi_ana_exmpls.Size = new System.Drawing.Size(252, 22);
            this.tsmi_ana_exmpls.Text = "Analysis by Examples Data File";
            this.tsmi_ana_exmpls.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_ana_inp_exm
            // 
            this.tsmi_ana_inp_exm.Name = "tsmi_ana_inp_exm";
            this.tsmi_ana_inp_exm.Size = new System.Drawing.Size(213, 22);
            this.tsmi_ana_inp_exm.Text = "Analysis Input Data File";
            this.tsmi_ana_inp_exm.Visible = false;
            this.tsmi_ana_inp_exm.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // tsmi_ana_res_exm
            // 
            this.tsmi_ana_res_exm.Name = "tsmi_ana_res_exm";
            this.tsmi_ana_res_exm.Size = new System.Drawing.Size(213, 22);
            this.tsmi_ana_res_exm.Text = "Analysis Process && Results";
            this.tsmi_ana_res_exm.Visible = false;
            this.tsmi_ana_res_exm.Click += new System.EventHandler(this.tsmi_newAnalysisDataFile_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Checked = true;
            this.viewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbarToolStripMenuItem,
            this.statusBarToolStripMenuItem});
            this.viewToolStripMenuItem.Enabled = false;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Visible = false;
            // 
            // toolbarToolStripMenuItem
            // 
            this.toolbarToolStripMenuItem.Name = "toolbarToolStripMenuItem";
            this.toolbarToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.toolbarToolStripMenuItem.Text = "Toolbar";
            this.toolbarToolStripMenuItem.Click += new System.EventHandler(this.toolbarToolStripMenuItem_Click);
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.statusBarToolStripMenuItem.Text = "Status Bar";
            this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.statusBarToolStripMenuItem_Click);
            // 
            // processAstFileToolStripMenuItem
            // 
            this.processAstFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runASTFileToolStripMenuItem,
            this.astDataInputToolStripMenuItem});
            this.processAstFileToolStripMenuItem.Enabled = false;
            this.processAstFileToolStripMenuItem.Name = "processAstFileToolStripMenuItem";
            this.processAstFileToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.processAstFileToolStripMenuItem.Text = "Process &AST File";
            this.processAstFileToolStripMenuItem.Visible = false;
            this.processAstFileToolStripMenuItem.Click += new System.EventHandler(this.processAstFileToolStripMenuItem_Click);
            // 
            // runASTFileToolStripMenuItem
            // 
            this.runASTFileToolStripMenuItem.Name = "runASTFileToolStripMenuItem";
            this.runASTFileToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.runASTFileToolStripMenuItem.Text = "Run A&ST File";
            this.runASTFileToolStripMenuItem.Click += new System.EventHandler(this.runASTFileToolStripMenuItem_Click);
            // 
            // astDataInputToolStripMenuItem
            // 
            this.astDataInputToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.basicInfoToolStripMenuItem,
            this.aToolStripMenuItem,
            this.nodesToolStripMenuItem,
            this.elementsToolStripMenuItem,
            this.supportToolStripMenuItem1,
            this.nodalLoadsToolStripMenuItem,
            this.toolStripMenuItem1});
            this.astDataInputToolStripMenuItem.Name = "astDataInputToolStripMenuItem";
            this.astDataInputToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.astDataInputToolStripMenuItem.Text = "AST &Data-Input";
            // 
            // basicInfoToolStripMenuItem
            // 
            this.basicInfoToolStripMenuItem.Name = "basicInfoToolStripMenuItem";
            this.basicInfoToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.basicInfoToolStripMenuItem.Text = "Basic &Info...";
            this.basicInfoToolStripMenuItem.Click += new System.EventHandler(this.basicInfoToolStripMenuItem_Click);
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.aToolStripMenuItem.Text = "Analysis &Type...";
            this.aToolStripMenuItem.Click += new System.EventHandler(this.aToolStripMenuItem_Click_1);
            // 
            // nodesToolStripMenuItem
            // 
            this.nodesToolStripMenuItem.Name = "nodesToolStripMenuItem";
            this.nodesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.nodesToolStripMenuItem.Text = "&Nodes...";
            this.nodesToolStripMenuItem.Click += new System.EventHandler(this.nodesToolStripMenuItem_Click_1);
            // 
            // elementsToolStripMenuItem
            // 
            this.elementsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.plateAndShellElementsToolStripMenuItem});
            this.elementsToolStripMenuItem.Name = "elementsToolStripMenuItem";
            this.elementsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.elementsToolStripMenuItem.Text = "&Elements";
            this.elementsToolStripMenuItem.Click += new System.EventHandler(this.elementsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(239, 22);
            this.toolStripMenuItem3.Text = "&1:3D Beam And Truss Members";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click_1);
            // 
            // plateAndShellElementsToolStripMenuItem
            // 
            this.plateAndShellElementsToolStripMenuItem.Name = "plateAndShellElementsToolStripMenuItem";
            this.plateAndShellElementsToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.plateAndShellElementsToolStripMenuItem.Text = "&2:Plate And Shell Elements";
            this.plateAndShellElementsToolStripMenuItem.Click += new System.EventHandler(this.plateAndShellElementsToolStripMenuItem_Click_1);
            // 
            // supportToolStripMenuItem1
            // 
            this.supportToolStripMenuItem1.Name = "supportToolStripMenuItem1";
            this.supportToolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
            this.supportToolStripMenuItem1.Text = "&Support";
            this.supportToolStripMenuItem1.Click += new System.EventHandler(this.supportToolStripMenuItem1_Click_1);
            // 
            // nodalLoadsToolStripMenuItem
            // 
            this.nodalLoadsToolStripMenuItem.Name = "nodalLoadsToolStripMenuItem";
            this.nodalLoadsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.nodalLoadsToolStripMenuItem.Text = "Nodal &Loads";
            this.nodalLoadsToolStripMenuItem.Click += new System.EventHandler(this.nodalLoadsToolStripMenuItem_Click_1);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItem1.Text = "Self &Weight Factor";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // tsmi_Process_Design
            // 
            this.tsmi_Process_Design.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_selectWorkingFolder,
            this.tsmi_selectDesignStandard,
            this.tsmi_Bridge_Design,
            this.tsmi_RCC_Structural_Design,
            this.tsmi_streamHydrology,
            this.tsmi_structureModeling});
            this.tsmi_Process_Design.Name = "tsmi_Process_Design";
            this.tsmi_Process_Design.Size = new System.Drawing.Size(98, 20);
            this.tsmi_Process_Design.Text = "Process &Design";
            // 
            // tsmi_selectWorkingFolder
            // 
            this.tsmi_selectWorkingFolder.Name = "tsmi_selectWorkingFolder";
            this.tsmi_selectWorkingFolder.Size = new System.Drawing.Size(244, 22);
            this.tsmi_selectWorkingFolder.Text = "Select Working Folder";
            this.tsmi_selectWorkingFolder.Click += new System.EventHandler(this.tsmi_Working_Folder_Click);
            // 
            // tsmi_selectDesignStandard
            // 
            this.tsmi_selectDesignStandard.Name = "tsmi_selectDesignStandard";
            this.tsmi_selectDesignStandard.Size = new System.Drawing.Size(244, 22);
            this.tsmi_selectDesignStandard.Text = "Select Design Standard";
            this.tsmi_selectDesignStandard.Click += new System.EventHandler(this.tsmi_selectDesignStandard_Click);
            // 
            // tsmi_Bridge_Design
            // 
            this.tsmi_Bridge_Design.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Working_Folder,
            this.tsmi_RCC_TGirder,
            this.toolStripSeparator10,
            this.tsmi_Composite,
            this.toolStripSeparator11,
            this.tsmi_PSC_IGirder,
            this.toolStripSeparator12,
            this.tsmi_PSC_Box_Girder_Bridge,
            this.tsmi_cont_PSC_Box_Girder_Bridge,
            this.toolStripSeparator13,
            this.tsmi_Steel_Truss_Bridge_Warren_1,
            this.tsmi_Steel_Truss_Bridge_Warren_2,
            this.tsmi_Steel_Truss_Bridge_Warren_3,
            this.tsmi_Steel_Truss_Bridge_K_Type,
            this.toolStripSeparator14,
            this.tsmi_arch_cable_bridge,
            this.tsmi_arch_steel_bridge,
            this.tsmi_cable_suspension_bridge,
            this.toolStripSeparator21,
            this.tsmi_cable_stayed_bridge,
            this.tsmi_extradossed,
            this.toolStripSeparator15,
            this.tsmi_Rail_Bridge,
            this.tsmi_Underpass,
            this.tsmi_GADs_Underpasses,
            this.toolStripSeparator20,
            this.tsmi_minor_Bridge,
            this.toolStripSeparator19,
            this.tsmi_RCC_Culverts,
            this.toolStripSeparator16,
            this.tsmi_Bridge_Abutment_Design,
            this.tsmi_Bridge_Pier_Design,
            this.tsmi_Bridge_Pier_Pile_Design,
            this.toolStripSeparator17,
            this.tsmi_RE_Wall_Design,
            this.tsmi_Bridge_Bearing_Design,
            this.tsmi_Bridge_Foundation,
            this.toolStripSeparator18,
            this.tsmi_Hydraulic_Calculations,
            this.tsmi_Material_Properties1,
            this.tsmi_CostEstimation});
            this.tsmi_Bridge_Design.Name = "tsmi_Bridge_Design";
            this.tsmi_Bridge_Design.Size = new System.Drawing.Size(244, 22);
            this.tsmi_Bridge_Design.Text = "Bridge Design";
            // 
            // tsmi_Working_Folder
            // 
            this.tsmi_Working_Folder.Name = "tsmi_Working_Folder";
            this.tsmi_Working_Folder.Size = new System.Drawing.Size(385, 22);
            this.tsmi_Working_Folder.Text = "Select Working Folder";
            this.tsmi_Working_Folder.Visible = false;
            this.tsmi_Working_Folder.Click += new System.EventHandler(this.tsmi_Working_Folder_Click);
            // 
            // tsmi_RCC_TGirder
            // 
            this.tsmi_RCC_TGirder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_RCC_TGirder_LSM,
            this.tsmi_RCC_TGirder_WSM});
            this.tsmi_RCC_TGirder.Name = "tsmi_RCC_TGirder";
            this.tsmi_RCC_TGirder.Size = new System.Drawing.Size(385, 22);
            this.tsmi_RCC_TGirder.Text = "Reinforced Cement Concrete (RCC) T-Girder Bridge";
            // 
            // tsmi_RCC_TGirder_LSM
            // 
            this.tsmi_RCC_TGirder_LSM.Name = "tsmi_RCC_TGirder_LSM";
            this.tsmi_RCC_TGirder_LSM.Size = new System.Drawing.Size(197, 22);
            this.tsmi_RCC_TGirder_LSM.Text = "Limit State Method";
            this.tsmi_RCC_TGirder_LSM.Click += new System.EventHandler(this.tsmi_NewMenu_Click);
            // 
            // tsmi_RCC_TGirder_WSM
            // 
            this.tsmi_RCC_TGirder_WSM.Name = "tsmi_RCC_TGirder_WSM";
            this.tsmi_RCC_TGirder_WSM.Size = new System.Drawing.Size(197, 22);
            this.tsmi_RCC_TGirder_WSM.Text = "Working Stress Method";
            this.tsmi_RCC_TGirder_WSM.Click += new System.EventHandler(this.tsmi_NewMenu_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(382, 6);
            // 
            // tsmi_Composite
            // 
            this.tsmi_Composite.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Composite_LSM,
            this.tsmi_Composite_WSM});
            this.tsmi_Composite.Name = "tsmi_Composite";
            this.tsmi_Composite.Size = new System.Drawing.Size(385, 22);
            this.tsmi_Composite.Text = "Steel Plate/Box Girder & RCC Slab Composite Bridge";
            // 
            // tsmi_Composite_LSM
            // 
            this.tsmi_Composite_LSM.Name = "tsmi_Composite_LSM";
            this.tsmi_Composite_LSM.Size = new System.Drawing.Size(197, 22);
            this.tsmi_Composite_LSM.Text = "Limit State Method";
            this.tsmi_Composite_LSM.Click += new System.EventHandler(this.tsmi_NewMenu_Click);
            // 
            // tsmi_Composite_WSM
            // 
            this.tsmi_Composite_WSM.Name = "tsmi_Composite_WSM";
            this.tsmi_Composite_WSM.Size = new System.Drawing.Size(197, 22);
            this.tsmi_Composite_WSM.Text = "Working Stress Method";
            this.tsmi_Composite_WSM.Click += new System.EventHandler(this.tsmi_NewMenu_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(382, 6);
            // 
            // tsmi_PSC_IGirder
            // 
            this.tsmi_PSC_IGirder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_PSC_IGirder_LSM,
            this.tsmi_PSC_IGirder_WSM});
            this.tsmi_PSC_IGirder.Name = "tsmi_PSC_IGirder";
            this.tsmi_PSC_IGirder.Size = new System.Drawing.Size(385, 22);
            this.tsmi_PSC_IGirder.Text = "Pre Stressed Concrete (PSC) I-Girder Bridge";
            // 
            // tsmi_PSC_IGirder_LSM
            // 
            this.tsmi_PSC_IGirder_LSM.Name = "tsmi_PSC_IGirder_LSM";
            this.tsmi_PSC_IGirder_LSM.Size = new System.Drawing.Size(197, 22);
            this.tsmi_PSC_IGirder_LSM.Text = "Limit State Method";
            this.tsmi_PSC_IGirder_LSM.Click += new System.EventHandler(this.tsmi_NewMenu_Click);
            // 
            // tsmi_PSC_IGirder_WSM
            // 
            this.tsmi_PSC_IGirder_WSM.Name = "tsmi_PSC_IGirder_WSM";
            this.tsmi_PSC_IGirder_WSM.Size = new System.Drawing.Size(197, 22);
            this.tsmi_PSC_IGirder_WSM.Text = "Working Stress Method";
            this.tsmi_PSC_IGirder_WSM.Click += new System.EventHandler(this.tsmi_NewMenu_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(382, 6);
            // 
            // tsmi_PSC_Box_Girder_Bridge
            // 
            this.tsmi_PSC_Box_Girder_Bridge.Name = "tsmi_PSC_Box_Girder_Bridge";
            this.tsmi_PSC_Box_Girder_Bridge.Size = new System.Drawing.Size(385, 22);
            this.tsmi_PSC_Box_Girder_Bridge.Text = "Pre Stressed Concrete (PSC) BOX-Girder Bridge";
            this.tsmi_PSC_Box_Girder_Bridge.Click += new System.EventHandler(this.tsmi_PSC_Box_Girder_Bridge_Click);
            // 
            // tsmi_cont_PSC_Box_Girder_Bridge
            // 
            this.tsmi_cont_PSC_Box_Girder_Bridge.Name = "tsmi_cont_PSC_Box_Girder_Bridge";
            this.tsmi_cont_PSC_Box_Girder_Bridge.Size = new System.Drawing.Size(385, 22);
            this.tsmi_cont_PSC_Box_Girder_Bridge.Text = "Continuous Pre Stressed Concrete (PSC) BOX-Girder Bridge";
            this.tsmi_cont_PSC_Box_Girder_Bridge.Click += new System.EventHandler(this.tsmi_cont_PSC_Box_Girder_Bridge_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(382, 6);
            // 
            // tsmi_Steel_Truss_Bridge_Warren_1
            // 
            this.tsmi_Steel_Truss_Bridge_Warren_1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.completeDesignToolStripMenuItem,
            this.deckSlabDesignToolStripMenuItem});
            this.tsmi_Steel_Truss_Bridge_Warren_1.Name = "tsmi_Steel_Truss_Bridge_Warren_1";
            this.tsmi_Steel_Truss_Bridge_Warren_1.Size = new System.Drawing.Size(385, 22);
            this.tsmi_Steel_Truss_Bridge_Warren_1.Text = "Steel Truss Bridge Warren 1";
            this.tsmi_Steel_Truss_Bridge_Warren_1.Click += new System.EventHandler(this.tsmi_Steel_Truss_Bridge_Warren_Click);
            // 
            // completeDesignToolStripMenuItem
            // 
            this.completeDesignToolStripMenuItem.Name = "completeDesignToolStripMenuItem";
            this.completeDesignToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.completeDesignToolStripMenuItem.Text = "Complete Design";
            this.completeDesignToolStripMenuItem.Visible = false;
            this.completeDesignToolStripMenuItem.Click += new System.EventHandler(this.completeDesignToolStripMenuItem_Click);
            // 
            // deckSlabDesignToolStripMenuItem
            // 
            this.deckSlabDesignToolStripMenuItem.Name = "deckSlabDesignToolStripMenuItem";
            this.deckSlabDesignToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.deckSlabDesignToolStripMenuItem.Text = "Deck Slab Design";
            this.deckSlabDesignToolStripMenuItem.Visible = false;
            this.deckSlabDesignToolStripMenuItem.Click += new System.EventHandler(this.deckSlabDesignToolStripMenuItem_Click);
            // 
            // tsmi_Steel_Truss_Bridge_Warren_2
            // 
            this.tsmi_Steel_Truss_Bridge_Warren_2.Name = "tsmi_Steel_Truss_Bridge_Warren_2";
            this.tsmi_Steel_Truss_Bridge_Warren_2.Size = new System.Drawing.Size(385, 22);
            this.tsmi_Steel_Truss_Bridge_Warren_2.Text = "Steel Truss Bridge Warren 2";
            this.tsmi_Steel_Truss_Bridge_Warren_2.Click += new System.EventHandler(this.tsmi_Steel_Truss_Bridge_Warren_Click);
            // 
            // tsmi_Steel_Truss_Bridge_Warren_3
            // 
            this.tsmi_Steel_Truss_Bridge_Warren_3.Name = "tsmi_Steel_Truss_Bridge_Warren_3";
            this.tsmi_Steel_Truss_Bridge_Warren_3.Size = new System.Drawing.Size(385, 22);
            this.tsmi_Steel_Truss_Bridge_Warren_3.Text = "Steel Truss Bridge Warren 3";
            this.tsmi_Steel_Truss_Bridge_Warren_3.Click += new System.EventHandler(this.tsmi_Steel_Truss_Bridge_Warren_Click);
            // 
            // tsmi_Steel_Truss_Bridge_K_Type
            // 
            this.tsmi_Steel_Truss_Bridge_K_Type.Name = "tsmi_Steel_Truss_Bridge_K_Type";
            this.tsmi_Steel_Truss_Bridge_K_Type.Size = new System.Drawing.Size(385, 22);
            this.tsmi_Steel_Truss_Bridge_K_Type.Text = "Steel Truss Bridge K Type";
            this.tsmi_Steel_Truss_Bridge_K_Type.Click += new System.EventHandler(this.tsmi_Steel_Truss_Bridge_Warren_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(382, 6);
            // 
            // tsmi_arch_cable_bridge
            // 
            this.tsmi_arch_cable_bridge.Name = "tsmi_arch_cable_bridge";
            this.tsmi_arch_cable_bridge.Size = new System.Drawing.Size(385, 22);
            this.tsmi_arch_cable_bridge.Text = "Arch Cable Suspension Bridge ";
            this.tsmi_arch_cable_bridge.Click += new System.EventHandler(this.tsmi_arch_cable_bridge_Click);
            // 
            // tsmi_arch_steel_bridge
            // 
            this.tsmi_arch_steel_bridge.Name = "tsmi_arch_steel_bridge";
            this.tsmi_arch_steel_bridge.Size = new System.Drawing.Size(385, 22);
            this.tsmi_arch_steel_bridge.Text = "Arch Steel Suspension Bridge";
            this.tsmi_arch_steel_bridge.Visible = false;
            this.tsmi_arch_steel_bridge.Click += new System.EventHandler(this.tsmi_arch_cable_bridge_Click);
            // 
            // tsmi_cable_suspension_bridge
            // 
            this.tsmi_cable_suspension_bridge.Name = "tsmi_cable_suspension_bridge";
            this.tsmi_cable_suspension_bridge.Size = new System.Drawing.Size(385, 22);
            this.tsmi_cable_suspension_bridge.Text = "Cable Suspension Bridge";
            this.tsmi_cable_suspension_bridge.Click += new System.EventHandler(this.tsmi_cable_suspension_bridge_Click);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(382, 6);
            // 
            // tsmi_cable_stayed_bridge
            // 
            this.tsmi_cable_stayed_bridge.Name = "tsmi_cable_stayed_bridge";
            this.tsmi_cable_stayed_bridge.Size = new System.Drawing.Size(385, 22);
            this.tsmi_cable_stayed_bridge.Text = "Cable Stayed Bridge";
            this.tsmi_cable_stayed_bridge.Click += new System.EventHandler(this.tsmi_cable_stayed_bridge_Click);
            // 
            // tsmi_extradossed
            // 
            this.tsmi_extradossed.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_extradossed_side_towers,
            this.tsmi_extradossed_central_towers});
            this.tsmi_extradossed.Name = "tsmi_extradossed";
            this.tsmi_extradossed.Size = new System.Drawing.Size(385, 22);
            this.tsmi_extradossed.Text = "Extradossed Cable Stayed Bridge";
            // 
            // tsmi_extradossed_side_towers
            // 
            this.tsmi_extradossed_side_towers.Name = "tsmi_extradossed_side_towers";
            this.tsmi_extradossed_side_towers.Size = new System.Drawing.Size(250, 22);
            this.tsmi_extradossed_side_towers.Text = "Bridge with Towers on Either Side";
            this.tsmi_extradossed_side_towers.Click += new System.EventHandler(this.tsmi_cable_stayed_bridge_Click);
            // 
            // tsmi_extradossed_central_towers
            // 
            this.tsmi_extradossed_central_towers.Name = "tsmi_extradossed_central_towers";
            this.tsmi_extradossed_central_towers.Size = new System.Drawing.Size(250, 22);
            this.tsmi_extradossed_central_towers.Text = "Bridge with CentralTowers Only";
            this.tsmi_extradossed_central_towers.Click += new System.EventHandler(this.tsmi_cable_stayed_bridge_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(382, 6);
            // 
            // tsmi_Rail_Bridge
            // 
            this.tsmi_Rail_Bridge.Name = "tsmi_Rail_Bridge";
            this.tsmi_Rail_Bridge.Size = new System.Drawing.Size(385, 22);
            this.tsmi_Rail_Bridge.Text = "Railway Steel Plate Girder Bridge";
            this.tsmi_Rail_Bridge.Click += new System.EventHandler(this.tsmi_Rail_Bridge_Click);
            // 
            // tsmi_Underpass
            // 
            this.tsmi_Underpass.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_road_box_pushed_underpass,
            this.tsmi_rail_box_pushed_underpass});
            this.tsmi_Underpass.Name = "tsmi_Underpass";
            this.tsmi_Underpass.Size = new System.Drawing.Size(385, 22);
            this.tsmi_Underpass.Text = "RCC Box Underpass";
            // 
            // tsmi_road_box_pushed_underpass
            // 
            this.tsmi_road_box_pushed_underpass.Name = "tsmi_road_box_pushed_underpass";
            this.tsmi_road_box_pushed_underpass.Size = new System.Drawing.Size(228, 22);
            this.tsmi_road_box_pushed_underpass.Text = "RCC Box for Road Underpass ";
            this.tsmi_road_box_pushed_underpass.Click += new System.EventHandler(this.tsmi_Underpass_Click);
            // 
            // tsmi_rail_box_pushed_underpass
            // 
            this.tsmi_rail_box_pushed_underpass.Name = "tsmi_rail_box_pushed_underpass";
            this.tsmi_rail_box_pushed_underpass.Size = new System.Drawing.Size(228, 22);
            this.tsmi_rail_box_pushed_underpass.Text = "RCC Box for Rail Underpass";
            this.tsmi_rail_box_pushed_underpass.Click += new System.EventHandler(this.tsmi_Underpass_Click);
            // 
            // tsmi_GADs_Underpasses
            // 
            this.tsmi_GADs_Underpasses.Name = "tsmi_GADs_Underpasses";
            this.tsmi_GADs_Underpasses.Size = new System.Drawing.Size(385, 22);
            this.tsmi_GADs_Underpasses.Text = "GADs for Other Underpasses";
            this.tsmi_GADs_Underpasses.Click += new System.EventHandler(this.tsmi_Underpass_Click);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(382, 6);
            // 
            // tsmi_minor_Bridge
            // 
            this.tsmi_minor_Bridge.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_minor_Bridge_ls,
            this.tsmi_minor_Bridge_ws});
            this.tsmi_minor_Bridge.Name = "tsmi_minor_Bridge";
            this.tsmi_minor_Bridge.Size = new System.Drawing.Size(385, 22);
            this.tsmi_minor_Bridge.Text = "Minor Bridge / RCC Slab Bridge";
            // 
            // tsmi_minor_Bridge_ls
            // 
            this.tsmi_minor_Bridge_ls.Name = "tsmi_minor_Bridge_ls";
            this.tsmi_minor_Bridge_ls.Size = new System.Drawing.Size(197, 22);
            this.tsmi_minor_Bridge_ls.Text = "Limit State Method";
            this.tsmi_minor_Bridge_ls.Click += new System.EventHandler(this.tsmi_NewMenu_Click);
            // 
            // tsmi_minor_Bridge_ws
            // 
            this.tsmi_minor_Bridge_ws.Name = "tsmi_minor_Bridge_ws";
            this.tsmi_minor_Bridge_ws.Size = new System.Drawing.Size(197, 22);
            this.tsmi_minor_Bridge_ws.Text = "Working Stress Method";
            this.tsmi_minor_Bridge_ws.Click += new System.EventHandler(this.tsmi_NewMenu_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(382, 6);
            // 
            // tsmi_RCC_Culverts
            // 
            this.tsmi_RCC_Culverts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_RCC_Culverts_LS,
            this.tsmi_RCC_Culverts_WS});
            this.tsmi_RCC_Culverts.Name = "tsmi_RCC_Culverts";
            this.tsmi_RCC_Culverts.Size = new System.Drawing.Size(385, 22);
            this.tsmi_RCC_Culverts.Text = "RCC Culverts Design";
            // 
            // tsmi_RCC_Culverts_LS
            // 
            this.tsmi_RCC_Culverts_LS.Name = "tsmi_RCC_Culverts_LS";
            this.tsmi_RCC_Culverts_LS.Size = new System.Drawing.Size(197, 22);
            this.tsmi_RCC_Culverts_LS.Text = "Limit State Method";
            this.tsmi_RCC_Culverts_LS.Click += new System.EventHandler(this.tsmi_RCC_Culverts_Click);
            // 
            // tsmi_RCC_Culverts_WS
            // 
            this.tsmi_RCC_Culverts_WS.Name = "tsmi_RCC_Culverts_WS";
            this.tsmi_RCC_Culverts_WS.Size = new System.Drawing.Size(197, 22);
            this.tsmi_RCC_Culverts_WS.Text = "Working Stress Method";
            this.tsmi_RCC_Culverts_WS.Click += new System.EventHandler(this.tsmi_RCC_Culverts_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(382, 6);
            // 
            // tsmi_Bridge_Abutment_Design
            // 
            this.tsmi_Bridge_Abutment_Design.Name = "tsmi_Bridge_Abutment_Design";
            this.tsmi_Bridge_Abutment_Design.Size = new System.Drawing.Size(385, 22);
            this.tsmi_Bridge_Abutment_Design.Text = "Bridge Abutment Design";
            this.tsmi_Bridge_Abutment_Design.Click += new System.EventHandler(this.tsmi_Bridge_Abutment_Design_Click);
            // 
            // tsmi_Bridge_Pier_Design
            // 
            this.tsmi_Bridge_Pier_Design.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_pier_1,
            this.tsmi_RCC_Pier_LS,
            this.tsmi_pier_2,
            this.tsmi_pier_3,
            this.tsmi_pier_4,
            this.tsmi_pier_5});
            this.tsmi_Bridge_Pier_Design.Name = "tsmi_Bridge_Pier_Design";
            this.tsmi_Bridge_Pier_Design.Size = new System.Drawing.Size(385, 22);
            this.tsmi_Bridge_Pier_Design.Text = "Bridge Pier Design";
            // 
            // tsmi_pier_1
            // 
            this.tsmi_pier_1.Name = "tsmi_pier_1";
            this.tsmi_pier_1.Size = new System.Drawing.Size(325, 22);
            this.tsmi_pier_1.Text = "Design of RCC Pier with Analysis";
            this.tsmi_pier_1.Click += new System.EventHandler(this.tsmi_Bridge_Pier_Design_Click);
            // 
            // tsmi_RCC_Pier_LS
            // 
            this.tsmi_RCC_Pier_LS.Name = "tsmi_RCC_Pier_LS";
            this.tsmi_RCC_Pier_LS.Size = new System.Drawing.Size(325, 22);
            this.tsmi_RCC_Pier_LS.Text = "Design of RCC Pier in Limit State Method";
            this.tsmi_RCC_Pier_LS.Click += new System.EventHandler(this.tsmi_RCC_Pier_LS_Click);
            // 
            // tsmi_pier_2
            // 
            this.tsmi_pier_2.Name = "tsmi_pier_2";
            this.tsmi_pier_2.Size = new System.Drawing.Size(325, 22);
            this.tsmi_pier_2.Text = "Design of RCC Pier with Pile / Open Foundation";
            this.tsmi_pier_2.Click += new System.EventHandler(this.tsmi_Bridge_Pier_Design_Click);
            // 
            // tsmi_pier_3
            // 
            this.tsmi_pier_3.Name = "tsmi_pier_3";
            this.tsmi_pier_3.Size = new System.Drawing.Size(325, 22);
            this.tsmi_pier_3.Text = "Design of RCC Pier with Pile Foundation";
            this.tsmi_pier_3.Click += new System.EventHandler(this.tsmi_Bridge_Pier_Design_Click);
            // 
            // tsmi_pier_4
            // 
            this.tsmi_pier_4.Name = "tsmi_pier_4";
            this.tsmi_pier_4.Size = new System.Drawing.Size(325, 22);
            this.tsmi_pier_4.Text = "Design of RCC Circular Pier";
            this.tsmi_pier_4.Click += new System.EventHandler(this.tsmi_Bridge_Pier_Design_Click);
            // 
            // tsmi_pier_5
            // 
            this.tsmi_pier_5.Name = "tsmi_pier_5";
            this.tsmi_pier_5.Size = new System.Drawing.Size(325, 22);
            this.tsmi_pier_5.Text = "Design of Stone Masonry Pier ";
            this.tsmi_pier_5.Click += new System.EventHandler(this.tsmi_Bridge_Pier_Design_Click);
            // 
            // tsmi_Bridge_Pier_Pile_Design
            // 
            this.tsmi_Bridge_Pier_Pile_Design.Name = "tsmi_Bridge_Pier_Pile_Design";
            this.tsmi_Bridge_Pier_Pile_Design.Size = new System.Drawing.Size(385, 22);
            this.tsmi_Bridge_Pier_Pile_Design.Text = "Bridge Pier Design with Piles";
            this.tsmi_Bridge_Pier_Pile_Design.Visible = false;
            this.tsmi_Bridge_Pier_Pile_Design.Click += new System.EventHandler(this.tsmi_Bridge_Pier_Pile_Design_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(382, 6);
            // 
            // tsmi_RE_Wall_Design
            // 
            this.tsmi_RE_Wall_Design.Name = "tsmi_RE_Wall_Design";
            this.tsmi_RE_Wall_Design.Size = new System.Drawing.Size(385, 22);
            this.tsmi_RE_Wall_Design.Text = "RE (Reinforced Earth) Wall Design";
            this.tsmi_RE_Wall_Design.Visible = false;
            this.tsmi_RE_Wall_Design.Click += new System.EventHandler(this.tsmi_RE_Wall_Design_Click);
            // 
            // tsmi_Bridge_Bearing_Design
            // 
            this.tsmi_Bridge_Bearing_Design.Name = "tsmi_Bridge_Bearing_Design";
            this.tsmi_Bridge_Bearing_Design.Size = new System.Drawing.Size(385, 22);
            this.tsmi_Bridge_Bearing_Design.Text = "Bridge Bearing Design";
            this.tsmi_Bridge_Bearing_Design.Click += new System.EventHandler(this.bridgeBearingDesignToolStripMenuItem_Click);
            // 
            // tsmi_Bridge_Foundation
            // 
            this.tsmi_Bridge_Foundation.Name = "tsmi_Bridge_Foundation";
            this.tsmi_Bridge_Foundation.Size = new System.Drawing.Size(385, 22);
            this.tsmi_Bridge_Foundation.Text = "Bridge Foundation Design";
            this.tsmi_Bridge_Foundation.Click += new System.EventHandler(this.tsmi_Bridge_Foundation_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(382, 6);
            // 
            // tsmi_Hydraulic_Calculations
            // 
            this.tsmi_Hydraulic_Calculations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hydraulicCalculationsToolStripMenuItem});
            this.tsmi_Hydraulic_Calculations.Name = "tsmi_Hydraulic_Calculations";
            this.tsmi_Hydraulic_Calculations.Size = new System.Drawing.Size(385, 22);
            this.tsmi_Hydraulic_Calculations.Text = "Hydraulic Calculations";
            // 
            // hydraulicCalculationsToolStripMenuItem
            // 
            this.hydraulicCalculationsToolStripMenuItem.Name = "hydraulicCalculationsToolStripMenuItem";
            this.hydraulicCalculationsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.hydraulicCalculationsToolStripMenuItem.Text = "Hydraulic Calculations";
            this.hydraulicCalculationsToolStripMenuItem.Click += new System.EventHandler(this.hYDRAULICCALCULATIONSToolStripMenuItem_Click);
            // 
            // tsmi_Material_Properties1
            // 
            this.tsmi_Material_Properties1.Name = "tsmi_Material_Properties1";
            this.tsmi_Material_Properties1.Size = new System.Drawing.Size(385, 22);
            this.tsmi_Material_Properties1.Text = "Material Properties";
            this.tsmi_Material_Properties1.Visible = false;
            this.tsmi_Material_Properties1.Click += new System.EventHandler(this.tsmi_Material_Properties_Click);
            // 
            // tsmi_CostEstimation
            // 
            this.tsmi_CostEstimation.Name = "tsmi_CostEstimation";
            this.tsmi_CostEstimation.Size = new System.Drawing.Size(385, 22);
            this.tsmi_CostEstimation.Text = "Cost Estimation";
            this.tsmi_CostEstimation.Click += new System.EventHandler(this.tsmi_CostEstimation_Click);
            // 
            // tsmi_RCC_Structural_Design
            // 
            this.tsmi_RCC_Structural_Design.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Working_Folder1,
            this.rCCSLABToolStripMenuItem,
            this.asdsdToolStripMenuItem,
            this.rCCCOLUMNToolStripMenuItem1,
            this.tsmi_re_wall_des_strc,
            this.tsmi_return_wall_des_strc,
            this.tsmi_tunnel_design,
            this.tsmi_rcc_structure_design,
            this.tsmi_structural_components,
            this.tsmi_jetty_design,
            this.tsmi_transmissionTower,
            this.tsmi_transmissionTower3Cables,
            this.tsmi_microwaveTower,
            this.tsmi_cableCarTower});
            this.tsmi_RCC_Structural_Design.Name = "tsmi_RCC_Structural_Design";
            this.tsmi_RCC_Structural_Design.Size = new System.Drawing.Size(244, 22);
            this.tsmi_RCC_Structural_Design.Text = "Structure Design (Optional)";
            // 
            // tsmi_Working_Folder1
            // 
            this.tsmi_Working_Folder1.Name = "tsmi_Working_Folder1";
            this.tsmi_Working_Folder1.Size = new System.Drawing.Size(295, 22);
            this.tsmi_Working_Folder1.Text = "Working Folder";
            this.tsmi_Working_Folder1.Visible = false;
            this.tsmi_Working_Folder1.Click += new System.EventHandler(this.tsmi_Working_Folder_Click);
            // 
            // rCCSLABToolStripMenuItem
            // 
            this.rCCSLABToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oneWayRCCSlabToolStripMenuItem,
            this.oneWayContinuousRCCSlabToolStripMenuItem,
            this.twoWayRCCSlabToolStripMenuItem});
            this.rCCSLABToolStripMenuItem.Name = "rCCSLABToolStripMenuItem";
            this.rCCSLABToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.rCCSLABToolStripMenuItem.Text = "RCC Slab";
            this.rCCSLABToolStripMenuItem.Visible = false;
            // 
            // oneWayRCCSlabToolStripMenuItem
            // 
            this.oneWayRCCSlabToolStripMenuItem.Name = "oneWayRCCSlabToolStripMenuItem";
            this.oneWayRCCSlabToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.oneWayRCCSlabToolStripMenuItem.Text = "One way RCC Slab";
            this.oneWayRCCSlabToolStripMenuItem.Click += new System.EventHandler(this.sLAB02ToolStripMenuItem_Click);
            // 
            // oneWayContinuousRCCSlabToolStripMenuItem
            // 
            this.oneWayContinuousRCCSlabToolStripMenuItem.Name = "oneWayContinuousRCCSlabToolStripMenuItem";
            this.oneWayContinuousRCCSlabToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.oneWayContinuousRCCSlabToolStripMenuItem.Text = "One way Continuous RCC Slab";
            this.oneWayContinuousRCCSlabToolStripMenuItem.Click += new System.EventHandler(this.sLAB03ToolStripMenuItem_Click);
            // 
            // twoWayRCCSlabToolStripMenuItem
            // 
            this.twoWayRCCSlabToolStripMenuItem.Name = "twoWayRCCSlabToolStripMenuItem";
            this.twoWayRCCSlabToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.twoWayRCCSlabToolStripMenuItem.Text = "Two Way RCC Slab";
            this.twoWayRCCSlabToolStripMenuItem.Click += new System.EventHandler(this.sLAB04ToolStripMenuItem_Click);
            // 
            // asdsdToolStripMenuItem
            // 
            this.asdsdToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simplySupportedSingleSpanBeamToolStripMenuItem,
            this.rectangularOrFlangeBeamBS8110ToolStripMenuItem});
            this.asdsdToolStripMenuItem.Name = "asdsdToolStripMenuItem";
            this.asdsdToolStripMenuItem.Size = new System.Drawing.Size(295, 22);
            this.asdsdToolStripMenuItem.Text = "RCC Beam";
            this.asdsdToolStripMenuItem.Visible = false;
            // 
            // simplySupportedSingleSpanBeamToolStripMenuItem
            // 
            this.simplySupportedSingleSpanBeamToolStripMenuItem.Name = "simplySupportedSingleSpanBeamToolStripMenuItem";
            this.simplySupportedSingleSpanBeamToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.simplySupportedSingleSpanBeamToolStripMenuItem.Text = "Simply Supported Single Span Beam";
            this.simplySupportedSingleSpanBeamToolStripMenuItem.Click += new System.EventHandler(this.singleSpanToolStripMenuItem_Click);
            // 
            // rectangularOrFlangeBeamBS8110ToolStripMenuItem
            // 
            this.rectangularOrFlangeBeamBS8110ToolStripMenuItem.Name = "rectangularOrFlangeBeamBS8110ToolStripMenuItem";
            this.rectangularOrFlangeBeamBS8110ToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.rectangularOrFlangeBeamBS8110ToolStripMenuItem.Text = "Continuous Multi Span Beam";
            this.rectangularOrFlangeBeamBS8110ToolStripMenuItem.Click += new System.EventHandler(this.rectangularOrFlangeBeamBS8110ToolStripMenuItem_Click);
            // 
            // rCCCOLUMNToolStripMenuItem1
            // 
            this.rCCCOLUMNToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.axiallyLoadedToolStripMenuItem,
            this.axialLoadBiAxialMomentToolStripMenuItem1});
            this.rCCCOLUMNToolStripMenuItem1.Name = "rCCCOLUMNToolStripMenuItem1";
            this.rCCCOLUMNToolStripMenuItem1.Size = new System.Drawing.Size(295, 22);
            this.rCCCOLUMNToolStripMenuItem1.Text = "RCC Column";
            this.rCCCOLUMNToolStripMenuItem1.Visible = false;
            // 
            // axiallyLoadedToolStripMenuItem
            // 
            this.axiallyLoadedToolStripMenuItem.Name = "axiallyLoadedToolStripMenuItem";
            this.axiallyLoadedToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.axiallyLoadedToolStripMenuItem.Text = "Axially Loaded Column BS:8110";
            this.axiallyLoadedToolStripMenuItem.Click += new System.EventHandler(this.axiallyLoadedToolStripMenuItem_Click);
            // 
            // axialLoadBiAxialMomentToolStripMenuItem1
            // 
            this.axialLoadBiAxialMomentToolStripMenuItem1.Name = "axialLoadBiAxialMomentToolStripMenuItem1";
            this.axialLoadBiAxialMomentToolStripMenuItem1.Size = new System.Drawing.Size(239, 22);
            this.axialLoadBiAxialMomentToolStripMenuItem1.Text = "Bi Axially Bent Column";
            this.axialLoadBiAxialMomentToolStripMenuItem1.Click += new System.EventHandler(this.axialLoadBiAxialMomentToolStripMenuItem1_Click);
            // 
            // tsmi_re_wall_des_strc
            // 
            this.tsmi_re_wall_des_strc.Name = "tsmi_re_wall_des_strc";
            this.tsmi_re_wall_des_strc.Size = new System.Drawing.Size(295, 22);
            this.tsmi_re_wall_des_strc.Text = "RE (Reinforced Earth) Wall Design";
            this.tsmi_re_wall_des_strc.Click += new System.EventHandler(this.tsmi_RE_Wall_Design_Click);
            // 
            // tsmi_return_wall_des_strc
            // 
            this.tsmi_return_wall_des_strc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_return_wall_cant,
            this.tsmi_return_wall_propped});
            this.tsmi_return_wall_des_strc.Name = "tsmi_return_wall_des_strc";
            this.tsmi_return_wall_des_strc.Size = new System.Drawing.Size(295, 22);
            this.tsmi_return_wall_des_strc.Text = "RCC Retaining Wall Design";
            // 
            // tsmi_return_wall_cant
            // 
            this.tsmi_return_wall_cant.Name = "tsmi_return_wall_cant";
            this.tsmi_return_wall_cant.Size = new System.Drawing.Size(254, 22);
            this.tsmi_return_wall_cant.Text = "Cantilever Retaining Wall";
            this.tsmi_return_wall_cant.Click += new System.EventHandler(this.tsmi_return_wall_cant_Click);
            // 
            // tsmi_return_wall_propped
            // 
            this.tsmi_return_wall_propped.Name = "tsmi_return_wall_propped";
            this.tsmi_return_wall_propped.Size = new System.Drawing.Size(254, 22);
            this.tsmi_return_wall_propped.Text = "Propped Cantilever Retaining Wall";
            this.tsmi_return_wall_propped.Click += new System.EventHandler(this.tsmi_return_wall_cant_Click);
            // 
            // tsmi_tunnel_design
            // 
            this.tsmi_tunnel_design.Name = "tsmi_tunnel_design";
            this.tsmi_tunnel_design.Size = new System.Drawing.Size(295, 22);
            this.tsmi_tunnel_design.Text = "Tunnel Design (RCC Lining && Steel Portal)";
            this.tsmi_tunnel_design.Click += new System.EventHandler(this.tsmi_structure_design_Click);
            // 
            // tsmi_rcc_structure_design
            // 
            this.tsmi_rcc_structure_design.Name = "tsmi_rcc_structure_design";
            this.tsmi_rcc_structure_design.Size = new System.Drawing.Size(295, 22);
            this.tsmi_rcc_structure_design.Text = "RCC Framed Building Design";
            this.tsmi_rcc_structure_design.Click += new System.EventHandler(this.tsmi_structure_design_Click);
            // 
            // tsmi_structural_components
            // 
            this.tsmi_structural_components.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_steel_beam,
            this.tsmi_steel_column,
            this.tsmi_isolatedFooting,
            this.tsmi_combinedFooting,
            this.tsmi_pIleFoundation,
            this.tsmi_raftFoundation});
            this.tsmi_structural_components.Name = "tsmi_structural_components";
            this.tsmi_structural_components.Size = new System.Drawing.Size(295, 22);
            this.tsmi_structural_components.Text = "Design of Structural Components";
            // 
            // tsmi_steel_beam
            // 
            this.tsmi_steel_beam.Name = "tsmi_steel_beam";
            this.tsmi_steel_beam.Size = new System.Drawing.Size(285, 22);
            this.tsmi_steel_beam.Text = "Design of Steel Beam";
            this.tsmi_steel_beam.Click += new System.EventHandler(this.tsmi_steel_beam_Click);
            // 
            // tsmi_steel_column
            // 
            this.tsmi_steel_column.Name = "tsmi_steel_column";
            this.tsmi_steel_column.Size = new System.Drawing.Size(285, 22);
            this.tsmi_steel_column.Text = "Design of Steel Column";
            this.tsmi_steel_column.Click += new System.EventHandler(this.tsmi_steel_beam_Click);
            // 
            // tsmi_isolatedFooting
            // 
            this.tsmi_isolatedFooting.Name = "tsmi_isolatedFooting";
            this.tsmi_isolatedFooting.Size = new System.Drawing.Size(285, 22);
            this.tsmi_isolatedFooting.Text = "Design of RCC Isolated Foundation";
            this.tsmi_isolatedFooting.Click += new System.EventHandler(this.isolatedFootingToolStripMenuItem1_Click);
            // 
            // tsmi_combinedFooting
            // 
            this.tsmi_combinedFooting.Name = "tsmi_combinedFooting";
            this.tsmi_combinedFooting.Size = new System.Drawing.Size(285, 22);
            this.tsmi_combinedFooting.Text = "Design of RCC Combined Foundation";
            this.tsmi_combinedFooting.Click += new System.EventHandler(this.combinedFootingToolStripMenuItem1_Click);
            // 
            // tsmi_pIleFoundation
            // 
            this.tsmi_pIleFoundation.Name = "tsmi_pIleFoundation";
            this.tsmi_pIleFoundation.Size = new System.Drawing.Size(285, 22);
            this.tsmi_pIleFoundation.Text = "Design of RCC Well and PIle Foundation";
            this.tsmi_pIleFoundation.Click += new System.EventHandler(this.tsmi_Bridge_Foundation_Click);
            // 
            // tsmi_raftFoundation
            // 
            this.tsmi_raftFoundation.Name = "tsmi_raftFoundation";
            this.tsmi_raftFoundation.Size = new System.Drawing.Size(285, 22);
            this.tsmi_raftFoundation.Text = "Design of RCC Raft Foundation";
            this.tsmi_raftFoundation.Click += new System.EventHandler(this.raftFoundationToolStripMenuItem_Click);
            // 
            // tsmi_jetty_design
            // 
            this.tsmi_jetty_design.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_jetty_design_RCC,
            this.tsmi_jetty_design_PSC});
            this.tsmi_jetty_design.Name = "tsmi_jetty_design";
            this.tsmi_jetty_design.Size = new System.Drawing.Size(295, 22);
            this.tsmi_jetty_design.Text = "Jetty Limit State Design";
            // 
            // tsmi_jetty_design_RCC
            // 
            this.tsmi_jetty_design_RCC.Name = "tsmi_jetty_design_RCC";
            this.tsmi_jetty_design_RCC.Size = new System.Drawing.Size(222, 22);
            this.tsmi_jetty_design_RCC.Text = "RCC Jetty Limit State Design";
            this.tsmi_jetty_design_RCC.Click += new System.EventHandler(this.tsmi_jetty_design_Click);
            // 
            // tsmi_jetty_design_PSC
            // 
            this.tsmi_jetty_design_PSC.Name = "tsmi_jetty_design_PSC";
            this.tsmi_jetty_design_PSC.Size = new System.Drawing.Size(222, 22);
            this.tsmi_jetty_design_PSC.Text = "PSC Jetty Limit State Design";
            this.tsmi_jetty_design_PSC.Click += new System.EventHandler(this.tsmi_jetty_design_Click);
            // 
            // tsmi_transmissionTower
            // 
            this.tsmi_transmissionTower.Name = "tsmi_transmissionTower";
            this.tsmi_transmissionTower.Size = new System.Drawing.Size(295, 22);
            this.tsmi_transmissionTower.Text = "Transmission Tower Design";
            this.tsmi_transmissionTower.Click += new System.EventHandler(this.tsmi_transmissionTower_Click);
            // 
            // tsmi_transmissionTower3Cables
            // 
            this.tsmi_transmissionTower3Cables.Name = "tsmi_transmissionTower3Cables";
            this.tsmi_transmissionTower3Cables.Size = new System.Drawing.Size(295, 22);
            this.tsmi_transmissionTower3Cables.Text = "Transmission Tower 3 Cables";
            this.tsmi_transmissionTower3Cables.Click += new System.EventHandler(this.tsmi_transmissionTower_Click);
            // 
            // tsmi_microwaveTower
            // 
            this.tsmi_microwaveTower.Name = "tsmi_microwaveTower";
            this.tsmi_microwaveTower.Size = new System.Drawing.Size(295, 22);
            this.tsmi_microwaveTower.Text = "Microwave Tower Design";
            this.tsmi_microwaveTower.Click += new System.EventHandler(this.tsmi_transmissionTower_Click);
            // 
            // tsmi_cableCarTower
            // 
            this.tsmi_cableCarTower.Name = "tsmi_cableCarTower";
            this.tsmi_cableCarTower.Size = new System.Drawing.Size(295, 22);
            this.tsmi_cableCarTower.Text = "Cable Car Tower Design";
            this.tsmi_cableCarTower.Click += new System.EventHandler(this.tsmi_transmissionTower_Click);
            // 
            // tsmi_streamHydrology
            // 
            this.tsmi_streamHydrology.Name = "tsmi_streamHydrology";
            this.tsmi_streamHydrology.Size = new System.Drawing.Size(244, 22);
            this.tsmi_streamHydrology.Text = "Stream Hydrology";
            this.tsmi_streamHydrology.Click += new System.EventHandler(this.tsmi_streamHydrology_Click);
            // 
            // tsmi_structureModeling
            // 
            this.tsmi_structureModeling.Name = "tsmi_structureModeling";
            this.tsmi_structureModeling.Size = new System.Drawing.Size(244, 22);
            this.tsmi_structureModeling.Text = "Structure Modeling with 3D Grid";
            this.tsmi_structureModeling.Click += new System.EventHandler(this.tsmi_structureModeling_Click);
            // 
            // tsmi_research_Studies
            // 
            this.tsmi_research_Studies.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_selectWorkingFolderResearch,
            this.tsmi_HSTBD,
            this.tsmi_dynamicAnalysis,
            this.tsmi_PDelta,
            this.tsmi_streamHydrologyResearch});
            this.tsmi_research_Studies.Name = "tsmi_research_Studies";
            this.tsmi_research_Studies.Size = new System.Drawing.Size(107, 20);
            this.tsmi_research_Studies.Text = "Research Studies";
            // 
            // tsmi_selectWorkingFolderResearch
            // 
            this.tsmi_selectWorkingFolderResearch.Name = "tsmi_selectWorkingFolderResearch";
            this.tsmi_selectWorkingFolderResearch.Size = new System.Drawing.Size(353, 22);
            this.tsmi_selectWorkingFolderResearch.Text = "Select Working Folder";
            this.tsmi_selectWorkingFolderResearch.Visible = false;
            this.tsmi_selectWorkingFolderResearch.Click += new System.EventHandler(this.tsmi_Working_Folder_Click);
            // 
            // tsmi_HSTBD
            // 
            this.tsmi_HSTBD.Name = "tsmi_HSTBD";
            this.tsmi_HSTBD.Size = new System.Drawing.Size(353, 22);
            this.tsmi_HSTBD.Text = "High Speed Train and Bridge Dynamics (Conditional)";
            this.tsmi_HSTBD.Click += new System.EventHandler(this.tsmi_HSTBD_Click);
            // 
            // tsmi_dynamicAnalysis
            // 
            this.tsmi_dynamicAnalysis.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_eigenValue,
            this.tsmi_timeHistory,
            this.tsmi_responseSpectrum});
            this.tsmi_dynamicAnalysis.Name = "tsmi_dynamicAnalysis";
            this.tsmi_dynamicAnalysis.Size = new System.Drawing.Size(353, 22);
            this.tsmi_dynamicAnalysis.Text = "Dynamic Analysis";
            // 
            // tsmi_eigenValue
            // 
            this.tsmi_eigenValue.Name = "tsmi_eigenValue";
            this.tsmi_eigenValue.Size = new System.Drawing.Size(178, 22);
            this.tsmi_eigenValue.Text = "Eigen Value";
            this.tsmi_eigenValue.Click += new System.EventHandler(this.tsmi_eigenValue_Click);
            // 
            // tsmi_timeHistory
            // 
            this.tsmi_timeHistory.Name = "tsmi_timeHistory";
            this.tsmi_timeHistory.Size = new System.Drawing.Size(178, 22);
            this.tsmi_timeHistory.Text = "Time History";
            this.tsmi_timeHistory.Click += new System.EventHandler(this.tsmi_eigenValue_Click);
            // 
            // tsmi_responseSpectrum
            // 
            this.tsmi_responseSpectrum.Name = "tsmi_responseSpectrum";
            this.tsmi_responseSpectrum.Size = new System.Drawing.Size(178, 22);
            this.tsmi_responseSpectrum.Text = "Response Spectrum";
            this.tsmi_responseSpectrum.Click += new System.EventHandler(this.tsmi_eigenValue_Click);
            // 
            // tsmi_PDelta
            // 
            this.tsmi_PDelta.Name = "tsmi_PDelta";
            this.tsmi_PDelta.Size = new System.Drawing.Size(353, 22);
            this.tsmi_PDelta.Text = "Non Linear Stage (P-Delta) Analysis";
            this.tsmi_PDelta.Click += new System.EventHandler(this.pDeltaAnalysisToolStripMenuItem_Click);
            // 
            // tsmi_streamHydrologyResearch
            // 
            this.tsmi_streamHydrologyResearch.Name = "tsmi_streamHydrologyResearch";
            this.tsmi_streamHydrologyResearch.Size = new System.Drawing.Size(353, 22);
            this.tsmi_streamHydrologyResearch.Text = "Stream Hydrology";
            this.tsmi_streamHydrologyResearch.Click += new System.EventHandler(this.tsmi_streamHydrology_Click);
            // 
            // tsmi_CAD_Viewer
            // 
            this.tsmi_CAD_Viewer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.astraProViewerToolStripMenuItem});
            this.tsmi_CAD_Viewer.Name = "tsmi_CAD_Viewer";
            this.tsmi_CAD_Viewer.Size = new System.Drawing.Size(81, 20);
            this.tsmi_CAD_Viewer.Text = "CAD &Viewer";
            // 
            // astraProViewerToolStripMenuItem
            // 
            this.astraProViewerToolStripMenuItem.Name = "astraProViewerToolStripMenuItem";
            this.astraProViewerToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.astraProViewerToolStripMenuItem.Text = "&Astra Pro Viewer";
            this.astraProViewerToolStripMenuItem.Click += new System.EventHandler(this.cADViewerToolStripMenuItem_Click);
            // 
            // utilityToolStripMenuItem
            // 
            this.utilityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.foldersAndOpenFiletoolStripMenuItem,
            this.explorerToolStripMenuItem,
            this.notePadToolStripMenuItem,
            this.tsmi_geotechnics,
            this.tsmi_Material_Properties,
            this.tsmi_Section_Properties,
            this.tsmi_seismic_coefficient,
            this.tsmi_ll_data,
            this.tsmi_steel_tables});
            this.utilityToolStripMenuItem.Name = "utilityToolStripMenuItem";
            this.utilityToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.utilityToolStripMenuItem.Text = "&Utilities";
            // 
            // foldersAndOpenFiletoolStripMenuItem
            // 
            this.foldersAndOpenFiletoolStripMenuItem.Name = "foldersAndOpenFiletoolStripMenuItem";
            this.foldersAndOpenFiletoolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.foldersAndOpenFiletoolStripMenuItem.Text = "&Open File";
            this.foldersAndOpenFiletoolStripMenuItem.Click += new System.EventHandler(this.foldersAndOpenFiletoolStripMenuItem_Click);
            // 
            // explorerToolStripMenuItem
            // 
            this.explorerToolStripMenuItem.Name = "explorerToolStripMenuItem";
            this.explorerToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.explorerToolStripMenuItem.Text = "Open &Working Folder";
            this.explorerToolStripMenuItem.Click += new System.EventHandler(this.explorerToolStripMenuItem_Click);
            // 
            // notePadToolStripMenuItem
            // 
            this.notePadToolStripMenuItem.Name = "notePadToolStripMenuItem";
            this.notePadToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.notePadToolStripMenuItem.Text = "&NotePad";
            this.notePadToolStripMenuItem.Click += new System.EventHandler(this.notePadToolStripMenuItem_Click);
            // 
            // tsmi_geotechnics
            // 
            this.tsmi_geotechnics.Name = "tsmi_geotechnics";
            this.tsmi_geotechnics.Size = new System.Drawing.Size(187, 22);
            this.tsmi_geotechnics.Text = "&Geotechnics";
            this.tsmi_geotechnics.Click += new System.EventHandler(this.tsmi_geotechnics_click);
            // 
            // tsmi_Material_Properties
            // 
            this.tsmi_Material_Properties.Name = "tsmi_Material_Properties";
            this.tsmi_Material_Properties.Size = new System.Drawing.Size(187, 22);
            this.tsmi_Material_Properties.Text = "Material Properties";
            this.tsmi_Material_Properties.Click += new System.EventHandler(this.tsmi_Material_Properties_Click);
            // 
            // tsmi_Section_Properties
            // 
            this.tsmi_Section_Properties.Name = "tsmi_Section_Properties";
            this.tsmi_Section_Properties.Size = new System.Drawing.Size(187, 22);
            this.tsmi_Section_Properties.Text = "Section Properties";
            this.tsmi_Section_Properties.Click += new System.EventHandler(this.tsmi_Material_Properties_Click);
            // 
            // tsmi_seismic_coefficient
            // 
            this.tsmi_seismic_coefficient.Name = "tsmi_seismic_coefficient";
            this.tsmi_seismic_coefficient.Size = new System.Drawing.Size(187, 22);
            this.tsmi_seismic_coefficient.Text = "Seismic Coefficient";
            this.tsmi_seismic_coefficient.Click += new System.EventHandler(this.tsmi_Material_Properties_Click);
            // 
            // tsmi_ll_data
            // 
            this.tsmi_ll_data.Name = "tsmi_ll_data";
            this.tsmi_ll_data.Size = new System.Drawing.Size(187, 22);
            this.tsmi_ll_data.Text = "Moving Load Data";
            this.tsmi_ll_data.Visible = false;
            this.tsmi_ll_data.Click += new System.EventHandler(this.tsmi_ll_data_Click);
            // 
            // tsmi_steel_tables
            // 
            this.tsmi_steel_tables.Name = "tsmi_steel_tables";
            this.tsmi_steel_tables.Size = new System.Drawing.Size(187, 22);
            this.tsmi_steel_tables.Text = "Steel Sections";
            this.tsmi_steel_tables.Click += new System.EventHandler(this.tsmi_steel_tables_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_contents,
            this.tsmi_contents_supplement,
            this.tsmi_sap_manual,
            this.toolStripSeparator24,
            this.tsmi_design_manual,
            this.tsmi_users_manual,
            this.tsmi_videos,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem,
            this.tsmi_send_mail,
            this.toolStripSeparator23,
            this.tsmi_link_1,
            this.tsmi_link_2});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // tsmi_contents
            // 
            this.tsmi_contents.Name = "tsmi_contents";
            this.tsmi_contents.Size = new System.Drawing.Size(326, 22);
            this.tsmi_contents.Text = "Con&tents";
            this.tsmi_contents.Click += new System.EventHandler(this.contentsToolStripMenuItem_Click);
            // 
            // tsmi_contents_supplement
            // 
            this.tsmi_contents_supplement.Name = "tsmi_contents_supplement";
            this.tsmi_contents_supplement.Size = new System.Drawing.Size(326, 22);
            this.tsmi_contents_supplement.Text = "Contents Supplement";
            this.tsmi_contents_supplement.Click += new System.EventHandler(this.contentsToolStripMenuItem_Click);
            // 
            // tsmi_sap_manual
            // 
            this.tsmi_sap_manual.Name = "tsmi_sap_manual";
            this.tsmi_sap_manual.Size = new System.Drawing.Size(326, 22);
            this.tsmi_sap_manual.Text = "SAP Input Data Manual";
            this.tsmi_sap_manual.Click += new System.EventHandler(this.contentsToolStripMenuItem_Click);
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(323, 6);
            // 
            // tsmi_design_manual
            // 
            this.tsmi_design_manual.Name = "tsmi_design_manual";
            this.tsmi_design_manual.Size = new System.Drawing.Size(326, 22);
            this.tsmi_design_manual.Text = "ASTRA Pro Design Manual";
            this.tsmi_design_manual.Click += new System.EventHandler(this.tsmi_videos_Click);
            // 
            // tsmi_users_manual
            // 
            this.tsmi_users_manual.Name = "tsmi_users_manual";
            this.tsmi_users_manual.Size = new System.Drawing.Size(326, 22);
            this.tsmi_users_manual.Text = "ASTRA Pro User\'s Manual";
            this.tsmi_users_manual.Click += new System.EventHandler(this.tsmi_videos_Click);
            // 
            // tsmi_videos
            // 
            this.tsmi_videos.Name = "tsmi_videos";
            this.tsmi_videos.Size = new System.Drawing.Size(326, 22);
            this.tsmi_videos.Text = "Tutorial Videos";
            this.tsmi_videos.Click += new System.EventHandler(this.tsmi_videos_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(323, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
            this.aboutToolStripMenuItem.Text = "About &ASTRA Pro";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tsmi_send_mail
            // 
            this.tsmi_send_mail.Name = "tsmi_send_mail";
            this.tsmi_send_mail.Size = new System.Drawing.Size(326, 22);
            this.tsmi_send_mail.Text = "Send Mail to TechSOFT";
            this.tsmi_send_mail.Click += new System.EventHandler(this.btn_SendMail_Click);
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(323, 6);
            // 
            // tsmi_link_1
            // 
            this.tsmi_link_1.Name = "tsmi_link_1";
            this.tsmi_link_1.Size = new System.Drawing.Size(326, 22);
            this.tsmi_link_1.Text = "Web Page Link for AASHTO Bridge Loading";
            this.tsmi_link_1.Click += new System.EventHandler(this.tsmi_link_1_Click);
            // 
            // tsmi_link_2
            // 
            this.tsmi_link_2.Name = "tsmi_link_2";
            this.tsmi_link_2.Size = new System.Drawing.Size(326, 22);
            this.tsmi_link_2.Text = "Web Page Link for BD 37_01 for HA & HB Loading";
            this.tsmi_link_2.Click += new System.EventHandler(this.tsmi_link_1_Click);
            // 
            // programsToolStripMenuItem
            // 
            this.programsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gPSTransformationGuideToolStripMenuItem1,
            this.googleEarthToolStripMenuItem1,
            this.globalMapperV11ToolStripMenuItem,
            this.sheetPileApplicationToolStripMenuItem});
            this.programsToolStripMenuItem.Name = "programsToolStripMenuItem";
            this.programsToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.programsToolStripMenuItem.Text = "Programs";
            // 
            // gPSTransformationGuideToolStripMenuItem1
            // 
            this.gPSTransformationGuideToolStripMenuItem1.Name = "gPSTransformationGuideToolStripMenuItem1";
            this.gPSTransformationGuideToolStripMenuItem1.Size = new System.Drawing.Size(214, 22);
            this.gPSTransformationGuideToolStripMenuItem1.Text = "GPS Transformation Guide";
            this.gPSTransformationGuideToolStripMenuItem1.Visible = false;
            this.gPSTransformationGuideToolStripMenuItem1.Click += new System.EventHandler(this.gPSTransformationGuideToolStripMenuItem1_Click);
            // 
            // googleEarthToolStripMenuItem1
            // 
            this.googleEarthToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoAboutThisProgramToolStripMenuItem,
            this.runProgramToolStripMenuItem});
            this.googleEarthToolStripMenuItem1.Name = "googleEarthToolStripMenuItem1";
            this.googleEarthToolStripMenuItem1.Size = new System.Drawing.Size(214, 22);
            this.googleEarthToolStripMenuItem1.Text = "Google Earth";
            // 
            // infoAboutThisProgramToolStripMenuItem
            // 
            this.infoAboutThisProgramToolStripMenuItem.Name = "infoAboutThisProgramToolStripMenuItem";
            this.infoAboutThisProgramToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.infoAboutThisProgramToolStripMenuItem.Text = "Info About the Program";
            this.infoAboutThisProgramToolStripMenuItem.Click += new System.EventHandler(this.infoAboutThisProgramToolStripMenuItem_Click);
            // 
            // runProgramToolStripMenuItem
            // 
            this.runProgramToolStripMenuItem.Name = "runProgramToolStripMenuItem";
            this.runProgramToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.runProgramToolStripMenuItem.Text = "Run Program";
            this.runProgramToolStripMenuItem.Click += new System.EventHandler(this.runProgramToolStripMenuItem_Click);
            // 
            // globalMapperV11ToolStripMenuItem
            // 
            this.globalMapperV11ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoAboutTheProgramToolStripMenuItem,
            this.runProgramToolStripMenuItem1});
            this.globalMapperV11ToolStripMenuItem.Name = "globalMapperV11ToolStripMenuItem";
            this.globalMapperV11ToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.globalMapperV11ToolStripMenuItem.Text = "Global Mapper V 11";
            // 
            // infoAboutTheProgramToolStripMenuItem
            // 
            this.infoAboutTheProgramToolStripMenuItem.Name = "infoAboutTheProgramToolStripMenuItem";
            this.infoAboutTheProgramToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.infoAboutTheProgramToolStripMenuItem.Text = "Info About the Program";
            this.infoAboutTheProgramToolStripMenuItem.Click += new System.EventHandler(this.infoAboutTheProgramToolStripMenuItem_Click);
            // 
            // runProgramToolStripMenuItem1
            // 
            this.runProgramToolStripMenuItem1.Name = "runProgramToolStripMenuItem1";
            this.runProgramToolStripMenuItem1.Size = new System.Drawing.Size(200, 22);
            this.runProgramToolStripMenuItem1.Text = "Run Program";
            this.runProgramToolStripMenuItem1.Click += new System.EventHandler(this.runProgramToolStripMenuItem1_Click);
            // 
            // sheetPileApplicationToolStripMenuItem
            // 
            this.sheetPileApplicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_sheet_pile});
            this.sheetPileApplicationToolStripMenuItem.Name = "sheetPileApplicationToolStripMenuItem";
            this.sheetPileApplicationToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.sheetPileApplicationToolStripMenuItem.Text = "Sheet Pile Application";
            this.sheetPileApplicationToolStripMenuItem.Click += new System.EventHandler(this.tsmi_sheet_pile_Click);
            // 
            // tsmi_sheet_pile
            // 
            this.tsmi_sheet_pile.Name = "tsmi_sheet_pile";
            this.tsmi_sheet_pile.Size = new System.Drawing.Size(291, 22);
            this.tsmi_sheet_pile.Text = "Select && Run the Program \"ProSheet.exe\"";
            this.tsmi_sheet_pile.Visible = false;
            this.tsmi_sheet_pile.Click += new System.EventHandler(this.tsmi_sheet_pile_Click);
            // 
            // ofdAst
            // 
            this.ofdAst.Filter = "Text File|*.txt|AST Files|*.ast|All Files|*.*";
            // 
            // sfdAst
            // 
            this.sfdAst.DefaultExt = "ast";
            this.sfdAst.Filter = "AST Files|*.ast";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.toolStrip1.AllowDrop = true;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnNewFile,
            this.tsbtnOpenFile,
            this.tsbtnSaveFile,
            this.toolStripSeparator6,
            this.tsbtnCut,
            this.tsbtnCopy,
            this.tsbtnPaste,
            this.toolStripSeparator7,
            this.tsbtnUndo,
            this.tsbtnRedo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(766, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // tsbtnNewFile
            // 
            this.tsbtnNewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnNewFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNewFile.Image")));
            this.tsbtnNewFile.ImageTransparentColor = System.Drawing.Color.Maroon;
            this.tsbtnNewFile.Name = "tsbtnNewFile";
            this.tsbtnNewFile.Size = new System.Drawing.Size(23, 22);
            this.tsbtnNewFile.Text = "New File";
            this.tsbtnNewFile.Click += new System.EventHandler(this.tsbtnNewFile_Click);
            // 
            // tsbtnOpenFile
            // 
            this.tsbtnOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOpenFile.Image")));
            this.tsbtnOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnOpenFile.Name = "tsbtnOpenFile";
            this.tsbtnOpenFile.Size = new System.Drawing.Size(23, 22);
            this.tsbtnOpenFile.Text = "Open File";
            this.tsbtnOpenFile.Click += new System.EventHandler(this.tsbtnOpenFile_Click);
            // 
            // tsbtnSaveFile
            // 
            this.tsbtnSaveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSaveFile.Image")));
            this.tsbtnSaveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSaveFile.Name = "tsbtnSaveFile";
            this.tsbtnSaveFile.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSaveFile.Text = "Save File";
            this.tsbtnSaveFile.Click += new System.EventHandler(this.tsbtnSaveFile_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnCut
            // 
            this.tsbtnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCut.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnCut.Image")));
            this.tsbtnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCut.Name = "tsbtnCut";
            this.tsbtnCut.Size = new System.Drawing.Size(23, 22);
            this.tsbtnCut.Text = "Cut";
            // 
            // tsbtnCopy
            // 
            this.tsbtnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnCopy.Image")));
            this.tsbtnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCopy.Name = "tsbtnCopy";
            this.tsbtnCopy.Size = new System.Drawing.Size(23, 22);
            this.tsbtnCopy.Text = "Copy";
            // 
            // tsbtnPaste
            // 
            this.tsbtnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPaste.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnPaste.Image")));
            this.tsbtnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPaste.Name = "tsbtnPaste";
            this.tsbtnPaste.Size = new System.Drawing.Size(23, 22);
            this.tsbtnPaste.Text = "Paste";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnUndo
            // 
            this.tsbtnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnUndo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUndo.Image")));
            this.tsbtnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUndo.Name = "tsbtnUndo";
            this.tsbtnUndo.Size = new System.Drawing.Size(23, 22);
            this.tsbtnUndo.Text = "Undo";
            // 
            // tsbtnRedo
            // 
            this.tsbtnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRedo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRedo.Image")));
            this.tsbtnRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRedo.Name = "tsbtnRedo";
            this.tsbtnRedo.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRedo.Text = "Redo";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsl_text});
            this.statusStrip1.Location = new System.Drawing.Point(0, 620);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(766, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsl_text
            // 
            this.tsl_text.Name = "tsl_text";
            this.tsl_text.Size = new System.Drawing.Size(207, 17);
            this.tsl_text.Text = "ASTRA Pro Dongle searching................";
            // 
            // helpProvider1
            // 
            this.helpProvider1.HelpNamespace = "";
            // 
            // tmr
            // 
            this.tmr.Enabled = true;
            this.tmr.Interval = 3999;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // btn_SendMail
            // 
            this.btn_SendMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SendMail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_SendMail.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_SendMail.Location = new System.Drawing.Point(696, 619);
            this.btn_SendMail.Name = "btn_SendMail";
            this.btn_SendMail.Size = new System.Drawing.Size(70, 23);
            this.btn_SendMail.TabIndex = 6;
            this.btn_SendMail.Text = "Send Mail";
            this.btn_SendMail.UseVisualStyleBackColor = true;
            this.btn_SendMail.Click += new System.EventHandler(this.btn_SendMail_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AstraFunctionOne.Properties.Resources.ASTRA_Unauthorized;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(766, 642);
            this.Controls.Add(this.btn_SendMail);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.mns_ASTRA_menues);
            this.HelpButton = true;
            this.helpProvider1.SetHelpKeyword(this, "Astra Viewer");
            this.helpProvider1.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mns_ASTRA_menues;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.helpProvider1.SetShowHelp(this, true);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASTRA Pro";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMain_KeyPress);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.mns_ASTRA_menues.ResumeLayout(false);
            this.mns_ASTRA_menues.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mns_ASTRA_menues;
        private System.Windows.Forms.ToolStripMenuItem tsmi_file;
        private System.Windows.Forms.ToolStripMenuItem tsmi_process_analysis;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Create_New_AST_File;
        private System.Windows.Forms.ToolStripMenuItem tsmi_open_txt;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofdAst;
        private System.Windows.Forms.ToolStripMenuItem toolbarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem2;
        private System.Windows.Forms.SaveFileDialog sfdAst;
        private System.Windows.Forms.ToolStripMenuItem tsmi_CAD_Viewer;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Process_Design;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_contents;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnNewFile;
        private System.Windows.Forms.ToolStripButton tsbtnOpenFile;
        private System.Windows.Forms.ToolStripButton tsbtnSaveFile;
        private System.Windows.Forms.ToolStripButton tsbtnCut;
        private System.Windows.Forms.ToolStripButton tsbtnCopy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbtnPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsbtnUndo;
        private System.Windows.Forms.ToolStripButton tsbtnRedo;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem astraProViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processAstFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_runTXTFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_convertToASTFile;
        private System.Windows.Forms.ToolStripMenuItem runASTFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_open_ast;
        private System.Windows.Forms.ToolStripMenuItem foldersAndOpenFiletoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem notePadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem astDataInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem basicInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elementsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem plateAndShellElementsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supportToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nodalLoadsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem explorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_RCC_Structural_Design;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Bridge_Design;
        private System.Windows.Forms.ToolStripMenuItem asdsdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rCCCOLUMNToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_structural_components;
        private System.Windows.Forms.ToolStripMenuItem rCCSLABToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oneWayRCCSlabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oneWayContinuousRCCSlabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem twoWayRCCSlabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem axiallyLoadedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem axialLoadBiAxialMomentToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_isolatedFooting;
        private System.Windows.Forms.ToolStripMenuItem tsmi_combinedFooting;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Rail_Bridge;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Bridge_Abutment_Design;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Bridge_Foundation;
        private System.Windows.Forms.ToolStripMenuItem tsmi_RCC_Culverts;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Underpass;
        private System.Windows.Forms.ToolStripMenuItem simplySupportedSingleSpanBeamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rectangularOrFlangeBeamBS8110ToolStripMenuItem;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_stage_ana;
        private System.Windows.Forms.ToolStripMenuItem tsmi_license;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Steel_Truss_Bridge_Warren_1;
        private System.Windows.Forms.ToolStripMenuItem completeDesignToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deckSlabDesignToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_PSC_Box_Girder_Bridge;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Hydraulic_Calculations;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Steel_Truss_Bridge_Warren_2;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Bridge_Pier_Design;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Bridge_Bearing_Design;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Material_Properties1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Working_Folder;
        private System.Windows.Forms.ToolStripMenuItem openASTRAWorksheetDesignToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem programsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gPSTransformationGuideToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem googleEarthToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem infoAboutThisProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem globalMapperV11ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoAboutTheProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runProgramToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_geotechnics;
        private System.Windows.Forms.ToolStripMenuItem tsmi_cable_stayed_bridge;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Working_Folder1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Material_Properties;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Section_Properties;
        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ll_data;
        private System.Windows.Forms.ToolStripMenuItem tsmi_videos;
        private System.Windows.Forms.ToolStripMenuItem tsmi_open_work_folder;
        private System.Windows.Forms.ToolStripMenuItem tsmi_RE_Wall_Design;
        private System.Windows.Forms.ToolStripMenuItem tsmi_cont_PSC_Box_Girder_Bridge;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
        private System.Windows.Forms.ToolStripMenuItem tsmi_minor_Bridge;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Steel_Truss_Bridge_K_Type;
        private System.Windows.Forms.ToolStripMenuItem tsmi_arch_cable_bridge;
        private System.Windows.Forms.ToolStripMenuItem tsmi_arch_steel_bridge;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripMenuItem tsmi_seismic_coefficient;
        private System.Windows.Forms.ToolStripMenuItem tsmi_contents_supplement;
        private System.Windows.Forms.ToolStripStatusLabel tsl_text;
        private System.Windows.Forms.Button btn_SendMail;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Bridge_Pier_Pile_Design;
        private System.Windows.Forms.ToolStripMenuItem tsmi_streamHydrology;
        private System.Windows.Forms.ToolStripMenuItem tsmi_viewInputData;
        private System.Windows.Forms.ToolStripMenuItem tsmi_send_mail;
        private System.Windows.Forms.ToolStripMenuItem tsmi_viewAnalysisASTFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_processAnalysisASTFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ast_data;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ast_basic_info;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ast_analysis_type;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ast_nodes;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ast_elements;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ast_beam_truss;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ast_plate;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ast_supports;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ast_nodal_loads;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ast_self_weight;
        private System.Windows.Forms.ToolStripMenuItem tsmi_newAnalysisTXTDataFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_openAnalysisTXTDataFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_openAnalysisExampleTXTDataFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_openStructureModelDrawingFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_saveTXTDataFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_openTXTDataInNotepad;
        private System.Windows.Forms.ToolStripMenuItem tsmi_openMSExcelDesignReport;
        private System.Windows.Forms.ToolStripMenuItem tsmi_workingFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmi_analysisInputDataFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_analysisProcessResults;
        private System.Windows.Forms.ToolStripMenuItem tsmi_selectWorkingFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmi_selectDesignStandard;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ana_exmpls;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ana_sap_file;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ana_inp_sap;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ana_res_sap;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ana_text_file;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ana_inp_text;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ana_res_text;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ana_view_text;
        private System.Windows.Forms.ToolStripMenuItem tsmi_openSAPDataFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
        private System.Windows.Forms.ToolStripMenuItem tsmi_openStageAnalysisTEXTDataFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ana_dwg_file;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ana_inp_dwg;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ana_res_dwg;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ana_inp_exm;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ana_res_exm;
        private System.Windows.Forms.ToolStripMenuItem tsmi_newAnalysisSAPDataFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_newAnalysisDWGDataFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_sap_manual;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
        private System.Windows.Forms.ToolStripMenuItem tsmi_link_1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_link_2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
        private System.Windows.Forms.ToolStripMenuItem tsmi_design_manual;
        private System.Windows.Forms.ToolStripMenuItem tsmi_users_manual;
        private System.Windows.Forms.ToolStripMenuItem tsmi_rcc_structure_design;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Steel_Truss_Bridge_Warren_3;
        private System.Windows.Forms.ToolStripMenuItem tsmi_tunnel_design;
        private System.Windows.Forms.ToolStripMenuItem tsmi_CostEstimation;
        private System.Windows.Forms.ToolStripMenuItem tsmi_cable_suspension_bridge;
        private System.Windows.Forms.ToolStripMenuItem tsmi_steel_tables;
        private System.Windows.Forms.ToolStripMenuItem tsmi_re_wall_des_strc;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pier_1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pier_2;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pier_3;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pier_4;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pier_5;
        private System.Windows.Forms.ToolStripMenuItem tsmi_extradossed;
        private System.Windows.Forms.ToolStripMenuItem tsmi_pIleFoundation;
        private System.Windows.Forms.ToolStripMenuItem tsmi_jetty_design;
        private System.Windows.Forms.ToolStripMenuItem tsmi_jetty_design_RCC;
        private System.Windows.Forms.ToolStripMenuItem tsmi_jetty_design_PSC;
        private System.Windows.Forms.ToolStripMenuItem tsmi_raftFoundation;
        private System.Windows.Forms.ToolStripMenuItem tsmi_extradossed_side_towers;
        private System.Windows.Forms.ToolStripMenuItem tsmi_extradossed_central_towers;
        private System.Windows.Forms.ToolStripMenuItem tsmi_road_box_pushed_underpass;
        private System.Windows.Forms.ToolStripMenuItem tsmi_rail_box_pushed_underpass;
        private System.Windows.Forms.ToolStripMenuItem tsmi_GADs_Underpasses;
        private System.Windows.Forms.ToolStripMenuItem tsmi_transmissionTower;
        private System.Windows.Forms.ToolStripMenuItem tsmi_microwaveTower;
        private System.Windows.Forms.ToolStripMenuItem tsmi_cableCarTower;
        private System.Windows.Forms.ToolStripMenuItem tsmi_structureModeling;
        private System.Windows.Forms.ToolStripMenuItem tsmi_RCC_Pier_LS;
        private System.Windows.Forms.ToolStripMenuItem tsmi_transmissionTower3Cables;
        private System.Windows.Forms.ToolStripMenuItem tsmi_research_Studies;
        private System.Windows.Forms.ToolStripMenuItem tsmi_HSTBD;
        private System.Windows.Forms.ToolStripMenuItem tsmi_PDelta;
        private System.Windows.Forms.ToolStripMenuItem tsmi_selectWorkingFolderResearch;
        private System.Windows.Forms.ToolStripMenuItem tsmi_streamHydrologyResearch;
        private System.Windows.Forms.ToolStripMenuItem tsmi_dynamicAnalysis;
        private System.Windows.Forms.ToolStripMenuItem tsmi_timeHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmi_responseSpectrum;
        private System.Windows.Forms.ToolStripMenuItem tsmi_eigenValue;
        private System.Windows.Forms.ToolStripMenuItem tsmi_RCC_TGirder;
        private System.Windows.Forms.ToolStripMenuItem tsmi_RCC_TGirder_LSM;
        private System.Windows.Forms.ToolStripMenuItem tsmi_RCC_TGirder_WSM;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Composite;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Composite_LSM;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Composite_WSM;
        private System.Windows.Forms.ToolStripMenuItem tsmi_PSC_IGirder;
        private System.Windows.Forms.ToolStripMenuItem tsmi_PSC_IGirder_LSM;
        private System.Windows.Forms.ToolStripMenuItem tsmi_PSC_IGirder_WSM;
        private System.Windows.Forms.ToolStripMenuItem tsmi_minor_Bridge_ls;
        private System.Windows.Forms.ToolStripMenuItem tsmi_minor_Bridge_ws;
        private System.Windows.Forms.ToolStripMenuItem tsmi_RCC_Culverts_LS;
        private System.Windows.Forms.ToolStripMenuItem tsmi_RCC_Culverts_WS;
        private System.Windows.Forms.ToolStripMenuItem hydraulicCalculationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_structure_text;
        private System.Windows.Forms.ToolStripMenuItem tsmi_structure_sap;
        private System.Windows.Forms.ToolStripMenuItem tsmi_steel_beam;
        private System.Windows.Forms.ToolStripMenuItem tsmi_steel_column;
        private System.Windows.Forms.ToolStripMenuItem sheetPileApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_sheet_pile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_return_wall_des_strc;
        private System.Windows.Forms.ToolStripMenuItem tsmi_return_wall_cant;
        private System.Windows.Forms.ToolStripMenuItem tsmi_return_wall_propped;
    }
}

