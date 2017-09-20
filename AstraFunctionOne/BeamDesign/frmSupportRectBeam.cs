using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AstraInterface.DataStructure;
namespace AstraFunctionOne.BeamDesign
{
    public partial class frmRectangularBeam : Form
    {
        #region Variable Declaration
        int DDEBUG;                                                                //                Tag to Write Debug Information
        //
        int VER;
        //        CString                                VER_STR;
        ////
        //        CStdioFile                inptr;                                                                //                Input File
        //        CString                                name_inp;                                                //                Input File Name
        //        CStdioFile                optr;                                                                //                Output File
        //        CString                                name_opt;                                                //                Output File Name
        //        CStdioFile                dptr;                                                                //                Design File
        //        CString                                name_des;                                                //                Design File Name
        //        CStdioFile                lptr;                                                                //                Log File
        //        CString                                name_log;                                                //                Log File Name
        ////
        //        CString                                NameProject;
        //        CString                                NameBlock;
        //        CString                                NameDesigner;
        //
        string VER_STR;
        //
        StreamWriter inptr;                                                                //                Input File
        string name_inp;                                                //                Input File Name
        StreamWriter optr;                                                                //                Output File
        string name_opt;                                                //                Output File Name
        StreamWriter dptr;                                                                //                Design File
        string name_des;                                                //                Design File Name
        StreamWriter lptr;                                                                //                Log File
        string name_log;                                                //                Log File Name
        //
        string NameProject;
        string NameBlock;
        string NameDesigner;

        int TagDesignOpen;                                //                Tag for Design File Open
        int TagLogOpen;                                                //                Tag for Log File Open
        //
        //                Headings
        //
        //CStringArray* BeamHead;
        //CStringArray* ColHead;
        //CStringArray* LoadHead;
        //CStringArray* LoadType;


        List<string> BeamHead;
        List<string> ColHead;
        List<string> LoadHead;
        List<string> LoadType;



        //
        //                Beam Data
        //
        int BeamCurrent;
        int BeamFrame;                                                                //                Tag for Beam/Frame - 0/1;
        int MomentR;                                                                //                Tag for Distribute Moment 0=No,1=Y
        double MCLD, MCLL, MCRD, MCRL;                //                Cantilever Moments;
        double P1, P2;                                                                                //                Moment DIstribution Percentage
        //
        //                Frame Column Parameters
        //
        int ColCurrent;
        //
        //                Load Cases
        //
        int LodAlloc;
        int LoadCases;                                                //                Number of Load Cases
        int LoadCurrent;                                //                Current Line in Load Display
        //
        //                Safety Factors
        //
        double S1A;                                                                //                Max factor for Dead Load
        double S2A;                                                                //                Safety Factor for Imposed Load
        double S3A;                                                                //                Minimum Safety Factor for Dead Load
        double S1;                                                                                //                Safety Factor
        double S2;                                                                                //                Safety Factor
        double S3;                                                                                //                Safety Factor
        //
        int TagDlg;                                                                //                Tag to denote which page of Dialog is Active
        //
        //                As per the BASIC Code
        //

        double[,] ASD, AST, ASV, G, Q, AR2, AR3;
        double[,] A2, A3, A4, A5, AA5, M1, M2, M3, M4, V2, M8, M9, MM, MN, M6, M7, VS, V1;
        int[,] A1;

        double[] L, B, D, L2, B2, B3, D2, D3, H2, H3, K2h, K3h, K1h, F1h, F2h;
        double[] F3h, F4h, GL, QL, GR, QR, D4h, Eh, Fh, Rh, C2, C3;
        double[] Q2, Q3, RAD, RA, RBD, RB, RAL, RBL, V, M, M0, M5, X5, X0, FC, PAS, VCA;


        //double                ASD[16,13],AST[16,13],ASV[16,13],L[16],B[16],D[16],L2[16],B2[17],B3[17],D2[17],D3[17],H2[17],H3[17];
        //double                K2h[17],K3h[17],K1h[16],F1h[16],F2h[16],F3h[16],F4h[16],G[17,16],Q[17,16];
        //double                GL[17],QL[17],GR[17],QR[17],D4h[17],Eh[17],Fh[17],Rh[17],C2[17],C3[17];
        //double                Q2[17],Q3[17];
        //double                AR2[16,11],AR3[17,11],RAD[16],RA[16],RBD[16],RB[16],RAL[16],RBL[16];
        //double                A2[17,17],A3[17,17],A4[17,17],A5[17,17],AA5[17,17];
        //double                V[22],V1[17,16],M[22],M0[17],M1[17,16],M2[17,16],M3[17,16],M4[17,16],M5[16];
        //double                V2[17,16],M8[17,22],M9[17,22],X5[16],MM[17,16],MN[17,16],M6[17,22],M7[17,22],X0[17];
        //int                                A1[17,17];
        //
        //                Intermediate Variables
        //
        int RAC;
        int N, NN;                                                //                NOBEAM,
        int I, J, K, P;                                //                Loop Variables
        double F1;
        double F2;
        double F3;
        double F4;
        double U;                                                                //                E^10 - HIGH VALUE
        double ZERO;                                                //                Zero
        //
        //                Extra Variable
        //
        double MM1;
        double MU1;
        double ASD1;
        double AST1;
        double ASV1;
        double ASV11;
        double PPAS;
        double DPAS;
        double ZX1;
        int IV;
        int JV;
        int IXJ;
        //
        //                Material Properties
        //
        double FCU;
        double DFCU;
        double VC;
        double FY;
        double FYV;
        double CVB;
        double CVT;
        double CV1;
        double CV2;
        //
        double AB1;
        double AB2;
        double AB3;
        double AB4;
        //
        double TGQK;
        double VGK;
        double VQK;
        double VV;
        //
        //                Static Data
        //
        //double                FC[7],PAS[7],VS[6,5],VCA[6];



        //double[,] ASD, AST, ASV, G, Q;
        //double[] L, B, D, B2, B3, D2, D3, H2, H3;
        //int[] L2;
        //double[] GL, QL, GR, QR, C2, C3, Q2, Q3;
        //double[,] AR2, AR3;
        //double[] RAD, RA, RBD, RB, RAL, RBL;
        //double[,] A2, A3, A4, A5, AA5, V1, M3, M4, M1, M2, M6, M7;
        //int[,] A1;

        //double[] V, M, M5, X5;
        //double[,] V2, M8, M9;
        //double[] K2_, K3_, K1_, F1_, F2_, F3_, F4_, D4_, E_, F_, R_;
        //int N = 0, NN = 0; // Number of Span
        //double AB1, AB2, AB3, AB4;

        //double val1, val2;
        //double A, C,_B,_D, S1, S2, S3;


        //double U, P1, P2;
        //// Max Factor for Dead Load
        //double S1A = 18.0d;

        //// Safety Factor for Imposed Load
        //double S2A = 18.9d;

        //// Min Safety Factor for Dead Load
        //double S3A = 10.9;

        //double FCU, FY, FYV, CVB, CVT;


        //double MCLD = 1; // Left Cantilever DL M
        //double MCLL = 1; // Left Cantilever LL M
        //double MCRD = 1; // Right Cantilever DL M
        //double MCRL = 1; // Right Cantilever LL M
        //double F1, F2, F3, F4;

        #endregion

        void Init()
        {
            ASD = new double[16, 13];
            AST = new double[16, 13];
            ASV = new double[16, 13];
            L = new double[16];
            B = new double[16];
            D = new double[16];
            L2 = new double[16];
            B2 = new double[17];
            B3 = new double[17];
            D2 = new double[17];
            D3 = new double[17];
            H2 = new double[17];
            H3 = new double[17];
            K2h = new double[17];
            K3h = new double[17];
            K1h = new double[16];
            F1h = new double[16];
            F2h = new double[16];
            F3h = new double[16];
            F4h = new double[16];
            G = new double[17, 16];
            Q = new double[17, 16];
            GL = new double[17];
            QL = new double[17];
            GR = new double[17];
            QR = new double[17];
            D4h = new double[17];
            Eh = new double[17];
            Fh = new double[17];
            Rh = new double[17];
            C2 = new double[17];
            C3 = new double[17];
            Q2 = new double[17];
            Q3 = new double[17];
            AR2 = new double[16, 11];
            AR3 = new double[17, 11];
            RAD = new double[16];
            RA = new double[16];
            RBD = new double[16];
            RB = new double[16];
            RAL = new double[16];
            RBL = new double[16];
            A2 = new double[17, 17];
            A3 = new double[17, 17];
            A4 = new double[17, 17];
            A5 = new double[17, 17];
            AA5 = new double[17, 17];
            V = new double[22];
            V1 = new double[17, 16];
            M = new double[22];
            M0 = new double[17];
            M1 = new double[17, 16];
            M2 = new double[17, 16];
            M3 = new double[17, 16];
            M4 = new double[17, 16];
            M5 = new double[16];
            V2 = new double[17, 16];
            M8 = new double[17, 22];
            M9 = new double[17, 22];
            X5 = new double[16];
            MM = new double[17, 16];
            MN = new double[17, 16];
            M6 = new double[17, 22];
            M7 = new double[17, 22];
            X0 = new double[17];
            //double                FC[7],PAS[7],VS[6,5],VCA[6];
            FC = new double[7];
            PAS = new double[7];
            VS = new double[6, 5];
            VCA = new double[6];

            A1 = new int[17, 17];

            //                Debug
            DDEBUG = 1;
            //                Beam Array
            N = 0;
            //
            //                Material Properties defaults
            //
            FCU = 2.5E7;
            FY = 4.60E8;
            FYV = 2.50E8;
            CVB = 0.040;
            CVT = 0.040;
            S1A = 1.35;
            S2A = 1.5;
            S3A = 1.5;
            P1 = 20;
            P2 = 0;
            MomentR = 1;
            U = Math.Pow(10.0, 10.0);
            ZERO = 0.0000001;
            //
            //                Strings
            //
            name_inp = Path.Combine(system_path, "beamIn.dat");
            name_opt = Path.Combine(system_path, "beamOut.dat");
            //name_opt = "C:\\test\\beamOut.dat";
            //name_des = "C:\\test\\Design.txt";
            name_des = rep_file_name;
            name_log = Path.Combine(system_path, "Design.log");
            //name_log = "C:\\test\\Design.log";
            //
            NameProject = "New Project";
            NameBlock = "Test";
            NameDesigner = "TechSOFT";
            //
            VER = 0;
            VER_STR = "Beam and Sub Frame Design Program";
            BeamFrame = 0;                //                0=Beam, 1=Frame
            //
            //                List Box Headers                :                BEAM
            //
            N = 0;
            BeamHead = new List<string>();
            BeamHead.Add("BeamNo");
            BeamHead.Add("Length (m)");
            BeamHead.Add("Breadth (mm)");
            BeamHead.Add("Depth (mm)");
            BeamCurrent = 0;
            //
            //                List Box Headers                :                FRAME Columns
            //
            ColHead = new List<string>();
            ColHead.Add("Col No");
            ColHead.Add("Upper L(m)");
            ColHead.Add("Upper B(mm)");
            ColHead.Add("Upper D(mm)");
            ColHead.Add("Lower L(m)");
            ColHead.Add("Lower B(mm)");
            ColHead.Add("Lower D(mm)");
            ColCurrent = 0;
            //
            //                List Box Headers                :                Loads
            //
            LoadHead = new List<string>();
            LoadHead.Add("Beam No");
            LoadHead.Add("Load Case");
            LoadHead.Add("Loading Type");
            LoadHead.Add("Dead Load");
            LoadHead.Add("Imposed Load");
            LoadHead.Add("Dim 1");
            LoadHead.Add("Dim 2");
            LoadCurrent = 0;
            LoadCases = 1;
            //
            //                Combo Box Headers                :                Load Type
            //
            LoadType = new List<string>();
            LoadType.Add("UDL (kN/m)");
            LoadType.Add("Point Load (kN)");
            LoadType.Add("Partial UDL (kN/m)");
            //
            TagDesignOpen = 0;                                                //                Tag for Design File Open
            TagLogOpen = 0;                                                                //                Tag for Log File Open

            //
            //                Static Data
            //
            FC[1] = 20E6;
            FC[2] = 25E6;
            FC[3] = 30E6;
            FC[4] = 40E6;
            FC[5] = 50E6;
            FC[6] = 60E6;
            //
            PAS[1] = 0.25;
            PAS[2] = 0.50;
            PAS[3] = 1.00;
            PAS[4] = 2.00;
            PAS[5] = 3.00;
            PAS[6] = 3.50;
            //
            VS[1, 1] = .35E6;
            VS[1, 2] = .35E6;
            VS[1, 3] = .35E6;
            VS[1, 4] = .35E6;
            //
            VS[2, 1] = .45E6;
            VS[2, 2] = .50E6;
            VS[2, 3] = .55E6;
            VS[2, 4] = .55E6;
            //
            VS[3, 1] = .60E6;
            VS[3, 2] = .65E6;
            VS[3, 3] = .70E6;
            VS[3, 4] = .75E6;
            //
            VS[4, 1] = .80E6;
            VS[4, 2] = .85E6;
            VS[4, 3] = .90E6;
            VS[4, 4] = .95E6;
            //
            VS[5, 1] = .85E6;
            VS[5, 2] = .90E6;
            VS[5, 3] = .95E6;
            VS[5, 4] = 1.0E6;
            //
            //                Innitialise Load Cases Data
            //
            for (I = 1; I < 16; I++)
            {
                for (J = 1; J < 16; J++)
                {
                    A1[I, J] = 1;
                    A2[I, J] = 0;
                    A3[I, J] = 0;
                    A4[I, J] = 0;
                    A5[I, J] = 0;
                    AA5[I, J] = 0;
                }
            }
            //
        }

        public void InitializeData()
        {
            Init();

            try
            {


                FCU = MyList.StringToDouble(txt_FCU.Text, 0.0);
                FY = MyList.StringToDouble(txt_FY.Text, 0.0);
                FYV = MyList.StringToDouble(txt_FYV.Text, 0.0);
                CVB = MyList.StringToDouble(txt_BottomCover.Text, 0.0);
                CVB /= 1000;
                CVT = MyList.StringToDouble(txt_TopCover.Text, 0.0);
                CVT /= 1000;

                S1A = MyList.StringToDouble(txt_MaxFactorDeadLoad.Text, 0.0);
                S2A = MyList.StringToDouble(txt_SafetyFactor.Text, 0.0);
                S3A = MyList.StringToDouble(txt_MinSF.Text, 0.0);
                P1 = MyList.StringToDouble(txt_Percentage.Text, 0.0);


                MCLL = MyList.StringToDouble(txt_MCLL.Text, 0.0);
                MCLD = MyList.StringToDouble(txt_MCLD.Text, 0.0);
                MCRL = MyList.StringToDouble(txt_MCRL.Text, 0.0);
                MCRD = MyList.StringToDouble(txt_MCRD.Text, 0.0);

                int row = 0;
                for (row = 0, I = 1; row < dgv_BeamGeometry.Rows.Count - 1; row++, I++)
                {

                    //                                Length
                    L[I] = MyList.StringToDouble(dgv_BeamGeometry[1, row].Value.ToString(), 0.0);
                    //L[I] = atof(SS->GetAt(1));
                    //                                Width
                    B[I] = MyList.StringToDouble(dgv_BeamGeometry[2, row].Value.ToString(), 0.0);
                    B[I] /= 1000;
                    //B[I] = atof(SS->GetAt(2)) / 1000;
                    //                                Depth
                    D[I] = MyList.StringToDouble(dgv_BeamGeometry[3, row].Value.ToString(), 0.0);
                    D[I] /= 1000;
                    //D[I] = atof(SS->GetAt(3)) / 1000;
                }
                for (row = 0; row < dgv_ColumnGeometry.Rows.Count - 1; row++)
                {
                    I = MyList.StringToInt(dgv_ColumnGeometry[0, row].Value.ToString(), 0);

                    //Length                -                Upper
                    H2[I] = MyList.StringToDouble(dgv_ColumnGeometry[1, row].Value.ToString(), 0.0);
                    //Width                -                Upper
                    B2[I] = MyList.StringToDouble(dgv_ColumnGeometry[2, row].Value.ToString(), 0.0);
                    B2[I] /= 1000.0;
                    //B2[I] = atof(SS->GetAt(2)) / 1000;
                    //Depth                -                Upper
                    D2[I] = MyList.StringToDouble(dgv_ColumnGeometry[3, row].Value.ToString(), 0.0);
                    D2[I] /= 1000.0;
                    //D2[I] = atof(SS->GetAt(3)) / 1000;
                    //Length                -                Lower
                    H3[I] = MyList.StringToDouble(dgv_ColumnGeometry[4, row].Value.ToString(), 0.0);
                    //H3[I] = atof(SS->GetAt(4));
                    //Width                -                Lower
                    B3[I] = MyList.StringToDouble(dgv_ColumnGeometry[5, row].Value.ToString(), 0.0);
                    B3[I] /= 1000.0;
                    //B3[I] = atof(SS->GetAt(5)) / 1000;
                    //Depth                -                Lower
                    D3[I] = MyList.StringToDouble(dgv_ColumnGeometry[6, row].Value.ToString(), 0.0);
                    D3[I] /= 1000.0;
                    //D3[I] = atof(SS->GetAt(6)) / 1000;
                }
                for (row = 0; row < dgv_LoadCase.Rows.Count - 1; row++)
                {

                    //                                                Load Type                :                1=UDL, 2=Point, 3=Partial UDL

                    I = MyList.StringToInt(dgv_LoadCase[0, row].Value.ToString(), 0);
                    J = MyList.StringToInt(dgv_LoadCase[1, row].Value.ToString(), 0);

                    //A1[I][J]
                    A1[I, J] = MyList.StringToInt(dgv_LoadCase[2, row].Value.ToString(), 0);

                    if (dgv_LoadCase[2, row].Value.ToString() == "UDL (kN/m)")
                        A1[I, J] = 1;
                    else if (dgv_LoadCase[2, row].Value.ToString() == "Point Load (kN)")
                        A1[I, J] = 2;
                    else if (dgv_LoadCase[2, row].Value.ToString() == "Partial Load (kN/m)")
                        A1[I, J] = 3;


                    //A1[I,J] = atoi(SS->GetAt(2));
                    if (A1[I, J] < 1) A1[I, J] = 1;
                    if (A1[I, J] > 3) A1[I, J] = 3;
                    //                                                Dead Load
                    A2[I, J] = MyList.StringToDouble(dgv_LoadCase[3, row].Value.ToString(), 0.0);
                    //A2[I, J] = atoi(SS->GetAt(3));
                    //                                                Imposed Load
                    A3[I, J] = MyList.StringToDouble(dgv_LoadCase[4, row].Value.ToString(), 0.0);
                    //A3[I, J] = atoi(SS->GetAt(4));
                    //                                                Dimension 1
                    A4[I, J] = MyList.StringToDouble(dgv_LoadCase[5, row].Value.ToString(), 0.0);
                    //A4[I, J] = atoi(SS->GetAt(5));
                    //                                                Dimension 2
                    AA5[I, J] = MyList.StringToDouble(dgv_LoadCase[6, row].Value.ToString(), 0.0);
                    //AA5[I, J] = atoi(SS->GetAt(6));
                }


                //                                                                Number of Beams
                N = dgv_BeamGeometry.Rows.Count - 1;
                //                                                                BEAM or FRAME
                //if (NoF > 1)
                BeamFrame = ((rbtnBeam.Checked) ? 0 : 1);
                //else
                //    BeamFrame = 0;
                //                                                                LOAD Cases
                //if (NoF > 1)
                LoadCases = 3;
                //else
                //LoadCases = 1;
                //                                                                Tag - Distribute Moments
                //if (NoF > 3)
                //    MomentR = atoi(SS->GetAt(3));
                //else
                MomentR = 1;                //                Default = YES
                //

            }
            catch (Exception ex) { }

            //inptr = new StreamWriter(new FileStream(name_inp, FileMode.Create));                                                                //                Input File
            //optr = new StreamWriter(new FileStream(name_opt, FileMode.Create));                                                                //                Input File
            dptr = new StreamWriter(new FileStream(name_des, FileMode.Create));                                                                //                Input File
            lptr = new StreamWriter(new FileStream(name_log, FileMode.Create));                                                                //                Input File
        }

        void SetDefaultData()
        {
            dgv_BeamGeometry.Rows.Add(1, 5.0, 300.0, 500.0);
            dgv_BeamGeometry.Rows.Add(2, 6.0, 300.0, 500.0);
            dgv_BeamGeometry.Rows.Add(3, 4.0, 300.0, 500.0);

            dgv_ColumnGeometry.Rows.Add(1, 5.0, 300.0, 300.0, 5.0, 300.0, 300.0);
            dgv_ColumnGeometry.Rows.Add(2, 5.0, 300.0, 300.0, 5.0, 300.0, 300.0);
            dgv_ColumnGeometry.Rows.Add(3, 5.0, 300.0, 300.0, 5.0, 300.0, 300.0);
            dgv_ColumnGeometry.Rows.Add(4, 5.0, 300.0, 300.0, 5.0, 300.0, 300.0);

            dgv_LoadCase.Rows.Add(1, 1, "UDL (kN/m)", 1.0, 0, 0, 0);
            dgv_LoadCase.Rows.Add(1, 2, "UDL (kN/m)", 1.0, 2, 1, 0);
            dgv_LoadCase.Rows.Add(1, 3, "UDL (kN/m)", 1.0, 3, 1, 0);

            dgv_LoadCase.Rows.Add(2, 1, "UDL (kN/m)", 1.0, 0, 0, 0);
            dgv_LoadCase.Rows.Add(2, 2, "UDL (kN/m)", 1.0, 2, 1, 0);
            dgv_LoadCase.Rows.Add(2, 3, "UDL (kN/m)", 1.0, 3, 1, 0);

            dgv_LoadCase.Rows.Add(3, 1, "UDL (kN/m)", 1.0, 0, 0, 0);
            dgv_LoadCase.Rows.Add(3, 2, "UDL (kN/m)", 1.0, 2, 1, 0);
            dgv_LoadCase.Rows.Add(3, 3, "UDL (kN/m)", 1.0, 3, 1, 0);
        
        
        }
        void MemberStiffness()
        {
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : Member Stiffness");
                lptr.Write(XX);
            }
            //
            //                For Beams
            //
            for (J = 1; J < N + 1; J++)
            {
                K1h[J] = B[J] * (Math.Pow(D[J], 3)) / L[J];
                if (BeamFrame == 0)
                {
                    K2h[J] = 0;
                    K2h[J + 1] = 0;
                    K3h[J] = 0;
                    K3h[J + 1] = 0;
                }
            }
            //
            //                For Columns
            //
            if (BeamFrame != 0)
            {
                for (J = 1; J < N + 2; J++)
                {
                    //                                                Upper
                    if (Math.Abs(H2[J]) < ZERO)
                        K2h[J] = 0;
                    else
                        K2h[J] = B2[J] * (Math.Pow(D2[J], 3)) / H2[J];
                    //                                                Lower
                    if (Math.Abs(H3[J]) < ZERO)
                        K3h[J] = 0;
                    else
                        K3h[J] = B3[J] * (Math.Pow(D3[J], 3)) / H3[J];
                }
            }
            //
            //                Innitialise Load Case Data
            //                                Set Dim1 & Dim2 to ZERO
            //
            for (I = 1; I < N + 1; I++)
            {
                for (J = 1; J < LoadCases + 1; J++)
                {
                    switch (A1[I,J])
                    {
                        case 1:
                            {
                                A4[I,J] = 0;
                                AA5[I,J] = 0;
                                break;
                            }
                        case 2:
                            {
                                AA5[I,J] = 0;
                                break;
                            }
                    }
                }
            }
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Calculate Fixed End Moments
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S1340()
        {
            int ll;
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S1340()");
                lptr.Write(XX);
            }
            for (I = 1; I < N + 1; I++)
            {
                F1 = 0;
                F2 = 0;
                F3 = 0;
                F4 = 0;
                F1h[I] = 0;
                F2h[I] = 0;
                F3h[I] = 0;
                F4h[I] = 0;
                //
                //                Loop over Load Cases
                //
                for (J = 1; J < LoadCases + 1; J++)
                {
                    ll = A1[I,J];
                    switch (ll)
                    {
                        //                                                                U D L
                        case 1:
                            {
                                S1490();
                                break;
                            }
                        //                                                                Point Load
                        case 2:
                            {
                                S1590();
                                break;
                            }
                        //                                                                Partial UDL
                        case 3:
                            {
                                S1730();
                                break;
                            }
                    }
                }
                //
                F2h[I] = -F2h[I];
                F4h[I] = -F4h[I];
                if (DDEBUG == 1)
                {
                    XX = string.Format("I={0},F1h={1},F2h={2},F3h={3},F4h={4},", I, F1h[I], F2h[I], F3h[I], F4h[I]);
                    lptr.Write(XX);
                }
            }
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Forces                -                U D L
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S1490()
        {
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S1490()");
                lptr.Write(XX);
            }
            //                F1
            F1 = A2[I,J] * Math.Pow(L[I], 2) / 12;
            //                F2
            F2 = -F1;
            //                F1h,F2h
            F1h[I] += F1;
            F2h[I] += F2;
            //                F3
            F3 = A3[I,J] * Math.Pow(L[I], 2) / 12;
            //                F4
            F4 = -F3;
            //                F3h,F4h
            F3h[I] += F3;
            F4h[I] += F4;
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Forces                -                Point Load
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S1590()
        {
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S1590()");
                lptr.Write(XX);
            }
            //                F1
            F1 = A4[I,J] / L[I] * Math.Pow((1 - (A4[I,J] / L[I])), 2) * A2[I,J] * L[I];
            //                F2
            F2 = -Math.Pow(A4[I,J] / L[I], 2) * (1 - A4[I,J] / L[I]) * A2[I,J] * L[I];
            //                F1h,F2h
            F1h[I] += F1;
            F2h[I] += F2;
            //                F3
            F3 = A4[I,J] / L[I] * Math.Pow((1 - (A4[I,J] / L[I])), 2) * A3[I,J] * L[I];
            //                F4
            F4 = -Math.Pow(A4[I,J] / L[I], 2) * (1 - A4[I,J] / L[I]) * A3[I,J] * L[I];
            //                F3h,F4h
            F3h[I] += F3;
            F4h[I] += F4;
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Forces                -                Distributed UDL
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S1730()
        {
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S1730()");
                lptr.Write(XX);
            }
            //
            AB1 = (6 - 8 * (A4[I,J] + A5[I,J]) / L[I] + 3 * (Math.Pow((A4[I,J] + A5[I,J]) / L[I], 2))) * Math.Pow((A4[I,J] + A5[I,J]) / L[I], 2);
            AB2 = (6 - 8 * A4[I,J] + 3 * Math.Pow((A4[I,J] / L[I]), 2)) * Math.Pow(A4[I,J] / L[I], 2);
            AB3 = (Math.Pow((A4[I,J] + A5[I,J]) / L[I], 3)) * (4 - 3 * ((A4[I,J] + A5[I,J]) / L[I]));
            AB4 = (Math.Pow(A4[I,J] / L[I], 3)) * (4 - 3 * (A4[I,J] / L[I]));
            //
            F1 = A2[I,J] * Math.Pow(L[I], 2) / 12 * (AB1 - AB2);
            F2 = -A2[I,J] * Math.Pow(L[I], 2) / 12 * (AB3 - AB4);
            F1h[I] += F1;
            F2h[I] += F2;
            //
            F3 = A3[I,J] * Math.Pow(L[I], 2) / 12 * (AB1 - AB2);
            F4 = -A3[I,J] * Math.Pow(L[I], 2) / 12 * (AB3 - AB4);
            F3h[I] += F3;
            F4h[I] += F4;
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Partial Factors of Safety
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S1890()
        {
            double A, B, C, D;

            A = 0;
            B = 0;
            C = 0;
            D = 0;
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S1890()");
                lptr.Write(XX);
            }
            if (RAC == 0)
            {
                S1 = S1A;
                S2 = S2A;
                S3 = S3A;
            }
            //
            //                Pattern for Partial Safety Factor
            //
            for (I = 1; I < NN + 2; I++)
            {
                //                                Innitialise
                if (I == 1)
                {
                    A = S1;
                    B = S2;
                    C = S3;
                    D = 0;
                }
                if (I == 2)
                {
                    A = S3;
                    B = 0;
                    C = S1;
                    D = S2;
                }
                //                                Populate
                for (J = 1; J < N + 1; J++)
                {
                    if (J % 2 > 0)
                    {
                        G[I,J] = A;
                        Q[I,J] = B;
                    }
                    else
                    {
                        G[I,J] = C;
                        Q[I,J] = D;
                    }
                }
                //                                Skip 1st Two
                if (I > 2)
                {
                    if (I % 2 > 0)
                    {
                        A = S1;
                        B = S2;
                        C = S3;
                        D = 0;
                    }
                    else
                    {
                        A = S3;
                        B = 0;
                        C = S1;
                        D = S2;
                    }
                    //
                    for (J = 1; J < I - 1; J++)
                    {
                        if (J % 2 > 0)
                        {
                            G[I,J] = A;
                            Q[I,J] = B;
                        }
                        else
                        {
                            G[I,J] = C;
                            Q[I,J] = D;
                        }
                    }
                    //
                    for (J = 1; J < N + 1; J++)
                    {
                        if (J % 2 > 0)
                        {
                            G[I,J] = C;
                            Q[I,J] = D;
                        }
                        else
                        {
                            G[I,J] = A;
                            Q[I,J] = B;
                        }
                    }
                    //
                    G[I,I - 2] = S1;
                    Q[I,I - 2] = S2;
                    G[I,I - 1] = S1;
                    Q[I,I - 1] = S2;
                }
                //
                if (Math.Abs(G[I,1] - S1) < ZERO)
                    GL[I] = S3;
                else
                    GL[I] = S1;
                //
                if (Math.Abs(G[I,N] - S1) < ZERO)
                    GR[I] = S3;
                else
                    GR[I] = S1;
                //
                if (Math.Abs(Q[I,1] - S2) < ZERO)
                    QL[I] = 0;
                else
                    QL[I] = S2;
                //
                if (Math.Abs(Q[I,N] - S2) < ZERO)
                    QR[I] = 0;
                else
                    QR[I] = S2;
            }
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Slope Deflection Equation                -                LHS
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S2230()
        {
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S2230()");
                lptr.Write(XX);
            }
            for (J = 1; J < N + 2; J++)
            {
                //                                First
                if (J == 1)
                {
                    D4h[1] = 4 * (K1h[1] + K2h[1] + K3h[1]);
                    Eh[1] = 2 * K1h[1];
                    continue;
                }
                //                                Last
                if (J == N + 1)
                {
                    D4h[J] = 4 * (K1h[N] + K2h[J] + K3h[J]);
                    Eh[J] = 0;
                    continue;
                }
                //                                Others
                D4h[J] = 4 * (K1h[J - 1] + K1h[J] + K2h[J] + K3h[J]);
                Eh[J] = 2 * K1h[J];
            }
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Slope Deflection Equation                -                RHS
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S2340()
        {
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S2340(),NN={0}", NN);
                lptr.Write(XX);
            }
            for (I = 1; I < NN + 2; I++)
            {
                for (J = 1; J < N + 2; J++)
                {
                    //                                                First
                    if (J == 1)
                    {
                        Fh[1] = -F1h[1] * G[I,1] - F3h[1] * Q[I,1] + MCLD * GL[I] + MCLL * QL[I];
                        continue;
                    }
                    //                                                Last
                    if (J == N + 1)
                    {
                        Fh[J] = F2h[N] * G[I,N] + F4h[N] * Q[I,N] - MCRD * GR[I] - MCRL * QR[I];
                        continue;
                    }
                    //                                                Others
                    Fh[J] = F2h[J - 1] * G[I,J - 1] + F4h[J - 1] * Q[I,J - 1] - F1h[J] * G[I,J] - F3h[J] * Q[I,J];
                }
                //                                Solution of Equation
                S3930();
            }
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Moment and Shear for Overall Beam
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S2500()
        {
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S2500(),NN={0}", NN);
                lptr.Write(XX);
            }
            for (J = 1; J < N + 1; J++)
            {
                M5[J] = 0;
                for (K = 1; K < 12; K++)
                {
                    V1[J,K] = 0;
                }
                for (K = 1; K < 16; K++)
                {
                    M3[J,K] = U;
                    M4[J,K] = -U;
                }
                for (I = 1; I < NN + 2; I++)
                {
                    //                                                Span Shear & Moments
                    S4130();
                    //                                                Envelope Sorting
                    for (K = 1; K < 12; K++)
                    {
                        if (Math.Abs(V[K]) > Math.Abs(V1[J,K])) V1[J,K] = V[K];
                    }
                    RA[J] = V1[J,1];
                    RB[J] = V1[J,11];
                    for (K = 1; K < 12; K++)
                    {
                        if (M[K] < M3[J,K])
                        {
                            M3[J,K] = M[K];
                            MM[J,I] = M[K];
                        }
                        if (M[K] > M4[J,K])
                        {
                            M4[J,K] = M[K];
                            MN[J,I] = M[K];
                        }
                        if (M[K] > M5[J])
                        {
                            M5[J] = M[K];
                            X5[J] = L[J] * (K - 1) / 10;
                        }
                    }
                }
                if (DDEBUG == 1)
                {
                    XX = string.Format("SR : 6500 : X5[{0}]={1}", J, X5[J]);
                    lptr.Write(XX);
                }
            }
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Printout Shear and Moment Envelopes
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S2830()
        {
            int TTY;
            double U2, U3;
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S2830()");
                lptr.Write(XX);
            }
            TTY = 1;
            XX = string.Format("Shear and Moment Envelope", J);
            lptr.Write(XX);
            for (J = 1; J < N + 1; J++)
            {
                if (TTY == 0)
                {
                    //                                Span & Location
                    XX = string.Format("Span Number {0}Location", J);
                    lptr.Write(XX);
                    for (K = 1; K < 12; K++)
                    {
                        XX = string.Format("{0,10:f3}", L[J] * (K - 1) / 10);
                        lptr.Write(XX);
                    }
                    //                                M 1
                    XX = string.Format("M 1");
                    lptr.Write(XX);
                    for (K = 1; K < 12; K++)
                    {
                        XX = string.Format("{0,10:f3}", M3[J, K]);
                        lptr.Write(XX);
                    }
                    //                                M 2
                    XX = string.Format("M 2");
                    lptr.Write(XX);
                    for (K = 1; K < 12; K++)
                    {
                        XX = string.Format("{0,10:f3}", M4[J, K]);
                        lptr.Write(XX);
                    }
                    //                                SHR
                    XX = string.Format("SHR");
                    lptr.Write(XX);
                    for (K = 1; K < 12; K++)
                    {
                        XX = string.Format("{0,10:f3}", V1[J, K]);
                        lptr.Write(XX);
                    }
                    //                                Asc
                    XX = string.Format("Asc");
                    lptr.Write(XX);
                    for (K = 1; K < 12; K++)
                    {
                        XX = string.Format("{0,10:f3}", ASD[J, K] * 1000000);
                        lptr.Write(XX);
                    }
                    //                                Ast
                    XX = string.Format("Ast");
                    lptr.Write(XX);
                    for (K = 1; K < 12; K++)
                    {
                        XX = string.Format("{0,10:f3}", AST[J, K] * 1000000);
                        lptr.Write(XX);
                    }
                    //                                sv-
                    XX = string.Format("sv-");
                    lptr.Write(XX);
                    for (K = 1; K < 12; K++)
                    {
                        XX = string.Format("{0,10:f3}", ASV[J, K] * 1000000);
                        lptr.Write(XX);
                    }
                }
                else
                {
                    //                                Span & Location
                    XX = string.Format("Span Number   Location        M 1        M 2        SHR        Asc        Ast        sv-", J);
                    lptr.Write(XX);
                    for (K = 1; K < 12; K++)
                    {
                //XX.Format(" %10d %10.3f %10.3f %10.3f %10.3f %10.3f %10.3f %10.3f"
                        XX = string.Format(" {0,10} {1,10:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3}"
                            , J
                            , L[J] * (K - 1) / 10
                            , M3[J,K]
                            , M4[J,K]
                            , V1[J,K]
                            , ASD[J,K] * 1000000
                            , AST[J,K] * 1000000
                            , ASV[J,K] * 1000);
                        lptr.Write(XX);
                    }
                }
                //
                S4570();
            }
            //
            //                Moment Redistribution
            //
            if (MomentR == 0) goto L3630;
            P2 = (100 - P1) / 100;
            for (I = 3; I < N + 2; I++)
            {
                for (J = 1; J < N + 1; J++)
                {
                    if (J == 1)
                    {
                        M6[I,1] = M1[I,1];
                    }
                    else
                    {
                        if (M1[I,J] < -M2[I,J - 1])
                        {
                            M6[I,J] = M1[I,J] * P2;
                            M7[I,J - 1] = M2[I,J - 1] + M1[I,J] - M6[I,J];
                        }
                        if (J == N)
                        {
                            M7[I,N] = M2[I,N];
                            continue;
                        }
                    }
                    if (-M2[I,J] <= M1[I,J + 1])
                    {
                        M7[I,J] = M2[I,J] * P2;
                        M6[I,J + 1] = M1[I,J + 1] + M2[I,J] - M7[I,J];
                    }
                }
            }
            //
            for (I = 3; I < N + 2; I++)
            {
                for (J = 1; J < N + 1; J++)
                {
                    M1[I,J] = M6[I,J];
                    M2[I,J] = M7[I,J];
                }
            }
            //
            for (J = 1; J < N + 1; J++)
            {
                M0[J] = 0;
                for (K = 1; K < 12; K++)
                {
                    V2[J,K] = 0;
                }
                for (K = 1; K < 22; K++)
                {
                    M8[J,K] = U;
                    M9[J,K] = -U;
                }
                for (I = 1; I < N + 1; I++)
                {
                    //                                                Span Shear & Moments
                    S4130();
                    //                                                Redistributed Envelope Sorting
                    for (K = 1; K < 12; K++)
                    {
                        if (Math.Abs(V[K]) > Math.Abs(V2[J,K])) V2[J,K] = V[K];
                    }
                    for (K = 1; K < 22; K++)
                    {
                        if (M[K] < M8[J,K]) M8[J,K] = M[K];
                        if (M[K] > M9[J,K]) M9[J,K] = M[K];
                        if (M[K] > M0[J])
                        {
                            M0[J] = M[K];
                            X0[J] = L[J] * (K - 1) / 20;
                        }
                    }
                }
            }

            int _k = 0;

            for (J = 1; J < N + 1; J++)
            {
                for (K = 1; K < 22; K++)
                {
                    if (K > 16)
                    {
                        _k = K - 16;
                        if (M8[J, K] > 0) M8[J, K] = M3[J+1, _k];
                        if (M9[J, K] < 0) M9[J, K] = M4[J + 1, _k];
                        if (M8[J, K] > (M3[J + 1, _k] * 0.7)) M8[J, K] = M3[J + 1, _k] * 0.7;
                        if (M9[J, K] < (M4[J + 1, _k] * 0.7)) M9[J, K] = M4[J + 1, _k] * 0.7;
                    }
                    else if (K < 16)
                    {
                        _k = K;
                        if (M8[J, K] > 0) M8[J, K] = M3[J, _k];
                        if (M9[J, K] < 0) M9[J, K] = M4[J, _k];
                        if (M8[J, K] > M3[J, _k] * 0.7) M8[J, K] = M3[J, _k] * 0.7;
                        if (M9[J, K] < M4[J, _k] * 0.7) M9[J, K] = M4[J, _k] * 0.7;
                    }
                }
            }
            //
            //                Print Out Redistributed Shear & Moment Envelope
            //
            XX = string.Format("Redistributed Shear and Moment Envelope - kN-m");
            lptr.Write(XX);
            for (J = 1; J < N + 1; J++)
            {
                XX = string.Format("Span Number {0}", J);
                lptr.Write(XX);
                //                                Redistributed Shear Envelope
                XX = string.Format("Redistributed Shear Envelope at 10th Point");
                lptr.Write(XX);
                for (K = 1; K < 12; K++)
                {
                    XX = string.Format("{0}", V2[J, K]);
                    lptr.Write(XX);
                }
                //                                Redistributed Hogging Envelope
                XX = string.Format("Redistributed Hogging Envelope at 20th Point");
                lptr.Write(XX);
                for (K = 1; K < 22; K++)
                {
                    XX = string.Format("{0}", M8[J, K]);
                    lptr.Write(XX);
                }
                //                                Redistributed Sagging Envelope
                XX = string.Format("Redistributed Sagging Envelope at 20th Point");
                lptr.Write(XX);
                for (K = 1; K < 22; K++)
                {
                    XX = string.Format("{0}", M9[J, K]);
                    lptr.Write(XX);
                }
                //                                Maximum Sagging Moment
                XX = string.Format("Maximum Sagging Moment {0}", M0[J]);
                lptr.Write(XX);
                XX = string.Format("At a Distance          {0}", X0[J]);
                lptr.Write(XX);
            }
        //
        //                After Moment Redistribution !
        //
        L3630:
            //
            //                For FRAME only
            //
            if (BeamFrame == 1)
            {
                //                                Column Moments
                for (J = 1; J < N + 2; J++)
                {
                    C2[J] = 0;
                    C3[J] = 0;
                    if ((Math.Abs(K2h[J]) < ZERO) && (Math.Abs(K3h[J]) < ZERO))
                    {
                        Q2[J] = 0;
                        Q3[J] = 0;
                        continue;
                    }
                    Q2[J] = K2h[J] / (K2h[J] + K3h[J]);
                    Q3[J] = K3h[J] / (K2h[J] + K3h[J]);
                }
                for (I = 1; I < N + 2; I++)
                {
                    for (J = 1; J < N + 2; J++)
                    {
                        //                                                                First
                        if (J == 1)
                        {
                            MM1 = M1[I,1] - (MCLD * GL[I] + MCLL * QL[I]);
                        }
                        else
                        {
                            //                                                                Last
                            if (J == N + 1)
                            {
                                MM1 = M2[I,1] + (MCRD * GR[I] + MCRL * QR[I]);
                            }
                            else
                            //                                                                                Others
                            {
                                MM1 = M2[I,J - 1] + M1[I,J];
                            }
                        }
                        U2 = MM1 * Q2[J];
                        U3 = MM1 * Q3[J];
                        if (Math.Abs(U2) > Math.Abs(C2[J])) C2[J] = U2;
                        if (Math.Abs(U3) > Math.Abs(C3[J])) C3[J] = U3;
                    }
                }
                //
                //                Print out Column Moments
                //
                S8240();
                XX = string.Format("Column No     Moment in Upper Column    Moment in Lower Column");
                lptr.Write(XX);
                for (J = 1; J < N + 2; J++)
                {
                    XX = string.Format("{0} {1} {2}", J, C3[J], C2[J]);
                    lptr.Write(XX);
                }
            }
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Equation Solution and End Moment Calculation
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S3930()
        {
            double DDh, FFh;
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S3930(),I={0}", I);
                lptr.Write(XX);
            }
            DDh = D4h[1];
            FFh = Fh[1];
            for (K = 2; K < N + 2; K++)
            {
                FFh = Fh[K] - Eh[K - 1] * FFh / DDh;
                DDh = D4h[K] - Math.Pow(Eh[K - 1], 2) / DDh;
            }
            Rh[N + 1] = FFh / DDh;
            Rh[N] = (Fh[N + 1] - D4h[N + 1] * Rh[N + 1]) / Eh[N];
            for (K = N; K > 1; K--)
            {
                Rh[K - 1] = (Fh[K] - D4h[K] * Rh[K] - Eh[K] * Rh[K + 1]) / Eh[K - 1];
            }
            //                End Moments
            for (J = 1; J < N + 1; J++)
            {
                M1[I,J] = (4 * Rh[J] + 2 * Rh[J + 1]) * K1h[J] + F1h[J] * G[I,J] + F3h[J] * Q[I,J];
                M2[I,J] = (2 * Rh[J] + 4 * Rh[J + 1]) * K1h[J] - F2h[J] * G[I,J] - F4h[J] * Q[I,J];
                if (DDEBUG == 1)
                {
                    XX = string.Format("End Moments : M1[{0},{1}]={2},   M2[{3},{4}]={5}", I, J, M1[I, J], I, J, M2[I, J]);
                    lptr.Write(XX);
                }
            }
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Span & Shear Moments
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S4130()
        {
            double LL = 0.0, RR1 = 0.0, RR2 = 0.0, RR3 = 0.0, RR4 = 0.0, AA2 = 0.0, AA3 = 0.0, AA4 = 0.0, AAA5 = 0.0, DD1 = 0.0, WW = 0.0, WW1 = 0.0, ZZ = 0.0, ZZ1;

            int AA1;
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S4130(),I={0},J={1}", I, J);
                lptr.Write(XX);
            }
            LL = L[J];
            RR1 = (M1[I,J] + M2[I,J]) / LL;
            RR2 = -RR1;
            //
            for (K = 1; K < 16; K++)
            {
                V[K] = RR1;
            }
            //
            for (K = 1; K < 12; K++)
            {
                M[K] = -M1[I,J] + RR1 * L[J] / 10 * (K - 1);
            }
            //
            for (K = 1; K < LoadCases + 1; K++)
            {
                AA1 = A1[J,K];
                AA2 = A2[J,K];
                AA3 = A3[J,K];
                AA4 = A4[J,K];
                AAA5 = AA5[J,K];
                WW = AA2 * G[I,J] + AA3 * Q[I,J];
                switch (AA1)
                {
                    case 1:
                        {
                            DD1 = LL / 2;
                            RR3 = WW * DD1;
                            RR4 = WW * LL - RR3;
                            break;
                        }
                    case 2:
                        {
                            DD1 = LL - AA4;
                            RR3 = WW * DD1 / LL;
                            RR4 = WW - RR3;
                            break;
                        }
                    case 3:
                        {
                            DD1 = LL - ((AA4 + AAA5) / 2);
                            RR3 = WW * (AAA5 - AA4) * DD1 / LL;
                            RR4 = WW * (AAA5 - AA4) - RR3;
                            break;
                        }
                }
                RR1 += RR3;
                RR2 += RR4;
                //
                for (P = 1; P < 12; P++)
                {
                    ZZ = (P - 1) * LL / 10;
                    if (ZZ <= AA4)
                    {
                        V[P] += RR3;
                        continue;
                    }
                    switch (AA1)
                    {
                        case 1:
                            {
                                WW1 = WW * ZZ; ;
                                break;
                            }
                        case 2:
                            {
                                if (ZZ > AAA5)
                                {
                                    V[P] -= RR4;
                                    continue;
                                }
                                if ((Math.Abs(ZZ - AA4) < ZERO) && (Math.Abs(V[P] - WW) > Math.Abs(V[P])))
                                {
                                    V[P] -= WW;
                                }
                                break;
                            }
                        case 3:
                            {
                                ZZ1 = ZZ - AA4;
                                if (ZZ > AAA5)
                                {
                                    V[P] -= RR4;
                                    continue;
                                }
                                WW1 = WW * ZZ1;
                                break;
                            }
                    }
                    V[P] = V[P] + RR3 - WW1;
                }
                //
                for (P = 2; P < 12; P++)
                {
                    ZZ = (P - 1) * LL / 10;
                    switch (AA1)
                    {
                        case 1:
                            {
                                M[P] += (RR3 * ZZ - WW * ZZ * ZZ / 2);
                                break;
                            }
                        case 2:
                            {
                                if (ZZ <= AA4) M[P] += RR3 * ZZ;
                                if (ZZ > AA4) M[P] += (RR3 * ZZ - WW * (ZZ - AA4));
                                break;
                            }
                        case 3:
                            {
                                ZZ1 = ZZ - AA4;
                                WW1 = WW * ZZ1;
                                if (ZZ <= AA4)
                                {
                                    M[P] += RR3 * ZZ;
                                    continue;
                                }
                                if (ZZ >= AAA5)
                                {
                                    M[P] += RR4 * (LL - ZZ);
                                    continue;
                                }
                                M[P] += RR3 * ZZ - WW1 * ZZ1 / 2;
                                break;
                            }
                    }
                }
            }
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Holding Screen
        //                (Not required in this program)
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S4570()
        {
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Calculate Reinforcements
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S4620()
        {
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S4620()");
                lptr.Write(XX);
            }
            CV1 = D[J] - CVB;
            CV2 = CVT;
            MM1 = Math.Abs(MM1) * 1000;
            MU1 = MM1 / (FCU * B[J] * Math.Pow(CV1, 2));
            //
            if (MU1 > 0.156)
            {
                ASD1 = (MU1 - 0.156) * FCU * B[J] * Math.Pow(CV1, 2) / (0.87 * FY * (CV1 - CV2));
                AST1 = 0.156 * FCU * B[J] * Math.Pow(CV1, 2) / (0.87 * FY * 0.7769 * CV1) + ASD1;
                return;
            }
            //
            ZX1 = 0.5 + Math.Pow((0.25 - MU1 / 0.9), 0.5);
            if (ZX1 > 0.95)
            {
                AST1 = MM1 / (0.87 * FY * 0.95 * CV1);
            }
            else
            {
                AST1 = MM1 / (0.87 * FY * ZX1 * CV1);
            }
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Write Input Data
        //                                This routine was to Display the Results on SCREEN
        //                                In this version it is not required, as all data
        //                                is written to a file
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S4810()
        {
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S4810()");
                lptr.Write(XX);
            }
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Calculate Shear Reinforcement
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S5130()
        {
            string XX, YY;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S5130(),J={0},K={1}", J, K);
                lptr.Write(XX);
            }
            //
            //                Fix FCU to Shear Table
            //                                Values increased from 4(M40) to 6(M60)                :                2010-04-23
            //
            IV = 1;
            JV = 1;
            if (FCU <= FC[1]) FCU = FC[1];
            if (FCU >= FC[6]) FCU = FC[6];
            while (true)
            {
                if ((FCU >= FC[JV]) && (FCU < FC[JV + 1])) break;
                JV++;
            }
            //
            DFCU = (FCU - FC[JV]) / (FC[JV + 1] - FC[JV]);
            for (IV = 1; IV < 6; IV++)
            {
                VCA[IV] = (DFCU * (VS[IV,JV + 1] - (VS[IV,JV]) + VS[IV,JV]));
            }
            //
            PPAS = AST1 / (B[J] * D[J]) * 100;
            if (PPAS > 4)
            {
                XX = string.Format("*********   Calculated Steel - {0} > 4.0%% ****(J={1},K={2})", PPAS, J, K);
                lptr.Write(XX);
            }
            //
            //                Get Steel % INDEX
            //                                Values increased from 5 to 6                :                2010-04-24
            //
            IV = 1;
            if (PPAS > PAS[6])
            {
                PPAS = PAS[6];
            }
            else
            {
                if (PPAS < PAS[IV])
                {
                    PPAS = PAS[IV];
                }
                else
                {
                    while (true)
                    {
                        if ((PPAS > PAS[IV]) && (PPAS < PAS[IV + 1]))
                        {
                            break;
                        }
                        else
                        {
                            IV++;
                        }
                    }
                }
            }
            //
            DPAS = (PPAS - PAS[IV]) / (PAS[IV + 1] - PAS[IV]);
            VC = DPAS * (VCA[IV + 1] - VCA[IV]) + VCA[IV];
            VV = Math.Abs(VV);
            if (((VV * 1000 / B[J] / CV1) < VC / 2) || ((VV * 1000 / B[J] / CV1) >= VC / 2))
            {
                if (Math.Abs(FYV - 4.1E08) < ZERO)
                {
                    ASV11 = 0.13 / 100 * B[J] * 1000;
                    ASV1 = ASV11;
                }
                if (Math.Abs(FYV - 2.5E08) < ZERO)
                {
                    ASV11 = 0.24 / 100 * B[J] * 1000;
                    ASV1 = ASV11;
                }
            }
            if (VV * 1000 / B[J] / CV1 > VC) ASV11 = (B[J] * (VV * 1000 / B[J] / CV1 - VC)) / (0.87 * FYV) * 1000;
            if (ASV11 < ASV1) ASV1 = ASV11;
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Identify M & V for Reinforcement Design
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S6500()
        {
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S6500()");
                lptr.Write(XX);
            }
            //
            for (J = 1; J < N + 1; J++)
            {
                if (Math.Abs(M3[J,1]) > Math.Abs(M4[J,1]))
                {
                    MM1 = M3[J,1];
                }
                else
                {
                    MM1 = M4[J,1];
                }
                //
                ASD1 = 0;
                AST1 = 0;
                ASV1 = 0;
                IXJ = (int)(X5[J] * 10 / L[J]) + 1;
                //                                Calculation
                S4620();
                //
                ASD[J,1] = ASD1;
                AST[J,1] = AST1;
                MM1 = M5[J];
                ASD1 = 0;
                AST1 = 0;
                ASV1 = 0;
                if (IXJ != 1)
                {
                    //                                                Calculation
                    S4620();
                    ASD[J,IXJ] = ASD1;
                    AST[J,IXJ] = AST1;
                }
                //
                if (Math.Abs(M3[J,11]) > Math.Abs(M4[J,11]))
                {
                    MM1 = M3[J,11];
                }
                else
                {
                    MM1 = M4[J,11];
                }
                ASD1 = 0;
                AST1 = 0;
                ASV1 = 0;
                //                                Calculation
                S4620();
                ASD[J,11] = ASD1;
                AST[J,11] = AST1;
                //
                for (K = 1; K < 12; K++)
                {
                    if (Math.Abs(M3[J,K]) < Math.Abs(M4[J,K]))
                    {
                        MM1 = M4[J,K];
                    }
                    else
                    {
                        MM1 = M3[J,K];
                    }
                    VV = V1[J,K];
                    //                                                MM1                =                Math.Abs(M3[J,K]);
                    //                                                Calculation
                    S4620();
                    S5130();
                    ASV[J,K] = ASV1;
                }
            }
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Write Solution Output
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S6790()
        {
            string XX, YY;

            XX = YY = "";
            //time_t                ltime;
            //char                CC[257];
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : 6790()");
                lptr.Write(XX);
            }
            //
            //                Draw Line
            //
            //XX = string.Format("********************************************************************************************");
            //dptr.WriteLine(XX);
            //XX = string.Format("***** SOLUTION OUTPUT *****");
            //dptr.WriteLine(XX);
            //XX = string.Format("********************************************************************************************");
            //dptr.WriteLine(XX);
            //
            //                Write Analysis
            //
            //                Time
            //time(&ltime);
            //ctime_s(CC,256,&ltime);
            //YY.Format("%s",CC);
            //YY.TrimLeft();
            //YY.TrimRight();
            //if (BeamFrame == 0)
            //    XX = string.Format("Continuous BEAM Analysis.Designed on {0}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:s"));
            //else
            //    XX = string.Format("Sub FRAME Analysis.Designed on {0}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:s"));
            //dptr.WriteLine(XX);
            //
            //                Loading Diagram
            //
            XX = string.Format("|===============================================================|");
            dptr.WriteLine(XX);
            XX = string.Format("|                                                               |");
            dptr.WriteLine(XX);
            XX = string.Format("|     ***** Continuous Beam & Subframe Design *****             |");
            dptr.WriteLine(XX);
            //XX = string.Format("|     Load Type 1 = Uniformly Distributed Load (UDL)            |");
            //dptr.WriteLine(XX);
            //XX = string.Format("|     Load Type 2 = Point Load                                  |");
            //dptr.WriteLine(XX);
            //XX = string.Format("|     Load Type 3 = Partial UDL                                 |");
            //dptr.WriteLine(XX);
            //XX = string.Format("|     Loading Specs for Type 3 have been                        |");
            //dptr.WriteLine(XX);
            //XX = string.Format("|     Altered to Suit the Diagram Below                         |");
            //dptr.WriteLine(XX);
            XX = string.Format("|                                                               |");
            dptr.WriteLine(XX);
            XX = string.Format("|                                                               |");
            dptr.WriteLine(XX);
            XX = string.Format("|     <----------------------- Dim B ---------->|               |");
            dptr.WriteLine(XX);
            XX = string.Format("|     <------- Dim A ------>|                   |               |");
            dptr.WriteLine(XX);
            XX = string.Format("|                           |     w kN/m        |               |");
            dptr.WriteLine(XX);
            XX = string.Format("|                           |___________________|               |");
            dptr.WriteLine(XX);
            XX = string.Format("|     |                                             |           |");
            dptr.WriteLine(XX);
            XX = string.Format("|     |_____________________________________________|           |");
            dptr.WriteLine(XX);
            XX = string.Format("|     |                                             |           |");
            dptr.WriteLine(XX);
            XX = string.Format("|     |         . . .    Span   . . .               |           |");
            dptr.WriteLine(XX);
            XX = string.Format("|     |                                             |           |");
            dptr.WriteLine(XX);
            XX = string.Format("|                                                               |");
            dptr.WriteLine(XX);
            XX = string.Format("|===============================================================|");
            //XX = string.Format("|==============================================================================================================|");
            dptr.WriteLine(XX);
            //
            //                Input Data
            //
            dptr.WriteLine();
            dptr.WriteLine();
            dptr.WriteLine("------------------------------------------------------------");
            dptr.WriteLine("USER'S DATA");
            dptr.WriteLine("------------------------------------------------------------");
            dptr.WriteLine();
            //
            //                Project Data
            //
            //    XX = string.Format("Project Name                       : {0}",NameProject);
            //    dptr.WriteLine(XX);
            //    XX = string.Format("Block   Name                       : {0}",NameBlock);
            //    dptr.WriteLine(XX);
            //    XX = string.Format("Designed By                        : {0}", NameDesigner);
            //    dptr.WriteLine(XX);
            ////
            //                Geometry
            //
            //                Beams
            //
            dptr.WriteLine("------------------------------------------------------------");
            dptr.WriteLine("B E A M");
            dptr.WriteLine("------------------------------------------------------------");
            dptr.WriteLine();
            dptr.WriteLine();
            XX = string.Format("Left Cantilever DL Moment (kN-m)  : {0:E12}", MCLD);
            dptr.WriteLine(XX);
            XX = string.Format("Right Cantilever DL Moment (kN-m) : {0:E12}", MCRD);
            dptr.WriteLine(XX);
            XX = string.Format("Left Cantilever LL Moment (kN-m)  : {0:E12}", MCLL);
            dptr.WriteLine(XX);
            XX = string.Format("Right Cantilever LL Moment (kN-m) : {0:E12}", MCRL);
            dptr.WriteLine(XX);
            dptr.WriteLine();
            dptr.WriteLine();
            XX = string.Format("Number of Spans            : {0}", N);
            dptr.WriteLine(XX);
            XX = string.Format("      Span       Length      Breadth        Depth", MCRL);
            dptr.WriteLine(XX);
            XX = string.Format("        No          (m)         (mm)         (mm)", MCRL);
            dptr.WriteLine(XX);
            for (I = 1; I < N + 1; I++)
            {
                XX = string.Format("{0,10} {1,12:f3} {2,12:f3} {3,12:f3}", I, L[I], B[I] * 1000, D[I] * 1000);
                dptr.WriteLine(XX);
            }
            //
            //                Columns
            //
            dptr.WriteLine();
            if (BeamFrame == 1)
            {
                dptr.WriteLine();
                dptr.WriteLine("------------------------------------------------------------");
                dptr.WriteLine("C O L U M N S");
                dptr.WriteLine("------------------------------------------------------------");
                dptr.WriteLine();

                XX = string.Format("                 ******      UPPER       *******        ******       LOWER       ****** ");
                dptr.WriteLine(XX);
                XX = string.Format("    Column       Height            B           D        Height            B           D ");
                dptr.WriteLine(XX);
                XX = string.Format("   Line No          (m)         (mm)         (mm)          (m)         (mm)         (mm)");
                dptr.WriteLine(XX);
                for (I = 1; I < N + 2; I++)
                {
                    XX = string.Format("{0,10} {1,12:f3} {2,12:f3} {3,12:f3} {4,12:f3} {5,12:f3} {6,12:f3}", I, H3[I], B3[I] * 1000, D3[I] * 1000, H2[I], B2[I] * 1000, D2[I] * 1000);
                    //XX.Format("%10d%12.3f%12.3f%12.3f%12.3f%12.3f%12.3f", I, H3[I], B3[I] * 1000, D3[I] * 1000, H2[I], B2[I] * 1000, D2[I] * 1000);
                    dptr.WriteLine(XX);
                }
            }
            //
            //                Loads
            //
            dptr.WriteLine();
            dptr.WriteLine("------------------------------------------------------------");
            dptr.WriteLine("L O A D I N G");
            dptr.WriteLine("------------------------------------------------------------");
            dptr.WriteLine();
            dptr.WriteLine();
            XX = string.Format("      Span         Load              Dead Load              Live Load      Dim A      Dim B");
            dptr.WriteLine(XX);
            XX = string.Format("        No         Type                 (kN,m)                 (kN,m)         (m)       (m)");
            dptr.WriteLine(XX);
            for (I = 1; I < N + 1; I++)
            {
                for (J = 1; J < LoadCases + 1; J++)
                {
                    if (A1[I, J] == 1)
                    {
                        YY = string.Format("         UDL");
                    }
                    if (A1[I, J] == 2)
                    {
                        YY = string.Format("  Point Load");
                    }
                    if (A1[I, J] == 3) { YY = string.Format(" Partial UDL"); }

                    XX = string.Format("{0,10} {1} {2,22:e12} {3,22:e12} {4,10:f3} {5,10:f3}", I, YY, A2[I, J], A3[I, J], A4[I, J], AA5[I, J]);
                    //XX.Format("%10d%s%22.12E%22.12E%10.3f%10.3f", I, YY, A2[I][J], A3[I][J], A4[I][J], AA5[I][J]);
                    dptr.WriteLine(XX);
                }
            }
            dptr.WriteLine();
            dptr.WriteLine();
            XX = string.Format("Dead Load Factor                   : {0,20:e12} ", S1);
            dptr.WriteLine(XX);
            XX = string.Format("Minimum Dead Load Factor           : {0,20:e12} ", S3);
            dptr.WriteLine(XX);
            XX = string.Format("Live Load Factor                   : {0,20:e12} ", S2);
            dptr.WriteLine(XX);
            //
            //                Material Properties
            //
            dptr.WriteLine();
            dptr.WriteLine("------------------------------------------------------------");
            dptr.WriteLine("MATERIAL PROPERTIES");
            dptr.WriteLine("------------------------------------------------------------");
            dptr.WriteLine();
            XX = string.Format("Concrete FCU (MPa)                 : {0,20:e12} ", FCU / 1000000);
            dptr.WriteLine(XX);
            XX = string.Format("Steel    Fy  (MPa)                 : {0,20:e12} ", FY / 1000000);
            dptr.WriteLine(XX);
            XX = string.Format("Steel    Fyv (MPa)                 : {0,20:e12} ", FYV / 1000000);
            dptr.WriteLine(XX);
            XX = string.Format("Effective Bar Position BOTTOM (mm) : {0,10:f3} ", CVB * 1000);
            dptr.WriteLine(XX);
            XX = string.Format("Effective Bar Position TOP    (mm) : {0,10:f3} ", CVT * 1000);
            dptr.WriteLine(XX);
            //
            //                Design Data
            //
            dptr.WriteLine();
            dptr.WriteLine("------------------------------------------------------------");
            dptr.WriteLine("DESIGN CALCULATIONS");
            dptr.WriteLine("------------------------------------------------------------");
            dptr.WriteLine();
            //
            //                Moments etc
            //
            //                Span & Location
            dptr.WriteLine("------------------------------------------------------------");
            dptr.WriteLine("MOMENTS, SHEAR, Areas");
            dptr.WriteLine("------------------------------------------------------------");
            dptr.WriteLine();

            XX = string.Format("Span Number    Location       M 1        M 2        SHR       Asc        Ast       Asv-", J);
            dptr.WriteLine(XX);
            XX = string.Format("                                                             (mm2)      (mm2)     (mm2/m)", J);
            dptr.WriteLine(XX);
            for (J = 1; J < N + 1; J++)
            {
                for (K = 1; K < 12; K++)
                {
                    //XX.Format(" %10d %10.3f %10.3f %10.3f %10.3f %10.3f %10.3f %10.3f"
                    XX = string.Format(" {0,10} {1,10:f3} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,10:f3} {7,10:f3}"
                        , J
                        , L[J] * (K - 1) / 10
                        , M3[J, K]
                        , M4[J, K]
                        , V1[J, K]
                        , ASD[J, K] * 1000000
                        , AST[J, K] * 1000000
                        , ASV[J, K] * 1000);
                    dptr.WriteLine(XX);
                }
                XX = string.Format("");
                dptr.WriteLine(XX);
            }
            //
            //                Moments etc for FRAME
            //
            if (BeamFrame == 1)
            {
                dptr.WriteLine();
                dptr.WriteLine("------------------------------------------------------------");
                dptr.WriteLine("MOMENTS in COLUMNS");
                dptr.WriteLine("------------------------------------------------------------");
                dptr.WriteLine();

                XX = string.Format(" Column No    Moment in Upper Column    Moment in Lower Column");
                dptr.WriteLine(XX);
                for (J = 1; J < N + 2; J++)
                {
                    //XX.Format("%10d %25.12E %25.12E", J, C3[J], C2[J]);
                    XX = string.Format("{0,10} {1,25:E12} {2,25:E12}", J, C3[J], C2[J]);
                    dptr.WriteLine(XX);
                }
            }
            dptr.WriteLine();
            dptr.WriteLine("------------------------------------------------------------");
            dptr.WriteLine("Reactions on Supports ***");
            dptr.WriteLine("------------------------------------------------------------");
            dptr.WriteLine();
            XX = string.Format("Support      Dead Load      Live Load   ||  Span         Dead Load             Imposed Load");
            dptr.WriteLine(XX);
            XX = string.Format("                  (kN)           (kN)   ||  Span    Left End   Right End    Left End   Right End");
            dptr.WriteLine(XX);
            for (J = 1; J < N + 2; J++)
            {
                TGQK = S1A * (RAD[J] - RBD[J - 1]) + S2A * (RAL[J] - RBL[J - 1]);
                VGK = (RAD[J] - RBD[J - 1]);
                if (Math.Abs(S2A) < ZERO)
                {
                    VQK = 0;
                }
                else
                {
                    VQK = ((V1[J, 1] - V1[J - 1, 11]) - S1A * VGK) / S2A;
                }

                if (Math.Abs(S2A) < ZERO)
                {
                    if (J < N + 1)
                        //XX.Format("%5d %15.3f %15.3f %10d %11.3f %11.3f %11.3f %11.3f ", J, VGK, VQK, J, RAD[J], -RBD[J], (V1[J][1] - S1A * RAD[J]) / 1, -(V1[J][11] - S1A * RBD[J]) / 1);
                        XX = string.Format("{0,5} {1,15:f3} {2,15:f3} {3,10:f3} {4,11:f3} {5,11:f3} {6,11:f3} {7,11:f3} ", J, VGK, VQK, J, RAD[J], -RBD[J], (V1[J, 1] - S1A * RAD[J]) / 1, -(V1[J, 11] - S1A * RBD[J]) / 1);
                    else
                        //XX.Format("%5d %15.3f %15.3f", J, VGK, VQK);
                        XX = string.Format("{0,5} {1,15:f3} {2,15:f3}", J, VGK, VQK);
                    dptr.WriteLine(XX);
                }
                else
                {
                    if (J < N + 1)
                        //XX.Format("%5d %15.3f %15.3f %10d %11.3f %11.3f %11.3f %11.3f ", J, VGK, VQK, J, RAD[J], -RBD[J], (V1[J][1] - S1A * RAD[J]) / 1, -(V1[J][11] - S1A * RBD[J]) / 1);
                        XX = string.Format("{0,5} {1,15:f3} {2,15:f3} {3,10:f3} {4,11:f3} {5,11:f3} {6,11:f3} {7,11:f3} ", J, VGK, VQK, J, RAD[J], -RBD[J], (V1[J, 1] - S1A * RAD[J]) / 1, -(V1[J, 11] - S1A * RBD[J]) / 1);
                    else
                        //XX.Format("%5d %15.3f %15.3f", J, VGK, VQK);
                        XX = string.Format("{0,5} {1,15:f3} {2,15:f3}", J, VGK, VQK);
                    dptr.WriteLine(XX);
                }
            }
            //
            #region End of Report
            dptr.WriteLine();
            dptr.WriteLine("---------------------------------------------------------------------------");
            dptr.WriteLine("---------------------       END OF REPORT        --------------------------");
            dptr.WriteLine("---------------------------------------------------------------------------");
            #endregion

            XX = string.Format("**  NOTE : Values are Service loads and excludes cantilever loads");
            dptr.WriteLine(XX);
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Determine Support Reactions for DEAD LOAD ONLY
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S7940()
        {
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S7940()");
                lptr.Write(XX);
            }
            RAC = 1;
            NN = 0;
            for (I = 1; I < N + 1; I++)
            {
                for (J = 1; J < LoadCases + 1; J++)
                {
                    AR2[I,J] = A2[I,J];
                    AR3[I,J] = A3[I,J];
                }
            }
            S1 = S3A;
            S2 = 0;
            S3 = S3A;
            //
            //                Do Dead Load First
            //
            S8160();
            //
            for (I = 1; I < N + 1; I++)
            {
                RAD[I] = RA[I];
                RBD[I] = RB[I];
            }
            //
            for (I = 1; I < N + 1; I++)
            {
                for (J = 1; J < LoadCases + 1; J++)
                {
                    A2[I,J] = AR2[I,J];
                    A3[I,J] = AR3[I,J];
                }
            }
            //
            RAC = 0;
            NN = N;
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Call Different Routines
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S8160()
        {
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S8160()");
                lptr.Write(XX);
            }
            S1340();                                //                1340
            S1890();                                //                1890
            S2230();                                //                2230
            S2340();                                //                2340
            S2500();                                //                2500
            //
            return;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        //
        //                Display Reaction Support
        //
        ///////////////////////////////////////////////////////////////////////////////////////////
        void S8240()
        {
            string XX;
            if (DDEBUG == 1)
            {
                XX = string.Format("SR : S8240()");
                lptr.Write(XX);
            }
            //
            //                Bypass this code, as it is being written in the File
            //                Originally this was displayed on the screen for user Interactiom
            //                                This is no longer needed !
            //
            //
            return;
        }
    }
}
