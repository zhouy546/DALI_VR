using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;

/// <summary>
///发送UDP字符串udpData_str
/// </summary>
public class SendUPDData : MonoBehaviour {

    public string udpData_str;
    [Tooltip("接受端口号")] public int m_ReceivePort = 29010;//接收的端口号 
    Socket udpserver = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    private string m_ip;//定义一个IP地址

    public bool udp_Send(string da, string ip, int port)
    {
        try
        {
            //设置服务IP，设置端口号
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);
            //发送数据
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(da);
            udpserver.SendTo(data, data.Length, SocketFlags.None, ipep);
            return true;
        }
        catch
        {
            return false;
        }
    }

    // Use this for initialization
    void Start()
    {
        m_ip = Network.player.ipAddress;
    }

    // Update is called once per frame
    void Update()
    {
        string _sSend = "";
        _sSend = udpData_str;
        udp_Send(_sSend, m_ip, m_ReceivePort);
    }
}