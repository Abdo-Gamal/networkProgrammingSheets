using System;
using System.IO;
public class FileInfoDemo
{
    public static void Main()
    {
        // Setting up file names
        string fileName = "file-info.cs", fileNameCopy = "file-info-copy.cs";
        // Testing file existence
        FileInfo fi = new FileInfo(fileName); // this source file
        Console.WriteLine("{0} does {1} exist", fileName, fi.Exists ? "" : "not");
        // Show file info properties:
        Console.WriteLine("DirectoryName: {0}", fi.DirectoryName);
        Console.WriteLine("FullName: {0}", fi.FullName);
        Console.WriteLine("Extension: {0}", fi.Extension);
        Console.WriteLine("Name: {0}", fi.Name);
        Console.WriteLine("Length: {0}", fi.Length);
        Console.WriteLine("CreationTime: {0}", fi.CreationTime);
        // Copy one file to another
        fi.CopyTo(fileNameCopy);
        FileInfo fiCopy = new FileInfo(fileNameCopy);
   // Does the copy exist?
          Console.WriteLine("{0} does {1} exist", fileNameCopy, fiCopy.Exists ? "" : "not");
        // Delete the copy again
        fiCopy.Delete();
        // Does the copy exist?
        Console.WriteLine("{0} does {1} exist",
        fileNameCopy, fiCopy.Exists ? "" : "not"); // !!??
                                                   // Create new FileInfo object for the copy
        FileInfo fiCopy1 = new FileInfo(fileNameCopy);
        // Check if the copy exists?
        Console.WriteLine("{0} does {1} exist", fileNameCopy, fiCopy1.Exists ? "" : "not");
        // Achieve a TextReader (StreamReader) from the file info object
        // and echo the first ten lines in the file to standard output
        using (StreamReader sr = fi.OpenText())
        {
            for (int i = 1; i <= 10; i++)
                Console.WriteLine(" " + sr.ReadLine());
        }
    }
}