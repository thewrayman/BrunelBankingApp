using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace BrunelServer
{
    public class ServerThread
    {
        private TcpClient socket = null;
        private string ThreadName;
        private double SharedVariable;

        public ServerThread(TcpClient socket, String ThreadName)
        {
            this.socket = socket;
            this.ThreadName = ThreadName;
        }

        public void start()
        {
            byte[] input = new Byte[2048];
            string datafromclient = null;
            ActionHandler ah = new ActionHandler();
            Console.WriteLine(ThreadName + " initialising..");
            NetworkStream networkStream = null;
            try
            {
                networkStream = socket.GetStream();
            }
            catch (InvalidOperationException){
                Console.WriteLine("failed to get stream");
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("get stream with " + input.Length);
                    networkStream.Read(input, 0, input.Length);
                    Console.WriteLine("trying to read");
                    datafromclient = System.Text.Encoding.ASCII.GetString(input);
                    datafromclient = datafromclient.Replace("\0", string.Empty);
                    Console.WriteLine("command from client: " + datafromclient);
                    ParseClientCommand(datafromclient);
                    Console.WriteLine("sent to parse data: " + datafromclient);

                }
                catch (Exception e)
                {
                    Console.WriteLine("failed in try loop" + e);
                }
            }

            

        }

        public void ParseClientCommand(string command)
        {
            Console.WriteLine("parsing client command: ");
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
        private Account GetAccount(string accountletter)
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
