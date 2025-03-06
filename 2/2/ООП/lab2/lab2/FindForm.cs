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
    public partial class FindForm : Form
    {
        private string request;
        private bool save;
        public FindForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            request = textBox1.Text;
            save = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public bool GetSave()
        {
            return save;
        }
        public string GetRequest()
        {
            return request;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            request = textBox1.Text;
            save = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
