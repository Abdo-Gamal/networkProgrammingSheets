using System;
using System.IO;
public class BinaryWriteSimpleTypes
{
    public static void Main()
    {
        string fn = "simple-types.bin";
        using (BinaryWriter bw =
        new BinaryWriter(new FileStream(fn, FileMode.Create)))
        {
            bw.Write(5); // 4 bytes
            bw.Write(""); // 1 bytes
            bw.Write(5.5); // 8 bytes
            bw.Write(5555M); // 16 bytes (decimal dt)
            bw.Write(5 == 6); // 1 bytes
        }
        FileInfo fi = new FileInfo(fn);
        Console.WriteLine("Length of {0}: {1}", fn, fi.Length);
    }
}