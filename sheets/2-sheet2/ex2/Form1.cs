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

namespace ex2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileCopy(textBox2.Text,textBox1.Text);

            textBox3.Clear();

            Stream s = new FileStream(textBox1.Text, FileMode.Open);
            int x = s.ReadByte();
            string str = "";
            while (x != -1)
            {
                str += String.Format("{0}", (char)x);
                x = s.ReadByte();
            }
            textBox3.Text = str;
            s.Close();
        }
    

        public static void FileCopy(string fromFile, string toFile)
        {
            //try
            //{
            //    using (FileStream FromFile = new FileStream(fromFile, FileMode.Open))
            //    {
            //        using (FileStream CopyFile = new FileStream(toFile, FileMode.Create))
            //        {
            //            int cur = FromFile.ReadByte();
            //            while (cur != -1)
            //            {
            //                CopyFile.WriteByte((byte)cur);
            //                 cur = FromFile.ReadByte();

            //            }

            //        }
            //    }


            //}
            //catch (FileNotFoundException e)
            //{
            //    Console.WriteLine("File {0} not found: ", e.FileName);
            //    throw;
            //}
            //catch (Exception)
            //{
            //    Console.WriteLine("Other file copy exception");
            //    throw;
            //}

            using( StreamReader sr = new StreamReader(fromFile))
            {
                using (StreamWriter sw = new StreamWriter(toFile))
                {
                    sw.WriteLine(sr.ReadToEnd());
                }
            }
        }
}
    }