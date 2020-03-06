using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class NetworkClient : MonoBehaviour
{
    public int port = 13000;
    public string serverAddr = "127.0.0.1";

    private TcpClient client = null;
    private NetworkStream stream = null;

    // Start is called before the first frame update
    void Start()
    {
        AttemptConnection();
    }

    private void AttemptConnection()
    {
        try
        {
            IPAddress localAddr = IPAddress.Parse(serverAddr);

            Debug.Log("Attempting to connect");
            client = new TcpClient(serverAddr, port);
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
            if (stream != null && stream.CanRead)
            {
                byte[] readBuffer = new byte[1024];
                StringBuilder message = new StringBuilder();
                int numBytes = 0;

                while (stream.DataAvailable)
                {
                    numBytes = stream.Read(readBuffer, 0, readBuffer.Length);

                    message.AppendFormat("{0}", Encoding.ASCII.GetString(readBuffer, 0, numBytes));
                }
                byte[] data = System.Text.Encoding.ASCII.GetBytes("hello world");
                stream.Write(data, 0, data.Length);
            }
        }
        catch (SocketException e)
        {
            Debug.LogException(e, this);
        }
    }
}
