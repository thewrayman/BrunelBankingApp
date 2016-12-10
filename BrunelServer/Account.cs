using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;

namespace BrunelServer
{
    class Account
    {

        /* Data structure for account actions
         * withdraw: string "withdraw", string[]{amount}
         * deposit:  string "deposit",  string[]{amount}
         * 
         * 
         * 
         * 
         *
         */ 
        private Object thisLock = new Object();
        private Form1 form;
        private string threadname;
        private double balance;
        private bool accessing = false;
        private int threadswaiting = 0;
        private string name = null;

        //https://msdn.microsoft.com/en-us/library/c5kehkcz.aspx
        public Account(double amount, Form1 formobj, string name)
        {
            balance = amount;
            this.form = formobj;
            this.name = name;
            updateDetails();
            form.UpdateStatus("Created a new account: " + name + ":" + amount);
        }

        private void updateDetails()
        {
            form.UpdateAccountBalance(balance, name);
        }

        public void acquireLockAndExecute(string command, string[] args)
        {
            Console.WriteLine(command);
            Thread me = Thread.CurrentThread;
            Console.WriteLine(me.Name + " attempting to aquire lock");
            threadswaiting++;

            while (accessing)
            {
                Console.WriteLine(me.Name + " waiting to get a lock..");
            }

            //here we aquire the lock of the account
            //whilst locked, it will execute the desired action
            //releases lock on finish of action
            threadswaiting--;
            lock (thisLock)
            {
                    accessing = true;
                    Console.WriteLine(me.Name + " got a lock");

                    //use reflection to take in the command and 
                    //execute the desired action with the args provided
                    Type thisType = this.GetType();
                    MethodInfo theMethod = thisType.GetMethod(command);                  
                    theMethod.Invoke(this, new object[] { args });
            }
            Console.WriteLine("released");

        }

        public void releaseLock()
        {
            Console.WriteLine("releasing lock");
            accessing = false;
            Monitor.PulseAll(thisLock);
        }

        public double withdraw(string[] args)
        {
            int amount = int.Parse(args[0]);
            if (accessing)
            {
                if (balance >= amount)
                {
                    Console.WriteLine("Balance before Withdrawal :  " + balance);
                    Console.WriteLine("Amount to Withdraw        : -" + amount);
                    balance = balance - amount;
                    Console.WriteLine("Balance after Withdrawal  :  " + balance);
                    form.UpdateStatus("withdrew " + amount + " from " + name + ".\n" + "New balance: " + balance);
                    updateDetails();
                    Thread.Sleep(10000);
                    releaseLock();
                    return amount;
                }
                else
                {
                    return -1; // transaction rejected
                }
            }
            else
            {
                return -1;
            }
        }

        public double deposit(string[] args)
        {
            Console.WriteLine("in deposit");
            int amount = int.Parse(args[0]);
            if (accessing)
            {
                Console.WriteLine("Balance before Deposit :  " + balance);
                Console.WriteLine("Amount to Deposit        : -" + amount);
                balance = balance + amount;
                Console.WriteLine("Balance after Deposit  :  " + balance);
                form.UpdateStatus("\nDeposited " + amount + " to " + name + ".\n" + "New balance: " + balance);
                updateDetails();
                releaseLock();
                return amount;
            }
            else
            {
                return -1;
            }
        }

        public double getBalance()
        {
            return balance;
        }
        public string getName()
        {
            return name;
        }


    }
}
