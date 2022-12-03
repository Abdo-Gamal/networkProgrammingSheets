using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;

namespace test
{
    internal class Program
    {
        [Serializable]
        public class emp
        {
            public string name;

        }

        string x;
        public void func1()
        {
            while (true)
            {
                //lock (this)
                //{
                o1.WaitOne();
                using (FileStream f=new FileStream("abd",FileMode.Create))
                {
                    BinaryFormatter s = new BinaryFormatter();
                    s.Serialize(f, new emp { name = "abdo gamal gaber" });
                    s.Serialize(f, new emp { name = "abdo2" });

                }
                using (FileStream f = new FileStream("abd", FileMode.Open))
                {
                    BinaryFormatter s = new BinaryFormatter();
                   emp e1= s.Deserialize(f)as emp;

                }
                o2.Set();
               // }
            }
        }
               
        public void func2()
        {
            while (true)
            {
                //lock (this)
                //{
                o2.WaitOne();
                    Console.WriteLine("func 2.....");
                    x = "222222";
                    Console.WriteLine(x);
                o1.Set();
                //}
            }
        }
        public Program()
        {
            Thread th1 = new Thread(new ThreadStart(func1));
            Thread th2 = new Thread(new ThreadStart(func1));
            th1.Start();
           // th2.Start();
        }
        AutoResetEvent o1 = new AutoResetEvent(true);
        AutoResetEvent o2 = new AutoResetEvent(false);

        static void Main(string[] args)
        {

            Program p = new Program();


        }
    }

}
