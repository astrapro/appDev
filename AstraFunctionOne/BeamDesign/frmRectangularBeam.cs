using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using AstraInterface.Interface;
using AstraInterface.DataStructure;

namespace AstraFunctionOne.BeamDesign
{
    public partial class frmRectangularBeam : Form
    {
        #region File Manage Variable
        string rep_file_name = "";
        string user_input_file = "";
        string user_path = "";
        string file_path = "";
        string system_path = "";

        bool is_process = false;
        IApplication iApp = null;
        #endregion

//                int                                                DDEBUG;                                                                //                Tag to Write Debug Information
////
//                int VER;
//                //        CString                                VER_STR;
//                ////
//                //        CStdioFile                inptr;                                                                //                Input File
//                //        CString                                name_inp;                                                //                Input File Name
//                //        CStdioFile                optr;                                                                //                Output File
//                //        CString                                name_opt;                                                //                Output File Name
//                //        CStdioFile                dptr;                                                                //                Design File
//                //        CString                                name_des;                                                //                Design File Name
//                //        CStdioFile                lptr;                                                                //                Log File
//                //        CString                                name_log;                                                //                Log File Name
//                ////
//                //        CString                                NameProject;
//                //        CString                                NameBlock;
//                //        CString                                NameDesigner;
//                //
//                string VER_STR;
//                //
//                StreamWriter inptr;                                                                //                Input File
//                string name_inp;                                                //                Input File Name
//                StreamWriter optr;                                                                //                Output File
//                string name_opt;                                                //                Output File Name
//                StreamWriter dptr;                                                                //                Design File
//                string name_des;                                                //                Design File Name
//                StreamWriter lptr;                                                                //                Log File
//                string name_log;                                                //                Log File Name
//                //
//                string NameProject;
//                string NameBlock;
//                string NameDesigner;
                
//        int TagDesignOpen;                                //                Tag for Design File Open
//        int                                                TagLogOpen;                                                //                Tag for Log File Open
////
////                Headings
//        //
//        //CStringArray* BeamHead;
//        //CStringArray* ColHead;
//        //CStringArray* LoadHead;
//        //CStringArray* LoadType;


//        List<string> BeamHead;
//        List<string> ColHead;
//        List<string> LoadHead;
//        List<string> LoadType;



////
////                Beam Data
////
//    int                                                                BeamCurrent;
//    int                                BeamFrame;                                                                //                Tag for Beam/Frame - 0/1;
//    int                                MomentR;                                                                //                Tag for Distribute Moment 0=No,1=Y
//    double                MCLD,MCLL,MCRD,MCRL;                //                Cantilever Moments;
//    double                P1,P2;                                                                                //                Moment DIstribution Percentage
////
////                Frame Column Parameters
////
//    int                                ColCurrent;
////
////                Load Cases
////
//    int                                LodAlloc;
//    int                                LoadCases;                                                //                Number of Load Cases
//    int                                LoadCurrent;                                //                Current Line in Load Display
////
////                Safety Factors
////
//    double                S1A;                                                                //                Max factor for Dead Load
//    double                S2A;                                                                //                Safety Factor for Imposed Load
//    double                S3A;                                                                //                Minimum Safety Factor for Dead Load
//    double                S1;                                                                                //                Safety Factor
//    double                S2;                                                                                //                Safety Factor
//    double                S3;                                                                                //                Safety Factor
////
//    int                                TagDlg;                                                                //                Tag to denote which page of Dialog is Active
////
////                As per the BASIC Code
////

//    double[,] ASD, AST, ASV, G, Q, AR2, AR3;
//    double[,] A2, A3, A4, A5, AA5, M1, M2, M3, M4, V2, M8, M9, MM, MN, M6, M7, VS, V1;
//    int[,] A1;

//    double[] L, B, D, L2, B2, B3, D2, D3, H2, H3, K2h, K3h, K1h, F1h, F2h;
//    double[] F3h, F4h, GL, QL, GR, QR, D4h, Eh, Fh, Rh, C2, C3;
//    double[] Q2, Q3, RAD, RA, RBD, RB, RAL, RBL, V, M, M0, M5, X5, X0, FC, PAS, VCA;


//    //double                ASD[16,13],AST[16,13],ASV[16,13],L[16],B[16],D[16],L2[16],B2[17],B3[17],D2[17],D3[17],H2[17],H3[17];
//    //double                K2h[17],K3h[17],K1h[16],F1h[16],F2h[16],F3h[16],F4h[16],G[17,16],Q[17,16];
//    //double                GL[17],QL[17],GR[17],QR[17],D4h[17],Eh[17],Fh[17],Rh[17],C2[17],C3[17];
//    //double                Q2[17],Q3[17];
//    //double                AR2[16,11],AR3[17,11],RAD[16],RA[16],RBD[16],RB[16],RAL[16],RBL[16];
//    //double                A2[17,17],A3[17,17],A4[17,17],A5[17,17],AA5[17,17];
//    //double                V[22],V1[17,16],M[22],M0[17],M1[17,16],M2[17,16],M3[17,16],M4[17,16],M5[16];
//    //double                V2[17,16],M8[17,22],M9[17,22],X5[16],MM[17,16],MN[17,16],M6[17,22],M7[17,22],X0[17];
//    //int                                A1[17,17];
////
////                Intermediate Variables
////
//    int                                RAC;
//    int                                N,NN;                                                //                NOBEAM,
//    int                                I,J,K,P;                                //                Loop Variables
//    double                F1;
//    double                F2;
//    double                F3;
//    double                F4;
//    double                U;                                                                //                E^10 - HIGH VALUE
//    double                ZERO;                                                //                Zero
////
////                Extra Variable
////
//    double                MM1;
//    double                MU1;
//    double                ASD1;
//    double                AST1;
//    double                ASV1;
//    double                ASV11;
//    double                PPAS;
//    double                DPAS;
//    double                ZX1;
//    int                                IV;
//    int                                JV;
//    int                                IXJ;
////
////                Material Properties
////
//    double                FCU;
//    double                DFCU;
//    double                VC;
//    double                FY;
//    double                FYV;
//    double                CVB;
//    double                CVT;
//    double                CV1;
//    double                CV2;
////
//    double                AB1;
//    double                AB2;
//    double                AB3;
//    double                AB4;
////
//    double                TGQK;
//    double                VGK;
//    double                VQK;
//    double                VV;
////
////                Static Data
////
//    //double                FC[7],PAS[7],VS[6,5],VCA[6];



//        //double[,] ASD, AST, ASV, G, Q;
//        //double[] L, B, D, B2, B3, D2, D3, H2, H3;
//        //int[] L2;
//        //double[] GL, QL, GR, QR, C2, C3, Q2, Q3;
//        //double[,] AR2, AR3;
//        //double[] RAD, RA, RBD, RB, RAL, RBL;
//        //double[,] A2, A3, A4, A5, AA5, V1, M3, M4, M1, M2, M6, M7;
//        //int[,] A1;

//        //double[] V, M, M5, X5;
//        //double[,] V2, M8, M9;
//        //double[] K2_, K3_, K1_, F1_, F2_, F3_, F4_, D4_, E_, F_, R_;
//        //int N = 0, NN = 0; // Number of Span
//        //double AB1, AB2, AB3, AB4;

//        //double val1, val2;
//        //double A, C,_B,_D, S1, S2, S3;


//        //double U, P1, P2;
//        //// Max Factor for Dead Load
//        //double S1A = 18.0d;

//        //// Safety Factor for Imposed Load
//        //double S2A = 18.9d;

//        //// Min Safety Factor for Dead Load
//        //double S3A = 10.9;

//        //double FCU, FY, FYV, CVB, CVT;


//        //double MCLD = 1; // Left Cantilever DL M
//        //double MCLL = 1; // Left Cantilever LL M
//        //double MCRD = 1; // Right Cantilever DL M
//        //double MCRL = 1; // Right Cantilever LL M
//        //double F1, F2, F3, F4;


        enum BEAM_TYPE
        {
            BEAM = 0,
            SUBFRAME = 1
        }

        BEAM_TYPE beam_type = BEAM_TYPE.BEAM;
        public frmRectangularBeam(IApplication app)
        {
            InitializeComponent();
            iApp = app;

            //ASD = new double[15, 12];
            //AST = new double[15, 12];
            //ASV = new double[15, 12];
            //L = new double[15];
            //B = new double[15];
            //D = new double[15];
            //L2 = new double[15];
            //B2 = new double[16];
            //B3 = new double[16];
            //D2 = new double[16];
            //D3 = new double[16];
            //H2 = new double[16];
            //H3 = new double[16];
            //G = new double[16, 15];
            //Q = new double[16, 15];
            //GL = new double[16];
            //QL = new double[16];
            //GR = new double[16];
            //QR = new double[16];
            //C2 = new double[16];
            //C3 = new double[16];
            //Q2 = new double[16];
            //Q3 = new double[16];
            //AR2 = new double[15, 10];
            //AR3 = new double[16, 10];
            //RAD = new double[15];
            //RB = new double[15];
            //RAL = new double[15];
            //RBL = new double[15];

            //A1 = new int[16, 16];
            //A2 = new double[16, 16];
            //A3 = new double[16, 16];
            //A4 = new double[16, 16];
            //A5 = new double[15, 15];
            //AA5 = new double[16, 16];
            //V = new double[11];
            //V1 = new double[16, 15];
            //M = new double[11];
            //M3 = new double[16, 15];
            //M4 = new double[16, 15];
            //M1 = new double[16, 15];
            //M2 = new double[16, 15];
            //M6 = new double[16, 15];
            //M7 = new double[16, 15];
            //M5 = new double[15];
            //V2 = new double[16, 15];
            //M8 = new double[16, 11];
            //M9 = new double[16, 11];
            //X5 = new double[15];


            //double[] K2_, K3_, K1_, F1_, F2_, F3_, F4_, D4_, E_, F_, R_;
            //K2_ = new double[16];
            //K3_ = new double[16];
            //K1_ = new double[15];
            //F1_ = new double[15];
            //F2_ = new double[15];
            //F3_ = new double[15];
            //F4_ = new double[15];
            //D4_ = new double[16];
            //E_ = new double[16];
            //F_ = new double[16];
            //R_ = new double[16];
        }

        private void frmRectangularBeam_Load(object sender, EventArgs e)
        {
            SetDefaultData();

            if (Directory.Exists(iApp.LastDesignWorkingFolder))
                FilePath = iApp.LastDesignWorkingFolder;
        }
        #region Form Events
        private void btnProcess_Click(object sender, EventArgs e)
        {
            InitializeData();
            Write_User_Input();
            if (Calculate_Program())
                if (File.Exists(rep_file_name)) { MessageBox.Show(this, "Report file written in " + rep_file_name, "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information); iApp.View_Result(rep_file_name);  } 
            else
                return;
            is_process = true;
            FilePath = user_path;
        }
        private void btnWorkingFolder_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.Cancel)
            {
                if (user_path.ToLower() != fbd.SelectedPath.ToLower())
                {
                    is_process = false;
                    FilePath = fbd.SelectedPath;
                }
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            iApp.View_Result(rep_file_name);
        }
        private void btnDrawing_Click(object sender, EventArgs e)
        {
            iApp.RunViewer(Path.Combine(file_path, "Beam Drawings"), "Continuous_Beam");
            //iApp.SetApp_Design_BEAM(user_input_file, "COMPOSITE_BRIDGE");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region IReport Members

        public bool Calculate_Program()
        {
            bool success = false;
            int i = 0; // for loop iterator
            int j = 0;
            //StreamWriter sw = new StreamWriter(new FileStream(rep_file_name, FileMode.Create));
            try
            {
                #region TechSOFT Banner
                dptr.WriteLine();
                dptr.WriteLine();
                dptr.WriteLine("\t\t*****************************************************");
                dptr.WriteLine("\t\t*              ASTRA Pro Release 20.0                *");
                dptr.WriteLine("\t\t*         TechSOFT Engineering Services             *");
                dptr.WriteLine("\t\t*                                                   *");
                dptr.WriteLine("\t\t*  DESIGN OF CONTINUOUS BEAM AND SUBFRAME ANALYSIS  *");
                dptr.WriteLine("\t\t*         FOR RECTANGULAR OR FLANGED BEAM           *");
                dptr.WriteLine("\t\t*****************************************************");
                dptr.WriteLine("\t\t-----------------------------------------------------");
                dptr.WriteLine("\t\t   THIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                dptr.WriteLine("\t\t-----------------------------------------------------");

                #endregion

                #region USER DATA

                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine("USER'S DATA");
                //sw.WriteLine("------------------------------------------------------------");
                //sw.WriteLine();
                //sw.WriteLine();
                //sw.WriteLine("ESTIMATED BASE DEPTH [H] = {0} m", txt_H.Text);
                //sw.WriteLine("ALLOWABLE BEARING PRESSURE [F] = {0} m", txt_F.Text);
                //sw.WriteLine("SPACING OF COLUMN [S] = {0} m", txt_S.Text);
                //sw.WriteLine("AXIAL LOADING FOR COLUMN 1 [N1] = {0} kN/sq.mm", txt_N1.Text);
                //sw.WriteLine("AXIAL LOADING FOR COLUMN 2 [N2] = {0} kN/sq.mm", txt_N2.Text);
                //if (Is_Rectangle_Footing)
                //{
                //    sw.WriteLine("TRIAL WIDTH [A] = {0} m", txt_A.Text);
                //    sw.WriteLine("TRIAL LENGTH [C] = {0} m", txt_C.Text);
                //}
                //else
                //{
                //    sw.WriteLine("TRIAL DIMENSION [A, B, C] = {0} m", txt_ABC.Text);
                //}
                //dptr.WriteLine();
                //dptr.WriteLine();
                //dptr.WriteLine("------------------------------------------------------------");
                //dptr.WriteLine("DESIGN CALCULATIONS");
                //dptr.WriteLine("------------------------------------------------------------");
                //dptr.WriteLine();
               
                #endregion

                //InitializeData();
                // Chiranjit , TechSOFT, Kolkata, 21 June, 2010
                //
                MemberStiffness();
                //
                //                Write Input
                //
                S4810();
                //
                //                Dead Load First
                //
                S7940();
                //
                //                Others
                //
                S1340();
                S1890();
                S2230();
                S2340();
                S2500();
                S6500();
                S2830();
                //
                //                Write to File
                //
                S6790();
                dptr.WriteLine();
                dptr.WriteLine();
                dptr.WriteLine();
                success = true;
            }
            catch (Exception ex) 
            { 
                lptr.Write(ex.ToString()); 
                success = false; 
            }
            finally
            {
                //sw.Flush();
                //sw.Close();

                lptr.Flush();
                //optr.Flush();
                dptr.Flush();
                
                lptr.Close();
                //optr.Close();
                dptr.Close();

            }
            return success;
        }

        #region My Sub
                /*
        #region SUB
                public void SUB_1340()
                {
                    //double F1, F2, F3, F4;
                    // To Calculate Fixed End Moments
                    for (int i = 0; i < N; i++)
                    {
                        F1 = F2 = F3 = F4 = 0.0;
                        F1_[i] = F2_[i] = F3_[i] = F4_[i] = 0.0;
                        for (int j = 0; j < L2[i]; j++)
                        {
                            if (A1[i, j] == 1)
                            {
                                // S/R FOR LOAD TYPE #1
                                F1 = Math.Pow(A2[i, j] * L[i], 2.0) / 12.0;
                                F2 = -F1;
                                F1_[i] = F1_[i] + F1;
                                F2_[i] = F2_[i] + F2;

                                F3 = Math.Pow(A3[i, j] * L[i], 2.0) / 12.0;
                                F4 = -F3;
                                F3_[i] += F3;
                                F4_[i] += F4;
                                // SUB_1490();
                                // for UDL
                            }
                            if (A1[i, j] == 2)
                            {
                                // SUB_1590();
                                // for Point Load
                                F1 = A4[i, j] / L[i] * Math.Pow((1 - (A4[i, j] / L[i])), 2.0) * A2[i, j] * L[i];
                                F2 = -Math.Pow((A4[i, j] / L[i]), 2.0) * (1 - (A4[i, j] / L[i])) * A2[i, j] * L[i];
                                F1_[i] = F1_[i] + F1;
                                F2_[i] += F2;

                                F3 = A4[i, j] / L[i] * Math.Pow((1 - (A4[i, j] / L[i])), 2.0) * A3[i, j] * L[i];
                                F4 = -Math.Pow((A4[i, j] / L[i]), 2.0) * (1 - (A4[i, j] / L[i])) * A3[i, j] * L[i];

                                F3_[i] += F3;
                                F4_[i] += F4;
                            }
                            if (A1[i, j] == 3)
                            {
                                // SUB_1730();
                                // for Partial Load
                                //S/R for Load Type #2
                                //AB1 = (6 - 8 * (A4[i, j] + A5[i, j]) / L[i] + 3 * Math.Pow(((A4[i, j] + A5[i, j]) / L[i]), 2.0)) * Math.Pow(((A4[i, j] + A5[i, j]) / L[i]), 2.0);
                                //AB2 = (6 - 8 * (A4[i, j]) / L[i] + 3 * Math.Pow(((A4[i, j] + A5[i, j]) / L[i]), 2.0)) * Math.Pow(((A4[i, j] + A5[i, j]) / L[i]), 2.0);
                                //AB3 = Math.Pow(((AB3[i, j] + A5[i, j]) / L[i]), 3.0) * (4 - 3 * ((A4[i, j] + A5[i, j]) / L[i]));
                                //AB4 = Math.Pow(((A4[i, j]) / L[i]), 3.0) * (4 - 3 * ((A4[i, j]) / L[i]));
                                //F1 = Math.Pow(A2[i, j] * L[i], 2.0) / 12.0 * (AB1 - AB2);
                                //F2 = -Math.Sqrt(A2[i, j] * L[i], 2.0) / 12.0 * (AB3 - AB4);
                                //F1_[i] += F1;
                                //F2_[i] += F2;

                                //F3 = Math.Pow(A3[i, j] * L[i], 2.0) / 12.0 * (AB1 - AB2);
                                //F4 = -(Math.Pow(A3[i, j] * L[i], 2.0)) / 12.0 * (AB3 - AB4);

                                //F3_[i] += F3;
                                //F4_[i] += F4;
                                //NN
                            }
                        }
                        F2_[i] = -F2_[i];
                        F4_[i] = -F4_[i];
                    }
                    // Complete Fixed End Moment
                }

                public void SUB_1490(int I, int J)
                {
                    // S/R for Load Type #1
                    F1 = A2[I, J] * L[I] * L[I] / 12;
                    F2 = -F1;
                    F1_[I] += F1;
                    F2_[I] += F2;
                    F3 = A3[I, J] * L[I] * L[I] / 12;
                    F4 = -F3;
                    F3_[I] += F3;
                    F4_[I] += F4;

                    // End S/R FOR LOAD TYPE #1
                }

                public void SUB_1590(int I, int J)
                {
                    F1 = A4[I, J] / L[I] * Math.Pow((1 - (A4[I, J] / L[I])), 2.0) * A2[I, J] * L[I];
                    F2 = -(Math.Pow((A4[I, J] / L[I]), 2.0)) * (1 - (A4[I, J] / L[I])) * A2[I, J] * L[I];
                    F1_[I] = F1_[I] + F1;
                    F2_[I] += F2;
                    F3 = A4[I, J] / L[I] * Math.Pow((1 - (A4[I, J] / L[I])), 2.0) * A3[I, J] * L[I];
                    F4 = -Math.Pow((A4[I, J] / L[I]), 2.0) * (1 - (A4[I, J] / L[I])) * A3[I, J] * L[I];
                    F3_[I] += F3;
                    F4_[I] += F4;
                    // End S/R FOR LOAD TYPE #2
                }
                public void SUB_1730(int I,int J)
                {
                    //S/R LOAD TYPE #3

                    AB1 = (6 - 8 * (A4[I, J] + A5[I, J]) / L[I] + 3 * Math.Pow(((A4[I, J] + A5[I, J]) / L[I]), 2.0)) * Math.Pow(((A4[I, J] + A5[I, J]) / L[I]), 2.0);
                    AB2 = (6 - 8 * (A4[I, J]) / L[I] + 3 * (Math.Pow(((A4[I, J]) / L[I]), 2))) * Math.Pow(((A4[I, J]) / L[I]), 2.0);
                    AB3 = Math.Pow(((A4[I, J] + A5[I, J]) / L[I]), 3) * (4 - 3 * ((A4[I, J] + A5[I, J]) / L[I]));
                    
                    AB4 = Math.Pow(((A4[I, J]) / L[I]), 3.0) * (4 - 3 * ((A4[I, J]) / L[I]));
                    F1 = Math.Pow(A2[I, J] * L[I], 2.0) / 12.0 * (AB1 - AB2);
                    F2 = -Math.Pow(A2[I, J] * L[I], 2.0) / 12.0 * (AB3 - AB4);
                    F1_[I] += F1;
                    F2_[I] += F2;

                    F3 = Math.Pow(A3[I, J] * L[I], 2.0) / 12.0 * (AB1 - AB2);
                    F4 = -(Math.Pow(A3[I, J] * L[I], 2.0)) / 12.0 * (AB3 - AB4);

                    F3_[I] += F3;
                    F4_[I] += F4;
                    // End s/r Loadtype 3
                }

                public void SUB_1930()
                {
                    S1 = S1A;
                    S2 = S2A;
                    S3 = S3A;
                }
                public void SUB_1970()
                {
                    NN = N;

                    int I, J;

                    for (I = 0; I < NN + 1; I++)
                    {
                        if (I == 0)
                        {
                            A = S1;
                            _B = S2;
                            C = S3;
                            _D = 0;
                        }
                        else if (I == 1)
                        {
                            A = S3;
                            _B = 0; C = S1;
                            _D = S2;
                        }
                        for (J = 0; J < N; J++)
                        {
                            val1 = (double)(J / 2.0);
                            val2 = (int)(J / 2);

                            if (val1 > val2)
                            {
                                G[I, J] = A;
                                Q[I, J] = _B;
                            }
                            else
                            {
                                G[I, J] = C;
                                Q[I, J] = _D;
                            }
                        }
                        if (I < 3)
                        {
                            SUB_2160(I);
                        }
                    }

                }

                private void SUB_2160(int I)
                {
                    if (G[I, 1] == S1)
                    {
                        GL[I] = S3;
                    }
                    else
                    {
                        GL[I] = S1;
                    }

                    if (G[I, N] == S1)
                    {
                        GR[I] = S3;
                    }
                    else
                    {
                        GR[I] = S1;
                    }
                    if (Q[I, 1] == S2)
                    {
                        QL[I] = 0;
                    }
                    else
                    {
                        QL[I] = S2;
                    }
                    if (Q[I, N] == S2)
                    {
                        QR[I] = 0;
                    }
                    else
                    {
                        QR[I] = S2;
                    }
                }
                public void SUB_2230()
                {
                    // S / R SLOPE DEFLECTION EQUATION  -LHS
                    int I, J;
                    for (J = 1; J <= N + 1; J++)
                    {
                        if (J == 1)
                        {
                            D4_[1] = 4 * (K1_[1] + K2_[1] + K3_[1]);
                            E_[1] = 2 * K1_[1];
                            continue;
                        }
                        if (J == N + 1)
                        {
                            D4_[J] = 4 * (K1_[N] + K2_[J] + K3_[J]);
                            E_[J] = 0;
                            continue;
                        }
                        D4_[J] = 4 * (K1_[J - 1] + K1_[J] + K2_[J] + K3_[J]);
                        E_[J] = 2 * K1_[J];
                    }
                }
                public void SUB_2340()
                {
                    // S/R SLOPE DEFLECTION EQ.   -RHS
                    int J, I;

                    for (I = 1; I <= NN + 1; I++)
                    {
                        for (J = 1; J < N + 1; J++)
                        {
                            if (J == 1)
                            {
                                F_[1] = -F1_[1] * G[I, J] - F3_[1] * Q[I, 1] + MCLD * GL[I] + MCLL * QL[I];
                                continue;
                            }
                            if (J == N + 1)
                            {
                                F_[J] = F2_[N] * G[I, N] + F4_[N] * Q[I, N] - MCRD * GR[I] - MCRL * QR[I];
                                continue;
                            }
                            F_[J] = F2_[J - 1] * G[I, J - 1] + F4_[J - 1] * Q[I, J - 1] - F1_[J] * G[I, J] - F3_[J] * Q[I, J];
                        }
                        //SUB_3930(); // SOLUTION OF EQUATION
                        //
                    }
                }
                public void SUB_2500()
                {
                    // S/R TO CALCULATE MOMENT AND SHEAR FOR OVERALL BEAM
                    U = Math.Pow(10, 10.0);
                    int I, J, K;

                    for (J = 1; J <= N; J++)
                    {
                        M5[J] = 0;
                        for (K = 1; K <= 11; K++)
                        {
                            V1[J, K] = 0;
                        }
                        for (K = 1; K <= 11; K++)
                        {
                            M3[J, K] = U;
                            M4[J, K] = -U;
                        }
                        for (I = 1; I <= NN + 1; I++)
                        {
                            //SUB_4130(); // SPAN SHEAR AND MOMENTS
                            // S/R ENVELOPE SORTING
                            for (K = 1; K <= 11; K++)
                            {
                                if (Math.Abs(V[K]) > Math.Abs(V1[J, K]))
                                {
                                    V1[J, K] = V[K];
                                }
                            }
                            RA[J] = V1[J, 1];
                            RB[J] = V1[J, 11];

                            for (K = 1; K <= 11; K++)
                            {
                                if (M[K] < M3[J, K])
                                {
                                    M3[J, K] = M[K];
                                }
                                // MM[J,i]=M[K];
                                if (M[K] > M4[J, K])
                                {
                                    M4[J, K] = M[K];
                                }
                                // MN[J,I)=M[K];
                                if (M[K] > M5[J])
                                {
                                    M5[J] = M[K];
                                    X5[J] = L[J] * (K - 1) / 10.0;
                                }
                            }
                        }
                    }

                    // END S ? R MOMENT & SHEAR OVERALL BEAM
                }
                public void SUB_2830()
                {
                    // OUT SHEAR AND MOMENT ENVELOPES
                    int J, I, K;

                    for (J = 1; J <= N; J++)
                    {
                        for (K = 1; K <= 11; K++)
                        {
                            // Print  L[J]*(K-1)/10
                        }
                    }
                    // MOMENT DISTRIBUTION

                    // User Input
                    P1 = 15.0d; // Percentage Distribution
                    P2 = (100 - P1) / 100.0;

                    for (I = 3; I <= N + 1; I++)
                    {
                        for (J = 1; J <= N; J++)
                        {
                            if (J == 1)
                            {
                                M6[I, 1] = M1[I, 1];

                                if ((-M2[I, J]) <= (M1[I, J + 1]))
                                {
                                    M7[I, J] = M2[I, J] * P2;
                                    M6[I, J + 1] = M1[I, J + 1] + M2[I, J + 1] - M7[I, J];
                                }
                                continue;
                            }
                            if ((M1[I, J]) < (-(M2[I, J - 1])))
                            {
                                M6[I, J] = M1[I, J] * P2;
                                M7[I, J - 1] = M2[I, J - 1] + M1[I, J] - M6[I, J];
                            }
                            if (J == N)
                            {
                                M7[I, N] = M2[I, N];
                                continue;
                            }
                        }
                    }
                }
        #endregion
                /**/
        #endregion
        public void Write_User_Input()
        {
            StreamWriter sw = new StreamWriter(new FileStream(user_input_file, FileMode.Create));

            try
            {
                #region User Data

                sw.WriteLine("BeamFrame = {0}", rbtnBeam.Checked.ToString());
                sw.WriteLine("MCLD = {0}", txt_MCLD.Text);
                sw.WriteLine("MCLL = {0}", txt_MCLL.Text);
                sw.WriteLine("MCRL = {0}", txt_MCRL.Text);
                sw.WriteLine("MCRD = {0}", txt_MCRD.Text);

                for (int i = 0; i < dgv_BeamGeometry.RowCount - 1; i++)
                {
                    sw.WriteLine("BEAM Data = {0} {1} {2} {3}",
                        dgv_BeamGeometry[0, i].Value.ToString(),
                        dgv_BeamGeometry[1, i].Value.ToString(),
                        dgv_BeamGeometry[2, i].Value.ToString(),
                        dgv_BeamGeometry[3, i].Value.ToString());
                }
                for (int i = 0; i < dgv_ColumnGeometry.RowCount - 1; i++)
                {
                    sw.WriteLine("Column Data = {0} {1} {2} {3} {4} {5} {6}",
                        dgv_ColumnGeometry[0, i].Value.ToString(),
                        dgv_ColumnGeometry[1, i].Value.ToString(),
                        dgv_ColumnGeometry[2, i].Value.ToString(),
                        dgv_ColumnGeometry[3, i].Value.ToString(),
                        dgv_ColumnGeometry[4, i].Value.ToString(),
                        dgv_ColumnGeometry[5, i].Value.ToString(),
                        dgv_ColumnGeometry[6, i].Value.ToString());
                }
                for (int i = 0; i < dgv_LoadCase.RowCount - 1; i++)
                {
                    sw.WriteLine("LoadCase Data = {0} {1} {2} {3} {4} {5} {6}",
                        dgv_LoadCase[0, i].Value.ToString(),
                        dgv_LoadCase[1, i].Value.ToString(),
                        dgv_LoadCase[2, i].Value.ToString(),
                        dgv_LoadCase[3, i].Value.ToString(),
                        dgv_LoadCase[4, i].Value.ToString(),
                        dgv_LoadCase[5, i].Value.ToString(),
                        dgv_LoadCase[6, i].Value.ToString());
                }
                sw.WriteLine("FCU = {0}", txt_FCU.Text);
                sw.WriteLine("FY = {0}", txt_FY.Text);
                sw.WriteLine("FYV = {0}", txt_FYV.Text);
                sw.WriteLine("CVT = {0}", txt_TopCover.Text);
                sw.WriteLine("CVB = {0}", txt_BottomCover.Text);
                sw.WriteLine("MaxFactor = {0}", txt_MaxFactorDeadLoad.Text);
                sw.WriteLine("SafetyFactor = {0}", txt_SafetyFactor.Text);
                sw.WriteLine("MinSF = {0}", txt_MinSF.Text);
                sw.WriteLine("Distribute = {0}", chk_distribute.Checked.ToString());
                sw.WriteLine("Percentage = {0}", txt_Percentage.Text);
                #endregion
            }
            catch (Exception ex) { }

            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        
        public void Read_User_Input()
        {
            #region USER DATA

            List<string> lst_content = new List<string>(File.ReadAllLines(user_input_file));
            string kStr = "";
            MyList mList = null;
            string VarName = "";

            #endregion
            int indx = -1;
            try
            {
                dgv_BeamGeometry.Rows.Clear();
                dgv_ColumnGeometry.Rows.Clear();
                dgv_LoadCase.Rows.Clear();
                for (int i = 0; i < lst_content.Count; i++)
                {
                    kStr = MyList.RemoveAllSpaces(lst_content[i]);
                    mList = new MyList(kStr, '=');
                    VarName = MyList.RemoveAllSpaces(mList.StringList[0]);
                    /**/
                    #region SWITCH
                    switch (VarName)
                    {
                        case "BeamFrame":
                            if (mList.StringList[1].Trim() == "True")
                                rbtnBeam.Checked = true;
                            else
                                rbtnFrame.Checked = true;
                            break;
                        case "MCLD":
                            txt_MCLD.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "MCLL":
                            txt_MCLL.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "MCRD":
                            txt_MCRL.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "MCRL":
                            txt_MCRL.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;

                        case "BEAM Data":
                            mList = new MyList(mList.StringList[1].Trim().TrimEnd().TrimStart(), ' ');
                            dgv_BeamGeometry.Rows.Add(mList.GetInt(0),
                                mList.GetDouble(1), mList.GetDouble(2), mList.GetDouble(3));
                            break;
                        case "Column Data":
                            mList = new MyList(mList.StringList[1].Trim().TrimEnd().TrimStart(), ' ');
                            dgv_ColumnGeometry.Rows.Add(mList.GetInt(0),
                                mList.GetDouble(1), mList.GetDouble(2), mList.GetDouble(3),
                                mList.GetDouble(4), mList.GetDouble(5), mList.GetDouble(6));
                            break;
                        case "LoadCase Data":
                            mList = new MyList(mList.StringList[1].Trim().TrimEnd().TrimStart(), ' ');
                            if (mList.StringList[2] == "UDL")
                            {
                                dgv_LoadCase.Rows.Add(mList.GetInt(0),
                                    mList.GetDouble(1), (mList.StringList[2] + " " + mList.StringList[3]),
                                    mList.GetDouble(4), mList.GetDouble(5), mList.GetDouble(6), mList.GetDouble(7));
                            }
                            else
                            {
                                dgv_LoadCase.Rows.Add(mList.GetInt(0),
                                    mList.GetDouble(1), (mList.StringList[2] + " " + mList.StringList[3] + " " + mList.StringList[4]),
                                    mList.GetDouble(5), mList.GetDouble(6), mList.GetDouble(7), mList.GetDouble(8));
                            }
                            break;
                        case "FCU":
                            txt_FCU.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "FY":
                            txt_FY.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "FYV":
                            txt_FYV.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "CVT":
                            txt_TopCover.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "CVB":
                            txt_BottomCover.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "MaxFactor":
                            txt_MaxFactorDeadLoad.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "SafetyFactor":
                            txt_SafetyFactor.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "MinSF":
                            txt_MinSF.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                        case "Distribute":
                            chk_distribute.Checked = (mList.StringList[1].Trim() == "True");
                            break;
                        case "Percentage":
                            txt_Percentage.Text = mList.StringList[1].Trim().TrimEnd().TrimStart();
                            break;
                    }
                    #endregion
                    /**/
                }
            }
            catch (Exception ex)
            {
            }
            lst_content.Clear();
        }
        public string GetAstraDirectoryPath(string userpath)
        {

            //<6.0 Process Design\\6.1 Bridge Design\\6.1.1 RCC T-Beam Bridge\\6.1.1.1 Design of Deck Slab\\
            //Bridge_Rcc_T_Beam_Deck_Slab.TXT>
            string kPath = Path.Combine(user_path, "Structural Design");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }

            kPath = Path.Combine(kPath, "RCC Beam");

            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }

            kPath = Path.Combine(kPath, "Design of Rectangular or Flanged Beam");
            if (!Directory.Exists(kPath))
            {
                Directory.CreateDirectory(kPath);
            }
            return kPath;
        }

        public string FilePath
        {
            set
            {
                this.Text = "DESIGN OF RECTANGULAR BEAM : " + value;
                user_path = value;
                file_path = GetAstraDirectoryPath(user_path);
                //file_path = Path.Combine(user_path, "RECTANGULAR_BEAM");
                if (Directory.Exists(file_path) == false)
                    Directory.CreateDirectory(file_path);

                system_path = Path.Combine(file_path, "AstraSys");
                if (Directory.Exists(system_path) == false)
                    Directory.CreateDirectory(system_path);

                rep_file_name = Path.Combine(file_path, "Beam_Rectangular_Or_Flanged.TXT");
                user_input_file = Path.Combine(system_path, "RECTANGULAR_BEAM.FIL");

                btnReport.Enabled = File.Exists(rep_file_name);
                btnDrawing.Enabled = File.Exists(rep_file_name);
                btnProcess.Enabled = Directory.Exists(value);

                if (File.Exists(user_input_file) && !is_process)
                {
                    string msg = "This folder \'" + value + "\' \n already contains Previous Design. Overwrite Previous Design?";
                    msg = "There is Design Data in the selected Folder.\nOpening the data taking from the Last Design.";
                    if (MessageBox.Show(msg, "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        Read_User_Input();
                }
            }
        }

        private void rbtnBeam_CheckedChanged(object sender, EventArgs e)
        {
            grbColGeometry.Enabled = rbtnFrame.Checked;
            dgv_ColumnGeometry.Visible = rbtnFrame.Checked;
        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {

        }

        private void dgv_BeamGeometry_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //if (dgv_BeamGeometry.RowCount > 16)
            //{
            //try
            //{
            //    double d1, d2, d3, d4, d5, d6;

            //    d1 = MyList.StringToDouble(dgv_BeamGeometry[1, dgv_BeamGeometry.RowCount - 2].Value.ToString(),0.0);
            //    d2 = MyList.StringToDouble(dgv_BeamGeometry[2, dgv_BeamGeometry.RowCount - 2].Value.ToString(),0.0);
            //    d3 = MyList.StringToDouble(dgv_BeamGeometry[3, dgv_BeamGeometry.RowCount - 2].Value.ToString(),0.0);

            //    dgv_ColumnGeometry.Rows.Add(dgv_ColumnGeometry.RowCount, d1, d2, d3, d1, d2, d3);
            //    SetCell_ColumnGeometry();
            //}
            //catch (Exception ex) { }
            //}
        }

        private void dgv_BeamGeometry_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_BeamGeometry.RowCount > 15)
            {
                try
                {
                    dgv_BeamGeometry.AllowUserToAddRows = false;
                    //MessageBox.Show(this, "Program is Limited to 15 Beams only.");
                }
                catch (Exception ex) { }
            }
            else if (dgv_BeamGeometry.RowCount < 15)
                dgv_BeamGeometry.AllowUserToAddRows = true;

            SetCell_BeamGeometry();

        }

        private void dgv_BeamGeometry_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            SetCell_BeamGeometry();
        }
        void SetCell_BeamGeometry()
        {
            double val = .0;
            try
            {
                for (int i = 0; i < dgv_BeamGeometry.RowCount-1; i++)
                {
                    dgv_BeamGeometry[0, i].Value = (i + 1);

                    if (dgv_BeamGeometry[1, i].Value != null)
                        val = MyList.StringToDouble(dgv_BeamGeometry[1, i].Value.ToString(), 0.0);
                    else
                        val = 0.0;
                    dgv_BeamGeometry[1, i].Value = val.ToString("0.000"); ;

                    if (dgv_BeamGeometry[2, i].Value != null)
                        val = MyList.StringToDouble(dgv_BeamGeometry[2, i].Value.ToString(), 0.0);
                    else
                        val = 0.0;
                    dgv_BeamGeometry[2, i].Value = val.ToString("0.000"); ;

                    if (dgv_BeamGeometry[3, i].Value != null)
                        val = MyList.StringToDouble(dgv_BeamGeometry[3, i].Value.ToString(), 0.0);
                    else
                        val = 0.0;
                    dgv_BeamGeometry[3, i].Value = val.ToString("0.000"); ;
                }
            }
            catch (Exception ex) { }
        }
        void SetCell_ColumnGeometry()
        {
            double val = 0.0;
            try
            {
                for (int i = 0; i < dgv_ColumnGeometry.RowCount-1; i++)
                {
                    dgv_ColumnGeometry[0, i].Value = (i + 1);

                    if (dgv_ColumnGeometry[1, i].Value != null)
                        val = MyList.StringToDouble(dgv_ColumnGeometry[1, i].Value.ToString(), 0.0);
                    else
                        val = 0.0;
                    dgv_ColumnGeometry[1, i].Value = val.ToString("0.000"); 

                    if (dgv_ColumnGeometry[2, i].Value != null)
                        val = MyList.StringToDouble(dgv_ColumnGeometry[2, i].Value.ToString(), 0.0);
                    else
                        val = 0.0;
                    dgv_ColumnGeometry[2, i].Value = val.ToString("0.000");

                    if (dgv_ColumnGeometry[3, i].Value != null)
                        val = MyList.StringToDouble(dgv_ColumnGeometry[3, i].Value.ToString(), 0.0);
                    else
                        val = 0.0;
                    dgv_ColumnGeometry[3, i].Value = val.ToString("0.000");

                    if (dgv_ColumnGeometry[4, i].Value != null)
                        val = MyList.StringToDouble(dgv_ColumnGeometry[4, i].Value.ToString(), 0.0);
                    else
                        val = 0.0;
                    dgv_ColumnGeometry[4, i].Value = val.ToString("0.000");

                    if (dgv_ColumnGeometry[5, i].Value != null)
                        val = MyList.StringToDouble(dgv_ColumnGeometry[5, i].Value.ToString(), 0.0);
                    else
                        val = 0.0;
                    dgv_ColumnGeometry[5, i].Value = val.ToString("0.000");

                    if (dgv_ColumnGeometry[6, i].Value != null)
                        val = MyList.StringToDouble(dgv_ColumnGeometry[6, i].Value.ToString(), 0.0);
                    else
                        val = 0.0;
                    dgv_ColumnGeometry[6, i].Value = val.ToString("0.000");
                }
            }
            catch (Exception ex) { }
        }

        private void dgv_ColumnGeometry_SelectionChanged(object sender, EventArgs e)
        {
            SetCell_ColumnGeometry();
        }
        /**/
        #endregion



    }
}
