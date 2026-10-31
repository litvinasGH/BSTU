using System;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int num = 2;
                if (eth.Checked)
                    num = 8;
                else if (ten.Checked)
                    num = 10;
                else if (sixt.Checked)
                    num = 16;
                Calculator calc = new Calculator(textBox1.Text, textBox2.Text, num, radioButton_NO.Checked);

                int result;
                if (radioButton_AND.Checked)
                    result = calc.AND_Calc();
                else if (radioButton_OR.Checked)
                    result = calc.OR_Calc();
                else if (radioButton_XOR.Checked)
                    result = calc.XOR_Calc();
                else
                    result = calc.NO_Calc();


                string result_out = $"Бинарный: {Convert.ToString(result, 2)}\n" +
                    $"Восмеричный: {Convert.ToString(result, 8)}\n" +
                    $"Десятичный: {result}\n" +
                    $"Шестнадцатиричный: {Convert.ToString(result, 16).ToUpper()}";

                MessageBox.Show(result_out, "Результат", MessageBoxButtons.OK);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                var err = MessageBox.Show("Имеются пустые строки!",
                    ex.Message, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                switch (err)
                {
                    case DialogResult.Abort:
                        Application.Exit();
                        break;

                    case DialogResult.Retry:
                        button1_Click(this, new EventArgs());
                        break;

                    case DialogResult.Ignore:

                        break;
                }
            }
            catch (FormatException ex)
            {
                var err = MessageBox.Show("Неверный формат чисел!",
                    ex.Message, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                switch (err)
                {
                    case DialogResult.Abort:
                        Application.Exit();
                        break;

                    case DialogResult.Retry:
                        button1_Click(this, new EventArgs());
                        break;

                    case DialogResult.Ignore:

                        break;
                }
            }
            catch (ArgumentException ex)
            {
                var err = MessageBox.Show(ex.Message,
                    ex.Message, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                switch (err)
                {
                    case DialogResult.Abort:
                        Application.Exit();
                        break;

                    case DialogResult.Retry:
                        button1_Click(this, new EventArgs());
                        break;

                    case DialogResult.Ignore:

                        break;
                }
            }
            catch (Exception ex)
            {

                var err = MessageBox.Show(ex.ToString(), ex.Message,
                    MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                switch (err)
                {
                    case DialogResult.Abort:
                        Application.Exit();
                        break;

                    case DialogResult.Retry:
                        button1_Click(this, new EventArgs());
                        break;

                    case DialogResult.Ignore:

                        break;
                }
            }
        }

        private void radioButton_NO_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_NO.Checked)
            {
                textBox2.Enabled = false;
            }
            else
            {
                textBox2.Enabled = true;
            }
        }
    }
}
