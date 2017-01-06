using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading;
using BrunelServer;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        const string clientexe = "c:\\test\\BrunelClient.exe";

        private List<Process> runningclients = new List<Process>();


        [TestMethod]
        public void WithdrawMultiClient()
        {
            startServer();
            LaunchAllClients();
            List<string> startingbalances = GetBalance();
            //should be: A-100;B-200;C-300;D-400  starting balance
            string[] withdrawcommands = {
                "withdraw;A;50",
                "withdraw;B;60",
                "withdraw;C;70",
                "withdraw;D;80"};
            int sent = SendCommand(runningclients, withdrawcommands);

            if(sent == 0)
            {
                Thread.Sleep(1000);
                List<string> newbalances = GetBalance();
                bool success = CorrectBalance(new int[] { 50, 140, 230, 320 }, newbalances);
            }
        }


        [TestMethod]
        public void DepositMultiClient()
        {
            startServer();
            LaunchAllClients();
            List<string> startingbalances = GetBalance();

            string[] depositcommands =
            {
                "deposit;A;100",
                "deposit;B;90",
                "deposit;C;80",
                "deposit;D;70"
            };
            Thread.Sleep(10000);
            int sent = SendCommand(runningclients, depositcommands);
            
            if (sent == 0)
            {
                Thread.Sleep(1000);
                List<string> newbalances = GetBalance();
                bool success = CorrectBalance(new int[] { 200, 290, 380, 470 }, newbalances);
            }
        }

        [TestMethod]
        public void TransferMultiClient()
        {
            startServer();
            LaunchAllClients();
            List<string> startingbalances = GetBalance();

            string[] trasnfercommands =
            {
                "transfer;A;B;50",
                "transfer;C;D;150"
            };
            int sent = SendCommand(runningclients, trasnfercommands);
            if (sent == 0)
            {
                Thread.Sleep(1000);
                List<string> newbalances = GetBalance();
                bool success = CorrectBalance(new int[] { 50, 250, 150, 550 }, newbalances);
            }
        }

        [TestMethod]
        public void Deadlock()
        {

        }


        [TestCleanup]
        public void Teardown()
        {
            runningclients = new List<Process>();
            KillInstances("BrunelClient");
            KillInstances("BrunelServer");
            
        }



        private void CreateClient(int x)
        /*
         * X = number of clients to launch
         * Creates a new client and waits 0.5s
         */
        {
            for (int i = 0; i < x; i++)
            {
                var p = new Process();
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.FileName = clientexe;
                p.StartInfo.UseShellExecute = false;
                p.Start();
                runningclients.Add(p);
                Thread.Sleep(500);
            }

        }

        private void LaunchAllClients()
        /*
         * Launch 4 clients
         */
        {
            CreateClient(4);
            Trace.WriteLine("Launched 4 clients");
        }

        private void KillInstances(string exe)
        /*
         * Takes a name, i.e "BrunelClient"
         * Will exit all processes of that name
         */
        {
            Process[] proceses = Process.GetProcessesByName(exe);
            foreach (Process p in proceses)
            {
                p.Kill();
            }
        }

        private void startServer()
        /*
         * Starts the server
         */
        {
            Trace.WriteLine("starting server..");
            Thread opthread = new Thread(() => Program.Main());
            opthread.Start();
            Trace.WriteLine("server started.");
            Thread.Sleep(1000);//time for initialisation
        }



        private Process[] GetRunningClients()
        /*
         * Returns a list of the all the running clients
         * This can then be used to pass commands to each client
         */
        {
            Process[] clients = null;
            clients = Process.GetProcessesByName("BrunelClient");
            return clients;
        }


        private int SendCommand(List<Process> clients, string[] commands)
            /*
             * Takes a list of clients and list of commands
             * length clients = length commands
             * passes command to client
             */ 
        {
            Trace.WriteLine("Sending commands..");
            if (!(clients.Count >=1)){
                Trace.WriteLine("no clients");
                return 1;
            }
            else
            {
                for(int i=0; i < commands.Length;i++)
                {
                    var streamWriter = clients[i].StandardInput;
                    streamWriter.WriteLine(commands[i]);
                    streamWriter.Close();
                }
                Trace.WriteLine("finished sending commands");
                return 0;
            }

        }

        private List<string> GetBalance(string[] name = null)
            /*
             * takes in an optional list of account names
             * for each account provided, will fetch the balance
             * returns list format: A,50,B,100,C,500....
             */ 
        {
            List<string> accountInfo = new List<string>();
            string[] accountnames = name;
            List<Account> accounts = Program.accounts;
            if (accountnames == null)
            {
                accountnames = new string[] { "A", "B", "C", "D" };
            }
            foreach (string accname in accountnames)
            {
                foreach(Account a in accounts)
                {
                    if (accname.Equals(a.getName()))
                    {
                        accountInfo.Add(accname);
                        accountInfo.Add(a.getBalance().ToString());
                    }
                }
            }
            Trace.WriteLine("Balances:");
            for(int i = 0; i < accountInfo.Count; i += 2)
            {
                Trace.WriteLine(accountInfo[i]+":"+accountInfo[i+1]);
            }
            return accountInfo;
        }

        private bool CorrectBalance(int[] expected, List<string> actual)
            /*
             * Takes in the expected balances for the accounts and the returned current balance
             * Compares expected against actual to verify the correct balances are maintained
             */ 
        {
            bool correct = true;
            for(int i=0; i < expected.Length; i++)
            {
                Trace.WriteLine("evalutating " + expected[i] + " and " + actual[(2*i) + 1]);
                if (expected[i] == int.Parse(actual[(2*i) + 1]))
                {
                    Trace.WriteLine("Balance " + i + " is correct: " + actual[(2 * i) + 1]);
                }
                else
                {
                    return false;
                }
            }

            return correct;
        }
    }
}
