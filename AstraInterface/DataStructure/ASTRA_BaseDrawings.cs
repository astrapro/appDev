using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace AstraInterface.DataStructure
{
    public class ASTRA_BaseDrawings
    {
        public static string Drawing_Path { get { return Path.Combine(Application.StartupPath, "DRAWINGS"); } }

        public static bool Copy_Drawings(string copy_path, string src_path)
        {

            if (!Directory.Exists(src_path)) return false;
            if (!Directory.Exists(copy_path)) Directory.CreateDirectory(copy_path);


            string dest_fname = "";
            string src_fname = "";




            foreach (var item in Directory.GetFiles(src_path))
            {
                try
                {
                    src_fname = item;
                    dest_fname = Path.GetFileName(src_fname);
                    dest_fname = Path.Combine(copy_path, dest_fname);

                    //File.Copy(src_fname, dest_fname, true);
                    File.Copy(src_fname, dest_fname, false);
                }
                catch (Exception exx) { }
            }


            return true;
        }
        #region RCC T Girder Limit State Method
        public static string RCC_T_Girder_LS_GAD
        {
            get
            {
                string dpath = @"Base Drawings\RCC T Girder Bridge Base Drawings\01 GAD";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        public static string RCC_T_Girder_LS_LONG_GIRDER
        {
            get
            {
                string dpath = @"Base Drawings\RCC T Girder Bridge Base Drawings\02 Long Girder";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        public static string RCC_T_Girder_LS_CROSS_GIRDER
        {
            get
            {
                string dpath = @"Base Drawings\RCC T Girder Bridge Base Drawings\03 Cross Girder";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        public static string RCC_T_Girder_LS_DECK_SLAB
        {
            get
            {
                string dpath = @"Base Drawings\RCC T Girder Bridge Base Drawings\04 Deck Slab";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        public static string RCC_T_Girder_LS_COUNTERFORT_ABUTMENT
        {
            get
            {
                string dpath = @"Base Drawings\RCC T Girder Bridge Base Drawings\05 Counterfort Abutment";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        public static string RCC_T_GIRDER_LS_CANTILEVER_ABUTMENT
        {
            get
            {
                string dpath = @"Base Drawings\RCC T Girder Bridge Base Drawings\06 Cantilever Abutment";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        public static string RCC_T_GIRDER_LS_PIER
        {
            get
            {
                string dpath = @"Base Drawings\RCC T Girder Bridge Base Drawings\07 Pier";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        #endregion RCC T Girder Limit State Method

        #region RCC SLAB BRIDGE
        public static string RCC_MINOR_BRIDGE_DECKSLAB
        {
            get
            {
                string dpath = @"Base Drawings\RCC Slab Bridge Base Drawings\01 Deckslab";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        public static string RCC_MINOR_BRIDGE_ABUTMENT_CANTILEVER
        {
            get
            {
                string dpath = @"Base Drawings\RCC Slab Bridge Base Drawings\02 Cantilever Abutment";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        public static string RCC_MINOR_BRIDGE_ABUTMENT_COUNTERFORT
        {
            get
            {
                string dpath = @"Base Drawings\RCC Slab Bridge Base Drawings\03 Counterfort Abutment";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        public static string RCC_MINOR_BRIDGE_ABUTMENT_PIER
        {
            get
            {
                string dpath = @"Base Drawings\RCC Slab Bridge Base Drawings\04 Pier";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        #endregion RCC SLAB BRIDGE


        #region RCC Culvert Design
        public static string RCC_CULVERT_BOX_MULTICELL
        {
            get
            {
                string dpath = @"Base Drawings\RCC Culvert Base Drawings\Multicell Box Culvert";
                return Path.Combine(Drawing_Path, dpath);
            }
        }

        public static string RCC_CULVERT_BOX_MULTICELL_WITH_EARTH_CUSION
        {
            get
            {
                string dpath = @"Base Drawings\RCC Culvert Base Drawings\Box Structure With Earth Cushion";
                return Path.Combine(Drawing_Path, dpath);
            }
        }

        public static string RCC_CULVERT_BOX_MULTICELL_WITHOUT_EARTH_CUSION
        {
            get
            {
                string dpath = @"Base Drawings\RCC Culvert Base Drawings\Box Structure Without Earth Cushion";
                return Path.Combine(Drawing_Path, dpath);
            }
        }

        public static string RCC_CULVERT_PIPE
        {
            get
            {
                string dpath = @"Base Drawings\RCC Culvert Base Drawings\Pipe Culvert";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        #endregion RCC Culvert Design


        #region RCC Foundation Design
        public static string RCC_FOUNDATION_PILE
        {
            get
            {
                string dpath = @"Base Drawings\RCC Foundation Base Drawings\Pile Foundation Drawing";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        public static string RCC_FOUNDATION_WELL
        {
            get
            {
                string dpath = @"Base Drawings\RCC Foundation Base Drawings\Well Foundation Drawing";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        #endregion RCC Foundation Design
        public static string PSC_I_Girder_LS_GAD
        {
            get
            {
                string dpath = @"Base Drawings\PSC I Girder Bridge Base Drawings\PSC I Girder Bridge Drawings";
                return Path.Combine(Drawing_Path, dpath);
            }
        }

        public static string COMPOSITE_LS_STEEL_PLATE
        {
            get
            {
                string dpath = @"Base Drawings\Composite Bridge Base Drawings\Sleel Plate Girder Drawings";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        public static string COMPOSITE_LS_STEEL_BOX
        {
            get
            {
                string dpath = @"Base Drawings\Composite Bridge Base Drawings\Sleel Box Girder Drawings";
                return Path.Combine(Drawing_Path, dpath);
            }
        }


        public static string PSC_BOX_Girder_GAD
        {
            get
            {
                string dpath = @"Base Drawings\PSC Bridge Box Girder Base Drawings";
                return Path.Combine(Drawing_Path, dpath);
            }
        }


        public static string PSC_JETTY_GAD
        {
            get
            {
                string dpath = @"Base Drawings\Jetty PSC Girder Drawings";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        public static string PSC_JETTY_GAD_BS
        {
            get
            {
                string dpath = @"Base Drawings\Jetty PSC Girder Drawings BS";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        public static string RCC_JETTY_GAD
        {
            get
            {
                string dpath = @"Base Drawings\Jetty RCC Girder Drawings";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
        public static string RCC_JETTY_GAD_BS
        {
            get
            {
                string dpath = @"Base Drawings\Jetty RCC Girder Drawings BS";
                return Path.Combine(Drawing_Path, dpath);
            }
        }
    }

    public enum eBaseDrawings
    {
        #region RCC T Girder Limit State Method
        RCC_T_Girder_LS_GAD = 0,
        RCC_T_Girder_LS_LONG_GIRDER = 1,
        RCC_T_Girder_LS_CROSS_GIRDER = 2,
        RCC_T_Girder_LS_DECK_SLAB = 3,
        RCC_T_Girder_LS_COUNTERFORT_ABUTMENT = 4,
        RCC_T_GIRDER_LS_CANTILEVER_ABUTMENT = 5,
        RCC_T_GIRDER_LS_PIER = 6,

        #endregion RCC T Girder Limit State Method

        #region RCC SLAB BRIDGE
        RCC_MINOR_BRIDGE_DECKSLAB = 7,
        RCC_MINOR_BRIDGE_COUNTERFORT_ABUTMENT = 8,
        RCC_MINOR_BRIDGE_CANTILEVER_ABUTMENT = 9,
        RCC_MINOR_BRIDGE_PIER = 10,
        #endregion RCC SLAB BRIDGE

        #region RCC Culvert Base Drawings

        RCC_CULVERT_BOX_MULTICELL = 11,
        RCC_CULVERT_BOX_MULTICELL_WITH_EARTH_CUSION = 12,
        RCC_CULVERT_BOX_MULTICELL_WITHOUT_EARTH_CUSION = 13,
        RCC_CULVERT_PIPE = 14,

        #endregion RCC Culvert Base Drawings

        #region RCC Foundation Base Drawings

        RCC_FOUNDATION_PILE = 15,
        RCC_FOUNDATION_WELL = 16,

        #endregion RCC Foundation Base Drawings


        #region PSC I Girder Limit State Method
        PSC_I_Girder_LS_GAD = 17,


        #endregion PSC I Girder Limit State Method

        #region Composite Limit State Method

        COMPOSITE_LS_STEEL_PLATE = 18,
        COMPOSITE_LS_STEEL_BOX = 19,

        #endregion Composite Limit State Method

        #region PSC Box Girder Bridge

        PSC_BOX_Girder_GAD = 20,

        #endregion PSC Box Girder Bridge


        #region Jetty RCC & PSC

        Jetty_RCC = 21,
        Jetty_PSC = 22,

        #endregion Jetty RCC & PSC


    }
}
