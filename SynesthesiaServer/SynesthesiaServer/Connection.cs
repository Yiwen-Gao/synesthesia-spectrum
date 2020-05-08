using System;
using System.Net.Sockets;
using System.Threading;

namespace SynesthesiaServer
{
    class Connection
    {
        public int port = 13000;

        private TcpListener server = null;
        private TcpClient client1 = null;
        private TcpClient client2 = null;

        public Connection(TcpListener server)
        {
            this.server = server;
        }

        public void WaitForConnection()
        {
            try
            {
                Console.WriteLine("Waiting for a connection");
                client1 = server.AcceptTcpClient();
                Console.WriteLine("Connected!");
                Console.WriteLine("Waiting for a second connection");
                client2 = server.AcceptTcpClient();
                Console.WriteLine("Connected!");
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetupListenerThreads()
        {
            NetworkStream stream1 = client1.GetStream();
            NetworkStream stream2 = client2.GetStream();
            try
            {
                byte[] firstPlayerMessage = System.Text.Encoding.ASCII.GetBytes("player:1!");
                stream1.Write(firstPlayerMessage);
                byte[] secondPlayerMessage = System.Text.Encoding.ASCII.GetBytes("player:2!");
                stream2.Write(secondPlayerMessage);
            }
            catch (Exception e)
            {

            }
            ThreadStart stream1Ref = new ThreadStart(() => { CheckForData(stream1, stream2); });
            ThreadStart stream2Ref = new ThreadStart(() => { CheckForData(stream2, stream1); });
            Thread stream1Thread = new Thread(stream1Ref);
            Thread stream2Thread = new Thread(stream2Ref);
            stream1Thread.Start();
            stream2Thread.Start();
        }

        void CheckForData(NetworkStream inStream, NetworkStream outStream)
        {
            try
            {
                byte[] bytes = new byte[256];
                int i;
                while ((i = inStream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);
                    outStream.Write(bytes, 0, i);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}
