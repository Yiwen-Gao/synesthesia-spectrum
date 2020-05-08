using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using UnityEngine.SceneManagement;

public class NetworkClient : MonoBehaviour
{
    public int PlayerNum { get { return playerNum; } }

    public int port = 13000;
    public string serverAddr = "127.0.0.1";
    StringBuilder messageBuilder;

    private TcpClient client = null;
    private NetworkStream stream = null;

    private Dictionary<string, Action<string>> callbacks = new Dictionary<string, Action<string>>();

    public bool offlineMode;

    public string firstLevel;
    private int playerNum = 1;

    // Start is called before the first frame update
    public void StartGame()
    {
        if (FindObjectsOfType<NetworkClient>().Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            messageBuilder = new StringBuilder();
            AttemptConnection();
            if (!offlineMode)
            {
                RegisterCallback("player", (string id) => {
                    playerNum = int.Parse(id);
                    SceneManager.LoadScene(firstLevel);
                });
            }
            else
            {
                SceneManager.LoadScene(firstLevel);
            }
        }
    }

    public void SendMessageNetwork(string message, string argument="")
    {
        if (offlineMode)
        {
            return;
        }
        try
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(message + ":" + argument + "!");
            stream.Write(data, 0, data.Length);
        }
        catch (SocketException e)
        {
            Debug.LogException(e, this);
        }
    }

    private void ReceiveMessageNetwork(string message)
    {
        string[] messages = message.Split(':');
        if (callbacks.ContainsKey(messages[0]))
        {
            try
            {
                if (messages.Length >= 2)
                {
                    callbacks[messages[0]].DynamicInvoke(messages[1]);
                }
                else
                {
                    callbacks[messages[0]].DynamicInvoke("");
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
            }
        }
    }

    public void RegisterCallback(string name, Action<string> func)
    {
        callbacks[name] = func;
    }

    private void AttemptConnection()
    {
        if (offlineMode)
        {
            Debug.Log("Playing in offline mode.");
            return;
        }
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
            Debug.Log("Failed to connect, playing in offline mode.");
            offlineMode = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (offlineMode)
        {
            return;
        }
        try
        {
            if (stream != null && stream.CanRead)
            {
                byte[] readBuffer = new byte[1024];
                int numBytes = 0;

                while (stream.DataAvailable)
                {
                    numBytes = stream.Read(readBuffer, 0, readBuffer.Length);

                    messageBuilder.AppendFormat("{0}", Encoding.ASCII.GetString(readBuffer, 0, numBytes));
                }
                if (messageBuilder.ToString().EndsWith("!"))
                {
                    messageBuilder.Length--;
                    Debug.Log("Received a message: " + messageBuilder);
                    string[] commands = messageBuilder.ToString().Split('!');
                    foreach (string command in commands)
                    {
                        ReceiveMessageNetwork(command);
                    }
                    messageBuilder = new StringBuilder();
                }
            }
        }
        catch (SocketException e)
        {
            Debug.LogException(e, this);
        }
    }
}
