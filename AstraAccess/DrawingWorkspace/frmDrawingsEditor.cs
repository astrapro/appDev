using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.Interface;
using AstraInterface.DataStructure;


using VectorDraw.Geometry;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;


namespace AstraAccess.DrawingWorkspace
{
    public partial class frmDrawingsEditor : Form
    {
        IApplication iApp;

        public string Drawing_Path { get; set; }
        public string SRC_Drawing_Path { get; set; }


        eBaseDrawings BaseDrawingType { get; set; }


        public string ReportFile { get; set; }

        public string App_Title { get; set; }
        public frmDrawingsEditor(IApplication app, eBaseDrawings BaseDrawing, string draw_path, string report_file)
        {
            InitializeComponent();
            iApp = app;

            Drawing_Path = draw_path;

            BaseDrawingType = BaseDrawing;
            ReportFile = report_file;
            App_Title = "";
        }
        public frmDrawingsEditor(IApplication app, eBaseDrawings BaseDrawing, string Title, string draw_path, string report_file)
        {
            InitializeComponent();
            iApp = app;

            Drawing_Path = draw_path;

            BaseDrawingType = BaseDrawing;
            ReportFile = report_file;
            App_Title = Title;
        }

        public void Set_SourceDrawing()
        {
            switch (BaseDrawingType)
            {
                #region RCC T Girder Bridge
                case eBaseDrawings.RCC_T_Girder_LS_GAD:

                    lbl_Title.Text = "RCC T Girder Bridge";
                    lbl_Drawing.Text = "General Arrangement Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_T_Girder_LS_GAD;
                    break;
                case eBaseDrawings.RCC_T_Girder_LS_LONG_GIRDER:

                    lbl_Title.Text = "RCC T Girder Bridge";
                    lbl_Drawing.Text = "Long Main Girder Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_T_Girder_LS_LONG_GIRDER;
                    break;
                case eBaseDrawings.RCC_T_Girder_LS_CROSS_GIRDER:
                    lbl_Title.Text = "RCC T Girder Bridge ";
                    lbl_Drawing.Text = "Cross Girder Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_T_Girder_LS_CROSS_GIRDER;
                    break;
                case eBaseDrawings.RCC_T_Girder_LS_DECK_SLAB:
                    lbl_Title.Text = "RCC T Girder Bridge";
                    lbl_Drawing.Text = "Deck Slab Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_T_Girder_LS_DECK_SLAB;
                    break;
                case eBaseDrawings.RCC_T_GIRDER_LS_CANTILEVER_ABUTMENT:
                    lbl_Title.Text = "RCC T Girder Bridge";
                    lbl_Drawing.Text = "Cantilever Abutment Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_T_GIRDER_LS_CANTILEVER_ABUTMENT;
                    break;
                case eBaseDrawings.RCC_T_Girder_LS_COUNTERFORT_ABUTMENT:
                    lbl_Title.Text = "RCC T Girder Bridge";
                    lbl_Drawing.Text = "Counterfort Abutment Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_T_Girder_LS_COUNTERFORT_ABUTMENT;
                    break;
                case eBaseDrawings.RCC_T_GIRDER_LS_PIER:
                    lbl_Title.Text = "RCC T Girder Bridge";
                    lbl_Drawing.Text = "Pier Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_T_GIRDER_LS_PIER;
                    break;
                #endregion RCC T Girder Bridge

                #region RCC Minor Bridge
                case eBaseDrawings.RCC_MINOR_BRIDGE_DECKSLAB:
                    lbl_Title.Text = "RCC Minor Bridge ";
                    lbl_Drawing.Text = "Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_MINOR_BRIDGE_DECKSLAB;
                    break;
                case eBaseDrawings.RCC_MINOR_BRIDGE_COUNTERFORT_ABUTMENT:
                    lbl_Title.Text = "RCC Minor Bridge ";
                    lbl_Drawing.Text = "Counterfort Abutment Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_MINOR_BRIDGE_ABUTMENT_COUNTERFORT;
                    break;
                case eBaseDrawings.RCC_MINOR_BRIDGE_CANTILEVER_ABUTMENT:
                    lbl_Title.Text = "RCC Minor Bridge ";
                    lbl_Drawing.Text = "Cantilever Abutment Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_MINOR_BRIDGE_ABUTMENT_CANTILEVER;
                    break;
                case eBaseDrawings.RCC_MINOR_BRIDGE_PIER:
                    lbl_Title.Text = "RCC Minor Bridge ";
                    lbl_Drawing.Text = "Pier Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_MINOR_BRIDGE_ABUTMENT_PIER;
                    break;
                #endregion RCC Minor Bridge



                #region RCC CULVERT

                case eBaseDrawings.RCC_CULVERT_BOX_MULTICELL:
                    lbl_Title.Text = "RCC Culvert ";
                    lbl_Drawing.Text = "Multicell Box Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_CULVERT_BOX_MULTICELL;
                    break;


                case eBaseDrawings.RCC_CULVERT_BOX_MULTICELL_WITH_EARTH_CUSION:
                    lbl_Title.Text = "RCC Culvert ";
                    lbl_Drawing.Text = "Multicell Box With Earth Cushion Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_CULVERT_BOX_MULTICELL_WITH_EARTH_CUSION;
                    break;

                case eBaseDrawings.RCC_CULVERT_BOX_MULTICELL_WITHOUT_EARTH_CUSION:
                    lbl_Title.Text = "RCC Culvert ";
                    lbl_Drawing.Text = "Multicell Box Without Earth Cushion Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_CULVERT_BOX_MULTICELL_WITHOUT_EARTH_CUSION;
                    break;


                case eBaseDrawings.RCC_CULVERT_PIPE:
                    lbl_Title.Text = "RCC Culvert ";
                    lbl_Drawing.Text = "Pipe Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_CULVERT_PIPE;
                    break;
                #endregion RCC CULVERT



                #region RCC Foundation

                case eBaseDrawings.RCC_FOUNDATION_PILE:
                    lbl_Title.Text = "RCC Foundation ";
                    lbl_Drawing.Text = "Pile Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_FOUNDATION_PILE;
                    break;
                case eBaseDrawings.RCC_FOUNDATION_WELL:
                    lbl_Title.Text = "RCC Foundation ";
                    lbl_Drawing.Text = "Well Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_FOUNDATION_WELL;
                    break;

                #endregion RCC Foundation

                #region PSC_I_Girder_LS_GAD
                case eBaseDrawings.PSC_I_Girder_LS_GAD:

                    lbl_Title.Text = "PSC I Girder Bridge Drawings";
                    //lbl_Drawing.Text = "General Arrangement Drawings";
                    lbl_Drawing.Text = "";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.PSC_I_Girder_LS_GAD;
                    break;
                #endregion PSC_I_Girder_LS_GAD

                #region COMPOSITE_LS_STEEL_PLATE

                case eBaseDrawings.COMPOSITE_LS_STEEL_PLATE:

                    lbl_Title.Text = "Composite Bridge";
                    lbl_Drawing.Text = "Steel Plate Girder Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.COMPOSITE_LS_STEEL_PLATE;
                    break;
                case eBaseDrawings.COMPOSITE_LS_STEEL_BOX:

                    lbl_Title.Text = "Composite Bridge";
                    lbl_Drawing.Text = "Steel Box Girder Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.COMPOSITE_LS_STEEL_BOX;
                    break;
                #endregion COMPOSITE_LS_STEEL_PLATE


                #region PSC_BOX_Girder_GAD
                case eBaseDrawings.PSC_BOX_Girder_GAD:

                    lbl_Title.Text = "PSC Box Girder Bridge";
                    lbl_Drawing.Text = "Drawings";

                    SRC_Drawing_Path = ASTRA_BaseDrawings.PSC_BOX_Girder_GAD;
                    break;
                #endregion PSC_BOX_Girder_GAD



                #region RCC Jetty
                case eBaseDrawings.Jetty_RCC:

                    lbl_Title.Text = "RCC Jetty";
                    lbl_Drawing.Text = "Drawings";

                    if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                        SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_JETTY_GAD;
                    else
                        SRC_Drawing_Path = ASTRA_BaseDrawings.RCC_JETTY_GAD_BS;
                    break;
                #endregion RCC_BOX_Girder_GAD



                #region PSC Jetty
                case eBaseDrawings.Jetty_PSC:

                    lbl_Title.Text = "PSC Jetty";
                    lbl_Drawing.Text = "Drawings";
                    if (iApp.DesignStandard == eDesignStandard.IndianStandard)
                        SRC_Drawing_Path = ASTRA_BaseDrawings.PSC_JETTY_GAD;
                    else
                        SRC_Drawing_Path = ASTRA_BaseDrawings.PSC_JETTY_GAD_BS;

                    break;
                #endregion PSC_BOX_Girder_GAD
            }

            if(App_Title != "")
            {
                lbl_Title.Text = App_Title;
            }
        }
        private void lst_drawings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_drawings.SelectedItems.Count == 0) return;

            string flname = Path.Combine(Drawing_Path, lst_drawings.SelectedItem.ToString());
            uC_CAD1.VDoc.Open(flname);
            uC_CAD1.VDoc.Redraw(true);

            Refresh_Drawing();
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(uC_CAD1.VDoc);

            return;
        }



        private void Refresh_Drawing()
        {

            vdDocument VD = uC_CAD1.VDoc;

            VD.OrthoMode = false;
            dgv_dim.Rows.Clear();



            Hashtable ht = lst_drawings.Tag as Hashtable;

            if (ht == null) ht = new Hashtable();

            ht.Clear();

            List<string> lst = new List<string>();

            EditDrawing ed = new EditDrawing(iApp.DesignStandard);

            ed.Set_Drawings(VD);

            ed.Fill_Dimentions(dgv_dim);
            ed.Fill_Reinforcement(dgv_reinf);
            ed.Fill_Area_Reinforcement(dgv_ast);
            ed.Fill_Notes(dgv_notes);

            ed.Fill_Cell_Reinforcement(dgv_box);
            ed.Fill_Cable_Profile(dgv_cbl_profile);


            foreach (var item in ed.All_CABLE)
            {
                cmb_cbl_nos.Items.Add(item);
            }

            if (cmb_cbl_nos.Items.Count > 0)
            {
                cmb_cbl_nos.SelectedIndex = 0;
            }


            //ed.Fill_Cable_Coordinates(dgv_cbl_coordinates, "1");
            ed.Fill_Cable_Coordinates(dgv_cbl_coordinates, cmb_cbl_nos.Text);

            
            if (ed.BD != null)
            {
                EditText et = ed.BD.Box_Title["1"] as EditText;
                if (et != null) txt_box_deg_title.Text = et.Value;


                et = ed.BD.Box_Weight_Steel["1"] as EditText;
                if (et != null) txt_box_steel_wgt.Text = et.Value;

                et = ed.BD.Box_Weight_Concrete["1"] as EditText;
                if (et != null) txt_box_concrete_wgt.Text = et.Value;
            }

            //if (dgv_reinf.RowCount == 0 && dgv_ast.RowCount == 0)
            //{
            //    sc_struc_dtls.Panel1Collapsed = true;
            //}
            //else
            //{
            //    sc_struc_dtls.Panel2Collapsed = false;
            //    if (dgv_reinf.RowCount == 0)
            //    {
            //        sc_reinf.Panel1Collapsed = true;
            //    }
            //    else if (dgv_ast.RowCount == 0)
            //    {
            //        sc_reinf.Panel2Collapsed = true;
            //    }
            //}

            tc_structures.TabPages.Clear();


            if (dgv_dim.RowCount != 0)
            {
                tc_structures.TabPages.Add(tab_dims);
            }
            if (dgv_reinf.RowCount != 0 || dgv_ast.RowCount != 0)
            {
                tc_structures.TabPages.Add(tab_rebars);
            }

            if (dgv_reinf.RowCount == 0)
            {
                sc_reinf.Panel1Collapsed = true;
            }
            else if (dgv_ast.RowCount == 0)
            {
                sc_reinf.Panel2Collapsed = true;
            }



            #region Rearrange Tabs

            tc_inputs.TabPages.Clear();

            if (dgv_dim.RowCount != 0 || dgv_reinf.RowCount != 0 || dgv_ast.RowCount != 0)
            {
                tc_inputs.TabPages.Add(tab_strc_details);
            }

            if (dgv_notes.RowCount != 0)
            {
                tc_inputs.TabPages.Add(tab_drgs_notes);
            }

            if (dgv_box.RowCount != 0)
            {
                tc_inputs.TabPages.Add(tab_bar_bnd_schedules);
            }
            if (dgv_cbl_profile.RowCount != 0)
            {
                tc_inputs.TabPages.Add(tab_cable_profile);
            }

            if (dgv_cbl_coordinates.RowCount != 0)
            {
                tc_inputs.TabPages.Add(tab_cable_coordinates);
            }

            #endregion Rearrange Tabs


            lst_drawings.Tag = ed;
            Refresh();
        }
        private void btn_Apply_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == btn_apply2)
            {
                //vdPropertyGrid1.SelectedObject
                Apply_2();
                return;
            }
            Apply();
        }

        private void Apply()
        {


            EditDrawing ed = lst_drawings.Tag as EditDrawing;

            if (ed != null)
            {
                ed.Save_Dimentions(dgv_dim);
                ed.Save_Reinforcement(dgv_reinf);
                ed.Save_AreaReinforcement(dgv_ast);
                ed.Save_Notes(dgv_notes);

                ed.Save_Cell_Reinforcement(dgv_box);
                ed.Save_Cable_Profile(dgv_cbl_profile);
                ed.Save_Cable_Coordinates(dgv_cbl_coordinates, "1");

                if (ed.BD != null)
                {
                    EditText et = ed.BD.Box_Title["1"] as EditText;


                    if (et != null) et.Set_Value(txt_box_deg_title.Text);


                    et = ed.BD.Box_Weight_Concrete["1"] as EditText;
                    if (et != null) et.Set_Value(txt_box_concrete_wgt.Text);


                    et = ed.BD.Box_Weight_Steel["1"] as EditText;
                    if (et != null) et.Set_Value(txt_box_steel_wgt.Text);
                }
              

            }

            uC_CAD1.VDoc.Redraw(true);

            if(MessageBox.Show("Do you want to save this Drawing ?", "ASTRA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (uC_CAD1.VDoc.SaveAs(uC_CAD1.VDoc.FileName)) Refresh_Drawing();
            }
        }

        private void Apply_2()
        {
            if (true)
            {
                #region Rename the texts lable


                vdDocument vd = uC_CAD1.VDoc;


                vdSelections sels = vd.Selections;


                int cou_1 = 1;
                foreach (var item in sels.Last)
                {
                    vdText vt = item as vdText;

                    if (vt != null)
                    {
                        //vt.Label = string.Format("CEL_1_BM_{0}_DIA", cou_1++);
                        vt.Label = string.Format(txt_format.Text, cou_1++);
                        vt.Update();
                    }
                }


                vd.Redraw(true);

                MessageBox.Show("DONE");
                return;

                #endregion Rename the texts lable
            }
        }

        private void dgv_data_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (ht == null) return;
            //vdMText mt = ht[e.RowIndex+1] as vdMText;

            //if (mt == null) return;
            //mt.TextString = dgv_dim[1, e.RowIndex].Value + "=" + dgv_dim[2, e.RowIndex].Value;

            //mt.Update();

            //uC_CAD1.VDoc.Redraw(true);
        }

        private void frmDrawingsEditor_Load(object sender, EventArgs e)
        {
            Load_Drawings();

            uC_CAD1.PropertyGrid = vdPropertyGrid1;
            uC_CAD1.iApp = iApp;

        }

        public void Load_Drawings()
        {
            Set_SourceDrawing();


            Drawing_Path = Path.Combine(Drawing_Path, Path.GetFileName(SRC_Drawing_Path));
            if (!Directory.Exists(Drawing_Path)) Directory.CreateDirectory(Drawing_Path);

            ASTRA_BaseDrawings.Copy_Drawings(Drawing_Path, SRC_Drawing_Path);

            foreach (var flname in Directory.GetFiles(Drawing_Path))
            {
                lst_drawings.Items.Add(Path.GetFileName(flname));
            }


            if (lst_drawings.Items.Count > 0)
                lst_drawings.SelectedIndex = 0;

        }

        private void btn_open_design_report_Click(object sender, EventArgs e)
        {
            if (File.Exists(ReportFile))
            {
                if (Path.GetExtension(ReportFile).ToLower().StartsWith(".x"))
                    iApp.OpenExcelFile(ReportFile, "2011ap");
                else
                    System.Diagnostics.Process.Start(ReportFile);
            }
            else
            {
                MessageBox.Show("Report file not found.", "ASTRA", MessageBoxButtons.OK);
            }
        }

        private void btn_open_drawings_Click(object sender, EventArgs e)
        {
            iApp.RunViewer(Drawing_Path);
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {

            if (lst_drawings.SelectedItem == null) return;


            string src_file = Path.Combine(SRC_Drawing_Path, lst_drawings.SelectedItem.ToString());

            string dest_file = Path.Combine(Drawing_Path, lst_drawings.SelectedItem.ToString());


            if (File.Exists(src_file) && File.Exists(dest_file))
            {
                if (MessageBox.Show("Refresh '" + Path.GetFileName(dest_file) + "' ?", "ASTRA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        File.Copy(src_file, dest_file, true);
                        uC_CAD1.VDoc.Open(dest_file);
                        uC_CAD1.VDoc.Redraw(true);
                        Refresh_Drawing();
                    }
                    catch (Exception exx) { }
                }
            }

        }

        private void uC_CAD1_Open_Drawing(object sender, EventArgs e)
        {

        }

        private void btn_finish_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmb_cbl_nos_SelectedIndexChanged(object sender, EventArgs e)
        {
            EditDrawing ed = lst_drawings.Tag as EditDrawing;

            if(ed != null)
            {
               if( ed.All_CABLE_Coor.Count > 0)
               {
                   ed.Fill_Cable_Coordinates(dgv_cbl_coordinates, cmb_cbl_nos.Text);
               }
            }

        }


    }


    public class EditDrawing
    {
        eDesignStandard DesignStandard { get; set; }
        Hashtable ht_DIM; //for Dimensions
        Hashtable ht_R_DIA; //for Reinforcement Bar Diameter
        Hashtable ht_R_NOS; //for Reinforcement Bar Spacing or Nos of Bars
        Hashtable ht_NT; //for Notes
        Hashtable ht_R_AS; //for Reinforcement Steel AREA

        public Bar_Details BD { get; set; }
        public Cable_Profile CP { get; set; }
        public Cable_Coordinates COR { get; set; }


        Hashtable ht_CELL; //for Multicell Box Culver Reinforcement


        Hashtable ht_CABLE_Profile; //for PSC I GIRDER CABLE PROFILE
        Hashtable ht_CABLE_Coordinates; //for PSC I GIRDER CABLE PROFILE

        List<EditText> lst_Text = new List<EditText>();

        public List<string> All_Dimensions { get; set; }
        public List<string> All_Reinforcements { get; set; }
        public List<string> All_Notes { get; set; }


        public List<string> All_CELL { get; set; }
        public List<string> All_CABLE { get; set; }
        public List<string> All_CABLE_Coor { get; set; }

        public List<EditText> AllTexts
        {
            get
            {

                return lst_Text;
            }
        }
        public EditDrawing(eDesignStandard DesStands)
        {
            DesignStandard = DesStands;

            ht_DIM = new Hashtable();
            ht_R_DIA = new Hashtable();
            ht_R_NOS = new Hashtable();
            ht_R_AS = new Hashtable();
            ht_NT = new Hashtable();

            ht_CELL = new Hashtable();
            ht_CABLE_Profile = new Hashtable();
            ht_CABLE_Coordinates = new Hashtable();


            All_Dimensions = new List<string>();
            All_Reinforcements = new List<string>();
            All_Notes = new List<string>();
            All_CELL = new List<string>();
            All_CABLE = new List<string>();
            All_CABLE_Coor = new List<string>();
        }
        public void Clear()
        {
            ht_DIM.Clear();
            ht_R_DIA.Clear();
            ht_R_NOS.Clear();
            ht_R_AS.Clear();
            ht_NT.Clear();
            ht_CELL.Clear();
            ht_CABLE_Profile.Clear();
            ht_CABLE_Coordinates.Clear();

            All_Dimensions.Clear();
            All_Reinforcements.Clear();
            All_Notes.Clear();
            All_CELL.Clear();
            All_CABLE.Clear();
            All_CABLE_Coor.Clear();


        }

        public void Set_Drawings(vdDocument VD)
        {

            List<string> lst = new List<string>();

            //MyList ml
            for (int i = 0; i < VD.ActiveLayOut.Entities.Count; i++)
            {
                var ent = VD.ActiveLayOut.Entities[i];

                try
                {
                    if (ent is vdMText)
                    {
                        #region vdMText
                        vdMText mt = (vdMText)ent;

                        Add_Text(mt);

                        #endregion vdMText
                    }
                    else if (ent is vdText)
                    {
                        #region vdText
                        vdText mt = (vdText)ent;

                        Add_Text(mt);

                        #endregion vdText
                    }
                }
                catch (Exception exx) { }
            }

            lst.Sort();
            All_Dimensions.Sort();
            //All_Reinforcements.Sort();
            All_Notes.Sort();
            All_CELL.Sort();
            All_CABLE.Sort();




            All_Dimensions = MyList.Sort_Int_Array_2(All_Dimensions);

            All_CABLE_Coor = MyList.Sort_Int_Array(All_CABLE_Coor);

            All_Reinforcements = MyList.Sort_Int_Array(All_Reinforcements);


        }


        public void Add_Text(object obj)
        {
            EditText et = new EditText();


            if (obj is vdMText)
            {
                vdMText mt = obj as vdMText;
                et.Text = mt.TextString;
                et.Label = mt.Label;
            }
            else if (obj is vdText)
            {
                vdText txt = obj as vdText;
                et.Text = txt.TextString;
                et.Label = txt.Label;
            }

            string str = et.Text.Replace("\\A1;", "").Trim();
            str = str.Replace("{", "").Trim();
            str = str.Replace("}", "").Trim();

            et.Text = str.Trim();

            

            MyList ml = new MyList(et.Label, '_');


            #region Reinforcement Details
            if (ml[0].StartsWith("R"))
            {
                if (ml.Count > 1)
                {
                    #region DIA Details

                    if (ml[1].StartsWith("DIA"))
                    {
                        et.Name = ml[2];
                        et.Items.Add(obj);

                        lst_Text.Add(et);

                        EditText r = ht_R_DIA[et.Name] as EditText;

                        if (!All_Reinforcements.Contains(et.Name)) All_Reinforcements.Add(et.Name);


                        if (r != null)
                        {
                            ht_R_DIA.Remove(et.Name);

                            r.Items.Add(obj);
                            ht_R_DIA.Add(et.Name, r);
                        }
                        else
                        {
                            ht_R_DIA.Add(et.Name, et);
                        }
                    }
                    #endregion DIA Details

                    #region BAR NOS Details
                    else if (ml[1].StartsWith("NOS"))
                    {
                        et.Name = ml[2];
                        et.Items.Add(obj);

                        lst_Text.Add(et);

                        EditText r = ht_R_NOS[et.Name] as EditText;
                        if (!All_Reinforcements.Contains(et.Name)) All_Reinforcements.Add(et.Name);

                        if (r != null)
                        {

                            ht_R_NOS.Remove(et.Name);

                            r.Items.Add(obj);
                            ht_R_NOS.Add(et.Name, r);
                        }
                        else
                        {
                            ht_R_NOS.Add(et.Name, et);
                        }
                    }
                    #endregion BAR NOS Details

                    #region AST Details

                    else if (ml[1].StartsWith("AS"))
                    {
                        et.Name = ml[2];
                        et.Items.Add(obj);

                        lst_Text.Add(et);

                        EditText r = ht_R_AS[et.Name] as EditText;

                        if (!All_Reinforcements.Contains(et.Name)) All_Reinforcements.Add(et.Name);

                        if (r != null)
                        {
                            ht_R_AS.Remove(et.Name);
                            r.Items.Add(obj);
                            ht_R_AS.Add(et.Name, r);
                        }
                        else
                        {
                            ht_R_AS.Add(et.Name, et);
                        }

                    }
                    #endregion AST Details
                }
            }
            #endregion Reinforcement Details

            #region Dimension Details

            else if (ml[0].StartsWith("DIM"))
            {
                if (ml.Count > 1)
                {
                    //if (ml.Count > 2)
                    //    et.Name = ml[0] + "=" + ml[1];
                    //else
                    //    et.Name = ml[0];

                    et.Name = ml[1];
                    et.Items.Add(obj);
                    lst_Text.Add(et);
                    //ht_dim.Add(et.Name, et);


                    EditText r = ht_DIM[et.Name] as EditText;
                    if (!All_Dimensions.Contains(et.Name)) All_Dimensions.Add(et.Name);

                    if (r != null)
                    {

                        ht_DIM.Remove(et.Name);

                        r.Items.Add(obj);
                        ht_DIM.Add(et.Name, r);
                    }
                    else
                    {
                        ht_DIM.Add(et.Name, et);
                    }
                }
            }
            #endregion Dimension Details

            #region Note Details

            else if (ml[0].StartsWith("NT"))
            {
                if (ml.Count > 1)
                {
                    if (DesignStandard == eDesignStandard.BritishStandard)
                    {
                        if (ml.Count > 2)
                        {
                            vdText vt = obj as vdText;
                            if (vt != null)
                            {
                                vt.Deleted = true;
                                vt.Update();
                                return;
                            }
                        }
                    }

                    et.Name = ml[1];
                    et.Items.Add(obj);
                    lst_Text.Add(et);
                    if (!All_Notes.Contains(et.Name)) All_Notes.Add(et.Name);



                    EditText r = ht_NT[et.Name] as EditText;

                    if (r != null)
                    {

                        ht_NT.Remove(et.Name);

                        r.Items.Add(obj);
                        ht_NT.Add(et.Name, r);
                    }
                    else
                    {
                        ht_NT.Add(et.Name, et);
                    }
                }
            }
            #endregion Note Details

            #region Multicell Box Culvert Details

            else if (ml[0].StartsWith("CEL"))
            {
                if (ml.Count > 1)
                {
                    et.Name = ml[1];
                    et.Items.Add(obj);
                    lst_Text.Add(et);


                    if (BD == null) BD = new Bar_Details();


                    //if (bd == null)
                    //{
                    //    bd = new Bar_Details();
                    //    ht_CELL.Add(et.Name, bd);
                    //}


                    et.Name = ml[3];


                    Hashtable ht = BD.Bar_Nos;

                    EditText r1 = ht[et.Name] as EditText;


                    if (ml[2].StartsWith("BOX"))
                    {
                        if (ml[3].StartsWith("TIT"))
                        {
                            ht = BD.Box_Title;
                        }
                        else if (ml[3].StartsWith("WGT"))
                        {
                            if (ml[4].StartsWith("ST"))
                            {
                                ht = BD.Box_Weight_Steel;
                            }
                            if (ml[4].StartsWith("CON"))
                            {
                                ht = BD.Box_Weight_Concrete;
                            }
                        }
                        et.Name = ml[1];
                    }
                    if (ml[2].StartsWith("BM"))
                    {
                        if (ml[4].StartsWith("DIA"))
                        {
                            ht = BD.Bar_Dia;
                        }
                        else if (ml[4].StartsWith("SPC"))
                        {
                            ht = BD.Bar_Spacing;
                        }
                        else if (ml[4].StartsWith("NOS"))
                        {
                            ht = BD.Bar_Nos;
                        }
                        else if (ml[4].StartsWith("LEN"))
                        {
                            ht = BD.Bar_Length;
                        }
                        else if (ml[4].StartsWith("M1"))
                        {
                            ht = BD.Bar_M1;
                        }
                        else if (ml[4].StartsWith("M2"))
                        {
                            ht = BD.Bar_M2;
                        }
                        else if (ml[4].StartsWith("TLN"))
                        {
                            ht = BD.Total_Length;
                        }
                        else if (ml[4].StartsWith("WGT"))
                        {
                            ht = BD.Weight;
                        }


                        if (!All_CELL.Contains(et.Name)) All_CELL.Add(et.Name);

                        et.Name = ml[3];

                    }

                    r1 = ht[et.Name] as EditText;
                    if (r1 != null)
                    {
                        ht.Remove(et.Name);
                        r1.Items.Add(obj);
                        ht.Add(et.Name, r1);
                    }
                    else
                    {
                        ht.Add(et.Name, et);
                    }

                    ht = ht_CELL;
                    //et.Name = ml[3];
                    r1 = ht[et.Label] as EditText;
                    if (r1 != null)
                    {
                        ht.Remove(et.Label);
                        r1.Items.Add(obj);
                        ht.Add(et.Label, r1);
                    }
                    else
                    {
                        ht.Add(et.Label, et);
                    }

                }
            }
            #endregion  Multicell Box Culvert Details

            #region  Cable Profile

            else if (ml[0].StartsWith("CBL"))
            {
                if (ml.Count > 2)
                {
                    et.Name = ml[2];
                    et.Items.Add(obj);
                    lst_Text.Add(et);

                    if (ml[1].StartsWith("COR"))
                    {
                        COR = ht_CABLE_Coordinates[et.Name] as Cable_Coordinates;
                        if (COR == null)
                        {
                            COR = new Cable_Coordinates();
                            ht_CABLE_Coordinates.Add(et.Name, COR);
                        }



                        Hashtable ht = COR.Coordinate_X;

                        EditText r1 = ht[et.Name] as EditText;


                        if (ml[3].StartsWith("X"))
                        {
                            ht = COR.Coordinate_X;
                        }
                        else if (ml[3].StartsWith("Y"))
                        {
                            ht = COR.Coordinate_Y;
                        }
                        else if (ml[3].StartsWith("Z"))
                        {
                            ht = COR.Coordinate_Z;
                        }


                        et.Name = ml[4];


                        if (!All_CABLE_Coor.Contains(et.Name)) All_CABLE_Coor.Add(et.Name);


                        r1 = ht[et.Name] as EditText;
                        if (r1 != null)
                        {
                            ht.Remove(et.Name);
                            r1.Items.Add(obj);
                            ht.Add(et.Name, r1);
                        }
                        else
                        {
                            ht.Add(et.Name, et);
                        }

                        //ht = ht_CABLE_Profile;
                        ////et.Name = ml[3];
                        //r1 = ht[et.Label] as EditText;
                        //if (r1 != null)
                        //{
                        //    ht.Remove(et.Label);
                        //    r1.Items.Add(obj);
                        //    ht.Add(et.Label, r1);
                        //}
                        //else
                        //{
                        //    ht.Add(et.Label, et);
                        //}




                    }
                    else
                    {

                        if (CP == null) CP = new Cable_Profile();


                        Hashtable ht = CP.CABLE_NOS;

                        EditText r1 = ht[et.Name] as EditText;


                        if (ml[1].StartsWith("NOS"))
                        {
                            ht = CP.CABLE_NOS;
                        }
                        else if (ml[1].StartsWith("EXT"))
                        {
                            ht = CP.EXTENSION_AT_EACH_JACKING_END;
                        }
                        else if (ml[1].StartsWith("LEN"))
                        {
                            ht = CP.CABLE_LENGTH;
                        }
                        else if (ml[1].StartsWith("ANG"))
                        {
                            ht = CP.EMERGENCE_ANGLE;
                        }
                        else if (ml[1].StartsWith("TYP"))
                        {
                            ht = CP.ANCHORAGE_TYPE;
                        }
                        else if (ml[1].StartsWith("JCK"))
                        {
                            ht = CP.JACKING_FORCE;
                        }
                        else if (ml[1].StartsWith("NST"))
                        {
                            ht = CP.STRANDS_NOS;
                        }

                        if (!All_CABLE.Contains(et.Name)) All_CABLE.Add(et.Name);


                        r1 = ht[et.Name] as EditText;
                        if (r1 != null)
                        {
                            ht.Remove(et.Name);
                            r1.Items.Add(obj);
                            ht.Add(et.Name, r1);
                        }
                        else
                        {
                            ht.Add(et.Name, et);
                        }

                        ht = ht_CABLE_Profile;
                        //et.Name = ml[3];
                        r1 = ht[et.Label] as EditText;
                        if (r1 != null)
                        {
                            ht.Remove(et.Label);
                            r1.Items.Add(obj);
                            ht.Add(et.Label, r1);
                        }
                        else
                        {
                            ht.Add(et.Label, et);
                        }
                    }
                }
            }
            #endregion Cable Profile

            else
            {
                ml = new MyList(et.Text, '=');

                if (ml.Count > 1)
                {
                    if (ml.Count > 2)
                        et.Name = ml[0] + "=" + ml[1];
                    else
                        et.Name = ml[0];

                    if (et.Value != "")
                    {
                        et.Items.Add(obj);
                        lst_Text.Add(et);
                        //ht_dim.Add(et.Name, et);

                        et.Label = ml[0];

                        EditText r = ht_DIM[et.Name] as EditText;
                        if (!All_Dimensions.Contains(et.Name)) All_Dimensions.Add(et.Name);

                        if (r != null)
                        {

                            ht_DIM.Remove(et.Name);

                            r.Items.Add(obj);
                            ht_DIM.Add(et.Name, r);
                        }
                        else
                        {
                            ht_DIM.Add(et.Name, et);
                        }
                    }
                }
            }
        }

        public void Fill_Dimentions(DataGridView dgv)
        {
            EditText et = null;
            dgv.Rows.Clear();
            foreach (var item in All_Dimensions)
            {
                et = ht_DIM[item] as EditText;
                if (et != null)
                    dgv.Rows.Add(dgv.RowCount + 1, et.Name, et.Value);
            }
        }
        public void Fill_Notes(DataGridView dgv)
        {
            EditText et = null;
            dgv.Rows.Clear();
            All_Notes.Sort();
            foreach (var item in All_Notes)
            {
                et = ht_NT[item] as EditText;
                if (et != null)
                    dgv.Rows.Add(et.Name, et.Value);
            }
        }
        public void Fill_Reinforcement(DataGridView dgv)
        {
            EditText et_dia = null;
            EditText et_nos = null;
            dgv.Rows.Clear();
            foreach (var item in All_Reinforcements)
            {
                et_dia = ht_R_DIA[item] as EditText;
                et_nos = ht_R_NOS[item] as EditText;
                if (et_dia != null && et_nos != null)
                    dgv.Rows.Add(dgv.RowCount + 1, et_dia.Name, et_dia.Value, et_nos.Value);
            }
        }

        public void Fill_Area_Reinforcement(DataGridView dgv)
        {
            EditText et_AS = null;
            dgv.Rows.Clear();
            foreach (var item in All_Reinforcements)
            {
                et_AS = ht_R_AS[item] as EditText;
                if (et_AS != null)
                    dgv.Rows.Add(dgv.RowCount + 1, et_AS.Name, et_AS.Value);
            }
        }

        public void Fill_Cell_Reinforcement(DataGridView dgv)
        {
            EditText et_DIA = null;
            EditText et_SPC = null;
            EditText et_M1 = null;
            EditText et_M2 = null;
            EditText et_LEN = null;
            EditText et_NOS = null;
            EditText et_TLN = null;
            EditText et_WGT = null;

            dgv.Rows.Clear();

            Bar_Details db = BD;


            if (db == null) return;




            foreach (var item in All_CELL)
            {
                et_DIA = db.Bar_Dia[item] as EditText;
                et_SPC = db.Bar_Spacing[item] as EditText;
                et_M1 = db.Bar_M1[item] as EditText;
                et_M2 = db.Bar_M2[item] as EditText;
                et_LEN = db.Bar_Length[item] as EditText;
                et_NOS = db.Bar_Nos[item] as EditText;
                et_TLN = db.Total_Length[item] as EditText;
                et_WGT = db.Weight[item] as EditText;


                if (et_DIA != null && et_DIA != null)
                {
                    dgv.Rows.Add(dgv.RowCount + 1
                       , et_DIA.Name
                       , et_DIA.Value
                       , et_SPC.Value
                       , et_M1.Value
                       , et_M2.Value
                       , et_LEN.Value
                       , et_NOS.Value
                       , et_TLN.Value
                       , et_WGT.Value
                        );
                }
            }
        }

        public void Fill_Cable_Profile(DataGridView dgv)
        {
            EditText et_NOS = null;
            EditText et_EXT = null;
            EditText et_LEN = null;
            EditText et_ANG = null;
            EditText et_TYP = null;
            EditText et_JCK = null;
            EditText et_NST = null;

            dgv.Rows.Clear();



            if (CP == null) return;

            foreach (var item in All_CABLE)
            {
                et_NOS = CP.CABLE_NOS[item] as EditText;
                et_EXT = CP.EXTENSION_AT_EACH_JACKING_END[item] as EditText;
                et_LEN = CP.CABLE_LENGTH[item] as EditText;
                et_ANG = CP.EMERGENCE_ANGLE[item] as EditText;
                et_TYP = CP.ANCHORAGE_TYPE[item] as EditText;
                et_JCK = CP.JACKING_FORCE[item] as EditText;
                et_NST = CP.STRANDS_NOS[item] as EditText;

                if (et_NOS != null
                    && et_EXT != null
                    && et_LEN != null
                    && et_ANG != null
                    && et_TYP != null
                    && et_JCK != null
                    && et_NST != null
                    )
                {
                    dgv.Rows.Add(

                        et_NOS.Value
                       , et_EXT.Value
                       , et_LEN.Value
                       , et_ANG.Value
                       , et_TYP.Value
                       , et_JCK.Value
                       , et_NST.Value
                        );
                }
            }
        }


        public void Fill_Cable_Coordinates(DataGridView dgv, string cable_no)
        {

            EditText et_X = null;
            EditText et_Y = null;
            EditText et_Z = null;
           

            dgv.Rows.Clear();


            COR = ht_CABLE_Coordinates[cable_no] as Cable_Coordinates;


            Cable_Coordinates CORX = ht_CABLE_Coordinates["0"] as Cable_Coordinates;

            if (COR == null) return;



            foreach (var item in All_CABLE_Coor)
            {
                et_X = CORX.Coordinate_X[item] as EditText;
                et_Y = COR.Coordinate_Y[item] as EditText;
                et_Z = COR.Coordinate_Z[item] as EditText;

                if (et_X != null && et_Y != null && et_Z != null)
                {
                    dgv.Rows.Add(item,
                        et_X.Value
                       , et_Y.Value
                       , et_Z.Value
                        );
                }
            }
        }

        public void Save_Dimentions(DataGridView dgv)
        {
            EditText et = null;
            //dgv.Rows.Clear();
            int i = 0;
            string str = "";
            vdDocument vdoc = null;
            for (i = 0; i < dgv.RowCount; i++)
            {
                str = dgv[1, i].Value.ToString();

                et = ht_DIM[str] as EditText;
                if (et != null)
                    foreach (var item in et.Items)
                    {
                        if (item is vdMText)
                        {
                            vdMText mt = item as vdMText;

                            //mt.TextString = str + " = " + dgv[2, i].Value.ToString();

                            if (mt.TextString.Contains("="))
                            {
                                if (mt.TextString.Contains("= "))
                                    mt.TextString = str + " = " + dgv[2, i].Value.ToString();
                                else
                                    mt.TextString = str + "=" + dgv[2, i].Value.ToString();
                            }
                            else
                                mt.TextString = dgv[2, i].Value.ToString();


                            mt.Update();

                            if (vdoc == null) vdoc = mt.Document;
                        }
                        else if (item is vdText)
                        {
                            vdText t = item as vdText;

                            //if (t.TextString.Contains("= "))
                            //    t.TextString = str + " = " + dgv[2, i].Value.ToString();
                            //else
                            //    t.TextString = dgv[2, i].Value.ToString();


                            if (t.TextString.Contains("="))
                            {
                                if (t.TextString.Contains("= "))
                                    t.TextString = str + " = " + dgv[2, i].Value.ToString();
                                else
                                    t.TextString = str + "=" + dgv[2, i].Value.ToString();
                            }
                            else
                                t.TextString = dgv[2, i].Value.ToString();



                            t.Update();
                            if (vdoc == null) vdoc = t.Document;
                        }
                    }
            }

            if (vdoc != null)
                vdoc.Redraw(true);

        }
        public void Save_Reinforcement(DataGridView dgv)
        {
            EditText et_dia = null;
            EditText et_nos = null;
            //dgv.Rows.Clear();
            string str = "";
            int i = 0;
            for (i = 0; i < dgv.RowCount; i++)
            {
                str = dgv[1, i].Value.ToString();

                et_dia = ht_R_DIA[str] as EditText;
                et_nos = ht_R_NOS[str] as EditText;

                foreach (var item in et_dia.Items)
                {
                    if (item is vdMText)
                    {
                        vdMText mt = item as vdMText;

                        mt.TextString = dgv[2, i].Value.ToString();
                        mt.Update();
                    }
                    if (item is vdText)
                    {
                        vdText t = item as vdText;

                        t.TextString = dgv[2, i].Value.ToString();
                        t.Update();
                    }
                }
                foreach (var item in et_nos.Items)
                {
                    if (item is vdMText)
                    {
                        vdMText mt = item as vdMText;

                        mt.TextString = dgv[3, i].Value.ToString();
                        mt.Update();
                    }
                    if (item is vdText)
                    {
                        vdText t = item as vdText;

                        t.TextString = dgv[3, i].Value.ToString();
                        t.Update();
                    }
                }
            }
        }

        public void Save_AreaReinforcement(DataGridView dgv)
        {
            EditText et = null;
            //dgv.Rows.Clear();
            int i = 0;
            string str = "";
            vdDocument vdoc = null;
            for (i = 0; i < dgv.RowCount; i++)
            {
                str = dgv[1, i].Value.ToString();

                et = ht_R_AS[str] as EditText;
                if (et != null)
                    foreach (var item in et.Items)
                    {
                        if (item is vdMText)
                        {
                            vdMText mt = item as vdMText;

                            //mt.TextString = str + " = " + dgv[2, i].Value.ToString();

                            if (mt.TextString.Contains("="))
                            {
                                if (mt.TextString.Contains("= "))
                                    mt.TextString = str + " = " + dgv[2, i].Value.ToString();
                                else
                                    mt.TextString = str + "=" + dgv[2, i].Value.ToString();
                            }
                            else
                                mt.TextString = dgv[2, i].Value.ToString();


                            mt.Update();

                            if (vdoc == null) vdoc = mt.Document;
                        }
                        else if (item is vdText)
                        {
                            vdText t = item as vdText;

                            //if (t.TextString.Contains("= "))
                            //    t.TextString = str + " = " + dgv[2, i].Value.ToString();
                            //else
                            //    t.TextString = dgv[2, i].Value.ToString();

                            if (t.TextString.Contains("="))
                            {
                                if (t.TextString.Contains("= "))
                                    t.TextString = str + " = " + dgv[2, i].Value.ToString();
                                else
                                    t.TextString = str + "=" + dgv[2, i].Value.ToString();
                            }
                            else
                                t.TextString = dgv[2, i].Value.ToString();


                            t.Update();
                            if (vdoc == null) vdoc = t.Document;
                        }
                    }
            }
            if (vdoc != null)
                vdoc.Redraw(true);

        }

        public void Save_Cell_Reinforcement(DataGridView dgv)
        {
            EditText et_dia = null;

            Hashtable coll = new Hashtable();
            //dgv.Rows.Clear();
            string str = "";
            int i = 0;

            vdDocument vdoc = null;
            Bar_Details bd = BD;

            for (i = 0; i < dgv.RowCount; i++)
            {
                str = dgv[1, i].Value.ToString();

                //et_dia = bd.Bar_Dia[str] as EditText;
                //coll.Add(et_dia);

                //et_dia = bd.Bar_Spacing[str] as EditText;
                //coll.Add(et_dia);

                //et_dia = bd.Bar_M1[str] as EditText;
                //coll.Add(et_dia);
                //et_dia = bd.Bar_M2[str] as EditText;
                //coll.Add(et_dia);
                //et_dia = bd.Bar_Length[str] as EditText;
                //coll.Add(et_dia);
                //et_dia = bd.Bar_Nos[str] as EditText;
                //coll.Add(et_dia);
                //et_dia = bd.Total_Length[str] as EditText;
                //coll.Add(et_dia);
                //et_dia = bd.Weight[str] as EditText;
                //coll.Add(et_dia);


                //for (int c = 2; c < dgv.ColumnCount; c++)
                //for (int c = 2; c < 3; c++)
                for (int c = 2; c < dgv.ColumnCount; c++)
                {

                    if (c == 2) et_dia = bd.Bar_Dia[str] as EditText;
                    else if (c == 3) et_dia = bd.Bar_Spacing[str] as EditText;
                    else if (c == 4) et_dia = bd.Bar_M1[str] as EditText;
                    else if (c == 5) et_dia = bd.Bar_M2[str] as EditText;
                    else if (c == 6) et_dia = bd.Bar_Length[str] as EditText;
                    else if (c == 7) et_dia = bd.Bar_Nos[str] as EditText;
                    else if (c == 8) et_dia = bd.Total_Length[str] as EditText;
                    else if (c == 9) et_dia = bd.Weight[str] as EditText;

                    foreach (var item in et_dia.Items)
                    {
                        if (item is vdMText)
                        {
                            vdMText mt = item as vdMText;

                            mt.TextString = dgv[c, i].Value.ToString();
                            mt.Update();
                            //mt.Document.Redraw(true);

                            if (vdoc == null) vdoc = mt.Document;
                        }
                        if (item is vdText)
                        {
                            vdText t = item as vdText;

                            t.TextString = dgv[c, i].Value.ToString();
                            t.Update();
                            //t.Document.Redraw(true);
                            if (vdoc == null) vdoc = t.Document;
                        }
                    }
                }
            }
            if (vdoc != null) vdoc.Redraw(true);
        }

        public void Save_Cable_Profile(DataGridView dgv)
        {
            EditText et_dia = null;

            Hashtable coll = new Hashtable();
            //dgv.Rows.Clear();
            string str = "";
            int i = 0;

            vdDocument vdoc = null;

            if (CP == null) return;

            for (i = 0; i < dgv.RowCount; i++)
            {
                str = dgv[0, i].Value.ToString();

                for (int c = 2; c < dgv.ColumnCount; c++)
                {

                    if (c == 1) et_dia = CP.EXTENSION_AT_EACH_JACKING_END[str] as EditText;
                    else if (c == 2) et_dia = CP.CABLE_LENGTH[str] as EditText;
                    else if (c == 3) et_dia = CP.EMERGENCE_ANGLE[str] as EditText;
                    else if (c == 4) et_dia = CP.ANCHORAGE_TYPE[str] as EditText;
                    else if (c == 5) et_dia = CP.JACKING_FORCE[str] as EditText;
                    else if (c == 6) et_dia = CP.STRANDS_NOS[str] as EditText;

                    et_dia.Set_Value(dgv[c, i].Value.ToString());
                }
            }
            if (vdoc != null) vdoc.Redraw(true);
        }


        public void Save_Cable_Coordinates(DataGridView dgv, string cable_no)
        {
            EditText et = null;

            Hashtable coll = new Hashtable();
         
            string str = "";

            int i = 0;

            vdDocument vdoc = null;

            if (COR == null) return;

            Cable_Coordinates CORX = ht_CABLE_Coordinates["0"] as Cable_Coordinates;

            for (i = 0; i < dgv.RowCount; i++)
            {
                str = dgv[0, i].Value.ToString();

                for (int c = 1; c < dgv.ColumnCount; c++)
                {

                    if (c == 1) et = CORX.Coordinate_X[str] as EditText;
                    else if (c == 2) et = COR.Coordinate_Y[str] as EditText;
                    else if (c == 3) et = COR.Coordinate_Z[str] as EditText;

                    if (et != null)
                    {
                        et.Set_Value(dgv[c, i].Value.ToString());
                    }
                }
            }
            if (vdoc != null) vdoc.Redraw(true);
        }

        public void Save_Notes(DataGridView dgv)
        {
            EditText et = null;
            //dgv.Rows.Clear();
            int i = 0;
            string str = "";
            vdDocument vdoc = null;
            for (i = 0; i < dgv.RowCount; i++)
            {
                str = dgv[0, i].Value.ToString();

                et = ht_NT[str] as EditText;
                if (et != null)
                    foreach (var item in et.Items)
                    {
                        if (item is vdMText)
                        {
                            vdMText mt = item as vdMText;

                            //mt.TextString = str + " = " + dgv[2, i].Value.ToString();


                            mt.TextString = dgv[1, i].Value.ToString();

                            //if (mt.TextString.Contains("="))
                            //{
                            //    if (mt.TextString.Contains("= "))
                            //        mt.TextString = str + " = " + dgv[2, i].Value.ToString();
                            //    else
                            //        mt.TextString = str + "=" + dgv[2, i].Value.ToString();
                            //}
                            //else
                            //    mt.TextString = dgv[2, i].Value.ToString();


                            mt.Update();

                            if (vdoc == null) vdoc = mt.Document;
                        }
                        else if (item is vdText)
                        {
                            vdText t = item as vdText;

                            t.TextString = dgv[1, i].Value.ToString();


                            t.Update();
                            if (vdoc == null) vdoc = t.Document;
                        }
                    }
            }

            if (vdoc != null)
                vdoc.Redraw(true);

        }

    }

    public class EditText
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public string Label { get; set; }

        public List<object> Items { get; set; }


        public string Value
        {
            get
            {

                string str = Text.Replace("\\A1;", "").Trim();
                str = str.Replace("\\P;", "").Trim();


                MyList ml = new MyList(str, '=');

                //if (ml.StringList.Count > 2) return (ml[0] + "=" + ml[1]);

                //if (ml.StringList.Count > 1)
                //    return ml[1];

                return ml[ml.StringList.Count - 1];
            }
        }
        public EditText()
        {
            Name = "";
            Text = "";
            Label = "";
            Items = new List<object>();
        }

        public void Set_Value(string val)
        {
            MyList ml = null;


            if (Text.Contains("="))
            {
                ml = new MyList(Text, '=');

                if(ml.Count == 2)
                {
                    val = ml[0] + "=" + val;
                }
            }

            foreach (var item in Items)
            {
                
                if (item is vdMText)
                {
                    vdMText mt = item as vdMText;
                    mt.TextString = val;
                    mt.Update();
                }
                if (item is vdText)
                {
                    vdText t = item as vdText;
                    t.TextString = val;
                    t.Update();
                }
            }
        }
    }

    public class Multicell_Box_Cell_Details
    {
        //public string Box_Cell_Designation: 3/22/3	
        public string Box_Cell_Designation { get; set; }


    }
    public class Bar_Details
    {


        //Bar Mark    	Bar Dia.	Spacing	M1	M2	        	LENGTH	Number of Bars   	Total Length    	Weight
        //    (MM.)            	(MM.)         	(MM.)         	(MM.)    	(MM.)    	(Nos.)	(Mtrs.)              	(KG.)
        public Hashtable Bar_Mark { get; set; }
        public Hashtable Bar_Dia { get; set; }
        public Hashtable Bar_Spacing { get; set; }
        public Hashtable Bar_M1 { get; set; }
        public Hashtable Bar_M2 { get; set; }
        public Hashtable Bar_Length { get; set; }
        public Hashtable Bar_Nos { get; set; }
        public Hashtable Total_Length { get; set; }
        public Hashtable Weight { get; set; }
        public Hashtable Box_Title { get; set; }
        public Hashtable Box_Weight_Steel { get; set; }
        public Hashtable Box_Weight_Concrete { get; set; }
        public Bar_Details()
        {

            Bar_Mark = new Hashtable();
            Bar_Dia = new Hashtable();
            Bar_Spacing = new Hashtable();
            Bar_M1 = new Hashtable();
            Bar_M2 = new Hashtable();
            Bar_Length = new Hashtable();
            Bar_Nos = new Hashtable();
            Total_Length = new Hashtable();
            Weight = new Hashtable();

            Box_Title = new Hashtable();
            Box_Weight_Steel = new Hashtable();
            Box_Weight_Concrete = new Hashtable();
        }

    }

    public class Cable_Profile
    {
        //CABLE NO.
        //EXTENSION AT EACH JACKING END (mm)
        //CABLE LENGTH (CUT LENGTH) IN (M)
        //EMERGENCE ANGLE IN (DEGREE)
        //ANCHORAGE TYPE
        //JACKING FORCE (kN)
        //NO. OF STRANDS
        public Hashtable CABLE_NOS { get; set; }
        public Hashtable EXTENSION_AT_EACH_JACKING_END { get; set; }
        public Hashtable CABLE_LENGTH { get; set; }
        public Hashtable EMERGENCE_ANGLE { get; set; }
        public Hashtable ANCHORAGE_TYPE { get; set; }
        public Hashtable JACKING_FORCE { get; set; }
        public Hashtable STRANDS_NOS { get; set; }

        public Cable_Profile()
        {
            CABLE_NOS = new Hashtable();
            EXTENSION_AT_EACH_JACKING_END = new Hashtable();
            CABLE_LENGTH = new Hashtable();
            EMERGENCE_ANGLE = new Hashtable();
            ANCHORAGE_TYPE = new Hashtable();
            JACKING_FORCE = new Hashtable();
            STRANDS_NOS = new Hashtable();
        }


    }

    public class Cable_Coordinates
    {
        public Hashtable Coordinate_X { get; set; }
        public Hashtable Coordinate_Y { get; set; }
        public Hashtable Coordinate_Z { get; set; }

        public Cable_Coordinates()
        {
            Coordinate_X = new Hashtable();
            Coordinate_Y = new Hashtable();
            Coordinate_Z = new Hashtable();
        }
    }
}
