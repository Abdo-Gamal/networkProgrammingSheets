using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [Serializable]
        public class studen
        {

            public int id { get; set; }
            public string name { get; set; }
            public int mark { get; set; }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (FileStream f = new FileStream("abdo.txt", FileMode.OpenOrCreate)) 
            {
                studen studen = new studen();
                studen.id =  int.Parse(textBox1.Text);
                studen.name =textBox2.Text;
                studen.mark = int.Parse( textBox3.Text);

                BinaryFormatter s=new BinaryFormatter();
                s.Serialize(f, studen);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FileStream f = new FileStream("abdo.txt", FileMode.Open))
            {
                studen studen = new studen();
            
                BinaryFormatter s = new BinaryFormatter();
                studen st= s.Deserialize(f) as studen ;
                 textBox1.Text = st.id.ToString();
                textBox2.Text= st.name ;
                textBox3.Text= st.mark.ToString();

            }

        }
    }
}
