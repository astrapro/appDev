using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AstraInterface.DataStructure;
using AstraInterface.Interface;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


using BridgeAnalysisDesign;


namespace LimitStateMethod.CostEstimation
{
    public partial class frm_CostEstimation : Form
    {
        IApplication iApp;
        public frm_CostEstimation(IApplication app)
        {
            InitializeComponent();
            iApp = app;
        }


        #region BOQ
        public void Load_Default_Box_BOQ_Data()
        {
            MyList mlist = null;

            cmb_boq_item.SelectedIndex = 0;

           
            List<string> list = new List<string>();

            #region Default Chainage

            list.Clear();
            //list.Add(string.Format("1        429+207                         429+207 "));
            //list.Add(string.Format("2        460+715                                460+971"));
            //list.Add(string.Format("3        470+240                                470+512"));
            //list.Add(string.Format("4        470+980                                471+248"));
            //list.Add(string.Format("5        475+330                                475+754"));


            list.Add(string.Format("1,429+207,429+207,1x7.0,7,2x12.0,3.50,24.00,168.00"));
            list.Add(string.Format("2,460+715,460+971,1x7.0,7,2x12.0,3.50,24.00,168.00"));
            list.Add(string.Format("3,470+240,470+512,1x7.0,7,2x12.0,3.50,24.00,168.00"));
            list.Add(string.Format("4,470+980,471+248,1x8.0,8,2x12.0,3.50,24.00,192.00"));
            list.Add(string.Format("5,475+330,475+754,1x8.0,8,2x12.0,3.50,24.00,192.00"));


            foreach (var item in list)
            {
                mlist = new MyList(MyList.RemoveAllSpaces(item), ',');

                //mlist.StringList.Insert(0, "");
                dgv_box_gen_data.Rows.Add(mlist.StringList.ToArray());
            }

            #endregion Default Chainage
            #region Structural Data

            list.Clear();

            //list.Add(string.Format("Skew angle,20.31,"));

            list.Add(string.Format("Structural Data,,"));
            list.Add(string.Format("Median Width,m,1.750,1.750"));
            list.Add(string.Format("Carrigeway Width (outer to outer Crash Barrier and Railing),m,12.000,12.000"));
            list.Add(string.Format("Length,m,13.750,13.750"));
            list.Add(string.Format("Clear Width,m,7.449,7.449"));
            list.Add(string.Format("Nos of Vent,nos,1,1"));
            list.Add(string.Format("Height of Box,m,3.850,3.850"));
            list.Add(string.Format("Floor Thickness,m,0.700,0.700"));
            list.Add(string.Format("Wall Thickness,m,0.550,0.550"));
            list.Add(string.Format("Slab Thickness,m,0.550,0.550"));
            list.Add(string.Format("Intermediate Wall Thickness,m,0.000,0.000"));
            list.Add(string.Format("Raft Offset,m,0.000,0.000"));
            list.Add(string.Format(",,"));
            list.Add(string.Format("FRL at Median / Center,,"));
            list.Add(string.Format("Cross Slope,m,-0.025,-0.025"));
            list.Add(string.Format("Invert Level,m,0.000,0.000"));
            list.Add(string.Format("OGL,m,0.000,0.000"));
            list.Add(string.Format("Fill Slope,m ,1.500,1.500"));
            list.Add(string.Format("Approach Slab,m,1.000,1.000"));
            list.Add(string.Format(",,"));
            list.Add(string.Format("Retaining Wall,,"));
            list.Add(string.Format("Length,m,6.365,6.375"));
            list.Add(string.Format(",,"));
            list.Add(string.Format("Curtain Wall,,"));
            //list.Add(string.Format(",,"));
            list.Add(string.Format("Total Bottom Width,m,1.300,1.650"));
            list.Add(string.Format("Overall Height,m,2.000,2.500"));
            list.Add(string.Format("Height of Bottom Offset,m,0.800,0.550"));
            list.Add(string.Format("Height of 2nd Offset,m,0.750,0.750"));
            list.Add(string.Format("Width of 2nd Offset,m,0.450,0.750"));
            list.Add(string.Format("Height of 3rd Offset,m,0.450,0.750"));
            list.Add(string.Format("Width of 3rd Offset,m,0.200,0.450"));
            list.Add(string.Format("Height of 4th Offset,m,0.000,0.450"));
            list.Add(string.Format("Width of 4th Offset,m,0.000,0.200"));
            list.Add(string.Format(",,"));
            list.Add(string.Format("Flexible Apron,,"));
            list.Add(string.Format("Width,m,3.000,6.000"));
            list.Add(string.Format("Thickness of Apron,m,0.750,0.750"));
            list.Add(string.Format(",,"));
            list.Add(string.Format("Cone Pitching and Filter Media,,"));
            //list.Add(string.Format(",,"));
            list.Add(string.Format("Stone Pitching Thickness,m,0.300,0.300"));
            list.Add(string.Format("Filter Media Thickness,m,0.150,0.150"));
            //list.Add(string.Format(",,"));

            foreach (var item in list)
            {
                mlist = new MyList(item,',');

                mlist.StringList.Insert(0,"");
                dgv_box_input_data.Rows.Add(mlist.StringList.ToArray());
            }
            #endregion Structural Data

            #region Quantity
            list.Clear();
            list.Add(string.Format("1,Earthwork Excavation,,"));
            list.Add(string.Format(",Box LHS,LHS,1"));
            list.Add(string.Format(",Shear Key,LHS,1"));
            list.Add(string.Format(",Retaining Wall,LHS,2"));
            list.Add(string.Format(",Rigid Apron,LHS,1"));
            list.Add(string.Format(",,LHS,2"));
            list.Add(string.Format(",Flexible Apron,LHS,1"));
            list.Add(string.Format(",Curtain Wall,LHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format(",Box RHS,RHS,1"));
            list.Add(string.Format(",Shear Key,RHS,1"));
            list.Add(string.Format(",Retaining Wall,RHS,2"));
            list.Add(string.Format(",Rigid Apron,RHS,1"));
            list.Add(string.Format(",,RHS,-2"));
            list.Add(string.Format(",Flexible Apron,RHS,1"));
            list.Add(string.Format(",Curtain Wall,RHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("2,M15 PCC for PCC,,"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format(",Box LHS,LHS,1"));
            list.Add(string.Format(",Shear Key,LHS,1"));
            list.Add(string.Format(",Retaining Wall,LHS,2"));
            list.Add(string.Format(",Rigid Apron,LHS,1"));
            list.Add(string.Format(",,LHS,-1"));
            list.Add(string.Format(",Flexible Apron,LHS,1"));
            list.Add(string.Format(",Curtain Wall,LHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format(",Box RHS,RHS,1"));
            list.Add(string.Format(",Shear Key,RHS,1"));
            list.Add(string.Format(",Retaining Wall,RHS,2"));
            list.Add(string.Format(",Rigid Apron,RHS,1"));
            list.Add(string.Format(",,RHS,-1"));
            list.Add(string.Format(",Flexible Apron,RHS,1"));
            list.Add(string.Format(",Curtain Wall,RHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("3,Backfiling Behind Wall / Box,,"));
            list.Add(string.Format(",Box LHS,LHS,2"));
            list.Add(string.Format(",Retaining Wall LHS,LHS,2"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format(",Box RHS,RHS,2"));
            list.Add(string.Format(",Retaining Wall RHS,RHS,2"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("4,Filter Media Behind Wall,,"));
            list.Add(string.Format(",Box LHS,LHS,2"));
            list.Add(string.Format(",Retaining Wall LHS,LHS,2"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format(",Box RHS,RHS,2"));
            list.Add(string.Format(",Retaining Wall RHS,RHS,2"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("5,Providing M15 for Wall,,"));
            list.Add(string.Format(",Curtain Wall LHS,LHS,1"));
            list.Add(string.Format(",,LHS,1"));
            list.Add(string.Format(",,LHS,1"));
            list.Add(string.Format(",,LHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format(",Curtain Wall RHS,RHS,1"));
            list.Add(string.Format(",,RHS,1"));
            list.Add(string.Format(",,RHS,1"));
            list.Add(string.Format(",,RHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("6,M30 Concrete for Raft,,"));
            list.Add(string.Format(",Box Raft,LHS,1"));
            list.Add(string.Format(",Shear Key,LHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format(",Box Raft,RHS,1"));
            list.Add(string.Format(",Shear Key,RHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("7,M30 for Retaining Wall,,"));
            list.Add(string.Format(",Raft,LHS,1"));
            list.Add(string.Format(",,,1"));
            list.Add(string.Format(",Raft,RHS,1"));
            list.Add(string.Format(",,,1"));
            list.Add(string.Format("8,HYSD FE 500 for Foundation,,"));
            list.Add(string.Format(",For Box,,"));
            list.Add(string.Format(",For Retaining Wall,,"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("9,M 30 for Substructure Wall,,"));
            list.Add(string.Format(",For Box,LHS,2"));
            list.Add(string.Format(",Intermediate Wall,LHS,0"));
            list.Add(string.Format(",For Box,RHS,2"));
            list.Add(string.Format(",Intermediate Wall,RHS,0"));
            list.Add(string.Format(",Haunch,LHS,1"));
            list.Add(string.Format(",,RHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format(",Bracket for Approach Slab,LHS,1"));
            list.Add(string.Format(",,RHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("10,M30 for Retaining Wall,,"));
            list.Add(string.Format(",Wall,LHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format(",Wall,RHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("11,HYSD FE 500 for Sub Structure,,"));
            list.Add(string.Format(",For Box,,"));
            list.Add(string.Format(",For Retaining Wall,,"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("12,M30 for Superstructure,,"));
            list.Add(string.Format(",Box,LHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format(",Box,RHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("13,HYSD FE 500 for Superstructure,,"));
            list.Add(string.Format(",For Box,,"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("14,Providing Weep Hole,,"));
            list.Add(string.Format(",Box Culvert,LHS,53"));
            list.Add(string.Format(",Box Culvert,RHS,53"));
            list.Add(string.Format(",Retaining Wall,LHS,56"));
            list.Add(string.Format(",Retaining Wall,RHS,56"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("15,M15 Approach Slab,,"));
            list.Add(string.Format(",Box Culvert,LHS,2"));
            list.Add(string.Format(",Box Culvert,RHS,2"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("16,M30 Approach Slab including Reinforcement,,"));
            list.Add(string.Format(",Box Culvert,LHS,2"));
            list.Add(string.Format(",Box Culvert,RHS,2"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("17,M40 RCC Crash Barrier,,"));
            list.Add(string.Format(",Box Culvert ,LHS,2"));
            list.Add(string.Format(",Box Culvert ,RHS,2"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("18,Wearing Coat,,"));
            list.Add(string.Format(",Box Culvert ,LHS,1"));
            list.Add(string.Format(",Box Culvert ,RHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("19,Providing Filler Type Joint,,"));
            list.Add(string.Format(",Box ,LHS,2"));
            list.Add(string.Format(",Box ,RHS,2"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("20,Providing Filter Media at Slope,,"));
            list.Add(string.Format(",,LHS,2"));
            list.Add(string.Format(",,RHS,2"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("21,Providing Stone Pitching at Slope,,"));
            list.Add(string.Format(",,LHS,2"));
            list.Add(string.Format(",,RHS,2"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("22,Providing Rigid Apron (150 mm Flat Stone with 300 mm PCC),,"));
            list.Add(string.Format(",,LHS,1"));
            list.Add(string.Format(",,,2"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format(",,RHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("23,750 mm Boulder Apron,,"));
            list.Add(string.Format(",,LHS,1"));
            list.Add(string.Format(",,RHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("24,Numbering of Structure,,"));
            list.Add(string.Format(",,LHS,1"));
            list.Add(string.Format(",,RHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("24,Providing Handrail,,"));
            list.Add(string.Format(",,LHS,1"));
            list.Add(string.Format(",,RHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("25,Providing Footpath,,"));
            list.Add(string.Format(",,LHS,1"));
            list.Add(string.Format(",,RHS,1"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("26,Painting of RCC Crash Barrier,,"));
            list.Add(string.Format(",,LHS,3"));
            list.Add(string.Format(",,RHS,3"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("27,Draiange Spout,,"));
            list.Add(string.Format(",,LHS,0"));
            list.Add(string.Format(",,RHS,0"));
            list.Add(string.Format(",,,"));
            list.Add(string.Format("28,Parapet Wall as per Drawing.,,"));
            list.Add(string.Format(",,LHS,0"));
            list.Add(string.Format(",,RHS,0"));
            list.Add(string.Format(",,,"));


            foreach (var item in list)
            {
                mlist = new MyList(item, ',');

                //mlist.StringList.Insert(0, "");
                dgv_box_input_qty.Rows.Add(mlist.StringList.ToArray());
            }


            #endregion Quantity




            Box_Modified_Cell();
        }


        public void Load_Default_Girder_BOQ_Data()
        {
            MyList mlist = null;

            cmb_boq_item.SelectedIndex = 1;


            List<string> list = new List<string>();

            #region Default Chainage

            list.Clear();
            //list.Add(string.Format("1        429+207                         429+207 "));
            //list.Add(string.Format("2        460+715                                460+971"));
            //list.Add(string.Format("3        470+240                                470+512"));
            //list.Add(string.Format("4        470+980                                471+248"));
            //list.Add(string.Format("5        475+330                                475+754"));

            list.Add(string.Format("1$454+64$454+675$1x26.6+1x38.75+1x26.6$91.95$1 x 12.0$12$1103.4"));


            //DataTable dt = new DataTable();

            //dt.Compute()
            foreach (var item in list)
            {
                mlist = new MyList(MyList.RemoveAllSpaces(item), '$');

                //mlist.StringList.Insert(0, "");
                dgv_girder_gen_data.Rows.Add(mlist.StringList.ToArray());
            }

            #endregion Default Chainage



            #region Structural Data

            list.Clear();

            //list.Add(string.Format("Skew angle,20.31,"));

            list.Add(string.Format("Structure Data$"));
            list.Add(string.Format("Overall Length of Bridge Dirt Wall to Dirt Wall$m$91.95"));
            list.Add(string.Format("Clear Height at Abutment$m$6.52"));
            list.Add(string.Format("Clear Height of Wall Pier$m$5.25"));
            list.Add(string.Format("Skew angle$deg$0.0"));
            list.Add(string.Format("$"));
            list.Add(string.Format("Average Span (c/c of expansion gap)$m$30.65"));
            list.Add(string.Format("Total width of structure$m$12.00"));
            list.Add(string.Format("Thickness of M-15 levelling course$m$0.10"));
            list.Add(string.Format("$"));
            list.Add(string.Format("Abutments$"));
            list.Add(string.Format("Length of abutment$m$12.00"));
            list.Add(string.Format("No. of Abutments (Total Both Side )$nos$2"));
            list.Add(string.Format("Foundation (Open)$nos$1"));
            list.Add(string.Format("Width 1 (Earth Side)$m$3.000"));
            list.Add(string.Format("Width 2 (Wall Thickness)$m$1.000"));
            list.Add(string.Format("Width 3 (River Side)$m$2.000"));
            list.Add(string.Format("Straight Depth of foundation $m$0.500"));
            list.Add(string.Format("Slant Depth of foundation $m$0.500"));
            list.Add(string.Format("Foundation depth below GL $m$3.000"));
            list.Add(string.Format("Abutment wall$"));
            list.Add(string.Format("Top width of wall $m$1.20"));
            list.Add(string.Format("$"));
            list.Add(string.Format("Abutment cap$"));
            list.Add(string.Format("Top width $m$1.57"));
            list.Add(string.Format("Slant height$m$0.40"));
            list.Add(string.Format("Straight height$m$0.40"));
            list.Add(string.Format("Dirt wall$"));
            list.Add(string.Format("width $m$0.30"));
            list.Add(string.Format("Area of bracket$sq.m$0.14"));
            list.Add(string.Format("Pier$"));
            list.Add(string.Format("Total No. of Pier  (Both Side)$nos$2"));
            list.Add(string.Format("Length of Pier$m$1.50"));
            list.Add(string.Format("Bottom Width of pier$m$1.50"));
            list.Add(string.Format("Top width of pier$m$1.50"));
            list.Add(string.Format("Pier Foundation (Open)$"));
            list.Add(string.Format("Length of foundation at bottom$m$8.45"));
            list.Add(string.Format("Length of foundation at Top$m$5.85"));
            list.Add(string.Format("Bottom width of foundation$m$6.20"));
            list.Add(string.Format("Top width of foundation $m$1.20"));
            list.Add(string.Format("Straight Depth of foundation $m$0.50"));
            list.Add(string.Format("Slant Depth of foundation $m$0.80"));
            list.Add(string.Format("Foundation depth below GL$m$3.00"));
            list.Add(string.Format("Pier Cap - Normal$"));
            list.Add(string.Format("length of top $m$10.40"));
            list.Add(string.Format("length of bottom $m$2.50"));
            list.Add(string.Format("Bottom width$m$2.30"));
            list.Add(string.Format("Top width$m$2.30"));
            list.Add(string.Format("Slant height $m$1.50"));
            list.Add(string.Format("Straight height $m$0.50"));
            list.Add(string.Format(" Super Structure Slab$"));
            list.Add(string.Format("Depth of deck slab for Only I Girder Type Slab$m$0.2"));
            list.Add(string.Format(" RCC Return wall with Abutment wall$"));
            list.Add(string.Format("Nr. Of Return wall$nos$4"));
            list.Add(string.Format("Width of wall$m$0.50"));
            list.Add(string.Format("Independent Median Wall$"));
            list.Add(string.Format("Nr. of Median wall$m$"));
            list.Add(string.Format("Length of Median wall$"));
            list.Add(string.Format("Bottom width found.$m$6.60"));
            list.Add(string.Format("$"));
            list.Add(string.Format("Independent Retaining Wall$"));
            list.Add(string.Format("Nr. of Retaining wall$m$0"));
            list.Add(string.Format("$"));
            list.Add(string.Format("Well Foundation$"));
            list.Add(string.Format("No of Abutment$nos$0"));
            list.Add(string.Format("No of Pier$nos$0"));
            list.Add(string.Format("Abutment Well Dia$8"));
            list.Add(string.Format("Pier Well Dia$m$6"));
            list.Add(string.Format("Height of Curb in Abutment$m$2.5"));
            list.Add(string.Format("Height of Curb in Pier$m$1.9"));
            list.Add(string.Format("Well Wall Thickness Abutment$m$1.25"));
            list.Add(string.Format("Well Wall Thickness Pier$m$1.00"));
            list.Add(string.Format("Well Total Depth Abutment$m$20.00"));
            list.Add(string.Format("Well Total Depth Pier$m$30.00"));
            list.Add(string.Format("Well Cap Thickness Abutment$m$1.80"));
            list.Add(string.Format("Well Cap Thickness Pier$m$1.80"));
            list.Add(string.Format("Nos of Intermediate including Top Plug for Abutment$nos$1"));
            list.Add(string.Format("Nos of Intermediate including Top Plug for Pier$nos$1"));
            list.Add(string.Format("Abutment Well Cap Length$m$11.7"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(",,"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Pile Data$"));
            list.Add(string.Format("$"));
            list.Add(string.Format("Abutment$nos$1"));
            list.Add(string.Format("Nos of Foundation$nos$2"));
            list.Add(string.Format("Pile Cap Length$m$12.3"));
            list.Add(string.Format("Width of Pile Cap$m$12.3"));
            list.Add(string.Format("Pile Cap Thickness$m$1.8"));
            list.Add(string.Format("Nos of Pile per Foundation$nos$16"));
            list.Add(string.Format("Length of Pile$m$29.11"));
            list.Add(string.Format("Depth of Pile Cap from OGL$m$0.5"));
            list.Add(string.Format("Pier$nos$1"));
            list.Add(string.Format("Nos of Foundation$nos$2"));
            list.Add(string.Format("Pile Cap Length$m$6.6"));
            list.Add(string.Format("Width of Pile Cap$m$7.4"));
            list.Add(string.Format("Pile Cap Thickness$m$1.8"));
            list.Add(string.Format("Nos of Pile per Foundation$nos$5"));
            list.Add(string.Format("Length of Pile$m$35.78"));
            list.Add(string.Format("Depth of Pile Cap from OGL$m$0.5"));
            list.Add(string.Format("Depth of Linear$m$3.50"));
            list.Add(string.Format("$"));
            list.Add(string.Format("Girder (BOX Type)$"));
            list.Add(string.Format("Height of PSC Box Girder$m$"));
            list.Add(string.Format("Bottom Slab Thicknes at Support$m$0.50"));
            list.Add(string.Format("Bottom Slab Thicknes at Mid$m$0.35"));
            list.Add(string.Format("Top Slab Thickness$m$0.25"));
            list.Add(string.Format("Overall Width of Slab$m$10.30"));
            list.Add(string.Format("Bottom Width of Deck$m$4.70"));
            list.Add(string.Format("Slope Width of Box$m$1.00"));
            list.Add(string.Format("Cantilever Width $m$1.80"));
            list.Add(string.Format("Side Wall Thickness at End$m$0.60"));
            list.Add(string.Format("Side Wall Thickness at Mid$m$0.35"));
            list.Add(string.Format("Nos of Haunches at End$nos$2"));
            list.Add(string.Format("Area of Haunch at End$sq.m$0.15"));
            list.Add(string.Format("Nos of Haunch at Mid$nos$4.00"));
            list.Add(string.Format("Area of Haunch at Mid$nos$0.09"));
            list.Add(string.Format("Slab Thickness at Cantilever Point$m$0.35"));
            list.Add(string.Format("Slab Thickness at Outer Edge$m$0.20"));
            list.Add(string.Format("End Diaphragm Thickness$m$0.70"));
            list.Add(string.Format(" $$"));
            list.Add(string.Format("Composite Superstructure $"));
            list.Add(string.Format("No of Span$nos$0"));
            list.Add(string.Format("Length of Span$m$25"));
            list.Add(string.Format("Nos of Steel Girder$nos$4"));
            list.Add(string.Format("Total Length $m$0.0"));
            list.Add(string.Format("Width$m$12"));
            list.Add(string.Format("Area$m$0.0"));
            list.Add(string.Format(""));
            foreach (var item in list)
            {
                mlist = new MyList(item, '$');

                mlist.StringList.Insert(0, "");
                dgv_girder_input_data.Rows.Add(mlist.StringList.ToArray());
            }
            #endregion Structural Data





            #region Structural Data 2

            list.Clear();

            //list.Add(string.Format("Skew angle,20.31,"));

            list.Add(string.Format("Girder$$$"));
            list.Add(string.Format("No. of Span$nos$0$0"));
            list.Add(string.Format("No. of Girder$nos$0$0"));
            list.Add(string.Format("Total length of girder$m$29.63$18.63"));
            list.Add(string.Format("End Portion$m$1.38$1.38"));
            list.Add(string.Format("Variable Portion$m$1.5$1.5"));
            list.Add(string.Format("$$$"));
            list.Add(string.Format("Mid section$$$"));
            list.Add(string.Format("Depth of Girder$m$0$1.77"));
            list.Add(string.Format("Bottom width$m$0.6$0.6"));
            list.Add(string.Format("Total depth at Bottom$m$0.4$0.4"));
            list.Add(string.Format("Straight depth at bottom$m$0.25$0.25"));
            list.Add(string.Format("Slant depth of Bottom$m$0.15$0.15"));
            list.Add(string.Format("Web Thickness$m$0.29$0.3"));
            list.Add(string.Format("$$$"));
            list.Add(string.Format("Top Width$m$1$1"));
            list.Add(string.Format("$$$"));
            list.Add(string.Format("Str. Depth at top$m$0.15$0.15"));
            list.Add(string.Format("Slant depth of top$m$0.1$0.1"));
            list.Add(string.Format("$$$"));
            list.Add(string.Format("End section$$$"));
            list.Add(string.Format("Slant height$m$0.05$0.05"));
            list.Add(string.Format("Straight height$m$0.15$0.15"));
            list.Add(string.Format("No of Intermediate Cross Beam$nos$0$0"));
            list.Add(string.Format("Superstructure Slab above RCC Beam / PSC Beam$$$"));
            list.Add(string.Format("Width of Footpath$m$1.5$1.5"));
            list.Add(string.Format("Depth of slab$m$0.24$0.24"));
            list.Add(string.Format("length of intermediate cross girder$m$2.71$2.7"));
            list.Add(string.Format("length of cross girder at end$m$2.4$2.4"));
            list.Add(string.Format("Width of Cross Girder$m$0.4$0"));
            list.Add(string.Format("$$$"));
            list.Add(string.Format("Average Pedestal Height + Bearing$m$0.4$0.4"));
            list.Add(string.Format("Seismic Arrestar$$$"));
            list.Add(string.Format("Nos of Seismic Arrestar$nos$0$0"));
            list.Add(string.Format("Length$m$1.2$0"));
            list.Add(string.Format("Width of Cross Girder$m$1.2$0"));
            list.Add(string.Format("Height$m$1.5$0"));
            //list.Add(string.Format(""));
          
            foreach (var item in list)
            {
                mlist = new MyList(item, '$');

                mlist.StringList.Insert(0, "");
                dgv_girder_input_data2.Rows.Add(mlist.StringList.ToArray());
            }
            #endregion Structural Data




             



            Girder_Modified_Cell();
        }


        public void Load_Default_ROB_BOQ_Data()
        {
            MyList mlist = null;

            cmb_boq_item.SelectedIndex = 2;

            List<string> list = new List<string>();

            #region Default Chainage

            list.Clear();
            //list.Add(string.Format("1        429+207                         429+207 "));
            //list.Add(string.Format("2        460+715                                460+971"));
            //list.Add(string.Format("3        470+240                                470+512"));
            //list.Add(string.Format("4        470+980                                471+248"));
            //list.Add(string.Format("5        475+330                                475+754"));


            list.Add(string.Format("1$485+920$485+920$$36.319+37.019+39.2+38.5$151.038$2x11.0$22.00$3323"));
            //list.Add(string.Format("")); 
            foreach (var item in list)
            {
                mlist = new MyList(MyList.RemoveAllSpaces(item), '$');

                //mlist.StringList.Insert(0, "");
                dgv_rob_gen_data.Rows.Add(mlist.StringList.ToArray());
            }

            #endregion Default Chainage



            #region Structural Data

            list.Clear();

            list.Add(string.Format("Overall Length of Bridge Dirt Wall to Dirt Wall$m$48.18"));
            list.Add(string.Format("Clear Height at Abutment$m$7.525"));
            list.Add(string.Format("Clear Height of Wall Pier$m$7.525"));
            list.Add(string.Format("Skew angle$deg$49"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Span (c/c of expansion gap)$m$16.06"));
            list.Add(string.Format("Total width of structure Deck Width Outer to Outer$m$12"));
            list.Add(string.Format("Thickness of M-10 levelling course$m$0.1"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Abutments$$"));
            list.Add(string.Format("Length of abutment$m$6"));
            list.Add(string.Format("No. of Abutments (Total Both Side )$nos$4"));
            list.Add(string.Format("Foundation (Open)$$0"));
            list.Add(string.Format("Bottom width of foundation$m$7.8"));
            list.Add(string.Format("Bottom Length of Foundation$m$13.4"));
            list.Add(string.Format("Top Width of Foundation$m$1"));
            list.Add(string.Format("Top Length of Foundation$m$13.4"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Straight Depth of foundation$m$0.75"));
            list.Add(string.Format("Slant Depth of foundation$m$0.75"));
            list.Add(string.Format("Foundation depth below GL$m$3.5"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Abutment wall$$"));
            list.Add(string.Format("Bottom width of wall$m$1.2"));
            list.Add(string.Format("top width of wall$m$1.2"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Abutment cap$$"));
            list.Add(string.Format("Top width$m$2"));
            list.Add(string.Format("Bottom Length$m$6"));
            list.Add(string.Format("Top Length$m$11"));
            list.Add(string.Format("Slant height$m$1.25"));
            list.Add(string.Format("Straight height$m$0.75"));
            list.Add(string.Format("Dirt wall$$"));
            list.Add(string.Format("width$m$0.3"));
            list.Add(string.Format("Area of bracket$sq.m$0.14"));
            list.Add(string.Format("Pier$$"));
            list.Add(string.Format("Total No. of Pier  (Both Side)$nos$4"));
            list.Add(string.Format("Length of Pier$m$6"));
            list.Add(string.Format("Bottom Width of pier$m$1.2"));
            list.Add(string.Format("Top width of pier$m$1.2"));
            list.Add(string.Format("Pier Foundation (Open)$$0"));
            list.Add(string.Format("Length of foundation at bottom$m$10"));
            list.Add(string.Format("Length of foundation at Top$m$5"));
            list.Add(string.Format("Bottom width of foundation$m$8.2"));
            list.Add(string.Format("Top width of foundation$m$1"));
            list.Add(string.Format("Straight Depth of foundation$m$0.75"));
            list.Add(string.Format("Slant Depth of foundation$m$1"));
            list.Add(string.Format("Foundation depth below GL$m$3.5"));
            list.Add(string.Format("Pier Cap - Normal$$"));
            list.Add(string.Format("length of top$m$11"));
            list.Add(string.Format("length of bottom$m$6"));
            list.Add(string.Format("Bottom width$m$3.7"));
            list.Add(string.Format("Top width$m$3.7"));
            list.Add(string.Format("Slant height$m$1.25"));
            list.Add(string.Format("Straight height$m$0.75"));
            list.Add(string.Format("Super Structure Slab$$"));
            list.Add(string.Format("Depth of deck slab for Only I Girder Type Slab$m$0.25"));
            list.Add(string.Format("RCC Return wall with Abutment wall$$"));
            list.Add(string.Format("Nr. Of Return wall$nos$"));
            list.Add(string.Format("Width of wall$m$0"));
            list.Add(string.Format("Independent Median Wall$m$"));
            list.Add(string.Format("Nos. of Median wall$nos$0"));
            list.Add(string.Format("Length of Median wall$m$0"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Independent Retaining Wall$$"));
            list.Add(string.Format("Nr. of Retaining wall$nos$4"));
            list.Add(string.Format("Overall Height of Retaining wall$m$1.75"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Length of Retaining wall$m$70"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Well Foundation$$"));
            list.Add(string.Format("No of Abutment$nos$0"));
            list.Add(string.Format("No of Pier$nos$0"));
            list.Add(string.Format("Abutment Well Dia$mm$8"));
            list.Add(string.Format("Pier Well Dia$mm$6"));
            list.Add(string.Format("Height of Curb in Abutment$m$2.5"));
            list.Add(string.Format("Height of Curb in Pier$m$1.9"));
            list.Add(string.Format("Well Wall Thickness Abutment$m$1.25"));
            list.Add(string.Format("Well Wall Thickness Pier$m$1"));
            list.Add(string.Format("Well Total Depth Abutment$m$20"));
            list.Add(string.Format("Well Total Depth Pier$m$30"));
            list.Add(string.Format("Well Cap Thickness Abutment$m$1.8"));
            list.Add(string.Format("Well Cap Thickness Pier$m$1.8"));
            list.Add(string.Format("Nos of Intermediate including Top Plug for Abutment$nos$1"));
            list.Add(string.Format("Nos of Intermediate including Top Plug for Pier$nos$1"));
            list.Add(string.Format("Abutment Well Cap Length$m$11.7"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Pile Data$$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Abutment$nos$1"));
            list.Add(string.Format("Nos of Foundation in Both Side$nos$4"));
            list.Add(string.Format("Pile Cap Length$m$8.7"));
            list.Add(string.Format("Width of Pile Cap$m$5.1"));
            list.Add(string.Format("Pile Cap Thickness$m$1.8"));
            list.Add(string.Format("Nos of Pile per Foundation$nos$6"));
            list.Add(string.Format("Length of Pile$m$27"));
            list.Add(string.Format("Depth of Pile Cap from OGL$m$0.5"));
            list.Add(string.Format("Pier$nos$1"));
            list.Add(string.Format("Nos of Foundation in Both Side$nos$4"));
            list.Add(string.Format("Pile Cap Length$m$8.7"));
            list.Add(string.Format("Width of Pile Cap$m$5.1"));
            list.Add(string.Format("Pile Cap Thickness$m$1.8"));
            list.Add(string.Format("Nos of Pile per Foundation$nos$6"));
            list.Add(string.Format("Length of Pile$m$27"));
            list.Add(string.Format("Depth of Pile Cap from OGL$m$0.5"));
            list.Add(string.Format("Depth of Linear$m$0"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Girder (BOX Type)$$"));
            list.Add(string.Format("Height of PSC Box Girder$m$2.2"));
            list.Add(string.Format("Bottom Slab Thicknes at Support$m$0.5"));
            list.Add(string.Format("Bottom Slab Thicknes at Mid$m$0.25"));
            list.Add(string.Format("Top Slab Thickness$m$0.25"));
            list.Add(string.Format("Overall Width of Slab$m$15.5"));
            list.Add(string.Format("Bottom Width of Deck$m$8.5"));
            list.Add(string.Format("Slope Width of Box$m$1"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Side Wall Thickness at End$m$0.5"));
            list.Add(string.Format("Side Wall Thickness at Mid$m$0.3"));
            list.Add(string.Format("Nos of Haunches at End$nos$8"));
            list.Add(string.Format("Area of Haunch at End$sq.m$0.01"));
            list.Add(string.Format("Nos of Haunch at Mid$nos$8"));
            list.Add(string.Format("Area of Haunch at Mid$sq.m$0.04"));
            list.Add(string.Format("Slab Thickness at Cantilever Point$m$0.35"));
            list.Add(string.Format("Slab Thickness at Outer Edge$m$0.2"));
            list.Add(string.Format("End Diaphragm Thickness$m$0.55"));
            list.Add(string.Format("Intermediate Beam Area$sq.m$0.51"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("End Diaphragm$$"));
            list.Add(string.Format("Intermediate Beam Area$sq.m$0.51"));
            foreach (var item in list)
            {
                mlist = new MyList(item, '$');

                mlist.StringList.Insert(0, "");
                dgv_rob_input_data.Rows.Add(mlist.StringList.ToArray());
            }
            #endregion Structural Data

          

            #region Structural Data 2

            list.Clear();

            list.Add(string.Format("Girder$$$"));
            list.Add(string.Format("No. of Span$nos$0$0"));
            list.Add(string.Format("No. of Girder$nos$0$0"));
            list.Add(string.Format("Total length of girder$m$29.63$18.63"));
            list.Add(string.Format("End Portion$m$1.38$1.38"));
            list.Add(string.Format("Variable Portion$m$1.5$1.5"));
            list.Add(string.Format("$$$"));
            list.Add(string.Format("Mid section$$$"));
            list.Add(string.Format("Depth of Girder$m$0$1.77"));
            list.Add(string.Format("Bottom width$m$0.6$0.6"));
            list.Add(string.Format("Total depth at Bottom$m$0.4$0.4"));
            list.Add(string.Format("Straight depth at bottom$m$0.25$0.25"));
            list.Add(string.Format("Slant depth of Bottom$m$0.15$0.15"));
            list.Add(string.Format("Web Thickness$m$0.29$0.3"));
            list.Add(string.Format("$$$"));
            list.Add(string.Format("Top Width$m$1$1"));
            list.Add(string.Format("$$$"));
            list.Add(string.Format("Str. Depth at top$m$0.15$0.15"));
            list.Add(string.Format("Slant depth of top$m$0.1$0.1"));
            list.Add(string.Format("$$$"));
            list.Add(string.Format("End section$$$"));
            list.Add(string.Format("Slant height$m$0.05$0.05"));
            list.Add(string.Format("Straight height$m$0.15$0.15"));
            list.Add(string.Format("No of Intermediate Cross Beam$nos$0$0"));
            list.Add(string.Format("Superstructure Slab above RCC Beam / PSC Beam$$$"));
            list.Add(string.Format("Width of Footpath$m$1.5$1.5"));
            list.Add(string.Format("Depth of slab$m$0.24$0.24"));
            list.Add(string.Format("length of intermediate cross girder$m$2.71$2.7"));
            list.Add(string.Format("length of cross girder at end$m$2.4$2.4"));
            list.Add(string.Format("Width of Cross Girder$m$0.4$0"));
            list.Add(string.Format("$$$"));
            list.Add(string.Format("Average Pedestal Height + Bearing$m$0.4$0.4"));
            list.Add(string.Format("Seismic Arrestar$m$$"));
            list.Add(string.Format("Nos of Seismic Arrestar$nos$0$0"));
            list.Add(string.Format("Length$m$1.2$0"));
            list.Add(string.Format("Width of Cross Girder$m$1.2$0"));
            list.Add(string.Format("Height$m$1.5$0"));

            foreach (var item in list)
            {
                mlist = new MyList(item, '$');

                mlist.StringList.Insert(0, "");
                dgv_rob_input_data2.Rows.Add(mlist.StringList.ToArray());
            }
            #endregion Structural Data

            ROB_Modified_Cell();
        }

        private void frm_CostEstimation_Load(object sender, EventArgs e)
        {
            //cmb_boq_item.Items.Clear();
            //cmb_boq_item.Items.Add("RCC Box Type Culverts & Bridges");
            //cmb_boq_item.Items.Add("RCC Girder Type Bridges");
            //cmb_boq_item.Items.Add("Composite Flyover-ROB-RUB");


            Set_Project_Name();




            Load_Default_Girder_BOQ_Data();
            Load_Default_ROB_BOQ_Data();
            Load_Default_Box_BOQ_Data();

            Load_Default_Rate_Analysis_Data();
            BOX_General_Data_Format();
            //Load_Cost_Estimate_Data();

            Girder_Modified_Cell();
            ROB_Modified_Cell();
            cmb_boq_drawings.SelectedIndex = 0;
        }
        public void Box_Modified_Cell()
        {
            string s1, s2, s3, s4;
            int sl_no = 1;


            DataGridView dgv = dgv_box_gen_data;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {

                    dgv[0, i].Value = (i + 1).ToString();
                }
                catch (Exception exx) { }
            }

            //dgv.Columns[3].DefaultCellStyle.BackColor = Color.Gray;
            //dgv.Columns[3].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.0f, FontStyle.Bold);
            //dgv.Columns[3].ReadOnly = true;

            //dgv.Columns[7].DefaultCellStyle.BackColor = Color.Gray;
            //dgv.Columns[7].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.0f, FontStyle.Bold);
            //dgv.Columns[7].ReadOnly = true;
            //dgv.Columns[8].DefaultCellStyle.BackColor = Color.Gray;
            //dgv.Columns[8].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.0f, FontStyle.Bold);
            //dgv.Columns[8].ReadOnly = true;


            #region Format Input Data

            for (int i = 0; i < dgv_box_input_data.RowCount; i++)
            {
                try
                {
                    if (dgv_box_input_data[0, i].Value == "") dgv_box_input_data[0, i].Value = "";
                    if (dgv_box_input_data[1, i].Value == "") dgv_box_input_data[1, i].Value = "";
                    if (dgv_box_input_data[2, i].Value == "") dgv_box_input_data[2, i].Value = "";
                    if (dgv_box_input_data[3, i].Value == "") dgv_box_input_data[3, i].Value = "";



                    s1 = dgv_box_input_data[0, i].Value.ToString();
                    s2 = dgv_box_input_data[1, i].Value.ToString();
                    s3 = dgv_box_input_data[2, i].Value.ToString();
                    s4 = dgv_box_input_data[3, i].Value.ToString();
                    if (s2 != "" && s3 == "" && s4 == "")
                    {
                        dgv_box_input_data.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        dgv_box_input_data.Rows[i].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.0f, FontStyle.Bold);
                        sl_no = 1;
                    }
                    else
                    {
                        if (s2 != "") dgv_box_input_data[0, i].Value = sl_no++;
                    }
                }
                catch (Exception exx) { }
            }

            #endregion Format Input Data


            #region Format Quantity Data

            for (int i = 0; i < dgv_box_input_qty.RowCount; i++)
            {
                try
                {
                    if (dgv_box_input_qty[0, i].Value == "") dgv_box_input_qty[0, i].Value = "";
                    if (dgv_box_input_qty[1, i].Value == "") dgv_box_input_qty[1, i].Value = "";
                    if (dgv_box_input_qty[2, i].Value == "") dgv_box_input_qty[2, i].Value = "";
                    if (dgv_box_input_qty[3, i].Value == "") dgv_box_input_qty[3, i].Value = "";



                    s1 = dgv_box_input_qty[0, i].Value.ToString();
                    s2 = dgv_box_input_qty[1, i].Value.ToString();
                    s3 = dgv_box_input_qty[2, i].Value.ToString();
                    s4 = dgv_box_input_qty[3, i].Value.ToString();
                    if (s2 != "" && s3 == "" && s4 == "")
                    {
                        dgv_box_input_qty.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        dgv_box_input_qty.Rows[i].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.0f, FontStyle.Bold);
                        sl_no = 1;
                    }
                    else
                    {
                        if (s2 != "") dgv_box_input_qty[0, i].Value = sl_no++;
                    }
                }
                catch (Exception exx) { }
            }

            #endregion Format Input Data


             

        }

        public void Girder_Modified_Cell()
        {
            string s1, s2, s3, s4;
            int sl_no = 1;


            DataGridView dgv = dgv_girder_gen_data;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {

                    dgv[0, i].Value = (i + 1).ToString();
                }
                catch (Exception exx) { }
            }

            

            #region Format Input Data
            dgv = dgv_girder_input_data;

            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {
                    if (dgv[0, i].Value == "") dgv[0, i].Value = "";
                    if (dgv[1, i].Value == "") dgv[1, i].Value = "";
                    if (dgv[2, i].Value == "") dgv[2, i].Value = "";
                    //if (dgv_box_input_data[3, i].Value == "") dgv_box_input_data[3, i].Value = "";



                    s1 = dgv[0, i].Value.ToString();
                    s2 = dgv[1, i].Value.ToString();
                    s3 = dgv[2, i].Value.ToString();
                    //s4 = dgv_box_input_data[3, i].Value.ToString();
                    if (s2 != "" && s3 == "")
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        dgv.Rows[i].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.0f, FontStyle.Bold);
                        sl_no = 1;
                    }
                    else
                    {
                        if (s2 != "") dgv[0, i].Value = sl_no++;
                    }
                }
                catch (Exception exx) { }
            }

            #endregion Format Input Data


            #region Format Input Data 2
            dgv = dgv_girder_input_data2;

            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {
                    if (dgv[0, i].Value == "") dgv[0, i].Value = "";
                    if (dgv[1, i].Value == "") dgv[1, i].Value = "";
                    if (dgv[2, i].Value == "") dgv[2, i].Value = "";
                    //if (dgv_box_input_data[3, i].Value == "") dgv_box_input_data[3, i].Value = "";



                    s1 = dgv[0, i].Value.ToString();
                    s2 = dgv[1, i].Value.ToString();
                    s3 = dgv[2, i].Value.ToString();
                    //s4 = dgv_box_input_data[3, i].Value.ToString();
                    if (s2 != "" && s3 == "")
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        dgv.Rows[i].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.0f, FontStyle.Bold);
                        sl_no = 1;
                    }
                    else
                    {
                        if (s2 != "") dgv[0, i].Value = sl_no++;
                    }
                }
                catch (Exception exx) { }
            }

            #endregion Format Input Data

             
        }
        public void ROB_Modified_Cell()
        {
            string s1, s2, s3, s4;
            int sl_no = 1;


            DataGridView dgv = dgv_rob_gen_data;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {

                    dgv[0, i].Value = (i + 1).ToString();
                }
                catch (Exception exx) { }
            }



            #region Format Input Data
            dgv = dgv_rob_input_data;

            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {
                    if (dgv[0, i].Value == "") dgv[0, i].Value = "";
                    if (dgv[1, i].Value == "") dgv[1, i].Value = "";
                    if (dgv[2, i].Value == "") dgv[2, i].Value = "";
                    //if (dgv_box_input_data[3, i].Value == "") dgv_box_input_data[3, i].Value = "";



                    s1 = dgv[0, i].Value.ToString();
                    s2 = dgv[1, i].Value.ToString();
                    s3 = dgv[2, i].Value.ToString();
                    //s4 = dgv_box_input_data[3, i].Value.ToString();
                    if (s2 != "" && s3 == "")
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        dgv.Rows[i].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.0f, FontStyle.Bold);
                        sl_no = 1;
                    }
                    else
                    {
                        if (s2 != "") dgv[0, i].Value = sl_no++;
                    }
                }
                catch (Exception exx) { }
            }

            #endregion Format Input Data


            #region Format Input Data 2
            dgv = dgv_rob_input_data2;

            for (int i = 0; i < dgv.RowCount; i++)
            {
                try
                {
                    if (dgv[0, i].Value == "") dgv[0, i].Value = "";
                    if (dgv[1, i].Value == "") dgv[1, i].Value = "";
                    if (dgv[2, i].Value == "") dgv[2, i].Value = "";
                    //if (dgv_box_input_data[3, i].Value == "") dgv_box_input_data[3, i].Value = "";



                    s1 = dgv[0, i].Value.ToString();
                    s2 = dgv[1, i].Value.ToString();
                    s3 = dgv[2, i].Value.ToString();
                    //s4 = dgv_box_input_data[3, i].Value.ToString();
                    if (s2 != "" && s3 == "")
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        dgv.Rows[i].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 9.0f, FontStyle.Bold);
                        sl_no = 1;
                    }
                    else
                    {
                        if (s2 != "") dgv[0, i].Value = sl_no++;
                    }
                }
                catch (Exception exx) { }
            }

            #endregion Format Input Data


        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        public void Create_Excel_File(string filename)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;

            oXL = new Excel.Application();
            oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
            oSheet = (Excel._Worksheet)oWB.ActiveSheet;

            oSheet.Cells[1, 1] = "First Name";
            oSheet.Cells[1, 2] = "Last Name";
            oSheet.Cells[1, 3] = "Full Name";
            oSheet.Cells[1, 4] = "Salary";


            for (int i = 0; i < dgv_box_input_data.RowCount; i++)
            {
                oSheet.Cells[i + 1, 1] = dgv_box_input_data[1, i].Value.ToString();
                oSheet.Cells[i + 1, 2] = dgv_box_input_data[2, i].Value.ToString();
                oSheet.Cells[i + 1, 3] = dgv_box_input_data[3, i].Value.ToString();
                //oSheet.Cells[i + 1, 4] = dgv_input_data[4, i].Value.ToString();
                
            }



            //Format A1:D1 as bold, vertical alignment = center.
            oSheet.get_Range("A1", "D1").Font.Bold = true;
            oSheet.get_Range("A1", "D1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            oSheet.Name = "NewOne";

            //string filename = "C:\\" + DateTime.Now.ToString("dd_MM_yy_hhmmss");


            //oWB.SaveAs(filename + "asdaa" + ".xlsx");
            oWB.SaveAs(filename);

            oWB.Close();

            Create_Worksheet(filename);
        }
        public void Create_Worksheet(string filePath)
        {
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }

            xlApp.DisplayAlerts = false;
            //string filePath = @"d:\test.xlsx";
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(filePath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Excel.Sheets worksheets = xlWorkBook.Worksheets;

            var xlNewSheet = (Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlNewSheet.Name = "newsheet";
            xlNewSheet.Cells[1, 1] = "New sheet content";

            xlNewSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlNewSheet.Select();

            xlWorkBook.Save();
            xlWorkBook.Close();

            releaseObject(xlNewSheet);
            releaseObject(worksheets);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            MessageBox.Show("New Worksheet Created!");
        }
        private void btn_BOQ_Process_Click(object sender, EventArgs e)
        {


            Button btn = sender as Button;
            string drawing_command, User_Drawing_Folder;
            if (btn.Name == btn_BOQ_new.Name)
            {


                if (Path.GetFileName(user_path) != Project_Name) Create_Project();


                if (cmb_boq_item.SelectedIndex == 0) BOQ_Box_Type_Bridges();
                else if (cmb_boq_item.SelectedIndex == 1) BOQ_Girder_Type_Bridges();
                else if (cmb_boq_item.SelectedIndex == 2) BOQ_ROB_RUB_Type_Bridges();
            }
            else if (btn.Name == btn_boq_open_drawings.Name)
            {
                drawing_command  = "";
                User_Drawing_Folder = cmb_boq_drawings.Text;

                if (cmb_boq_drawings.SelectedIndex == 0) //  Drawings for Box Culvert Single Cell
                {
                    drawing_command = "Box_Culvert_Single_Cell";
                }
                else if (cmb_boq_drawings.SelectedIndex == 1)//Drawings for Box Culvert Double Cell
                {
                    drawing_command = "Box_Culvert_Double_Cell";
                }
                else if (cmb_boq_drawings.SelectedIndex == 2)//Drawings for Box Culvert Tripple Cell
                {
                    drawing_command = "Box_Culvert_3_Cell";
                }
                else if (cmb_boq_drawings.SelectedIndex == 3)//Drawings for Composite Bridges
                {
                    drawing_command = "COST_Composite_Bridges";
                }
                else if (cmb_boq_drawings.SelectedIndex == 4)//Drawings for Girder Bridges
                {
                    drawing_command = "COST_Girder_Bridges";
                }
                else if (cmb_boq_drawings.SelectedIndex == 5)//Drawings for Pipe Culvert
                {
                    drawing_command = "Pipe_Culvert";
                }
                else if (cmb_boq_drawings.SelectedIndex == 6)//Drawings for Slab Culvert
                {
                    drawing_command = "COST_Slab_Culvert";
                }
                iApp.RunViewer(Path.Combine(Get_Project_Folder(), User_Drawing_Folder), drawing_command);
            }
            else 
            {
                drawing_command = "";
                User_Drawing_Folder = "";
                //User_Drawing_Folder = cmb_boq_drawings.Text;

                if (btn.Name == btn_drawings_box_1_cell.Name) //  Drawings for Box Culvert Single Cell
                {
                    drawing_command = "Box_Culvert_Single_Cell";
                    User_Drawing_Folder = "Drawings for Box Culvert Single Cell";
                }
                else if (btn.Name == btn_drawings_box_2_cell.Name)//Drawings for Box Culvert Double Cell
                {
                    drawing_command = "Box_Culvert_Double_Cell";
                    User_Drawing_Folder = "Drawings for Box Culvert Double Cell";
                }
                else if (btn.Name == btn_drawings_box_3_cell.Name)//Drawings for Box Culvert Tripple Cell
                {
                    drawing_command = "Box_Culvert_3_Cell";
                    User_Drawing_Folder = "Drawings for Box Culvert Tripple Cell";
                }
                else if (btn.Name == btn_drawings_composite.Name)//Drawings for Composite Bridges
                {
                    drawing_command = "COST_Composite_Bridges";
                    User_Drawing_Folder = "Drawings for Composite Bridges";
                }
                else if (btn.Name == btn_drawings_girder.Name)//Drawings for Girder Bridges
                {
                    drawing_command = "COST_Girder_Bridges";
                    User_Drawing_Folder = "Drawings for Girder Bridges";
                }
                else if (btn.Name == btn_drawings_pipe.Name)//Drawings for Pipe Culvert
                {
                    drawing_command = "Pipe_Culvert";
                    User_Drawing_Folder = "Drawings for Pipe Culvert";
                }
                else if (btn.Name == btn_drawings_slab.Name)//Drawings for Slab Culvert
                {
                    drawing_command = "COST_Slab_Culvert";
                    User_Drawing_Folder = "Drawings for Slab Culvert";
                }
                iApp.RunViewer(Path.Combine(Get_Project_Folder(), User_Drawing_Folder), drawing_command);
            }
        }



        public string Get_Project_Folder()
        {

            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, "Cost Estimation");

            if(Path.GetFileName(user_path) == Project_Name) file_path = user_path;

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            file_path = Path.Combine(file_path, cmb_boq_item.Text);

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);

            return file_path;
        }
        private void BOQ_Box_Type_Bridges()
        {

            string file_path = Get_Project_Folder();

            //file_path = Path.Combine(file_path, "BoQ_Bridges_Box_Type.xlsx");
            file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text +".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Cost Estimation\BOQ\01 BoQ_Bridges_Box_Type.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["List_MNB"];


            int ch = 27;

            char cr = 'B';

            ch = (int)cr;
            string rng = "";
            string val1 = "";
            string val2 = "";

            Excel.Range formatRange;
            formatRange = myExcelWorksheet.get_Range("b" + (dgv_box_gen_data.RowCount + 8), "L" + (dgv_box_gen_data.RowCount + 8));
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.LightGreen);

            int rindx = 0;
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    int c = 0;
                    if (rindx < dgv_box_gen_data.RowCount - 1)
                    {
                        myExcelWorksheet.get_Range("B" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();

                        myExcelWorksheet.get_Range("C" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();

                        val1 = dgv_box_gen_data[c, rindx].Value.ToString();

                        Excel.Worksheet ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets[4 + rindx];

                        ch1.get_Range("J5").Formula = val1;

                        ch1.Name = val1;

                        #region Update INput Data
                        List<double> lhs = new List<double>();
                        List<double> rhs = new List<double>();
                        for (int k = 0; k < dgv_box_input_data.RowCount; k++)
                        {
                            if (dgv_box_input_data[2, k].Value != null && dgv_box_input_data[3, k].Value != "")
                                lhs.Add(MyList.StringToDouble(dgv_box_input_data[3, k].Value.ToString(), 0.0));


                            if (dgv_box_input_data[4, k].Value != null && dgv_box_input_data[2, k].Value != "")
                                rhs.Add(MyList.StringToDouble(dgv_box_input_data[4, k].Value.ToString(), 0.0));

                        }

                        ch1.get_Range("Q3").Formula = txt_boq_skew_ang.Text;
                        ch1.get_Range("E6").Formula = txt_boq_box_nos_strcs.Text;


                        int indx = 0;
                        for (int k = 3; k < 90; k++)
                        {

                            if (
                                k == 19 ||
                                k >= 14 && k <= 16 ||
                                k >= 29 && k <= 40 ||
                                k >= 50 && k <= 64 ||
                                k >= 67 && k <= 87 ||
                                k >= 24 && k <= 27
                                ) continue;
                            try
                            {
                                //if (k == 3)
                                //{
                                //    ch1.get_Range("Q" + k).Formula = lhs[indx].ToString();
                                //    indx++;
                                //}

                                string v1 = ch1.get_Range("N" + k).Formula.ToString();
                                if (v1.StartsWith("=") == false) ch1.get_Range("N" + k).Formula = lhs[indx].ToString();

                                v1 = ch1.get_Range("O" + k).Formula.ToString();
                                if (v1.StartsWith("=") == false) ch1.get_Range("O" + k).Formula = rhs[indx].ToString();

                                //ch1.get_Range("O" + k).Formula = rhs[indx].ToString();

                                indx++;




                            }
                            catch (Exception exx) { }
                        }
                        #endregion Update INput Data

                        if (true)
                        {
                            #region Update Quantity Data
                            List<int> nos = new List<int>();
                            for (int k = 0; k < dgv_box_input_qty.RowCount; k++)
                            {
                                if (dgv_box_input_qty[3, k].Value != null && dgv_box_input_qty[3, k].Value != "")
                                    nos.Add(MyList.StringToInt(dgv_box_input_qty[3, k].Value.ToString(), 0));
                            }


                            indx = 0;
                            for (int k = 11; k < 242; k++)
                            {

                                if (
                                    k == 18 ||
                                    k == 38 ||
                                    k == 52 ||
                                    k == 73 ||
                                    k == 84 ||
                                    k == 111 ||
                                    k == 119 ||
                                    k == 134 ||
                                    k == 201 ||
                                    k >= 26 && k <= 30 ||
                                    k >= 46 && k <= 49 ||
                                    k >= 55 && k <= 59 ||
                                    k >= 65 && k <= 68 ||
                                    k >= 65 && k <= 68 ||
                                    k >= 78 && k <= 81 ||
                                    k >= 87 && k <= 90 ||
                                    k >= 95 && k <= 98 ||
                                    k >= 101 && k <= 104 ||
                                    k >= 101 && k <= 104 ||
                                    k >= 114 && k <= 117 ||
                                    k >= 114 && k <= 117 ||
                                    k >= 121 && k <= 126 ||
                                    k >= 129 && k <= 132 ||
                                    k >= 136 && k <= 141 ||
                                    k >= 143 && k <= 147 ||
                                    k >= 152 && k <= 155 ||
                                    k >= 158 && k <= 161 ||
                                    k >= 164 && k <= 167 ||
                                    k >= 170 && k <= 173 ||
                                    k >= 176 && k <= 179 ||
                                    k >= 182 && k <= 185 ||
                                    k >= 188 && k <= 191 ||
                                    k >= 194 && k <= 198 ||
                                    k >= 204 && k <= 207 ||
                                    k >= 210 && k <= 214 ||
                                    k >= 217 && k <= 219 ||
                                    k >= 222 && k <= 224 ||
                                    k >= 227 && k <= 229 ||
                                    k >= 232 && k <= 234 ||


                                    k >= 237 && k <= 239
                                    ) continue;
                                try
                                {
                                    if (k == 3)
                                    {
                                        ch1.get_Range("E" + k).Formula = nos[indx].ToString();
                                        indx++;
                                    }

                                    string v1 = ch1.get_Range("E" + k).Formula.ToString();
                                    if (v1.StartsWith("=") == false) ch1.get_Range("E" + k).Formula = nos[indx].ToString();

                                    indx++;


                                }
                                catch (Exception exx) { }
                            }
                            #endregion Update Quantity Data

                        }

                        myExcelWorksheet.get_Range("D" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("E" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("F" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();

                        val1 = dgv_box_gen_data[c, rindx].Value.ToString();

                        myExcelWorksheet.get_Range("G" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("H" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();
                        //myExcelWorksheet.get_Range("I" + (i + 9)).Formula = dgv_gen_data[c++, rindx].Value.ToString();
                        //myExcelWorksheet.get_Range("I" + (i + 9)).Formula = string.Format("=");
                        myExcelWorksheet.get_Range("I" + (i + 9)).Formula = "=" + val1.Replace("x", "*"); c++;
                        //myExcelWorksheet.get_Range("J" + (i + 9)).Formula = dgv_gen_data[c++, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("J" + (i + 9)).Formula = string.Format("=+F{0}*I{0}", (i + 9));
                        myExcelWorksheet.get_Range("K" + (i + 9)).Formula = "RCC Box Type ";
                        myExcelWorksheet.get_Range("L" + (i + 9)).Formula = "";

                        ch1.Visible = Excel.XlSheetVisibility.xlSheetVisible;

                        ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets.get_Item(3);

                        formatRange = (Excel.Range)ch1.Columns[6 + i, Type.Missing];
                        formatRange.EntireColumn.Hidden = false;
                    }
                    else
                    {
                        val1 = "";

                        myExcelWorksheet.get_Range("B" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("C" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("D" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("E" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("F" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("G" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("H" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("I" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("J" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("K" + (i + 9)).Formula = "";
                        myExcelWorksheet.get_Range("L" + (i + 9)).Formula = "";

                        Excel.Worksheet ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets[4 + rindx];

                        ch1.Visible = Excel.XlSheetVisibility.xlSheetHidden;

                        ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets.get_Item(3);

                        formatRange = (Excel.Range)ch1.Columns[6 + i, Type.Missing];
                        formatRange.EntireColumn.Hidden = true;
                    }
                    rindx++;
                }
                catch (Exception exx) { }
            }


            rindx = 0;

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            iApp.Excel_Open_Message();
        }

        private void BOQ_Girder_Type_Bridges()
        {


            string file_path = Get_Project_Folder();

            //file_path = Path.Combine(file_path, "BoQ_Bridges_Girder_Type.xlsx");
            file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Cost Estimation\BOQ\02 BoQ_Bridges_Girder_Type.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["List of MJR"];


            int ch = 27;

            char cr = 'B';

            ch = (int)cr;
            string rng = "";
            string val1 = "";
            string val2 = "";

            DataGridView dgv = dgv_girder_gen_data;

            Excel.Range formatRange;
            formatRange = myExcelWorksheet.get_Range("A" + (dgv.RowCount + 7), "I" + (dgv.RowCount + 7));
            formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);

            int rindx = 0;
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    int c = 0;
                    if (rindx < dgv.RowCount - 1)
                    {
                        myExcelWorksheet.get_Range("A" + (i + 8)).Formula = dgv[c++, rindx].Value.ToString();

                        myExcelWorksheet.get_Range("B" + (i + 8)).Formula = dgv[c++, rindx].Value.ToString();

                        val1 = dgv[c, rindx].Value.ToString();

                        Excel.Worksheet ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets[4 + rindx];

                        ch1.get_Range("H5").Formula = val1;

                        ch1.Name = val1;

                        ch1.get_Range("C7").Formula = txt_boq_girder_nos_strcs.Text;
                        
                        if (false)
                        {
                            #region Update INput Data
                            List<double> lhs = new List<double>();
                            List<double> rhs = new List<double>();
                            for (int k = 0; k < dgv.RowCount; k++)
                            {
                                if (dgv[2, k].Value != null && dgv[2, k].Value != "")
                                    lhs.Add(MyList.StringToDouble(dgv[2, k].Value.ToString(), 0.0));


                                if (dgv[3, k].Value != null && dgv[2, k].Value != "")
                                    rhs.Add(MyList.StringToDouble(dgv[3, k].Value.ToString(), 0.0));

                            }

                            ch1.get_Range("Q3").Formula = txt_boq_skew_ang.Text;
                            ch1.get_Range("E6").Formula = txt_boq_box_nos_strcs.Text;


                            int indx = 0;
                            for (int k = 6; k <= 147; k++)
                            {

                                if (
                                    k == 19 ||
                                    k >= 14 && k <= 16 ||
                                    k >= 29 && k <= 40 ||
                                    k >= 50 && k <= 64 ||
                                    k >= 67 && k <= 87 ||
                                    k >= 24 && k <= 27
                                    ) continue;


                                try
                                {

                                    string v1 = ch1.get_Range("N" + k).Formula.ToString();
                                    if (v1.StartsWith("=") == false) ch1.get_Range("N" + k).Formula = lhs[indx].ToString();

                                    v1 = ch1.get_Range("O" + k).Formula.ToString();
                                    if (v1.StartsWith("=") == false) ch1.get_Range("O" + k).Formula = rhs[indx].ToString();

                                    //ch1.get_Range("O" + k).Formula = rhs[indx].ToString();

                                    indx++;




                                }
                                catch (Exception exx) { }
                            }
                            #endregion Update INput Data
                        }


                        myExcelWorksheet.get_Range("C" + (i + 8)).Formula = dgv[c++, rindx].Value.ToString();

                        val1 = dgv[c, rindx].Value.ToString();

                        //myExcelWorksheet.get_Range("D" + (i + 8)).Formula = val1.Replace("x", "*"); c++;
                        myExcelWorksheet.get_Range("D" + (i + 8)).Formula = val1; c++;


                        myExcelWorksheet.get_Range("E" + (i + 8)).Formula = "=" + val1.Replace("x", "*"); c++;
                        //myExcelWorksheet.get_Range("E" + (i + 8)).Formula = dgv[c++, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("F" + (i + 8)).Formula = dgv[c++, rindx].Value.ToString();

                        val1 = dgv[c, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("G" + (i + 8)).Formula = "=" + val1.Replace("x", "*"); c++;
                        //myExcelWorksheet.get_Range("G" + (i + 8)).Formula = dgv[c++, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("H" + (i + 8)).Formula = string.Format("=+E{0}*G{0}", (i + 8));

                        ch1.Visible = Excel.XlSheetVisibility.xlSheetVisible;

                        ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets.get_Item(3);

                        formatRange = (Excel.Range)ch1.Columns[6 + i, Type.Missing];
                        formatRange.EntireColumn.Hidden = false;
                    }
                    else
                    {
                        val1 = "";

                        myExcelWorksheet.get_Range("A" + (i + 8)).Formula = val1;
                        myExcelWorksheet.get_Range("B" + (i + 8)).Formula = val1;
                        myExcelWorksheet.get_Range("C" + (i + 8)).Formula = val1;
                        myExcelWorksheet.get_Range("D" + (i + 8)).Formula = val1;
                        myExcelWorksheet.get_Range("E" + (i + 8)).Formula = val1;
                        myExcelWorksheet.get_Range("F" + (i + 8)).Formula = val1;
                        myExcelWorksheet.get_Range("G" + (i + 8)).Formula = val1;
                        myExcelWorksheet.get_Range("H" + (i + 8)).Formula = val1;
                        myExcelWorksheet.get_Range("I" + (i + 8)).Formula = val1;

                        Excel.Worksheet ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets[4 + rindx];

                        ch1.Visible = Excel.XlSheetVisibility.xlSheetHidden;

                        ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets.get_Item(3);

                        formatRange = (Excel.Range)ch1.Columns[6 + i, Type.Missing];
                        formatRange.EntireColumn.Hidden = true;
                    }
                    rindx++;
                }
                catch (Exception exx) { }
            }


            rindx = 0;

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            iApp.Excel_Open_Message();
        }

        private void BOQ_Girder_Type_Bridges_2016_05_05()
        {


            string file_path = Get_Project_Folder();

            file_path = Path.Combine(file_path, "BoQ_Bridges_Girder_Type.xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Cost Estimation\BOQ\02 BoQ_Bridges_Girder_Type.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["List of MJR"];


            int ch = 27;

            char cr = 'B';

            ch = (int)cr;
            string rng = "";
            string val1 = "";
            string val2 = "";

            DataGridView dgv = dgv_girder_gen_data;

            Excel.Range formatRange;
            formatRange = myExcelWorksheet.get_Range("b" + (dgv.RowCount + 8), "I" + (dgv.RowCount + 8));
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.LightGreen);

            int rindx = 0;
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    int c = 0;
                    if (rindx < dgv.RowCount - 1)
                    {
                        myExcelWorksheet.get_Range("B" + (i + 9)).Formula = dgv[c++, rindx].Value.ToString();

                        myExcelWorksheet.get_Range("C" + (i + 9)).Formula = dgv[c++, rindx].Value.ToString();

                        val1 = dgv[c, rindx].Value.ToString();

                        Excel.Worksheet ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets[4 + rindx];

                        ch1.get_Range("J5").Formula = val1;

                        ch1.Name = val1;

                        #region Update INput Data
                        List<double> lhs = new List<double>();
                        List<double> rhs = new List<double>();
                        for (int k = 0; k < dgv.RowCount; k++)
                        {
                            if (dgv[2, k].Value != null && dgv[2, k].Value != "")
                                lhs.Add(MyList.StringToDouble(dgv[2, k].Value.ToString(), 0.0));


                            if (dgv[3, k].Value != null && dgv[2, k].Value != "")
                                rhs.Add(MyList.StringToDouble(dgv[3, k].Value.ToString(), 0.0));

                        }

                        ch1.get_Range("Q3").Formula = txt_boq_skew_ang.Text;
                        ch1.get_Range("E6").Formula = txt_boq_box_nos_strcs.Text;


                        int indx = 0;
                        for (int k = 6; k <= 147; k++)
                        {

                            if (
                                k == 19 ||
                                k >= 14 && k <= 16 ||
                                k >= 29 && k <= 40 ||
                                k >= 50 && k <= 64 ||
                                k >= 67 && k <= 87 ||
                                k >= 24 && k <= 27
                                ) continue;


                            try
                            {

                                string v1 = ch1.get_Range("N" + k).Formula.ToString();
                                if (v1.StartsWith("=") == false) ch1.get_Range("N" + k).Formula = lhs[indx].ToString();

                                v1 = ch1.get_Range("O" + k).Formula.ToString();
                                if (v1.StartsWith("=") == false) ch1.get_Range("O" + k).Formula = rhs[indx].ToString();

                                //ch1.get_Range("O" + k).Formula = rhs[indx].ToString();

                                indx++;




                            }
                            catch (Exception exx) { }
                        }
                        #endregion Update INput Data

                        if (true)
                        {
                            #region Update Quantity Data
                            List<int> nos = new List<int>();
                            for (int k = 0; k < dgv_box_input_qty.RowCount; k++)
                            {
                                if (dgv_box_input_qty[3, k].Value != null && dgv_box_input_qty[3, k].Value != "")
                                    nos.Add(MyList.StringToInt(dgv_box_input_qty[3, k].Value.ToString(), 0));
                            }


                            indx = 0;
                            for (int k = 11; k < 242; k++)
                            {

                                if (
                                    k == 18 ||
                                    k == 38 ||
                                    k == 52 ||
                                    k == 73 ||
                                    k == 84 ||
                                    k == 111 ||
                                    k == 119 ||
                                    k == 134 ||
                                    k == 201 ||
                                    k >= 26 && k <= 30 ||
                                    k >= 46 && k <= 49 ||
                                    k >= 55 && k <= 59 ||
                                    k >= 65 && k <= 68 ||
                                    k >= 65 && k <= 68 ||
                                    k >= 78 && k <= 81 ||
                                    k >= 87 && k <= 90 ||
                                    k >= 95 && k <= 98 ||
                                    k >= 101 && k <= 104 ||
                                    k >= 101 && k <= 104 ||
                                    k >= 114 && k <= 117 ||
                                    k >= 114 && k <= 117 ||
                                    k >= 121 && k <= 126 ||
                                    k >= 129 && k <= 132 ||
                                    k >= 136 && k <= 141 ||
                                    k >= 143 && k <= 147 ||
                                    k >= 152 && k <= 155 ||
                                    k >= 158 && k <= 161 ||
                                    k >= 164 && k <= 167 ||
                                    k >= 170 && k <= 173 ||
                                    k >= 176 && k <= 179 ||
                                    k >= 182 && k <= 185 ||
                                    k >= 188 && k <= 191 ||
                                    k >= 194 && k <= 198 ||
                                    k >= 204 && k <= 207 ||
                                    k >= 210 && k <= 214 ||
                                    k >= 217 && k <= 219 ||
                                    k >= 222 && k <= 224 ||
                                    k >= 227 && k <= 229 ||
                                    k >= 232 && k <= 234 ||


                                    k >= 237 && k <= 239
                                    ) continue;
                                try
                                {
                                    if (k == 3)
                                    {
                                        ch1.get_Range("E" + k).Formula = nos[indx].ToString();
                                        indx++;
                                    }

                                    string v1 = ch1.get_Range("E" + k).Formula.ToString();
                                    if (v1.StartsWith("=") == false) ch1.get_Range("E" + k).Formula = nos[indx].ToString();

                                    indx++;


                                }
                                catch (Exception exx) { }
                            }
                            #endregion Update Quantity Data

                        }

                        myExcelWorksheet.get_Range("D" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("E" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("F" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();

                        val1 = dgv_box_gen_data[c, rindx].Value.ToString();

                        myExcelWorksheet.get_Range("G" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("H" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();
                        //myExcelWorksheet.get_Range("I" + (i + 9)).Formula = dgv_gen_data[c++, rindx].Value.ToString();
                        //myExcelWorksheet.get_Range("I" + (i + 9)).Formula = string.Format("=");
                        myExcelWorksheet.get_Range("I" + (i + 9)).Formula = "=" + val1.Replace("x", "*"); c++;
                        //myExcelWorksheet.get_Range("J" + (i + 9)).Formula = dgv_gen_data[c++, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("J" + (i + 9)).Formula = string.Format("=+F{0}*I{0}", (i + 9));
                        myExcelWorksheet.get_Range("K" + (i + 9)).Formula = "RCC Box Type ";
                        myExcelWorksheet.get_Range("L" + (i + 9)).Formula = "";

                        ch1.Visible = Excel.XlSheetVisibility.xlSheetVisible;

                        ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets.get_Item(3);

                        formatRange = (Excel.Range)ch1.Columns[6 + i, Type.Missing];
                        formatRange.EntireColumn.Hidden = false;
                    }
                    else
                    {
                        val1 = "";

                        myExcelWorksheet.get_Range("B" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("C" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("D" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("E" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("F" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("G" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("H" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("I" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("J" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("K" + (i + 9)).Formula = "";
                        myExcelWorksheet.get_Range("L" + (i + 9)).Formula = "";

                        Excel.Worksheet ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets[4 + rindx];

                        ch1.Visible = Excel.XlSheetVisibility.xlSheetHidden;

                        ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets.get_Item(3);

                        formatRange = (Excel.Range)ch1.Columns[6 + i, Type.Missing];
                        formatRange.EntireColumn.Hidden = true;
                    }
                    rindx++;
                }
                catch (Exception exx) { }
            }


            rindx = 0;

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            iApp.Excel_Open_Message();
        }

        private void BOQ_ROB_RUB_Type_Bridges()
        {


            string file_path = Get_Project_Folder();

            //file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");
            file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Cost Estimation\BOQ\04 BoQ_Flyover_ROB_RUBs.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["List of ROB_FO"];

            DataGridView dgv = dgv_rob_gen_data;

            int after_indx = 8;

            int ch = 27;

            char cr = 'B';

            ch = (int)cr;
            string rng = "";
            string val1 = "";
            string val2 = "";

            Excel.Range formatRange;
            formatRange = myExcelWorksheet.get_Range("b" + (dgv.RowCount + after_indx), "L" + (dgv.RowCount + after_indx));
            formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);

            int rindx = 0;


            for (int i = 0; i < 10; i++)
            {
                try
                {
                    int c = 0;
                    if (rindx < dgv.RowCount - 1)
                    {
                        myExcelWorksheet.get_Range("B" + (i + after_indx)).Formula = dgv[c++, rindx].Value.ToString();

                        myExcelWorksheet.get_Range("C" + (i + after_indx)).Formula = dgv[c++, rindx].Value.ToString();

                        val1 = dgv[c, rindx].Value.ToString();

                        Excel.Worksheet ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets[4 + rindx];

                        ch1.get_Range("H6").Formula = val1;

                        ch1.Name = val1;

                        ch1.get_Range("C7").Formula = txt_boq_rob_nos_strcs.Text;

                        if (false)
                        {
                            #region Update INput Data
                            List<double> lhs = new List<double>();
                            List<double> rhs = new List<double>();
                            for (int k = 0; k < dgv_box_input_data.RowCount; k++)
                            {
                                if (dgv_box_input_data[2, k].Value != null && dgv_box_input_data[2, k].Value != "")
                                    lhs.Add(MyList.StringToDouble(dgv_box_input_data[2, k].Value.ToString(), 0.0));


                                if (dgv_box_input_data[3, k].Value != null && dgv_box_input_data[2, k].Value != "")
                                    rhs.Add(MyList.StringToDouble(dgv_box_input_data[3, k].Value.ToString(), 0.0));

                            }

                            ch1.get_Range("Q3").Formula = txt_boq_skew_ang.Text;
                            ch1.get_Range("E6").Formula = txt_boq_box_nos_strcs.Text;


                            int indx = 0;
                            for (int k = 3; k < 90; k++)
                            {

                                if (
                                    k == 19 ||
                                    k >= 14 && k <= 16 ||
                                    k >= 29 && k <= 40 ||
                                    k >= 50 && k <= 64 ||
                                    k >= 67 && k <= 87 ||
                                    k >= 24 && k <= 27
                                    ) continue;
                                try
                                {
                                    //if (k == 3)
                                    //{
                                    //    ch1.get_Range("Q" + k).Formula = lhs[indx].ToString();
                                    //    indx++;
                                    //}

                                    string v1 = ch1.get_Range("N" + k).Formula.ToString();
                                    if (v1.StartsWith("=") == false) ch1.get_Range("N" + k).Formula = lhs[indx].ToString();

                                    v1 = ch1.get_Range("O" + k).Formula.ToString();
                                    if (v1.StartsWith("=") == false) ch1.get_Range("O" + k).Formula = rhs[indx].ToString();

                                    //ch1.get_Range("O" + k).Formula = rhs[indx].ToString();

                                    indx++;




                                }
                                catch (Exception exx) { }
                            }
                            #endregion Update INput Data
                        }

                        myExcelWorksheet.get_Range("D" + (i + after_indx)).Formula = dgv[c++, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("E" + (i + after_indx)).Formula = dgv[c++, rindx].Value.ToString();
                        val1 = dgv[c, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("F" + (i + after_indx)).Formula = dgv[c++, rindx].Value.ToString();


                        //myExcelWorksheet.get_Range("G" + (i + 9)).Formula = dgv[c++, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("G" + (i + after_indx)).Formula = "=" + val1.Replace("x", "*"); c++;
                        val1 = dgv[c, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("H" + (i + after_indx)).Formula = dgv[c++, rindx].Value.ToString();



                        myExcelWorksheet.get_Range("I" + (i + after_indx)).Formula = "=" + val1.Replace("x", "*"); c++;
                        myExcelWorksheet.get_Range("J" + (i + after_indx)).Formula = string.Format("=+I{0}*G{0}", (i + after_indx));
                        myExcelWorksheet.get_Range("K" + (i + after_indx)).Formula = "";
                        myExcelWorksheet.get_Range("L" + (i + after_indx)).Formula = "";

                        ch1.Visible = Excel.XlSheetVisibility.xlSheetVisible;

                        ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets.get_Item(3);

                        formatRange = (Excel.Range)ch1.Columns[6 + i, Type.Missing];
                        formatRange.EntireColumn.Hidden = false;
                    }
                    else
                    {
                        val1 = "";

                        myExcelWorksheet.get_Range("B" + (i + after_indx)).Formula = val1;
                        myExcelWorksheet.get_Range("C" + (i + after_indx)).Formula = val1;
                        myExcelWorksheet.get_Range("D" + (i + after_indx)).Formula = val1;
                        myExcelWorksheet.get_Range("E" + (i + after_indx)).Formula = val1;
                        myExcelWorksheet.get_Range("F" + (i + after_indx)).Formula = val1;
                        myExcelWorksheet.get_Range("G" + (i + after_indx)).Formula = val1;
                        myExcelWorksheet.get_Range("H" + (i + after_indx)).Formula = val1;
                        myExcelWorksheet.get_Range("I" + (i + after_indx)).Formula = val1;
                        myExcelWorksheet.get_Range("J" + (i + after_indx)).Formula = val1;
                        myExcelWorksheet.get_Range("K" + (i + after_indx)).Formula = "";
                        myExcelWorksheet.get_Range("L" + (i + after_indx)).Formula = "";

                        Excel.Worksheet ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets[4 + rindx];

                        ch1.Visible = Excel.XlSheetVisibility.xlSheetHidden;

                        ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets.get_Item(3);

                        formatRange = (Excel.Range)ch1.Columns[6 + i, Type.Missing];
                        formatRange.EntireColumn.Hidden = true;
                    }
                    rindx++;
                }
                catch (Exception exx) { }
            }


            rindx = 0;

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            iApp.Excel_Open_Message();
        }

        private void BOQ_ROB_RUB_Type_Bridges_2016_05_05()
        {


            string file_path = Get_Project_Folder();

            file_path = Path.Combine(file_path, "BoQ_Flyover_ROB_RUBs.xlsx");

            string copy_path = file_path;

            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Cost Estimation\BOQ\04 BoQ_Flyover_ROB_RUBs.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
            }

            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;

            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;

            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["List of ROB_FO"];


            int ch = 27;

            char cr = 'B';

            ch = (int)cr;
            string rng = "";
            string val1 = "";
            string val2 = "";

            Excel.Range formatRange;
            formatRange = myExcelWorksheet.get_Range("b" + (dgv_box_gen_data.RowCount + 8), "L" + (dgv_box_gen_data.RowCount + 8));
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.LightGreen);

            int rindx = 0;
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    int c = 0;
                    if (rindx < dgv_box_gen_data.RowCount - 1)
                    {
                        myExcelWorksheet.get_Range("B" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();

                        myExcelWorksheet.get_Range("C" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();

                        val1 = dgv_box_gen_data[c, rindx].Value.ToString();

                        Excel.Worksheet ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets[4 + rindx];

                        ch1.get_Range("J5").Formula = val1;

                        ch1.Name = val1;

                        #region Update INput Data
                        List<double> lhs = new List<double>();
                        List<double> rhs = new List<double>();
                        for (int k = 0; k < dgv_box_input_data.RowCount; k++)
                        {
                            if (dgv_box_input_data[2, k].Value != null && dgv_box_input_data[2, k].Value != "")
                                lhs.Add(MyList.StringToDouble(dgv_box_input_data[2, k].Value.ToString(), 0.0));


                            if (dgv_box_input_data[3, k].Value != null && dgv_box_input_data[2, k].Value != "")
                                rhs.Add(MyList.StringToDouble(dgv_box_input_data[3, k].Value.ToString(), 0.0));

                        }

                        ch1.get_Range("Q3").Formula = txt_boq_skew_ang.Text;
                        ch1.get_Range("E6").Formula = txt_boq_box_nos_strcs.Text;


                        int indx = 0;
                        for (int k = 3; k < 90; k++)
                        {

                            if (
                                k == 19 ||
                                k >= 14 && k <= 16 ||
                                k >= 29 && k <= 40 ||
                                k >= 50 && k <= 64 ||
                                k >= 67 && k <= 87 ||
                                k >= 24 && k <= 27
                                ) continue;
                            try
                            {
                                //if (k == 3)
                                //{
                                //    ch1.get_Range("Q" + k).Formula = lhs[indx].ToString();
                                //    indx++;
                                //}

                                string v1 = ch1.get_Range("N" + k).Formula.ToString();
                                if (v1.StartsWith("=") == false) ch1.get_Range("N" + k).Formula = lhs[indx].ToString();

                                v1 = ch1.get_Range("O" + k).Formula.ToString();
                                if (v1.StartsWith("=") == false) ch1.get_Range("O" + k).Formula = rhs[indx].ToString();

                                //ch1.get_Range("O" + k).Formula = rhs[indx].ToString();

                                indx++;




                            }
                            catch (Exception exx) { }
                        }
                        #endregion Update INput Data

                        if (true)
                        {
                            #region Update Quantity Data
                            List<int> nos = new List<int>();
                            for (int k = 0; k < dgv_box_input_qty.RowCount; k++)
                            {
                                if (dgv_box_input_qty[3, k].Value != null && dgv_box_input_qty[3, k].Value != "")
                                    nos.Add(MyList.StringToInt(dgv_box_input_qty[3, k].Value.ToString(), 0));
                            }


                            indx = 0;
                            for (int k = 11; k < 242; k++)
                            {

                                if (
                                    k == 18 ||
                                    k == 38 ||
                                    k == 52 ||
                                    k == 73 ||
                                    k == 84 ||
                                    k == 111 ||
                                    k == 119 ||
                                    k == 134 ||
                                    k == 201 ||
                                    k >= 26 && k <= 30 ||
                                    k >= 46 && k <= 49 ||
                                    k >= 55 && k <= 59 ||
                                    k >= 65 && k <= 68 ||
                                    k >= 65 && k <= 68 ||
                                    k >= 78 && k <= 81 ||
                                    k >= 87 && k <= 90 ||
                                    k >= 95 && k <= 98 ||
                                    k >= 101 && k <= 104 ||
                                    k >= 101 && k <= 104 ||
                                    k >= 114 && k <= 117 ||
                                    k >= 114 && k <= 117 ||
                                    k >= 121 && k <= 126 ||
                                    k >= 129 && k <= 132 ||
                                    k >= 136 && k <= 141 ||
                                    k >= 143 && k <= 147 ||
                                    k >= 152 && k <= 155 ||
                                    k >= 158 && k <= 161 ||
                                    k >= 164 && k <= 167 ||
                                    k >= 170 && k <= 173 ||
                                    k >= 176 && k <= 179 ||
                                    k >= 182 && k <= 185 ||
                                    k >= 188 && k <= 191 ||
                                    k >= 194 && k <= 198 ||
                                    k >= 204 && k <= 207 ||
                                    k >= 210 && k <= 214 ||
                                    k >= 217 && k <= 219 ||
                                    k >= 222 && k <= 224 ||
                                    k >= 227 && k <= 229 ||
                                    k >= 232 && k <= 234 ||


                                    k >= 237 && k <= 239
                                    ) continue;
                                try
                                {
                                    if (k == 3)
                                    {
                                        ch1.get_Range("E" + k).Formula = nos[indx].ToString();
                                        indx++;
                                    }

                                    string v1 = ch1.get_Range("E" + k).Formula.ToString();
                                    if (v1.StartsWith("=") == false) ch1.get_Range("E" + k).Formula = nos[indx].ToString();

                                    indx++;


                                }
                                catch (Exception exx) { }
                            }
                            #endregion Update Quantity Data

                        }

                        myExcelWorksheet.get_Range("D" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("E" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("F" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();

                        val1 = dgv_box_gen_data[c, rindx].Value.ToString();

                        myExcelWorksheet.get_Range("G" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("H" + (i + 9)).Formula = dgv_box_gen_data[c++, rindx].Value.ToString();
                        //myExcelWorksheet.get_Range("I" + (i + 9)).Formula = dgv_gen_data[c++, rindx].Value.ToString();
                        //myExcelWorksheet.get_Range("I" + (i + 9)).Formula = string.Format("=");
                        myExcelWorksheet.get_Range("I" + (i + 9)).Formula = "=" + val1.Replace("x", "*"); c++;
                        //myExcelWorksheet.get_Range("J" + (i + 9)).Formula = dgv_gen_data[c++, rindx].Value.ToString();
                        myExcelWorksheet.get_Range("J" + (i + 9)).Formula = string.Format("=+F{0}*I{0}", (i + 9));
                        myExcelWorksheet.get_Range("K" + (i + 9)).Formula = "RCC Box Type ";
                        myExcelWorksheet.get_Range("L" + (i + 9)).Formula = "";

                        ch1.Visible = Excel.XlSheetVisibility.xlSheetVisible;

                        ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets.get_Item(3);

                        formatRange = (Excel.Range)ch1.Columns[6 + i, Type.Missing];
                        formatRange.EntireColumn.Hidden = false;
                    }
                    else
                    {
                        val1 = "";

                        myExcelWorksheet.get_Range("B" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("C" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("D" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("E" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("F" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("G" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("H" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("I" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("J" + (i + 9)).Formula = val1;
                        myExcelWorksheet.get_Range("K" + (i + 9)).Formula = "";
                        myExcelWorksheet.get_Range("L" + (i + 9)).Formula = "";

                        Excel.Worksheet ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets[4 + rindx];

                        ch1.Visible = Excel.XlSheetVisibility.xlSheetHidden;

                        ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets.get_Item(3);

                        formatRange = (Excel.Range)ch1.Columns[6 + i, Type.Missing];
                        formatRange.EntireColumn.Hidden = true;
                    }
                    rindx++;
                }
                catch (Exception exx) { }
            }


            rindx = 0;

            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);

            iApp.Excel_Open_Message();
        }


        public void Open_Worksheet_BOQ()
        {
            string file_path = Path.Combine(iApp.LastDesignWorkingFolder, "Cost Estimation");

            if (!Directory.Exists(file_path)) Directory.CreateDirectory(file_path);


            file_path = Path.Combine(file_path, "General_BOQ_1.xlsx");

            string copy_path = file_path;



            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Cost Estimation\BOQ\01 BoQ_Bridges_Box_Type.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
                //iapp.OpenExcelFile(copy_path, "2011ap");
            }
        }

        public void Create_Excel_Sheet(Excel.Sheets src_sheet, Excel.Sheets dest_sheet)
        {

        }

         
        private void btn_gd_insert_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgv_box_gen_data;
            Button btn = sender as Button;
            int indx = 0;
            if( dgv.SelectedCells.Count > 0)
                indx = dgv.SelectedCells[0].RowIndex;


            if (btn_gd_insert.Name == btn.Name)
            {
                dgv.Rows.Insert(indx, "", "", "", "", "", "", "", "", "", "");
            }
            else if (btn_gd_delete.Name == btn.Name)
            {
                dgv.Rows.RemoveAt(indx);
            }
            Box_Modified_Cell();

        }

        private void dgv_gen_data_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            if (cmb_boq_item.SelectedIndex == 0)
                BOX_General_Data_Format();
            else if (cmb_boq_item.SelectedIndex == 1)
                Girder_General_Data_Format();
            else if (cmb_boq_item.SelectedIndex == 2)
                ROB_General_Data_Format();
        }

        private void BOX_General_Data_Format()
        {
            DataGridView dgv = dgv_box_gen_data;


            if (cmb_boq_item.SelectedIndex == 1) dgv = dgv_girder_gen_data;
            if (cmb_boq_item.SelectedIndex == 2) dgv = dgv_rob_gen_data;

            DataTable dt = new DataTable();

            double len = 0.0;
            double wd = 0.0;
            string stuc = "";
            double val = 0.0;
            MyList mlist = null;
            for (int i = 0; i < dgv.RowCount - 1; i++)
            {
                try
                {
                    dgv[0, i].Value = (i + 1).ToString();

                    if (dgv[3, i].Value == null)
                        dgv[3, i].Value = "";

                    stuc = dgv[3, i].Value.ToString();
                    stuc = stuc.ToLower().Replace("*", "x").Trim();
                    stuc = stuc.ToLower().Trim();


                    mlist = new MyList(stuc.ToLower(), 'x');

                    dgv[3, i].Value = stuc;



                    len = MyList.Get_Expression_Result(stuc);
                    

                    //var v1 = dt.Compute("3 * (2+4)", "");
                    //if (mlist.Count > 0)
                    //{
                    //    len = mlist.GetDouble(0);
                    //    if (mlist.Count > 1)
                    //    {
                    //        len *= mlist.GetDouble(1);
                    //    }
                    //}

                    dgv[4, i].Value = len.ToString("f3");
                    if (dgv[5, i].Value == null) dgv[5, i].Value = "";



                    stuc = dgv[5, i].Value.ToString();
                    mlist = new MyList(stuc.ToLower(), 'x');

                    stuc = dgv[5, i].Value.ToString();
                    stuc = stuc.ToLower().Replace("*", "x").Trim();
                    stuc = stuc.ToLower().Trim();


                    mlist = new MyList(stuc.ToLower(), 'x');

                    dgv[5, i].Value = stuc;

                    wd = MyList.Get_Expression_Result(stuc);
                     

                    //if (mlist.Count > 0)
                    //{
                    //    wd = mlist.GetDouble(0);
                    //    if (mlist.Count > 1)
                    //    {
                    //        wd *= mlist.GetDouble(1);
                    //    }
                    //    dgv[7, i].Value = wd.ToString("f3");
                    //}
                    dgv[7, i].Value = wd.ToString("f3");

                    dgv[8, i].Value = (len * wd).ToString("f3");

                }
                catch (Exception exx) { }

            }
            txt_tot_nos.Text = dgv.RowCount - 1 + "";
        }

        private void Girder_General_Data_Format()
        {
            DataGridView dgv = dgv_girder_gen_data;

            //if (cmb_boq_item.SelectedIndex == 1) dgv = dgv_girder_gen_data;
            //if (cmb_boq_item.SelectedIndex == 2) dgv = dgv_rob_gen_data;

            DataTable dt = new DataTable();

            double len = 0.0;
            double wd = 0.0;
            string stuc = "";
            double val = 0.0;
            MyList mlist = null;
            for (int i = 0; i < dgv.RowCount - 1; i++)
            {
                try
                {
                    dgv[0, i].Value = (i + 1).ToString();

                    if (dgv[3, i].Value == null)
                        dgv[3, i].Value = "";

                    stuc = dgv[3, i].Value.ToString();
                    stuc = stuc.ToLower().Replace("*", "x").Trim();
                    stuc = stuc.ToLower().Trim();


                    mlist = new MyList(stuc.ToLower(), 'x');

                    dgv[3, i].Value = stuc;

                   
                        len = MyList.Get_Expression_Result(stuc);

            
 
                    //var v1 = dt.Compute("3 * (2+4)", "");
                    //if (mlist.Count > 0)
                    //{
                    //    len = mlist.GetDouble(0);
                    //    if (mlist.Count > 1)
                    //    {
                    //        len *= mlist.GetDouble(1);
                    //    }
                    //}

                    dgv[4, i].Value = len.ToString("f3");
                    if (dgv[5, i].Value == null) dgv[5, i].Value = "";



                    stuc = dgv[5, i].Value.ToString();
                    stuc = stuc.ToLower().Replace("*", "x").Trim();
                    stuc = stuc.ToLower().Trim();

                    dgv[5, i].Value = stuc;

                    wd = MyList.Get_Expression_Result(stuc);

                    

                    //if (mlist.Count > 0)
                    //{
                    //    wd = mlist.GetDouble(0);
                    //    if (mlist.Count > 1)
                    //    {
                    //        wd *= mlist.GetDouble(1);
                    //    }
                    //    dgv[7, i].Value = wd.ToString("f3");
                    //}
                    dgv[6, i].Value = wd.ToString("f3");

                    dgv[7, i].Value = (len * wd).ToString("f3");

                }
                catch (Exception exx) { }

            }
            txt_tot_nos.Text = dgv.RowCount - 1 + "";
        }
        private void ROB_General_Data_Format()
        {
            DataGridView dgv = dgv_rob_gen_data;


            //if (cmb_boq_item.SelectedIndex == 1) dgv = dgv_girder_gen_data;
            //if (cmb_boq_item.SelectedIndex == 2) dgv = dgv_rob_gen_data;

            DataTable dt = new DataTable();

            double len = 0.0;
            double wd = 0.0;
            string stuc = "";
            double val = 0.0;
            MyList mlist = null;
            for (int i = 0; i < dgv.RowCount - 1; i++)
            {
                try
                {
                    dgv[0, i].Value = (i + 1).ToString();

                    if (dgv[4, i].Value == null)
                        dgv[4, i].Value = "";

                    stuc = dgv[4, i].Value.ToString();
                    stuc = stuc.ToLower().Replace("*", "x").Trim();
                    stuc = stuc.ToLower().Trim();



                    dgv[4, i].Value = stuc;

                    len = MyList.Get_Expression_Result(stuc);

                    dgv[5, i].Value = len.ToString("f3");


                    if (dgv[5, i].Value == null) dgv[5, i].Value = "";



                    stuc = dgv[6, i].Value.ToString();
                    stuc = stuc.ToLower().Replace("*", "x").Trim();
                    stuc = stuc.ToLower().Trim();

                    dgv[6, i].Value = stuc;

                    wd = MyList.Get_Expression_Result(stuc);
  
                    dgv[7, i].Value = wd.ToString("f3");

                    dgv[8, i].Value = (len * wd).ToString("f3");

                }
                catch (Exception exx) { }

            }
            txt_tot_nos.Text = dgv.RowCount - 1 + "";
        }

        private void btn_boq_file_brw_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;


            string file_name = "";
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    file_name = ofd.FileName;
                }
            }


            if (File.Exists(file_name))
            {
                if (btn.Name == btn_boq_file_brw.Name)
                {
                    txt_boq_file.Text = file_name;
                }
                else if (btn.Name == btn_rate_file_brw.Name)
                {
                    txt_rate_file.Text = file_name;
                }
            }

        }

        #endregion BOQ

        #region Rate Analysis

        void Load_Default_Rate_Analysis_Data()
        {
            DataGridView dgv = dgv_rate_materials;



            List<string> list = new List<string>();
            #region [2016 04 05]
            if (false)
            {
                #region Material Input Data
                list.Add(string.Format("1$Stone Boulder of size 150 mm and below at Cruser Plant$1490.00$"));
                list.Add(string.Format("2$Supply of quarried stone 150 - 200 mm size for Hand Broken at site$1490.00$"));
                list.Add(string.Format("3$Boulder with minimum size of 300 mm  for Pitching at Site$1444.00$"));
                list.Add(string.Format("4$Coarse sand at mixing plant$1000.00$"));
                list.Add(string.Format("5$Coarse sand at site$1000.00$"));
                list.Add(string.Format("6$Fine sand at Site$850.00$"));
                list.Add(string.Format("7$Moorum at Site$1130.00$"));
                list.Add(string.Format("8$Gravel/Quarry spall at Site$1490.00$"));
                list.Add(string.Format("9$Granular Material or hard murrum for GSB works at Site$1130.00$"));
                list.Add(string.Format("10$Granular Material or hard murrum for GSB works at Mixing Plant$1130.00$"));
                list.Add(string.Format("11$Fly ash conforming to IS: 3812 ( Part II  & I) at HMP Plant / Batching Plant / Crushing Plant$60.00$"));
                list.Add(string.Format("12$Filter media/Filter Material as per Table 300-3 (MoRT&H Specification)$1540.00$"));
                list.Add(string.Format("13$Close graded Granular sub-base Material  53 mm to 9.5 mm  $1525.00$1525"));
                list.Add(string.Format("14$Close graded Granular sub-base Material  37.5 mm to 9.5 mm  $1540.00$"));
                list.Add(string.Format("15$Close graded Granular sub-base Material  26.5 mm to 9.5 mm  $1598.00$"));
                list.Add(string.Format("16$Close graded Granular sub-base Material  9.5 mm to 4.75 mm  $1359.50$"));
                list.Add(string.Format("17$Close graded Granular sub-base Material  9.5 mm to 2.36 mm  $1310.00$"));
                list.Add(string.Format("18$Close graded Granular sub-base Material  4.75mm to 2.36 mm  $1179.50$"));
                list.Add(string.Format("19$Close graded Granular sub-base Material  4.75mm to 75 micron mm  $1179.50$"));
                list.Add(string.Format("20$Close graded Granular sub-base Material  2.36 mm $1130.00$"));
                list.Add(string.Format("21$Stone crusher dust finer than 3mm with not more than 10% passing 0.075 sieve.$1130.00$"));
                list.Add(string.Format("22$Coarse graded Granular sub-base Material  2.36 mm & below   $1130.00$"));
                list.Add(string.Format("23$Coarse graded Granular sub-base Material  4.75mm to 75 micron mm  $1130.00$"));
                list.Add(string.Format("24$Coarse graded Granular sub-base Material  4.75 mm to 2.36 mm  $1179.50$"));
                list.Add(string.Format("25$Coarse graded Granular sub-base Material  9.5 mm to 4.75 mm   $1359.50$"));
                list.Add(string.Format("26$Coarse graded Granular sub-base Material  26.5 mm to 4.75 mm  $1467.50$"));
                list.Add(string.Format("27$Coarse graded Granular sub-base Material  26.5 mm to 9.5 mm   $1598.00$"));
                list.Add(string.Format("28$Coarse graded Granular sub-base Material  37.5 mm to 9.5 mm  $1540.00$"));
                list.Add(string.Format("29$Coarse graded Granular sub-base Material  53 mm to 26 .5mm   $1633.00$"));
                list.Add(string.Format("30$Aggregates  below  5.6 mm $1229.00$"));
                list.Add(string.Format("31$Aggregates  22.4 mm to 2.36 mm $1480.00$"));
                list.Add(string.Format("32$Aggregates  22.4 mm to 5.6 mm $1529.50$"));
                list.Add(string.Format("33$Aggregates  45 mm to 2.8 mm $1399.50$"));
                list.Add(string.Format("34$Aggregates  45 mm to 22.4 mm $1700.00$"));
                list.Add(string.Format("35$Aggregates  53 mm to 2.8 mm $1394.50$"));
                list.Add(string.Format("36$Aggregates  53 mm to 22.4 mm $1695.00$"));
                list.Add(string.Format("37$Aggregates  63 mm to 2.8 mm $1379.50$"));
                list.Add(string.Format("38$Aggregates  63 mm to 45 mm $1550.00$"));
                list.Add(string.Format("39$Aggregates  90 mm to 45 mm $1570.00$"));
                list.Add(string.Format("40$Aggregates  10 mm to 5 mm $1359.50$"));
                list.Add(string.Format("41$Aggregates  11.2 mm to 0.09 mm $1310.00$"));
                list.Add(string.Format("42$Aggregates  13.2 mm to 0.09 mm $1497.50$"));
                list.Add(string.Format("43$Aggregates  13.2 mm to 5.6 mm $1547.00$"));
                list.Add(string.Format("44$Aggregates  13.2 mm to 10 mm $1677.50$"));
                list.Add(string.Format("45$Aggregates  20 mm to 10 mm $1660.00$"));
                list.Add(string.Format("46$Aggregates  25 mm to 10 mm $1660.00$"));
                list.Add(string.Format("47$Aggregates  19 mm to 6 mm $1529.50$"));
                list.Add(string.Format("48$Aggregates  37.5 mm to 19 mm $1710.00$"));
                list.Add(string.Format("49$Aggregates  37.5 mm to 25 mm $1648.00$"));
                list.Add(string.Format("50$Aggregates  6 mm nominal size$1229.00$"));
                list.Add(string.Format("51$Aggregates  10 mm nominal size$1490.00$"));
                list.Add(string.Format("52$Aggregates  13.2/12.5 mm nominal size$1865.00$"));
                list.Add(string.Format("53$Aggregates  20 mm nominal size$1830.00$"));
                list.Add(string.Format("54$Aggregates  25 mm nominal size$1706.00$"));
                list.Add(string.Format("55$Aggregates  40 mm nominal size$1590.00$"));
                list.Add(string.Format("56$AC pipe 100 mm dia$38.59$"));
                list.Add(string.Format("57$Acrylic polymer bonding coat$385.88$"));
                list.Add(string.Format("58$Alluminium Paint$192.94$"));
                list.Add(string.Format("59$Aluminium alloy plate 2mm Thick$4630.50$"));
                list.Add(string.Format("60$Aluminium alloy/galvanised steel $46305.00$"));
                list.Add(string.Format("61$Aluminium sheeting fixed with encapsulated lens type reflective sheeting  including 2% towards lettering, cost of angle iron, cost of drilling holes, nuts, bolts etc.and signs as applicable$6063.75$"));
                list.Add(string.Format("62$Aluminium studs 100 x 100 mm fitted with lense reflectors$374.85$"));
                list.Add(string.Format("63$Barbed wire$66.15$"));
                list.Add(string.Format("64$Bearing (Cost of parts)$8820.00$"));
                list.Add(string.Format("65$Bearing (Cast steel rocker bearing assembly of 250 tonne )$33185.25$"));
                list.Add(string.Format("66$Bearing (Elastomeric bearing assembly consisting of 7 internal layers of elastomer bonded to 6 nos. internal reinforcing steel laminates by the process of vulcanisation,)$12678.75$"));
                list.Add(string.Format("67$Bearing (Forged steel roller bearing of 250 tonne$104847.75$"));
                list.Add(string.Format("68$Bearing (Pot type bearing assembly consisting of a metal piston supported by a disc, PTFE pads providing sliding surfaces against stainless steel mating together with cast steel assemblies/fabricated structural steel assemblies  duly painted with all comp$95917.50$"));
                list.Add(string.Format("69$Bearing (PTFE sliding plate bearing assembly of 80 tonnes )$38697.75$"));
                list.Add(string.Format("70$Bearing (Supply of sliding plate bearing of 80 tonne)$33185.25$"));
                list.Add(string.Format("71$Bentonite $5.51$"));
                list.Add(string.Format("72$Binding wire$66.15$"));
                list.Add(string.Format("73$Bitumen ( Cationic Emulsion ) bulk MS$20883.00$"));
                list.Add(string.Format("74$Bitumen (60-70 grade) $21613.00$"));
                list.Add(string.Format("75$Bitumen (80-100 grade )$20813.00$"));
                list.Add(string.Format("76$Bitumen (Cutback )$26993.00$"));
                list.Add(string.Format("77$Bitumen (emulsion) bulk MS $20883.00$"));
                list.Add(string.Format("78$Bitumen (modified graded) $26993.00$"));
                list.Add(string.Format("79$Brick$4.96$"));
                list.Add(string.Format("80$C.I.shoes for the pile$59.54$"));
                list.Add(string.Format("81$Cement $7560.00$"));
                list.Add(string.Format("82$Cold twisted bars (HYSD Bars)$51700.00$"));
                list.Add(string.Format("83$Coller for joints 300 mm dia$275.63$"));
                list.Add(string.Format("84$Compressible Fibre Board(20mm thick)$496.13$"));
                list.Add(string.Format("85$Connectors/ Staples$55.13$"));
                list.Add(string.Format("86$Copper Plate(12m long x 250mmwide)$661.50$"));
                list.Add(string.Format("87$Corrosion resistant Structural steel$54022.50$"));
                list.Add(string.Format("88$Corrugated sheet, 3 mm thick, Thrie beam section railing$71.66$"));
                list.Add(string.Format("89$Credit for excavated rock found suitable for use$110.25$"));
                list.Add(string.Format("90$Curing compound $99.23$"));
                list.Add(string.Format("91$Delineators from ISI certified firm as per the standard drawing given in IRC - 79 $468.56$"));
                list.Add(string.Format("92$Earth Cost  or  compensation for earth taken from private land$33.08$"));
                list.Add(string.Format("93$Elastomeric slab seal expansion joint assembly manufactured by using chloroprene, elastomer for elastomeric slab unit conforming to clause 915.1 of IRC: 83 (part II),$11025.00$"));
                list.Add(string.Format("94$Electric Detonators @ 1 detonator for 1/2 gelatin stick of 125 gms each$551.25$"));
                list.Add(string.Format("95$Epoxy compound with accessories for preparing epoxy mortar$1102.50$"));
                list.Add(string.Format("96$Epoxy mortar$330.75$"));
                list.Add(string.Format("97$Epoxy primer $551.25$"));
                list.Add(string.Format("98$Epoxy resin-hardner mix for prime coat$551.25$"));
                list.Add(string.Format("99$Flag of red color cloth 600 x 600 mm$33.08$"));
                list.Add(string.Format("100$Flowering Plants $44.10$"));
                list.Add(string.Format("101$Galvanised MS flat clamp$275.63$"));
                list.Add(string.Format("102$Galvanised steel wire crates of mesh size 100 mm x 100 mm woven with 4mm dia. GI wire in rolls of required size.$165.38$"));
                list.Add(string.Format("103$Galvanised structural steel plate 200 mm wide, 6 mm thick, 24 m long$60.64$"));
                list.Add(string.Format("104$Gelatin 80%     $66.15$"));
                list.Add(string.Format("105$Geo grids$137.81$"));
                list.Add(string.Format("106$Geomembrane$77.18$"));
                list.Add(string.Format("107$Geonets$82.69$"));
                list.Add(string.Format("108$Geotextile$99.23$"));
                list.Add(string.Format("109$Geotextile filter fabric$0.00$"));
                list.Add(string.Format("110$GI bolt 10 mm Dia$5.51$"));
                list.Add(string.Format("111$Grouting pump with agitator$330.75$"));
                list.Add(string.Format("112$Grass (Doob)$8.82$"));
                list.Add(string.Format("113$Grass (Fine)$36.38$"));
                list.Add(string.Format("114$HDPE pipes 75mm dia$115.76$"));
                list.Add(string.Format("115$HDPE pipes 90mm dia$121.28$"));
                list.Add(string.Format("116$Hedge plants$12.13$"));
                list.Add(string.Format("117$Helical pipes 600mm diameter$110.25$"));
                list.Add(string.Format("118$Hot applied thermoplastic compound$81.59$"));
                list.Add(string.Format("119$HTS strand $107486.00$"));
                list.Add(string.Format("120$Joint Sealant Compound$595.35$"));
                list.Add(string.Format("121$Jute netting, open weave, 2.5 cm square opening for seeding and Mulching$33.08$"));
                list.Add(string.Format("122$LDO for steam curing$49.61$"));
                list.Add(string.Format("123$M.S. Clamps$13.23$"));
                list.Add(string.Format("124$M.S. Clamps$57.33$"));
                list.Add(string.Format("125$M.S.shoes @ 35 Kg per pile of 15 m$54.02$"));
                list.Add(string.Format("126$Mild Steel bars$51700.00$"));
                list.Add(string.Format("127$Modular  strip/box seal expansion joint including anchorage catering to a horizontal movement beyond 70 mm and upto 140mm assembly comprising of edge beams, central beam,2 modules chloroprene seal, anchorage elements, support and control system, all steel$55.13$"));
                list.Add(string.Format("128$Modular  strip/box seal expansion joint catering to a horizontal movement beyond 140mm and upto 210mm box/box seal joint assembly containing 3 modules/cells and comprising of edge beams, two central beams, chloroprene seal, anchorage elements, support and$55.13$"));
                list.Add(string.Format("129$Nipples 12mm$5.51$"));
                list.Add(string.Format("130$Nuts and bolts      $66.15$"));
                list.Add(string.Format("131$Paint$286.65$"));
                list.Add(string.Format("132$Pavement Marking Paint$220.50$"));
                list.Add(string.Format("133$Paving Fabric$0.00$"));
                list.Add(string.Format("134$Perforated geosynthetic pipe 150 mm dia $385.88$"));
                list.Add(string.Format("135$Perforated pipe of cement concrete, internal dia 100 mm$220.50$"));
                list.Add(string.Format("136$Pesticide$330.75$"));
                list.Add(string.Format("137$Pipes 200 mm dia, 2.5 m long for drainage$165.38$"));
                list.Add(string.Format("138$Plastic sheath, 1.25 mm thick for dowel bars$4.41$"));
                list.Add(string.Format("139$Plastic tubes 50 cm dia, 1.2 m high$33.08$"));
                list.Add(string.Format("140$Polymer braids$192.94$"));
                list.Add(string.Format("141$Pre moulded Joint filler,25 mm thick for expansion joint.$518.18$"));
                list.Add(string.Format("142$Pre-coated stone chips of 13.2 mm nominal size$468.56$"));
                list.Add(string.Format("143$Preformed continuous chloroprene elastomer or closed cell foam sealing element with high tear strength, vulcanised in a single operation for the full length of a joint to ensure water tightness.$154.35$"));
                list.Add(string.Format("144$Pre-moulded asphalt filler board$771.75$"));
                list.Add(string.Format("145$Pre-packed cement based polymer concrete of strength 45 Mpa at 28 days$330.75$"));
                list.Add(string.Format("146$Primer$192.94$"));
                list.Add(string.Format("147$Quick setting compound$24.26$"));
                list.Add(string.Format("148$Random Rubble Stone$496.13$"));
                list.Add(string.Format("149$RCC Pipe NP 4 heavy duty non presure pipe 1000 mm dia $5250.00$"));
                list.Add(string.Format("150$RCC Pipe NP 4 heavy duty non presure pipe 1200 mm dia $6510.00$"));
                list.Add(string.Format("151$RCC Pipe NP 4 heavy duty non presure pipe 300 mm dia$787.50$"));
                list.Add(string.Format("152$Reflectorising glass beads$66.15$"));
                list.Add(string.Format("153$Reinforcement strips 60 mm wide 5 mm thick as per clause 3102. (Copper Strips)$121.28$"));
                list.Add(string.Format("154$Reinforcement strips 60 mm wide 5 mm thick as per clause 3102. (Galvanised carbon steel strips)$13.23$"));
                list.Add(string.Format("155$Reinforcement strips 60 mm wide 5 mm thick as per clause 3102. (Glass reinforced polymer/fibre reinforced polymer/polymeric strips)$24.26$"));
                list.Add(string.Format("156$Reinforcement strips 60 mm wide 5 mm thick as per clause 3102. (Stainless steel strips)$14.33$"));
                list.Add(string.Format("157$Reinforcement strips 60 mm wide 5 mm thick as per clause 3102. Aluminium strips)$24.26$"));
                list.Add(string.Format("158$Rivets$0.33$"));
                list.Add(string.Format("159$Sand bags (Cost of sand and Empty cement bag)$8.82$"));
                list.Add(string.Format("160$Sapling 2 m high 25 mm dia $16.54$"));
                list.Add(string.Format("161$Scrap tyres of size 900 x 20$88.20$"));
                list.Add(string.Format("162$Seeds$1323.00$"));
                list.Add(string.Format("163$Selected earth $110.25$"));
                list.Add(string.Format("164$Separation Membrane of impermeable plastic sheeting 125 micron thick$16.54$"));
                list.Add(string.Format("165$Sheathing duct$275.63$"));
                list.Add(string.Format("166$Shrubs$11.03$"));
                list.Add(string.Format("167$Sludge / Farm yard manure @ 0.18 cum per 100 sqm at site of work for turfing$275.63$"));
                list.Add(string.Format("168$Sodium vapour lamp$3087.00$"));
                list.Add(string.Format("169$Square Rubble  Coursed Stone$1212.75$"));
                list.Add(string.Format("170$Steel circular hollow pole of standard specification for street lighting to mount light at 5 m height above deck level $4961.25$"));
                list.Add(string.Format("171$Steel circular hollow pole of standard specification for street lighting to mount light at 9 m height above road level $6945.75$"));
                list.Add(string.Format("172$Steel drum 300 mm dia 1.2 m high/empty bitumen drum$330.75$"));
                list.Add(string.Format("173$Steel helmet and cushion block on top of pile head during driving.$49.61$"));
                list.Add(string.Format("174$Steel pipe 25 mm external dia as per               IS:1239$44.10$"));
                list.Add(string.Format("175$Steel pipe 50 mm external dia as per               IS:1239$88.20$"));
                list.Add(string.Format("176$Steel wire rope 20 mm $66.15$"));
                list.Add(string.Format("177$Steel wire rope 40 mm$110.25$"));
                list.Add(string.Format("178$Strip seal expansion join$9922.50$"));
                list.Add(string.Format("179$Structural Steel$50000.00$"));
                list.Add(string.Format("180$Super plastisizer admixture IS marked as per 9103-1999 $33.08$"));
                list.Add(string.Format("181$Synthetic Geogrids as per clause 3102.8 and approved design and specifications.$385.88$"));
                list.Add(string.Format("182$Through and bond stone$16.54$"));
                list.Add(string.Format("183$Tie rods 20mm diameter $55.13$"));
                list.Add(string.Format("184$Tiles size 300 x 300 mm and 25 mm thick$11.58$"));
                list.Add(string.Format("185$Timber$13230.00$"));
                list.Add(string.Format("186$Traffic cones with 150 mm reflective sleeve$694.58$"));
                list.Add(string.Format("187$Tube anchorage set complete with bearing plate, permanent wedges etc$1378.13$"));
                list.Add(string.Format("188$Unstaked lime$7500.00$"));
                list.Add(string.Format("189$Water$82.69$"));
                list.Add(string.Format("190$Water based cement paint$6.62$"));
                list.Add(string.Format("191$Welded steel wire fabric $50.72$"));
                list.Add(string.Format("192$Wire mesh 50mm x 50mm size of 3mm wire$198.45$"));
                list.Add(string.Format("193$Wooden ballies 2 Dia for bracing$2.21$"));
                list.Add(string.Format("194$Wooden ballies 8 Dia and 9 m long$5.51$"));
                list.Add(string.Format("195$Wooden packing$2205.00$"));
                list.Add(string.Format("196$Wooden staff for fastening of flag 25 mm dia, one m long$13.23$"));
                list.Add(string.Format("197$Bitumen PMB-40 / (80/25)$64134.63$"));
                list.Add(string.Format("198$RCC pipe NP-4 Heavyduty Non-pressure pipe 600mm dia$2315.25$"));
                list.Add(string.Format("199$RCC pipe NP-4 Heavyduty Non-pressure pipe 900mm dia$4079.25$"));
                list.Add(string.Format("200$AC Pipe 150mm dia$44.10$"));
                list.Add(string.Format("201$Tiles size 250 x 250 mm and 25 mm thick$9.37$"));
                list.Add(string.Format("202$Structural steel chanel beam etc.$50.00$"));
                list.Add(string.Format("203$Interlocking Tiles$0.00$"));
                list.Add(string.Format("$a) 60mm$330.75$"));
                list.Add(string.Format("$b) 80mm$441.00$"));
                list.Add(string.Format("$c) 100mm$523.69$"));
                list.Add(string.Format("204$Plastic Road studs 100 x 100 mm fitted with lense reflectors$226.01$"));




                #endregion Material Input Data
            }
            #endregion [2016 04 05]


            MyList mlist = null;


            #region Material
            list.Add(string.Format("M-001$Stone Boulder of size 150 mm and below at Cruser Plant$cum$1490.00"));
            list.Add(string.Format("M-002$Supply of quarried stone 150 - 200 mm size for Hand Broken at site$cum$1490.00"));
            list.Add(string.Format("M-003$Boulder with minimum size of 300 mm  for Pitching at Site$cum$1444.00"));
            list.Add(string.Format("M-004$Coarse sand at Mixing Plant$cum$1000.00"));
            list.Add(string.Format("M-005$Coarse sand at Site$cum$1000.00"));
            list.Add(string.Format("M-006$Fine sand at Site$cum$850.00"));
            list.Add(string.Format("M-007$Moorum at Site$cum$1130.00"));
            list.Add(string.Format("M-008$Gravel/Quarry spall at Site$Cum$1490.00"));
            list.Add(string.Format("M-009$Granular Material or hard murrum for GSB works at Site$Cum$1130.00"));
            list.Add(string.Format("M-010$Granular Material or hard murrum for GSB works at Mixing Plant$Cum$1130.00"));
            list.Add(string.Format("M-011$Fly ash conforming to IS: 3812 ( Part II  & I) atHMP Plant / Batching Plant / Crushing Plant$Cum$60.00"));
            list.Add(string.Format("M-012$Filter media/Filter Material as per Table 300-3 (MoRT&H Specification)$Cum$1540.00"));
            list.Add(string.Format("M-013$Close graded Granular sub-base Material  53 mm to 9.5 mm  $cum$1525.00"));
            list.Add(string.Format("M-014$Close graded Granular sub-base Material  37.5 mm to 9.5 mm  $cum$1540.00"));
            list.Add(string.Format("M-015$Close graded Granular sub-base Material  26.5 mm to 9.5 mm  $cum$1598.00"));
            list.Add(string.Format("M-016$Close graded Granular sub-base Material  9.5 mm to 4.75 mm  $cum$1359.50"));
            list.Add(string.Format("M-017$Close graded Granular sub-base Material  9.5 mm to 2.36 mm  $cum$1310.00"));
            list.Add(string.Format("M-018$Close graded Granular sub-base Material  4.75mm to 2.36 mm  $cum$1179.50"));
            list.Add(string.Format("M-019$Close graded Granular sub-base Material  4.75mm to 75 micron mm  $$1179.50"));
            list.Add(string.Format("M-020$Close graded Granular sub-base Material  2.36 mm $cum$1130.00"));
            list.Add(string.Format("M-021$Stone crusher dust finer than 3mm with not more than 10% passing 0.075 sieve.$cum$1130.00"));
            list.Add(string.Format("M-022$Coarse graded Granular sub-base Material  2.36 mm & below   $cum$1130.00"));
            list.Add(string.Format("M-023$Coarse graded Granular sub-base Material  4.75mm to 75 micron mm  $cum$1130.00"));
            list.Add(string.Format("M-024$Coarse graded Granular sub-base Material  4.75 mm to 2.36 mm  $cum$1179.50"));
            list.Add(string.Format("M-025$Coarse graded Granular sub-base Material  9.5 mm to 4.75 mm   $cum$1359.50"));
            list.Add(string.Format("M-026$Coarse graded Granular sub-base Material  26.5 mm to 4.75 mm  $cum$1467.50"));
            list.Add(string.Format("M-027$Coarse graded Granular sub-base Material  26.5 mm to 9.5 mm   $cum$1598.00"));
            list.Add(string.Format("M-028$Coarse graded Granular sub-base Material  37.5 mm to 9.5 mm  $cum$1540.00"));
            list.Add(string.Format("M-029$Coarse graded Granular sub-base Material  53 mm to 26 .5mm   $cum$1633.00"));
            list.Add(string.Format("M-030$Aggregates  below  5.6 mm $cum$1229.00"));
            list.Add(string.Format("M-031$Aggregates  22.4 mm to 2.36 mm $cum$1480.00"));
            list.Add(string.Format("M-032$Aggregates  22.4 mm to 5.6 mm $cum$1529.50"));
            list.Add(string.Format("M-033$Aggregates  45 mm to 2.8 mm $cum$1399.50"));
            list.Add(string.Format("M-034$Aggregates  45 mm to 22.4 mm $cum$1700.00"));
            list.Add(string.Format("M-035$Aggregates  53 mm to 2.8 mm $cum$1394.50"));
            list.Add(string.Format("M-036$Aggregates  53 mm to 22.4 mm $cum$1695.00"));
            list.Add(string.Format("M-037$Aggregates  63 mm to 2.8 mm $cum$1379.50"));
            list.Add(string.Format("M-038$Aggregates  63 mm to 45 mm $cum$1550.00"));
            list.Add(string.Format("M-039$Aggregates  90 mm to 45 mm $cum$1570.00"));
            list.Add(string.Format("M-040$Aggregates  10 mm to 5 mm $cum$1359.50"));
            list.Add(string.Format("M-041$Aggregates  11.2 mm to 0.09 mm $cum$1310.00"));
            list.Add(string.Format("M-042$Aggregates  13.2 mm to 0.09 mm $cum$1497.50"));
            list.Add(string.Format("M-043$Aggregates  13.2 mm to 5.6 mm $cum$1547.00"));
            list.Add(string.Format("M-044$Aggregates  13.2 mm to 10 mm $cum$1677.50"));
            list.Add(string.Format("M-045$Aggregates  20 mm to 10 mm $cum$1660.00"));
            list.Add(string.Format("M-046$Aggregates  25 mm to 10 mm $cum$1660.00"));
            list.Add(string.Format("M-047$Aggregates  19 mm to 6 mm $cum$1529.50"));
            list.Add(string.Format("M-048$Aggregates  37.5 mm to 19 mm $cum$1710.00"));
            list.Add(string.Format("M-049$Aggregates  37.5 mm to 25 mm $cum$1648.00"));
            list.Add(string.Format("M-050$Aggregates  6 mm nominal size$cum$1229.00"));
            list.Add(string.Format("M-051$Aggregates  10 mm nominal size$cum$1490.00"));
            list.Add(string.Format("M-052$Aggregates  13.2/12.5 mm nominal size$cum$1865.00"));
            list.Add(string.Format("M-053$Aggregates  20 mm nominal size$cum$1830.00"));
            list.Add(string.Format("M-054$Aggregates  25 mm nominal size$cum$1706.00"));
            list.Add(string.Format("M-055$Aggregates  40 mm nominal size$cum$1590.00"));
            list.Add(string.Format("M-056$AC pipe 100 mm dia$metre$38.59"));
            list.Add(string.Format("M-057$Acrylic polymer bonding coat$litre$385.88"));
            list.Add(string.Format("M-058$Alluminium Paint$litre$192.94"));
            list.Add(string.Format("M-059$Aluminium alloy plate 2mm Thick$sqm$4630.50"));
            list.Add(string.Format("M-060$Aluminium alloy/galvanised steel $tonne$46305.00"));
            list.Add(string.Format("M-061$Aluminium sheeting fixed with encapsulated lens type reflective sheeting  including 2% towards lettering, cost of angle iron, cost of drilling holes, nuts, bolts etc.and signs as applicable$sqm$6063.75"));
            list.Add(string.Format("M-062$Aluminium studs 100 x 100 mm fitted with lense reflectors$nos$374.85"));
            list.Add(string.Format("M-063$Barbed wire$kg$66.15"));
            list.Add(string.Format("M-064$Bearing (Cost of parts)$nos$8820.00"));
            list.Add(string.Format("M-065$Bearing (Cast steel rocker bearing assembly of 250 tonne )$nos$33185.25"));
            list.Add(string.Format("M-066$Bearing (Elastomeric bearing assembly consisting of 7 internal layers of elastomer bonded to 6 nos. internal reinforcing steel laminates by the process of vulcanisation,)$nos$12678.75"));
            list.Add(string.Format("M-067$Bearing (Forged steel roller bearing of 250 tonne$nos$104847.75"));
            list.Add(string.Format("M-068$Bearing (Pot type bearing assembly consisting of a metal piston supported by a disc, PTFE pads providing sliding surfaces against stainless steel mating together with cast steel assemblies/fabricated structural steel assemblies  duly painted with all components $nos$95917.50"));
            list.Add(string.Format("M-069$Bearing (PTFE sliding plate bearing assembly of 80 tonnes )$nos$38697.75"));
            list.Add(string.Format("M-070$Bearing (Supply of sliding plate bearing of 80 tonne)$nos$33185.25"));
            list.Add(string.Format("M-071$Bentonite $kg$5.51"));
            list.Add(string.Format("M-072$Binding wire$kg$66.15"));
            list.Add(string.Format("M-073$Bitumen ( Cationic Emulsion )$tonne$20883.00"));
            list.Add(string.Format("M-074$Bitumen (60-70 grade)$tonne$21613.00"));
            list.Add(string.Format("M-075$Bitumen (80-100 grade )$tonne$20813.00"));
            list.Add(string.Format("M-076$Bitumen (Cutback )$tonne$26993.00"));
            list.Add(string.Format("M-077$Bitumen (emulsion)$tonne$20883.00"));
            list.Add(string.Format("M-078$Bitumen (modified graded) $tonne$26993.00"));
            list.Add(string.Format("M-079$Brick$each$4.96"));
            list.Add(string.Format("M-080$C.I.shoes for the pile$kg$59.54"));
            list.Add(string.Format("M-081$Cement $tonne$7560.00"));
            list.Add(string.Format("M-082$Cold twisted bars (HYSD Bars)$tonne$51700.00"));
            list.Add(string.Format("M-083$Coller for joints 300 mm dia$nos$275.63"));
            list.Add(string.Format("M-084$Compressible Fibre Board(20mm thick)$sqm$496.13"));
            list.Add(string.Format("M-085$Connectors/ Staples$each$55.13"));
            list.Add(string.Format("M-086$Copper Plate(12m long x 250mmwide)$kg$661.50"));
            list.Add(string.Format("M-087$Corrosion resistant Structural steel$tonne$54022.50"));
            list.Add(string.Format("M-088$Corrugated sheet, 3 mm thick, \"Thrie\" beam section railing$kg$71.66"));
            list.Add(string.Format("M-089$Credit for excavated rock found suitable for use$cum$110.25"));
            list.Add(string.Format("M-090$Curing compound $liter$99.23"));
            list.Add(string.Format("M-091$Delineators from ISI certified firm as per the standard drawing given in IRC - 79 $each$468.56"));
            list.Add(string.Format("M-092$ Earth Cost  or  compensation for earth taken from private land$cum$33.08"));
            list.Add(string.Format("M-093$Elastomeric slab seal expansion joint assembly manufactured by using chloroprene, elastomer for elastomeric slab unit conforming to clause 915.1 of IRC: 83 (part II),$metre$11025.00"));
            list.Add(string.Format("M-094$Electric Detonators @ 1 detonator for 1/2 gelatin stick of 125 gms each$100 nos$551.25"));
            list.Add(string.Format("M-095$Epoxy compound with accessories for preparing epoxy mortar$kg$1102.50"));
            list.Add(string.Format("M-096$Epoxy mortar$kg$330.75"));
            list.Add(string.Format("M-097$Epoxy primer $kg$551.25"));
            list.Add(string.Format("M-098$Epoxy resin-hardner mix for prime coat$kg$551.25"));
            list.Add(string.Format("M-099$Flag of red color cloth 600 x 600 mm$each$33.08"));
            list.Add(string.Format("M-100$Flowering Plants $each$44.10"));
            list.Add(string.Format("M-101$Galvanised MS flat clamp$nos$275.63"));
            list.Add(string.Format("M-102$Galvanised steel wire crates of mesh size 100 mm x 100 mm woven with 4mm dia. GI wire in rolls of required size.$sqm$165.38"));
            list.Add(string.Format("M-103$Galvanised structural steel plate 200 mm wide, 6 mm thick, 24 m long$kg$60.64"));
            list.Add(string.Format("M-104$Gelatin 80%     $kg$66.15"));
            list.Add(string.Format("M-105$Geo grids$sqm$137.81"));
            list.Add(string.Format("M-106$Geomembrane$sqm$77.18"));
            list.Add(string.Format("M-107$Geonets$sqm$82.69"));
            list.Add(string.Format("M-108$Geotextile$sqm$99.23"));
            list.Add(string.Format("M-109$Geotextile filter fabric$sqm$0.00"));
            list.Add(string.Format("M-110$GI bolt 10 mm Dia$nos$5.51"));
            list.Add(string.Format("M-111$Grouting pump with agitator$hour$330.75"));
            list.Add(string.Format("M-112$ Grass (Doob)$kg$8.82"));
            list.Add(string.Format("M-113$Grass (Fine)$kg$36.38"));
            list.Add(string.Format("M-114$HDPE pipes 75mm dia$metre$115.76"));
            list.Add(string.Format("M-115$HDPE pipes 90mm dia$metre$121.28"));
            list.Add(string.Format("M-116$Hedge plants$each$12.13"));
            list.Add(string.Format("M-117$Helical pipes 600mm diameter$metre$110.25"));
            list.Add(string.Format("M-118$Hot applied thermoplastic compound$litre$81.59"));
            list.Add(string.Format("M-119$HTS strand $tonne$107486.00"));
            list.Add(string.Format("M-120$Joint Sealant Compound$kg$595.35"));
            list.Add(string.Format("M-121$Jute netting, open weave, 2.5 cm square opening for seeding and Mulching$sqm$33.08"));
            list.Add(string.Format("M-122$LDO for steam curing$litre$49.61"));
            list.Add(string.Format("M-123$M.S. Clamps$nos$13.23"));
            list.Add(string.Format("M-124$M.S. Clamps$kg$57.33"));
            list.Add(string.Format("M-125$M.S.shoes @ 35 Kg per pile of 15 m$kg$54.02"));
            list.Add(string.Format("M-126$Mild Steel bars$tonne$51700.00"));
            list.Add(string.Format("M-127$Modular  strip/box seal expansion joint including anchorage catering to a horizontal movement beyond 70 mm and upto 140mm assembly comprising of edge beams, central beam,2 modules chloroprene seal, anchorage elements, support and control system, all steel sections protected against corrosion and installed by the manufacturer or his authorised representative$metre$55.13"));
            list.Add(string.Format("M-128$Modular  strip/box seal expansion joint catering to a horizontal movement beyond 140mm and upto 210mm box/box seal joint assembly containing 3 modules/cells and comprising of edge beams, two central beams, chloroprene seal, anchorage elements, support and control system, all steel sections protected against corrosion and installed by the manufacturer or his authorised representative$metre$55.13"));
            list.Add(string.Format("M-129$Nipples 12mm$nos$5.51"));
            list.Add(string.Format("M-130$Nuts and bolts      $kg$66.15"));
            list.Add(string.Format("M-131$Paint$litre$286.65"));
            list.Add(string.Format("M-132$Pavement Marking Paint$litre$220.50"));
            list.Add(string.Format("M-133$Paving Fabric$sqm$0.00"));
            list.Add(string.Format("M-134$Perforated geosynthetic pipe 150 mm dia $metre$385.88"));
            list.Add(string.Format("M-135$Perforated pipe of cement concrete, internal dia 100 mm$metre$220.50"));
            list.Add(string.Format("M-136$Pesticide$kg$330.75"));
            list.Add(string.Format("M-137$Pipes 200 mm dia, 2.5 m long for drainage$metre$165.38"));
            list.Add(string.Format("M-138$Plastic sheath, 1.25 mm thick for dowel bars$sqm$4.41"));
            list.Add(string.Format("M-139$Plastic tubes 50 cm dia, 1.2 m high$nos$33.08"));
            list.Add(string.Format("M-140$Polymer braids$metre$192.94"));
            list.Add(string.Format("M-141$Pre moulded Joint filler,25 mm thick for expansion joint.$sqm$518.18"));
            list.Add(string.Format("M-142$Pre-coated stone chips of 13.2 mm nominal size$cum$468.56"));
            list.Add(string.Format("M-143$Preformed continuous chloroprene elastomer or closed cell foam sealing element with high tear strength, vulcanised in a single operation for the full length of a joint to ensure water tightness.$metre$154.35"));
            list.Add(string.Format("M-144$Pre-moulded asphalt filler board$sqm$771.75"));
            list.Add(string.Format("M-145$Pre-packed cement based polymer concrete of strength 45 Mpa at 28 days$kg$330.75"));
            list.Add(string.Format("M-146$Primer$kg$192.94"));
            list.Add(string.Format("M-147$Quick setting compound$kg$24.26"));
            list.Add(string.Format("M-148$Random Rubble Stone$cum$496.13"));
            list.Add(string.Format("M-149$RCC Pipe NP 4 heavy duty non presure pipe 1000 mm dia $metre$5250.00"));
            list.Add(string.Format("M-150$RCC Pipe NP 4 heavy duty non presure pipe 1200 mm dia $metre$6510.00"));
            list.Add(string.Format("M-151$RCC Pipe NP 4 heavy duty non presure pipe 300 mm dia$metre$787.50"));
            list.Add(string.Format("M-152$Reflectorising glass beads$kg$66.15"));
            list.Add(string.Format("M-153$Reinforcement strips 60 mm wide 5 mm thick as per clause 3102. (Copper Strips)$metre$121.28"));
            list.Add(string.Format("M-154$Reinforcement strips 60 mm wide 5 mm thick as per clause 3102. (Galvanised carbon steel strips)$metre$13.23"));
            list.Add(string.Format("M-155$Reinforcement strips 60 mm wide 5 mm thick as per clause 3102. (Glass reinforced polymer/fibre reinforced polymer/polymeric strips)$metre$24.26"));
            list.Add(string.Format("M-156$Reinforcement strips 60 mm wide 5 mm thick as per clause 3102. (Stainless steel strips)$metre$14.33"));
            list.Add(string.Format("M-157$Reinforcement strips 60 mm wide 5 mm thick as per clause 3102. Aluminium strips)$metre$24.26"));
            list.Add(string.Format("M-158$Rivets$each$0.33"));
            list.Add(string.Format("M-159$Sand bags (Cost of sand and Empty cement bag)$nos$8.82"));
            list.Add(string.Format("M-160$Sapling 2 m high 25 mm dia $each$16.54"));
            list.Add(string.Format("M-161$Scrap tyres of size 900 x 20$nos$88.20"));
            list.Add(string.Format("M-162$Seeds$kg$1323.00"));
            list.Add(string.Format("M-163$Selected earth $cum$110.25"));
            list.Add(string.Format("M-164$Separation Membrane of impermeable plastic sheeting 125 micron thick$sqm$16.54"));
            list.Add(string.Format("M-165$Sheathing duct$metre$275.63"));
            list.Add(string.Format("M-166$Shrubs$each$11.03"));
            list.Add(string.Format("M-167$Sludge / Farm yard manure @ 0.18 cum per 100 sqm at site of work for turfing$cum$275.63"));
            list.Add(string.Format("M-168$Sodium vapour lamp$each$3087.00"));
            list.Add(string.Format("M-169$Square Rubble  Coursed Stone$cum$1212.75"));
            list.Add(string.Format("M-170$Steel circular hollow pole of standard specification for street lighting to mount light at 5 m height above deck level $each$4961.25"));
            list.Add(string.Format("M-171$Steel circular hollow pole of standard specification for street lighting to mount light at 9 m height above road level $each$6945.75"));
            list.Add(string.Format("M-172$Steel drum 300 mm dia 1.2 m high/empty bitumen drum$nos$330.75"));
            list.Add(string.Format("M-173$Steel helmet and cushion block on top of pile head during driving.$kg$49.61"));
            list.Add(string.Format("M-174$Steel pipe 25 mm external dia as per               IS:1239$metre$44.10"));
            list.Add(string.Format("M-175$Steel pipe 50 mm external dia as per               IS:1239$metre$88.20"));
            list.Add(string.Format("M-176$Steel wire rope 20 mm $kg$66.15"));
            list.Add(string.Format("M-177$Steel wire rope 40 mm$kg$110.25"));
            list.Add(string.Format("M-178$Strip seal expansion join$metre$9922.50"));
            list.Add(string.Format("M-179$Structural Steel$tonne$50246.30"));
            list.Add(string.Format("M-180$Super plastisizer admixture IS marked as per 9103-1999 $kg$33.08"));
            list.Add(string.Format("M-181$Synthetic Geogrids as per clause 3102.8 and approved design and specifications.$sqm$385.88"));
            list.Add(string.Format("M-182$Through and bond stone$each$16.54"));
            list.Add(string.Format("M-183$Tie rods 20mm diameter $nos$55.13"));
            list.Add(string.Format("M-184$Tiles size 300 x 300 mm and 25 mm thick$each$11.58"));
            list.Add(string.Format("M-185$Timber$cum$13230.00"));
            list.Add(string.Format("M-186$Traffic cones with 150 mm reflective sleeve$nos$694.58"));
            list.Add(string.Format("M-187$Tube anchorage set complete with bearing plate, permanent wedges etc$nos$1378.13"));
            list.Add(string.Format("M-188$Unstaked lime$tonne$7500.00"));
            list.Add(string.Format("M-189$Water$KL$82.69"));
            list.Add(string.Format("M-190$Water based cement paint$litre$6.62"));
            list.Add(string.Format("M-191$Welded steel wire fabric $kg$50.72"));
            list.Add(string.Format("M-192$Wire mesh 50mm x 50mm size of 3mm wire$kg$198.45"));
            list.Add(string.Format("M-193$Wooden ballies 2\" Dia for bracing$each$2.21"));
            list.Add(string.Format("M-194$Wooden ballies 8\" Dia and 9 m long$each$5.51"));
            list.Add(string.Format("M-195$Wooden packing$cum$2205.00"));
            list.Add(string.Format("M-196$Wooden staff for fastening of flag 25 mm dia, one m long$each$13.23"));
            list.Add(string.Format("M-197$Bitumen PMB-40 (80/25)$MT$64134.63"));
            list.Add(string.Format("M-198$RCC pipe NP-4 Heavyduty Non-pressure pipe 600mm dia$Rm$2315.25"));
            list.Add(string.Format("M-199$RCC pipe NP-4 Heavyduty Non-pressure pipe 900mm dia$Rm$4079.25"));
            list.Add(string.Format("M-200$AC Pipe 150mm dia$Rm$44.10"));
            list.Add(string.Format("M-201$Tiles size 250 x 250 mm and 25 mm thick$Each$9.37"));
            list.Add(string.Format("M-202$Structural steel chanel beam etc.$Kg.$102.27"));
            list.Add(string.Format("M-203$Interlocking Tiles$$0.00"));
            list.Add(string.Format("$a) 60mm$Sqm$330.75"));
            list.Add(string.Format("$b) 80mm$Sqm$441.00"));
            list.Add(string.Format("$c) 100mm$Sqm$523.69"));
            list.Add(string.Format("M-204$Plastic Road studs 100 x 100 mm fitted with lense reflectors$Nos.$226.01"));


            foreach (var item in list)
            {
                mlist = new MyList(MyList.RemoveAllSpaces(item), '$');

                //mlist.StringList.Insert(0, "");
                dgv.Rows.Add(mlist.StringList.ToArray());
            }
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            #endregion Material

            dgv = dgv_rate_labour;

            #region labour

            list.Clear();
            list.Add(string.Format("L-01$Blacksmith (IInd class)$day$321.00"));
            list.Add(string.Format("L-02$Blacksmith (Ist class)/ Welder/ Plumber/ Electrician$day$353.00"));
            list.Add(string.Format("L-03$Blaster (Stone cutter)$day$353.00"));
            list.Add(string.Format("L-04$Carpenter I Class$day$353.00"));
            list.Add(string.Format("L-05$Chiseller (Head Mazdoor)$day$353.00"));
            list.Add(string.Format("L-06$Driller (Jumper)$day$353.00"));
            list.Add(string.Format("L-07$Diver$day$353.00"));
            list.Add(string.Format("L-08$Fitter$day$353.00"));
            list.Add(string.Format("L-09$Mali$day$291.00"));
            list.Add(string.Format("L-10$Mason  (IInd  class)$day$321.00"));
            list.Add(string.Format("L-11$Mason  (Ist  class)$day$353.00"));
            list.Add(string.Format("L-12$Mate / Supervisor$day$353.00"));
            list.Add(string.Format("L-13$Mazdoor  $day$265.00"));
            list.Add(string.Format("L-14$Mazdoor/Dresser (Semi Skilled)$day$291.00"));
            list.Add(string.Format("L-15$Mazdoor/Dresser/Sinker (Skilled)$day$321.00"));
            list.Add(string.Format("L-16$Medical Officer$day$1031.00"));
            list.Add(string.Format("L-17$Operator(grouting)$day$353.00"));
            list.Add(string.Format("L-18$Painter I class$day$353.00"));
            list.Add(string.Format("L-19$Para medical personnel$day$353.00"));


            foreach (var item in list)
            {
                mlist = new MyList(MyList.RemoveAllSpaces(item), '$');

                //mlist.StringList.Insert(0, "");
                dgv.Rows.Add(mlist.StringList.ToArray());
            }
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            #endregion labour


            dgv = dgv_rate_machine;

            #region Machine

            list.Clear();
            list.Add(string.Format("P&M-001$Air Compressor$ hour$339.90"));
            list.Add(string.Format("P&M-002$Batching and Mixing Plant (a)  30 cum capacity$ hour$2376.00"));
            list.Add(string.Format("P&M-003$Batching and Mixing Plant (b)  15 - 20 cum capacity$ hour$1980.00"));
            list.Add(string.Format("P&M-004$Bitumen Pressure Distributor$ hour$1141.80"));
            list.Add(string.Format("P&M-005$Bitumen Boiler oil fired$ hour$211.20"));
            list.Add(string.Format("P&M-006$Concrete Paver Finisher with 40 HP Motor$ hour$3052.50"));
            list.Add(string.Format("P&M-007$Concrete Pump of 45 & 30 cum capacity$ hour$272.25"));
            list.Add(string.Format("P&M-008$Concrete Bucket$ hour$16.50"));
            list.Add(string.Format("P&M-009$Concrete Mixer (a)  0.4/0.28 cum$ hour$247.50"));
            list.Add(string.Format("P&M-010$Concrete Mixer (b) 1 cum$ hour$247.50"));
            list.Add(string.Format("P&M-011$Crane (a)  80 tonnes$ hour$1361.25"));
            list.Add(string.Format("P&M-012$Cranes b)  35 tonnes$ hour$907.50"));
            list.Add(string.Format("P&M-013$Cranes c)  3 tonnes$ hour$379.50"));
            list.Add(string.Format("P&M-014$Dozer D - 80 - A 12$ hour$3960.00"));
            list.Add(string.Format("P&M-015$Dozer D - 50 - A 15$ hour$2347.95"));
            list.Add(string.Format("P&M-016$Emulsion Pressure Distributor$ hour$851.40"));
            list.Add(string.Format("P&M-017$Front End loader 1 cum bucket capacity$ hour$858.00"));
            list.Add(string.Format("P&M-018$Generator (a)  125 KVA$ hour$742.50"));
            list.Add(string.Format("P&M-019$Generator( b)  63  KVA$ hour$396.00"));
            list.Add(string.Format("P&M-020$GSB Plant 50 cum$ hour$1105.50"));
            list.Add(string.Format("P&M-021$Hotmix Plant - 120 TPH capacity$ hour$24915.00"));
            list.Add(string.Format("P&M-022$Hotmix Plant - 100 TPH capacity$ hour$18425.55"));
            list.Add(string.Format("P&M-023$Hotmix Plant - 60 to 90 TPH capacity$ hour$14734.50"));
            list.Add(string.Format("P&M-024$Hotmix Plant - 40 to 60 TPH capacity$ hour$11797.50"));
            list.Add(string.Format("P&M-025$Hydraulic Chip Spreader$ hour$2805.00"));
            list.Add(string.Format("P&M-026$Hydraulic Excavator of 1 cum bucket$ hour$1386.00"));
            list.Add(string.Format("P&M-027$Integrated Stone Crusher 100THP$ hour$9223.50"));
            list.Add(string.Format("P&M-028$Integrated Stone Crusher 200 HP$ hour$19404.00"));
            list.Add(string.Format("P&M-029$Kerb Casting Machine$ hour$330.00"));
            list.Add(string.Format("P&M-030$Mastic Cooker$ hour$66.00"));
            list.Add(string.Format("P&M-031$Mechanical Broom Hydraulic$ hour$379.50"));
            list.Add(string.Format("P&M-032$Motor Grader 3.35 mtr blade$ hour$2549.25"));
            list.Add(string.Format("P&M-033$Mobile slurry seal equipment$ hour$1072.50"));
            list.Add(string.Format("P&M-034$Paver Finisher Hydrostatic with sensor control 100 TPH$ hour$2846.25"));
            list.Add(string.Format("P&M-035$Paver Finisher Mechanical 100 TPH$ hour$1037.85"));
            list.Add(string.Format("P&M-036$Piling Rig with Bantonite Pump$ hour$5816.25"));
            list.Add(string.Format("P&M-037$Pneumatic Road Roller$ hour$1323.30"));
            list.Add(string.Format("P&M-038$Pneumatic Sinking Plant$ hour$4438.50"));
            list.Add(string.Format("P&M-039$Pot Hole Repair Machine$ hour$965.25"));
            list.Add(string.Format("P&M-040$Prestressing Jack with Pump & access$ hour$136.95"));
            list.Add(string.Format("P&M-041$Ripper$ hour$29.70"));
            list.Add(string.Format("P&M-042$Rotavator$ hour$18.15"));
            list.Add(string.Format("P&M-043$Road marking machine$ hour$99.00"));
            list.Add(string.Format("P&M-044$Smooth Wheeled Roller 8 tonne$ hour$490.05"));
            list.Add(string.Format("P&M-045$Tandem Road Roller$ hour$1217.70"));
            list.Add(string.Format("P&M-046$Tipper - 5 cum$ km$25.74"));
            list.Add(string.Format("P&M-047$Tipper - 5 cum$ tonne.km$2.87"));
            list.Add(string.Format("P&M-048$Tipper - 5 cum$ hour$330.00"));
            list.Add(string.Format("P&M-049$Transit Mixer 4.0/4.5 cum$ hour$990.00"));
            list.Add(string.Format("P&M-050$Transit Mixer 4/4.5 cum$ tonne.km$8.61"));
            list.Add(string.Format("P&M-051$Transit Mixer 3.0 cum$ hour$907.50"));
            list.Add(string.Format("P&M-052$Transit Mixer 3.0 cum$ tonne.km$8.09"));
            list.Add(string.Format("P&M-053$Tractor$ hour$386.10"));
            list.Add(string.Format("P&M-054$Tractor with Rotevator$ hour$404.25"));
            list.Add(string.Format("P&M-055$Tractor with Ripper$ hour$415.80"));
            list.Add(string.Format("P&M-056$Truck 5.5 cum per 10 tonnes$ km$23.93"));
            list.Add(string.Format("P&M-057$Truck 5.5 cum per 10 tonnes$ hour$495.00"));
            list.Add(string.Format("P&M-058$Truck 5.5 cum per 10 tonnes$ tonne.km$2.64"));
            list.Add(string.Format("P&M-059$Vibratory Roller 8 tonne$ hour$1640.10"));
            list.Add(string.Format("P&M-060$Water Tanker$ hour$495.00"));
            list.Add(string.Format("P&M-061$Water Tanker$ km$25.74"));
            list.Add(string.Format("P&M-062$Wet Mix Plant 60 TPH$ hour$1282.05"));
            list.Add(string.Format("P&M-063$Air compressor with pneumatic chisel attachment for cutting hard clay.$hour$340"));
            list.Add(string.Format("P&M-064$Batch type cold mixing plant 100-120 TPH capacity producing an average output of 75 tonne per hour$hour$11798"));
            list.Add(string.Format("P&M-065$Belt conveyor system$hour$990"));
            list.Add(string.Format("P&M-066$Boat to carry atleast 20 persons$hour$495"));
            list.Add(string.Format("P&M-067$Cement concrete batch mix plant @ 175 cum per hour (effective output)$hour$7100"));
            list.Add(string.Format("P&M-068$Cement concrete batch mix plant @ 75 cum per hour$hour$5050"));
            list.Add(string.Format("P&M-069$Cold milling machine @ 20 cum per hour$hour$2200"));
            list.Add(string.Format("P&M-070$Crane  5 tonne capacity $hour$380"));
            list.Add(string.Format("P&M-071$Crane  10 tonne capacity $hour$495"));
            list.Add(string.Format("P&M-072$Crane  15 tonne capacity $hour$611"));
            list.Add(string.Format("P&M-073$Crane  20 tonne capacity $hour$743"));
            list.Add(string.Format("P&M-074$Crane 40 T capacity$hour$908"));
            list.Add(string.Format("P&M-075$Crane with grab 0.75 cum capacity $hour$1518"));
            list.Add(string.Format("P&M-076$Compressor with guniting equipment along with accessories$hour$137"));
            list.Add(string.Format("P&M-077$Drum mix plant for cold mixes of appropriate capacity but not less than 75 tonnes/hour.$hour$11798"));
            list.Add(string.Format("P&M-078$Epoxy Injection gun$hour$66"));
            list.Add(string.Format("P&M-079$Generator 33 KVA$hour$396"));
            list.Add(string.Format("P&M-080$Generator 100 KVA$hour$743"));
            list.Add(string.Format("P&M-081$Generator 250 KVA$hour$1073"));
            list.Add(string.Format("P&M-082$Induction, deinduction and erection of plant and equipment including all components and accessories for pneumatic method of well sinking. $hour$4439"));
            list.Add(string.Format("P&M-083$Joint Cutting Machine with 2-3 blades (for rigid pavement)$hour$413"));
            list.Add(string.Format("P&M-084$Jack for Lifting  40 tonne lifting capacity.$day$825"));
            list.Add(string.Format("P&M-085$Piling rig Including double acting pile driving hammer (Hydraulic rig) $hrs$5816"));
            list.Add(string.Format("P&M-086$Plate compactor$hour$124"));
            list.Add(string.Format("P&M-087$Snow blower equipment 140 HP @ 600 cum per hour$hour$1650"));
            list.Add(string.Format("P&M-088$Texturing machine (for rigid pavement)$hour$330"));
            list.Add(string.Format("P&M-089$Truck Trailor 30 tonne  capacity$hour$825"));
            list.Add(string.Format("P&M-090$ Truck Trailor 30 tonne  capacity$t.km$28"));
            list.Add(string.Format("P&M-091$Tunnel Boring machine$hour$33000"));
            list.Add(string.Format("P&M-092$Vibrating Pile driving hammer complete with power unit and accessories.$hour$16500"));
            list.Add(string.Format("P&M-093$Wet Mix Plant 100 TPH $hour$1815"));
            list.Add(string.Format("P&M-094$Wet Mix Plant 75 TPH $hour$1485"));


            foreach (var item in list)
            {
                mlist = new MyList(MyList.RemoveAllSpaces(item), '$');

                //mlist.StringList.Insert(0, "");
                dgv.Rows.Add(mlist.StringList.ToArray());
            }
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            #endregion labour

        }

        void Load_Cost_Estimate_Data()
        {
            DataGridView dgv = dgv_cost_estimate;

            List<string> list = new List<string>();

            MyList mlist = null;

            #region
            list.Add(string.Format("6C,01$Earthwork in excavation of foundation for structures complete as per drawing and Technical Specification Clause 304  including all Leads and Lifts                                               $$$$"));
            list.Add(string.Format("a)$a)Ordinary and soft Soils (material to be used in embankments)$Cum$$ 60 $ -   "));
            list.Add(string.Format("b)$b)Ordinary and soft Soils (material to disposal)$Cum$$$ -   "));
            list.Add(string.Format("c)$c)Soft rock$Cum$$$ -   "));
            list.Add(string.Format("6C,02$Providing Back filling behind abutments, wing walls and return walls, with selected imported granular material of approved quality including all leads and lifts complete as per Technical Specification Clause 305.$Cum$$ 2,975 $ -   "));
            list.Add(string.Format("6C,03$Providing and laying of Filter media of Stone aggregate conforming to Technical Specifications Clause 2504.2.2 behind abutment, wing wall, retaining wall and return walls, complete as per drawing and Technical Specifications Clause 305 & 2504.$Cum$$ 2,970 $ -   "));
            list.Add(string.Format("6C,04$Providing Plain Cement concrete  for  levelling course etc., under foundations including centering complete as per drawing and Technical Specification  clauses 1500,1700,& 2100.$$$$ -   "));
            list.Add(string.Format("a)$PCC M15 grade$Cum$$ 7,262 $ -   "));
            list.Add(string.Format("6C,05$Providing and laying Reinforced cement concrete for foundations complete as per drawing and Technical Specifications Section 1500,1700, 2100  & 2200 (Abutment Pile Cap and Pier Pile Cap)$$$$ -   "));
            list.Add(string.Format("a)$PCC M20$Cum$$ 8,022 $ -   "));
            list.Add(string.Format("b)$RCC M25$Cum$$ 8,893 $ -   "));
            list.Add(string.Format("c)$RCC M30$Cum$$ 8,909 $ -   "));
            list.Add(string.Format("d)$RCC M35$Cum$$ 9,031 $ -   "));
            list.Add(string.Format("e)$RCC M40$Cum$$$ -   "));
            list.Add(string.Format("f)$HYSD bar reinforcement complete as per drawing and Technical Specification  clause no 1600. for 6A,05a to 6A,05e$Mt$$ 79,364 $ -   "));
            list.Add(string.Format("6C,06$Structural Cement concrete for  Reinforced cement concrete for ABUTMENT including centering complete as per drawing  Technical specification  clauses 1500,1700,2200$$$$ -   "));
            list.Add(string.Format("a)$PCC M30$Cum$$$ -   "));
            list.Add(string.Format("b)$RCC M30$Cum$$ 9,477 $ -   "));
            list.Add(string.Format("c)$RCC M35$Cum$$ 9,656 $ -   "));
            list.Add(string.Format("d)$RCC M40$Cum$$$ -   "));
            list.Add(string.Format("e)$RCC M45$Cum$$$ -   "));
            list.Add(string.Format("f)$HYSD bar reinforcement complete as per drawing and Technical Specification  clause no 1600. for 6A,06a to 6A,06e$Mt$$ 79,517 $ -   "));
            list.Add(string.Format("6C,07$Structural Cement concrete for  Reinforced cement concrete for ABUTMENT CAP including centering complete as per drawing  Technical specification  clauses 1500,1700,2200$$$$ -   "));
            list.Add(string.Format("a)$PCC M30$Cum$$$ -   "));
            list.Add(string.Format("b)$RCC M30$Cum$$ 9,477 $ -   "));
            list.Add(string.Format("c)$RCC M35$Cum$$ 9,656 $ -   "));
            list.Add(string.Format("d)$RCC M40$Cum$$$ -   "));
            list.Add(string.Format("e)$RCC M45$Cum$$$ -   "));
            list.Add(string.Format("f)$HYSD bar reinforcement complete as per drawing and Technical Specification  clause no 1600. for 6A,07g$Mt$$ 79,517 $ -   "));
            list.Add(string.Format("6C,08$Structural Cement concrete for  Reinforced cement concrete for DIRT WALL including centering complete as per drawing  Technical specification  clauses 1500,1700,2200$$$$ -   "));
            list.Add(string.Format("a)$PCC M30$Cum$$$ -   "));
            list.Add(string.Format("b)$RCC M30$Cum$$ 9,477 $ -   "));
            list.Add(string.Format("c)$RCC M35$Cum$83.8$ 9,656 $ 809,293 "));
            list.Add(string.Format("d)$RCC M40$Cum$$$ -   "));
            list.Add(string.Format("e)$RCC M45$Cum$$$ -   "));
            list.Add(string.Format("f)$HYSD bar reinforcement complete as per drawing and Technical Specification  clause no 1600. for 6A,08a to 6A,08e$Mt$9.2$ 79,517 $ 733,097 "));
            list.Add(string.Format("6C,09$Structural Cement concrete for  Reinforced cement concrete for PEDESTAL including centering complete as per drawing  Technical specification  clauses 1500,1700,2200$$$$ -   "));
            list.Add(string.Format("a)$PCC M30$Cum$$$ -   "));
            list.Add(string.Format("b)$RCC M30$Cum$$ 9,477 $ -   "));
            list.Add(string.Format("c)$RCC M35$Cum$$ 9,656 $ -   "));
            list.Add(string.Format("d)$RCC M40$Cum$6.9$ 11,973 $ 82,757 "));
            list.Add(string.Format("e)$RCC M45$Cum$$$ -   "));
            list.Add(string.Format("f)$HYSD bar reinforcement complete as per drawing and Technical Specification  clause no 1600. for 6A,09a to 6A,08e$Mt$0.8$ 79,517 $ 65,955 "));
            list.Add(string.Format("6C,10$Structural Cement concrete for  Reinforced cement concrete for PIER including centering complete as per drawing  Technical specification  clauses 1500,1700,2200$$$$ -   "));
            list.Add(string.Format("a)$PCC M30$Cum$$$ -   "));
            list.Add(string.Format("b)$RCC M30$Cum$$ 9,477 $ -   "));
            list.Add(string.Format("c)$RCC M35$Cum$$ 9,656 $ -   "));
            list.Add(string.Format("d)$RCC M40$Cum$$$ -   "));
            list.Add(string.Format("e)$RCC M45$Cum$$$ -   "));
            list.Add(string.Format("f)$HYSD bar reinforcement complete as per drawing and Technical Specification  clause no 1600. for 6A,10a to 6A,10e$Mt$$ 79,517 $ -   "));
            list.Add(string.Format("6C,11$Structural Cement concrete for  Reinforced cement concrete for PIER CAP including centering complete as per drawing  Technical specification  clauses 1500,1700,2200$$$$ -   "));
            list.Add(string.Format("a)$PCC M30$Cum$$$ -   "));
            list.Add(string.Format("b)$RCC M30$Cum$$ 9,477 $ -   "));
            list.Add(string.Format("c)$RCC M35$Cum$$ 9,656 $ -   "));
            list.Add(string.Format("d)$RCC M40$Sqm$$$ -   "));
            list.Add(string.Format("e)$RCC M45$Cum$$$ -   "));
            list.Add(string.Format("f)$HYSD bar reinforcement complete as per drawing and Technical Specification  clause no 1600. for 6A,11a to 6A,11e$Mt$$ 79,517 $ -   "));
            list.Add(string.Format("6C,12$Structural Cement concrete for Reinforced cement concrete/PSC for GIRDER including centering complete as per drawing  Technical specification  clauses 809,1500,1700  $$$$ -   "));
            list.Add(string.Format("a)$RCC M30$Cum$$ 10,772 $ -   "));
            list.Add(string.Format("b)$RCC M35$Cum$$ 10,788 $ -   "));
            list.Add(string.Format("c)$RCC M40$Cum$$$ -   "));
            list.Add(string.Format("d)$RCC M45$Cum$$$ -   "));
            list.Add(string.Format("$PSC GIRDER (BOX)$$$$ -   "));
            list.Add(string.Format("e)$PSC M40$Cum$$$ -   "));
            list.Add(string.Format("f)$PSC M45$Cum$$ 12,658 $ -   "));
            list.Add(string.Format("g)$PSC M50$Cum$$ 12,922 $ -   "));
            list.Add(string.Format("h)$HYSD bar reinforcement complete as per drawing and Technical Specification  clause no 1600. for 6A,12a to 6A12d$Mt$$ 80,779 $ -   "));
            list.Add(string.Format("i)$HT Steel for PSC concrete complete as per drawing and Technical Specification 1800,1009 for 6A,12e to 6A,12g$Mt$$ 177,450 $ -   "));
            list.Add(string.Format("6C,13$Structural Cement concrete for Reinforced cement concrete/PSC for SLAB including centering complete as per drawing  Technical specification  clauses 809,1500,1700  $$$$ -   "));
            list.Add(string.Format("a)$RCC M30$Cum$$ 10,772 $ -   "));
            list.Add(string.Format("b)$RCC M35$Cum$906.0$ 10,788 $ 9,774,415 "));
            list.Add(string.Format("c)$RCC M40$Cum$$$ -   "));
            list.Add(string.Format("d)$RCC M45$Cum$$$ -   "));
            list.Add(string.Format("$PSC Box GIRDER$$$$ -   "));
            list.Add(string.Format("e)$PSC M40$Cum$$$ -   "));
            list.Add(string.Format("f)$PSC M45$Cum$$ 12,658 $ -   "));
            list.Add(string.Format("g)$PSC M50$Cum$$ 12,922 $ -   "));
            list.Add(string.Format("h)$HYSD bar reinforcement complete as per drawing and Technical Specification  clause no 1600. for 6A,13a to 6A,13d$Mt$145.0$ 80,779 $ 11,710,308 "));
            list.Add(string.Format("i)$HT Steel for PSC concrete complete as per drawing and Technical Specification 1800,1009 for 6A,13e to 6A,13g$Mt$$ 177,450 $ -   "));
            list.Add(string.Format("6C,14$Providing bored cast-in-situ M35 grade RCC piles excluding reinforcement complete as per drawings and technical specifications and removal of excavated earth with all lifts and lead up to 1000m$$$$ -   "));
            list.Add(string.Format("a)$RCC M35 Pile Cap$Cum$$ 9,162 $ -   "));
            list.Add(string.Format("b)$RCC M35 1.2m dia piles$Rm$$ 16,272 $ -   "));
            list.Add(string.Format("c)$Providing MS Linear for Pile $MT$$ 60,000 $ -   "));
            list.Add(string.Format("6C,15$Pile load test on single vertical pile in accordance with IS:2911 (Part IV)$$$$ -   "));
            list.Add(string.Format("a)$a) Initial load test$Nos$$ 75,000 $ -   "));
            list.Add(string.Format("b)$b) Routine Load Test$Nos$$ 25,000 $ -   "));
            list.Add(string.Format("6C,16$Bituminous Concrete Wearing Coat of total 65 mm thick comprising two layers of 25 mm thick Bituminous Concrete  laid over a coat of 15 mm thick Mastic Asphalt as per Technical Specification  Sections 509, 515, 521, & 2700.$$$$ -   "));
            list.Add(string.Format("a)$Wearing coat$Sqm$3061.7$ 774 $ 2,369,309 "));
            list.Add(string.Format("6C,17$Providing and fixing drainage spouts complete as per drawing, Technical Specification clause 2705.$Nos$60.0$ 1,605 $ 96,300 "));
            list.Add(string.Format("$Down take pipe$Rm$523.9$ 300 $ 157,163 "));
            list.Add(string.Format("6C,18$Providing and fixing bearings complete as per drawing and Technical Specification 2000,2200 and 2300.$$$$ -   "));
            list.Add(string.Format("a)$a) Elastomeric bearings$CC$$ 1 $ -   "));
            list.Add(string.Format("b)$b) Pot bearings$nos$64.0$ 50,000 $ 3,200,000 "));
            list.Add(string.Format("c)$c) Craft Paper / Tap Paper bearings$Rm$$ 250 $ -   "));
            list.Add(string.Format("6C,19$Providing and fixing expansion joints complete as per drawing and Technical Specification No. 2600 and confirming to   MoRT&H  modified interim specifications $$$$ -   "));
            list.Add(string.Format("a)$20 mm thick compressible fibre board$Rm$$ 178 $ -   "));
            list.Add(string.Format("c)$Strip seal type of expansion joint complete as per drawing and Technical specification $Rm$204.2$ 14,369 $ 2,933,520 "));
            list.Add(string.Format("6C,20$Providing Reinforced Cement Concrete M 30 grade in approach slabs including reinforcement complete as per drawings and Technical Specifications 1500,1600 and 1700 & 2704$$$$ -   "));
            list.Add(string.Format("a)$PCC M15$Cum$41.0$ 6,984 $ 286,591 "));
            list.Add(string.Format("b)$RCC M30 (including Reinforcement)$Cum$85.7$ 12,827 $ 1,099,859 "));
            list.Add(string.Format("c)$Providing, cutting, bending and fixing in position of TMT bar reinforcement in reinforced concrete structure for Approach Slab complete as per drawing and Technical Specification Section 1600$Ton$$$ -   "));
            list.Add(string.Format("6C,21$Providing weep holes in abutments, wing walls and return walls complete as per drawing and Technical Specification 2706.$Nos$$ 99 $ -   "));
            list.Add(string.Format("6C,22$Providing and constructing RCC crash barrier M 40 grade including cost of centering as per approved Drawings and Technical Specifications Clause 809 and as per IRC-6$Rm$317.3$ 4,106 $ 1,302,737 "));
            list.Add(string.Format("6C,23$Providing and Construction of Hand-Railing (cast-in-situ) M35 Grade including cost of centering as per approved Drawings and Technical Specifications $LM$317.3$ 1,200 $ 380,732 "));
            list.Add(string.Format("6C,24$Painting of Crash Barrier$Sqm$951.8$ 250 $ 237,957 "));
            list.Add(string.Format("6C,25$Providing and Construction of footpath$Sqm$475.9$ 871 $ 414,521 "));
            list.Add(string.Format("6C,26$Painting  of FO / ROB / RUB No. and Span arrangement as per Technical specifications$Nos$2.0$ 200 $ 400 "));
            list.Add(string.Format("6C,27$Providing Plain Cement Concrete for Curtainwalls /toe walls etc. including centering all complete as per drawing and Technical Specifications Section Section 1500 & 1700$$$$ -   "));
            list.Add(string.Format("a)$PCC M15$Cum$$ 7,262 $ -   "));
            list.Add(string.Format("b)$PCC M20$Cum$$$ -   "));
            list.Add(string.Format("6C,28$Providing and laying Dry Boulder apron complete as per Drawings and Technical Specifications Clause 2503$Cum$$ 5,222 $ -   "));
            list.Add(string.Format("6C,29$Providing and laying PCC filling for bed protection  as per Drawings and Technical Specifications $$$$ -   "));
            list.Add(string.Format("a)$PCC M10 grade$Cum$$ 6,517 $ -   "));
            list.Add(string.Format("b)$PCC M15 grade$Cum$$ 7,262 $ -   "));
            list.Add(string.Format("6C,30$Providing boulder apron complete as per drawings and Technical Specifications Section 2500$Cum$$ 5,222 $ -   "));
            list.Add(string.Format("6C,31$Filter media (150 mm thick) underneath pitching in slopes and flat surfaces complete as per drawing & Technical Specification Clause 2504$Cum$$ 3,150 $ -   "));
            list.Add(string.Format("6C,32$Stone pitching on sloped sufaces (300 mm thick) for earth protection complete (for Quadrants) as per drawings and Technical Specification 2504$Cum$$ 5,222 $ -   "));
            list.Add(string.Format("6C,33$Construction of flexible apron 1 m thick comprising of loose stone boulders weighing not less than 40 kg beyond curtain wall.$Cum$$ 5,322 $ -   "));
            list.Add(string.Format("6C,34$Providing and laying flat stone apron 150 mm thick flat stone embedded in 300 mm thick concrete $Cum$$ 9,638 $ -   "));
            list.Add(string.Format("6C,35$Provision for Protection Works$Ls$$$ -   "));
            list.Add(string.Format("6C,36$Carry out confirmatory bores upto required depths at each foundation location of bridge complete in all respects including testing as per Technical Specifications Section 2400 and interpretation of the bore data and presentation of the results as per IRC $$$$ -   "));
            list.Add(string.Format("a)$i) Depth between 0 m to 10 m$LM$$$ -   "));
            list.Add(string.Format("b)$ii) Depth between 10 m to 20 m$LM$$$ -   "));
            list.Add(string.Format("c)$iii) Depth between 20 m to 30 m$LM$$$ -   "));
            list.Add(string.Format("6C,37$Construction of RCC Retaining Wall / Median Wall$$$$ -   "));
            list.Add(string.Format("a)$M25 Grade$$$$ -   "));
            list.Add(string.Format("b)$M30 Grade$$$ 9,477 $ -   "));
            list.Add(string.Format("c)$M35 Grade$Cum$$ 9,656 $ -   "));
            list.Add(string.Format("d)$HYSD bar reinforcement complete as per drawing and Technical Specification  clause no 1600.$Mt$$ 79,517 $ -   "));
            list.Add(string.Format("6C,38$Providing and Fixing Steel Girder for Superstructure as per Technical Specification$Mt$1268.7$ 100,000 $ 126,872,050 "));
            list.Add(string.Format("6C,39$Well Foundation$$$$ -   "));
            list.Add(string.Format("a)$Cutting Edge$MT$$$ -   "));
            list.Add(string.Format("b)$Well Curb  M 35$Cum$$$ -   "));
            list.Add(string.Format("c)$Well Steining M 35$Cum$$$ -   "));
            list.Add(string.Format("d)$Bottom Plug M 30$Cum$$$ -   "));
            list.Add(string.Format("e)$Top and Intermediate Plug M 25$Cum$$$ -   "));
            list.Add(string.Format("f)$Well Cap M 35$Cum$$$ -   "));
            list.Add(string.Format("g)$TMT Steel for Curb, Steining and Well Cap$Ton$$$ -   "));
            list.Add(string.Format("h)$Sinking of Well - Abutment 0 to 10 m$m$$$ -   "));
            list.Add(string.Format("i)$Sinking of Well - Abutment 10 to 20 m$m$$$ -   "));
            list.Add(string.Format("j)$Sinking of Well - Abutment 20 to 30 m$m$$$ -   "));
            list.Add(string.Format("k)$Sinking of Well - Pier 0 to 10 m$m$$$ -   "));
            list.Add(string.Format("l)$Sinking of Well - Pier 10 to 20 m$m$$$ -   "));
            list.Add(string.Format("m)$Sinking of Well - Pier 20 to 30 m$m$$$ -   "));
            list.Add(string.Format("n)$Sand Filling$Cum$$$ -   "));
            list.Add(string.Format("6C,40$Construction of Reinforced Earth Wall$$$$"));
            list.Add(string.Format("a)$PCC For RE Wall Foundation$Cum$210.7$ 7,262 $ 1,529,756 "));
            list.Add(string.Format("b)$\"Providing RCC Facia Panel for Reinforced earth retaining wall / Reinforced Earth Structures (RES) for approaches and ramps as per Techincal specification 3100 and additional specifications A-14\"$Sqm$14810.7$ 5,259 $ 77,889,419 "));
            //list.Add(string.Format("A-14\"$Sqm$14810.7$ 5,259 $ 77,889,419 "));
            list.Add(string.Format("c)$Filter media behind RE walls complete with all leads and lifts  as per drawing and Technical Specification Clause 2200$Cum$7735.8$ 2,970 $ 22,975,416 "));
            list.Add(string.Format("d)$Construction of embankment with Reinforced Earth$Cum$43021.0$ 303 $ 13,035,363 "));
            list.Add(string.Format("e)$Providing and constructing RCC crash barrier with friction slab M 40 grade over RE wall including cost of centering, shuttering and reinforcement as per approved Drawings and Technical Specifications $Rmt$2296$ 14,108 $ 32,393,962 "));
            list.Add(string.Format("6C,41$Cost for Launching of Steel Girders for ROB 185.592$LS$$$"));
            list.Add(string.Format("$Total Amount$$$$ 310,350,880 "));
            list.Add(string.Format(""));
            #endregion


            foreach (var item in list)
            {
                mlist = new MyList(MyList.RemoveAllSpaces(item), '$');

                //mlist.StringList.Insert(0, "");
                dgv.Rows.Add(mlist.StringList.ToArray());
            }
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;



        }
        private void btn_RateAnalysis_Process_Click(object sender, EventArgs e)
        {

            string file_path = Get_Project_Folder();


            file_path = Path.Combine(file_path, "Rate Analysis for " + cmb_boq_item.Text +".xlsx");

            string copy_path = file_path;



            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Cost Estimation\Rate Analysis.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
                //iapp.OpenExcelFile(copy_path, "2011ap");
            }



            Excel.Application myExcelApp;
            Excel.Workbooks myExcelWorkbooks;
            Excel.Workbook myExcelWorkbook;



            object misValue = System.Reflection.Missing.Value;

            myExcelApp = new Excel.ApplicationClass();
            myExcelApp.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks = myExcelApp.Workbooks;


            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook = myExcelWorkbooks.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.Sheets["material"];

            Excel.Worksheet ch1 = myExcelWorksheet;


            int ch = 27;

            char cr = 'B';

            ch = (int)cr;
            string rng = "";
            string val1 = "";
            string val2 = "";



            Excel.Range formatRange;
            formatRange = myExcelWorksheet.get_Range("b" + (dgv_box_gen_data.RowCount + 8), "L" + (dgv_box_gen_data.RowCount + 8));
            formatRange.Interior.Color = System.Drawing.
            ColorTranslator.ToOle(System.Drawing.Color.LightGreen);

            int rindx = 0;


            try
            {
                #region Update INput Data materials
                List<double> rates = new List<double>();

                DataGridView dgv = dgv_rate_materials;
                for (int k = 0; k < dgv.RowCount; k++)
                {
                    if (dgv[3, k].Value != null && dgv[3, k].Value != "")
                        rates.Add(MyList.StringToDouble(dgv[3, k].Value.ToString(), 0.0));
                }

                ch1.get_Range("Q3").Formula = txt_boq_skew_ang.Text;
                ch1.get_Range("E6").Formula = txt_boq_box_nos_strcs.Text;


                int indx = 0;
                for (int k = 3; k < 209; k++)
                {

                    ////if (
                    ////    k == 19 ||
                    ////    k >= 14 && k <= 16 ||
                    ////    k >= 29 && k <= 40 ||
                    ////    k >= 50 && k <= 64 ||
                    ////    k >= 67 && k <= 87 ||
                    ////    k >= 24 && k <= 27
                    ////    ) continue;
                    try
                    {

                        string v1 = ch1.get_Range("V" + k).Formula.ToString();
                        if (v1.StartsWith("=") == false) ch1.get_Range("V" + k).Formula = rates[indx].ToString();
                     
                        indx++;

                    }
                    catch (Exception exx) { }
                }
                #endregion Update INput Data materials


                ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets["INPUT"];


                #region Update INput Data Machine
                rates.Clear();

                dgv = dgv_rate_machine;
                for (int k = 0; k < dgv.RowCount; k++)
                {
                    if (dgv[3, k].Value != null && dgv[3, k].Value != "")
                        rates.Add(MyList.StringToDouble(dgv[3, k].Value.ToString(), 0.0));
                }

                //ch1.get_Range("Q3").Formula = txt_boq_skew_ang.Text;
                //ch1.get_Range("E6").Formula = txt_boq_nos_strcs.Text;


                indx = 0;
                for (int k = 14; k <= 108; k++)
                {

                    ////if (
                    ////    k == 19 ||
                    ////    k >= 14 && k <= 16 ||
                    ////    k >= 29 && k <= 40 ||
                    ////    k >= 50 && k <= 64 ||
                    ////    k >= 67 && k <= 87 ||
                    ////    k >= 24 && k <= 27
                    ////    ) continue;
                    if (
                        k == 76
                        ) continue;


                    try
                    {

                        //string v1 = ch1.get_Range("H" + k).Formula.ToString();

                        //if (!v1.StartsWith("=")) 
                            ch1.get_Range("H" + k).Formula = rates[indx].ToString();

                        indx++;

                    }
                    catch (Exception exx) { }
                }
                #endregion Update INput Data Machine


                //ch1 = (Excel.Worksheet)myExcelWorkbook.Sheets["INPUT"];


                #region Update INput Data Labour
                rates.Clear();

                dgv = dgv_rate_labour;
                for (int k = 0; k < dgv.RowCount; k++)
                {
                    if (dgv[3, k].Value != null && dgv[3, k].Value != "")
                        rates.Add(MyList.StringToDouble(dgv[3, k].Value.ToString(), 0.0));
                }

                //ch1.get_Range("Q3").Formula = txt_boq_skew_ang.Text;
                //ch1.get_Range("E6").Formula = txt_boq_nos_strcs.Text;


                indx = 0;
                for (int k = 112; k <= 130; k++)
                {
                    ////if (
                    ////    k == 19 ||
                    ////    k >= 14 && k <= 16 ||
                    ////    k >= 29 && k <= 40 ||
                    ////    k >= 50 && k <= 64 ||
                    ////    k >= 67 && k <= 87 ||
                    ////    k >= 24 && k <= 27
                    ////    ) continue;
                   

                    try
                    {

                        //string v1 = ch1.get_Range("H" + k).Formula.ToString();

                        //if (!v1.StartsWith("=")) 
                        ch1.get_Range("H" + k).Formula = rates[indx].ToString();

                        indx++;

                    }
                    catch (Exception exx) { }
                }
                #endregion Update INput Data Labour



            }
            catch (Exception exx) { }


            rindx = 0;


            myExcelWorkbook.Save();

            releaseObject(myExcelWorkbook);
            //releaseObject(myExcelApp);

            iApp.Excel_Open_Message();
        }

        private void tsmi_new_Click(object sender, EventArgs e)
        {
            ToolStripItem tsi = sender as ToolStripItem;

            string menu = tsi.Text.ToUpper();

            if (menu == "NEW")
            {
            }
            else if (menu == "OPEN")
            {
                using(OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Text Files (*.txt)|*.txt";
                    if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel) 
                    {
                        Save_FormRecord.Read_All_Data(this, ofd.FileName, true);
                        BOX_General_Data_Format();
                        Box_Modified_Cell();
                        MessageBox.Show("Data Opened Successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            else if (menu == "SAVE")
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Text Files (*.txt)|*.txt";
                    //sfd.InitialDirectory = 
                    if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                    {
                        Save_FormRecord.Write_All_Data(this, sfd.FileName, true);
                        MessageBox.Show("Data Saved Successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                    }
                }
            }
            else if (menu == "CLOSE")
            {
                this.Close();
            }
        }
        #endregion Rate Analysis


        #region Cost Estimation

        private void btn_cost_process_Click(object sender, EventArgs e)
        {

            string boq = txt_boq_file.Text;

            if (!File.Exists(boq))
            {
                boq = Path.Combine(Get_Project_Folder(), boq);

            }

            if (!File.Exists(boq))
            {
                MessageBox.Show("BoQ file not found.");
                return;
            }
            string rate = txt_rate_file.Text;

            if (!File.Exists(rate))
            {
                rate = Path.Combine(Get_Project_Folder(), rate);

            }

            if (!File.Exists(rate))
            {
                MessageBox.Show("Rate Analysis file not found.");
                return;
            }

            Create_Cost_Estimation(boq, rate);
        }
        public void Create_Cost_Estimation(string boq_file,string rate_file)
        {

            string file_path = Get_Project_Folder();

            file_path = Path.Combine(file_path, "Cost Estimation for " + cmb_boq_item.Text + ".xlsx");

            string copy_path = file_path;



            file_path = Path.Combine(Application.StartupPath, @"DESIGN\Cost Estimation\Cost Estimate.xlsx");

            if (File.Exists(file_path))
            {
                File.Copy(file_path, copy_path, true);
                //iapp.OpenExcelFile(copy_path, "2011ap");
            }

            #region Cost Excel File

            Excel.Application myExcelApp_cost;
            Excel.Workbooks myExcelWorkbooks_cost;
            Excel.Workbook myExcelWorkbook_cost;



            object misValue = System.Reflection.Missing.Value;

            myExcelApp_cost = new Excel.ApplicationClass();
            myExcelApp_cost.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks_cost = myExcelApp_cost.Workbooks;


            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook_cost = myExcelWorkbooks_cost.Open(copy_path, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook_cost.Sheets["6C FO & ROB, RUB"];

            Excel.Worksheet ch1 = myExcelWorksheet;



            #endregion Cost Excel File



            #region BOQ Excel File

            Excel.Application myExcelApp_boq;
            Excel.Workbooks myExcelWorkbooks_boq;
            Excel.Workbook myExcelWorkbook_boq;



            //object misValue = System.Reflection.Missing.Value;

            myExcelApp_boq = new Excel.ApplicationClass();
            myExcelApp_boq.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks_boq = myExcelApp_boq.Workbooks;


            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook_boq = myExcelWorkbooks_boq.Open(boq_file, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet_boq = (Excel.Worksheet)myExcelWorkbook_boq.Sheets["BOQ"];

            Excel.Worksheet ws_boq = myExcelWorksheet_boq;



            #endregion BOQ Excel File


            #region Rate Excel File

            Excel.Application myExcelApp_rate;
            Excel.Workbooks myExcelWorkbooks_rate;
            Excel.Workbook myExcelWorkbook_rate;



            //object misValue = System.Reflection.Missing.Value;

            myExcelApp_rate = new Excel.ApplicationClass();
            myExcelApp_rate.Visible = true;
            //myExcelApp.Visible = false;
            myExcelWorkbooks_rate = myExcelApp_rate.Workbooks;


            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            myExcelWorkbook_rate = myExcelWorkbooks_rate.Open(rate_file, 0, false, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //Excel.Worksheet myExcelWorksheet = (Excel.Worksheet)myExcelWorkbook.ActiveSheet;
            Excel.Worksheet myExcelWorksheet_rate = (Excel.Worksheet)myExcelWorkbook_rate.Sheets["SUMMARY"];

            Excel.Worksheet ws_rate = myExcelWorksheet_rate;



            #endregion BOQ Excel File











            int ch = 27;

            char cr = 'B';

            ch = (int)cr;
            string rng = "";
            string val1 = "";
            string val2 = "";



            //Excel.Range formatRange;
            //formatRange = myExcelWorksheet.get_Range("b" + (dgv_gen_data.RowCount + 8), "L" + (dgv_gen_data.RowCount + 8));
            //formatRange.Interior.Color = System.Drawing.
            //ColorTranslator.ToOle(System.Drawing.Color.LightGreen);

            int rindx = 0;



            #region List of Cells


            List<string> cells = new List<string>();

            #region 6C,01
            cells.Add(string.Format(""));
            cells.Add(string.Format("452-458"));
            cells.Add(string.Format("469-470"));
            cells.Add(string.Format("461-462"));
            #endregion 6C,01


            #region 6C,02
            cells.Add(string.Format("942-943"));
            #endregion 6C,02

            #region 6C,03
            cells.Add(string.Format("944"));
            #endregion 6C,03


            #region 6C,04
            cells.Add(string.Format("484"));
            #endregion 6C,04


            #region 6C,05

            cells.Add(string.Format(""));
            cells.Add(string.Format("485"));
            cells.Add(string.Format("494"));
            cells.Add(string.Format("500"));
            cells.Add(string.Format("503"));
            cells.Add(string.Format(""));
            cells.Add(string.Format("862"));
            #endregion 6C,05

            #region 6C,06
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));
            cells.Add(string.Format("921"));
            cells.Add(string.Format("931"));
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));
            cells.Add(string.Format("938"));
            #endregion 6C,06

            #region 6C,07
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));
            cells.Add(string.Format("921"));
            cells.Add(string.Format("931"));
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));
            cells.Add(string.Format("938"));
            #endregion 6C,07

            #region 6C,08
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));
            cells.Add(string.Format("921"));
            cells.Add(string.Format("931"));
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));
            cells.Add(string.Format("938"));
            #endregion 6C,08

            #region 6C,09
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));
            cells.Add(string.Format("921"));
            cells.Add(string.Format("931"));
            cells.Add(string.Format("934"));
            cells.Add(string.Format(""));
            cells.Add(string.Format("938"));
            #endregion 6C,09

            #region 6C,10
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));
            cells.Add(string.Format("921"));
            cells.Add(string.Format("931"));
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));
            cells.Add(string.Format("938"));
            #endregion 6C,10

            #region 6C,11
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));
            cells.Add(string.Format("921"));
            cells.Add(string.Format("931"));
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));
            cells.Add(string.Format("938"));
            #endregion 6C,11


            #region 6C,12
            cells.Add(string.Format(""));
            cells.Add(string.Format("1004-1010"));
            cells.Add(string.Format("1028"));
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));
            cells.Add(string.Format("1071"));
            cells.Add(string.Format("1076"));
            cells.Add(string.Format("1084"));
            cells.Add(string.Format("1085"));
            #endregion 6C,12


            #region 6C,13
            cells.Add(string.Format(""));
            cells.Add(string.Format("1004-1010"));
            cells.Add(string.Format("1027-1033"));
            cells.Add(string.Format("934"));
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));
            cells.Add(string.Format("1071"));
            cells.Add(string.Format("1076"));
            cells.Add(string.Format("1084"));
            cells.Add(string.Format("1085"));
            #endregion 6C,13


            #region 6C,14
            cells.Add(string.Format(""));
            cells.Add(string.Format("860"));
            cells.Add(string.Format("835"));
            cells.Add(string.Format("NF-60000"));
            #endregion 6C,14


            #region 6C,15
            cells.Add(string.Format(""));
            cells.Add(string.Format("NF-75000"));
            cells.Add(string.Format("NF-25000"));
            #endregion 6C,15

            #region 6C,16
            cells.Add(string.Format(""));
            cells.Add(string.Format("1087"));
            #endregion 6C,16

            #region 6C,17
            cells.Add(string.Format("1091"));
            cells.Add(string.Format("NF-300"));
            #endregion 6C,17

            #region 6C,18
            cells.Add(string.Format(""));
            cells.Add(string.Format("948"));
            cells.Add(string.Format("950"));
            cells.Add(string.Format(""));//Remove
            #endregion 6C,18

            #region 6C,19
            cells.Add(string.Format(""));
            cells.Add(string.Format("1102"));
            cells.Add(string.Format("1108"));
            #endregion 6C,19

            #region 6C,20
            cells.Add(string.Format(""));
            cells.Add(string.Format("1092"));
            cells.Add(string.Format("1093"));
            cells.Add(string.Format("1094"));
            #endregion 6C,20

            #region 6C,21
            cells.Add(string.Format("940"));
            #endregion 6C,21

            #region 6C,22
            cells.Add(string.Format("334"));
            #endregion 6C,22


            #region 6C,23
            cells.Add(string.Format("1089"));
            cells.Add(string.Format("1090"));
            #endregion 6C,23

            #region 6C,24
            cells.Add(string.Format("NF-250"));
            #endregion 6C,24

            #region 6C,25
            cells.Add(string.Format("199"));
            #endregion 6C,25

            #region 6C,26
            cells.Add(string.Format("NF-200"));
            #endregion 6C,26

            #region 6C,27
            cells.Add(string.Format(""));
            cells.Add(string.Format("1129"));
            cells.Add(string.Format(""));
            #endregion 6C,27


            #region 6C,28
            cells.Add(string.Format("1114"));
            #endregion 6C,28

            #region 6C,29
            cells.Add(string.Format(""));
            cells.Add(string.Format("474"));
            cells.Add(string.Format("484"));
            #endregion 6C,29

            #region 6C,30
            cells.Add(string.Format("1114"));
            #endregion 6C,30

            #region 6C,31
            cells.Add(string.Format("1120"));
            #endregion 6C,31

            #region 6C,32
            cells.Add(string.Format("1118"));
            #endregion 6C,32

            #region 6C,33
            cells.Add(string.Format("1130"));
            #endregion 6C,33

            #region 6C,34
            cells.Add(string.Format("1124"));
            #endregion 6C,34

            #region 6C,35
            cells.Add(string.Format(""));//Remove
            #endregion 6C,35

            #region 6C,36
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));//Remove
            cells.Add(string.Format(""));//Remove
            cells.Add(string.Format(""));//Remove
            #endregion 6C,36

            #region 6C,37
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));
            cells.Add(string.Format("921"));
            cells.Add(string.Format("931"));
            cells.Add(string.Format("938"));
            #endregion 6C,37

            #region 6C,38
            cells.Add(string.Format("NF-100000"));
            #endregion 6C,38

            #region 6C,39
            cells.Add(string.Format(""));
            cells.Add(string.Format(""));//a
            cells.Add(string.Format(""));//b
            cells.Add(string.Format(""));//c
            cells.Add(string.Format(""));//d
            cells.Add(string.Format(""));//e
            cells.Add(string.Format(""));//f
            cells.Add(string.Format(""));//g
            cells.Add(string.Format(""));//h
            cells.Add(string.Format(""));//i
            cells.Add(string.Format(""));//j
            cells.Add(string.Format(""));//k
            cells.Add(string.Format(""));//l
            cells.Add(string.Format(""));//m
            cells.Add(string.Format("831"));//n
            #endregion 6C,39

            #region 6C,40
            cells.Add(string.Format(""));
            cells.Add(string.Format("484"));
            cells.Add(string.Format("NF-5259"));
            cells.Add(string.Format("944"));
            cells.Add(string.Format("NF-303"));
            cells.Add(string.Format("344"));
            #endregion 6C,40



            #endregion List of Cells



            try
            {
                if (true)
                {
                    #region Update INput Data

                    //ws_boq.get_Range("Q3").Formula = txt_boq_skew_ang.Text;
                    //ws_boq.get_Range("E6").Formula = txt_boq_nos_strcs.Text;


                    int indx = 0;

                    MyList ml = null;


                    dgv_cost_estimate.Rows.Clear();


                    for (int k = 7; k < 159; k++)
                    {

                        ////if (
                        ////    k == 19 ||
                        ////    k >= 14 && k <= 16 ||
                        ////    k >= 29 && k <= 40 ||
                        ////    k >= 50 && k <= 64 ||
                        ////    k >= 67 && k <= 87 ||
                        ////    k >= 24 && k <= 27
                        ////    ) continue;
                        //if (k > 135)
                        //    k = k;

                        
                        try
                        {

                            string v1 = "";


                            try
                            {
                                v1 = ws_boq.get_Range("E" + k).Value.ToString();


                            }
                            catch (Exception ex0)
                            {
                                
                                throw;
                            }

                            //if (v1.StartsWith("=") == false) ch1.get_Range("V" + k).Formula = rates[indx].ToString();


                            string v2 = cells[indx].Replace("-"," TO ");
                            
                            List<int> cl = MyList.Get_Array_Intiger(v2);
                            if(v2.StartsWith("NF"))
                            {
                                //v2 =
                                if (cl.Count > 0) v2 = cl[0].ToString();

                            }
                            else 
                            {
                                if (cl.Count > 0)
                                {
                                    List<double> vls = new List<double>();
                                    foreach (var item in cl)
                                    {

                                        try
                                        {
                                            string v3 = ws_rate.get_Range("D" + item).Value.ToString();
                                            vls.Add(MyList.StringToDouble(v3, 0.0));

                                        }
                                        catch (Exception err) 
                                        {
                                            vls.Add(0.0);
                                        }
                                    }

                                    vls.Sort();
                                    v2 = vls[0].ToString();
                                }
                            }
                            ch1.get_Range("D" + k).Formula = v1;
                            ch1.get_Range("E" + k).Formula = v2;

                            //dgv_cost_estimate[0, indx].Value = ch1.get_Range("A" + k).Value.ToString();
                            //dgv_cost_estimate[1, indx].Value = ch1.get_Range("B" + k).Value.ToString();
                            //dgv_cost_estimate[2, indx].Value = ch1.get_Range("C" + k).Value.ToString();
                            //dgv_cost_estimate[3, indx].Value = ch1.get_Range("D" + k).Value.ToString();
                            //dgv_cost_estimate[4, indx].Value = ch1.get_Range("E" + k).Value.ToString();
                            //dgv_cost_estimate[5, indx].Value = ch1.get_Range("F" + k).Value.ToString();

                            string s1, s2, s3, s4, s5, s6;

                            s1 = "";
                            s2 = "";
                            s3 = "";
                            s4 = "";
                            s5 = "";
                            s6 = "";
                            try
                            {
                                s1 = ch1.get_Range("A" + k).Value.ToString();
                            }
                            catch (Exception e1) { }

                            try
                            {
                                s2 = ch1.get_Range("B" + k).Value.ToString();
                            }
                            catch (Exception e2) { }


                            try
                            {
                                s3 = ch1.get_Range("C" + k).Value.ToString();
                            }
                            catch (Exception e3) { }




                            try
                            {
                                s4 = ch1.get_Range("D" + k).Value.ToString();
                                s4 = double.Parse(s4).ToString("f3");
                                //if (s4 == "0.000") s6 = "";
                            }
                            catch (Exception e4) { }




                            try
                            {
                                s5 = ch1.get_Range("E" + k).Value.ToString();
                                s5 = double.Parse(s5).ToString("f3");
                                //if (s5 == "0.000") s6 = "";
                            }
                            catch (Exception e5) { }




                            try
                            {
                                s6 = ch1.get_Range("F" + k).Value.ToString();
                                s6 = double.Parse(s6).ToString("f3");

                                //if (s6 == "0.000") s6 = "";
                            }
                            catch (Exception e6) { }

                            if (s3 == "")
                            {
                                s4 = s5 = s6 = "";
                            }
                            try
                            {

                                dgv_cost_estimate.Rows.Add(
                                    s1,
                                    s2,
                                    s3,
                                    s4,
                                    s5,
                                    s6);
                            }
                            catch (Exception e7) { }







                            indx++;

                        }
                        catch (Exception exx) { }
                    }
                    #endregion Update INput Data
                }
            }
            catch (Exception exx) { }


            rindx = 0;















            myExcelWorkbook_cost.Save();

            releaseObject(myExcelWorkbook_cost);



            //myExcelWorkbook_boq.Save();

            releaseObject(myExcelWorkbook_boq);

            //myExcelWorkbook_rate.Save();

            releaseObject(myExcelWorkbook_rate);



            myExcelApp_boq.Quit();
            myExcelApp_rate.Quit();
            //Excel.Workbooks myExcelWorkbooks_boq;
            //Excel.Workbook myExcelWorkbook_boq;

            iApp.Excel_Open_Message();
        }





        #endregion Cost Estimation

        private void cmb_boq_item_SelectedIndexChanged(object sender, EventArgs e)
        {
            tc_boq.TabPages.Clear();

            if (cmb_boq_item.SelectedIndex == 0)
            {
                tc_boq.TabPages.Add(tab_boq_box);
            }
            else if (cmb_boq_item.SelectedIndex == 1)
            {
                tc_boq.TabPages.Add(tab_boq_girder);

            }
            else if (cmb_boq_item.SelectedIndex == 2)
            {
                tc_boq.TabPages.Add(tab_boq_rob);

            }

            string file_path = Get_Project_Folder();

            //file_path = Path.Combine(file_path, "BoQ_Bridges_Box_Type.xlsx");
            //file_path = Path.Combine(file_path, "BoQ for " + cmb_boq_item.Text + ".xlsx");
            //file_path = "BoQ for " + cmb_boq_item.Text + ".xlsx";
            txt_boq_file.Text = "BoQ for " + cmb_boq_item.Text + ".xlsx"; ;
            txt_rate_file.Text = "Rate Analysis for " + cmb_boq_item.Text + ".xlsx";  
            txt_cost_file.Text = "Cost Estimation for " + cmb_boq_item.Text + ".xlsx";
            
        }

        private void btn_cost_open_Click(object sender, EventArgs e)
        {
            iApp.Open_ASTRA_Worksheet_Dialog();
        }

        private void btn_drawings_users_Click(object sender, EventArgs e)
        {
            iApp.RunViewer();
        }





        #region Create Project / Open Project


        private void btn_new_design_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_browse_design.Name)
            {
                frm_Open_Project frm = new frm_Open_Project(this.Name, Path.Combine(iApp.LastDesignWorkingFolder, Title));
                if (frm.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    user_path = frm.Example_Path;
                    iApp.Read_Form_Record(this, frm.Example_Path);
                    txt_project_name.Text = Path.GetFileName(frm.Example_Path);
                    Open_Project();
                }
            }
            else if (btn.Name == btn_new_design.Name)
            {
                //IsCreate_Data = true;
                Create_Project();
            }
            Button_Enable_Disable();
        }

        public string user_path
        {
            get
            {
                return iApp.user_path;
            }
            set
            {
                iApp.user_path = value;
            }
        }
        public void Create_Project()
        {
            user_path = Path.Combine(iApp.LastDesignWorkingFolder, Title);
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            string fname = Path.Combine(user_path, Project_Name + ".apr");

            int ty = (int)Project_Type;
            File.WriteAllText(fname, ty.ToString());
            user_path = Path.Combine(user_path, Project_Name);

            if (Directory.Exists(user_path))
            {
                switch (MessageBox.Show(Project_Name + " is already exist. Do you want overwrite ?",
                    "ASTRA", MessageBoxButtons.YesNoCancel))
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        return;
                    case System.Windows.Forms.DialogResult.Yes:
                        //Delete Folders
                        Delete_Folder(user_path);
                        break;
                }
            }
            if (!Directory.Exists(user_path))
            {
                Directory.CreateDirectory(user_path);
            }
            Write_All_Data();

            MessageBox.Show(Project_Name + " is Created.", "ASTRA", MessageBoxButtons.OK);
        }

        private void Write_All_Data()
        {
            iApp.Save_Form_Record(this, user_path);
        }

        public void Set_Project_Name()
        {
            string dir = Path.Combine(iApp.LastDesignWorkingFolder, Title);

            string prj_name = "";
            string prj_dir = "";
            int c = 1;
            if (Directory.Exists(dir))
            {
                while (true)
                {
                    prj_name = "DESIGN JOB #" + c.ToString("00");
                    prj_dir = Path.Combine(dir, prj_name);

                    if (!Directory.Exists(prj_dir)) break;
                    c++;
                }
            }
            else
                prj_name = "DESIGN JOB #" + c.ToString("00");

            txt_project_name.Text = prj_name;

        }
        private void Open_Project()
        {
            //throw new NotImplementedException();
        }

        private void Button_Enable_Disable()
        {
            //throw new NotImplementedException();
        }


        public void All_Button_Enable(bool flag)
        {
            //btn_create_data.Enabled = flag;
            //btn_process_analysis.Enabled = flag;
            //btn_create_data.Enabled = flag;
            //btn_process_analysis
        }
        public string Project_Name
        {
            get
            {
                return txt_project_name.Text;
            }
            set
            {
                txt_project_name.Text = value;
            }
        }
        eASTRADesignType Project_Type
        {
            get
            {
                return eASTRADesignType.Cost_Estimation;
            }
        }

        public void Delete_Folder(string folder)
        {
            MyList.Delete_Folder(folder);
        }

        #endregion Chiranjit [2016 09 07]





        public string Title
        {
            get
            {
                return "COST ESTIMATION ANALYSIS";
            }
        }
    }
}
