using System;
using System.IO;
using System.Linq;


namespace youknowcaliber
{
    internal sealed class Filemanager
    {

        // Remove directory
        public static void RecursiveDelete(string path)
        {
            DirectoryInfo baseDir = new DirectoryInfo(path);

            if (!baseDir.Exists) return;
            foreach (var dir in baseDir.GetDirectories())
                RecursiveDelete(dir.FullName);

            baseDir.Delete(true);
        }

        // Copy directory
        public static void CopyDirectory(string sourceFolder, string destFolder)
        {
            try
            {
                if (!Directory.Exists(destFolder))
                    Directory.CreateDirectory(destFolder);
                string[] files = Directory.GetFiles(sourceFolder);
                foreach (string file in files)
                {
                    string name = Path.GetFileName(file);
                    string dest = Path.Combine(destFolder, name);
                    File.Copy(file, dest);
                }
                string[] folders = Directory.GetDirectories(sourceFolder);
                foreach (string folder in folders)
                {
                    string name = Path.GetFileName(folder);
                    string dest = Path.Combine(destFolder, name);
                    CopyDirectory(folder, dest);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // Get directory size
        public static long DirectorySize(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            return dir.GetFiles().Sum(fi => fi.Length) +
                   dir.GetDirectories().Sum(di => DirectorySize(di.FullName));
        }
    }
}
