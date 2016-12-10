using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BrunelServer
{
    class ActionHandler
    {
        /* This class will handle the incoming commands from the client
         * and dispatch the desired transations
         * Withdraw and deposit are functionality from Account class
         * Transfer will call withdraw from account 1 and deposit from account 2
         * 
         * deposit/withdraw format:  withdraw;A;50
         * above means, withdraw 50 from account A
         * 
         * transfer format:  transfer;A;B;50
         * above means, transfer 50 from A to B
         */ 
        public static void ParseClientCommand(string command)
        {
            Console.WriteLine("parsing client command: " + command);
            string[] commands = command.Split(';');

            if (commands[0] != "transfer")
            {
                Account targetacc = GetAccount(commands[1]);
                targetacc.acquireLockAndExecute(commands[0], new string[] { commands[2] });
            }
            if (commands[0] == "transfer")
            {
                Account sourceacc = GetAccount(commands[1]);
                Account targetacc = GetAccount(commands[2]);
                sourceacc.acquireLockAndExecute("withdraw", new string[] { commands[3] });
                targetacc.acquireLockAndExecute("deposit", new string[] { commands[3] });
            }
        }
        private static Account GetAccount(string accountletter)
        {
            Account account = null;
            foreach (Account a in Program.accounts)
            {
                if (a.getName() == accountletter)
                {
                    account = a;
                }
            }
            return account;
        }
    }
}
