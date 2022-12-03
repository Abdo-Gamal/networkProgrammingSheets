using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace abdo_section1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button2.Text = "connect";

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button2.Text = "lisetien";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "lisetien"&& radioButton2.Checked) {
                groupBox1.Enabled = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            textBox3.Clear();
        }
    }
}
