using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace BrunelServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static Form1 mainform = null;
        public static List<Account> accounts = new List<Account>();
        static void Main()
        {
            Console.WriteLine("starting app..");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainform = new Form1();
            Thread opthread = new Thread(() => Application.Run(mainform));
            opthread.Start();
            initialise();

            
        }
        static void initialise()
        {
            Account aa = new Account(100, mainform, "A");
            accounts.Add(aa);
            Account ab = new Account(200, mainform, "B");
            accounts.Add(ab);
            BrunelServer bs = new BrunelServer(mainform);
            Account ac = new Account(300, mainform, "C");
            accounts.Add(ac);
            bs.Listen();
            
            //ac.acquireLockAndExecute("withdraw", new string[] { "50"});
            //Thread.Sleep(5000);
            //ac.acquireLockAndExecute("deposit", new string[] { "200" });
        }
    }
}
