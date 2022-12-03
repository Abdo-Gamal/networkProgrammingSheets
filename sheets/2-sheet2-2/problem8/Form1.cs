using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace problem8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            config c= new config(textBox1.Text, checkBox1.Checked,int.Parse( textBox2.Text));
            c.date = dateTimePicker1.Value;
            using (FileStream strm = new FileStream("config.txt", FileMode.OpenOrCreate))
            {
                IFormatter fmt = new BinaryFormatter();
                fmt.Serialize(strm, c);
               
            }
            // -----------------------------------------------------------
            c = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FileStream strm = new FileStream("config.txt", FileMode.Open))
            {
                IFormatter fmt = new BinaryFormatter();
                config c = fmt.Deserialize(strm) as config;
                textBox1.Text = c.ConnectionString;
                checkBox1.Checked = c.RLValue==0?false:true;
                textBox2.Text=c.RLValue.ToString();
                dateTimePicker1.Value= c.date;
             
            }
        }
        [Serializable]
        public class abdo
        {
            public string name { get; set; }
            [OnDeserializing]
            internal void OnSerializingMethod()
            {
                Console.WriteLine("serializing . . . .");

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            using (FileStream x=new FileStream("abdo.txt",FileMode.Create))
            {
                BinaryFormatter b =new  BinaryFormatter();
                b.Serialize(x,new abdo() { name="gamal"});
            }
            using (FileStream x = new FileStream("abdo.txt", FileMode.Open))
            {
                BinaryFormatter b = new BinaryFormatter();
               abdo c= b.Deserialize(x)as abdo;
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
    