using System;
using System.IO;
using System.Threading;

class MThread_APP
{
    // used to indicate which thread we are in
    private string _threadOutput = "";
    private bool _stopThreads = false;
    public MThread_APP()
    {
        Thread thread1 = new Thread(new ThreadStart(DisplayThread1));
        Thread thread2 = new Thread(new ThreadStart(DisplayThread2));
        // start them
        thread1.Start();
        thread2.Start();
    }
    void DisplayThread1()
    {
        while (_stopThreads == false)
        {
            // lock on the current instance of the class for thread #1
            lock (this)
            {
                Console.WriteLine("Display Thread 1");
                _threadOutput = "Hello Thread1";
                Thread.Sleep(1000); // simulate a lot of processing
                                    // tell the user what thread we are in thread #1
                Console.WriteLine("Thread 1 Output --> {0}", _threadOutput);
            }// lock released for thread #1
         
        }
    }

    void DisplayThread2()
    {
        while (_stopThreads == false)
        {
            // lock on the current instance of the class for thread #2
            lock (this)
            {
                Console.WriteLine("Display Thread 2");
                _threadOutput = "Hello Thread2";
                Thread.Sleep(1000); // simulate a lot of processing
                                    // tell the user what thread we are in thread #1
                Console.WriteLine("Thread 2 Output --> {0}", _threadOutput);
            } // lock released for thread #2 here
        }
    }
}
class MainClass
{
    public static void Main()
    {
        new MThread_APP();
    }
}