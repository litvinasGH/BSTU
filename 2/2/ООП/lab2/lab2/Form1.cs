using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form1 : Form
    {
        BindingList<FileBook> fb;
        BindingList<Author> authors;
        BindingList<Publisher> publishers;
        public Form1()
        {
            InitializeComponent();

            fb = new BindingList<FileBook>();
            authors = new BindingList<Author>();
            publishers = new BindingList<Publisher>();

            listBox1.DataSource = fb;
            Author_list.DataSource = authors;
            Pub_list.DataSource = publishers;
            Author_list.DisplayMember = "fullName";
            Pub_list.DisplayMember = "Name";
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (listBox1.SelectedItems.Count == 0)
            {
                ToolStripMenuItem_delete.Enabled = false;
                подсчётToolStripMenuItem.Enabled = false;
            }
            else
            {
                ToolStripMenuItem_delete.Enabled = true;
                подсчётToolStripMenuItem.Enabled = true;
            }
        }

        private void ToolStripMenuItem_delete_Click(object sender, EventArgs e)
        {
            fb.RemoveAt(listBox1.SelectedIndex);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numericUpDown_year.Maximum = DateTime.Now.Year;
            Add_button.Enabled = false;
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
                else if (control is ListBox listBox)
                {
                    listBox.SelectedIndexChanged += CheckForm;
                }
            }
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
                else if (control is ListBox listBox)
                {
                    if (listBox.SelectedItems.Count == 0)
                    {
                        Add_button.Enabled = false;
                        return;
                    }
                }
            }
            Add_button.Enabled = true;
            if (!double.TryParse(textBox_size.Text, out var size) ||
                !double.TryParse(textBox_price.Text, out var size2))
            {
                Add_button.Enabled = false;
            }

        }


        private void Add_button_Click(object sender, EventArgs e)
        {
            List<Author> selectedAuthors = new List<Author>();

            foreach (var selectedItem in Author_list.SelectedItems)
            {
                if (selectedItem is Author author)
                {
                    selectedAuthors.Add(author);
                }
            }
            FileBook fileB = new FileBook(
                textBox_title.Text, textBox_genre.Text,
                Convert.ToDouble(textBox_size.Text),
                (int)numericUpDown_count.Value, (Publisher)Pub_list.SelectedItem,
                (int)numericUpDown_year.Value, selectedAuthors,
                Convert.ToDouble(textBox_price.Text)
                );
            fb.Add(fileB);
        }

        private void Edit_auth_Click(object sender, EventArgs e)
        {
            Auth form2 = new Auth(authors);

            form2.ShowDialog();
        }

        private void Edit_pub_Click(object sender, EventArgs e)
        {
            Publ form2 = new Publ(publishers);
            form2.ShowDialog();
        }



        private void книгиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fb.Clear();
        }

        private void авторовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            authors.Clear();
        }

        private void издателейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            publishers.Clear();
        }


        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataContainer dataContainer = new DataContainer(fb, authors, publishers);
                DataManager.SaveToXml(dataContainer, saveFileDialog1.FileName);
            }
        }

  

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataContainer data = DataManager.LoadFromXml(openFileDialog1.FileName);
                fb.Clear();
                authors.Clear();
                publishers.Clear();
                fb = data.FileBooks;
                authors = data.Authors;
                publishers = data.Publishers;

                listBox1.DataSource = fb;
                Author_list.DataSource = authors;
                Pub_list.DataSource = publishers;

            }
        }

        private void подсчётToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double res = (fb[listBox1.SelectedIndex].Price * 0.7) / fb[listBox1.SelectedIndex].AuthorList.Count;
            MessageBox.Show($"Каждый автор данной книги получит {res} д.е. с человека", "Подсчёт");
        }
    }
}
