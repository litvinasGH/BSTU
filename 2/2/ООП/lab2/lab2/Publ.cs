using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lab2
{
    public partial class Publ : Form
    {
        BindingList<Publisher> publishers;

        public Publ(BindingList<Publisher> publishers)
        {
            InitializeComponent();
            this.publishers = publishers;

            listBox1.DataSource = publishers;

            foreach (Control control in groupBox1.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.TextChanged += CheckForm;
                }
                else if (control is NumericUpDown numericUpDown)
                {
                    numericUpDown.ValueChanged += CheckForm;
                }
            }

            numericUpDown_year.Maximum = DateTime.Now.Year;


        }

        private void CheckForm(object sender, EventArgs e)
        {
            foreach (Control control in groupBox1.Controls)
            {
                if (control is TextBox textBox)
                {
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        Add_button.Enabled = false;
                        return;
                    }
                }
            }
            string pattern = @"^[a-zA-Zа-яА-Я\s]+$";
            if (
                !Regex.IsMatch(textBox_genre.Text, pattern) ||
                !Regex.IsMatch(textBox_title.Text, pattern) ||
                !Regex.IsMatch(textBox_city.Text, pattern)
                )
            {
                Add_button.Enabled = false;
            }
            else
            {
                Add_button.Enabled = true;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (listBox1.SelectedItems.Count == 0)
            {
                ToolStripMenuItem_delete.Enabled = false;
            }
            else
            {
                ToolStripMenuItem_delete.Enabled = true;
            }
        }

        private void ToolStripMenuItem_delete_Click(object sender, EventArgs e)
        {
            publishers.RemoveAt(listBox1.SelectedIndex);
        }

        private void Publ_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void Add_button_Click(object sender, EventArgs e)
        {
            Publisher publisher = new Publisher(textBox_title.Text,
                textBox_genre.Text, textBox_city.Text, (int)numericUpDown_year.Value, radioButton1.Checked);
            publishers.Add(publisher);
        }
    }
}
