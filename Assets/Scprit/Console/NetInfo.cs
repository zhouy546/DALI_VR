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
        text.text = "MyPort: " + getPort() + "\n" + "IP: " + getAddress()+"\n"+"Target Port: "+ getTargetPort();
    }

    public string getTargetPort() {
        return ValueSheet.m_TargetPort.ToString();

    }

    public string getPort() {
        return ValueSheet.m_ReceivePort.ToString(); 
    }

    public string getAddress() {

            return Network.player.ipAddress.ToString();
        
    }
}
