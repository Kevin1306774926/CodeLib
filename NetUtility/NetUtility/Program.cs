using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace NetUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            //Gets the machine names that are connected on LAN
            Process netUtility = new Process();
            netUtility.StartInfo.FileName = "net.exe";
            netUtility.StartInfo.CreateNoWindow = true;
            netUtility.StartInfo.Arguments = "view";
            netUtility.StartInfo.RedirectStandardOutput = true;
            netUtility.StartInfo.UseShellExecute = false;
            netUtility.StartInfo.RedirectStandardError = true;
            netUtility.Start();

            StreamReader streamReader = new StreamReader(netUtility.StandardOutput.BaseStream, netUtility.StandardOutput.CurrentEncoding);
            string line = "";
            while((line=streamReader.ReadLine())!=null)
            {
                if(line.StartsWith("\\"))
                {
                    Console.WriteLine(line.Substring(2).Substring(0, line.Substring(2).IndexOf(" ")).ToUpper());
                }
            }
            streamReader.Close();

            IPHostEntry myIPHostEntry = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress myIPAddress in myIPHostEntry.AddressList)
            {
                Console.WriteLine(myIPAddress.ToString());
            }

            Console.ReadKey();
            netUtility.WaitForExit(1000);
        }
    }
}
