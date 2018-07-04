using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NetInfo : MonoBehaviour {
    public Text text;
	// Use this for initialization
	void Start () {
		
	}

    public void OnEnable()
    {
        text.text = "Port :" + getPort() + "\n" + "IP :" + getAddress();
    }

    public string getPort() {
        return GetUDPMessage.m_ReceivePort.ToString(); 
    }

    public string getAddress() {

            return Network.player.ipAddress.ToString();
        
    }
}
