using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
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

            fb.ListChanged += (s, e) => UpdateStatusCount();
            listBox1.DataSource = fb;
            Author_list.DataSource = authors;
            Pub_list.DataSource = publishers;
            Author_list.DisplayMember = "fullName";
            Pub_list.DisplayMember = "Name";

            this.KeyPreview = true; 
            this.KeyDown += MainForm_KeyDown;

            
            UpdateStatusCount();
            UpdateLastAction("Запуск программы");
        }


        private void UpdateStatusCount()
        {
            statusStrip1.Items["lblCount"].Text = $"Объектов: {fb.Count}";
        }
        private string _lastAction = "Нет";

        private void UpdateLastAction(string action)
        {
            _lastAction = action;
            statusStrip1.Items["lblLastAction"].Text = $"Последнее действие: {_lastAction}";
            UpdateDateTime();
        }
        private void UpdateDateTime()
        {
            statusStrip1.Items["lblDateTime"].Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                поискToolStripMenuItem_Click(sender, e); 
                e.Handled = true; 
            }

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (listBox1.SelectedItems.Count != 1)
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
            UpdateLastAction("Удаление книги");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numericUpDown_year.Maximum = DateTime.Now.Year;
        }

        //private void CheckForm(object sender, EventArgs e)
        //{
        //    foreach (Control control in groupBox1.Controls)
        //    {
        //        if (control is TextBox textBox)
        //        {
        //            if (string.IsNullOrWhiteSpace(textBox.Text))
        //            {
        //                Add_button.Enabled = false;
        //                return;
        //            }
        //        }
        //        else if (control is ListBox listBox)
        //        {
        //            if (listBox.SelectedItems.Count == 0)
        //            {
        //                Add_button.Enabled = false;
        //                return;
        //            }
        //        }
        //    }
        //    string pattern = @"^[a-zA-Zа-яА-Я\s]+$";
        //    if (!double.TryParse(textBox_size.Text, out var size) ||
        //        !double.TryParse(textBox_price.Text, out var size2) ||
        //        !Regex.IsMatch(textBox_genre.Text, pattern) ||
        //        size < 0 || size2 < 0
        //        )
        //    {
        //        Add_button.Enabled = false;
        //    }
        //    else
        //    {
        //        Add_button.Enabled = true;
        //    }

        //}


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
                Double.TryParse(textBox_size.Text, out double o) ? o : -1,
                (int)numericUpDown_count.Value, (Publisher)Pub_list.SelectedItem,
                (int)numericUpDown_year.Value, selectedAuthors,
                Double.TryParse(textBox_price.Text, out double op) ? op : -1
                );

            if (!ModelValidator.Validate(fileB, out var errors))
            {
                MessageBox.Show($"Ошибки:\n- {string.Join("\n- ", errors.Select(x => x.ErrorMessage))}", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            fb.Add(fileB);
            UpdateLastAction("Добавление книги");
        }

        private void Edit_auth_Click(object sender, EventArgs e)
        {
            Auth form2 = new Auth(authors);

            form2.ShowDialog();
            UpdateLastAction("Изменения списка авторов");
        }

        private void Edit_pub_Click(object sender, EventArgs e)
        {
            Publ form2 = new Publ(publishers);
            form2.ShowDialog();
            UpdateLastAction("Изменение списка издателей");
        }



        private void книгиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fb.Clear();
            UpdateLastAction("Удаление книг");
        }

        private void авторовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            authors.Clear();
            UpdateLastAction("Удаление авторов");
        }

        private void издателейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            publishers.Clear();
            UpdateLastAction("Удаление издателей");
        }


        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataContainer dataContainer = new DataContainer(fb, authors, publishers);
                DataManager.SaveToXml(dataContainer, saveFileDialog1.FileName);
                UpdateLastAction("Сохранение");
            }
        }



        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
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
                    UpdateLastAction("Загрузка");

                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Файл не содержит поддержимоваю информацию или имеет не поддерживаемый формат", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception) { throw; }


        }

        private void подсчётToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double res = (fb[listBox1.SelectedIndex].Price * 0.7) / fb[listBox1.SelectedIndex].AuthorList.Count;
            MessageBox.Show($"Каждый автор данной книги получит {res} д.е. с человека", "Подсчёт");
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Версия: {Application.ProductVersion}\nСоздатель: Качинскас Вацловас Вацловович",
                "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information
                );
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindForm findForm = new FindForm();
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                string request = findForm.GetRequest();
                List<FileBook> fbf = new List<FileBook>();

                bool isRegexValid = false;
                Regex regex = null;

                try
                {
                    regex = new Regex(request.Trim(), RegexOptions.IgnoreCase);
                    isRegexValid = true;
                }
                catch
                {
                    isRegexValid = false;
                }

                if (string.IsNullOrWhiteSpace(request))
                    fbf = fb.ToList();
                else
                {
                    if (isRegexValid)
                    {
                        fbf = fb.Where(book =>
                            regex.IsMatch(book.Genre ?? "") ||
                            regex.IsMatch(book.FileSize.ToString()) ||
                            regex.IsMatch(book.Title ?? "") ||
                            regex.IsMatch(book.CountOfPage.ToString()) ||
                            regex.IsMatch(book.Publisher.Name ?? "") ||
                            regex.IsMatch(book.Year.ToString()) ||
                            (book.AuthorList?.Any(a => regex.IsMatch(a.FullName)) ?? false) ||
                            regex.IsMatch(book.Price.ToString())
                            ).ToList();
                    }
                    else
                    {
                        request = request.Trim().ToLower();

                        fbf = fb
                            .Where(book =>
                                (book.Genre?.ToLower() ?? "").Contains(request) ||
                                book.FileSize.ToString().Contains(request) ||
                                (book.Title?.ToLower() ?? "").Contains(request) ||
                                book.CountOfPage.ToString().Contains(request) ||
                                (book.Publisher.Name?.ToLower() ?? "").Contains(request) ||
                                book.Year.ToString().Contains(request) ||
                                (book.AuthorList?.Any(a => a.FullName.ToLower().Contains(request)) ?? false) ||
                                book.Price.ToString().Contains(request))
                            .ToList();
                    }
                }

                switch (findForm.GetSave())
                {
                    case false:
                        listBox1.ClearSelected();

                        var itemsCopy = listBox1.Items.Cast<object>().ToList();

                        foreach (var item in itemsCopy)
                        {
                            if (item is FileBook book && fbf.Contains(book))
                            {
                                listBox1.SetSelected(listBox1.Items.IndexOf(item), true);
                            }
                        }
                        break;

                    case true:

                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            DataContainer dataContainer = new DataContainer(new BindingList<FileBook>(fbf), authors, publishers);
                            DataManager.SaveToXml(dataContainer, saveFileDialog1.FileName);
                        }
                        break;
                }
            }
        }

        private void имениToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fb = new BindingList<FileBook>(fb.OrderBy(book => book.Title).ToList());
            listBox1.DataSource = fb;
            UpdateLastAction("Сортировка");
        }

        private void весToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fb = new BindingList<FileBook>(fb.OrderBy(book => book.FileSize).ToList());
            listBox1.DataSource = fb;
            UpdateLastAction("Сортировка");
        }

    }
}
