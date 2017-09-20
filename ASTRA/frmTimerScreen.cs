using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.DataStructure;

namespace AstraFunctionOne
{
    public partial class frmTimerScreen : Form
    {
        //eASTRAImage ast_img_type = eASTRAImage.RCC_Cantilever_Slab;
        public int Counter { get; set; }
        public frmTimerScreen(eASTRAImage ASTRA_ImgType)
        {
            ASTRA_ImageType = ASTRA_ImgType;
            InitializeComponent();
            timer1.Disposed += new EventHandler(timer1_Disposed);
            Counter = 0;
        }
        public eASTRAImage ASTRA_ImageType { get; set; }
        public void SetImage()
        {
            switch (ASTRA_ImageType)
            {
                case eASTRAImage.Steel_Truss_Bridge:
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Steel_Truss_Bridge;
                    break;
                case eASTRAImage.Composite_Bridge:
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Titled_Composite_Bridge;
                    break;
                case eASTRAImage.PSC_I_Girder_Short_Span:
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Titled_Pre_Stressed_Bridge;
                    break;
                case eASTRAImage.RCC_T_Beam_Bridge:
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.RCC_T_Girder_Bridge;
                    break;
                case eASTRAImage.Rail_Plate_Girder:
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Rail_Plate_Girder;
                    break;
                case eASTRAImage.PSC_Box_Girder:
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.PSC_BOX_GIRDER_1;
                    break;
                case eASTRAImage.PSC_I_Girder_Long_Span:
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.TBeam_Main_Girder_Bottom_Flange;
                    break;
                case eASTRAImage.Cable_Stayed_Bridge:
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Cable_Stayed_Bridge;
                    break;
                case eASTRAImage.RE_Wall:
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.RE_Wall_Design;
                    break;
                case eASTRAImage.Continuous_PSC_Box_Girder: // Chiranjit [201307 23]
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Cont_PSC_Box_Girder_Bridge;
                    break;
                case eASTRAImage.Steel_Truss_Bridge_K_type: // Chiranjit [201307 23]
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.K_truss_2;
                    break;
                case eASTRAImage.Arch_Cable_Suspension: // Chiranjit [201307 23]
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Humber_Bay_Arch_Bridge;
                    break;
                case eASTRAImage.Cable_Suspension: // Chiranjit [201307 23]
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Suspension_Bridge_Photo;
                    break;
                case eASTRAImage.Extradosed: // Chiranjit [2017 02 03]
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Extradossed_PSC_Box_Girder_Bridge;
                    break;
                case eASTRAImage.Extradosed_SideTowers: // Chiranjit [2017 02 03]
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Extra_Dossed_Bridge__Splash_Image1_;
                    break;
                case eASTRAImage.Extradosed_CentralTowers: // Chiranjit [2017 02 03]
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.ExtradosedCableStayed;
                    break;
                case eASTRAImage.Jetty: // Chiranjit [2017 03 08]
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.jetty;
                    break;
                case eASTRAImage.Transmission_Tower: // Chiranjit [2017 03 08]
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Transmission_Tower1;
                    break;
                case eASTRAImage.Transmission_Tower_3_Cables: // Chiranjit [2017 03 08]
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Transmission_Tower3;
                    break;
                case eASTRAImage.Cable_Car_Tower: // Chiranjit [2017 03 08]
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Cable_Car_Tower1;
                    break;
                case eASTRAImage.Microwave_Tower: // Chiranjit [2017 03 08]
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Microwave_Tower1;
                    break;
            }
        }
        private void pcb_Images_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTimerScreen_Load(object sender, EventArgs e)
        {
            //label1.Text = "5";
            SetImage();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (ASTRA_ImageType == eASTRAImage.Extradosed)
            {
                if (Counter % 2 == 0)
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Extradossed_PSC_Box_Girder_Bridge;
                else if (Counter % 3 == 0)
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Extra_Dossed_Bridge__Splash_Image1_;
                else
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.ExtradosedCableStayed;

                if (Counter == 7)
                {
                    timer1.Stop();
                    this.Close();
                }

                Counter++;
                label1.Text = (7 - Counter).ToString();
            }
            else if (ASTRA_ImageType == eASTRAImage.Transmission_Tower)
            {
                if (Counter % 2 == 0)
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Transmission_Tower1;
                else 
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Transmission_Tower2;
                //else
                //    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Transmission_Tower3;

                if (Counter == 7)
                {
                    timer1.Stop();
                    this.Close();
                }

                Counter++;
                label1.Text = (7 - Counter).ToString();
            }
            else if (ASTRA_ImageType == eASTRAImage.Cable_Car_Tower)
            {
                if (Counter % 2 == 0)
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Cable_Car_Tower1;
                else if (Counter % 3 == 0)
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Cable_Car_Tower2;
                else
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Cable_Car_Tower3;

                if (Counter == 7)
                {
                    timer1.Stop();
                    this.Close();
                }

                Counter++;
                label1.Text = (7 - Counter).ToString();
            }
            else if (ASTRA_ImageType == eASTRAImage.Microwave_Tower)
            {
                if (Counter % 2 == 0)
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Microwave_Tower1;
                else if (Counter % 3 == 0)
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Microwave_Tower2;
                else
                    pcb_Images.BackgroundImage = global::AstraFunctionOne.Properties.Resources.Microwave_Tower3;

                if (Counter == 7)
                {
                    timer1.Stop();
                    this.Close();
                }

                Counter++;
                label1.Text = (7 - Counter).ToString();
            }
            else
            {
                if (Counter == 5)
                {
                    timer1.Stop();
                    this.Close();
                }
                Counter++;
                label1.Text = (5 - Counter).ToString();
            }
            //this.Opacity = (1.0 - (double)(Counter / 20.0));

        }

        void timer1_Disposed(object sender, EventArgs e)
        {
        }
    }
}
