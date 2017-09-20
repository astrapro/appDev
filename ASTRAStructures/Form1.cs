using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AstraInterface.DataStructure;

namespace ASTRAStructures
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> lsi = new List<int>();






            List<string> list = new List<string>(richTextBox1.Lines);




            foreach (var item in list)
            {
                lsi.AddRange(MyList.Get_Array_Intiger(item).ToArray());
            }


            lsi.Sort();

            listBox1.Items.Clear();
            foreach (var item in lsi)
            {
                listBox1.Items.Add(item);
            }


            List<int> miss = new List<int>();

            for (int i = 1; i < lsi.Count; i++)
            {
                if(lsi[i] - lsi[i-1] > 1)
                {
                    for (int j = lsi[i-1]+1; j < lsi[i]; j++)
                    {
                        miss.Add(j);
                    }
                }
            }

            miss.Sort();





            string ss = MyList.Get_Array_Text(lsi);



            string saz = MyList.Get_Array_Text(miss);

            lsi.Clear();

            lsi = MyList.Get_Array_Intiger(ss);


            textBox1.Text = ss;
            textBox2.Text = saz;


            list.Clear();

            foreach (var item in lsi)
            {
                list.Add(item + " PRISMATIC AX 0.326");
            }

            richTextBox1.Lines = list.ToArray();

        }
    }
}
