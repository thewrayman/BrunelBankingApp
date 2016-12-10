using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;

namespace BrunelServer
{
    class BrunelServer
    {
        int ListenPort = 4545;
        String ServerName = "BrunelServer";
        Boolean listening = true;

        TcpListener serversocket = null;
        TcpClient clientsocket = null;

        Form1 formobj;


        public BrunelServer(Form1 form)
        {
            Console.WriteLine("CREATED BS OBJ");
            formobj = form;
            System.Threading.Thread.Sleep(1000);
            form.UpdateStatus("created obj");            
        }
        public BrunelServer(Form1 form, int port)
        {
            ListenPort = port;
        }

        public void Listen()
        {
            try
            {
                serversocket = new TcpListener(IPAddress.Any, ListenPort);
                serversocket.Start();
                clientsocket = default(TcpClient);
                
            }
            catch (Exception e)
            {
                Console.WriteLine("could not start server on port " + ListenPort);
                formobj.UpdateStatus("could not start server on port");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine(e);
                Environment.Exit(5);
            }
            Console.WriteLine("Successfully started server");
            formobj.UpdateStatus("started listening"+"\n");

            int clients = 0;

            while (listening)
            {
                clients += 1;
                clientsocket = serversocket.AcceptTcpClient();
                Console.WriteLine("Accepted client number " + clients);
                formobj.UpdateStatus("Accepted client number " + clients);
                ServerThread client = new ServerThread(clientsocket, clients.ToString());
                Thread clientthread = new Thread(() => client.start());
                clientthread.Start();

            }
            

        }


    }
}
