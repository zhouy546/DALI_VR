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

    public static SendUPDData instance;

    public string udpData_str;
    string _sSend = "";

    //[Tooltip("接受端口号")] public int m_ReceivePort = 29010;//接收的端口号 
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

    }

    public void initialization() {
        if (instance == null)
        {
            instance = this;
        }

        m_ip = Network.player.ipAddress;


    }

    // Update is called once per frame
    void Update()
    {
       udpData_str = CreateMessage();
       
       _sSend = udpData_str;

        udp_Send(_sSend, m_ip, ValueSheet.m_TargetPort);
       //StartCoroutine(Sent());
    }

    IEnumerator Sent() {

        udp_Send(_sSend, m_ip, ValueSheet.m_TargetPort);

        yield return new WaitForSeconds(25 / 1000);

    }


    string CreateMessage() {
        string splitChar = "%";

        string x = UtilityFunction.Mapping(ValueSheet.CamRotation.x,ValueSheet.Cam_X_RotMinium, ValueSheet.Cam_X_RotMaxium, -1, 1).ToString("0.000");

        string y = UtilityFunction.Mapping(ValueSheet.CamRotation.y, ValueSheet.Cam_Y_RotMinium, ValueSheet.Cam_Y_RotMaxium, -1, 1).ToString("0.000");

        string z = UtilityFunction.Mapping(ValueSheet.CamRotation.z, ValueSheet.Cam_Z_RotMinium, ValueSheet.Cam_Z_RotMaxium, -1, 1).ToString("0.000");

        string str = x+ splitChar+y+ splitChar+z;

            return str;

    }

    private void OnGUI()
    {
        GUIStyle bb = new GUIStyle();
        bb.normal.background = null;    //这是设置背景填充的
        bb.normal.textColor = new Color(1.0f, 0.5f, 0.0f);   //设置字体颜色的
        bb.fontSize = 40;       //当然，这是字体大小

        //居中显示FPS
        GUI.Label(new Rect((Screen.width / 2) - 40, 0, 200, 200), "udp message: " + _sSend, bb);

    }
}