using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;

public class Client : MonoBehaviour
{

    public string default_host = "10.100.41.154";
    public int default_port = 8888;

    // Start is called before the first frame update
    void Start()
    {

        // use previous saved ip address if it exits
        string previousHost = PlayerPrefs.GetString("host");
        if(previousHost != null)
            default_host = previousHost;

        ClientConn.Connect(default_host, default_port);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.touchCount > 0)
        //{
        //    ClientConn.Send("Toimiiko");
        //}

    }
}

public static class ClientConn
{

    public static string host;
    public static int port;
    static Socket socket = null;

    public static void Connect(string _host, int _port)
    {
        if (socket != null) socket.Close();
        host = _host;
        port = _port;
        PlayerPrefs.SetString("host", host); // save

        Socket s = null;
        IPAddress ipAddress = IPAddress.Parse(host);

        IPEndPoint ipe = new IPEndPoint(ipAddress, port);

        s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        s.Connect(ipe);

        if (!s.Connected)
        {
            Debug.LogError("IOT: NOT CONNECTED");
            s = null;
        }
        else
        {
            Debug.Log("IOT: Connected");
        }

        ClientConn.socket = s;
    }

    public static void Send(string txt)
    {
        byte[] data = Encoding.ASCII.GetBytes(txt);
        socket.Send(data);
    }

}


