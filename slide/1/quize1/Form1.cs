using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quize1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filename= textBox1.Text;
            using(StreamWriter sr=new StreamWriter(filename))
            {
                foreach(var line in richTextBox1.Text)
                {
                    sr.Write(line);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            string filename = textBox1.Text;
            using (StreamReader sr = new StreamReader(filename))
            {
               string str=   sr.ReadToEnd();

                richTextBox1.Text = str;
            }

        }
    }
}
