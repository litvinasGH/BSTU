using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            numericUpDown_year.Maximum = DateTime.Now.Year;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if(listBox1.SelectedItems.Count == 0)
            {
                ToolStripMenuItem_delete.Enabled = false;
                ToolStripMenuItem_edit.Enabled = false;
            }
            else
            {
                ToolStripMenuItem_delete.Enabled = true;
                ToolStripMenuItem_edit.Enabled = true;
            }
        }

        private void ToolStripMenuItem_delete_Click(object sender, EventArgs e)
        {
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }
    }
}
