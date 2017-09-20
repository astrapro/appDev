using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using AstraInterface.DataStructure;

namespace AstraFunctionOne
{
    public partial class frmViewResult : Form, IFind
    {

        frm_findcs find;
        public List<string> Lines { get; set; }
        int CurrentPosition = 0;

        Thread thd = null;


        string dataFile = "";
        private Font printFont;
        private StringReader streamToPrint;
        int PageNo = -1;
        bool IsLandScape
        {
            get
            {
                return tsmi_land_scape.Checked;
            }
            set
            {
                tsmi_land_scape.Checked = value;
            }
        }
        public frmViewResult(string dataFile)
        {
            InitializeComponent();
            this.dataFile = dataFile;
            Lines = new List<string>();
        }
        public frmViewResult(string dataFile, bool  is_landscape)
        {
            InitializeComponent();
            this.dataFile = dataFile;
            Lines = new List<string>();
            IsLandScape = is_landscape;

        }

        private void frmViewResult_Load(object sender, EventArgs e)
        {
            //toolTip1.SetToolTip(cmb_step, "Click Here to go STEPS and TABLES quick references.");
            //toolTip1.SetToolTip(lbl_line_col, "Click Here to go this selected lines.");
            if (File.Exists(dataFile))
            {
                pd.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(20, 20, 20, 50);
                //Chiranjit [2012 09 07]
                //For Steel Truss Design Report
                if ((Path.GetFileNameWithoutExtension(dataFile).ToUpper() == "DESIGN_REP") || IsLandScape)
                    pd.DefaultPageSettings.Landscape = true;
                PageNo = 0;
                psd.Document = pd;
                psd.PageSettings.Margins = new System.Drawing.Printing.Margins(20, 20, 20, 50);
                rtbData.Lines = File.ReadAllLines(dataFile);


                tslbl_notok.Visible = rtbData.Text.Contains("NOT OK");
                this.Text = "View Result [" + AstraInterface.DataStructure.MyList.Get_Modified_Path(dataFile) + "]";
                try
                {
                    //Image img =  AstraFunctionOne.Properties.Resources.BOX_1;
                    //Clipboard.SetImage(img);
                    //rtbData.Paste();
                    SetColor();

                    //Image img = AstraFunctionOne.Properties.Resources.BOX_1;
                    //Clipboard.SetImage(img);
                    //rtbData.Paste();
                    



                    //thd = new Thread(new ThreadStart(RunThread));
                    //thd.Start();
                    SaveFile();
                    Select_Steps();


                    if (lsv_steps.Items.Count == 0)
                    {
                        spc_1.Panel1Collapsed = true;
                    }

                    //spc_1.Panel1Collapsed = (Path.GetFileName(dataFile) == "ANALYSIS_REP.TXT");
                }
                catch (Exception ex) { }

                lbl_line_col.BackColor = Color.Black;
                lbl_line_col.ForeColor = Color.Yellow;

            }
            else
                this.Close();
        }

        //Chiranjit [2012 10 19]
        public void RunThread()
        {
            SetColor();
            SaveFile();
            Select_Steps();
        }

        //Chiranjit [2012 09 12]
        Hashtable hash_index;

        List<int> Step_Lines { get; set; }
        public void Select_Steps()
        {
            Step_Lines = new List<int>();
            hash_index = new Hashtable();
            cmb_step.Items.Clear();
            cmb_step.Items.Add("Select...Step.....");



            List<string> analysis_list = new List<string>();
            #region analysis result list
            analysis_list.Add(string.Format("User's data"));
            analysis_list.Add(string.Format("JOINT COORDINATE"));
            analysis_list.Add(string.Format("MEMBER INCIDENCES"));
            analysis_list.Add(string.Format("START GROUP DEFINITION"));
            analysis_list.Add(string.Format("MEMBER PROPERTY"));
            analysis_list.Add(string.Format("CONSTANT"));
            analysis_list.Add(string.Format("SUPPORT"));
            analysis_list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));
            analysis_list.Add(string.Format("LOAD GENERATION"));
            analysis_list.Add(string.Format("C O N T R O L   I N F O R M A T I O N"));
            analysis_list.Add(string.Format("NODAL POINT INPUT DATA"));
            analysis_list.Add(string.Format("GENERATED NODAL DATA"));
            analysis_list.Add(string.Format("EQUATION NUMBERS"));
            analysis_list.Add(string.Format("ELEMENT LOAD MULTIPLIERS"));
            analysis_list.Add(string.Format("TRUSS ELEMENT DATA"));
            analysis_list.Add(string.Format("3 / D   B E A M   E L E M E N T S"));
            analysis_list.Add(string.Format("MATERIAL PROPERTIES"));
            analysis_list.Add(string.Format("BEAM GEOMETRIC PROPERTIES"));
            analysis_list.Add(string.Format("ELEMENT LOAD MULTIPLIERS"));
            analysis_list.Add(string.Format("3/D BEAM ELEMENT DATA"));
            analysis_list.Add(string.Format("E Q U A T I O N   P A R A M E T E R S"));
            analysis_list.Add(string.Format("N O D A L   L O A D S   (S T A T I C)   O R   M A S S E S   (D Y N A M I C)"));
            analysis_list.Add(string.Format("N O D E   D I S P L A C E M E N T S / R O T A T I O N S"));
            analysis_list.Add(string.Format("TRUSS MEMBER ACTIONS"));
            analysis_list.Add(string.Format(".....BEAM FORCES AND MOMENTS"));
            analysis_list.Add(string.Format("  SHELL ELEMENT STRESSES"));
            analysis_list.Add(string.Format("....8 NODE SOLID ELEMENT DATA"));
            analysis_list.Add(string.Format("  .....8-NODE SOLID ELEMENT STRESSES"));
            analysis_list.Add(string.Format(" THIN  PLATE/SHELL  ELEMENT DATA"));
            analysis_list.Add(string.Format(" T H I N   P L A T E / S H E L L   E L E M E N T S"));
            analysis_list.Add(string.Format("S T A T I C   S O L U T I O N   T I M E   L O G"));
            analysis_list.Add(string.Format("O V E R A L L   T I M E   L O G"));
            analysis_list.Add(string.Format("SUMMARY OF MAXIMUM SUPPORT FORCES"));



            analysis_list.Add(string.Format("WEIGHT CALCULATION OF SUPER IMPOSED DEAD LOAD"));
            analysis_list.Add(string.Format("WEIGHT CALCULATION OF STEEL STRUCTURE LOAD"));
            //analysis_list.Add(string.Format("CROSS GIRDER"));
            //analysis_list.Add(string.Format("STRINGER BEAM"));
            //analysis_list.Add(string.Format("BOTTOM CHORD"));
            //analysis_list.Add(string.Format("TOP CHORD"));
            //analysis_list.Add(string.Format("END RAKERS"));
            //analysis_list.Add(string.Format("DIAGONAL MEMBER"));
            //analysis_list.Add(string.Format("VERTICAL MEMBER"));
            //analysis_list.Add(string.Format("TOP CHORD BRACINGS"));
            //analysis_list.Add(string.Format("BOTTOM CHORD BRACINGS"));

            analysis_list.Add(string.Format("STRINGER BEAM"));
            analysis_list.Add(string.Format("CROSS GIRDER"));
            analysis_list.Add(string.Format("BOTTOM CHORD"));
            analysis_list.Add(string.Format("TOP CHORD"));
            analysis_list.Add(string.Format("END RAKERS"));
            analysis_list.Add(string.Format("DIAGONAL MEMBER"));
            analysis_list.Add(string.Format("VERTICAL MEMBER"));
            analysis_list.Add(string.Format("TOP CHORD BRACINGS"));
            analysis_list.Add(string.Format("BOTTOM CHORD BRACINGS"));
            analysis_list.Add(string.Format("CANTILEVER BRACKETS"));
            analysis_list.Add(string.Format("SHORT VERTICAL MEMBER"));
            analysis_list.Add(string.Format("TOP DIAGONAL MEMBER"));
            analysis_list.Add(string.Format("BOTTOM DIAGONAL MEMBER"));
            analysis_list.Add(string.Format("TOP VERTICAL MEMBER"));
            analysis_list.Add(string.Format("BOTTOM VERTICAL MEMBER"));
            analysis_list.Add(string.Format("SHORT DIAGONAL MEMBER"));
            analysis_list.Add(string.Format("ARCH MEMBERS"));
            analysis_list.Add(string.Format("SUSPENSION CABLES"));
            analysis_list.Add(string.Format("TRANSVERSE MEMBER"));

            analysis_list.Add(string.Format("MEMBER GROUP"));



            #endregion


            List<string> list = new List<string>(rtbData.Lines);
            //list.Sort();
            int indx = 0;

            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                //indx += item.Length + 1;
                indx += item.Length;
                if (item.ToUpper().StartsWith("STEP") ||
                   item.ToUpper().StartsWith("TABLE") ||
                   item.ToUpper().StartsWith("STAGE") ||
                    //item.ToUpper().StartsWith("DESIGN") ||
                    item.ToUpper().StartsWith("USER"))
                {
                    if (!cmb_step.Items.Contains(item))
                    {
                        Step_Lines.Add(i);
                        cmb_step.Items.Add(item);
                        hash_index.Add(cmb_step.Items.Count - 1, indx);
                    }
                }
                else
                {
                    foreach (var l in analysis_list)
                    {
                        //if (item.ToUpper().Contains(l.ToUpper()))
                        if (item.Contains(l))
                        {
                            if (!cmb_step.Items.Contains(item))
                            {
                                Step_Lines.Add(i);
                                cmb_step.Items.Add(item);
                                hash_index.Add(cmb_step.Items.Count - 1, indx);
                            }
                        }
                    }
                }
            }
            list.Clear();
            foreach (var item in cmb_step.Items)
            {
                lsv_steps.Items.Add(item.ToString().Trim());
                //lstb_steps.Items.
            }
            if (lsv_steps.Items.Count > 0)
            {
                lsv_steps.Items.RemoveAt(0);
                cmb_step.SelectedIndex = 0;
            }
        }
        void SetColor()
        {
            int indx = rtbData.Find("NOT OK", RichTextBoxFinds.None);

            int start_index = 0;

            int end_ln = rtbData.Find("\n", RichTextBoxFinds.None);
            end_ln = rtbData.Find("\n", RichTextBoxFinds.None);

            string st = rtbData.Text;

            string ch = "";

            for (int i = indx; i >= 0; i--)
            {
                ch = st.Substring(i, 1);

                //if (ch == "=" || ch == "_")
                if (ch == "\n")
                {
                    //ch = st.Substring(i, 1);
                    rtbData.SelectionStart = i;
                    rtbData.SelectionLength = (indx - i);
                    start_index = indx + 1;
                    indx = rtbData.Find("NOT OK", start_index, RichTextBoxFinds.None);

                    i = indx;
                    if (indx == -1)
                    {
                        //indx = rtbData.Find("OK", start_index, RichTextBoxFinds.None);
                        //rtbData.SelectionColor = Color.DarkGreen;
                        break;
                    }
                    else
                        rtbData.SelectionColor = Color.Red;
                }
            }
            //ch = st.Substring(indx, 1);
            start_index = 0;

            //do
            //{
            //    //indx = rtbData.Find("NOT OK", RichTextBoxFinds.None);
            //    indx = rtbData.Find("NOT OK", start_index, RichTextBoxFinds.None);



            //    indx = rtbData.Find("NOT OK", start_index + 1, RichTextBoxFinds.None);


            //    if (indx == -1)
            //    {
            //        indx = rtbData.Find("OK", start_index, RichTextBoxFinds.None);

            //        if (indx != -1)
            //        {
            //            rtbData.SelectionStart = indx;
            //            rtbData.SelectionLength = 2;
            //            rtbData.SelectionColor = Color.DarkGreen;
            //            start_index = indx + 2;

            //            if (start_index > rtbData.Text.Length - 2) break;

            //        }
            //        //return;
            //    }
            //    else
            //    {
            //        rtbData.SelectionStart = indx;
            //        rtbData.SelectionLength = 6;
            //        rtbData.SelectionColor = Color.Red;
            //        start_index = indx + 6;
            //        if (start_index > rtbData.Text.Length - 2) break;
            //    }
            //}
            //while (indx != -1);
            rtbData.SelectionStart = 0;
            rtbData.SelectionLength = 1;
        }

    

        void SaveFile()
        {
            try
            {
                string file_name = Path.GetFileNameWithoutExtension(dataFile);
                file_name = file_name + ".rtf";
                file_name = Path.Combine(Path.GetDirectoryName(dataFile), file_name);
                if (File.Exists(file_name))
                    File.Delete(file_name);
                rtbData.SaveFile(file_name, RichTextBoxStreamType.RichText);
            }
            catch (Exception ex) { }
        }
        //Chiranjit [2012 11 21]
        public Image Get_Truss_Section(string des_str)
        {
            MyList mlist = new MyList(MyList.RemoveAllSpaces(des_str.Replace(",","-")), '-');

            if (mlist.Count > 0)
            {
                if (mlist.IsInt(1))
                {
                    switch (mlist.GetInt(1))
                    {
                        case 1:
                            return ImageCollection.Truss_Section1;
                        case 2:
                            return ImageCollection.Truss_Section2;
                        case 3:
                            return ImageCollection.Truss_Section3;
                        case 4:
                            return ImageCollection.Truss_Section4;
                        case 5:
                            return ImageCollection.Truss_Section5;
                        case 6:
                            return ImageCollection.Truss_Section6;
                        case 7:
                            return ImageCollection.Truss_Section7;
                        case 8:
                            return ImageCollection.Truss_Section8;
                        case 9:
                            return ImageCollection.Truss_Section9;
                        case 10:
                            return ImageCollection.Truss_Section10;
                        case 11:
                            return ImageCollection.Truss_Section11;
                        case 12:
                            return ImageCollection.Truss_Section12;
                        case 13:
                            return ImageCollection.Truss_Section13;
                        case 14:
                            return ImageCollection.Truss_Section14;
                        case 15:
                            return ImageCollection.Truss_Section15;
                    }
                }
            }
            return ImageCollection.Truss_Section1;
        }
        Image img = AstraFunctionOne.Properties.Resources.BOX_1;
        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            //float leftMargin = 20;
            //float topMargin = 20;
            string line = null;

            string file_name = Path.GetFileNameWithoutExtension(dataFile).ToUpper();

            //ev.MarginBounds.Height = ev.PageBounds.Height;
            //Font step_f = new Font(printFont.FontFamily, printFont.Height, FontStyle.Bold);

            // Calculate the number of lines per page.
            linesPerPage = ev.MarginBounds.Height /
               printFont.GetHeight(ev.Graphics);

            //Font bol_f = new Font("Lucida Console", 9.0f, FontStyle.Bold);
            Font bol_f = new Font("Verdana", 7.0f, FontStyle.Bold);
            
            //DateTime.Now.ToString("dd MM/yyyy hh:mm:ss tt")

            string kStr = ( "ASTRA Pro :   " + file_name + " , Page No : " + (++PageNo).ToString());
            //kStr = (DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToLongTimeString() + ", Techsoft Engineering Services, " + " ASTRA Pro, Page No : " + (++PageNo).ToString());
           
            
            
            ev.Graphics.DrawString(kStr, bol_f, Brushes.DarkKhaki, 20.0f, 1.0f, new StringFormat());



            ev.Graphics.DrawLine(Pens.Blue, 0.0f, 15.0f, 1024.0f, 15.0f);

            bool isSection = false;

            // Print each line of the file.
            while (count < linesPerPage &&
               ((line = streamToPrint.ReadLine()) != null))
            {
                line = line.Replace("\t", "        ");
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));



                if (line.ToUpper().StartsWith("STEP")
                    && line.ToUpper().Contains("DESIGN OF")
                    && line.ToUpper().Contains("SECTION")
                    && line.ToUpper().Contains("FIG"))
                {
                     img = Get_Truss_Section(line);

                     ev.Graphics.DrawString(line, printFont, Brushes.Black,
                        leftMargin, yPos, new StringFormat());
                }
                else if (line.ToUpper().StartsWith("REFER TO ABUTMENT DIAGRAM"))
                {
                    if (img != null)
                    {
                        img = ImageCollection.Abutment;
                        float sd = img.Height / printFont.GetHeight(ev.Graphics) + 2.0f;

                        if (yPos + img.Height > ev.PageBounds.Height)
                        {
                            ev.Graphics.DrawImage(img, new Point((int)leftMargin, (int)(ev.PageBounds.Height - img.Height - 10.0)));
                        }
                        else
                            ev.Graphics.DrawImage(img, new Point((int)leftMargin, (int)(yPos - 10.0)));

                        yPos += img.Height;

                        count += (int)(img.Height / printFont.GetHeight(ev.Graphics));
                    }
                    ev.Graphics.DrawString("SAMPLE ABUTMENT DIAGRAM", printFont, Brushes.Red,
                       leftMargin + 200, yPos, new StringFormat());
                }
                else if (line.ToUpper().StartsWith("REFER TO PIER  DIAGRAM"))
                {
                    if (img != null)
                    {
                        img = ImageCollection.Pier_drawing;
                        float sd = img.Height / printFont.GetHeight(ev.Graphics) + 2.0f;

                        if (yPos + img.Height > ev.PageBounds.Height)
                        {
                            ev.Graphics.DrawImage(img, new Point((int)leftMargin, (int)(ev.PageBounds.Height - img.Height - 10.0)));
                        }
                        else
                            ev.Graphics.DrawImage(img, new Point((int)leftMargin, (int)(yPos - 10.0)));

                        yPos += img.Height + 10;

                        count += (int)(img.Height / printFont.GetHeight(ev.Graphics));
                    }
                    ev.Graphics.DrawString("SAMPLE PIER DIAGRAM", printFont, Brushes.Red,
                       leftMargin + 300, yPos, new StringFormat());
                }
                else if (line.ToUpper().StartsWith("REFER TO PIER DIAGRAM"))
                {
                    if (img != null)
                    {
                        img = ImageCollection.Pier_Box_drawing;
                        float sd = img.Height / printFont.GetHeight(ev.Graphics) + 2.0f;

                        if (yPos + img.Height > ev.PageBounds.Height)
                        {
                            ev.Graphics.DrawImage(img, new Point((int)leftMargin, (int)(ev.PageBounds.Height - img.Height - 10.0)));
                        }
                        else
                            ev.Graphics.DrawImage(img, new Point((int)leftMargin, (int)(yPos - 10.0)));

                        yPos += img.Height + 10;

                        count += (int)(img.Height / printFont.GetHeight(ev.Graphics));
                    }
                    ev.Graphics.DrawString("SAMPLE PIER DIAGRAM", printFont, Brushes.Red,
                       leftMargin + 300, yPos, new StringFormat());
                }
                else if (line.ToUpper().StartsWith("SELECTED SECTION DATA"))
                {
                    if (img != null)
                    {
                        Bitmap b = new Bitmap(img);
                        b.MakeTransparent(Color.Black);
                        float sd = img.Height / printFont.GetHeight(ev.Graphics) + 2.0f;

                        if (yPos + img.Height > ev.PageBounds.Height)
                        {
                            ev.Graphics.DrawImage(b, new Point(500, (int)(ev.PageBounds.Height - img.Height - 10.0)));
                        }
                        else
                            ev.Graphics.DrawImage(b, new Point(500, (int)(yPos - 10.0)));
                    }
                    ev.Graphics.DrawString(line, printFont, Brushes.Black,
                       leftMargin, yPos, new StringFormat());
                }
                else if (line.ToUpper().StartsWith("REFER TO CROSS SECTION DIAGRAM IN FILE 'SECTIONDIAGRAM.JPG'"))
                {
                    img = AstraFunctionOne.Properties.Resources.BOX_1;


                    float sd = img.Height / printFont.GetHeight(ev.Graphics);

                    ev.Graphics.DrawImage(img, new Point(10, (int)(yPos + 10)));
                    count += (int)sd;


                    //ev.Graphics.DrawString(line, printFont, Brushes.DarkMagenta,
                    //   leftMargin, yPos, new StringFormat());
                }
                else if (line.ToUpper().StartsWith("STEP"))
                {
                    ev.Graphics.DrawString(line, printFont, Brushes.DarkMagenta,
                       leftMargin, yPos, new StringFormat());
                }
                else if (line.ToUpper().Contains("NOT OK"))
                {
                    ev.Graphics.DrawString(line, printFont, Brushes.Red,
                       leftMargin, yPos, new StringFormat());
                }
                else if (line.ToUpper().Contains("NOT SAFE"))
                {
                    ev.Graphics.DrawString(line, printFont, Brushes.Red,
                       leftMargin, yPos, new StringFormat());
                }
                else if (line.ToUpper().Contains("OK") && !(line.ToUpper().Contains("NOT OK")))
                {
                    ev.Graphics.DrawString(line, printFont, Brushes.Green,
                       leftMargin, yPos, new StringFormat());
                }
                else if (line.ToUpper().Contains("SAFE") && !(line.ToUpper().Contains("NOT SAFE")))
                {
                    ev.Graphics.DrawString(line, printFont, Brushes.Green,
                       leftMargin, yPos, new StringFormat());
                }
                else
                {
                    ev.Graphics.DrawString(line, printFont, Brushes.Black,
                       leftMargin, yPos, new StringFormat());
                }
                count++;


                if (line.Contains("DESIGN SECTION SUMMARY"))
                {
                    if (file_name.StartsWith("DESIGN_REP"))
                        printFont = new Font("Lucida Console", 7);
                }
               
                if(file_name == "DESIGN_OF_CONTINUOUS_PSC_BOX_GIRDER")
                    printFont = new Font("Lucida Console", 5);


            }
            //ev.Graphics.DrawString(file_name , bol_f, Brushes.Blue, 20.0f, 1.0f, new StringFormat());
         

            // If more lines exist, print another page.
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }

        private void btn_print_Click(object sender, EventArgs e)
        {

        }

        private void tsb_print_Click(object sender, EventArgs e)
        {
            ToolStripButton tsb = sender as ToolStripButton;
            try
            {
                streamToPrint = new StringReader(rtbData.Text);
                try
                {
                    printFont = new Font("Lucida Console", 8);
                    //Chiranjit [2012 09 07]
                    //For Steel Truss Design Report
                    PageNo = 0;
                  
                    if (tsb.Name == tsb_page_setup.Name)
                    {
                        psd.Document = pd;
                        psd.ShowDialog();
                    }
                    else if (tsb.Name == tsb_print_prev.Name)
                    {
                        ppd.Document = pd;
                        ppd.Height = this.Height-40;
                        ppd.Width = this.Width;

                        
                        ppd.ShowDialog();
                    }
                    else if (tsb.Name == tsb_print.Name)
                    {
                        printDialog1.Document = pd;
                        if (printDialog1.ShowDialog() != DialogResult.Cancel)
                            pd.Print();
                    }
                }
                finally
                {
                    streamToPrint.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmb_step_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!is_internal)
            {
                select_text(cmb_step.Text);
                CurrentPosition = Lines.Count - 1;
            }
        }
        private void select_text(string txt)
        {
            try
            {
                int indx = rtbData.Find(txt);
                //if (hash_index[cmb_step.SelectedIndex] != null)
                if (indx != -1)
                {
                    //rtbData.SelectedText = cmb_step.Text;
                    //rtbData.Select((int)hash_index[cmb_step.SelectedIndex], cmb_step.Text.Length);
                    rtbData.Select(indx, txt.Length);
                    rtbData.ScrollToCaret();
                    //rtbData.SelectionBackColor = Color.Red;
                    rtbData.SelectionBackColor = Color.YellowGreen;

                    //rtbData.SelectionLength = cmb_step.Text.Length;

                    Lines.Remove(cmb_step.Items[0].ToString());
                    if (Lines.Contains(txt)) Lines.Remove(txt);
                    Lines.Add(txt);
                    Show_Next_Previous_Text();
                }
            }
            catch (Exception ex) { }
        }

        private void rtbData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F)
            {

                find = new frm_findcs(this, rtbData.SelectedText);
                find.Owner = this;
                find.Show();
            }
            else
                CurrentLine();
        }

        string lineText = "";
        bool is_internal = false;
        private void CurrentLine()
        {
            try
            {
                if (last_text != lineText)
                    last_text = lineText;
                //this.WordWrap = false;
                int cursorPosition = rtbData.SelectionStart;
                int lineIndex = rtbData.GetLineFromCharIndex(cursorPosition);
                int c = rtbData.GetPositionFromCharIndex(lineIndex).Y;
                lineText = rtbData.Lines[lineIndex];
                //lbl_line_col.Text = "Line : " + lineIndex + " , Col : " + c;
                lbl_line_col.Text = "Line No : " + (lineIndex + 1) + ((lineText != "") ? (",  Text : " + lineText) : "");
                Show_Next_Previous_Text();

                for (int i = 0; i < Step_Lines.Count - 1; i++)
                {
                    int d1 = (int)Step_Lines[i];
                    int d2 = (int)Step_Lines[i + 1];


                    if (lineIndex >= d1 && lineIndex < d2)
                    {
                        is_internal = true;
                        cmb_step.SelectedIndex = i + 1;
                        is_internal = false;
                        break;
                    }
                    if (lineIndex >= Step_Lines[Step_Lines.Count - 1])
                    {
                        is_internal = true;
                        cmb_step.SelectedIndex = Step_Lines.Count;
                        is_internal = false;
                        break;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void rtbData_MouseClick(object sender, MouseEventArgs e)
        {
            CurrentLine();
        }

        private void lbl_line_col_Click(object sender, EventArgs e)
        {
            select_text(lineText);
            //last_text = lineText;
            tssl_last.Text = ">>>>";
            CurrentPosition = Lines.Count - 2;

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
        }
        string last_text = "";
        private void tssl_last_Click(object sender, EventArgs e)
        {
            if (tssl_last.Text == ">>>>")
            {
                select_text(last_text);
                tssl_last.Text = "<<<<";
            }
            else
            {
                select_text(lineText);
                tssl_last.Text = ">>>>";
            }
        }

        private void tsb_prev_Click(object sender, EventArgs e)
        {
            //using (OpenFileDialog ofd = new OpenFileDialog())
            //{
            //    if (ofd.ShowDialog() != DialogResult.Cancel)
            //    {
            //        string lstrFile = ofd.FileName;

            //    Image img = AstraFunctionOne.Properties.Resources.BOX_1;
            //        //ev.Graphics.DrawImage(img,new Point(10, (int)(yPos+10)));

            //        //rtbData.LoadFile(lstrFile);

            //    rtbData.CreateGraphics().DrawImage(img, new Point(10, 10));

            //        //rtbData.
            //        //Bitmap myBitmap = new Bitmap(lstrFile);
            //        //// Copy the bitmap to the clipboard.
            //        //Clipboard.SetDataObject(myBitmap);
            //        //// Get the format for the object type.
            //        //DataFormats.Format myFormat = DataFormats.GetFormat(DataFormats.Bitmap);
            //        //// After verifying that the data can be pasted, paste
            //        //if (rtbData.CanPaste(myFormat))
            //        //{
            //        //    rtbData.Paste(myFormat);
                        
            //        //}
            //        //else
            //        //{
            //        //    MessageBox.Show("The data format that you attempted site" +
            //        //      " is not supported by this control.");
            //        //}
            //        ////Image img = Image.FromFile(ofd.FileName);
            //        ////Clipboard.SetImage(img);
            //        ////rtbData.Paste();
                    
            //    }
            //}

            if (((ToolStripButton)(sender)).Name == tsb_next.Name)
            {
                CurrentPosition++;
                if (CurrentPosition >= Lines.Count)
                    CurrentPosition = 0;
                if (Lines.Count > 0)
                    select_text(Lines[CurrentPosition]);
            }
            else if (((ToolStripButton)(sender)).Name == tsb_prev.Name)
            {
                CurrentPosition--;
                if (CurrentPosition <= -1)
                    CurrentPosition = Lines.Count - 1;
                if (Lines.Count > 0)
                    select_text(Lines[CurrentPosition]);
            }
        }

        private void Show_Next_Previous_Text()
        {
            try
            {
                //tsb_prev.ToolTipText == "Previous text.... ";
                //tsb_next.ToolTipText == "Next text...";
                if (CurrentPosition > 0)
                {
                    tsb_prev.ToolTipText = "Previous text is \n\n [\"" + Lines[CurrentPosition - 1] + "\"]"; ;
                }
                if (CurrentPosition < Lines.Count - 1)
                {
                    tsb_next.ToolTipText = "Next text is \n\n[\"" + Lines[CurrentPosition + 1] + "\"]";
                }
            }
            catch (Exception ex) { }
        }

        private void tsmi_land_scape_Click(object sender, EventArgs e)
        {
            //tsmi_land_scape.Checked = !tsmi_land_scape.Checked;
            pd.DefaultPageSettings.Landscape = tsmi_land_scape.Checked;
        }

        private void tssb_find_ButtonClick(object sender, EventArgs e)
        {

            find = new frm_findcs(this, rtbData.SelectedText);
            find.Owner = this;
            find.Show();

        }
        public List<string> list_text { get; set; }

        public string FindText
        {
            get
            {
                return rtbData.Text;
            }
        }

        public void Set_Text(int index, int length)
        {
            rtbData.Select(index, length);
            rtbData.ScrollToCaret();
            rtbData.SelectionBackColor = Color.Yellow;
            //rtbData
        }

        private void tslbl_notok_Click(object sender, EventArgs e)
        {

            find = new frm_findcs(this, "NOT OK");
            find.Owner = this;
            find.Show();
        }

        private void lstb_steps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsv_steps.SelectedItems.Count > 0)
            {
                select_text(lsv_steps.SelectedItems[0].Text.ToString());
                CurrentPosition = Lines.Count - 1;
            }
        }

    }
}
