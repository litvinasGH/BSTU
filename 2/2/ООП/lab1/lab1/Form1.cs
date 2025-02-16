using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (bin.Checked)
                    num = 2;
                else if (eth.Checked)
                    num = 8;
                else if (ten.Checked)
                    num = 10;
                else if (sixt.Checked)
                    num = 16;
                Calculator calc = new Calculator(textBox1.Text, textBox2.Text, num, radioButton_NO.Checked);

                int result = 0;
                if (radioButton_AND.Checked)
                    result = calc.AND_Calc();
                else if (radioButton_OR.Checked)
                    result = calc.OR_Calc();
                else if (radioButton_XOR.Checked)
                    result = calc.XOR_Calc();
                else if (radioButton_NO.Checked)
                    result = calc.NO_Calc();


                string result_out = "Бинарный"
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
