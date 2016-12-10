using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace BrunelClient
{
    public class BrunelClient
    {
        TcpClient clientsocket = new TcpClient();
        int targetport = 4545;
        string ServerName = "127.0.0.1";
        string clientname = "client";

        public BrunelClient() { }
        public BrunelClient(int port)
        {
            targetport = port;
        }

        public void start()
        {
            byte[] fromserver = new byte[2048];
            byte[] fromclient = new byte[2048];
            string datafromserver = null;
            string datafromclient = null;
            NetworkStream networkStream = null;
            try
            {
                clientsocket.Connect(ServerName, targetport);
                Console.WriteLine("connected to server");
                networkStream = clientsocket.GetStream();
                
            }
            catch(Exception e)
            {
                Console.WriteLine("failed to connect to server"+e);
            }

            while (true)
            {
                Console.WriteLine("input your command");
                datafromclient = Console.ReadLine();
                if(datafromclient != null)
                {
                    Console.WriteLine("sending data to server: " + datafromclient);
                    fromclient = Encoding.ASCII.GetBytes(datafromclient);
                    networkStream.Write(fromclient, 0, fromclient.Length);
                    networkStream.Flush();
                    Console.WriteLine("data sent");
                }
                //networkStream.Read(fromserver, 0, (int)clientsocket.ReceiveBufferSize);
                //datafromserver = System.Text.Encoding.ASCII.GetString(fromserver);
                //Console.WriteLine("got from server: " + datafromserver);
            }
            
        }


    }
}
