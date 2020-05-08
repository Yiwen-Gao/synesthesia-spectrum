using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;

namespace SynesthesiaServer
{
    class Program
    {
        public static int port = 13000;

        static void Main()
        {
            IPAddress localAddr = IPAddress.Any;

            TcpListener server = new TcpListener(localAddr, port);
            server.Start();

            ArrayList connections = new ArrayList();

            while(true)
            {
                Connection c = new Connection(server);
                c.WaitForConnection();
                c.SetupListenerThreads();
                connections.Add(c);
            }
        }
    
}
}
