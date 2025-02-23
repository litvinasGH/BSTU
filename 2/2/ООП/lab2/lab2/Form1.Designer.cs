namespace lab2
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Add_button = new System.Windows.Forms.Button();
            this.Edit_pub = new System.Windows.Forms.Button();
            this.Edit_auth = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Pub_list = new System.Windows.Forms.ListBox();
            this.Author_list = new System.Windows.Forms.ListBox();
            this.textBox_price = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown_year = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown_count = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_size = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_genre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_title = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.книгиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.авторовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.издателейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.подсчётToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_year)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_count)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Файлы книг";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Add_button);
            this.groupBox1.Controls.Add(this.Edit_pub);
            this.groupBox1.Controls.Add(this.Edit_auth);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.Pub_list);
            this.groupBox1.Controls.Add(this.Author_list);
            this.groupBox1.Controls.Add(this.textBox_price);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.numericUpDown_year);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numericUpDown_count);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox_size);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_genre);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_title);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(400, 54);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(265, 484);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Добавление книги";
            // 
            // Add_button
            // 
            this.Add_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Add_button.Location = new System.Drawing.Point(8, 423);
            this.Add_button.Name = "Add_button";
            this.Add_button.Size = new System.Drawing.Size(246, 56);
            this.Add_button.TabIndex = 18;
            this.Add_button.Text = "Добавить";
            this.Add_button.UseVisualStyleBackColor = true;
            this.Add_button.Click += new System.EventHandler(this.Add_button_Click);
            // 
            // Edit_pub
            // 
            this.Edit_pub.Location = new System.Drawing.Point(134, 384);
            this.Edit_pub.Name = "Edit_pub";
            this.Edit_pub.Size = new System.Drawing.Size(75, 23);
            this.Edit_pub.TabIndex = 17;
            this.Edit_pub.Text = "Изменить";
            this.Edit_pub.UseVisualStyleBackColor = true;
            this.Edit_pub.Click += new System.EventHandler(this.Edit_pub_Click);
            // 
            // Edit_auth
            // 
            this.Edit_auth.Location = new System.Drawing.Point(8, 384);
            this.Edit_auth.Name = "Edit_auth";
            this.Edit_auth.Size = new System.Drawing.Size(75, 23);
            this.Edit_auth.TabIndex = 16;
            this.Edit_auth.Text = "Изменить";
            this.Edit_auth.UseVisualStyleBackColor = true;
            this.Edit_auth.Click += new System.EventHandler(this.Edit_auth_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(135, 267);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Издательство";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 267);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Авторы";
            // 
            // Pub_list
            // 
            this.Pub_list.FormattingEnabled = true;
            this.Pub_list.Location = new System.Drawing.Point(134, 283);
            this.Pub_list.Name = "Pub_list";
            this.Pub_list.Size = new System.Drawing.Size(120, 95);
            this.Pub_list.TabIndex = 13;
            // 
            // Author_list
            // 
            this.Author_list.FormattingEnabled = true;
            this.Author_list.Location = new System.Drawing.Point(8, 283);
            this.Author_list.Name = "Author_list";
            this.Author_list.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.Author_list.Size = new System.Drawing.Size(120, 95);
            this.Author_list.TabIndex = 12;
            // 
            // textBox_price
            // 
            this.textBox_price.Location = new System.Drawing.Point(8, 232);
            this.textBox_price.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_price.Name = "textBox_price";
            this.textBox_price.Size = new System.Drawing.Size(254, 20);
            this.textBox_price.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 216);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Цена доступа";
            // 
            // numericUpDown_year
            // 
            this.numericUpDown_year.Location = new System.Drawing.Point(8, 191);
            this.numericUpDown_year.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_year.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericUpDown_year.Name = "numericUpDown_year";
            this.numericUpDown_year.Size = new System.Drawing.Size(253, 20);
            this.numericUpDown_year.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 176);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Год";
            // 
            // numericUpDown_count
            // 
            this.numericUpDown_count.Location = new System.Drawing.Point(8, 155);
            this.numericUpDown_count.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_count.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericUpDown_count.Name = "numericUpDown_count";
            this.numericUpDown_count.Size = new System.Drawing.Size(253, 20);
            this.numericUpDown_count.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 136);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Кол-во страниц";
            // 
            // textBox_size
            // 
            this.textBox_size.Location = new System.Drawing.Point(8, 112);
            this.textBox_size.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_size.Name = "textBox_size";
            this.textBox_size.Size = new System.Drawing.Size(254, 20);
            this.textBox_size.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 96);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Вес файла (в кб)";
            // 
            // textBox_genre
            // 
            this.textBox_genre.Location = new System.Drawing.Point(8, 72);
            this.textBox_genre.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_genre.Name = "textBox_genre";
            this.textBox_genre.Size = new System.Drawing.Size(254, 20);
            this.textBox_genre.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 56);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Жанр";
            // 
            // textBox_title
            // 
            this.textBox_title.Location = new System.Drawing.Point(8, 34);
            this.textBox_title.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_title.Name = "textBox_title";
            this.textBox_title.Size = new System.Drawing.Size(254, 20);
            this.textBox_title.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Имя";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(675, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem,
            this.загрузитьToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // listBox1
            // 
            this.listBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(10, 54);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(386, 485);
            this.listBox1.TabIndex = 4;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_delete,
            this.подсчётToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // ToolStripMenuItem_delete
            // 
            this.ToolStripMenuItem_delete.Name = "ToolStripMenuItem_delete";
            this.ToolStripMenuItem_delete.Size = new System.Drawing.Size(118, 22);
            this.ToolStripMenuItem_delete.Text = "Удалить";
            this.ToolStripMenuItem_delete.Click += new System.EventHandler(this.ToolStripMenuItem_delete_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить";
            this.загрузитьToolStripMenuItem.Click += new System.EventHandler(this.загрузитьToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.книгиToolStripMenuItem,
            this.авторовToolStripMenuItem,
            this.издателейToolStripMenuItem});
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            // 
            // книгиToolStripMenuItem
            // 
            this.книгиToolStripMenuItem.Name = "книгиToolStripMenuItem";
            this.книгиToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.книгиToolStripMenuItem.Text = "Книги";
            this.книгиToolStripMenuItem.Click += new System.EventHandler(this.книгиToolStripMenuItem_Click);
            // 
            // авторовToolStripMenuItem
            // 
            this.авторовToolStripMenuItem.Name = "авторовToolStripMenuItem";
            this.авторовToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.авторовToolStripMenuItem.Text = "Авторов";
            this.авторовToolStripMenuItem.Click += new System.EventHandler(this.авторовToolStripMenuItem_Click);
            // 
            // издателейToolStripMenuItem
            // 
            this.издателейToolStripMenuItem.Name = "издателейToolStripMenuItem";
            this.издателейToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.издателейToolStripMenuItem.Text = "Издателей";
            this.издателейToolStripMenuItem.Click += new System.EventHandler(this.издателейToolStripMenuItem_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "xml";
            this.saveFileDialog1.FileName = "Books.xml";
            this.saveFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            this.saveFileDialog1.Title = "Сохранить данные";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "xml";
            this.openFileDialog1.FileName = "Books.xml";
            this.openFileDialog1.Filter = "\"XML files (*.xml)|*.xml|All files (*.*)|*.*\"";
            this.openFileDialog1.Title = "Загрузить данные";
            // 
            // подсчётToolStripMenuItem
            // 
            this.подсчётToolStripMenuItem.Name = "подсчётToolStripMenuItem";
            this.подсчётToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.подсчётToolStripMenuItem.Text = "Подсчёт";
            this.подсчётToolStripMenuItem.Click += new System.EventHandler(this.подсчётToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 549);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(691, 588);
            this.MinimumSize = new System.Drawing.Size(691, 588);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_year)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_count)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_title;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown_count;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_size;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_genre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_year;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_price;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_delete;
        private System.Windows.Forms.Button Add_button;
        private System.Windows.Forms.Button Edit_pub;
        private System.Windows.Forms.Button Edit_auth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox Pub_list;
        private System.Windows.Forms.ListBox Author_list;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem книгиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem авторовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem издателейToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem подсчётToolStripMenuItem;
    }
}

