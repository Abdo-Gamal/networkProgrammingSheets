using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Stream s = new FileStream("abdo.txt", FileMode.Create);
            //Random random = new Random(1);
            //for (int i = 0; i < 1000; i++)
            //{
            //    char buffer = (char)random.Next(1, 7);
            //    s.WriteByte((byte)buffer);
            //}
            //s.Close();

            //Stream ss = new FileStream("abdo.txt", FileMode.Open);

            //int[]arr=new int[9];
            //for (int i = 0; i < 1000; i++)
            //   arr[ ss.ReadByte()]++;

            //for (int i = 0; i < 6; i++)
            //    Console.WriteLine($"{i} : {arr[i]} " );

            //ss.Close();

            using (StreamWriter sr = new StreamWriter("textt.txt"))
            {
                Random random = new Random();
                for (int i = 0; i < 6; i++)
                {
                    int x = random.Next(1, 100);
                    sr.WriteLine(x);
                }
            }
            using (StreamReader sw = new StreamReader("textt.txt"))
            {
                int[] arr = new int[102];
                string num;
                int mx = -22;
                while ((num = sw.ReadLine()) != null) //
                   mx= Math.Max(mx, int.Parse(num));
               
                    Console.WriteLine($"{mx} ");
            }
        }
    }
}
