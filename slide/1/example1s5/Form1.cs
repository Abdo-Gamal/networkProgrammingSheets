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

namespace example1s5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
  
            using (Stream s = new FileStream("myFile.txt", FileMode.Create))
                     {
                     s.WriteByte(79); // O 01001111
                     s.WriteByte(79); // O 01001111
                     s.WriteByte(80); // P 01010000
             }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stream s = new FileStream(TextPath.Text, FileMode.Open);
             int i, j, k, m, n;
             i = s.ReadByte(); // O 79 01001111
             j = s.ReadByte(); // O 79 01001111
             k = s.ReadByte(); // P 80 01010000
             m = s.ReadByte(); // -1 EOF
             n = s.ReadByte(); // -1 EOF
             textBox1.Text = String.Format("{0} {1} {2} {3} {4}", i, j, k, m, n);
             s.Close();
        }
    }
}
