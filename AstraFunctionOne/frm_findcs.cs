using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AstraFunctionOne
{
    public partial class frm_findcs : Form
    {
        IFind ifnd;
        public frm_findcs(IFind ifind, string select_text)
        {
            InitializeComponent();
            ifnd = ifind;
            if (ifnd.list_text == null)
                ifnd.list_text = new List<string>();
            else
            {
                cmb_find.Items.AddRange(ifnd.list_text.ToArray());
            }
            cmb_find.Text = select_text;
        }

        private void frm_findcs_Load(object sender, EventArgs e)
        {
            btn_find_prev.Enabled = false;

            if (cmb_find.Text.ToUpper() == "NOT OK") Do_Find();
        }
        int current_index = -1;
        List<int> list_index = new List<int>();

        private void btn_find_Click(object sender, EventArgs e)
        {
            Do_Find();
        }

        private void Do_Find()
        {
            int indx = 0;

            if (cmb_find.Text == "")
            {
                MessageBox.Show("PLease Enter a word.", "View Result.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmb_find.Focus();
                return;
            }
            if (current_index == -1 && cmb_find.Text != "")
            {
                if (ifnd.list_text.Contains(cmb_find.Text))
                {
                    ifnd.list_text.Remove(cmb_find.Text);
                }
                ifnd.list_text.Insert(0, cmb_find.Text);
                //txt_find.DataSource = ifnd.list_text;
                cmb_find.Items.Clear();
                cmb_find.Items.AddRange(ifnd.list_text.ToArray());
                
                list_index.Clear();
                while (indx != -1)
                {
                    if(chk_case.Checked)
                        indx = ifnd.FindText.IndexOf(cmb_find.Text, indx, StringComparison.Ordinal);
                    else
                        indx = ifnd.FindText.IndexOf(cmb_find.Text, indx, StringComparison.OrdinalIgnoreCase);
                    if (indx != -1)
                    {
                        list_index.Add(indx);
                        indx += cmb_find.Text.Length;
                    }
                }
            }
            btn_find_prev.Enabled = (list_index.Count > 0);
            if (list_index.Count == 0)
            {
                MessageBox.Show("'" + cmb_find.Text + "' not found.", "View Result.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                grb_find.Text = "'" + cmb_find.Text + "' not found.";
            }

            else
            {
                grb_find.Text = list_index.Count + " items found.";
            }
            Find_Next();
        }
        void Find_Next()
        {
            current_index++;

            if (current_index == list_index.Count)
                current_index = 0;
            if (current_index >= 0 && current_index < list_index.Count)
            {
                ifnd.Set_Text(list_index[current_index], cmb_find.Text.Length);
                grb_find.Text = list_index.Count + " items found. [ " + (current_index + 1) + " / " + list_index.Count + " ]";
            }

        }
        void Find_Previous()
        {
            current_index--;

            if (current_index <= -1)
                current_index = list_index.Count - 1;
            if (current_index >= 0 && current_index < list_index.Count)
            {
                ifnd.Set_Text(list_index[current_index], cmb_find.Text.Length);
                grb_find.Text = list_index.Count + " items found. [ " + (current_index + 1) + " / " + list_index.Count + " ]";
            }
        }

        private void txt_find_TextChanged(object sender, EventArgs e)
        {
            current_index = -1;
            list_index.Clear();
            btn_find_prev.Enabled = false;
        }

        private void txt_find_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                Do_Find();
            else if (e.KeyChar == (char)Keys.Escape)
                this.Close();
        }

        private void btn_find_prev_Click(object sender, EventArgs e)
        {
            Find_Previous();
        }
    }
    public interface IFind
    {
        string FindText { get; }
        void Set_Text(int index, int length);
        List<string> list_text { get; set; }
    }
}
