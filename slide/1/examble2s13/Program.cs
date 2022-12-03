using System;
using System.IO;
public class CopyApp
{
    public static void Main(string[] args)
    {
        FileCopy(args[0], args[1]);
    }
    public static void FileCopy(string fromFile, string toFile)
    {
        try
        {
            using (FileStream FromFile = new FileStream(fromFile, FileMode.Open))
            {
                using (FileStream CopyFile = new FileStream(toFile, FileMode.Create))
                {
                    int cur= FromFile.ReadByte();
                    while (cur!= -1)
                    {
                        CopyFile.WriteByte((byte)cur);
                    }

                }
            }

        
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("File {0} not found: ", e.FileName);
            throw;
        }
        catch (Exception)
        {
            Console.WriteLine("Other file copy exception");
            throw;
        }
    }
}