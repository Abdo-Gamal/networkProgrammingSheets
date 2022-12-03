using System;
using System.IO;
using System.Text;
public class TextWriterProg
{
    public static void Main()
    {
        string str = "A æ u å æ ø i æ å",
      strEquiv = "A \u00E6 u \u00E5 \u00E6 \u00F8 i \u00E6 \u00E5";

        TextWriter tw1 = new StreamWriter(                         // Iso-Latin-1
                      "f-iso.txt",
              Encoding.GetEncoding("iso-8859-1")),

        tw2 = new StreamWriter(                         // UTF-8
                      new FileStream("f-utf8.txt", FileMode.Create),
                      new UTF8Encoding()),
        tw3 = new StreamWriter(                         // UTF-16              
                      new FileStream("f-utf16.txt", FileMode.Create),
                      new UnicodeEncoding());

        tw1.WriteLine(str); tw1.WriteLine(strEquiv);
        tw2.WriteLine(str); tw2.WriteLine(strEquiv);
        tw3.WriteLine(str); tw3.WriteLine(strEquiv);
        tw1.Close();
        tw2.Close();
        tw3.Close();


        //////////////////////////////////
    }
}

//using System;
//using System.IO;

//public class TextSimpleTypes
//{
//    class Point
//    {
//        public Point(int x,int y)
//        {
//            this.x = x;
//            this.y = y;
//        }
//        public int x;
//        public int y;

//    }
//    class Die
//    {
//        public Die(int xDie)
//        {
//            this.x = x;
//        }
//        public int x;

//    }
//    public static void Main()
//    {

//        using (TextWriter tw = new StreamWriter("simple-types.txt"))
//        {
//            tw.Write(5); tw.WriteLine();
//            tw.Write(5.5); tw.WriteLine();
//            tw.Write(5555M); tw.WriteLine();
//            tw.Write(5 == 6); tw.WriteLine();
//        }

//        using (TextWriter twnst = new StreamWriter("non-simple-types.txt"))
//        {
//            twnst.Write(new Point(1, 2)); 
//            twnst.WriteLine();
//            twnst.Write(new Die(6));
//            twnst.WriteLine();
//        }

//    }
//}
