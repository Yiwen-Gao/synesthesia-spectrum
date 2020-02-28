using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class NetworkHost : MonoBehaviour
{
    public int port=13000;

    private TcpListener server = null;
    private TcpClient client = null;
    private NetworkStream stream = null;

    void Start()
    {
        WaitForConnection();
    }

    private void WaitForConnection()
    {
        try
        {
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            server = new TcpListener(localAddr, port);

            Debug.Log("Waiting for a connection");
            client = server.AcceptTcpClient();
            Debug.Log("Connected!");

            stream = client.GetStream();
        }
        catch (SocketException e)
        {
            Debug.LogException(e, this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (stream.CanRead)
            {
                byte[] readBuffer = new byte[1024];
                StringBuilder message = new StringBuilder();
                int numBytes = 0;

                while(stream.DataAvailable)
                {
                    numBytes = stream.Read(readBuffer, 0, readBuffer.Length);

                    message.AppendFormat("{0}", Encoding.ASCII.GetString(readBuffer, 0, numBytes));
                }
            }
        }
        catch (SocketException e)
        {
            Debug.LogException(e, this);
        }
    }
}
