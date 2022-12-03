using System;
using System.IO;
using System.Text;

public class TextReaderProg
{
    public static void Main()
    {

        using (
		  TextReader tr1 = new StreamReader(
                            new FileStream("f-iso.txt", FileMode.Open),
                            Encoding.GetEncoding("iso-8859-1")))
        {
            Console.WriteLine(tr1.ReadLine()); Console.WriteLine(tr1.ReadLine());

        }


        using (TextReader tr2 = new StreamReader(
          new FileStream("f-utf8.txt", FileMode.Open),
          new UTF8Encoding()))
        {
            Console.WriteLine(tr2.ReadLine()); Console.WriteLine(tr2.ReadLine());

        }
        using (TextReader tr3 = new StreamReader(             // UTF-16
                                   new FileStream("f-utf16.txt", FileMode.Open),
                                   new UnicodeEncoding()))
        {
            Console.WriteLine(tr3.ReadLine()); Console.WriteLine(tr3.ReadLine());

        }




        // Raw reading of the files to control the contents at byte level
        FileStream fs1 = new FileStream("f-iso.txt", FileMode.Open),
                    fs2 = new FileStream("f-utf8.txt", FileMode.Open),
                    fs3 = new FileStream("f-utf16.txt", FileMode.Open);

        StreamReport(fs1, "Iso Latin 1");
        StreamReport(fs2, "UTF-8");
        StreamReport(fs3, "UTF-16");

        fs1.Close();
        fs2.Close();
        fs3.Close();
    }
    // end of file is \n\r
    public static void StreamReport(FileStream fs, string encoding)
    {
        Console.WriteLine();
        Console.WriteLine(encoding);
        int ch, i = 0;
        do
        {
            ch = fs.ReadByte();
            if (ch != -1&& (char)ch!='\n'&&(char)ch != '\r')
                Console.Write("{0,4}", ch);
            i++;
            if (i % 10 == 0) Console.WriteLine();
        } while (ch != -1);
        Console.WriteLine();
    }

}
