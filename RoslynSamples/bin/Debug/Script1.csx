using System;
using System.Diagnostics;
using System.Threading;

string userName = Environment.UserName;
Console.WriteLine("Logged in user - " + userName);
Process[] procs = Process.GetProcesses();
if (procs != null && procs.Length > 0)
{
    Console.WriteLine("Total Process Count = " + procs.Length);
    for (int i = 0; i < procs.Length; i++)
    {
        Console.WriteLine(procs[i].ProcessName);
        Thread.Sleep(1000);
    }
}