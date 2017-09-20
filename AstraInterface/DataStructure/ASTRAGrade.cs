using System;
using System.Collections.Generic;
using System.Text;

namespace AstraInterface.DataStructure
{
    public class ASTRAGrade
    {
        public CONCRETE_GRADE ConcreteGrade { get; set; }
        public STEEL_GRADE SteelGrade { get; set; }
        public double Modular_Ratio { get; set; }

        public double Allowable_Stress_Concrete_kg_sq_cm
        {
            get
            {
                return double.Parse((((double)(int)ConcreteGrade) * 10.0 / 3.0).ToString("f2"));
            }
        }
        public double sigma_c_kg_sq_cm
        {
            get
            {
                return Allowable_Stress_Concrete_kg_sq_cm;
            }
        }
        public double sigma_c_N_sq_mm
        {
            get
            {
                return fcb;
            }
        }

        public double Permissible_Stress_Steel
        {
            get
            {
                switch (SteelGrade)
                {
                    case STEEL_GRADE.Fe240:
                        return 125.0;
                    case STEEL_GRADE.Fe415:
                        return 200.0;
                    case STEEL_GRADE.Fe500:
                        return 240.0;
                }
                return 240.0;
            }
        }
        public double sigma_sv_N_sq_mm
        {
            get
            {
                return Permissible_Stress_Steel;
            }
        }
        public double sigma_st_N_sq_mm
        {
            get
            {
                return Permissible_Stress_Steel;
            }
        }
        public double fst
        {
            get
            {
                return Permissible_Stress_Steel;
            }
        }
        public double fck
        {
            get
            {
                return (double)(int)ConcreteGrade;
            }
        }
        public double fcb
        {
            get
            {
                return double.Parse((fck / 3).ToString("f2"));
            }

        }
        public double fcc
        {
            get
            {
                return double.Parse((fck / 4).ToString("f2"));
            }

        }
        public double fy
        {
            get
            {
                return (double)(int)SteelGrade;
            }
        }
        public double m
        {
            get
            {
                return Modular_Ratio;
            }
        }
        public double n
        {
            get
            {
                return ((m * fcb) / (m * fcb + fst));
            }
        }
        public double Lever_Arm_Factor
        {
            get
            {
                return (1 - n / 3);
            }
        }
        public double j
        {
            get
            {
                return Lever_Arm_Factor;
            }
        }
        public double Q
        {
            get
            {
                return (n * j * fcb / 2);
            }
        }
        public double Moment_Factor
        {
            get
            {
                return Q;
            }

        }
        public ASTRAGrade()
        {
            ConcreteGrade = CONCRETE_GRADE.M25;
            SteelGrade = STEEL_GRADE.Fe415;
        }
        public ASTRAGrade(string conc_grade, string steel_grade)
        {
            ConcreteGrade = (CONCRETE_GRADE.M25);
            SteelGrade = (STEEL_GRADE.Fe415);

            try
            {

                ConcreteGrade = (CONCRETE_GRADE)(int.Parse(conc_grade));
                SteelGrade = (STEEL_GRADE)(int.Parse(steel_grade));
                //Modular_Ratio = 10;
            }
            catch (Exception ex) { }
        }
        public ASTRAGrade(double conc_grade, double steel_grade)
        {
            ConcreteGrade = (CONCRETE_GRADE)((int)(conc_grade));
            SteelGrade = (STEEL_GRADE)((int)(steel_grade));
        }
        public ASTRAGrade(CONCRETE_GRADE conc_grade, STEEL_GRADE steel_grade)
        {
            ConcreteGrade = conc_grade;
            SteelGrade = steel_grade;
        }

    }
}
