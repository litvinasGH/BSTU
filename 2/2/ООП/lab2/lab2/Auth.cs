using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Auth : Form
    {
        BindingList<Author> authors;

        public Auth(BindingList<Author> authors)
        {
            InitializeComponent();
            this.authors = authors;

            listBox1.DataSource = authors;

            foreach (Control control in groupBox1.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.TextChanged += CheckForm;
                }
                else if (control is MonthCalendar monthCalendar)
                {
                    monthCalendar.DateChanged += CheckForm;
                }
            }


            monthCalendar1.MaxDate = DateTime.Today;
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
                !Regex.IsMatch(textBox_title.Text, pattern)
                )
            {
                Add_button.Enabled = false;
            }
            else
            {
                Add_button.Enabled = true;
            }

        }

        private void Auth_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
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

        private void Add_button_Click(object sender, EventArgs e)
        {
            Author author = new Author(textBox_title.Text, textBox_genre.Text, monthCalendar1.SelectionStart);
            authors.Add(author);
        }

        private void ToolStripMenuItem_delete_Click(object sender, EventArgs e)
        {
            authors.RemoveAt(listBox1.SelectedIndex);
        }
    }


}
