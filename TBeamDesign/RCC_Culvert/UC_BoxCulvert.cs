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


namespace BridgeAnalysisDesign.RCC_Culvert
{
    public partial class UC_BoxCulvert : UserControl
    {
         IApplication iApp;

        public UC_BoxCulvert()
        {
            InitializeComponent();
            Load_Default_Data();
        }
        public void SetIApplication(IApplication app)
        {
            iApp = app;
        }
        void Load_Default_Data()
        {
            //DataGridView dgv = dgv_input_data;

            List<string> list = new List<string>();
            MyList mlist = null;


            #region Base Pressure
            list.Add(string.Format("Number of cell in culvert$1$"));
            list.Add(string.Format("Clear Span$8.8$m"));
            list.Add(string.Format("Clear Height$7.6$m"));
            list.Add(string.Format("Overall width$12$m"));
            list.Add(string.Format("Carriageway width$11$m"));
            list.Add(string.Format("Crash barrier width$0.5$m"));
            list.Add(string.Format("Footpath width/ Safety kerb$0$m"));
            list.Add(string.Format("Minimum thickness of wearing coat$0.075$m"));
            list.Add(string.Format("Thickness of soil fill over Bottom slab$0$m"));
            list.Add(string.Format("lean concrete thickness over deck$0.000$m"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Width of Haunch$0.3$m"));
            list.Add(string.Format("Depth of Haunch$0.3$m"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Top slab thickness$0.70$m"));
            list.Add(string.Format("Bottom slab thickness$0.70$m"));
            list.Add(string.Format("Side wall thickness $0.65$m"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Safe Bearing Capacity$150$kN/m²"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Density of concrete$25$kN/m³"));
            list.Add(string.Format("unit weight of soil$18$kN/m³"));
            list.Add(string.Format("unit weight of submerged soil$10$kN/m³"));
            list.Add(string.Format("unit weight of wearing coat$22$kN/m³"));
            list.Add(string.Format("Grade of concrete$35$N/mm2"));
            list.Add(string.Format("Grade of reinforcement$500$N/mm2"));
            list.Add(string.Format("Length of the box considered for design$1$m"));
            list.Add(string.Format("Modulus of elasticity of concret$2.96E+04$N/m2"));
            list.Add(string.Format("Modulus of elasticity of Steel$2.00E+05$N/m2"));
            list.Add(string.Format("Permissible stresses$$"));
            list.Add(string.Format("Permissible Compressive stress in Concrete$11.5$Mpa"));
            list.Add(string.Format("Permissible Tensile stress in Steel$240$Mpa"));
            list.Add(string.Format("Modular ratio, m$6.76$"));
            list.Add(string.Format("factor, k$0.245$"));
            list.Add(string.Format("Lever arm factor, j$0.92$"));
            list.Add(string.Format("Moment resistance factor, R$1.29$Mpa"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Grade of shear reinforcement$500$"));
            list.Add(string.Format("$$"));
            list.Add(string.Format("Design Levels$$"));
            list.Add(string.Format("Formation road level$812.043$m"));
            list.Add(string.Format("Highest Flood level$810.56$m"));
            list.Add(string.Format("Lowest Water level$803$m"));
            list.Add(string.Format("Maximum Scour level$803$m"));
            list.Add(string.Format("Foundation level$802.899$m"));

            //dgv = dgv_input_data;



            MyList.Fill_List_to_Grid(dgv_design_data, list, '$');
            //dgv_base_pressure.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


            #endregion Base Pressure



            //Modified_Cell(dgv_input_data);

            //if (dgv[2, i].Value == "") dgv[2, i].Value = "";

        }

    }
}
