using System;
using System.IO;
public class DirectoryDemo
{

    public static void FileCopy(string fromFile)
    {
        //حل المعيدة 
        using (Stream fromStream = new FileStream(fromFile, FileMode.Open))
        {
            long strmLgt = fromStream.Length;
            Console.WriteLine(strmLgt);
            byte[] strmContent = new byte[strmLgt];          // A byte array large enough to 
                                                             // hold the fromFile.

            int totalBytesRead = 0,
                bytesRead,
                n = 0;

            const int chuckSize = 100;

            do
            {
                bytesRead = fromStream.Read(strmContent, totalBytesRead, Math.Min(chuckSize, (int)strmLgt - totalBytesRead));
                totalBytesRead += bytesRead;
                Console.WriteLine(".." + totalBytesRead);
                Console.WriteLine(bytesRead);
                n++;
            } while (totalBytesRead < strmLgt && bytesRead > 0);

            foreach (byte bt in strmContent) Console.Write((char)bt);

            Console.WriteLine();
            Console.WriteLine("Number of calls to Read: {0}", n);
        }
    }

    public static void Main()
    {
        string fileName = "binarystreams.txt"; // The current source file
        using (FileStream str =
            new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            int len = (int)str.Length;
            byte[] buffer = new byte[len];
            int byteToRead = len;
            int byteReaded = 0;
            int chuncksize = 3;
            while (byteToRead > 0)
            {
                int n = str.Read(buffer, byteReaded, chuncksize);//مش  هيمسح ال buffer هيحط  عليها 
                byteToRead -= n;
                byteReaded += n;

            }
            foreach (byte bt in buffer) Console.Write((char)bt);

        }

    }
}