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

namespace ex7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }

   
        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Clear();

            string fileName =textBox1.Text; // The current source file
            FileInfo fi = new FileInfo(fileName); // This source file
            string thisFile = fi.FullName;
            string thisDir = Directory.GetParent(thisFile).FullName;

            string[] dirs = Directory.GetDirectories(thisDir);
            string[] fils = Directory.GetFiles(thisDir);


            listView1.Clear();
            listView1.Columns.Add("folder");
            listView1.Columns.Add("Description");

            foreach (string d in dirs) {
                if (listView1.Items.ContainsKey(d) == false) 
                listView1.Items.Add(d, 0); 
            }
            foreach (string f in fils)
            {
                if (listView1.Items.ContainsKey(f) == false)

                    listView1.Items.Add(f, 1);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.Text == "largeIcon")
            {
                listView1.View = View.LargeIcon;
            }
            else if (comboBox1.Text == "Details")
            {
                listView1.View = View.Details;

            }
            else if (comboBox1.Text == "smallIcon")
            {
                listView1.View = View.SmallIcon;
            }
            else if (comboBox1.Text == "list")
            {
                listView1.View = View.List;
            }
            else if (comboBox1.Text == "till")
                listView1.View = View.Tile;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            textBox1.Text= listView1.SelectedItems[0].Text;
             string fileName2 = listView1.SelectedItems[0].Text; // The current source file
            FileInfo fi = new FileInfo(fileName2); // This source file
            string thisFile = fi.FullName;
          //  string thisDir = Directory.GetParent(thisFile).FullName;

            string[] dirs = Directory.GetDirectories(fileName2);
            string[] fils = Directory.GetFiles(fileName2);
            string fileName  = listView1.SelectedItems[0].Text;
            textBox1.Text = fileName;

            listView1.Clear();
            listView1.Columns.Add("folder");
            listView1.Columns.Add("Description");

            foreach (string d in dirs)
            {
                if (listView1.Items.ContainsKey(d) == false)
                    listView1.Items.Add(d, 0);
            }
            foreach (string f in fils)
            {
                if (listView1.Items.ContainsKey(f) == false)

                    listView1.Items.Add(f, 1);
            }
        }
    }
}
   