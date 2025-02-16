namespace lab1
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
            this.RadioBox = new System.Windows.Forms.GroupBox();
            this.radioButton_NO = new System.Windows.Forms.RadioButton();
            this.radioButton_XOR = new System.Windows.Forms.RadioButton();
            this.radioButton_OR = new System.Windows.Forms.RadioButton();
            this.radioButton_AND = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ten = new System.Windows.Forms.RadioButton();
            this.sixt = new System.Windows.Forms.RadioButton();
            this.eth = new System.Windows.Forms.RadioButton();
            this.bin = new System.Windows.Forms.RadioButton();
            this.RadioBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RadioBox
            // 
            this.RadioBox.Controls.Add(this.radioButton_NO);
            this.RadioBox.Controls.Add(this.radioButton_XOR);
            this.RadioBox.Controls.Add(this.radioButton_OR);
            this.RadioBox.Controls.Add(this.radioButton_AND);
            this.RadioBox.Location = new System.Drawing.Point(9, 10);
            this.RadioBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RadioBox.Name = "RadioBox";
            this.RadioBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RadioBox.Size = new System.Drawing.Size(135, 109);
            this.RadioBox.TabIndex = 0;
            this.RadioBox.TabStop = false;
            this.RadioBox.Text = "Действие";
            // 
            // radioButton_NO
            // 
            this.radioButton_NO.AutoSize = true;
            this.radioButton_NO.Location = new System.Drawing.Point(5, 84);
            this.radioButton_NO.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton_NO.Name = "radioButton_NO";
            this.radioButton_NO.Size = new System.Drawing.Size(40, 17);
            this.radioButton_NO.TabIndex = 3;
            this.radioButton_NO.TabStop = true;
            this.radioButton_NO.Text = "НЕ";
            this.radioButton_NO.UseVisualStyleBackColor = true;
            this.radioButton_NO.CheckedChanged += new System.EventHandler(this.radioButton_NO_CheckedChanged);
            // 
            // radioButton_XOR
            // 
            this.radioButton_XOR.AutoSize = true;
            this.radioButton_XOR.Location = new System.Drawing.Point(5, 62);
            this.radioButton_XOR.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton_XOR.Name = "radioButton_XOR";
            this.radioButton_XOR.Size = new System.Drawing.Size(126, 17);
            this.radioButton_XOR.TabIndex = 2;
            this.radioButton_XOR.TabStop = true;
            this.radioButton_XOR.Text = "Исключающее ИЛИ";
            this.radioButton_XOR.UseVisualStyleBackColor = true;
            // 
            // radioButton_OR
            // 
            this.radioButton_OR.AutoSize = true;
            this.radioButton_OR.Location = new System.Drawing.Point(5, 40);
            this.radioButton_OR.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton_OR.Name = "radioButton_OR";
            this.radioButton_OR.Size = new System.Drawing.Size(49, 17);
            this.radioButton_OR.TabIndex = 1;
            this.radioButton_OR.TabStop = true;
            this.radioButton_OR.Text = "ИЛИ";
            this.radioButton_OR.UseVisualStyleBackColor = true;
            // 
            // radioButton_AND
            // 
            this.radioButton_AND.AutoSize = true;
            this.radioButton_AND.Checked = true;
            this.radioButton_AND.Location = new System.Drawing.Point(5, 18);
            this.radioButton_AND.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton_AND.Name = "radioButton_AND";
            this.radioButton_AND.Size = new System.Drawing.Size(33, 17);
            this.radioButton_AND.TabIndex = 0;
            this.radioButton_AND.TabStop = true;
            this.radioButton_AND.Text = "И";
            this.radioButton_AND.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(151, 69);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(203, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Первое число";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 97);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Второе число";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(151, 112);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(203, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(77, 152);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(215, 58);
            this.button1.TabIndex = 5;
            this.button1.Text = "Выполнить вычесление";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ten);
            this.groupBox1.Controls.Add(this.sixt);
            this.groupBox1.Controls.Add(this.eth);
            this.groupBox1.Controls.Add(this.bin);
            this.groupBox1.Location = new System.Drawing.Point(151, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(202, 41);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Представление";
            // 
            // ten
            // 
            this.ten.AutoSize = true;
            this.ten.Location = new System.Drawing.Point(95, 17);
            this.ten.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ten.Name = "ten";
            this.ten.Size = new System.Drawing.Size(47, 17);
            this.ten.TabIndex = 1;
            this.ten.TabStop = true;
            this.ten.Text = "DEC";
            this.ten.UseVisualStyleBackColor = true;
            // 
            // sixt
            // 
            this.sixt.AutoSize = true;
            this.sixt.Location = new System.Drawing.Point(146, 17);
            this.sixt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.sixt.Name = "sixt";
            this.sixt.Size = new System.Drawing.Size(47, 17);
            this.sixt.TabIndex = 1;
            this.sixt.TabStop = true;
            this.sixt.Text = "HEX";
            this.sixt.UseVisualStyleBackColor = true;
            // 
            // eth
            // 
            this.eth.AutoSize = true;
            this.eth.Location = new System.Drawing.Point(50, 17);
            this.eth.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.eth.Name = "eth";
            this.eth.Size = new System.Drawing.Size(47, 17);
            this.eth.TabIndex = 1;
            this.eth.TabStop = true;
            this.eth.Text = "OCT";
            this.eth.UseVisualStyleBackColor = true;
            // 
            // bin
            // 
            this.bin.AutoSize = true;
            this.bin.Checked = true;
            this.bin.Location = new System.Drawing.Point(5, 17);
            this.bin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bin.Name = "bin";
            this.bin.Size = new System.Drawing.Size(43, 17);
            this.bin.TabIndex = 0;
            this.bin.TabStop = true;
            this.bin.Text = "BIN";
            this.bin.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 227);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.RadioBox);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximumSize = new System.Drawing.Size(386, 266);
            this.MinimumSize = new System.Drawing.Size(386, 266);
            this.Name = "Form1";
            this.Text = "Бинарный калькулятор";
            this.RadioBox.ResumeLayout(false);
            this.RadioBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox RadioBox;
        private System.Windows.Forms.RadioButton radioButton_NO;
        private System.Windows.Forms.RadioButton radioButton_XOR;
        private System.Windows.Forms.RadioButton radioButton_OR;
        private System.Windows.Forms.RadioButton radioButton_AND;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton bin;
        private System.Windows.Forms.RadioButton ten;
        private System.Windows.Forms.RadioButton sixt;
        private System.Windows.Forms.RadioButton eth;
    }
}

