using System;
using System.IO;
public class DirectoryInfoDemo
{
    public static void Main()
    {
        string fileName = "directory-info.cs"; // The current source file
                                               // Get the DirectoryInfo of the current directory
                                               // from the FileInfo of the current source file
        FileInfo fi = new FileInfo(fileName); // This source file
        DirectoryInfo di = fi.Directory;
        Console.WriteLine("File {0} is in directory \n {1}", fi, di);
        // Get the files and directories in the parent directory.
        FileInfo[] files = di.Parent.GetFiles();
        DirectoryInfo[] dirs = di.Parent.GetDirectories();
        // Show the name of files and directories on the console
        Console.WriteLine("\nListing directory {0}:", di.Parent);

        foreach (DirectoryInfo d in dirs)
            Console.WriteLine(d.Name);
        foreach (FileInfo f in files)
            Console.WriteLine(f);
    }
}