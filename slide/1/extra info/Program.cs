using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_info
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ex11();
            Console.ReadKey();
        }

        public static void ex1()
        {
            string fileName = "test.txt";
            using (FileStream fi = new FileStream(
                  fileName, FileMode.OpenOrCreate,FileAccess.ReadWrite))
            {
                fi.WriteByte(65);
                Console.WriteLine(fi.Length);
                Console.WriteLine(fi.CanRead);
                Console.WriteLine(fi.CanWrite);
                Console.WriteLine(fi.Position);
                Console.WriteLine(fi.CanSeek);
                Console.WriteLine(fi.CanTimeout);

            }




        }

        public static void ex2()
        {
            string fileName = "test.txt";
            using (FileStream fi = new FileStream(
                  fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
               byte []buf=new byte[fi.Length];
                int byteToRead =(int)fi.Length;
                int byteReaded = 0;
                while(byteToRead > 0)
                {
                    int n = fi.Read(buf, byteReaded, byteToRead);//return byte by oc buffer size
                    byteToRead-= n;
                    byteReaded += n;
                }
                foreach(var b in buf)
                {
                    Console.WriteLine(b);
                }
                using (FileStream fw = new FileStream(
             "write.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    fw.Write(buf, 0, buf.Length);

                }
            }

         



        }
        public static void ex3()
        {
            string fileName = "test3.txt";
            using (FileStream fi = new FileStream(
                  fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                fi.Seek(20,SeekOrigin.Begin);
                fi.WriteByte(65);
                fi.Position = 0;
                for(int i = 0; i < fi.Length; i++)
                {
                    Console.WriteLine(fi.ReadByte());
                }
            }





        }

        public static void ex4()
        {
            string fileName = "test4.txt";
            using(TextWriter fi=new StreamWriter(fileName))
            {
                fi.WriteLine("abdo gamal ahmed gaber ");//fi.weite.

            }

        }
        public static void ex5()
        {
            string fileName = "test4.txt";
            using (TextWriter fi = new StreamWriter(fileName))
            {
                fi.WriteLine("abdo gamal ahmed ");//fi.weite.

            }

        }

        public static void ex6()
        {
            string fileName = "test4.txt";
            using (TextReader fi = new StreamReader(fileName))
            {
                while (fi.Peek() > 0)
                {
                   Console.WriteLine((char) fi.Read());
                }
            }

        }
        public static void ex7()
        {
            string fileName = "test4.txt";
            using (TextReader fi = new StreamReader(fileName))
            {
                string line;
                while ( (line=fi.ReadLine()) != null )
                {
                    Console.WriteLine(line);
                }
            }

        }
        public static void ex8()
        {
            string fileName = "test5.txt";
          
                string[] line =
                {
                    "abdo",
                    "gamal",
                    "gaber"
                };

            File.WriteAllLines(fileName,line);

        }
        public static void ex9()
        {
            string fileName = "test6.txt";

            string text = "abdo Gamal Gaber ";
            File.WriteAllText(fileName, text);

        }
        public static void ex10()
        {
            string fileName = "test6.txt";

            string str = File.ReadAllText(fileName);
            Console.WriteLine(str);
        }
        public static void ex11()
        {
            string fileName = "test6.txt";

            string []str = File.ReadAllLines(fileName);
            foreach (string str2 in str)
            {
                Console.WriteLine(str2);

            }
        }

    }
}
