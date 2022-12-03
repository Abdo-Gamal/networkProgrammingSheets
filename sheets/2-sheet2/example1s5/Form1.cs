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
            string str = textBox1.Text;
            //using (Stream s = new FileStream(TextPath.Text, FileMode.Create))
            //{
            //    foreach (string str2 in str.Split('\n'))
            //     {

            //        foreach (char str3 in str2)
            //        {
            //            s.WriteByte((byte)str3); // O 01001111
            //        }
            //    }
            //}
            using (StreamWriter sr = new StreamWriter(TextPath.Text))
            {
                sr.WriteLine(str);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            //Stream s = new FileStream(TextPath.Text, FileMode.Open);
            //int x = s.ReadByte();
            //string str = "";
            //while (x != -1)
            //{
            //    str += String.Format("{0}", x);
            //     x = s.ReadByte();
            //}
            //textBox1.Text = str;
            //s.Close();
            //using(StreamReader sr=new StreamReader(TextPath.Text)){

            //    string str=sr.ReadToEnd();
            //    textBox1.Text = str;

            //}
            using (StreamReader sr = new StreamReader(TextPath.Text))
            {
                string line="";
                while ((line=sr.ReadLine()) != null)
                {
                    textBox1.Text += line;
                }


            }
        }
    }
}